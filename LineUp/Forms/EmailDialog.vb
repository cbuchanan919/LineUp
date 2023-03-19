Imports System.ComponentModel
Imports System.Windows.Forms

Public Class EmailDialog
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub EmailDialog_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        pdfInvoice.Visible = False
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim Result As Integer = MsgBox("Are you sure you want to send an email to " & txtEmailTo.Text & "?", MsgBoxStyle.OkCancel)
        If Result = MsgBoxResult.Ok Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnShow_Hover(sender As Object, e As EventArgs) Handles btnShow.MouseHover
        txtEmailPassword.UseSystemPasswordChar = False
    End Sub
    Private Sub btnShow_leave(sender As Object, e As EventArgs) Handles btnShow.MouseLeave
        txtEmailPassword.UseSystemPasswordChar = True

    End Sub

    Private Sub EmailDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LineUp.MybtnFmt.Format_Controls(Me)
    End Sub

    Private Sub lblAttachFiles_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblAttachFiles.LinkClicked

        Dim result As Integer = attachFiles.ShowDialog()
        If result = MsgBoxResult.Ok Then
            For Each myFile As String In attachFiles.FileNames
                cboAttachFiles.Items.Add(myFile)
            Next
            cboAttachFiles.SelectedIndex = 1

        End If
    End Sub

    Private Sub cboAttachFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAttachFiles.SelectedIndexChanged
        If cboAttachFiles.SelectedItem.ToString <> "" AndAlso cboAttachFiles.SelectedItem.ToString <> "None" Then
            If System.IO.File.Exists(cboAttachFiles.SelectedItem) Then
                If cboAttachFiles.SelectedItem.ToString.Contains(".pdf") Then
                    'pdfInvoice.Navigate(cboAttachFiles.SelectedItem)
                    pdfInvoice.Source = New Uri(cboAttachFiles.SelectedItem)
                End If

            Else

                pdfInvoice.Source = New Uri("about:blank")
                'pdfInvoice.Navigate("about:blank")
            End If
        Else
            pdfInvoice.Source = New Uri("about:blank")
            'pdfInvoice.Navigate("about:blank")
        End If

    End Sub


End Class
