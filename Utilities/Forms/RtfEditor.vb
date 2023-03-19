Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class RtfEditor


#Region "RTF Editor"

#Region "Declarations"

    Private currentFile As String
    Private currentFileName As String
    Private checkPrint As Integer
    Private intCurrentFile As Integer = 0
    Public btnFmt As FormatControls

#End Region


#Region "Open, Close, and Misc Methods"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        btnFmt = New FormatControls(True, False, True)
        btnFmt.Format_Controls(Me)
    End Sub

    Private Sub gbRtfEditor_Enter(sender As Object, e As EventArgs) Handles gbRtfEditor.Enter
        'blank...
    End Sub

    ''' <summary>
    ''' Set to e.cancel close form. (e.Cancel = CancelCloseForm)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CancelCloseForm() As Boolean
        Dim CancelClose As Boolean = False

        If rtbDoc.Modified Then

            If MsgBox("The current document has not been saved, would you like to exit without saving?", MsgBoxStyle.YesNo, "Unsaved Document") = MsgBoxResult.Yes Then

            Else
                CancelClose = True
                rtbDoc.Focus()
            End If
        Else

            rtbDoc.Clear()

        End If
        Return CancelClose
    End Function

    ''' <summary>
    ''' Clears the Rich Text Box.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearRTFBox()
        rtbDoc.Clear()
    End Sub

    ''' <summary>
    ''' Saves the rtf to the specified location.
    ''' </summary>
    ''' <param name="FullFileName"></param>
    ''' <remarks></remarks>
    Public Sub SaveRtf(ByVal FullFileName As String)
        currentFile = FullFileName
        SaveToolStripMenuItem_Click(New Object, New EventArgs)
    End Sub

    Public Sub loadStringAsRtf(ByVal StringToLoad As String)
        rtbDoc.Clear()
        rtbDoc.Rtf = StringToLoad
    End Sub

    Public Function ReturnRtfStr() As String
        Return rtbDoc.Rtf
    End Function
#End Region
   


#Region "Menu Methods"

    Private Sub UpdatelblLastUpdated()
        Dim FileDate As DateTime = IO.File.GetLastWriteTime(currentFile)
        lblLastUpdated.Text = "Last Updated: " & FileDate.ToString("d") & " " & FileDate.ToShortTimeString
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click

        If rtbDoc.Modified Then

            Dim answer As Integer
            answer = MessageBox.Show("The current document has not been saved, would you like to continue without saving?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If answer = DialogResult.Yes Then
                rtbDoc.Clear()
            Else
                Exit Sub
            End If

        Else

            rtbDoc.Clear()

        End If

        currentFile = ""
        gbRtfEditor.Text = "New Document"

    End Sub



    Private Sub OpenToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click

        If rtbDoc.Modified Then

            Dim answer As Integer
            answer = MessageBox.Show("The current document has not been saved, would you like to continue without saving?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If answer = DialogResult.No Then
                Exit Sub

            Else

                OpenFile()

            End If

        Else

            OpenFile()

        End If

    End Sub

    ''' <summary>
    ''' Returns a boolean value that shows if the rtfbox was modified by the user.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isRtfBoxModified() As Boolean
        Dim isMod As Boolean = False
        isMod = rtbDoc.Modified
        Return isMod
    End Function

    ''' <summary>
    ''' opens the specified file, or opens an openfile dialog.
    ''' </summary>
    ''' <param name="filePathToOpen"></param>
    ''' <remarks></remarks>
    Public Sub OpenFile(Optional ByVal filePathToOpen As String = "")
        Try
            Dim strExt As String = ""
            If filePathToOpen = "" Then
                OpenFileDialog1.Title = "RTE - Open File"
                OpenFileDialog1.DefaultExt = "rtf"
                OpenFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*"
                OpenFileDialog1.FilterIndex = 1
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    filePathToOpen = OpenFileDialog1.FileName
                Else
                    Exit Sub
                End If

                If OpenFileDialog1.FileName = "" Then Exit Sub

            End If
            strExt = System.IO.Path.GetExtension(filePathToOpen)
            strExt = strExt.ToUpper()
            If IO.File.Exists(filePathToOpen) Then
                Select Case strExt
                    Case ".RTF"

                        rtbDoc.LoadFile(filePathToOpen, RichTextBoxStreamType.RichText)
                    Case Else
                        Dim txtReader As System.IO.StreamReader
                        txtReader = New System.IO.StreamReader(filePathToOpen)
                        rtbDoc.Text = txtReader.ReadToEnd
                        txtReader.Close()
                        txtReader = Nothing
                        rtbDoc.SelectionStart = 0
                        rtbDoc.SelectionLength = 0
                End Select
                currentFile = filePathToOpen
                currentFileName = System.IO.Path.GetFileName(currentFile)
                rtbDoc.Modified = False
                gbRtfEditor.Text = currentFileName

            End If

            UpdatelblLastUpdated()
            Application.DoEvents()
            UpdateZoom(tbZoom.Value)

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        




    End Sub



    Private Sub SaveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click

        If currentFile = "" Then
            SaveAsToolStripMenuItem_Click(Me, e)
            Exit Sub
        End If

        Dim strExt As String
        strExt = System.IO.Path.GetExtension(currentFile)
        strExt = strExt.ToUpper()

        Select Case strExt
            Case ".RTF"
                rtbDoc.SaveFile(currentFile)
            Case Else
                ' to save as plain text
                Dim txtWriter As System.IO.StreamWriter
                txtWriter = New System.IO.StreamWriter(currentFile)
                txtWriter.Write(rtbDoc.Text)
                txtWriter.Close()
                txtWriter = Nothing
                rtbDoc.SelectionStart = 0
                rtbDoc.SelectionLength = 0
                rtbDoc.Modified = False
        End Select
        rtbDoc.Modified = False
        currentFileName = System.IO.Path.GetFileName(currentFile)
        gbRtfEditor.Text = currentFileName
        UpdatelblLastUpdated()

    End Sub



    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click

        SaveFileDialog1.Title = "RTE - Save File"
        SaveFileDialog1.DefaultExt = "rtf"
        SaveFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName = "" Then Exit Sub

        Dim strExt As String
        strExt = System.IO.Path.GetExtension(SaveFileDialog1.FileName)
        strExt = strExt.ToUpper()

        Select Case strExt
            Case ".RTF"
                rtbDoc.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText)
            Case Else
                Dim txtWriter As System.IO.StreamWriter
                txtWriter = New System.IO.StreamWriter(SaveFileDialog1.FileName)
                txtWriter.Write(rtbDoc.Text)
                txtWriter.Close()
                txtWriter = Nothing
                rtbDoc.SelectionStart = 0
                rtbDoc.SelectionLength = 0
        End Select

        currentFile = SaveFileDialog1.FileName
        rtbDoc.Modified = False
        currentFileName = System.IO.Path.GetFileName(currentFile)
        gbRtfEditor.Text = currentFileName
        UpdatelblLastUpdated()

    End Sub



    'Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click

    '    If rtbDoc.Modified Then

    '        Dim answer As Integer
    '        answer = MessageBox.Show("The current document has not been saved, would you like to continue without saving?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

    '        If answer = Windows.Forms.DialogResult.No Then
    '            Exit Sub

    '        Else

    '            Application.Exit()

    '        End If

    '    Else

    '        Application.Exit()

    '    End If


    'End Sub



    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SelectAllToolStripMenuItem.Click

        Try

            rtbDoc.SelectAll()

        Catch exc As Exception

            MessageBox.Show("Unable to select all document content.", "RTE - Select", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub



    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click

        Try

            rtbDoc.Copy()

        Catch exc As Exception

            MessageBox.Show("Unable to copy document content.", "RTE - Copy", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub



    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click

        Try

            rtbDoc.Cut()

        Catch exc As Exception

            MessageBox.Show("Unable to cut document content.", "RTE - Cut", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub



    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasteToolStripMenuItem.Click

        Try
            rtbDoc.Paste()

        Catch exc As Exception

            MessageBox.Show("Unable to copy clipboard content to document.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub



    Private Sub SelectFontToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SelectFontToolStripMenuItem.Click

        If Not rtbDoc.SelectionFont Is Nothing Then
            FontDialog1.Font = rtbDoc.SelectionFont
        Else
            FontDialog1.Font = Nothing
        End If

        FontDialog1.ShowApply = True

        If FontDialog1.ShowDialog() = DialogResult.OK Then
            rtbDoc.SelectionFont = FontDialog1.Font
        End If

    End Sub



    Private Sub FontColorToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FontColorToolStripMenuItem.Click

        ColorDialog1.Color = rtbDoc.ForeColor

        If ColorDialog1.ShowDialog = DialogResult.OK Then
            rtbDoc.SelectionColor = ColorDialog1.Color
        End If

    End Sub



    Private Sub BoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BoldToolStripMenuItem.Click

        If Not rtbDoc.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = rtbDoc.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle

            If rtbDoc.SelectionFont.Bold = True Then
                newFontStyle = FontStyle.Regular
            Else
                newFontStyle = FontStyle.Bold
            End If

            rtbDoc.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If

    End Sub



    Private Sub ItalicToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ItalicToolStripMenuItem.Click

        If Not rtbDoc.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = rtbDoc.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle

            If rtbDoc.SelectionFont.Italic = True Then
                newFontStyle = FontStyle.Regular
            Else
                newFontStyle = FontStyle.Italic
            End If

            rtbDoc.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If

    End Sub



    Private Sub UnderlineToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles UnderlineToolStripMenuItem.Click

        If Not rtbDoc.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = rtbDoc.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle

            If rtbDoc.SelectionFont.Underline = True Then
                newFontStyle = FontStyle.Regular
            Else
                newFontStyle = FontStyle.Underline
            End If

            rtbDoc.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If

    End Sub



    Private Sub NormalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NormalToolStripMenuItem.Click

        If Not rtbDoc.SelectionFont Is Nothing Then

            Dim currentFont As System.Drawing.Font = rtbDoc.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle
            newFontStyle = FontStyle.Regular

            rtbDoc.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

        End If

    End Sub



    Private Sub PageColorToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PageColorToolStripMenuItem.Click

        ColorDialog1.Color = rtbDoc.BackColor

        If ColorDialog1.ShowDialog = DialogResult.OK Then
            rtbDoc.BackColor = ColorDialog1.Color
        End If

    End Sub



    Private Sub mnuUndo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuUndo.Click

        If rtbDoc.CanUndo Then rtbDoc.Undo()

    End Sub



    Private Sub mnuRedo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuRedo.Click

        If rtbDoc.CanRedo Then rtbDoc.Redo()

    End Sub



    Private Sub LeftToolStripMenuItem_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles LeftToolStripMenuItem.Click

        rtbDoc.SelectionAlignment = HorizontalAlignment.Left

    End Sub



    Private Sub CenterToolStripMenuItem_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles CenterToolStripMenuItem.Click

        rtbDoc.SelectionAlignment = HorizontalAlignment.Center

    End Sub



    Private Sub RightToolStripMenuItem_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles RightToolStripMenuItem.Click

        rtbDoc.SelectionAlignment = HorizontalAlignment.Right

    End Sub



    Private Sub AddBulletsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddBulletsToolStripMenuItem.Click

        rtbDoc.BulletIndent = 10
        rtbDoc.SelectionBullet = True

    End Sub



    Private Sub RemoveBulletsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RemoveBulletsToolStripMenuItem.Click

        rtbDoc.SelectionBullet = False

    End Sub



    Private Sub mnuIndent0_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuIndent0.Click

        rtbDoc.SelectionIndent = 0

    End Sub



    Private Sub mnuIndent5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuIndent5.Click

        rtbDoc.SelectionIndent = 5

    End Sub



    Private Sub mnuIndent10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuIndent10.Click

        rtbDoc.SelectionIndent = 10

    End Sub



    Private Sub mnuIndent15_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuIndent15.Click

        rtbDoc.SelectionIndent = 15

    End Sub



    Private Sub mnuIndent20_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuIndent20.Click

        rtbDoc.SelectionIndent = 20

    End Sub



    Private Sub FindToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FindToolStripMenuItem.Click

        Dim f As New frmFind(Me)
        f.Show()

    End Sub



    Private Sub FindAndReplaceToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FindAndReplaceToolStripMenuItem.Click

        Dim f As New frmReplace(Me)
        f.Show()

    End Sub



    Private Sub PreviewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PreviewToolStripMenuItem.Click

        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()

    End Sub



    Private Sub PrintToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PrintToolStripMenuItem.Click

        PrintDialog1.Document = PrintDocument1

        If PrintDialog1.ShowDialog() = DialogResult.OK Then
            PrintDocument1.Print()
        End If

    End Sub



    Private Sub mnuPageSetup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuPageSetup.Click

        PageSetupDialog1.Document = PrintDocument1
        PageSetupDialog1.ShowDialog()

    End Sub



    Private Sub InsertImageToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles InsertImageToolStripMenuItem.Click

        OpenFileDialog1.Title = "RTE - Insert Image File"
        OpenFileDialog1.DefaultExt = "rtf"
        OpenFileDialog1.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.ShowDialog()

        If OpenFileDialog1.FileName = "" Then Exit Sub

        Try
            Dim strImagePath As String = OpenFileDialog1.FileName
            Dim img As Image
            img = Image.FromFile(strImagePath)
            Clipboard.SetDataObject(img)
            Dim df As DataFormats.Format
            df = DataFormats.GetFormat(DataFormats.Bitmap)
            If Me.rtbDoc.CanPaste(df) Then
                Me.rtbDoc.Paste(df)
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to insert image format selected.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try

    End Sub


#End Region


#Region "Toolbar Methods"


    Private Sub tbrSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrSave.Click

        SaveToolStripMenuItem_Click(Me, e)

    End Sub



    Private Sub tbrOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrOpen.Click

        OpenToolStripMenuItem_Click(Me, e)

    End Sub



    Private Sub tbrNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrNew.Click

        NewToolStripMenuItem_Click(Me, e)

    End Sub



    Private Sub tbrBold_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrBold.Click

        BoldToolStripMenuItem_Click(Me, e)

    End Sub



    Private Sub tbrItalic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrItalic.Click

        ItalicToolStripMenuItem_Click(Me, e)

    End Sub



    Private Sub tbrUnderline_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrUnderline.Click

        UnderlineToolStripMenuItem_Click(Me, e)

    End Sub



    Private Sub tbrFont_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrFont.Click

        SelectFontToolStripMenuItem_Click(Me, e)

    End Sub



    Private Sub tbrLeft_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrLeft.Click

        rtbDoc.SelectionAlignment = HorizontalAlignment.Left

    End Sub



    Private Sub tbrCenter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrCenter.Click

        rtbDoc.SelectionAlignment = HorizontalAlignment.Center

    End Sub



    Private Sub tbrRight_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrRight.Click

        rtbDoc.SelectionAlignment = HorizontalAlignment.Right

    End Sub



    Private Sub tbrFind_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbrFind.Click

        Dim f As New frmFind(Me)
        f.Show()

    End Sub


    Private Sub tbrSave_Click_1(sender As Object, e As EventArgs) Handles tbrSave.Click

    End Sub

    Private Sub tbZoom_Scroll(sender As Object, e As EventArgs) Handles tbZoom.ValueChanged
        UpdateZoom(tbZoom.Value)

    End Sub

    Private Sub lblZoom_Click(sender As Object, e As EventArgs) Handles lblZoom.DoubleClick
        UpdateZoom()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="zoom">Default zoom is 100</param>
    ''' <remarks></remarks>
    Private Sub UpdateZoom(Optional ByVal zoom As Integer = -1013)
        Try
            If zoom = -1013 Then
                tbZoom.Value = 100
            End If
            rtbDoc.ZoomFactor = (tbZoom.Value / 100)
            lblZoom.Text = tbZoom.Value & "%"
        Catch ex As Exception

        End Try
    End Sub


#End Region


#End Region


End Class
