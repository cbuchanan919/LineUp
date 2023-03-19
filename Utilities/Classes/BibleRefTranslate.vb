Imports System
Imports System.IO
Imports System.Xml

''' <summary>
''' Reads and provides translation for some of the different languages
''' </summary>
''' <remarks></remarks>
Public Class BibleRefTranslate


#Region "Properties & Fields"

    ''' <summary>
    ''' Stores the names of the books of the bible in a table with different languages
    ''' </summary>
    ''' <remarks></remarks>
    Public Property BookDT As DataTable = Nothing

    ''' <summary>
    ''' Contains the letter replacement for some accented letters
    ''' </summary>
    ''' <remarks></remarks>
    Private Property LetterReplaceDict As New Dictionary(Of String, String)

    Public Property ResourceDir As String = ""

    Public Enum BibleVersion
        KJV
        Darby
    End Enum

    ''' <summary>
    ''' Currently loaded Bible
    ''' </summary>
    ''' <remarks></remarks>
    Public Property BibleDict() As Dictionary(Of String, Reference) = Nothing

    ''' <summary>
    ''' KJV Bible
    ''' </summary>
    ''' <remarks></remarks>
    Private Property KJVDict() As Dictionary(Of String, Reference) = Nothing

    ''' <summary>
    ''' Darby Bible
    ''' </summary>
    ''' <remarks></remarks>
    Private Property DarbyDict() As Dictionary(Of String, Reference) = Nothing


    Private _currentBibleVersion As bibleVersion
    Public Property currentBibleVersion() As bibleVersion
        Get
            Return _currentBibleVersion
        End Get
        Set(ByVal value As bibleVersion)
            Select Case value 'sets the version of the bible dict.
                Case bibleVersion.darby
                    bibleDict = darbyDict
                Case bibleVersion.kjv
                    bibleDict = kjvDict
            End Select
            _currentBibleVersion = value
        End Set
    End Property


#End Region


#Region "Init"

    Public Sub New(ByVal aResourceDirectory As String, ByVal currentVersion As bibleVersion)
        ResourceDir = aResourceDirectory
        If IsNothing(BookDT) Then
            BookDT = CreateBibleBookNameTable()
        End If
        LetterReplaceDict = populateLetterReplaceDict()

        'reads bibles to dict.
        darbyDict = ReadBible(bibleVersion.darby)
        kjvDict = ReadBible(bibleVersion.kjv)
        currentBibleVersion = currentVersion

    End Sub
#End Region


