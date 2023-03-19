Imports System.IO
Imports System.Text.RegularExpressions ' Used to find the periodical folders


''' <summary>
''' Contains information for 1 UV product.
''' </summary>
Public Class UvProductInfo


#Region "Properties"

    Public Property ItemNum As Integer = cNullInt

    Public Property Title As String = ""

    ''' <summary>
    ''' returns the text to use if creating a foldername ie. 'Title - 1234'
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property folderTitle() As String
        Get
            Dim name As String = InvTitle
            If SubTitle <> "" Then
                name = name.Replace(SubTitle, "") 'removes subtitle - keeps title shorter
            End If
            name = Text.RegularExpressions.Regex.Replace(name, "[\\/:*?""<>|\r\n]", "", System.Text.RegularExpressions.RegexOptions.Singleline) 'removes invalid folder characters from title
            name = name.Replace("-", "_") ' replaced - with _ so that there is only the 1 hyphen between the item number and title
            Return name.Trim & " - " & ItemNum
        End Get
    End Property

    Private _strInvTitle As String = ""
    Public Property InvTitle As String
        Get
            If _strInvTitle = "" Then
                Return Title
            End If
            Dim newInverted As String = ""
            If _strInvTitle <> "" AndAlso Title <> "" Then
                If Title.Contains(_strInvTitle) Then
                    If Title.IndexOf(_strInvTitle) > 0 Then
                        'inverted title - ie. Advocate or the Accuser, The

                        'Advocate is the inverted word.
                        Dim indexOfInvert As Integer = Title.IndexOf(_strInvTitle) 'gets the place in the string where the inverted word occurs
                        'gets from the inverted word to the end of the string
                        newInverted = Title.Substring(indexOfInvert, Title.Length - indexOfInvert)

                        'next section tries to get the remainder of the title.
                        'gets the beginning of the title to the beginning of the invert.
                        newInverted &= ", " & Title.Substring(0, indexOfInvert - 1)
                        Return newInverted
                    End If
                Else
                    Return _strInvTitle & ", " & Title 'the idea is that if there's an inverted word that is just random, it's will include the title

                End If

            End If

            Return Me._strInvTitle
        End Get
        Set(value As String)
            _strInvTitle = value
        End Set
    End Property


    Public Property SubTitle() As String = ""

    Public Property SalePrice As Decimal = cNullInt


    Public Property PageCt As Integer = cNullInt


    Public Property Author As String = ""

    Public Property Type As String = ""

    Public Property Source As String = ""


    Public Property WebText As String = ""


    Public Property CatalogText As String = ""


    Public Property TypeUV As String = ""

    ''' <summary>
    ''' converts the uv 2 letter code to the full length string it represents
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property convertTypeUV As String
        Get
            Dim type As String = TypeUV
            Select Case type
                Case "AD"
                    type = "Advertising"
                Case "BB"
                    type = "Spanish - Book about the Bible"
                Case "BC"
                    type = "Bible Case"
                Case "BI"
                    type = "Bible"
                Case "CA"
                    type = "Calendar"
                Case "CM"
                    type = "Chart / Map"
                Case "GR"
                    type = "General Reading"
                Case "HB"
                    type = "Hymn Book"
                Case "MB"
                    type = "Ministry Book"
                Case "PA"
                    type = "Pamphlet"
                Case "PC"
                    type = "Electronic product"
                Case "PE"
                    type = "Periodical"
                Case "RF"
                    type = "Reference"
                Case "SS"
                    type = "Sunday School"
                Case "ST"
                    type = "Stationary"
                Case "SU"
                    type = "Supply"
                Case "TA"
                    type = "Audio"
                Case "TE"
                    type = "Text"
                Case "TR"
                    type = "Tract"
                Case "WE"
                    type = "Web"
            End Select
            Return type
        End Get

    End Property

    Public Property Language As String = ""
    Public Property SubType As String = ""
    Public Property PricePer As Decimal = cNullInt
    Public Property CoverPath As String = ""
    Public Property BodyPath As String = ""
    Public Property Year() As String = ""
    Public Property PageSize() As String = ""

    Public Property SupItem() As String = ""

    Public Property SupID() As String = ""

    ''' <summary>
    ''' If the class has been successfully matched from a database
    ''' </summary>
    ''' <returns></returns>
    Public Property MatchedOK() As Boolean = False

    ''' <summary>
    ''' this is a temp property, used while getting items ordered for the last 5 years
    ''' </summary>
    ''' <returns></returns>
    Public Property TempOrdered As Integer = 0

    ''' <summary>
    ''' populated by the FindMatches function. List of project folders that were found.
    ''' </summary>
    ''' <returns></returns>
    Public Property FoundProjectFolders As New List(Of DirectoryInfo)

    ''' <summary>
    ''' used to see how many final cover files were found in the cover repopulation
    ''' </summary>
    ''' <returns></returns>
    Public Property FoundCoverFiles As New List(Of FileInfo)

    ''' <summary>
    ''' Used to keep track of the found cover status...
    ''' </summary>
    ''' <returns></returns>
    Public Property FoundCoverStatus As String = ""

    'Public Property ProductionFiles As New List(Of JQProductionFileInfo)

    'Public Function GetCoversOrBodies(ByVal WhatToGet As BodyVsCover) As List(Of JQProductionFileInfo)
    '    Dim files As New List(Of JQProductionFileInfo)
    '    For Each myFile As JQProductionFileInfo In ProductionFiles
    '        If myFile.ProductBodyOrCover = WhatToGet Then
    '            files.Add(myFile)
    '        End If
    '    Next
    '    If files.Count = 0 Then
    '        Return Nothing
    '    Else
    '        Return files
    '    End If
    'End Function


#End Region


#Region "Init"

    ''' <summary>
    ''' Creates a new empty instance of UvProductInfo
    ''' </summary>
    ''' <param name="matched">whether or not the instance was matched from / found in a database</param>
    Public Sub New(ByVal matched As Boolean)
        MatchedOK = matched

    End Sub
    ''' <summary>
    ''' Stores the info for 1 product.
    ''' </summary>
    ''' <param name="ItemNum">Item number</param> 
    ''' <param name="Title">Product Title</param>
    ''' <param name="InvTitle">Inverted title</param> 
    ''' <param name="SalePrice"></param>
    ''' <param name="PageCt">Page Count</param>
    ''' <param name="Author">Author</param>
    ''' <param name="Type"></param>
    ''' <param name="Source">Product Source</param>
    ''' <param name="WebText">Web Text</param>
    ''' <param name="CatalogText">Catalog Text</param>
    ''' <param name="TypeUV"></param>
    ''' <param name="Language">Product Language</param>
    ''' <param name="SubType"></param>
    ''' <param name="PricePer"></param>
    ''' <param name="MatchedOK">Successfully Matched</param>
    ''' <param name="PageSize">Product Page Size</param>
    ''' <param name="SupItem">Supplier Number</param>
    ''' <param name="SupID">Supplier ID</param>
    Public Sub New(ByVal ItemNum As String,
                   ByVal Title As String,
                   ByVal InvTitle As String,
                   ByVal SalePrice As String,
                   ByVal PageCt As String,
                   ByVal Author As String,
                   ByVal Type As String,
                   ByVal Source As String,
                   ByVal WebText As String,
                   ByVal CatalogText As String,
                   ByVal TypeUV As String,
                   ByVal Language As String,
                   ByVal SubType As String,
                   ByVal PricePer As String,
                   ByVal MatchedOK As Boolean,
                   Optional ByVal PageSize As String = "",
                   Optional ByVal SupItem As String = "",
                   Optional ByVal SupID As String = "")

        Me.ItemNum = ItemNum
        Me.Title = Title
        Me.InvTitle = InvTitle
        Me.SalePrice = SalePrice
        Me.PageCt = PageCt
        Me.Author = Author
        Me.Type = Type
        Me.Source = Source
        Me.WebText = WebText
        Me.CatalogText = CatalogText
        Me.TypeUV = TypeUV
        Me.Language = Language
        Me.SubType = SubType
        Me.PricePer = PricePer
        Me.MatchedOK = MatchedOK
        Me.PageSize = PageSize
        Me.SupItem = SupItem
        Me.SupID = SupID


    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Run only after finished getting data from uv. 
    ''' Makes title more complete by adding year, language and subtitle if necessary
    ''' </summary>
    Public Sub ConfigTitle()
        If SubTitle <> "" Then
            Title &= ": " & SubTitle
        End If
        If Language <> "" Then
            Title = Language & " " & Title
        End If
        If Year() <> "" Then
            Dim startDt As Date = Date.Today.AddYears(-5)
            For i As Integer = 0 To 11
                'goes from 5 years ago to ~ 5 years in the future and tries to match the uv year to the year...
                Dim yr As String = startDt.Year.ToString
                If yr.Substring(3) = Year Or yr = Year Then
                    'uv records the last digit of the year...
                    Year = yr 'the year was a match!
                    If Language = "" Then
                        'the default for english is no language listed...
                        Title = Year & " English " & Title
                    Else
                        Title = Year & " " & Title
                    End If

                    Exit For
                End If
                startDt = startDt.AddYears(1)
            Next
        End If
    End Sub
    ''' <summary>
    ''' Clears the different strings / Properties
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearInfo()
        ItemNum = ""
        Title = ""
        InvTitle = ""
        SalePrice = ""
        PageCt = ""
        Author = ""
        Type = ""
        Source = ""
        WebText = ""
        CatalogText = ""
        TypeUV = ""
        Language = ""
        SubType = ""
        PricePer = ""
        Year = ""
        MatchedOK = False
        PageSize = ""
        SupItem = ""
        SupID = ""
    End Sub

    ''' <summary>
    ''' returns a copy of the uvProductInfo
    ''' </summary>
    ''' <returns></returns>
    Public Function clone() As UvProductInfo
        Dim newInfo As New UvProductInfo(MatchedOK)
        With newInfo
            .Author = Author
            .BodyPath = BodyPath
            .CatalogText = CatalogText
            .CoverPath = CoverPath
            .InvTitle = _strInvTitle
            .ItemNum = ItemNum
            .Language = Language
            .MatchedOK = MatchedOK
            .PageCt = PageCt
            .PageSize = PageSize
            .PricePer = PricePer
            .SalePrice = SalePrice
            .Source = Source
            .SubTitle = SubTitle
            .SubType = SubType
            .SupItem = SupItem
            .Title = Title
            .Type = Type
            .TypeUV = TypeUV
            .WebText = WebText
            .Year = Year
            .SupID = SupID
        End With



        Return newInfo
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("Item Num:".PadRight(20) & vbTab & ItemNum)
        sb.AppendLine("Title:".PadRight(20) & vbTab & Title)
        sb.AppendLine("Inverted Title:".PadRight(20) & vbTab & InvTitle)
        sb.AppendLine("Author:".PadRight(20) & vbTab & Author)
        sb.AppendLine("Body Path:".PadRight(20) & vbTab & BodyPath)
        sb.AppendLine("Catalog Text:".PadRight(20) & vbTab & CatalogText)
        sb.AppendLine("Cover Path:".PadRight(20) & vbTab & CoverPath)
        sb.AppendLine("Language:".PadRight(20) & vbTab & Language)
        sb.AppendLine("Matched OK:".PadRight(20) & vbTab & MatchedOK.ToString)
        sb.AppendLine("Page Count:".PadRight(20) & vbTab & PageCt)
        sb.AppendLine("Page Size:".PadRight(20) & vbTab & PageSize)
        sb.AppendLine("Price Per:".PadRight(20) & vbTab & PricePer)
        sb.AppendLine("Sale Price:".PadRight(20) & vbTab & SalePrice)
        sb.AppendLine("Source:".PadRight(20) & vbTab & Source)
        sb.AppendLine("Sub Type:".PadRight(20) & vbTab & SubType)
        sb.AppendLine("Subtitle:".PadRight(20) & vbTab & SubTitle)
        sb.AppendLine("Supplier Item:".PadRight(20) & vbTab & SupItem)
        sb.AppendLine("Type:".PadRight(20) & vbTab & Type)
        sb.AppendLine("UV Type:".PadRight(20) & vbTab & TypeUV)
        sb.AppendLine("Web Text:".PadRight(20) & vbTab & WebText)
        sb.AppendLine("Year:".PadRight(20) & vbTab & Year)
        sb.AppendLine("Supplier ID:".PadRight(20) & vbTab & SupID)


        Return sb.ToString
    End Function





#End Region


End Class
