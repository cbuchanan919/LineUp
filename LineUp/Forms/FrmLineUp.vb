Imports Falcon.PageComposer

'Imports iTextSharp.text.pdf
'Imports iTextSharp.text

Imports System
Imports System.IO
Imports System.ComponentModel

'currently used to create barcodes
'Imports System.Drawing
'Imports System.Drawing.Imaging
'Imports System.Text.RegularExpressions ' Used to find the page size in Cover & Body
Imports System.Xml



'Imports Microsoft.Office.Interop

Public Class LineUp

#Region "Variables / Properties"

    Public Property LineUpLoaded As Boolean = False

    Protected Friend Property sqlInfo As SQLConnectionUtilities

    Public Property MyPrinterMgmt As PrinterMgmt

    Private Property MyJQRowIO As JQRowIO

    Private Property MyJQNoteIO As JQNoteIO = Nothing

    Public Property MyUvProductInfoIO As UvProductInfoIO = Nothing

    Public Property MyJQProjectDirIO As JQProjectDirIO = Nothing

    Public Property MyJQProductionIO As JQProductionIO = Nothing

    Private Property MyJQTimeUpdateIO As JQTimeUpdateIO

    Private Property MyJQBarcodeIO As JQBarcodeIO

    Private Property MyPersonalizeIO As PersonalizeIO


    Private Property PicNotFoundPath As String = My.Settings.dirResources & "PicNotFound.jpg"

    Public Property MybtnFmt As FormatControls

    Private Property LastFilePath As String = ""

    Public Property Log As New frmError("Lineup Error Log")

    ''' <summary>
    ''' Stores jobQ picture x,y location
    ''' </summary>
    Private piclocation(1) As Integer

    ''' <summary>
    ''' used to store the picture file path of the product entered
    ''' </summary>
    ''' <returns></returns>
    Private Property picPath As String = ""


    Private Property currentQJob As JQRowInfo = Nothing

    Private Property lastQJobPdfShown As JQRowInfo = Nothing






    'personalized data table & binding source
    'Public Property DesignsDS As New DataSet
    Private Property DesignsBindingsource As New BindingSource

    ''' <summary>
    ''' Dictionary of the Mx Order Number, to its info
    ''' </summary>
    Public Property mxOrdersDict As New Dictionary(Of Integer, PersonalizeMxInfo)

    Public Property currentMxOrder As New PersonalizeMxInfo

    'keeps track of which mx is currently displaying
    Private Property WhichMX As Integer = 0


    ''' <summary>
    ''' Config info for custom imposition of calendars
    ''' </summary>
    Public Property CBConfig As New PcConfig

    ''' <summary>
    ''' List of currently selected Custom ID Rows.
    ''' </summary>
    Public Property selectedPersonalizedIDs As New List(Of PersonalizeRowInfo)


    ''' <summary>
    ''' keeps track of Personalized dgv Cell data (value) currently edited
    ''' </summary>
    ''' <returns></returns>
    Private Property CellContents As String = ""


    ''' <summary>
    ''' records if the personalized tab has been displayed. Helps with responsiveness?
    ''' </summary>
    Private Property wasPersonalizedTabDisplayed As Boolean = False

    ''' <summary>
    ''' pauses the loading of pdf's / updating search while personalize jobq is loading.
    ''' </summary>
    Private Property PersonalizeJobQisLoading As Boolean = False



    ''' <summary>
    ''' stores the info of each customer from the customer data table.
    ''' </summary>
    Private Property PersonalizedCustomerInfo As Personalize_custDataTable = Nothing




    ''' <summary>
    ''' boolean keeps the xml reading to a minimum
    ''' </summary>
    Public Property xmlCustomOrdersHaveBeenRead() As Boolean = False


#End Region


