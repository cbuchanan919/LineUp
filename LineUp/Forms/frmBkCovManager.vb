Imports System.IO
Imports System.ComponentModel

Public Class frmBkCovManager

#Region "Properties"
    Private Property folderPath As String = ""
    ''' <summary>
    ''' List of all products found
    ''' </summary>
    ''' <returns></returns>
    Private Property Prods As New List(Of UvProductInfo)
    ''' <summary>
    ''' list of products that had a match to a cover file
    ''' </summary>
    ''' <returns></returns>
    Private Property okProds As New List(Of UvProductInfo)
    ''' <summary>
    ''' list of products that had no matching files
    ''' </summary>
    ''' <returns></returns>
    Private Property noFilesProds As New List(Of UvProductInfo)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Property multiFilesProds As New List(Of UvProductInfo)
    Private Property noDirProds As New List(Of UvProductInfo)
    Private Property multiDirProds As New List(Of UvProductInfo)


    Private Property ProdsResult As New Text.StringBuilder
    Private Property okProdsResult As New Text.StringBuilder
    Private Property noFilesProdsResult As New Text.StringBuilder
    Private Property multiFilesProdsResult As New Text.StringBuilder
    Private Property noDirProdsResult As New Text.StringBuilder
    Private Property multiDirProdsResult As New Text.StringBuilder


    Private Property oldCovDir As DirectoryInfo = Nothing
    Private Property newCovDir As DirectoryInfo = Nothing
    Private Property MyJQProjectDirIO As New JQProjectDirIO
#End Region

