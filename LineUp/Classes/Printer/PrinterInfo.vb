Imports System.Data.SqlClient


''' <summary>
''' holds the details for the printer. Each printer can have multiple printer queues.
''' </summary>
Public Class PrinterInfo


#Region "Properties"
    Private Property SqlConnStr As String = ""
    'Private Property SQLInfo As Utilities.SQLConnectionUtilities

    Public Property Queues As New List(Of PrinterQueueInfo)

    ''' <summary>
    ''' only set to true if it's going to be a new printer
    ''' </summary>
    ''' <returns></returns>
    Public Property PrinterIsNew As Boolean = False

    Public Property PrinterName As String = ""
    Private Property PrinterStatus As String = ""
    Private Property PrinterNotes As String = ""
    Private Property PrinterIsColor As Boolean = False
    Private Property PrinterClickBW As Decimal = 0
    Private Property PrinterClickColor As Decimal = 0




    Private Property txtPName As TextBox
    Private Property cboPStatus As ComboBox
    Private Property txtPNotes As TextBox
    Public Property chkPIsColor As CheckBox
    Private Property txtPBWClick As TextBox
    Private Property txtPColorClick As TextBox
    Private Property btnEdit As Button
    Private ReadOnly Property BtnEditTxt As String
        Get
            Dim sb As New Text.StringBuilder
            sb.AppendLine(PrinterName)

            If PrinterNotes = "" Then
                sb.Append(PrinterStatus)
            Else
                sb.Append(PrinterStatus & " - " & PrinterNotes)
            End If
            Return sb.ToString
        End Get
    End Property
    Private Property btnSaveChanges As Button
    Private Property btnAddQueue As Button



#End Region


#Region "Init"

    Public Sub New(ByVal SqlConnStr As String)
        Me.SqlConnStr = SqlConnStr
    End Sub

#End Region


#Region "GUI Methods"

    ''' <summary>
    ''' creates the printer info box GUI.
    ''' </summary>
    ''' <returns></returns>
    Public Function CreateEditBox(Optional ByVal ShowAddQueueButton As Boolean = True) As GroupBox

        PopulatePrinterGUI()

        Dim gb As New GroupBox
        gb.Size = New Size(342, 274)
        gb.Text = "Printer Info:"
        gb.BackColor = Color.LightBlue

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

        AddHandler btnSaveChanges.Click, AddressOf btnSaveChangesClicked

        AddHandler btnAddQueue.Click, AddressOf btnAddQueueClicked

        btnTb.Controls.Add(btnSaveChanges, 0, 0)
        btnTb.Controls.Add(btnAddQueue, 1, 0)
        If Not ShowAddQueueButton Then
            btnAddQueue.Visible = False
        End If

        AddHandler chkPIsColor.CheckedChanged, AddressOf btnPIsColorCheckChanged



        With flow.Controls

            .Add(PrinterGui.CreateTable("Name:", txtPName))
            .Add(PrinterGui.CreateTable("Status:", cboPStatus))
            .Add(PrinterGui.CreateTable("Notes:", txtPNotes))
            .Add(PrinterGui.CreateTable("Prints in Color:", chkPIsColor))
            .Add(PrinterGui.CreateTable("B/W Click:", txtPBWClick))
            .Add(PrinterGui.CreateTable("Color Click:", txtPColorClick))
            .Add(btnTb)

        End With




        Return gb
    End Function

    ''' <summary>
    ''' Creates a printer edit button. Can be clicked to edit printer settings.
    ''' </summary>
    ''' <returns></returns>
    Public Function CreateEditButton() As Button

        btnEdit = New Button
        btnEdit.Size = New Size(200, 50)

        UpdateBtnEdit()
        AddHandler btnEdit.Click, AddressOf btnEdit_Clicked

        Return btnEdit
    End Function

    Private Sub UpdateBtnEdit()
        btnEdit.Text = BtnEditTxt
        If PrinterStatus.ToLower.Contains("alive") Then
            btnEdit.BackColor = Color.LightGreen
        ElseIf PrinterStatus.ToLower.Contains("dead") Then
            btnEdit.BackColor = Color.Red
        ElseIf PrinterStatus.ToLower.Contains("problematic") Then
            btnEdit.BackColor = Color.Orange
        End If
    End Sub
    Private Sub btnEdit_Clicked()
        Dim w As New Form

        w.Size = New Size(355, 310)
        w.Text = "Printer Settings"
        w.FormBorderStyle = FormBorderStyle.FixedToolWindow
        w.StartPosition = FormStartPosition.CenterParent
        Dim gb As GroupBox = CreateEditBox(False)
        'gb.Dock = DockStyle.Fill
        w.Controls.Add(gb)
        w.ShowDialog()
        UpdateBtnEdit()

    End Sub
    Private Sub btnPIsColorCheckChanged()
        Dim chkQueues As New List(Of CheckBox)
        Dim isChecked As Boolean = chkPIsColor.Checked
        For Each q As PrinterQueueInfo In Queues

            If Not IsNothing(q.ChkQIsColor) Then
                If isChecked Then
                    q.ChkQIsColor.Enabled = True
                Else
                    q.ChkQIsColor.Checked = False
                    q.ChkQIsColor.Enabled = False
                End If
            End If
        Next

    End Sub

    ''' <summary>
    ''' saves the changes to the printers
    ''' </summary>
    Private Sub btnSaveChangesClicked()
        Dim pName As String = PrinterName 'keeps old name before sync.
        Dim errors As String = ""

        If PrinterValidateAndUpdate(errors) Then

            If PrinterIsNew Then
                If addPrinterToPrinterNames() Then
                    MsgBox("Printer Added!")
                End If
            Else
                If updatePrinterInPrinterNames(pName) Then
                    MsgBox("Printer Updated!")
                End If
            End If
        Else
            MsgBox(errors)
        End If



    End Sub
    ''' <summary>
    ''' adds a new printer to the queue.
    ''' </summary>
    Private Sub btnAddQueueClicked()
        Dim q As New PrinterQueueInfo(SqlConnStr)
        q.PrinterName = PrinterName
        q.QueueIsNew = True
        Queues.Add(q)

        PrinterGui.FlowPanelAddControl(q.CreateEditBox)
        Application.DoEvents()

        If Not IsNothing(chkPIsColor) AndAlso chkPIsColor.Checked = False Then
            'check mark init'ed, and is set to false
            q.ChkQIsColor.Checked = False
            q.ChkQIsColor.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Creates/Populates the text boxes from the printer class properties
    ''' </summary>
    Private Sub PopulatePrinterGUI()

        're-init text boxes
        txtPName = PrinterGui.CreateTxtBox

        cboPStatus = PrinterGui.CreateCboBox({"Alive", "Dead", "Problematic"}.ToList)
        txtPNotes = PrinterGui.CreateTxtBox
        chkPIsColor = PrinterGui.CreateChkBox
        txtPBWClick = PrinterGui.CreateTxtBox
        txtPColorClick = PrinterGui.CreateTxtBox
        btnSaveChanges = PrinterGui.CreateBtn("Save Changes")
        btnAddQueue = PrinterGui.CreateBtn("Add Queue")

        'set text in text boxes
        txtPName.Text = PrinterName
        cboPStatus.Text = PrinterStatus
        txtPNotes.Text = PrinterNotes
        chkPIsColor.Checked = PrinterIsColor
        txtPColorClick.Text = PrinterClickColor
        txtPBWClick.Text = PrinterClickBW

    End Sub


    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder

        sb.AppendLine("PrinterName: " & PrinterName)
        sb.AppendLine("PrinterStatus: " & PrinterStatus)
        sb.AppendLine("PrinterNotes: " & PrinterNotes)
        sb.AppendLine("PrinterIsColor: " & PrinterIsColor)
        sb.AppendLine("PrinterClickBW: " & PrinterClickBW)
        sb.AppendLine("PrinterClickColor: " & PrinterClickColor)
        Return sb.ToString
    End Function


