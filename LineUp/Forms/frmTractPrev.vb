Imports System.IO

''' <summary>
''' A simple form to preview tract pictures
''' </summary>
Public Class frmTractPrev


#Region "Properties"

    Private Const cSpacer As String = "                                                                 ~"
    Private Property prodUVinfo As UvProductInfoIO
    Private Property numDict As New Dictionary(Of String, tractImages)

#End Region

#Region "Init"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        LineUp.MybtnFmt.Format_Controls(Me)
        prodUVinfo = LineUp.MyUvProductInfoIO.clone

    End Sub

    Private Sub frmTractPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

#End Region

#Region "Methods"



    Private Sub txtEntry_TextChanged(sender As Object, e As KeyEventArgs) Handles txtEntry.KeyDown
        'processes the entry on a enter key down
        Select Case e.KeyCode
            Case Keys.Return
                AddItemToList()
        End Select


    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        AddItemToList()
    End Sub

    Private Sub AddItemToList()
        Dim tempUvProdInfo As UvProductInfo = prodUVinfo.findProduct(txtEntry.Text)


        Dim textToAdd As String = ""

        If tempUvProdInfo.MatchedOK Then
            textToAdd = tempUvProdInfo.ItemNum & cSpacer & tempUvProdInfo.Title
        Else
            textToAdd = txtEntry.Text & cSpacer & " "
        End If
        If Not lbItems.Items.Contains(textToAdd) Then
            lbItems.Items.Add(textToAdd)
        End If


        PopulatePics()
        txtEntry.SelectAll()
        tmrCheckStatus.Start()


    End Sub

    ''' <summary>
    ''' Re-Populates the pictures based on the lblItems list
    ''' </summary>
    Private Sub PopulatePics()
        ''picPath = ProdDirInfo.GetPicPath(ProductNumber, My.Settings.dirWebProd)
        FlowPicsPreview.Controls.Clear()


        For Each myItem In lbItems.Items

            Dim itm As String = myItem.ToString.Replace(cSpacer, "^")
            Dim parts() As String = itm.Split("^")
            Dim num As String = parts(0)
            Dim Title As String = parts(1)
            If Title.Length > 18 Then
                Title = Title.Substring(0, 15) & "..."
            End If
            If Not numDict.ContainsKey(num) Then
                Dim foldersToSearch As New List(Of String)
                foldersToSearch.AddRange({"X:\Print & Ebook Projects\Tracts\[5] Final", "X:\Print & Ebook Projects\Tracts\[1] Tract Candidates"}.ToList)
                Dim tract As New tractImages(num, Title, foldersToSearch)
                numDict.Add(num, tract)
            End If

            If numDict.ContainsKey(num) Then
                FlowPicsPreview.Controls.Add(numDict(num).GpBox)
            End If

        Next
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If lbItems.SelectedItems.Count <> 1 Then
            '    e.Cancel = True
        End If
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveItemToolStripMenuItem.Click
        Dim newList As New List(Of String) ' Creates a temp list
        If lbItems.SelectedItems.Count > 0 Then
            'if there's a item to remove
            For i As Integer = 0 To lbItems.Items.Count - 1
                'goes through each item and adds it to the temp list if it's not selected to be removed
                If Not lbItems.SelectedIndices.Contains(i) Then
                    newList.Add(lbItems.Items(i))

                End If
            Next
        End If
        lbItems.Items.Clear()
        For Each myItem In newList
            'adds items from the temp list back to the items list
            lbItems.Items.Add(myItem)
        Next
        PopulatePics()
    End Sub


    Private Sub tmrCheckStatus_Tick(sender As Object, e As EventArgs) Handles tmrCheckStatus.Tick
        'updates the status based on how many tracts are processing the image density thingy
        tmrCheckStatus.Stop() 'stops to do processing
        Console.WriteLine("Starting timer")
        Dim sortStr As String = "Sort Items"
        Dim startTimer As Boolean = True
        Dim ct As Integer = 0
        For i As Integer = 0 To lbItems.Items.Count - 1
            Dim curItem As Object = lbItems.Items(i)
            curItem = curItem.ToString.Replace(cSpacer, "^")
            Dim itemNum As String = curItem.ToString.Split("^")(0) 'gets the item number from the list view item
            If numDict.ContainsKey(itemNum) Then
                If Not numDict(itemNum).avgColorCalculated Then
                    ct += 1
                End If
            End If
        Next


        If ct = 0 Then
            startTimer = False
            With SortItemsToolStripMenuItem
                .Enabled = True
                .Text = sortStr
            End With
            txtStatus.Text = "All images scanned!"
        Else
            Dim waiting As String = " (Waiting for " & ct & " tracts to be processed)"
            With SortItemsToolStripMenuItem
                .Enabled = False
                .Text = sortStr & waiting
            End With
            txtStatus.Text = waiting.Trim
        End If

        If startTimer Then
            'starts the timer again
            tmrCheckStatus.Start()
        End If
        Console.WriteLine("StOpPiNg tImEr")
    End Sub

