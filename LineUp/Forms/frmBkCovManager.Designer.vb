<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBkCovManager
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnAll = New System.Windows.Forms.Button()
        Me.btnMatchedOK = New System.Windows.Forms.Button()
        Me.btnNoFilesFound = New System.Windows.Forms.Button()
        Me.btnMultipleFilesFound = New System.Windows.Forms.Button()
        Me.btnMultipleFoldersFound = New System.Windows.Forms.Button()
        Me.btnNone = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.UpDownNumFolders = New System.Windows.Forms.NumericUpDown()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtResults = New System.Windows.Forms.TextBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnExportCov = New System.Windows.Forms.Button()
        Me.btnExportACover = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.WebOld = New System.Windows.Forms.WebBrowser()
        Me.WebNew = New System.Windows.Forms.WebBrowser()
        Me.lvFileNames = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColOld = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColNew = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.UpDownNumFolders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.Controls.Add(Me.btnAll)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnMatchedOK)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnNoFilesFound)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnMultipleFilesFound)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnMultipleFoldersFound)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnNone)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBox3)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 19)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(230, 395)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'btnAll
        '
        Me.btnAll.Location = New System.Drawing.Point(3, 3)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Padding = New System.Windows.Forms.Padding(5)
        Me.btnAll.Size = New System.Drawing.Size(222, 45)
        Me.btnAll.TabIndex = 7
        Me.btnAll.Text = "All"
        Me.btnAll.UseVisualStyleBackColor = True
        '
        'btnMatchedOK
        '
        Me.btnMatchedOK.Location = New System.Drawing.Point(3, 54)
        Me.btnMatchedOK.Name = "btnMatchedOK"
        Me.btnMatchedOK.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMatchedOK.Size = New System.Drawing.Size(222, 45)
        Me.btnMatchedOK.TabIndex = 9
        Me.btnMatchedOK.Text = "Matched OK Files"
        Me.btnMatchedOK.UseVisualStyleBackColor = True
        '
        'btnNoFilesFound
        '
        Me.btnNoFilesFound.Location = New System.Drawing.Point(3, 105)
        Me.btnNoFilesFound.Name = "btnNoFilesFound"
        Me.btnNoFilesFound.Padding = New System.Windows.Forms.Padding(5)
        Me.btnNoFilesFound.Size = New System.Drawing.Size(222, 45)
        Me.btnNoFilesFound.TabIndex = 10
        Me.btnNoFilesFound.Text = "No Files Found "
        Me.btnNoFilesFound.UseVisualStyleBackColor = True
        '
        'btnMultipleFilesFound
        '
        Me.btnMultipleFilesFound.Location = New System.Drawing.Point(3, 156)
        Me.btnMultipleFilesFound.Name = "btnMultipleFilesFound"
        Me.btnMultipleFilesFound.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMultipleFilesFound.Size = New System.Drawing.Size(222, 45)
        Me.btnMultipleFilesFound.TabIndex = 11
        Me.btnMultipleFilesFound.Text = "Multiple Files Found "
        Me.btnMultipleFilesFound.UseVisualStyleBackColor = True
        '
        'btnMultipleFoldersFound
        '
        Me.btnMultipleFoldersFound.Location = New System.Drawing.Point(3, 207)
        Me.btnMultipleFoldersFound.Name = "btnMultipleFoldersFound"
        Me.btnMultipleFoldersFound.Padding = New System.Windows.Forms.Padding(5)
        Me.btnMultipleFoldersFound.Size = New System.Drawing.Size(222, 45)
        Me.btnMultipleFoldersFound.TabIndex = 12
        Me.btnMultipleFoldersFound.Text = "Multiple Folders Found"
        Me.btnMultipleFoldersFound.UseVisualStyleBackColor = True
        '
        'btnNone
        '
        Me.btnNone.Location = New System.Drawing.Point(3, 258)
        Me.btnNone.Name = "btnNone"
        Me.btnNone.Padding = New System.Windows.Forms.Padding(5)
        Me.btnNone.Size = New System.Drawing.Size(222, 45)
        Me.btnNone.TabIndex = 13
        Me.btnNone.Text = "None"
        Me.btnNone.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.UpDownNumFolders)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 309)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(222, 47)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Number of folders to open at a time"
        '
        'UpDownNumFolders
        '
        Me.UpDownNumFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UpDownNumFolders.Location = New System.Drawing.Point(3, 19)
        Me.UpDownNumFolders.Name = "UpDownNumFolders"
        Me.UpDownNumFolders.Size = New System.Drawing.Size(216, 23)
        Me.UpDownNumFolders.TabIndex = 0
        Me.UpDownNumFolders.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 7
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnRefresh, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnExportCov, 6, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.btnExportACover, 3, 9)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 10
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(702, 460)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'GroupBox2
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.GroupBox2, 5)
        Me.GroupBox2.Controls.Add(Me.txtResults)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.TableLayoutPanel1.SetRowSpan(Me.GroupBox2, 9)
        Me.GroupBox2.Size = New System.Drawing.Size(454, 417)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Results View:"
        '
        'txtResults
        '
        Me.txtResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtResults.Location = New System.Drawing.Point(3, 19)
        Me.txtResults.Multiline = True
        Me.txtResults.Name = "txtResults"
        Me.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtResults.Size = New System.Drawing.Size(448, 395)
        Me.txtResults.TabIndex = 0
        Me.txtResults.Text = "LOADING..."
        '
        'btnRefresh
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btnRefresh, 2)
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefresh.Location = New System.Drawing.Point(3, 426)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(178, 31)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "Refresh Results"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.GroupBox1, 2)
        Me.GroupBox1.Controls.Add(Me.FlowLayoutPanel1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(463, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.TableLayoutPanel1.SetRowSpan(Me.GroupBox1, 9)
        Me.GroupBox1.Size = New System.Drawing.Size(236, 417)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Folders to Open in Explorer:"
        '
        'btnExportCov
        '
        Me.btnExportCov.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExportCov.Location = New System.Drawing.Point(583, 426)
        Me.btnExportCov.Name = "btnExportCov"
        Me.btnExportCov.Size = New System.Drawing.Size(116, 31)
        Me.btnExportCov.TabIndex = 7
        Me.btnExportCov.Text = "Export Covers"
        Me.btnExportCov.UseVisualStyleBackColor = True
        '
        'btnExportACover
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btnExportACover, 2)
        Me.btnExportACover.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExportACover.Location = New System.Drawing.Point(279, 426)
        Me.btnExportACover.Name = "btnExportACover"
        Me.btnExportACover.Size = New System.Drawing.Size(178, 31)
        Me.btnExportACover.TabIndex = 8
        Me.btnExportACover.Text = "Export A Cover"
        Me.btnExportACover.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(716, 494)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(708, 466)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "     Creation     "
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(708, 466)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "     Compare     "
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 236.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox5, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox6, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(702, 460)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lvFileNames)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.TableLayoutPanel2.SetRowSpan(Me.GroupBox4, 3)
        Me.GroupBox4.Size = New System.Drawing.Size(230, 454)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Files Found"
        '
        'WebOld
        '
        Me.WebOld.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebOld.Location = New System.Drawing.Point(3, 19)
        Me.WebOld.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebOld.Name = "WebOld"
        Me.WebOld.Size = New System.Drawing.Size(221, 432)
        Me.WebOld.TabIndex = 1
        '
        'WebNew
        '
        Me.WebNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebNew.Location = New System.Drawing.Point(3, 19)
        Me.WebNew.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebNew.Name = "WebNew"
        Me.WebNew.Size = New System.Drawing.Size(221, 432)
        Me.WebNew.TabIndex = 2
        '
        'lvFileNames
        '
        Me.lvFileNames.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.ColOld, Me.ColNew})
        Me.lvFileNames.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFileNames.FullRowSelect = True
        Me.lvFileNames.HideSelection = False
        Me.lvFileNames.Location = New System.Drawing.Point(3, 19)
        Me.lvFileNames.MultiSelect = False
        Me.lvFileNames.Name = "lvFileNames"
        Me.lvFileNames.Size = New System.Drawing.Size(224, 432)
        Me.lvFileNames.TabIndex = 0
        Me.lvFileNames.UseCompatibleStateImageBehavior = False
        Me.lvFileNames.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "File Name"
        Me.colName.Width = 120
        '
        'ColOld
        '
        Me.ColOld.Text = "Old"
        Me.ColOld.Width = 40
        '
        'ColNew
        '
        Me.ColNew.Text = "New"
        Me.ColNew.Width = 40
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.WebOld)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(239, 3)
        Me.GroupBox5.Name = "GroupBox5"
        Me.TableLayoutPanel2.SetRowSpan(Me.GroupBox5, 3)
        Me.GroupBox5.Size = New System.Drawing.Size(227, 454)
        Me.GroupBox5.TabIndex = 3
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Old Files"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.WebNew)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Location = New System.Drawing.Point(472, 3)
        Me.GroupBox6.Name = "GroupBox6"
        Me.TableLayoutPanel2.SetRowSpan(Me.GroupBox6, 3)
        Me.GroupBox6.Size = New System.Drawing.Size(227, 454)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "New Files"
        '
        'frmBkCovManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(716, 494)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmBkCovManager"
        Me.Text = "Cover Manager"
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.UpDownNumFolders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtResults As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents UpDownNumFolders As NumericUpDown
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnAll As Button
    Friend WithEvents btnMatchedOK As Button
    Friend WithEvents btnNoFilesFound As Button
    Friend WithEvents btnMultipleFilesFound As Button
    Friend WithEvents btnMultipleFoldersFound As Button
    Friend WithEvents btnNone As Button
    Friend WithEvents btnExportCov As Button
    Friend WithEvents btnExportACover As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents WebOld As WebBrowser
    Friend WithEvents WebNew As WebBrowser
    Friend WithEvents lvFileNames As ListView
    Friend WithEvents colName As ColumnHeader
    Friend WithEvents ColOld As ColumnHeader
    Friend WithEvents ColNew As ColumnHeader
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents GroupBox6 As GroupBox
End Class
