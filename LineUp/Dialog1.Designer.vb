<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dialog1
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
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.listBoxHelp = New System.Windows.Forms.ListBox()
        Me.btnRefreshHelp = New System.Windows.Forms.Button()
        Me.btnRemoveHelp = New System.Windows.Forms.Button()
        Me.btnAddNewHelp = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.listBoxHelp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRefreshHelp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRemoveHelp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNewHelp)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.OK_Button)
        Me.SplitContainer1.Size = New System.Drawing.Size(808, 539)
        Me.SplitContainer1.SplitterDistance = 269
        Me.SplitContainer1.TabIndex = 5
        '
        'listBoxHelp
        '
        Me.listBoxHelp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listBoxHelp.BackColor = System.Drawing.Color.White
        Me.listBoxHelp.FormattingEnabled = True
        Me.listBoxHelp.ItemHeight = 15
        Me.listBoxHelp.Location = New System.Drawing.Point(8, 15)
        Me.listBoxHelp.Name = "listBoxHelp"
        Me.listBoxHelp.Size = New System.Drawing.Size(253, 469)
        Me.listBoxHelp.TabIndex = 8
        '
        'btnRefreshHelp
        '
        Me.btnRefreshHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefreshHelp.AutoSize = True
        Me.btnRefreshHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshHelp.BackColor = System.Drawing.Color.SkyBlue
        Me.btnRefreshHelp.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnRefreshHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshHelp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefreshHelp.ForeColor = System.Drawing.Color.Black
        Me.btnRefreshHelp.Location = New System.Drawing.Point(13, 495)
        Me.btnRefreshHelp.Name = "btnRefreshHelp"
        Me.btnRefreshHelp.Size = New System.Drawing.Size(65, 29)
        Me.btnRefreshHelp.TabIndex = 11
        Me.btnRefreshHelp.Text = "Refresh"
        Me.btnRefreshHelp.UseVisualStyleBackColor = False
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
        Me.btnRemoveHelp.Location = New System.Drawing.Point(175, 495)
        Me.btnRemoveHelp.Name = "btnRemoveHelp"
        Me.btnRemoveHelp.Size = New System.Drawing.Size(69, 29)
        Me.btnRemoveHelp.TabIndex = 10
        Me.btnRemoveHelp.Text = "Remove"
        Me.btnRemoveHelp.UseVisualStyleBackColor = False
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
        Me.btnAddNewHelp.Location = New System.Drawing.Point(88, 495)
        Me.btnAddNewHelp.Name = "btnAddNewHelp"
        Me.btnAddNewHelp.Size = New System.Drawing.Size(76, 29)
        Me.btnAddNewHelp.TabIndex = 9
        Me.btnAddNewHelp.Text = "Add New"
        Me.btnAddNewHelp.UseVisualStyleBackColor = False
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.BackColor = System.Drawing.Color.SkyBlue
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.ForeColor = System.Drawing.Color.White
        Me.OK_Button.Location = New System.Drawing.Point(340, 412)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(91, 34)
        Me.OK_Button.TabIndex = 5
        Me.OK_Button.Text = "Close"
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'Dialog1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(808, 539)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Dialog1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Dialog1"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents listBoxHelp As System.Windows.Forms.ListBox
    Friend WithEvents btnRefreshHelp As System.Windows.Forms.Button
    Friend WithEvents btnRemoveHelp As System.Windows.Forms.Button
    Friend WithEvents btnAddNewHelp As System.Windows.Forms.Button

End Class