#End Region

#Region "DragDrop"

    Private Sub FlowPicsPreview_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbItems.MouseDown
        Dim ix As Integer = lbItems.IndexFromPoint(e.Location)
        If ix <> -1 Then
            lbItems.DoDragDrop(ix.ToString, DragDropEffects.Move)
        End If
    End Sub

    Private Sub lbItems_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lbItems.DragOver
        If e.Data.GetDataPresent(DataFormats.Text) Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub lbItems_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lbItems.DragDrop
        If e.Data.GetDataPresent(DataFormats.Text) Then
            Dim dix As Integer = CInt(e.Data.GetData(DataFormats.Text))
            Dim ix As Integer = lbItems.IndexFromPoint(lbItems.PointToClient(New Point(e.X, e.Y)))

            If ix <> -1 Then
                Dim obj As Object = lbItems.Items(dix)
                lbItems.Items.Remove(obj)
                lbItems.Items.Insert(ix, obj)
                PopulatePics()
            End If
        End If
    End Sub



#End Region

#Region "Find TractPics"



    Private Sub ClearListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearListToolStripMenuItem.Click
        numDict.Clear()
        lbItems.Items.Clear()
        PopulatePics()

    End Sub

    Private Sub SortItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SortItemsToolStripMenuItem.Click
        Dim allItemsAveraged As Boolean = True
        Dim sortedItems As New List(Of tractImages)
        For Each myItem In lbItems.Items

            Dim itm As String = myItem.ToString.Replace(cSpacer, "^")
            Dim parts() As String = itm.Split("^")
            Dim num As String = parts(0)



            If numDict.ContainsKey(num) Then
                If numDict(num).avgColorCalculated Then
                    'sortedItems.Add()
                    sortedItems.Add(numDict(num))
                Else
                    allItemsAveraged = False
                    MsgBox("Not done looking at the images... please try again in a minute :( " & vbCrLf & vbCrLf & "If you keep getting this message, it may have errored out...")
                    Exit Sub
                End If
            End If
        Next
        sortedItems.Sort(Function(x, y) x.avgColor.CompareTo(y.avgColor))
        lbItems.Items.Clear()
        For Each trImg As tractImages In sortedItems
            lbItems.Items.Add(trImg.productNumber & cSpacer & trImg.title)
        Next
        PopulatePics()

    End Sub


#End Region

End Class


Public Class tractImages

#Region "Properties"
    Public Property title() As String = ""
    Public Property productNumber() As String = ""

    Private _groupBox As GroupBox = Nothing
    Public Property GpBox() As GroupBox
        Get
            If IsNothing(_groupBox) Then
                createGroupBox()
            End If
            Return _groupBox
        End Get
        Set(ByVal value As GroupBox)
            _groupBox = value
        End Set
    End Property

    Public Property finalFolder() As String = ""
    Public Property foldersToSearch() As New List(Of String)
    Public Property candidateTractFolder() As String = ""

    Private Const cSpacer As String = "                                                                 ~"

    ''' <summary>
    ''' attempt at getting the average tract color
    ''' </summary>
    ''' <returns></returns>
    Public Property avgColor() As Integer
    Public Property avgColorCalculated As Boolean = False
    Private Property avgColorIO As AvgColor
#End Region

#Region "Init"
    Public Sub New(ByVal itemNumber As String, ByVal prodTitle As String, tractFoldersToSearch As List(Of String))
        productNumber = itemNumber
        title = prodTitle
        foldersToSearch.AddRange(tractFoldersToSearch.ToList)
        Dim found As Boolean = False
        Dim finDir As String = "X:\Print & Ebook Projects\Tracts\[5] Final"
        findTractFolder(finDir)

    End Sub



#End Region


