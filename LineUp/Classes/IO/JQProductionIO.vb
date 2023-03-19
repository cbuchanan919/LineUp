Imports System.IO
''' <summary>
''' Locates and stores the locations of the production files. This class is where the folders are populated. (in the init)
''' </summary>
Public Class JQProductionIO


#Region "Properties"

    ''' <summary>
    ''' Master list of Production directories - the info for each category
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductionFolders As New List(Of JQProductionDirInfo)

    ''' <summary>
    ''' Master list of all production files
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductionFiles As New List(Of JQProductionFileInfo)

    ''' <summary>
    ''' contains a list of common alignment pdf's
    ''' </summary>
    ''' <returns></returns>
    Public Property AlignmentFiles As New List(Of JQProductionFileInfo)

#End Region


#Region "Init"

    Public Sub New()
        With My.Settings


            'Pamphlet
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Pamphlet, New DirectoryInfo(.dirPamBod), BodyVsCover.Body, 1, PrinterCategory.Pamphlet_Body))
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Pamphlet, New DirectoryInfo(.dirPamCov), BodyVsCover.Cover, 2, PrinterCategory.Pamphlet_Cover))
            'ProductionFolders.Add(New ProductType(cPamphlet, .dirPamBod, .dirPamCov, cPamBod, cPamCov, 1, 2))                       'pamphlet

            'MiniPamphlet
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Mini_Pamphlet, New DirectoryInfo(.dirMiniPamBod), BodyVsCover.Body, 2, PrinterCategory.Pamphlet_Body))
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Mini_Pamphlet, New DirectoryInfo(.dirMiniPamCov), BodyVsCover.Cover, 4, PrinterCategory.Pamphlet_Cover))
            'ProductionFolders.Add(New ProductType(cMiniPamphlet, .dirMiniPamBod, .dirMiniPamCov, cMiniPamBod, cMiniPamCov, 2, 4))   'mini pamphlet

            'Book
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Book, New DirectoryInfo(.dirBkBod), BodyVsCover.Body, 4, PrinterCategory.Book_Body))
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Book, New DirectoryInfo(.dirBkCov), BodyVsCover.Cover, 2, PrinterCategory.Book_Cover))
            'ProductionFolders.Add(New ProductType(cBook, .dirBkBod, .dirBkCov, cBkBod, cBkCov, 4, 2))                               'book

            'full bleed book
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Full_Bleed_Book, New DirectoryInfo(.dirBkBodBleed), BodyVsCover.Body, 4, PrinterCategory.Book_Body))
            'ProductionFolders.Add(New ProductionDirInfo(ProductCategory.Full_Bleed_Book, New DirectoryInfo(.), BodyVsCover.Cover, 2))
            'ProductionFolders.Add(New ProductType(cFullBleedBook, .dirBkBodBleed, .dirBkCov, cBkBodFullBleed, cBkCov, 4, 2))        'full bleed book

            'Brochure
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Brochure, New DirectoryInfo(.dirBrochure), BodyVsCover.Body, 2, PrinterCategory.Brochure))
            'ProductionFolders.Add(New ProductType(cBrochure, .dirBrochure, "", cBrochure, "", 2, 0))                                'brochure

            'Leaflet
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Leaflet, New DirectoryInfo(.dirLeaflet), BodyVsCover.Body, 2, PrinterCategory.Leaflet))
            'ProductionFolders.Add(New ProductType(cLeaflet, .dirLeaflet, "", cLeaflet, "", 2, 0))                                   'leaflet

            'Tract Card
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Tract_Card, New DirectoryInfo(.dirTractCard), BodyVsCover.Cover, 1, PrinterCategory.Tract_Card))
            'ProductionFolders.Add(New ProductType(cTractCard, "", .dirTractCard, "", cTractCard, 0, 1))                             'tract card

            'Hymnbook
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Hymn_Book, New DirectoryInfo(.dirHymnbookBod), BodyVsCover.Body, 1, PrinterCategory.Hymnbook_Body))
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Hymn_Book, New DirectoryInfo(.dirHymnbookCov), BodyVsCover.Cover, 1, PrinterCategory.Hymnbook_Cover))
            'ProductionFolders.Add(New ProductType(cHymnBook, .dirHymnbookBod, .dirHymnbookCov, cHymnBod, cHymnCov, 1, 1))           'hymn book

            'chart
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Chart, New DirectoryInfo(.dirChart), BodyVsCover.Cover, 1, PrinterCategory.Chart))
            'ProductionFolders.Add(New ProductType(cChart, "", .dirChart, "", cChart, 0, 1))                                         'chart

            'Cd Album Cover
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Cd_Album_Cover, New DirectoryInfo(.dirCdAlbumCov), BodyVsCover.Cover, 2, PrinterCategory.CD_Album_Cover))
            'ProductionFolders.Add(New ProductType(cCdAlbumCover, "", .dirCdAlbumCov, "", cCdAlbumCover, 0, 2))                      'cd album cover

            'Mini Gift Book
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Mini_Gift_Book, New DirectoryInfo(.dirMiniGiftBkBod), BodyVsCover.Body, 8, PrinterCategory.Mini_Book_12x18_Body))
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Mini_Gift_Book, New DirectoryInfo(.dirMiniGiftBkCov), BodyVsCover.Cover, 4, PrinterCategory.Book_Cover))
            'ProductionFolders.Add(New ProductType(cMiniGiftBook, .dirMiniGiftBkBod, .dirMiniGiftBkCov, cMiniGiftBookBod, cMiniGiftBookCov, 8, 4))       'mini gift book (landscape)

            'Mini Economy Book
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Mini_Economy_Book, New DirectoryInfo(.dirMiniEconBkBod), BodyVsCover.Body, 8, PrinterCategory.Mini_Book_12x18_Body))
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Mini_Economy_Book, New DirectoryInfo(.dirMiniEconBkCov), BodyVsCover.Cover, 4, PrinterCategory.Book_Cover))
            'ProductionFolders.Add(New ProductType(cMiniEconBk, .dirMiniEconBkBod, .dirMiniEconBkCov, cMiniEconBkBod, cMiniEconBkCov, 8, 4))       'mini economy book (portrait)

            '12x9 book
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Book_12x9, New DirectoryInfo(.dirBkBod12x9), BodyVsCover.Body, 4, PrinterCategory.Book_12x9_Body))
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Book_12x9, New DirectoryInfo(.dirBkCov12x9), BodyVsCover.Cover, 1, PrinterCategory.Book_12x9_Cover))
            'ProductionFolders.Add(New ProductType(cBook12x9, .dirBkBod12x9, .dirBkCov12x9, cbkBod12x9, cbkCov12x9, 4, 1))           '4 up, 2 cut books on 12x18

            'shareword
            ProductionFolders.Add(New JQProductionDirInfo(ProductCategory.Share_Word, New DirectoryInfo(.dirShareWordProduction), BodyVsCover.Body, 1, PrinterCategory.ShareWord_Printer))
            'ProductionFolders.Add(New ProductType(cShareWord, .dirShareWordProduction, "", cShareWord, "", 1, 1))                     'share word


        End With
        PopulateProductionFiles()
        PopulateAlignmentFiles()
    End Sub

#End Region


#Region "Methods"
    ''' <summary>
    ''' finds the files in each production folder and adds them to the ProductionFolders list
    ''' </summary>
    Public Sub PopulateProductionFiles()
        ProductionFiles.Clear()
        For Each myFolder As JQProductionDirInfo In ProductionFolders
            For Each myFile As FileInfo In myFolder.ProductionDirectory.GetFiles("*.pdf", SearchOption.TopDirectoryOnly)
                ProductionFiles.Add(New JQProductionFileInfo(myFolder, New Utilities.CMS_FileName(myFile)))
            Next
        Next
    End Sub

    Public Sub PopulateAlignmentFiles()
        AlignmentFiles.Clear()
        Dim align As New JQProductionDirInfo(ProductCategory.Alignment_Sheet, New DirectoryInfo(My.Settings.dirAlignmentSheet), BodyVsCover.Body, 1, PrinterCategory.Alignment_Sheets)
        For Each myFile As FileInfo In align.ProductionDirectory.GetFiles("*.pdf", SearchOption.TopDirectoryOnly)
            AlignmentFiles.Add(New JQProductionFileInfo(align, New Utilities.CMS_FileName(myFile)))

        Next
    End Sub



    ''' <summary>
    ''' returns a list of production files that match the product number
    ''' </summary>
    ''' <param name="ProductNumber"></param>
    ''' <returns></returns>
    Public Function GetProductionFiles(ByVal ProductNumber As Integer) As List(Of JQProductionFileInfo)
        Dim FoundFiles As New List(Of JQProductionFileInfo)
        Try
            If ProductNumber <> cNullInt And ProductNumber <> 0 Then
                For Each productionFile As JQProductionFileInfo In ProductionFiles
                    If IsNumeric(productionFile.ProductionFile.ID) Then
                        If ProductNumber = productionFile.ProductionFile.ID Then
                            FoundFiles.Add(productionFile)
                        End If
                    End If

                Next
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Get Production Files Error")
        End Try
        Return FoundFiles
    End Function


    Public Function getCMSPicPath(ByVal aProdNum As String, ByVal dirToSearch As String) As String
        Dim picPath As String = ""
        Try
            Dim allowedExt As List(Of String) = {".jpg", ".png", ".gif", ".bmp"}.ToList

            Dim prodImgDir As String = Path.Combine(dirToSearch, getSubFolder(aProdNum), aProdNum)
            If Directory.Exists(prodImgDir) Then
                Dim cmsFiles As New List(Of Utilities.CMS_FileName)

                Dim sizes() As String = {"Lrg", "Med", "Sml", "Nsd"}

                Dim dir As New DirectoryInfo(prodImgDir)
                For Each fiInfo As FileInfo In dir.GetFiles.ToList
                    Dim cms As New Utilities.CMS_FileName(fiInfo)
                    If cms.IsValidFileName AndAlso cms.Category = Utilities.cImage Then
                        cmsFiles.Add(cms)
                    End If

                Next

                Dim mostRecentYear As Integer = 0
                For Each cms As Utilities.CMS_FileName In cmsFiles
                    If IsNumeric(cms.Year) Then
                        Dim cmsYear As Integer = cms.Year
                        If cmsYear > mostRecentYear Then mostRecentYear = cmsYear 'if it's a product that has a year, it will find the newest year.
                    End If
                Next
                Dim yr As String = ""
                If mostRecentYear > 0 Then yr = mostRecentYear.ToString
                For i = cmsFiles.Count - 1 To 0 Step -1
                    'goes backward through the list and removes the files that are the incorrect year.
                    Dim cms = cmsFiles(i)
                    If cms.Year <> yr Then
                        cmsFiles.RemoveAt(i)
                    End If
                Next

                For Each curSize As String In sizes
                    If picPath = "" Then
                        For Each cmsFile As Utilities.CMS_FileName In cmsFiles
                            If picPath = "" Then
                                If cmsFile.Size = curSize AndAlso allowedExt.Contains(cmsFile.Extension) Then
                                    picPath = cmsFile.OriginalFile.FullName
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next





            End If
        Catch ex As Exception

        End Try
        Return picPath
    End Function

    ''' <summary>
    ''' uses the id to create the subfolder string
    ''' </summary>
    Private Function getSubFolder(ByVal id As String) As String

        Dim newSubFolder As String = ""
        Select Case id.Length
            Case 3
                newSubFolder = "0"
            Case 4
                newSubFolder = id.Substring(0, 1)
            Case 5
                newSubFolder = id.Substring(0, 2)
            Case 6
                newSubFolder = id.Substring(0, 3)
            Case 7
                newSubFolder = id.Substring(0, 4)
            Case Else

        End Select
        Return newSubFolder


    End Function

    ''' <summary>
    ''' Looks for the specified product number (aProdNum) in the specified folder (DirToSearch)
    ''' </summary>
    ''' <param name="job"></param>
    ''' <param name="DirToSearch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPicPath(ByVal job As JQRowInfo, ByVal DirToSearch As String) As String



        Dim PicPath As String = ""
        Try
            If job.ItemNumberIsValid Then
                'adds a \ to the end of the folder string if there's not one there...
                If DirToSearch.Substring(DirToSearch.Length - 1) <> "\" Then
                    DirToSearch &= "\"
                End If

                Dim aProdNum As String = job.ItemNumber.ToString
                Dim years As List(Of Integer) = {Date.Today.Year, Date.Today.AddYears(1).Year}.ToList

                ' used to store the first number(s) of the product number stored
                Dim picDigit As String = ""

                Select Case job.ItemNumber.ToString.Length

                    Case 4
                        'The first character from input
                        picDigit = aProdNum.Substring(0, 1)
                    Case 5
                        'The first two characters from input
                        picDigit = aProdNum.Substring(0, 2)
                    Case 6
                        picDigit = aProdNum.Substring(0, 3)

                    Case Else
                        picDigit = aProdNum.Substring(0, 1)

                End Select

                Dim ProductImageFolder As String = DirToSearch & picDigit & "\" & aProdNum
                'MsgBox(ProductImageFolder)

                'PicPath = PicNotFoundPath

                If Directory.Exists(ProductImageFolder) Then
                    Dim dirinfo As New DirectoryInfo(ProductImageFolder)
                    Dim fileList As New List(Of FileInfo)
                    fileList.AddRange(dirinfo.GetFiles)


                    'the file "extensions" used by order of preference
                    Dim PicSizes() As String = {"-Large.png", "-Medium.png", "-Small.png", ".png", ".jpg"}
                    Dim FoundItems As List(Of String) = New System.Collections.Generic.List(Of String)
                    Dim foundFile As Boolean = False
                    For Each picsize As String In PicSizes
                        'goes through each extension by order of preference
                        If Not foundFile Then
                            Dim TempPicPath As String = ProductImageFolder & "\" & aProdNum & picsize
                            For Each myFile As FileInfo In fileList
                                If Not foundFile Then
                                    'if myfile contains that extension
                                    If myFile.FullName = TempPicPath Then
                                        'PicPath = TempPicPath
                                        If Not FoundItems.Contains(TempPicPath) Then
                                            FoundItems.Add(TempPicPath)
                                        End If
                                        Exit For
                                        foundFile = True
                                    End If
                                End If

                            Next
                        End If


                    Next

                    ' Get a reference to each folder in that directory.




                    'this searches for the picture by next year, then this year. The Product Number, Cover, Back. If it can't find anything it tries to find by just the year & product number
                    For Each myYear As Integer In years
                        For Each fri In fileList
                            If fri.Name.Contains(myYear) Then
                                If fri.Name.Contains(aProdNum) Then
                                    'adds things found according to year & product number
                                    FoundItems.Add(fri.FullName)
                                    If fri.Name.Contains("Cover") Then
                                        If Not fri.Name.Contains("Back") Then
                                            For Each picSize As String In PicSizes
                                                If fri.Name.Contains(picSize) Then
                                                    PicPath = fri.FullName
                                                    GoTo end_of_for
                                                End If
                                            Next
                                        End If
                                    End If
                                    FoundItems.Add(fri.FullName)
                                End If
                            End If
                        Next



                    Next
                    'If there was a image found in the for loop that matched the year & product Number, it's added to the FoundItems list & gone through here

                    For Each item As String In FoundItems
                        For Each picSize As String In PicSizes
                            If item.Contains(picSize) Then
                                PicPath = item
                                GoTo end_of_for
                                'I know that goto is terrible... but that was the easiest way that I could think of to exit the for loop...
                            End If
                        Next
                    Next
end_of_for:

                    'If PicPath = "" Then




                    'End If
                Else
                    PicPath = ""
                End If

            End If

        Catch ex As Exception
            PicPath = ""
        End Try
        Return PicPath
    End Function
#End Region


End Class
