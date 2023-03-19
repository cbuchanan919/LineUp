Imports System.IO
Imports System.Xml

''' <summary>
''' Contains the info for one job entry
''' </summary>
Public Class JQRowInfo


#Region "Wrapper Properties"

    ''' <summary>
    ''' Used when creating the dgv columns - makes it able to show blanks
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemNumberWrapper As Object
        Get
            If ItemNumber = 0 Or ItemNumber = cNullInt Then
                Return DBNull.Value
            Else
                Return ItemNumber
            End If
        End Get
        Set(value As Object)
            If IsDBNull(value) OrElse IsNothing(value) Then
                ItemNumber = cNullInt
            ElseIf value.ToString <> "" Then
                If IsNumeric(value.ToString) Then
                    Dim int As Integer = Integer.Parse(value.ToString)
                    ItemNumber = int
                Else
                    Throw New Exception("Invalid entry")
                End If
            Else
                ItemNumber = cNullInt
            End If
        End Set
    End Property


    ''' <summary>
    ''' used when creating the dgv columns - makes it able to show blanks
    ''' </summary>
    ''' <returns></returns>
    Public Property OrderQuantityWrapper() As Object
        Get
            If OrderQuantity = 0 Or OrderQuantity = cNullInt Then
                Return DBNull.Value
            Else
                Return OrderQuantity
            End If
        End Get
        Set(ByVal value As Object)
            If IsDBNull(value) Then
                OrderQuantity = cNullInt
            ElseIf value.ToString <> "" Then
                If IsNumeric(value.ToString) Then
                    Dim int As Integer = Integer.Parse(value.ToString)
                    If JobTicketWasPrinted Then
                        Throw New Exception("Unable to update quantity: Job ticket was already created.")
                    Else

                        OrderQuantity = int
                    End If

                Else
                    Throw New Exception("Invalid entry")
                End If
            Else
                OrderQuantity = cNullInt
            End If

        End Set
    End Property

#End Region


#Region "Properties"

    ''' <summary>
    ''' returns true if it's not a null number and it's greater than 0
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ItemNumberIsValid As Boolean
        Get
            If ItemNumber = cNullInt Then Return False
            If ItemNumber <= 0 Then Return False

            Return True
        End Get
    End Property
    Public Property ItemNumber As Integer = cNullInt
    Public Property Title As String = ""
    Public Property OrderQuantity As Integer = cNullInt
    Public Property Description As String = ""
    Public Property Status As String = ""
    Public Property Priority As String = ""
    Public Property OrderPlaced As Date = cNullDate
    Public Property DueDate As Date = cNullDate
    Public Property SubmittedBy As String = ""
    Public Property Comments As String = ""
    Public Property CompletedOn As Date = cNullDate
    Public Property LastUpdated As Date = cNullDate
    Public Property IsActive As Boolean = True
    Public Property StatusHistory As New List(Of String)
    Public Property JobID As Integer = cNullInt
    Public Property JobTicketHistory As New List(Of String)
    Public ReadOnly Property JobTicketWasPrinted As Boolean
        Get
            Return JobTicketHistory.Count > 0
        End Get
    End Property
    Public Property uvProdInfo As UvProductInfo = Nothing
    Public Property ProjectDirs As List(Of JQProjectDirInfo) = Nothing
    ''' <summary>
    ''' stores the production files
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductionFiles As New List(Of JQProductionFileInfo)
    ''' <summary>
    ''' stores the picture path file info.
    ''' </summary>
    ''' <returns></returns>
    Public Property PicFileInfo As FileInfo = Nothing

    ''' <summary>
    ''' this is a new date, to be checked against to see if it's null
    ''' </summary>
    ''' <returns></returns>
    Private Property blankDate As New Date



    Public ReadOnly Property StatusHistoryStr
        Get
            Dim tempHistory As New List(Of String)
            For Each history As String In StatusHistory
                tempHistory.Add(history.Replace(";", ""))
            Next
            Return String.Join(";", tempHistory)
        End Get
    End Property

    Public Enum EmailStatus
        Deleted
        Completed
        Ordered
    End Enum


    ''' <summary>
    ''' returns the combined version of the item number and title
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property getSummaryStr As String
        Get
            Dim temp As New List(Of String)
            Dim itemNum = ItemNumber.ToString
            If itemNum <> "" And itemNum <> cNullInt Then temp.Add(itemNum)
            If Title <> "" Then temp.Add(Title)
            Return String.Join(" - ", temp)
        End Get
    End Property
    Private Property _jobTicket As JQProductionFileInfo = Nothing
    Public Property jobTicket As JQProductionFileInfo
        Get
            If IsNothing(_jobTicket) Then
                Dim dir As New JQProductionDirInfo(ProductCategory.Job_Ticket, New DirectoryInfo(System.IO.Path.Combine(My.Settings.dirResources, "Job Tickets")), BodyVsCover.Not_Set, 1, PrinterCategory.Job_Ticket)
                Dim fp As String = Path.Combine(dir.ProductionDirectory.FullName, JobID & " - Job Ticket.pdf")
                _jobTicket = New JQProductionFileInfo(dir, New Utilities.CMS_FileName(New FileInfo(fp)))
            End If
            Return _jobTicket
        End Get
        Set(value As JQProductionFileInfo)
            _jobTicket = value
        End Set
    End Property





#End Region


#Region "Methods"

    ''' <summary>
    ''' returns a copy of the jobqrow
    ''' </summary>
    ''' <returns></returns>
    Public Function Clone() As JQRowInfo
        Dim newQ As New JQRowInfo
        newQ.ItemNumber = ItemNumber
        newQ.Title = Title
        newQ.OrderQuantity = OrderQuantity
        newQ.Description = Description
        newQ.Status = Status
        newQ.Priority = Priority
        newQ.OrderPlaced = OrderPlaced
        newQ.DueDate = DueDate
        newQ.SubmittedBy = SubmittedBy
        newQ.Comments = Comments
        newQ.CompletedOn = CompletedOn
        newQ.LastUpdated = LastUpdated
        newQ.IsActive = IsActive

        For Each history As String In JobTicketHistory
            newQ.JobTicketHistory.Add(history)
        Next
        For Each history As String In StatusHistory
            newQ.StatusHistory.Add(history)
        Next
        For Each file As JQProductionFileInfo In ProductionFiles
            newQ.ProductionFiles.Add(file.Clone)

        Next
        If Not IsNothing(ProjectDirs) Then
            newQ.ProjectDirs = New List(Of JQProjectDirInfo)
            For Each project As JQProjectDirInfo In ProjectDirs
                newQ.ProjectDirs.Add(New JQProjectDirInfo(project.ProjectDirectory, project.ProjectType))
            Next
        End If


        newQ.JobID = JobID
        Return newQ
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("ID: " & JobID)
        sb.AppendLine("Item #: " & ItemNumber)
        sb.AppendLine("Title: " & Title)
        sb.AppendLine("Order Quantity: " & OrderQuantity)
        sb.AppendLine("Description: " & Description)
        sb.AppendLine("Status: " & Status)
        sb.AppendLine("Priority: " & Priority)
        sb.AppendLine("Order Placed: " & OrderPlaced)
        sb.AppendLine("Due Date: " & DueDate)
        sb.AppendLine("Submitted By: " & SubmittedBy)
        sb.AppendLine("Comments: " & Comments)
        sb.AppendLine("Completed On: " & CompletedOn)
        sb.AppendLine("Last Updated: " & LastUpdated)
        sb.AppendLine("Is Active: " & IsActive)
        sb.AppendLine("Ticket Created: " & String.Join(", ", JobTicketHistory))
        sb.AppendLine("Status History: ")
        For Each history As String In StatusHistory
            sb.AppendLine("     " & history.Replace(cSplit, " - "))
        Next
        Return sb.ToString
    End Function


    ''' <summary>
    ''' 
    ''' </summary>  
    ''' <param name="eStatus">"This item was finished today", or "This item was deleted today" or "This item was created today" </param>
    ''' <remarks></remarks>
    Public Function SendEmail(ByVal eStatus As EmailStatus) As Boolean
        Dim success As Boolean = False
        Dim email As EmailLogInfo = JQRowIO.CreateEmailReport(Me, eStatus)
        If Not IsNothing(email) Then

            Dim ePass As String = My.Settings.ePassword
            'Dim result As Utilities.EmailSendResults = Utilities.GenUtil.SendEmail(My.Settings.eSendAsUser, "cbuchanan@bibletruthpublishers.com", "", ePass, "", email.Subject, email.Contents, My.Settings.eServer, My.Settings.ePort, True, False)
            Dim result As Utilities.EmailSendResults = Utilities.GenUtil.SendEmail(My.Settings.eSendAsUser, My.Settings.eMailTo, "", ePass, "", email.Subject, email.Contents, My.Settings.eServer, My.Settings.ePort, True, False)
            If result.SentSuccess = EmailSendResults.EmailStatuses.Sent Then
                email.TimeSent = Now
                'MyJQRowIO.emailLogs.AddLogToSql(email)
                'MyJQRowIO.emailLogs.getSentEmails()
                success = True
                LineUp.UpdateStatus(result.ToString, False)
                If ePass <> My.Settings.ePassword Then
                    My.Settings.ePassword = ePass
                    Settings.UpdateAllSettingFromMySettings()
                    Settings.updateSqlFromAllSettings()
                End If

            Else
                MsgBox(result, MsgBoxStyle.Exclamation, "Couldn't send email")
            End If

        End If




        'Dim subject As String = ""
        'Dim Body As String = ""
        'Dim itemStatus As String = ""
        'Select Case eStatus
        '    Case EmailStatus.Deleted
        '        subject = getSummaryStr & " - Deleted!"
        '        itemStatus = "This item was deleted: "
        '    Case EmailStatus.Completed
        '        subject = getSummaryStr & " - Finished!"
        '        itemStatus = "This item was completed: "
        '    Case EmailStatus.Ordered
        '        subject = $"Product Ordered - {getSummaryStr}"
        '        itemStatus = "This item was Ordered: "
        '    Case Else
        '        subject = "???"
        'End Select

        'Body = ("<html><body style=""font-family:tahoma,geneva,sans-serif;font-size:16px;""><p>JobQ Automated Message,</p><p>" _
        '        & itemStatus & ":&nbsp;</p><p><span style=""font-size:18px;""><span style=""font-family:tahoma,geneva,sans-serif;"">" & ToString() &
        '        "</span></span></p><p>Thanks!</p></body></html>")



        ''Sends an email to whomever for zDone items

        ''Dim ePass As String = My.Settings.ePassword
        'Try

        '    Dim result As Utilities.EmailSendResults = Utilities.GenUtil.SendEmail(My.Settings.eSendAsUser,
        '                         My.Settings.eMailTo,
        '                         "",
        '                         ePass,
        '                         "",
        '                         subject,
        '                         Body,
        '                         My.Settings.eServer,
        '                         My.Settings.ePort,
        '                         True,
        '                         False)
        '    If result.SentSuccess = Utilities.EmailSendResults.EmailStatuses.Sent Then
        '        success = True
        '        LineUp.UpdateStatus(result.ToString, False)
        '        If ePass <> My.Settings.ePassword Then
        '            My.Settings.ePassword = ePass
        '            Settings.UpdateAllSettingFromMySettings()
        '            Settings.updateSqlFromAllSettings()
        '        End If

        '    Else
        '        MsgBox(result, MsgBoxStyle.Exclamation, "Couldn't send email")
        '    End If

        'Catch ex As Exception
        '    success = False
        '    MsgBox("Email Failed" & vbCrLf & vbCrLf & ex.Message)
        'End Try

        Return success

    End Function


    ''' <summary>
    ''' formats an xml document to have indentations and whitespace
    ''' </summary>
    ''' <param name="XMLString"></param>
    ''' <returns></returns>
    Private Function PrettyXML(XMLString As String) As String
        Dim sw As New System.IO.StringWriter()
        Dim xw As New XmlTextWriter(sw)
        xw.Formatting = Formatting.Indented
        xw.Indentation = 4
        Dim doc As New XmlDocument
        doc.LoadXml(XMLString)
        doc.Save(xw)
        Return sw.ToString()
    End Function



    ''' <summary>
    ''' if one of the properties is set to be nothing, resets it to null values
    ''' Also, it does property verification... ie, if status contains zDone...
    ''' </summary>
    Public Sub SelfCheck()

        '---------- null protection ---------------
        If IsNothing(ItemNumber) Then ItemNumber = cNullInt
        If IsNothing(Title) Then Title = ""
        If IsNothing(OrderQuantity) Then OrderQuantity = cNullInt
        If IsNothing(Description) Then Description = ""
        If IsNothing(Status) Then Status = ""
        If IsNothing(Priority) Then Priority = ""
        If IsNothing(OrderPlaced) OrElse OrderPlaced = blankDate Then OrderPlaced = cNullDate
        If IsNothing(DueDate) OrElse DueDate = blankDate Then DueDate = cNullDate
        If IsNothing(SubmittedBy) Then SubmittedBy = ""
        If IsNothing(Comments) Then Comments = ""
        If IsNothing(CompletedOn) OrElse CompletedOn = blankDate Then CompletedOn = cNullDate
        If IsNothing(IsActive) Then IsActive = True
        If IsNothing(StatusHistory) Then StatusHistory = New List(Of String)
        If IsNothing(JobID) Then JobID = cNullInt
        If IsNothing(JobTicketHistory) Then JobTicketHistory = New List(Of String)

        '----------- Auto Update -----------------
        LastUpdated = Now
        If Status.ToLower.Contains("zdone") And CompletedOn = cNullDate Or CompletedOn = blankDate Then
            ' job finished
            IsActive = False
            Dim t As Task = Task.Factory.StartNew(Sub() SendEmail(EmailStatus.Completed))
            CompletedOn = Date.Today
        End If

        '----------- History Update --------------
        Try
            Dim addHistory As Boolean = False
            'Dim lastHistory As String = StatusHistory.Last.Split(cSplit)(0)
            If StatusHistory.Count = 0 Then
                addHistory = True
            ElseIf Not StatusHistory.Last.Split(cSplit)(1) = Status Then
                'the last status history doesn't equal the status
                addHistory = True
            End If
            If addHistory Then
                StatusHistory.Add(Date.Now.ToString & cSplit & Status.Replace(cSplit, ""))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' returns the time since order was placed till completed time or till today if not complete
    ''' </summary>
    ''' <returns></returns>
    Public Function GetJobDurationInDays() As Double
        Dim placed As Date = OrderPlaced
        If IsNothing(placed) Or placed = blankDate Or placed = cNullDate Then
            placed = Now
        End If
        Dim completedDate As Date = CompletedOn
        If IsNothing(CompletedOn) Or CompletedOn = blankDate Or CompletedOn = cNullDate Then
            'blank completed date - unfinished
            completedDate = Now
        End If
        Dim timeDiff As TimeSpan = completedDate - placed

        Return Double.Parse(timeDiff.Days)
    End Function



    ''' <summary>
    ''' goes through each production file, and disposes the production group box.
    ''' </summary>
    Public Sub DisposeAllProductionGB()

        jobTicket.DisposeProductionGB()

        For Each prodInfo As JQProductionFileInfo In ProductionFiles
            prodInfo.DisposeProductionGB()
        Next

    End Sub

    ''' <summary>
    ''' creates / returns the groupbox
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetProductionGBsAsync(ByVal MyPrinterMgmt As PrinterMgmt, Optional ByVal forceCreate As Boolean = False) As Task(Of List(Of GroupBox))
        For Each myFile As JQProductionFileInfo In ProductionFiles
            Dim MakeNewBox As Boolean = False
            If IsNothing(myFile.ProductionGB) Or forceCreate Then
                MakeNewBox = True
            End If
            If MakeNewBox Then
                Await myFile.CreateProductionGB(ItemNumber, OrderQuantity, MyPrinterMgmt)

            End If

        Next

        Dim boxes As New List(Of GroupBox)

        'If jobTicket.ProductionFile.OriginalFile.Exists Then
        '    jobTicket.ProductionFile.OriginalFile.Delete()
        'End If
        'Dim TicketIO As New PdfJobTicketIO()
        'TicketIO.CreateJobTicketLetterSize(jobTicket.ProductionFile.OriginalFile.FullName, OrderQuantity, True, "", Me)
        'boxes.Add(Await (jobTicket.CreateProductionGB(ItemNumber, 1, MyPrinterMgmt)))

        ProductionFiles.Sort(Function(y, x) x.ProductBodyOrCover.CompareTo(y.ProductBodyOrCover))
        For Each myFile As JQProductionFileInfo In ProductionFiles
            boxes.Add(myFile.ProductionGB)
        Next


        Return boxes
    End Function




    ''' <summary>
    '''  Creates the product description in the JobQprod
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ConfigureDescription() As String

        Dim pgCt As Integer = 0
        If Not IsNothing(uvProdInfo) Then
            pgCt = uvProdInfo.PageCt
        End If

        Dim categories As New List(Of ProductCategory)

        For Each myFile As JQProductionFileInfo In ProductionFiles
            If Not categories.Contains(myFile.ProductType) Then
                categories.Add(myFile.ProductType)

            End If
        Next
        If categories.Count = 0 OrElse categories(0) = ProductCategory.Not_Set Then
            Description = ""


        Else
            ' only go for the first result? Not sure how else to do it.
            Dim CategoryStr As String = categories(0).ToString().Replace("_", " ")
            Select Case categories(0)
                Case ProductCategory.Book, ProductCategory.Book_12x9, ProductCategory.Full_Bleed_Book
                    'set page count
                    If pgCt >= 1 And pgCt <= 75 Then
                        Description = "28lb " & CategoryStr

                    ElseIf pgCt >= 76 And pgCt <= 199 Then
                        Description = "24lb " & CategoryStr

                    ElseIf pgCt >= 200 And pgCt <= 499 Then
                        Description = "20lb " & CategoryStr
                    ElseIf pgCt >= 500 Then
                        Description = "16lb " & CategoryStr
                    Else
                        Description = CategoryStr
                    End If
                Case ProductCategory.Tract_Card
                    Description = CategoryStr
                    Comments = "http://www.zooprinting.com/"
                Case Else
                    Description = CategoryStr

            End Select
        End If


        If Description = cUnknown Or Description = "" Then
            Description = uvProdInfo.convertTypeUV
        End If

        If uvProdInfo.SupID = cShareWordSupID Then
            ' it found a share word match.
            Description = cShareWord
        End If

        Return Description
    End Function

    Public Function CreateJobTicketLabelSize(ByVal filePath As String,
                                    ByVal totalQuantity As Integer, ByVal add10PercentExtra As Boolean,
                                    ByVal specialInstructions As String) As Boolean
        Dim success As Boolean = False
        Try

            success = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "CreateJobTicketLabelSize")
        End Try
        Return success
    End Function



    Public Function xUpReasoning(ByVal totalQuantity As Integer, ByVal xUp As Integer, ByVal add10PercentExtra As Boolean, Optional ByRef layerQuan As Integer = -1) As String
        Try
            Dim extra10 As String = ""
            If add10PercentExtra Then 'adds extra 10% for production needs
                Dim tempQuan As Integer = totalQuantity
                totalQuantity = Math.Floor(totalQuantity * 1.1)
                extra10 = "An extra 10% were added to the final count for production purposes. "
                If tempQuan + 50 < totalQuantity Then
                    totalQuantity = tempQuan + 50 'max extra of 50
                    extra10 = "An extra 50 were added to the final count for production purposes. "
                End If
            End If


            layerQuan = totalQuantity / xUp

            Dim txtPlus1 As String = ""
            If xUp <> 1 AndAlso layerQuan Mod 2 <> 0 Then
                'gives a 'rounded' number to be printed.
                ' layerQuan = Math.Ceiling(layerQuan) 'removes numbers after the decimal
                layerQuan += 1
                txtPlus1 = " Since it's impossible to print a partial sheet, 1 was added to meet quantity."
            ElseIf layerQuan Mod 2 <> 0 Then
                layerQuan += 1

            End If

            Dim reasoning As String = "(" & extra10 & "There are " & xUp & " per sheet. So, " & totalQuantity & " divided by " & xUp & " = " & totalQuantity / xUp & "." & txtPlus1 & ")"
            Return reasoning
        Catch ex As Exception

        End Try

        Return ""
    End Function


#End Region


End Class
