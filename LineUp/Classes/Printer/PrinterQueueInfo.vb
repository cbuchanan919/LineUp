Imports System.Data.SqlClient

''' <summary>
''' holds the details for the printer queue
''' </summary>
Public Class PrinterQueueInfo


#Region "Properties"

    Private Property SqlConnStr As String = ""

    Public Property QueueID As Integer = -1
    Public Property PrinterName As String = ""
    Public Property QueueCategory As PrinterCategory = PrinterCategory.Basic_Printer
    Public Property QueueDescription As String = ""
    Public Property QueueIsColor As Boolean = False
    Public Property QueuePath As String = ""
    Public Property QueueHotFolderEnabled As Boolean = False
    Public Property QueueHotFolderPath As String = ""
    Public Property QueueIsGpPrinter As Boolean = False
    Public Property QueueDocWidth As Decimal = 0
    Public Property QueueDocHeight As Decimal = 0

    Public Property QueueIsNew As Boolean = False


    'Public Property TxtQName As TextBox
    Public Property TxtQDescription As TextBox
    Public Property cboQCategory As ComboBox
    Public Property ChkQIsColor As CheckBox
    Public Property TxtQPath As TextBox
    Public Property TxtHotFolderPath As TextBox
    Public Property ChkQHotFolderEnabled As CheckBox
    Public Property ChkQIsGpPrinter As CheckBox
    Public Property TxtQDocWidth As TextBox
    Public Property TxtQDocHeight As TextBox

    Private Property btnSaveChanges As Button
    Private Property btnMakeDefault As Button
    Private Property btnDeleteQueue As Button

    Private Property btnViewQueue As Button
    Private Property CurrentGB As GroupBox

    Private Property OwningForm As Form
#End Region


#Region "Init"

    Public Sub New(ByVal SqlConnStr As String)
        Me.SqlConnStr = SqlConnStr
    End Sub

#End Region


#Region "GUI Methods"

    ''' <summary>
    ''' Creates a view printer queue button
    ''' </summary>
    ''' <param name="OwningForm">Used to close the form when default printer set</param>
    ''' <param name="includePrName"></param>
    ''' <returns></returns>
    Public Function CreateViewBtn(ByRef OwningForm As Form, Optional ByVal includePrName As Boolean = True) As Button

        Dim btn As New Button
        btn.Size = New Size(200, 40)



        If QueueIsColor Then
            btn.BackColor = Color.LightGreen
        Else
            btn.BackColor = Color.LightBlue
        End If

        Me.OwningForm = OwningForm
        Dim line1 As String = ""
        If includePrName Then
            line1 = PrinterName & " - " & QueueCategory.ToString.Replace("_", " ")
        Else
            line1 = QueueCategory.ToString.Replace("_", " ")
        End If

        Dim btnText As String = "(" & QueueDocWidth & "x" & QueueDocHeight & ")"
        btnText = btnText.Replace(".000", "")
        btnText = btnText.Replace(".250", ".25")
        btnText = btnText.Replace(".500", ".5")
        btnText = btnText.Replace(".750", ".75")
        btnText = line1 & vbCrLf & btnText

        btn.Text = btnText
        AddHandler btn.Click, AddressOf btnMakeDefault_Clicked

        btnViewQueue = btn
        Return btn
    End Function


    Public Function CreateEditBox() As GroupBox


        PopulateQueueGUI()

        Dim gb As New GroupBox
        gb.Size = New Size(342, 300)
        gb.Text = "Queue Info:"
        If QueueIsNew Then
            gb.BackColor = Color.LightPink
        Else
            gb.BackColor = Color.LightGreen
        End If


        Dim flow As New FlowLayoutPanel
        flow.Parent = gb
        flow.Dock = DockStyle.Fill
        flow.BackColor = Color.Transparent

        Dim btnTb As TableLayoutPanel = PrinterGui.CreateTable(height:=35)
        btnTb.ColumnStyles.Clear()
        btnTb.ColumnCount = 3
        For i As Integer = 1 To btnTb.ColumnCount
            btnTb.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100 / btnTb.ColumnCount))
        Next


        btnTb.Controls.Add(btnSaveChanges, 0, 0)
        'btnTb.Controls.Add(btnMakeDefault, 1, 0)
        btnTb.Controls.Add(btnDeleteQueue, 2, 0)





        With flow.Controls

            '.Add(SQLInfo.CreateTable("Name:", TxtQName))
            .Add(PrinterGui.CreateTable("Type:", cboQCategory))
            .Add(PrinterGui.CreateTable("Path:", TxtQPath))
            .Add(PrinterGui.CreateTable("Hot Folder Path:", TxtHotFolderPath))
            .Add(PrinterGui.CreateTable("Use Hot Folder:", ChkQHotFolderEnabled)) ', "Is a GP Printer:", ChkQIsGpPrinter))
            .Add(PrinterGui.CreateTable("Descrip./Notes:", TxtQDescription))
            .Add(PrinterGui.CreateTable("Prints in Color:", ChkQIsColor))

            .Add(PrinterGui.CreateTable("Doc Width:", TxtQDocWidth, "Doc Height:", TxtQDocHeight))
            '.Add(SQLInfo.CreateTable())
            .Add(btnTb)

        End With



        CurrentGB = gb
        Return gb
    End Function

    Private Sub btnSaveChangesClicked()
        Dim errors As String = ""
        If QueueValidateAndUpdate(errors) Then
            If QueueIsNew Then
                If addQueueToPrinterQueues() Then
                    MsgBox("Queue Added!")
                    QueueIsNew = False
                    PrinterGui.FlowPanelRemoveControl(CurrentGB)
                    PrinterGui.FlowPanelAddControl(CreateEditBox)
                End If
            Else
                If UpdateQueueInPrinterQueues() Then
                    MsgBox("Queue Updated!")
                End If
            End If
        Else
            MsgBox(errors, MsgBoxStyle.Exclamation)
        End If

    End Sub
    Private Sub btnMakeDefault_Clicked()
        LineUp.MyPrinterMgmt.SetDefaultPrinter(QueuePath)
        LineUp.UpdateStatus("Default Printer Set: " & LineUp.MyPrinterMgmt.GetDefaultPrinter, False)
        If Not IsNothing(OwningForm) Then
            OwningForm.Close()
        End If
    End Sub

    Private Sub btnDeleteQueue_Clicked()
        If MsgBox("Are you sure you want to delete '" & PrinterName & " - " & cboQCategory.Text & "'?", MsgBoxStyle.YesNo, "Delete Queue?") = MsgBoxResult.Yes Then
            If DeleteQueueFromPrinterQueues() Then
                PrinterGui.FlowPanelRemoveControl(CurrentGB)
            End If

        End If

    End Sub


    Private Function QueueValidateAndUpdate(ByRef errors As String) As Boolean
        Dim sb As New Text.StringBuilder
        Dim success As Boolean = True
        If IsNumeric(TxtQDocWidth.Text) Or TxtQDocWidth.Text = "" Then
            'it's ok
        Else
            sb.AppendLine("Invalid document width")
        End If
        If IsNumeric(TxtQDocHeight.Text) Or TxtQDocHeight.Text = "" Then
            'it's ok
        Else
            sb.AppendLine("Invalid document height")
        End If
        errors &= sb.ToString

        If sb.ToString <> "" Then
            success = False
        End If
        If cboQCategory.SelectedIndex = -1 Then
            Beep()
        End If


        If success Then

            'QueueName = TxtQName.Text
            QueueCategory = cboQCategory.SelectedIndex
            QueueDescription = TxtQDescription.Text
            QueueIsColor = ChkQIsColor.Checked
            QueuePath = TxtQPath.Text
            QueueHotFolderPath = TxtHotFolderPath.Text
            QueueHotFolderEnabled = ChkQHotFolderEnabled.Checked
            QueueIsGpPrinter = ChkQIsGpPrinter.Checked
            If TxtQDocWidth.Text <> "" Then QueueDocWidth = TxtQDocWidth.Text
            If TxtQDocHeight.Text <> "" Then QueueDocHeight = TxtQDocHeight.Text


        End If


        Return success
    End Function

    ''' <summary>
    ''' Populates the different elements of the printer queue.
    ''' </summary>
    Private Sub PopulateQueueGUI()

        'TxtQName = SQLInfo.CreateTxtBox
        'TxtQDescription = SQLInfo.CreateTxtBox
        Dim items As Array = System.Enum.GetValues(GetType(PrinterCategory))
        Dim descriptionItems As New List(Of String)

        For Each item As PrinterCategory In items
            descriptionItems.Add(item.ToString)
        Next



        'init's the different gui items
        cboQCategory = PrinterGui.CreateCboBox(descriptionItems)
        TxtQDescription = PrinterGui.CreateTxtBox()
        ChkQIsColor = PrinterGui.CreateChkBox
        TxtQPath = PrinterGui.CreateTxtBox
        TxtHotFolderPath = PrinterGui.CreateTxtBox
        ChkQHotFolderEnabled = PrinterGui.CreateChkBox
        ChkQIsGpPrinter = PrinterGui.CreateChkBox
        TxtQDocWidth = PrinterGui.CreateTxtBox
        TxtQDocHeight = PrinterGui.CreateTxtBox
        btnSaveChanges = PrinterGui.CreateBtn("Save Changes")
        btnMakeDefault = PrinterGui.CreateBtn("Make Default")
        btnDeleteQueue = PrinterGui.CreateBtn("Delete Queue")

        'sets the text to the correct thing
        cboQCategory.SelectedIndex = QueueCategory
        TxtQDescription.Text = QueueDescription
        ChkQIsColor.Checked = QueueIsColor
        TxtQPath.Text = QueuePath
        TxtHotFolderPath.Text = QueueHotFolderPath
        ChkQHotFolderEnabled.Checked = QueueHotFolderEnabled
        ChkQIsGpPrinter.Checked = QueueIsGpPrinter
        TxtQDocWidth.Text = QueueDocWidth
        TxtQDocHeight.Text = QueueDocHeight

        ChkQIsColor.Anchor = AnchorStyles.Left
        ChkQHotFolderEnabled.Anchor = AnchorStyles.Left
        ChkQIsGpPrinter.Anchor = AnchorStyles.Left

        AddHandler btnSaveChanges.Click, AddressOf btnSaveChangesClicked

        'AddHandler btnMakeDefault.Click, AddressOf btnMakeDefault_Clicked

        AddHandler btnDeleteQueue.Click, AddressOf btnDeleteQueue_Clicked



    End Sub



