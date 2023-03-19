Imports System.IO

Imports System.Xml
Imports System.Data.SqlClient


Public Class JQRowIO


#Region "Variables & Properties"

    Private Property MyJQProductionIO As JQProductionIO
    Private Property MyUvProductInfoIO As UvProductInfoIO

    'Private Property SqlInfo As Utilities.SQLConnectionUtilities
    Private Property SqlConnStr As String = ""

    ''' <summary>
    ''' Records how many times To cancel deleting a row If it was canceled by user
    ''' </summary>
    Private Property cancelDelete As Integer = 0


    Protected Friend Property dgv As DataGridView


    ''' <summary>
    ''' stores a list of the 'unmodified' jobs. 
    ''' </summary>
    ''' <returns></returns>
    Private Property AllJobQRows As New Dictionary(Of Integer, JQRowInfo)
    ''' <summary>
    ''' the list of displayed jobs
    ''' </summary>
    ''' <returns></returns>
    Public Property DisplayedJobs As New System.ComponentModel.BindingList(Of JQRowInfo)



    Public Enum DisplayOptions
        DisplayAll
        DisplayCurrent
        DisplayArchive
    End Enum
    Public currentDisplay As DisplayOptions = DisplayOptions.DisplayCurrent


    ''' <summary>
    ''' the email log IO
    ''' </summary>
    ''' <returns></returns>
    Public Property emailLogs As EmailLogIO


#End Region


#Region "Init"

    ''' <summary>
    ''' Creates a new sql connection
    ''' </summary>
    ''' <param name="SqlConnStr">sql connection info</param>
    ''' <param name="aDataGridView">datagridview to update</param>
    Public Sub New(ByVal SqlConnStr As String,
                   ByVal aDataGridView As DataGridView,
                   ByVal ProdUvInfo As UvProductInfoIO)


        Me.SqlConnStr = SqlConnStr
        dgv = aDataGridView
        Me.MyUvProductInfoIO = ProdUvInfo
        MyJQProductionIO = New JQProductionIO()

        emailLogs = New EmailLogIO(SqlConnStr)


    End Sub

#End Region


