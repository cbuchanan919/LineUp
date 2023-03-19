Imports System.ComponentModel
Imports System.IO

Public Class frmPrintTC

#Region "Properties"
    ''' <summary>
    ''' directory that the tc comes from
    ''' </summary>
    ''' <returns></returns>
    Private Property TcDir As DirectoryInfo = Nothing
    ''' <summary>
    ''' List of files to send to the printer
    ''' </summary>
    ''' <returns></returns>
    Private Property TcFiles As New List(Of FileInfo)

    ''' <summary>
    ''' Initial directory of the file dialog
    ''' </summary>
    ''' <returns></returns>
    Private Property InitialDirectory As String = ""
    ''' <summary>
    ''' Current file index (file to copy index)
    ''' </summary>
    ''' <returns></returns>
    Private Property FileIndex As Integer = 0

    ''' <summary>
    ''' Used to get printers from print management
    ''' </summary>
    ''' <returns></returns>
    Private Property MyPrinterMgmt As PrinterMgmt = Nothing

    ''' <summary>
    ''' Manages copying files
    ''' </summary>
    ''' <returns></returns>
    Private Property CopyTask As Task = Nothing
#End Region


#Region "Init & Misc"

    ''' <summary>
    ''' New Print TC Window
    ''' </summary>
    ''' <param name="MyPrinterMgmt">printer management, gets hot folders</param>
    ''' <param name="folderPath">TC file initial directory</param>
    Public Sub New(ByVal MyPrinterMgmt As PrinterMgmt, Optional ByVal folderPath As String = "")
        Me.MyPrinterMgmt = MyPrinterMgmt

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        PopulatePrinters()
        If folderPath = "" Then
            InitialDirectory = Path.Combine(My.Settings.dirPeriodicals, "TC", Date.Today.Year + 1)
            If Not Directory.Exists(InitialDirectory) Then
                InitialDirectory = Path.Combine(My.Settings.dirPeriodicals, "TC", Date.Today.Year)
            End If
        Else
            InitialDirectory = folderPath
            TcDir = New DirectoryInfo(folderPath)
            TcFiles = TcDir.GetFiles("*.pdf").ToList

        End If
    End Sub


    Private Sub frmPrintTC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub frmPrintTC_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        tmrPrint.Stop()
    End Sub

#End Region


#Region "Methods"


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If chkIsPaused.Checked Then
            'unchecking will trigger an event, and that will start the print timer.
            chkIsPaused.Checked = False
        Else
            ConfigureTimerTime(0.01)
        End If


    End Sub

    Private Sub chkIsPaused_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsPaused.CheckedChanged
        If Not chkIsPaused.Checked Then
            ConfigureTimerTime(0.01)
        End If

    End Sub


    ''' <summary>
    ''' configures and starts the timer to wait (udCopyEverySeconds.Value) seconds
    ''' </summary>
    ''' <param name="SecondsToWait">optional: will override ui value</param>
    Private Sub ConfigureTimerTime(Optional SecondsToWait As Double = 0)
        tmrPrint.Stop()
        Dim seconds As Double = udCopyEverySeconds.Value
        If SecondsToWait <> 0 Then
            seconds = SecondsToWait
        End If
        Dim ms As Integer = seconds * 1000
        tmrPrint.Interval = ms

        tmrPrint.Start()
    End Sub


    ''' <summary>
    ''' populates combo box with printers and the desktop
    ''' </summary>
    Private Sub PopulatePrinters()
        cboPrinters.Items.Clear()

        For Each printer As PrinterInfo In MyPrinterMgmt.Printers
            For Each q As PrinterQueueInfo In printer.Queues
                If q.QueueCategory = PrinterCategory.TC_Body And q.QueueHotFolderEnabled Then
                    cboPrinters.Items.Add(q.QueueHotFolderPath)
                End If
            Next
        Next
        cboPrinters.Items.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Production Files"))
        cboPrinters.SelectedIndex = 0
    End Sub


    ''' <summary>
    ''' Loads new TC files into the print window
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSelectFiles_Click(sender As Object, e As EventArgs) Handles btnSelectFiles.Click

        Dim filesDlg As New OpenFileDialog
        filesDlg.InitialDirectory = InitialDirectory
        filesDlg.Multiselect = True

        If filesDlg.ShowDialog = DialogResult.OK Then
            tmrPrint.Stop()
            FileIndex = 0
            chkIsPaused.Checked = True

            Dim sorted As List(Of String) = filesDlg.FileNames.ToList
            sorted.Sort()

            TcFiles = New List(Of FileInfo)
            For Each fn As String In sorted
                TcFiles.Add(New FileInfo(fn))
            Next

            TcDir = TcFiles(0).Directory

            lblFound.Text = $"Selected { TcFiles.Count } of { TcDir.GetFiles.Count } files"

            clbFilesToPrint.DataSource = Nothing
            clbFilesToPrint.Items.Clear()
            clbFilesToPrint.DataSource = TcFiles

        End If


    End Sub

    Private Sub tmrPrint_Tick(sender As Object, e As EventArgs) Handles tmrPrint.Tick

        tmrPrint.Stop()
        Dim OkToContinue = True
        If Not IsNothing(CopyTask) Then
            CopyTask.Wait() 'wait for task if its not finished copying yet.
        End If



        If Not chkIsPaused.Checked Then
            OkToContinue = CopyFiles()
        End If
        If FileIndex > 0 AndAlso FileIndex Mod udPauseAfter.Value = 0 Then
            ' pause printing automatically
            chkIsPaused.Checked = True
            Application.DoEvents()
        End If
        If OkToContinue Then
            ConfigureTimerTime()
        Else
            chkIsPaused.Checked = True
        End If

    End Sub


    ''' <summary>
    ''' copies files to the specified printer (in ui) and moves files to a new "Printed" subfolder
    ''' </summary>
    ''' <returns></returns>
    Private Function CopyFiles() As Boolean
        Dim success As Boolean = False
        If TcFiles.Count > FileIndex Then
            Try
                Dim curFile As FileInfo = TcFiles(FileIndex)
                If curFile.Exists Then
                    curFile.CopyTo(Path.Combine(cboPrinters.SelectedItem, curFile.Name), True)
                    Dim dir As String = Path.Combine(curFile.DirectoryName, "Printed")

                    If chkMoveFiles.Checked Then
                        If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
                        curFile.MoveTo(Path.Combine(dir, curFile.Name))
                    End If

                    clbFilesToPrint.SetItemChecked(FileIndex, True)
                    lblExported.Text = $"Exported {FileIndex + 1} of {TcFiles.Count} files"
                End If
                success = True
                FileIndex += 1 'increment counter
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Copy Files Error")
            End Try

        Else
            'return false?
        End If
        Return success
    End Function


#End Region



End Class