Imports System.IO
Imports System.Xml

Public Class GenUtil

    ''' <summary>
    ''' returns the display name if possible, otherwise, just the username
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetUserName() As String
        Dim user As String = Environment.UserName
        Try
            user = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName
        Catch ex As Exception
        End Try
        Return user
    End Function


    ''' <summary>
    ''' Returns True if and when access is available
    ''' </summary>
    ''' <param name="filePath">File path to wait for.</param>
    ''' <param name="MinutesToWait">Maximum time to wait. Enter 0 to wait the default amount. Will roughly wait that amount of time...</param>
    ''' <param name="strAvailableAfterResult">returns the length of time it waited.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function WaitForFile(ByVal filePath As String, ByVal MinutesToWait As Double, Optional ByRef strAvailableAfterResult As String = "") As Boolean
        Dim Access As Boolean = False
        Dim currentStep As String = ""
        Try
            If MinutesToWait <= 0 Then
                MinutesToWait = 10
            End If
            Dim result As String = "Available after "
            Dim numTries As Integer = 0 'counter of steps throughout the function. 
            Dim MaxCount As Integer = 0
            MaxCount = MinutesToWait * 600 '

            Dim fileExists As Boolean = False
            'This block waits until the program can get full access to the file


            '------------waits for file to exist--------------
            currentStep = "Step 1"
            Do Until fileExists = True Or numTries > MaxCount 'max of 10 minutes
                Threading.Thread.Sleep(300) 'added in an attempt to stop errors... Will add extra wait time
                If IO.File.Exists(filePath) Then
                    fileExists = True
                Else
                    Threading.Thread.Sleep(100)
                    'fiFile.Refresh()
                End If
                numTries += 1
                currentStep = "Step 2 - " & numTries
            Loop
            '--------------------------------------------------

            '----------if file exists, gets write access-------
            'numTries = 0 'if uncommented, this would effectively double the wait time.
            If fileExists Then
                'numTries = 0
                currentStep = "Step 3"
                Dim fiFile As New IO.FileInfo(filePath)
                currentStep = "Step 4"

                fiFile.Refresh()
                currentStep = "Step 5"

                Do Until fiFile.Length > 1 Or numTries > MaxCount 'max of 10 minutes
                    Threading.Thread.Sleep(100)
                    If IO.File.Exists(filePath) Then 'this if was added to prevent throwing errors...
                        fiFile.Refresh()
                    End If

                    numTries += 1
                    currentStep = "Step 6 - " & numTries

                Loop
                Do Until Access = True Or numTries > MaxCount 'max of 10 minutes
                    numTries += 1
                    currentStep = "Step 7 - " & numTries
                    Try
                        'Threading.Thread.Sleep(100)
                        Using myReader As New IO.FileStream(filePath, System.IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.None, 100)
                            myReader.ReadByte()
                            myReader.Close()
                        End Using
                        Access = True
                    Catch ex As Exception
                        If ex.Message.Contains("Access to the path") Then
                            MsgBox(ex.Message & vbCrLf & "(Please check read only permissions)", MsgBoxStyle.Critical, "Waiting for File")
                        Else
                            Threading.Thread.Sleep(100)
                        End If

                    End Try
                Loop
            End If

            If Access = False Then
                result = "NOT available after "
            End If
            Select Case numTries
                Case 1
                    result &= "1 try."
                Case Else
                    result &= numTries & " tries."
            End Select

            strAvailableAfterResult = result
            'myMainWindow.Invoke(m_NotifyMainWindow, inPath, ControlToUpdate)
            'myMainWindow.Invoke(m_NotifyMainWindow, result, ControlToUpdate)
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & "Current Step: " & currentStep, MsgBoxStyle.Critical, "Waiting for File")
        End Try


        Return Access
    End Function




    ''' <summary>
    ''' Writes the status to the log file.
    ''' </summary>
    ''' <param name="strStatus">The status to be written to the log file.</param>
    ''' <param name="LogPath">The log file path.</param>
    ''' <remarks></remarks>
    Public Shared Sub Log(ByVal strStatus As String, ByRef LogPath As String)
        Dim Success As Boolean = False
        Dim i As Integer = 0

        'waitForFile(LogPath)

        Do Until Success = True Or i >= 100
            Try
                IO.File.AppendAllText(LogPath, vbNewLine & strStatus & "," & Date.Now.ToShortTimeString)
                Success = True

            Catch ex As Exception
                Threading.Thread.Sleep(500)
            End Try
            i += 1
        Loop


    End Sub


    ''' <summary>
    ''' Sends an email in html format
    ''' </summary>
    ''' <param name="EmailFrom">The email account that you're sending from</param>
    ''' <param name="EmailTo">Where the email is being sent. (Separate multiple emails by a semicolon)</param>
    ''' <param name="EmailCC">Email addresses to carbon copy. (Separate multiple emails by a semicolon)</param>
    ''' <param name="Password">Your email's password</param>
    ''' <param name="FileAttachPath">File paths that you want to attach. (Separate multiple paths by a semicolon)</param>
    ''' <param name="EmailSubject">The email's subject</param>
    ''' <param name="EmailBody">The email's body. vbLf gets converted to a html line break</param>
    ''' <param name="Server">Default is secure.emailsrvr.com</param>
    ''' <param name="intPortNum">Default is 587</param>
    ''' <param name="booUseSSL">Default is True</param>
    ''' <param name="purposefullyFailForTesting">If true, fails before sending and errors out.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SendEmail(ByVal EmailFrom As String,
                                     ByVal EmailTo As String,
                                     ByVal EmailCC As String,
                                     ByRef Password As String,
                                     ByVal FileAttachPath As String,
                                     ByVal EmailSubject As String,
                                     ByVal EmailBody As String,
                                     ByVal Server As String,
                                     ByVal intPortNum As Integer,
                                     ByVal booUseSSL As Boolean,
                                     ByVal purposefullyFailForTesting As Boolean) As EmailSendResults

        Dim EmailResult As New EmailSendResults(Environment.UserName, EmailSendResults.EmailStatuses.NotSent, DateTime.Now, "")

        Dim Body As String = EmailBody.Replace(vbLf, "<br>")
        Dim Mail As New System.Net.Mail.MailMessage
        Dim SMTP As New System.Net.Mail.SmtpClient(Server)

        Mail.From = New System.Net.Mail.MailAddress(EmailFrom)
        SMTP.Credentials = New System.Net.NetworkCredential(EmailFrom, Password)

        Dim SendSuccess As Boolean = False



        'prepares the body of the email. Is currently in html format.

        Try
            If EmailTo.Contains(";") Then
                For Each email In EmailTo.Split(";")
                    Mail.To.Add(email)
                Next
            Else
                Mail.To.Add(EmailTo)
            End If

            If EmailCC <> "" Then
                For Each cc In EmailCC.Split(";")
                    If cc <> "" Then
                        Mail.CC.Add(cc)
                    End If
                Next
            End If

            Mail.Subject = EmailSubject
            Mail.IsBodyHtml = True

            For Each invoice In FileAttachPath.Split(";")
                If System.IO.File.Exists(invoice) Then
                    Mail.Attachments.Add(New System.Net.Mail.Attachment(invoice))
                End If
            Next


            If Body.ToLower.Contains("</html>") Then
                'it possibly already has the html formatting... hopefully!
            Else
                Body = ("<html><body style=""font-family:times new roman,times,serif;font-size:16px;""><p>" & Body & "</body></html>")
            End If

            Mail.Body = Body 'Message Here

            SMTP.EnableSsl = booUseSSL
            SMTP.Port = intPortNum



            Dim ErrorMessage As String = ""
            Dim tryCt As Integer = 0
            Dim UpdatePassword As Boolean = False
            Do Until SendSuccess = True Or tryCt >= 3
                Try
                    tryCt += 1
                    If purposefullyFailForTesting Then
                        MsgBox("No Emails Are Sending....")
                        Dim a As Integer = 1
                        Dim b As String = "Failed Testing!"
                        a = b
                    End If


                    SMTP.Send(Mail)
                    SendSuccess = True
                Catch ex As Exception
                    ErrorMessage = ex.Message & vbCrLf
                    If ErrorMessage.Contains("Sender address rejected: Access denied") Then
                        'ErrorLog.RichTextBox1.Text &= ex.Message & " " & System.Reflection.MethodInfo.GetCurrentMethod.ToString & vbCrLf
                        Dim UP As New frmUpdatePassword(EmailFrom, Password, ErrorMessage)
                        If UP.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                            Password = UP.txtPassword.Text
                            UpdatePassword = True

                            SMTP.Credentials = New System.Net.NetworkCredential(EmailFrom, Password)
                        End If
                    ElseIf ErrorMessage.Contains("A call to SSPI failed") Then
                        'try again.
                    Else
                        MsgBox(ErrorMessage, MsgBoxStyle.Exclamation)
                        '
                        tryCt = 5
                    End If


                End Try

            Loop



            If SendSuccess Then
                EmailResult.SentSuccess = EmailSendResults.EmailStatuses.Sent
            Else
                EmailResult.SentSuccess = EmailSendResults.EmailStatuses.Failed
                EmailResult.ErrorMessage = ErrorMessage
            End If


        Catch ex As Exception
            EmailResult.SentSuccess = EmailSendResults.EmailStatuses.Failed
            EmailResult.ErrorMessage = ex.Message & " " & System.Reflection.MethodInfo.GetCurrentMethod.ToString & vbCrLf
        End Try




        Return EmailResult
    End Function




    ''' <summary>
    ''' updates the settings for all the users.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateSettings(ByVal xmlFilePath As String, ByVal SettingsPropertyValueList As List(Of System.Configuration.SettingsPropertyValue)) As Boolean
        Dim success As Boolean = True
        Try
            Dim FilePath As String = xmlFilePath
            If File.Exists(FilePath) Then
                File.Delete(FilePath)
            End If
            Dim xmlSettings As New XmlWriterSettings
            xmlSettings.Indent = True
            Using writer As XmlWriter = XmlWriter.Create(FilePath, xmlSettings)
                With writer
                    .WriteStartDocument()
                    .WriteStartElement("Settings")
                    For Each setting As System.Configuration.SettingsPropertyValue In SettingsPropertyValueList
                        .WriteElementString(setting.Name, setting.PropertyValue.ToString)
                    Next
                    .WriteEndElement()
                    .WriteEndDocument()
                    .Close()
                End With
            End Using

        Catch ex As Exception
            success = False
        End Try

        Return success
    End Function

    Public Shared Function ReadSettings(ByVal xmlFilePath As String, ByRef SettingsPropertyValueList As List(Of System.Configuration.SettingsPropertyValue)) As Boolean
        Dim Success As Boolean = True
        Try
            Dim FilePath As String = xmlFilePath
            If IO.File.Exists(FilePath) Then
                Dim TagName As String = ""
                Dim TitleStr As String = ""
                Dim ValueStr As String = ""

                Dim ConfigXmlDoc As New Xml.XmlDocument
                ConfigXmlDoc.Load(FilePath)

                Dim SettingsNodeList As Xml.XmlNodeList = ConfigXmlDoc.DocumentElement.ChildNodes

                For Each SettingNode As Xml.XmlNode In SettingsNodeList
                    TagName = SettingNode.Name
                    ValueStr = SettingNode.InnerText

                    For Each setting As System.Configuration.SettingsPropertyValue In SettingsPropertyValueList
                        If setting.Name = TagName Then
                            setting.PropertyValue = ValueStr
                        End If
                    Next

                Next

            Else
                Success = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Success = False
        End Try
        Return Success
    End Function

    ''' <summary>
    ''' Saves the current time sheet (CurrentWeekFile)
    ''' </summary>
    ''' <param name="dgv"></param>
    ''' <param name="theFilePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function WriteDGVandDStoXML(ByRef dgv As System.Windows.Forms.DataGridView, ByVal ds As DataSet, ByVal theFilePath As String, ByRef strError As String, Optional ByVal NewTableName As String = "") As Boolean
        Dim Success As Boolean = True
        Try

            'if the current cell is in the last row, (or if it created a new row) it trigger this if statement
            If dgv.IsCurrentCellDirty Or dgv.IsCurrentRowDirty Then
                dgv.CurrentRow.DataGridView.EndEdit()
                dgv.EndEdit()
                Dim cm As System.Windows.Forms.CurrencyManager
                cm = dgv.BindingContext(dgv.DataSource, dgv.DataMember)
                cm.EndCurrentEdit()
            End If
            Dim tempSettings As New XmlWriterSettings
            tempSettings.Indent = True
            tempSettings.OmitXmlDeclaration = False


            Dim Settings As New Xml.XmlWriterSettings
            Settings.Indent = True
            Settings.OmitXmlDeclaration = False

            Dim myStream As New FileStream(theFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)

            Dim SWriter As Xml.XmlWriter = XmlWriter.Create(myStream, tempSettings)
            'Dim SWriter As Xml.XmlWriter = XmlWriter.Create(theFilePath, tempSettings)

            Dim oldTableName As String = ds.Tables(0).TableName
            If NewTableName = "" Then
                'it keeps the table name
                NewTableName = oldTableName
            End If

            ds.Tables(0).TableName = NewTableName

            'TimeSheetTable.AcceptChanges()
            Console.WriteLine("Writing XML")

            ds.WriteXml(SWriter)
            SWriter.Close()
            Console.WriteLine("Closing XML")
            myStream.Flush()
            myStream.Close()
            ds.Tables(0).TableName = oldTableName

        Catch ex As Exception
            strError &= System.Reflection.MethodInfo.GetCurrentMethod.ToString & ex.Message
            'MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Your changes may be lost!")
            Success = False
        End Try
        Return Success
    End Function


    ''' <summary>
    ''' Returns True if the number is even
    ''' </summary>
    ''' <param name="Number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsEven(Number As Double) As Boolean
        If (Number And 1) = 0 Then
            Return True
        Else
            Return False
        End If
    End Function




    ''' <summary>
    ''' Gets the monday of the week, and finds which monday of the month it is. Should return 1-5. (If it returns 0 then it errored)
    ''' </summary>
    ''' <param name="aMonday"></param>
    ''' <param name="isLastMondayofMonth"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMondayOfMonth(ByVal aMonday As Date, ByRef isLastMondayofMonth As Boolean) As Integer
        Dim MonOfMo As Integer = 0
        Try
            If aMonday.DayOfWeek = DayOfWeek.Monday Then
                isLastMondayofMonth = True
                Dim FirstOfMonth As Date = Date.Parse(aMonday.Month & " 1 " & aMonday.Year)
                Dim LastofMonth As Date = FirstOfMonth.AddMonths(1)
                LastofMonth = LastofMonth.AddDays(-1)
                'MsgBox(FirstOfMonth.ToShortDateString & vbCrLf & LastofMonth.ToShortDateString)

                Do Until Date.Compare(FirstOfMonth, LastofMonth) = 1
                    'if it's before or the same day as aMonday
                    If Date.Compare(FirstOfMonth, aMonday) < 1 Then
                        If FirstOfMonth.DayOfWeek = DayOfWeek.Monday Then
                            MonOfMo += 1
                        End If
                    Else
                        If FirstOfMonth.DayOfWeek = DayOfWeek.Monday Then
                            isLastMondayofMonth = False
                        End If
                    End If

                    FirstOfMonth = FirstOfMonth.AddDays(1)
                Loop
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "GetMondayOfMonth")
        End Try
        Return MonOfMo
    End Function


    ''' <summary>
    ''' Returns the monday of the date given, or the monday of current week
    ''' </summary>
    ''' <param name="CustomDate">If blank, it defaults to todays date.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMonday(Optional ByVal CustomDate As Date = #1/1/1167#) As Date

        Dim myDate As Date
        If CustomDate = #1/1/1167# Then
            myDate = Date.Today
        Else
            myDate = CustomDate
        End If
        Dim Monday As Date
        Dim dayDiff As Integer = myDate.DayOfWeek - DayOfWeek.Monday
        Monday = myDate.AddDays(-dayDiff)

        'tries to fix the new year end bug. (if it's 1/1/year or older, then it uses this time sheet.)
        Dim NewYear As Date = CDate("1/1/" & myDate.Year)
        Dim NewYearDiff As Integer = NewYear.DayOfWeek - DayOfWeek.Saturday
        For i As Integer = 0 To NewYearDiff Step -1
            Dim aDate As Date = NewYear.AddDays(-i)
            If aDate = myDate Then
                Monday = NewYear
            End If
            'MsgBox("New Year = " & NewYear.ToShortDateString & vbCrLf & _
            '"My Date = " & myDate.ToShortDateString & vbCrLf & _
            '"A Date = " & aDate.ToShortDateString)
        Next

        If Monday.DayOfWeek = DayOfWeek.Sunday Then
            Monday = Monday.AddDays(1) 'makes the beginning of the week monday.
        End If

        Return Monday
    End Function


    ''' <summary>
    ''' Kills any running processes that contains ProgramName in its name. ie "Photoshop"
    ''' </summary>
    ''' <param name="ProgramName">Ie. "WinWord", "Photoshop", "Indesign", etc.</param>
    Public Shared Sub KillProgram(ByVal ProgramName As String)
        Dim killProgram() As Process = System.Diagnostics.Process.GetProcessesByName(ProgramName)

        Threading.Thread.Sleep(500)
        For Each myProcess As Process In killProgram
            Try
                myProcess.Kill()
            Catch ex As Exception
                'sometimes users don't have authority to kill program. especially if using a terminal server
            End Try

        Next
    End Sub


    ''' <summary>
    ''' Goes through running processes and determines if specified program is running
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function IsProgramRunning(ByVal ProgramName As String)
        Dim appProc() As Process = Process.GetProcessesByName(ProgramName)
        If appProc.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function ConvertForJavaScript(ByVal parInputStr As String) As String

        Dim OutputStr As String = parInputStr
        Dim TempBackslash As String = "ZXQZXQ"

        OutputStr = OutputStr.Replace("\", TempBackslash)
        'OutputStr = OutputStr.Replace("'", "\'")
        'OutputStr = OutputStr.Replace(TkGlobal.TkvQuote, "\" & TkGlobal.TkvQuote)
        OutputStr = OutputStr.Replace(TempBackslash, "\\")

        Return OutputStr

    End Function


    ''' <summary>
    ''' returns the string as encoded base64
    ''' </summary>
    ''' <param name="input">the string to encode</param>
    ''' <returns></returns>
    Public Shared Function EncodeBase64(input As String) As String
        Return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input))
    End Function

    ''' <summary>
    ''' decodes the base64 string
    ''' </summary>
    ''' <param name="input">the string to decode</param>
    ''' <returns></returns>
    Public Shared Function DecodeBase64(input As String) As String
        Return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input))
    End Function

    ''' <summary>
    ''' finds all the math primes between the two numbers.
    ''' </summary>
    ''' <param name="lower"></param>
    ''' <param name="upper"></param>
    ''' <returns></returns>
    Public Shared Function GetPrimesInRange(ByVal lower As Integer, ByVal upper As Integer) As List(Of Integer)
        Dim foundNumbers As New List(Of Integer)

        If lower < upper Then
            'lower is less than upper, and both are greater than 1
            Dim num As Integer = lower
            While num <= upper
                If num > 1 Then

                    Dim isPrime As Boolean = True
                    Dim i As Integer = 2

                    While i < num
                        If (num Mod i) = 0 Then
                            isPrime = False
                            i = num
                        End If
                        i += 1
                    End While


                    If isPrime Then foundNumbers.Add(num)
                End If
                num += 1
            End While
        End If

        Return foundNumbers
    End Function
End Class

Public Class EmailSendResults

#Region "Properties"
    Public Enum EmailStatuses
        Failed
        Sent
        NotSent

    End Enum

    Public Property Username As String = ""
    Public Property SentSuccess As EmailStatuses = EmailStatuses.NotSent
    Public Property SentTime As Date
    Public Property ErrorMessage As String = ""
    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.Append(Username & " - " & SentSuccess.ToString & " - " & SentTime.ToString("MM/dd/yy h:mm tt"))
        If SentSuccess = EmailStatuses.Failed Then
            sb.Append(" - " & ErrorMessage.Replace("-", "_"))
        End If
        Return sb.ToString
    End Function

#End Region

#Region "Init"
    Public Sub New(ByVal Username As String, ByVal SentSuccess As EmailStatuses, ByVal SentTime As Date, ByVal ErrorMessage As String)
        Me.Username = Username
        Me.SentSuccess = SentSuccess
        Me.SentTime = SentTime
        Me.ErrorMessage = ErrorMessage
    End Sub
    Public Sub New()

    End Sub
#End Region

End Class