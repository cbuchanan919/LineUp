Imports System.IO

Imports iTextSharp.text.pdf
Imports iTextSharp.text

Public Class PdfJobTicketIO
#Region "Properties"
    'used to create the pdf of the Invoice (personalized Tab)
    Private Property strInvoiceTxt As String = ""
    Private Property mainPDFFont As iTextSharp.text.pdf.BaseFont = BaseFont.CreateFont(("C:\Windows\Fonts\courbd.ttf"), BaseFont.WINANSI, BaseFont.EMBEDDED) 'Current.Server.MapPath
    Private Property Font30 As New iTextSharp.text.Font(mainPDFFont, 30)
    Private Property Font22 As New iTextSharp.text.Font(mainPDFFont, 22)
    Private Property Font18 As New iTextSharp.text.Font(mainPDFFont, 18)
    Private Property Font12 As New iTextSharp.text.Font(mainPDFFont, 12)
    Private Property Font11 As New iTextSharp.text.Font(mainPDFFont, 11)
    Private Property Font10 As New iTextSharp.text.Font(mainPDFFont, 10)
    Private Property Font7_5 As New iTextSharp.text.Font(mainPDFFont, 7.5)


    Private Property CalibriBoldFont As BaseFont = BaseFont.CreateFont(Path.Combine(My.Settings.dirResources, "CALIBRIB_1.TTF"), BaseFont.WINANSI, BaseFont.EMBEDDED) 'Current.Server.MapPath
    Private Property CalibriFont As BaseFont = BaseFont.CreateFont(Path.Combine(My.Settings.dirResources, "CALIBRI_1.TTF"), BaseFont.WINANSI, BaseFont.EMBEDDED) 'Current.Server.MapPath



#End Region

#Region "Init"


#End Region