#Region "Init"
    Private Sub frmOpenFolderDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmOpenFolderDlg_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Application.DoEvents()
        PopulateBookCovers()
        PopulateComparison()
        MsgBox("Hover over the buttons on the right to see what it will process in the results view.", MsgBoxStyle.Information)
    End Sub



    ''' <summary>
    ''' loads the list of file names. Also color codes if a file exists or not.
    ''' </summary>
    Private Sub PopulateComparison()
        Dim newFP As String = Path.Combine(folderPath, "New Version")
        lvFileNames.Items.Clear()

        If Directory.Exists(folderPath) And Directory.Exists(newFP) Then
            oldCovDir = New DirectoryInfo(folderPath)
            newCovDir = New DirectoryInfo(newFP)

            Dim oldFiles As List(Of FileInfo) = oldCovDir.GetFiles("*.pdf", SearchOption.TopDirectoryOnly).ToList()
            Dim newFiles As List(Of FileInfo) = newCovDir.GetFiles("*.pdf", SearchOption.TopDirectoryOnly).ToList()

            Dim FNList As New List(Of String)

            Dim oldFNs As New List(Of String)
            Dim newFNs As New List(Of String)

            For Each fi As FileInfo In oldFiles
                oldFNs.Add(fi.Name.ToLower)
                If Not FNList.Contains(fi.Name.ToLower) Then
                    FNList.Add(fi.Name.ToLower)
                End If
            Next
            For Each fi As FileInfo In newFiles
                newFNs.Add(fi.Name.ToLower)
                If Not FNList.Contains(fi.Name.ToLower) Then
                    FNList.Add(fi.Name.ToLower)
                End If
            Next

            For Each fn As String In FNList
                Dim lvItem As New ListViewItem
                lvItem.Text = fn
                lvItem.UseItemStyleForSubItems = False
                Dim subOld As New ListViewItem.ListViewSubItem()
                Dim subNew As New ListViewItem.ListViewSubItem()
                If Not oldFNs.Contains(fn.ToLower) Then
                    subOld.BackColor = Color.Red
                    subOld.Text = "X"
                End If
                If Not newFNs.Contains(fn.ToLower) Then
                    subNew.BackColor = Color.Red
                    subNew.Text = "X"
                End If
                lvItem.SubItems.Add(subOld)
                lvItem.SubItems.Add(subNew)
                lvFileNames.Items.Add(lvItem)
            Next

        End If

    End Sub


    Public Sub New(ByVal folderPath As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.folderPath = folderPath

    End Sub

#End Region

#Region "Methods"
    'Private Sub btnOK_Click(sender As Object, e As EventArgs)
    '    Dim okList As List(Of RadioButton) = {rdbAll, rdbMatchedOK, rdbNoFilesFound, rdbMultipleFilesFound, rdbMultipleFoldersFound}.ToList
    '    Dim isChecked As Boolean = False
    '    For Each rdb As RadioButton In okList
    '        If rdb.Checked Then
    '            isChecked = True
    '        End If

    '    Next

    '    If isChecked Then


    '        DialogResult = DialogResult.OK
    '    ElseIf rdbNone.Checked Then
    '        DialogResult = DialogResult.Cancel
    '    Else
    '        MsgBox("Please select which folders to view from the options above.", MsgBoxStyle.Exclamation)
    '    End If
    'End Sub


    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        PopulateBookCovers()
        PopulateComparison()
    End Sub

    Public Sub PopulateBookCovers()

        Try
            txtResults.Text = "LOADING..."
            Application.DoEvents()

            If Directory.Exists(folderPath) Then
                Dim covDir As New DirectoryInfo(folderPath)

                Prods = New List(Of UvProductInfo)
                okProds = New List(Of UvProductInfo)
                noFilesProds = New List(Of UvProductInfo)
                multiFilesProds = New List(Of UvProductInfo)
                noDirProds = New List(Of UvProductInfo)
                multiDirProds = New List(Of UvProductInfo)
                ProdsResult.Clear()
                okProdsResult.Clear()
                noFilesProdsResult.Clear()
                multiFilesProdsResult.Clear()
                noDirProdsResult.Clear()
                multiDirProdsResult.Clear()

                For Each fi As FileInfo In covDir.GetFiles("*.pdf", SearchOption.TopDirectoryOnly)
                    Try
                        'goes through all books pdf's in the above book covers folder

                        Dim prod As New UvProductInfo(False)

                        'gets file name without extension
                        Dim fn As String = fi.Name.ToLower.Replace(".pdf", "")

                        If IsNumeric(fn) Then
                            prod = LineUp.MyUvProductInfoIO.findProduct(fn).clone

                            If Not IsNothing(prod) Then
                                If MyJQProjectDirIO.GetDirectories(prod.ItemNum).Count = 1 Then
                                    ' 1 project folder found. 
                                    For Each projFile As FileInfo In prod.FoundProjectFolders(0).GetFiles("*", SearchOption.AllDirectories)
                                        If projFile.FullName.ToLower.Contains("cover") And projFile.Name.ToLower.Contains("final") And Not projFile.FullName.ToLower.Contains("archive") _
                                                And Not projFile.DirectoryName.ToLower.Contains("links") And (projFile.Extension.ToLower = ".psd" Or projFile.Extension.ToLower = ".indd") Then
                                            prod.FoundCoverFiles.Add(projFile)
                                        Else
                                            'beep
                                        End If
                                    Next
                                End If
                            End If
                        Else
                            prod.Title = fn
                        End If
                        Prods.Add(prod)
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "PopulateBookCovers")
                    End Try

                Next





                For Each prod As UvProductInfo In Prods
                    Select Case prod.FoundProjectFolders.Count
                        Case 0
                            prod.FoundCoverStatus = "Problem: No Project Folder Found"
                            noDirProds.Add(prod)
                        Case 1
                            Select Case prod.FoundCoverFiles.Count
                                Case 0
                                    prod.FoundCoverStatus = "Problem: No Final File Found"
                                    noFilesProds.Add(prod)
                                Case 1
                                    'prod.FoundCoverStatus = "(No problems?) 1 Final Cover file found."
                                    okProds.Add(prod)
                                Case Else
                                    prod.FoundCoverStatus = "Problem: Multiple Final Files Found"
                                    multiFilesProds.Add(prod)
                            End Select
                        Case Else
                            prod.FoundCoverStatus = "Problem: Multiple Project Folders Found"
                            multiDirProds.Add(prod)
                    End Select
                Next

                'creates the messages

                ProdsResult.AppendLine(okProds.Count & " books are ok and have a matching final cover.")
                ProdsResult.AppendLine((Prods.Count - okProds.Count) & " books have problems.")
                ProdsResult.AppendLine()

                ProdsResult.Append(AddToStringBuilder(okProdsResult, "The following items are considered to be OK: ", okProds))
                ProdsResult.Append(AddToStringBuilder(noFilesProdsResult, "There were no files found for the following items: ", noFilesProds))
                ProdsResult.Append(AddToStringBuilder(multiFilesProdsResult, "There were multiple files found for the following items: ", multiFilesProds))
                ProdsResult.Append(AddToStringBuilder(noDirProdsResult, "There were no folders found for the following items: ", noDirProds))
                ProdsResult.Append(AddToStringBuilder(multiDirProdsResult, "There were multiple folders found for the following items: ", multiDirProds))

                txtResults.Text = ProdsResult.ToString

            Else
                MsgBox("This folder was not found: " & vbCrLf & folderPath)
            End If
        Catch ex As Exception

            MsgBox(ex.Message)
        End Try


    End Sub


    Private Function AddToStringBuilder(ByRef sb As Text.StringBuilder, ByVal title As String, ByVal prods As List(Of UvProductInfo)) As String
        sb.AppendLine(title & vbCrLf)
        For Each curProject As UvProductInfo In prods
            sb.AppendLine(curProject.ItemNum & " - " & curProject.Title)
            If curProject.FoundCoverFiles.Count > 0 Then
                For Each myFile As FileInfo In curProject.FoundCoverFiles
                    sb.AppendLine(myFile.FullName)
                Next
            Else
                For Each myFolder As DirectoryInfo In curProject.FoundProjectFolders
                    sb.AppendLine(myFolder.FullName)
                Next
            End If

            sb.AppendLine(curProject.CoverPath.Replace(";", vbCrLf))
            sb.AppendLine()
        Next
        sb.AppendLine()
        sb.AppendLine()
        Return sb.ToString
    End Function

    Private Sub ShowMsgs(ByVal title As String, ByVal prods As List(Of UvProductInfo), ByVal QuanToProcessAtATime As Integer)
        'opens the different project folders.
        If QuanToProcessAtATime <= 0 Then QuanToProcessAtATime = 10
        Dim openCt As Integer = QuanToProcessAtATime
        Dim totalOpened As Integer = 0
        For Each uvfile As UvProductInfo In prods
            If openCt >= QuanToProcessAtATime Then
                Dim nxtFn As String = "The next file to process is: " & vbCrLf & uvfile.ItemNum & " - " & Truncate(uvfile.Title, 35)  '& ")"
                If uvfile.FoundCoverStatus <> "" Then nxtFn &= vbCrLf & uvfile.FoundCoverStatus
                Select Case MsgBox("Opening folders paused." & vbCrLf & vbCrLf & "Do you want to process the next " & openCt & " folder(s)? " & vbCrLf & "(Processed " & totalOpened & " of " & prods.Count & ")" & vbCrLf & vbCrLf & nxtFn, MsgBoxStyle.YesNoCancel, title)
                    Case MsgBoxResult.Yes
                        openCt = 0
                        For Each dir As DirectoryInfo In uvfile.FoundProjectFolders
                            Process.Start("explorer.exe", Chr(34) & dir.FullName & Chr(34))
                        Next
                    Case MsgBoxResult.No
                        openCt = 0
                    Case Else
                        Exit Sub
                End Select
                txtResults.Text = nxtFn.Replace("The next file to process is: ", "The last file shown was: ")
                Application.DoEvents()
            End If

            openCt += 1
            totalOpened += 1

        Next
        '        lblStart.Text = "of " & 
    End Sub

    Private Sub btnAll_Click(sender As Object, e As EventArgs) Handles btnAll.Click, btnMatchedOK.Click, btnNoFilesFound.Click, btnMultipleFilesFound.Click, btnMultipleFoldersFound.Click, btnNone.Click
        If TypeOf sender Is Button Then
            Dim sendButton As Button = sender
            Dim val As Integer = UpDownNumFolders.Value
            Select Case True
                Case btnAll Is sendButton
                    ShowMsgs("All Found Covers", Prods, val)
                Case btnMatchedOK Is sendButton
                    ShowMsgs("Successfully Matched Covers", okProds, val)
                Case btnNoFilesFound Is sendButton
                    ShowMsgs("No Covers Found", noFilesProds, val)
                Case btnMultipleFilesFound Is sendButton
                    ShowMsgs("Multiple Files Found", multiFilesProds, val)
                Case btnMultipleFoldersFound Is sendButton
                    ShowMsgs("Multiple Folders Found", multiDirProds, val)
                Case btnNone Is sendButton
                    MsgBox("Sorry, I can't open folders that don't exist...")
                Case Else
                    'do nothing
            End Select
        End If



    End Sub

    Private Sub btnNone_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnAll_MouseHover(sender As Object, e As EventArgs) Handles btnAll.MouseHover, btnMatchedOK.MouseHover, btnNoFilesFound.MouseHover, btnMultipleFilesFound.MouseHover, btnMultipleFoldersFound.MouseHover, btnNone.MouseHover
        If TypeOf sender Is Button Then
            Dim sendbutton As Button = sender
            Select Case True
                Case btnAll Is sendbutton
                    txtResults.Text = ProdsResult.ToString
                Case btnMatchedOK Is sendbutton
                    txtResults.Text = okProdsResult.ToString
                Case btnNoFilesFound Is sendbutton
                    txtResults.Text = noFilesProdsResult.ToString
                Case btnMultipleFilesFound Is sendbutton
                    txtResults.Text = multiFilesProdsResult.ToString
                Case btnMultipleFoldersFound Is sendbutton
                    txtResults.Text = multiDirProdsResult.ToString
                Case btnNone Is sendbutton
                    txtResults.Text = noDirProdsResult.ToString
                Case Else
                    'do nothing
            End Select
        End If
    End Sub


    Public Function Truncate(value As String, length As Integer) As String
        ' If argument is too big, return the original string.
        ' ... Otherwise take a substring from the string's start index.
        If length > value.Length Then
            Return value
        Else
            Return value.Substring(0, length) & "..."
        End If
    End Function

    Private Sub btnExportCov_Click(sender As Object, e As EventArgs) Handles btnExportCov.Click
        If MsgBox("Are you sure you want to export " & okProds.Count & " PDF(s)?" & vbCrLf & vbCrLf & "(They are the ones that are referenced by the 'Matched OK Files' button)", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim imp As New IndesignIO_Impose()

            imp.ImposeProductCovers(okProds)
        End If


    End Sub

    Private Sub btnExportACover_Click(sender As Object, e As EventArgs) Handles btnExportACover.Click
        Dim imp As New IndesignIO_Impose()
        If My.Computer.Keyboard.CtrlKeyDown Then
            ' special - allows to export all ok files.
            Dim msg As String = "Do you want to create book covers for all " & okProds.Count & " books?"
            If MsgBox(msg, MsgBoxStyle.YesNo, "Create Book Covers") = MsgBoxResult.Yes Then

                imp.ImposeProductCovers(okProds)
            End If
        Else
            imp.ImposeABookCover()
        End If

    End Sub

    Private Sub lvFileNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvFileNames.SelectedIndexChanged

        If lvFileNames.SelectedItems.Count = 1 Then
            WebOld.Navigate("about:blank")
            WebNew.Navigate("about:blank")
            Dim fn As String = lvFileNames.SelectedItems(0).Text
            If oldCovDir.Exists Then
                Dim fp As String = Path.Combine(oldCovDir.FullName, fn)
                If File.Exists(fp) Then
                    WebOld.Navigate(fp)
                End If
            End If
            If newCovDir.Exists Then
                Dim fp As String = Path.Combine(newCovDir.FullName, fn)
                If File.Exists(fp) Then
                    WebNew.Navigate(fp)
                End If
            End If
        End If

    End Sub



#End Region



End Class