#Region "Methods"

    Public Function ReturnReferenceAndVerseText(ByVal Refs As List(Of Reference)) As String
        Dim Result As New Text.StringBuilder

        Try
            If Refs.Count = 0 Then
                Result.Append("No Matches Found...")
            Else
                For Each myRef As Reference In Refs
                    Result.Append(ReturnBook(myRef.Book, English) & " " & myRef.Chapter & ":" & myRef.Verse & vbCrLf)
                    Result.Append(ReturnVerseByReference(myRef) & vbCrLf & vbCrLf)
                Next
            End If

        Catch ex As Exception
            Result.Clear()
        End Try

        Return Result.ToString
    End Function


    ''' <summary>
    ''' Cleans the string of certain punctuation –, :.; etc.
    ''' </summary>
    ''' <param name="strToSearch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PrepStringForReferenceSearch(ByVal strToSearch As String) As String
        strToSearch = strToSearch.Trim
        strToSearch = strToSearch.Replace("‑", "-")
        strToSearch = strToSearch.Replace("–", "-")
        strToSearch = strToSearch.Replace(",", "_")
        strToSearch = strToSearch.Replace(" ", "_")
        strToSearch = strToSearch.Replace(":", "_")
        strToSearch = strToSearch.Replace(".", "")
        strToSearch = strToSearch.Replace(";", "__")
        strToSearch = strToSearch.Replace("ª", "")
        strToSearch = strToSearch.Replace(" Ew. ", " ") 'Polish language
        Return strToSearch
    End Function

    ''' <summary>
    ''' Re-Populates the Letter Replace Dictionary
    ''' </summary>
    ''' <remarks></remarks>
    Public Function populateLetterReplaceDict() As Dictionary(Of String, String)
        Dim LetterReplace As New Dictionary(Of String, String)
        With LetterReplace
            .Clear()
            .Add("á", "a")
            .Add("ă", "a")
            .Add("ã", "a")
            .Add("Á", "A")


            .Add("ç", "c")

            .Add("ë", "e")
            .Add("è", "e")
            .Add("ê", "e")
            .Add("é", "e")
            .Add("Ê", "E")
            .Add("É", "E")


            .Add("ï", "i")
            .Add("í", "i")

            .Add("ó", "o")
            .Add("ô", "o")
            .Add("õ", "o")


            .Add("ú", "u")
        End With
        Return LetterReplace
    End Function





    Public Function FindReferencesFromFileName(ByVal InputString As String, ByVal CurrentLanguage As String) As List(Of Reference)
        Dim foundReferences As New List(Of Reference)

        If InputString <> "" Then
            Dim Removes As New List(Of String)
            Removes.Add(CurrentLanguage & "-")

            Removes.Add("Gospel-")
            Removes.Add("Believers-")
            Removes.Add("-Artistic-")
            Removes.Add("Artistic-")
            Removes.Add("-Artistic")
            Removes.Add("-NKJV-")
            Removes.Add("NKJV-")
            Removes.Add("-NKJV")
            Removes.Add(".indd")
            Removes.Add(".png")

            For Yr As Integer = Date.Now.Year + 2 To Date.Now.Year - 10 Step -1
                'adds to the removes list potential years around current year
                Dim YrStr As String = Yr & "-"
                Removes.Add(YrStr)
            Next

            For Each remove As String In Removes
                If InputString.Contains(remove) Then
                    InputString = InputString.Replace(remove, "")
                End If
            Next

            Dim sol As New List(Of String)
            With sol
                .Add("Song_of_Solomon")
                .Add("Song_of_Songs")
                .Add("Canticle_of_Canticles")
                .Add("Song_of_Sol")
                .Add("Song_of_Sg")
                '.Add("SOS")
            End With
            For Each abbreviation As String In sol
                'song of solomon is problematic. I'm here trying to replace any of the abbreviations with a single "Song" - In order to make it work betterer
                If InputString.Contains(abbreviation) Then
                    InputString = InputString.Replace(abbreviation, "Song")
                End If
            Next


            If IsNumeric(InputString.Substring(0, 2)) Then
                Try
                    'thinks that it's a month number...
                    InputString = InputString.Substring(2, InputString.Length - 2)
                Catch ex As Exception
                End Try
            End If

            InputString = InputString.Trim("-")

            If InputString.Contains("(") Then
                'removes the (2), (3) etc, from the file name
                InputString = System.Text.RegularExpressions.Regex.Replace(InputString, "\(\d\)", "")
            End If

            'VerseRef = VerseRef.Replace("_", "$")
            If InputString.Contains("__") Then
                InputString = InputString.Replace("__", "$")
            End If
            '  MsgBox(InputString)
            For Each Reference As String In InputString.Split("$")
                Try


                    Dim Book As String = ""
                    Dim Chapter As Integer = 0
                    Dim Verse As Integer = 0
                    Dim Part() As String = Reference.Split("_")
                    Dim i As Integer = -1

                    Dim IsNumberedBook As Boolean = False

                    'finds the book name
                    Dim originalBookString As String = ""
                    Dim MatchedBook As String = ""
                    Dim int As Integer = -1
                    For eachPart As Integer = 0 To Part.Count - 1
                        If MatchedBook = "" Then
                            Dim tryMatch As String = ReturnBook(Part(eachPart), English)
                            If tryMatch = "" Then
                                tryMatch = Part(eachPart)
                                For nextPart As Integer = eachPart + 1 To Part.Count - 1 Step +1
                                    If MatchedBook = "" Then
                                        tryMatch &= " " & Part(nextPart)
                                        Dim tryMatch2 As String = ReturnBook(tryMatch, English)
                                        If tryMatch2 <> "" Then
                                            originalBookString = tryMatch.Replace(" ", "_")
                                            MatchedBook = tryMatch2
                                            int = nextPart
                                        End If
                                    End If

                                Next
                            Else
                                originalBookString = Part(eachPart).Replace(" ", "_")
                                MatchedBook = tryMatch
                                int = eachPart
                            End If
                        End If

                    Next
                    If MatchedBook <> "" Then
                        'MsgBox("Found Book:" & MatchedBook & vbCrLf & "Original Bk:" & originalBookString & vbCrLf & Reference)
                        Book = MatchedBook
                        Part = Nothing
                        Reference = Reference.Replace(originalBookString, "")
                        Reference = Reference.Trim
                        Reference = Reference.Trim("_")
                        Dim Parts() As String = Reference.Split("_")

                        'old code to find the book name
                        'If IsNumeric(Part(0)) Then
                        '    ' is a numbered book
                        '    'txtEnglishVerse.Text &= "Numbered Book "
                        '    Book = Part(0) & " " & Part(1)
                        '    Dim BkName As String = ReturnBook(Book, English)
                        '    If BkName <> "" Then
                        '        MsgBox(BkName)
                        '    End If

                        '    IsNumberedBook = True
                        'Else

                        '    ReturnBook(Part(0), English)

                        '    Book = Part(0)
                        'End If



                        'finds the chapter number

                        'If IsNumberedBook Then
                        '    i = 2
                        'Else
                        '    i = 1
                        'End If

                        i = 0
                        If IsNumeric(Parts(i)) Then
                            Chapter = Parts(i)
                        Else
                            'MsgBox(Parts(i))
                            Chapter = 0
                        End If

                        'finds the verse number
                        i += 1

                        For x As Integer = i To Parts.Count - 1
                            If Parts(x).Contains("-") Then
                                'is a verse range
                                Dim verseParts() As String = Parts(x).Split("-")
                                For vsInt As Integer = verseParts(0) To verseParts(1) Step 1
                                    Dim vs As New Reference(Book, ReturnBook(Book, Abbreviation), Chapter, vsInt)
                                    foundReferences.Add(vs)
                                Next
                                'For Each foundVerse In verseParts
                                '    Dim vs As New Reference(Book, ReturnBook(Book, Abbreviation), Chapter, foundVerse)
                                '    foundReferences.Add(vs)
                                'Next

                            Else
                                Verse = Parts(x)
                                Dim vs As New Reference(Book, ReturnBook(Book, Abbreviation), Chapter, Verse)
                                foundReferences.Add(vs)
                            End If
                        Next
                    End If


                Catch ex As Exception
                    '  MsgBox(ex.Message)
                End Try


            Next
        End If
        Return foundReferences
    End Function


    ''' <summary>
    ''' goes through the books of the bible table and returns the requested language (or "Abbreviation")
    ''' </summary>
    ''' <param name="BibleBook"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnBook(ByVal BibleBook As String, ByVal ConvertToLanguage As String) As String
        Dim NoAcc As String = RemoveAccent(BibleBook)
        Dim Abbrev As String = ""

        If BibleBook <> "" Then
            If IsNothing(BookDT) Then
                BookDT = CreateBibleBookNameTable()
            End If

            Dim Found As Boolean = False
            For Each myRow As DataRow In BookDT.Rows
                If Found = False Then
                    For Each myCol As DataColumn In BookDT.Columns
                        If Not Found Then
                            If myRow(myCol).ToString.ToLower = BibleBook.ToLower Or myRow(myCol).ToString.ToLower = NoAcc Then 'RemoveAccent(myRow(myCol).ToString)
                                Found = True
                                Abbrev = myRow(ConvertToLanguage).ToString
                            End If
                        End If
                    Next
                End If
            Next
            'Beep()
        End If

        'MsgBox(BibleBook & vbCrLf & Abbrev)
        Return Abbrev
    End Function


    ''' <summary>
    ''' Creates a books of the bible table with different languages
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CreateBibleBookNameTable() As DataTable
        Dim BooksDT As DataTable = New DataTable
        Try
            Dim sr As New StreamReader(Path.Combine(ResourceDir, "Bible Book Names Per Language.csv"), System.Text.Encoding.UTF8)
            Dim line As String = sr.ReadLine
            Dim columnsAdded As Boolean = False
            Do While line <> Nothing
                If line <> "" Then
                    Dim LineParts() As String = line.Split(",")
                    'MsgBox(line)

                    If columnsAdded = False Then
                        'adds the column name based upon the first line (should be languages)
                        For Each part In LineParts
                            BooksDT.Columns.Add(part)
                        Next
                        columnsAdded = True
                    Else
                        'adds the info to the data table
                        Dim DR As DataRow = BooksDT.NewRow
                        For i As Integer = 0 To LineParts.Count - 1
                            DR(i) = LineParts(i)
                        Next
                        BooksDT.Rows.Add(DR)
                    End If

                End If

                line = sr.ReadLine
            Loop
            sr.Close()
        Catch ex As Exception

        End Try

        Return BooksDT
    End Function


    ''' <summary>
    ''' Returns the the accent free string
    ''' </summary>
    ''' <param name="inputString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RemoveAccent(ByVal inputString As String) As String
        Dim NoAccentStr As String = inputString.ToLower
        Try
            If LetterReplaceDict.Keys.Count < 4 Then
                'PopulateForm()
            Else
                For Each myLetter In LetterReplaceDict.Keys
                    If NoAccentStr.Contains(myLetter) Then
                        NoAccentStr = NoAccentStr.Replace(myLetter, LetterReplaceDict(myLetter))
                    End If
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Return NoAccentStr
    End Function


    ''' <summary>
    ''' Returns the text of the verse from the specified reference.
    ''' </summary>
    ''' <param name="aRef"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnVerseByReference(ByVal aRef As Reference) As String
        Dim VerseText As String = ""
        Try
            If IsNothing(BookDT) Then
                BookDT = CreateBibleBookNameTable()
            End If
            If bibleDict.ContainsKey(aRef.ToString) Then
                VerseText = bibleDict(aRef.ToString).Text
            Else
                VerseText = "Verse not found..."
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return VerseText
    End Function

    ''' <summary>
    ''' Reads through specified xml Doc to create a dictionary of verses. ie. Gen.1.1
    ''' </summary>
    ''' <param name="bibleVersion">Options are: "KJV", or "Darby"</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadBible(ByVal bibleVersion As bibleVersion) As Dictionary(Of String, Reference)

        Dim StatusOK As Boolean = True
        Dim aBibleDict As New Dictionary(Of String, Reference)
        Dim myVersion As String = ""
        If bibleVersion = bibleVersion.darby Then
            myVersion = "Darby"
        Else
            myVersion = "KJV"
        End If
        Try

            Dim BiblePath As String = Path.Combine(ResourceDir, myVersion & ".xml") '"kjv.xml" '
            Dim TagName As String = ""
            Dim TitleStr As String = ""
            Dim ValueStr As String = ""
            Dim i As Integer = 0
            If File.Exists(BiblePath) Then
                Dim BibleXmlDoc As New XmlDocument
                BibleXmlDoc.Load(BiblePath)
                Dim BibleNodeList As XmlNodeList = BibleXmlDoc.ChildNodes
                For Each myBible As XmlNode In BibleNodeList

                    For Each book As XmlNode In myBible.ChildNodes
                        Try
                            Dim bookName As String = ReturnBook(book.Attributes("num").Value.ToString, English)
                            Dim bookAbbr As String = ReturnBook(bookName, Abbreviation)

                            'bookName = Form1.genUtil.ReturnBook(bookName, "Abbreviation")
                            ' MsgBox(bookName)
                            For Each chapter As XmlNode In book.ChildNodes
                                Dim ChapterNumber As Integer = CInt(chapter.Attributes("num").Value.ToString)

                                For Each verse As XmlNode In chapter.ChildNodes
                                    Dim Key As String = ""
                                    Try
                                        Dim VerseNumber As Integer = CInt(verse.Attributes("num").Value.ToString)

                                        Dim Ref As New Reference(bookName, bookAbbr, ChapterNumber, VerseNumber, verse.InnerText)
                                        Key = bookAbbr & "." & ChapterNumber & "." & VerseNumber
                                        aBibleDict.Add(Key, Ref)
                                        i += 1
                                    Catch ex As Exception
                                        MsgBox(ex.Message)
                                    End Try


                                Next
                            Next
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    Next
                Next

            End If
        Catch ex As Exception
            StatusOK = False
            MsgBox(ex.Message)
        End Try


        Return aBibleDict
    End Function



