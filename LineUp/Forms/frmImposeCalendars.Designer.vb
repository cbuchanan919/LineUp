<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImposeCalendars
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImposeCalendars))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdb6120 = New System.Windows.Forms.RadioButton()
        Me.rdb6880 = New System.Windows.Forms.RadioButton()
        Me.rdb7427 = New System.Windows.Forms.RadioButton()
        Me.rdb8101 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnImpose = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdb6120)
        Me.GroupBox1.Controls.Add(Me.rdb6880)
        Me.GroupBox1.Controls.Add(Me.rdb7427)
        Me.GroupBox1.Controls.Add(Me.rdb8101)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(260, 125)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Step 1. Select Calendar's Product Number"
        '
        'rdb6120
        '
        Me.rdb6120.AutoSize = True
        Me.rdb6120.Location = New System.Drawing.Point(6, 97)
        Me.rdb6120.Name = "rdb6120"
        Me.rdb6120.Size = New System.Drawing.Size(159, 19)
        Me.rdb6120.TabIndex = 0
        Me.rdb6120.Text = "6120 - Custom Tract Card"
        Me.rdb6120.UseVisualStyleBackColor = True
        '
        'rdb6880
        '
        Me.rdb6880.AutoSize = True
        Me.rdb6880.Location = New System.Drawing.Point(6, 72)
        Me.rdb6880.Name = "rdb6880"
        Me.rdb6880.Size = New System.Drawing.Size(200, 19)
        Me.rdb6880.TabIndex = 0
        Me.rdb6880.Text = "6880 - Joyful News Mini Calendar"
        Me.rdb6880.UseVisualStyleBackColor = True
        '
        'rdb7427
        '
        Me.rdb7427.AutoSize = True
        Me.rdb7427.Location = New System.Drawing.Point(6, 47)
        Me.rdb7427.Name = "rdb7427"
        Me.rdb7427.Size = New System.Drawing.Size(188, 19)
        Me.rdb7427.TabIndex = 0
        Me.rdb7427.Text = "7427 - Custom Wallet Calendar"
        Me.rdb7427.UseVisualStyleBackColor = True
        '
        'rdb8101
        '
        Me.rdb8101.AutoSize = True
        Me.rdb8101.Checked = True
        Me.rdb8101.Location = New System.Drawing.Point(6, 22)
        Me.rdb8101.Name = "rdb8101"
        Me.rdb8101.Size = New System.Drawing.Size(194, 19)
        Me.rdb8101.TabIndex = 0
        Me.rdb8101.TabStop = True
        Me.rdb8101.Text = "8101 - Gospel of Peace Calendar"
        Me.rdb8101.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnCancel)
        Me.GroupBox2.Controls.Add(Me.btnImpose)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 146)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(260, 125)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Step 2. Input Info"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(69, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(177, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "(Separate designs by semicolon)"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(179, 91)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 28)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnImpose
        '
        Me.btnImpose.Location = New System.Drawing.Point(98, 91)
        Me.btnImpose.Name = "btnImpose"
        Me.btnImpose.Size = New System.Drawing.Size(75, 28)
        Me.btnImpose.TabIndex = 2
        Me.btnImpose.Text = "Impose"
        Me.btnImpose.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(72, 22)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(182, 23)
        Me.TextBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Design ID:"
        '
        'frmImpose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(284, 285)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmImpose"
        Me.Text = "Impose Calendars"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rdb6120 As RadioButton
    Friend WithEvents rdb6880 As RadioButton
    Friend WithEvents rdb7427 As RadioButton
    Friend WithEvents rdb8101 As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents btnImpose As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label2 As Label
End Class
