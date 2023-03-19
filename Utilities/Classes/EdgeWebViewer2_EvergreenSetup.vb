Imports System.Net
Imports Microsoft.Win32
Imports System.IO

Public Class EdgeWebViewer2_EvergreenSetup

#Region "Properties"
    Private Property EvergreenDownloadLoc As String = "https://go.microsoft.com/fwlink/p/?LinkId=2124703"
    Private Property Reg64Loc As String = "HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}"
    Private Property Reg32Loc As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}"
#End Region

#Region "Init"
    Public Sub New(ByVal autoInstall As Boolean)
        If autoInstall Then
            ' InstallEvergreenWebViewer2()
            If Not EvergreenIsInstalled() Then
                InstallEvergreenWebViewer2()
            End If
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Checks the registry to see if evergreen webview2 is installed
    ''' </summary>
    ''' <returns></returns>
    Friend Function EvergreenIsInstalled() As Boolean
        If Environment.Is64BitOperatingSystem Then
            Return CheckRegistryKeyExists(Reg64Loc)
        Else
            Return CheckRegistryKeyExists(Reg32Loc)
        End If
    End Function
    Private Function CheckRegistryKeyExists(ByVal regLoc As String) As Boolean
        Dim regKey = My.Computer.Registry.GetValue(regLoc, "Name", Nothing)
        If regKey Is Nothing Then
            Return False
            'MsgBox("Registry key does not exist!")
        Else
            Return True
            'MsgBox("Registry key exists.")
        End If
    End Function
    Friend Function InstallEvergreenWebViewer2() As Boolean
        Dim dir As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "cbEdgeSetup")
        If Not Directory.Exists(dir) Then
            Directory.CreateDirectory(dir)
        End If
        Dim localFP As String = IO.Path.Combine(dir, "WebView2Setup.exe")
        Using client As New WebClient()
            client.DownloadFile(EvergreenDownloadLoc, localFP)

        End Using
        If File.Exists(localFP) Then


            Dim procStartInfo As New ProcessStartInfo
            Dim procExecuting As New Process
            'Dim arg As String = "/silent /install"
            Dim quote As String = Chr(34)

            With procStartInfo

                .UseShellExecute = True
                .FileName = localFP
                '.Arguments = quote & arg & quote
                .WindowStyle = ProcessWindowStyle.Normal
                .Verb = "runas" 'add this to prompt for elevation
            End With

            procExecuting = Process.Start(procStartInfo)
            procExecuting.WaitForExit()

            Return True
        Else
            Return False
        End If
    End Function
#End Region

End Class
