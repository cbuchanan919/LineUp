<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmailDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EmailDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtEmailPassword = New System.Windows.Forms.TextBox()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.txtEmailCC = New System.Windows.Forms.TextBox()
        Me.txtEmailFrom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEmailTo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtEmailBody = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEmailSubject = New System.Windows.Forms.TextBox()
        Me.lblAttachFiles = New System.Windows.Forms.LinkLabel()
        Me.cboAttachFiles = New System.Windows.Forms.ComboBox()
        Me.pdfInvoice = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.attachFiles = New System.Windows.Forms.OpenFileDialog()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.pdfInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(476, 404)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(170, 33)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.BackColor = System.Drawing.Color.DodgerBlue
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderSize = 0
        Me.Cancel_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black
        Me.Cancel_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel_Button.ForeColor = System.Drawing.Color.White
        Me.Cancel_Button.Location = New System.Drawing.Point(3, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(78, 27)
        Me.Cancel_Button.TabIndex = 8
        Me.Cancel_Button.TabStop = False
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = False
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.BackColor = System.Drawing.Color.DodgerBlue
        Me.OK_Button.FlatAppearance.BorderSize = 0
        Me.OK_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black
        Me.OK_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK_Button.ForeColor = System.Drawing.Color.White
        Me.OK_Button.Location = New System.Drawing.Point(88, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(78, 27)
        Me.OK_Button.TabIndex = 9
        Me.OK_Button.TabStop = False
        Me.OK_Button.Text = "Send Email"
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 2)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.pdfInvoice)
        Me.SplitContainer1.Size = New System.Drawing.Size(865, 396)
        Me.SplitContainer1.SplitterDistance = 647
        Me.SplitContainer1.TabIndex = 2
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.SplitContainer2, 3, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txtEmailCC, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtEmailFrom, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txtEmailTo, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 2, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txtEmailBody, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.txtEmailSubject, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.lblAttachFiles, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.cboAttachFiles, 1, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(647, 396)
        Me.TableLayoutPanel2.TabIndex = 2
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(403, 30)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtEmailPassword)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnShow)
        Me.SplitContainer2.Size = New System.Drawing.Size(244, 30)
        Me.SplitContainer2.SplitterDistance = 183
        Me.SplitContainer2.TabIndex = 0
        Me.SplitContainer2.TabStop = False
        '
        'txtEmailPassword
        '
        Me.txtEmailPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmailPassword.Location = New System.Drawing.Point(3, 3)
        Me.txtEmailPassword.Margin = New System.Windows.Forms.Padding(0)
        Me.txtEmailPassword.Name = "txtEmailPassword"
        Me.txtEmailPassword.Size = New System.Drawing.Size(180, 23)
        Me.txtEmailPassword.TabIndex = 4
        Me.txtEmailPassword.UseSystemPasswordChar = True
        '
        'btnShow
        '
        Me.btnShow.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnShow.FlatAppearance.BorderSize = 0
        Me.btnShow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black
        Me.btnShow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange
        Me.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShow.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.ForeColor = System.Drawing.Color.White
        Me.btnShow.Location = New System.Drawing.Point(0, 0)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(57, 30)
        Me.btnShow.TabIndex = 0
        Me.btnShow.TabStop = False
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = False
        '
        'txtEmailCC
        '
        Me.txtEmailCC.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmailCC.Location = New System.Drawing.Point(406, 3)
        Me.txtEmailCC.Name = "txtEmailCC"
        Me.txtEmailCC.Size = New System.Drawing.Size(238, 23)
        Me.txtEmailCC.TabIndex = 2
        '
        'txtEmailFrom
        '
        Me.txtEmailFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmailFrom.Location = New System.Drawing.Point(93, 33)
        Me.txtEmailFrom.Name = "txtEmailFrom"
        Me.txtEmailFrom.Size = New System.Drawing.Size(237, 23)
        Me.txtEmailFrom.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(65, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "To:"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(49, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 15)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "From:"
        '
        'txtEmailTo
        '
        Me.txtEmailTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmailTo.Location = New System.Drawing.Point(93, 3)
        Me.txtEmailTo.Name = "txtEmailTo"
        Me.txtEmailTo.Size = New System.Drawing.Size(237, 23)
        Me.txtEmailTo.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(374, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(26, 15)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "CC:"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(340, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 15)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Password:"
        '
        'txtEmailBody
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.txtEmailBody, 4)
        Me.txtEmailBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEmailBody.Location = New System.Drawing.Point(3, 123)
        Me.txtEmailBody.Name = "txtEmailBody"
        Me.txtEmailBody.Size = New System.Drawing.Size(641, 270)
        Me.txtEmailBody.TabIndex = 7
        Me.txtEmailBody.Text = ""
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(38, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Subject:"
        '
        'txtEmailSubject
        '
        Me.txtEmailSubject.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.SetColumnSpan(Me.txtEmailSubject, 3)
        Me.txtEmailSubject.Location = New System.Drawing.Point(93, 93)
        Me.txtEmailSubject.Name = "txtEmailSubject"
        Me.txtEmailSubject.Size = New System.Drawing.Size(551, 23)
        Me.txtEmailSubject.TabIndex = 6
        '
        'lblAttachFiles
        '
        Me.lblAttachFiles.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblAttachFiles.AutoSize = True
        Me.lblAttachFiles.Location = New System.Drawing.Point(16, 67)
        Me.lblAttachFiles.Name = "lblAttachFiles"
        Me.lblAttachFiles.Size = New System.Drawing.Size(71, 15)
        Me.lblAttachFiles.TabIndex = 0
        Me.lblAttachFiles.TabStop = True
        Me.lblAttachFiles.Text = "Attach Files:"
        '
        'cboAttachFiles
        '
        Me.cboAttachFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.SetColumnSpan(Me.cboAttachFiles, 3)
        Me.cboAttachFiles.FormattingEnabled = True
        Me.cboAttachFiles.Location = New System.Drawing.Point(93, 64)
        Me.cboAttachFiles.Name = "cboAttachFiles"
        Me.cboAttachFiles.Size = New System.Drawing.Size(551, 23)
        Me.cboAttachFiles.TabIndex = 5
        Me.cboAttachFiles.Text = "None"
        '
        'pdfInvoice
        '
        Me.pdfInvoice.CreationProperties = Nothing
        Me.pdfInvoice.DefaultBackgroundColor = System.Drawing.Color.White
        Me.pdfInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pdfInvoice.Location = New System.Drawing.Point(0, 0)
        Me.pdfInvoice.Name = "pdfInvoice"
        Me.pdfInvoice.Size = New System.Drawing.Size(214, 396)
        Me.pdfInvoice.TabIndex = 0
        Me.pdfInvoice.ZoomFactor = 1.0R
        '
        'EmailDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(867, 449)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EmailDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "EmailDialog"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.pdfInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtEmailPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtEmailCC As System.Windows.Forms.TextBox
    Friend WithEvents txtEmailSubject As System.Windows.Forms.TextBox
    Friend WithEvents txtEmailFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEmailTo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEmailBody As System.Windows.Forms.RichTextBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents lblAttachFiles As System.Windows.Forms.LinkLabel
    Friend WithEvents attachFiles As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cboAttachFiles As System.Windows.Forms.ComboBox
    Friend WithEvents pdfInvoice As Microsoft.Web.WebView2.WinForms.WebView2
End Class
