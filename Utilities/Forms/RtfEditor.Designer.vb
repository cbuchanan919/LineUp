<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RtfEditor
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RtfEditor))
        Me.gbRtfEditor = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblLastUpdated = New System.Windows.Forms.Label()
        Me.tbZoom = New System.Windows.Forms.TrackBar()
        Me.lblZoom = New System.Windows.Forms.Label()
        Me.rtbDoc = New ExtendedRichTextBox.RichTextBoxPrintCtrl()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tbrNew = New System.Windows.Forms.ToolStripButton()
        Me.tbrOpen = New System.Windows.Forms.ToolStripButton()
        Me.tbrSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tbrFont = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tbrLeft = New System.Windows.Forms.ToolStripButton()
        Me.tbrCenter = New System.Windows.Forms.ToolStripButton()
        Me.tbrRight = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tbrBold = New System.Windows.Forms.ToolStripButton()
        Me.tbrItalic = New System.Windows.Forms.ToolStripButton()
        Me.tbrUnderline = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.tbrFind = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPageSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRedo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindAndReplaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator()
        Me.InsertImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectFontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.FontColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.BoldToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItalicToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnderlineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NormalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.PageColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ParagraphToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IndentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuIndent0 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuIndent5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuIndent10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuIndent15 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuIndent20 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAlign = New System.Windows.Forms.ToolStripMenuItem()
        Me.LeftToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CenterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BulletsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddBulletsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveBulletsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.gbRtfEditor.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbRtfEditor
        '
        Me.gbRtfEditor.Controls.Add(Me.TableLayoutPanel1)
        Me.gbRtfEditor.Controls.Add(Me.ToolStrip1)
        Me.gbRtfEditor.Controls.Add(Me.MenuStrip2)
        Me.gbRtfEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbRtfEditor.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbRtfEditor.Location = New System.Drawing.Point(0, 0)
        Me.gbRtfEditor.Name = "gbRtfEditor"
        Me.gbRtfEditor.Size = New System.Drawing.Size(550, 259)
        Me.gbRtfEditor.TabIndex = 1
        Me.gbRtfEditor.TabStop = False
        Me.gbRtfEditor.Text = "RTF Editor"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.rtbDoc, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 68)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(544, 188)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.68421!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.68421!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.63158!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblLastUpdated, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.tbZoom, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblZoom, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(20, 168)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(504, 20)
        Me.TableLayoutPanel2.TabIndex = 11
        '
        'lblLastUpdated
        '
        Me.lblLastUpdated.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblLastUpdated.AutoSize = True
        Me.lblLastUpdated.BackColor = System.Drawing.Color.White
        Me.lblLastUpdated.Location = New System.Drawing.Point(419, 2)
        Me.lblLastUpdated.Name = "lblLastUpdated"
        Me.lblLastUpdated.Size = New System.Drawing.Size(82, 15)
        Me.lblLastUpdated.TabIndex = 9
        Me.lblLastUpdated.Text = "Last Updated: "
        '
        'tbZoom
        '
        Me.tbZoom.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.tbZoom.AutoSize = False
        Me.tbZoom.BackColor = System.Drawing.Color.White
        Me.tbZoom.Location = New System.Drawing.Point(34, 3)
        Me.tbZoom.Maximum = 500
        Me.tbZoom.Minimum = 25
        Me.tbZoom.Name = "tbZoom"
        Me.tbZoom.Size = New System.Drawing.Size(82, 14)
        Me.tbZoom.SmallChange = 10
        Me.tbZoom.TabIndex = 7
        Me.tbZoom.TickFrequency = 25
        Me.tbZoom.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbZoom.Value = 100
        '
        'lblZoom
        '
        Me.lblZoom.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblZoom.AutoSize = True
        Me.lblZoom.BackColor = System.Drawing.Color.White
        Me.lblZoom.Location = New System.Drawing.Point(122, 2)
        Me.lblZoom.Name = "lblZoom"
        Me.lblZoom.Size = New System.Drawing.Size(35, 15)
        Me.lblZoom.TabIndex = 8
        Me.lblZoom.Text = "100%"
        '
        'rtbDoc
        '
        Me.rtbDoc.AcceptsTab = True
        Me.rtbDoc.BackColor = System.Drawing.Color.White
        Me.rtbDoc.DetectUrls = False
        Me.rtbDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbDoc.Location = New System.Drawing.Point(23, 3)
        Me.rtbDoc.Name = "rtbDoc"
        Me.rtbDoc.Size = New System.Drawing.Size(498, 162)
        Me.rtbDoc.TabIndex = 6
        Me.rtbDoc.Text = ""
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.White
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbrNew, Me.tbrOpen, Me.tbrSave, Me.ToolStripSeparator3, Me.tbrFont, Me.ToolStripSeparator4, Me.tbrLeft, Me.tbrCenter, Me.tbrRight, Me.ToolStripSeparator7, Me.tbrBold, Me.tbrItalic, Me.tbrUnderline, Me.ToolStripSeparator8, Me.tbrFind, Me.ToolStripSeparator9})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 43)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(544, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tbrNew
        '
        Me.tbrNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrNew.Image = CType(resources.GetObject("tbrNew.Image"), System.Drawing.Image)
        Me.tbrNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrNew.Name = "tbrNew"
        Me.tbrNew.Size = New System.Drawing.Size(23, 22)
        Me.tbrNew.Text = "New"
        '
        'tbrOpen
        '
        Me.tbrOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrOpen.Image = CType(resources.GetObject("tbrOpen.Image"), System.Drawing.Image)
        Me.tbrOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrOpen.Name = "tbrOpen"
        Me.tbrOpen.Size = New System.Drawing.Size(23, 22)
        Me.tbrOpen.Text = "Open"
        '
        'tbrSave
        '
        Me.tbrSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrSave.Image = CType(resources.GetObject("tbrSave.Image"), System.Drawing.Image)
        Me.tbrSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrSave.Name = "tbrSave"
        Me.tbrSave.Size = New System.Drawing.Size(23, 22)
        Me.tbrSave.Text = "Save"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'tbrFont
        '
        Me.tbrFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrFont.Image = CType(resources.GetObject("tbrFont.Image"), System.Drawing.Image)
        Me.tbrFont.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrFont.Name = "tbrFont"
        Me.tbrFont.Size = New System.Drawing.Size(23, 22)
        Me.tbrFont.Text = "Font"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'tbrLeft
        '
        Me.tbrLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrLeft.Image = CType(resources.GetObject("tbrLeft.Image"), System.Drawing.Image)
        Me.tbrLeft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrLeft.Name = "tbrLeft"
        Me.tbrLeft.Size = New System.Drawing.Size(23, 22)
        Me.tbrLeft.Text = "Left"
        '
        'tbrCenter
        '
        Me.tbrCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrCenter.Image = CType(resources.GetObject("tbrCenter.Image"), System.Drawing.Image)
        Me.tbrCenter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrCenter.Name = "tbrCenter"
        Me.tbrCenter.Size = New System.Drawing.Size(23, 22)
        Me.tbrCenter.Text = "Center"
        '
        'tbrRight
        '
        Me.tbrRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrRight.Image = CType(resources.GetObject("tbrRight.Image"), System.Drawing.Image)
        Me.tbrRight.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrRight.Name = "tbrRight"
        Me.tbrRight.Size = New System.Drawing.Size(23, 22)
        Me.tbrRight.Text = "Right"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'tbrBold
        '
        Me.tbrBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrBold.Image = CType(resources.GetObject("tbrBold.Image"), System.Drawing.Image)
        Me.tbrBold.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrBold.Name = "tbrBold"
        Me.tbrBold.Size = New System.Drawing.Size(23, 22)
        Me.tbrBold.Text = "Bold"
        '
        'tbrItalic
        '
        Me.tbrItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrItalic.Image = CType(resources.GetObject("tbrItalic.Image"), System.Drawing.Image)
        Me.tbrItalic.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrItalic.Name = "tbrItalic"
        Me.tbrItalic.Size = New System.Drawing.Size(23, 22)
        Me.tbrItalic.Text = "Italic"
        '
        'tbrUnderline
        '
        Me.tbrUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrUnderline.Image = CType(resources.GetObject("tbrUnderline.Image"), System.Drawing.Image)
        Me.tbrUnderline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrUnderline.Name = "tbrUnderline"
        Me.tbrUnderline.Size = New System.Drawing.Size(23, 22)
        Me.tbrUnderline.Text = "Underline"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'tbrFind
        '
        Me.tbrFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbrFind.Image = CType(resources.GetObject("tbrFind.Image"), System.Drawing.Image)
        Me.tbrFind.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbrFind.Name = "tbrFind"
        Me.tbrFind.Size = New System.Drawing.Size(23, 22)
        Me.tbrFind.Text = "Find"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'MenuStrip2
        '
        Me.MenuStrip2.BackColor = System.Drawing.Color.White
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.EditToolStripMenuItem, Me.FontToolStripMenuItem, Me.ParagraphToolStripMenuItem, Me.BulletsToolStripMenuItem})
        Me.MenuStrip2.Location = New System.Drawing.Point(3, 19)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip2.Size = New System.Drawing.Size(544, 24)
        Me.MenuStrip2.TabIndex = 4
        Me.MenuStrip2.Text = "MenuStrip2"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.ToolStripSeparator1, Me.SaveToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripMenuItem2, Me.mnuPageSetup, Me.PreviewToolStripMenuItem, Me.PrintToolStripMenuItem, Me.ToolStripMenuItem3})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.ToolStripMenuItem1.Text = "&File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.NewToolStripMenuItem.Image = CType(resources.GetObject("NewToolStripMenuItem.Image"), System.Drawing.Image)
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.NewToolStripMenuItem.Text = "&New"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(139, 6)
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.SaveToolStripMenuItem.Image = CType(resources.GetObject("SaveToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.SaveToolStripMenuItem.Text = "&Save..."
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.OpenToolStripMenuItem.Image = CType(resources.GetObject("OpenToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.OpenToolStripMenuItem.Text = "&Open..."
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save &As..."
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(139, 6)
        '
        'mnuPageSetup
        '
        Me.mnuPageSetup.BackColor = System.Drawing.SystemColors.Control
        Me.mnuPageSetup.Name = "mnuPageSetup"
        Me.mnuPageSetup.Size = New System.Drawing.Size(142, 22)
        Me.mnuPageSetup.Text = "Page Setup..."
        '
        'PreviewToolStripMenuItem
        '
        Me.PreviewToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.PreviewToolStripMenuItem.Name = "PreviewToolStripMenuItem"
        Me.PreviewToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.PreviewToolStripMenuItem.Text = "Pre&view..."
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.PrintToolStripMenuItem.Image = CType(resources.GetObject("PrintToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.PrintToolStripMenuItem.Text = "&Print..."
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(139, 6)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUndo, Me.mnuRedo, Me.ToolStripSeparator6, Me.FindToolStripMenuItem, Me.FindAndReplaceToolStripMenuItem, Me.ToolStripSeparator2, Me.SelectAllToolStripMenuItem, Me.ToolStripMenuItem5, Me.CopyToolStripMenuItem, Me.CutToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ToolStripMenuItem8, Me.InsertImageToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        '
        'mnuUndo
        '
        Me.mnuUndo.BackColor = System.Drawing.SystemColors.Control
        Me.mnuUndo.Image = CType(resources.GetObject("mnuUndo.Image"), System.Drawing.Image)
        Me.mnuUndo.Name = "mnuUndo"
        Me.mnuUndo.Size = New System.Drawing.Size(173, 22)
        Me.mnuUndo.Text = "&Undo"
        '
        'mnuRedo
        '
        Me.mnuRedo.BackColor = System.Drawing.SystemColors.Control
        Me.mnuRedo.Image = CType(resources.GetObject("mnuRedo.Image"), System.Drawing.Image)
        Me.mnuRedo.Name = "mnuRedo"
        Me.mnuRedo.Size = New System.Drawing.Size(173, 22)
        Me.mnuRedo.Text = "&Redo"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(170, 6)
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.FindToolStripMenuItem.Image = CType(resources.GetObject("FindToolStripMenuItem.Image"), System.Drawing.Image)
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.FindToolStripMenuItem.Text = "Fi&nd..."
        '
        'FindAndReplaceToolStripMenuItem
        '
        Me.FindAndReplaceToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.FindAndReplaceToolStripMenuItem.Image = CType(resources.GetObject("FindAndReplaceToolStripMenuItem.Image"), System.Drawing.Image)
        Me.FindAndReplaceToolStripMenuItem.Name = "FindAndReplaceToolStripMenuItem"
        Me.FindAndReplaceToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.FindAndReplaceToolStripMenuItem.Text = "Find and &Replace..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(170, 6)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select &All"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(170, 6)
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.CopyToolStripMenuItem.Image = CType(resources.GetObject("CopyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.CopyToolStripMenuItem.Text = "&Copy"
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.CutToolStripMenuItem.Image = CType(resources.GetObject("CutToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.CutToolStripMenuItem.Text = "C&ut"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.PasteToolStripMenuItem.Image = CType(resources.GetObject("PasteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.PasteToolStripMenuItem.Text = "Pas&te"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(170, 6)
        '
        'InsertImageToolStripMenuItem
        '
        Me.InsertImageToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.InsertImageToolStripMenuItem.Name = "InsertImageToolStripMenuItem"
        Me.InsertImageToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.InsertImageToolStripMenuItem.Text = "Insert Image..."
        '
        'FontToolStripMenuItem
        '
        Me.FontToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectFontToolStripMenuItem, Me.ToolStripMenuItem6, Me.FontColorToolStripMenuItem, Me.ToolStripSeparator5, Me.BoldToolStripMenuItem, Me.ItalicToolStripMenuItem, Me.UnderlineToolStripMenuItem, Me.NormalToolStripMenuItem, Me.ToolStripMenuItem7, Me.PageColorToolStripMenuItem})
        Me.FontToolStripMenuItem.Name = "FontToolStripMenuItem"
        Me.FontToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.FontToolStripMenuItem.Text = "F&ont"
        '
        'SelectFontToolStripMenuItem
        '
        Me.SelectFontToolStripMenuItem.Image = CType(resources.GetObject("SelectFontToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SelectFontToolStripMenuItem.Name = "SelectFontToolStripMenuItem"
        Me.SelectFontToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.SelectFontToolStripMenuItem.Text = "Se&lect Font..."
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.BackColor = System.Drawing.Color.White
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(138, 6)
        '
        'FontColorToolStripMenuItem
        '
        Me.FontColorToolStripMenuItem.Name = "FontColorToolStripMenuItem"
        Me.FontColorToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.FontColorToolStripMenuItem.Text = "Font &Color..."
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(138, 6)
        '
        'BoldToolStripMenuItem
        '
        Me.BoldToolStripMenuItem.Image = CType(resources.GetObject("BoldToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BoldToolStripMenuItem.Name = "BoldToolStripMenuItem"
        Me.BoldToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.BoldToolStripMenuItem.Text = "&Bold"
        '
        'ItalicToolStripMenuItem
        '
        Me.ItalicToolStripMenuItem.Image = CType(resources.GetObject("ItalicToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ItalicToolStripMenuItem.Name = "ItalicToolStripMenuItem"
        Me.ItalicToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.ItalicToolStripMenuItem.Text = "&Italic"
        '
        'UnderlineToolStripMenuItem
        '
        Me.UnderlineToolStripMenuItem.Image = CType(resources.GetObject("UnderlineToolStripMenuItem.Image"), System.Drawing.Image)
        Me.UnderlineToolStripMenuItem.Name = "UnderlineToolStripMenuItem"
        Me.UnderlineToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.UnderlineToolStripMenuItem.Text = "&Underline"
        '
        'NormalToolStripMenuItem
        '
        Me.NormalToolStripMenuItem.Name = "NormalToolStripMenuItem"
        Me.NormalToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.NormalToolStripMenuItem.Text = "&Normal"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(138, 6)
        '
        'PageColorToolStripMenuItem
        '
        Me.PageColorToolStripMenuItem.Name = "PageColorToolStripMenuItem"
        Me.PageColorToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.PageColorToolStripMenuItem.Text = "&Page Color..."
        '
        'ParagraphToolStripMenuItem
        '
        Me.ParagraphToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IndentToolStripMenuItem, Me.mnuAlign})
        Me.ParagraphToolStripMenuItem.Name = "ParagraphToolStripMenuItem"
        Me.ParagraphToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.ParagraphToolStripMenuItem.Text = "P&aragraph"
        '
        'IndentToolStripMenuItem
        '
        Me.IndentToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.IndentToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuIndent0, Me.mnuIndent5, Me.mnuIndent10, Me.mnuIndent15, Me.mnuIndent20})
        Me.IndentToolStripMenuItem.Name = "IndentToolStripMenuItem"
        Me.IndentToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
        Me.IndentToolStripMenuItem.Text = "&Indent"
        '
        'mnuIndent0
        '
        Me.mnuIndent0.Name = "mnuIndent0"
        Me.mnuIndent0.Size = New System.Drawing.Size(105, 22)
        Me.mnuIndent0.Text = "None"
        '
        'mnuIndent5
        '
        Me.mnuIndent5.Name = "mnuIndent5"
        Me.mnuIndent5.Size = New System.Drawing.Size(105, 22)
        Me.mnuIndent5.Text = "5 pts"
        '
        'mnuIndent10
        '
        Me.mnuIndent10.Name = "mnuIndent10"
        Me.mnuIndent10.Size = New System.Drawing.Size(105, 22)
        Me.mnuIndent10.Text = "10 pts"
        '
        'mnuIndent15
        '
        Me.mnuIndent15.Name = "mnuIndent15"
        Me.mnuIndent15.Size = New System.Drawing.Size(105, 22)
        Me.mnuIndent15.Text = "15 pts"
        '
        'mnuIndent20
        '
        Me.mnuIndent20.Name = "mnuIndent20"
        Me.mnuIndent20.Size = New System.Drawing.Size(105, 22)
        Me.mnuIndent20.Text = "20 pts"
        '
        'mnuAlign
        '
        Me.mnuAlign.BackColor = System.Drawing.SystemColors.Control
        Me.mnuAlign.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LeftToolStripMenuItem, Me.CenterToolStripMenuItem, Me.RightToolStripMenuItem})
        Me.mnuAlign.Name = "mnuAlign"
        Me.mnuAlign.Size = New System.Drawing.Size(108, 22)
        Me.mnuAlign.Text = "&Align"
        '
        'LeftToolStripMenuItem
        '
        Me.LeftToolStripMenuItem.Image = CType(resources.GetObject("LeftToolStripMenuItem.Image"), System.Drawing.Image)
        Me.LeftToolStripMenuItem.Name = "LeftToolStripMenuItem"
        Me.LeftToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.LeftToolStripMenuItem.Text = "Left"
        '
        'CenterToolStripMenuItem
        '
        Me.CenterToolStripMenuItem.Image = CType(resources.GetObject("CenterToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CenterToolStripMenuItem.Name = "CenterToolStripMenuItem"
        Me.CenterToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.CenterToolStripMenuItem.Text = "Center"
        '
        'RightToolStripMenuItem
        '
        Me.RightToolStripMenuItem.Image = CType(resources.GetObject("RightToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RightToolStripMenuItem.Name = "RightToolStripMenuItem"
        Me.RightToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.RightToolStripMenuItem.Text = "Right"
        '
        'BulletsToolStripMenuItem
        '
        Me.BulletsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddBulletsToolStripMenuItem, Me.RemoveBulletsToolStripMenuItem})
        Me.BulletsToolStripMenuItem.Name = "BulletsToolStripMenuItem"
        Me.BulletsToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.BulletsToolStripMenuItem.Text = "&Bullets"
        '
        'AddBulletsToolStripMenuItem
        '
        Me.AddBulletsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.AddBulletsToolStripMenuItem.Name = "AddBulletsToolStripMenuItem"
        Me.AddBulletsToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.AddBulletsToolStripMenuItem.Text = "A&dd Bullets"
        '
        'RemoveBulletsToolStripMenuItem
        '
        Me.RemoveBulletsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.RemoveBulletsToolStripMenuItem.Name = "RemoveBulletsToolStripMenuItem"
        Me.RemoveBulletsToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.RemoveBulletsToolStripMenuItem.Text = "&Remove Bullets"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.MinMargins = New System.Drawing.Printing.Margins(1, 1, 1, 1)
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'RtfEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gbRtfEditor)
        Me.Name = "RtfEditor"
        Me.Size = New System.Drawing.Size(550, 259)
        Me.gbRtfEditor.ResumeLayout(False)
        Me.gbRtfEditor.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents gbRtfEditor As System.Windows.Forms.GroupBox
    Friend WithEvents lblZoom As System.Windows.Forms.Label
    Friend WithEvents tbZoom As System.Windows.Forms.TrackBar
    Friend WithEvents rtbDoc As ExtendedRichTextBox.RichTextBoxPrintCtrl
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tbrNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbrFont As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbrLeft As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrCenter As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrRight As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbrBold As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrItalic As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbrUnderline As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbrFind As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuStrip2 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPageSetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRedo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FindToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindAndReplaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents InsertImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectFontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FontColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BoldToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItalicToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnderlineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NormalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ParagraphToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent0 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndent20 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAlign As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LeftToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CenterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BulletsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddBulletsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveBulletsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents lblLastUpdated As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

End Class
