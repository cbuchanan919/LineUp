<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmHelp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHelp))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lstBoxHelp = New System.Windows.Forms.ListBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnRefreshHelp = New System.Windows.Forms.Button()
        Me.btnAddNewHelp = New System.Windows.Forms.Button()
        Me.btnRemoveHelp = New System.Windows.Forms.Button()
        Me.btnRename = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.RtfEditor1 = New Utilities.RtfEditor()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RtfEditor1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1057, 549)
        Me.SplitContainer1.SplitterDistance = 263
        Me.SplitContainer1.TabIndex = 5
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.lstBoxHelp)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.FlowLayoutPanel1)
        Me.SplitContainer2.Size = New System.Drawing.Size(263, 549)
        Me.SplitContainer2.SplitterDistance = 507
        Me.SplitContainer2.TabIndex = 12
        '
        'lstBoxHelp
        '
        Me.lstBoxHelp.BackColor = System.Drawing.Color.White
        Me.lstBoxHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstBoxHelp.FormattingEnabled = True
        Me.lstBoxHelp.ItemHeight = 17
        Me.lstBoxHelp.Location = New System.Drawing.Point(0, 0)
        Me.lstBoxHelp.Name = "lstBoxHelp"
        Me.lstBoxHelp.Size = New System.Drawing.Size(263, 507)
        Me.lstBoxHelp.TabIndex = 8
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.Controls.Add(Me.btnRefreshHelp)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnAddNewHelp)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnRemoveHelp)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnRename)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(263, 38)
        Me.FlowLayoutPanel1.TabIndex = 12
        '
        'btnRefreshHelp
        '
        Me.btnRefreshHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefreshHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshHelp.BackColor = System.Drawing.Color.SkyBlue
        Me.btnRefreshHelp.BackgroundImage = Global.LineUp.My.Resources.Resources.RefreshWhite
        Me.btnRefreshHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefreshHelp.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnRefreshHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshHelp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefreshHelp.ForeColor = System.Drawing.Color.Black
        Me.btnRefreshHelp.Location = New System.Drawing.Point(18, 3)
        Me.btnRefreshHelp.Name = "btnRefreshHelp"
        Me.btnRefreshHelp.Size = New System.Drawing.Size(29, 29)
        Me.btnRefreshHelp.TabIndex = 11
        Me.btnRefreshHelp.UseVisualStyleBackColor = False
        '
        'btnAddNewHelp
        '
        Me.btnAddNewHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddNewHelp.AutoSize = True
        Me.btnAddNewHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddNewHelp.BackColor = System.Drawing.Color.SkyBlue
        Me.btnAddNewHelp.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnAddNewHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNewHelp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNewHelp.ForeColor = System.Drawing.Color.Black
        Me.btnAddNewHelp.Location = New System.Drawing.Point(53, 3)
        Me.btnAddNewHelp.Name = "btnAddNewHelp"
        Me.btnAddNewHelp.Size = New System.Drawing.Size(47, 29)
        Me.btnAddNewHelp.TabIndex = 9
        Me.btnAddNewHelp.Text = "New"
        Me.btnAddNewHelp.UseVisualStyleBackColor = False
        '
        'btnRemoveHelp
        '
        Me.btnRemoveHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveHelp.AutoSize = True
        Me.btnRemoveHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRemoveHelp.BackColor = System.Drawing.Color.SkyBlue
        Me.btnRemoveHelp.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnRemoveHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveHelp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveHelp.ForeColor = System.Drawing.Color.Black
        Me.btnRemoveHelp.Location = New System.Drawing.Point(106, 3)
        Me.btnRemoveHelp.Name = "btnRemoveHelp"
        Me.btnRemoveHelp.Size = New System.Drawing.Size(58, 29)
        Me.btnRemoveHelp.TabIndex = 10
        Me.btnRemoveHelp.Text = "Delete"
        Me.btnRemoveHelp.UseVisualStyleBackColor = False
        '
        'btnRename
        '
        Me.btnRename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRename.AutoSize = True
        Me.btnRename.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRename.BackColor = System.Drawing.Color.SkyBlue
        Me.btnRename.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRename.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRename.ForeColor = System.Drawing.Color.Black
        Me.btnRename.Location = New System.Drawing.Point(170, 3)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Size = New System.Drawing.Size(69, 29)
        Me.btnRename.TabIndex = 12
        Me.btnRename.Text = "Rename"
        Me.btnRename.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.AutoSize = True
        Me.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnClose.BackColor = System.Drawing.Color.SkyBlue
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(726, 14)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(52, 29)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'RtfEditor1
        '
        Me.RtfEditor1.BackColor = System.Drawing.Color.White
        Me.RtfEditor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RtfEditor1.Location = New System.Drawing.Point(0, 0)
        Me.RtfEditor1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RtfEditor1.Name = "RtfEditor1"
        Me.RtfEditor1.Size = New System.Drawing.Size(790, 549)
        Me.RtfEditor1.TabIndex = 12
        '
        'frmHelp
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1057, 549)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHelp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Help"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lstBoxHelp As System.Windows.Forms.ListBox
    Friend WithEvents btnRefreshHelp As System.Windows.Forms.Button
    Friend WithEvents btnRemoveHelp As System.Windows.Forms.Button
    Friend WithEvents btnAddNewHelp As System.Windows.Forms.Button
    Friend WithEvents btnRename As System.Windows.Forms.Button
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RtfEditor1 As Utilities.RtfEditor

End Class
