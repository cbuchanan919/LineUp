Imports System.IO
Imports Falcon.SiteCommon
Imports System.Xml

''' <summary>
''' Stores info from one row of the personalize dgv
''' </summary>
Public Class PersonalizeRowInfo



#Region "Properties"
    ''' <summary>
    ''' If not using sql, the uniquest id that I can find... "MxNumber - CustomID"
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property UniqueID As String
        Get
            'Dim sb As Text.StringBuilder
            'sb.Append(MxNumber & " - ")
            'sb.Append(DesignId & " - ")
            'sb.Append(DesignQuantity)
            'Return sb.ToString
            Return MxNumber & " - " & DesignId '& " - " & DesignQuantity
        End Get

    End Property
    ''' <summary>
    ''' SQL ID
    ''' </summary>
    ''' <returns></returns>
    Public Property ID As Integer = cNullInt

    Private _mxNumber As String = ""
    ''' <summary>
    ''' Stores the Order Number
    ''' </summary>
    ''' <returns></returns>
    Public Property MxNumber As String
        Get
            Return _mxNumber
        End Get
        Set(value As String)
            value = value.ToLower
            'makes sure that the mxNumber has the Mx in the front.
            value = value.Replace("mx", "")
            'If value.Contains("mx") Then
            '    value.Remove("mx")
            'End If
            _mxNumber = "Mx" & value
        End Set
    End Property

    ''' <summary>
    ''' Returns the Mx Order Number without the 'Mx'
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MxNumberNoMX As Integer
        Get
            If _mxNumber.Trim = "" Or _mxNumber.Trim.ToLower = "mx" Then
                Return cNullInt
            Else
                Return _mxNumber.ToLower.Replace("mx", "")
            End If

        End Get
    End Property

    ''' <summary>
    ''' Stores the Item Number
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemNumber As String = ""

    Private _designId As String = ""
    ''' <summary>
    ''' Stores the Design ID
    ''' </summary>
    ''' <returns></returns>
    Public Property DesignId As String
        Get
            Return _designId
        End Get
        Set(value As String)
            '10‑6‑30560
            If value.Contains("‑") Then
                value = value.Replace("‑", "-") 'these hyphens are slightly different.
            End If
            _designId = value
        End Set
    End Property

    ''' <summary>
    ''' Stores the Design Quantity
    ''' </summary>
    ''' <returns></returns>
    Public Property DesignQuantity As String = ""

    ''' <summary>
    ''' Stores UV Status
    ''' </summary>
    ''' <returns></returns>
    Public Property UvStatus As String = ""

    ''' <summary>
    ''' Stores Current Status
    ''' </summary>
    ''' <returns></returns>
    Public Property PrintStatus As String = ""


    ''' <summary>
    ''' The time the row was created / added
    ''' </summary>
    ''' <returns></returns>
    Public Property OrderCreated As Date = cNullDate

    ''' <summary>
    ''' Stores the Status histories
    ''' </summary>
    ''' <returns></returns>
    Public Property StatusHistory As New List(Of String)

    ''' <summary>
    ''' Stores Label History
    ''' </summary>
    ''' <returns></returns>
    Public Property LabelHistory As New List(Of String)

    ''' <summary>
    ''' Returns true if labels have been printed
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LabelsHaveBeenPrinted As Boolean
        Get
            Return LabelHistory.Count > 0
        End Get
    End Property
    ''' <summary>
    ''' Used when creating a list of labels (Temporary - Data not saved)
    ''' </summary>
    ''' <returns></returns>
    Public Property CreateLabels As Boolean = False

    ''' <summary>
    ''' stores the assumed production file path
    ''' </summary>
    ''' <returns></returns>
    Public Property BodyFilePath As String = ""

    Public Property CoverFilePath() As String = ""

    ''' <summary>
    ''' where the imposition file on the 1and1 server is found
    ''' </summary>
    ''' <returns></returns>
    Public Property FtpPath As String = ""

    '''' <summary>
    '''' stores the index in the personalize dgv that the info belongs to
    '''' </summary>
    '''' <returns></returns>
    'Public Property RowIndex As Integer = -1

    ''' <summary>
    ''' used for dan's imposition
    ''' </summary>
    ''' <returns></returns>
    Private Property CalDesign As New Object

    ''' <summary>
    ''' the folder path to the pdf directory
    ''' </summary>
    ''' <returns></returns>
    Private Property LocalPdfDir As String = ""

    ''' <summary>
    ''' folder where the indesign should be stored
    ''' </summary>
    ''' <returns></returns>
    Private Property LocalInddDir As String = ""

    Public Property Barcode As New JQBarcodeInfo

    ''' <summary>
    ''' Is current vs is Archive. Used to show on the dgv
    ''' </summary>
    ''' <returns></returns>
    Public Property IsCurrent As Boolean = True

    ''' <summary>
    ''' Stores the last ToString used (use after loading / saving the row)
    ''' </summary>
    ''' <returns></returns>
    Public Property LastToString As String = ""

    ''' <summary>
    ''' Returns a string with multiple lines of info that represent the class.
    ''' </summary>
    ''' <param name="includeDescription">Describe each line of info first</param>
    ''' <returns></returns>
    Public Function ToString(Optional ByVal includeDescription As Boolean = True) As String
        Dim sb As New Text.StringBuilder
        If includeDescription Then
            sb.AppendLine("ID: " & ID)
            sb.AppendLine("Mx: " & MxNumber)
            sb.AppendLine("Item#: " & ItemNumber)
            sb.AppendLine("DesignID: " & DesignId)
            sb.AppendLine("DesignQuan: " & DesignQuantity)
            sb.AppendLine("UvStatus: " & UvStatus)
            sb.AppendLine("PrintStatus: " & PrintStatus)
            sb.AppendLine("OrderCreated: " & OrderCreated)
            sb.AppendLine("Status History: " & String.Join(", ", StatusHistory))
            sb.AppendLine("Label History: " & String.Join(", ", LabelHistory))
            sb.AppendLine("Create Labels: " & CreateLabels.ToString)
            sb.AppendLine("Body File: " & BodyFilePath)
            sb.AppendLine("Cover File: " & CoverFilePath)
            sb.AppendLine("Ftp Path: " & FtpPath)
            sb.AppendLine("Is Current: " & IsCurrent.ToString)
        Else
            sb.AppendLine(ID)
            sb.AppendLine(MxNumber)
            sb.AppendLine(ItemNumber)
            sb.AppendLine(DesignId)
            sb.AppendLine(DesignQuantity)
            sb.AppendLine(UvStatus)
            sb.AppendLine(PrintStatus)
            sb.AppendLine(OrderCreated)
            sb.AppendLine(String.Join(", ", StatusHistory))
            sb.AppendLine(String.Join(", ", LabelHistory))
            sb.AppendLine(CreateLabels.ToString)
            sb.AppendLine(BodyFilePath)
            sb.AppendLine(CoverFilePath)
            sb.AppendLine(FtpPath)
            If IsCurrent Then
                sb.AppendLine("Current")
            Else
                sb.AppendLine("Archive")
            End If
            'sb.AppendLine("Is Current: " & IsCurrent.ToString)
        End If

        Return sb.ToString
        'Return MyBase.ToString()
    End Function

    Public Function Clone(Optional ByVal keepID As Boolean = True) As PersonalizeRowInfo
        Dim cloned As New PersonalizeRowInfo
        Try

            With cloned
                If keepID Then
                    .ID = ID
                Else
                    .ID = cNullInt
                End If
                .MxNumber = MxNumber
                .ItemNumber = ItemNumber
                .DesignId = DesignId
                .DesignQuantity = DesignQuantity
                .UvStatus = UvStatus
                .PrintStatus = PrintStatus
                .OrderCreated = OrderCreated
                For Each history As String In StatusHistory
                    .StatusHistory.Add(history)
                Next
                For Each history As String In LabelHistory
                    .LabelHistory.Add(history)
                Next
                .CreateLabels = CreateLabels
                .BodyFilePath = BodyFilePath
                .CoverFilePath = CoverFilePath
                .FtpPath = FtpPath
                .IsCurrent = IsCurrent


            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Clone PersonalizeRowInfo Error")
        End Try

        Return cloned
    End Function
