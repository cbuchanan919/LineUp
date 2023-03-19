Public Class ctrlWeb
    Private Sub ctrlWebBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub





















    Public Property UrlToLoadWhenShown As String = ""
    Public Property PrintWhenLoaded As Boolean = False

    ''' <summary>
    ''' New browser window
    ''' </summary> 
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="UrlToLoadWhenShown"></param>
    Public Sub New(ByVal UrlToLoadWhenShown As String, Optional ByVal PrintWhenLoaded As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.UrlToLoadWhenShown = UrlToLoadWhenShown
        Me.PrintWhenLoaded = PrintWhenLoaded
    End Sub

    Private Sub frmBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If UrlToLoadWhenShown <> "" Then
            Web.Source = New Uri(UrlToLoadWhenShown)
        End If

    End Sub


    Private Sub Web2_NavigationCompleted(sender As Object, e As EventArgs) Handles Web.NavigationCompleted
        NavTxt.Text = Web.CoreWebView2.Source
        If PrintWhenLoaded Then
            Web.CoreWebView2.ExecuteScriptAsync("window.print();")
            PrintWhenLoaded = False
        End If
    End Sub

    Private Sub GoBtn_Click(sender As Object, e As EventArgs) Handles GoBtn.Click
        Navigate(NavTxt.Text)
    End Sub


    Private Sub NavTxt_KeyDown(sender As Object, e As KeyEventArgs) Handles NavTxt.KeyDown
        If e.KeyCode = Keys.Return Then
            Navigate(NavTxt.Text)
        End If
    End Sub
    ''' <summary>
    ''' Navigates to http/https url
    ''' </summary>
    ''' <param name="url"></param>
    Public Sub Navigate(ByVal url As String)
        Dim uri As New UriBuilder()

        'If IO.File.Exists(url) Then
        '    url = "file://" & url
        If Not (url.Contains("http") Or url.Contains("https")) Then
            url = "https://" & url
        End If
        Web.Source = New Uri(url)
        ' Web.CoreWebView2.Navigate(url)
    End Sub


End Class
