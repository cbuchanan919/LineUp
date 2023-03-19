Imports System.IO
Public Class ImposeTractHomePrint

#Region "Properties"

    Public Property Searched As Boolean = False
    ''' <summary>
    ''' key = item number, value = full file path
    ''' </summary>
    ''' <returns></returns>
    Public Property FoundFiles As New SortedDictionary(Of String, String)
    Public Property SearchResults As New Text.StringBuilder

#End Region

#Region "Init"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LineUp.MybtnFmt.Format_Controls(Me)
        txtSearchLoc.Text = "X:\Print & Ebook Projects\Tracts\[5] Final\English Color"

    End Sub
    Private Sub TractHomePrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ImposeTractHomePrint_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim sb As New Text.StringBuilder
        sb.AppendLine("This feature can search for final indesign files inside the search folder.")
        sb.AppendLine("It will export the pdf and images to the images to process web folder. (Specified in settings?)")
        MsgBox(sb.ToString, MsgBoxStyle.Information)
    End Sub

#End Region


#Region "Methods"

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        If Searched Then
            Dim tempDict As New SortedDictionary(Of String, String)
            For Each myItem As ListViewItem In ListView1.Items
                If myItem.Checked Then
                    Dim key As String = myItem.Text
                    If FoundFiles.ContainsKey(key) Then
                        tempDict.Add(key, FoundFiles(key))
                    End If
                End If
            Next
            FoundFiles.Clear()
            For Each item In tempDict
                FoundFiles.Add(item.Key, item.Value)
            Next
            DialogResult = DialogResult.OK
            Hide()
        Else
            MsgBox("Must search for .indd files first", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Hide()
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchForFiles(txtSearchLoc.Text)
    End Sub

    Private Sub SearchForFiles(ByVal searchLoc As String)

        FoundFiles.Clear()
        SearchResults.Clear()
        ListView1.Items.Clear()


        If Directory.Exists(searchLoc) Then
            Dim searchDir As New DirectoryInfo(searchLoc)
            If searchLoc(searchLoc.Length - 1) <> "\" Then searchLoc &= "\"


            Dim allFinal As List(Of FileInfo) = searchDir.GetFiles("*final.indd", SearchOption.AllDirectories).ToList

            For Each final As FileInfo In allFinal
                If Not final.FullName.ToLower.Contains("archive") Then
                    Dim itemNumber As String = ""
                    Dim p As DirectoryInfo = Directory.GetParent(final.FullName)
                    Dim pp As DirectoryInfo = Directory.GetParent(p.FullName)
                    Dim ppp As DirectoryInfo = Directory.GetParent(pp.FullName)
                    Dim parts As New List(Of String)
                    parts.AddRange(p.Name.Split("-").ToList)
                    parts.AddRange(pp.Name.Split("-").ToList)
                    parts.AddRange(ppp.Name.Split("-").ToList)

                    For Each part As String In parts
                        If IsNumeric(part) Then
                            Dim d As Double = Double.Parse(part)
                            If d > 999 Then
                                itemNumber = part
                            End If
                        End If
                    Next


                    If itemNumber <> "" Then
                        If FoundFiles.ContainsKey(itemNumber) Then
                            MsgBox("error duplicate item number found - " & vbCrLf & itemNumber & vbCrLf & final.FullName & vbCrLf & vbCrLf &
                                   "already had: " & vbCrLf & FoundFiles(itemNumber))
                        Else
                            Dim item As New ListViewItem(itemNumber)
                            item.SubItems.Add(final.FullName.Replace(searchLoc, ""))
                            ListView1.Items.Add(item)
                            item.Checked = False


                            FoundFiles.Add(itemNumber, final.FullName)
                        End If

                    End If

                End If

            Next
            Searched = True

        End If
        ColFullFP.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
        Application.DoEvents()
        MsgBox("Please go through the list and put a check mark next to the final files you wish to export.", MsgBoxStyle.OkOnly, "Select Files To Export")
    End Sub

    Private Sub txtResults_TextChanged(sender As Object, e As EventArgs)

    End Sub


#End Region



End Class