#End Region



#Region "Init"

    Public Sub New(ByVal MxNumber As String,
                   ByVal ItemNumber As String,
                   ByVal DesignId As String,
                   ByVal DesignQuantity As String,
                   ByVal PrintStatus As String,
                   ByVal LabelHistory As List(Of String),
                   ByVal StatusHistory As List(Of String),
                   ByVal BodyFilePath As String,
                   ByVal FtpPath As String)
        Me.MxNumber = MxNumber
        Me.ItemNumber = ItemNumber
        Me.DesignId = DesignId
        Me.DesignQuantity = DesignQuantity
        Me.PrintStatus = PrintStatus
        Me.LabelHistory = LabelHistory
        Me.StatusHistory = StatusHistory
        Me.BodyFilePath = BodyFilePath
        Me.FtpPath = FtpPath

        Barcode = New JQBarcodeInfo
        Barcode.BarcodeValue = Me.DesignId.Split("-")(2) 'The last 5 digits of the custom id
        Barcode.BarcodeText = Me.DesignId
    End Sub

    Public Sub New()

    End Sub


#End Region



#Region "Public Methods"

    ''' <summary>
    ''' clears the settings for the different properties
    ''' </summary>
    Public Sub clearInfo()
        MxNumber = ""
        ItemNumber = ""
        DesignId = ""
        DesignQuantity = ""
        PrintStatus = ""
        LabelHistory = New List(Of String)
        StatusHistory = New List(Of String)
        BodyFilePath = ""
        FtpPath = ""
        calDesign = New Object
        localPdfDir = ""
        localInddDir = ""
        barcode = New JQBarcodeInfo
    End Sub

    ''' <summary>
    ''' Configures the file paths. Needs the item number, Design ID and Quantity fields populated. Returns true if success
    ''' </summary>
    ''' <returns></returns>
    Public Function ConfigureFilePaths(ByVal pcConfigFile As Falcon.PageComposer.PcConfig) As Boolean

        Dim success As Boolean = True
        Try
            Dim fp As String = ""
            Dim fpDesignQuan As String = ""

            Const pdfExt As String = ".pdf"
            Const xmlExt As String = ".xml"
            Dim quanStr As String = "-Q" & DesignQuantity & pdfExt

            Select Case ItemNumber
                'selects which item number the custom id is, and sets the file paths & properties accordingly.
                Case "8101"
                    fp = My.Settings.dir8101IDs & DesignId & pdfExt
                    fpDesignQuan = My.Settings.dir8101IDs & DesignId & quanStr
                    FtpPath = pcConfigFile.FTPServerImprintPath8101 & DesignId & xmlExt
                    CalDesign = New Personalization.GospelOfPeace.ScDesign
                    LocalPdfDir = pcConfigFile.ComposedFolderPath8101
                    LocalInddDir = My.Settings.dir8101Users

                Case "7427"
                    fp = My.Settings.dir7427IDs & DesignId & pdfExt
                    fpDesignQuan = My.Settings.dir7427IDs & DesignId & quanStr
                    FtpPath = pcConfigFile.FTPServerImprintPath7427 & DesignId & xmlExt
                    CalDesign = New Personalization.CustomPocketCalendar.ScDesign
                    LocalPdfDir = pcConfigFile.PocketCalendarComposedFolderPath7427
                    LocalInddDir = My.Settings.dir7427Users

                Case "6880"
                    fp = My.Settings.dir6880IDs & DesignId & pdfExt
                    fpDesignQuan = My.Settings.dir6880IDs & DesignId & quanStr
                    FtpPath = pcConfigFile.FTPServerImprintPath6880 & DesignId & xmlExt
                    CalDesign = New Personalization.JoyfulNewsMiniCalendar.ScDesign
                    LocalPdfDir = pcConfigFile.JncMiniComposedFolderPath6880
                    LocalInddDir = My.Settings.dir6880Users

                Case "6120"
                    fp = My.Settings.dir6120IDs & DesignId & pdfExt
                    fpDesignQuan = My.Settings.dir6120IDs & DesignId & quanStr
                    FtpPath = pcConfigFile.FTPServerImprintPath6120 & DesignId & xmlExt
                    CalDesign = New Personalization.CustomTractCard.ScDesign
                    LocalPdfDir = pcConfigFile.TractCardComposedFolderPath6120
                    LocalInddDir = My.Settings.dir6120Users

                Case "42194"
                    'marks design - john / romans
                    Dim fn As String = ItemNumber & "-" & DesignId & "-"
                    fpDesignQuan = Path.Combine(My.Settings.dir42194IDs, fn & "8upBody" & pdfExt)
                    CoverFilePath = Path.Combine(My.Settings.dir42194IDs, fn & "4upCover" & pdfExt)
                    CalDesign = Nothing
                    FtpPath = ""
                    LocalPdfDir = ""
                    LocalInddDir = ""


                Case Else
                    BodyFilePath = ""
                    FtpPath = ""
                    success = False
            End Select

            If File.Exists(fpDesignQuan) Then
                BodyFilePath = fpDesignQuan
            ElseIf File.Exists(fp) Then
                BodyFilePath = fp
            Else
                BodyFilePath = ""
                success = False
            End If

        Catch ex As Exception
            success = False
        End Try


        Return success
    End Function

    Private Enum Methods As Integer
        ftp
        local
        none
    End Enum

    Public Function ImposeCalendar(ByVal config As Falcon.PageComposer.PcConfig, ByRef errors As List(Of String), ByVal showDialogPrompt As Boolean) As Boolean
        Dim success As Boolean = False


        Dim pdfFP As String = "" 'pdf file path - specified by PageComposer
        Dim inddFP As String = "" 'indd file path - specified by PageComposer
        Dim pdfFN As String = "" 'pdf file name - name of file from pdfFP
        Dim inddFN As String = "" 'indd file name - name of file from inddFP
        Dim myTKLog As New Falcon.Toolkit.TkLog()
        myTKLog.Enable()





        Try
            Dim err As String = ""
            Dim xmlFP As String = Path.Combine(My.Settings.dirResources, "CustomIDs", DesignId & ".xml")
            Dim method As Methods = Methods.none
            'Dim locateFileSuccess As Boolean = False
            If DownloadFile(FtpPath, xmlFP, "", config, err) Then
                If File.Exists(xmlFP) Then
                    Dim lastXmlWriteTime As Date = File.GetLastWriteTime(xmlFP) 'stores the last time the file was written to.
                    Dim continueOK As Boolean = False

                    If showDialogPrompt Then

                        Process.Start(xmlFP)
                        If MsgBox("XML Preview - Press OK to continue.", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                            continueOK = True
                            If DateTime.Compare(lastXmlWriteTime, File.GetLastWriteTime(xmlFP)) <> 0 Then
                                If MsgBox("Do you want to upload your changes to the xml files?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    'if the file was modified / saved by the user, it will upload the changed version to the 1&1 site. (Overwrites original)
                                    If UploadFile(FtpPath, xmlFP, config, err) Then
                                        LineUp.UpdateStatus("File Uploaded! :)", False)
                                    End If

                                End If
                            End If

                        End If
                    Else
                        continueOK = True
                    End If


                    'End



                    If continueOK Then

                        'gets the username (email) from the the downloaded file.
                        Dim usrID As String = GetProperty(xmlFP, "UserID")
                        LocalInddDir = Path.Combine(LocalInddDir, usrID)

                        If ItemNumber <> "6120" Then
                            Dim year As String = GetProperty(xmlFP, "CalendarYear")
                            If year <> config.CurrentYear Then
                                If MsgBox("The calendar year is incorrect!" & vbCrLf _
                                    & "Calendar's year is: " & year & vbCrLf _
                                    & "Current year is: " & config.CurrentYear & vbCrLf & vbCrLf _
                                    & "Are you sure you want to impose anyway?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    errors.Add("Incorrect Calendar Year")
                                    Return False
                                End If
                            End If
                        End If


                        If Not Directory.Exists(LocalInddDir) Then
                            ' creates the user directory if it doesn't exist
                            'MsgBox("*****************************************************************" & vbCrLf &
                            '       localInddDir &
                            '       "*****************************************************************")
                            Directory.CreateDirectory(LocalInddDir)
                        End If


                        If Falcon.PageComposer.PcCompose.Pageup(config, CInt(ItemNumber), DesignId, MxNumberNoMX, CInt(DesignQuantity), xmlFP, True, False, False, LocalInddDir, CalDesign, inddFP, pdfFP, myTKLog) Then
                            success = True
                            'If File.GetLastWriteTime(xmlFP) <> lastXmlWriteTime Then
                            '    If MsgBox("Do you want to upload your changes to the xml files?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            '        If UploadFile(FtpPath, xmlFP, config, err) Then
                            '            MsgBox("Uploaded! :)")
                            '        End If
                            '    End If

                            'End If

                        Else
                            'Dim txt As New Text.StringBuilder()
                            errors.Add("Imposition Failed. Error:")
                            For Each myEntry As Falcon.Toolkit.TkLog.TkLogEntry In myTKLog.Entries
                                errors.Add(myEntry.Text)
                            Next
                        End If

                        'success = true 'file skipped.
                    Else
                        errors.Add("User canceled imposition")
                    End If

                Else
                    errors.Add("Local XML file does not exist")
                End If
            Else
                errors.Add("Download Failed")
            End If


            If File.Exists(pdfFP) Then
                'moves the pdf file to the composed folder.
                Dim moveSuccess As Boolean = False
                Do Until moveSuccess
                    Try
                        pdfFN = Path.GetFileName(pdfFP)
                        Dim newFP As String = Path.Combine(LocalPdfDir, pdfFN)
                        If File.Exists(newFP) Then
                            File.Delete(newFP) 'removes existing pdf file.
                        End If
                        Dim newPdfFP As String = Path.Combine(LocalPdfDir, pdfFN)

                        File.Move(pdfFP, newPdfFP)
                        BodyFilePath = newPdfFP 'assigns the new file path & name to the FilePath property

                        'MsgBox("Old: " & pdfFP & vbCrLf & "New: " & )
                        moveSuccess = True
                    Catch ex As Exception
                        If MessageBox.Show("Error moving " & pdfFN & ":" & vbCrLf & ex.Message & vbCrLf & "Click OK to retry moving the file", "File access error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) = DialogResult.Cancel Then
                            moveSuccess = True
                            success = False
                            errors.Add(pdfFN & " in use. Unable to move.")
                        End If

                    End Try

                Loop

            End If


        Catch ex As Exception
            errors.Add(ex.Message)
        End Try

        Return success
    End Function

    ''' <summary>
    ''' uploads the local file to the ftp location. (Also renames the original ftp file to include backup)
    ''' </summary>
    ''' <param name="strFTPPath">FTP upload file path</param>
    ''' <param name="localfilePath">local file path</param>
    ''' <param name="config">Dan's config file</param>
    ''' <param name="errors">returns any errors that occured</param>
    ''' <returns></returns>
    Private Function UploadFile(ByVal strFTPPath As String,
                                  ByVal localfilePath As String,
                                  ByVal config As Falcon.PageComposer.PcConfig,
                                  ByRef errors As String) As Boolean
        Dim success As Boolean = False
        Try
            If File.Exists(localfilePath) Then
                Dim localFile As New FileInfo(localfilePath)
                'Dim ftp As New Falcon.PageComposer.PcFTP.PcFtpClient(config.FTPServerURL, config.FTPLoginName, config.FTPLoginPassword, True)
                Dim ftp As New FluentFTP.FtpClient(config.FTPServerURL, 21, config.FTPLoginName, config.FTPLoginPassword)
                'ftp = New FtpClient(CbConfig.FTPServerURL, 21, CbConfig.FTPLoginName, CbConfig.FTPLoginPassword)
                ftp.SslProtocols = Security.Authentication.SslProtocols.Tls
                ftp.EncryptionMode = FluentFTP.FtpEncryptionMode.Explicit




                'MsgBox(strFTPPath)

                If ftp.FileExists(strFTPPath) Then
                    Dim newName As String = strFTPPath

                    Dim insertPoint As Integer = -1
                    For i As Integer = newName.Length - 1 To 0 Step -1 'gets the file extension location
                        If newName(i) = "." Then
                            insertPoint = i
                            Exit For
                        End If
                    Next


                    newName = newName.Insert(insertPoint, "-Backup-" & Now.ToString("yyyy-MM-dd HH.mm.ss"))
                    'MsgBox("*************************************" & vbCrLf &
                    '       "*************************************" & vbCrLf &
                    '       newName)
                    ftp.Rename(strFTPPath, newName)
                    'ftp.FtpRename(strFTPPath, newName, True)
                End If
                Dim fs As New FileStream(localFile.FullName, FileMode.Open)
                ftp.Upload(fs, strFTPPath, FluentFTP.FtpRemoteExists.Overwrite)
                'ftp.Upload(localFile, strFTPPath, True)
                success = True
            End If

        Catch ex As Exception
            errors = " - Upload Failed - " & ex.Message
        End Try
        Return success
    End Function


    ''' <summary>
    ''' downloads the ftp file to the local file path
    ''' </summary>
    ''' <param name="strFTPPath">FTP file to download</param>
    ''' <param name="localfilePath">where to save the file</param>
    ''' <param name="FileContents">returns the file contents as string</param>
    ''' <param name="config">Dan's config file</param>
    ''' <param name="errors">returns any errors that occured</param>
    ''' <returns></returns>
    Private Function DownloadFile(ByVal strFTPPath As String,
                                  ByVal localfilePath As String,
                                  ByRef FileContents As String,
                                  ByVal config As Falcon.PageComposer.PcConfig,
                                  ByRef errors As String) As Boolean

        Dim Success As Boolean = False

        Try
            If File.Exists(localfilePath) Then
                File.Delete(localfilePath)
            End If
            FileContents = ""

            Dim ftp As New Falcon.PageComposer.PcFTP.PcFtpClient(config.FTPServerURL, config.FTPLoginName, config.FTPLoginPassword, True)

            If ftp.Download(strFTPPath, localfilePath, True, config.UseSSL) Then
                Success = True
            Else
                errors = "Unable to download " & vbCrLf & strFTPPath
            End If




            'Using sr As StreamReader = New StreamReader(localfilePath)
            '    Dim Line As String = ""
            '    Line = sr.ReadLine
            '    Do While (Line <> "")
            '        FileContents &= Line & vbCrLf
            '        Line = sr.ReadLine
            '    Loop
            '    sr.Close()
            'End Using

            'Dim sr As New StreamReader(LocalFilePath)


        Catch ex As Exception
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            errors = " - Download Failed - " & vbCrLf & localfilePath & vbCrLf & ex.Message & vbCrLf
        End Try

        Return Success
    End Function



    Private Function GetProperty(ByVal IdFilePath As String, ByVal whatProperty As String) As String
        Dim ReturnProperty As String = ""
        Try

            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(IdFilePath)

            Dim xmlNodes As XmlNodeList = xmlDoc.ChildNodes

            For Each PropertyNode As XmlNode In xmlNodes
                If PropertyNode.Name = "Design" Then

                    For Each settingNode As XmlNode In PropertyNode.ChildNodes
                        If settingNode.Name = whatProperty Then
                            ReturnProperty = settingNode.InnerText
                        End If
                    Next
                End If
                'MsgBox(PropertyNode.Name & vbCrLf & PropertyNode.InnerText)

            Next

        Catch ex As Exception
            'ReturnProperty = ""
        End Try
        Return ReturnProperty
    End Function



    ''' <summary>
    ''' shows an email dialog box, and emails with selected message and attachemnt.
    ''' </summary>
    ''' <param name="emailTo"></param>
    ''' <param name="BillerName"></param>
    ''' <returns></returns>
    Public Function PrepSendEmail(ByVal emailTo As String, ByVal BillerName As String) As Boolean
        Dim success As Boolean = False
        Try
            'creates an email dialog
            Dim eD As New EmailDialog
            'populates email info
            eD.txtEmailTo.Text = emailTo
            eD.txtEmailFrom.Text = My.Settings.eSendAsUser
            eD.txtEmailPassword.Text = My.Settings.ePassword
            Try
                Dim BN() As String = BillerName.Split("-")
                eD.txtEmailBody.Text = "Hi " & BN(1) & ","
            Catch ex As Exception
                LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            End Try
            eD.txtEmailBody.Text &= vbLf & vbLf & vbLf & "Thanks!" & vbLf & "BibleTruthPublishers.com"
            eD.cboAttachFiles.Items.Clear()
            eD.cboAttachFiles.Items.Add("None")
            If File.Exists(bodyFilePath) Then
                eD.cboAttachFiles.Items.Add(bodyFilePath)
                eD.attachFiles.InitialDirectory = Path.GetDirectoryName(bodyFilePath)
            End If
            eD.cboAttachFiles.Text = eD.cboAttachFiles.Items(0)

            Select Case itemNumber
                Case "8101"
                    eD.txtEmailSubject.Text = "Gospel of Peace ID " & DesignId
                Case "7427"
                    eD.txtEmailSubject.Text = "Custom Wallet Calendar ID " & DesignId
                Case "6880"
                    eD.txtEmailSubject.Text = "Joyful News Mini-Calendar ID " & DesignId
                Case "6120"
                    eD.txtEmailSubject.Text = "Personalized Tract Card ID " & DesignId
                Case Else

            End Select
            Dim result As Integer = eD.ShowDialog()
            If result = MsgBoxResult.Ok Then

                Try
                    Dim sendEmail As String = True
                    Dim attachedFilePath As String = ""
                    Dim emailResult As String = ""

                    Dim cc As String = eD.txtEmailCC.Text
                    If cc = "" Or cc = " " Then
                        cc = eD.txtEmailFrom.Text
                    Else
                        cc &= ";" & eD.txtEmailFrom.Text
                    End If

                    Dim Body As String = "<html><head><title>" & eD.txtEmailSubject.Text &
                        "</title></head><body><p>" & eD.txtEmailBody.Text.Replace(vbLf, "<br>") & "</p></body></html>"

                    If File.Exists(eD.cboAttachFiles.Text) Then
                        attachedFilePath = eD.cboAttachFiles.Text
                    ElseIf eD.cboAttachFiles.Text = "None" Then
                    Else
                        If MsgBox("I wasn't able to find " & eD.cboAttachFiles.Text & "." & vbCrLf &
                                                       "Do you want to send the email anyway?",
                                  MsgBoxStyle.YesNo, "Attachment not Found") = MsgBoxResult.No Then
                            sendEmail = False
                        End If

                    End If
                    If sendEmail Then


                        Utilities.GenUtil.SendEmail(eD.txtEmailFrom.Text,
                                                                      eD.txtEmailTo.Text,
                                                                      cc,
                                                                      eD.txtEmailPassword.Text,
                                                                      eD.cboAttachFiles.Text,
                                                                      eD.txtEmailSubject.Text,
                                                                      Body,
                                                                      My.Settings.eServer,
                                                                      My.Settings.ePort,
                                                                      True,
                                                                      False)

                    End If


                    'SMTP.Send(Mail)
                    If emailResult.ToLower.Contains("sent") Then
                        LineUp.UpdateStatus("Email to " & eD.txtEmailTo.Text & " sent ok!", False)
                    Else
                        MsgBox("Email Failed:" & vbCrLf & emailResult, MsgBoxStyle.Critical)
                    End If



                Catch ex As Exception
                    LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                    MsgBox("Email Failed" & vbCrLf & vbCrLf & ex.Message)
                End Try



            End If
        Catch ex As Exception
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try

        Return success
    End Function


    ''' <summary>
    ''' Adds the current status to the pq history column
    ''' </summary> 
    ''' <remarks></remarks>
    Public Sub AddStatusToHistory()

        Dim addToHistory As Boolean = True
        If StatusHistory.Count > 0 Then
            Dim latest As String = StatusHistory(StatusHistory.Count - 1)
            Dim pts() As String = latest.Split("-")
            If pts.Count = 2 Then
                If PrintStatus = pts(1) Then
                    addToHistory = False
                End If
            End If
        Else
            addToHistory = True
        End If
        If addToHistory Then
            StatusHistory.Add(Date.Now.ToString & " - " & PrintStatus)
        End If



    End Sub

#End Region



End Class