#Region "Methods"
    Public Sub clicked(sender As Object, e As EventArgs)

        Clipboard.Clear()
        Try
            Clipboard.SetText(finalFolder)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Function createGroupBox() As GroupBox
        GpBox = New GroupBox
        AddHandler GpBox.DoubleClick, AddressOf clicked

        Try
            Dim tmpItem As New JQRowInfo
            If IsNumeric(productNumber) Then
                Integer.TryParse(productNumber, tmpItem.ItemNumber)
            End If

            Dim PicPath As String = LineUp.MyJQProductionIO.GetPicPath(tmpItem, My.Settings.dirWebProd)

            Dim files As List(Of String) = GetTractPics(productNumber, My.Settings.dirWebProd) 'returns a list of tract preview pics
            If files.Count = 0 Then
                files.Add(Path.Combine(My.Settings.dirResources, "PicNotFound.jpg"))
            End If

            ' LineUp.MyProductionIO.ClearInfo()
            GpBox.Text = productNumber & " - " & title
            GpBox.Height = 450
            GpBox.Width = 270


            Dim picTable As New TableLayoutPanel 'a table to layout the pictures on
            AddHandler picTable.DoubleClick, AddressOf clicked
            picTable.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
            picTable.ColumnCount = 2
            picTable.ColumnStyles.Add(New ColumnStyle(SizeType.Percent))
            picTable.ColumnStyles.Add(New ColumnStyle(SizeType.Percent))
            picTable.RowCount = 2
            picTable.RowStyles.Add(New RowStyle(SizeType.Percent))
            picTable.RowStyles.Add(New RowStyle(SizeType.Percent))
            For Each style As RowStyle In picTable.RowStyles

                style.SizeType = SizeType.Percent
                style.Height = 50
            Next
            For Each style As ColumnStyle In picTable.ColumnStyles
                style.SizeType = SizeType.Percent
                style.Width = 50
            Next
            picTable.Dock = DockStyle.Fill

            For Each selFile As String In files
                If File.Exists(selFile) Then

                    Dim picBox As New PictureBox

                    picBox.Load(selFile)
                    picBox.SizeMode = PictureBoxSizeMode.Zoom
                    picBox.Dock = DockStyle.Fill
                    'picBox.BackColor = Color.Blue


                    'next row contains the possible suffixes
                    '{"-Large.png", "-Large.jpg", "-Preview1.jpg", "-Preview1.png", "-Preview2Single.jpg", "-Preview2Single.png"}
                    Dim added As Boolean = False
                    If selFile.Contains("-Large") Then
                        picTable.Controls.Add(picBox, 1, 0)
                        added = True
                    ElseIf selFile.Contains("-Preview1") Then
                        picTable.Controls.Add(picBox, 0, 1)
                        Dim c As Control = picTable.GetControlFromPosition(0, 1)
                        picTable.SetColumnSpan(c, 2)
                        added = True
                    ElseIf selFile.Contains("Preview2Single") Then
                        picTable.Controls.Add(picBox, 0, 0)
                        added = True
                    ElseIf selFile.Contains("PicNotFound.jpg") Then
                        picTable.Controls.Add(picBox, 0, 0)
                        Dim c As Control = picTable.GetControlFromPosition(0, 0)
                        picTable.SetColumnSpan(c, 2)
                        picTable.SetRowSpan(c, 2)
                        added = True
                    End If
                    If added Then
                        AddHandler picBox.DoubleClick, AddressOf clicked
                    End If
                End If
            Next
            avgColorIO = New AvgColor(Me, files)
            Dim tStart As New Threading.ThreadStart(AddressOf avgColorIO.getAverageColor)
            Dim tThread As New Threading.Thread(tStart)
            tThread.Priority = Threading.ThreadPriority.Lowest
            tThread.Start()



            GpBox.Controls.Add(picTable)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return GpBox

    End Function


    ''' <summary>
    ''' Looks for the specified product number (aProdNum) in the specified folder (DirToSearch)
    ''' </summary>
    ''' <param name="aProdNum"></param>
    ''' <param name="DirToSearch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTractPics(ByVal aProdNum As String, ByVal DirToSearch As String) As List(Of String)


        Dim PicPaths As New List(Of String)

        Try
            'adds a \ to the end of the folder string if there's not one there...
            If DirToSearch.Substring(DirToSearch.Length - 1) <> "\" Then
                DirToSearch &= "\"
            End If

            ' used to store the first number(s) of the product number stored
            Dim picDigit As String = ""

            Select Case aProdNum.Length

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
                Dim PicSizes() As String = {"-Large.png", "-Large.jpg", "-Preview1.jpg", "-Preview1.png", "-Preview2Single.jpg", "-Preview2Single.png"}
                'Dim FoundItems As List(Of String) = New List(Of String)

                For Each picsize As String In PicSizes
                    'goes through each specified pic size, and tries to find a match in the found file listing
                    Dim TempPicPath As String = ProductImageFolder & "\" & aProdNum & picsize
                    For Each myFile As FileInfo In fileList
                        If myFile.FullName = TempPicPath Then
                            'PicPath = TempPicPath
                            Dim foundMatch As Boolean = False
                            For Each selPic In PicPaths
                                If selPic.Contains(picsize.Split(".")(0)) Then
                                    'it already found a file with a different ext.
                                    foundMatch = True
                                End If
                            Next
                            If Not foundMatch Then
                                PicPaths.Add(TempPicPath)
                            End If

                            Exit For
                        End If
                    Next

                Next

            Else
                PicPaths = New List(Of String)
            End If

        Catch ex As Exception
            PicPaths = New List(Of String)
        End Try
        Return PicPaths
    End Function

    Private Sub findTractFolder(ByVal folderPath As String)

        Dim foundIt As Boolean = False
        If Directory.Exists(folderPath) Then
            Dim dir As New DirectoryInfo(folderPath)

            For Each langFolder As DirectoryInfo In dir.GetDirectories
                'eng color, eng Stand, etc.
                If langFolder.Name.Contains(productNumber) Then
                    foundIt = True
                    finalFolder = langFolder.FullName
                End If
                For Each tractFold As DirectoryInfo In langFolder.GetDirectories
                    If Not foundIt Then
                        Dim nameParts() As String = tractFold.Name.Split("-")
                        For Each part As String In nameParts
                            part = part.Trim
                            If part = productNumber Then
                                foundIt = True
                                finalFolder = tractFold.FullName
                            End If
                        Next
                    End If
                Next
                If foundIt Then
                    Exit For
                End If
            Next
            If foundIt Then
                'the product number was found in a folder name.
                dir = New DirectoryInfo(finalFolder)
                Dim finalFolderMatched As Boolean = False
                Dim finalFolderOpts As New List(Of DirectoryInfo)
                finalFolderOpts.Add(New DirectoryInfo(finalFolder)) 'adds the 'master' product folder.
                finalFolderOpts.AddRange(dir.GetDirectories) 'adds the sub folders from the 'master'
                For Each myFolder As DirectoryInfo In finalFolderOpts
                    'goes through each folder. starting with the root prod. folder
                    If Not myFolder.FullName.ToLower.Contains("archive") Then
                        If Not finalFolderMatched Then
                            For Each myFile As FileInfo In myFolder.GetFiles("*.indd")
                                If myFile.Name.ToLower.Contains("final") Then
                                    finalFolderMatched = True
                                    finalFolder = myFolder.FullName 'sets the final folder to the one with the final file in it.
                                End If
                            Next
                        End If
                    End If
                Next


            End If

        End If

    End Sub

#End Region

End Class

''' <summary>
''' gets the average color for 1 tract. Each tract has several images / image files (ie. front, inside, back, etc?)
''' </summary>
Public Class AvgColor

    Private Property OwningClass() As tractImages
    Private Property files As List(Of String)

    Public Sub New(ByVal trImg As tractImages, ByVal filesToSearch As List(Of String))
        OwningClass = trImg
        files = filesToSearch
    End Sub

    ''' <summary>
    ''' Gets the average color for the files in the 'files' list
    ''' </summary>
    Public Sub getAverageColor() 'As Integer ' Color

        Try
            Dim results As Integer = 0
            Dim fileCt As Integer = 0
            For Each selFile As String In files
                If File.Exists(selFile) Then
                    fileCt += 1
                    Dim bmp As New Bitmap(selFile)
                    Dim totalR As Integer = 0
                    Dim totalG As Integer = 0
                    Dim totalB As Integer = 0
                    For x As Integer = 0 To bmp.Width - 1
                        For y As Integer = 0 To bmp.Height - 1
                            Dim pixel As Color = bmp.GetPixel(x, y)
                            totalR += pixel.R
                            totalG += pixel.G
                            totalB += pixel.B

                        Next
                    Next
                    Dim totalPixels As Integer = bmp.Height * bmp.Width
                    Dim averageR As Integer = totalR \ totalPixels
                    Dim averageG As Integer = totalG \ totalPixels
                    Dim averageB As Integer = totalB \ totalPixels
                    ' averageB /= 1.25
                    'averageB = averageB ^ (averageB / 255)
                    results += (averageR + averageG + averageB) ' / 3
                End If

            Next
            If fileCt > 0 Then
                OwningClass.avgColor = results / fileCt
            End If
            'Return Color.FromArgb(averageR, averageg, averageb)

            OwningClass.avgColorCalculated = True
            'Return (averageR + averageG + averageB) / 3
            ' MsgBox(OwningClass.avgColor)
        Catch ex As Exception

        End Try

    End Sub

End Class