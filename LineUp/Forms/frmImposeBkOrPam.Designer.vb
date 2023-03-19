<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImposeBkOrPam
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImposeBkOrPam))
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtImposeSearch = New System.Windows.Forms.TextBox()
        Me.btnSearchPrntEbk = New System.Windows.Forms.Button()
        Me.ListOpenPrePrintFolder = New System.Windows.Forms.ListBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.WidthInch = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RotLabel = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PagesLabel = New System.Windows.Forms.Label()
        Me.txtSavePDFas = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.FlowLayoutPanel5 = New System.Windows.Forms.FlowLayoutPanel()
        Me.rdbMiniPam = New System.Windows.Forms.RadioButton()
        Me.rdbCustomSize = New System.Windows.Forms.RadioButton()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.FlowLayoutPanel4 = New System.Windows.Forms.FlowLayoutPanel()
        Me.rdb12x18MiniBook = New System.Windows.Forms.RadioButton()
        Me.rdbBooklet = New System.Windows.Forms.RadioButton()
        Me.rdbCDC_XL = New System.Windows.Forms.RadioButton()
        Me.rdbCDC = New System.Windows.Forms.RadioButton()
        Me.rdbYCC = New System.Windows.Forms.RadioButton()
        Me.rdbBGC = New System.Windows.Forms.RadioButton()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.rdbFourUp = New System.Windows.Forms.RadioButton()
        Me.rdbFullBleed = New System.Windows.Forms.RadioButton()
        Me.rdbQuarterNSideBySide = New System.Windows.Forms.RadioButton()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnImposeBkCov = New System.Windows.Forms.Button()
        Me.btnImposePamCov = New System.Windows.Forms.Button()
        Me.btnSelectPDF = New System.Windows.Forms.Button()
        Me.btnPDFImposeSave = New System.Windows.Forms.Button()
        Me.txtSelectPDF = New System.Windows.Forms.TextBox()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Status1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.FlowLayoutPanel5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.FlowLayoutPanel4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.FlowLayoutPanel3.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox7)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox9)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1234, 530)
        Me.FlowLayoutPanel1.TabIndex = 2
        Me.FlowLayoutPanel1.WrapContents = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.TableLayoutPanel13)
        Me.GroupBox7.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(1209, 229)
        Me.GroupBox7.TabIndex = 2
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Search For Project Folder:"
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 1
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.TableLayoutPanel12, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.ListOpenPrePrintFolder, 0, 1)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(3, 19)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 2
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(1203, 207)
        Me.TableLayoutPanel13.TabIndex = 35
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 2
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.42345!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.57655!))
        Me.TableLayoutPanel12.Controls.Add(Me.txtImposeSearch, 1, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.btnSearchPrntEbk, 0, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 1
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(1197, 34)
        Me.TableLayoutPanel12.TabIndex = 0
        '
        'txtImposeSearch
        '
        Me.txtImposeSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtImposeSearch.Location = New System.Drawing.Point(127, 3)
        Me.txtImposeSearch.Name = "txtImposeSearch"
        Me.txtImposeSearch.Size = New System.Drawing.Size(1067, 23)
        Me.txtImposeSearch.TabIndex = 35
        '
        'btnSearchPrntEbk
        '
        Me.btnSearchPrntEbk.AutoSize = True
        Me.btnSearchPrntEbk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSearchPrntEbk.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnSearchPrntEbk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSearchPrntEbk.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnSearchPrntEbk.FlatAppearance.BorderSize = 0
        Me.btnSearchPrntEbk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnSearchPrntEbk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchPrntEbk.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSearchPrntEbk.ForeColor = System.Drawing.Color.White
        Me.btnSearchPrntEbk.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSearchPrntEbk.Location = New System.Drawing.Point(4, 4)
        Me.btnSearchPrntEbk.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearchPrntEbk.Name = "btnSearchPrntEbk"
        Me.btnSearchPrntEbk.Size = New System.Drawing.Size(116, 26)
        Me.btnSearchPrntEbk.TabIndex = 33
        Me.btnSearchPrntEbk.Text = "Search"
        Me.btnSearchPrntEbk.UseVisualStyleBackColor = False
        '
        'ListOpenPrePrintFolder
        '
        Me.ListOpenPrePrintFolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListOpenPrePrintFolder.FormattingEnabled = True
        Me.ListOpenPrePrintFolder.ItemHeight = 15
        Me.ListOpenPrePrintFolder.Location = New System.Drawing.Point(3, 43)
        Me.ListOpenPrePrintFolder.MinimumSize = New System.Drawing.Size(100, 100)
        Me.ListOpenPrePrintFolder.Name = "ListOpenPrePrintFolder"
        Me.ListOpenPrePrintFolder.Size = New System.Drawing.Size(1197, 161)
        Me.ListOpenPrePrintFolder.TabIndex = 34
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.GroupBox10)
        Me.GroupBox9.Controls.Add(Me.txtSavePDFas)
        Me.GroupBox9.Controls.Add(Me.GroupBox2)
        Me.GroupBox9.Controls.Add(Me.FlowLayoutPanel2)
        Me.GroupBox9.Controls.Add(Me.btnSelectPDF)
        Me.GroupBox9.Controls.Add(Me.btnPDFImposeSave)
        Me.GroupBox9.Controls.Add(Me.txtSelectPDF)
        Me.GroupBox9.Controls.Add(Me.SplitContainer5)
        Me.GroupBox9.Location = New System.Drawing.Point(3, 238)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(1209, 248)
        Me.GroupBox9.TabIndex = 41
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "PDF Imposition"
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox10.Location = New System.Drawing.Point(186, 58)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(202, 109)
        Me.GroupBox10.TabIndex = 56
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Selected PDF Info"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label6, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.WidthInch, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.RotLabel, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label5, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.PagesLabel, 1, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 19)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(196, 87)
        Me.TableLayoutPanel3.TabIndex = 55
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label6.AutoSize = True
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(42, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 15)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Size:"
        '
        'WidthInch
        '
        Me.WidthInch.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.WidthInch.AutoSize = True
        Me.WidthInch.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.WidthInch.Location = New System.Drawing.Point(78, 7)
        Me.WidthInch.Name = "WidthInch"
        Me.WidthInch.Size = New System.Drawing.Size(16, 15)
        Me.WidthInch.TabIndex = 45
        Me.WidthInch.Text = "..."
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(17, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Rotation:"
        '
        'RotLabel
        '
        Me.RotLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RotLabel.AutoSize = True
        Me.RotLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.RotLabel.Location = New System.Drawing.Point(78, 36)
        Me.RotLabel.Name = "RotLabel"
        Me.RotLabel.Size = New System.Drawing.Size(16, 15)
        Me.RotLabel.TabIndex = 50
        Me.RotLabel.Text = "..."
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label5.AutoSize = True
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(31, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 15)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Pages:"
        '
        'PagesLabel
        '
        Me.PagesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.PagesLabel.AutoSize = True
        Me.PagesLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PagesLabel.Location = New System.Drawing.Point(78, 65)
        Me.PagesLabel.Name = "PagesLabel"
        Me.PagesLabel.Size = New System.Drawing.Size(16, 15)
        Me.PagesLabel.TabIndex = 46
        Me.PagesLabel.Text = "..."
        '
        'txtSavePDFas
        '
        Me.txtSavePDFas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSavePDFas.Location = New System.Drawing.Point(677, 206)
        Me.txtSavePDFas.Name = "txtSavePDFas"
        Me.txtSavePDFas.Size = New System.Drawing.Size(523, 23)
        Me.txtSavePDFas.TabIndex = 43
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.TableLayoutPanel11)
        Me.GroupBox2.Location = New System.Drawing.Point(575, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox2.Size = New System.Drawing.Size(624, 188)
        Me.GroupBox2.TabIndex = 52
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Impose Body as:"
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 3
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.Controls.Add(Me.GroupBox8, 2, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.GroupBox6, 1, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.GroupBox5, 0, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(1, 17)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(622, 170)
        Me.TableLayoutPanel11.TabIndex = 57
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.FlowLayoutPanel5)
        Me.GroupBox8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox8.Location = New System.Drawing.Point(415, 1)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox8.Size = New System.Drawing.Size(206, 168)
        Me.GroupBox8.TabIndex = 56
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Misc. Sheet Size"
        '
        'FlowLayoutPanel5
        '
        Me.FlowLayoutPanel5.AutoScroll = True
        Me.FlowLayoutPanel5.Controls.Add(Me.rdbMiniPam)
        Me.FlowLayoutPanel5.Controls.Add(Me.rdbCustomSize)
        Me.FlowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel5.Location = New System.Drawing.Point(1, 17)
        Me.FlowLayoutPanel5.Name = "FlowLayoutPanel5"
        Me.FlowLayoutPanel5.Size = New System.Drawing.Size(204, 150)
        Me.FlowLayoutPanel5.TabIndex = 0
        '
        'rdbMiniPam
        '
        Me.rdbMiniPam.AutoSize = True
        Me.rdbMiniPam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbMiniPam.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbMiniPam.Location = New System.Drawing.Point(1, 1)
        Me.rdbMiniPam.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbMiniPam.Name = "rdbMiniPam"
        Me.rdbMiniPam.Size = New System.Drawing.Size(149, 34)
        Me.rdbMiniPam.TabIndex = 2
        Me.rdbMiniPam.Text = "8.5x11 Sheet" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "5.5x3.75 Mini-Pamphlet"
        Me.rdbMiniPam.UseVisualStyleBackColor = True
        '
        'rdbCustomSize
        '
        Me.rdbCustomSize.AutoSize = True
        Me.rdbCustomSize.Location = New System.Drawing.Point(1, 37)
        Me.rdbCustomSize.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbCustomSize.Name = "rdbCustomSize"
        Me.rdbCustomSize.Size = New System.Drawing.Size(154, 19)
        Me.rdbCustomSize.TabIndex = 40
        Me.rdbCustomSize.TabStop = True
        Me.rdbCustomSize.Text = "4UP - Custom Sheet Size" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.rdbCustomSize.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.FlowLayoutPanel4)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Location = New System.Drawing.Point(208, 1)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox6.Size = New System.Drawing.Size(205, 168)
        Me.GroupBox6.TabIndex = 55
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "12x18 Sheet Size"
        '
        'FlowLayoutPanel4
        '
        Me.FlowLayoutPanel4.Controls.Add(Me.rdb12x18MiniBook)
        Me.FlowLayoutPanel4.Controls.Add(Me.rdbBooklet)
        Me.FlowLayoutPanel4.Controls.Add(Me.rdbCDC_XL)
        Me.FlowLayoutPanel4.Controls.Add(Me.rdbCDC)
        Me.FlowLayoutPanel4.Controls.Add(Me.rdbYCC)
        Me.FlowLayoutPanel4.Controls.Add(Me.rdbBGC)
        Me.FlowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel4.Location = New System.Drawing.Point(1, 17)
        Me.FlowLayoutPanel4.Name = "FlowLayoutPanel4"
        Me.FlowLayoutPanel4.Size = New System.Drawing.Size(203, 150)
        Me.FlowLayoutPanel4.TabIndex = 0
        '
        'rdb12x18MiniBook
        '
        Me.rdb12x18MiniBook.AutoSize = True
        Me.rdb12x18MiniBook.Location = New System.Drawing.Point(1, 1)
        Me.rdb12x18MiniBook.Margin = New System.Windows.Forms.Padding(1)
        Me.rdb12x18MiniBook.Name = "rdb12x18MiniBook"
        Me.rdb12x18MiniBook.Size = New System.Drawing.Size(171, 19)
        Me.rdb12x18MiniBook.TabIndex = 39
        Me.rdb12x18MiniBook.TabStop = True
        Me.rdb12x18MiniBook.Text = "3.75x5.5 Booklet (Economy)"
        Me.rdb12x18MiniBook.UseVisualStyleBackColor = True
        '
        'rdbBooklet
        '
        Me.rdbBooklet.AutoSize = True
        Me.rdbBooklet.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbBooklet.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbBooklet.Location = New System.Drawing.Point(1, 22)
        Me.rdbBooklet.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbBooklet.Name = "rdbBooklet"
        Me.rdbBooklet.Size = New System.Drawing.Size(124, 19)
        Me.rdbBooklet.TabIndex = 38
        Me.rdbBooklet.TabStop = True
        Me.rdbBooklet.Text = "4x5.5 Booklet (Gift)"
        Me.rdbBooklet.UseVisualStyleBackColor = True
        '
        'rdbCDC_XL
        '
        Me.rdbCDC_XL.AutoSize = True
        Me.rdbCDC_XL.Location = New System.Drawing.Point(3, 45)
        Me.rdbCDC_XL.Name = "rdbCDC_XL"
        Me.rdbCDC_XL.Size = New System.Drawing.Size(65, 19)
        Me.rdbCDC_XL.TabIndex = 40
        Me.rdbCDC_XL.TabStop = True
        Me.rdbCDC_XL.Text = "CDC XL"
        Me.rdbCDC_XL.UseVisualStyleBackColor = True
        '
        'rdbCDC
        '
        Me.rdbCDC.AutoSize = True
        Me.rdbCDC.Location = New System.Drawing.Point(3, 70)
        Me.rdbCDC.Name = "rdbCDC"
        Me.rdbCDC.Size = New System.Drawing.Size(49, 19)
        Me.rdbCDC.TabIndex = 41
        Me.rdbCDC.TabStop = True
        Me.rdbCDC.Text = "CDC"
        Me.rdbCDC.UseVisualStyleBackColor = True
        '
        'rdbYCC
        '
        Me.rdbYCC.AutoSize = True
        Me.rdbYCC.Location = New System.Drawing.Point(3, 95)
        Me.rdbYCC.Name = "rdbYCC"
        Me.rdbYCC.Size = New System.Drawing.Size(48, 19)
        Me.rdbYCC.TabIndex = 41
        Me.rdbYCC.TabStop = True
        Me.rdbYCC.Text = "YCC"
        Me.rdbYCC.UseVisualStyleBackColor = True
        '
        'rdbBGC
        '
        Me.rdbBGC.AutoSize = True
        Me.rdbBGC.Location = New System.Drawing.Point(3, 120)
        Me.rdbBGC.Name = "rdbBGC"
        Me.rdbBGC.Size = New System.Drawing.Size(48, 19)
        Me.rdbBGC.TabIndex = 42
        Me.rdbBGC.TabStop = True
        Me.rdbBGC.Text = "BGC"
        Me.rdbBGC.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.FlowLayoutPanel3)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(1, 1)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox5.Size = New System.Drawing.Size(205, 168)
        Me.GroupBox5.TabIndex = 55
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "11x17 Sheet Size"
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.AutoScroll = True
        Me.FlowLayoutPanel3.Controls.Add(Me.rdbFourUp)
        Me.FlowLayoutPanel3.Controls.Add(Me.rdbFullBleed)
        Me.FlowLayoutPanel3.Controls.Add(Me.rdbQuarterNSideBySide)
        Me.FlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(1, 17)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(203, 150)
        Me.FlowLayoutPanel3.TabIndex = 0
        '
        'rdbFourUp
        '
        Me.rdbFourUp.AutoSize = True
        Me.rdbFourUp.Checked = True
        Me.rdbFourUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbFourUp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbFourUp.Location = New System.Drawing.Point(1, 1)
        Me.rdbFourUp.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbFourUp.Name = "rdbFourUp"
        Me.rdbFourUp.Size = New System.Drawing.Size(102, 19)
        Me.rdbFourUp.TabIndex = 1
        Me.rdbFourUp.TabStop = True
        Me.rdbFourUp.Text = "5x8 Book Body"
        Me.rdbFourUp.UseVisualStyleBackColor = True
        '
        'rdbFullBleed
        '
        Me.rdbFullBleed.AutoSize = True
        Me.rdbFullBleed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbFullBleed.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbFullBleed.Location = New System.Drawing.Point(1, 22)
        Me.rdbFullBleed.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbFullBleed.Name = "rdbFullBleed"
        Me.rdbFullBleed.Size = New System.Drawing.Size(156, 19)
        Me.rdbFullBleed.TabIndex = 36
        Me.rdbFullBleed.Text = "5x8 Full Bleed Book Body"
        Me.rdbFullBleed.UseVisualStyleBackColor = True
        '
        'rdbQuarterNSideBySide
        '
        Me.rdbQuarterNSideBySide.AutoSize = True
        Me.rdbQuarterNSideBySide.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdbQuarterNSideBySide.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.rdbQuarterNSideBySide.Location = New System.Drawing.Point(1, 43)
        Me.rdbQuarterNSideBySide.Margin = New System.Windows.Forms.Padding(1)
        Me.rdbQuarterNSideBySide.Name = "rdbQuarterNSideBySide"
        Me.rdbQuarterNSideBySide.Size = New System.Drawing.Size(155, 19)
        Me.rdbQuarterNSideBySide.TabIndex = 37
        Me.rdbQuarterNSideBySide.Text = "5x8 Cheap 1-Off Printing"
        Me.rdbQuarterNSideBySide.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.btnImposeBkCov)
        Me.FlowLayoutPanel2.Controls.Add(Me.btnImposePamCov)
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(7, 79)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(173, 103)
        Me.FlowLayoutPanel2.TabIndex = 54
        '
        'btnImposeBkCov
        '
        Me.btnImposeBkCov.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImposeBkCov.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnImposeBkCov.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnImposeBkCov.FlatAppearance.BorderSize = 0
        Me.btnImposeBkCov.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnImposeBkCov.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImposeBkCov.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnImposeBkCov.ForeColor = System.Drawing.Color.White
        Me.btnImposeBkCov.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImposeBkCov.Location = New System.Drawing.Point(3, 3)
        Me.btnImposeBkCov.Name = "btnImposeBkCov"
        Me.btnImposeBkCov.Size = New System.Drawing.Size(137, 27)
        Me.btnImposeBkCov.TabIndex = 40
        Me.btnImposeBkCov.Text = "Impose Book Cover"
        Me.btnImposeBkCov.UseVisualStyleBackColor = False
        Me.btnImposeBkCov.Visible = False
        '
        'btnImposePamCov
        '
        Me.btnImposePamCov.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImposePamCov.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnImposePamCov.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnImposePamCov.FlatAppearance.BorderSize = 0
        Me.btnImposePamCov.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnImposePamCov.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImposePamCov.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnImposePamCov.ForeColor = System.Drawing.Color.White
        Me.btnImposePamCov.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnImposePamCov.Location = New System.Drawing.Point(3, 36)
        Me.btnImposePamCov.Name = "btnImposePamCov"
        Me.btnImposePamCov.Size = New System.Drawing.Size(164, 27)
        Me.btnImposePamCov.TabIndex = 41
        Me.btnImposePamCov.Text = "Impose Pamphlet Cover"
        Me.btnImposePamCov.UseVisualStyleBackColor = False
        Me.btnImposePamCov.Visible = False
        '
        'btnSelectPDF
        '
        Me.btnSelectPDF.AutoSize = True
        Me.btnSelectPDF.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSelectPDF.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnSelectPDF.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnSelectPDF.FlatAppearance.BorderSize = 0
        Me.btnSelectPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnSelectPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectPDF.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSelectPDF.ForeColor = System.Drawing.Color.White
        Me.btnSelectPDF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSelectPDF.Location = New System.Drawing.Point(19, 26)
        Me.btnSelectPDF.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSelectPDF.Name = "btnSelectPDF"
        Me.btnSelectPDF.Size = New System.Drawing.Size(94, 27)
        Me.btnSelectPDF.TabIndex = 51
        Me.btnSelectPDF.Text = "Select PDF ..."
        Me.btnSelectPDF.UseVisualStyleBackColor = False
        '
        'btnPDFImposeSave
        '
        Me.btnPDFImposeSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPDFImposeSave.AutoSize = True
        Me.btnPDFImposeSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnPDFImposeSave.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPDFImposeSave.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnPDFImposeSave.FlatAppearance.BorderSize = 0
        Me.btnPDFImposeSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnPDFImposeSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPDFImposeSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnPDFImposeSave.ForeColor = System.Drawing.Color.White
        Me.btnPDFImposeSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnPDFImposeSave.Location = New System.Drawing.Point(578, 205)
        Me.btnPDFImposeSave.Name = "btnPDFImposeSave"
        Me.btnPDFImposeSave.Size = New System.Drawing.Size(93, 27)
        Me.btnPDFImposeSave.TabIndex = 0
        Me.btnPDFImposeSave.Text = "Save PDF As"
        Me.btnPDFImposeSave.UseVisualStyleBackColor = False
        '
        'txtSelectPDF
        '
        Me.txtSelectPDF.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelectPDF.Location = New System.Drawing.Point(116, 27)
        Me.txtSelectPDF.Name = "txtSelectPDF"
        Me.txtSelectPDF.Size = New System.Drawing.Size(456, 23)
        Me.txtSelectPDF.TabIndex = 44
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Location = New System.Drawing.Point(394, 65)
        Me.SplitContainer5.Name = "SplitContainer5"
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.PictureBox2)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.PictureBox3)
        Me.SplitContainer5.Size = New System.Drawing.Size(165, 101)
        Me.SplitContainer5.SplitterDistance = 81
        Me.SplitContainer5.TabIndex = 42
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Status1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 508)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1234, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Status1
        '
        Me.Status1.Name = "Status1"
        Me.Status1.Size = New System.Drawing.Size(39, 17)
        Me.Status1.Text = "Status"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(81, 101)
        Me.PictureBox2.TabIndex = 38
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PictureBox3.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(80, 101)
        Me.PictureBox3.TabIndex = 39
        Me.PictureBox3.TabStop = False
        '
        'frmImposeBkOrPam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1234, 530)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmImposeBkOrPam"
        Me.Text = "Imposition"
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.FlowLayoutPanel5.ResumeLayout(False)
        Me.FlowLayoutPanel5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.FlowLayoutPanel4.ResumeLayout(False)
        Me.FlowLayoutPanel4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.FlowLayoutPanel3.ResumeLayout(False)
        Me.FlowLayoutPanel3.PerformLayout()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer5.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents txtImposeSearch As TextBox
    Friend WithEvents btnSearchPrntEbk As Button
    Friend WithEvents ListOpenPrePrintFolder As ListBox
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Private WithEvents Label6 As Label
    Private WithEvents WidthInch As Label
    Private WithEvents Label2 As Label
    Private WithEvents RotLabel As Label
    Private WithEvents Label5 As Label
    Private WithEvents PagesLabel As Label
    Friend WithEvents txtSavePDFas As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents FlowLayoutPanel5 As FlowLayoutPanel
    Friend WithEvents rdbMiniPam As RadioButton
    Friend WithEvents rdbCustomSize As RadioButton
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents FlowLayoutPanel4 As FlowLayoutPanel
    Friend WithEvents rdb12x18MiniBook As RadioButton
    Friend WithEvents rdbBooklet As RadioButton
    Friend WithEvents rdbCDC_XL As RadioButton
    Friend WithEvents rdbCDC As RadioButton
    Friend WithEvents rdbYCC As RadioButton
    Friend WithEvents rdbBGC As RadioButton
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents FlowLayoutPanel3 As FlowLayoutPanel
    Friend WithEvents rdbFourUp As RadioButton
    Friend WithEvents rdbFullBleed As RadioButton
    Friend WithEvents rdbQuarterNSideBySide As RadioButton
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents btnImposeBkCov As Button
    Friend WithEvents btnImposePamCov As Button
    Friend WithEvents btnSelectPDF As Button
    Friend WithEvents btnPDFImposeSave As Button
    Friend WithEvents txtSelectPDF As TextBox
    Friend WithEvents SplitContainer5 As SplitContainer
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents Status1 As ToolStripStatusLabel
End Class