#End Region


#Region "SQL Methods"


    ''' <summary>
    ''' Adds the queue to the printer queues. Note: be sure to reload after, it will not have the queue id since it's added by sql automagically.
    ''' </summary>
    ''' <returns></returns>
    Public Function addQueueToPrinterQueues() As Boolean
        Dim success As Boolean = False
        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim query As String = ""
                    query &= "INSERT INTO PrinterQueues (PrinterName, QueueCategory, QueueDescription, QueueIsColor, QueuePath, QueueIsHotFolder, QueueIsGpPrinter, QueueDocWidth, QueueDocHeight, QueueHotFolderPath) "
                    query &= "VALUES (@PrinterName, @QueueCategory, @QueueDescription, @QueueIsColor, @QueuePath, @QueueIsHotFolder, @QueueIsGpPrinter, @QueueDocWidth, @QueueDocHeight, @QueueHotFolderPath)"

                    With cmd
                        .CommandText = query
                        .Parameters.AddWithValue("@PrinterName", PrinterName)
                        .Parameters.AddWithValue("@QueueCategory", PrinterCategoryToString(QueueCategory))
                        .Parameters.AddWithValue("@QueueDescription", QueueDescription)
                        .Parameters.AddWithValue("@QueueIsColor", QueueIsColor)
                        .Parameters.AddWithValue("@QueuePath", QueuePath)
                        .Parameters.AddWithValue("@QueueIsHotFolder", QueueHotFolderEnabled)
                        .Parameters.AddWithValue("@QueueIsGpPrinter", QueueIsGpPrinter)
                        .Parameters.AddWithValue("@QueueDocWidth", QueueDocWidth)
                        .Parameters.AddWithValue("@QueueDocHeight", QueueDocHeight)
                        .Parameters.AddWithValue("@QueueHotFolderPath", QueueHotFolderPath)
                    End With
                    conn.Open()
                    If cmd.ExecuteNonQuery > 0 Then
                        success = True
                    End If

                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "addQueueToPrinterQueues")
        End Try

        Return success
    End Function

    Public Function UpdateQueueInPrinterQueues() As Boolean
        Dim success As Boolean = False
        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim query As String = ""
                    query &= "UPDATE PrinterQueues "
                    query &= "SET PrinterName = @PrinterName, QueueCategory = @QueueCategory, QueueDescription = @QueueDescription, QueueIsColor = @QueueIsColor, QueuePath = @QueuePath, "
                    query &= "QueueIsHotFolder = @QueueIsHotFolder, QueueIsGpPrinter = @QueueIsGpPrinter, QueueDocWidth = @QueueDocWidth, QueueDocHeight = @QueueDocHeight, QueueHotFolderPath = @QueueHotFolderPath "
                    query &= "WHERE QueueID = @QueueID"
                    With cmd
                        .CommandText = query
                        .Parameters.AddWithValue("@PrinterName", PrinterName)
                        .Parameters.AddWithValue("@QueueCategory", PrinterCategoryToString(QueueCategory))
                        .Parameters.AddWithValue("@QueueDescription", QueueDescription)
                        .Parameters.AddWithValue("@QueueIsColor", QueueIsColor)
                        .Parameters.AddWithValue("@QueuePath", QueuePath)
                        .Parameters.AddWithValue("@QueueIsHotFolder", QueueHotFolderEnabled)
                        .Parameters.AddWithValue("@QueueIsGpPrinter", QueueIsGpPrinter)
                        .Parameters.AddWithValue("@QueueDocWidth", QueueDocWidth)
                        .Parameters.AddWithValue("@QueueDocHeight", QueueDocHeight)
                        .Parameters.AddWithValue("@QueueHotFolderPath", QueueHotFolderPath)
                        .Parameters.AddWithValue("@QueueID", QueueID)
                    End With
                    conn.Open()
                    If cmd.ExecuteNonQuery > 0 Then
                        success = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "UpdateQueueInPrinterQueues")
        End Try
        Return success
    End Function

    Public Function DeleteQueueFromPrinterQueues() As Boolean
        Dim success As Boolean = False
        Try
            If QueueID >= 0 Then
                Using conn As New SqlConnection(SqlConnStr)
                    Using cmd As SqlCommand = conn.CreateCommand
                        cmd.CommandText = "DELETE FROM PrinterQueues WHERE QueueID = @QueueID"
                        cmd.Parameters.AddWithValue("@QueueID", QueueID)
                        conn.Open()
                        If cmd.ExecuteNonQuery > 0 Then
                            success = True
                        End If
                    End Using
                End Using
            Else
                Throw New Exception("Queue ID is not set")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "DeleteQueueFromPrinterQueues")
        End Try
        Return success
    End Function

#End Region


#Region "Misc Methods"
    Public Overrides Function ToString() As String

        Dim sb As New Text.StringBuilder
        sb.AppendLine("QueueID: " & QueueID)
        sb.AppendLine("PrinterName: " & PrinterName)
        sb.AppendLine("QueueCategory: " & QueueCategory)
        sb.AppendLine("QueueDescription: " & QueueDescription)
        sb.AppendLine("QueueIsColor: " & QueueIsColor)
        sb.AppendLine("QueuePath: " & QueuePath)
        sb.AppendLine("QueueHotFolderPath: " & QueueHotFolderPath)
        sb.AppendLine("QueueIsHotFolder: " & QueueHotFolderEnabled)
        sb.AppendLine("QueueIsGpPrinter: " & QueueIsGpPrinter)
        sb.AppendLine("QueueDocWidth: " & QueueDocWidth)
        sb.AppendLine("QueueDocHeight: " & QueueDocHeight)


        Return sb.ToString
    End Function

    Public Sub TryToInstallPrinter()
        If QueuePath <> "" Then
            Process.Start("explorer.exe", Chr(34) & QueuePath & Chr(34))
        End If
    End Sub

#End Region



End Class
