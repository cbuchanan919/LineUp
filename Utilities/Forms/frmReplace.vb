Imports System.Windows.Forms

''' <summary>
''' This class provides four subroutines used to:
''' Find (find the first instance of a search term)
''' Find Next (find other instances of the search term after the first one is found)
''' Replace (replace the current selection with replacement text)
''' Replace All (replace all instances of search term with replacement text)
''' </summary>
''' <remarks></remarks>

Public Class frmReplace
    Private RtfEd As RtfEditor
    Public Sub New(ByVal OwningForm As RtfEditor)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        RtfEd = OwningForm
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click

        Dim StartPosition As Integer
        Dim SearchType As CompareMethod

        If chkMatchCase.Checked = True Then
            SearchType = CompareMethod.Binary
        Else
            SearchType = CompareMethod.Text
        End If

        StartPosition = InStr(1, RtfEd.rtbDoc.Text, txtSearchTerm.Text, SearchType)

        If StartPosition = 0 Then
            MessageBox.Show("String: '" & txtSearchTerm.Text.ToString() & "' not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        RtfEd.rtbDoc.Select(StartPosition - 1, txtSearchTerm.Text.Length)
        RtfEd.rtbDoc.ScrollToCaret()
        RtfEd.Focus()


    End Sub



    Private Sub btnFindNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindNext.Click

        Dim StartPosition As Integer = RtfEd.rtbDoc.SelectionStart + 2
        Dim SearchType As CompareMethod

        If chkMatchCase.Checked = True Then
            SearchType = CompareMethod.Binary
        Else
            SearchType = CompareMethod.Text
        End If

        StartPosition = InStr(StartPosition, RtfEd.rtbDoc.Text, txtSearchTerm.Text, SearchType)

        If StartPosition = 0 Then
            MessageBox.Show("String: '" & txtSearchTerm.Text.ToString() & "' not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        RtfEd.rtbDoc.Select(StartPosition - 1, txtSearchTerm.Text.Length)
        RtfEd.rtbDoc.ScrollToCaret()
        RtfEd.Focus()

    End Sub



    Private Sub btnReplace_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReplace.Click

        If RtfEd.rtbDoc.SelectedText.Length <> 0 Then
            RtfEd.rtbDoc.SelectedText = txtReplacementText.Text
        End If

        Dim StartPosition As Integer = RtfEd.rtbDoc.SelectionStart + 2
        Dim SearchType As CompareMethod

        If chkMatchCase.Checked = True Then
            SearchType = CompareMethod.Binary
        Else
            SearchType = CompareMethod.Text
        End If

        StartPosition = InStr(StartPosition, RtfEd.rtbDoc.Text, txtSearchTerm.Text, SearchType)

        If StartPosition = 0 Then
            MessageBox.Show("String: '" & txtSearchTerm.Text.ToString() & "' not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If

        RtfEd.rtbDoc.Select(StartPosition - 1, txtSearchTerm.Text.Length)
        RtfEd.rtbDoc.ScrollToCaret()
        RtfEd.Focus()

    End Sub



    Private Sub btnReplaceAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReplaceAll.Click


        Dim currentPosition As Integer = RtfEd.rtbDoc.SelectionStart
        Dim currentSelect As Integer = RtfEd.rtbDoc.SelectionLength

        RtfEd.rtbDoc.Rtf = Replace(RtfEd.rtbDoc.Rtf, Trim(txtSearchTerm.Text), Trim(txtReplacementText.Text))
        RtfEd.rtbDoc.SelectionStart = currentPosition
        RtfEd.rtbDoc.SelectionLength = currentSelect

        RtfEd.Focus()

    End Sub


    Private Sub frmReplace_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RtfEd.btnFmt.Format_Controls(Me)
    End Sub


End Class