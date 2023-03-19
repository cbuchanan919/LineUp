Imports System.IO


''' <summary>
''' Contains the functions to find the different production Files
''' </summary>
''' <remarks></remarks>
Public Class JQProductionFileInfo
    Inherits JQProductionDirInfo


#Region "Properties"



    Public Property ProductionFile As Utilities.CMS_FileName = Nothing

    Public Property PdfSpecs As Utilities.PdfInfo = Nothing

    ''' <summary>
    ''' paper weight to use for the product.
    ''' </summary>
    ''' <returns></returns>
    Public Property PaperWeight As String = ""


    Private Property MyPrinterMgmt As PrinterMgmt = Nothing
    Private Property ItemNumber As Integer = 0
    Private Property QuantityOrdered As Integer = 0


    ''' <summary>
    ''' returns the filename without extension (if it exists)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetProductionFileNameNoExtension As String
        Get
            Dim sb As New Text.StringBuilder
            If Not IsNothing(ProductionFile) AndAlso ProductionFile.OriginalFile.Exists Then
                sb.Append(Path.GetFileNameWithoutExtension(ProductionFile.OriginalFile.FullName))
            End If

            Return sb.ToString
        End Get
    End Property


    Public Property ProductionGB As GroupBox = Nothing

    Public Property ProductionFolderMenuItem As ToolStripMenuItem

    Private Property pdf As Microsoft.Web.WebView2.WinForms.WebView2 = Nothing

    Private Const RevealInExplorer As String = "Reveal in Explorer"

#End Region


