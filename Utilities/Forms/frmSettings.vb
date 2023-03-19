''' <summary>
''' this class was designed to be used by the settings class
''' </summary>
Public Class frmSettings



    'Note: this class was designed to be used by the settings class






    Private Sub frmXmlSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If MsgBox("Save Settings?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DialogResult = System.Windows.Forms.DialogResult.OK
            Hide()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Discard Changes?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DialogResult = System.Windows.Forms.DialogResult.Cancel
            Hide()
        End If

    End Sub

    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Dim sb As New Text.StringBuilder
        sb.AppendLine("Tip:" & vbCrLf & "Click on the explanation (italic words) to change them.")
        sb.AppendLine()
        sb.AppendLine("Click the name of the setting to change the category (tab page) that it's organized under.")
        MsgBox(sb.ToString, MsgBoxStyle.Information)
    End Sub
End Class