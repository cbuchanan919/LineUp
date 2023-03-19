Imports System.Windows.Forms

Public Class YearEndAcctDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If txtSave.Text <> "" AndAlso cmbYear.Text <> "" Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            Beep()
            MsgBox("Please select a year, and provide a file path!", MsgBoxStyle.Exclamation)
        End If
        
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnSaveLoc_Click(sender As Object, e As EventArgs) Handles btnSaveLoc.Click
        SaveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        SaveFileDialog1.Filter = ("Comma Separated Values | *.csv")
        SaveFileDialog1.FileName = cmbYear.Text & " Year End Accounting"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtSave.Text = SaveFileDialog1.FileName
        End If


    End Sub

    Private Sub YearEndAcctDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LineUp.MybtnFmt.Format_Controls(Me)
    End Sub
End Class
