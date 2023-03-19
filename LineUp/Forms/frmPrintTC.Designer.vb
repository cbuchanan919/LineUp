<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintTC
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
        Me.tmrPrint = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFound = New System.Windows.Forms.Label()
        Me.btnSelectFiles = New System.Windows.Forms.Button()
        Me.udCopyEverySeconds = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.udPauseAfter = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboPrinters = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkIsPaused = New System.Windows.Forms.CheckBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.chkMoveFiles = New System.Windows.Forms.CheckBox()
        Me.clbFilesToPrint = New System.Windows.Forms.CheckedListBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblExported = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupBox1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.udCopyEverySeconds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udPauseAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmrPrint
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.SplitContainer1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(933, 497)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "The Christian - Export Config"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 19)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.clbFilesToPrint)
        Me.SplitContainer1.Size = New System.Drawing.Size(927, 475)
        Me.SplitContainer1.SplitterDistance = 121
        Me.SplitContainer1.TabIndex = 3
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblFound, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSelectFiles, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.udCopyEverySeconds, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.udPauseAfter, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.cboPrinters, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.chkIsPaused, 4, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnPrint, 5, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.chkMoveFiles, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(927, 121)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'lblFound
        '
        Me.lblFound.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFound.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblFound, 2)
        Me.lblFound.Location = New System.Drawing.Point(3, 37)
        Me.lblFound.Name = "lblFound"
        Me.lblFound.Size = New System.Drawing.Size(74, 15)
        Me.lblFound.TabIndex = 1
        Me.lblFound.Text = "Found 0 files"
        '
        'btnSelectFiles
        '
        Me.btnSelectFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSelectFiles.Location = New System.Drawing.Point(3, 3)
        Me.btnSelectFiles.Name = "btnSelectFiles"
        Me.btnSelectFiles.Size = New System.Drawing.Size(148, 24)
        Me.btnSelectFiles.TabIndex = 0
        Me.btnSelectFiles.Text = "Select TC Files"
        Me.btnSelectFiles.UseVisualStyleBackColor = True
        '
        'udCopyEverySeconds
        '
        Me.udCopyEverySeconds.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.udCopyEverySeconds.DecimalPlaces = 2
        Me.udCopyEverySeconds.Location = New System.Drawing.Point(619, 3)
        Me.udCopyEverySeconds.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.udCopyEverySeconds.Minimum = New Decimal(New Integer() {25, 0, 0, 131072})
        Me.udCopyEverySeconds.Name = "udCopyEverySeconds"
        Me.udCopyEverySeconds.Size = New System.Drawing.Size(148, 23)
        Me.udCopyEverySeconds.TabIndex = 2
        Me.udCopyEverySeconds.Value = New Decimal(New Integer() {35, 0, 0, 65536})
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(544, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Pause every"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(773, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "second(s)"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(516, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Copy 1 file every "
        '
        'udPauseAfter
        '
        Me.udPauseAfter.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.udPauseAfter.Location = New System.Drawing.Point(619, 33)
        Me.udPauseAfter.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.udPauseAfter.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.udPauseAfter.Name = "udPauseAfter"
        Me.udPauseAfter.Size = New System.Drawing.Size(148, 23)
        Me.udPauseAfter.TabIndex = 2
        Me.udPauseAfter.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(773, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 15)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "files."
        '
        'cboPrinters
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.cboPrinters, 2)
        Me.cboPrinters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrinters.FormattingEnabled = True
        Me.cboPrinters.Location = New System.Drawing.Point(619, 63)
        Me.cboPrinters.Name = "cboPrinters"
        Me.cboPrinters.Size = New System.Drawing.Size(305, 23)
        Me.cboPrinters.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(532, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 15)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Printer to Use:"
        '
        'chkIsPaused
        '
        Me.chkIsPaused.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.chkIsPaused.AutoSize = True
        Me.chkIsPaused.Checked = True
        Me.chkIsPaused.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsPaused.Location = New System.Drawing.Point(665, 96)
        Me.chkIsPaused.Name = "chkIsPaused"
        Me.chkIsPaused.Size = New System.Drawing.Size(102, 19)
        Me.chkIsPaused.TabIndex = 6
        Me.chkIsPaused.Text = "Pause Printing"
        Me.chkIsPaused.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPrint.Location = New System.Drawing.Point(773, 93)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(151, 25)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print / Export  Files"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'chkMoveFiles
        '
        Me.chkMoveFiles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMoveFiles.AutoSize = True
        Me.chkMoveFiles.Checked = True
        Me.chkMoveFiles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TableLayoutPanel1.SetColumnSpan(Me.chkMoveFiles, 3)
        Me.chkMoveFiles.Location = New System.Drawing.Point(3, 96)
        Me.chkMoveFiles.Name = "chkMoveFiles"
        Me.chkMoveFiles.Size = New System.Drawing.Size(293, 19)
        Me.chkMoveFiles.TabIndex = 10
        Me.chkMoveFiles.Text = "Upon completion, move files to 'Printed' subfolder"
        Me.ToolTip1.SetToolTip(Me.chkMoveFiles, "If checked, it will move files to a subfolder called 'Printed'")
        Me.chkMoveFiles.UseVisualStyleBackColor = True
        '
        'clbFilesToPrint
        '
        Me.clbFilesToPrint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilesToPrint.FormattingEnabled = True
        Me.clbFilesToPrint.Location = New System.Drawing.Point(0, 0)
        Me.clbFilesToPrint.Name = "clbFilesToPrint"
        Me.clbFilesToPrint.Size = New System.Drawing.Size(927, 350)
        Me.clbFilesToPrint.TabIndex = 2
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblExported})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 497)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(933, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblExported
        '
        Me.lblExported.Name = "lblExported"
        Me.lblExported.Size = New System.Drawing.Size(110, 17)
        Me.lblExported.Text = "Exported 0 of 0 files"
        '
        'frmPrintTC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(933, 519)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmPrintTC"
        Me.ShowIcon = False
        Me.Text = "Print TC"
        Me.GroupBox1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.udCopyEverySeconds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udPauseAfter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tmrPrint As Timer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblFound As Label
    Friend WithEvents btnSelectFiles As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents udCopyEverySeconds As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents udPauseAfter As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents btnPrint As Button
    Friend WithEvents clbFilesToPrint As CheckedListBox
    Friend WithEvents chkIsPaused As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cboPrinters As ComboBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents chkMoveFiles As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblExported As ToolStripStatusLabel
End Class
