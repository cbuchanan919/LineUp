Public Class frmSqlConnectionUtilities
#Region "Properties"
    Public Property SqlInfo As SQLConnectionUtilities = Nothing
#End Region

#Region "Init"
    Public Sub New(ByVal sqlConnStr As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim parser As New System.Data.SqlClient.SqlConnectionStringBuilder(sqlConnStr)

        txtSqlServer.Text = parser.DataSource
        txtSqlDatabase.Text = parser.InitialCatalog
        If parser.IntegratedSecurity = False Then
            txtSqlUser.Text = parser.UserID
            txtSqlPassword.Text = parser.Password
        End If


    End Sub


    Private Sub frmSqlConnectionUtilities_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        SqlInfo = New SQLConnectionUtilities(txtSqlServer.Text, txtSqlUser.Text, txtSqlPassword.Text, txtSqlDatabase.Text)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub



#End Region


End Class