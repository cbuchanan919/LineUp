

Public Class frmEmailLogViewer

#Region "Properties"
    Private Const split = "                                                                                      " & cSplit
    Private Property sqlConnStr As String = ""
    Private Property logs As EmailLogIO
#End Region

#Region "Init"
    Private Sub frmEmailLogViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        prepGui()
    End Sub
    Public Sub New(ByVal sqlConnStr As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.sqlConnStr = sqlConnStr
        logs = New EmailLogIO(sqlConnStr)

    End Sub

#End Region


#Region "Methods"

    Public Sub prepGui()
        lstLogs.Items.Clear()

        logs.getSentEmails()
        lstLogs.Items.Add("Refresh")
        For Each log In logs.SentEmails.Values
            lstLogs.Items.Add(log.TimeSent & split & log.ID)
        Next
        txtDetails.Clear()
        webLogViewer.Navigate("about:blank")
    End Sub

    Private Sub lstLogs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLogs.SelectedIndexChanged
        If lstLogs.SelectedItems.Count = 1 Then
            Dim itemTxt As String = lstLogs.SelectedItem.ToString

            If itemTxt = "Refresh" Then
                prepGui()
            Else
                Dim id As Integer = -1
                If Integer.TryParse(itemTxt.Split(cSplit)(1), id) Then
                    'id found
                    logs.getAllJobDetails(id)
                    txtDetails.Text = "Details:" & vbCrLf & logs.SentEmails(id).ToString
                    webLogViewer.DocumentText = logs.SentEmails(id).Contents
                End If
            End If


        End If
    End Sub




#End Region

End Class