#End Region


#Region "SQL Methods"


    ''' <summary>
    ''' Gats all the printers listed. Includes all the printer queues inside each sqlPrinterInfo
    ''' </summary>
    ''' <returns></returns>
    Public Function getPrinters() As List(Of PrinterInfo)
        Dim printerDict As New Dictionary(Of String, PrinterInfo)
        Dim printers As New List(Of PrinterInfo)

        Try

            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim query As String = ""
                    query &= "SELECT PrinterNames.PrinterName, PrinterStatus, PrinterNotes, PrinterIsColor, PrinterClickBW, PrinterClickColor, " '0-5
                    query &= "QueueID, QueueCategory, QueueDescription, QueueIsColor, QueuePath, QueueIsHotFolder, QueueIsGpPrinter, QueueDocWidth, QueueDocHeight, QueueHotFolderPath " '6-15
                    query &= "FROM PrinterNames "
                    query &= "LEFT JOIN PrinterQueues ON PrinterNames.PrinterName = PrinterQueues.PrinterName"
                    cmd.CommandText = query
                    conn.Open()

                    Using reader As SqlDataReader = cmd.ExecuteReader

                        Do While reader.Read
                            'reads through each row of the sql query (think table layout).
                            Dim p As New PrinterInfo(SqlConnStr)
                            Dim q As New PrinterQueueInfo(SqlConnStr)
                            p.PrinterName = reader.GetString(0) 'always should have value

                            '------ p -----
                            If Not reader.IsDBNull(1) Then p.PrinterStatus = reader.GetString(1)
                            If Not reader.IsDBNull(2) Then p.PrinterNotes = reader.GetString(2)
                            If Not reader.IsDBNull(3) Then p.PrinterIsColor = reader.GetBoolean(3)
                            If Not reader.IsDBNull(4) Then p.PrinterClickBW = reader.GetDecimal(4)
                            If Not reader.IsDBNull(5) Then p.PrinterClickColor = reader.GetDecimal(5)

                            If Not printerDict.ContainsKey(p.PrinterName) Then
                                printerDict.Add(p.PrinterName, p)
                            End If


                            '----- q -----

                            If Not reader.IsDBNull(6) Then
                                'if null, no queue to add.
                                q.QueueID = reader.GetInt32(6)
                                q.PrinterName = p.PrinterName


                                Dim qCategory As String = ""
                                If Not reader.IsDBNull(7) Then qCategory = reader.GetString(7)
                                If IsNumeric(qCategory) Then
                                    q.QueueCategory = CInt(qCategory)
                                Else
                                    q.QueueCategory = PrinterCategoryFromString(qCategory)
                                End If





                                If Not reader.IsDBNull(8) Then q.QueueDescription = reader.GetString(8)
                                If Not reader.IsDBNull(9) Then q.QueueIsColor = reader.GetBoolean(9)
                                If Not reader.IsDBNull(10) Then q.QueuePath = reader.GetString(10)
                                If Not reader.IsDBNull(11) Then q.QueueHotFolderEnabled = reader.GetBoolean(11)
                                If Not reader.IsDBNull(12) Then q.QueueIsGpPrinter = reader.GetBoolean(12)
                                If Not reader.IsDBNull(13) Then q.QueueDocWidth = reader.GetDecimal(13)
                                If Not reader.IsDBNull(14) Then q.QueueDocHeight = reader.GetDecimal(14)
                                If Not reader.IsDBNull(15) Then q.QueueHotFolderPath = reader.GetString(15)

                                printerDict(q.PrinterName).Queues.Add(q)

                            End If




                        Loop
                        'SQLInfo.sqlConn.Close()

                        printers.AddRange(printerDict.Values.ToList)
                    End Using
                End Using

            End Using

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "getPrinterNames")
        End Try

        Return printers
    End Function



    ''' <summary>
    ''' Validates printer. If valid, updates curPrinter to match text in text boxes
    ''' </summary>
    ''' <param name="errors"></param>
    ''' <returns></returns>
    Private Function PrinterValidateAndUpdate(ByRef errors As String) As Boolean
        Dim sb As New Text.StringBuilder
        Dim success As Boolean = False
        If txtPName.Text = "" Then sb.AppendLine("Printer name is blank")
        If txtPColorClick.Text = "" Or IsNumeric(txtPColorClick.Text) Then
            'ok entry
        Else
            sb.AppendLine("Color click isn't numeric")
        End If
        If txtPBWClick.Text = "" Or IsNumeric(txtPBWClick.Text) Then
            'ok entry
        Else
            sb.AppendLine("Black click isn't numeric")
        End If
        errors &= sb.ToString
        If sb.ToString = "" Then
            success = True
        End If
        If success Then

            PrinterName = txtPName.Text
            PrinterStatus = cboPStatus.Text
            PrinterNotes = txtPNotes.Text
            PrinterIsColor = chkPIsColor.Checked
            If txtPColorClick.Text <> "" Then PrinterClickColor = txtPColorClick.Text
            If txtPBWClick.Text <> "" Then PrinterClickBW = txtPBWClick.Text

            For Each q As PrinterQueueInfo In Queues
                q.PrinterName = PrinterName
            Next

        End If
        Return success
    End Function


    Public Function addPrinterToPrinterNames() As Boolean
        Dim success As Boolean = False

        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim query As String = ""
                    query &= "INSERT INTO PrinterNames (PrinterName, PrinterStatus, PrinterNotes, PrinterIsColor, PrinterClickBW, PrinterClickColor) "
                    query &= "VALUES (@PrinterName, @PrinterStatus, @PrinterNotes, @PrinterIsColor, @PrinterClickBW, @PrinterClickColor)"
                    With cmd
                        .CommandText = query
                        .Parameters.AddWithValue("@PrinterName", PrinterName)
                        .Parameters.AddWithValue("@PrinterStatus", PrinterStatus)
                        .Parameters.AddWithValue("@PrinterNotes", PrinterNotes)
                        .Parameters.AddWithValue("@PrinterIsColor", PrinterIsColor)
                        .Parameters.AddWithValue("@PrinterClickBW", PrinterClickBW)
                        .Parameters.AddWithValue("@PrinterClickColor", PrinterClickColor)
                    End With
                    conn.Open()
                    If cmd.ExecuteNonQuery > 0 Then
                        success = True
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "addPrinterToPrinterNames")
        End Try
        Return success
    End Function

    Public Function updatePrinterInPrinterNames(Optional oldPrinterName As String = "") As Boolean
        Dim success As Boolean = False
        Try
            If oldPrinterName = "" Then
                oldPrinterName = PrinterName
            End If
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim query As String = ""
                    query &= "UPDATE PrinterNames "
                    query &= "SET PrinterName = @PrinterName, PrinterStatus = @PrinterStatus, PrinterNotes = @PrinterNotes, "
                    query &= "PrinterIsColor = @PrinterIsColor, PrinterClickBW = @PrinterClickBW, PrinterClickColor = @PrinterClickColor "
                    query &= "WHERE PrinterName = @oldPrinterName"

                    With cmd
                        .CommandText = query
                        .Parameters.AddWithValue("@PrinterName", PrinterName)
                        .Parameters.AddWithValue("@PrinterStatus", PrinterStatus)
                        .Parameters.AddWithValue("@PrinterNotes", PrinterNotes)
                        .Parameters.AddWithValue("@PrinterIsColor", PrinterIsColor)
                        .Parameters.AddWithValue("@PrinterClickBW", PrinterClickBW)
                        .Parameters.AddWithValue("@PrinterClickColor", PrinterClickColor)
                        .Parameters.AddWithValue("@oldPrinterName", oldPrinterName)
                    End With
                    conn.Open()
                    If cmd.ExecuteNonQuery > 0 Then
                        success = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "updatePrinterInPrinterNames")
        End Try
        Return success
    End Function
    Public Function DeletePrinterFromPrinterNames() As Boolean
        Dim success As Boolean = False
        If PrinterName <> "" Then
            Try
                Using conn As New SqlConnection(SqlConnStr)
                    Using cmd As SqlCommand = conn.CreateCommand
                        cmd.CommandText = "DELETE FROM PrinterNames WHERE PrinterName = @PrinterName"
                        cmd.Parameters.Add("@PrinterName", SqlDbType.VarChar).Value = PrinterName
                        conn.Open()
                        If cmd.ExecuteNonQuery > 0 Then
                            success = True
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "deletePrinterFromPrinterNames")
            End Try
        End If

        Return success
    End Function



#End Region


End Class


