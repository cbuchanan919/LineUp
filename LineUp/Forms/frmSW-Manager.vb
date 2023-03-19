Imports System.IO

Public Class frmSW_Manager


#Region "Properties / Variables"

    Private shareWordItems As New List(Of UvProductInfo)
    Private resultsDict As New Dictionary(Of ListViewItem, UvProductInfo)

    Private currentShareWord As UvProductInfo = Nothing 'currently loaded share word item

    Private shownFilesDict As New Dictionary(Of ListViewItem, IO.FileInfo) 'list of files found for the share word thingy...


    Private Const cSpacer As String = "     "
    Private loadAll As Boolean

#End Region

#Region "Init & Misc."

    Public Sub New(Optional ByVal loadAll As Boolean = False)
        Me.loadAll = loadAll
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmSW_Manager_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LineUp.MybtnFmt.Format_Controls(Me)
        updateSWItems()
        performSearch("")

    End Sub

    Private Sub frmSW_Manager_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        resizeFillerColumn()
    End Sub

    Private Sub updateSWItems()

        'Dim sortDict As New SortedDictionary(Of String, uvProductInfo)
        For Each prod As UvProductInfo In LineUp.MyUvProductInfoIO.productDict.Values
            If prod.SupID = cShareWordSupID Or loadAll = True Then
                'sortDict.Add(prod.invTitle, prod)
                shareWordItems.Add(prod)
            End If
        Next
        'For Each key As String In sortDict.Keys
        '    shareWordItems.Add(sortDict(key))
        'Next
        shareWordItems.Sort(Function(x, y) x.InvTitle.CompareTo(y.InvTitle))

        Me.Text = "Share Word Manager (" & shareWordItems.Count & " Items)"

        If shareWordItems.Count < 25 Then
            MsgBox("Not loaded yet. Please try again in a minute!")
            Close()
        End If

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

#End Region


#Region "GUI / Button / Menu Events"


    Private Sub tmrSearch_Tick(sender As Object, e As EventArgs) Handles tmrSearch.Tick
        tmrSearch.Stop()
        performSearch(txtSearch.Text)

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        tmrSearch.Stop()
        tmrSearch.Start()
    End Sub


    Private Sub btnAddFile_Click(sender As Object, e As EventArgs) Handles btnAddFile.Click
        addFileToProjectFolder()
    End Sub

    Private Sub AddShareWordFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddShareWordFileToolStripMenuItem.Click
        addFileToProjectFolder()
    End Sub


    Private Sub DeleteShareWordFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteShareWordFileToolStripMenuItem.Click
        deleteSelectedFile()
    End Sub


    Private Sub CreateProductionPDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateProductionPDFToolStripMenuItem.Click
        CreateProductionPDF()
    End Sub
    Private Sub btnCreatePDF_Click(sender As Object, e As EventArgs) Handles btnCreatePDF.Click
        CreateProductionPDF()
    End Sub



    Private Sub lvResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvResults.SelectedIndexChanged
        currentShareWord = Nothing
        If lvResults.SelectedIndices.Count > 0 Then
            Dim item As ListViewItem = lvResults.Items(lvResults.SelectedIndices(0))
            If resultsDict.ContainsKey(item) Then
                currentShareWord = resultsDict(item)
            End If
        End If
        showFiles()

    End Sub


    Private Sub lvFiles_DoubleClick(sender As Object, e As EventArgs) Handles lvFiles.DoubleClick
        If lvFiles.SelectedItems.Count > 0 Then
            If shownFilesDict.ContainsKey(lvFiles.SelectedItems(0)) Then
                Process.Start(shownFilesDict(lvFiles.SelectedItems(0)).FullName)
            End If
        End If
    End Sub


#End Region


