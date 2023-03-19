Imports Utilities
Public Class ctrlFormPrinters

#Region "Properties & Variables"

    Private Property SQLInfo As SQLConnectionUtilities

    Private Property PrintMgmt As PrinterMgmt
    Private Property OwningForm As Form
    Private Property selectedPrinter As PrinterInfo

#End Region

#Region "Init"

    Public Sub New(ByVal sqlConnUtils As SQLConnectionUtilities, Optional ByVal OwningForm As Form = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        SQLInfo = sqlConnUtils
        PrinterGui.flowPanel = flowPrinters

        Me.OwningForm = OwningForm
        PrintMgmt = New PrinterMgmt(SQLInfo.sqlConnStr)
        PopulateTrees()
    End Sub



#End Region

#Region "Methods"


    ''' <summary>
    ''' loads the printer selection trees.
    ''' </summary>
    Private Sub PopulateTrees()
        'there are 2 main trees that get populated: categories, and trees
        'clears form
        flowPrinters.Controls.Clear()
        treePrinters.Nodes.Clear()
        treeCategories.Nodes.Clear()

        selectedPrinter = Nothing

        'keeps track of the different descriptions / categories
        Dim categories As New List(Of PrinterCategory)

        PrintMgmt.updatePrinters()


        'adds the printers to the printers node
        For Each p As PrinterInfo In PrintMgmt.Printers
            Dim pNode As New TreeNode(p.PrinterName)
            treePrinters.Nodes.Add(pNode)
            For Each q As PrinterQueueInfo In p.Queues
                If Not categories.Contains(q.QueueCategory) Then
                    categories.Add(q.QueueCategory)
                End If
            Next
        Next

        categories.Sort()


        'adds nodes to category node
        Dim byCat As New TreeNode("By Category")
        'Dim Categories As Array = System.Enum.GetValues(GetType(PrinterCategory))
        For Each category As PrinterCategory In categories
            byCat.Nodes.Add(category.ToString.Replace("_", " "))
        Next

        'adds nodes to printer node
        Dim byPrt As New TreeNode("By Printer")
        For Each p As PrinterInfo In PrintMgmt.Printers
            byPrt.Nodes.Add(p.PrinterName)
        Next

        'adds the nodes to the categories node
        treeCategories.Nodes.Add("All")
        treeCategories.Nodes.Add(byCat)
        treeCategories.Nodes.Add(byPrt)
        treeCategories.ExpandAll()

        treeCategories.SelectedNode = treeCategories.Nodes(0)

    End Sub



    ''' <summary>
    ''' shows the queues of the printer selected
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treePrinters.AfterSelect
        selectedPrinter = Nothing
        flowPrinters.Controls.Clear()

        PrintMgmt.updatePrinters()

        For Each p As PrinterInfo In PrintMgmt.Printers
            If p.PrinterName = treePrinters.SelectedNode.Text Then
                'adds the printer info, as well as all of the queues it has
                flowPrinters.Controls.Add(p.CreateEditBox)
                selectedPrinter = p
                For Each q As PrinterQueueInfo In p.Queues
                    flowPrinters.Controls.Add(q.CreateEditBox)
                    If Not p.chkPIsColor.Checked Then
                        q.ChkQIsColor.Enabled = False
                        q.ChkQIsColor.Checked = False
                    End If

                Next
                Exit Sub
            End If
        Next

    End Sub



    ''' <summary>
    ''' goes through printer names first, then matches against the queue category
    ''' </summary>
    ''' <param name="CategoryOrPrinterName"></param>
    Private Sub showCategoriesOfPrinters(Optional ByVal CategoryOrPrinterName As String = "")
        flowPrinters.Controls.Clear()
        PrintMgmt.updatePrinters()

        Dim foundMatch As Boolean = False

        If CategoryOrPrinterName = "" Then
            'show all printers
            foundMatch = True
            For Each p As PrinterInfo In PrintMgmt.Printers
                Dim gb As New GroupBox
                Dim gbFont As Font = gb.Font
                gb.Font = New Font(gbFont.Name, gbFont.Size, FontStyle.Bold)
                Dim ct As Integer = 0
                gb.Text = p.PrinterName & ":"

                Dim flow As New FlowLayoutPanel
                flow.Padding = New Padding(0)
                flow.Margin = New Padding(0)
                flow.Dock = DockStyle.Fill
                gb.Controls.Add(flow)



                For Each q As PrinterQueueInfo In p.Queues
                    flow.Controls.Add(q.CreateViewBtn(OwningForm, False))
                    ct += 1
                Next
                gb.Size = New Size(240, (46 * ct) + 20)
                flowPrinters.Controls.Add(gb)
            Next
        End If
        If Not foundMatch Then
            'look for printer name match
            For Each p As PrinterInfo In PrintMgmt.Printers
                If p.PrinterName = CategoryOrPrinterName Then
                    'if printer match, adds all the queues in printer.
                    foundMatch = True
                    For Each q As PrinterQueueInfo In p.Queues
                        flowPrinters.Controls.Add(q.CreateViewBtn(OwningForm))
                    Next
                    Exit For
                End If
            Next
        End If


        If Not foundMatch Then
            'adds by category of printer queue
            Dim Category As PrinterCategory = PrinterCategoryFromString(CategoryOrPrinterName)
            For Each p As PrinterInfo In PrintMgmt.Printers
                For Each q As PrinterQueueInfo In p.Queues
                    If q.QueueCategory = Category Then
                        flowPrinters.Controls.Add(q.CreateViewBtn(OwningForm))
                    End If
                Next
            Next
        End If
        'LineUp.ctrlFmt.Format_Controls(flowPrinters)
    End Sub

    Private Sub treeCategories_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treeCategories.AfterSelect

        If treeCategories.SelectedNode.Text = "All" Then
            showCategoriesOfPrinters()
        Else
            showCategoriesOfPrinters(treeCategories.SelectedNode.Text)
        End If

    End Sub

    Private Sub treePrinters_LostFocus(sender As Object, e As EventArgs) Handles treePrinters.LostFocus
        treePrinters.SelectedNode = Nothing
    End Sub

    Private Sub treeCategories_LostFocus(sender As Object, e As EventArgs) Handles treeCategories.LostFocus
        treeCategories.SelectedNode = Nothing
    End Sub

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        PrintMgmt.InstallAllPrinters()
    End Sub


    ''' <summary>
    ''' adds a new printer to the printers list and flow
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAddPrinter_Click(sender As Object, e As EventArgs) Handles btnAddPrinter.Click

        flowPrinters.Controls.Clear()
        Dim p As New PrinterInfo(SQLInfo.sqlConnStr)
        p.PrinterIsNew = True
        PrintMgmt.Printers.Add(p)
        flowPrinters.Controls.Add(p.CreateEditBox)

    End Sub




    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click, btnRefresh2.Click
        PopulateTrees()

    End Sub

    Private Sub btnDeletePrinter_Click(sender As Object, e As EventArgs) Handles btnDeletePrinter.Click
        If Not IsNothing(selectedPrinter) Then
            If MsgBox("Are you sure you wish to delete " & selectedPrinter.PrinterName & "?") Then
                If selectedPrinter.DeletePrinterFromPrinterNames() Then
                    MsgBox("Printer deleted successfully.")
                End If

                PopulateTrees()
            End If
        Else
            MsgBox("No printer selected...")
        End If

    End Sub

#End Region



End Class
