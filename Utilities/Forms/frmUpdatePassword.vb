Imports System.Windows.Forms

Public Class frmUpdatePassword
#Region "Properties"
    Private Property OldPassword As String = ""
    Private Property OkToClose As Boolean = False

#End Region


#Region "init"
    Private Sub frmUpdatePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim meFmt As New FormatControls(True, False, False)
        meFmt.Format_Controls(Me)
        meFmt = Nothing
    End Sub
    Public Sub New(ByVal username As String, ByVal password As String, ByVal errorMsg As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lblUser.Text = username
        OldPassword = password
        txtPassword.Text = password
        txtError.Text = errorMsg


    End Sub
#End Region

#Region "Methods"
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim ErrorMessage As String = ""
        Dim Errors As Boolean = False
        If txtPassword.Text.Length < 3 Then
            ErrorMessage &= "The password must be at least 3 characters long." & vbCrLf
            Errors = True
        End If
        If txtPassword.Text = OldPassword Then
            ErrorMessage &= "Your password hasn't changed. Please update." & vbCrLf
            Errors = True
        End If

        If Errors Then
            MsgBox(ErrorMessage)
        Else
            OkToClose = True
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()


        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        OkToClose = True
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmUpdatePassword_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If OkToClose = True Then

        Else
            e.Cancel = True
        End If
    End Sub

#End Region



End Class
