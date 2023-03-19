<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmError
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmError))
        Me.ErrorRTF = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'ErrorRTF
        '
        Me.ErrorRTF.DetectUrls = False
        Me.ErrorRTF.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ErrorRTF.Location = New System.Drawing.Point(0, 0)
        Me.ErrorRTF.Name = "ErrorRTF"
        Me.ErrorRTF.Size = New System.Drawing.Size(777, 528)
        Me.ErrorRTF.TabIndex = 0
        Me.ErrorRTF.Text = ""
        '
        'frmError
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 528)
        Me.Controls.Add(Me.ErrorRTF)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmError"
        Me.Text = "Error Log"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ErrorRTF As System.Windows.Forms.RichTextBox
End Class