#Region "Misc. & Load & Exit"


    Public Sub New()

        Dim edge As New EdgeWebViewer2_EvergreenSetup(True) 'leave this line at the beginning of the new initalization

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            pdfInvoice.EnsureCoreWebView2Async()

            GetImpositionSettings()

            MybtnFmt = New FormatControls(True, False, True)
            MybtnFmt.Format_Controls(Me)

            ' loads info from sql. 
            loadSql()
            ' frmSettings.ReadSettings()

            MyJQProjectDirIO = New JQProjectDirIO()

            MyJQProductionIO = New JQProductionIO



            PicNotFoundPath = My.Settings.dirResources & "PicNotFound.jpg"

            ' MsgBox("settings reloaded." & vbCrLf & "XML Path: " & My.Settings.XMLpath & vbCrLf & vbCrLf & "Loading prodUVinfo")




            'sets the text in the text boxes to the search phrase
            txtJobQSearch.Text = cSearch
            txtPersonalizeSearch.Text = cSearch
            UpdateStatus("Loading...", False)
            ' MsgBox("Step 3")

            ' Hides the order info tab (at the bottom of screen)
            TabControlInfo.TabPages.Remove(TabOrderInfo)






            MyPersonalizeIO = New PersonalizeIO(My.Settings.MxCustomPath, Me)


            ' Gets the default original printer
            MyPrinterMgmt.OriginalPrinter = MyPrinterMgmt.GetDefaultPrinter()


            ' Enables the timer to check for updated jobq
            TimerCheckForUpdate.Enabled = True


            ' loads the file path used for the default directory of print & ebooks
            LastFilePath = Path.GetDirectoryName(My.Settings.PrEBookDir)
            LastFilePath = Path.GetDirectoryName(LastFilePath)

            UpdateStatus("", False)

            ' XMLReadCustomOrders()

            TmrUpdatePQ.Start()


            If BackupIsNeeded() Then
                CreateDGVBackup(False)
            End If


            showVersion()


            If My.Computer.Name.ToLower = "pc-cb" Then
                'shows up only if its on my computer
                ForCbOnlyTSItem.Visible = True

            Else
                ForCbOnlyTSItem.Visible = False
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        MyJQRowIO.formatCells("")

    End Sub


    Private Sub LineUp_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        LineUpLoaded = True
    End Sub



    ''' <summary>
    ''' Shows the current version in the task bar
    ''' </summary>
    Private Sub showVersion()
        Try
            Dim m_xmld = New XmlDocument()
            m_xmld.Load(Application.ExecutablePath & ".manifest")
            ToolStripVersion.Text = "v" & m_xmld.ChildNodes.Item(1).ChildNodes.Item(0).Attributes.GetNamedItem("version").Value
            m_xmld = Nothing
        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try
    End Sub


    Private Sub Form1_resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pbProducts.Parent = Me

        pbProducts.BringToFront()

        'records current picture location on mouse over
        piclocation(0) = Me.Size.Width - 115
        piclocation(1) = Me.Size.Height - 160
        pbProducts.SetBounds(piclocation(0), piclocation(1), 100, 100)
        pbProductsClicked = False
    End Sub


    ''' <summary>
    ''' Loads the different datagridviews from sql.
    ''' </summary>
    ''' <returns></returns>
    Private Function loadSql() As Boolean
        Dim success As Boolean = True
        Try
            '----------------------------------- Init sqlInfo from my.settings ---------------------------------------------------------
            Dim errors As String = ""
            Dim sqlServer As String = My.Settings.SqlServer
            Dim usr As String = My.Settings.SqlUser
            Dim shibboleth As String = My.Settings.SqlPassword
            Dim dataBase As String = My.Settings.SqlLineupDatabase
            sqlInfo = New SQLConnectionUtilities(sqlServer, usr, shibboleth, dataBase, Nothing)
            'Console.WriteLine(sqlInfo.sqlConnStr)
            Try

                '------------------------------------ UV Connection ------------------------------------ 
                MyUvProductInfoIO = New UvProductInfoIO("", "", sqlInfo, UvProductInfoIO.InfoLoadOptions.UvAsThread)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "loadSql")
            End Try

            '----------------------------------- Load Settings From SQL-------------------------------------------------
            Settings = New Utilities.Settings(sqlInfo.sqlConnStr, My.Settings.PropertyValues)
            Settings.LoadSettingsFromSQL()
            Settings.UpdateMySettingValueFromAllSettings()



            '----------------------------------- JobQ ---------------------------------------------------------
            MyJQRowIO = New JQRowIO(sqlInfo.sqlConnStr, dgvJobQ, MyUvProductInfoIO.clone) 'BTP_JobQ
            MyJQRowIO.LoadSql(errors)
            MyJQRowIO.formatCells(errors)

            '----------------------------------- loads the job q notes from sql -----------------------------------
            MyJQNoteIO = New JQNoteIO(sqlInfo.sqlConnStr)
            MyJQNoteIO.LoadAllNotes()

            '----------------------------------- sql time update info ---------------------------------------------------------
            MyJQTimeUpdateIO = New JQTimeUpdateIO(sqlInfo.sqlConnStr)
            MyJQTimeUpdateIO.UpdateLocalUpdateTimeFromSql()


            '----------------------------------- loads the printers from sql -----------------------------------
            MyPrinterMgmt = New PrinterMgmt(sqlInfo.sqlConnStr)
            MyPrinterMgmt.updatePrinters()
            ShowPrinterInfoButtons()

            '----------------------------------- loads the barcodes from sql -----------------------------------
            MyJQBarcodeIO = New JQBarcodeIO(sqlInfo.sqlConnStr)
            MyJQBarcodeIO.GetBarcodesFromSql()




            '---------- populates the days since order was placed ----------
            Dim sb As New Text.StringBuilder
            sb.Append("Current: " & Math.Round(MyJQRowIO.GetDaysSinceOrderPlaced(JQRowIO.DisplayOptions.DisplayCurrent, Date.Today.Year), 0) & " Days             ")
            sb.Append(Date.Today.Year.ToString & "'s History: " & Math.Round(MyJQRowIO.GetDaysSinceOrderPlaced(JQRowIO.DisplayOptions.DisplayArchive, Date.Today.Year), 0) & " Days")
            CountsMenuItem.Text = sb.ToString

            If errors <> "" Then
                MsgBox(errors, MsgBoxStyle.Critical)
                success = False
            End If
        Catch ex As Exception
            success = False
            MsgBox(ex.Message)
        End Try
        Return success
    End Function

    ''' <summary>
    ''' shows the printer edit buttons
    ''' </summary>
    Private Sub ShowPrinterInfoButtons()
        FlowPrinterInfo.Controls.Clear()

        For Each p As PrinterInfo In MyPrinterMgmt.Printers
            FlowPrinterInfo.Controls.Add(p.CreateEditButton)
            ' FlowPrinterInfo.Controls.Add(New Button())

        Next
    End Sub


    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try

            MyPrinterMgmt.ResetToOriginalPrinter()

            MyJQRowIO.ClearLoadedProductionFiles()
            pdfInvoice.Visible = False 'this line keeps it from erroring out when closing program

            'deletes the .pdf & .png files in the dirMxOrders folderpath
            If Directory.Exists(My.Settings.dirMxOrders) Then
                Dim dirInfo As String() = Directory.GetFiles(My.Settings.dirMxOrders)
                Dim Count As Integer = 0
                For Each myFile As String In dirInfo
                    If myFile.Contains(".pdf") Or myFile.Contains(".png") Then
                        If File.Exists(myFile) Then
                            Try
                                File.Delete(myFile)
                            Catch ex As Exception
                                'MsgBox(ex.Message)
                                'Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                            End Try
                        End If
                    End If
                Next


            End If
            e.Cancel = False
        Catch ex As Exception
            MsgBox(ex.Message)
            'Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try


    End Sub

    ''' <summary>
    ''' Updates the status bar with message. If it's an error, defines duration of flashing
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <param name="isError"></param>
    Protected Friend Sub UpdateStatus(ByVal Message As String, ByVal isError As Boolean)
        If isError Then
            Status1.Text = cStatusA & Message
            Status1.ForeColor = Color.Red
            StatusBackColor = Color.DarkOrange
        Else
            Dim msg As String = ""
            If Message = "" Then
                msg = ""
            Else
                msg = " - " & Message
            End If
            Status1.Text = cStatusGood & msg
            Status1.ForeColor = Color.Black
            StatusBackColor = Color.SkyBlue
        End If

        statusBlink = 5
        tmrResetStatus.Start()
    End Sub

    Private statusBlink As Integer = 0
    Private StatusBackColor As Color

    Private Sub tmrResetStatus_Tick(sender As Object, e As EventArgs) Handles tmrResetStatus.Tick
        If Status1.Text = cStatusGood Then
            'stops blinking.
            statusBlink = 0
            tmrResetStatus.Stop()
            Status1.BackColor = Color.Azure
        ElseIf statusBlink >= 0 Then

            If GenUtil.IsEven(statusBlink) Then
                Status1.BackColor = Color.Azure
            Else
                Status1.BackColor = StatusBackColor
            End If
            statusBlink -= 1
        Else
            tmrResetStatus.Stop()
        End If

    End Sub



    Private Sub TabChanged(sender As Object, e As EventArgs) Handles LineUpTabCtrl.SelectedIndexChanged
        'this sub handles the switching of tabs. If there's a product that's selected, it will try to load it upon tab switch. (depends on which tab is selected)

        TmrUpdatePQ.Stop()
        TmrUpdatePQ.Interval = 600000 '10 minutes
        If MyPersonalizeIO.AllPersonalizeRows.Count = 0 Then
            TmrUpdatePQ.Interval = 1000 '1 second
            'designs ds is required to run this part
        End If

        'Hides buttons, to be shown when the correct tab is opened
        btnRefresh.Visible = False
        btnLoadProd.Visible = False

        pbProducts.Visible = True
        TabControlInfo.TabPages.Remove(TabOrderInfo)
        If Not TabControlInfo.Contains(TabProdInfo) Then
            TabControlInfo.TabPages.Add(TabProdInfo)
        End If
        Select Case LineUpTabCtrl.SelectedIndex


            Case 0 'JobQ
                btnRefresh.Visible = True
                ' btnImportJobQ.Visible = True

                'clear production tab
                'MyJQRowIO.ClearLoadedProductionFiles()


            Case 1 'Production
                btnLoadProd.Visible = True

                Application.DoEvents()
                'tries to load the pdfs that correspond to the txtProdNumPrint.text
                LoadProductionPDF()


            Case 2 'Personalize

                tmrPersonalizeDisplay.Interval = 500
                tmrPersonalizeDisplay.Enabled = True
                wasPersonalizedTabDisplayed = True
                'TabOrderInfo.Visible = True
                'TabProdInfo.Visible = False
                pbProducts.Visible = False
                TabControlInfo.TabPages.Remove(TabProdInfo)
                If Not TabControlInfo.Contains(TabOrderInfo) Then
                    TabControlInfo.TabPages.Add(TabOrderInfo)
                End If

                TmrUpdatePQ.Interval = 30000 '30 seconds


            Case Else

        End Select
        TmrUpdatePQ.Start()


    End Sub


    Private Sub tmrPersonalizeDisplay_Tick(sender As Object, e As EventArgs) Handles tmrPersonalizeDisplay.Tick
        tmrPersonalizeDisplay.Enabled = False
        XMLReadCustomOrders()
        'UpdatePJobqDgv()
    End Sub


    'used to record how many key presses left I have from the barcode reader
    Private meKeyBarcode As Boolean = False
    Private meKeyDownString As String = ""
    Private CancelNextKeyPress As Integer = 0
    Private RefreshXMLTimer As Boolean = False


    Private Sub meKeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If CancelNextKeyPress > 0 Then
            CancelNextKeyPress -= 1

            'frmErrorLog.txtErrLog.Text &= vbCrLf & vbCrLf & "Suppressed" & vbCrLf & vbCrLf


        Else

            If meKeyBarcode = False Then

                Select Case e.KeyCode

                    'goes to the search form
                    Case Keys.F
                        If e.Modifiers = Keys.Control Then
                            Select Case LineUpTabCtrl.SelectedIndex

                                Case 0  'JobQ
                                    txtJobQSearch.Focus()
                                    Beep()
                                    txtJobQSearch.SelectAll()



                                Case 1 'Production



                                Case 2 'Personalize
                                    txtPersonalizeSearch.Focus()
                                    Beep()
                                    txtPersonalizeSearch.SelectAll()
                            End Select
                        End If

                    Case Keys.Back
                        'ability to use control + backspace to remove a word instead of a letter
                        If My.Computer.Keyboard.CtrlKeyDown Then
                            SendKeys.Send("^+{LEFT}{BACKSPACE}")
                            e.SuppressKeyPress = True
                            Me.Focus()
                        End If


                    Case Keys.Oemtilde 'used to record from a barcode
                        meKeyBarcode = True
                        meKeyDownString = ""
                        Beep()
                        e.SuppressKeyPress = True
                        Me.Focus()
                        'UpdatePQFilter("")
                        tmrSearchPersonalize.Stop()
                        tmrSearchPersonalize.Start()
                        If dgvPersonalizeJobQ.IsCurrentCellInEditMode Then
                            dgvPersonalizeJobQ.EndEdit()
                        End If

                    Case Keys.Q
                        If Control.ModifierKeys = Keys.Control Then
                            Me.Close()
                        End If

                End Select


            End If


        End If

    End Sub


    Private Sub meKeyPress_ForBarcode(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

        'this handles getting key data from the barcode scanner. I put it under me.keypress because of ease of getting the characters entered
        'mekeybarcode is set to true under Private Sub meKeyDown.

        If meKeyBarcode = True Then
            Try
                meKeyDownString = meKeyDownString.Replace("~", "")
                Select Case e.KeyChar

                    'if its the ending tilde, then it tries to find the info
                    Case "~"
                        meKeyBarcode = False
                        e.Handled = True

                        'tries to suppress the next keypress (usually return)
                        CancelNextKeyPress = 1

                        'if it's numeric, then it's an ID
                        If IsNumeric(meKeyDownString) Then
                            LineUpTabCtrl.SelectedIndex = 2 'PersonalizeJobQ
                            txtPersonalizeSearch.Text = cSearch
                            'XMLReadCustomOrders()
                            Application.DoEvents()
                            ' txtPersonalizeSearch.Text = meKeyDownString
                            UpdatePQFilter(meKeyDownString)
                            dgvPersonalizeJobQ.ClearSelection()
                            dgvPersonalizeJobQ.Rows(0).Selected = True


                        Else
                            'updates the status
                            If LineUpTabCtrl.SelectedIndex = 2 Then 'PersonalizedJobQ

                                Dim ReturnedBarcode As JQBarcodeInfo = MyJQBarcodeIO.GetBarcode(meKeyDownString)
                                Beep()
                                If dgvPersonalizeJobQ.Rows.Count < 3 Then
                                    dgvPersonalizeJobQ.Rows(0).Selected = True
                                End If
                                If Not IsNothing(ReturnedBarcode) Then
                                    If dgvPersonalizeJobQ.SelectedRows.Count = 1 Then

                                        'ReturnedString = meKeyDownString
                                        For Each selRow As PersonalizeRowInfo In selectedPersonalizedIDs
                                            selRow.AddStatusToHistory()
                                            selRow.PrintStatus = ReturnedBarcode.BarcodeText
                                        Next
                                        MyPersonalizeIO.WritePersonalizedXml()
                                        'FocusTimer.Interval = 500
                                        RefreshXMLTimer = True
                                        'FocusTimer.Start()

                                        'Dim strPersonalizeSearch As String = txtPersonalizeSearch.Text
                                        'XMLReadCustomOrders()
                                        'txtPersonalizeSearch.Text = strPersonalizeSearch
                                    End If


                                End If
                            End If

                        End If




                    Case Else
                        meKeyDownString &= e.KeyChar
                        e.Handled = True
                End Select

            Catch ex As Exception
                Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            End Try
            If meKeyDownString.Count >= 6 Then
                meKeyBarcode = False
                meKeyDownString = ""

            End If

        End If




    End Sub


    Private Sub SearchFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtJobQSearch.KeyDown, txtPersonalizeSearch.KeyDown

        'removes the ' or " from being able to be entered as text
        Select Case e.KeyCode
            Case Keys.OemQuotes
                e.SuppressKeyPress = True
                Beep()
                e.Handled = True


        End Select
        'If e.KeyCode = Keys.OemQuotes Then

        'End If

    End Sub


    Private Function BackupIsNeeded() As Boolean
        Dim isNeeded As Boolean = True
        Dim BackupFP As String = Path.Combine(My.Settings.dirResources, cBackups)
        If Directory.Exists(BackupFP) Then
            Dim di As New DirectoryInfo(BackupFP)
            Dim foundFiles As New List(Of FileInfo)
            foundFiles.AddRange(di.GetFiles)
            'goes through all the files in the backup folder, and looks at the creation time and compares it with current one.
            For Each myFile As FileInfo In foundFiles
                If myFile.CreationTime > Date.Today.AddDays(-7) Then
                    'since the creation time is within 7 days, it won't create a backup.
                    isNeeded = False
                End If
            Next
        End If
        Return isNeeded
    End Function
    Private Sub CreateJobQBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateJobQBackupToolStripMenuItem.Click
        CreateDGVBackup(True)
    End Sub
    ''' <summary>
    ''' Creates the backup of the jobQ and JobqHistory
    ''' </summary>
    Private Sub CreateDGVBackup(ByVal ShowMessage As Boolean)
        Try


            Dim results As New Text.StringBuilder
            Dim backupFolder As String = Path.Combine(My.Settings.dirResources, cBackups) 'path to the backup folder
            Dim fn As String = "Backup Of JobQ - " & Date.Today.ToString("yyyy.MM.dd") & ".xml"
            If Not Directory.Exists(backupFolder) Then
                Directory.CreateDirectory(backupFolder)
            End If
            If MyJQRowIO.CreateXmlBackup(Path.Combine(backupFolder, fn)) Then
                results.AppendLine(Path.Combine(backupFolder, fn))
            End If


            ''This is an if block that copies the personalize tab to a new file for this session
            Try
                Dim CopyPath As String = My.Settings.MxCustomPath.Replace(".xml", " - Backup " & Today.ToString("yyyy MM dd") & ".xml")
                If File.Exists(CopyPath) Then
                    File.Delete(CopyPath)
                End If
                File.Copy(My.Settings.MxCustomPath, CopyPath)
                My.Settings.MxCustomPath = CopyPath
            Catch ex As Exception

            End Try

            Dim msgresults As String = "The following backed up successfully: " & vbCrLf & results.ToString
            If ShowMessage Then
                MsgBox(msgresults)
            Else
                Log.addError(msgresults, System.Reflection.MethodInfo.GetCurrentMethod.ToString, False)
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub





#End Region


#Region "Tab Job Queue"


    Private Sub dgvJobQ2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvJobQ.CellContentClick
        'blank...
    End Sub

    Private Sub dgvJobQ2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvJobQ.CellEndEdit
        Try

            Dim errors As String = ""
            'if it's not empty, then it will run cellCheck. (Tries to do different things based upon which column it's in.)
            If IsNothing(dgvJobQ.CurrentRow.Cells(col_JobID).Value) Then
                'blank row,
            ElseIf Not IsDBNull(dgvJobQ.CurrentCell.Value) AndAlso dgvJobQ.CurrentCell.Value IsNot Nothing AndAlso dgvJobQ.CurrentCell.Value.ToString <> "" Then
                MyJQRowIO.NewRowCheck(MyJQProductionIO)
                'ElseIf Not sqlJobQIO.AllJobQRows.ContainsKey(dgv.CurrentRow.Cells(col_JobID).Value) Then

            Else
                'fixes the bug that if someone deletes a cell, it won't update the table
                If MyJQRowIO.dgv.CurrentRow.Cells(col_JobID).Value.ToString <> "-99" Then
                    MyJQRowIO.updateSql(errors)
                End If

            End If
            MyJQTimeUpdateIO.UpdateSqlWithNewTime(errors)
            MyJQRowIO.formatCells()

            If errors <> "" Then
                MsgBox(errors, MsgBoxStyle.Critical)
                Log.addError(errors, "dgvJobQ2_CellEndEdit", True)

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "dgvJobQ2_CellEndEdit")
        End Try

    End Sub


    Private Sub dgvJobQ2KeyDown(sender As Object, e As KeyEventArgs) Handles dgvJobQ.KeyDown
        If Not dgvJobQ.SelectedRows.Count > 0 Then
            Select Case e.KeyCode
                Case Keys.Delete
                    If MsgBox("Are you sure you want to delete the selected cells?", MsgBoxStyle.OkCancel, "Delete?") = MsgBoxResult.Ok Then

                        For Each delCell As DataGridViewCell In dgvJobQ.SelectedCells
                            'MsgBox(delCell.OwningColumn.ValueType.ToString)
                            If delCell.OwningColumn.ValueType = GetType(DateTime) Then
                                delCell.Value = cNullDate
                            ElseIf delCell.OwningColumn.Name <> col_JobID Then
                                delCell.Value = ""
                            End If

                        Next
                        'updates the spreadsheet when someone deletes a row in jobQ
                        MyJQRowIO.updateSql()
                        MyJQTimeUpdateIO.UpdateSqlWithNewTime()
                        Beep()
                    End If
            End Select

        End If


    End Sub

    ' Private Sub JobQ_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles JobQ.CellClick, JobQ.CellContentClick
    Private Sub JobQ2_SelectionChanged(sender As Object, e As EventArgs) Handles dgvJobQ.SelectionChanged

        If LineUpLoaded Then
            Dim jobs As List(Of JQRowInfo) = MyJQRowIO.GetSelectedJobs()
            If jobs.Count > 0 Then
                Dim job As JQRowInfo = jobs(0)

                If Not IsNothing(job) Then
                    Product_Changed(job)
                End If
            Else
                'nothing - keeps it from crashing
            End If

        End If

    End Sub


    Private Sub dgvJobQ2sorted(sender As Object, e As System.EventArgs) Handles dgvJobQ.Sorted

        'after sorting the cell formats are lost, reapplies colors & whatnot
        MyJQRowIO.formatCells()

    End Sub


    Private Sub JobQ_RemoveRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles dgvJobQ.UserDeletingRow


        e.Cancel = MyJQRowIO.RemoveDGVRow(True)
        MyJQTimeUpdateIO.UpdateSqlWithNewTime()
    End Sub


    Private Sub btnSearchProds_Click(sender As Object, e As EventArgs) Handles btnSearchProds.Click

        Dim prods As New frmProdSearch
        prods.txtSearch.Text = txtJobQSearch.Text
        If Not My.Computer.Keyboard.CtrlKeyDown Then
            If prods.ShowDialog = DialogResult.OK Then
                Dim jobs As New List(Of JQRowInfo)
                For Each row As DataGridViewRow In prods.dgvSearch.SelectedRows
                    'sets standard new job settings
                    Dim job As New JQRowInfo

                    job.ItemNumber = row.Cells(0).Value
                    job.Title = row.Cells(1).Value
                    job.Description = row.Cells(6).Value
                    job.Status = "Not Started"
                    job.OrderPlaced = Date.Today
                    job.DueDate = Date.Today.AddDays(14)
                    job.SubmittedBy = System.Environment.UserName
                    job.LastUpdated = Now
                    jobs.Add(job)
                Next
                Try
                    'it tries to add, update, format, then remove the rows. (it's in that order in case of failure, so it doesn't lose the row)
                    MyJQRowIO.AddUpdateJobsToSqlTable(jobs, InsertOrUpdate.InsertInto)
                    MyJQRowIO.LoadSql()
                    MyJQRowIO.formatCells()

                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try

                txtJobQSearch.Text = cSearch

            End If
            prods.Close()
        Else
            prods.Show()
        End If


        'frmProdSearch.Show()
        'frmProdSearch.Focus()

    End Sub





    ''' <summary>
    '''  Sets the production cost per info found. Finds the cost of the single item, then multiplies it by the quantity wanted.
    ''' </summary>
    ''' <param name="Quantity"></param>
    ''' <remarks></remarks>
    Private Sub ProductionCost(ByVal Quantity As Integer)
        Try

            If Not IsNothing(currentQJob.uvProdInfo) Then
                'sets the production cost per info found. Finds the cost of the single item, then multiplies it by the quantity wanted.
                Quantity = Quantity + ((Quantity / 100) * 10) 'gives 10% extra

                Dim CostPer As Double = 0
                Dim TotalCost As Double = 0
                Dim PageCost As Double = 0
                Dim PaperCost As Double = 0
                Dim CoverCost As Double = 0
                Dim ColorClickCost As Double = My.Settings.Cost_ColorClick.Split(" ").First
                Dim BlackClickCost As Double = My.Settings.Cost_BlackClick.Split(" ").First
                Dim LaminateCost As Double = My.Settings.Cost_Laminate.Split(" ").First
                Dim ProdType As ProductCategory = ProductCategory.Not_Set 'MyProductionIO.CurrentExport.ProductType
                If currentQJob.ProductionFiles.Count > 0 Then
                    ProdType = currentQJob.ProductionFiles(0).ProductType
                End If

                Dim PgCt As Integer = currentQJob.uvProdInfo.PageCt

                'I plan to figure out the cost of a single book, and multiply that by quantity wanted.
                If ProdType = ProductCategory.Book Or ProdType = ProductCategory.Book_12x9 Or ProdType = ProductCategory.Full_Bleed_Book Then


                    If PgCt >= 200 Then
                        PaperCost = My.Settings.Cost_20lbPaper.Split(" ").First
                    Else
                        PaperCost = My.Settings.Cost_24lbPaper.Split(" ").First
                    End If

                    'gets the pages in the book and divides it by 2, to represent sheets, multiplies it by the cost to get it per layer, divides it by 4 to get per book.
                    PaperCost = (PaperCost * (PgCt / 2)) / 4
                    BlackClickCost = (BlackClickCost * PgCt) / 4

                    CoverCost = My.Settings.Cost_BookCov.Split(" ").First

                    CoverCost = (CoverCost + ColorClickCost + LaminateCost) / 2

                    CostPer = (PaperCost + BlackClickCost + CoverCost)
                    TotalCost = CostPer * Quantity
                    'MsgBox(CostPer.ToString & vbCrLf & TotalCost.ToString)
                    txtProductionCost.Text = CostPer.ToString("c") & " (" & TotalCost.ToString("c") & ")"
                    'PageCost = PageCost + (CoverCost(0) * Quantity)

                ElseIf ProdType = ProductCategory.Pamphlet Then
                    'figures cost for the pamphlet
                    PaperCost = (My.Settings.Cost_8_5x11_20lbPaper.Split(" ").First * (PgCt / 4))
                    BlackClickCost = (BlackClickCost * (PgCt / 2))
                    CoverCost = ((My.Settings.Cost_PamCov.Split(" ").First + ColorClickCost) / 2)

                    CostPer = PaperCost + CoverCost + BlackClickCost
                    TotalCost = CostPer * Quantity
                    txtProductionCost.Text = CostPer.ToString("c") & " (" & TotalCost.ToString("c") & ")"

                ElseIf ProdType = ProductCategory.Mini_Pamphlet Then
                    'figures cost for the mini-pamphlet
                    PaperCost = ((My.Settings.Cost_8_5x11_20lbPaper.Split(" ").First * (PgCt / 4)) / 2)
                    BlackClickCost = (BlackClickCost * (PgCt / 4))
                    CoverCost = ((My.Settings.Cost_PamCov.Split(" ").First + ColorClickCost) / 4)

                    CostPer = PaperCost + CoverCost + BlackClickCost
                    TotalCost = CostPer * Quantity
                    txtProductionCost.Text = CostPer.ToString("c") & " (" & TotalCost.ToString("c") & ")"

                Else
                    txtProductionCost.Text = cUnknown
                End If
            End If

        Catch ex As Exception
            txtProductionCost.Text = cUnknown
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try


    End Sub








    Private dataErrors As New List(Of Exception)


    Private Sub DataErrorTimer_Tick(sender As Object, e As EventArgs) Handles DataErrorTimer.Tick
        DataErrorTimer.Stop()
        Dim sb As New Text.StringBuilder
        Try
            If dataErrors.Count > 0 Then
                sb.AppendLine("The following error(s) occured: " & vbCrLf)
                For Each ex As Exception In dataErrors
                    sb.AppendLine(ex.Message)
                Next
                MsgBox(sb.ToString, MsgBoxStyle.Critical, "Data Error(s)")
                If dataErrors.Count > 1 Then
                    LineUpLoaded = False
                    rdbJobQCurrent.Checked = True
                    LineUpLoaded = True
                    'sqlTimeUpdateInfo.UpdateLocalUpdateTimeFromSql()
                    loadSql()
                End If
            End If
        Catch ex As Exception

        End Try

        dataErrors.Clear()
    End Sub



    ''' <summary>
    ''' handles data errors in the job q dgv
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dgvJobQ_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvJobQ.DataError
        DataErrorTimer.Stop()
        dataErrors.Add(e.Exception)
        DataErrorTimer.Start()

        'MsgBox(e.Exception.Message, MsgBoxStyle.Critical, "dgvJobQ_DataError")

    End Sub


    Private Sub tmrSearchQ_Tick(sender As Object, e As EventArgs) Handles tmrSearchQ.Tick
        tmrSearchQ.Stop()
        If LineUpLoaded Then
            If txtJobQSearch.Text <> "" And txtJobQSearch.Text <> cSearch Then
                'actual query
                MyJQRowIO.updateDisplayedJobs(txtJobQSearch.Text)
            Else
                MyJQRowIO.updateDisplayedJobs()
            End If
            MyJQRowIO.formatCells()
        End If
    End Sub
    Private Sub JobQSearch_TextChanged(sender As Object, e As EventArgs) Handles txtJobQSearch.TextChanged
        tmrSearchQ.Stop()
        tmrSearchQ.Start()

    End Sub

    Private Sub JobQSearch_LostFocus(sender As Object, e As EventArgs) Handles txtJobQSearch.LostFocus
        If txtJobQSearch.Text = "" Then
            txtJobQSearch.Text = cSearch
        End If
    End Sub
    Private Sub JobQSearch_Click(sender As Object, e As EventArgs) Handles txtJobQSearch.GotFocus
        If txtJobQSearch.Text = cSearch Then
            txtJobQSearch.Text = ""
        End If
    End Sub

    Private Sub Button_Refresh(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LineUpLoaded = False
        rdbJobQCurrent.Checked = True
        LineUpLoaded = True
        'sqlTimeUpdateInfo.UpdateLocalUpdateTimeFromSql()
        loadSql()
        MyJQProductionIO = New JQProductionIO
        MyJQProjectDirIO = New JQProjectDirIO

    End Sub


    Private Sub TimerCheckForUpdate_Tick(sender As Object, e As EventArgs) Handles TimerCheckForUpdate.Tick
        'This sub controls the timer tick. Checks for updates from the sql server

        Try
            'if the original time & the new time don't match, it updates all tables
            If Not MyJQTimeUpdateIO.IsUpToDate() Then
                loadSql()
                'plays whatever sound is sound.wav
                My.Computer.Audio.Play(Path.Combine(My.Settings.dirResources, "sound.wav"), AudioPlayMode.Background)
            End If
        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try

    End Sub


    Private Sub rdbJobQHistory_CheckedChanged(sender As Object, e As EventArgs) Handles rdbJobQHistory.CheckedChanged
        If rdbJobQHistory.Checked Then SpecifyJobQLimits()
    End Sub

    Private Sub rdbJobQCurrent_CheckedChanged(sender As Object, e As EventArgs) Handles rdbJobQCurrent.CheckedChanged
        If rdbJobQCurrent.Checked Then SpecifyJobQLimits()
    End Sub

    Private Sub SpecifyJobQLimits()
        If LineUpLoaded Then
            Select Case True
                Case rdbJobQCurrent.Checked
                    MyJQRowIO.currentDisplay = JQRowIO.DisplayOptions.DisplayCurrent
                    btnAccounting.Visible = False
                Case rdbJobQHistory.Checked
                    MyJQRowIO.currentDisplay = JQRowIO.DisplayOptions.DisplayArchive
                    btnAccounting.Visible = True
            End Select
            MyJQRowIO.updateDisplayedJobs()
            MyJQRowIO.formatColumns()


            If MyJQRowIO.currentDisplay = JQRowIO.DisplayOptions.DisplayCurrent Then
                MyJQRowIO.formatCells()
            End If



        End If
    End Sub


#End Region


#Region "Tab Print"



    Private Sub btnAlignSheets_Click(sender As Object, e As EventArgs) Handles btnAlignSheets.Click
        Load_AlignmentSheets()
    End Sub

    Private Sub Load_AlignmentSheets()
        'loads the alignment sheets
        MsgBox("Sorry, this isn't currently implemented...")
        'LoadProductionPDF(MyProductionIO.AlignmentFiles)
    End Sub




    Private Sub ProductionFilesFlow_Paint(sender As Object, e As PaintEventArgs) Handles ProductionFilesFlow.Paint
        'blank...
    End Sub

    Private Async Sub LoadProductionPDF() 'Optional ByVal files As List(Of ProductionFileInfo) = Nothing)


        If IsNothing(lastQJobPdfShown) OrElse currentQJob.ToString <> lastQJobPdfShown.ToString Then
            'either first starting program, or a different row is selected...
            lastQJobPdfShown = currentQJob.Clone

            ProductionFilesFlow.Controls.Clear()
            If Not IsNothing(currentQJob) Then
                Dim result As Task(Of List(Of GroupBox)) = currentQJob.GetProductionGBsAsync(MyPrinterMgmt)
                Await (result)
                'Beep()
                ProductionFilesFlow.Controls.AddRange(result.Result.ToArray)
                'ProductionFilesFlow.Controls.AddRange(currentQJob.GetProductionGBsAsync.Result.ToArray)

                For Each myNote As JQNoteInfo In MyJQNoteIO.getNotesForItemNumber(currentQJob.ItemNumber, True)
                    ProductionFilesFlow.Controls.Add(myNote.GetGroupBox)
                Next

            End If
        End If


    End Sub


    Private Sub Load_Pictures(ByVal job As JQRowInfo)
        'Try
        Try
            Dim OldPicPath As String = picPath
            If Not IsNothing(job.PicFileInfo) AndAlso job.PicFileInfo.Exists Then
                picPath = job.PicFileInfo.FullName
            Else
                picPath = MyJQProductionIO.GetPicPath(job, My.Settings.dirWebProd)

                If picPath = "" Then
                    picPath = PicNotFoundPath
                End If
                job.PicFileInfo = New FileInfo(picPath)
            End If

            If OldPicPath <> picPath Then
                pbProducts.Load(picPath)
                pbProducts.SizeMode = PictureBoxSizeMode.Zoom

            End If
        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try

    End Sub






    ''' <summary>
    ''' Finds and updates the product info
    ''' </summary>
    ''' <param name="Job"></param>
    ''' <remarks></remarks>
    Private Sub Product_Changed(ByVal job As JQRowInfo)
        Try
            If IsNothing(currentQJob) OrElse job.JobID <> currentQJob.JobID Then
                currentQJob = job
                'Clears Info
                btnLoadProd.Text = ("Load")
                txtProdNumPrint.Text = cUnknown
                txtlTitle.Text = cUnknown
                txtInvTitle.Text = cUnknown
                txtSalePrice.Text = cUnknown
                txtPageCt.Text = cUnknown
                txtAuthor.Text = cUnknown
                txtType.Text = cUnknown
                txtSource.Text = cUnknown

                txtQuantityPrint.Text = ""
                'txtCatalog.Text = cUnknown
                txtCatalog.Rtf = "{\rtf1\ansi \b0" & cUnknown & "}"

                'updates the quantity of product
                txtQuantityPrint.Text = job.OrderQuantity

                'job.uvProdInfo = New uvProductInfo(False) 'resets the uv info

                If job.ItemNumber <> cNullInt And job.ItemNumber > 0 Then ' there's a product number

                    If IsNothing(currentQJob.uvProdInfo) Then
                        currentQJob.uvProdInfo = MyUvProductInfoIO.findProduct(job.ItemNumber)
                    End If

                    If IsNothing(currentQJob.ProjectDirs) Then
                        job.ProjectDirs = MyJQProjectDirIO.GetDirectories(job.ItemNumber)
                        'If job.ProjectDirs.Count > 0 Then
                        '    MsgBox(job.ProjectDirs(0).ProjectDirectory.FullName)
                        'End If

                    End If
                    job.ProductionFiles = MyJQProductionIO.GetProductionFiles(job.ItemNumber)

                    Dim isError As Boolean = False
                    If job.ProductionFiles.Count = 0 Then isError = True
                    UpdateStatus(job.ProductionFiles.Count & " file(s) found.", isError)

                    'MsgBox(ProdDirInfo.currentExport.covDir & vbCrLf & ProdDirInfo.currentExport.bodDir)

                    Load_Pictures(job)


                    'This converts the final quantity to body layers to print


                    ProductionCost(job.OrderQuantity)




                    If Not IsNothing(currentQJob.uvProdInfo) AndAlso currentQJob.uvProdInfo.MatchedOK Then




                        'sets the .text properties for the different labels according to their strings
                        With currentQJob.uvProdInfo
                            txtProdNumPrint.Text = .ItemNum
                            txtlTitle.Text = .Title
                            txtInvTitle.Text = .InvTitle
                            txtSalePrice.Text = "$" & .SalePrice
                            txtPageCt.Text = .PageCt
                            txtAuthor.Text = .Author
                            txtType.Text = .Type
                            txtSource.Text = .Source
                            txtCatalog.Rtf = "{\rtf1\ansi \b Web Text: \b0" & .WebText & " \par \b Catalog Text: \b0" & vbCrLf & .CatalogText & "}"
                        End With




                        Try
                            'this try adds the total sale price of the materials
                            Dim price As Double
                            price = currentQJob.uvProdInfo.SalePrice * currentQJob.OrderQuantity
                            'I think that "c" string is for currency
                            txtSalePrice.Text &= " (" & price.ToString("c") & ")"
                        Catch ex As Exception
                            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                        End Try

                    Else
                        'if nothing is found, it resets the different strings & titles

                        If txtProdNumPrint.Focused = False Then
                            txtProdNumPrint.Text = cUnknown
                        End If
                        txtlTitle.Text = cUnknown
                        txtInvTitle.Text = cUnknown
                        txtSalePrice.Text = cUnknown
                        txtPageCt.Text = cUnknown
                        txtAuthor.Text = cUnknown
                        txtType.Text = cUnknown
                        txtSource.Text = cUnknown
                        txtSource.Text = cUnknown

                        'txtCatalog.Text = UNKNOWN
                    End If





                    Dim prodType As ProductCategory = ProductCategory.Not_Set
                    If currentQJob.ProductionFiles.Count > 0 Then prodType = currentQJob.ProductionFiles(0).ProductType

                    'if a file wasn't found then it uses this-
                    If prodType = ProductCategory.Not_Set Then
                        btnLoadProd.Text = ("Load " & job.ItemNumber)
                        'btnCovExport.Text = ("Print Cover")
                        UpdateStatus("", False)
                        'MsgBox(bodyExport & vbCrLf & coverExport & vbCrLf & "I loveeeee lamp!")


                    Else
                        'a file was found - 



                        ' get_Specs("Product_Changed")
                        'btnCovExport.Text = ("Print " & Type & " Cover")
                        ' btnBodyPrint.Text = ("Print " & Type & " Body")

                        UpdateStatus("A " & prodType.ToString & " was found.", False)

                        btnLoadProd.Text = ("Load " & prodType.ToString)


                    End If

                    'input too short, complains

                End If

            End If

        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try


    End Sub

    '''' <summary>
    '''' Creates a message to show production file status.
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub FilesFoundStatusUpdate(ByVal WhatToSay As String)
    '    Dim isError As Boolean = False
    '    Dim StatusMessage As String = ""
    '    WhatToSay = WhatToSay.Trim
    '    Try
    '        With MyProductionIO.CurrentExport
    '            If .bodDir = "" Then
    '                StatusMessage &= "Body not " & WhatToSay & ", "
    '                isError = True
    '            Else
    '                StatusMessage &= "Body " & WhatToSay & ", "
    '            End If

    '            If .covDir = "" Then
    '                StatusMessage &= "Cover not " & WhatToSay & "."
    '                isError = True
    '            Else
    '                StatusMessage &= "Cover " & WhatToSay & "."
    '            End If
    '            If .bodDir = "" And .covDir = "" Then
    '                StatusMessage = "Production files not found"
    '                isError = True
    '            End If
    '        End With

    '        UpdateStatus(StatusMessage, isError)
    '    Catch ex As Exception
    '        Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
    '    End Try

    'End Sub

    Private Sub txtProdNumPrint_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtProdNumPrint.KeyPress

    End Sub

    Private Sub txtProdNumPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProdNumPrint.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                Try
                    If txtProdNumPrint.Focused = True Then
                        If currentQJob.uvProdInfo.ItemNum <> txtProdNumPrint.Text Then
                            If IsNumeric(txtProdNumPrint.Text) Then
                                If txtProdNumPrint.Text.Length > 3 Then
                                    Dim Quan As Integer = 0
                                    Dim strQuan As String = txtQuantityPrint.Text.ToString
                                    If IsNumeric(strQuan) Then
                                        Quan = CInt(strQuan)
                                    End If
                                    Dim job As New JQRowInfo()
                                    job.OrderQuantity = Quan
                                    job.ItemNumber = Integer.Parse(txtProdNumPrint.Text)
                                    Product_Changed(job)
                                End If

                            End If
                        End If
                    End If

                Catch ex As Exception
                    Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                    ' MsgBox(ex.Message.ToString)
                End Try
        End Select
    End Sub

    Private Sub ProdNum_TextChanged(sender As Object, e As EventArgs) Handles txtProdNumPrint.TextChanged



    End Sub

    Private Sub txtProdNumPrint_GotFocus(sender As Object, e As EventArgs) Handles txtProdNumPrint.GotFocus, txtProdNumPrint.Click
        txtProdNumPrint.SelectAll()
    End Sub

    Private Sub Product_Find(sender As Object, e As EventArgs) Handles btnLoadProd.Click
        LoadProductionPDF()
    End Sub





    Private Sub Prod_KeyDown(sender As Object, e As KeyEventArgs)  ', txtProdNum1.KeyDown

        'Detects if enter (return) is pressed and runs product_find

        Select Case e.KeyCode
            Case Keys.Return
                Product_Find(sender, New System.EventArgs)

        End Select
    End Sub







    Private Sub txtFinalQuan_TextChanged(sender As Object, e As EventArgs) Handles txtQuantityPrint.LostFocus
        If txtQuantityPrint.Text <> "" Then
            Try
                currentQJob.OrderQuantity = txtQuantityPrint.Text
                'ProductQuantity = txtQuantityPrint.Text
                'ProductionQuantity(ProductQuantity)
                'ProductionCost(ProductQuantity)
            Catch ex As Exception
                Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)

            End Try
        End If



    End Sub




    Private Sub pbProducts_Click(sender As Object, e As EventArgs) Handles pbProducts.DoubleClick
        If My.Computer.Keyboard.CtrlKeyDown Then
            MsgBox(pbProducts.ImageLocation)
        End If
    End Sub





#End Region


#Region "Tab History"



    Private Sub btnAccounting_Click(sender As Object, e As EventArgs) Handles btnAccounting.Click
        MyJQRowIO.CreateAccountingFileForYear()
    End Sub






#End Region


#Region "Tab Personalize"


#Region "Personalize Imposition"


    Private Sub CreateCustomProofGPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateCustomProofGPToolStripMenuItem.Click
        'Shows a simple imposition dialog window
        Dim imp As New frmImposeCalendars
        imp.ShowDialog()
    End Sub

    ''' <summary>
    ''' In charge of imposition of each selected row in the personalize dgv
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnImpose_Click(sender As Object, e As EventArgs) Handles btnImpose.Click
        Me.UseWaitCursor = True
        Application.DoEvents()
        GetImpositionSettings()
        If rdbBodyPDF.Checked Or rdbCovPDF.Checked Then
            'Clear pdf display so that the pdf can be overwritten
            rdbBodyPDF.Checked = False
            rdbCovPDF.Checked = False
            pdfInvoice.CoreWebView2.Navigate("about:blank")
        End If
        Dim showPrompt As Boolean = Not My.Computer.Keyboard.CtrlKeyDown 'if the ctrl key is down, it doesnt show the prompt
        Try
            For Each selRow As PersonalizeRowInfo In selectedPersonalizedIDs
                'goes through each selected custom order line
                Dim errors As New List(Of String)
                selRow.ConfigureFilePaths(CBConfig)
                If selRow.ImposeCalendar(CBConfig, errors, showPrompt) Then
                    Select Case selRow.PrintStatus
                        Case "Order", "Imposed", "Imposed - Imposed"
                            selRow.PrintStatus = "Imposed"
                        Case Else
                            selRow.PrintStatus = "Imposed - " & selRow.PrintStatus
                    End Select
                    selRow.AddStatusToHistory()
                Else

                    Dim s As New Text.StringBuilder
                    s.AppendLine("Imposition failed: The following errors were found - ")
                    If errors.Count > 0 Then
                        For Each myBad In errors
                            s.AppendLine(myBad)
                        Next
                    Else
                        s.AppendLine("")
                        s.AppendLine("That's weird... there aren't any errors :(")
                        s.AppendLine("Who wrote this code anyway?")
                        s.AppendLine("Hire someone else!")
                    End If


                    MsgBox(s.ToString, MsgBoxStyle.Critical)

                End If

            Next
        Catch ex As Exception

        End Try


        MyPersonalizeIO.WritePersonalizedXml()
        XMLReadCustomOrders()
        Me.UseWaitCursor = False
        Application.DoEvents()

    End Sub




    Private Sub btnImposeBkCov_Click______________________Copy(sender As Object, e As EventArgs) 'DELETE ME!!!
        'I'd love to be able to impose Book Covers eventually...
        OpenFileDialog1.Title = "Please Select the Book Cover File you Want to Impose"
        OpenFileDialog1.Filter = "All files (*.*)|*.*|Indesign Files (*.indd)|*.indd"
        SaveFileDialog1.DefaultExt = "indd"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.InitialDirectory = LastFilePath
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = False
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            Dim OriginPath As String = OpenFileDialog1.FileName
            Dim FinalPath As String = ""
            If OriginPath.Contains(My.Settings.PrEBookDir) Then
                FinalPath = My.Settings.dirBkBod
            ElseIf OriginPath.Contains(My.Settings.PrEChartDir) Then
                FinalPath = My.Settings.dirChart
            ElseIf OriginPath.Contains(My.Settings.PrEHymnBookDir) Then
                FinalPath = My.Settings.dirHymnbookBod
            ElseIf OriginPath.Contains(My.Settings.PrELeafletDir) Then
                FinalPath = My.Settings.dirLeaflet
            ElseIf OriginPath.Contains(My.Settings.PrEPamphletDir) Then
                FinalPath = My.Settings.dirPamBod
            Else
                FinalPath = OpenFileDialog1.InitialDirectory
            End If

            SaveFileDialog1.InitialDirectory = FinalPath


            Me.UseWaitCursor = True
            Application.DoEvents()
            SaveFileDialog1.Title = "Please Choose a PDF Export Location"
            SaveFileDialog1.Filter = "All files (*.*)|*.*|PDF Files | *.pdf"
            SaveFileDialog1.DefaultExt = "pdf"
            SaveFileDialog1.FilterIndex = 2

            If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim idApp As InDesign.Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)
                Dim ScriptArgList As New List(Of String)
                Dim ReturnData As String = ""
                Dim lstReturnData As New List(Of String)

                ScriptArgList.Add(GenUtil.ConvertForJavaScript(My.Settings.bkCovImposeTemplatePath))
                ScriptArgList.Add(GenUtil.ConvertForJavaScript(OpenFileDialog1.FileName))
                ScriptArgList.Add(GenUtil.ConvertForJavaScript(SaveFileDialog1.FileName))
                Dim ScriptArgs() As String = ScriptArgList.ToArray


                ReturnData = idApp.DoScript(My.Settings.dirResources & "ImposeBookCover.jsx", InDesign.idScriptLanguage.idJavascript, ScriptArgs)

                'For Each myString As String In ReturnData.Split(vbTa)b
                '    lstReturnData.Add(myString)
                'Next


                If ReturnData <> "" Then
                    MsgBox(ReturnData)
                End If


            End If

        End If
        Me.UseWaitCursor = False

    End Sub
    Private Function GetProperty(ByVal IdFilePath As String, ByVal whatProperty As String) As String
        Dim ReturnProperty As String = ""
        Try

            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(IdFilePath)

            Dim xmlNodes As XmlNodeList = xmlDoc.ChildNodes

            For Each PropertyNode As XmlNode In xmlNodes
                If PropertyNode.Name = "Design" Then

                    For Each settingNode As XmlNode In PropertyNode.ChildNodes
                        If settingNode.Name = whatProperty Then
                            ReturnProperty = settingNode.InnerText
                        End If
                    Next
                End If
                'MsgBox(PropertyNode.Name & vbCrLf & PropertyNode.InnerText)

            Next

        Catch ex As Exception
            'ReturnProperty = ""
        End Try
        Return ReturnProperty
    End Function



    Private Sub GetImpositionSettings()

        ' Dim myCBConfig As New Falcon.PageComposer.PcConfig
        Dim ErrorList As New List(Of String)
        Dim SettingsPath As String = My.Settings.PcConfigPath

        If File.Exists(SettingsPath) = False Then
            SettingsPath = My.Settings.dirResources & My.Settings.PcConfigPath
        End If

        Dim Result As Boolean = CBConfig.Load(SettingsPath, ErrorList)
        Dim s As String = ""
        For i As Integer = 0 To 4
            If (ErrorList.Count - 1) >= i Then
                'if the error list contains index i, it shows the error.
                s &= i & ": " & ErrorList(i) & vbCrLf
            End If
        Next
        If ErrorList.Count >= 5 Then
            s &= "..."
        End If

        If s <> "" Then
            s = "The following errors were recorded. Imposition may be impossible till fixed." & vbCrLf & vbCrLf & s
            MsgBox(s, MsgBoxStyle.Critical)
        End If


    End Sub
#End Region


    ''' <summary>
    ''' Gets the current status from the loading thread
    ''' </summary>
    ''' <param name="CurValue"></param>
    ''' <param name="MaxValue"></param>
    ''' <remarks></remarks>
    Public Sub ReceiveProgress(ByVal CurValue As Integer, ByVal MaxValue As Integer)
        If InvokeRequired Then
            Me.Invoke(Sub() ReceiveProgress(CurValue, MaxValue))
        Else
            Try
                If ProgressBar1.Maximum <> MaxValue Then
                    ProgressBar1.Maximum = MaxValue
                End If

                If CurValue <= MaxValue Then
                    ProgressBar1.Value = CurValue
                Else
                    ProgressBar1.Maximum = MaxValue
                    ProgressBar1.Value = MaxValue
                End If
                Application.DoEvents()
            Catch ex As Exception
                Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            End Try
        End If


    End Sub



    ''' <summary>
    ''' checks to see if someone else has saved the file before you did.
    ''' Returns True if up to date, or orders were refreshed
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PersonalizeDGVIsUpToDate() As Boolean
        '
        Dim UpToDate As Boolean = False
        If MyPersonalizeIO.PersonalizXmlUpdatedLast = File.GetLastWriteTime(My.Settings.MxCustomPath) Then
            UpToDate = True
        ElseIf MessageBox.Show("Your information is out of date. Please press OK to get the most recent data", "Data loss may occur!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.OK Then

            Dim CurrentView As Integer = dgvPersonalizeJobQ.FirstDisplayedScrollingRowIndex
            XMLReadCustomOrders()
            UpToDate = True
            Try
                dgvPersonalizeJobQ.FirstDisplayedScrollingRowIndex = CurrentView
            Catch ex As Exception
                Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            End Try

        End If

        Return UpToDate
    End Function


    Private Sub TmrUpdatePQ_Tick(sender As Object, e As EventArgs) Handles TmrUpdatePQ.Tick
        'checks to see if file has been updated by someone else
        TmrUpdatePQ.Stop()
        Dim startTimerAgain As Boolean = False


        If LineUpTabCtrl.SelectedIndex = 2 Then
            'Personalize
            TmrUpdatePQ.Interval = 30000 '30 seconds
        Else
            TmrUpdatePQ.Interval = 600000 '10 minutes
        End If

        Try
            If MyPersonalizeIO.PersonalizXmlUpdatedLast <> File.GetLastWriteTime(My.Settings.MxCustomPath) Then
                Dim IsUpdating As Boolean = False
                'if it's being edited, then it complains
                If dgvPersonalizeJobQ.SelectedCells.Count > 0 Then
                    For Each myCell As DataGridViewCell In dgvPersonalizeJobQ.SelectedCells
                        If myCell.IsInEditMode Then
                            IsUpdating = True
                        End If
                    Next
                End If

                Dim cView As Integer = dgvPersonalizeJobQ.FirstDisplayedScrollingRowIndex
                If IsUpdating Then
                    'brings up a prompt to reload data
                    PersonalizeDGVIsUpToDate()
                Else
                    'reloads the spreadsheet
                    XMLReadCustomOrders()
                End If


                'Try
                '    dgvPersonalizeJobQ.FirstDisplayedScrollingRowIndex = cView
                'Catch ex As Exception
                '    Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                'End Try


            End If
            If MyPersonalizeIO.AllPersonalizeRows.Count = 0 Then
                TmrUpdatePQ.Interval = 3000 '3 seconds
                'loaded personalized rows are required to run this part
                startTimerAgain = True
            Else
                startPersonalizedCustomerInfoThread()
            End If
        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try
        If xmlCustomOrdersHaveBeenRead Or startTimerAgain Then
            TmrUpdatePQ.Start()
        End If
        'MsgBox(TmrUpdatePQ.Enabled & vbCrLf & TmrUpdatePQ.Interval / 1000)


    End Sub


    Private Sub btnDesignsRefresh_Click(sender As Object, e As EventArgs) Handles btnDesignsRefresh.Click


        'resets the txtpersonalizesearch.text to search
        txtPersonalizeSearch.Text = cSearch
        rdbCurrent.Checked = True
        XMLReadCustomOrders()

        mxOrdersDict.Clear()



    End Sub


    ''' <summary>
    ''' Records the current Line in view on the personalized JobQ.
    ''' </summary>
    ''' <remarks></remarks>
    Private Property PqCView As Integer = 0

    ''' <summary>
    ''' updates the dataGridView to match the data table. 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FormatPersonalizeColumns()

        Try
            dgvPersonalizeJobQ.AutoGenerateColumns = False
            dgvPersonalizeJobQ.Columns.Clear()

            dgvPersonalizeJobQ.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'If SkipColumnWidthSetup = False Then
            'sets the settings for each of the columns
            With dgvPersonalizeJobQ.Columns
                .Add(AddColumn(Col_OrderNumber, "Order Number", GetType(String), "MxNumberNoMX", 50))
                .Add(AddColumn(Col_Item, "Item", GetType(String), "ItemNumber", 30))
                .Add(AddColumn(Col_DesignID, "Design ID", GetType(String), "DesignId", 70))
                .Add(AddColumn(Col_Quantity, "Quan.", GetType(String), "DesignQuantity", 30))
                .Add(AddColumn(Col_OrderStatus, "UV Status", GetType(String), "UvStatus", 60))
                .Add(AddColumn(Col_PrintStatus, "Print Status", GetType(String), "PrintStatus", 100, DataGridViewAutoSizeColumnMode.Fill))
                .Add(AddColumn(Col_OrderCreationDate, "Order Creation Date", GetType(Date), "OrderCreated", 80))
                Dim col As DataGridViewColumn = AddColumn(Col_Labels, "Labels Printed", GetType(Boolean), "LabelsHaveBeenPrinted", 70)
                col.CellTemplate = New DataGridViewCheckBoxCell
                .Add(col)
                .Add(AddColumn(Col_Filler, "", GetType(String), "", 100, DataGridViewAutoSizeColumnMode.Fill))
            End With

            'goes through the table and color codes it to keep mx's together
            FormatPersonalizeRows()

            'updates the current view to what it was at the beginning
            Try
                If PqCView > 0 Then

                    dgvPersonalizeJobQ.FirstDisplayedScrollingRowIndex = PqCView
                Else
                    dgvPersonalizeJobQ.FirstDisplayedScrollingRowIndex = dgvPersonalizeJobQ.RowCount - 1
                End If
            Catch ex As Exception
                Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            End Try

            dgvPersonalizeJobQ.Columns(Col_Filler).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvPersonalizeJobQ.Columns(Col_Filler).ReadOnly = True
            lblDesignsShowing.Text = dgvPersonalizeJobQ.RowCount - 1

            PersonalizeJobQisLoading = False

        Catch ex As Exception
            Beep()
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try
    End Sub
    Public Function AddColumn(ByVal colName As String, ByVal headerText As String, ByVal whatType As Type,
                              ByVal dataProperty As String, ByVal width As Integer,
                              Optional ByVal viewMode As DataGridViewAutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet) As DataGridViewColumn
        Dim cell As New DataGridViewTextBoxCell()
        Dim Col As New DataGridViewColumn With {
                .Name = colName,
                .CellTemplate = cell,
                .HeaderText = headerText,
                .ValueType = whatType,
                .Width = width * 2,
                .MinimumWidth = width,
                .AutoSizeMode = viewMode
            }

        If dataProperty <> "" Then Col.DataPropertyName = dataProperty

        Return Col
    End Function


    ''' <summary>
    ''' Reads the personalized xml file and converts it to a dgv and datatable
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub XMLReadCustomOrders() 'Optional ByVal SkipColumnWidthSetup As Boolean = False
        'this sub reads the master xml file

        Try
            PersonalizeJobQisLoading = True
            PqCView = 0
            'Dim cview As Integer = 0
            If dgvPersonalizeJobQ.RowCount > 2 Then
                PqCView = dgvPersonalizeJobQ.FirstDisplayedScrollingRowIndex
            End If

            MyPersonalizeIO.LoadPersonalizedInfo()


        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try



    End Sub




    ''' <summary>
    ''' Gets the completed dataset from the thread, and if success, it uses it to update the personalize datagridview
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReceiveSuccess()
        If InvokeRequired Then
            Me.Invoke(Sub() ReceiveSuccess())
        Else

            Try

                TabPersonalize.Text = MyPersonalizeIO.PersonalizeTabText


                UpdatePQFilter(txtPersonalizeSearch.Text, True)

                Application.DoEvents()
                PersonalizeJobQisLoading = False
                If wasPersonalizedTabDisplayed AndAlso dgvPersonalizeJobQ.Rows.Count > 3 Then
                    For Each myRow As DataGridViewRow In dgvPersonalizeJobQ.Rows
                        myRow.Selected = False
                    Next
                    dgvPersonalizeJobQ.Rows(dgvPersonalizeJobQ.Rows.Count - 2).Selected = True
                    dgvPersonalizeJobQ.FirstDisplayedScrollingRowIndex = dgvPersonalizeJobQ.SelectedRows(0).Index
                End If
                xmlCustomOrdersHaveBeenRead = True

                TmrUpdatePQ.Start()


            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

    End Sub

    Private Sub txtPersonalizeSearch_GotFocus(sender As Object, e As EventArgs) Handles txtPersonalizeSearch.GotFocus

        If txtPersonalizeSearch.Text = cSearch Then
            txtPersonalizeSearch.Text = ""
            txtPersonalizeSearch.ForeColor = Color.Black
        Else

        End If
    End Sub

    Private Sub txtPersonalizeSearch_LostFocus(sender As Object, e As EventArgs) Handles txtPersonalizeSearch.LostFocus
        If txtPersonalizeSearch.Text = "" Then
            txtPersonalizeSearch.Text = cSearch
        End If
    End Sub

    Private Sub rdoBtnCurrent_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCurrent.CheckedChanged, rdbArchive.CheckedChanged
        If rdbCurrent.Checked Then
            btnArchive.Text = "Send to Archive"
        ElseIf rdbArchive.Checked Then
            btnArchive.Text = "Restore ID's"
        End If
        tmrSearchPersonalize.Stop()
        tmrSearchPersonalize.Start()
    End Sub

    Private Sub txtPersonalizeSearch_TextChanged(sender As Object, e As EventArgs) Handles txtPersonalizeSearch.TextChanged
        tmrSearchPersonalize.Stop()
        tmrSearchPersonalize.Start()
    End Sub


    Private Sub tmrSearchPersonalize_Tick(sender As Object, e As EventArgs) Handles tmrSearchPersonalize.Tick
        tmrSearchPersonalize.Stop()
        UpdatePQFilter(txtPersonalizeSearch.Text)
    End Sub


    ''' <summary>
    ''' Updates the filter for the personalizedJobQ Datagridview
    ''' </summary>
    ''' <param name="mySearch"></param>
    ''' <param name="forceFilter">Leave false unless you read through this sub</param>
    Private Sub UpdatePQFilter(ByVal mySearch As String, Optional ByVal forceFilter As Boolean = False)

        'if the xmlFile has been read... 
        If (PersonalizeJobQisLoading = False AndAlso Not IsNothing(MyPersonalizeIO)) Or forceFilter Then

            'filters through the design ID's
            Try
                Dim SearchIn As PersonalizeIO.SearchCategory = PersonalizeIO.SearchCategory.All
                If rdbAllOrders.Checked Then
                    SearchIn = PersonalizeIO.SearchCategory.All
                ElseIf rdbCurrent.Checked Then
                    SearchIn = PersonalizeIO.SearchCategory.Current
                ElseIf rdbArchive.Checked Then
                    SearchIn = PersonalizeIO.SearchCategory.Archive
                End If


                MyPersonalizeIO.FilterRows(SearchIn, mySearch)

                DesignsBindingsource.SuspendBinding()
                DesignsBindingsource.Dispose()
                DesignsBindingsource = New BindingSource
                DesignsBindingsource.DataSource = MyPersonalizeIO.DisplayedPersonalizeRows


                dgvPersonalizeJobQ.DataSource = Nothing

                dgvPersonalizeJobQ.Dispose()

                dgvPersonalizeJobQ = New DataGridView
                dgvPersonalizeJobQ.AutoGenerateColumns = False




                dgvPersonalizeJobQ.DataSource = DesignsBindingsource
                dgvPersonalizeJobQ.Dock = DockStyle.Fill
                dgvPersonalizeJobQ.ContextMenuStrip = PersonalizeJobQContextMenu

                SplitContainer7.Panel1.Controls.Add(dgvPersonalizeJobQ)

                Application.DoEvents()

                FormatPersonalizeColumns()

                UpdateStatus("", False)
                FormatPersonalizeRows()
                lblDesignsShowing.Text = dgvPersonalizeJobQ.RowCount - 1



            Catch ex As Exception
                Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                UpdateStatus("Error", True)
            End Try
        End If
    End Sub




    ''' <summary>
    ''' 'goes through the table and color codes it to keep mx's together by alternating colors
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FormatPersonalizeRows()
        'separates each different mx order by alternating colors

        Try
            If dgvPersonalizeJobQ.Rows.Count > 0 Then
                'keeps track of the different settings for the grouped colors
                Dim OrderA As Integer = dgvPersonalizeJobQ.Item(Col_OrderNumber, 0).Value
                Dim OrderB As Integer = dgvPersonalizeJobQ.Item(Col_OrderNumber, 0).Value
                Dim RowColor() As Color = {Color.Linen, Color.LightCyan}
                Dim SelRowColor() As Color = {Color.Peru, Color.CornflowerBlue}
                Dim counter As Integer = 0

                'this for groups the colors according to the mx number
                For Each ColorRow As DataGridViewRow In dgvPersonalizeJobQ.Rows

                    'For i As Integer = 0 To gridCustomID.Rows.Count - 1
                    Try
                        'orderB equals current mx value
                        OrderB = ColorRow.Cells(Col_OrderNumber).Value
                        'if they're the same then it sets it to the current counter
                        If OrderA = OrderB Then
                            ColorRow.DefaultCellStyle.BackColor = RowColor(counter)
                            ColorRow.DefaultCellStyle.SelectionBackColor = SelRowColor(counter)
                        Else
                            If counter = 0 Then
                                counter = 1
                            Else
                                counter = 0
                            End If
                            ColorRow.DefaultCellStyle.BackColor = RowColor(counter)
                            ColorRow.DefaultCellStyle.SelectionBackColor = SelRowColor(counter)
                            OrderA = ColorRow.Cells(Col_OrderNumber).Value
                            OrderB = ColorRow.Cells(Col_OrderNumber).Value
                        End If
                    Catch ex As Exception
                        Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                    End Try

                Next
                If dgvPersonalizeJobQ.Rows.Count >= MyPersonalizeIO.DisplayedPersonalizeRows.Count Then
                    For i As Integer = 0 To MyPersonalizeIO.DisplayedPersonalizeRows.Count - 1
                        'go through and add histories to row
                        Dim curRowInfo As PersonalizeRowInfo = MyPersonalizeIO.DisplayedPersonalizeRows(i)
                        Dim curDgvRow As DataGridViewRow = dgvPersonalizeJobQ.Rows(i)

                        curDgvRow.Cells(Col_PrintStatus).ToolTipText = String.Join(vbCrLf, curRowInfo.StatusHistory)
                        curDgvRow.Cells(Col_Labels).ToolTipText = String.Join(vbCrLf, curRowInfo.LabelHistory)
                    Next
                End If




                ''separates each different mx order by alternating colors

                ''keeps track of the different settings for the grouped colors
                'Dim OrderA As Integer = dgvPersonalizeJobQ.Item("OrderNumber", 0).Value
                'Dim OrderB As Integer = dgvPersonalizeJobQ.Item("OrderNumber", 0).Value
                'Dim Rand As New Random
                'Dim X As Integer = Rand.Next(200, 256)
                'Dim Y As Integer = Rand.Next(200, 256)
                'Dim Z As Integer = Rand.Next(200, 256)
                'Dim RowColor As Color = Color.FromArgb(X, Y, Z)
                'Dim SelRowColor As Color = Color.FromArgb(X - 150, Y - 150, Z - 150)
                '' Dim counter As Integer = 0

                ''this for groups the colors according to the mx number
                'For Each ColorRow As DataGridViewRow In dgvPersonalizeJobQ.Rows

                '    Dim R As Integer = Rand.Next(200, 256)
                '    Dim G As Integer = Rand.Next(200, 256)
                '    Dim B As Integer = Rand.Next(200, 256)

                '    'For i As Integer = 0 To gridCustomID.Rows.Count - 1
                '    Try
                '        'orderB equals current mx value
                '        OrderB = ColorRow.Cells("OrderNumber").Value
                '        'if they're the same then it sets it to the current counter
                '        If OrderA = OrderB Then
                '            ColorRow.DefaultCellStyle.BackColor = RowColor
                '            ColorRow.DefaultCellStyle.SelectionBackColor = SelRowColor
                '        Else
                '            RowColor = Color.FromArgb(R, G, B)
                '            SelRowColor = Color.FromArgb(R - 150, G - 150, B - 150)
                '            ColorRow.DefaultCellStyle.BackColor = RowColor
                '            ColorRow.DefaultCellStyle.SelectionBackColor = SelRowColor
                '            OrderA = ColorRow.Cells("OrderNumber").Value
                '            OrderB = ColorRow.Cells("OrderNumber").Value
                '        End If
                '    Catch ex As Exception
                '        Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                '    End Try
                '    R = 0
                '    G = 0
                '    B = 0
                'Next
            End If

        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try


    End Sub



    Function AddCellstoList() As Boolean
        'this takes care of adding the cells to the lists
        Dim success As Boolean = True
        Try
            selectedPersonalizedIDs.Clear()
            If dgvPersonalizeJobQ.SelectedRows.Count > 0 Then


                For Each myRow As DataGridViewRow In dgvPersonalizeJobQ.SelectedRows

                    selectedPersonalizedIDs.Add(MyPersonalizeIO.DisplayedPersonalizeRows(myRow.Index))

                Next

            Else
                success = False
            End If
            For Each row As PersonalizeRowInfo In selectedPersonalizedIDs
                row.ConfigureFilePaths(CBConfig)

            Next


        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            ' MsgBox(ex.Message & vbCrLf & "AddCellstoList")
            success = False
        End Try
        Return success
    End Function

    Private Sub PersonalizeJobQ_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvPersonalizeJobQ.DataError

    End Sub



    Private Sub JQKeyDown(sender As Object, e As KeyEventArgs) Handles dgvPersonalizeJobQ.KeyDown
        If PersonalizeDGVIsUpToDate() Then
            If Not dgvPersonalizeJobQ.SelectedRows.Count > 0 Then
                Select Case e.KeyCode
                    Case Keys.Delete
                        If MsgBox("Are you sure you want to delete the selected cells?", MsgBoxStyle.OkCancel, "Delete?") = MsgBoxResult.Ok Then
                            For Each delCell As DataGridViewCell In dgvPersonalizeJobQ.SelectedCells
                                delCell.Value = ""
                            Next
                            'DesignsTable.WriteXml(My.Settings.MxCustomPath)
                            'UpdatePQSaveTime()
                            MyPersonalizeIO.WritePersonalizedXml()


                        End If
                End Select
            End If


        End If


    End Sub


    Private Sub PersonalizeJobQ_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPersonalizeJobQ.CellClick, dgvPersonalizeJobQ.CellContentClick
        'blank on purpose (right coding area)
    End Sub

    Private Sub PQ_CellSelectionChanged(sender As Object, e As EventArgs) Handles dgvPersonalizeJobQ.SelectionChanged
        'If PersonalizeJobQ.ContainsFocus Then
        If Not txtPersonalizeSearch.ContainsFocus Then
            PQSelectedCellChanged()
        End If
        'End If
    End Sub


    Private Sub PQSelectedCellChanged()

        Dim Succeeded As Boolean = True
        If Not PersonalizeJobQisLoading Then 'this line helps make it not load pdf's while loading the dgv.
            If dgvPersonalizeJobQ.SelectedRows.Count > 0 Then
                'if the mx is the same as the last selected row, it doesn't do anything
                If Not IsDBNull(dgvPersonalizeJobQ.CurrentRow.Cells(0).Value) Then
                    Try
                        If AddCellstoList() = True Then
                            'resets the counter
                            WhichMX = 0
                            ShowInfo(Succeeded)
                        End If

                    Catch ex As Exception
                        Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                        Succeeded = False
                    End Try
                End If
            End If
        End If


    End Sub

    Private Property LastLoadedPersonalizeMxNumber As Integer = 0

    Private Sub ShowInfo(ByRef Succeeded As Boolean)

        Try
            Dim mxNum As Integer = selectedPersonalizedIDs(WhichMX).MxNumberNoMX
            If mxNum <> LastLoadedPersonalizeMxNumber Then 'Or rdbBodyPDF.Checked Or rdbCovPDF.Checked Then
                If Not mxOrdersDict.ContainsKey(mxNum) Then
                    'currentMxOrder = mxOrdersDict(mxNum)
                    mxOrdersDict.Add(mxNum, New PersonalizeMxInfo(mxNum))
                End If

                currentMxOrder = mxOrdersDict(mxNum)


                With currentMxOrder
                    'handles showing the personalized xml info
                    txtEmail.Text = .Email
                    txtItemNumber.Text = selectedPersonalizedIDs(WhichMX).ItemNumber
                    txtPhoneNumber.Text = .PhoneNum
                    txtShipAddress.Text = .ShipAddress
                    txtShipNote.Text = cShipNote & .ShipNote
                    txtBillingNameNumber.Text = .BillName
                    txtReceiptNum.Text = .ReceiptNumber
                    txtMxNumber.Text = .MxNumber
                End With


                Try
                    'shows the history in a drop down format
                    cmbStatusHistory.Items.Clear()
                    For Each HistoryLine As String In selectedPersonalizedIDs(WhichMX).StatusHistory
                        If HistoryLine.Trim <> "" Then
                            cmbStatusHistory.Items.Insert(0, HistoryLine)
                        End If
                    Next
                    cmbStatusHistory.Text = cmbStatusHistory.Items(0)

                Catch ex As Exception
                    Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                End Try


                'sets the Status to blank if there are multiple status's
                Dim BlankStat As Boolean = False
                For Each selInfo In selectedPersonalizedIDs
                    If selInfo.PrintStatus <> selectedPersonalizedIDs(WhichMX).PrintStatus Then
                        BlankStat = True
                    End If
                Next

                If BlankStat = True Then
                    cmbStatus.Text = ""
                Else
                    cmbStatus.Text = selectedPersonalizedIDs(WhichMX).PrintStatus
                End If



                Dim MxPath As String = My.Settings.dirMxOrders & selectedPersonalizedIDs(WhichMX).MxNumber
                'if the "Show Invoice" Radio Button is checked, it will create a pdf of the Mx
                If rdbInvoice.Checked Then
                    Dim TicketIO As New PdfJobTicketIO()
                    Dim title As String = selectedPersonalizedIDs(WhichMX).MxNumber
                    If TicketIO.CreatePDF(MxPath & ".txt", title, MxPath & ".pdf") Then UpdateStatus($"PDF For: { title } created.", False)
                End If
                ShowPersonalizedPDF()
            End If
            LastLoadedPersonalizeMxNumber = mxNum



        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            Succeeded = False
        End Try
        'If txtPersonalizeSearch.Focused = False Then
        '    FocusTimer.Start()
        'End If

        If Succeeded = True Then
            txtItemNumber.Text = selectedPersonalizedIDs(WhichMX).ItemNumber
            txtDesignID.Text = selectedPersonalizedIDs(WhichMX).DesignId
        Else
            txtItemNumber.Text = ""
            txtDesignID.Text = ""
        End If

        Dim Quan As Integer = selectedPersonalizedIDs.Count - 1

        'disables clicking the button if there are no more or no fewer options
        If WhichMX > 0 Then
            btnPrev.Enabled = True
            btnPrev.BackColor = Color.DodgerBlue
        Else
            btnPrev.Enabled = False
            btnPrev.BackColor = Color.Gray
        End If
        If WhichMX < Quan Then
            btnNext.Enabled = True
            btnNext.BackColor = Color.DodgerBlue
        Else
            btnNext.Enabled = False
            btnNext.BackColor = Color.Gray
        End If

    End Sub


    Private Sub rdbLabels_click(sender As Object, e As EventArgs) Handles rdbLabels.Click
        If My.Computer.Keyboard.CtrlKeyDown Then
            Dim labelsFP As String = Path.Combine(My.Settings.dirResources, "Labels.pdf")
            copyToClipboardAndOpenToInExplorer(labelsFP)

            'If File.Exists(labelsFP) Then
            '    Process.Start(labelsFP)
            'End If
        End If

    End Sub

    Private Sub rdbCovPDF_click(sender As Object, e As EventArgs) Handles rdbCovPDF.Click
        Try
            Dim myFile As String = selectedPersonalizedIDs(WhichMX).CoverFilePath
            copyToClipboardAndOpenToInExplorer(myFile)
            ShowPersonalizedPDF()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnImposePDF_CtrlClick(sender As Object, e As EventArgs) Handles rdbBodyPDF.Click
        Try

            Dim myFile As String = selectedPersonalizedIDs(WhichMX).BodyFilePath
            copyToClipboardAndOpenToInExplorer(myFile)
            ShowPersonalizedPDF()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnInvoice_CtrlClick(sender As Object, e As EventArgs) Handles rdbInvoice.Click
        Try
            Dim myFile As String = My.Settings.dirMxOrders & selectedPersonalizedIDs(WhichMX).MxNumber & ".txt"
            copyToClipboardAndOpenToInExplorer(myFile)
            ShowPersonalizedPDF()
        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try
    End Sub
    ''' <summary>
    ''' If ctrl key is down, it copies a file path to the clipboard, and opens the explorer window with the file selected 
    ''' </summary>
    ''' <param name="fp">Full File Path</param>
    Private Sub copyToClipboardAndOpenToInExplorer(ByVal fp As String)
        If My.Computer.Keyboard.CtrlKeyDown Then
            If File.Exists(fp) Then
                Diagnostics.Process.Start("Explorer.exe", "/select," & fp)
                Clipboard.Clear()
                Clipboard.SetText(fp)

            Else
                UpdateStatus("I couldn't find the file!", True)
                Beep()
            End If
        End If
    End Sub

    Public Sub receiveCustDataTable(ByVal custDT As Personalize_custDataTable)
        Try
            If InvokeRequired Then
                Me.Invoke(Sub() receiveCustDataTable(custDT))
            Else

                PersonalizedCustomerInfo = custDT
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub startPersonalizedCustomerInfoThread()

        'MsgBox("startPersonalizedCustomerInfoThread disabled for testing")

        'PersonalizedCustomerInfo = New Personalize_custDataTable(DesignsDS, My.Settings.dirResources, Me, False)
        PersonalizedCustomerInfo = New Personalize_custDataTable(MyPersonalizeIO.AllPersonalizeRows, My.Settings.dirResources, Me, False)
        ' Dim custDT As New custDataTable(DesignsDS, My.Settings.dirResources, Me, True)

        'Dim threadStart As New Threading.ThreadStart(AddressOf custDT.runAsThread)
        Dim threadStart As New Threading.ThreadStart(AddressOf PersonalizedCustomerInfo.runAsThread)

        Dim cusDTthread As New Threading.Thread(threadStart)
        cusDTthread.Priority = Threading.ThreadPriority.BelowNormal
        cusDTthread.IsBackground = True
        cusDTthread.Start()

    End Sub

    Private Sub btnCustSearch_Click(sender As Object, e As EventArgs) Handles btnCustSearch.Click


        Try
            Dim tempdir As String = ""
            If IsNothing(PersonalizedCustomerInfo) Then
                startPersonalizedCustomerInfoThread()
                Beep()
            Else

                tempdir = PersonalizedCustomerInfo.resourceDir
                Log.addError(tempdir, "btnCustSearch_Click", False)
                Dim CustSearch As New frmCustSearch(PersonalizedCustomerInfo) 'Application.StartupPath)
                CustSearch.Show()
            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub btnPrintLabels_Click(sender As Object, e As EventArgs) Handles btnPrintLabels.Click

        CreatePersonalizedJobLabel()

    End Sub



    Private Sub CreatePersonalizedJobLabel()
        Try

            If PersonalizeDGVIsUpToDate() = False Then
                Exit Sub
            End If

            For Each selRow As PersonalizeRowInfo In selectedPersonalizedIDs
                selRow.Barcode.CreateBarcode()
            Next

            Dim idsToMxDict As New Dictionary(Of PersonalizeRowInfo, PersonalizeMxInfo)

            For Each selOrder As PersonalizeRowInfo In selectedPersonalizedIDs
                If Not mxOrdersDict.ContainsKey(selOrder.MxNumberNoMX) Then
                    mxOrdersDict.Add(selOrder.MxNumberNoMX, New PersonalizeMxInfo(selOrder.MxNumberNoMX))
                End If
                idsToMxDict.Add(selOrder, mxOrdersDict(selOrder.MxNumberNoMX))
            Next

            'clears the pdfInvoice
            pdfInvoice.CoreWebView2.Navigate("about:blank")

            Dim TicketIO As New PdfJobTicketIO()

            Dim LabelPath As String = TicketIO.CreatePersonalizedJobLabelPdf(idsToMxDict)

            If LabelPath <> "" Then
                Dim PrintedOK As Boolean = True
                Try
                    'loads the labels pdf, sets the default printer, shows the dialog
                    rdbLabels.Checked = True
                    Application.DoEvents()
                    rdbLabels.Visible = True
                    pdfInvoice.CoreWebView2.Navigate(LabelPath)


                    MyPrinterMgmt.SetDefaultPrinterByCategory(PrinterCategory.Label_Printer)
                    'pdfInvoice.printWithDialog()
                    pdfInvoice.CoreWebView2.ExecuteScriptAsync("window.print();")
                    'pdfInvoice.CoreWebView2.ShowPrintPreviewDialog()
                    LastShownPersonalizedPDF = LabelPath
                Catch ex As Exception
                    PrintedOK = False
                    Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                    Diagnostics.Process.Start("Explorer.exe", MyPrinterMgmt.GetPrinterByCategory(PrinterCategory.Label_Printer))
                    UpdateStatus("I couldn't make it work. Install the correct printer driver and click print again.", True)
                End Try
                If Not PrintedOK Then
                    If MsgBox("When the printer is installed, press OK to try again", MsgBoxStyle.OkCancel, "Printing Error") = MsgBoxResult.Ok Then
                        Try
                            'loads the labels pdf, sets the default printer, shows the dialog
                            MyPrinterMgmt.SetDefaultPrinterByCategory(PrinterCategory.Label_Printer)
                            'pdfInvoice.printWithDialog()
                            pdfInvoice.CoreWebView2.ExecuteScriptAsync("window.print();")
                            'pdfInvoice.ShowPrintPreviewDialog()
                        Catch ex As Exception
                            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                        End Try
                    End If
                End If

                MyPersonalizeIO.WritePersonalizedXml()
                ' DesignsTable.WriteXml(My.Settings.MxCustomPath)
                'UpdatePQSaveTime()
                UpdateStatus("Labels created.", False)
                'Else
                '    Throw New Exception("Something went wrong...")
            End If



        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            MsgBox(ex.Message.ToString, MsgBoxStyle.OkOnly, "CreateLabelPDF")
            UpdateStatus("Something went wrong, I couldn't create a pdf of the the invoice. :(", True)
        End Try


    End Sub










    Dim LastShownPersonalizedPDF As String = ""
    Private Sub ShowPersonalizedPDF()
        'shows the pdf in pdfinvoice

        Dim FilePath As String = ""

        Try
            rdbLabels.Visible = False
            Select Case True

                Case rdbInvoice.Checked
                    'selectedPersonalizedIDs(WhichMX).FilePath
                    Dim TicketIO As New PdfJobTicketIO
                    Dim title As String = selectedPersonalizedIDs(WhichMX).MxNumber
                    FilePath = My.Settings.dirMxOrders & selectedPersonalizedIDs(WhichMX).MxNumber & ".pdf"
                    If TicketIO.CreatePDF(FilePath.Replace(".pdf", ".txt"), title, FilePath) Then UpdateStatus($"PDF For: { title } created.", False)

                Case rdbBodyPDF.Checked
                    FilePath = selectedPersonalizedIDs(WhichMX).BodyFilePath

                Case rdbCovPDF.CheckAlign
                    FilePath = selectedPersonalizedIDs(WhichMX).CoverFilePath

            End Select

            If LastShownPersonalizedPDF <> FilePath Then

                If File.Exists(FilePath) Then

                    If selectedPersonalizedIDs(WhichMX).ItemNumber.Contains("8101") AndAlso rdbBodyPDF.Checked Then
                        pdfInvoice.CoreWebView2.Navigate(FilePath & "#page=28")
                    Else
                        pdfInvoice.CoreWebView2.Navigate(FilePath)
                    End If

                Else
                    If File.Exists(My.Settings.dirResources & "Oops.pdf") Then
                        pdfInvoice.CoreWebView2.Navigate(My.Settings.dirResources & "Oops.pdf")
                    End If
                End If
            End If
            ' MsgBox(LastShownPersonalizedPDF & vbCrLf & FilePath)
            LastShownPersonalizedPDF = FilePath
        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try


    End Sub

    Private Sub PQ_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvPersonalizeJobQ.CellBeginEdit
        Try
            If Not PersonalizeDGVIsUpToDate() Then
                e.Cancel = True
                Exit Sub
            End If
            CellContents = dgvPersonalizeJobQ.Rows(e.RowIndex).Cells(e.ColumnIndex).Value


        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            '  MsgBox(ex.Message & vbCrLf & "PQ_CellBeginEdit")
        End Try

    End Sub


    'Private Sub PQ_CellEndEdit2(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPersonalizeJobQ.CellEndEdit
    '    ' getting my thoughts out...
    '    Dim Prompt As Boolean = False
    '    If IsDBNull(dgvPersonalizeJobQ.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
    '        Prompt = True
    '    ElseIf CellContents <> dgvPersonalizeJobQ.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Then
    '        Prompt = True
    '    End If

    '    If Prompt Then

    '    End If

    'End Sub


    Private Sub PQ_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPersonalizeJobQ.CellEndEdit
        Try
            '  MsgBox(e.ColumnIndex & vbCrLf & e.RowIndex & vbCrLf & PersonalizeJobQ.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            Dim refreshTable As Boolean = False
            Dim CellValueChanged As Boolean = False


            If IsDBNull(dgvPersonalizeJobQ.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                CellValueChanged = True
            ElseIf CellContents <> dgvPersonalizeJobQ.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Then
                CellValueChanged = True
            End If
            If CellValueChanged = True Then
                If e.ColumnIndex = 5 Then
                    'Print Status

                    'MsgBox("dgv: " & PersonalizeJobQ.Rows(e.RowIndex).Cells(0).Value)
                    MyPersonalizeIO.DisplayedPersonalizeRows(e.RowIndex).AddStatusToHistory()
                    refreshTable = True

                Else
                    Dim str As String = ""

                    'creates a string with before and after results
                    str = vbCrLf & vbCrLf &
                       "It was: """ & CellContents & """" & vbCrLf & vbCrLf &
                       "Change to: " & """" & dgvPersonalizeJobQ.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & """"
                    Dim Result As Integer = MsgBox("Are you sure you want to keep the following changes?" & str, MsgBoxStyle.YesNoCancel, "Keep Changes?")
                    Select Case Result
                        Case MsgBoxResult.Yes

                        Case MsgBoxResult.No, MsgBoxResult.Cancel
                            dgvPersonalizeJobQ.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = CellContents

                    End Select
                End If


            End If


            'DesignsTable.WriteXml(My.Settings.MxCustomPath)
            MyPersonalizeIO.WritePersonalizedXml()

            If refreshTable Then

                'BeginInvoke(New MethodInvoker());
                ' UpdatePQFilter(txtPersonalizeSearch.Text)
            End If


            'updates the time the file was last updated
            ' UpdatePQSaveTime()
        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            ' MsgBox(ex.Message & vbCrLf & "PQ_CellEndEdit")
        End Try

    End Sub



    Private Sub PQ_RowAddedDeleting(sender As Object, e As DataGridViewRowCancelEventArgs) Handles dgvPersonalizeJobQ.UserDeletingRow '
        If Not PersonalizeDGVIsUpToDate() Then
            e.Cancel = True
            Exit Sub
        End If
        If MsgBox("Are you sure you want to delete this entry?", MsgBoxStyle.OkCancel, "Delete?") = MsgBoxResult.Cancel Then
            e.Cancel = True
            Exit Sub
        End If



    End Sub

    Private Sub PQ_RowDeleted(sender As Object, e As DataGridViewRowEventArgs) Handles dgvPersonalizeJobQ.UserDeletedRow
        Try

            'DesignsTable.WriteXml(My.Settings.MxCustomPath)

            'updates the time the file was last updated
            MyPersonalizeIO.WritePersonalizedXml()
        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try


    End Sub





    Private Sub btnArchive_Click(sender As Object, e As EventArgs) Handles btnArchive.Click
        'takes care of "Archiving" the files
        'all it does is changes the "Current" value with "Archive" value in the DateTime Column
        Dim strPrompt As String = ""
        Dim strTitle As String = ""
        Dim WillBeCurrent As Boolean = False
        If rdbCurrent.Checked Then
            strPrompt = "Are you sure you want to move the following DesignID's to the archive?"
            strTitle = "Send to Archive?"
            WillBeCurrent = False

        ElseIf rdbArchive.Checked Then
            strPrompt = "Are you sure you want to restore the following DesignID's?"
            strTitle = "Restore ID's?"
            WillBeCurrent = True

        End If


        If dgvPersonalizeJobQ.SelectedRows.Count > 0 Then
            If strPrompt.Contains("archive") Then
                strPrompt = "Are you sure you want to move all " & dgvPersonalizeJobQ.SelectedRows.Count & " DesignID's to the archive?"
            Else
                strPrompt = "Are you sure you want to restore all " & dgvPersonalizeJobQ.SelectedRows.Count & " DesignID's?"
            End If




            If MsgBox(strPrompt, MsgBoxStyle.YesNo, strTitle) = MsgBoxResult.Yes Then
                For Each row As PersonalizeRowInfo In selectedPersonalizedIDs
                    row.IsCurrent = WillBeCurrent
                Next
                MyPersonalizeIO.WritePersonalizedXml()
                UpdatePQFilter(txtPersonalizeSearch.Text)
            End If

        End If



    End Sub

    Private Sub cmbStatus_LostFocus(sender As Object, e As EventArgs) Handles cmbStatus.LostFocus
        UpdatePQStatus()
    End Sub

    Private Sub cmbStatus_keyDown(sender As Object, e As KeyEventArgs) Handles cmbStatus.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                UpdatePQStatus()
        End Select

    End Sub
    Private Sub cmbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus.SelectedIndexChanged ', cmbStatus.KeyDown
        Label8.Focus()
    End Sub
    Private Sub UpdatePQStatus()
        For Each selRow As PersonalizeRowInfo In selectedPersonalizedIDs
            selRow.AddStatusToHistory()
            selRow.PrintStatus = cmbStatus.Text
            selRow.AddStatusToHistory()
        Next

        'For Each myRow As DataGridViewRow In dgvPersonalizeJobQ.SelectedRows
        '    AddStatusToHistoryPQ(myRow.Index)
        '    myRow.Cells(5).Value = cmbStatus.Text
        '    AddStatusToHistoryPQ(myRow.Index)
        'Next

        MyPersonalizeIO.WritePersonalizedXml()
        'DesignsTable.WriteXml(My.Settings.MxCustomPath)

        'updates the time the file was last updated
        'UpdatePQSaveTime()
        'Beep()
        'Label8.Focus()

    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        WhichMX -= 1
        ShowInfo(True)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        WhichMX += 1
        ShowInfo(True)
    End Sub






#End Region


#Region "Info Bar"

    Private Sub btnHelp_Click(sender As Object, e As EventArgs)


    End Sub
    Private Sub btnSettings_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub pbProducts_Click_1(sender As Object, e As EventArgs) Handles pbProducts.Click
        'right area
    End Sub

    Private Property pbProductsClicked As Boolean = False
    Private Sub PictureBox1_over(sender As Object, e As EventArgs) Handles pbProducts.Click 'MouseClick
        If pbProductsClicked Then
            'it was clicked, now shrink
            'restores picture to original location & size on mouse click
            pbProducts.SetBounds(piclocation(0), piclocation(1), 100, 80)
        Else
            'not clicked yet, now expand
            pbProducts.Parent = Me
            pbProducts.Anchor = AnchorStyles.Bottom And AnchorStyles.Right
            pbProducts.BringToFront()

            'records current picture location on mouse over
            Dim newPicLoc(1) As Integer
            piclocation(0) = Me.Size.Width - 125
            piclocation(1) = Me.Size.Height - 160

            'resizes the picture to larger size
            newPicLoc(0) = piclocation(0) - 410
            newPicLoc(1) = piclocation(1) - 410
            pbProducts.SetBounds(newPicLoc(0), newPicLoc(1), 500, 500)
        End If


        'should toggle clicked between true or false
        pbProductsClicked += 1

    End Sub

    'Private Sub PictureBox1_leave(sender As Object, e As EventArgs) Handles pbProducts.MouseLeave

    '    'restores picture to original location & size on mouse leave
    '    pbProducts.SetBounds(piclocation(0), piclocation(1), 100, 100)
    'End Sub




    Private Sub ToolStripBTP_Click(sender As Object, e As EventArgs) Handles ToolStripBTP.Click
        Try
            Dim btpSite As String = "http://www.bibletruthpublishers.com"
            If Not IsNothing(currentQJob) AndAlso currentQJob.ItemNumber <> cNullInt Then
                btpSite &= "/pd" & currentQJob.ItemNumber
            End If
            Dim web As New frmWebr(btpSite)
            web.Show()
            'Process.Start(btpSite)
        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try

    End Sub

    Private Sub lblCustEmail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblCustEmail.LinkClicked
        'blank, brings to right area...
    End Sub
    Private Sub lblCustEmail_Click(sender As Object, e As EventArgs) Handles lblCustEmail.Click
        selectedPersonalizedIDs(WhichMX).PrepSendEmail(currentMxOrder.Email, currentMxOrder.BillName)
    End Sub
#End Region


#Region "Menu Strips"


#Region "Main Form"

    Private Sub ImposeBookPamEtcToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImposeBookPamEtcToolStripMenuItem.Click
        Dim imp As New frmImposeBkOrPam(currentQJob.uvProdInfo, MyJQProjectDirIO)
        imp.ShowDialog()
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub HelpToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        If My.Computer.Keyboard.CtrlKeyDown Then
            frmHelp.LoadFixList = True
        End If

        'brings up & focuses the help page
        frmHelp.Show()
        frmHelp.Focus()
    End Sub

    Private Sub ErrorLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LineUpErrorLogToolStripMenuItem.Click
        Log.Show()
        Log.Focus()

    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click

        Dim tab As New TabPage("     Printers     ")
        Dim ctrlPt As New ctrlFormPrinters(sqlInfo)
        ctrlPt.Parent = tab
        ctrlPt.Dock = DockStyle.Fill
        ctrlPt.TabControl1.SelectedIndex = 1

        Settings = New Utilities.Settings(sqlInfo.sqlConnStr, My.Settings.PropertyValues)


        If Settings.ShowSettings({tab}.ToList) Then
            My.Settings.Save()
        End If


    End Sub


    Private Sub CreateBarcodeImagesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateBarcodeImagesToolStripMenuItem.Click
        MyJQBarcodeIO.BarcodeGenerator()

    End Sub

    Private Sub CreateStatusLabelsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateStatusLabelsToolStripMenuItem.Click
        'creates labels for the selected rows in frmbarcodestatuses.dgv...
        LineUpTabCtrl.SelectedIndex = 2 'PersonalizeJobQ
        Application.DoEvents()
        Dim TicketIO As New PdfJobTicketIO()
        Dim labelsFP As String = TicketIO.CreateStatusLabelsFromStatusDGV(MyJQBarcodeIO)
        If labelsFP <> "" Then


            Try
                'loads the labels pdf, sets the default printer, shows the dialog
                pdfInvoice.CoreWebView2.Navigate(labelsFP)


                MyPrinterMgmt.SetDefaultPrinterByCategory(PrinterCategory.Label_Printer)
                pdfInvoice.CoreWebView2.ExecuteScriptAsync("window.print();")


                UpdateStatus("Labels created.", False)
            Catch ex As Exception
                Diagnostics.Process.Start("Explorer.exe", MyPrinterMgmt.GetPrinterByCategory(PrinterCategory.Label_Printer))
                UpdateStatus("I couldn't make it work. Install the correct printer driver and click print again.", True)

                Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                MsgBox(ex.Message.ToString & vbCrLf & "CreateLabelPDF")
                UpdateStatus("Something went wrong, I couldn't create a pdf of the the invoice. :(", True)
            End Try

        End If

    End Sub

    Private Sub TractPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TractPreviewToolStripMenuItem.Click
        Dim tract As New frmTractPrev
        tract.Show()
        tract.Focus()


    End Sub

    Private Sub HomePrintTractsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomePrintTractsToolStripMenuItem.Click
        Dim homePrint As New IndesignIO_Impose
        Dim homePrintForm As New ImposeTractHomePrint
        If homePrintForm.ShowDialog = DialogResult.OK Then
            Dim sb As New Text.StringBuilder
            sb.AppendLine("Do you want to create tract pics & home print PDF's for the following " & homePrintForm.FoundFiles.Keys.Count & " tract files?")
            sb.AppendLine("(They will export to: " & My.Settings.dirHomePrintExportLoc & ")" & vbCrLf)
            For Each key As String In homePrintForm.FoundFiles.Keys
                sb.AppendLine(key & " - " & homePrintForm.FoundFiles(key))
            Next
            If MsgBox(sb.ToString, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim counter As Integer = 0
                For Each key As String In homePrintForm.FoundFiles.Keys
                    Dim tr As New TractInfo(key,
                                            homePrintForm.FoundFiles(key),
                                            My.Settings.dirHomePrintExportLoc,
                                            True,
                                            True,
                                            True)
                    homePrint.ImposeHomePrintTracts(tr)
                    counter += 1
                    Dim msg As String = "Creating Tract Files(" & counter & " of " & homePrintForm.FoundFiles.Count & ")"
                    UpdateStatus(msg, False)
                    Application.DoEvents()
                Next
            End If

        End If


    End Sub

    Private Sub PurchasingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchasingToolStripMenuItem.Click
        Process.Start(Path.Combine(My.Settings.dirResources, "QP Supplies Inventory & Production Purchasing Log.xlsx"))
    End Sub

    Private Sub ComputerSoftwareKeysToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ComputerSoftwareKeysToolStripMenuItem.Click

        Dim procStartInfo As New ProcessStartInfo
        Dim procExecuting As New Process
        Dim arg As String = Path.Combine(My.Settings.dirResources, "BTP Software Licenses.xlsx")
        Dim quote As String = Chr(34)

        If File.Exists(arg) Then
            With procStartInfo
                .UseShellExecute = True
                .FileName = "excel.exe"
                .Arguments = quote & arg & quote
                .WindowStyle = ProcessWindowStyle.Normal
                .Verb = "runas" 'add this to prompt for elevation
            End With

            procExecuting = Process.Start(procStartInfo)
        End If


    End Sub

    Private Sub CreateBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateBackupToolStripMenuItem.Click
        CreateDGVBackup(True)
    End Sub


    Private Sub ShareWordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShareWordToolStripMenuItem.Click
        Dim manage As New frmSW_Manager

        manage.Show()
    End Sub



    Private Sub FindFoldersWithMultipleFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindFoldersWithMultipleFilesToolStripMenuItem.Click
        Dim openFolder As New FolderBrowserDialog
        openFolder.SelectedPath = My.Settings.dirShareWordProjects
        If MsgBox("This is a utility that goes through the share word folders and looks for folders that have multiple files and moves them to a subdirectory called 'Multiple'.", MsgBoxStyle.OkCancel, "Do you wish to continue?") = MsgBoxResult.Ok Then
            If openFolder.ShowDialog = DialogResult.OK Then
                Dim sw As New ShareWordIO(openFolder.SelectedPath)
                sw.moveFoldersWithMultipleFiles("0 - Multiple")
            End If
        End If

    End Sub


    Private Sub BatchCreateProductionFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatchCreateProductionFilesToolStripMenuItem1.Click
        'Dim proj As DirectoryInfo = New DirectoryInfo(My.Settings.dirShareWordProjects)
        'Dim prod As DirectoryInfo = New DirectoryInfo(My.Settings.dirShareWordProduction)
        'Dim productionFI As List(Of FileInfo) = New DirectoryInfo(prod).GetFiles.ToList


        'MsgBox("Feature is currently unavailable...")

        'Dim openFolder As New FolderBrowserDialog
        'openFolder.SelectedPath = My.Settings.dirShareWordProjects
        'If MsgBox("This is a utility that goes through the sub folders and tries to create a production pdf for each file found. Only works when 1 file is found per folder.", MsgBoxStyle.OkCancel, "Do you wish to continue?") = MsgBoxResult.Ok Then
        '    If openFolder.ShowDialog = DialogResult.OK Then
        '        Dim wordConn As New WordConnection
        '        Dim shareWordProd As ProductionDirInfo = Nothing
        '        For Each myProd As ProductionDirInfo In MyProductionIO.ProdTypes
        '            If myProd.ProductType = cShareWord Then
        '                'finds the shareword product type. (trying to get the production directory)
        '                shareWordProd = myProd
        '                Exit For
        '            End If
        '        Next
        '        Dim sharedirs As New DirectoryInfo(openFolder.SelectedPath)
        '        For Each shareDir As DirectoryInfo In sharedirs.GetDirectories
        '            Dim itemNum As String = shareDir.Name.Split("-")(1).Trim
        '            Dim files() As FileInfo = shareDir.GetFiles
        '            If files.Count = 1 Then
        '                Dim pdfPath As String = Path.Combine(shareWordProd.bodDir, itemNum & ".pdf")
        '                If File.Exists(pdfPath) Then
        '                    File.Delete(pdfPath)
        '                End If
        '                wordConn.exportWordToPDF(files(0).FullName, pdfPath)
        '                Threading.Thread.Sleep(500)
        '            End If
        '        Next

        '    End If
        'End If

    End Sub

    Private Sub HarvestShareWordFilesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HarvestShareWordFilesToolStripMenuItem1.Click
        If My.Computer.Name.ToLower = "pc-cb" Then
            Dim wordConn As New WordConnectionIO
            Dim prods As New List(Of UvProductInfo)
            prods.AddRange(MyUvProductInfoIO.productDict.Values.ToList)
            Dim openFolder As New FolderBrowserDialog
            openFolder.SelectedPath = "\\pc-john\C\Users\John\Documents\"
            If openFolder.ShowDialog = DialogResult.OK Then
                wordConn.MatchDocsToProducts(prods, openFolder.SelectedPath, ProgressBar1)
            End If
        Else
            MsgBox("Sorry... Still testing this feature")
        End If
    End Sub


    Private Sub ShowPrinterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowPrinterToolStripMenuItem.Click

        'If frmPrinter.ShowDialog = DialogResult.OK Then
        '    SetDefaultPrinter(frmPrinter.SelectedPrinter)
        '    MsgBox("The default printer is: " & GetDefaultPrinter())

        'End If
        MsgBox("The default printer is: " & MyPrinterMgmt.GetDefaultPrinter())
        Dim p As New GetPrinterStatus
        p.popP()
    End Sub



#End Region


#Region "Tab JobQ"

    Private Sub JobQMenuStrip_Opening(sender As Object, e As CancelEventArgs) Handles JobQMenuStrip.Opening
        If MyJQRowIO.currentDisplay = JQRowIO.DisplayOptions.DisplayCurrent Then
            menuMoveToArchive.Enabled = True
        Else
            menuMoveToArchive.Enabled = False
        End If

        Try
            'load the project folders into the tool strip menu
            Dim projMenu As ToolStripDropDownItem = SearchForProjectFolderToolStripMenuItem
            Dim projMenuItemsCt As Integer = 0
            projMenu.DropDownItems.Clear()
            Dim ExportCategories As List(Of ProductCategory) = {ProductCategory.Book, ProductCategory.Mini_Economy_Book, ProductCategory.Pamphlet, ProductCategory.Mini_Pamphlet}.ToList



            'clear the production menu & re-add items.
            Dim prodMenu As ToolStripDropDownItem = ProductionToolStripMenuItem 'ProductionFilesToolStripMenuItem
            prodMenu.DropDownItems.Clear()
            prodMenu.DropDownItems.Add(ExportFilesToPrintersToolStripMenuItem)
            prodMenu.DropDownItems.Add(CopyProductionFilesToDesktopToolStripMenuItem)
            prodMenu.DropDownItems.Add(New ToolStripSeparator)


            Dim ct As Integer = 0
            For Each job As JQRowInfo In MyJQRowIO.GetSelectedJobs
                If IsNothing(job.uvProdInfo) Then job.uvProdInfo = MyUvProductInfoIO.findProduct(job.ItemNumber)
                If IsNothing(job.uvProdInfo) Then job.uvProdInfo = New UvProductInfo(False) 'added because it errors out if uv products haven't loaded yet

                If IsNothing(job.ProjectDirs) Then job.ProjectDirs = MyJQProjectDirIO.GetDirectories(job)


                For Each dirInfo As JQProjectDirInfo In job.ProjectDirs
                    If projMenu.DropDownItems.Count > 0 Then
                        'there are previous items. adds a toolstrip separator
                        projMenu.DropDownItems.Add(New ToolStripSeparator)
                    End If

                    'click to open project folder
                    Dim tsDirInfo As New ToolStripMenuItem($"Open { dirInfo.ProjectDirectory.FullName }")
                    AddHandler tsDirInfo.Click, AddressOf ProjectFolderMenuItemClicked
                    projMenu.DropDownItems.Add(tsDirInfo)

                    For Each cover As FileInfo In dirInfo.FindFiles(BodyVsCover.Cover)

                        Dim tsImpose As New ToolStripMenuItem($"Impose '{ cover.Name }' as...") 'ts item to hold the sub imposition tool strip items


                        For Each category As ProductCategory In ExportCategories
                            Dim tsCategory As New ToolStripMenuItem(category.ToString.Replace("_", " "))
                            'this next part would turn the correct ts item's back color green, but it doesn't distinguish between mini & normal pamphlets
                            'If dirInfo.ProjectType = category Then
                            '    tsCategory.BackColor = Color.LightGreen
                            'End If
                            AddHandler tsCategory.Click, AddressOf ImposeCoverMenuItemClicked
                            tsImpose.DropDownItems.Add(tsCategory)
                        Next
                        projMenu.DropDownItems.Add(tsImpose)
                        'projMenu.DropDownItems.Add(New ToolStripSeparator)



                        projMenuItemsCt += 1
                        projMenu.DropDownItems.Add(tsImpose)
                    Next


                Next


                'add production file items
                job.ProductionFiles = MyJQProductionIO.GetProductionFiles(job.ItemNumber)
                For Each file As JQProductionFileInfo In job.ProductionFiles
                    prodMenu.DropDownItems.Add(file.ProductionFolderMenuItem)
                    ct += 1
                Next
            Next

            If projMenuItemsCt = 0 Then
                'reset projmenu
                projMenu.DropDownItems.Clear()
            End If

            If ct = 1 Then
                prodMenu.Text = "Production (1 File)"
            Else
                prodMenu.Text = $"Production ({ ct } Files)"
            End If
        Catch ex As Exception

        End Try

    End Sub



    Private Sub CopyProductionFilesToDesktopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyProductionFilesToDesktopToolStripMenuItem.Click
        Dim selJobs As List(Of JQRowInfo) = MyJQRowIO.GetSelectedJobs()
        Dim count As Integer = 0
        For Each job As JQRowInfo In selJobs
            job.ProductionFiles = MyJQProductionIO.GetProductionFiles(job.ItemNumber)
            count += job.ProductionFiles.Count
        Next

        If count = 0 Then
            MsgBox("Sorry, no files found to copy.")
        Else
            If MsgBox($"Do you want to copy all { count } file(s) to the desktop?", MsgBoxStyle.YesNo, "Copy Production Files") = MsgBoxResult.Yes Then
                count = 0
                Dim baseFP As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Production Files") ' - { Date.Today.ToShortDateString.Replace("/", "-") }")
                If Not Directory.Exists(baseFP) Then
                    Directory.CreateDirectory(baseFP)
                End If
                For Each job As JQRowInfo In selJobs
                    For Each prodFile As JQProductionFileInfo In job.ProductionFiles
                        prodFile.CopyProductionFile(job.ItemNumber, job.OrderQuantity, baseFP, False)
                        count += 1
                    Next
                Next
                MsgBox($"Copied { count } files", MsgBoxStyle.Information, "Production Files Copied")
            End If
        End If

    End Sub


    Public Sub ProductFolderMenuItemClicked(ByVal sender As Object, e As System.EventArgs)
        Dim mi As ToolStripMenuItem = sender
        Process.Start("explorer.exe", Chr(34) & mi.Text & Chr(34))
    End Sub
    Public Sub ProjectFolderMenuItemClicked(ByVal sender As Object, e As System.EventArgs)

        If TypeOf sender Is ToolStripMenuItem Then
            Dim mi As ToolStripMenuItem = sender
            Dim txt As String = mi.Text.Replace("Open ", "")
            If txt = "Search For more..." Then
                Dim searchbox As New frmImposeBkOrPam(currentQJob.uvProdInfo, MyJQProjectDirIO)
                searchbox.ShowDialog()
            Else
                Process.Start("explorer.exe", Chr(34) & txt & Chr(34))
            End If
            'MsgBox(mi.Text)
        End If


    End Sub

    Private Sub ImposeCoverMenuItemClicked(ByVal sender As Object, e As EventArgs)
        If TypeOf sender Is ToolStripItem Then
            Dim tsItem As ToolStripItem = sender
            Dim tsParent As ToolStripItem = tsItem.OwnerItem
            Dim ImposeAs As ProductCategory = ProductCategoryFromString(tsItem.Text)
            If ImposeAs > -1 Then
                Dim fn As String = tsParent.Text.Replace("Impose '", "").Replace("' as...", "") ' the file name without "Impose  as..."
                For Each job As JQRowInfo In MyJQRowIO.GetSelectedJobs
                    If Not IsNothing(job.ProjectDirs) Then
                        For Each projDir In job.ProjectDirs
                            For Each coverFile In projDir.FindFiles(BodyVsCover.Cover)
                                If coverFile.Name = fn Then
                                    Dim imp As New IndesignIO_Impose
                                    If imp.ImposeProductCover(coverFile, job.ItemNumber, ImposeAs, job.Title) Then
                                        btnRefresh.PerformClick()
                                        Application.DoEvents()
                                        MsgBox($"Cover imposed for { job.getSummaryStr }.")
                                    End If


                                    Exit Sub
                                End If
                            Next
                        Next
                    End If
                Next
            End If


        End If


    End Sub




    Private Sub menuMoveToArchive_Click(sender As Object, e As System.EventArgs) Handles menuMoveToArchive.Click

        'for the most part this sub handles copying to and from the jobq to the history

        Try
            Dim archiveJobs As New List(Of JQRowInfo)
            Dim ArchiveMsg As String = ""
            Select Case dgvJobQ.SelectedRows.Count
                Case 0
                    MsgBox("There are no rows selected. " & vbCrLf & vbCrLf &
                    "Select the rows you wish To archive by:" & vbCrLf &
                    "clicking the rectangle at the far left of the row.", MsgBoxStyle.Information, "Select rows to archive")
                    Exit Sub
                Case Else
                    ArchiveMsg = "Are you sure you want to move the following item(s) to archive?" & vbCrLf & vbCrLf

            End Select
            For Each aRow As DataGridViewRow In dgvJobQ.SelectedRows
                Dim jobId As Integer = aRow.Cells(col_JobID).Value
                For Each job As JQRowInfo In MyJQRowIO.DisplayedJobs
                    If job.JobID = jobId Then
                        'found match
                        archiveJobs.Add(job)
                        ArchiveMsg &= job.getSummaryStr & vbCrLf
                    End If
                Next
            Next

            'records result from question
            If MsgBox(ArchiveMsg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                For Each archJob As JQRowInfo In archiveJobs
                    archJob.IsActive = False
                Next
                MyJQRowIO.updateSql()
                loadSql()
            End If



        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try

    End Sub


    Private Sub DeleteRowsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteRowsToolStripMenuItem.Click
        If MyJQRowIO.dgv.SelectedRows.Count > 0 Then
            MyJQRowIO.RemoveDGVRow()
        Else
            MsgBox("There are no rows selected. " & vbCrLf & vbCrLf &
                   "Select the rows you wish to delete by" & vbCrLf &
                   "clicking the rectangle at the far left of the row.", MsgBoxStyle.Information, "Select rows to delete")
        End If

    End Sub


#End Region


#Region "Tab PersonalizeJobQ"


    Private Sub pdfInvoice_Enter(sender As Object, e As EventArgs)
        'AllowsPDFFocus = True
    End Sub

    Private Sub pdfInvoice_GotFocus(sender As Object, e As EventArgs)
        'If AllowsPDFFocus = False Then
        '    FocusTimer.Start()
        'End If
    End Sub

    Private Sub PersonalizeJobQContextMenu_Opening(sender As Object, e As CancelEventArgs) Handles PersonalizeJobQContextMenu.Opening
        SendToArchiveToolStripMenuItem.Enabled = True
        RestoreIDsFromArchiveToolStripMenuItem.Enabled = True
        Select Case dgvPersonalizeJobQ.SelectedRows.Count
            Case 0
                e.Cancel = True
            Case Else
                Select Case True
                    Case rdbArchive.Checked
                        SendToArchiveToolStripMenuItem.Enabled = False
                    Case rdbCurrent.Checked
                        RestoreIDsFromArchiveToolStripMenuItem.Enabled = False

                End Select
        End Select


    End Sub

    Private Sub RefreshPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshPageToolStripMenuItem.Click
        btnDesignsRefresh_Click(sender, e)
    End Sub

    Private Sub SendToArchiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendToArchiveToolStripMenuItem.Click
        btnArchive_Click(sender, e)
    End Sub

    Private Sub RestoreIDsFromArchiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreIDsFromArchiveToolStripMenuItem.Click
        btnArchive_Click(sender, e)
    End Sub

    Private Sub PrintLabelsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintLabelsToolStripMenuItem.Click
        'btnPrintLabels_Click(sender, e)
        CreatePersonalizedJobLabel()
    End Sub

    Private Sub ImposeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImposeToolStripMenuItem.Click
        btnImpose_Click(sender, e)
    End Sub



    Private Sub OrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderToolStripMenuItem.Click
        cmbStatus.Text = OrderToolStripMenuItem.Text
        UpdatePQStatus()
    End Sub

    Private Sub ImposedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImposedToolStripMenuItem.Click
        cmbStatus.Text = ImposedToolStripMenuItem.Text
        UpdatePQStatus()
    End Sub

    Private Sub Printing1200aToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Printing1200aToolStripMenuItem.Click
        cmbStatus.Text = Printing1200aToolStripMenuItem.Text
        UpdatePQStatus()
    End Sub

    Private Sub Printing1200bToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Printing1200bToolStripMenuItem.Click
        cmbStatus.Text = Printing1200bToolStripMenuItem.Text
        UpdatePQStatus()
    End Sub

    Private Sub DrilledToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DrilledToolStripMenuItem.Click
        cmbStatus.Text = DrilledToolStripMenuItem.Text
        UpdatePQStatus()
    End Sub

    Private Sub FilledToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FilledToolStripMenuItem.Click
        cmbStatus.Text = FilledToolStripMenuItem.Text
        UpdatePQStatus()
    End Sub

    Private Sub ShippedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShippedToolStripMenuItem.Click
        cmbStatus.Text = ShippedToolStripMenuItem.Text
        UpdatePQStatus()
    End Sub




    Private Sub CopyFileNamesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CopyFileNamesToolStripMenuItem1.Click
        Try
            'this merely creates a string with the different file names of the pdf's in it. formatted so that it can be pasted into the indesign open file dialog.
            Dim NewList As String = ""
            For Each myRow As DataGridViewRow In dgvPersonalizeJobQ.SelectedRows
                NewList &= """" & myRow.Cells(2).Value.ToString & "-Q" & myRow.Cells(3).Value.ToString & ".pdf" & """ "

            Next
            Clipboard.SetText(NewList.Trim)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub CopyFilesToDesktopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyFilesToDesktopToolStripMenuItem.Click, CopyFilesToDesktopToolStripMenuItem1.Click
        Dim sb As New Text.StringBuilder
        For Each selOrder As PersonalizeRowInfo In selectedPersonalizedIDs
            If File.Exists(selOrder.BodyFilePath) Then
                sb.AppendLine(selOrder.BodyFilePath)
            End If
        Next

        If MsgBox("Do you want to copy the following files to the desktop?" & vbCrLf & vbCrLf & sb.ToString, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            sb.Clear()

            For Each selOrder In selectedPersonalizedIDs
                Dim aFile As String = selOrder.BodyFilePath
                If File.Exists(aFile) Then
                    Try
                        'MsgBox(aFile)
                        Dim FiInfo As New FileInfo(aFile)
                        Dim FiName As String = selOrder.DesignId & "-Q" & selOrder.DesignQuantity & FiInfo.Extension
                        'Dim FiName As String = Path.GetFileNameWithoutExtension(aFile)
                        'FiName = selOrder.DesignId & "-Q" & selOrder.DesignQuantity & FiInfo.Extension
                        'FiName &= "-Q" & selOrder.DesignQuantity & FiInfo.Extension
                        Dim newFp As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FiName)
                        File.Copy(aFile, newFp)
                        sb.AppendLine(newFp)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            Next
            MsgBox("The following files were copied okay:" & vbCrLf & vbCrLf & sb.ToString, MsgBoxStyle.Information, "Copied Successfully")
        End If
    End Sub



    Private Sub ExportGPPrtABCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportGPPrtABCToolStripMenuItem.Click
        ExportGP()
    End Sub

    Private Sub ExportTo1200a1200bToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportGPPrtABCToolStripMenuItem2.Click
        ExportGP()
    End Sub

    Private Sub ExportTo1200aToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportTo1200aToolStripMenuItem.Click
        ExportGP(True, False, False)
    End Sub

    Private Sub ExportTo1200bToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportTo1200bToolStripMenuItem.Click
        ExportGP(False, True, False)
    End Sub

    Private Sub ExportTo6120aToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportTo6120aToolStripMenuItem.Click
        ExportGP(False, False, True)
    End Sub


    ''' <summary>
    ''' This calls indesign and relinks the files and exports to the hot folders (most settings specified in the script file)
    ''' </summary>
    ''' <param name="printerA">send to 1250p</param>
    ''' <param name="printerB">send to 1200b</param>
    ''' <param name="printerC">send to 6120a</param>
    Private Sub ExportGP(Optional ByVal printerA As Boolean = True,
                         Optional ByVal printerB As Boolean = True,
                         Optional ByVal printerC As Boolean = True)

        Try
            If (printerA Or printerB Or printerC) Then
                Dim ScriptArgList As New List(Of String)
                Dim ReturnData As String = ""
                Dim lstReturnData As New List(Of String)

                'include which printers?
                If printerA Then ScriptArgList.Add("PrtA")
                If printerB Then ScriptArgList.Add("PrtB")
                If printerC Then ScriptArgList.Add("PrtC")

                Dim foundFile As Boolean = False
                For Each selOrder In selectedPersonalizedIDs
                    'goes through selected rows, and if it's GP, it adds the file name to the array
                    If selOrder.ItemNumber = 8101 Then
                        Dim aFile As String = selOrder.BodyFilePath
                        If File.Exists(aFile) Then
                            Dim FiInfo As New FileInfo(aFile)
                            ScriptArgList.Add("?" & FiInfo.Name)
                            foundFile = True
                        End If
                    End If
                Next
                Dim ScriptArgs() As String = ScriptArgList.ToArray

                If foundFile Then
                    Dim idApp As InDesign.Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)
                    ReturnData = idApp.DoScript(My.Settings.dirInddScripts & "Export Gop.jsx", InDesign.idScriptLanguage.idJavascript, ScriptArgs)
                    If ReturnData <> "" Then
                        'Throw New Exception(ReturnData.to)
                    End If
                Else
                    MsgBox("No Gospel of Peace files found / selected!", MsgBoxStyle.Exclamation)
                End If
            Else
                MsgBox("Please make sure to include a printer!", MsgBoxStyle.Exclamation)
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



    End Sub



#End Region



#End Region




    Private Function returnFilePath(ByVal itemNumber As String, ByRef folderPath As String) As String
        Dim fp As String = ""
        folderPath = ""


        Dim sourceFolder As New List(Of String)
        'searches the folders in the search location
        For Each Location As String In My.Settings.PrESearchLoc.Split(";")
            sourceFolder.Add(Location)
        Next

        Dim AuthorLastName() = currentQJob.uvProdInfo.Author.ToLower.Split(" ")

        Dim results As New List(Of FileInfo)

        Try

            ' Make a reference to a directory.
            For Each directory In sourceFolder
                Dim dirinfo As New DirectoryInfo(directory)
                ' Get a reference to each folder in that directory.
                Dim FolderArray As DirectoryInfo() = dirinfo.GetDirectories()


                'Dim mySearch() As String = txtImposeSearch.Text.Split(";")
                For Each fri As DirectoryInfo In FolderArray

                    If Not itemNumber = "" Then
                        If fri.Name.ToLower.Contains(itemNumber.ToLower) Then
                            'if it is, then it is added to an internal list
                            folderPath = fri.FullName

                            fp = returnFinalFile(fri.FullName, ".pdf", "final")

                            Exit For
                        End If
                    End If
                Next fri



            Next


        Catch ex As Exception
            Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            MsgBox(ex.Message.ToString & vbCrLf & "btnSearchPrntEbk_Click", MsgBoxStyle.OkCancel, "Something Failed... ")
        End Try


        Return fp
    End Function

    Private Function returnFinalFile(ByVal dirPath As String, ByVal whatExtension As String, ByVal whatToLookFor As String) As String
        Dim FinalFile As String = ""
        If Directory.Exists(dirPath) Then
            Dim rootDir As New DirectoryInfo(dirPath)
            Dim searchDirs As New List(Of DirectoryInfo)
            searchDirs.Add(rootDir) 'added the root to search through first.
            searchDirs.AddRange(rootDir.GetDirectories) 'added sub dirs to search through next.


            For Each myDir As DirectoryInfo In searchDirs
                If myDir.FullName.ToLower.Contains("archive") Or myDir.FullName.ToLower.Contains("cover") Then
                    'ignores folders
                Else
                    For Each rootFile As FileInfo In myDir.GetFiles
                        If rootFile.Extension.ToLower = whatExtension.ToLower Then
                            If rootFile.Name.ToLower.Contains(whatToLookFor.ToLower) And rootFile.Name.ToLower.Contains("cover") = False Then
                                FinalFile = rootFile.FullName
                                Return FinalFile
                                'if found, returns final file & skips rest of processing
                            End If
                        End If
                    Next
                End If

            Next

            'For each subDir As DirectoryInfo In 
        End If
        Return FinalFile

    End Function


    Private Sub GetGPEmailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetGPEmailsToolStripMenuItem.Click
        Dim showSaveDlg As Boolean = False
        Dim emails As New List(Of String)
        If MsgBox("This allows you to select a folder and get the folder names from subfolders. Do you wish to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            Dim opnFold As New FolderBrowserDialog
            If Directory.Exists(My.Settings.dir8101Users) Then
                opnFold.SelectedPath = My.Settings.dir8101Users
            Else
                opnFold.SelectedPath = "C:\"
            End If
            If opnFold.ShowDialog = DialogResult.OK Then
                showSaveDlg = True
                If Directory.Exists(opnFold.SelectedPath) Then
                    Dim dirInfo As New DirectoryInfo(opnFold.SelectedPath)
                    For Each subFolder As DirectoryInfo In dirInfo.GetDirectories
                        If Not emails.Contains(subFolder.Name) Then
                            If subFolder.Name.Contains("@") Then
                                emails.Add(subFolder.Name)
                            End If
                        End If
                    Next
                End If

            End If
            If MsgBox("If you wish to copy the designs folder from the 1&1 server to a local folder, I can go through the folder and find any designs in xml format for GP." & vbCrLf & vbCrLf & "Do you want to try?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If opnFold.ShowDialog Then
                    Dim counter As Integer = 0
                    showSaveDlg = True
                    Dim dir As New DirectoryInfo(opnFold.SelectedPath)

                    For Each myFile As FileInfo In dir.GetFiles("*.xml", SearchOption.TopDirectoryOnly)
                        Dim fileText As String = File.ReadAllText(myFile.FullName)
                        If fileText.Contains("CustomTractCard") Or fileText.Contains("CustomPocketCalendar") Then
                            'non gp calendar
                        Else
                            Dim AddEmail As Boolean = False
                            Dim xmlDoc As New XmlDocument()
                            xmlDoc.Load(myFile.FullName)
                            For Each xmlDocNode As XmlNode In xmlDoc.ChildNodes
                                Select Case xmlDocNode.Name
                                    Case "Design"
                                        For Each designNode As XmlNode In xmlDocNode.ChildNodes
                                            Select Case designNode.Name
                                                Case "UserID"
                                                    Dim txt As String = designNode.InnerText
                                                    If Not emails.Contains(txt) Then
                                                        If txt.Contains("@") Then
                                                            counter += 1
                                                            emails.Add(txt)
                                                        End If

                                                    End If
                                            End Select

                                        Next
                                End Select

                            Next

                        End If

                    Next

                    MsgBox(counter.ToString)
                End If
            End If
        End If
        If emails.Count > 0 And showSaveDlg Then
            emails.Sort()
            Dim savefileDlg As New SaveFileDialog
            savefileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            savefileDlg.Filter = "TextFile|*.txt"
            savefileDlg.FileName = "Names"
            If savefileDlg.ShowDialog = DialogResult.OK Then
                Dim sb As New Text.StringBuilder
                For Each myItem In emails
                    sb.AppendLine(myItem.ToLower)
                Next
                IO.File.WriteAllText(savefileDlg.FileName, sb.ToString)
            End If
        End If
    End Sub






    Private Sub PrintShareWordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintShareWordToolStripMenuItem.Click

        Dim errorMsg As New Text.StringBuilder

        Dim jobs As List(Of JQRowInfo) = MyJQRowIO.GetSelectedJobs
        If jobs.Count = 1 And jobs(0).uvProdInfo.SupID = cShareWordSupID Then
            'selection ok

            MyPrinterMgmt.SetDefaultPrinterByCategory(PrinterCategory.ShareWord_Printer)
            LineUpTabCtrl.SelectedTab = tabPrint

        Else
            errorMsg.AppendLine("Please make sure to select 1 ShareWord Product!")
        End If

        If errorMsg.ToString <> "" Then
            MsgBox(errorMsg, MsgBoxStyle.Critical, "Printing Share Word")
        End If
    End Sub
    Dim inited As Boolean = False
    Private Property uvPrompt As String = ""
    Private Sub InvToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvToolStripMenuItem.Click

        If My.Computer.Name.ToLower.Contains("pc-cb") Then
            Try
                Dim uvConn As New Utilities.UniVerseConnection("", "")
                If Not inited Then
                    inited = True 'only puts in the Select Query at the first run

                    uvPrompt = "SELECT ITEM FROM PRODUCTS WHERE " & uvConn.quoted("BULK.LOC") & " <> ''"
                    Dim SelectProductQuery As String = "SELECT @ID, TITLE, SUBTITLE, INVERT, PRICE, PAGES, AUTHOR, LANGUAGE, TYPE, SOURCE, YEAR, " &
                                uvConn.quoted({"COVER.2", "SUP.ITEM", "SUB.TYPE", "WEB.TEXT", "CATALOG.TEXT", "SUP.ID"}) & " FROM PRODUCTS WHERE @ID = 0000"
                    Dim SelectOrdersQuery As String = "SELECT @ID, DATE.INV, ITEM.CC, QUAN.CC, DATE.PAID FROM MX.INVOICES WHERE @ID = '321100'"
                    uvPrompt = SelectProductQuery
                    'prompt = SelectOrdersQuery
                End If

                Dim newInput As String = InputBox("Please enter UV SQL Command", "UV SQL String", uvPrompt)
                If newInput <> "" Then
                    uvPrompt = newInput
                    ' prodUVinfo.productDict.Clear()
                    If uvConn.UVAccess(uvPrompt) Then


                        Dim resultsDT As DataTable = uvConn.DS.Tables(0).Copy

                        Dim test As New frmTest
                        Dim testDSource As New BindingSource
                        testDSource.DataSource = resultsDT
                        test.dgvTest.DataSource = testDSource
                        test.Show()
                    Else
                        InvToolStripMenuItem.PerformClick()
                    End If

                End If
                Exit Sub

                Dim fp As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "BTP Items Ordered Last 5 Years.csv")
                Console.WriteLine("Starting...")
                Using sr As New StreamReader(fp)
                    While (sr.Peek() > 0)

                        Dim parts() As String = sr.ReadLine().Split(",")
                        If parts.Count = 2 Then
                            Dim key As String = parts(0)
                            Dim value As String = parts(1)
                            If IsNumeric(key) And IsNumeric(value) Then
                                Dim item As UvProductInfo = MyUvProductInfoIO.findProduct(key)
                                MyUvProductInfoIO.productDict(key).TempOrdered += CInt(value)
                                Console.WriteLine(key)

                            End If
                        End If
                    End While
                End Using

            Catch ex As Exception
                MsgBox(ex.Message)
                InvToolStripMenuItem.PerformClick()
            End Try

            'OutputProductsToolStripMenuItem.PerformClick()
        Else
            MsgBox("sorry... you're not authorized")
        End If
    End Sub

    Private Sub OutputProductsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutputProductsToolStripMenuItem.Click

        Try

            Dim fp As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Products " & Date.Today.ToString("yyyy.MM.dd") & ".xml")

            'Dim ds As DataSet = prodUVinfo.prodInfoDS


            If MsgBox("Do you want to export the " & MyUvProductInfoIO.productDict.Values.Count & " products as xml to: " & vbCrLf & vbCrLf & fp, MsgBoxStyle.YesNo, "Export Products?") = MsgBoxResult.Yes Then
                Dim XmlSettings As New XmlWriterSettings
                XmlSettings.OmitXmlDeclaration = False
                XmlSettings.Indent = True
                Dim currentOutput As String = ""
                Dim writer As XmlWriter = XmlWriter.Create(fp, XmlSettings)
                Log.addError("Starting Export!", "OutputProductsToolStripMenuItem_Click", False)
                Log.addError("Starting Export!", "OutputProductsToolStripMenuItem_Click", True)
                Try
                    writer.WriteStartDocument()
                    writer.WriteStartElement("Items")
                    For Each key As String In MyUvProductInfoIO.productDict.Keys
                        Dim prod As UvProductInfo = MyUvProductInfoIO.productDict(key)
                        writer.WriteStartElement("Item")
                        currentOutput = prod.ToString

                        WriteXmlSafeString(Col_ProductID, prod.ItemNum, writer)
                        WriteXmlSafeString(Col_Title, prod.Title, writer)
                        WriteXmlSafeString(Col_TitleInverted, prod.InvTitle, writer)
                        WriteXmlSafeString(Col_Price, prod.SalePrice, writer)
                        WriteXmlSafeString(Col_Pages, prod.PageCt, writer)
                        WriteXmlSafeString(Col_PageSize, prod.PageSize, writer)
                        WriteXmlSafeString(Col_Author, prod.Author, writer)
                        WriteXmlSafeString(Col_Medium, prod.Type, writer)
                        WriteXmlSafeString(Col_Source, prod.Source, writer)
                        WriteXmlSafeString(Col_WebText, prod.WebText, writer)
                        WriteXmlSafeString(Col_CatalogText, prod.CatalogText, writer)
                        WriteXmlSafeString(Col_Type, prod.TypeUV, writer)
                        WriteXmlSafeString(Col_Language, prod.Language, writer)
                        WriteXmlSafeString(Col_SubType, prod.SubType, writer)
                        WriteXmlSafeString(col_SupItem, prod.SupItem, writer)
                        WriteXmlSafeString(col_SupID, prod.SupID, writer)
                        WriteXmlSafeString(col_Year, prod.Year, writer)

                        writer.WriteEndElement() 'item
                    Next
                    writer.WriteEndElement() 'items
                    writer.WriteEndDocument()
                    writer.Flush()
                    writer.Close()
                Catch ex As Exception
                    Log.addError(ex.Message, "OutputProductsToolStripMenuItem_Click", True)
                    MsgBox(ex.Message & vbCrLf & vbCrLf & "Errored on: " & currentOutput)
                    Try
                        writer.Dispose()
                    Catch ex2 As Exception
                        Log.addError(ex2.Message, "OutputProductsToolStripMenuItem_Click", True)
                    End Try
                End Try
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



    End Sub
    Private Sub WriteXmlSafeString(ByVal columnName As String, ByVal value As String, ByRef writer As XmlWriter)
        value = value.Trim
        Dim sb As New Text.StringBuilder
        Dim ch As Char
        For i As Integer = 0 To value.Length - 1
            ch = value(i)

            If XmlConvert.IsXmlChar(ch) Then
                sb.Append(ch)
            End If
        Next
        writer.WriteStartElement(columnName)
        writer.WriteString(sb.ToString)
        writer.WriteEndElement()
        'writer.Flush()


    End Sub


    Private Sub SetDefaultPrinterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetDefaultPrinterToolStripMenuItem.Click
        Dim frm As New Form
        frm.Size = New Size(800, 500)
        frm.Text = "Select Default Printer"

        Dim pctrl As New ctrlFormPrinters(sqlInfo, frm)
        pctrl.Parent = frm
        pctrl.Dock = DockStyle.Fill

        frm.ShowDialog()
    End Sub




    Private Sub ShowJobDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowJobDetailsToolStripMenuItem.Click
        For Each job As JQRowInfo In MyJQRowIO.GetSelectedJobs
            job.ProductionFiles = MyJQProductionIO.GetProductionFiles(job.ItemNumber)

            Dim sb As New Text.StringBuilder
            sb.AppendLine(job.ToString)
            For Each productionFileInfo As JQProductionFileInfo In job.ProductionFiles
                sb.AppendLine(productionFileInfo.ToString)
            Next
            MsgBox(sb.ToString)

        Next

    End Sub


    Private Sub FinishedYearCountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinishedYearCountToolStripMenuItem.Click
        MsgBox(MyJQRowIO.getYearsCt)
    End Sub


    Private Sub tmrSendStatusEmail_Tick(sender As Object, e As EventArgs) Handles tmrSendStatusEmail.Tick
        tmrSendStatusEmail.Stop()

        sendEmailReport()

        tmrSendStatusEmail.Interval = 60000 * 30 '60000=1minute. so 30 minutes
        tmrSendStatusEmail.Start()

    End Sub

    Private Sub tmrSendProofStatusEmail_Tick(sender As Object, e As EventArgs) Handles tmrSendProofStatusEmail.Tick
        tmrSendProofStatusEmail.Stop()

        Dim ThreadStart As New Threading.ThreadStart(AddressOf SendProofReportEmail)
        Dim ReadThread As New Threading.Thread(ThreadStart)
        ReadThread.Priority = Threading.ThreadPriority.Normal
        ReadThread.IsBackground = True
        ReadThread.Start()

        'SendProofReportEmail()

        tmrSendProofStatusEmail.Interval = 60000 * 120 '60000=1minute. so 2 hours
        tmrSendProofStatusEmail.Start()
    End Sub

    Private Sub SendEmailReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendEmailReportToolStripMenuItem.Click
        If MsgBox("Send an email report to " & My.Settings.eMailTo & "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            sendEmailReport(True)
        End If

    End Sub

    Private Sub sendEmailReport(Optional ByVal forceCreateLog As Boolean = False)
        If LineUpLoaded Then
            Dim email As EmailLogInfo = MyJQRowIO.CreateEmailLog(forceCreateLog)
            If Not IsNothing(email) Then

                Dim result As Utilities.EmailSendResults = Utilities.GenUtil.SendEmail(My.Settings.eSendAsUser, My.Settings.eMailTo, "", My.Settings.ePassword, "", email.Subject, email.Contents, My.Settings.eServer, My.Settings.ePort, True, False)
                If result.SentSuccess = EmailSendResults.EmailStatuses.Sent Then
                    email.TimeSent = Now
                    MyJQRowIO.emailLogs.AddLogToSql(email)
                    MyJQRowIO.emailLogs.getSentEmails()
                End If

            End If

        End If
    End Sub

    Private Sub CheckProofRequestsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckProofRequestsToolStripMenuItem.Click

        If MsgBox("Send a proof email report to " & My.Settings.eMailProofReportTo & "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            SendProofReportEmail(True)
        End If

    End Sub

    ''' <summary>
    ''' potentially sends an email for the proofs on 1and1 that are late.
    ''' </summary>
    ''' <param name="forceCreateLog"></param>
    Private Sub SendProofReportEmail(Optional ByVal forceCreateLog As Boolean = False)
        If LineUpLoaded Then
            Dim proofs As New Email_CheckProofRequestsIO(CBConfig, sqlInfo.sqlConnStr)
            Dim email As EmailLogInfo = proofs.CreateProofEmailLog(forceCreateLog)
            If IsNothing(email) Then
                Console.WriteLine("No Emails to send")

            Else

                'Clipboard.SetText(email.Contents)
                Dim result As Utilities.EmailSendResults = Utilities.GenUtil.SendEmail(My.Settings.eSendAsUser, My.Settings.eMailProofReportTo, "", My.Settings.ePassword, "", email.Subject, email.Contents, My.Settings.eServer, My.Settings.ePort, True, False)
                If result.SentSuccess = EmailSendResults.EmailStatuses.Sent Then
                    Console.WriteLine("Emails Sent OK")
                    email.TimeSent = Now
                    MyJQRowIO.emailLogs.AddLogToSql(email)
                    MyJQRowIO.emailLogs.getSentEmails()
                Else
                    Console.WriteLine("Emails failed to send")
                End If

            End If
        End If
    End Sub


    Private Sub EmailNotificationLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmailNotificationLogToolStripMenuItem.Click
        Dim viewer As New frmEmailLogViewer(sqlInfo.sqlConnStr)
        viewer.ShowDialog()
    End Sub


    Private Sub RepopulateFoldersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RepopulateFoldersToolStripMenuItem.Click
        RepopulateCovers()
    End Sub
    Private Sub RepopulateCoversToolStripMenuItem_Click(sender As Object, e As EventArgs)
        RepopulateCovers()
    End Sub

    ''' <summary>
    ''' this sub goes through the book covers folder, and tries to find the project folder for it.
    ''' </summary>
    Private Sub RepopulateCovers()
        Dim fp As String = "X:\Production\Books\Covers\2up 12x18"

        Dim openFolder As New frmBkCovManager(fp)
        openFolder.ShowDialog()

    End Sub

    Private Sub NoteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoteToolStripMenuItem.Click
        Dim n As New JQNoteInfo(MyJQNoteIO)
        n.NoteTitle = "Note Title"
        ProductionFilesFlow.Controls.Add(n.GetGroupBox)
    End Sub

    Private Sub ShowXMLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowXMLToolStripMenuItem.Click
        If File.Exists(My.Settings.MxCustomPath) Then
            Process.Start(My.Settings.MxCustomPath)
        Else
            MsgBox("File Not Found: " & vbCrLf & My.Settings.MxCustomPath)
        End If
    End Sub

    Private Sub ShowRowDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowRowDetailsToolStripMenuItem.Click
        If selectedPersonalizedIDs.Count < 11 Then
            For Each selRow As PersonalizeRowInfo In selectedPersonalizedIDs
                MsgBox(selRow.ToString())
            Next
        End If
    End Sub


    Private Sub ExportFilesToPrintersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportFilesToPrintersToolStripMenuItem.Click
        Dim rows As List(Of JQRowInfo) = MyJQRowIO.GetSelectedJobs

        Dim sb As New Text.StringBuilder
        Dim queues As New List(Of PrinterQueueInfo)

        Dim copyDict As New Dictionary(Of String, List(Of String)) 'original file path, new file path
        For Each row As JQRowInfo In rows
            row.ProductionFiles = MyJQProductionIO.GetProductionFiles(row.ItemNumber)
            For Each file As JQProductionFileInfo In row.ProductionFiles
                Dim locs As List(Of String) = file.GetExportLocations(row.ItemNumber, row.OrderQuantity, MyPrinterMgmt)
                If locs.Count > 0 Then
                    sb.AppendLine()
                    copyDict.Add(file.ProductionFile.OriginalFile.FullName, locs)
                    sb.AppendLine($"Exporting: { row.ItemNumber} - { row.Title } - { file.ProductBodyOrCover }")
                    sb.AppendLine(String.Join(vbCrLf, locs))
                End If
            Next

        Next
        If copyDict.Keys.Count = 0 Then
            MsgBox("No files found to export")
        Else
            If MsgBox(sb.ToString, MsgBoxStyle.YesNo, "Copy Files?") = MsgBoxResult.Yes Then
                ProgressBar1.Maximum = copyDict.Keys.Count - 1
                ProgressBar1.Step = 1
                ProgressBar1.Value = 0
                For Each sourceKey As String In copyDict.Keys
                    'Threading.Thread.Sleep(1500)
                    For Each loc As String In copyDict(sourceKey)
                        Try
                            Dim dir As String = Path.GetDirectoryName(loc)
                            'MsgBox(dir)
                            If Not Directory.Exists(dir) Then
                                Directory.CreateDirectory(dir)
                            End If
                            File.Copy(sourceKey, loc, True)
                        Catch ex As Exception
                            MsgBox(ex.Message & vbCrLf & vbCrLf & "Source: " & sourceKey & vbCrLf & "Destination: " & loc, MsgBoxStyle.Critical, "Error Copying File")
                        End Try

                    Next

                    ProgressBar1.PerformStep()
                    Application.DoEvents()
                Next
                MyJQRowIO.UpdateStatusOfSelectedJobs($"All Done!{vbCrLf & vbCrLf }Do you want to update the job status's?", "Waiting to Print, Files sent to printer.")
                'If Directory.Exists(desktop) Then
                '    Process.Start("Explorer.exe", "/select," & desktop)
                'End If

            End If
            If MsgBox("Create Job Tickets?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                CreateJobTicketLabelForSelectedQPJobs()
            End If
        End If

    End Sub

    Private Sub UpdateStatusOfJobsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateStatusOfJobsToolStripMenuItem.Click
        MyJQRowIO.UpdateStatusOfSelectedJobs()
    End Sub

    Private Sub PrintJobTicketsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintJobTicketsToolStripMenuItem.Click
        CreateJobTicketLabelForSelectedQPJobs()
    End Sub

    ''' <summary>
    ''' Creates / displays job tickets
    ''' </summary>
    Private Sub CreateJobTicketLabelForSelectedQPJobs()


        Dim resultfp As String = ""

        Dim ticketIO As New PdfJobTicketIO()
        Dim selectedJobs As New List(Of JQRowInfo)

        For Each job As JQRowInfo In MyJQRowIO.GetSelectedJobs()
            job.ProductionFiles = MyJQProductionIO.GetProductionFiles(job.ItemNumber)
            selectedJobs.Add(job)
        Next

        If ticketIO.CreateJQTicket(selectedJobs, MyJQProductionIO, resultfp) Then
            MyJQRowIO.updateSql()

            Dim web As New frmWebr("file://" & resultfp, True)

            web.Show()

        End If

    End Sub

    Private Sub WebBrowserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WebBrowserToolStripMenuItem.Click
        Dim web As New frmWebr("https://google.com") '"file://" & Path.Combine(My.Settings.dirResources, "CreatingTickets.html"))


        'web.PrintWhenLoaded = True
        web.Show()



    End Sub

    Private Sub PrintTCFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintTCFilesToolStripMenuItem.Click
        Dim tc As New frmPrintTC(MyPrinterMgmt)
        tc.Show()

    End Sub
End Class