#End Region


End Class

''' <summary>
''' Contains information for 1 bible reference. ie. John 11:35 - Jesus wept.
''' </summary>
Public Class Reference


#Region "Properties"

    ''' <summary>
    ''' The name of the book
    ''' </summary>
    Public Property Book As String = ""

    ''' <summary>
    ''' Abbreviation of the book
    ''' </summary>
    Public Property BookAbbreviation As String = ""

    ''' <summary>
    ''' The Chapter Number
    ''' </summary>
    Public Property Chapter As Integer = 0

    ''' <summary>
    ''' The Verse Number
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Verse As Integer = 0

    ''' <summary>
    ''' Text of the Verse
    ''' </summary>
    Public Property Text As String = ""

#End Region


#Region "Init"

    ''' <summary>
    ''' Creates an empty Reference class
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Creates a filled out Reference class
    ''' </summary>
    ''' <param name="Book">Book's Name</param>
    ''' <param name="BookAbbreviation">Book's Abbreviation</param>
    ''' <param name="Chapter">Chapter Number</param>
    ''' <param name="Verse">Verse Number</param>
    ''' <param name="Text">Verse's Text</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Book As String, ByVal BookAbbreviation As String, ByVal Chapter As Integer, ByVal Verse As Integer, Optional ByVal Text As String = "")
        Me.Book = Book
        Me.BookAbbreviation = BookAbbreviation
        Me.Chapter = Chapter
        Me.Verse = Verse
        Me.Text = Text
    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Returns the Reference
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Dim key As String = BookAbbreviation & "." & Chapter & "." & Verse
        Return key
    End Function

#End Region


End Class
