Option Explicit On
Option Strict Off

'Imports Falcon.PageComposer
'Imports Falcon.SiteCommon
'Imports Falcon.Toolkit
'Imports System.IO
'Imports System.Text.RegularExpressions
'Imports System.Web
'Imports System.Xml
'Imports Word = Microsoft.Office.Interop.Word

Public Class PcCompose

#Region "Shared"

    '    Private Shared Sub CheckVerseFiles(ByVal parDesign As Personalization.GospelOfPeace.ScDesign)

    '        Dim FileArray As String() = Nothing
    '        Dim FileName As String = ""
    '        Dim FilePath As String = ""
    '        Dim FolderArray As String() = Nothing
    '        Dim FolderPath As String = ""
    '        Dim I As Integer = 0
    '        Dim J As Integer = 0
    '        Dim LanguageList As New List(Of String)
    '        Dim LanguageName As String = ""
    '        Dim OutputStr As String = ""
    '        Dim PageupFile As KeyValuePair(Of String, Boolean) = Nothing
    '        Dim PageupFileList As Dictionary(Of String, Boolean) = Nothing
    '        Dim ResourceFile As KeyValuePair(Of String, Boolean) = Nothing
    '        Dim ResourceFileList As Dictionary(Of String, Boolean) = Nothing

    '        If parDesign IsNot Nothing Then
    '            FolderPath = "C:\BTP\Production\Resources\Personalize\GospelOfPeace\" & parDesign.CalendarYear.ToString
    '            If Directory.Exists(FolderPath) Then
    '                FolderArray = Directory.GetDirectories(FolderPath, "*.*", SearchOption.TopDirectoryOnly)

    '                If FolderArray IsNot Nothing Then
    '                    If FolderArray.Length > 0 Then
    '                        For I = 0 To FolderArray.Length - 1
    '                            FileName = Path.GetFileNameWithoutExtension(FolderArray(I))
    '                            If Not LanguageList.Contains(FileName) Then LanguageList.Add(FileName)
    '                        Next I
    '                    End If
    '                End If

    '                If LanguageList.Count > 0 Then
    '                    For I = 0 To LanguageList.Count - 1
    '                        LanguageName = LanguageList(I)
    '                        PageupFileList = Nothing
    '                        ResourceFileList = Nothing

    '                        FolderPath = "C:\BTP\Production\Resources\Personalize\GospelOfPeace\" & parDesign.CalendarYear.ToString & "\" & LanguageName & "\Verses"
    '                        If Directory.Exists(FolderPath) Then
    '                            FileArray = Directory.GetFiles(FolderPath)
    '                            If FileArray IsNot Nothing Then
    '                                If FileArray.Length > 0 Then
    '                                    ResourceFileList = New Dictionary(Of String, Boolean)

    '                                    For J = 0 To FileArray.Length - 1
    '                                        FileName = Path.GetFileNameWithoutExtension(FileArray(J))
    '                                        If FileName <> "" Then
    '                                            If (FileName.Contains("_") Or FileName.Contains("-")) And Not ResourceFileList.ContainsKey(FileName) Then
    '                                                ResourceFileList.Add(FileName, False)
    '                                            End If
    '                                        End If
    '                                    Next J
    '                                End If
    '                            End If
    '                        End If

    '                        FolderPath = "C:\BTP\Production\Pageup\Personalize\GospelOfPeace\" & parDesign.CalendarYear.ToString & "\" & LanguageName & "\Verses"
    '                        If Directory.Exists(FolderPath) Then
    '                            FileArray = Directory.GetFiles(FolderPath)
    '                            If FileArray IsNot Nothing Then
    '                                If FileArray.Length > 0 Then
    '                                    PageupFileList = New Dictionary(Of String, Boolean)

    '                                    For J = 0 To FileArray.Length - 1
    '                                        FileName = Path.GetFileNameWithoutExtension(FileArray(J))
    '                                        If FileName <> "" Then
    '                                            If (FileName.Contains("_") Or FileName.Contains("-")) And Not PageupFileList.ContainsKey(FileName) Then
    '                                                PageupFileList.Add(FileName, False)
    '                                            End If
    '                                        End If
    '                                    Next J
    '                                End If
    '                            End If
    '                        End If

    '                        If ResourceFileList IsNot Nothing And PageupFileList IsNot Nothing Then
    '                            For Each ResourceFile In ResourceFileList
    '                                FileName = ResourceFile.Key
    '                                If PageupFileList.ContainsKey(FileName) Then PageupFileList(FileName) = True
    '                            Next ResourceFile

    '                            For Each PageupFile In PageupFileList
    '                                FileName = PageupFile.Key
    '                                If ResourceFileList.ContainsKey(FileName) Then ResourceFileList(FileName) = True
    '                            Next PageupFile

    '                            For Each PageupFile In PageupFileList
    '                                If Not PageupFile.Value Then OutputStr &= vbCrLf & LanguageName & ": resource verse file missing for " & PageupFile.Key
    '                            Next PageupFile

    '                            For Each ResourceFile In ResourceFileList
    '                                If Not ResourceFile.Value Then OutputStr &= vbCrLf & LanguageName & ": pageup verse file missing for " & ResourceFile.Key
    '                            Next ResourceFile
    '                        End If
    '                    Next I
    '                End If
    '            End If
    '        End If

    '        FilePath = "C:\BTP\Production\Resources\Verse Check.txt"
    '        If File.Exists(FilePath) Then File.Delete(FilePath)
    '        File.WriteAllText(FilePath, OutputStr)

    '    End Sub

    '    Private Shared Function ConvertForJavaScript(ByVal parInputStr As String) As String

    '        Dim OutputStr As String = parInputStr
    '        Dim TempBackslash As String = "ZXQZXQ"

    '        OutputStr = OutputStr.Replace("\", TempBackslash)
    '        'OutputStr = OutputStr.Replace("'", "\'")
    '        'OutputStr = OutputStr.Replace(TkGlobal.TkvQuote, "\" & TkGlobal.TkvQuote)
    '        OutputStr = OutputStr.Replace(TempBackslash, "\\")

    '        Return OutputStr

    '    End Function

    '    Private Shared Function GetDesign(ByVal parInputFileStr As String) As Personalization.GospelOfPeace.ScDesign

    '        Dim Design As Personalization.GospelOfPeace.ScDesign = Nothing
    '        Dim InputFileStr As String = Trim(parInputFileStr)

    '        If InputFileStr <> "" Then
    '            Design = Personalization.GospelOfPeace.ScGospelOfPeace.ReadDesignXML(InputFileStr)
    '        End If

    '        Return Design

    '    End Function

    '    Private Shared Function GetGPCDocumentFilePath(ByVal parUserFolderPath As String, _
    '                                                   ByVal parCustomID As String, _
    '                                                   ByVal parOrderNum As Integer, _
    '                                                   ByVal parQuantity As Integer) As String

    '        Dim DateStr As String = Date.Now.ToString("yyMMdd")
    '        Dim FileNum As Integer = 0
    '        Dim FilePath As String = ""

    '        If parUserFolderPath <> "" And parCustomID <> "" And parOrderNum > 0 And parQuantity > 0 Then
    '            If Directory.Exists(parUserFolderPath) Then
    '                Do
    '                    FileNum += 1
    '                    FilePath = parUserFolderPath & "\" & parCustomID & "-MX" & parOrderNum.ToString & "-D" & DateStr & "-Q" & parQuantity.ToString
    '                    If FileNum > 1 Then FilePath &= "-N" & FileNum.ToString
    '                    FilePath &= ".indd"
    '                Loop Until Not File.Exists(FilePath)
    '            End If
    '        End If

    '        Return FilePath

    '    End Function

    '    Private Shared Function GetImprintFileName(ByVal parImprintLine As String) As String

    '        Dim FileName As String = ""

    '        If IsImprintFromFile(parImprintLine) Then
    '            FileName = Trim(Right(parImprintLine, Len(parImprintLine) - Len(Personalization.GospelOfPeace.ScGospelOfPeace.ImprintFileTag)))
    '        End If

    '        Return FileName

    '    End Function

    '    Private Shared Function GetLogoFilePath(ByVal parDesign As Personalization.GospelOfPeace.ScDesign, _
    '                                            ByVal parProductID As Integer, _
    '                                            ByVal parStdResources As Personalization.GospelOfPeace.ScStdResources, _
    '                                            ByVal parStdRsrcFolderPath As String, _
    '                                            ByVal parUserRsrcFolderPath As String) As String

    '        Dim FileName As String = ""
    '        Dim FilePath As String = ""
    '        Dim ImprintLine As String = ""
    '        Dim IndexPos As Integer = 0
    '        Dim StdFileName As String = ""
    '        Dim StdName As String = ""

    '        If parDesign IsNot Nothing And parStdResources IsNot Nothing And parStdRsrcFolderPath <> "" And parUserRsrcFolderPath <> "" Then
    '            If Directory.Exists(parStdRsrcFolderPath) And Directory.Exists(parUserRsrcFolderPath) Then
    '                ImprintLine = parDesign.Imprints.Imprint(16).Line(0)
    '                If ImprintLine <> "" Then
    '                    If IsImprintFromFile(ImprintLine) Then
    '                        FileName = GetImprintFileName(ImprintLine)
    '                        If IsStdResource(FileName) Then
    '                            StdName = GetStdName(FileName)
    '                            If StdName <> Personalization.GospelOfPeace.ScGospelOfPeace.BlankImprNameToken And (parDesign.IncludeBtpLogo Or StdName <> "BTP Logo") Then
    '                                StdFileName = GetStdFileName(StdName, parStdResources)
    '                                If StdFileName <> "" Then
    '                                    IndexPos = TkString.Last(StdFileName, ".")
    '                                    If IndexPos > 0 Then StdFileName = Trim(Left(StdFileName, IndexPos - 1))

    '                                    FilePath = parStdRsrcFolderPath & "\" & StdFileName & ".pdf"
    '                                End If
    '                            End If

    '                        Else
    '                            FilePath = parUserRsrcFolderPath & "\" & FileName
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If

    '        Return FilePath

    '    End Function

    '    Private Shared Sub GetProductData(ByVal parDesign As Personalization.GospelOfPeace.ScDesign, _
    '                                      ByVal parDailyMsgSets As Personalization.GospelOfPeace.ScDailyMsgSets, _
    '                                      ByVal parDefaultProductID As Integer, _
    '                                      ByRef parProductID As Integer, _
    '                                      ByRef parBrandLabel As String)

    '        Dim BrandLabel As String = "BTP"
    '        Dim ProductID As Integer = parDefaultProductID

    '        If parDesign IsNot Nothing And parDailyMsgSets IsNot Nothing Then
    '            ProductID = parDesign.GetProductID(parDailyMsgSets)

    '            If ProductID = 7104 Or ProductID = 7481 Then BrandLabel = "VB"
    '        End If

    '        parBrandLabel = BrandLabel
    '        parProductID = ProductID

    '    End Sub

    '    Private Shared Function GetResourceFilePath(ByVal parImprintLine As String, _
    '                                                ByVal parProductID As Integer, _
    '                                                ByVal parStdResources As Personalization.GospelOfPeace.ScStdResources, _
    '                                                ByVal parStdRsrcFolderPath As String, _
    '                                                ByVal parUserRsrcFolderPath As String) As String

    '        Dim FileName As String = ""
    '        Dim FilePath As String = ""
    '        Dim IndexPos As Integer = 0
    '        Dim LoopNum As Integer = 0
    '        Dim StdFileName As String = ""
    '        Dim StdName As String = ""

    '        If parImprintLine <> "" And parStdResources IsNot Nothing And parStdRsrcFolderPath <> "" And parUserRsrcFolderPath <> "" Then
    '            If IsImprintFromFile(parImprintLine) And Directory.Exists(parStdRsrcFolderPath) And Directory.Exists(parUserRsrcFolderPath) Then
    '                FileName = GetImprintFileName(parImprintLine)
    '                If IsStdResource(FileName) Then
    '                    StdName = GetStdName(FileName)
    '                    If StdName <> Personalization.GospelOfPeace.ScGospelOfPeace.BlankImprNameToken Then
    '                        StdFileName = GetStdFileName(StdName, parStdResources)
    '                        If StdFileName <> "" Then
    '                            IndexPos = TkString.Last(StdFileName, ".")
    '                            If IndexPos > 0 Then StdFileName = Trim(Left(StdFileName, IndexPos - 1))

    '                            Do
    '                                LoopNum += 1
    '                                FilePath = parStdRsrcFolderPath & "\" & StdFileName
    '                                If LoopNum = 1 Then FilePath &= "-" & TkInteger.ToString(parProductID)
    '                                FilePath &= ".pdf"
    '                            Loop Until File.Exists(FilePath) Or LoopNum = 2
    '                        End If
    '                    End If

    '                Else
    '                    FilePath = parUserRsrcFolderPath & "\" & FileName
    '                End If
    '            End If
    '        End If

    '        Return FilePath

    '    End Function

    '    Private Shared Function GetStdFileName(ByVal parStdName As String, _
    '                                           ByVal parStdResources As Personalization.GospelOfPeace.ScStdResources) As String

    '        Dim I As Integer = 0
    '        Dim StdFileName As String = ""

    '        If parStdName <> "" And parStdResources IsNot Nothing Then
    '            With parStdResources
    '                If .NumStdResources > 0 Then
    '                    For I = 0 To .NumStdResources - 1
    '                        With .StdResource(I)
    '                            If .Name = parStdName Then
    '                                StdFileName = .FileName
    '                                Exit For
    '                            End If
    '                        End With
    '                    Next I
    '                End If
    '            End With
    '        End If

    '        Return StdFileName

    '    End Function

    '    Private Shared Function GetStdName(ByVal parFileName) As String

    '        Dim StdName As String = ""

    '        If IsStdResource(parFileName) Then StdName = Trim(Right(parFileName, Len(parFileName) - Len(Personalization.GospelOfPeace.ScGospelOfPeace.ImprintStdTag)))

    '        Return StdName

    '    End Function

    '    Private Shared Sub GetUploadedFiles(ByVal parConfig As PcConfig, _
    '                                        ByVal parDesign As Personalization.GospelOfPeace.ScDesign)

    '        Dim FileName As String = ""
    '        Dim FileNameList As List(Of String) = Nothing
    '        Dim I As Integer = 0
    '        Dim LocalUserFolderPath As String = ""
    '        Dim RemoteUserFolderPath As String = ""
    '        Dim TextStr As String = ""

    '        If parDesign IsNot Nothing Then
    '            FileNameList = New List(Of String)

    '            With parDesign
    '                If .Imprints IsNot Nothing Then
    '                    If .Imprints.NumImprints > 0 Then
    '                        For I = 0 To .Imprints.NumImprints - 1
    '                            With .Imprints.Imprint(I)
    '                                If .NumLines > 0 Then
    '                                    TextStr = .Line(0)
    '                                    If IsImprintFromFile(TextStr) Then
    '                                        FileName = GetImprintFileName(TextStr)
    '                                        If Not IsStdResource(FileName) And FileName <> Personalization.GospelOfPeace.ScGospelOfPeace.BlankImprNameToken And Not FileNameList.Contains(FileName) Then
    '                                            FileNameList.Add(FileName)
    '                                        End If
    '                                    End If
    '                                End If
    '                            End With
    '                        Next I
    '                    End If
    '                End If
    '            End With

    '            If FileNameList.Count > 0 Then
    '                With parDesign
    '                    LocalUserFolderPath = parConfig.ResourcesFolderPath8101 & "\Users\" & Personalization.GospelOfPeace.ScGospelOfPeace.UserFolderName(.UserID)
    '                    If Not Directory.Exists(LocalUserFolderPath) Then Directory.CreateDirectory(LocalUserFolderPath)

    '                    RemoteUserFolderPath = parConfig.FTPServerUserResourcesPath8101 & "/" & Personalization.GospelOfPeace.ScGospelOfPeace.UserFolderName(.UserID) & "/Graphics/"
    '                End With

    '                For I = 0 To FileNameList.Count - 1
    '                    FileName = FileNameList(I)

    '                    Try
    '                        Dim FtpClient As New FTP.PcFtpClient(parConfig.FTPServerURL, parConfig.FTPLoginName, parConfig.FTPLoginPassword, False)

    '                        Dim RemoteUserFolderList As FTP.PcFtpDirectory = FtpClient.ListDirectoryDetail("/" & RemoteUserFolderPath)
    '                        Dim RemoteUserFileList As FTP.PcFtpDirectory = RemoteUserFolderList.GetFiles()

    '                        For Each RemoteFile As FTP.PcFtpFileInfo In RemoteUserFileList
    '                            If RemoteFile.FileName = FileName Then
    '                                FtpClient.Download(RemoteFile, LocalUserFolderPath & "\" & FileName, True)
    '                            End If
    '                        Next RemoteFile

    '                    Catch
    '                    End Try
    '                Next I
    '            End If
    '        End If

    '    End Sub

    '    Private Shared Function GetValue(ByVal parInputFileStr As String, _
    '                                     ByVal parCopyhole As String) As String

    '        Dim EndTag As String = "story"
    '        Dim StartTag As String = "story copyhole=" & Chr(34) & parCopyhole & Chr(34)
    '        Dim ValueStr As String = ""

    '        Dim ValueMatch As Match = Regex.Match(parInputFileStr, "(?<=\<" & StartTag & "\>).*?(?=\<\/" & EndTag & "\>)")
    '        If ValueMatch.Success Then ValueStr = ValueMatch.Value

    '        Return ValueStr

    '    End Function

    '    Private Shared Sub GospelOfPeacePageup(ByVal parConfig As PcConfig, _
    '                                           ByVal parProductID As Integer, _
    '                                           ByVal parCustomID As String, _
    '                                           ByVal parOrderNum As Integer, _
    '                                           ByVal parQuantity As Integer, _
    '                                           ByVal parInputFileStr As String)

    '        Dim BrandLabel As String = ""
    '        Dim DataSeparator As String = "†"
    '        Dim Design As Personalization.GospelOfPeace.ScDesign = GetDesign(parInputFileStr)
    '        Dim ErrorMsg As String = ""
    '        Dim FileName As String = ""
    '        Dim FilePath As String = ""
    '        Dim FolderPath As String = ""
    '        Dim I As Integer = 0
    '        Dim J As Integer = 0
    '        Dim LineStr As String = ""
    '        Dim ModuleName As String = "Gospel of Peace Calendar Pageup"
    '        Dim MonthlyGridsFilePath As String = ""
    '        Dim NumDays As Integer = 0
    '        Dim NumImprints As Integer = 0
    '        Dim NumValues As Integer = 0
    '        Dim OutputFilePath As String = ""
    '        Dim PageupScript As String = parConfig.ScriptFilePath8101
    '        Dim PdfFilePath As String = ""
    '        Dim ProductID As Integer = 0
    '        Dim RecordSeparator As String = "‡"
    '        Dim ResourcesFlag As Boolean = True
    '        Dim ReturnData As String = ""
    '        Dim ReturnDataArray As String() = Nothing
    '        Dim ScriptArgList As List(Of String) = Nothing
    '        Dim ScriptArgs As String() = Nothing
    '        Dim StatusOK As Boolean = True
    '        Dim StdRsrcFolderPath As String = ""
    '        Dim SubtitleStr As String = ""
    '        Dim TemplateFilePath As String = ""
    '        Dim TextStr As String = ""
    '        Dim ThisDate As Date = Nothing
    '        Dim TitleStr As String = ""
    '        Dim UserPageupFolderPath As String = ""
    '        Dim UserRsrcFolderPath As String = ""
    '        Dim VersesFolderPath As String = ""

    '        ' Design from DesignID data

    '        If Design Is Nothing Then
    '            MsgBox("Cannot read design from DesignID file.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Pageup script

    '        If Not File.Exists(PageupScript) Then
    '            MsgBox("InDesign pageup script doesn't exist (" & PageupScript & ").", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Base InDesign template

    '        If Design IsNot Nothing Then
    '            TemplateFilePath = parConfig.PageupFolderPath8101 & "\" & Design.CalendarYear.ToString & "\" & Design.LanguageName & "\" & Design.CalendarYear.ToString & _
    '                " Base Template-" & Design.LanguageName & ".indd"
    '            If Not File.Exists(TemplateFilePath) Then
    '                MsgBox("InDesign template file doesn't exist (" & TemplateFilePath & ").", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '                ResourcesFlag = False
    '            End If
    '        End If

    '        If ResourcesFlag Then
    '            StdRsrcFolderPath = parConfig.PageupFolderPath8101 & "\" & Design.CalendarYear.ToString & "\" & Design.LanguageName & "\Designs"

    '            UserRsrcFolderPath = parConfig.ResourcesFolderPath8101 & "\Users\" & Personalization.GospelOfPeace.ScGospelOfPeace.UserFolderName(Design.UserID)
    '            If Not Directory.Exists(UserRsrcFolderPath) Then Directory.CreateDirectory(UserRsrcFolderPath)

    '            ' Check verse files

    '            ' CheckVerseFiles(Design)

    '            ' Retrieve necessary user uploaded files

    '            GetUploadedFiles(parConfig, Design)

    '            ' Get daily messages

    '            FolderPath = parConfig.ResourcesFolderPath8101 & "\" & Design.CalendarYear.ToString & "\" & Design.LanguageName
    '            Dim DailyMsgSets As Personalization.GospelOfPeace.ScDailyMsgSets = Design.GetDailyMessages(FolderPath)

    '            ' Get holiday sets

    '            FolderPath = parConfig.ResourcesFolderPath8101 & "\" & Design.CalendarYear.ToString
    '            Dim HolidaySets As Personalization.GospelOfPeace.ScHolidaySets = Design.GetHolidaySets(FolderPath)

    '            ' Get standard resources

    '            FolderPath = parConfig.ResourcesFolderPath8101 & "\" & Design.CalendarYear.ToString & "\" & Design.LanguageName
    '            Dim StdResources As Personalization.GospelOfPeace.ScStdResources = Design.GetStdResources(FolderPath)

    '            ' Get product data

    '            GetProductData(Design, DailyMsgSets, parProductID, ProductID, BrandLabel)

    '            Design.GetProductTitle(ProductID, TitleStr, SubtitleStr)

    '            ' Get output document file path

    '            UserPageupFolderPath = parConfig.PageupFolderPath8101 & "\Users\" & Personalization.GospelOfPeace.ScGospelOfPeace.UserFolderName(Design.UserID)
    '            If Not Directory.Exists(UserPageupFolderPath) Then Directory.CreateDirectory(UserPageupFolderPath)

    '            OutputFilePath = GetGPCDocumentFilePath(UserPageupFolderPath, parCustomID, parOrderNum, parQuantity)

    '            PdfFilePath = parConfig.ComposedFolderPath8101 & "\" & parCustomID & "-Q" & parQuantity.ToString & ".pdf"
    '            If File.Exists(PdfFilePath) Then File.Delete(PdfFilePath)

    '            ' Start InDesign execution

    '            Dim IdApp As Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)

    '            ' Initialize

    '            ScriptArgList = New List(Of String)

    '            ScriptArgList.Add("init")
    '            ScriptArgList.Add(TkInteger.ToString(Design.CalendarYear))
    '            ScriptArgList.Add(TkConvertText.ForJavaScript(TemplateFilePath))

    '            ScriptArgs = ScriptArgList.ToArray

    '            ReturnData = IdApp.DoScript(PageupScript, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '            ReturnDataArray = ReturnData.Split(vbTab)

    '            NumValues = ReturnDataArray.Length

    '            If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '            If ErrorMsg <> "" Then
    '                TkGlobal.ErrorMessage(ErrorMsg, ModuleName)
    '                StatusOK = False
    '            End If

    '            ' Daily text

    '            If StatusOK Then
    '                Dim DailyText As Dictionary(Of Date, String) = Design.GetDailyText(HolidaySets, DailyMsgSets, RecordSeparator, DataSeparator)

    '                If DailyText IsNot Nothing Then
    '                    ScriptArgList.Clear()

    '                    ScriptArgList.Add("daily-text")
    '                    ScriptArgList.Add(TkInteger.ToString(Design.CalendarYear))
    '                    ScriptArgList.Add(RecordSeparator)
    '                    ScriptArgList.Add(DataSeparator)

    '                    NumDays = 0
    '                    ScriptArgList.Add(TkInteger.ToString(NumDays))

    '                    ThisDate = New Date(Design.CalendarYear, 1, 1)
    '                    Do
    '                        TextStr = DailyText(ThisDate)
    '                        If TextStr <> "" Then
    '                            TextStr = TextStr.Replace(Personalization.GospelOfPeace.ScGospelOfPeace.HardReturnChar, vbLf)

    '                            ScriptArgList.Add(TkConvertText.ForJavaScript(ThisDate.ToString("M/d/yyyy") & RecordSeparator & TextStr))
    '                            NumDays += 1
    '                        End If

    '                        ThisDate = ThisDate.AddDays(1.0#)
    '                    Loop Until ThisDate.Year > Design.CalendarYear

    '                    ScriptArgList(4) = TkInteger.ToString(NumDays)

    '                    ScriptArgs = ScriptArgList.ToArray

    '                    ReturnData = IdApp.DoScript(PageupScript, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '                    ReturnDataArray = ReturnData.Split(vbTab)

    '                    NumValues = ReturnDataArray.Length

    '                    If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '                    If ErrorMsg <> "" Then
    '                        TkGlobal.ErrorMessage(ErrorMsg, ModuleName)
    '                        StatusOK = False
    '                    End If
    '                End If
    '            End If

    '            ' Verses

    '            If StatusOK Then
    '                VersesFolderPath = parConfig.PageupFolderPath8101 & "\" & Design.CalendarYear.ToString & "\" & Design.LanguageName & "\Verses"
    '                If Directory.Exists(VersesFolderPath) Then
    '                    ScriptArgList.Clear()

    '                    ScriptArgList.Add("verses")
    '                    ScriptArgList.Add(TkInteger.ToString(Design.CalendarYear))
    '                    ScriptArgList.Add(TkConvertText.ForJavaScript(VersesFolderPath))

    '                    If Design.CurrentVerseList IsNot Nothing Then
    '                        If Design.CurrentVerseList.Count > 0 Then
    '                            For I = 0 To Design.CurrentVerseList.Count - 1
    '                                FileName = Design.CurrentVerseList(I) & ".indd"
    '                                FilePath = VersesFolderPath & "\" & FileName

    '                                If Not File.Exists(FilePath) Then
    '                                    FileName = Design.CurrentVerseList(I) & ".pdf"
    '                                    FilePath = VersesFolderPath & "\" & FileName
    '                                    If Not File.Exists(FilePath) Then FileName = ""
    '                                End If

    '                                If FileName = "" Then
    '                                    TkGlobal.Info("The verse file doesn't exist for " & TkDate.MonthName(I + 1) & " (" & Design.CurrentVerseList(I) & ").", ModuleName)
    '                                End If

    '                                ScriptArgList.Add(TkConvertText.ForJavaScript(FileName))
    '                            Next I
    '                        End If
    '                    End If

    '                    ScriptArgs = ScriptArgList.ToArray

    '                    ReturnData = IdApp.DoScript(PageupScript, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '                    ReturnDataArray = ReturnData.Split(vbTab)

    '                    NumValues = ReturnDataArray.Length

    '                    If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '                    If ErrorMsg <> "" Then
    '                        TkGlobal.ErrorMessage(ErrorMsg, ModuleName)
    '                        StatusOK = False
    '                    End If
    '                End If
    '            End If

    '            ' Imprints

    '            If StatusOK Then
    '                ScriptArgList.Clear()

    '                ScriptArgList.Add("imprints")
    '                ScriptArgList.Add(TkInteger.ToString(Design.CalendarYear))
    '                ScriptArgList.Add(RecordSeparator)
    '                ScriptArgList.Add(DataSeparator)
    '                ScriptArgList.Add(TkConvertText.ForJavaScript(Personalization.GospelOfPeace.ScGospelOfPeace.ImprintFileTag))
    '                ScriptArgList.Add(TkInteger.ToString(Personalization.GospelOfPeace.ScGospelOfPeace.NumImprintLines))

    '                With Design.Imprints
    '                    If .NumImprints > 0 Then
    '                        NumImprints = .NumImprints
    '                        If NumImprints > 14 Then NumImprints = 14

    '                        For I = 0 To NumImprints - 1
    '                            TextStr = ""
    '                            With .Imprint(I)
    '                                If .NumLines > 0 Then
    '                                    For J = 0 To .NumLines - 1
    '                                        LineStr = .Line(J)

    '                                        If J = 0 Then
    '                                            FilePath = GetResourceFilePath(LineStr, ProductID, StdResources, StdRsrcFolderPath, UserRsrcFolderPath)
    '                                            If FilePath <> "" Then
    '                                                If File.Exists(FilePath) Then
    '                                                    LineStr = Personalization.GospelOfPeace.ScGospelOfPeace.ImprintFileTag & FilePath
    '                                                Else
    '                                                    TkGlobal.Info("Imprint file doesn't exist (" & FilePath & ").", ModuleName)
    '                                                    LineStr = ""
    '                                                End If
    '                                            End If
    '                                        End If

    '                                        If J > 0 Then TextStr &= DataSeparator
    '                                        TextStr &= LineStr
    '                                    Next J
    '                                End If
    '                            End With

    '                            ScriptArgList.Add(TkConvertText.ForJavaScript(TextStr))
    '                        Next I
    '                    End If
    '                End With

    '                ScriptArgs = ScriptArgList.ToArray

    '                ReturnData = IdApp.DoScript(PageupScript, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '                ReturnDataArray = ReturnData.Split(vbTab)

    '                NumValues = ReturnDataArray.Length

    '                If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '                If ErrorMsg <> "" Then
    '                    TkGlobal.ErrorMessage(ErrorMsg, ModuleName)
    '                    StatusOK = False
    '                End If
    '            End If

    '            ' Inside back cover

    '            If StatusOK Then
    '                ScriptArgList.Clear()

    '                ScriptArgList.Add("inside-back-cover")
    '                ScriptArgList.Add(TkInteger.ToString(Design.CalendarYear))
    '                ScriptArgList.Add(TkConvertText.ForJavaScript(Personalization.GospelOfPeace.ScGospelOfPeace.ImprintFileTag))

    '                With Design.Imprints
    '                    If .NumImprints >= 16 Then
    '                        For I = 15 To 16
    '                            TextStr = ""
    '                            With .Imprint(I - 1)
    '                                If .NumLines > 0 Then
    '                                    FilePath = GetResourceFilePath(.Line(0), ProductID, StdResources, StdRsrcFolderPath, UserRsrcFolderPath)
    '                                    If FilePath <> "" Then
    '                                        If File.Exists(FilePath) Then
    '                                            TextStr = Personalization.GospelOfPeace.ScGospelOfPeace.ImprintFileTag & FilePath
    '                                        Else
    '                                            TkGlobal.Info("Inside back cover file doesn't exist (" & FilePath & ").", ModuleName)
    '                                        End If
    '                                    End If
    '                                End If
    '                            End With

    '                            ScriptArgList.Add(TkConvertText.ForJavaScript(TextStr))
    '                        Next I
    '                    End If
    '                End With

    '                ScriptArgs = ScriptArgList.ToArray

    '                ReturnData = IdApp.DoScript(PageupScript, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '                ReturnDataArray = ReturnData.Split(vbTab)

    '                NumValues = ReturnDataArray.Length

    '                If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '                If ErrorMsg <> "" Then
    '                    TkGlobal.ErrorMessage(ErrorMsg, ModuleName)
    '                    StatusOK = False
    '                End If
    '            End If

    '            ' Back cover

    '            If StatusOK Then
    '                ScriptArgList.Clear()

    '                ScriptArgList.Add("back-cover")
    '                ScriptArgList.Add(TkInteger.ToString(Design.CalendarYear))
    '                ScriptArgList.Add(TkConvertText.ForJavaScript(BrandLabel))
    '                ScriptArgList.Add(TkInteger.ToString(ProductID))
    '                ScriptArgList.Add(TkConvertText.ForJavaScript(TitleStr))
    '                ScriptArgList.Add(TkConvertText.ForJavaScript(SubtitleStr))
    '                ScriptArgList.Add(TkConvertText.ForJavaScript(parCustomID))

    '                ScriptArgs = ScriptArgList.ToArray

    '                ReturnData = IdApp.DoScript(PageupScript, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '                ReturnDataArray = ReturnData.Split(vbTab)

    '                NumValues = ReturnDataArray.Length

    '                If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '                If ErrorMsg <> "" Then
    '                    TkGlobal.ErrorMessage(ErrorMsg, ModuleName)
    '                    StatusOK = False
    '                End If
    '            End If

    '            ' Logo

    '            If StatusOK Then
    '                FilePath = GetLogoFilePath(Design, ProductID, StdResources, StdRsrcFolderPath, UserRsrcFolderPath)
    '                If FilePath <> "" Then
    '                    If File.Exists(FilePath) Then
    '                        ScriptArgList.Clear()

    '                        ScriptArgList.Add("logo")
    '                        ScriptArgList.Add(TkInteger.ToString(Design.CalendarYear))
    '                        ScriptArgList.Add(TkConvertText.ForJavaScript(FilePath))

    '                        ScriptArgs = ScriptArgList.ToArray

    '                        ReturnData = IdApp.DoScript(PageupScript, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '                        ReturnDataArray = ReturnData.Split(vbTab)

    '                        NumValues = ReturnDataArray.Length

    '                        If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '                        If ErrorMsg <> "" Then
    '                            TkGlobal.ErrorMessage(ErrorMsg, ModuleName)
    '                            StatusOK = False
    '                        End If
    '                    End If
    '                End If
    '            End If

    '            ' Monthly grids

    '            If StatusOK Then
    '                MonthlyGridsFilePath = parConfig.PageupFolderPath8101 & "\" & Design.CalendarYear.ToString & "\" & Design.CalendarYear.ToString & " Monthly Grids.indd"
    '                If File.Exists(MonthlyGridsFilePath) Then
    '                    ScriptArgList.Clear()

    '                    ScriptArgList.Add("monthly-grids")
    '                    ScriptArgList.Add(TkInteger.ToString(Design.CalendarYear))
    '                    ScriptArgList.Add(TkConvertText.ForJavaScript(MonthlyGridsFilePath))

    '                    ScriptArgs = ScriptArgList.ToArray

    '                    ReturnData = IdApp.DoScript(PageupScript, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '                    ReturnDataArray = ReturnData.Split(vbTab)

    '                    NumValues = ReturnDataArray.Length

    '                    If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '                    If ErrorMsg <> "" Then
    '                        TkGlobal.ErrorMessage(ErrorMsg, ModuleName)
    '                        StatusOK = False
    '                    End If
    '                End If
    '            End If

    '            ' Finish

    '            If StatusOK Then
    '                ScriptArgList.Clear()

    '                ScriptArgList.Add("done")
    '                ScriptArgList.Add(TkInteger.ToString(Design.CalendarYear))
    '                ScriptArgList.Add(TkConvertText.ForJavaScript(OutputFilePath))
    '                ScriptArgList.Add(TkConvertText.ForJavaScript(PdfFilePath))

    '                ScriptArgs = ScriptArgList.ToArray

    '                ReturnData = IdApp.DoScript(PageupScript, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '                ReturnDataArray = ReturnData.Split(vbTab)

    '                NumValues = ReturnDataArray.Length

    '                If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '                If ErrorMsg <> "" Then
    '                    TkGlobal.ErrorMessage(ErrorMsg, ModuleName)
    '                    StatusOK = False
    '                End If
    '            End If
    '        End If

    '    End Sub

    '    Private Shared Function IsAppRunning(ByVal parAppName As String) As Boolean

    '        Dim AppName As String = parAppName.Trim.ToLower
    '        Dim IsRunning As Boolean = False

    '        If AppName <> "" Then
    '            For Each Proc As Process In Process.GetProcesses
    '                If Proc.ProcessName.ToLower.Contains(AppName) Then
    '                    IsRunning = True
    '                    Exit For
    '                End If
    '            Next Proc
    '        End If

    '        Return IsRunning

    '    End Function

    '    Private Shared Function IsImprintFromFile(ByVal parImprintLine As String) As Boolean

    '        Return (parImprintLine.ToLower.StartsWith(Personalization.GospelOfPeace.ScGospelOfPeace.ImprintFileTag.ToLower))

    '    End Function

    '    Private Shared Function IsStdResource(ByVal parFileName As String) As Boolean

    '        Return (parFileName.ToLower.StartsWith(Personalization.GospelOfPeace.ScGospelOfPeace.ImprintStdTag.ToLower))

    '    End Function

    '    Public Shared Sub Pageup(ByVal parConfig As PcConfig, _
    '                             ByVal parProductID As Integer, _
    '                             ByVal parCustomID As String, _
    '                             ByVal parOrderNum As Integer, _
    '                             ByVal parQuantity As Integer, _
    '                             ByVal parInputFileStr As String, _
    '                             ByVal parSiteVersionNum As Single)

    '        Select Case parProductID
    '            Case 6120

    '                ' Tract Card

    '                TractCardPageup(parConfig, parCustomID, parInputFileStr)

    '            Case 6880

    '                ' JNC Mini

    '                JncMiniPageup(parConfig, parCustomID, parInputFileStr)

    '            Case 7427

    '                ' Pocket Calendar

    '                PocketCalendarPageup(parConfig, parCustomID, parInputFileStr)

    '            Case 8101

    '                ' Gospel of Peace Calendar

    '                GospelOfPeacePageup(parConfig, parProductID, parCustomID, parOrderNum, parQuantity, parInputFileStr)
    '        End Select

    '    End Sub

    '    Private Shared Sub JncMiniPageup(ByVal parConfig As PcConfig, _
    '                                     ByVal parCustomID As String, _
    '                                     ByVal parInputFileStr As String)

    '        Dim ASlotTextFilePath As String = ""
    '        Dim BSlotTextFilePath As String = ""
    '        Dim ColorBaseFolderPath As String = parConfig.JncMiniColorBaseFolderPath6880
    '        Dim CSlotTextFilePath As String = ""
    '        Dim ErrorMsg As String = ""
    '        Dim FilePath As String = ""
    '        Dim FontSizeCode As Integer = CInt(GetValue(parInputFileStr, "FontSizeCode"))
    '        Dim I As Integer = 0
    '        Dim ImprintFilePath As String = ""
    '        Dim ImprintHtmlStr As String = GetValue(parInputFileStr, "Imprint")
    '        Dim InDesignDocFilePath As String = ""
    '        Dim IsInDesignRunning As Boolean = IsAppRunning("InDesign")
    '        Dim LanguageName As String = GetValue(parInputFileStr, "Language")
    '        Dim ModuleName As String = "JNC Mini Pageup"
    '        Dim MonthDate As DateTime = Nothing
    '        Dim NumValues As Integer = 0
    '        Dim PageupScriptFilePath As String = parConfig.JncMiniScriptFilePath6880
    '        Dim PdfFilePath As String = ""
    '        Dim ResourcesFlag As Boolean = True
    '        Dim ReturnData As String = ""
    '        Dim ReturnDataArray As String() = Nothing
    '        Dim RsrcFolderPath As String = parConfig.MiniJNCResourcePath6880
    '        Dim ScriptArgList As List(Of String) = Nothing
    '        Dim ScriptArgs As String() = Nothing
    '        Dim TempHtmlFilePath As String = ""
    '        Dim TemplateFilePath As String = parConfig.JncMiniTemplateFilePath6880
    '        Dim Year As Integer = CInt(GetValue(parInputFileStr, "Year"))

    '        ' InDesign template

    '        If Not IO.File.Exists(TemplateFilePath) Then
    '            MsgBox("InDesign template file doesn't exist." & vbCrLf & vbCrLf & TemplateFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Color base files

    '        ColorBaseFolderPath = ColorBaseFolderPath.Replace("{y}", Year.ToString)

    '        If IO.Directory.Exists(ColorBaseFolderPath) Then
    '            For I = 1 To 12
    '                MonthDate = New DateTime(Year, I, 1)
    '                FilePath = ColorBaseFolderPath & "\" & I.ToString & " " & Left(MonthDate.ToString("MMMM"), 3) & "-" & Year.ToString & ".psd"
    '                If Not IO.File.Exists(FilePath) Then
    '                    MsgBox("Color base file doesn't exist." & vbCrLf & vbCrLf & FilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '                    ResourcesFlag = False
    '                End If
    '            Next I

    '        Else
    '            MsgBox("Color base folder doesn't exist." & vbCrLf & vbCrLf & ColorBaseFolderPath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' A slot text file

    '        ASlotTextFilePath = parConfig.LanguagePath6880 & LanguageName & "\" & Year.ToString & "A.pdf"

    '        If Not IO.File.Exists(ASlotTextFilePath) Then
    '            MsgBox("A slot text file doesn't exist." & vbCrLf & vbCrLf & ASlotTextFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' B slot text file

    '        BSlotTextFilePath = parConfig.LanguagePath6880 & LanguageName & "\" & Year.ToString & "B.pdf"

    '        If Not IO.File.Exists(BSlotTextFilePath) Then
    '            MsgBox("B slot text file doesn't exist." & vbCrLf & vbCrLf & BSlotTextFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' C slot text file

    '        CSlotTextFilePath = parConfig.LanguagePath6880 & LanguageName & "\" & Year.ToString & "C.pdf"

    '        If Not IO.File.Exists(CSlotTextFilePath) Then
    '            MsgBox("C slot text file doesn't exist." & vbCrLf & vbCrLf & CSlotTextFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        If ResourcesFlag Then

    '            ' InDesign document file path

    '            InDesignDocFilePath = parConfig.JncMiniComposedFolderPath6880 & "\" & parCustomID & ".indd"
    '            If IO.File.Exists(InDesignDocFilePath) Then IO.File.Delete(InDesignDocFilePath)

    '            ' PDF file path

    '            PdfFilePath = parConfig.JncMiniComposedFolderPath6880 & "\" & parCustomID & ".pdf"
    '            If IO.File.Exists(PdfFilePath) Then IO.File.Delete(PdfFilePath)

    '            ' Imprint

    '            ImprintFilePath = ""

    '            If ImprintHtmlStr <> "" And FontSizeCode > 0 Then

    '                ' Save imprint as temporary HTML file

    '                ImprintHtmlStr = ImprintHtmlStr.Replace("arial", "Arial").Replace("helvetica", "Helvetica")
    '                ImprintHtmlStr = "<html><head></head><body>" & HttpUtility.HtmlDecode(ImprintHtmlStr) & "</body></html>"

    '                TempHtmlFilePath = parConfig.JncMiniComposedFolderPath6880 & "\&TempImprint-" & parCustomID & ".html"
    '                If IO.File.Exists(TempHtmlFilePath) Then IO.File.Delete(TempHtmlFilePath)
    '                IO.File.WriteAllText(TempHtmlFilePath, ImprintHtmlStr)

    '                ' Convert imprint HTML file to Word document

    '                Dim WordApp As New Word.Application
    '                WordApp.Visible = False

    '                On Error GoTo WordDocError

    '                Dim WordDoc As New Word.Document
    '                WordDoc = WordApp.Documents.Open(FileName:=TempHtmlFilePath, ReadOnly:=True, Visible:=False)

    '                ImprintFilePath = parConfig.JncMiniComposedFolderPath6880 & "\&TempImprint-" & parCustomID & ".docx"
    '                If IO.File.Exists(ImprintFilePath) Then IO.File.Delete(ImprintFilePath)

    '                WordDoc.SaveAs(ImprintFilePath, FileFormat:=Word.WdSaveFormat.wdFormatDocumentDefault)

    '                WordDoc.Close(SaveChanges:=False)

    '                GoTo WordDocResume

    'WordDocError:

    '                ErrorMsg = "Microsoft Word error #" & Err.Number.ToString & ": " & Err.Description
    '                MsgBox(ErrorMsg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)

    'WordDocResume:

    '                WordApp.Quit(SaveChanges:=False)
    '            End If

    '            ' Start InDesign execution

    '            On Error GoTo InDesignError

    '            Dim IdApp As InDesign.Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)

    '            ' Script parameters

    '            ScriptArgList = New List(Of String)

    '            ScriptArgList.Add(Year.ToString)
    '            ScriptArgList.Add(ConvertForJavaScript(TemplateFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(InDesignDocFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(ColorBaseFolderPath))
    '            ScriptArgList.Add(ConvertForJavaScript(ASlotTextFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(BSlotTextFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(CSlotTextFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(ImprintFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(PdfFilePath))
    '            ScriptArgList.Add(FontSizeCode.ToString)

    '            ' Generate InDesign file

    '            ScriptArgs = ScriptArgList.ToArray

    '            ReturnData = IdApp.DoScript(PageupScriptFilePath, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '            ReturnDataArray = ReturnData.Split(vbTab)

    '            NumValues = ReturnDataArray.Length

    '            If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '            If ErrorMsg <> "" Then MsgBox(ErrorMsg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)

    '            GoTo InDesignResume

    'InDesignError:

    '            ErrorMsg = "InDesign error #" & Err.Number.ToString & ": " & Err.Description
    '            MsgBox(ErrorMsg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)

    'InDesignResume:

    '            If IO.File.Exists(ImprintFilePath) Then IO.File.Delete(ImprintFilePath)
    '            If IO.File.Exists(TempHtmlFilePath) Then IO.File.Delete(TempHtmlFilePath)

    '            ' Finish InDesign execution

    '            'If Not IsInDesignRunning Then IdApp.Quit(InDesign.idSaveOptions.idNo)
    '        End If

    '    End Sub

    '    Private Shared Sub PocketCalendarPageup(ByVal parConfig As PcConfig, _
    '                                            ByVal parCustomID As String, _
    '                                            ByVal parInputFileStr As String)

    '        Dim BackFilePath As String = ""
    '        Dim BackName As String = GetValue(parInputFileStr, "Back")
    '        Dim CalPadFilePath As String = ""
    '        Dim ErrorMsg As String = ""
    '        Dim FontSizeCode As Integer = CInt(GetValue(parInputFileStr, "FontSizeCode"))
    '        Dim FrontFilePath As String = ""
    '        Dim FrontName As String = GetValue(parInputFileStr, "Front")
    '        Dim ImprintFilePath As String = ""
    '        Dim ImprintHtmlStr As String = GetValue(parInputFileStr, "Imprint")
    '        Dim InDesignDocFilePath As String = ""
    '        Dim IsInDesignRunning As Boolean = IsAppRunning("InDesign")
    '        Dim LanguageName As String = GetValue(parInputFileStr, "Language")
    '        Dim MessageFilePath As String = ""
    '        Dim MessageName As String = GetValue(parInputFileStr, "Message")
    '        Dim ModuleName As String = "Pocket Calendar Pageup"
    '        Dim NumValues As Integer = 0
    '        Dim PageupScriptFilePath As String = parConfig.PocketCalendarScriptFilePath7427
    '        Dim PdfFilePath As String = ""
    '        Dim ResourcesFlag As Boolean = True
    '        Dim ReturnData As String = ""
    '        Dim ReturnDataArray As String() = Nothing
    '        Dim RsrcFolderPath As String = parConfig.PocketCalendarCardsResourcePath7427
    '        Dim ScriptArgList As List(Of String) = Nothing
    '        Dim ScriptArgs As String() = Nothing
    '        Dim TempHtmlFilePath As String = ""
    '        Dim TemplateFilePath As String = parConfig.PocketCalendarTemplateFilePath7427
    '        Dim VerseFilePath As String = ""
    '        Dim VerseName As String = GetValue(parInputFileStr, "Verse")
    '        Dim Year As Integer = CInt(GetValue(parInputFileStr, "Year"))

    '        ' Graphic images for front and back

    '        BackFilePath = RsrcFolderPath & "Backs\" & BackName.Replace(" ", "_") & ".pdf"
    '        CalPadFilePath = RsrcFolderPath & "Calendar_Pads\" & Year & " " & LanguageName & ".pdf"
    '        FrontFilePath = RsrcFolderPath & "Fronts\" & FrontName.Replace(" ", "_") & ".pdf"
    '        MessageFilePath = RsrcFolderPath & "Messages\" & LanguageName & "\" & MessageName.Replace(" ", "_").Replace("&amp;#39;", "'") & ".pdf"
    '        VerseFilePath = RsrcFolderPath & "Verses\" & LanguageName & "\" & VerseName.Replace(":", ".") & ".pdf"

    '        ' InDesign template

    '        If Not IO.File.Exists(TemplateFilePath) Then
    '            MsgBox("InDesign template file doesn't exist." & vbCrLf & vbCrLf & TemplateFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Front file

    '        If Not IO.File.Exists(FrontFilePath) Then
    '            MsgBox("Front file doesn't exist." & vbCrLf & vbCrLf & FrontFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Message file

    '        If Not IO.File.Exists(MessageFilePath) Then
    '            MsgBox("Message file doesn't exist." & vbCrLf & vbCrLf & MessageFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Back file

    '        If Not IO.File.Exists(BackFilePath) Then
    '            MsgBox("Back file doesn't exist." & vbCrLf & vbCrLf & BackFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Calendar pad file

    '        If Not IO.File.Exists(CalPadFilePath) Then
    '            MsgBox("Calendar pad file doesn't exist." & vbCrLf & vbCrLf & CalPadFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Verse file

    '        If Not IO.File.Exists(VerseFilePath) Then
    '            MsgBox("Verse file doesn't exist." & vbCrLf & vbCrLf & VerseFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        If ResourcesFlag Then

    '            ' InDesign document file path

    '            InDesignDocFilePath = parConfig.PocketCalendarComposedFolderPath7427 & "\" & parCustomID & ".indd"
    '            If IO.File.Exists(InDesignDocFilePath) Then IO.File.Delete(InDesignDocFilePath)

    '            ' PDF file path

    '            PdfFilePath = parConfig.PocketCalendarComposedFolderPath7427 & "\" & parCustomID & ".pdf"
    '            If IO.File.Exists(PdfFilePath) Then IO.File.Delete(PdfFilePath)

    '            ' Imprint

    '            ImprintFilePath = ""

    '            If ImprintHtmlStr <> "" And FontSizeCode > 0 Then

    '                ' Save imprint as temporary HTML file

    '                ImprintHtmlStr = ImprintHtmlStr.Replace("arial", "Arial").Replace("helvetica", "Helvetica")
    '                ImprintHtmlStr = "<html><head></head><body>" & HttpUtility.HtmlDecode(ImprintHtmlStr) & "</body></html>"

    '                TempHtmlFilePath = parConfig.PocketCalendarComposedFolderPath7427 & "\&TempImprint-" & parCustomID & ".html"
    '                If IO.File.Exists(TempHtmlFilePath) Then IO.File.Delete(TempHtmlFilePath)
    '                IO.File.WriteAllText(TempHtmlFilePath, ImprintHtmlStr)

    '                ' Convert imprint HTML file to Word document

    '                Dim WordApp As New Word.Application
    '                WordApp.Visible = False

    '                On Error GoTo WordDocError

    '                Dim WordDoc As New Word.Document
    '                WordDoc = WordApp.Documents.Open(FileName:=TempHtmlFilePath, ReadOnly:=True, Visible:=False)

    '                ImprintFilePath = parConfig.PocketCalendarComposedFolderPath7427 & "\&TempImprint-" & parCustomID & ".docx"
    '                If IO.File.Exists(ImprintFilePath) Then IO.File.Delete(ImprintFilePath)

    '                WordDoc.SaveAs(ImprintFilePath, FileFormat:=Word.WdSaveFormat.wdFormatDocumentDefault)

    '                WordDoc.Close(SaveChanges:=False)

    '                GoTo WordDocResume

    'WordDocError:

    '                ErrorMsg = "Microsoft Word error #" & Err.Number.ToString & ": " & Err.Description
    '                MsgBox(ErrorMsg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)

    'WordDocResume:

    '                WordApp.Quit(SaveChanges:=False)
    '            End If

    '            ' Start InDesign execution

    '            On Error GoTo InDesignError

    '            Dim IdApp As InDesign.Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)

    '            ' Script parameters

    '            ScriptArgList = New List(Of String)

    '            ScriptArgList.Add(ConvertForJavaScript(TemplateFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(InDesignDocFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(FrontFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(MessageFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(BackFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(CalPadFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(VerseFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(ImprintFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(PdfFilePath))
    '            ScriptArgList.Add(FontSizeCode.ToString)

    '            ' Generate InDesign file

    '            ScriptArgs = ScriptArgList.ToArray

    '            ReturnData = IdApp.DoScript(PageupScriptFilePath, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '            ReturnDataArray = ReturnData.Split(vbTab)

    '            NumValues = ReturnDataArray.Length

    '            If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '            If ErrorMsg <> "" Then MsgBox(ErrorMsg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)

    '            GoTo InDesignResume

    'InDesignError:

    '            ErrorMsg = "InDesign error #" & Err.Number.ToString & ": " & Err.Description
    '            MsgBox(ErrorMsg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)

    'InDesignResume:

    '            If IO.File.Exists(ImprintFilePath) Then IO.File.Delete(ImprintFilePath)
    '            If IO.File.Exists(TempHtmlFilePath) Then IO.File.Delete(TempHtmlFilePath)

    '            ' Finish InDesign execution

    '            'If Not IsInDesignRunning Then IdApp.Quit(InDesign.idSaveOptions.idNo)
    '        End If

    '    End Sub

    '    Private Shared Sub TractCardPageup(ByVal parConfig As PcConfig, _
    '                                       ByVal parCustomID As String, _
    '                                       ByVal parInputFileStr As String)

    '        Dim BackFilePath As String = ""
    '        Dim BackName As String = GetValue(parInputFileStr, "Back")
    '        Dim ErrorMsg As String = ""
    '        Dim FontSizeCode As Integer = CInt(GetValue(parInputFileStr, "FontSizeCode"))
    '        Dim FrontFilePath As String = ""
    '        Dim FrontName As String = GetValue(parInputFileStr, "Front")
    '        Dim ImprintFilePath As String = ""
    '        Dim ImprintHtmlStr As String = GetValue(parInputFileStr, "Imprint")
    '        Dim InDesignDocFilePath As String = ""
    '        Dim IsInDesignRunning As Boolean = IsAppRunning("InDesign")
    '        Dim ModuleName As String = "Tract Card Pageup"
    '        Dim NumValues As Integer = 0
    '        Dim PageupScriptFilePath As String = parConfig.TractCardScriptFilePath6120
    '        Dim PdfFilePath As String = ""
    '        Dim ResourcesFlag As Boolean = True
    '        Dim ReturnData As String = ""
    '        Dim ReturnDataArray As String() = Nothing
    '        Dim RsrcFolderPath As String = parConfig.TractCardsResourcePath6120
    '        Dim ScriptArgList As List(Of String) = Nothing
    '        Dim ScriptArgs As String() = Nothing
    '        Dim TempHtmlFilePath As String = ""
    '        Dim TemplateFilePath As String = parConfig.TractCardTemplateFilePath6120

    '        ' Graphic images for front and back

    '        BackFilePath = RsrcFolderPath & "Backs\" & BackName.Replace(" ", "_").Replace(".png", "") & ".pdf"
    '        FrontFilePath = RsrcFolderPath & "Fronts\" & FrontName.Replace(" ", "_").Replace(".png", "") & ".pdf"

    '        ' InDesign template

    '        If Not IO.File.Exists(TemplateFilePath) Then
    '            MsgBox("InDesign template file doesn't exist." & vbCrLf & vbCrLf & TemplateFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Front file

    '        If Not IO.File.Exists(FrontFilePath) Then
    '            MsgBox("Front file doesn't exist." & vbCrLf & vbCrLf & FrontFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        ' Back file

    '        If Not IO.File.Exists(BackFilePath) Then
    '            MsgBox("Back file doesn't exist." & vbCrLf & vbCrLf & BackFilePath, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)
    '            ResourcesFlag = False
    '        End If

    '        If ResourcesFlag Then

    '            ' InDesign document file path

    '            InDesignDocFilePath = parConfig.TractCardComposedFolderPath6120 & "\" & parCustomID & ".indd"
    '            If IO.File.Exists(InDesignDocFilePath) Then IO.File.Delete(InDesignDocFilePath)

    '            ' PDF file path

    '            PdfFilePath = parConfig.TractCardComposedFolderPath6120 & "\" & parCustomID & ".pdf"
    '            If IO.File.Exists(PdfFilePath) Then IO.File.Delete(PdfFilePath)

    '            ' Imprint

    '            ImprintFilePath = ""

    '            If ImprintHtmlStr <> "" And FontSizeCode > 0 Then

    '                ' Save imprint as temporary HTML file

    '                ImprintHtmlStr = ImprintHtmlStr.Replace("arial", "Arial").Replace("helvetica", "Helvetica")
    '                ImprintHtmlStr = "<html><head></head><body>" & HttpUtility.HtmlDecode(ImprintHtmlStr) & "</body></html>"

    '                TempHtmlFilePath = parConfig.TractCardComposedFolderPath6120 & "\&TempImprint-" & parCustomID & ".html"
    '                If IO.File.Exists(TempHtmlFilePath) Then IO.File.Delete(TempHtmlFilePath)
    '                IO.File.WriteAllText(TempHtmlFilePath, ImprintHtmlStr)

    '                ' Convert imprint HTML file to Word document

    '                Dim WordApp As New Word.Application
    '                WordApp.Visible = False

    '                On Error GoTo WordDocError

    '                Dim WordDoc As New Word.Document
    '                WordDoc = WordApp.Documents.Open(FileName:=TempHtmlFilePath, ReadOnly:=True, Visible:=False)

    '                ImprintFilePath = parConfig.TractCardComposedFolderPath6120 & "\&TempImprint-" & parCustomID & ".docx"
    '                If IO.File.Exists(ImprintFilePath) Then IO.File.Delete(ImprintFilePath)

    '                WordDoc.SaveAs(ImprintFilePath, FileFormat:=Word.WdSaveFormat.wdFormatDocumentDefault)

    '                WordDoc.Close(SaveChanges:=False)

    '                GoTo WordDocResume

    'WordDocError:

    '                ErrorMsg = "Microsoft Word error #" & Err.Number.ToString & ": " & Err.Description
    '                MsgBox(ErrorMsg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)

    'WordDocResume:

    '                WordApp.Quit(SaveChanges:=False)
    '            End If

    '            ' Start InDesign execution

    '            On Error GoTo InDesignError

    '            Dim IdApp As InDesign.Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)

    '            ' Script parameters

    '            ScriptArgList = New List(Of String)

    '            ScriptArgList.Add(ConvertForJavaScript(TemplateFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(InDesignDocFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(FrontFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(BackFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(ImprintFilePath))
    '            ScriptArgList.Add(ConvertForJavaScript(PdfFilePath))
    '            ScriptArgList.Add(FontSizeCode.ToString)

    '            ' Generate InDesign file

    '            ScriptArgs = ScriptArgList.ToArray

    '            ReturnData = IdApp.DoScript(PageupScriptFilePath, InDesign.idScriptLanguage.idJavascript, ScriptArgs)

    '            ReturnDataArray = ReturnData.Split(vbTab)

    '            NumValues = ReturnDataArray.Length

    '            If NumValues >= 1 Then ErrorMsg = ReturnDataArray(NumValues - 1)

    '            If ErrorMsg <> "" Then MsgBox(ErrorMsg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)

    '            GoTo InDesignResume

    'InDesignError:

    '            ErrorMsg = "InDesign error #" & Err.Number.ToString & ": " & Err.Description
    '            MsgBox(ErrorMsg, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, ModuleName)

    'InDesignResume:

    '            If IO.File.Exists(ImprintFilePath) Then IO.File.Delete(ImprintFilePath)
    '            If IO.File.Exists(TempHtmlFilePath) Then IO.File.Delete(TempHtmlFilePath)

    '            ' Finish InDesign execution

    '            'If Not IsInDesignRunning Then IdApp.Quit(InDesign.idSaveOptions.idNo)
    '        End If

    '    End Sub

#End Region

End Class ' PcCompose