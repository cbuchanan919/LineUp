Imports System.IO
Imports System.Text.RegularExpressions ' Used to find the page size in Cover & Body
Imports Utilities
Imports iTextSharp.text.pdf

Public Class frmImposeBkOrPam

#Region "Properties & Variables"

    Private Property currentUVProductInfo As UvProductInfo
    Private Property pdfImpose As New PdfImposition() 'used to impose
    Private Property LastFilePath As String = ""

    Private Property MyJQProjectDirIO As JQProjectDirIO = Nothing

#End Region

#Region "Init"

    Public Sub New(ByVal currentUVProductInfo As UvProductInfo, ByVal MyJQProjectDirIO As JQProjectDirIO)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.currentUVProductInfo = currentUVProductInfo
        Me.MyJQProjectDirIO = MyJQProjectDirIO
    End Sub
    Private Sub frmImposeBkOrPam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsNothing(currentUVProductInfo) Then
            txtImposeSearch.Text = currentUVProductInfo.ItemNum & "; " & currentUVProductInfo.Title
            btnSearchPrntEbk.PerformClick()

        End If
    End Sub

#End Region



#Region "Tab Imposition"
    'takes care of updating the status bar
    Protected Friend Sub UpdateStatus(ByVal Message As String, ByVal isError As Boolean)
        If isError Then
            Status1.Text = cStatusA & Message
            Status1.ForeColor = Color.Red
        Else
            Dim msg As String = ""
            If Message = "" Then
                msg = ""
            Else
                msg = " - " & Message
            End If
            Status1.Text = cStatusGood & msg
            Status1.ForeColor = Color.Black

        End If

    End Sub



    Private Sub btnSearchPrntEbk_Click(sender As Object, e As EventArgs) Handles btnSearchPrntEbk.Click
        Try


            'clears the previous results
            ListOpenPrePrintFolder.Items.Clear()
            'if there's text in the txtProdNumPrePrint text box then...
            If txtImposeSearch.Text.Length > 0 Then
                'searches the folder names (in the directories that are listed below) for a match 
                For Each prodDir As JQProjectDirInfo In MyJQProjectDirIO.GetDirectories(txtImposeSearch.Text)
                    ListOpenPrePrintFolder.Items.Add(prodDir.ProjectDirectory.FullName)
                Next

                'creates proper grammar for the status result
                Select Case currentUVProductInfo.FoundProjectFolders.Count
                    Case 0
                        UpdateStatus("No matches found", True)
                        ListOpenPrePrintFolder.Items.Add("No Matches Found")
                    Case 1
                        UpdateStatus("1 folder found.", False)
                    Case Else
                        UpdateStatus(ListOpenPrePrintFolder.Items.Count & " folders found.", False)
                End Select

            End If
        Catch ex As Exception
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try

    End Sub




    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListOpenPrePrintFolder.SelectedIndexChanged
        'selects the first item in the list
        If ListOpenPrePrintFolder.Items.Count = 1 Then
            ListOpenPrePrintFolder.SelectedIndex = 0
            'ListOpenPrePrintFolder.SetSelected(0, True)
        End If

        If ListOpenPrePrintFolder.SelectedItems.Count = 1 Then
            'tries to get the final file
            HighlightFileExplorer(ListOpenPrePrintFolder.SelectedItem.ToString, False)
        End If
    End Sub

    Private Sub ListOpenPrePrintFolder_Click(sender As Object, e As EventArgs) Handles ListOpenPrePrintFolder.DoubleClick
        'tries to open the folder in explorer & highlight the final file
        If ListOpenPrePrintFolder.SelectedItems.Count = 1 And Directory.Exists(ListOpenPrePrintFolder.SelectedItem.ToString) Then
            HighlightFileExplorer(ListOpenPrePrintFolder.SelectedItem.ToString, True)
        End If

    End Sub

    Private Sub HighlightFileExplorer(ByVal folder As String, ByVal launchExplorer As Boolean)

        txtSelectPDF.Text = ""
        Try
            Dim dirinfo As New DirectoryInfo(folder)
            ' Get a reference to each folder in that directory.
            Dim FileArray As FileInfo() = dirinfo.GetFiles()
            ' Display the names of the folders.
            'Dim fri


            ' MsgBox(dirinfo.ToString)
            Dim FileToSelect As String = String.Empty
            For Each fri As FileInfo In FileArray
                'OpenFolder = String.Empty
                If fri.Name.ToLower.Contains("cover") Then
                Else
                    If fri.Name.ToLower.Contains(".indd") And fri.Name.ToLower.Contains("final") Then
                        'FileToSelect = fri.FullName
                    ElseIf fri.Name.ToLower.Contains(".pdf") And fri.Name.ToLower.Contains("final") And FileToSelect = String.Empty Then
                        FileToSelect = fri.FullName

                    End If
                End If


            Next fri

            '
            LastFilePath = folder
            If launchExplorer = True Then
                If FileToSelect = String.Empty Then
                    Process.Start("explorer.exe", Chr(34) & folder & Chr(34))

                Else
                    Process.Start("explorer.exe", "/select," & Chr(34) & FileToSelect & Chr(34))
                    txtSelectPDF.Text = FileToSelect.ToString

                End If

            Else
                If FileToSelect = String.Empty Then
                Else
                    txtSelectPDF.Text = FileToSelect.ToString
                End If

            End If

        Catch ex As Exception
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try
    End Sub

    Private Sub txtProdNumPrePrint_TextChanged(sender As Object, e As KeyEventArgs) Handles txtImposeSearch.KeyDown

        'Detects if enter (return) is pressed and runs product_find
        Select Case e.KeyCode
            Case Keys.Return
                btnSearchPrntEbk_Click(sender, New System.EventArgs)

        End Select
    End Sub
    Private Sub ClearRdbInImposition(Optional ByVal selectedRadioButton As RadioButton = Nothing)
        Try
            Dim rdbList As New List(Of RadioButton)
            With rdbList
                .Add(rdbFourUp)
                .Add(rdbFullBleed)
                .Add(rdbQuarterNSideBySide)
                .Add(rdbBooklet)
                .Add(rdbMiniPam)
                .Add(rdb12x18MiniBook)
                .Add(rdbCustomSize)
                .Add(rdbCDC_XL)
                .Add(rdbCDC)
                .Add(rdbYCC)
                .Add(rdbBGC)
            End With
            For Each myButton As RadioButton In rdbList
                If myButton.Name.Length > 0 Then
                    If Not IsNothing(selectedRadioButton) Then
                        If myButton.Name = selectedRadioButton.Name Then
                            myButton.Checked = True
                        Else
                            myButton.Checked = False
                        End If
                    Else
                        myButton.Checked = False
                    End If
                End If

            Next
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Returns true if PDF is an imposable size.
    ''' </summary>
    ''' <param name="UpdateRadioButtons">True will update radio button selection. False will not.</param>
    ''' <returns></returns>
    Private Function CheckSizeAgainstRDB(ByVal UpdateRadioButtons As Boolean) As Boolean
        Dim OktoProceed As Boolean = True

        Try
            Dim S As String = txtSelectPDF.Text
            If File.Exists(S) Then
                'used for itextsharp pdf imposition
                Dim PageCount As Integer
                Dim hi As Double = 0.0 'page height
                Dim wi As Double = 0.0 'page width
                Dim Rot As Single = 0  'page rotation

                Dim psize As iTextSharp.text.Rectangle
                Dim reader As PdfReader
                'end of itextsharp pdf imposition variables

                UpdateStatus("Wait...", False)
                Application.DoEvents()



                LastFilePath = Path.GetDirectoryName(S)
                reader = New PdfReader(S)
                PageCount = reader.NumberOfPages

                'assume whole doc is as 1st page; useless to impose a doc with varying pages

                'itextsharp finds the size in inches multiplied by 72. (maybe pixels?)
                psize = reader.GetPageSizeWithRotation(1)
                Rot = reader.GetPageRotation(1)
                reader.Close()

                wi = psize.Width / 72
                hi = psize.Height / 72



                RotLabel.Text = Rot.ToString()

                WidthInch.Text = wi & "x" & hi & " Inches"

                PagesLabel.Text = PageCount.ToString


                Dim foreColor As Color = Color.Black
                Dim rdb As New RadioButton


                If wi = 5 And hi = 8 Then
                    rdb = rdbFourUp

                ElseIf wi = 5.25 And hi = 8.25 Then
                    rdb = rdbFullBleed

                ElseIf wi = 5.75 And hi = 4.25 Then
                    rdb = rdbBooklet

                ElseIf wi = 5.5 And hi = 3.75 Then
                    rdb = rdbMiniPam

                ElseIf wi = 2.625 And hi = 4.0 Then
                    rdb = rdbCDC

                ElseIf wi = 4.5 And hi = 5.75 Then
                    rdb = rdbYCC


                Else
                    rdb = rdbCustomSize
                    foreColor = Color.Red
                    'OktoProceed = False
                End If


                If UpdateRadioButtons Then
                    ClearRdbInImposition()
                    rdb.Checked = True

                ElseIf rdb.Checked = False Then
                    'it's not the currently selected radio button. will probably result in error
                    OktoProceed = False
                    If wi = 5 And hi = 8 Then
                        If rdb12x18MiniBook.Checked Then
                            OktoProceed = True 'override for mini booklets 
                        End If
                    ElseIf wi = 4.5 And hi = 5.75 Then
                        If rdbBGC.Checked Then
                            OktoProceed = True 'override for bgc calendar
                        End If
                    End If
                End If
                WidthInch.ForeColor = foreColor

                If OktoProceed = False Then
                    UpdateStatus("Please select standard size for imposition", True)

                Else
                    UpdateStatus("", False)
                End If

            Else
                WidthInch.Text = "..."

                PagesLabel.Text = "..."

                RotLabel.Text = "..."
                WidthInch.ForeColor = Color.Black
                UpdateStatus("", False)
                OktoProceed = False
            End If
        Catch ex As Exception

        End Try

        Return OktoProceed
    End Function

    Private Sub txtSelectPDF_TextChanged(sender As Object, e As EventArgs) Handles txtSelectPDF.TextChanged

        If File.Exists(txtSelectPDF.Text) Then
            Try

                If CheckSizeAgainstRDB(True) Then
                    Dim S As String = txtSelectPDF.Text
                    Dim ImposePath As String = ""

                    'tries to set the output folder based on the input folder
                    Try
                        'S = txtSelectPDF.Text

                        If S.Contains(My.Settings.PrEBookDir) Then
                            ImposePath = My.Settings.dirBkBod

                        ElseIf S.Contains(My.Settings.PrEChartDir) Then
                            ImposePath = My.Settings.dirChart

                        ElseIf S.Contains(My.Settings.PrEHymnBookDir) Then
                            ImposePath = My.Settings.dirHymnbookBod

                        ElseIf S.Contains(My.Settings.PrELeafletDir) Then
                            ImposePath = My.Settings.dirLeaflet

                        ElseIf S.Contains(My.Settings.PrEPamphletDir) Then
                            ImposePath = My.Settings.dirMiniPamBod

                        Else
                            ImposePath = OpenFileDialog1.InitialDirectory
                        End If

                    Catch ex As Exception
                        LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                    End Try

                    'this try section tries to figure out the file name based on the numbers it finds in the string
                    Try
                        Dim cleanString As String
                        '                               [any numerical digits]{4 or 5 consecutively}
                        cleanString = Regex.Match(txtSelectPDF.Text, "[0-9]{4,5}").Value '"[A-Za-z\-/]", "")
                        If cleanString.Length >= 4 Then
                            If currentUVProductInfo.ItemNum = cleanString Then
                                ImposePath &= cleanString & ".pdf"

                            End If
                        End If
                    Catch ex As Exception
                        LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                    End Try
                    txtSavePDFas.Text = ImposePath
                End If

            Catch ex As Exception

            End Try

        Else

        End If


    End Sub


    Private Sub btnSelectPDF_Click(sender As Object, e As EventArgs) Handles btnSelectPDF.Click

        OpenFileDialog1.Filter = "All files (*.*)|*.*|pdf files (*.pdf)|*.pdf"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.InitialDirectory = LastFilePath
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            txtSelectPDF.Text = OpenFileDialog1.FileName
        End If
    End Sub



    Private Sub btnPDFImposeSave_Click(sender As Object, e As EventArgs) Handles btnPDFImposeSave.Click

        If rdbFourUp.Checked = True Or
            rdbQuarterNSideBySide.Checked = True Or
            rdbFullBleed.Checked = True Or
            rdbMiniPam.Checked = True Or
            rdbBooklet.Checked = True Or
            rdbCustomSize.Checked = True Or
            rdb12x18MiniBook.Checked = True Or
            rdbCDC_XL.Checked = True Or
            rdbCDC.Checked = True Or
            rdbYCC.Checked = True Or
            rdbBGC.Checked = True Then

            If txtSavePDFas.Text.Length > 0 Then
                Try
                    'if there's only a directory in the text box then:
                    If Directory.Exists(txtSavePDFas.Text) Then
                        SaveFileDialog1.InitialDirectory = txtSavePDFas.Text

                        'if there's a pdf file path in the text box then:
                    ElseIf txtSavePDFas.Text.Contains(".pdf") Then
                        SaveFileDialog1.InitialDirectory = Directory.GetParent(txtSavePDFas.Text).FullName
                        SaveFileDialog1.FileName = txtSavePDFas.Text.Replace(SaveFileDialog1.InitialDirectory & "\", "")
                    End If

                Catch ex As Exception
                    LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                End Try


            ElseIf txtSelectPDF.Text.Length > 0 Then

                'tries to set the output folder based on the input folder
                Try
                    If txtSelectPDF.Text.Contains(My.Settings.PrEBookDir.ToString) Then
                        SaveFileDialog1.InitialDirectory = My.Settings.dirBkBod
                    ElseIf txtSelectPDF.Text.Contains(My.Settings.PrEChartDir.ToString) Then
                        SaveFileDialog1.InitialDirectory = My.Settings.dirChart
                    ElseIf txtSelectPDF.Text.Contains(My.Settings.PrEHymnBookDir.ToString) Then
                        SaveFileDialog1.InitialDirectory = My.Settings.dirHymnbookBod
                    ElseIf txtSelectPDF.Text.Contains(My.Settings.PrELeafletDir.ToString) Then
                        SaveFileDialog1.InitialDirectory = My.Settings.dirLeaflet
                    ElseIf txtSelectPDF.Text.Contains(My.Settings.PrEPamphletDir.ToString) Then
                        SaveFileDialog1.InitialDirectory = My.Settings.dirPamBod
                    Else
                        SaveFileDialog1.InitialDirectory = OpenFileDialog1.InitialDirectory
                    End If

                Catch ex As Exception
                    LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                End Try



                'this try section tries to figure out the file name based on the numbers it finds in the string
                Try
                    Dim cleanString As String
                    '                               [any numerical digits]{4 or 5 consecutively}
                    cleanString = Regex.Match(txtSelectPDF.Text, "[0-9]{4,5}").Value '"[A-Za-z\-/]", "")
                    If cleanString.Length >= 4 Then
                        If currentUVProductInfo.ItemNum = cleanString Then
                            SaveFileDialog1.FileName = cleanString & ".pdf"
                        Else
                            'MsgBox(strItemNum)
                            'FindUpdateProductInfo(cleanString)
                            'if it can't find it in the xml then it doesn't know for sure it's the product number. so it takes it out
                            If currentUVProductInfo.ItemNum = cleanString Then
                                SaveFileDialog1.FileName = cleanString & ".pdf"
                            Else
                                SaveFileDialog1.FileName = ""
                            End If
                        End If
                    Else
                        SaveFileDialog1.FileName = ""
                    End If
                Catch ex As Exception
                    LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                End Try

            End If

            SaveFileDialog1.Filter = "PDF Files | *.pdf"
            SaveFileDialog1.DefaultExt = "pdf"
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

                Dim ImpositionType As String = ""
                Select Case True
                    Case rdbFourUp.Checked
                        ImpositionType = PdfImposition.FourUp
                    Case rdbQuarterNSideBySide.Checked
                        ImpositionType = PdfImposition.Economy
                    Case rdbFullBleed.Checked
                        ImpositionType = PdfImposition.FullBleed
                    Case rdbBooklet.Checked
                        ImpositionType = PdfImposition.Booklet12x18
                    Case rdbMiniPam.Checked
                        ImpositionType = PdfImposition.MiniPamphlet
                    Case rdbCustomSize.Checked
                        ImpositionType = PdfImposition.CustomSized4Up
                    Case rdb12x18MiniBook.Checked
                        ImpositionType = PdfImposition.MiniBooklet3_75x5_5
                    Case rdbCDC_XL.Checked
                        ImpositionType = PdfImposition.ChristiansDailyXL
                    Case rdbCDC.Checked
                        ImpositionType = PdfImposition.ChristiansDaily
                    Case rdbYCC.Checked
                        ImpositionType = PdfImposition.YoungChristianCalendar
                    Case rdbBGC.Checked
                        ImpositionType = PdfImposition.BoysGirlsCalendar

                End Select
                Dim Errors As String = ""


                If pdfImpose.Impose_5x8_PDF(txtSelectPDF.Text, SaveFileDialog1.FileName, ImpositionType, Errors) Then
                    UpdateStatus("File Exported!", False)
                Else
                    MsgBox(Errors, MsgBoxStyle.Critical, "Exporting Error")
                End If
            End If
        End If



    End Sub







    Private Sub btnImposeBkCov_Click(sender As Object, e As EventArgs) Handles btnImposeBkCov.Click
        'I'd love to be able to impose Book Covers eventually...
        OpenFileDialog1.Title = "Please Select the Book Cover File you Want to Impose"
        OpenFileDialog1.Filter = "All files (*.*)|*.*|Indesign Files (*.indd)|*.indd"
        SaveFileDialog1.DefaultExt = "indd"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.InitialDirectory = LastFilePath
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = False
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            Dim OriginPath As String = OpenFileDialog1.FileName
            Dim FinalPath As String = ""
            If OriginPath.Contains(My.Settings.PrEBookDir.ToString) Then
                FinalPath = My.Settings.dirBkBod
            ElseIf OriginPath.Contains(My.Settings.PrEChartDir.ToString) Then
                FinalPath = My.Settings.dirChart
            ElseIf OriginPath.Contains(My.Settings.PrEHymnBookDir.ToString) Then
                FinalPath = My.Settings.dirHymnbookBod
            ElseIf OriginPath.Contains(My.Settings.PrELeafletDir.ToString) Then
                FinalPath = My.Settings.dirLeaflet
            ElseIf OriginPath.Contains(My.Settings.PrEPamphletDir.ToString) Then
                FinalPath = My.Settings.dirPamBod
            Else
                FinalPath = OpenFileDialog1.InitialDirectory
            End If

            SaveFileDialog1.InitialDirectory = FinalPath


            Me.UseWaitCursor = True
            Application.DoEvents()
            SaveFileDialog1.Title = "Please Choose a PDF Export Location"
            SaveFileDialog1.Filter = "All files (*.*)|*.*|PDF Files | *.pdf"
            SaveFileDialog1.DefaultExt = "pdf"
            SaveFileDialog1.FilterIndex = 2

            If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim idApp As InDesign.Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)
                Dim ScriptArgList As New List(Of String)
                Dim ReturnData As String = ""
                Dim lstReturnData As New List(Of String)

                ScriptArgList.Add(Utilities.GenUtil.ConvertForJavaScript(My.Settings.bkCovImposeTemplatePath))
                ScriptArgList.Add(Utilities.GenUtil.ConvertForJavaScript(OpenFileDialog1.FileName))
                ScriptArgList.Add(Utilities.GenUtil.ConvertForJavaScript(SaveFileDialog1.FileName))
                Dim ScriptArgs() As String = ScriptArgList.ToArray


                ReturnData = idApp.DoScript(My.Settings.dirResources & "ImposeBookCover.jsx", InDesign.idScriptLanguage.idJavascript, ScriptArgs)

                'For Each myString As String In ReturnData.Split(vbTa)b
                '    lstReturnData.Add(myString)
                'Next


                If ReturnData <> "" Then
                    MsgBox(ReturnData)
                End If


            End If

        End If
        Me.UseWaitCursor = False

    End Sub

    Private Sub btnImposePamCov_Click(sender As Object, e As EventArgs) Handles btnImposePamCov.Click
        'I'd love to be able to impose Pamphlet Covers eventually...
    End Sub



    Private Sub rdb12x18MiniBook_CheckedChanged(sender As Object, e As EventArgs) Handles rdb12x18MiniBook.CheckedChanged
        impositionRDB_Changed(rdb12x18MiniBook)
    End Sub

    Private Sub rdb6x9BkBod_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCustomSize.CheckedChanged
        impositionRDB_Changed(rdbCustomSize)
    End Sub

    Private Sub rdbFourUp_CheckedChanged(sender As Object, e As EventArgs) Handles rdbFourUp.CheckedChanged
        impositionRDB_Changed(rdbFourUp)
    End Sub

    Private Sub rdbFullBleed_CheckedChanged(sender As Object, e As EventArgs) Handles rdbFullBleed.CheckedChanged
        impositionRDB_Changed(rdbFullBleed)
    End Sub

    Private Sub rdbQuarterNSideBySide_CheckedChanged(sender As Object, e As EventArgs) Handles rdbQuarterNSideBySide.CheckedChanged
        impositionRDB_Changed(rdbQuarterNSideBySide)
    End Sub

    Private Sub rdbCDC_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCDC.CheckedChanged
        impositionRDB_Changed(rdbCDC)
    End Sub
    Private Sub rdbYCC_CheckedChanged(sender As Object, e As EventArgs) Handles rdbYCC.CheckedChanged
        impositionRDB_Changed(rdbYCC)
    End Sub
    Private Sub rdbBGC_CheckedChanged(sender As Object, e As EventArgs) Handles rdbBGC.CheckedChanged
        impositionRDB_Changed(rdbBGC)
    End Sub
    Private Sub rdbCDC_XL_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCDC_XL.CheckedChanged
        impositionRDB_Changed(rdbCDC_XL)
    End Sub

    Private Sub rdbBooklet_CheckedChanged(sender As Object, e As EventArgs) Handles rdbBooklet.CheckedChanged
        impositionRDB_Changed(rdbBooklet)
    End Sub

    Private Sub rdbMiniPam_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMiniPam.CheckedChanged
        impositionRDB_Changed(rdbMiniPam)
    End Sub
    'Private Sub impositionRDB_Changed(sender As Object, e As EventArgs)
    'rdbFourUp.CheckedChanged,
    '                                                                           rdbQuarterNSideBySide.CheckedChanged,
    '                                                                           rdbFullBleed.CheckedChanged,
    '                                                                           rdbMiniPam.CheckedChanged

    Private Sub impositionRDB_Changed(ByVal aRadioButton As RadioButton)
        If aRadioButton.Checked = True Then
            ClearRdbInImposition(aRadioButton)
            CheckSizeAgainstRDB(False)

            'when a radio button is selected, it shows the layout picture
            PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
            PictureBox3.SizeMode = PictureBoxSizeMode.Zoom
            Try
                Dim Rsc As String = My.Settings.dirResources

                Select Case True
                    Case rdbFourUp.Checked
                        PictureBox2.Load(Rsc & "BkLayout01.png")
                        PictureBox3.Load(Rsc & "BkLayout02.png")

                    Case rdbQuarterNSideBySide.Checked
                        PictureBox2.Load(Rsc & "Cheap01.png")
                        PictureBox3.Load(Rsc & "Cheap02.png")

                    Case rdbFullBleed.Checked
                        PictureBox2.Load(Rsc & "FullBleed1.png")
                        PictureBox3.Load(Rsc & "FullBleed2.png")

                    Case rdbBooklet.Checked
                        PictureBox2.Load(Rsc & "PicNotFound.jpg")
                        PictureBox3.Load(Rsc & "PicNotFound.jpg")

                    Case rdbMiniPam.Checked
                        PictureBox2.Load(Rsc & "PicNotFound.jpg")
                        PictureBox3.Load(Rsc & "PicNotFound.jpg")

                    Case rdb12x18MiniBook.Checked
                        PictureBox2.Load(Rsc & "PicNotFound.jpg")
                        PictureBox3.Load(Rsc & "PicNotFound.jpg")

                    Case rdbCustomSize.Checked
                        PictureBox2.Load(Rsc & "PicNotFound.jpg")
                        PictureBox3.Load(Rsc & "PicNotFound.jpg")

                End Select

            Catch ex As Exception
                LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            End Try
        End If


    End Sub
#End Region


End Class