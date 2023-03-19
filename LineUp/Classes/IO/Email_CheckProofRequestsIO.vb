Imports System.IO
Imports System.Xml
Imports FluentFTP

Public Class Email_CheckProofRequestsIO


#Region "Properties"

    Private Property CbConfig As Falcon.PageComposer.PcConfig
    Private Property SqlConnStr As String = ""
    Public Property EmailLogs As EmailLogIO
    Private Property ftp As FtpClient

#End Region


#Region "Init"

    Public Sub New(ByVal CbConfig As Falcon.PageComposer.PcConfig, ByVal SqlConnStr As String)
        Me.CbConfig = CbConfig
        Me.SqlConnStr = SqlConnStr
        EmailLogs = New EmailLogIO(SqlConnStr)
        ftp = New FtpClient(CbConfig.FTPServerURL, 21, CbConfig.FTPLoginName, CbConfig.FTPLoginPassword)
        ftp.SslProtocols = Security.Authentication.SslProtocols.Tls
        ftp.EncryptionMode = FtpEncryptionMode.Explicit

    End Sub

#End Region


#Region "Methods"


    Private Function GetProofFiles() As List(Of FtpListItem)
        Dim FoundFiles As New List(Of FtpListItem)
        Try

            'Dim ftp As New Falcon.PageComposer.PcFTP.PcFtpClient(CbConfig.FTPServerURL, CbConfig.FTPLoginName, CbConfig.FTPLoginPassword, True)

            Dim productFP As String = "/Data/BibleTruthPublishers.%/Output/Personalize/?"

            Dim sites As List(Of String) = {"com", "net", "info"}.ToList
            Dim productFolderNames As List(Of String) = {"CustomPocketCalendar/Proof Requests/",
                                                         "CustomTractCard/Proof Requests/",
                                                         "GospelOfPeace/Proof Requests/",
                                                         "JoyfulNewsMiniCalendar/Proof Requests/",
                                                         "GospelForManyTongues/Print File Requests/"}.ToList

            Dim dirs As New List(Of String)
            For Each site As String In sites
                Dim fp As String = productFP.Replace("%", site)
                For Each folder In productFolderNames
                    dirs.Add(fp.Replace("?", folder))
                Next
            Next

            For Each myDir As String In dirs

                Dim files As List(Of FtpListItem) = ftp.GetListing(myDir).ToList
                For Each myFile As FtpListItem In files
                    FoundFiles.Add(myFile)
                Next
            Next

        Catch ex As Exception

        End Try


        Return FoundFiles
    End Function

    ''' <summary>
    ''' Returns a new Proof email log. Criteria for sending: There are proof request items, and an email hasn't been sent in 6? hours
    ''' </summary>
    ''' <param name="forceCreateLog">Create a log regardless (even if it's not friday / due to be created)</param>
    ''' <returns></returns>
    Public Function CreateProofEmailLog(Optional ByVal forceCreateLog As Boolean = False) As EmailLogInfo

        Try
            Dim createEmail As Boolean = True
            Dim proofFiles As New List(Of FtpListItem)
            Dim emailLog As New EmailLogInfo(True)
            emailLog.EmailType = EmailLogInfo.TypeOfEmail.ProofRequestErrorLog


            'check to see if email has already been sent
            EmailLogs.getSentEmails()
            Dim altEmailSent As Boolean = False
            For Each log In EmailLogs.SentEmails.Values
                If log.EmailType = EmailLogInfo.TypeOfEmail.ProofRequestErrorLog Then 'only care about proof email logs
                    If log.TimeSent > Now.AddHours(-7) Then 'it will only send if the most recent email is older than ~6 hours
                        altEmailSent = True
                    End If
                End If
            Next
            'another computer / user sent the email already. don't send again
            If altEmailSent Then createEmail = False

            If forceCreateLog Then createEmail = True

            If createEmail Then
                'check to see if there are files to complain about
                proofFiles = GetProofFiles()
                Dim hasOldFiles As Boolean = False
                For Each fi As FtpListItem In proofFiles
                    If fi.Modified <= Now.AddHours(-6) Then
                        hasOldFiles = True
                    End If
                Next
                If hasOldFiles = False Then
                    'no files are old enough
                    createEmail = False
                End If
            End If

            If forceCreateLog Then createEmail = True

            If createEmail Then

                emailLog.Subject = "Unresolved Proof Requests"

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
                xmlTitle.AppendChild(xmlDoc.CreateTextNode("Proof Requests"))
                xmlHead.AppendChild(xmlTitle)

                'create body element and append it to the root element
                Dim xmlBody As XmlElement = xmlDoc.CreateElement("body")
                xmlBody.SetAttribute("style", "font-family:tahoma,geneva,sans-serif;font-size:16px;")
                xmlroot.AppendChild(xmlBody)

                AddXmlSection("Unresolved Proof Requests", "The following item(s) haven't been processed by pc-impose yet.", proofFiles, False, xmlDoc, xmlBody)


                'create the automated message title
                Dim xmlH6 As XmlElement = xmlDoc.CreateElement("h5")
                xmlBody.AppendChild(xmlH6)
                xmlH6.AppendChild(xmlDoc.CreateTextNode("This is an automated message from LineUp. Automagically created at " & Now.ToString & ", by " & Environment.MachineName))

                emailLog.Contents = PrettyXML(xmlDoc.InnerXml)
                Return emailLog
            Else
                'if email not set to send, return blank string
                Return Nothing
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
    ''' <param name="proofs">list of jobs</param>
    ''' <param name="includeDaysLate">include the days late column</param>
    ''' <param name="xmlDoc">the reference to the xmldoc to add the table to</param>
    ''' <param name="xmlBody">the reference to the xmlbody to add the table to</param>
    Private Sub AddXmlSection(ByVal title As String, ByVal subTitle As String, ByVal proofs As List(Of FtpListItem), ByVal includeDaysLate As Boolean, ByRef xmlDoc As XmlDocument, ByRef xmlBody As XmlElement)


        'create the h2 title
        Dim xmlH2 As XmlElement = xmlDoc.CreateElement("h2")
        xmlBody.AppendChild(xmlH2)
        xmlH2.AppendChild(xmlDoc.CreateTextNode(title))

        'create the h5 subtitle
        Dim xmlH4 As XmlElement = xmlDoc.CreateElement("h4")
        xmlBody.AppendChild(xmlH4)
        xmlH4.AppendChild(xmlDoc.CreateTextNode(subTitle))

        'create the table and append it
        Dim xmlTable As XmlElement = xmlDoc.CreateElement("table")
        'xmlTable.SetAttribute("style", "width=800px; vertical-align: bottom; text-align: left;")
        xmlBody.AppendChild(xmlTable)

        'create header row & items
        Dim xmlHeader As XmlElement = xmlDoc.CreateElement("tr")
        xmlTable.AppendChild(xmlHeader)
        xmlHeader.AppendChild(CreateXmlCell("File Name:", True, xmlDoc, 200))
        xmlHeader.AppendChild(CreateXmlCell("Modified Time:", True, xmlDoc, 200))
        xmlHeader.AppendChild(CreateXmlCell("File Path:", True, xmlDoc, 500))


        'create the list of items
        For Each proof As FtpListItem In proofs



            Dim xmlRow As XmlElement = xmlDoc.CreateElement("tr")
            xmlTable.AppendChild(xmlRow)



            xmlRow.AppendChild(CreateXmlCell(proof.Name, False, xmlDoc, 200))
            xmlRow.AppendChild(CreateXmlCell(proof.Modified.ToString, False, xmlDoc, 200))
            xmlRow.AppendChild(CreateXmlCell(proof.FullName, False, xmlDoc, 500))


        Next
    End Sub

    Private Function CreateXmlCell(ByVal text As String, ByVal isHeader As Boolean, ByRef xmldoc As XmlDocument, Optional ByVal width As Integer = 100) As XmlElement
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
    Private Function PrettyXML(XMLString As String) As String
        Dim sw As New StringWriter()
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