#Region "CRUD Methods"

    ''' <summary>
    ''' gets a copy of the jobq table from sql
    ''' </summary>
    ''' <param name="errors">returns reason for failure</param>
    ''' <returns></returns>
    Public Function LoadSql(Optional ByRef errors As String = "") As Boolean
        Dim success As Boolean = True
        Try
            Dim list As New List(Of Integer)
            AllJobQRows.Clear()
            dgv.Columns.Clear()
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand()
                    Dim cols As List(Of String) = {"Item", "Title", "FinalQuantity", "Description", "Status", "Priority", "OrderPlaced", "DueDate", "SubmittedBy", "Comments", "CompletedOn", "LastUpdated", "IsActive", "StatusHistory", "JobID", "JobTicketCreated"}.ToList
                    cmd.CommandText = "SELECT " & String.Join(", ", cols) & " FROM QpJobs"
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()

                            Try
                                Dim Job As New JQRowInfo
                                Job.ItemNumber = reader.GetInt32(cols.IndexOf("Item"))
                                Job.Title = reader.GetString(cols.IndexOf("Title"))
                                Job.OrderQuantity = reader.GetInt32(cols.IndexOf("FinalQuantity"))
                                Job.Description = reader.GetString(cols.IndexOf("Description"))
                                Job.Status = reader.GetString(cols.IndexOf("Status"))
                                Job.Priority = reader.GetString(cols.IndexOf("Priority"))
                                Job.OrderPlaced = reader.GetDateTime(cols.IndexOf("OrderPlaced"))
                                Job.DueDate = reader.GetDateTime(cols.IndexOf("DueDate"))
                                Job.SubmittedBy = reader.GetString(cols.IndexOf("SubmittedBy"))
                                Job.Comments = reader.GetString(cols.IndexOf("Comments"))
                                Job.CompletedOn = reader.GetDateTime(cols.IndexOf("CompletedOn"))
                                Job.LastUpdated = reader.GetDateTime(cols.IndexOf("LastUpdated"))
                                Job.IsActive = reader.GetBoolean(cols.IndexOf("IsActive"))
                                Dim tempHist As String = reader.GetString(cols.IndexOf("StatusHistory"))
                                Job.StatusHistory = tempHist.Split(";").ToList
                                Job.JobID = reader.GetInt32(cols.IndexOf("JobID"))
                                Dim ticketHistory As String = reader.GetString(cols.IndexOf("JobTicketCreated"))
                                For Each history As String In ticketHistory.Split(";")
                                    If history.Trim <> "" Then
                                        Job.JobTicketHistory.Add(history)
                                    End If
                                Next



                                '---------- nothing check ---------- 

                                If Job.ItemNumber = cNullInt Then Job.ItemNumber = Nothing
                                If Job.OrderQuantity = cNullInt Then Job.OrderQuantity = Nothing
                                If Job.OrderPlaced = cNullDate Then Job.OrderPlaced = Nothing
                                If Job.DueDate = cNullDate Then Job.DueDate = Nothing
                                If Job.CompletedOn = cNullDate Then Job.CompletedOn = Nothing
                                If Job.LastUpdated = cNullDate Then Job.LastUpdated = Nothing

                                '----------------------------------- 


                                If Not AllJobQRows.ContainsKey(Job.JobID) Then
                                    AllJobQRows.Add(Job.JobID, Nothing)
                                End If
                                AllJobQRows(Job.JobID) = Job
                            Catch ex As Exception
                                MsgBox(ex.Message)
                            End Try



                        End While

                        updateDisplayedJobs()
                    End Using
                End Using
            End Using


        Catch ex As Exception
            success = False
            errors &= ex.Message & vbCrLf
        End Try

        Return success
    End Function

    ''' <summary>
    ''' re-loads the displayed jobs from AllJobQRows
    ''' </summary>
    Public Sub updateDisplayedJobs(Optional ByVal SearchStr As String = "")
        DisplayedJobs.Clear()
        DisplayedJobs = New System.ComponentModel.BindingList(Of JQRowInfo)
        Dim tempJobs As New List(Of JQRowInfo)
        For Each job As JQRowInfo In AllJobQRows.Values
            Dim displayJob As Boolean = False
            Select Case currentDisplay
                Case DisplayOptions.DisplayAll
                    displayJob = True
                Case DisplayOptions.DisplayArchive
                    If Not job.IsActive Then
                        displayJob = True
                    End If
                Case DisplayOptions.DisplayCurrent
                    If job.IsActive Then
                        displayJob = True
                    End If
            End Select
            If displayJob Then

                If SearchStr = "" Then
                    tempJobs.Add(job.Clone)
                Else
                    'theres a search
                    Dim JobStr As String = job.ToString
                    Dim searches As String() = SearchStr.Split(";")
                    Dim matched As Boolean = False
                    For Each newSearch As String In searches
                        If JobStr.IndexOf(newSearch, StringComparison.OrdinalIgnoreCase) >= 0 Then
                            'match made
                            matched = True
                        End If
                    Next

                    If matched Then
                        tempJobs.Add(job.Clone)
                    End If

                End If

            End If
        Next
        If currentDisplay = DisplayOptions.DisplayCurrent Then
            tempJobs.Sort(Function(y, x) y.Status.CompareTo(x.Status))
        Else
            tempJobs.Sort(Function(y, x) x.CompletedOn.CompareTo(y.CompletedOn))
        End If

        For Each job As JQRowInfo In tempJobs
            DisplayedJobs.Add(job)
        Next



        formatColumns()
        dgv.DataSource = DisplayedJobs
    End Sub


    ''' <summary>
    ''' updates the sql datatable from the dgv
    ''' </summary>
    ''' <param name="errors">returns reason for failure</param>
    ''' <returns></returns>
    Public Function updateSql(Optional ByRef errors As String = "") As Boolean
        Dim success As Boolean = True
        Try
            LineUp.Validate()
            dgv.EndEdit()
            Dim rowsToUpdate As New List(Of JQRowInfo) 'the list of jobs that have changed
            For Each Job As JQRowInfo In DisplayedJobs
                Dim id As Integer = Job.JobID

                If AllJobQRows(id).ToString <> Job.ToString Then
                    'different contents, update

                    rowsToUpdate.Add(Job)

                End If

            Next
            If rowsToUpdate.Count > 0 Then
                'updates the sql table with the new jobs
                AddUpdateJobsToSqlTable(rowsToUpdate, InsertOrUpdate.Update)
                formatCells()
                For Each job As JQRowInfo In rowsToUpdate
                    AllJobQRows(job.JobID) = job.Clone
                Next
            End If

        Catch ex As Exception
            LoadSql()

            success = False
            errors &= ex.Message & vbCrLf
        End Try
        Return success
    End Function


    ''' <summary>
    ''' adds the list of jobs to the sql table
    ''' </summary>
    ''' <param name="jobs"></param>
    Public Sub AddUpdateJobsToSqlTable(ByVal jobs As List(Of JQRowInfo), ByVal AddVsUpdate As InsertOrUpdate)
        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand()
                    Dim query As String = ""
                    Select Case AddVsUpdate
                        Case InsertOrUpdate.InsertInto
                            query &= "INSERT INTO QpJobs "
                            query &= "(Item, Title, FinalQuantity, Description, Status, Priority, OrderPlaced, DueDate, SubmittedBy, Comments, CompletedOn, LastUpdated, IsActive, StatusHistory, JobTicketCreated) VALUES "
                            query &= "(@Item, @Title, @FinalQuantity, @Description, @Status, @Priority, @OrderPlaced, @DueDate, @SubmittedBy, @Comments, @CompletedOn, @LastUpdated, @IsActive, @StatusHistory, @JobTicketCreated) "
                        Case InsertOrUpdate.Update
                            query &= "UPDATE QpJobs "
                            query &= "SET Item = @Item, Title = @Title, FinalQuantity = @FinalQuantity, Description = @Description, Status = @Status, Priority = @Priority, OrderPlaced = @OrderPlaced, DueDate = @DueDate, SubmittedBy = @SubmittedBy, Comments = @Comments, CompletedOn = @CompletedOn, LastUpdated = @LastUpdated, IsActive = @IsActive, StatusHistory = @StatusHistory, JobTicketCreated = @JobTicketCreated "
                            query &= "WHERE JobID = @JobID"
                        Case Else

                    End Select
                    If query <> "" Then
                        cmd.CommandText = query
                        conn.Open()
                        For Each job As JQRowInfo In jobs
                            job.SelfCheck()

                            With cmd.Parameters
                                .Clear()
                                If AddVsUpdate = InsertOrUpdate.Update Then .Add("@JobID", SqlDbType.Int).Value = job.JobID
                                .Add("@Item", SqlDbType.Int).Value = job.ItemNumber
                                .Add("@Title", SqlDbType.VarChar).Value = job.Title
                                .Add("@FinalQuantity", SqlDbType.Int).Value = job.OrderQuantity
                                .Add("@Description", SqlDbType.VarChar).Value = job.Description
                                .Add("@Status", SqlDbType.VarChar).Value = job.Status
                                .Add("@Priority", SqlDbType.VarChar).Value = job.Priority
                                .Add("@OrderPlaced", SqlDbType.Date).Value = job.OrderPlaced
                                .Add("@DueDate", SqlDbType.Date).Value = job.DueDate
                                .Add("@SubmittedBy", SqlDbType.VarChar).Value = job.SubmittedBy
                                .Add("@Comments", SqlDbType.VarChar).Value = job.Comments
                                .Add("@CompletedOn", SqlDbType.Date).Value = job.CompletedOn
                                .Add("@LastUpdated", SqlDbType.DateTime).Value = job.LastUpdated
                                .Add("@IsActive", SqlDbType.Bit).Value = job.IsActive
                                .Add("@StatusHistory", SqlDbType.VarChar).Value = job.StatusHistoryStr
                                .Add("@JobTicketCreated", SqlDbType.VarChar).Value = String.Join(";", job.JobTicketHistory)

                                cmd.ExecuteNonQuery()

                                If AddVsUpdate = InsertOrUpdate.InsertInto Then
                                    Dim q2 As String = "Select @@Identity"
                                    Using cmd2 As SqlCommand = conn.CreateCommand
                                        cmd2.CommandText = q2
                                        job.JobID = cmd2.ExecuteScalar
                                    End Using

                                End If

                            End With
                        Next

                    End If

                End Using

            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function DeleteJobsFromSql(ByVal jobsToDelete As List(Of JQRowInfo), Optional sendEmail As Boolean = True) As Boolean
        Dim success As Boolean = False
        Try
            If jobsToDelete.Count > 0 Then
                Dim ids As New List(Of Integer)
                For Each job As JQRowInfo In jobsToDelete
                    ids.Add(job.JobID)
                    If sendEmail Then
                        Dim t As Task = Task.Factory.StartNew(Sub() job.SendEmail(JQRowInfo.EmailStatus.Deleted))

                    End If
                Next
                Using conn As New SqlConnection(SqlConnStr)
                    Using cmd As SqlCommand = conn.CreateCommand
                        cmd.CommandText = "DELETE FROM QpJobs WHERE JobID IN (" & String.Join(", ", ids) & ")"
                        conn.Open()
                        If cmd.ExecuteNonQuery = jobsToDelete.Count Then
                            success = True
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "DeleteJobsFromSql")
        End Try
        Return success
    End Function


    Public Function RemoveDGVRow(Optional ByVal SendEmail As Boolean = True) As Boolean
        Dim CancelRemovingRow As Boolean = False
        Try
            'cancel delete is used to record # of times to cancel the prompt & action, if user says to CANCEL deleting rows.
            If cancelDelete > 0 Then
                '- counter
                cancelDelete -= 1
                'cancels the event
                CancelRemovingRow = True
                MsgBox("CancelDelete")
            Else
                Dim Prompt As String = "Delete the following?" & vbCrLf & vbCrLf
                Dim deleteJobs As New List(Of JQRowInfo)
                For Each row As DataGridViewRow In dgv.SelectedRows
                    Dim rowId As Integer = row.Cells(col_JobID).Value
                    If AllJobQRows.ContainsKey(rowId) Then
                        deleteJobs.Add(AllJobQRows(rowId))
                        Prompt &= AllJobQRows(rowId).getSummaryStr & vbCrLf
                    End If

                Next
                Select Case MsgBox(Prompt, MsgBoxStyle.YesNo, "Remove?")
                    Case MsgBoxResult.No
                        'sets cancelDelete to 1 less than the amount of selected rows. (no off by 1 error)
                        cancelDelete = dgv.SelectedRows.Count - 1
                        CancelRemovingRow = True
                    Case MsgBoxResult.Yes
                        'removes each selected row
                        DeleteJobsFromSql(deleteJobs, SendEmail)
                        LoadSql()
                        formatCells()
                        CancelRemovingRow = True
                End Select

            End If

        Catch ex As Exception
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            Beep()
        End Try

        Return CancelRemovingRow
    End Function

#End Region


#Region "Checks and Formatting Methods"


    ''' <summary>
    ''' Checks the cells of the dgvJobQ and does different things depending on which cell is being edited
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub NewRowCheck(ByVal myProductionIO As JQProductionIO)

        Try

            ' if the current row is a new row, then...
            If Not AllJobQRows.ContainsKey(dgv.CurrentRow.Cells(col_JobID).Value) Then
                'records current column
                Dim colNum As Integer = dgv.CurrentCell.ColumnIndex

                If Not IsDBNull(dgv.CurrentCell.Value) AndAlso dgv.CurrentCell.Value.ToString() <> "" Then
                    'it only updates it if it's not empty

                    Dim job As New JQRowInfo
                    job.Status = "Not Started"
                    job.OrderPlaced = Today
                    job.DueDate = Today.AddDays(14)
                    job.SubmittedBy = Environment.UserName

                    ' if someone types in a product number, it tries to find the product & input all the info it can
                    If colNum = 0 Then

                        'tries to add info to the row if possible

                        job.ItemNumber = dgv.CurrentRow.Cells(0).Value
                        job.ProductionFiles = myProductionIO.GetProductionFiles(job.ItemNumber)

                        job.uvProdInfo = MyUvProductInfoIO.findProduct(job.ItemNumber)

                        job.ConfigureDescription()

                        ''adds the description of the product based upon CheckType & xml
                        'Dim CheckedProdType As String = prodDirIO.ReturnCheckedProdType(prodDirIO.CurrentExport.ProductType, uvProd, uvProd.PageCt)
                        'If CheckedProdType = cUnknown Or CheckedProdType = "" Then
                        '    CheckedProdType = job.uvProdInfo.convertUvType
                        'End If
                        ''enters a product description if it can find it
                        'job.Description = CheckedProdType 'dgv.CurrentRow.Cells(3).Value = CheckedProdType 'ProdDirInfo.currentExport.ProdType

                        'If job.uvProdInfo.SupID = cShareWordSupID Then
                        '    ' it found a share word match.
                        '    job.Description = cShareWord
                        'End If

                        'inserts title
                        job.Title = job.uvProdInfo.Title

                        ''if it's a tract card then it adds the address to the website for tract card printing
                        'If prodDirIO.CurrentExport.ProductType = "Tract Card" Then
                        '    job.Comments = "http://www.zooprinting.com/"
                        'End If
                    End If

                    Select Case colNum
                        Case 0
                            If IsNumeric(dgv.CurrentCell.Value.ToString()) Then job.ItemNumber = dgv.CurrentCell.Value
                        Case 1
                            job.Title = dgv.CurrentCell.Value
                        Case 2
                            job.OrderQuantity = dgv.CurrentCell.Value
                        Case 3
                            job.Status = dgv.CurrentCell.Value
                        Case 4
                            job.Priority = dgv.CurrentCell.Value
                    End Select
                    'job.SelfCheck()
                    AddUpdateJobsToSqlTable({job}.ToList, InsertOrUpdate.InsertInto)
                    'LoadSql()
                    AllJobQRows.Add(job.JobID, job.Clone)
                    DisplayedJobs.Add(job)
                    For i As Integer = DisplayedJobs.Count - 1 To 0 Step -1
                        If DisplayedJobs(i).JobID = cNullInt Then
                            DisplayedJobs.RemoveAt(i)
                        End If
                    Next
                    dgv.DataSource = DisplayedJobs
                    'updateDisplayedJobs()
                    formatCells()

                    'send email
                    Dim t As Task = Task.Factory.StartNew(Sub() job.SendEmail(JQRowInfo.EmailStatus.Ordered))




                    'since it's a new row, it exits before updating sql

                    Exit Sub




                End If



            End If

        Catch ex As Exception
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try


        updateSql()



    End Sub

    ''' <summary>
    ''' Formats the cells - background color, font color, etc.
    ''' </summary>
    ''' <param name="errors"></param>
    ''' <returns></returns>
    Public Function formatCells(ByRef Optional errors As String = "") As Boolean
        Dim success As Boolean = True
        Try
            'sets the background for each cell in the current status column based on the following words

            For Each row As DataGridViewRow In dgv.Rows
                'keeps track of the difference between due date & today
                Dim timediff As TimeSpan
                'TimeString is used to record the tool tip for each cell
                Dim TimeStr As String = ""

                If row.Index < dgv.Rows.Count - 1 Then
                    If AllJobQRows.ContainsKey(row.Cells(col_JobID).Value) Then
                        Dim curJob As JQRowInfo = AllJobQRows(row.Cells(col_JobID).Value)

                        If curJob.JobTicketWasPrinted Then
                            With row.Cells(col_FinalQuan)
                                ' .ReadOnly = True
                                .ToolTipText = "Job ticket was printed."
                                .Style.BackColor = Color.LightSlateGray
                                .Style.SelectionBackColor = Color.LightSlateGray
                            End With
                        End If

                        'Dim days As Double = curJob.GetJobDurationInDays

                        'converts the dates to days away

                        'takes the due date value(7), and subtracts it from today's date
                        If curJob.DueDate <> cNullDate And curJob.DueDate <> New Date Then
                            timediff = curJob.DueDate.Subtract(Date.Today)
                            Select Case timediff.TotalDays
                                Case < 0
                                    'if its due date is older than today (ie. yesterday)
                                    TimeStr = ("This is " & timediff.TotalDays * -1 & " days late")
                                Case = 0
                                    'if its due date is today...
                                    TimeStr = ("Due today!")
                                Case = 1
                                    'due tomorrow
                                    TimeStr = ("Due tomorrow")
                                Case > 1
                                    'due days away
                                    TimeStr = ("This has " & timediff.TotalDays & " days left")
                                Case Else
                                    TimeStr = ""
                            End Select
                        Else
                            TimeStr = ""
                        End If

                        'sets the timestring to be the tooltiptext
                        row.Cells(col_DueDate).ToolTipText = TimeStr
                        row.Cells(col_Status).ToolTipText = TimeStr


                        'if it contains "zdone" it's handled later in the sub
                        If Not curJob.Status.Contains("zdone") Then

                            With row.Cells(col_DueDate).Style
                                Select Case timediff.TotalDays

                                    Case Is > 3
                                        'if it's due 4 or more days in the future, it turns green, selection = darker green
                                        .BackColor = Color.PaleGreen
                                        .SelectionBackColor = Color.OliveDrab
                                    Case Is >= 1
                                        'If it's due tomorrow, turns orange, selection, darker orange
                                        .BackColor = Color.Orange
                                        .SelectionBackColor = Color.DarkGoldenrod

                                    Case Is <= 0
                                        'if it's due today, or older than today, it turns purple, selection = darker purple
                                        .BackColor = Color.Fuchsia
                                        .SelectionBackColor = Color.DarkViolet

                                    Case Else
                                        'applies the same background color as the rest of the spreadsheet
                                        If row.Cells(7).RowIndex Mod 2 = 0 Then
                                            'it's even? anyway it matches the rest of the spreadsheet
                                            .BackColor = Color.White
                                            .SelectionBackColor = Color.DodgerBlue
                                        Else
                                            'it's odd? anyway it matches the rest of the spreadsheet
                                            .BackColor = Color.Azure
                                            .SelectionBackColor = Color.DodgerBlue
                                        End If
                                End Select
                            End With
                        End If



                        If curJob.Description.Contains(cShareWord) Then
                            row.Cells(col_Description).Style.BackColor = Color.Turquoise
                        End If


                        '-------- Status History --------
                        Dim sb As New Text.StringBuilder
                        For Each history As String In curJob.StatusHistory
                            sb.AppendLine(history.Replace(cSplit, " - "))
                        Next
                        row.Cells(col_Status).ToolTipText = sb.ToString
                        '--------------------------------


                        'Current status - color coding
                        With row.Cells(col_Status).Style
                            Dim lowerStatus As String = curJob.Status.ToLower
                            Select Case True

                                Case curJob.Status.ToLower.Contains("zdone")
                                    'sets the tooltip & background colors if the status contains zdone
                                    row.Cells(col_DueDate).ToolTipText = "All Done!"
                                    row.Cells(col_Status).ToolTipText = "All Done!"

                                    'makes the cells match the rest of the spreadsheet
                                    If row.Cells(col_Status).RowIndex Mod 2 = 0 Then
                                        .BackColor = Color.White
                                        .SelectionBackColor = Color.DodgerBlue
                                        row.Cells(col_DueDate).Style.BackColor = Color.White
                                        row.Cells(col_DueDate).Style.SelectionBackColor = Color.DodgerBlue
                                    Else
                                        .BackColor = Color.Azure
                                        row.Cells(col_DueDate).Style.BackColor = Color.Azure
                                        .SelectionBackColor = Color.DodgerBlue
                                        row.Cells(col_DueDate).Style.SelectionBackColor = Color.DodgerBlue
                                    End If

                                Case lowerStatus.Contains("not started")
                                    .BackColor = Color.Red
                                    .SelectionBackColor = Color.DarkRed

                                Case lowerStatus.Contains("sar")
                                    .BackColor = Color.LightPink
                                    .SelectionBackColor = Color.DeepPink

                                Case lowerStatus.Contains("mrg")
                                    .BackColor = Color.LightPink
                                    .SelectionBackColor = Color.DeepPink

                                Case lowerStatus.Contains("design")
                                    .BackColor = Color.LightPink
                                    .SelectionBackColor = Color.DeepPink

                                Case lowerStatus.Contains("waiting")
                                    .BackColor = Color.Yellow
                                    .SelectionBackColor = Color.DarkOrange

                                Case lowerStatus.Contains("ing")
                                    .BackColor = Color.GreenYellow
                                    .SelectionBackColor = Color.Green

                                Case Else
                                    .BackColor = row.DefaultCellStyle.BackColor 'Color.White
                                    .SelectionBackColor = row.DefaultCellStyle.SelectionBackColor

                            End Select

                        End With

                    End If

                End If

            Next

        Catch ex As Exception
            success = False
            errors &= ex.Message & vbCrLf
        End Try
        Return success
        'Throw New NotImplementedException()
    End Function
    Public Function AddColumn(ByVal colName As String, ByVal headerText As String, ByVal whatType As Type,
                              ByVal dataProperty As String, ByVal width As Integer,
                              Optional ByVal viewMode As DataGridViewAutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet) As DataGridViewColumn
        Dim cell As New DataGridViewTextBoxCell()
        Dim Col As New DataGridViewColumn With {
                .Name = colName,
                .CellTemplate = cell,
                .HeaderText = headerText,
                .ValueType = whatType,
                .Width = width * 2,
                .MinimumWidth = width,
                .AutoSizeMode = viewMode
            }

        If dataProperty <> "" Then Col.DataPropertyName = dataProperty

        Return Col
    End Function

    ''' <summary>
    ''' Formats the Columns - Column Width, sort order, etc.
    ''' </summary>
    ''' <param name="errors"></param>
    ''' <returns></returns>
    Public Function formatColumns(Optional ByRef errors As String = "") As Boolean

        Dim success As Boolean = True

        Try
            dgv.AutoGenerateColumns = False


            dgv.Columns.Clear()
            dgv.BackgroundColor = Color.White

            With dgv.Columns
                .Add(AddColumn(Col_Item, "Item", GetType(Integer), "ItemNumberWrapper", 25))
                .Add(AddColumn(Col_Title, "Title", GetType(String), "Title", 100, DataGridViewAutoSizeColumnMode.Fill))
                '.Add(getColumn(col_FinalQuan, "Final Quan", GetType(Integer), "OrderQuantity", 50))
                .Add(AddColumn(col_FinalQuan, "Final Quan", GetType(Integer), "OrderQuantityWrapper", 50))
                .Add(AddColumn(col_Description, "Description", GetType(String), "Description", 75))
                .Add(AddColumn(col_Status, "Status", GetType(Integer), "Status", 150))
                .Add(AddColumn(col_Priority, "Priority", GetType(Integer), "Priority", 50))
                .Add(AddColumn(col_OrderPlaced, "Order Placed", GetType(Date), "OrderPlaced", 50, ))
                .Add(AddColumn(col_DueDate, "Due Date", GetType(Date), "DueDate", 50))
                .Add(AddColumn(col_SubmittedBy, "Submitted By", GetType(String), "SubmittedBy", 50))
                .Add(AddColumn(col_Comments, "Comments", GetType(String), "Comments", 75))
                .Add(AddColumn(col_CompletedOn, "Completed On", GetType(Date), "CompletedOn", 50))
                .Add(AddColumn(Col_Filler, "", GetType(String), "", 2, DataGridViewAutoSizeColumnMode.Fill))
                .Add(AddColumn(col_JobID, "Job ID", GetType(Integer), "JobID", 2, DataGridViewAutoSizeColumnMode.None))
            End With

            'dgv.AutoResizeColumns()
            success = True


        Catch ex As Exception
            success = False
            errors &= ex.Message & vbCrLf
        End Try
        Return success
    End Function

#End Region


#Region "Methods to Convert from old Databases"


    ''' <summary>
    ''' gets a copy of the jobq table from sql
    ''' </summary> 
    ''' <returns></returns>
    Public Function loadSqlFromOldDB(ByVal table As String) As Boolean
        Dim success As Boolean = True
        Try
            dgv.Columns.Clear()

            Dim query As String = "" '"SELECT Item, Title, FinalQuantity, Description, Status, Priority, OrderPlaced, DueDate, SubmittedBy, Comments, CompletedOn, LastUpdated, IsActive, StatusHistory, JobID FROM QpJobs"
            query = "SELECT Item, Title, FinalQuantity, Description, Status, Priority, OrderPlaced, DueDate, SubmittedBy, Comments, CompletedOn FROM " & table & " ORDER BY IDTime ASC"
            Dim dt As New DataTable
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    cmd.CommandText = query
                    Using adapt As New SqlDataAdapter(cmd)
                        adapt.Fill(dt)
                    End Using
                End Using
            End Using

            Dim jobs As List(Of JQRowInfo) = GetJobsFromOldDT(dt)
            AddUpdateJobsToSqlTable(jobs, InsertOrUpdate.InsertInto)
            'updateDisplayedJobs()
        Catch ex As Exception
            MsgBox(ex.Message)
            success = False

        End Try

        Return success
    End Function




    ''' <summary>
    ''' returns a list of the jobs from the specified data table
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    Private Function GetJobsFromOldDT(ByVal dt As DataTable) As List(Of JQRowInfo)

        Dim newJobs As New List(Of JQRowInfo)
        For Each dRow As DataRow In dt.Rows
            Dim job As New JQRowInfo
            Dim err As String = ""

            For Each col As DataColumn In dt.Columns
                Try
                    If Not IsDBNull(dRow(col)) AndAlso dRow(col).ToString <> "" Then
                        Select Case col.ColumnName
                            Case Col_Item
                                job.ItemNumber = dRow(col)
                            Case Col_Title
                                job.Title = dRow(col)
                            Case col_FinalQuan
                                job.OrderQuantity = dRow(col)
                            Case col_Description
                                job.Description = dRow(col)
                            Case col_Status
                                job.Status = dRow(col)
                            Case col_Priority
                                job.Priority = dRow(col)
                            Case col_OrderPlaced
                                job.OrderPlaced = dRow(col)
                            Case col_DueDate
                                job.DueDate = dRow(col)
                            Case col_SubmittedBy
                                job.SubmittedBy = dRow(col)
                            Case col_Comments
                                job.Comments = dRow(col)
                            Case col_CompletedOn
                                job.CompletedOn = dRow(col)
                            Case col_LastUpdated
                                job.LastUpdated = dRow(col)
                            Case col_IsActive
                                job.IsActive = dRow(col)
                            Case col_JobID
                                job.JobID = dRow(col)
                            Case Else
                                MsgBox(col.ColumnName, MsgBoxStyle.OkOnly, "GetJobsFromDT")
                        End Select
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.Message)

                    err &= ex.Message
                End Try

            Next

            '-------- last updated check -------
            Dim lastUpdated As Date = job.OrderPlaced
            If job.CompletedOn > lastUpdated Then lastUpdated = job.CompletedOn
            job.LastUpdated = lastUpdated
            '----------------------------------- 


            '---------- nothing check ---------- 

            If job.ItemNumber = cNullInt Then job.ItemNumber = Nothing
            If job.OrderQuantity = cNullInt Then job.OrderQuantity = Nothing
            If job.OrderPlaced = cNullDate Then job.OrderPlaced = Nothing
            If job.DueDate = cNullDate Then job.DueDate = Nothing
            If job.LastUpdated = cNullDate Then job.LastUpdated = Nothing
            If job.CompletedOn = cNullDate Then
                job.CompletedOn = Nothing
            Else
                job.IsActive = False
            End If
            '----------------------------------- 


            newJobs.Add(job)

        Next
        Return newJobs
    End Function


#End Region


#Region "Accounting"


    Public Sub CreateAccountingFileForYear()
        Dim acctDlg As New YearEndAcctDialog

        Dim years As New List(Of Integer)
        For Each myJob As JQRowInfo In AllJobQRows.Values
            If Not years.Contains(myJob.CompletedOn.Year) Then
                years.Add(myJob.CompletedOn.Year)
            End If
        Next
        acctDlg.cmbYear.Items.Clear()
        For Each myYear In years
            'adds the years found in the datagridview
            acctDlg.cmbYear.Items.Add(myYear)
        Next
        acctDlg.cmbYear.Sorted = True

        If acctDlg.cmbYear.Items.Count > 0 Then 'selects the most recent year.
            acctDlg.cmbYear.SelectedIndex = acctDlg.cmbYear.Items.Count - 1
        End If

        If acctDlg.ShowDialog = MsgBoxResult.Ok Then

            Dim lstTypeUV As New List(Of String)
            Dim lstQuan As New List(Of String)
            Dim lstCost As New List(Of String)
            Dim selYear As Integer = Integer.Parse(acctDlg.cmbYear.SelectedItem)

            'Dim dictUvType As New Dictionary(Of String, String)
            'Dim dictCost As New Dictionary(Of String, String)

            Dim SuccessLst As New List(Of String)
            SuccessLst.Add(JobtoString(Nothing, Nothing, "", "", True)) 'creates 'header'

            Dim FailedLst As New List(Of String)
            FailedLst.Add(JobtoString(Nothing, Nothing, "", "", True)) 'creates 'header'

            'Dim lstUvType As New List(Of String)
            Dim Category As String = ""

            For Each job As JQRowInfo In AllJobQRows.Values
                Dim prodInfo As New UvProductInfo(False)


                Category = ""
                Dim Success As Boolean = False
                Dim Failed As Boolean = False
                Dim FailedReason As New Text.StringBuilder
                Dim okLanguages As List(Of String) = {"english", "spanish"}.ToList
                Try
                    If job.CompletedOn.Year = selYear Then
                        If Not job.ItemNumberIsValid Then
                            Failed = True
                            FailedReason.Append("No/Invalid Product Number. ")
                        Else
                            'ok so far, gets info for the prod number
                            prodInfo = MyUvProductInfoIO.findProduct(job.ItemNumber).clone
                            If prodInfo.PricePer > 100 And prodInfo.TypeUV <> "BI" Then
                                MsgBox(prodInfo.ToString)
                            End If
                            If IsNothing(prodInfo) Then
                                'error - product not found.
                                Success = False
                                Failed = True
                                FailedReason.Append("Unable to find product number. ")
                            Else
                                If prodInfo.TypeUV.ToUpper = "TR" AndAlso prodInfo.SubType.ToLower = "tc" Then
                                    'makes the price of tract cards per card instead of per 100 pack
                                    prodInfo.SalePrice = prodInfo.SalePrice / 100
                                End If

                                'calculates total sales price

                                prodInfo.PricePer = prodInfo.SalePrice


                                If prodInfo.SalePrice > 0 AndAlso job.OrderQuantity > 0 Then
                                    prodInfo.SalePrice = job.OrderQuantity * prodInfo.SalePrice 'gets total sales price
                                Else
                                    prodInfo.SalePrice = 0
                                End If
                                Category = prodInfo.TypeUV
                                If prodInfo.SubType <> "" AndAlso prodInfo.SubType.ToLower = "ta" Then
                                    Category = prodInfo.SubType
                                End If
                                If prodInfo.Language <> "" Then
                                    If prodInfo.Language.ToLower = "spanish" Then
                                        Category = "SP"
                                    End If
                                End If


                                If prodInfo.Language <> "" And Not okLanguages.Contains(prodInfo.Language.ToLower) Then
                                    Failed = True
                                    FailedReason.Append("Language other than English or Spanish. ")
                                ElseIf Category = "" Then
                                    Failed = True
                                    FailedReason.Append("Uv category not found. ")

                                ElseIf lstTypeUV.Contains(Category) Then
                                    If job.OrderQuantity = 0 Or job.OrderQuantity = cNullInt Then
                                        Failed = True
                                        FailedReason.Append("Quantity not found. ")
                                    Else

                                        For i As Integer = 0 To lstTypeUV.Count - 1
                                            If lstTypeUV.Item(i) = Category Then
                                                'adds the quantites together for a total quantity
                                                lstQuan(i) += job.OrderQuantity

                                                'adds the prices together to get a total sales price
                                                lstCost(i) += prodInfo.SalePrice

                                            End If
                                        Next
                                        Success = True
                                    End If

                                Else
                                    lstTypeUV.Add(Category)
                                    lstQuan.Add(job.OrderQuantity)
                                    lstCost.Add(prodInfo.SalePrice)
                                    Success = True
                                End If
                            End If



                        End If
                    End If


                    If Success Then
                        SuccessLst.Add(JobtoString(job, prodInfo, Category))
                    End If

                    If Failed Then
                        FailedLst.Add(JobtoString(job, prodInfo, "", FailedReason.ToString))
                    End If

                Catch ex As Exception



                    FailedLst.Add(JobtoString(job, prodInfo, "", ex.Message))
                End Try

            Next
            Dim s As String = ""

            'Dim Success As String = ""
            'Dim Failed As String = ""

            'creates the first table
            s &= "UV Type,Quantity,Sales Price" & vbCrLf
            For i As Integer = 0 To lstTypeUV.Count - 1
                s &= lstTypeUV(i) & "," & lstQuan(i) & "," & lstCost(i) & vbCrLf
            Next

            'creates the success table
            'converts the success list to string
            s &= vbCrLf & vbCrLf & vbCrLf & "Success:" & vbCrLf
            For Each item As String In SuccessLst
                s &= item & vbCrLf
            Next


            'converts the failed list to string
            s &= vbCrLf & vbCrLf & vbCrLf & "Failed:" & vbCrLf
            For Each item As String In FailedLst
                s &= item & vbCrLf
            Next




            Try
                Dim ResultWriter As New StreamWriter(acctDlg.txtSave.Text)
                'MsgBox(s)
                ResultWriter.Write(s)
                ResultWriter.Close()
                MsgBox("File exported to: " & acctDlg.txtSave.Text)
            Catch ex As Exception
                'Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Write Error")
            End Try

        End If


    End Sub

    ''' <summary>
    ''' returns the details of the job as a string
    ''' </summary>
    ''' <param name="job"></param>
    ''' <param name="prodInfo"></param>
    ''' <param name="Category"></param>
    ''' <param name="aFailedReason"></param>
    ''' <param name="createHeader">create a header string instead of job details</param>
    ''' <returns></returns>
    Private Function JobtoString(ByVal job As JQRowInfo,
                                 ByVal prodInfo As UvProductInfo,
                                 Optional ByVal Category As String = "",
                                 Optional ByVal aFailedReason As String = "",
                                 Optional ByVal createHeader As Boolean = False) As String

        Dim sb As New Text.StringBuilder
        Dim jobParts As New List(Of String)

        If createHeader Then
            With jobParts
                .Add("")
                .Add("Item Number")
                .Add("Title")
                .Add("Order Quantity")
                .Add("Description")
                .Add("Status")
                .Add("Priority")
                .Add("Order Placed Date")
                .Add("Due Date")
                .Add("Submitted By")
                .Add("Comments")
                .Add("Completed On Date")
                .Add("Job Ticket Created")

                .Add("UV Type")
                .Add("UV SubType")
                .Add("Language")
                .Add("Category")
                .Add("Price Per")
                .Add("Sale Price")

                .Add("Failed Reason")
            End With
        Else
            With jobParts
                .Add("")
                .Add(job.ItemNumber)
                .Add(job.Title)
                .Add(job.OrderQuantity)
                .Add(job.Description)
                .Add(job.Status)
                .Add(job.Priority)
                .Add(job.OrderPlaced.ToShortDateString)
                .Add(job.DueDate.ToShortDateString)
                .Add(job.SubmittedBy)
                .Add(job.Comments)
                .Add(job.CompletedOn.ToShortDateString)
                .Add(String.Join(";", job.JobTicketHistory))

                .Add(prodInfo.TypeUV)
                .Add(prodInfo.SubType)
                .Add(prodInfo.Language)
                .Add(Category)
                .Add(prodInfo.PricePer)
                .Add(prodInfo.SalePrice)

                .Add(aFailedReason)
            End With
        End If



        For Each part As String In jobParts

            Try
                part = part.Replace("""", "")
                sb.Append(part.Replace(",", "") & ",")
            Catch ex As Exception
                'Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                sb.Append(",")
            End Try
        Next

        Return sb.ToString
    End Function

#End Region


#Region "Methods - Misc"




    ''' <summary>
    ''' Goes through, and updates the selected job statuses
    ''' </summary>
    Public Sub UpdateStatusOfSelectedJobs(Optional ByVal PromptQuestion As String = "Please enter a new status for the selected rows.", Optional ByVal DefaultStatus As String = "")
        Dim jobs As List(Of JQRowInfo) = GetSelectedJobs()
        If jobs.Count > 0 Then
            Dim status As String = jobs(0).Status
            If DefaultStatus <> "" Then status = DefaultStatus
            Dim result As String = InputBox(PromptQuestion, "Update Job Status?", status)
            If result <> "" Then
                For Each row As JQRowInfo In GetSelectedJobs()
                    row.Status = result
                Next
                Dim errors As String = ""
                updateSql(errors)
                If errors <> "" Then Throw New Exception(errors)
            End If
        Else
            Throw New Exception("No jobs selected")
        End If

    End Sub


    Public Function ClearLoadedProductionFiles() As Boolean
        Dim success As Boolean
        For Each row As JQRowInfo In DisplayedJobs
            row.DisposeAllProductionGB()
        Next
        Return success
    End Function


    ''' <summary>
    ''' returns a string with CompletedYear: Count. Each year is it's own line
    ''' </summary>
    ''' <returns></returns>
    Public Function getYearsCt() As String
        Dim results As New SortedDictionary(Of Integer, Integer)
        For Each job As JQRowInfo In AllJobQRows.Values
            Dim yr As Integer = job.CompletedOn.Year
            If Not results.ContainsKey(yr) Then
                results.Add(yr, 0)
            End If
            results(yr) += 1
        Next
        Dim sb As New Text.StringBuilder
        For Each key As Integer In results.Keys
            sb.AppendLine(key.ToString & ": " & results(key))
        Next
        Return sb.ToString()
    End Function


    Public Function GetDaysSinceOrderPlaced(ByVal displayOpt As DisplayOptions, Optional ByVal filterByFinishedYear As Integer = -1) As Double
        Dim sb As New Text.StringBuilder

        Dim days As Double = 0
        Dim itemCount As Integer = 0
        Dim longDurationIDs As New Text.StringBuilder
        Dim tempDate As New Date
        Try
            If Not IsNothing(AllJobQRows) AndAlso AllJobQRows.Count > 0 Then
                For Each job As JQRowInfo In AllJobQRows.Values
                    Dim processJob As Boolean = False
                    'gets / checks the finished date
                    Dim finishedDate As Date = job.CompletedOn
                    If finishedDate = tempDate Or finishedDate = cNullDate Then
                        finishedDate = Now 'no finished date recorded
                    End If
                    If filterByFinishedYear = -1 Then
                        processJob = True
                    Else
                        'year supplied, must filter
                        If finishedDate.Year = filterByFinishedYear Then processJob = True
                    End If

                    If processJob Then
                        Dim processed As Boolean = False
                        Select Case displayOpt
                            Case DisplayOptions.DisplayAll
                                days += job.GetJobDurationInDays
                                itemCount += 1
                                processed = True
                            Case DisplayOptions.DisplayArchive
                                If Not job.IsActive Then
                                    days += job.GetJobDurationInDays
                                    itemCount += 1
                                    processed = True
                                End If
                            Case Else
                                'gets current
                                If job.IsActive Then
                                    days += job.GetJobDurationInDays
                                    itemCount += 1
                                    processed = True
                                End If
                        End Select
                        If processed Then
                            Dim jobDuration As Double = job.GetJobDurationInDays
                            If jobDuration > 300 Then
                                longDurationIDs.AppendLine("Item #: " & job.ItemNumber & " - " & "JobID: " & job.JobID & ": " & jobDuration & " Days long")

                            End If
                        End If


                    End If



                Next

            End If
            If longDurationIDs.Length > 0 Then
                'Clipboard.SetText(longDurationIDs.ToString())
                MsgBox("These jobs had a seemingly longer duration than thought: " & vbCrLf & vbCrLf & longDurationIDs.ToString())
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Calculating Order Time")
        End Try

        Return days / itemCount
    End Function


    Public Function CreateXmlBackup(ByVal fullFilePath As String)
        Dim success As Boolean = False
        Try
            Dim xwSettings As New XmlWriterSettings
            xwSettings.Indent = True
            xwSettings.OmitXmlDeclaration = False
            Using xw As XmlWriter = XmlWriter.Create(fullFilePath, xwSettings)
                With xw
                    .WriteStartDocument()
                    .WriteStartElement("DGVBackup")
                    For Each backupJob As JQRowInfo In AllJobQRows.Values
                        .WriteStartElement("Job")
                        .WriteElementString(Col_Item, backupJob.ItemNumber)
                        .WriteElementString(Col_Title, backupJob.Title)
                        .WriteElementString(col_FinalQuan, backupJob.OrderQuantity.ToString)
                        .WriteElementString(col_Description, backupJob.Description)
                        .WriteElementString(col_Status, backupJob.Status)
                        .WriteElementString(col_Priority, backupJob.Priority)
                        .WriteElementString(col_OrderPlaced, backupJob.OrderPlaced.ToString)
                        .WriteElementString(col_DueDate, backupJob.DueDate.ToString)
                        .WriteElementString(col_SubmittedBy, backupJob.SubmittedBy)
                        .WriteElementString(col_Comments, backupJob.Comments)
                        .WriteElementString(col_CompletedOn, backupJob.CompletedOn.ToString)
                        .WriteElementString(col_LastUpdated, backupJob.LastUpdated.ToString)
                        .WriteElementString(col_IsActive, backupJob.IsActive.ToString)
                        .WriteElementString(Col_StatusHistory, backupJob.StatusHistoryStr)
                        .WriteElementString(col_JobID, backupJob.JobID.ToString)
                        .WriteElementString(col_JobTicketCreated, String.Join(";", backupJob.JobTicketHistory))
                        .WriteEndElement()
                    Next
                End With
                xw.Close()

            End Using


            success = True
        Catch ex As Exception
            success = False
            MsgBox(ex.Message)
        End Try
        Return success
    End Function


    ''' <summary>
    ''' looks through the selected cells in the dgv, and returns a list of jobQRow for the selected rows
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSelectedJobs() As List(Of JQRowInfo)
        Dim foundJobs As New List(Of JQRowInfo)
        Dim rowIndexes As New List(Of Integer)
        For Each myCell As DataGridViewCell In dgv.SelectedCells
            If Not rowIndexes.Contains(myCell.RowIndex) Then rowIndexes.Add(myCell.RowIndex)
        Next
        'Console.WriteLine("Row Indexes Count: " & rowIndexes.Count)
        For Each index As Integer In rowIndexes
            If index < DisplayedJobs.Count Then
                'keeps the index from throwing an error when the last row (new row) was selected.
                foundJobs.Add(DisplayedJobs(index))
            End If

        Next



        Return foundJobs
    End Function

    ''' <summary>
    ''' Creates an email with a job report. (doesn't send it)
    ''' </summary>
    ''' <param name="rowInfo">The job row to send the report about</param>
    ''' <param name="eStatus">"This item was finished today", or "This item was deleted today" or "This item was created today" </param>
    ''' <returns></returns>
    Public Shared Function CreateEmailReport(ByVal rowInfo As JQRowInfo, ByVal eStatus As JQRowInfo.EmailStatus) As EmailLogInfo

        Dim emailLog As New EmailLogInfo(True, EmailLogInfo.TypeOfEmail.JobStatusUpdate)

        emailLog.Subject = $"Product {eStatus.ToString} - {rowInfo.getSummaryStr}"



        'create new xml(html) doc  
        Dim xmlDoc As New XmlDocument()

        'create the html tag
        Dim xmlroot As XmlElement = xmlDoc.CreateElement("html")
        xmlDoc.AppendChild(xmlroot)

        'create head tag
        Dim xmlHead As XmlElement = xmlDoc.CreateElement("head")
        xmlroot.AppendChild(xmlHead)

        'create title tag
        Dim xmlTitle As XmlElement = xmlDoc.CreateElement("title")
        xmlTitle.AppendChild(xmlDoc.CreateTextNode("QP Items"))
        xmlHead.AppendChild(xmlTitle)

        'create body element and append it to the root element
        Dim xmlBody As XmlElement = xmlDoc.CreateElement("body")
        xmlBody.SetAttribute("style", "font-family:tahoma,geneva,sans-serif;font-size:16px;")
        xmlroot.AppendChild(xmlBody)


        AddXmlSection($"The following item(s) are {eStatus}:", {rowInfo}.ToList, False, xmlDoc, xmlBody)


        'create the automated message title
        Dim xmlH6 As XmlElement = xmlDoc.CreateElement("h5")
        xmlBody.AppendChild(xmlH6)
        xmlH6.AppendChild(xmlDoc.CreateTextNode("This is an automated message from LineUp. Automagically created at " & Now.ToString & ", by " & Environment.MachineName))

        emailLog.Contents = PrettyXML(xmlDoc.InnerXml)




        Return emailLog
    End Function

    ''' <summary>
    ''' Returns a new email log. Criteria for sending: it's Friday, No emails have been sent in the past 6 days, and there are items in the job q.
    ''' </summary>
    ''' <param name="forceCreateLog">Create a log regardless (even if it's not friday / due to be created)</param>
    ''' <param name="jqRow">Emails status of specific job</param>
    ''' <returns></returns>
    Public Function CreateEmailLog(Optional ByVal forceCreateLog As Boolean = False, Optional ByVal jqRow As JQRowInfo = Nothing) As EmailLogInfo

        Try
            Dim count As Integer = 0
            Dim createEmail As Boolean = True
            Dim emailLog As New EmailLogInfo(True)
            emailLog.EmailType = EmailLogInfo.TypeOfEmail.LineupLog
            'lists...
            Dim lateItems As New List(Of JQRowInfo)
            Dim dueTodayItems As New List(Of JQRowInfo)
            Dim okItems As New List(Of JQRowInfo)
            Dim completedRecently As New List(Of JQRowInfo)

            If Date.Today.DayOfWeek <> DayOfWeek.Friday Then
                'only send on fridays
                createEmail = False
            End If
            If createEmail Then
                'check to see if email has already been sent
                emailLogs.getSentEmails()
                Dim altEmailSent As Boolean = False
                For Each log As EmailLogInfo In emailLogs.SentEmails.Values
                    If log.EmailType = EmailLogInfo.TypeOfEmail.LineupLog Then 'only care about lineup email logs
                        If log.TimeSent.Date > Now.Date.AddDays(-7) Then 'it will only send if the most recent email is older than 6 days
                            altEmailSent = True
                        End If
                    End If
                Next
                'another computer / user sent the email already. don't send again
                If altEmailSent Then createEmail = False
            End If


            If forceCreateLog Then createEmail = True

            If createEmail Then
                'populate lists
                For Each job As JQRowInfo In AllJobQRows.Values
                    If job.IsActive Then
                        'not in archive
                        count += 1
                        If job.DueDate < Now.Date Then
                            'late job
                            lateItems.Add(job)
                        ElseIf job.DueDate = Now.Date Then
                            'due today
                            dueTodayItems.Add(job)
                        Else
                            'future due
                            okItems.Add(job)
                        End If
                    Else
                        If job.CompletedOn.Date >= Date.Today.AddDays(-7) Then
                            completedRecently.Add(job)
                        End If
                    End If
                Next


                If lateItems.Count > 0 Then
                    emailLog.Subject = "Past Due Items In Lineup (and other info)"
                ElseIf dueTodayItems.Count > 0 Then
                    emailLog.Subject = "Items Due Today In Lineup (and other info)"
                Else
                    emailLog.Subject = "Items in Lineup"
                End If
                'create new xml(html) doc  
                Dim xmlDoc As New XmlDocument()

                'create the html tag
                Dim xmlroot As XmlElement = xmlDoc.CreateElement("html")
                xmlDoc.AppendChild(xmlroot)

                'create head tag
                Dim xmlHead As XmlElement = xmlDoc.CreateElement("head")
                xmlroot.AppendChild(xmlHead)

                'create title tag
                Dim xmlTitle As XmlElement = xmlDoc.CreateElement("title")
                xmlTitle.AppendChild(xmlDoc.CreateTextNode("QP Items"))
                xmlHead.AppendChild(xmlTitle)

                'create body element and append it to the root element
                Dim xmlBody As XmlElement = xmlDoc.CreateElement("body")
                xmlBody.SetAttribute("style", "font-family:tahoma,geneva,sans-serif;font-size:16px;")
                xmlroot.AppendChild(xmlBody)

                If lateItems.Count > 0 Then
                    AddXmlSection("The following item(s) are past due:", lateItems, True, xmlDoc, xmlBody)
                End If
                If dueTodayItems.Count > 0 Then
                    AddXmlSection("The following item(s) are due today:", dueTodayItems, False, xmlDoc, xmlBody)
                End If
                If okItems.Count > 0 Then
                    AddXmlSection("The following item(s) are due in the next few days:", okItems, False, xmlDoc, xmlBody)
                End If
                If completedRecently.Count > 0 Then
                    AddXmlSection("The following item(s) were completed in the last few days", completedRecently, False, xmlDoc, xmlBody)
                End If

                'create the automated message title
                Dim xmlH6 As XmlElement = xmlDoc.CreateElement("h5")
                xmlBody.AppendChild(xmlH6)
                xmlH6.AppendChild(xmlDoc.CreateTextNode("This is an automated message from LineUp. Automagically created at " & Now.ToString & ", by " & Environment.MachineName))

                emailLog.Contents = PrettyXML(xmlDoc.InnerXml)

                'if no items found, return blank string
                If count > 0 Then
                    Return emailLog
                Else
                    Return Nothing
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Adds a title and table to the xml
    ''' </summary>
    ''' <param name="title">Header text</param>
    ''' <param name="jobList">list of jobs</param>
    ''' <param name="includeDaysLate">include the days late column</param>
    ''' <param name="xmlDoc">the reference to the xmldoc to add the table to</param>
    ''' <param name="xmlBody">the reference to the xmlbody to add the table to</param>
    Private Shared Sub AddXmlSection(ByVal title As String, ByVal jobList As List(Of JQRowInfo), ByVal includeDaysLate As Boolean, ByRef xmlDoc As XmlDocument, ByRef xmlBody As XmlElement)


        'create the h2 title
        Dim xmlH2 As XmlElement = xmlDoc.CreateElement("h2")
        xmlBody.AppendChild(xmlH2)
        xmlH2.AppendChild(xmlDoc.CreateTextNode(title))

        'create the table and append it
        Dim xmlTable As XmlElement = xmlDoc.CreateElement("table")
        'xmlTable.SetAttribute("style", "width=800px; vertical-align: bottom; text-align: left;")
        xmlBody.AppendChild(xmlTable)

        'create header row & items
        Dim xmlHeader As XmlElement = xmlDoc.CreateElement("tr")
        xmlTable.AppendChild(xmlHeader)
        xmlHeader.AppendChild(CreateXmlCell("Pic:", True, xmlDoc, 50))
        xmlHeader.AppendChild(CreateXmlCell("Item #:", True, xmlDoc))
        xmlHeader.AppendChild(CreateXmlCell("Title:", True, xmlDoc, 250))
        xmlHeader.AppendChild(CreateXmlCell("Due Date:", True, xmlDoc))
        If includeDaysLate Then xmlHeader.AppendChild(CreateXmlCell("Days Late:", True, xmlDoc))
        xmlHeader.AppendChild(CreateXmlCell("Status:", True, xmlDoc, 300))

        'create the list of items
        For Each job As JQRowInfo In jobList

            Dim picPath As String = LineUp.MyJQProductionIO.GetPicPath(job, My.Settings.dirWebProd)

            Dim xmlRow As XmlElement = xmlDoc.CreateElement("tr")
            xmlTable.AppendChild(xmlRow)

            'add image

            Dim imgCell As XmlElement = xmlDoc.CreateElement("td")
            xmlRow.AppendChild(imgCell)

            'link for the whole row?
            Dim imgLink As XmlElement = xmlDoc.CreateElement("a")
            imgLink.SetAttribute("href", picPath)
            imgCell.AppendChild(imgLink)

            Dim imgElement As XmlElement = xmlDoc.CreateElement("img")
            imgElement.SetAttribute("src", picPath)
            imgElement.SetAttribute("alt", picPath)
            imgElement.SetAttribute("width", "50")
            imgLink.AppendChild(imgElement)

            Dim statHist As String = job.StatusHistory(job.StatusHistory.Count - 1)
            statHist = statHist.Replace(cSplit, " - ")


            xmlRow.AppendChild(CreateXmlCell(job.ItemNumber, False, xmlDoc))
            xmlRow.AppendChild(CreateXmlCell(job.Title, False, xmlDoc, 250))
            xmlRow.AppendChild(CreateXmlCell(job.DueDate, False, xmlDoc))
            If includeDaysLate Then
                xmlRow.AppendChild(CreateXmlCell(Math.Round((Now - job.DueDate).TotalDays, 0), False, xmlDoc))
            End If



            xmlRow.AppendChild(CreateXmlCell(statHist, False, xmlDoc, 300))



        Next
    End Sub
    Private Shared Function CreateXmlCell(ByVal text As String, ByVal isHeader As Boolean, ByRef xmldoc As XmlDocument, Optional ByVal width As Integer = 100) As XmlElement
        Dim elementTxt As String = "td"
        If isHeader Then elementTxt = "th"
        Dim xmlCell As XmlElement = xmldoc.CreateElement(elementTxt)
        xmlCell.SetAttribute("style", "width: " & width & "px;")
        xmlCell.AppendChild(xmldoc.CreateTextNode(text))
        Return xmlCell
    End Function
    ''' <summary>
    ''' formats an xml document to have indentations and whitespace
    ''' </summary>
    ''' <param name="XMLString"></param>
    ''' <returns></returns>
    Private Shared Function PrettyXML(XMLString As String) As String
        Dim sw As New System.IO.StringWriter()
        Dim xw As New XmlTextWriter(sw)
        xw.Formatting = Formatting.Indented
        xw.Indentation = 4
        Dim doc As New XmlDocument
        doc.LoadXml(XMLString)
        doc.Save(xw)
        Return sw.ToString()
    End Function







#End Region




End Class