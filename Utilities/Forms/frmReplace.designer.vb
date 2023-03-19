<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReplace
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReplace))
        Me.btnFindNext = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.chkMatchCase = New System.Windows.Forms.CheckBox()
        Me.txtSearchTerm = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtReplacementText = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnReplace = New System.Windows.Forms.Button()
        Me.btnReplaceAll = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnFindNext
        '
        Me.btnFindNext.Location = New System.Drawing.Point(110, 143)
        Me.btnFindNext.Name = "btnFindNext"
        Me.btnFindNext.Size = New System.Drawing.Size(87, 24)
        Me.btnFindNext.TabIndex = 5
        Me.btnFindNext.Text = "Find &Next"
        Me.btnFindNext.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(15, 143)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(87, 24)
        Me.btnFind.TabIndex = 4
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'chkMatchCase
        '
        Me.chkMatchCase.AutoSize = True
        Me.chkMatchCase.Location = New System.Drawing.Point(14, 108)
        Me.chkMatchCase.Name = "chkMatchCase"
        Me.chkMatchCase.Size = New System.Drawing.Size(88, 19)
        Me.chkMatchCase.TabIndex = 3
        Me.chkMatchCase.Text = "Match Case"
        Me.chkMatchCase.UseVisualStyleBackColor = True
        '
        'txtSearchTerm
        '
        Me.txtSearchTerm.Location = New System.Drawing.Point(14, 29)
        Me.txtSearchTerm.Name = "txtSearchTerm"
        Me.txtSearchTerm.Size = New System.Drawing.Size(374, 23)
        Me.txtSearchTerm.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 15)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Search Term:"
        '
        'txtReplacementText
        '
        Me.txtReplacementText.Location = New System.Drawing.Point(15, 81)
        Me.txtReplacementText.Name = "txtReplacementText"
        Me.txtReplacementText.Size = New System.Drawing.Size(373, 23)
        Me.txtReplacementText.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 15)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Replacement Text:"
        '
        'btnReplace
        '
        Me.btnReplace.Location = New System.Drawing.Point(205, 142)
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.Size = New System.Drawing.Size(87, 24)
        Me.btnReplace.TabIndex = 6
        Me.btnReplace.Text = "&Replace"
        Me.btnReplace.UseVisualStyleBackColor = True
        '
        'btnReplaceAll
        '
        Me.btnReplaceAll.Location = New System.Drawing.Point(301, 142)
        Me.btnReplaceAll.Name = "btnReplaceAll"
        Me.btnReplaceAll.Size = New System.Drawing.Size(87, 24)
        Me.btnReplaceAll.TabIndex = 7
        Me.btnReplaceAll.Text = "Replace &All"
        Me.btnReplaceAll.UseVisualStyleBackColor = True
        '
        'frmReplace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(415, 185)
        Me.Controls.Add(Me.btnReplaceAll)
        Me.Controls.Add(Me.btnReplace)
        Me.Controls.Add(Me.txtReplacementText)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnFindNext)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.chkMatchCase)
        Me.Controls.Add(Me.txtSearchTerm)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReplace"
        Me.ShowInTaskbar = False
        Me.Text = "RTE - Replace Text"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnFindNext As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents chkMatchCase As System.Windows.Forms.CheckBox
    Friend WithEvents txtSearchTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtReplacementText As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnReplace As System.Windows.Forms.Button
    Friend WithEvents btnReplaceAll As System.Windows.Forms.Button
End Class
