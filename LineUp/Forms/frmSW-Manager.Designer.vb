<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSW_Manager
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSW_Manager))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lvResults = New System.Windows.Forms.ListView()
        Me.colTitle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colItemNum = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnAddFile = New System.Windows.Forms.Button()
        Me.btnCreatePDF = New System.Windows.Forms.Button()
        Me.btnJobTicket = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddShareWordFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteShareWordFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateProductionPDFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyFolderNameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.UtilitiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FixFolderNamesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lvFiles = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDateMod = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colFiller = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtShareWordInfo = New System.Windows.Forms.TextBox()
        Me.tmrSearch = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(984, 465)
        Me.SplitContainer1.SplitterDistance = 288
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer2.Size = New System.Drawing.Size(288, 465)
        Me.SplitContainer2.SplitterDistance = 51
        Me.SplitContainer2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(288, 51)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search:"
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(3, 21)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(282, 25)
        Me.txtSearch.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.AutoSize = True
        Me.GroupBox2.Controls.Add(Me.lvResults)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.MinimumSize = New System.Drawing.Size(237, 307)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(288, 410)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Results:"
        '
        'lvResults
        '
        Me.lvResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTitle, Me.colItemNum})
        Me.lvResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvResults.HideSelection = False
        Me.lvResults.Location = New System.Drawing.Point(3, 21)
        Me.lvResults.MultiSelect = False
        Me.lvResults.Name = "lvResults"
        Me.lvResults.Size = New System.Drawing.Size(282, 386)
        Me.lvResults.TabIndex = 0
        Me.lvResults.TileSize = New System.Drawing.Size(250, 60)
        Me.lvResults.UseCompatibleStateImageBehavior = False
        Me.lvResults.View = System.Windows.Forms.View.Tile
        '
        'colTitle
        '
        Me.colTitle.Text = "Title"
        Me.colTitle.Width = 250
        '
        'colItemNum
        '
        Me.colItemNum.Text = "Item Number"
        Me.colItemNum.Width = 120
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox4, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.24731!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.75269!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(692, 465)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.btnAddFile)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCreatePDF)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnJobTicket)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label1)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnClose)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(580, 138)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(3)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(109, 324)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'btnAddFile
        '
        Me.btnAddFile.Location = New System.Drawing.Point(6, 6)
        Me.btnAddFile.MinimumSize = New System.Drawing.Size(100, 65)
        Me.btnAddFile.Name = "btnAddFile"
        Me.btnAddFile.Size = New System.Drawing.Size(100, 65)
        Me.btnAddFile.TabIndex = 0
        Me.btnAddFile.Text = "Add ShareWord File"
        Me.btnAddFile.UseVisualStyleBackColor = True
        '
        'btnCreatePDF
        '
        Me.btnCreatePDF.Location = New System.Drawing.Point(6, 77)
        Me.btnCreatePDF.MinimumSize = New System.Drawing.Size(100, 65)
        Me.btnCreatePDF.Name = "btnCreatePDF"
        Me.btnCreatePDF.Size = New System.Drawing.Size(100, 65)
        Me.btnCreatePDF.TabIndex = 0
        Me.btnCreatePDF.Text = "Create Production PDF"
        Me.btnCreatePDF.UseVisualStyleBackColor = True
        '
        'btnJobTicket
        '
        Me.btnJobTicket.Location = New System.Drawing.Point(6, 148)
        Me.btnJobTicket.MinimumSize = New System.Drawing.Size(100, 65)
        Me.btnJobTicket.Name = "btnJobTicket"
        Me.btnJobTicket.Size = New System.Drawing.Size(100, 65)
        Me.btnJobTicket.TabIndex = 1
        Me.btnJobTicket.Text = "Create Job Ticket"
        Me.btnJobTicket.UseVisualStyleBackColor = True
        Me.btnJobTicket.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 216)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 30)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = " "
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(6, 249)
        Me.btnClose.MinimumSize = New System.Drawing.Size(100, 65)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 65)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Manager"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox3.Controls.Add(Me.lvFiles)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(3, 138)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(571, 324)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Project Files:"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddShareWordFileToolStripMenuItem, Me.DeleteShareWordFileToolStripMenuItem, Me.CreateProductionPDFToolStripMenuItem, Me.ToolStripSeparator1, Me.CopyFolderNameToolStripMenuItem, Me.ToolStripSeparator2, Me.UtilitiesToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(243, 126)
        '
        'AddShareWordFileToolStripMenuItem
        '
        Me.AddShareWordFileToolStripMenuItem.Name = "AddShareWordFileToolStripMenuItem"
        Me.AddShareWordFileToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.AddShareWordFileToolStripMenuItem.Text = "Add ShareWord File"
        '
        'DeleteShareWordFileToolStripMenuItem
        '
        Me.DeleteShareWordFileToolStripMenuItem.Name = "DeleteShareWordFileToolStripMenuItem"
        Me.DeleteShareWordFileToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.DeleteShareWordFileToolStripMenuItem.Text = "Delete ShareWord File"
        '
        'CreateProductionPDFToolStripMenuItem
        '
        Me.CreateProductionPDFToolStripMenuItem.Name = "CreateProductionPDFToolStripMenuItem"
        Me.CreateProductionPDFToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.CreateProductionPDFToolStripMenuItem.Text = "Create Production PDF"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(239, 6)
        '
        'CopyFolderNameToolStripMenuItem
        '
        Me.CopyFolderNameToolStripMenuItem.Name = "CopyFolderNameToolStripMenuItem"
        Me.CopyFolderNameToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.CopyFolderNameToolStripMenuItem.Text = "Copy Folder Name to Clipboard"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(239, 6)
        '
        'UtilitiesToolStripMenuItem
        '
        Me.UtilitiesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FixFolderNamesToolStripMenuItem})
        Me.UtilitiesToolStripMenuItem.Name = "UtilitiesToolStripMenuItem"
        Me.UtilitiesToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.UtilitiesToolStripMenuItem.Text = "Utilities"
        '
        'FixFolderNamesToolStripMenuItem
        '
        Me.FixFolderNamesToolStripMenuItem.Name = "FixFolderNamesToolStripMenuItem"
        Me.FixFolderNamesToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.FixFolderNamesToolStripMenuItem.Text = "Fix Folder Names"
        '
        'lvFiles
        '
        Me.lvFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colDateMod, Me.colSize, Me.colFiller})
        Me.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFiles.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvFiles.FullRowSelect = True
        Me.lvFiles.HideSelection = False
        Me.lvFiles.Location = New System.Drawing.Point(3, 21)
        Me.lvFiles.MultiSelect = False
        Me.lvFiles.Name = "lvFiles"
        Me.lvFiles.Size = New System.Drawing.Size(565, 300)
        Me.lvFiles.TabIndex = 0
        Me.lvFiles.TileSize = New System.Drawing.Size(400, 80)
        Me.lvFiles.UseCompatibleStateImageBehavior = False
        Me.lvFiles.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "File Name"
        Me.colName.Width = 224
        '
        'colDateMod
        '
        Me.colDateMod.Text = "Date Modified"
        Me.colDateMod.Width = 190
        '
        'colSize
        '
        Me.colSize.Text = "Size"
        Me.colSize.Width = 57
        '
        'colFiller
        '
        Me.colFiller.Text = ""
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtShareWordInfo)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(571, 129)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Share Word Info:"
        '
        'txtShareWordInfo
        '
        Me.txtShareWordInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtShareWordInfo.Location = New System.Drawing.Point(3, 21)
        Me.txtShareWordInfo.Multiline = True
        Me.txtShareWordInfo.Name = "txtShareWordInfo"
        Me.txtShareWordInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtShareWordInfo.Size = New System.Drawing.Size(565, 105)
        Me.txtShareWordInfo.TabIndex = 0
        Me.txtShareWordInfo.WordWrap = False
        '
        'tmrSearch
        '
        Me.tmrSearch.Interval = 500
        '
        'frmSW_Manager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(984, 465)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmSW_Manager"
        Me.Text = "Share Word Manager"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lvResults As ListView
    Friend WithEvents colTitle As ColumnHeader
    Friend WithEvents colItemNum As ColumnHeader
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents lvFiles As ListView
    Friend WithEvents colName As ColumnHeader
    Friend WithEvents colDateMod As ColumnHeader
    Friend WithEvents colSize As ColumnHeader
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents txtShareWordInfo As TextBox
    Friend WithEvents btnCreatePDF As Button
    Friend WithEvents btnAddFile As Button
    Friend WithEvents colFiller As ColumnHeader
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AddShareWordFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteShareWordFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateProductionPDFToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnJobTicket As Button
    Friend WithEvents CopyFolderNameToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents UtilitiesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FixFolderNamesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents tmrSearch As Timer
End Class
