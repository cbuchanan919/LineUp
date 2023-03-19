<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LineUp
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LineUp))
        Me.JobQMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuMoveToArchive = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateStatusOfJobsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteRowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateBackupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.SearchForProjectFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowJobDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyProductionFilesToDesktopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportFilesToPrintersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintJobTicketsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintShareWordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PersonalizeJobQContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RefreshPageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendToArchiveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestoreIDsFromArchiveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintLabelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImposedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Printing1200aToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Printing1200bToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DrilledToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilledToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShippedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImposeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyFilesToDesktopToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportGPPrtABCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportGPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportGPPrtABCToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportTo1200aToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportTo1200bToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportTo6120aToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyFileNamesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyFilesToDesktopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowXMLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowRowDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSearchProds = New System.Windows.Forms.Button()
        Me.rdbBodyPDF = New System.Windows.Forms.RadioButton()
        Me.rdbInvoice = New System.Windows.Forms.RadioButton()
        Me.btnImpose = New System.Windows.Forms.Button()
        Me.txtSalePrice = New System.Windows.Forms.TextBox()
        Me.txtProductionCost = New System.Windows.Forms.TextBox()
        Me.txtType = New System.Windows.Forms.TextBox()
        Me.txtPageCt = New System.Windows.Forms.TextBox()
        Me.txtQuantityPrint = New System.Windows.Forms.TextBox()
        Me.txtShipAddress = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtDesignID = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtItemNumber = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtPhoneNumber = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtReceiptNum = New System.Windows.Forms.TextBox()
        Me.txtBillingNameNumber = New System.Windows.Forms.TextBox()
        Me.txtMxNumber = New System.Windows.Forms.TextBox()
        Me.btnAlignSheets = New System.Windows.Forms.Button()
        Me.btnLoadProd = New System.Windows.Forms.Button()
        Me.btnCustSearch = New System.Windows.Forms.Button()
        Me.txtJobQSearch = New System.Windows.Forms.TextBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Status = New System.Windows.Forms.StatusStrip()
        Me.Status1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripBTP = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.TimerCheckForUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.TmrUpdatePQ = New System.Windows.Forms.Timer(Me.components)
        Me.tmrResetStatus = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WebBrowserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateStatusLabelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateBarcodeImagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TractPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateJobQBackupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImposeBookPamEtcToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateCustomProofGPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HomePrintTractsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.GetGPEmailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HarvestShareWordFilesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindFoldersWithMultipleFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatchCreateProductionFilesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.RepopulateFoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintTCFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PurchasingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComputerSoftwareKeysToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LineUpErrorLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmailNotificationLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ForCbOnlyTSItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowPrinterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InvToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OutputProductsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FinishedYearCountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendEmailReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckProofRequestsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestFindFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CountsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShareWordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetDefaultPrinterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NoteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrPersonalizeDisplay = New System.Windows.Forms.Timer(Me.components)
        Me.SplitContainer8 = New System.Windows.Forms.SplitContainer()
        Me.LineUpTabCtrl = New System.Windows.Forms.TabControl()
        Me.TabProd = New System.Windows.Forms.TabPage()
        Me.SplitContainer10 = New System.Windows.Forms.SplitContainer()
        Me.FlowPrinterInfo = New System.Windows.Forms.FlowLayoutPanel()
        Me.SplitContainer9 = New System.Windows.Forms.SplitContainer()
        Me.dgvJobQ = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnAccounting = New System.Windows.Forms.Button()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rdbJobQCurrent = New System.Windows.Forms.RadioButton()
        Me.rdbJobQHistory = New System.Windows.Forms.RadioButton()
        Me.tabPrint = New System.Windows.Forms.TabPage()
        Me.ProductionFilesFlow = New System.Windows.Forms.FlowLayoutPanel()
        Me.TabPersonalize = New System.Windows.Forms.TabPage()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer7 = New System.Windows.Forms.SplitContainer()
        Me.dgvPersonalizeJobQ = New System.Windows.Forms.DataGridView()
        Me.rdbAllOrders = New System.Windows.Forms.RadioButton()
        Me.lblDesignsShowing = New System.Windows.Forms.Label()
        Me.rdbArchive = New System.Windows.Forms.RadioButton()
        Me.rdbCurrent = New System.Windows.Forms.RadioButton()
        Me.txtPersonalizeSearch = New System.Windows.Forms.TextBox()
        Me.btnDesignsRefresh = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnPrintLabels = New System.Windows.Forms.Button()
        Me.btnArchive = New System.Windows.Forms.Button()
        Me.pdfInvoice = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.rdbCovPDF = New System.Windows.Forms.RadioButton()
        Me.rdbLabels = New System.Windows.Forms.RadioButton()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.txtShipNote = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbStatusHistory = New System.Windows.Forms.ComboBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabControlInfo = New System.Windows.Forms.TabControl()
        Me.TabProdInfo = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.txtAuthor = New System.Windows.Forms.TextBox()
        Me.txtInvTitle = New System.Windows.Forms.TextBox()
        Me.txtlTitle = New System.Windows.Forms.TextBox()
        Me.lblQuantitylbl = New System.Windows.Forms.Label()
        Me.lblTitlelbl = New System.Windows.Forms.Label()
        Me.lblInvTitlelbl = New System.Windows.Forms.Label()
        Me.lblAuthorlbl = New System.Windows.Forms.Label()
        Me.txtProdNumPrint = New System.Windows.Forms.TextBox()
        Me.lblItemNumlbl = New System.Windows.Forms.Label()
        Me.lblSourcelbl = New System.Windows.Forms.Label()
        Me.lblPageCtlbl = New System.Windows.Forms.Label()
        Me.lblTypelbl = New System.Windows.Forms.Label()
        Me.lblSalePricelbl = New System.Windows.Forms.Label()
        Me.lblProductionCostlbl = New System.Windows.Forms.Label()
        Me.pbProducts = New System.Windows.Forms.PictureBox()
        Me.txtCatalog = New System.Windows.Forms.RichTextBox()
        Me.TabOrderInfo = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblCustEmail = New System.Windows.Forms.LinkLabel()
        Me.tmrSearchQ = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSendStatusEmail = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSendProofStatusEmail = New System.Windows.Forms.Timer(Me.components)
        Me.DataErrorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearchPersonalize = New System.Windows.Forms.Timer(Me.components)
        Me.JobQMenuStrip.SuspendLayout()
        Me.PersonalizeJobQContextMenu.SuspendLayout()
        Me.Status.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer8.Panel1.SuspendLayout()
        Me.SplitContainer8.Panel2.SuspendLayout()
        Me.SplitContainer8.SuspendLayout()
        Me.LineUpTabCtrl.SuspendLayout()
        Me.TabProd.SuspendLayout()
        CType(Me.SplitContainer10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer10.Panel1.SuspendLayout()
        Me.SplitContainer10.Panel2.SuspendLayout()
        Me.SplitContainer10.SuspendLayout()
        CType(Me.SplitContainer9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer9.Panel1.SuspendLayout()
        Me.SplitContainer9.Panel2.SuspendLayout()
        Me.SplitContainer9.SuspendLayout()
        CType(Me.dgvJobQ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.tabPrint.SuspendLayout()
        Me.TabPersonalize.SuspendLayout()
        CType(Me.SplitContainer6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.SplitContainer7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer7.Panel1.SuspendLayout()
        Me.SplitContainer7.Panel2.SuspendLayout()
        Me.SplitContainer7.SuspendLayout()
        CType(Me.dgvPersonalizeJobQ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.pdfInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel3.SuspendLayout()
        Me.TabControlInfo.SuspendLayout()
        Me.TabProdInfo.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.pbProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabOrderInfo.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'JobQMenuStrip
        '
        Me.JobQMenuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.JobQMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuMoveToArchive, Me.UpdateStatusOfJobsToolStripMenuItem, Me.DeleteRowsToolStripMenuItem, Me.ToolStripSeparator4, Me.CreateBackupToolStripMenuItem, Me.ToolStripSeparator6, Me.SearchForProjectFolderToolStripMenuItem, Me.ShowJobDetailsToolStripMenuItem, Me.ProductionToolStripMenuItem, Me.PrintJobTicketsToolStripMenuItem, Me.ToolStripSeparator8, Me.PrintShareWordToolStripMenuItem})
        Me.JobQMenuStrip.Name = "ContextMenuStrip1"
        Me.JobQMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.JobQMenuStrip.Size = New System.Drawing.Size(190, 220)
        Me.JobQMenuStrip.Text = "hi"
        '
        'menuMoveToArchive
        '
        Me.menuMoveToArchive.Name = "menuMoveToArchive"
        Me.menuMoveToArchive.Size = New System.Drawing.Size(189, 22)
        Me.menuMoveToArchive.Text = "Move to Archive"
        '
        'UpdateStatusOfJobsToolStripMenuItem
        '
        Me.UpdateStatusOfJobsToolStripMenuItem.Name = "UpdateStatusOfJobsToolStripMenuItem"
        Me.UpdateStatusOfJobsToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.UpdateStatusOfJobsToolStripMenuItem.Text = "Update Status of Jobs"
        '
        'DeleteRowsToolStripMenuItem
        '
        Me.DeleteRowsToolStripMenuItem.Name = "DeleteRowsToolStripMenuItem"
        Me.DeleteRowsToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.DeleteRowsToolStripMenuItem.Text = "Delete Row(s)"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(186, 6)
        '
        'CreateBackupToolStripMenuItem
        '
        Me.CreateBackupToolStripMenuItem.Name = "CreateBackupToolStripMenuItem"
        Me.CreateBackupToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.CreateBackupToolStripMenuItem.Text = "Create Lineup Backup"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(186, 6)
        '
        'SearchForProjectFolderToolStripMenuItem
        '
        Me.SearchForProjectFolderToolStripMenuItem.Name = "SearchForProjectFolderToolStripMenuItem"
        Me.SearchForProjectFolderToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SearchForProjectFolderToolStripMenuItem.Text = "Project Folder"
        '
        'ShowJobDetailsToolStripMenuItem
        '
        Me.ShowJobDetailsToolStripMenuItem.Name = "ShowJobDetailsToolStripMenuItem"
        Me.ShowJobDetailsToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ShowJobDetailsToolStripMenuItem.Text = "Show Job Details"
        '
        'ProductionToolStripMenuItem
        '
        Me.ProductionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyProductionFilesToDesktopToolStripMenuItem, Me.ExportFilesToPrintersToolStripMenuItem})
        Me.ProductionToolStripMenuItem.Name = "ProductionToolStripMenuItem"
        Me.ProductionToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ProductionToolStripMenuItem.Text = "Production"
        '
        'CopyProductionFilesToDesktopToolStripMenuItem
        '
        Me.CopyProductionFilesToDesktopToolStripMenuItem.Name = "CopyProductionFilesToDesktopToolStripMenuItem"
        Me.CopyProductionFilesToDesktopToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
        Me.CopyProductionFilesToDesktopToolStripMenuItem.Text = "Copy Production Files To Desktop"
        '
        'ExportFilesToPrintersToolStripMenuItem
        '
        Me.ExportFilesToPrintersToolStripMenuItem.Name = "ExportFilesToPrintersToolStripMenuItem"
        Me.ExportFilesToPrintersToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
        Me.ExportFilesToPrintersToolStripMenuItem.Text = "Export Files To Printers"
        '
        'PrintJobTicketsToolStripMenuItem
        '
        Me.PrintJobTicketsToolStripMenuItem.Name = "PrintJobTicketsToolStripMenuItem"
        Me.PrintJobTicketsToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.PrintJobTicketsToolStripMenuItem.Text = "Print Job Tickets"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(186, 6)
        '
        'PrintShareWordToolStripMenuItem
        '
        Me.PrintShareWordToolStripMenuItem.Name = "PrintShareWordToolStripMenuItem"
        Me.PrintShareWordToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.PrintShareWordToolStripMenuItem.Text = "Print ShareWord"
        '
        'PersonalizeJobQContextMenu
        '
        Me.PersonalizeJobQContextMenu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.PersonalizeJobQContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshPageToolStripMenuItem, Me.SendToArchiveToolStripMenuItem, Me.RestoreIDsFromArchiveToolStripMenuItem, Me.PrintLabelsToolStripMenuItem, Me.SetStatusToolStripMenuItem, Me.ToolStripSeparator9, Me.ImposeToolStripMenuItem, Me.CopyFilesToDesktopToolStripMenuItem1, Me.ExportGPPrtABCToolStripMenuItem, Me.ExportGPToolStripMenuItem, Me.ToolStripSeparator11, Me.ShowXMLToolStripMenuItem, Me.ShowRowDetailsToolStripMenuItem})
        Me.PersonalizeJobQContextMenu.Name = "PersonalizeJobQContextMenu"
        Me.PersonalizeJobQContextMenu.Size = New System.Drawing.Size(262, 258)
        '
        'RefreshPageToolStripMenuItem
        '
        Me.RefreshPageToolStripMenuItem.Name = "RefreshPageToolStripMenuItem"
        Me.RefreshPageToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.RefreshPageToolStripMenuItem.Text = "Refresh Orders"
        '
        'SendToArchiveToolStripMenuItem
        '
        Me.SendToArchiveToolStripMenuItem.Name = "SendToArchiveToolStripMenuItem"
        Me.SendToArchiveToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.SendToArchiveToolStripMenuItem.Text = "Send To Archive"
        '
        'RestoreIDsFromArchiveToolStripMenuItem
        '
        Me.RestoreIDsFromArchiveToolStripMenuItem.Name = "RestoreIDsFromArchiveToolStripMenuItem"
        Me.RestoreIDsFromArchiveToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.RestoreIDsFromArchiveToolStripMenuItem.Text = "Restore ID's From Archive"
        '
        'PrintLabelsToolStripMenuItem
        '
        Me.PrintLabelsToolStripMenuItem.Name = "PrintLabelsToolStripMenuItem"
        Me.PrintLabelsToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.PrintLabelsToolStripMenuItem.Text = "Print Labels"
        '
        'SetStatusToolStripMenuItem
        '
        Me.SetStatusToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OrderToolStripMenuItem, Me.ImposedToolStripMenuItem, Me.Printing1200aToolStripMenuItem, Me.Printing1200bToolStripMenuItem, Me.DrilledToolStripMenuItem, Me.FilledToolStripMenuItem, Me.ShippedToolStripMenuItem})
        Me.SetStatusToolStripMenuItem.Name = "SetStatusToolStripMenuItem"
        Me.SetStatusToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.SetStatusToolStripMenuItem.Text = "Set Status"
        '
        'OrderToolStripMenuItem
        '
        Me.OrderToolStripMenuItem.Name = "OrderToolStripMenuItem"
        Me.OrderToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.OrderToolStripMenuItem.Text = "Order"
        '
        'ImposedToolStripMenuItem
        '
        Me.ImposedToolStripMenuItem.Name = "ImposedToolStripMenuItem"
        Me.ImposedToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.ImposedToolStripMenuItem.Text = "Imposed"
        '
        'Printing1200aToolStripMenuItem
        '
        Me.Printing1200aToolStripMenuItem.Name = "Printing1200aToolStripMenuItem"
        Me.Printing1200aToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.Printing1200aToolStripMenuItem.Text = "Printing (1200a)"
        '
        'Printing1200bToolStripMenuItem
        '
        Me.Printing1200bToolStripMenuItem.Name = "Printing1200bToolStripMenuItem"
        Me.Printing1200bToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.Printing1200bToolStripMenuItem.Text = "Printing (1200b)"
        '
        'DrilledToolStripMenuItem
        '
        Me.DrilledToolStripMenuItem.Name = "DrilledToolStripMenuItem"
        Me.DrilledToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.DrilledToolStripMenuItem.Text = "Drilled"
        '
        'FilledToolStripMenuItem
        '
        Me.FilledToolStripMenuItem.Name = "FilledToolStripMenuItem"
        Me.FilledToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.FilledToolStripMenuItem.Text = "Filled"
        '
        'ShippedToolStripMenuItem
        '
        Me.ShippedToolStripMenuItem.Name = "ShippedToolStripMenuItem"
        Me.ShippedToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.ShippedToolStripMenuItem.Text = "Shipped"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(258, 6)
        '
        'ImposeToolStripMenuItem
        '
        Me.ImposeToolStripMenuItem.Name = "ImposeToolStripMenuItem"
        Me.ImposeToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.ImposeToolStripMenuItem.Text = "Impose"
        '
        'CopyFilesToDesktopToolStripMenuItem1
        '
        Me.CopyFilesToDesktopToolStripMenuItem1.Name = "CopyFilesToDesktopToolStripMenuItem1"
        Me.CopyFilesToDesktopToolStripMenuItem1.Size = New System.Drawing.Size(261, 22)
        Me.CopyFilesToDesktopToolStripMenuItem1.Text = "Copy Files to Desktop"
        '
        'ExportGPPrtABCToolStripMenuItem
        '
        Me.ExportGPPrtABCToolStripMenuItem.Name = "ExportGPPrtABCToolStripMenuItem"
        Me.ExportGPPrtABCToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.ExportGPPrtABCToolStripMenuItem.Text = "Export GP (1250p && 1200b && 6120a)"
        '
        'ExportGPToolStripMenuItem
        '
        Me.ExportGPToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportGPPrtABCToolStripMenuItem2, Me.ExportTo1200aToolStripMenuItem, Me.ExportTo1200bToolStripMenuItem, Me.ExportTo6120aToolStripMenuItem, Me.CopyFileNamesToolStripMenuItem1, Me.CopyFilesToDesktopToolStripMenuItem})
        Me.ExportGPToolStripMenuItem.Name = "ExportGPToolStripMenuItem"
        Me.ExportGPToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.ExportGPToolStripMenuItem.Text = "Export GP Options"
        '
        'ExportGPPrtABCToolStripMenuItem2
        '
        Me.ExportGPPrtABCToolStripMenuItem2.Name = "ExportGPPrtABCToolStripMenuItem2"
        Me.ExportGPPrtABCToolStripMenuItem2.Size = New System.Drawing.Size(249, 22)
        Me.ExportGPPrtABCToolStripMenuItem2.Text = "Export to 1250p && 1200b && 6120a"
        '
        'ExportTo1200aToolStripMenuItem
        '
        Me.ExportTo1200aToolStripMenuItem.Name = "ExportTo1200aToolStripMenuItem"
        Me.ExportTo1200aToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.ExportTo1200aToolStripMenuItem.Text = "Export to 1250p"
        '
        'ExportTo1200bToolStripMenuItem
        '
        Me.ExportTo1200bToolStripMenuItem.Name = "ExportTo1200bToolStripMenuItem"
        Me.ExportTo1200bToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.ExportTo1200bToolStripMenuItem.Text = "Export to 1200b"
        '
        'ExportTo6120aToolStripMenuItem
        '
        Me.ExportTo6120aToolStripMenuItem.Name = "ExportTo6120aToolStripMenuItem"
        Me.ExportTo6120aToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.ExportTo6120aToolStripMenuItem.Text = "Export to 6120a"
        '
        'CopyFileNamesToolStripMenuItem1
        '
        Me.CopyFileNamesToolStripMenuItem1.Name = "CopyFileNamesToolStripMenuItem1"
        Me.CopyFileNamesToolStripMenuItem1.Size = New System.Drawing.Size(249, 22)
        Me.CopyFileNamesToolStripMenuItem1.Text = "Copy File Names"
        '
        'CopyFilesToDesktopToolStripMenuItem
        '
        Me.CopyFilesToDesktopToolStripMenuItem.Name = "CopyFilesToDesktopToolStripMenuItem"
        Me.CopyFilesToDesktopToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.CopyFilesToDesktopToolStripMenuItem.Text = "Copy Files to Desktop"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(258, 6)
        '
        'ShowXMLToolStripMenuItem
        '
        Me.ShowXMLToolStripMenuItem.Name = "ShowXMLToolStripMenuItem"
        Me.ShowXMLToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.ShowXMLToolStripMenuItem.Text = "Display UV Custom MX File (XML)"
        '
        'ShowRowDetailsToolStripMenuItem
        '
        Me.ShowRowDetailsToolStripMenuItem.Name = "ShowRowDetailsToolStripMenuItem"
        Me.ShowRowDetailsToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.ShowRowDetailsToolStripMenuItem.Text = "Show Selected Row Details"
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 100
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 100
        Me.ToolTip1.ReshowDelay = 20
        '
        'btnSearchProds
        '
        Me.btnSearchProds.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnSearchProds.BackColor = System.Drawing.Color.White
        Me.btnSearchProds.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSearchProds.Location = New System.Drawing.Point(1077, 4)
        Me.btnSearchProds.Margin = New System.Windows.Forms.Padding(40, 3, 3, 3)
        Me.btnSearchProds.Name = "btnSearchProds"
        Me.btnSearchProds.Size = New System.Drawing.Size(222, 26)
        Me.btnSearchProds.TabIndex = 40
        Me.btnSearchProds.Text = "Browse Products"
        Me.ToolTip1.SetToolTip(Me.btnSearchProds, "Search products by title, author, product number, etc...")
        Me.btnSearchProds.UseVisualStyleBackColor = False
        '
        'rdbBodyPDF
        '
        Me.rdbBodyPDF.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rdbBodyPDF.AutoSize = True
        Me.rdbBodyPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbBodyPDF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Underline)
        Me.rdbBodyPDF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbBodyPDF.Location = New System.Drawing.Point(104, 7)
        Me.rdbBodyPDF.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbBodyPDF.Name = "rdbBodyPDF"
        Me.rdbBodyPDF.Size = New System.Drawing.Size(80, 21)
        Me.rdbBodyPDF.TabIndex = 62
        Me.rdbBodyPDF.Text = "Body PDF"
        Me.ToolTip1.SetToolTip(Me.rdbBodyPDF, "Ctrl + Click to open file in explorer")
        Me.rdbBodyPDF.UseVisualStyleBackColor = True
        '
        'rdbInvoice
        '
        Me.rdbInvoice.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rdbInvoice.AutoSize = True
        Me.rdbInvoice.Checked = True
        Me.rdbInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbInvoice.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Underline)
        Me.rdbInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbInvoice.Location = New System.Drawing.Point(37, 7)
        Me.rdbInvoice.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbInvoice.Name = "rdbInvoice"
        Me.rdbInvoice.Size = New System.Drawing.Size(65, 21)
        Me.rdbInvoice.TabIndex = 61
        Me.rdbInvoice.TabStop = True
        Me.rdbInvoice.Text = "Invoice"
        Me.ToolTip1.SetToolTip(Me.rdbInvoice, "Ctrl + Click to open file in explorer")
        Me.rdbInvoice.UseVisualStyleBackColor = True
        '
        'btnImpose
        '
        Me.btnImpose.AutoSize = True
        Me.btnImpose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnImpose.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnImpose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnImpose.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnImpose.FlatAppearance.BorderSize = 0
        Me.btnImpose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnImpose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImpose.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnImpose.ForeColor = System.Drawing.Color.White
        Me.btnImpose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImpose.Location = New System.Drawing.Point(182, 243)
        Me.btnImpose.Margin = New System.Windows.Forms.Padding(2)
        Me.btnImpose.MinimumSize = New System.Drawing.Size(0, 30)
        Me.btnImpose.Name = "btnImpose"
        Me.btnImpose.Size = New System.Drawing.Size(86, 46)
        Me.btnImpose.TabIndex = 63
        Me.btnImpose.Text = "Impose"
        Me.ToolTip1.SetToolTip(Me.btnImpose, "Hold down the ctrl button skip xml preview")
        Me.btnImpose.UseVisualStyleBackColor = False
        '
        'txtSalePrice
        '
        Me.txtSalePrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSalePrice.BackColor = System.Drawing.Color.White
        Me.txtSalePrice.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSalePrice.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtSalePrice.Location = New System.Drawing.Point(978, 5)
        Me.txtSalePrice.Name = "txtSalePrice"
        Me.txtSalePrice.ReadOnly = True
        Me.txtSalePrice.Size = New System.Drawing.Size(129, 17)
        Me.txtSalePrice.TabIndex = 60
        Me.ToolTip1.SetToolTip(Me.txtSalePrice, "Enter quantity desired here. (Creates job standards for you)")
        '
        'txtProductionCost
        '
        Me.txtProductionCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProductionCost.BackColor = System.Drawing.Color.White
        Me.txtProductionCost.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtProductionCost.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtProductionCost.Location = New System.Drawing.Point(725, 5)
        Me.txtProductionCost.Name = "txtProductionCost"
        Me.txtProductionCost.ReadOnly = True
        Me.txtProductionCost.Size = New System.Drawing.Size(129, 17)
        Me.txtProductionCost.TabIndex = 59
        Me.ToolTip1.SetToolTip(Me.txtProductionCost, "Enter quantity desired here. (Creates job standards for you)")
        Me.txtProductionCost.Visible = False
        '
        'txtType
        '
        Me.txtType.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtType.BackColor = System.Drawing.Color.White
        Me.txtType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtType.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtType.Location = New System.Drawing.Point(473, 89)
        Me.txtType.Name = "txtType"
        Me.txtType.ReadOnly = True
        Me.txtType.Size = New System.Drawing.Size(129, 17)
        Me.txtType.TabIndex = 58
        Me.ToolTip1.SetToolTip(Me.txtType, "Enter quantity desired here. (Creates job standards for you)")
        '
        'txtPageCt
        '
        Me.txtPageCt.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPageCt.BackColor = System.Drawing.Color.White
        Me.txtPageCt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPageCt.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtPageCt.Location = New System.Drawing.Point(473, 61)
        Me.txtPageCt.Name = "txtPageCt"
        Me.txtPageCt.ReadOnly = True
        Me.txtPageCt.Size = New System.Drawing.Size(129, 17)
        Me.txtPageCt.TabIndex = 57
        Me.ToolTip1.SetToolTip(Me.txtPageCt, "Enter quantity desired here. (Creates job standards for you)")
        '
        'txtQuantityPrint
        '
        Me.txtQuantityPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQuantityPrint.BackColor = System.Drawing.Color.White
        Me.txtQuantityPrint.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtQuantityPrint.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtQuantityPrint.Location = New System.Drawing.Point(473, 33)
        Me.txtQuantityPrint.Name = "txtQuantityPrint"
        Me.txtQuantityPrint.Size = New System.Drawing.Size(129, 17)
        Me.txtQuantityPrint.TabIndex = 51
        Me.ToolTip1.SetToolTip(Me.txtQuantityPrint, "Enter quantity desired here. (Creates job standards for you)")
        '
        'txtShipAddress
        '
        Me.txtShipAddress.BackColor = System.Drawing.Color.White
        Me.txtShipAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtShipAddress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtShipAddress.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtShipAddress.Location = New System.Drawing.Point(934, 3)
        Me.txtShipAddress.Multiline = True
        Me.txtShipAddress.Name = "txtShipAddress"
        Me.txtShipAddress.ReadOnly = True
        Me.TableLayoutPanel7.SetRowSpan(Me.txtShipAddress, 4)
        Me.txtShipAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtShipAddress.Size = New System.Drawing.Size(371, 90)
        Me.txtShipAddress.TabIndex = 57
        Me.ToolTip1.SetToolTip(Me.txtShipAddress, "Shipping Address")
        '
        'Label27
        '
        Me.Label27.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label27.AutoSize = True
        Me.Label27.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label27.Location = New System.Drawing.Point(526, 50)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(51, 19)
        Me.Label27.TabIndex = 37
        Me.Label27.Text = "Phone:"
        Me.ToolTip1.SetToolTip(Me.Label27, "Customer's Phone #")
        '
        'txtDesignID
        '
        Me.txtDesignID.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtDesignID.BackColor = System.Drawing.Color.White
        Me.txtDesignID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDesignID.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtDesignID.Location = New System.Drawing.Point(583, 27)
        Me.txtDesignID.Name = "txtDesignID"
        Me.txtDesignID.ReadOnly = True
        Me.txtDesignID.Size = New System.Drawing.Size(138, 17)
        Me.txtDesignID.TabIndex = 51
        Me.ToolTip1.SetToolTip(Me.txtDesignID, "Design ID #")
        '
        'Label18
        '
        Me.Label18.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label18.AutoSize = True
        Me.Label18.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label18.Location = New System.Drawing.Point(505, 26)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(72, 19)
        Me.Label18.TabIndex = 52
        Me.Label18.Text = "Design ID:"
        Me.ToolTip1.SetToolTip(Me.Label18, "Design ID #")
        '
        'txtItemNumber
        '
        Me.txtItemNumber.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtItemNumber.BackColor = System.Drawing.Color.White
        Me.txtItemNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtItemNumber.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtItemNumber.Location = New System.Drawing.Point(584, 4)
        Me.txtItemNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.txtItemNumber.MaxLength = 150
        Me.txtItemNumber.Name = "txtItemNumber"
        Me.txtItemNumber.ReadOnly = True
        Me.txtItemNumber.Size = New System.Drawing.Size(136, 17)
        Me.txtItemNumber.TabIndex = 50
        Me.ToolTip1.SetToolTip(Me.txtItemNumber, "Order Number")
        '
        'Label22
        '
        Me.Label22.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label22.AutoSize = True
        Me.Label22.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label22.Location = New System.Drawing.Point(525, 2)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(52, 19)
        Me.Label22.TabIndex = 33
        Me.Label22.Text = "Item #:"
        Me.ToolTip1.SetToolTip(Me.Label22, "Order Number")
        '
        'txtPhoneNumber
        '
        Me.txtPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtPhoneNumber.BackColor = System.Drawing.Color.White
        Me.txtPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPhoneNumber.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtPhoneNumber.Location = New System.Drawing.Point(583, 51)
        Me.txtPhoneNumber.Name = "txtPhoneNumber"
        Me.txtPhoneNumber.ReadOnly = True
        Me.txtPhoneNumber.Size = New System.Drawing.Size(138, 17)
        Me.txtPhoneNumber.TabIndex = 53
        Me.ToolTip1.SetToolTip(Me.txtPhoneNumber, "Customer's Phone #")
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(837, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 19)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "Ship Address:"
        Me.ToolTip1.SetToolTip(Me.Label3, "Shipping Address")
        '
        'txtEmail
        '
        Me.txtEmail.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtEmail.BackColor = System.Drawing.Color.White
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtEmail.Location = New System.Drawing.Point(128, 75)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReadOnly = True
        Me.txtEmail.Size = New System.Drawing.Size(225, 17)
        Me.txtEmail.TabIndex = 54
        Me.ToolTip1.SetToolTip(Me.txtEmail, "Customer Email")
        '
        'txtReceiptNum
        '
        Me.txtReceiptNum.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtReceiptNum.BackColor = System.Drawing.Color.White
        Me.txtReceiptNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtReceiptNum.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtReceiptNum.Location = New System.Drawing.Point(128, 51)
        Me.txtReceiptNum.Name = "txtReceiptNum"
        Me.txtReceiptNum.ReadOnly = True
        Me.txtReceiptNum.Size = New System.Drawing.Size(225, 17)
        Me.txtReceiptNum.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.txtReceiptNum, "Shipping Name")
        '
        'txtBillingNameNumber
        '
        Me.txtBillingNameNumber.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtBillingNameNumber.BackColor = System.Drawing.Color.White
        Me.txtBillingNameNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBillingNameNumber.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtBillingNameNumber.Location = New System.Drawing.Point(128, 27)
        Me.txtBillingNameNumber.Name = "txtBillingNameNumber"
        Me.txtBillingNameNumber.ReadOnly = True
        Me.txtBillingNameNumber.Size = New System.Drawing.Size(225, 17)
        Me.txtBillingNameNumber.TabIndex = 55
        Me.ToolTip1.SetToolTip(Me.txtBillingNameNumber, "Billing Name")
        '
        'txtMxNumber
        '
        Me.txtMxNumber.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMxNumber.BackColor = System.Drawing.Color.White
        Me.txtMxNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMxNumber.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtMxNumber.Location = New System.Drawing.Point(128, 3)
        Me.txtMxNumber.Name = "txtMxNumber"
        Me.txtMxNumber.ReadOnly = True
        Me.txtMxNumber.Size = New System.Drawing.Size(225, 17)
        Me.txtMxNumber.TabIndex = 58
        Me.ToolTip1.SetToolTip(Me.txtMxNumber, "Billing Name")
        '
        'btnAlignSheets
        '
        Me.btnAlignSheets.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAlignSheets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAlignSheets.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnAlignSheets.FlatAppearance.BorderSize = 0
        Me.btnAlignSheets.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnAlignSheets.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAlignSheets.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnAlignSheets.ForeColor = System.Drawing.Color.White
        Me.btnAlignSheets.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAlignSheets.Location = New System.Drawing.Point(1113, 59)
        Me.btnAlignSheets.Name = "btnAlignSheets"
        Me.TableLayoutPanel1.SetRowSpan(Me.btnAlignSheets, 2)
        Me.btnAlignSheets.Size = New System.Drawing.Size(86, 50)
        Me.btnAlignSheets.TabIndex = 36
        Me.btnAlignSheets.Text = "Load Alignment Sheets"
        Me.ToolTip1.SetToolTip(Me.btnAlignSheets, "Loads standard Alignment Sheets")
        Me.btnAlignSheets.UseVisualStyleBackColor = False
        Me.btnAlignSheets.Visible = False
        '
        'btnLoadProd
        '
        Me.btnLoadProd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadProd.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnLoadProd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLoadProd.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnLoadProd.FlatAppearance.BorderSize = 0
        Me.btnLoadProd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black
        Me.btnLoadProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadProd.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnLoadProd.ForeColor = System.Drawing.Color.White
        Me.btnLoadProd.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnLoadProd.Location = New System.Drawing.Point(1114, 4)
        Me.btnLoadProd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLoadProd.Name = "btnLoadProd"
        Me.TableLayoutPanel1.SetRowSpan(Me.btnLoadProd, 2)
        Me.btnLoadProd.Size = New System.Drawing.Size(84, 48)
        Me.btnLoadProd.TabIndex = 44
        Me.btnLoadProd.Text = "Load"
        Me.ToolTip1.SetToolTip(Me.btnLoadProd, "Loads Production Files" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Control click to open your own files")
        Me.btnLoadProd.UseVisualStyleBackColor = False
        Me.btnLoadProd.Visible = False
        '
        'btnCustSearch
        '
        Me.btnCustSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCustSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCustSearch.Location = New System.Drawing.Point(480, 2)
        Me.btnCustSearch.Name = "btnCustSearch"
        Me.btnCustSearch.Size = New System.Drawing.Size(105, 24)
        Me.btnCustSearch.TabIndex = 62
        Me.btnCustSearch.Text = "Cust. Search"
        Me.ToolTip1.SetToolTip(Me.btnCustSearch, "Search through customer records")
        Me.btnCustSearch.UseVisualStyleBackColor = True
        '
        'txtJobQSearch
        '
        Me.txtJobQSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJobQSearch.BackColor = System.Drawing.Color.SkyBlue
        Me.txtJobQSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        Me.txtJobQSearch.Location = New System.Drawing.Point(3, 3)
        Me.txtJobQSearch.Name = "txtJobQSearch"
        Me.txtJobQSearch.Size = New System.Drawing.Size(319, 27)
        Me.txtJobQSearch.TabIndex = 39
        Me.txtJobQSearch.Text = "Search..."
        Me.ToolTip1.SetToolTip(Me.txtJobQSearch, "Separate queries with a semicolon")
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.AutoSize = True
        Me.btnRefresh.BackColor = System.Drawing.Color.SkyBlue
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRefresh.Location = New System.Drawing.Point(10, 3)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(64, 30)
        Me.btnRefresh.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Refresh JobQ (Gets the latest Version)")
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Status
        '
        Me.Status.BackColor = System.Drawing.Color.Transparent
        Me.Status.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.Status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Status1, Me.ToolStripBTP, Me.ToolStripVersion, Me.ProgressBar1})
        Me.Status.Location = New System.Drawing.Point(0, 569)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(1316, 25)
        Me.Status.TabIndex = 13
        Me.Status.Text = "StatusStrip1"
        '
        'Status1
        '
        Me.Status1.BackColor = System.Drawing.Color.Azure
        Me.Status1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Status1.Name = "Status1"
        Me.Status1.Size = New System.Drawing.Size(78, 20)
        Me.Status1.Text = "Status: Good"
        '
        'ToolStripBTP
        '
        Me.ToolStripBTP.IsLink = True
        Me.ToolStripBTP.Name = "ToolStripBTP"
        Me.ToolStripBTP.Size = New System.Drawing.Size(1158, 20)
        Me.ToolStripBTP.Spring = True
        Me.ToolStripBTP.Text = "www.BibleTruthPublishers.com"
        '
        'ToolStripVersion
        '
        Me.ToolStripVersion.Name = "ToolStripVersion"
        Me.ToolStripVersion.Size = New System.Drawing.Size(13, 20)
        Me.ToolStripVersion.Text = "v"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(50, 19)
        Me.ProgressBar1.ToolTipText = "Personalize JobQ Load Status"
        '
        'TimerCheckForUpdate
        '
        Me.TimerCheckForUpdate.Interval = 15000
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'TmrUpdatePQ
        '
        Me.TmrUpdatePQ.Interval = 500
        '
        'tmrResetStatus
        '
        Me.tmrResetStatus.Interval = 700
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Azure
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.LogsToolStripMenuItem, Me.ForCbOnlyTSItem, Me.CountsMenuItem, Me.ShareWordToolStripMenuItem, Me.SetDefaultPrinterToolStripMenuItem, Me.NoteToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip1.Size = New System.Drawing.Size(1316, 28)
        Me.MenuStrip1.TabIndex = 48
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WebBrowserToolStripMenuItem, Me.HelpToolStripMenuItem1, Me.SettingsToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'WebBrowserToolStripMenuItem
        '
        Me.WebBrowserToolStripMenuItem.Name = "WebBrowserToolStripMenuItem"
        Me.WebBrowserToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.WebBrowserToolStripMenuItem.Text = "Web Browser"
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(143, 22)
        Me.HelpToolStripMenuItem1.Text = "Help"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.BackColor = System.Drawing.Color.Azure
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateStatusLabelsToolStripMenuItem, Me.CreateBarcodeImagesToolStripMenuItem, Me.ToolStripSeparator2, Me.TractPreviewToolStripMenuItem, Me.ToolStripSeparator3, Me.CreateJobQBackupToolStripMenuItem, Me.ToolStripSeparator1, Me.ImposeBookPamEtcToolStripMenuItem, Me.CreateCustomProofGPToolStripMenuItem, Me.HomePrintTractsToolStripMenuItem, Me.ToolStripSeparator5, Me.GetGPEmailsToolStripMenuItem, Me.ToolStripSeparator7, Me.ToolStripMenuItem1, Me.ToolStripSeparator10, Me.RepopulateFoldersToolStripMenuItem, Me.PrintTCFilesToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(46, 24)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'CreateStatusLabelsToolStripMenuItem
        '
        Me.CreateStatusLabelsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.CreateStatusLabelsToolStripMenuItem.Name = "CreateStatusLabelsToolStripMenuItem"
        Me.CreateStatusLabelsToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.CreateStatusLabelsToolStripMenuItem.Text = "Create Barcode Labels"
        '
        'CreateBarcodeImagesToolStripMenuItem
        '
        Me.CreateBarcodeImagesToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.CreateBarcodeImagesToolStripMenuItem.Name = "CreateBarcodeImagesToolStripMenuItem"
        Me.CreateBarcodeImagesToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.CreateBarcodeImagesToolStripMenuItem.Text = "Create Barcode Images"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.BackColor = System.Drawing.Color.White
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(271, 6)
        '
        'TractPreviewToolStripMenuItem
        '
        Me.TractPreviewToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.TractPreviewToolStripMenuItem.Name = "TractPreviewToolStripMenuItem"
        Me.TractPreviewToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.TractPreviewToolStripMenuItem.Text = "Tract Preview"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.BackColor = System.Drawing.Color.White
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(271, 6)
        '
        'CreateJobQBackupToolStripMenuItem
        '
        Me.CreateJobQBackupToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.CreateJobQBackupToolStripMenuItem.Name = "CreateJobQBackupToolStripMenuItem"
        Me.CreateJobQBackupToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.CreateJobQBackupToolStripMenuItem.Text = "Create Lineup Backup"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.BackColor = System.Drawing.Color.White
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(271, 6)
        '
        'ImposeBookPamEtcToolStripMenuItem
        '
        Me.ImposeBookPamEtcToolStripMenuItem.Name = "ImposeBookPamEtcToolStripMenuItem"
        Me.ImposeBookPamEtcToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.ImposeBookPamEtcToolStripMenuItem.Text = "Impose Book/Pam/Etc."
        '
        'CreateCustomProofGPToolStripMenuItem
        '
        Me.CreateCustomProofGPToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.CreateCustomProofGPToolStripMenuItem.Name = "CreateCustomProofGPToolStripMenuItem"
        Me.CreateCustomProofGPToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.CreateCustomProofGPToolStripMenuItem.Text = "Impose Design ID"
        '
        'HomePrintTractsToolStripMenuItem
        '
        Me.HomePrintTractsToolStripMenuItem.Name = "HomePrintTractsToolStripMenuItem"
        Me.HomePrintTractsToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.HomePrintTractsToolStripMenuItem.Text = "Impose Home Print Tracts && Web Pics"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(271, 6)
        '
        'GetGPEmailsToolStripMenuItem
        '
        Me.GetGPEmailsToolStripMenuItem.Name = "GetGPEmailsToolStripMenuItem"
        Me.GetGPEmailsToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.GetGPEmailsToolStripMenuItem.Text = "Get GP Emails From Folder Names"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(271, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HarvestShareWordFilesToolStripMenuItem1, Me.FindFoldersWithMultipleFilesToolStripMenuItem, Me.BatchCreateProductionFilesToolStripMenuItem1})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(274, 22)
        Me.ToolStripMenuItem1.Text = "Share Word Utilities"
        '
        'HarvestShareWordFilesToolStripMenuItem1
        '
        Me.HarvestShareWordFilesToolStripMenuItem1.Name = "HarvestShareWordFilesToolStripMenuItem1"
        Me.HarvestShareWordFilesToolStripMenuItem1.Size = New System.Drawing.Size(239, 22)
        Me.HarvestShareWordFilesToolStripMenuItem1.Text = "Harvest Share Word Files"
        '
        'FindFoldersWithMultipleFilesToolStripMenuItem
        '
        Me.FindFoldersWithMultipleFilesToolStripMenuItem.Name = "FindFoldersWithMultipleFilesToolStripMenuItem"
        Me.FindFoldersWithMultipleFilesToolStripMenuItem.Size = New System.Drawing.Size(239, 22)
        Me.FindFoldersWithMultipleFilesToolStripMenuItem.Text = "Find Folders With Multiple Files"
        '
        'BatchCreateProductionFilesToolStripMenuItem1
        '
        Me.BatchCreateProductionFilesToolStripMenuItem1.Name = "BatchCreateProductionFilesToolStripMenuItem1"
        Me.BatchCreateProductionFilesToolStripMenuItem1.Size = New System.Drawing.Size(239, 22)
        Me.BatchCreateProductionFilesToolStripMenuItem1.Text = "Batch Create Production Files"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(271, 6)
        '
        'RepopulateFoldersToolStripMenuItem
        '
        Me.RepopulateFoldersToolStripMenuItem.Name = "RepopulateFoldersToolStripMenuItem"
        Me.RepopulateFoldersToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.RepopulateFoldersToolStripMenuItem.Text = "Book Cover Manager"
        '
        'PrintTCFilesToolStripMenuItem
        '
        Me.PrintTCFilesToolStripMenuItem.Name = "PrintTCFilesToolStripMenuItem"
        Me.PrintTCFilesToolStripMenuItem.Size = New System.Drawing.Size(274, 22)
        Me.PrintTCFilesToolStripMenuItem.Text = "Print TC Files"
        '
        'LogsToolStripMenuItem
        '
        Me.LogsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PurchasingToolStripMenuItem, Me.ComputerSoftwareKeysToolStripMenuItem, Me.LineUpErrorLogToolStripMenuItem, Me.EmailNotificationLogToolStripMenuItem})
        Me.LogsToolStripMenuItem.Name = "LogsToolStripMenuItem"
        Me.LogsToolStripMenuItem.Size = New System.Drawing.Size(44, 24)
        Me.LogsToolStripMenuItem.Text = "Logs"
        '
        'PurchasingToolStripMenuItem
        '
        Me.PurchasingToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.PurchasingToolStripMenuItem.Name = "PurchasingToolStripMenuItem"
        Me.PurchasingToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.PurchasingToolStripMenuItem.Text = "Supplies Purchasing History"
        '
        'ComputerSoftwareKeysToolStripMenuItem
        '
        Me.ComputerSoftwareKeysToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.ComputerSoftwareKeysToolStripMenuItem.Name = "ComputerSoftwareKeysToolStripMenuItem"
        Me.ComputerSoftwareKeysToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.ComputerSoftwareKeysToolStripMenuItem.Text = "Computer Software Licensing"
        '
        'LineUpErrorLogToolStripMenuItem
        '
        Me.LineUpErrorLogToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.LineUpErrorLogToolStripMenuItem.Name = "LineUpErrorLogToolStripMenuItem"
        Me.LineUpErrorLogToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.LineUpErrorLogToolStripMenuItem.Text = "Error Log"
        '
        'EmailNotificationLogToolStripMenuItem
        '
        Me.EmailNotificationLogToolStripMenuItem.Name = "EmailNotificationLogToolStripMenuItem"
        Me.EmailNotificationLogToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.EmailNotificationLogToolStripMenuItem.Text = "Email Notification Log"
        '
        'ForCbOnlyTSItem
        '
        Me.ForCbOnlyTSItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowPrinterToolStripMenuItem, Me.InvToolStripMenuItem, Me.OutputProductsToolStripMenuItem, Me.FinishedYearCountToolStripMenuItem, Me.SendEmailReportToolStripMenuItem, Me.CheckProofRequestsToolStripMenuItem, Me.TestFindFilesToolStripMenuItem})
        Me.ForCbOnlyTSItem.Name = "ForCbOnlyTSItem"
        Me.ForCbOnlyTSItem.Size = New System.Drawing.Size(56, 24)
        Me.ForCbOnlyTSItem.Text = "Testing"
        '
        'ShowPrinterToolStripMenuItem
        '
        Me.ShowPrinterToolStripMenuItem.Name = "ShowPrinterToolStripMenuItem"
        Me.ShowPrinterToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.ShowPrinterToolStripMenuItem.Text = "Show Default Printer"
        '
        'InvToolStripMenuItem
        '
        Me.InvToolStripMenuItem.Name = "InvToolStripMenuItem"
        Me.InvToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.InvToolStripMenuItem.Text = "Interact with UV"
        '
        'OutputProductsToolStripMenuItem
        '
        Me.OutputProductsToolStripMenuItem.Name = "OutputProductsToolStripMenuItem"
        Me.OutputProductsToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.OutputProductsToolStripMenuItem.Text = "Export Products XML to Desktop"
        '
        'FinishedYearCountToolStripMenuItem
        '
        Me.FinishedYearCountToolStripMenuItem.Name = "FinishedYearCountToolStripMenuItem"
        Me.FinishedYearCountToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.FinishedYearCountToolStripMenuItem.Text = "Show Finished Year Count"
        '
        'SendEmailReportToolStripMenuItem
        '
        Me.SendEmailReportToolStripMenuItem.Name = "SendEmailReportToolStripMenuItem"
        Me.SendEmailReportToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.SendEmailReportToolStripMenuItem.Text = "Send Email Report"
        '
        'CheckProofRequestsToolStripMenuItem
        '
        Me.CheckProofRequestsToolStripMenuItem.Name = "CheckProofRequestsToolStripMenuItem"
        Me.CheckProofRequestsToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.CheckProofRequestsToolStripMenuItem.Text = "Send Proof Requests Email Report"
        '
        'TestFindFilesToolStripMenuItem
        '
        Me.TestFindFilesToolStripMenuItem.Name = "TestFindFilesToolStripMenuItem"
        Me.TestFindFilesToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.TestFindFilesToolStripMenuItem.Text = "test find files"
        '
        'CountsMenuItem
        '
        Me.CountsMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.CountsMenuItem.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.CountsMenuItem.Name = "CountsMenuItem"
        Me.CountsMenuItem.Size = New System.Drawing.Size(51, 24)
        Me.CountsMenuItem.Text = "_____"
        '
        'ShareWordToolStripMenuItem
        '
        Me.ShareWordToolStripMenuItem.Name = "ShareWordToolStripMenuItem"
        Me.ShareWordToolStripMenuItem.Size = New System.Drawing.Size(130, 24)
        Me.ShareWordToolStripMenuItem.Text = "Share Word Manager"
        '
        'SetDefaultPrinterToolStripMenuItem
        '
        Me.SetDefaultPrinterToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue
        Me.SetDefaultPrinterToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.SetDefaultPrinterToolStripMenuItem.Name = "SetDefaultPrinterToolStripMenuItem"
        Me.SetDefaultPrinterToolStripMenuItem.Size = New System.Drawing.Size(114, 24)
        Me.SetDefaultPrinterToolStripMenuItem.Text = "Set Default Printer"
        '
        'NoteToolStripMenuItem
        '
        Me.NoteToolStripMenuItem.Name = "NoteToolStripMenuItem"
        Me.NoteToolStripMenuItem.Size = New System.Drawing.Size(43, 24)
        Me.NoteToolStripMenuItem.Text = "note"
        Me.NoteToolStripMenuItem.Visible = False
        '
        'tmrPersonalizeDisplay
        '
        Me.tmrPersonalizeDisplay.Interval = 500
        '
        'SplitContainer8
        '
        Me.SplitContainer8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer8.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer8.Location = New System.Drawing.Point(0, 28)
        Me.SplitContainer8.Name = "SplitContainer8"
        Me.SplitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer8.Panel1
        '
        Me.SplitContainer8.Panel1.Controls.Add(Me.LineUpTabCtrl)
        '
        'SplitContainer8.Panel2
        '
        Me.SplitContainer8.Panel2.Controls.Add(Me.TabControlInfo)
        Me.SplitContainer8.Panel2MinSize = 100
        Me.SplitContainer8.Size = New System.Drawing.Size(1316, 541)
        Me.SplitContainer8.SplitterDistance = 391
        Me.SplitContainer8.TabIndex = 47
        '
        'LineUpTabCtrl
        '
        Me.LineUpTabCtrl.Controls.Add(Me.TabProd)
        Me.LineUpTabCtrl.Controls.Add(Me.tabPrint)
        Me.LineUpTabCtrl.Controls.Add(Me.TabPersonalize)
        Me.LineUpTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LineUpTabCtrl.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.LineUpTabCtrl.ItemSize = New System.Drawing.Size(25, 25)
        Me.LineUpTabCtrl.Location = New System.Drawing.Point(0, 0)
        Me.LineUpTabCtrl.Name = "LineUpTabCtrl"
        Me.LineUpTabCtrl.Padding = New System.Drawing.Point(30, 3)
        Me.LineUpTabCtrl.SelectedIndex = 0
        Me.LineUpTabCtrl.Size = New System.Drawing.Size(1316, 391)
        Me.LineUpTabCtrl.TabIndex = 14
        '
        'TabProd
        '
        Me.TabProd.BackColor = System.Drawing.Color.White
        Me.TabProd.Controls.Add(Me.SplitContainer10)
        Me.TabProd.Location = New System.Drawing.Point(4, 29)
        Me.TabProd.Name = "TabProd"
        Me.TabProd.Padding = New System.Windows.Forms.Padding(3)
        Me.TabProd.Size = New System.Drawing.Size(1308, 358)
        Me.TabProd.TabIndex = 3
        Me.TabProd.Text = "Job Queue"
        '
        'SplitContainer10
        '
        Me.SplitContainer10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer10.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer10.IsSplitterFixed = True
        Me.SplitContainer10.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer10.Name = "SplitContainer10"
        Me.SplitContainer10.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer10.Panel1
        '
        Me.SplitContainer10.Panel1.Controls.Add(Me.FlowPrinterInfo)
        '
        'SplitContainer10.Panel2
        '
        Me.SplitContainer10.Panel2.Controls.Add(Me.SplitContainer9)
        Me.SplitContainer10.Size = New System.Drawing.Size(1302, 352)
        Me.SplitContainer10.SplitterDistance = 58
        Me.SplitContainer10.TabIndex = 42
        '
        'FlowPrinterInfo
        '
        Me.FlowPrinterInfo.AutoScroll = True
        Me.FlowPrinterInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowPrinterInfo.Location = New System.Drawing.Point(0, 0)
        Me.FlowPrinterInfo.Name = "FlowPrinterInfo"
        Me.FlowPrinterInfo.Size = New System.Drawing.Size(1302, 58)
        Me.FlowPrinterInfo.TabIndex = 0
        '
        'SplitContainer9
        '
        Me.SplitContainer9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer9.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer9.IsSplitterFixed = True
        Me.SplitContainer9.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer9.Name = "SplitContainer9"
        Me.SplitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer9.Panel1
        '
        Me.SplitContainer9.Panel1.Controls.Add(Me.dgvJobQ)
        '
        'SplitContainer9.Panel2
        '
        Me.SplitContainer9.Panel2.Controls.Add(Me.TableLayoutPanel10)
        Me.SplitContainer9.Size = New System.Drawing.Size(1302, 290)
        Me.SplitContainer9.SplitterDistance = 252
        Me.SplitContainer9.TabIndex = 41
        '
        'dgvJobQ
        '
        Me.dgvJobQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvJobQ.ContextMenuStrip = Me.JobQMenuStrip
        Me.dgvJobQ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvJobQ.Location = New System.Drawing.Point(0, 0)
        Me.dgvJobQ.Name = "dgvJobQ"
        Me.dgvJobQ.Size = New System.Drawing.Size(1302, 252)
        Me.dgvJobQ.TabIndex = 1
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 4
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.btnSearchProds, 3, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.txtJobQSearch, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.FlowLayoutPanel2, 2, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.FlowLayoutPanel1, 1, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(1302, 34)
        Me.TableLayoutPanel10.TabIndex = 0
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.btnRefresh)
        Me.FlowLayoutPanel2.Controls.Add(Me.btnAccounting)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(650, 0)
        Me.FlowLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(325, 34)
        Me.FlowLayoutPanel2.TabIndex = 42
        '
        'btnAccounting
        '
        Me.btnAccounting.Location = New System.Drawing.Point(94, 3)
        Me.btnAccounting.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.btnAccounting.Name = "btnAccounting"
        Me.btnAccounting.Size = New System.Drawing.Size(153, 30)
        Me.btnAccounting.TabIndex = 36
        Me.btnAccounting.Text = "Year End Accounting"
        Me.btnAccounting.UseVisualStyleBackColor = True
        Me.btnAccounting.Visible = False
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Label1)
        Me.FlowLayoutPanel1.Controls.Add(Me.rdbJobQCurrent)
        Me.FlowLayoutPanel1.Controls.Add(Me.rdbJobQHistory)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(328, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(319, 28)
        Me.FlowLayoutPanel1.TabIndex = 41
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 19)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "              "
        '
        'rdbJobQCurrent
        '
        Me.rdbJobQCurrent.AutoSize = True
        Me.rdbJobQCurrent.Checked = True
        Me.rdbJobQCurrent.Location = New System.Drawing.Point(74, 3)
        Me.rdbJobQCurrent.Name = "rdbJobQCurrent"
        Me.rdbJobQCurrent.Size = New System.Drawing.Size(94, 23)
        Me.rdbJobQCurrent.TabIndex = 0
        Me.rdbJobQCurrent.TabStop = True
        Me.rdbJobQCurrent.Text = "Current     "
        Me.rdbJobQCurrent.UseVisualStyleBackColor = True
        '
        'rdbJobQHistory
        '
        Me.rdbJobQHistory.AutoSize = True
        Me.rdbJobQHistory.Location = New System.Drawing.Point(174, 3)
        Me.rdbJobQHistory.Name = "rdbJobQHistory"
        Me.rdbJobQHistory.Size = New System.Drawing.Size(91, 23)
        Me.rdbJobQHistory.TabIndex = 1
        Me.rdbJobQHistory.Text = "History     "
        Me.rdbJobQHistory.UseVisualStyleBackColor = True
        '
        'tabPrint
        '
        Me.tabPrint.BackColor = System.Drawing.Color.White
        Me.tabPrint.Controls.Add(Me.ProductionFilesFlow)
        Me.tabPrint.Location = New System.Drawing.Point(4, 29)
        Me.tabPrint.Name = "tabPrint"
        Me.tabPrint.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPrint.Size = New System.Drawing.Size(1308, 358)
        Me.tabPrint.TabIndex = 0
        Me.tabPrint.Text = "Production"
        '
        'ProductionFilesFlow
        '
        Me.ProductionFilesFlow.AutoScroll = True
        Me.ProductionFilesFlow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProductionFilesFlow.Location = New System.Drawing.Point(3, 3)
        Me.ProductionFilesFlow.Name = "ProductionFilesFlow"
        Me.ProductionFilesFlow.Size = New System.Drawing.Size(1302, 352)
        Me.ProductionFilesFlow.TabIndex = 41
        '
        'TabPersonalize
        '
        Me.TabPersonalize.BackColor = System.Drawing.Color.White
        Me.TabPersonalize.Controls.Add(Me.SplitContainer6)
        Me.TabPersonalize.Location = New System.Drawing.Point(4, 29)
        Me.TabPersonalize.Name = "TabPersonalize"
        Me.TabPersonalize.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPersonalize.Size = New System.Drawing.Size(1308, 358)
        Me.TabPersonalize.TabIndex = 6
        Me.TabPersonalize.Text = "Personalize"
        Me.TabPersonalize.UseVisualStyleBackColor = True
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer6.Name = "SplitContainer6"
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.SplitContainer7)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer6.Size = New System.Drawing.Size(1302, 352)
        Me.SplitContainer6.SplitterDistance = 757
        Me.SplitContainer6.TabIndex = 0
        '
        'SplitContainer7
        '
        Me.SplitContainer7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer7.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer7.IsSplitterFixed = True
        Me.SplitContainer7.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer7.Name = "SplitContainer7"
        Me.SplitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer7.Panel1
        '
        Me.SplitContainer7.Panel1.Controls.Add(Me.dgvPersonalizeJobQ)
        '
        'SplitContainer7.Panel2
        '
        Me.SplitContainer7.Panel2.Controls.Add(Me.rdbAllOrders)
        Me.SplitContainer7.Panel2.Controls.Add(Me.btnCustSearch)
        Me.SplitContainer7.Panel2.Controls.Add(Me.lblDesignsShowing)
        Me.SplitContainer7.Panel2.Controls.Add(Me.rdbArchive)
        Me.SplitContainer7.Panel2.Controls.Add(Me.rdbCurrent)
        Me.SplitContainer7.Panel2.Controls.Add(Me.txtPersonalizeSearch)
        Me.SplitContainer7.Panel2.Controls.Add(Me.btnDesignsRefresh)
        Me.SplitContainer7.Size = New System.Drawing.Size(757, 352)
        Me.SplitContainer7.SplitterDistance = 318
        Me.SplitContainer7.TabIndex = 1
        '
        'dgvPersonalizeJobQ
        '
        Me.dgvPersonalizeJobQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPersonalizeJobQ.ContextMenuStrip = Me.PersonalizeJobQContextMenu
        Me.dgvPersonalizeJobQ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPersonalizeJobQ.Location = New System.Drawing.Point(0, 0)
        Me.dgvPersonalizeJobQ.Name = "dgvPersonalizeJobQ"
        Me.dgvPersonalizeJobQ.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPersonalizeJobQ.Size = New System.Drawing.Size(757, 318)
        Me.dgvPersonalizeJobQ.TabIndex = 0
        '
        'rdbAllOrders
        '
        Me.rdbAllOrders.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbAllOrders.AutoSize = True
        Me.rdbAllOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbAllOrders.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbAllOrders.Location = New System.Drawing.Point(414, 4)
        Me.rdbAllOrders.Name = "rdbAllOrders"
        Me.rdbAllOrders.Size = New System.Drawing.Size(41, 23)
        Me.rdbAllOrders.TabIndex = 63
        Me.rdbAllOrders.Text = "All"
        Me.rdbAllOrders.UseVisualStyleBackColor = True
        '
        'lblDesignsShowing
        '
        Me.lblDesignsShowing.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDesignsShowing.AutoSize = True
        Me.lblDesignsShowing.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDesignsShowing.Location = New System.Drawing.Point(641, 6)
        Me.lblDesignsShowing.Name = "lblDesignsShowing"
        Me.lblDesignsShowing.Size = New System.Drawing.Size(29, 19)
        Me.lblDesignsShowing.TabIndex = 61
        Me.lblDesignsShowing.Text = "IDs"
        '
        'rdbArchive
        '
        Me.rdbArchive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbArchive.AutoSize = True
        Me.rdbArchive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbArchive.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbArchive.Location = New System.Drawing.Point(337, 4)
        Me.rdbArchive.Name = "rdbArchive"
        Me.rdbArchive.Size = New System.Drawing.Size(71, 23)
        Me.rdbArchive.TabIndex = 60
        Me.rdbArchive.Text = "Archive"
        Me.rdbArchive.UseVisualStyleBackColor = True
        '
        'rdbCurrent
        '
        Me.rdbCurrent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbCurrent.AutoSize = True
        Me.rdbCurrent.Checked = True
        Me.rdbCurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbCurrent.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbCurrent.Location = New System.Drawing.Point(258, 3)
        Me.rdbCurrent.Name = "rdbCurrent"
        Me.rdbCurrent.Size = New System.Drawing.Size(73, 23)
        Me.rdbCurrent.TabIndex = 59
        Me.rdbCurrent.TabStop = True
        Me.rdbCurrent.Text = "Current"
        Me.rdbCurrent.UseVisualStyleBackColor = True
        '
        'txtPersonalizeSearch
        '
        Me.txtPersonalizeSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPersonalizeSearch.BackColor = System.Drawing.Color.SkyBlue
        Me.txtPersonalizeSearch.Location = New System.Drawing.Point(3, 3)
        Me.txtPersonalizeSearch.Name = "txtPersonalizeSearch"
        Me.txtPersonalizeSearch.Size = New System.Drawing.Size(249, 26)
        Me.txtPersonalizeSearch.TabIndex = 58
        Me.txtPersonalizeSearch.Text = "Search..."
        '
        'btnDesignsRefresh
        '
        Me.btnDesignsRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDesignsRefresh.AutoSize = True
        Me.btnDesignsRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDesignsRefresh.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDesignsRefresh.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnDesignsRefresh.FlatAppearance.BorderSize = 0
        Me.btnDesignsRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnDesignsRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDesignsRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnDesignsRefresh.ForeColor = System.Drawing.Color.White
        Me.btnDesignsRefresh.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDesignsRefresh.Location = New System.Drawing.Point(691, 0)
        Me.btnDesignsRefresh.Name = "btnDesignsRefresh"
        Me.btnDesignsRefresh.Size = New System.Drawing.Size(63, 27)
        Me.btnDesignsRefresh.TabIndex = 57
        Me.btnDesignsRefresh.Text = "Refresh"
        Me.btnDesignsRefresh.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 6
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnImpose, 2, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.btnPrintLabels, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.btnArchive, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.pdfInvoice, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.FlowLayoutPanel3, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.txtShipNote, 3, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Label9, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbStatusHistory, 1, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbStatus, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label8, 0, 5)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 7
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(541, 352)
        Me.TableLayoutPanel2.TabIndex = 72
        '
        'btnPrintLabels
        '
        Me.btnPrintLabels.AutoSize = True
        Me.btnPrintLabels.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnPrintLabels.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPrintLabels.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPrintLabels.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnPrintLabels.FlatAppearance.BorderSize = 0
        Me.btnPrintLabels.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnPrintLabels.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintLabels.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnPrintLabels.ForeColor = System.Drawing.Color.White
        Me.btnPrintLabels.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnPrintLabels.Location = New System.Drawing.Point(92, 243)
        Me.btnPrintLabels.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPrintLabels.MinimumSize = New System.Drawing.Size(0, 30)
        Me.btnPrintLabels.Name = "btnPrintLabels"
        Me.btnPrintLabels.Size = New System.Drawing.Size(86, 46)
        Me.btnPrintLabels.TabIndex = 65
        Me.btnPrintLabels.Text = "Print Labels"
        Me.btnPrintLabels.UseVisualStyleBackColor = False
        '
        'btnArchive
        '
        Me.btnArchive.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnArchive.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnArchive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnArchive.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnArchive.FlatAppearance.BorderSize = 0
        Me.btnArchive.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnArchive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnArchive.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnArchive.ForeColor = System.Drawing.Color.White
        Me.btnArchive.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnArchive.Location = New System.Drawing.Point(2, 243)
        Me.btnArchive.Margin = New System.Windows.Forms.Padding(2)
        Me.btnArchive.MinimumSize = New System.Drawing.Size(0, 30)
        Me.btnArchive.Name = "btnArchive"
        Me.btnArchive.Size = New System.Drawing.Size(86, 46)
        Me.btnArchive.TabIndex = 57
        Me.btnArchive.Text = "Send to Archive"
        Me.btnArchive.UseVisualStyleBackColor = False
        '
        'pdfInvoice
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.pdfInvoice, 6)
        Me.pdfInvoice.CreationProperties = Nothing
        Me.pdfInvoice.DefaultBackgroundColor = System.Drawing.Color.White
        Me.pdfInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pdfInvoice.Location = New System.Drawing.Point(3, 3)
        Me.pdfInvoice.Name = "pdfInvoice"
        Me.TableLayoutPanel2.SetRowSpan(Me.pdfInvoice, 3)
        Me.pdfInvoice.Size = New System.Drawing.Size(535, 185)
        Me.pdfInvoice.TabIndex = 72
        Me.pdfInvoice.ZoomFactor = 1.0R
        '
        'FlowLayoutPanel3
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.FlowLayoutPanel3, 4)
        Me.FlowLayoutPanel3.Controls.Add(Me.btnPrev)
        Me.FlowLayoutPanel3.Controls.Add(Me.rdbInvoice)
        Me.FlowLayoutPanel3.Controls.Add(Me.rdbBodyPDF)
        Me.FlowLayoutPanel3.Controls.Add(Me.rdbCovPDF)
        Me.FlowLayoutPanel3.Controls.Add(Me.rdbLabels)
        Me.FlowLayoutPanel3.Controls.Add(Me.btnNext)
        Me.FlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(93, 194)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(354, 44)
        Me.FlowLayoutPanel3.TabIndex = 73
        '
        'btnPrev
        '
        Me.btnPrev.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnPrev.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPrev.BackgroundImage = CType(resources.GetObject("btnPrev.BackgroundImage"), System.Drawing.Image)
        Me.btnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPrev.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnPrev.FlatAppearance.BorderSize = 0
        Me.btnPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrev.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnPrev.ForeColor = System.Drawing.Color.White
        Me.btnPrev.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnPrev.Location = New System.Drawing.Point(3, 3)
        Me.btnPrev.MinimumSize = New System.Drawing.Size(0, 30)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(30, 30)
        Me.btnPrev.TabIndex = 66
        Me.btnPrev.UseVisualStyleBackColor = False
        '
        'rdbCovPDF
        '
        Me.rdbCovPDF.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rdbCovPDF.AutoSize = True
        Me.rdbCovPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbCovPDF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Underline)
        Me.rdbCovPDF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbCovPDF.Location = New System.Drawing.Point(186, 7)
        Me.rdbCovPDF.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbCovPDF.Name = "rdbCovPDF"
        Me.rdbCovPDF.Size = New System.Drawing.Size(85, 21)
        Me.rdbCovPDF.TabIndex = 62
        Me.rdbCovPDF.Text = "Cover PDF"
        Me.rdbCovPDF.UseVisualStyleBackColor = True
        '
        'rdbLabels
        '
        Me.rdbLabels.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rdbLabels.AutoSize = True
        Me.rdbLabels.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbLabels.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Underline)
        Me.rdbLabels.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbLabels.Location = New System.Drawing.Point(273, 7)
        Me.rdbLabels.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbLabels.Name = "rdbLabels"
        Me.rdbLabels.Size = New System.Drawing.Size(62, 21)
        Me.rdbLabels.TabIndex = 62
        Me.rdbLabels.Text = "Labels"
        Me.rdbLabels.UseVisualStyleBackColor = True
        Me.rdbLabels.Visible = False
        '
        'btnNext
        '
        Me.btnNext.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNext.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNext.BackgroundImage = CType(resources.GetObject("btnNext.BackgroundImage"), System.Drawing.Image)
        Me.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnNext.ForeColor = System.Drawing.Color.White
        Me.btnNext.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnNext.Location = New System.Drawing.Point(3, 39)
        Me.btnNext.MinimumSize = New System.Drawing.Size(0, 30)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(30, 30)
        Me.btnNext.TabIndex = 65
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'txtShipNote
        '
        Me.txtShipNote.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.SetColumnSpan(Me.txtShipNote, 3)
        Me.txtShipNote.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtShipNote.Location = New System.Drawing.Point(273, 244)
        Me.txtShipNote.Multiline = True
        Me.txtShipNote.Name = "txtShipNote"
        Me.txtShipNote.ReadOnly = True
        Me.TableLayoutPanel2.SetRowSpan(Me.txtShipNote, 3)
        Me.txtShipNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtShipNote.Size = New System.Drawing.Size(265, 105)
        Me.txtShipNote.TabIndex = 58
        Me.txtShipNote.Text = "Ship Note:"
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label9.AutoSize = True
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(31, 327)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 19)
        Me.Label9.TabIndex = 70
        Me.Label9.Text = "History:"
        '
        'cmbStatusHistory
        '
        Me.cmbStatusHistory.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.SetColumnSpan(Me.cmbStatusHistory, 2)
        Me.cmbStatusHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbStatusHistory.FormattingEnabled = True
        Me.cmbStatusHistory.Location = New System.Drawing.Point(93, 324)
        Me.cmbStatusHistory.Name = "cmbStatusHistory"
        Me.cmbStatusHistory.Size = New System.Drawing.Size(174, 27)
        Me.cmbStatusHistory.TabIndex = 71
        '
        'cmbStatus
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.cmbStatus, 2)
        Me.cmbStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Items.AddRange(New Object() {"Order", "Imposed", "Printing (1200a)", "Printing (1200b)", "Drilled", "Filled", "Shipped"})
        Me.cmbStatus.Location = New System.Drawing.Point(93, 294)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(174, 27)
        Me.cmbStatus.TabIndex = 69
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label8.AutoSize = True
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(37, 296)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 19)
        Me.Label8.TabIndex = 68
        Me.Label8.Text = "Status:"
        '
        'TabControlInfo
        '
        Me.TabControlInfo.Controls.Add(Me.TabProdInfo)
        Me.TabControlInfo.Controls.Add(Me.TabOrderInfo)
        Me.TabControlInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlInfo.ItemSize = New System.Drawing.Size(90, 20)
        Me.TabControlInfo.Location = New System.Drawing.Point(0, 0)
        Me.TabControlInfo.Name = "TabControlInfo"
        Me.TabControlInfo.Padding = New System.Drawing.Point(30, 3)
        Me.TabControlInfo.SelectedIndex = 0
        Me.TabControlInfo.Size = New System.Drawing.Size(1316, 146)
        Me.TabControlInfo.TabIndex = 40
        '
        'TabProdInfo
        '
        Me.TabProdInfo.Controls.Add(Me.TableLayoutPanel1)
        Me.TabProdInfo.Location = New System.Drawing.Point(4, 24)
        Me.TabProdInfo.Name = "TabProdInfo"
        Me.TabProdInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.TabProdInfo.Size = New System.Drawing.Size(1308, 118)
        Me.TabProdInfo.TabIndex = 1
        Me.TabProdInfo.Text = "Product Info"
        Me.TabProdInfo.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 10
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.txtSalePrice, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtProductionCost, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtType, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnLoadProd, 8, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPageCt, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtSource, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtAuthor, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtInvTitle, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtlTitle, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtQuantityPrint, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblQuantitylbl, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTitlelbl, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblInvTitlelbl, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblAuthorlbl, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtProdNumPrint, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblItemNumlbl, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblSourcelbl, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPageCtlbl, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTypelbl, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblSalePricelbl, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblProductionCostlbl, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pbProducts, 9, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnAlignSheets, 8, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCatalog, 4, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1302, 112)
        Me.TableLayoutPanel1.TabIndex = 43
        '
        'txtSource
        '
        Me.txtSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSource.BackColor = System.Drawing.Color.White
        Me.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSource.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtSource.Location = New System.Drawing.Point(103, 89)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.ReadOnly = True
        Me.txtSource.Size = New System.Drawing.Size(264, 17)
        Me.txtSource.TabIndex = 56
        '
        'txtAuthor
        '
        Me.txtAuthor.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAuthor.BackColor = System.Drawing.Color.White
        Me.txtAuthor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAuthor.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtAuthor.Location = New System.Drawing.Point(103, 61)
        Me.txtAuthor.Name = "txtAuthor"
        Me.txtAuthor.ReadOnly = True
        Me.txtAuthor.Size = New System.Drawing.Size(264, 17)
        Me.txtAuthor.TabIndex = 55
        '
        'txtInvTitle
        '
        Me.txtInvTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInvTitle.BackColor = System.Drawing.Color.White
        Me.txtInvTitle.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInvTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtInvTitle.Location = New System.Drawing.Point(103, 33)
        Me.txtInvTitle.Name = "txtInvTitle"
        Me.txtInvTitle.ReadOnly = True
        Me.txtInvTitle.Size = New System.Drawing.Size(264, 17)
        Me.txtInvTitle.TabIndex = 54
        '
        'txtlTitle
        '
        Me.txtlTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtlTitle.BackColor = System.Drawing.Color.White
        Me.txtlTitle.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtlTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtlTitle.Location = New System.Drawing.Point(103, 5)
        Me.txtlTitle.Name = "txtlTitle"
        Me.txtlTitle.ReadOnly = True
        Me.txtlTitle.Size = New System.Drawing.Size(264, 17)
        Me.txtlTitle.TabIndex = 40
        '
        'lblQuantitylbl
        '
        Me.lblQuantitylbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblQuantitylbl.AutoSize = True
        Me.lblQuantitylbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblQuantitylbl.Location = New System.Drawing.Point(401, 32)
        Me.lblQuantitylbl.Name = "lblQuantitylbl"
        Me.lblQuantitylbl.Size = New System.Drawing.Size(66, 19)
        Me.lblQuantitylbl.TabIndex = 52
        Me.lblQuantitylbl.Text = "Quantity:"
        '
        'lblTitlelbl
        '
        Me.lblTitlelbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblTitlelbl.AutoSize = True
        Me.lblTitlelbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTitlelbl.Location = New System.Drawing.Point(60, 4)
        Me.lblTitlelbl.Name = "lblTitlelbl"
        Me.lblTitlelbl.Size = New System.Drawing.Size(37, 19)
        Me.lblTitlelbl.TabIndex = 45
        Me.lblTitlelbl.Text = "Title:"
        '
        'lblInvTitlelbl
        '
        Me.lblInvTitlelbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblInvTitlelbl.AutoSize = True
        Me.lblInvTitlelbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblInvTitlelbl.Location = New System.Drawing.Point(5, 32)
        Me.lblInvTitlelbl.Name = "lblInvTitlelbl"
        Me.lblInvTitlelbl.Size = New System.Drawing.Size(92, 19)
        Me.lblInvTitlelbl.TabIndex = 46
        Me.lblInvTitlelbl.Text = "Inverted Title:"
        '
        'lblAuthorlbl
        '
        Me.lblAuthorlbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblAuthorlbl.AutoSize = True
        Me.lblAuthorlbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAuthorlbl.Location = New System.Drawing.Point(42, 60)
        Me.lblAuthorlbl.Name = "lblAuthorlbl"
        Me.lblAuthorlbl.Size = New System.Drawing.Size(55, 19)
        Me.lblAuthorlbl.TabIndex = 37
        Me.lblAuthorlbl.Text = "Author:"
        '
        'txtProdNumPrint
        '
        Me.txtProdNumPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProdNumPrint.BackColor = System.Drawing.Color.White
        Me.txtProdNumPrint.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtProdNumPrint.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtProdNumPrint.Location = New System.Drawing.Point(474, 5)
        Me.txtProdNumPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.txtProdNumPrint.MaxLength = 150
        Me.txtProdNumPrint.Name = "txtProdNumPrint"
        Me.txtProdNumPrint.Size = New System.Drawing.Size(127, 17)
        Me.txtProdNumPrint.TabIndex = 50
        '
        'lblItemNumlbl
        '
        Me.lblItemNumlbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblItemNumlbl.AutoSize = True
        Me.lblItemNumlbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblItemNumlbl.Location = New System.Drawing.Point(415, 4)
        Me.lblItemNumlbl.Name = "lblItemNumlbl"
        Me.lblItemNumlbl.Size = New System.Drawing.Size(52, 19)
        Me.lblItemNumlbl.TabIndex = 33
        Me.lblItemNumlbl.Text = "Item #:"
        '
        'lblSourcelbl
        '
        Me.lblSourcelbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblSourcelbl.AutoSize = True
        Me.lblSourcelbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSourcelbl.Location = New System.Drawing.Point(44, 88)
        Me.lblSourcelbl.Name = "lblSourcelbl"
        Me.lblSourcelbl.Size = New System.Drawing.Size(53, 19)
        Me.lblSourcelbl.TabIndex = 42
        Me.lblSourcelbl.Text = "Source:"
        '
        'lblPageCtlbl
        '
        Me.lblPageCtlbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblPageCtlbl.AutoSize = True
        Me.lblPageCtlbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPageCtlbl.Location = New System.Drawing.Point(383, 60)
        Me.lblPageCtlbl.Name = "lblPageCtlbl"
        Me.lblPageCtlbl.Size = New System.Drawing.Size(84, 19)
        Me.lblPageCtlbl.TabIndex = 36
        Me.lblPageCtlbl.Text = "Page Count:"
        '
        'lblTypelbl
        '
        Me.lblTypelbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblTypelbl.AutoSize = True
        Me.lblTypelbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTypelbl.Location = New System.Drawing.Point(427, 88)
        Me.lblTypelbl.Name = "lblTypelbl"
        Me.lblTypelbl.Size = New System.Drawing.Size(40, 19)
        Me.lblTypelbl.TabIndex = 40
        Me.lblTypelbl.Text = "Type:"
        '
        'lblSalePricelbl
        '
        Me.lblSalePricelbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblSalePricelbl.AutoSize = True
        Me.lblSalePricelbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSalePricelbl.Location = New System.Drawing.Point(903, 4)
        Me.lblSalePricelbl.Name = "lblSalePricelbl"
        Me.lblSalePricelbl.Size = New System.Drawing.Size(69, 19)
        Me.lblSalePricelbl.TabIndex = 35
        Me.lblSalePricelbl.Text = "Sale Price:"
        '
        'lblProductionCostlbl
        '
        Me.lblProductionCostlbl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblProductionCostlbl.AutoSize = True
        Me.lblProductionCostlbl.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProductionCostlbl.Location = New System.Drawing.Point(608, 4)
        Me.lblProductionCostlbl.Name = "lblProductionCostlbl"
        Me.lblProductionCostlbl.Size = New System.Drawing.Size(111, 19)
        Me.lblProductionCostlbl.TabIndex = 47
        Me.lblProductionCostlbl.Text = "Production Cost:"
        Me.lblProductionCostlbl.Visible = False
        '
        'pbProducts
        '
        Me.pbProducts.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbProducts.BackColor = System.Drawing.Color.Transparent
        Me.pbProducts.ImageLocation = "\\btp.local\shared\Computer\BTP Programs\LineUp\Resources\PicNotFound.jpg"
        Me.pbProducts.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pbProducts.InitialImage = Nothing
        Me.pbProducts.Location = New System.Drawing.Point(1213, 3)
        Me.pbProducts.Name = "pbProducts"
        Me.TableLayoutPanel1.SetRowSpan(Me.pbProducts, 3)
        Me.pbProducts.Size = New System.Drawing.Size(86, 78)
        Me.pbProducts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbProducts.TabIndex = 0
        Me.pbProducts.TabStop = False
        '
        'txtCatalog
        '
        Me.txtCatalog.BackColor = System.Drawing.Color.White
        Me.txtCatalog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtCatalog, 4)
        Me.txtCatalog.Location = New System.Drawing.Point(608, 31)
        Me.txtCatalog.Name = "txtCatalog"
        Me.txtCatalog.ReadOnly = True
        Me.TableLayoutPanel1.SetRowSpan(Me.txtCatalog, 3)
        Me.txtCatalog.Size = New System.Drawing.Size(499, 78)
        Me.txtCatalog.TabIndex = 53
        Me.txtCatalog.Text = ""
        '
        'TabOrderInfo
        '
        Me.TabOrderInfo.Controls.Add(Me.TableLayoutPanel7)
        Me.TabOrderInfo.Location = New System.Drawing.Point(4, 24)
        Me.TabOrderInfo.Name = "TabOrderInfo"
        Me.TabOrderInfo.Size = New System.Drawing.Size(1308, 118)
        Me.TabOrderInfo.TabIndex = 0
        Me.TabOrderInfo.Text = "Order Info"
        Me.TabOrderInfo.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.AutoSize = True
        Me.TableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel7.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel7.ColumnCount = 6
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.09524!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.80952!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.09524!))
        Me.TableLayoutPanel7.Controls.Add(Me.txtShipAddress, 5, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Label27, 2, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.txtDesignID, 3, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.Label18, 2, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.txtItemNumber, 3, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Label22, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.txtPhoneNumber, 3, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.Label3, 4, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.txtEmail, 1, 3)
        Me.TableLayoutPanel7.Controls.Add(Me.txtReceiptNum, 1, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.Label25, 0, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.txtBillingNameNumber, 1, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.Label23, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.txtMxNumber, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Label7, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.lblCustEmail, 0, 3)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 5
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0007!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0007!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.9993!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.9993!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(1308, 118)
        Me.TableLayoutPanel7.TabIndex = 43
        '
        'Label25
        '
        Me.Label25.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label25.AutoSize = True
        Me.Label25.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label25.Location = New System.Drawing.Point(12, 50)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(110, 19)
        Me.Label25.TabIndex = 46
        Me.Label25.Text = "Receipt Number:"
        '
        'Label23
        '
        Me.Label23.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label23.AutoSize = True
        Me.Label23.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label23.Location = New System.Drawing.Point(12, 26)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(110, 19)
        Me.Label23.TabIndex = 45
        Me.Label23.Text = "Cust. # && Name:"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label7.AutoSize = True
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(37, 2)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 19)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "Mx Number:"
        '
        'lblCustEmail
        '
        Me.lblCustEmail.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblCustEmail.AutoSize = True
        Me.lblCustEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCustEmail.Location = New System.Drawing.Point(78, 74)
        Me.lblCustEmail.Name = "lblCustEmail"
        Me.lblCustEmail.Size = New System.Drawing.Size(44, 19)
        Me.lblCustEmail.TabIndex = 60
        Me.lblCustEmail.TabStop = True
        Me.lblCustEmail.Text = "Email:"
        '
        'tmrSearchQ
        '
        Me.tmrSearchQ.Interval = 500
        '
        'tmrSendStatusEmail
        '
        Me.tmrSendStatusEmail.Enabled = True
        Me.tmrSendStatusEmail.Interval = 15000
        '
        'tmrSendProofStatusEmail
        '
        Me.tmrSendProofStatusEmail.Enabled = True
        Me.tmrSendProofStatusEmail.Interval = 15000
        '
        'DataErrorTimer
        '
        Me.DataErrorTimer.Interval = 500
        '
        'tmrSearchPersonalize
        '
        Me.tmrSearchPersonalize.Interval = 500
        '
        'LineUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Azure
        Me.ClientSize = New System.Drawing.Size(1316, 594)
        Me.Controls.Add(Me.SplitContainer8)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "LineUp"
        Me.Text = "LineUp"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.JobQMenuStrip.ResumeLayout(False)
        Me.PersonalizeJobQContextMenu.ResumeLayout(False)
        Me.Status.ResumeLayout(False)
        Me.Status.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer8.Panel1.ResumeLayout(False)
        Me.SplitContainer8.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer8.ResumeLayout(False)
        Me.LineUpTabCtrl.ResumeLayout(False)
        Me.TabProd.ResumeLayout(False)
        Me.SplitContainer10.Panel1.ResumeLayout(False)
        Me.SplitContainer10.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer10.ResumeLayout(False)
        Me.SplitContainer9.Panel1.ResumeLayout(False)
        Me.SplitContainer9.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer9.ResumeLayout(False)
        CType(Me.dgvJobQ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel2.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.tabPrint.ResumeLayout(False)
        Me.TabPersonalize.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer6.ResumeLayout(False)
        Me.SplitContainer7.Panel1.ResumeLayout(False)
        Me.SplitContainer7.Panel2.ResumeLayout(False)
        Me.SplitContainer7.Panel2.PerformLayout()
        CType(Me.SplitContainer7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer7.ResumeLayout(False)
        CType(Me.dgvPersonalizeJobQ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.pdfInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel3.ResumeLayout(False)
        Me.FlowLayoutPanel3.PerformLayout()
        Me.TabControlInfo.ResumeLayout(False)
        Me.TabProdInfo.ResumeLayout(False)
        Me.TabProdInfo.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.pbProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabOrderInfo.ResumeLayout(False)
        Me.TabOrderInfo.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents tabPrint As System.Windows.Forms.TabPage
    Friend WithEvents LineUpTabCtrl As System.Windows.Forms.TabControl
    Friend WithEvents TabProd As System.Windows.Forms.TabPage
    Friend WithEvents pbProducts As System.Windows.Forms.PictureBox
    Friend WithEvents Status As System.Windows.Forms.StatusStrip
    Friend WithEvents Status1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents JobQMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents menuMoveToArchive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdatedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TimerCheckForUpdate As System.Windows.Forms.Timer
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents FinalQuantityDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrderPlacedDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblSourcelbl As System.Windows.Forms.Label
    Friend WithEvents lblTypelbl As System.Windows.Forms.Label
    Friend WithEvents lblSalePricelbl As System.Windows.Forms.Label
    Friend WithEvents lblPageCtlbl As System.Windows.Forms.Label
    Friend WithEvents lblProductionCostlbl As System.Windows.Forms.Label
    Friend WithEvents txtProdNumPrint As System.Windows.Forms.TextBox
    Friend WithEvents lblItemNumlbl As System.Windows.Forms.Label
    Friend WithEvents lblInvTitlelbl As System.Windows.Forms.Label
    Friend WithEvents lblTitlelbl As System.Windows.Forms.Label
    Friend WithEvents txtQuantityPrint As System.Windows.Forms.TextBox
    Friend WithEvents lblQuantitylbl As System.Windows.Forms.Label
    Friend WithEvents btnLoadProd As System.Windows.Forms.Button
    Friend WithEvents lblAuthorlbl As System.Windows.Forms.Label
    Friend WithEvents btnAlignSheets As System.Windows.Forms.Button
    Friend WithEvents txtJobQSearch As System.Windows.Forms.TextBox
    Friend WithEvents TabPersonalize As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnDesignsRefresh As System.Windows.Forms.Button
    Friend WithEvents dgvPersonalizeJobQ As System.Windows.Forms.DataGridView
    Friend WithEvents txtPersonalizeSearch As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer7 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtDesignID As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtItemNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtShipAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtReceiptNum As System.Windows.Forms.TextBox
    Friend WithEvents txtBillingNameNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtPhoneNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdbArchive As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCurrent As System.Windows.Forms.RadioButton
    Friend WithEvents lblDesignsShowing As System.Windows.Forms.Label
    Friend WithEvents txtShipNote As System.Windows.Forms.TextBox
    Friend WithEvents rdbBodyPDF As System.Windows.Forms.RadioButton
    Friend WithEvents rdbInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents btnImpose As System.Windows.Forms.Button
    Friend WithEvents txtCatalog As System.Windows.Forms.RichTextBox
    Friend WithEvents SplitContainer8 As System.Windows.Forms.SplitContainer
    Friend WithEvents TmrUpdatePQ As System.Windows.Forms.Timer
    Friend WithEvents btnArchive As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents txtMxNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnPrintLabels As System.Windows.Forms.Button
    Friend WithEvents ToolStripBTP As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripVersion As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbStatusHistory As System.Windows.Forms.ComboBox
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents txtAuthor As System.Windows.Forms.TextBox
    Friend WithEvents txtInvTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtlTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtSalePrice As System.Windows.Forms.TextBox
    Friend WithEvents txtProductionCost As System.Windows.Forms.TextBox
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents txtPageCt As System.Windows.Forms.TextBox
    Friend WithEvents TabControlInfo As System.Windows.Forms.TabControl
    Friend WithEvents TabOrderInfo As System.Windows.Forms.TabPage
    Friend WithEvents TabProdInfo As System.Windows.Forms.TabPage
    Friend WithEvents lblCustEmail As System.Windows.Forms.LinkLabel
    Friend WithEvents tmrResetStatus As System.Windows.Forms.Timer
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateStatusLabelsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSearchProds As System.Windows.Forms.Button
    Friend WithEvents SplitContainer10 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer9 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnCustSearch As System.Windows.Forms.Button
    Friend WithEvents DeleteRowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PersonalizeJobQContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RefreshPageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendToArchiveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestoreIDsFromArchiveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintLabelsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImposeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetStatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImposedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Printing1200aToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Printing1200bToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DrilledToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilledToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShippedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents TractPreviewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateJobQBackupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateBarcodeImagesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateCustomProofGPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ForCbOnlyTSItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LogsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PurchasingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ComputerSoftwareKeysToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LineUpErrorLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents CreateBackupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents dgvJobQ As DataGridView
    Friend WithEvents ShowPrinterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CountsMenuItem As ToolStripMenuItem
    Friend WithEvents tmrPersonalizeDisplay As Timer
    Friend WithEvents GetGPEmailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents ShareWordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents HarvestShareWordFilesToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents FindFoldersWithMultipleFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BatchCreateProductionFilesToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents PrintShareWordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents rdbCovPDF As RadioButton
    Friend WithEvents HomePrintTractsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportGPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents ExportGPPrtABCToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ExportTo1200aToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportTo1200bToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyFileNamesToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents CopyFilesToDesktopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportGPPrtABCToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InvToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OutputProductsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportTo6120aToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FlowPrinterInfo As FlowLayoutPanel
    Friend WithEvents ImposeBookPamEtcToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetDefaultPrinterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SearchForProjectFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents rdbJobQCurrent As RadioButton
    Friend WithEvents rdbJobQHistory As RadioButton
    Friend WithEvents FinishedYearCountToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tmrSearchQ As Timer
    Friend WithEvents ShowJobDetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents btnAccounting As Button
    Friend WithEvents tmrSendStatusEmail As Timer
    Friend WithEvents EmailNotificationLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SendEmailReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents RepopulateFoldersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckProofRequestsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tmrSendProofStatusEmail As Timer
    Friend WithEvents ProductionFilesFlow As FlowLayoutPanel
    Friend WithEvents NoteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataErrorTimer As Timer
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents CopyFilesToDesktopToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ShowXMLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents pdfInvoice As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents FlowLayoutPanel3 As FlowLayoutPanel
    Friend WithEvents rdbLabels As RadioButton
    Friend WithEvents rdbAllOrders As RadioButton
    Friend WithEvents tmrSearchPersonalize As Timer
    Friend WithEvents ShowRowDetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyProductionFilesToDesktopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProductionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportFilesToPrintersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UpdateStatusOfJobsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents PrintJobTicketsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WebBrowserToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TestFindFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintTCFilesToolStripMenuItem As ToolStripMenuItem
End Class
