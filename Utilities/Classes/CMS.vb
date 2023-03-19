Imports System.Text.RegularExpressions
Imports System.IO


Public Class CMS_FileName


#Region "Properties"

    Public Enum CalPageOpts
        pages12
        pages4
        pagesAbbreviated
    End Enum


    ''' <summary>
    ''' returns ({"?", "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"}) as a list
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CalOrder12Pg() As List(Of String)
        Get
            Return {"?", "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"}.ToList
        End Get
    End Property


    ''' <summary>
    ''' returns ({"?", "janfebmar", "aprmayjun", "julaugsep", "octnovdec"}) as a list
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property calOrder4Pg() As List(Of String)
        Get
            Return {"?", "janfebmar", "aprmayjun", "julaugsep", "octnovdec"}.ToList
        End Get
    End Property

    Public ReadOnly Property CalOrderAbbreviated() As List(Of String)
        Get
            Return {"?", "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec", "wallet", "??", "backcover", "cover"}.ToList
        End Get
    End Property

    ''' <summary>
    ''' returns {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"} as a list
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MonthsAbbreviated As List(Of String)
        Get
            Return {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}.ToList
        End Get
    End Property

    ''' <summary>
    ''' The file info of the original file
    ''' </summary>
    ''' <returns></returns>
    Public Property OriginalFile() As FileInfo

    ''' <summary>
    ''' use only when sure it will be processing file names with product / library id. eg. don't use with X:\CMS\3\3052\139720.rtf because it won't pick up the correct folder name
    ''' </summary>
    ''' <returns></returns>
    Public Property possibleFolderName() As String

    ''' <summary>
    ''' File's ID. (if found / Applicable)
    ''' </summary>
    ''' <returns></returns>
    Public Property ID() As String = ""

    ''' <summary>
    ''' File's Category. (if found / Applicable)
    ''' </summary>
    ''' <returns></returns>
    Public Property Category() As String = ""


    ''' <summary>
    ''' File's year (if found / Applicable)
    ''' </summary>
    ''' <returns></returns>
    Public Property Year As String = ""


    ''' <summary>
    ''' File number (if found / Applicable) Note: not the product number
    ''' </summary>
    ''' <returns></returns>
    Public Property FileNumber() As String = ""

    ''' <summary>
    ''' Optional description (if found / Applicable)
    ''' </summary>
    ''' <returns></returns>
    Public Property Description() As String = ""


    Public Property CalStyleSelected() As CalPageOpts = -1

    ''' <summary>
    ''' Lrg, Med, Sml, Nsd, etc (if found / Applicable)
    ''' </summary>
    ''' <returns></returns>
    Public Property Size() As String = ""

    ''' <summary>
    ''' File extension
    ''' </summary>
    ''' <returns></returns>
    Public Property Extension() As String = ""



    ''' <summary>
    ''' what's used to separate categories / parts.
    ''' </summary>
    ''' <returns></returns>
    Public Property spacer() As String = "-"
    '_spacer = "-" ' Comment this line!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



    ''' <summary>
    ''' returns the file name for local cms 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LocalCMSFileName() As String
        Get
            Dim localName As New Text.StringBuilder

            If isValidFileName Then

                localName.Append(ID & spacer)

                If Category <> "" Then
                    localName.Append(Category & spacer)
                End If

                If Year <> "" Then
                    localName.Append(Year & spacer)
                End If

                If FileNumber <> "" Then
                    localName.Append(FileNumber.Trim.PadLeft(3, "0") & spacer)
                End If

                If Description <> "" Then
                    localName.Append(Description & spacer)
                End If

                If size = "" Then
                    If Category = cImage Then
                        localName.Append(cNonStandard)
                    End If
                Else
                    localName.Append(size)
                End If
                Dim tempName As String = localName.ToString.TrimEnd(" - ".ToCharArray)
                localName.Clear()
                localName.Append(tempName & Extension)

            Else
                localName.Append(OriginalFile.Name)
            End If






            Return localName.ToString
        End Get
    End Property

    ''' <summary>
    ''' Returns the file name for the web cms
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property WebCMSFileName() As String
        Get
            Dim webName As New Text.StringBuilder
            If isValidFileName Then
                webName.Append(ID & spacer)

                If Category <> "" Then
                    webName.Append(Category & spacer)
                End If

                If Year <> "" Then
                    webName.Append(Year & spacer)
                End If

                If FileNumber <> "" Then
                    webName.Append(FileNumber.Trim.PadLeft(3, "0") & spacer)
                End If

                If Description <> "" Then

                    If Category = cMaster Or Category = cLibrary Or Category = cPageUp Then
                        webName.Append(Description & spacer)
                    End If
                End If

                'If description <> "" Then
                '    _webCMSFileName &= description & spacer
                'End If

                If size = "" Then
                    If Category = cImage Then
                        webName.Append(cNonStandard)

                    End If
                Else
                    webName.Append(size)

                End If

                'If originalFile.Name.Contains(" - ") And Category = cProduct Then
                '    MsgBox(originalFile.Name)
                'End If

                Dim temp As String = webName.ToString.TrimEnd(" - ".ToCharArray)
                webName.Clear()
                webName.Append(temp)

                webName.Append(Extension)

            Else
                webName.Append(OriginalFile.Name)
            End If

            Return webName.ToString

        End Get
    End Property

    Public ReadOnly Property ProductFileName As String
        Get
            Dim productName As New Text.StringBuilder

            Dim tempSpacer As String = spacer 'overrides spacer for product side file name
            spacer = "-"

            If isValidFileName Then

                productName.Append(ID & spacer)

                If Year <> "" Then
                    productName.Append(Year & spacer)
                End If

                If CalStyleSelected > -1 Then
                    'it has a calendar month
                    Dim mo As String = ""
                    Select Case CalStyleSelected
                        Case CalPageOpts.pages12
                            mo = CalOrder12Pg(FileNumber)
                        Case CalPageOpts.pages4
                            mo = calOrder4Pg(FileNumber)
                        Case CalPageOpts.pagesAbbreviated
                            mo = CalOrderAbbreviated(FileNumber)
                    End Select
                    For Each month As String In MonthsAbbreviated
                        If mo.Contains(month.ToLower) Then
                            'replaces with proper case version
                            mo = mo.Replace(month.ToLower, month)
                        End If
                    Next
                    productName.Append(mo & spacer)
                End If

                If Description <> "" Then
                    Dim d As String = Description.ToLower
                    If d.Contains("blurb") Or d.Contains("homeprint") Or d.Contains("preview") Then
                        productName.Append(Description.Replace("_", "-") & spacer)
                    Else
                        productName.Append(Description & spacer)
                    End If
                End If


                Dim prodSize As String = ""
                Select Case size
                    Case cLarge
                        prodSize = "Large-"
                    Case cMedium
                        prodSize = "Medium-"
                    Case cSmall
                        prodSize = "Small-"
                    Case Else
                        prodSize = ""
                End Select
                productName.Append(prodSize)




            Else
                productName.Append(OriginalFile.Name)
            End If
            Dim temp As String = productName.ToString.TrimEnd(" - ".ToCharArray)
            productName.Clear()
            productName.Append(temp)

            productName.Append(Extension)

            'reverts spacer to original
            spacer = tempSpacer

            Return productName.ToString
        End Get
    End Property


    ''' <summary>
    ''' File name was or was not parsed successfully
    ''' </summary>
    ''' <returns></returns>
    Public Property IsValidFileName() As Boolean = True

    ''' <summary>
    ''' reason the file wasn't able to be parsed.
    ''' </summary>
    ''' <returns></returns>
    Public Property invalidReason() As String = ""


#End Region


#Region "Init"

    ''' <summary>
    ''' automatically parses the currentFile name
    ''' </summary>
    ''' <param name="currentFile"></param>
    Public Sub New(ByVal currentFile As FileInfo)
        OriginalFile = currentFile
        ParseFileName()
    End Sub
#End Region


#Region "Methods"



    Public Function ParseFileName(Optional ByRef errors As String = "") As Boolean
        Dim success As Boolean = True
        Try

            Const cUndetermined As String = "?????"

            Dim originalNameNoExt As String = Path.GetFileNameWithoutExtension(OriginalFile.FullName)

            If OriginalFile.Name.Contains(" - ") Then
                spacer = " - "
            End If

            Extension = OriginalFile.Extension

            'gets the file id and folder id
            ID = getProductNumberFromFileName(originalNameNoExt)




            'sets the category
            Select Case OriginalFile.Extension.ToLower
                Case ".png", ".jpg", ".jpeg", ".psd", ".tiff", ".bmp", ".gif"
                    'Image File
                    Category = cImage

                Case ".txt", ".rtf", ".doc", ".docx", ".pdf", ".xml", ".html"
                    'General File
                    Category = cGeneral

                Case ".wav", ".mp3"
                    'Audio File
                    Category = cAudio

                Case ".epub", ".mobi"
                    'Ebook File
                    Category = cEbook

                Case ".eps"
                    Category = cImage
                    errors &= "EPS file format"
                    success = False

                Case ".indd"


                Case ""

                Case Else
                    Category = cUndetermined
                    success = False

            End Select
            originalNameNoExt = originalNameNoExt.Replace("Back-Cover", "BackCover")
            Dim parts As New List(Of String)(originalNameNoExt.Split("-").ToList)
            Dim i As Integer = 0
            For Each myPart In parts
                myPart = myPart.Trim
                Select Case True

                    Case myPart = ID
                        'the id has already been determined

                    Case myPart = Category
                        'the category has already been determined

                    Case myPart = cProduct,
                         myPart = cMaster,
                         myPart = cPageUp,
                         myPart = cFinal,
                         myPart = cProof2,
                         myPart = cProof,
                         myPart = cEbook

                        Category = myPart

                    Case myPart = cLibrary
                        Category = cLibrary


'----------- FileNumber or Year ---------------
                    Case IsNumeric(myPart)
                        'either the image number or image year
                        Dim numPart As Integer = myPart
                        If numPart < 1000 Then
                            ' probably file number
                            FileNumber = myPart
                        Else
                            ' probably file year
                            Year = numPart
                        End If

'----------- FileSize ----------------------
                    Case myPart = cLarge, myPart = "Large"
                        Size = cLarge

                    Case myPart = cMedium, myPart = "Medium"
                        Size = cMedium

                    Case myPart = cSmall, myPart = "Small"
                        Size = cSmall

                    Case myPart = cNonStandard, myPart.ToLower.Contains("webad")
                        Size = cNonStandard
                        addToDescription(myPart)

'----------- Image Number --------------------
                    Case CalOrderAbbreviated.Contains(myPart.ToLower)
                        CalStyleSelected = CalPageOpts.pagesAbbreviated
                        FileNumber = CalOrderAbbreviated.IndexOf(myPart.ToLower).ToString.PadLeft(2)

                    Case CalOrder12Pg.Contains(myPart.ToLower)
                        CalStyleSelected = CalPageOpts.pages12
                        FileNumber = CalOrder12Pg.IndexOf(myPart.ToLower).ToString.PadLeft(2)

                    Case calOrder4Pg.Contains(myPart.ToLower)
                        CalStyleSelected = CalPageOpts.pages4
                        FileNumber = calOrder4Pg.IndexOf(myPart.ToLower)

                    Case Else
                        'description... I think
                        addToDescription(myPart)


                        If myPart.ToLower.Contains("webad") Then
                            Size = cNonStandard

                        ElseIf myPart.ToLower.Contains("preview") Then
                            myPart = myPart.ToLower.Replace("preview", "")
                            myPart = myPart.Replace("single", "")
                            If IsNumeric(myPart) Then
                                FileNumber = myPart
                            Else
                                MsgBox("The following file doesn't seem to match normal naming:" & myPart & vbCrLf & OriginalFile.FullName, MsgBoxStyle.Information)
                            End If

                        ElseIf myPart.ToLower.Contains("datasheet") Then
                            myPart = myPart.ToLower.Replace("datasheet", "")
                            If IsNumeric(myPart) Then
                                FileNumber = myPart
                            End If
                        End If

                End Select
                i += 1
            Next
            If Category = cImage Or Category = cProduct Or Category = cGeneral Then
                If FileNumber = "" Then
                    FileNumber = "0"
                End If
            End If

            If Not IsNumeric(ID) Then
                'an id wasn't found.
                success = False
                errors &= "ID is not numeric"
            End If

            If OriginalFile.Name = ID & Extension Then
                If Extension = ".rtf" Or Extension = ".xml" Then
                    Category = ""
                    FileNumber = ""

                End If
            End If
            Description = Description.Replace("-", " ")


        Catch ex As Exception
            success = False
            errors &= ex.Message
            MsgBox(ex.Message)
        End Try

        IsValidFileName = success
        invalidReason = errors
        Return success
    End Function

    Private Sub AddToDescription(ByVal whatToAdd As String)

        If Description <> "" Then
            Description &= "_" & whatToAdd
        Else
            Description = whatToAdd
        End If
    End Sub


    Public Function ImageSameExceptNamedSize(ByVal otherCMSFileName As CMS_FileName) As Boolean


        If IsValidFileName <> otherCMSFileName.IsValidFileName Then
            Return False

        ElseIf IsValidFileName And otherCMSFileName.IsValidFileName Then
            'found an image match
            With otherCMSFileName
                Select Case True
                    Case ID <> .ID
                        Return False
                    Case Year <> .Year
                        Return False
                    Case Category <> .Category
                        Return False
                    Case Extension <> .Extension
                        Return False
                    Case FileNumber <> .FileNumber
                        Return False

                End Select



            End With
        Else
            If WebCMSFileName <> otherCMSFileName.WebCMSFileName Then
                Return False
            End If
        End If

        Return True
    End Function


    ''' <summary>
    ''' Returns the product number from a file path (assuming that the product number is the first thing in the file name)
    ''' </summary>
    ''' <param name="aFileNameWithoutExtension"></param> 
    ''' <returns></returns>
    Public Function getProductNumberFromFileName(ByVal aFileNameWithoutExtension As String) As String
        Dim ProdNumber As String = ""
        Try
            Dim aFileNoExt As String = aFileNameWithoutExtension
            ' Dim fi As New FileInfo(aFilePath)
            'dim fiNoExt as string = fi.
            If aFileNoExt.Length > 6 AndAlso Regex.IsMatch(aFileNoExt.Substring(0, 7), "\d\d\d\d\d\d\d") Then
                ProdNumber = aFileNoExt.Substring(0, 7)
                possibleFolderName = aFileNoExt.Substring(0, 4)
            ElseIf aFileNoExt.Length > 5 AndAlso Regex.IsMatch(aFileNoExt.Substring(0, 6), "\d\d\d\d\d\d") Then
                ProdNumber = aFileNoExt.Substring(0, 6)
                possibleFolderName = aFileNoExt.Substring(0, 3)
            ElseIf aFileNoExt.Length > 4 AndAlso Regex.IsMatch(aFileNoExt.Substring(0, 5), "\d\d\d\d\d") Then
                ProdNumber = aFileNoExt.Substring(0, 5)
                possibleFolderName = aFileNoExt.Substring(0, 2)
            ElseIf aFileNoExt.Length > 3 AndAlso Regex.IsMatch(aFileNoExt.Substring(0, 4), "\d\d\d\d") Then
                ProdNumber = aFileNoExt.Substring(0, 4)
                possibleFolderName = aFileNoExt.Substring(0, 1)
            ElseIf aFileNoExt.Length > 2 AndAlso Regex.IsMatch(aFileNoExt.Substring(0, 3), "\d\d\d") Then
                'this If statement allows files to be named with 3 digit prefixes as opposed to the standard 4 digits 
                ProdNumber = aFileNoExt.Substring(0, 3)
                possibleFolderName = "0"
            Else
                ProdNumber = "aaaa"
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            ProdNumber = "aaaa"
        End Try
        Return ProdNumber
    End Function
#End Region


End Class

