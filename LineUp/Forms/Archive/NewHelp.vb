Imports System.Windows.Forms

Public Class NewHelp

    Public PromptTitle As String = "New Help Article?"


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If MsgBox("The help article will be called " & vbCrLf & vbCrLf & txtTitle.Text, MsgBoxStyle.OkCancel, PromptTitle) = MsgBoxResult.Ok Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub NewHelp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'I used a timer because I couldn't think of any other way to make it focus to the title of the new article
        Timer1.Interval = 10
        Timer1.Start()

        LineUp.MybtnFmt.Format_Controls(Me)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        txtTitle.Focus()
    End Sub
End Class
