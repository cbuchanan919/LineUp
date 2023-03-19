Imports System.IO
Public Class frmWebr

    'Public Property UrlToLoadWhenShown As String = ""
    'Public Property PrintWhenLoaded As Boolean = False


    Private web As ctrlWeb = Nothing
    ''' <summary>
    ''' New browser window
    ''' </summary> 
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 

        web = New ctrlWeb()
        web.Parent = Me
        web.Dock = DockStyle.Fill
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="UrlToLoadWhenShown"></param>
    Public Sub New(ByVal UrlToLoadWhenShown As String, Optional ByVal PrintWhenLoaded As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Me.UrlToLoadWhenShown = UrlToLoadWhenShown
        'Me.PrintWhenLoaded = PrintWhenLoaded

        web = New ctrlWeb(UrlToLoadWhenShown, PrintWhenLoaded)
        web.Parent = Me
        web.Dock = DockStyle.Fill

    End Sub

    Private Sub frmBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class