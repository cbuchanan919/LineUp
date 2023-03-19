Imports System.IO
Imports System.Text.RegularExpressions ' Used to find the periodical folders

Public Class JQProjectDirIO

#Region "Properties"

    ''' <summary>
    ''' List of all of the folders found. (thoughout all categories)
    ''' </summary>
    ''' <returns></returns>
    Public Property ProjectDirectories As List(Of JQProjectDirInfo) = Nothing


#End Region

#Region "Init"

    Public Sub New()

        PopulateFolders()

    End Sub

#End Region

#Region "Methods"
    Public Function PopulateFolders() As List(Of JQProjectDirInfo)
        Dim success As Boolean = False
        Try

            ProjectDirectories = New List(Of JQProjectDirInfo)
            Dim projectDirToTypeDict As New Dictionary(Of String, ProductCategory)

            With projectDirToTypeDict
                .Add(My.Settings.PrEBookDir, ProductCategory.Book)
                .Add(My.Settings.PrEChartDir, ProductCategory.Chart)
                .Add(My.Settings.PrEHymnBookDir, ProductCategory.Hymn_Book)
                .Add(My.Settings.PrELeafletDir, ProductCategory.Leaflet)
                .Add(My.Settings.PrEPamphletDir, ProductCategory.Pamphlet)
                .Add(My.Settings.PrEPeriodicals, ProductCategory.Periodical)
            End With
            'For Each loc As String In My.Settings.PrESearchLoc
            '    If Not projectDirToTypeDict.ContainsKey Then
            'Next

            For Each loc As String In projectDirToTypeDict.Keys
                Dim dirInfo As New DirectoryInfo(loc)
                If dirInfo.Exists Then
                    For Each projFolder As DirectoryInfo In dirInfo.EnumerateDirectories()
                        ProjectDirectories.Add(New JQProjectDirInfo(projFolder, projectDirToTypeDict(loc)))

                    Next
                End If
            Next
            success = True
        Catch ex As Exception
            success = False
        End Try
        Return ProjectDirectories
    End Function


    Public Function GetDirectories(ByVal SearchStr As String, Optional ByVal maxCount As Integer = 0) As List(Of JQProjectDirInfo)
        Dim found As New List(Of JQProjectDirInfo)
        SearchStr = SearchStr.ToLower
        If Not IsNothing(ProjectDirectories) Then
            For Each projectDir As JQProjectDirInfo In ProjectDirectories
                If projectDir.ProjectDirectory.Name.ToLower.Contains(SearchStr) Then
                    found.Add(projectDir)
                End If
                If maxCount > 0 AndAlso found.Count >= maxCount Then
                    Return found
                End If
            Next
        End If
        Return found
    End Function


    ''' <summary>
    ''' Goes through the directories in my.settings.PrESearchLoc, and looks for the job. If search string specified, ignores job.
    ''' </summary>
    ''' <param name="searchStr">separate each search by a semicolon. if "" is given, searches for ItemNum; Title</param>
    ''' <returns></returns>
    Public Function GetDirectories(ByVal job As JQRowInfo, Optional ByVal searchStr As String = "") As List(Of JQProjectDirInfo)
        Dim matches As New List(Of JQProjectDirInfo)
        Try

            If searchStr = "" AndAlso (job.ItemNumber <> cNullInt Or job.Title <> "") Then
                Dim searches As New List(Of String)
                If job.ItemNumber <> cNullInt And job.ItemNumber > 0 Then searches.Add(job.ItemNumber)
                If job.Title.Trim <> "" Then searches.Add(job.Title.Trim)

                searchStr = String.Join(";", searches)
            End If

            'if there's text in the search stringthen...
            If searchStr.Length > 0 And Not IsNothing(ProjectDirectories) Then
                'searches the folder names (in the directories that are listed below) for a match 



                For Each dirinfo In ProjectDirectories

                    For Each search As String In searchStr.Split(";")
                        search = search.Trim
                        If search <> "" Then
                            If dirinfo.ProjectDirectory.Name.ToLower.Contains(search.ToLower) Then
                                'if it is, then it is added to an internal list
                                If Not matches.Contains(dirinfo) Then
                                    matches.Add(dirinfo)
                                End If
                                'OpenFolder.Add(fri.FullName)
                            End If
                        End If


                    Next
                Next

            End If

        Catch ex As Exception
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try

        Return matches
    End Function



    '''' <summary>
    '''' Goes through the directories in my.settings.PrESearchLoc, and looks for the search string.
    '''' </summary>
    '''' <param name="searchStr">separate each search by a semicolon. if "" is given, searches for ItemNum; Title</param>
    '''' <returns></returns>
    'Public Function FindProjectDirectories(Optional ByVal searchStr As String = "") As List(Of DirectoryInfo)
    '    Dim matches As New List(Of DirectoryInfo)
    '    Try
    '        If IsNothing(FoundProjectFolders) Then FoundProjectFolders = New List(Of DirectoryInfo)
    '        FoundProjectFolders.Clear()
    '        If searchStr = "" AndAlso (ItemNum <> cNullInt Or Title <> "") Then
    '            searchStr = ItemNum & "; " & Title
    '        End If

    '        'if there's text in the txtProdNumPrePrint text box then...
    '        If searchStr.Length > 0 Then
    '            'searches the folder names (in the directories that are listed below) for a match 


    '            Dim sourceFolder As New List(Of String)
    '            'searches the folders in the search location
    '            For Each Location As String In My.Settings.PrESearchLoc.Split(";")
    '                sourceFolder.Add(Location)
    '            Next

    '            'Dim AuthorLastName() = ProdInfo.Author.ToLower.Split(" ")


    '            Try
    '                'Dim OpenFolder As New List(Of String)
    '                ' Make a reference to a directory.
    '                For Each directory In sourceFolder
    '                    Dim dirinfo As New DirectoryInfo(directory)
    '                    ' Get a reference to each folder in that directory.
    '                    Dim FolderArray As DirectoryInfo() = dirinfo.GetDirectories()



    '                    'Dim mySearch() As String = txtImposeSearch.Text.Split(";")
    '                    For Each fri As DirectoryInfo In FolderArray
    '                        ' Display the names of the folders.
    '                        For Each search As String In searchStr.Split(";")
    '                            search = search.Trim
    '                            If Not search = "" Then
    '                                If fri.Name.ToLower.Contains(search.ToLower) Then
    '                                    'if it is, then it is added to an internal list
    '                                    If Not matches.Contains(fri) Then
    '                                        matches.Add(fri)
    '                                    End If
    '                                    'OpenFolder.Add(fri.FullName)
    '                                End If
    '                            End If


    '                        Next
    '                    Next



    '                Next

    '                'If OpenFolder.Count < 11 Then
    '                '    OpenFolder.Add("")
    '                '    OpenFolder.Add("Other items by this author - ")
    '                '    OpenFolder.Add("")
    '                '    For Each directory In sourceFolder
    '                '        Dim dirinfo As New DirectoryInfo(directory)
    '                '        ' Get a reference to each folder in that directory.
    '                '        Dim FolderArray As DirectoryInfo() = dirinfo.GetDirectories()
    '                '        ' Display the names of the folders.
    '                '        Dim fri As DirectoryInfo
    '                '        For Each fri In FolderArray

    '                '            If fri.Name.ToLower.Contains(AuthorLastName(AuthorLastName.Count - 1)) And OpenFolder.Count < 11 Then
    '                '                OpenFolder.Add(fri.FullName)
    '                '            End If

    '                '        Next

    '                '    Next
    '                'End If



    '                'originally it would automatically open a folder if it only found 1 folder
    '                'If OpenFolder.Count = 1 Then
    '                '    HighlightFileExplorer(OpenFolder(0).ToString, False)
    '                'End If

    '            Catch ex As Exception
    '                LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
    '                MsgBox(ex.Message.ToString & vbCrLf & "btnSearchPrntEbk_Click", MsgBoxStyle.OkCancel, "Something Failed... ")
    '            End Try


    '            'tries to find periodical info.
    '            For Each periodical As String In My.Settings.periodicals.Split(";")
    '                For Each SearchItem As String In searchStr.Split(";")
    '                    If SearchItem.ToLower.Contains(periodical.ToLower) Then


    '                        'tries to find the year of the periodical, keeps only 4 digits of numbers
    '                        Dim year = Regex.Match(SearchItem, "[0-9]{4}").Value
    '                        If year <> "" Then

    '                            Dim strFindIssue As String = SearchItem.Replace(year, "")

    '                            Dim Issue = Regex.Match(strFindIssue, "[0-9]{1,2}").Value

    '                            If Issue.ToString = String.Empty Then
    '                                Issue = FindMonth(strFindIssue)
    '                                'MsgBox(Issue.ToString)
    '                            End If

    '                            Try
    '                                Dim PeriodicalFolder As String = My.Settings.PrEPeriodicals & periodical & "\" & year & "\"


    '                                Dim dirinfo As New DirectoryInfo(PeriodicalFolder)
    '                                ' Get a reference to each folder in that directory.
    '                                Dim FolderArray As DirectoryInfo() = dirinfo.GetDirectories()
    '                                ' Display the names of the folders.


    '                                For Each fri As DirectoryInfo In FolderArray
    '                                    'checks to see if txtProdNumPrePrint is empty & checks to see if the folder name contains it

    '                                    If fri.Name.ToLower.Contains(Issue) And Not Issue = String.Empty Then
    '                                        'ListOpenPrePrintFolder.Items.Add(fri.FullName)
    '                                        If Not matches.Contains(fri) Then
    '                                            matches.Add(fri)
    '                                        End If
    '                                    End If

    '                                    'MsgBox(fri.Name)
    '                                Next fri

    '                            Catch ex As Exception
    '                                LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
    '                            End Try
    '                        End If



    '                    End If
    '                Next

    '            Next

    '        End If

    '    Catch ex As Exception
    '        LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
    '    End Try
    '    FoundProjectFolders.AddRange(matches)
    '    Return matches
    'End Function
#End Region
End Class