#Region "Methods"

    Private Sub performSearch(ByVal searchStr As String)

        lvResults.Items.Clear()
        resultsDict.Clear()


        Dim findings As New List(Of UvProductInfo)
        searchStr = searchStr.Trim.ToLower
        For Each share As UvProductInfo In shareWordItems
            Dim addToFindings As Boolean = False
            Select Case True
                Case searchStr = ""
                    addToFindings = True
                Case share.ItemNum.ToString.Contains(searchStr)
                    addToFindings = True
                Case share.Title.ToLower.Contains(searchStr)
                    addToFindings = True
                Case share.Author.ToLower.Contains(searchStr)
                    addToFindings = True
            End Select
            If addToFindings Then
                findings.Add(share.clone)
            End If
        Next

        If searchStr <> "" Then
            'this next part tries to put the matches that start with the search str at the top...
            Dim topMatches As New List(Of UvProductInfo)
            For Each foundShare As UvProductInfo In findings

                Dim titleSubStr As String = foundShare.Title.Substring(0, maxLength(foundShare.Title, searchStr.Length))
                Dim invTitleSubStr As String = foundShare.InvTitle.Substring(0, maxLength(foundShare.InvTitle, searchStr.Length))

                If titleSubStr.ToLower = searchStr Or invTitleSubStr.ToLower = searchStr Then
                    topMatches.Add(foundShare)
                End If
            Next
            topMatches.Reverse()
            For Each topMatch As UvProductInfo In topMatches
                findings.Remove(topMatch)
                findings.Insert(0, topMatch)
            Next
        End If


        For Each share As UvProductInfo In findings
            Dim title As String = share.InvTitle
            If title.Length > 60 Then
                title = title.Substring(0, 60) & "..."
            ElseIf title.Length = 0 Then
                title = "No Title Found"
            End If
            Dim result As New ListViewItem(title)
            result.SubItems.Add("(" & share.ItemNum & ", " & share.Author & ")")
            lvResults.Items.Add(result)
            resultsDict.Add(result, share)
        Next

    End Sub

    ''' <summary>
    ''' if trying to get a sub string, this will limit the length of the search to keep it from going out of bounds.
    ''' </summary>
    ''' <param name="strToCount">original string - the one to get a sub string from</param>
    ''' <param name="searchLength">the length of the desired sub string - ie search string</param>
    ''' <returns></returns>
    Private Function maxLength(ByVal strToCount As String, ByVal searchLength As Integer) As Integer
        If strToCount.Length >= searchLength Then
            Return searchLength
        Else
            Return strToCount.Length
        End If
    End Function



    ''' <summary>
    ''' displays the files in the file list view
    ''' </summary>
    Private Sub showFiles()

        lvFiles.Items.Clear()
        txtShareWordInfo.Text = ""
        shownFilesDict.Clear()

        If Not IsNothing(currentShareWord) Then
            txtShareWordInfo.Text = currentShareWord.ToString
            Dim ShareIO As New ShareWordIO(My.Settings.dirShareWordProjects)
            Dim shareFolder As IO.DirectoryInfo = ShareIO.GetShareWordFolderByID(currentShareWord.ItemNum)

            If Not IsNothing(shareFolder) Then
                If IO.Directory.Exists(shareFolder.FullName) Then
                    For Each myFile As IO.FileInfo In shareFolder.GetFiles
                        If cWordOkExtensions.Contains(myFile.Extension.ToLower) Then
                            Dim lvItem As New ListViewItem(myFile.Name & cSpacer)
                            lvItem.SubItems.Add(myFile.LastWriteTime & cSpacer)
                            lvItem.SubItems.Add(GetFileSize(myFile.FullName) & cSpacer)

                            lvFiles.Items.Add(lvItem)
                            shownFilesDict.Add(lvItem, myFile)

                        End If
                    Next

                    lvFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

                    resizeFillerColumn()
                End If
            End If
        End If
    End Sub



    Private Sub deleteSelectedFile()
        If lvFiles.SelectedItems.Count = 1 Then
            If shownFilesDict.ContainsKey(lvFiles.SelectedItems(0)) Then
                If MsgBox("Are you sure you want to delete:" & vbCrLf & vbCrLf &
                          shownFilesDict(lvFiles.SelectedItems(0)).Name, MsgBoxStyle.YesNo, "Delete File?") = MsgBoxResult.Yes Then

                    File.Delete(shownFilesDict(lvFiles.SelectedItems(0)).FullName)
                End If
            End If
        End If
        showFiles()
    End Sub


    ''' <summary>
    ''' Adds a file to the current project folder.
    ''' </summary>
    Private Sub addFileToProjectFolder()

        Try
            If IsNothing(currentShareWord) Then
                MsgBox("Please select a ShareWord project on the left before continuing!", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim loadDlg As New OpenFileDialog
            loadDlg.InitialDirectory = My.Settings.dirShareWordJohnDocuments
            Dim filter As String = "*" & String.Join(";*", cWordOkExtensions)
            loadDlg.Filter = "Word Docs|" & filter & "|All Files|*.*"
            loadDlg.Multiselect = False
            Dim word As String = "winword"
            If loadDlg.ShowDialog = DialogResult.OK Then
                Dim fileToAdd As New IO.FileInfo(loadDlg.FileName)
                If MsgBox("Is this the file to use for production?" & vbCrLf &
                      vbCrLf &
                      fileToAdd.Name & vbCrLf &
                      vbCrLf &
                      currentShareWord.Title, MsgBoxStyle.YesNo, "Create production pdf?") = MsgBoxResult.Yes Then


                    Dim wordConn As New WordConnectionIO
                    Dim pdfPath As String = Path.Combine(My.Settings.dirShareWordProduction, currentShareWord.ItemNum & ".pdf")
                    If File.Exists(pdfPath) Then
                        File.Delete(pdfPath)
                    End If
                    wordConn.ExportWordToPDF(fileToAdd.FullName, pdfPath)
                    ' MsgBox(prodtype.bodDir)
                    Threading.Thread.Sleep(1000)

                End If


                Dim destDir As New DirectoryInfo(My.Settings.dirShareWordProjects)
                Dim destDirStr As String = ""

                For Each shareFolder As DirectoryInfo In destDir.GetDirectories
                    Dim num As String = shareFolder.Name.Split("-")(1).Trim
                    If num = currentShareWord.ItemNum AndAlso num <> "" Then
                        destDirStr = shareFolder.FullName
                    End If
                Next
                'if the program gets to this point, it didn't find a folder. create one now
                If destDirStr = "" Then
                    destDirStr = Path.Combine(destDir.FullName, currentShareWord.folderTitle)
                End If

                If Not Directory.Exists(destDirStr) Then
                    Directory.CreateDirectory(destDirStr)
                End If
                Dim destFilePath As String = Path.Combine(destDirStr, fileToAdd.Name)
                If File.Exists(destFilePath) Then
                    Select Case MsgBox("File already exists, Overwrite existing file?", MsgBoxStyle.YesNo)
                        Case MsgBoxResult.Yes
                            File.Delete(destFilePath)
                            File.Move(fileToAdd.FullName, destFilePath)
                        Case MsgBoxResult.No
                            MsgBox("Original file was left where it was...")
                    End Select
                Else
                    File.Move(fileToAdd.FullName, destFilePath)
                End If


                showFiles()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub


    Private Sub CreateProductionPDF()
        If lvFiles.SelectedIndices.Count = 1 Then
            If shownFilesDict.ContainsKey(lvFiles.SelectedItems(0)) Then
                Dim fi As IO.FileInfo = shownFilesDict(lvFiles.SelectedItems(0))
                Dim prompt As New Text.StringBuilder("Are you sure you want to create the production pdf for:" & vbCrLf & vbCrLf)
                prompt.AppendLine(currentShareWord.Title)
                prompt.AppendLine(currentShareWord.ItemNum & vbCrLf & vbCrLf)
                prompt.AppendLine("With this file?")
                prompt.Append(fi.Name)

                If MsgBox(prompt.ToString, MsgBoxStyle.YesNo, "Create Production PDF") = MsgBoxResult.Yes Then
                    Dim word As New WordConnectionIO
                    Dim exportPath As String = IO.Path.Combine(My.Settings.dirShareWordProduction, currentShareWord.ItemNum & ".pdf")
                    If word.ExportWordToPDF(fi.FullName, exportPath) Then
                        If MsgBox("File Created!" & vbCrLf & "Would you like to open it now?", MsgBoxStyle.YesNo, "Created Successfully") = MsgBoxResult.Yes Then
                            Process.Start(exportPath)
                        End If
                        Dim johnKCopy As String = Path.Combine(My.Settings.dirShareWordJohnK, currentShareWord.ItemNum & ".pdf")
                        Try
                            If File.Exists(johnKCopy) Then
                                File.Delete(johnKCopy)
                            End If
                            File.Copy(exportPath, johnKCopy)
                        Catch ex As Exception
                            MsgBox("I wasn't able to copy the production file to :" & johnKCopy & vbCrLf & vbCrLf & ex.Message)
                        End Try

                    End If

                End If

            End If
        Else
            MsgBox("A file must be selected first...")
        End If
    End Sub


    Public Function GetFileSize(ByVal TheFile As String) As String
        If TheFile.Length = 0 Then Return ""
        If Not System.IO.File.Exists(TheFile) Then Return ""
        '---
        Dim TheSize As ULong = My.Computer.FileSystem.GetFileInfo(TheFile).Length
        Dim SizeType As String = ""
        '---
        Dim DoubleBytes As Double = 0.0

        Try
            Select Case TheSize
                Case Is >= 1099511627776
                    DoubleBytes = CDbl(TheSize / 1099511627776) 'TB
                    Return FormatNumber(DoubleBytes, 2) & " TB"
                Case 1073741824 To 1099511627775
                    DoubleBytes = CDbl(TheSize / 1073741824) 'GB
                    Return FormatNumber(DoubleBytes, 2) & " GB"
                Case 1048576 To 1073741823
                    DoubleBytes = CDbl(TheSize / 1048576) 'MB
                    Return FormatNumber(DoubleBytes, 2) & " MB"
                Case 1024 To 1048575
                    DoubleBytes = CDbl(TheSize / 1024) 'KB
                    Return FormatNumber(DoubleBytes, 2) & " KB"
                Case 0 To 1023
                    DoubleBytes = TheSize ' bytes
                    Return FormatNumber(DoubleBytes, 2) & " bytes"
                Case Else
                    Return ""
            End Select
        Catch
            Return ""
        End Try
    End Function

    Private Sub resizeFillerColumn()
        'this next part tries to make a filler column...
        Dim width As Integer = lvFiles.Width
        For i As Integer = 0 To lvFiles.Columns.Count - 2
            width -= lvFiles.Columns(i).Width
        Next
        lvFiles.Columns(3).Width = width - 10
    End Sub

    'Private Sub btnJobTicket_Click(sender As Object, e As EventArgs) Handles btnJobTicket.Click
    '    If Not IsNothing(currentShareWord) Then
    '        Dim fp As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), currentShareWord.ItemNum & ".pdf")
    '        If currentShareWord.createJobTicket(fp, 50, True, "") Then '" It's wabbit season, and I'm hunting wabbits, so be vewy, vewy quiet!") Then
    '            Process.Start(fp)
    '        End If
    '    End If

    'End Sub

    Private Sub ShowFolderNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyFolderNameToolStripMenuItem.Click
        If Not IsNothing(currentShareWord) Then
            Clipboard.Clear()
            Clipboard.SetText(currentShareWord.folderTitle)
            Beep()
        End If
    End Sub

    Private Sub FixFolderNamesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FixFolderNamesToolStripMenuItem.Click
        Dim opnDlg As New FolderBrowserDialog
        opnDlg.SelectedPath = My.Settings.dirShareWordProjects
        If opnDlg.ShowDialog = DialogResult.OK Then

            Dim shareIO As New ShareWordIO(opnDlg.SelectedPath)
            Dim folderTitlesToCorrect As New List(Of UvProductInfo)
            Dim ct As Integer = 0
            For Each shareItem As UvProductInfo In shareWordItems
                Try
                    Dim shareFolder As DirectoryInfo = shareIO.GetShareWordFolderByID(shareItem.ItemNum)
                    If Not IsNothing(shareFolder) Then
                        If shareFolder.Name.ToLower <> shareItem.folderTitle.ToLower Then
                            FileIO.FileSystem.RenameDirectory(shareFolder.FullName, shareItem.folderTitle)
                            ct += 1
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("Renaming failed for: " & shareItem.folderTitle & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try

            Next

            MsgBox(ct & " folder title correction(s) were made.")

        End If
    End Sub



    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Select Case e.KeyValue
            Case Keys.Return
                If lvResults.Items.Count > 0 Then
                    lvResults.Select()
                    lvResults.Items(0).Selected = True
                End If
        End Select
    End Sub



#End Region







End Class