#Region "Init"



    ''' <summary>
    ''' A new Production File Info...
    ''' </summary>
    Public Sub New(ByVal prodDirInfo As JQProductionDirInfo, ByVal ProductionFile As Utilities.CMS_FileName)
        MyBase.New(prodDirInfo.ProductType, prodDirInfo.ProductionDirectory, prodDirInfo.ProductBodyOrCover, prodDirInfo.CopiesPerLayer, prodDirInfo.MyPrinterCategory)
        Me.ProductionFile = ProductionFile


        'configure menu tool strip items
        ProductionFolderMenuItem = New ToolStripMenuItem(ProductType.ToString.Replace("_", " ") & " - " & ProductBodyOrCover.ToString & " - " & Me.ProductionFile.OriginalFile.Name)
        AddHandler ProductionFolderMenuItem.Click, AddressOf OpenProductionFile

        Dim reveal As New ToolStripMenuItem(RevealInExplorer)
        ProductionFolderMenuItem.DropDownItems.Add(reveal)
        AddHandler reveal.Click, AddressOf OpenProductionFile

    End Sub


#End Region


#Region "Methods"

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.Append(MyBase.ToString)
        sb.AppendLine(ProductionFile.OriginalFile.FullName)
        If Not IsNothing(PdfSpecs) Then
            sb.AppendLine(PdfSpecs.ToString())
        End If

        Return sb.ToString
    End Function

    ''' <summary>
    ''' Returns a 'surface' level copy of the production file. 
    ''' </summary>
    ''' <returns></returns>
    Public Function Clone() As JQProductionFileInfo
        Dim ClonedDir As New JQProductionDirInfo(ProductType, New DirectoryInfo(ProductionDirectory.FullName), ProductBodyOrCover, CopiesPerLayer, MyPrinterCategory)
        Dim cloned As New JQProductionFileInfo(ClonedDir, ProductionFile)
        Return cloned
    End Function

    ''' <summary>
    ''' Opens the production file. (or selects it in explorer)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OpenProductionFile(ByVal sender As Object, e As System.EventArgs)

        If TypeOf sender Is ToolStripMenuItem Then
            Dim mi As ToolStripMenuItem = sender
            If mi.Text = RevealInExplorer Then
                Process.Start("Explorer.exe", "/select," & ProductionFile.OriginalFile.FullName)
            Else
                'Process.Start("explorer.exe", Chr(34) & ProductionFile.OriginalFile.FullName & Chr(34))
                Process.Start("explorer.exe", ProductionFile.OriginalFile.FullName)
            End If

            'MsgBox(mi.Text)
        End If


    End Sub


    ''' <summary>
    ''' Creates a new groupbox
    ''' </summary>
    ''' <param name="QuantityOrdered"></param>
    ''' <returns></returns>
    Public Async Function CreateProductionGB(ByVal ItemNumber As Integer, ByVal QuantityOrdered As Integer, ByVal MyPrinterMgmt As PrinterMgmt) As Task(Of GroupBox)

        Me.MyPrinterMgmt = MyPrinterMgmt
        Me.ItemNumber = ItemNumber
        Me.QuantityOrdered = QuantityOrdered

        If QuantityOrdered = cNullInt Then QuantityOrdered = 0

        PdfSpecs = New Utilities.PdfInfo(ProductionFile.OriginalFile.FullName)
        CalculatePaperWeight()

        Dim productionQuantity As Integer = CalculateProductionQuantity(QuantityOrdered)
        Dim layers As Integer = CalculateLayerCount(productionQuantity)
        Dim layerStr As String = layers & " " & ProductBodyOrCover.ToString().Replace("Not_Set", "") & " Layers (" & CopiesPerLayer & " up)"

        ProductionGB = New GroupBox
        ProductionGB.Size = New Size(800, 800)
        ProductionGB.Text = GetProductionFileNameNoExtension() & " - " & ProductBodyOrCover.ToString.Replace("_", " ")

        Dim tb As New TableLayoutPanel
        tb.Dock = DockStyle.Fill

        tb.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120))
        'tb.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        tb.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 80%))
        tb.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150))
        tb.ColumnCount = 3

        tb.RowStyles.Add(New RowStyle(SizeType.Absolute, 35))
        tb.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
        tb.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
        tb.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
        tb.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
        tb.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
        tb.RowStyles.Add(New RowStyle(SizeType.AutoSize))

        tb.Controls.Add(New Label() With {.Text = "File Path:", .Anchor = AnchorStyles.Right}, 0, 0)
        Dim ll As New LinkLabel() With {.Text = ProductionFile.OriginalFile.FullName, .Dock = DockStyle.Fill}
        AddHandler ll.Click, AddressOf LinkLabelClicked
        tb.Controls.Add(ll, 1, 0)
        Dim exp As New Button() With {.Text = "Export To Printer", .Dock = DockStyle.Fill}
        AddHandler exp.Click, AddressOf ExportButtonClicked
        tb.Controls.Add(exp, 2, 0)

        tb.Controls.Add(New Label() With {.Text = "Sheet Count:", .Anchor = AnchorStyles.Right}, 0, 1)
        tb.Controls.Add(New Label() With {.Text = CalculateSheetCount(layers).ToString("N0"), .Dock = DockStyle.Fill}, 1, 1)

        tb.Controls.Add(New Label() With {.Text = "Paper Weight:", .Anchor = AnchorStyles.Right}, 0, 2)
        tb.Controls.Add(New Label() With {.Text = PaperWeight, .Dock = DockStyle.Fill}, 1, 2)

        tb.Controls.Add(New Label() With {.Text = "Paper Size:", .Anchor = AnchorStyles.Right}, 0, 3)
        tb.Controls.Add(New Label() With {.Text = PdfSpecs.pageWidth & " x " & PdfSpecs.pageHeight, .Dock = DockStyle.Fill}, 1, 3)

        tb.Controls.Add(New Label() With {.Text = "Page Count:", .Anchor = AnchorStyles.Right}, 0, 4)
        tb.Controls.Add(New Label() With {.Text = PdfSpecs.pageCount, .Dock = DockStyle.Fill}, 1, 4)

        tb.Controls.Add(New Label() With {.Text = "Layers:", .Anchor = AnchorStyles.Right}, 0, 5)
        tb.Controls.Add(New Label() With {.Text = layerStr, .Dock = DockStyle.Fill}, 1, 5)

        Dim startPg As Integer = 1
        If ProductBodyOrCover = BodyVsCover.Body And (ProductType = ProductCategory.Pamphlet Or ProductType = ProductCategory.Mini_Pamphlet) Then
            startPg = 3
        End If

        ''Microsoft Edge Plugin
        'Dim pdf As New 
        pdf = New Microsoft.Web.WebView2.WinForms.WebView2
        Await (pdf.EnsureCoreWebView2Async())
        tb.Controls.Add(pdf, 0, 6)
        tb.SetColumnSpan(pdf, 3)
        pdf.Dock = DockStyle.Fill
        pdf.CoreWebView2.Navigate(ProductionFile.OriginalFile.FullName & $"#page={startPg}")



        ProductionGB.Controls.Add(tb)


        Return ProductionGB
    End Function

    ''' <summary>
    ''' Disposes the production group box.
    ''' </summary>
    Public Sub DisposeProductionGB()
        If Not IsNothing(pdf) Then
            pdf.Visible = False
            pdf.Dispose()
        End If
        If Not IsNothing(ProductionGB) Then
            ProductionGB.Dispose()
        End If
        ProductionGB = Nothing
    End Sub

    Private Sub LinkLabelClicked(sender As Object, e As EventArgs)
        If TypeOf sender Is LinkLabel Then
            Dim ll As LinkLabel = sender
            If File.Exists(ProductionFile.OriginalFile.FullName) Then
                If My.Computer.Keyboard.CtrlKeyDown Then
                    Process.Start("Explorer.exe", "/select," & ProductionFile.OriginalFile.FullName)
                    ' Process.Start("Explorer.exe", "/select," & ll.Text)
                Else
                    Process.Start(ProductionFile.OriginalFile.FullName)
                    ' Process.Start(ll.Text)
                End If

            End If
        End If
    End Sub
    Private Sub ExportButtonClicked(sender As Object, e As EventArgs)
        Dim locs As List(Of String) = GetExportLocations(ItemNumber, QuantityOrdered, MyPrinterMgmt)
        Dim sb As New Text.StringBuilder
        Dim errored As Boolean = False
        For Each loc As String In locs
            Try
                Dim dir As String = Path.GetDirectoryName(loc) 'gets parent directory
                'MsgBox(dir)
                If Not Directory.Exists(dir) Then
                    Directory.CreateDirectory(dir)
                End If
                File.Copy(ProductionFile.OriginalFile.FullName, loc, True)
                sb.AppendLine(loc)
            Catch ex As Exception
                sb.AppendLine(vbCrLf & "Failed: " & loc)
                sb.AppendLine(ex.Message & vbCrLf)
                errored = True
            End Try
        Next
        If errored Then
            MsgBox(sb.ToString, MsgBoxStyle.Critical, "File Export Status")
        Else
            sb.Insert(0, "Files copied ok: " & vbCrLf)
            MsgBox(sb.ToString, MsgBoxStyle.OkOnly, "File Export Status")
        End If


    End Sub

    ''' <summary>
    ''' Calculates the weight of paper to show
    ''' </summary>
    Public Function CalculatePaperWeight() As String
        If Not IsNothing(PdfSpecs) Then
            Select Case ProductBodyOrCover
                Case BodyVsCover.Not_Set
                    PaperWeight = cUnknown
                Case BodyVsCover.Cover
                    Select Case ProductType
                        Case ProductCategory.Pamphlet, ProductCategory.Mini_Pamphlet
                            PaperWeight = c80lbCover
                        Case ProductCategory.Cd_Album_Cover
                            PaperWeight = c24lbPaper
                        Case Else
                            PaperWeight = c100lbCover
                    End Select

                Case BodyVsCover.Body
                    Select Case ProductType
                        Case ProductCategory.Pamphlet, ProductCategory.Mini_Pamphlet, ProductCategory.Share_Word
                            PaperWeight = c20lbPaper

                        Case ProductCategory.Book, ProductCategory.Full_Bleed_Book, ProductCategory.Book_12x9, ProductCategory.Hymn_Book
                            If PdfSpecs.pageCount > 0 And PdfSpecs.pageCount <= 75 Then
                                PaperWeight = c28lbPaper
                            ElseIf PdfSpecs.pageCount > 75 And PdfSpecs.pageCount < 200 Then
                                PaperWeight = c24lbPaper
                            ElseIf PdfSpecs.pageCount >= 200 And PdfSpecs.pageCount < 450 Then
                                PaperWeight = c20lbPaper
                            ElseIf PdfSpecs.pageCount >= 450 Then
                                PaperWeight = c16lbPaper
                            Else
                                PaperWeight = c20lbPaper

                            End If
                        Case Else
                            PaperWeight = c20lbPaper
                    End Select
            End Select
        End If
        Return PaperWeight
    End Function


    ''' <summary>
    ''' returns quantity to produce. Either an extra 50, or extra 10%. Whichever is smaller. 
    ''' ie. For an order of 100 books we would produce 110 worth.
    ''' For an order of 1000, we would produce 1050.
    ''' </summary>
    ''' <param name="inQuantity"></param>
    ''' <returns></returns>
    Private Function CalculateProductionQuantity(ByVal inQuantity As Integer) As Integer


        Dim ProductionQuantity As Integer = 0
        Dim result As String = ""


        'This converts the final quantity to body layers to print
        Try
            ProductionQuantity = inQuantity * 1.1
            'this should put a cap on how many it will print (max 50 extra)
            If (inQuantity + 50) <= ProductionQuantity Then
                ProductionQuantity = inQuantity + 50
            End If

        Catch ex As Exception
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            ProductionQuantity = 0
        End Try
        Return ProductionQuantity


    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="QuantityWanted"></param>
    ''' <param name="add10Percent"></param>
    ''' <returns></returns>
    Public Function CalculateLayerCount(ByVal QuantityWanted As Integer, Optional ByVal add10Percent As Boolean = False) As Integer
        If add10Percent Then
            QuantityWanted = CalculateProductionQuantity(QuantityWanted)
        End If
        Dim Calculation As Integer = 0

        Try
            Calculation = Math.Round(QuantityWanted \ CopiesPerLayer, 1)

            If Calculation * CopiesPerLayer < QuantityWanted Then
                Calculation += 1 'adds 1 for rounding.
            End If

            'If ProductBodyOrCover = BodyVsCover.Cover Then
            If Calculation Mod 2 <> 0 Then
                'if it's an odd number, add one cover - for ease of lamination
                Calculation += 1
            End If
            'End If


        Catch ex As Exception

        End Try


        Return Calculation
    End Function


    Public Function CalculateSheetCount(ByVal layerCount As Integer) As Integer
        Try
            If ProductBodyOrCover = BodyVsCover.Cover Then
                Return layerCount
            Else
                If Not IsNothing(PdfSpecs) Then
                    Return (PdfSpecs.pageCount / 2) * layerCount
                End If


            End If
        Catch ex As Exception

        End Try
        Return 0
    End Function


    ''' <summary>
    ''' returns a new file name that can be used to copy the file to a hot folder / desktop
    ''' </summary>
    ''' <param name="ItemNumber"></param>
    ''' <param name="OrderQuantity"></param>
    ''' <returns></returns>
    Public Function GetNewFileName(ByVal ItemNumber As Integer, ByVal OrderQuantity As Integer) As String
        Dim fileName As New Text.StringBuilder

        If IsNothing(PdfSpecs) Then
            PdfSpecs = New Utilities.PdfInfo(ProductionFile.OriginalFile.FullName)
        End If
        Dim replaces As New Dictionary(Of String, String)
        replaces.Add(" Paper", "")
        replaces.Add(" Bond", "")
        replaces.Add("?", "_")

        Dim paperSize As String = PdfSpecs.pageWidth & "x" & PdfSpecs.pageHeight

        Dim paperWeight As String = CalculatePaperWeight()
        For Each replaceKey As String In replaces.Keys
            paperWeight = paperWeight.Replace(replaceKey, replaces(replaceKey))
        Next
        paperWeight = paperWeight.Replace("?", "_")

        Dim layerQuan As Integer = CalculateLayerCount(OrderQuantity, True)

        'Item#-(paperSize paperWeight)-Quan.pdf
        fileName.Append(ItemNumber)
        fileName.Append($"-({ paperSize }_{ paperWeight })")
        'If ProductPrinterCategory <> PrinterCategory.Pamphlet_Body Then
        '    fileName.Append($"-({ paperWeight })")
        'End If
        fileName.Append($"-Q{ layerQuan}.pdf")
        Return fileName.ToString
    End Function


    '''' <summary>
    '''' returns a new file name that can be used to copy the file to a hot folder / desktop
    '''' </summary>
    '''' <param name="ItemNumber"></param>
    '''' <param name="OrderQuantity"></param>
    '''' <returns></returns>
    'Public Function GetNewFileName(ByVal ItemNumber As Integer, ByVal OrderQuantity As Integer) As String
    '    If IsNothing(PdfSpecs) Then
    '        PdfSpecs = New Utilities.PdfInfo(ProductionFile.OriginalFile.FullName)
    '    End If
    '    Dim paperWeight As String = CalculatePaperWeight.Replace(" Paper", " Bond")
    '    Dim layerQuan As Integer = CalculateLayerCount(OrderQuantity, True)
    '    Dim fileName As New Text.StringBuilder
    '    fileName.Append(ItemNumber)
    '    If ProductPrinterCategory <> PrinterCategory.Pamphlet_Body Then fileName.Append($"-({ paperWeight.Replace("?", "_") })")
    '    fileName.Append($"-Q{ layerQuan}.pdf")
    '    Return fileName.ToString
    'End Function

    ''' <summary>
    ''' Goes through printers and printer queues, and returns a list of hot folders to copy the file to. Note: If no match found, returns desktop location.
    ''' </summary>
    ''' <param name="ItemNumber"></param>
    ''' <param name="QuantityOrdered"></param>
    ''' <returns></returns>
    Public Function GetExportLocations(ByVal ItemNumber As Integer, ByVal QuantityOrdered As Integer, ByVal MyPrinterMgmt As PrinterMgmt) As List(Of String)
        Dim results As New List(Of String)
        If Not IsNothing(MyPrinterMgmt) And QuantityOrdered <> 0 Then
            For Each printer As PrinterInfo In MyPrinterMgmt.Printers
                For Each queue As PrinterQueueInfo In printer.Queues
                    If queue.QueueCategory = MyPrinterCategory And queue.QueueHotFolderPath <> "" And queue.QueueHotFolderEnabled Then
                        Dim copyFP As String = Path.Combine(queue.QueueHotFolderPath, GetNewFileName(ItemNumber, QuantityOrdered))
                        results.Add(copyFP)
                    End If
                Next
            Next

        End If
        If results.Count = 0 And QuantityOrdered > 0 Then
            Dim copyFP As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Production Files", GetNewFileName(ItemNumber, QuantityOrdered))
            results.Add(copyFP)
        End If
        Return results
    End Function



    ''' <summary>
    ''' Copies the production file to the specified location (baseFP).
    ''' </summary>
    ''' <param name="ItemNumber">File's Item Number</param>
    ''' <param name="OrderQuantity">Total quantity (not layers) to print</param>
    ''' <param name="baseFP">Where to copy the files to</param>
    ''' <param name="CreateSubDirectories">Does the file get placed in sub directories (under the base fp)</param>
    ''' <returns></returns>
    Public Function CopyProductionFile(ByVal ItemNumber As Integer, ByVal OrderQuantity As Integer, ByVal baseFP As String, Optional ByVal CreateSubDirectories As Boolean = False) As Boolean
        Dim success As Boolean = False
        Dim fp As String = baseFP
        If CreateSubDirectories Then
            fp = Path.Combine(baseFP, ProductType.ToString, ProductBodyOrCover.ToString)
            If Not Directory.Exists(fp) Then
                Directory.CreateDirectory(fp)
            End If
        End If

        Dim copyPath As String = Path.Combine(fp, GetNewFileName(ItemNumber, OrderQuantity))

        File.Copy(ProductionFile.OriginalFile.FullName, copyPath, True)
        Return success
    End Function

#End Region


End Class
