Public Class frmImposeCalendars
    Dim allowedChar As String = "1234567890-"
    Public imposeCal As New List(Of PersonalizeRowInfo)


    Private Sub frmImpose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LineUp.MybtnFmt.Format_Controls(Me)

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not e.KeyChar = "-" AndAlso Not e.KeyChar = ";" Then
            e.KeyChar = ""
        End If

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Return Then
            btnImpose.PerformClick()
        End If
    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnImpose_Click(sender As Object, e As EventArgs) Handles btnImpose.Click
        Me.DialogResult = DialogResult.OK
        For Each selCalendar In TextBox1.Text.Split(";")
            If selCalendar <> "" AndAlso selCalendar <> " " Then
                Dim prodNum As String = ""
                Select Case True
                    Case rdb8101.Checked
                        prodNum = "8101"

                    Case rdb7427.Checked
                        prodNum = "7427"

                    Case rdb6880.Checked
                        prodNum = "6880"

                    Case rdb6120.Checked
                        prodNum = "6120"

                End Select
                Dim cal As New PersonalizeRowInfo("100000", prodNum, selCalendar, "1", "Test Order", New List(Of String), New List(Of String), "", "")
                cal.configureFilePaths(LineUp.CBConfig)
                Dim errors As New List(Of String)
                If cal.imposeCalendar(LineUp.CBConfig, errors, True) Then
                    If IO.File.Exists(cal.bodyFilePath) Then
                        Diagnostics.Process.Start("Explorer.exe", "/select," & cal.bodyFilePath)
                    End If
                Else
                    Dim s As New Text.StringBuilder
                    s.AppendLine("The following errors occured:")
                    For Each err As String In errors
                        s.AppendLine(err)
                    Next
                    MsgBox(s.ToString)
                End If

            End If
        Next
    End Sub

End Class