#Region "Methods"


    Public Function CreatePDF(ByVal txtPath As String, ByVal title As String, ByVal pdfPath As String) As Boolean
        Dim success As Boolean = False
        'keeps mx on the front
        If Not txtPath.Contains("Mx") Then
            txtPath = "Mx" & txtPath
        End If
        'Dim FileLocation As String = My.Settings.dirMxOrders & txtPath & ".txt"
        'Dim PDFFileLocation As String = FileLocation.Replace(".txt", ".pdf")

        'uses the value passed to it to create the pdf

        'strBTP and stroldBTP are strings that it tries to find and replace in the first line. Used to make a larger font for BTP
        Dim strBTP As String = ("BIBLE TRUTH PUBLISHERS")
        Dim stroldBTP As String = ("                              BIBLE TRUTH PUBLISHERS                                                          ")
        Dim AddDepositNumber As String = ""

        Try
            If File.Exists(txtPath) Then


                'reads the invoice txt file to string
                'MsgBox(txtPath & vbCrLf & title & vbCrLf & pdfPath)
                strInvoiceTxt = ""
                Dim lines As New List(Of String)
                lines.AddRange(File.ReadAllText(txtPath).Split(vbLf).ToList)
                For Each myLine As String In lines
                    myLine = myLine.TrimEnd
                    strInvoiceTxt &= myLine & vbLf
                Next
                'Dim fileReader As New System.IO.StreamReader(txtPath)
                'Dim stringReader As String = fileReader.ReadLine()
                'Do While stringReader.Contains("  ") = True
                '    stringReader = stringReader.Replace("  ", " ")
                'Loop



                'creates letter sized pdf
                Using fs As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
                    Using doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 18, 18, 18, 18)

                        Dim pdfWriter As pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs)
                        'Dim Table As New PdfPTable(3)
                        'Table.AddCell(New Phrase(
                        Dim paragraph As New Paragraph
                        Dim BTP As New Paragraph

                        paragraph.Font = Font11
                        paragraph.SetLeading(1.0F, 1.05F)
                        paragraph.Alignment = 0

                        BTP.Font = Font18
                        BTP.SetLeading(1.0F, 1.1F)
                        BTP.Alignment = 1
                        BTP.Add(strBTP)
                        doc.AddTitle(title)
                        doc.AddAuthor("Bible Truth Publishers")
                        doc.Open()
                        'If strInvoiceTxt.Contains(stringReader) Then

                        'End If

                        'Makes the BibleTruthPublishers larger
                        If strInvoiceTxt.Contains(stroldBTP) Then
                            strInvoiceTxt = strInvoiceTxt.Remove(0, stroldBTP.Count)
                            paragraph.Add(strInvoiceTxt)
                            doc.Add(BTP)
                            doc.Add(paragraph)
                        Else

                            paragraph.Add(strInvoiceTxt)
                            doc.Add(paragraph)
                        End If
                        doc.Close()
                        success = true
                    End Using
                End Using

            End If
        Catch ex As Exception
            'log.addError("Error creating " & title & "." & vbCrLf & ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            '' MsgBox(ex.Message.ToString & vbCrLf & "CreatePDF")
            'UpdateStatus("Something went wrong, I couldn't create a pdf of the the invoice. :(", True)
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "PdfJobTicketIO.CreatePDF")
        End Try
        Return success

    End Function



    Public Function CreateJobTicketLetterSize(ByVal filePath As String,
                                              ByVal totalQuantity As Integer,
                                              ByVal add10PercentExtra As Boolean,
                                              ByVal specialInstructions As String,
                                              ByVal MyJQRowInfo As JQRowInfo) As Boolean

        'MsgBox("Skipping create job ticket")
        'Return False

        Dim success As Boolean = True
        Try

            Dim ProdDirIO As New JQProductionIO

            If IsNothing(MyJQRowInfo.uvProdInfo) Then
                MyJQRowInfo.uvProdInfo = New UvProductInfo(False) 'keeps errors at bay for items with no item number.
            End If
            'Dim tmpInfo As New JobQRow
            'tmpInfo.ItemNumber = ItemNum
            Dim imgPath As String = ProdDirIO.GetPicPath(MyJQRowInfo, My.Settings.dirWebProd)



            Dim mainFont As New iTextSharp.text.Font(CalibriFont, 11)
            Dim MainFontBold As New iTextSharp.text.Font(CalibriBoldFont, 11)
            Dim largeFont As New iTextSharp.text.Font(CalibriFont, 18)
            If File.Exists(filePath) Then
                File.Delete(filePath)
            End If

            Using fs As New System.IO.FileStream(filePath, System.IO.FileMode.Create)
                Using doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 18, 18, 18, 18)
                    Dim pdfWriter As PdfWriter = PdfWriter.GetInstance(doc, fs)
                    doc.Open()
                    doc.NewPage()

                    '------ adds the header to the job ticket -----
                    Dim headerTable As New PdfPTable(2)
                    headerTable.DefaultCell.HorizontalAlignment = 1
                    headerTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER
                    headerTable.WidthPercentage = 100

                    If imgPath = "" Then
                        headerTable.AddCell(CreateCell("", mainFont, CellAlign:=0, RowSpan:=2))
                    Else
                        headerTable.AddCell(CreateCell("", mainFont, CellAlign:=0, ImagePath:=imgPath, RowSpan:=2))
                    End If

                    headerTable.AddCell(CreateCell(MyJQRowInfo.uvProdInfo.ItemNum & " - " & "Job Ticket", largeFont, 2))
                    headerTable.CompleteRow()


                    headerTable.AddCell(CreateCell("Created " & Date.Today.ToShortDateString, mainFont, 2))
                    headerTable.CompleteRow()




                    doc.Add(headerTable)
                    '----------------------------------------------

                    '----- adds product info to the job ticket -----
                    Dim prodInfoTable As New PdfPTable(6)
                    prodInfoTable.DefaultCell.HorizontalAlignment = 1
                    prodInfoTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER
                    prodInfoTable.WidthPercentage = 100
                    With prodInfoTable

                        .AddCell(CreateCell(" ", largeFont)) 'spacing...
                        .CompleteRow()

                        .AddCell(CreateCell("Item Number:", mainFont, 0))
                        .AddCell(CreateCell(MyJQRowInfo.uvProdInfo.ItemNum, mainFont, 0))
                        .CompleteRow()

                        .AddCell(CreateCell("Title:", mainFont, 0))
                        .AddCell(CreateCell(MyJQRowInfo.Title, mainFont, 0, ColumnSpan:=5))
                        .CompleteRow()

                        .AddCell(CreateCell("Copies Needed:", mainFont, 0))
                        .AddCell(CreateCell(totalQuantity & " Final Copies", largeFont, 0, ColumnSpan:=5))
                        .CompleteRow()

                        .AddCell(CreateCell("Final Dimensions:", mainFont, 0))
                        .AddCell(CreateCell(MyJQRowInfo.uvProdInfo.PageSize, mainFont, 0, ColumnSpan:=5))
                        .CompleteRow()

                        .AddCell(CreateCell("Type:", mainFont, 0))
                        .AddCell(CreateCell(MyJQRowInfo.uvProdInfo.Type, mainFont, 0, ColumnSpan:=5))
                        .CompleteRow()

                        .AddCell(CreateCell("UV Type:", mainFont, 0))
                        .AddCell(CreateCell(MyJQRowInfo.uvProdInfo.convertTypeUV, mainFont, 0, ColumnSpan:=5))
                        .CompleteRow()


                        If specialInstructions <> "" Then
                            .AddCell(CreateCell("Special Instructions:", mainFont, 0))
                            .AddCell(CreateCell(specialInstructions, MainFontBold, 0, ColumnSpan:=5))
                            .CompleteRow()
                        End If

                        .AddCell(CreateCell(" ", largeFont)) 'spacing...
                        .CompleteRow()

                        Dim ruleOfThumb As String = "Production Printers: Tray 1 is for slip sheets, Tray 3 is for body paper, Tray 5 is for covers."
                        .AddCell(CreateCell("Rule of Thumb:", mainFont, 0))
                        .AddCell(CreateCell(ruleOfThumb, mainFont, 0, ColumnSpan:=5))
                        .CompleteRow()


                        MyJQRowInfo.ProductionFiles.Sort(Function(y, x) y.ProductBodyOrCover.CompareTo(x.ProductBodyOrCover))
                        For Each myFile As JQProductionFileInfo In MyJQRowInfo.ProductionFiles
                            If IsNothing(myFile.PdfSpecs) Then
                                myFile.PdfSpecs = New Utilities.PdfInfo(myFile.ProductionFile.OriginalFile.FullName)
                            End If
                            myFile.CalculatePaperWeight()

                            .AddCell(CreateCell(" ", largeFont)) 'spacing...
                            .CompleteRow()

                            .AddCell(CreateCell(" ", largeFont)) 'spacing...
                            .CompleteRow()

                            Dim layerQuan As Integer = 0
                            Dim reasoning As String = MyJQRowInfo.xUpReasoning(totalQuantity, myFile.CopiesPerLayer, add10PercentExtra, layerQuan)

                            .AddCell(CreateCell(myFile.ProductBodyOrCover.ToString & "     -", largeFont, CellAlign:=0))
                            .AddCell(CreateCell(layerQuan & " Layers.", largeFont, 0))
                            .CompleteRow()

                            .AddCell(CreateCell(reasoning, mainFont, 0, ColumnSpan:=6))
                            .CompleteRow()

                            .AddCell(CreateCell("Production File:", mainFont, 0))
                            .AddCell(CreateCell(myFile.ProductionFile.OriginalFile.FullName, mainFont, 0, ColumnSpan:=5))
                            .CompleteRow()

                            .AddCell(CreateCell("Paper to Use:", mainFont, 0))
                            .AddCell(CreateCell(myFile.CalculatePaperWeight, mainFont, 0, ColumnSpan:=2))
                            .CompleteRow()

                            .AddCell(CreateCell("Page Size:", mainFont, 0))
                            .AddCell(CreateCell(myFile.PdfSpecs.pageWidth & "x" & myFile.PdfSpecs.pageHeight, mainFont, 0))
                            .CompleteRow()

                            If myFile.ProductBodyOrCover = BodyVsCover.Body Then
                                .AddCell(CreateCell("Page Count:", mainFont, 0))
                                Dim sheetCt As Integer = Math.Ceiling(myFile.PdfSpecs.pageCount / 2)
                                Dim sheetCtPl As String = " (" & sheetCt & " Sheets)"
                                If sheetCt = 1 Then sheetCtPl = " (" & sheetCt & " Sheet)"

                                .AddCell(CreateCell(myFile.PdfSpecs.pageCount & sheetCtPl, mainFont, 0, ColumnSpan:=5))
                                .CompleteRow()
                            End If

                            .AddCell(CreateCell("Total Sheet Count:", mainFont, 0))
                            .AddCell(CreateCell(myFile.CalculateSheetCount(layerQuan), mainFont, 0, ColumnSpan:=5))
                            .CompleteRow()
                        Next



                        .AddCell(CreateCell(" ", largeFont)) 'spacing...
                        .CompleteRow()

                        .AddCell(CreateCell(" ", largeFont)) 'spacing...
                        .CompleteRow()

                        .AddCell(CreateCell("(The instructions on this page are a guide, and are quite possibly wrong...)", mainFont, 1, ColumnSpan:=6)) 'spacing...
                        .CompleteRow()

                    End With


                    doc.Add(prodInfoTable)

                    '-----------------------------------------------




                    doc.AddTitle("Job Ticket")
                    doc.AddAuthor("Bible Truth Publishers")

                    doc.Close()
                End Using
            End Using


        Catch ex As Exception
            success = False
            MsgBox(ex.Message)
        End Try





        Return success
    End Function


    ''' <summary>
    ''' Creates a label based on the selected status in the 'status' dgv, and returns the file path if successful. If failed / canceled, returns ""
    ''' </summary>
    ''' <param name="MyJQBarcodeIO"></param>
    ''' <returns></returns>
    Public Function CreateStatusLabelsFromStatusDGV(ByVal MyJQBarcodeIO As JQBarcodeIO) As String
        Dim labelsFP As String = ""
        Dim bCodesDlg As New frmBarcodes(MyJQBarcodeIO)
        If bCodesDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                labelsFP = My.Settings.dirResources & "Labels.pdf"
                If File.Exists(labelsFP) Then
                    File.Delete(labelsFP)
                End If

                'Dim MedFont As New iTextSharp.text.Font(yourFont, 10)
                '3x4 page size
                Dim pgSize As New iTextSharp.text.Rectangle(288.0F, 216.0F)

                Using fs As New System.IO.FileStream(labelsFP, System.IO.FileMode.Create)
                    Using doc As New iTextSharp.text.Document(pgSize, 9, 9, 9, 9)

                        Dim pdfWriter As pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs)
                        doc.Open()
                        Dim s As String = ""
                        For Each myrow As DataGridViewRow In bCodesDlg.dgvBarcode.SelectedRows
                            If Not IsNothing(myrow.Cells(1).Value) Then
                                Dim bc As JQBarcodeInfo = MyJQBarcodeIO.GetBarcode(myrow.Cells(1).Value)
                                bc.CreateBarcode()

                                doc.NewPage()

                                Dim aTable As New PdfPTable(3)
                                aTable.DefaultCell.HorizontalAlignment = 1
                                aTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER
                                aTable.WidthPercentage = 100

                                'Add Barcode
                                aTable.AddCell(CreateCell("", Font10, , , 3, bc.FinalFilePath, 175, 250, 50))

                                'Design ID
                                aTable.AddCell(CreateCell(bc.BarcodeText, Font30, 1, , 3))


                                doc.Add(aTable)
                                aTable = Nothing
                            End If

                        Next

                        ''goes through the currently selected Mx numbers and creates labels for each
                        'For i As Integer = 0 To cmbStatus.Items.Count - 1 'lstmxnumber.count



                        'Next

                        'Next
                        doc.AddTitle("Labels")
                        doc.AddAuthor("Bible Truth Publishers")

                        doc.Close()
                    End Using
                End Using
            Catch ex As Exception

            End Try
        End If

        Return labelsFP
    End Function

    ''' <summary>
    ''' Creates a 3x4 inch QP Room job ticket (Note. Important to load production files first)
    ''' </summary>
    ''' <param name="jobs"></param>
    ''' <param name="LabelPath"></param>
    ''' <returns></returns>
    Public Function CreateJQTicket(ByVal jobs As List(Of JQRowInfo), ByVal MyJQProductionIO As JQProductionIO, Optional ByRef LabelPath As String = "") As Boolean
        Dim success As Boolean = False
        Try
            LabelPath = Path.Combine(My.Settings.dirResources, "JobTicketLabels.pdf")
            Dim prompt As New Text.StringBuilder
            For Each job As JQRowInfo In jobs
                If job.JobTicketWasPrinted Then
                    prompt.AppendLine(job.getSummaryStr)
                    prompt.AppendLine(String.Join(vbCrLf, job.JobTicketHistory))
                End If
            Next

            Dim jobsToProcess As New List(Of JQRowInfo)
            Dim result As MsgBoxResult = MsgBoxResult.Yes 'default add all.
            If prompt.ToString <> "" Then
                result = MsgBox("It looks like the following job tickets were printed already. Print again?" & vbCrLf & vbCrLf & prompt.ToString, MsgBoxStyle.YesNoCancel)
            End If

            If result = MsgBoxResult.Cancel Then
                LabelPath = ""
                Return False
            End If

            For Each job As JQRowInfo In jobs
                If result = MsgBoxResult.Yes Then
                    'adds it regardless
                    jobsToProcess.Add(job)
                ElseIf Not job.JobTicketWasPrinted Then
                    'if it's no, only add the jobs that weren't printed yet
                    jobsToProcess.Add(job)
                End If
            Next

            If jobsToProcess.Count > 0 Then

                '3x4 page size
                Dim pgSize As New iTextSharp.text.Rectangle(288.0F, 216.0F)
                Dim mainFont As New iTextSharp.text.Font(CalibriFont, 11)
                Dim MainFontBold As New iTextSharp.text.Font(CalibriBoldFont, 11)

                Using fs As New System.IO.FileStream(LabelPath, System.IO.FileMode.Create)
                    Using doc As New iTextSharp.text.Document(pgSize, 9, 9, 9, 9)

                        Dim pdfWriter As pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs)
                        doc.Open()


                        For Each job As JQRowInfo In jobsToProcess
                            If IsNothing(job.uvProdInfo) Then
                                job.uvProdInfo = New UvProductInfo(False) 'keeps errors at bay for jobs with no item number.
                            End If

                            doc.NewPage()
                            Dim aTable As New PdfPTable(3)
                            aTable.DefaultCell.HorizontalAlignment = 1
                            aTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER
                            aTable.WidthPercentage = 100

                            ' Product Number
                            aTable.AddCell(CreateCell("BTP#" & job.ItemNumber, MainFontBold, 0))
                            ' Job ID
                            aTable.AddCell(CreateCell($"(JQ#{ job.JobID })", Font7_5))
                            ' Quantity
                            aTable.AddCell(CreateCell("Quan: " & job.OrderQuantity, MainFontBold, 2))

                            ' Title
                            aTable.AddCell(CreateCell("Title: ", mainFont, 2))
                            aTable.AddCell(CreateCell(job.Title, mainFont, 0, ColumnSpan:=2))

                            ' Author
                            aTable.AddCell(CreateCell("Author: ", mainFont, 2))
                            aTable.AddCell(CreateCell(job.uvProdInfo.Author, mainFont, 0, ColumnSpan:=2))

                            ' Date Created
                            aTable.AddCell(CreateCell("Order Placed:", mainFont, 2))
                            aTable.AddCell(CreateCell(job.OrderPlaced, mainFont, 0, ColumnSpan:=2))


                            ' spacer
                            aTable.AddCell(CreateCell(" ", Font7_5))
                            aTable.CompleteRow()

                            For Each prodFile As JQProductionFileInfo In job.ProductionFiles
                                ' add covers
                                If IsNothing(prodFile.PdfSpecs) Then prodFile.PdfSpecs = New Utilities.PdfInfo(prodFile.ProductionFile.OriginalFile.FullName)
                                If prodFile.ProductBodyOrCover = BodyVsCover.Cover Then
                                    aTable.AddCell(CreateCell(PrinterCategoryToString(prodFile.MyPrinterCategory) & ":", MainFontBold, 2, )) 'prodFile.ProductBodyOrCover.ToString 
                                    aTable.AddCell(CreateCell(prodFile.GetNewFileName(job.ItemNumber, job.OrderQuantity), mainFont, 0, ColumnSpan:=2))
                                    aTable.CompleteRow()
                                End If
                            Next

                            ' spacer
                            aTable.AddCell(CreateCell(" ", Font7_5))
                            aTable.CompleteRow()

                            For Each prodFile As JQProductionFileInfo In job.ProductionFiles
                                ' add Bodies
                                If prodFile.ProductBodyOrCover = BodyVsCover.Body Then
                                    aTable.AddCell(CreateCell(PrinterCategoryToString(prodFile.MyPrinterCategory) & ":", MainFontBold, 2, )) ' prodFile.ProductBodyOrCover.ToString 
                                    aTable.AddCell(CreateCell(prodFile.GetNewFileName(job.ItemNumber, job.OrderQuantity), mainFont, 0, ColumnSpan:=2))
                                    aTable.CompleteRow()
                                End If
                            Next



                            'For Each prodFile As JQProductionFileInfo In job.ProductionFiles
                            '    If IsNothing(prodFile.PdfSpecs) Then prodFile.PdfSpecs = New Utilities.PdfInfo(prodFile.ProductionFile.OriginalFile.FullName)

                            '    ' blank row
                            '    aTable.AddCell(CreateCell(" ", mainFont))
                            '    aTable.CompleteRow()

                            '    ' cover / body file
                            '    aTable.AddCell(CreateCell(prodFile.ProductBodyOrCover.ToString & ":", MainFontBold, 2, ))
                            '    aTable.AddCell(CreateCell($"({prodFile.CalculateLayerCount(job.OrderQuantity, True)} Layers )", mainFont, 0, ColumnSpan:=2))
                            '    aTable.CompleteRow()

                            '    ' paper size
                            '    aTable.AddCell(CreateCell("Paper Size:", mainFont, 2))
                            '    aTable.AddCell(CreateCell(prodFile.PdfSpecs.pageWidth & " x " & prodFile.PdfSpecs.pageHeight, mainFont, 0))
                            '    aTable.CompleteRow()

                            '    ' paper weight
                            '    aTable.AddCell(CreateCell("Paper Weight:", mainFont, 2))
                            '    aTable.AddCell(CreateCell(prodFile.CalculatePaperWeight, mainFont, 0))
                            '    aTable.CompleteRow()


                            'Next
                            doc.Add(aTable)

                            'get table height
                            Dim ct As Integer = aTable.Rows.Count
                            Dim height As Single = 0
                            For i As Integer = 0 To ct - 1
                                height += aTable.GetRowHeight(i)
                            Next
                            Dim SpaceAboveImage As Double = 10
                            Dim difference As Double = doc.PageSize.Height - (height + 28 + SpaceAboveImage)
                            If difference > 0 Then
                                Dim fp As String = MyJQProductionIO.GetPicPath(job, My.Settings.dirWebProd)
                                Dim picTable As New PdfPTable(2)
                                picTable.AddCell(CreateCell("", Font10, 2, 0, 2, ImagePath:=fp, ImageHeight:=difference, SpaceBeforeImage:=SpaceAboveImage))
                                doc.Add(picTable)
                            End If
                            aTable.AddCell(CreateCell("test", Font10))
                            'MsgBox(height & vbCrLf & height / 72 & vbCrLf & doc.PageSize.Height / 72)

                            job.JobTicketHistory.Add(Environment.UserName & " - " & Now.ToString)
                        Next

                        doc.AddTitle("Job Ticket Labels")
                        doc.AddAuthor("Bible Truth Publishers")

                        doc.Close()
                        success = True
                    End Using
                End Using

            Else
                LabelPath = ""
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "CreateJQTicket")
        End Try

        Return success

    End Function


    ''' <summary>
    ''' Creates the Personalize Label, and returns the file path if successful. If failed / canceled, returns ""
    ''' </summary>
    ''' <param name="selectedPersonalizedIDs"></param>
    ''' <returns></returns>
    Public Function CreatePersonalizedJobLabelPdf(ByVal selectedPersonalizedIDs As Dictionary(Of PersonalizeRowInfo, PersonalizeMxInfo)) As String
        Dim LabelPath As String = Path.Combine(My.Settings.dirResources, "Labels.pdf")
        Try
            Dim prompt As New Text.StringBuilder
            prompt.AppendLine("It looks like the following ID(s) were already printed, print anyway?" & vbCrLf)
            Dim ShowPrompt As Boolean = False
            Dim LabelCounter As Integer = 0

            For Each selID As PersonalizeRowInfo In selectedPersonalizedIDs.Keys

                If selID.LabelHistory.Count = 0 Then
                    selID.CreateLabels = True
                    LabelCounter += 1
                Else
                    ShowPrompt = True
                    For Each history As String In selID.LabelHistory
                        prompt.AppendLine(selID.MxNumber & " - " & selID.DesignId & " - " & history)
                    Next
                    prompt.AppendLine()
                    selID.CreateLabels = False
                End If
            Next


            If ShowPrompt = True Then
                Dim result As MsgBoxResult = MsgBox(prompt.ToString, MsgBoxStyle.YesNoCancel, "Reprint labels?")
                If result = MsgBoxResult.Yes Then
                    For Each selOrder In selectedPersonalizedIDs.Keys
                        If selOrder.CreateLabels = False Then
                            selOrder.CreateLabels = True
                            LabelCounter += 1
                        End If
                    Next
                ElseIf result = MsgBoxResult.Cancel Then
                    Return ""
                End If
            End If

            If LabelCounter = 0 Then
                Return ""
            End If

            '3x4 page size
            Dim pgSize As New iTextSharp.text.Rectangle(288.0F, 216.0F)


            Using fs As New System.IO.FileStream(LabelPath, System.IO.FileMode.Create)
                Using doc As New iTextSharp.text.Document(pgSize, 9, 9, 9, 9)

                    Dim pdfWriter As pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs)
                    doc.Open()
                    Dim s As String = ""
                    Dim Shots As String = ""
                    Shots =
                        "__ Staples " & vbTab & "__ Toner " & vbTab &
                        "__ Holes " & vbTab & "__ Shift " & vbTab &
                        "__ Order " & vbTab & "__ Initials"


                    'goes through the currently selected Mx numbers and creates labels for each
                    For Each selOrder As PersonalizeRowInfo In selectedPersonalizedIDs.Keys

                        Try
                            With selOrder
                                If selOrder.CreateLabels Then
                                    'gets the xml info for the current item
                                    'If Not mxOrdersDict.ContainsKey(selOrder.MxNumberNoMX) Then
                                    '    mxOrdersDict.Add(selOrder.MxNumberNoMX, New PersonalizeMxInfo(selOrder.MxNumberNoMX))
                                    'End If

                                    Dim orderInfo As PersonalizeMxInfo = selectedPersonalizedIDs(selOrder) 'the current mx info

                                    Dim IDSplit() As String = .DesignId.Split("-")
                                    Dim BarcodePath As String = My.Settings.dirMxOrders & IDSplit(2) & ".png"
                                    Dim TotalQuantity As Integer = 0
                                    Dim Remainder As Integer = 0
                                    Dim QuanPerBox As Integer = 0
                                    Dim BoxOf As Double = 0
                                    Dim CurBox As Integer = 0
                                    Select Case .ItemNumber
                                        Case "8101"
                                            TotalQuantity = .DesignQuantity
                                            If TotalQuantity <= 100 Then
                                                QuanPerBox = TotalQuantity
                                            Else
                                                QuanPerBox = 100
                                            End If
                                            ' Case "7427", "6880", "6120"
                                        Case Else
                                            TotalQuantity = .DesignQuantity
                                            QuanPerBox = TotalQuantity
                                            Shots = ""

                                    End Select
                                    Remainder = TotalQuantity
                                    BoxOf = TotalQuantity / QuanPerBox
                                    '"rounds up" the box count so that it will show the correct number of boxes
                                    If BoxOf.ToString.Contains(".") Then
                                        Dim Str As String = BoxOf
                                        BoxOf = Str.Split(".")(0) + 1
                                    End If



                                    Do While Remainder > 0
                                        doc.NewPage()
                                        CurBox += 1
                                        Dim aTable As New PdfPTable(3)
                                        aTable.DefaultCell.HorizontalAlignment = 1
                                        aTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER
                                        aTable.WidthPercentage = 100

                                        'BTP Number
                                        aTable.AddCell(CreateCell("#" & .ItemNumber, Font10))

                                        'current box Number
                                        aTable.AddCell(CreateCell("Box " & CurBox & " of " & BoxOf, Font10))

                                        'quantity per box
                                        aTable.AddCell(CreateCell("Q/B: " & QuanPerBox, Font10))

                                        'adds the MxNumber
                                        aTable.AddCell(CreateCell("Mx" & orderInfo.MxNumberNoMx, Font22, 0, , 2))

                                        'Total Quantity Ordered
                                        aTable.AddCell(CreateCell("Order Of: " & TotalQuantity, Font10))

                                        'Add Barcode
                                        aTable.AddCell(CreateCell("", Font10, , , 3, BarcodePath))

                                        'Design ID
                                        aTable.AddCell(CreateCell("ID: " & .DesignId, Font22, 0, , 3))

                                        'Customer Name
                                        aTable.AddCell(CreateCell("Cus# " & orderInfo.BillName, Font10, 0, , 3, ))

                                        'Receipt Number
                                        aTable.AddCell(CreateCell("R# " & orderInfo.ReceiptNumber, Font10, , , , ))
                                        aTable.CompleteRow()

                                        'ie. it's not a gop calendar
                                        If Shots = "" Then
                                            'ship note
                                            aTable.AddCell(CreateCell("Ship Note: " & orderInfo.ShipNote, Font7_5, 0, , 2, ))
                                            aTable.CompleteRow()
                                            aTable.AddCell(CreateCell("", Font7_5, 2, , 3, My.Settings.dirResources & "BTPLogo.png", 75, 100, 10))

                                        Else
                                            'ship note
                                            aTable.AddCell(CreateCell("Ship Note: " & orderInfo.ShipNote, Font7_5, 0, , 3, ))
                                            'Shots
                                            aTable.AddCell(CreateCell(Shots, Font7_5, 0, , 3))
                                            aTable.AddCell(CreateCell("", Font7_5, 2, , 3, My.Settings.dirResources & "BTPLogo.png", 75, 100, 10))
                                        End If


                                        doc.Add(aTable)
                                        aTable = Nothing
                                        s &= orderInfo.BillAddress & vbCrLf
                                        Remainder -= QuanPerBox
                                        If Remainder < QuanPerBox Then
                                            QuanPerBox = Remainder
                                        End If
                                    Loop

                                    'updates the status on the JobQ
                                    selOrder.LabelHistory.Add(System.Environment.UserName & " - " & Now.ToString)
                                    'dgvPersonalizeJobQ.Rows(.RowIndex).Cells(10).Value &= System.Environment.UserName & " - " & Date.Today.ToString("d") & "; "

                                    'selOrder.LabelHistory.Add(Environment.UserName & " - " & Now.ToShortDateString)
                                End If
                            End With
                            'Only Prints the label if it's marked as good to print

                        Catch ex As Exception
                            '  log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Creating Job Ticket")
                        End Try

                    Next

                    doc.AddTitle("Labels")
                    doc.AddAuthor("Bible Truth Publishers")

                    doc.Close()
                    Beep()
                End Using
            End Using


        Catch ex As Exception
            MsgBox(ex.Message.ToString & vbCrLf & "CreateLabelPDF")
            Return ""
        End Try
        Return LabelPath

    End Function


    ''' <summary>
    ''' creates an itextsharp table cell with the specified parameters
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <param name="myfont"></param>
    ''' <param name="CellAlign"></param>
    ''' <param name="CellBorder"></param>
    ''' <param name="ColumnSpan"></param>
    ''' <param name="ImagePath"></param>
    ''' <param name="ImageHeight"></param>
    ''' <param name="ImageWidth"></param>
    ''' <param name="SpaceBeforeImage"></param>
    ''' <param name="CellBackgroundColor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateCell(ByVal Text As String,
                                ByVal myfont As iTextSharp.text.Font,
                                Optional ByVal CellAlign As Integer = 1,
                                Optional ByVal CellBorder As Integer = 0,
                                Optional ByVal ColumnSpan As Integer = 1,
                                Optional ByVal ImagePath As String = "",
                                Optional ByVal ImageHeight As Double = 125.0,
                                Optional ByVal ImageWidth As Double = 150.0,
                                Optional ByVal SpaceBeforeImage As Double = 0,
                                Optional ByVal CellBackgroundColor As iTextSharp.text.BaseColor = Nothing,
                                Optional ByVal RowSpan As Integer = 1)
        Dim myCell As New PdfPCell

        Try
            If ImagePath <> "" AndAlso File.Exists(ImagePath) Then
                Dim myImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ImagePath)
                myImage.ScaleToFit(ImageWidth, ImageHeight)
                myImage.SpacingBefore = SpaceBeforeImage
                'myImage.SpacingAfter = 10.0F
                myImage.Alignment = Element.ALIGN_CENTER
                myCell = New PdfPCell(myImage)
                myCell.HorizontalAlignment = CellAlign
                myCell.Border = CellBorder
                myCell.Colspan = ColumnSpan
                myCell.Rowspan = RowSpan
                If Not IsNothing(CellBackgroundColor) Then
                    myCell.BackgroundColor = CellBackgroundColor
                End If

            Else
                'MsgBox(ImagePath)
                Dim myPhrase As New Phrase
                myPhrase.Font = myfont
                myPhrase.Add(Text)
                myCell = New PdfPCell(myPhrase)
                myCell.HorizontalAlignment = CellAlign
                myCell.Border = CellBorder
                myCell.Colspan = ColumnSpan
                myCell.Rowspan = RowSpan
                If Not IsNothing(CellBackgroundColor) Then
                    myCell.BackgroundColor = CellBackgroundColor
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return myCell

    End Function




#End Region
End Class
