<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrlWeb
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Web = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.NavTxt = New System.Windows.Forms.TextBox()
        Me.GoBtn = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.Web, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Web, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NavTxt, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GoBtn, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(530, 364)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'Web
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Web, 2)
        Me.Web.CreationProperties = Nothing
        Me.Web.DefaultBackgroundColor = System.Drawing.Color.White
        Me.Web.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Web.Location = New System.Drawing.Point(3, 33)
        Me.Web.Name = "Web"
        Me.Web.Size = New System.Drawing.Size(524, 328)
        Me.Web.TabIndex = 1
        Me.Web.ZoomFactor = 1.0R
        '
        'NavTxt
        '
        Me.NavTxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NavTxt.Location = New System.Drawing.Point(3, 3)
        Me.NavTxt.Name = "NavTxt"
        Me.NavTxt.Size = New System.Drawing.Size(424, 23)
        Me.NavTxt.TabIndex = 2
        '
        'GoBtn
        '
        Me.GoBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GoBtn.Location = New System.Drawing.Point(433, 3)
        Me.GoBtn.Name = "GoBtn"
        Me.GoBtn.Size = New System.Drawing.Size(94, 24)
        Me.GoBtn.TabIndex = 3
        Me.GoBtn.Text = "Go"
        Me.GoBtn.UseVisualStyleBackColor = True
        '
        'ctrlWeb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ctrlWeb"
        Me.Size = New System.Drawing.Size(530, 364)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.Web, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Web As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents NavTxt As TextBox
    Friend WithEvents GoBtn As Button
End Class
