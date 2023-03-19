Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text


Public Class PdfImposition


#Region "Properties and Declarations"


    ''' <summary>
    ''' the blank page provider.
    ''' </summary>
    Private readerBlankpg As PdfReader

    '---------------------------------- Used for standard books ----------------------------------
    'key for list below(width, skew, skew, height, side to side placement, up & down placement)
    Private EvenTopLeft() As Single = {1.0F, 0.0F, 0.0F, 1.0F, 36.0F, 630.0F} 'ConvSngl(0.5), ConvSngl(8.75)} '
    Private EvenTopRight() As Single = {1.0F, 0.0F, 0.0F, 1.0F, 432.0F, 630.0F}
    Private EvenBotLeft() As Single = {1.0F, 0.0F, 0.0F, 1.0F, 36.0F, 18.0F}
    Private EvenBotRight() As Single = {1.0F, 0.0F, 0.0F, 1.0F, 432.0F, 18.0F}

    Private OddTopLeft() As Single = {1.0F, 0.0F, 0.0F, 1.0F, 0.0F, 630.0F}
    Private OddTopRight() As Single = {1.0F, 0.0F, 0.0F, 1.0F, 396.0F, 630.0F}
    Private OddBotLeft() As Single = {1.0F, 0.0F, 0.0F, 1.0F, 0.0F, 18.0F}
    Private OddBotRight() As Single = {1.0F, 0.0F, 0.0F, 1.0F, 396.0F, 18.0F}
    '-----------------------------------------------------------------------------------------------

    Public Const FourUp As String = "FourUp"
    Public Const Economy As String = "Economy"
    Public Const FullBleed As String = "FullBleed"
    Public Const Booklet12x18 As String = "12x18Booklet"
    'Public Const Pamphlet As String = "Pamphlet"
    Public Const MiniPamphlet As String = "MiniPamphlet"
    Public Const FourUp6x9 As String = "FourUp6x9"
    Public Const MiniBooklet3_75x5_5 As String = "MiniBooklet3.75x5.5"
    Public Const CustomSized4Up As String = "CustomSized4Up"
    Public Const ChristiansDaily As String = "ChristiansDaily"
    Public Const ChristiansDailyXL As String = "ChristiansDailyXL"
    Public Const BoysGirlsCalendar As String = "BoysGirlsCalendar"
    Public Const YoungChristianCalendar As String = "YoungChristianCalendar"


#End Region

#Region "Init"

    Public Sub New() 'ByVal BlankPageFilePath As String)
        readerBlankpg = New PdfReader(Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Utilities.BlankPage.pdf")) 'BlankPageFilePath)
        'Dim SR As New StreamReader( Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Utilities.RomNum.txt"))
    End Sub

#End Region

#Region "Main Methods"

    ''' <summary>
    ''' Saves a 11x17 pdf from the 5x8 pdf path given. 
    ''' "Economy" takes a 5x8 pdf and put its on (1 book per layer) on a 11x17 sheet. 
    ''' "FullBleed" takes a 5.25 x 8.25 pdf. 
    ''' "12x18Booklet" takes a 5.75 x 4.25 pdf. 
    ''' "FourUp6x9" takes a 6x9 pdf and puts it on a 12x18 sheet.
    ''' "MiniBooklet3.75x5.5" takes a 5x8 pdf and lays it out 8up on a 12x18 sheet.
    ''' </summary>
    ''' <param name="OriginalPdfPath"></param>
    ''' <param name="OutputPdfPath"></param>
    ''' <param name="ImpositionType">Imposition Types: "FourUp", "Economy", "FullBleed", "12x18Booklet", "MiniPamphlet", "FourUp6x9", "MiniBooklet3.75x5.5", "CustomSized4Up", "ChristiansDaily", "ChristiansDailyXL", "YoungChristianCalendar", "BoysGirlsCalendar"</param>
    ''' <returns>True if imposition was successful.</returns>
    ''' <remarks></remarks>
    Public Function Impose_5x8_PDF(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String, ByVal ImpositionType As String, ByRef ReturnedErrors As String) As Boolean
        Dim ImposeSuccess As Boolean = True

        Try
            If File.Exists(OriginalPdfPath) Then
                Select Case ImpositionType

                    Case FourUp
                        'creates a 11x17 file image
                        impose4UP(OriginalPdfPath, OutputPdfPath)

                    Case Economy
                        'creates a 11x17 file image
                        imposeEconomy(OriginalPdfPath, OutputPdfPath)

                    Case FullBleed
                        'creates a 11x17 file image
                        imposeFullBleed(OriginalPdfPath, OutputPdfPath)

                    Case Booklet12x18
                        'creates a 12x18 file image
                        imposeBooklet12x18(OriginalPdfPath, OutputPdfPath)

                    Case MiniPamphlet
                        'creates a 5.5x8.5 file image
                        imposeMiniPamphlet(OriginalPdfPath, OutputPdfPath)

                    Case CustomSized4Up
                        imposeCustomSized4Up(OriginalPdfPath, OutputPdfPath)

                    Case MiniBooklet3_75x5_5
                        'creates a 18x12 (landscape) file
                        imposeMiniBooklet3_75x5_5(OriginalPdfPath, OutputPdfPath)

                    Case ChristiansDaily
                        ImposeChristiansDaily(OriginalPdfPath, OutputPdfPath)

                    Case ChristiansDailyXL
                        'creates an oversized 12x18 pdf.
                        imposeChristiansDailyXL(OriginalPdfPath, OutputPdfPath)

                    Case BoysGirlsCalendar
                        ImposeStandardBlockCalendar(OriginalPdfPath, OutputPdfPath, True)

                    Case YoungChristianCalendar
                        ImposeStandardBlockCalendar(OriginalPdfPath, OutputPdfPath, False)

                        'Case FourUp6x9
                        '    no imposition yet...
                        'creates a 12x18 image file
                        ' document = CreateDoc(12, 18, DocTitle)
                    Case Else

                        ReturnedErrors &= "Imposition type not correctly given." & vbCrLf
                End Select
            Else
                Throw New FileNotFoundException("File not found", OriginalPdfPath)
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            ReturnedErrors &= ex.Message & vbCrLf
            ImposeSuccess = False
        End Try



        Return ImposeSuccess
    End Function

    Private Function getPdfReader(ByVal OriginalPdfPath As String) As PdfReader
        Return New PdfReader(OriginalPdfPath)
    End Function


    Private Sub AddtoPage(ByRef NewPDF As PdfContentByte, ByVal writer As PdfWriter, ByVal CurPageNum As Integer, ByVal ImgPlaceSpecs() As Single, ByVal pageCount As Integer, ByVal reader As PdfReader)
        Dim page As PdfImportedPage
        If CurPageNum <= pageCount Then
            page = writer.GetImportedPage(reader, CurPageNum)
        Else
            page = writer.GetImportedPage(readerBlankpg, 1)
        End If

        '1.0F, 0, 0, 1.0F, 36, 630
        NewPDF.AddTemplate(page, ImgPlaceSpecs(0), ImgPlaceSpecs(1), ImgPlaceSpecs(2), ImgPlaceSpecs(3), ImgPlaceSpecs(4), ImgPlaceSpecs(5))


    End Sub

    ''' <summary>
    ''' Converts inches as single to a iTextSharp integer (*72)
    ''' </summary>
    ''' <param name="Inches"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvSngl(ByVal Inches As Single) As Single
        Dim s As Single = Inches * 72
        'MsgBox("S:" & s & vbCrLf & "I:" & Inches)
        Return s
    End Function

    ''' <summary>
    ''' Rotates the pages clockwise to what it is now
    ''' </summary>
    ''' <param name="originalPdfPath">path of the original pdf</param>
    ''' <param name="desiredRotation">must be 0, 90, 180 or 270</param>
    ''' <param name="destinationPdfPath">path to save the destination pdf to</param>
    ''' <returns></returns>
    Private Function rotatePages(ByVal originalPdfPath As String, ByVal destinationPdfPath As String, ByVal desiredRotation As Integer) As Boolean
        Dim success As Boolean = True

        Try
            Dim reader As New PdfReader(originalPdfPath)
            Dim info As New PdfInfo(originalPdfPath)
            For n As Integer = 1 To info.pageCount Step +1
                Dim page As PdfDictionary = reader.GetPageN(n)
                Dim rotation As PdfNumber = page.GetAsNumber(PdfName.ROTATE)
                If Not IsNothing(rotation) Then
                    desiredRotation += rotation.IntValue()
                    desiredRotation = desiredRotation Mod 360
                End If
                page.Put(PdfName.ROTATE, New PdfNumber(desiredRotation))
            Next
            Dim stamper As New PdfStamper(reader, New FileStream(destinationPdfPath, FileMode.OpenOrCreate))
            stamper.Close()
            reader.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "RotatePages")
            success = False
        End Try

        Return success
    End Function

    ''' <summary>
    ''' Creates the base iTextSharp document.
    ''' </summary>
    ''' <param name="widthInInches"></param>
    ''' <param name="heightInInches"></param>
    ''' <param name="title"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDoc(ByVal widthInInches As Double, ByVal heightInInches As Double, ByVal title As String) As iTextSharp.text.Document

        Dim docHeight As Single = ConvSngl(heightInInches)
        Dim docWidth As Single = ConvSngl(widthInInches)
        Dim tSize As New Rectangle(docWidth, docHeight)

        Dim Doc As New Document(tSize, 0F, 0F, 0F, 0F)
        Doc.AddTitle(title)
        Doc.AddCreator("Bible Truth Publishers")
        Return Doc

    End Function

    ''' <summary>
    ''' creates a pdf writer with the correct conformance settings
    ''' </summary>
    ''' <param name="outputPdfPath"></param>
    ''' <param name="document"></param>
    ''' <returns></returns>
    Private Function createWriter(ByVal outputPdfPath As String, ByVal document As Document)
        ' create a writer that listens to the document that it's writing to
        Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(outputPdfPath, FileMode.Create))
        writer.PDFXConformance = PdfWriter.PDFX32002

        Return writer
    End Function

#End Region



#Region "Imposition"


    ''' <summary>
    ''' Creates a standard 4up imposition on 11x17
    ''' </summary>
    ''' <param name="OriginalPdfPath"></param>
    ''' <param name="OutputPdfPath"></param>
    Private Sub impose4UP(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String)

        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        Dim document As Document = CreateDoc(11, 17, DocTitle)
        Dim writer As PdfWriter = createWriter(OutputPdfPath, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim i As Integer = 0
        Dim cb As PdfContentByte = writer.DirectContent

        Dim Evens As New List(Of Single())
        Dim Odds As New List(Of Single())
        With Evens
            .Add(EvenTopLeft)
            .Add(EvenTopRight)
            .Add(EvenBotLeft)
            .Add(EvenBotRight)
        End With

        With Odds
            .Add(OddTopLeft)
            .Add(OddTopRight)
            .Add(OddBotLeft)
            .Add(OddBotRight)
        End With

        While i < info.pageCount
            document.NewPage()


            i += 1
            'Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)

            ' If ((i \ 2) * 2 <> i) = True Then

            If GenUtil.IsEven(i) Then
                'even numbered page
                For Each pg As Single() In Evens
                    AddtoPage(cb, writer, i, pg, info.pageCount, reader)
                Next

            Else
                'odd numbered page
                For Each pg As Single() In Odds
                    AddtoPage(cb, writer, i, pg, info.pageCount, reader)
                Next
            End If



            'If (i = 1) AndAlso (checkCutLines.Checked) Then
            '    cb.SetLineWidth(0)
            '    cb.SetRGBColorStroke(0, 0, 0)
            '    cb.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE)
            '    cb.MoveTo(width, 0)
            '    cb.LineTo(width, 30)
            '    cb.MoveTo(width, height)
            '    cb.LineTo(width, height - 30)
            '    cb.Stroke()
            'End If

        End While
        ' step 5: close the document
        document.Close()
        reader.Close()
    End Sub


    Private Sub imposeEconomy(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String)

        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        Dim document As Document = CreateDoc(11, 17, DocTitle)
        Dim writer As PdfWriter = createWriter(OutputPdfPath, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim cb As PdfContentByte = writer.DirectContent
        Dim i As Integer = 0

        Dim pOne As Integer = 0
        Dim pTwo As Integer = 0
        Dim pThree As Integer = 0
        Dim pFour As Integer = 0
        'p1,p2,p3,p4 keep track of the different pages for each corner for economy printing. ie. - 

        'p1 keeps track of pages 1-25
        'p2 keeps track of pages 26-50
        'p3 keeps track of pages 51-75
        'p4 keeps track of pages 76-100

        'in a hundred page book of course.



        'TopLeft, TopRight, BotLeft, BotRight keep track of which page goes on what part of the page. (it's different if it's even or odd pages)
        Dim TopLeft As Integer = 0
        Dim TopRight As Integer = 0
        Dim BotLeft As Integer = 0
        Dim BotRight As Integer = 0


        Dim FinalPgCount As Integer = info.pageCount
        Do Until FinalPgCount Mod 8 = 0
            FinalPgCount += 1
        Loop
        ' MsgBox(FinalPgCount)
        'Exit Sub


        While i < CInt(Math.Ceiling(FinalPgCount / 4.0)) 'gets the final page count and divides it by 4 because the pages are placed 4 up on a sheet
            document.NewPage()

            i += 1
            pOne = i
            pTwo = i + CInt(Math.Ceiling(FinalPgCount / 4.0))
            pThree = i + (CInt(Math.Ceiling(FinalPgCount / 4.0)) * 2)
            pFour = i + (CInt(Math.Ceiling(FinalPgCount / 4.0)) * 3)


            If GenUtil.IsEven(i) Then 'makes sure that the pages match up if pages are flipped (for duplexing)
                'is even
                TopLeft = pTwo
                TopRight = pOne
                BotLeft = pFour
                BotRight = pThree

                'even numbered page

                'Top Left
                AddtoPage(cb, writer, TopLeft, EvenTopLeft, info.pageCount, reader)

                'Top Right
                AddtoPage(cb, writer, TopRight, EvenTopRight, info.pageCount, reader)

                'Bottom Left
                AddtoPage(cb, writer, BotLeft, EvenBotLeft, info.pageCount, reader)

                'Bottom Right
                AddtoPage(cb, writer, BotRight, EvenBotRight, info.pageCount, reader)

            Else
                'IsOdd
                TopLeft = pOne
                TopRight = pTwo
                BotLeft = pThree
                BotRight = pFour

                'odd numbered page

                'Top Left
                AddtoPage(cb, writer, TopLeft, OddTopLeft, info.pageCount, reader)

                'Top Right
                AddtoPage(cb, writer, TopRight, OddTopRight, info.pageCount, reader)

                'Bottom Left
                AddtoPage(cb, writer, BotLeft, OddBotLeft, info.pageCount, reader)

                'Bottom Right
                AddtoPage(cb, writer, BotRight, OddBotRight, info.pageCount, reader)

            End If

            'Else
            'TopLeft = i
            'BotRight = pFour
            'End If

            ' cut marks only on 1st page
            'If i = 1 Then
            '    If checkCutLinez.Checked Then
            '        cb.SetLineWidth(0)
            '        cb.SetRGBColorStroke(0, 0, 0)
            '        cb.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE)
            '        cb.MoveTo(width, 0)
            '        cb.LineTo(width, 30)
            '        cb.MoveTo(width, height)
            '        cb.LineTo(width, height - 30)
            '        cb.Stroke()
            '    End If
            'End If

        End While
        ' step 5: close the document
        document.Close()
        reader.Close()
    End Sub

    Private Sub imposeFullBleed(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String)

        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        Dim document As Document = CreateDoc(11, 17, DocTitle)
        Dim writer As PdfWriter = createWriter(OutputPdfPath, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim cb As PdfContentByte = writer.DirectContent

        ' open the document
        document.Open()

        Dim i As Integer = 0
        While i < info.pageCount
            document.NewPage()
            i += 1
            Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)

            If GenUtil.IsEven(i) Then

                'even numbered page

                'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)

                'top left                             .375 8.625
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 27, 621)

                'top right                            5.625 8.625
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 405, 621)

                'bottom left                         .375 .125
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 27, 9)

                'bottom right                        5.625 .125
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 405, 9)


                cb.SetLineWidth(1)
                cb.SetCMYKColorStroke(0, 0, 0, 500)
                'cb.SetRGBColorStroke(0, 0, 0)
                cb.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE)
                cb.MoveTo(0, 18)
                cb.LineTo(18, 18)
                cb.MoveTo(0, 630)
                cb.LineTo(18, 630)

                cb.MoveTo(0, 594)
                cb.LineTo(18, 594)
                cb.MoveTo(0, 1206)
                cb.LineTo(18, 1206)
                cb.Stroke()

            Else
                'odd numbered page

                'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)

                'top left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 9, 621)

                'top right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 387, 621)

                'bottom left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 9, 9)

                'bottom right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 387, 9)

                cb.SetLineWidth(1)
                cb.SetCMYKColorStroke(0, 0, 0, 500)
                'cb.SetRGBColorStroke(0, 0, 0)
                cb.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE)
                cb.MoveTo(774, 18)
                cb.LineTo(792, 18)
                cb.MoveTo(774, 630)
                cb.LineTo(792, 630)

                cb.MoveTo(774, 594)
                cb.LineTo(792, 594)
                cb.MoveTo(774, 1206)
                cb.LineTo(792, 1206)


                cb.Stroke()
            End If


        End While
        ' step 5: close the document
        document.Close()
        reader.Close()
    End Sub

    ''' <summary>
    ''' creates a 12x18 file image
    ''' </summary>
    ''' <param name="OriginalPdfPath"></param>
    ''' <param name="OutputPdfPath"></param>
    Private Sub imposeBooklet12x18(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String)

        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        Dim document As Document = CreateDoc(12, 18, DocTitle)
        Dim writer As PdfWriter = createWriter(OutputPdfPath, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim cb As PdfContentByte = writer.DirectContent
        Dim i As Integer = 0

        Dim mainPDFFont As BaseFont = BaseFont.CreateFont(("C:\Windows\Fonts\courbd.ttf"), BaseFont.WINANSI, BaseFont.EMBEDDED) 'Current.Server.MapPath
        Dim Font11 As New iTextSharp.text.Font(mainPDFFont, 11)

        While i < info.pageCount
            document.NewPage()
            i += 1




            Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)

            If GenUtil.IsEven(i) Then

                'even numbered page

                'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)

                'FirstRow left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0.375), ConvSngl(13.5))

                'FirstRow right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(6.125), ConvSngl(13.5))


                'SecondRow left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0.375), ConvSngl(9.25))

                'SecondRow right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(6.125), ConvSngl(9.25))


                'ThirdRow left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0.375), ConvSngl(4.5))

                'ThirdRow right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(6.125), ConvSngl(4.5))


                'FourthRow left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0.375), ConvSngl(0.25))

                'FourthRow right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(6.125), ConvSngl(0.25))






                'cb.SetLineWidth(1)
                'cb.SetRGBColorStroke(0, 0, 0)
                'cb.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE)
                'cb.MoveTo(0, 18)
                'cb.LineTo(18, 18)
                'cb.MoveTo(0, 630)
                'cb.LineTo(18, 630)

                'cb.MoveTo(0, 594)
                'cb.LineTo(18, 594)
                'cb.MoveTo(0, 1206)
                'cb.LineTo(18, 1206)
                'cb.Stroke()

            Else
                'odd numbered page

                'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)

                'FirstRow left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0.125), ConvSngl(13.5))

                'FirstRow right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(5.875), ConvSngl(13.5))


                'SecondRow left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0.125), ConvSngl(9.25))

                'SecondRow right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(5.875), ConvSngl(9.25))


                'ThirdRow left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0.125), ConvSngl(4.5))

                'ThirdRow right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(5.875), ConvSngl(4.5))


                'FourthRow left
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0.125), ConvSngl(0.25))

                'FourthRow right
                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(5.875), ConvSngl(0.25))

                cb.SetLineWidth(1)
                cb.SetCMYKColorStroke(0, 0, 0, 500)
                'cb.SetRGBColorStroke(0, 0, 0)

                cb.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE)

                Dim ct As New ColumnText(cb)
                Dim cuts As New Phrase("Cut 1: 17.75,  Rotate 180,  Cut 2: 17.50,  Cut 3: 9.00,  Cut 4:  8.50,  Rotate 90 (Spine facing out),  Cut 5: 11.75,  Cut 6: 6.00,     Final Size: 5.5x4", Font11)


                ct.SetSimpleColumn(cuts, ConvSngl(0.5), ConvSngl(8.75), ConvSngl(10), ConvSngl(9.25), 15, Element.ALIGN_LEFT)
                ct.Go()


                'cb.MoveTo(774, 18)
                'cb.LineTo(792, 18)
                'cb.MoveTo(774, 630)
                'cb.LineTo(792, 630)

                'cb.MoveTo(774, 594)
                'cb.LineTo(792, 594)
                'cb.MoveTo(774, 1206)
                'cb.LineTo(792, 1206)


                'cb.Stroke()
            End If







        End While
        ' step 5: close the document
        document.Close()
        reader.Close()
    End Sub

    Private Sub imposeMiniPamphlet(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String)

        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        Dim document As Document = CreateDoc(5.5, 8.5, DocTitle)
        Dim writer As PdfWriter = createWriter(OutputPdfPath, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim i As Integer = 0
        Dim cb As PdfContentByte = writer.DirectContent


        While i < info.pageCount


            document.NewPage()
            i += 1
            Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)

            'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)
            'Top
            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0), ConvSngl(0))

            'Bottom
            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(0), ConvSngl(4.75))

        End While
        ' step 5: close the document
        document.Close()
        reader.Close()
    End Sub

    Private Sub imposeCustomSized4Up(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String)
        'info.pageHeight
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        'MsgBox((info.pageWidth * 2).ToString & " x " & (info.pageHeight * 2).ToString)
        Dim document As Document = CreateDoc(info.pageWidth * 2, info.pageHeight * 2, DocTitle)
        Dim writer As PdfWriter = createWriter(OutputPdfPath, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)






        Dim i As Integer = 0
        Dim cb As PdfContentByte = writer.DirectContent


        While i < info.pageCount

            document.NewPage()
            i += 1
            Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)

            'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)
            'top left
            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, ConvSngl(info.pageHeight))

            'top right
            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(info.pageWidth), ConvSngl(info.pageHeight))

            'bottom left
            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0)

            'bottom right
            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(info.pageWidth), 0)

        End While
        ' step 5: close the document
        document.Close()
        reader.Close()
    End Sub


    Private Sub imposeMiniBooklet3_75x5_5(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String)

        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        Dim document As Document = CreateDoc(18, 12, DocTitle)
        Dim writer As PdfWriter = createWriter(OutputPdfPath, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim i As Integer = 0
        Dim cb As PdfContentByte = writer.DirectContent

        While i < info.pageCount
            document.NewPage()
            i += 1
            Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)

            If GenUtil.IsEven(i) Then

                'even numbered page

                '.6875 zoom

                'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)

                'top left left                        
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(0.75), ConvSngl(6.25))

                'top middle left
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(5.25), ConvSngl(6.25))

                'top middle right
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(9.75), ConvSngl(6.25))

                'top right right
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(14.25), ConvSngl(6.25))


                'bottom left left
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(0.75), ConvSngl(0.25))

                'bottom middle left
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(5.25), ConvSngl(0.25))

                'bottom middle right
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(9.75), ConvSngl(0.25))

                'bottom right right
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(14.25), ConvSngl(0.25))


                'cb.SetLineWidth(1)
                'cb.SetCMYKColorStroke(0, 0, 0, 500)
                ''cb.SetRGBColorStroke(0, 0, 0)
                'cb.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE)

                'cb.MoveTo(ConvSngl(0.125), ConvSngl(4.5))
                'cb.LineTo(ConvSngl(8.875), ConvSngl(4.5))

                'cb.MoveTo(ConvSngl(0.125), ConvSngl(9))
                'cb.LineTo(ConvSngl(8.875), ConvSngl(9))

                'cb.MoveTo(ConvSngl(0.125), ConvSngl(13.5))
                'cb.LineTo(ConvSngl(8.875), ConvSngl(13.5))

                'cb.Stroke()

            Else
                'odd numbered page

                'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)

                'top left left                        
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(0.25), ConvSngl(6.25))

                'top middle left
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(4.75), ConvSngl(6.25))

                'top middle right
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(9.25), ConvSngl(6.25))

                'top right right
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(13.75), ConvSngl(6.25))


                'bottom left left
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(0.25), ConvSngl(0.25))

                'bottom middle left
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(4.75), ConvSngl(0.25))

                'bottom middle right
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(9.25), ConvSngl(0.25))

                'bottom right right
                cb.AddTemplate(page, 0.6875F, 0, 0, 0.6875F, ConvSngl(13.75), ConvSngl(0.25))

                cb.SetLineWidth(1)
                cb.SetCMYKColorStroke(0, 0, 0, 500)
                'cb.SetRGBColorStroke(0, 0, 0)
                cb.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE)
                'cb.MoveTo(774, 18)
                'cb.LineTo(792, 18)
                'cb.MoveTo(774, 630)
                'cb.LineTo(792, 630)

                'cb.MoveTo(774, 594)
                'cb.LineTo(792, 594)
                'cb.MoveTo(774, 1206)
                'cb.LineTo(792, 1206)

                'cb.MoveTo(ConvSngl(0.125), ConvSngl(4.5))
                'cb.LineTo(ConvSngl(8.875), ConvSngl(4.5))

                'cb.MoveTo(ConvSngl(0.125), ConvSngl(9))
                'cb.LineTo(ConvSngl(8.875), ConvSngl(9))

                'cb.MoveTo(ConvSngl(0.125), ConvSngl(13.5))
                'cb.LineTo(ConvSngl(8.875), ConvSngl(13.5))


                'cb.Stroke()
            End If


        End While
        ' step 5: close the document
        document.Close()
        reader.Close()
    End Sub

    Private Sub imposeChristiansDailyXL(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String)
        Dim tempDestination As String = OutputPdfPath.ToLower.Replace(".pdf", "-temp.pdf")
        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        Dim document As Document = CreateDoc(18.11, 12.25, DocTitle)
        Dim writer As PdfWriter = createWriter(tempDestination, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim i As Integer = 0
        Dim cb As PdfContentByte = writer.DirectContent


        '----------------------------The way this works----------------------------
        'This is based from the bottom left corner. There are 8 blocks to be set, 
        'And all need to have bleed that can be cut off. They also need to match 
        'the bgc block size. (Block width * block Height)
        'inside each pre-trimmed block the page is placed at a certain 
        'distance (xIn, yIn) from the bottom left side Of that pre trimmed block.
        'The pdf is saved with a temp file name, and sent off to be rotated & saved
        '--------------------------------------------------------------------------


        Dim blockWidth As Single = 4.5 'pre trimmed block width
        Dim blockHeight As Single = 5.75 'pre trimmed block height

        Dim xIn As Single = 0.5156 'x position inside pre-trimmed block where the final pdf is placed
        Dim yIn As Single = 0.3294 'y position inside pre-trimmed block where the final pdf is placed

        Dim xOffset As Single = 0.055 'distance between edge of paper and first block on x axis (side to side)
        Dim botYoffset As Single = 0.625 'distance from bottom of paper to bottom of bottom pre-trimmed block
        Dim topYoffset As Single = botYoffset + blockHeight + (0.125) 'distance from bottom of paper to bottom of top pre-trimmed block

        Dim zoom As Single = 1.3214285714285716
        info.pageWidth *= zoom 'sets the page width to the zoomed version
        info.pageHeight *= zoom



        While i < info.pageCount

            document.NewPage()
            i += 1
            Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)
            Dim x1, x2, x3, x4, topY, botY As Single
            x1 = xOffset + xIn 'left left position
            x2 = xOffset + (blockWidth * 1) + xIn 'mid left position
            x3 = xOffset + (blockWidth * 2) + xIn 'mid right position
            x4 = xOffset + (blockWidth * 3) + xIn 'right right position
            topY = topYoffset + yIn 'top position
            botY = botYoffset + yIn 'bot position


            'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)
            'top left left
            cb.AddTemplate(page, zoom, 0, 0, zoom, ConvSngl(x1), ConvSngl(topY))

            'top mid left
            cb.AddTemplate(page, zoom, 0, 0, zoom, ConvSngl(x2), ConvSngl(topY))

            'top mid right
            cb.AddTemplate(page, zoom, 0, 0, zoom, ConvSngl(x3), ConvSngl(topY))

            'top right
            cb.AddTemplate(page, zoom, 0, 0, zoom, ConvSngl(x4), ConvSngl(topY))


            'bottom left left
            cb.AddTemplate(page, zoom, 0, 0, zoom, ConvSngl(x1), ConvSngl(botY))

            'bottom mid left
            cb.AddTemplate(page, zoom, 0, 0, zoom, ConvSngl(x2), ConvSngl(botY))

            'bottom mid right
            cb.AddTemplate(page, zoom, 0, 0, zoom, ConvSngl(x3), ConvSngl(botY))

            'bottom right
            cb.AddTemplate(page, zoom, 0, 0, zoom, ConvSngl(x4), ConvSngl(botY))


            cb.SetLineWidth(1.5)
            cb.SetCMYKColorStroke(0, 0, 0, 500)
            cb.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE)
            Dim xStart As Single = 0
            Dim xStop As Single = 0.125

            Dim yStart As Single = 0
            Dim yStop As Single = 0.125


#Region "Marks"

            Dim trimWidth As Single = 4.125 'final trimmed width
            Dim trimHeight As Single = 5.375 'final trimmed height
            Dim trimXstart As Single = 0.1875 'x position inside pre-trimmed block of the left final trim
            Dim trimYstart As Single = 0.2581 'y position inside pre-trimmed block of the bottom final trim

            x1 = xOffset + trimXstart 'left left position
            x2 = xOffset + (blockWidth * 1) + trimXstart 'mid left position
            x3 = xOffset + (blockWidth * 2) + trimXstart 'mid right position
            x4 = xOffset + (blockWidth * 3) + trimXstart 'right right position

            botY = botYoffset + trimYstart 'yIn 'bot position
            topY = topYoffset + trimYstart
            xStart = 0
            xStop = 0.0625
            yStart = 0
            yStop = 0.5

            '--------------------bottom marks--------------------
            cb.MoveTo(ConvSngl(x1), (ConvSngl(yStart)))
            cb.LineTo(ConvSngl(x1), (ConvSngl(yStop)))

            cb.MoveTo(ConvSngl(x1 + trimWidth), (ConvSngl(yStart)))
            cb.LineTo(ConvSngl(x1 + trimWidth), (ConvSngl(yStop)))


            cb.MoveTo(ConvSngl(x2), ConvSngl(yStart))
            cb.LineTo(ConvSngl(x2), ConvSngl(yStop))

            cb.MoveTo(ConvSngl(x2 + trimWidth), ConvSngl(yStart))
            cb.LineTo(ConvSngl(x2 + trimWidth), ConvSngl(yStop))


            cb.MoveTo(ConvSngl(x3), ConvSngl(yStart))
            cb.LineTo(ConvSngl(x3), ConvSngl(yStop))

            cb.MoveTo(ConvSngl(x3 + trimWidth), ConvSngl(yStart))
            cb.LineTo(ConvSngl(x3 + trimWidth), ConvSngl(yStop))


            cb.MoveTo(ConvSngl(x4), ConvSngl(yStart))
            cb.LineTo(ConvSngl(x4), ConvSngl(yStop))

            cb.MoveTo(ConvSngl(x4 + trimWidth), ConvSngl(yStart))
            cb.LineTo(ConvSngl(x4 + trimWidth), ConvSngl(yStop))
            '--------------------------------------------------

            '--------------------Left marks--------------------
            cb.MoveTo(ConvSngl(xStart), ConvSngl(botY))
            cb.LineTo(ConvSngl(xStop), ConvSngl(botY))

            cb.MoveTo(ConvSngl(xStart), ConvSngl(botY + info.pageHeight))
            cb.LineTo(ConvSngl(xStop), ConvSngl(botY + info.pageHeight))


            cb.MoveTo(ConvSngl(xStart), ConvSngl(topY))
            cb.LineTo(ConvSngl(xStop), ConvSngl(topY))

            cb.MoveTo(ConvSngl(xStart), ConvSngl(topY + info.pageHeight))
            cb.LineTo(ConvSngl(xStop), ConvSngl(topY + info.pageHeight))
            '--------------------------------------------------

            '-------------------Right marks--------------------
            xStop = 18.11
            xStart = xStop - 0.0625

            cb.MoveTo(ConvSngl(xStart), ConvSngl(botY))
            cb.LineTo(ConvSngl(xStop), ConvSngl(botY))

            cb.MoveTo(ConvSngl(xStart), ConvSngl(botY + info.pageHeight))
            cb.LineTo(ConvSngl(xStop), ConvSngl(botY + info.pageHeight))


            cb.MoveTo(ConvSngl(xStart), ConvSngl(topY))
            cb.LineTo(ConvSngl(xStop), ConvSngl(topY))

            cb.MoveTo(ConvSngl(xStart), ConvSngl(topY + info.pageHeight))
            cb.LineTo(ConvSngl(xStop), ConvSngl(topY + info.pageHeight))
            '--------------------------------------------------
#End Region


            cb.Stroke()
        End While

        ' step 5: close the document
        document.Close()
        reader.Close()
        writer.Close()
        If rotatePages(tempDestination, OutputPdfPath, 270) Then
            File.Delete(tempDestination)
        End If
    End Sub

    Private Sub ImposeStandardBlockCalendar(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String, ByVal doubleSided As Boolean)

        Dim tempDestination As String = OutputPdfPath.ToLower.Replace(".pdf", "-temp.pdf")
        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        Dim document As Document = CreateDoc(18.11, 12.125, DocTitle)
        Dim writer As PdfWriter = createWriter(tempDestination, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim i As Integer = 0
        Dim cb As PdfContentByte = writer.DirectContent


        '----------------------------The way this works----------------------------
        'This is based from the bottom left corner. There are 8 blocks to be set, 
        'And all need to have bleed that can be cut off. They also need to match 
        'the bgc block size. (Block width * block Height)
        'The pdf is saved with a temp file name, and sent off to be rotated & saved
        '--------------------------------------------------------------------------

        'top to bottom positions
        Dim row1YposSideA As Single = 6.4375 'y position of the bottom left corner of the top row
        Dim row2YposSideA As Single = 0.4375 'y position of the bottom left corner of the bottom row

        'side to side positions
        Dim col1XposSideA As Single = 0.055 'x position of the bottom left corner of the 1st column
        Dim col2XposSideA As Single = 4.555 'x position of the bottom left corner of the 2nd column
        Dim col3XposSideA As Single = 9.055 'x position of the bottom left corner of the 3rd column
        Dim col4XposSideA As Single = 13.555 'x position of the bottom left corner of the 4th column

        'lists of rows and columns
        Dim rows As List(Of Single) = {row1YposSideA, row2YposSideA}.ToList
        Dim cols As List(Of Single) = {col1XposSideA, col2XposSideA, col3XposSideA, col4XposSideA}.ToList

        'mark sizes
        Dim markWidth As Single = 0.055
        Dim markHeight As Single = 0.4375

        Dim xRightStart As Single = 18.11 - markWidth '(document.PageSize.Width / 72) - markWidth
        Dim xRightStop As Single = xRightStart + markWidth

        'block sizes
        Dim blockHeight As Single = 5.375
        Dim blockWidth As Single = 4.125
        Dim bleed As Single = 0.1875

        While i < info.pageCount


            document.NewPage()
            i += 1
            Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)


            If doubleSided AndAlso GenUtil.IsEven(i) Then
                ' its a double sided calendar, and it's the back side
                ' - 0.5F moves the placement down on the page 1/2 inch
                rows = {row1YposSideA - 0.5F, row2YposSideA - 0.5F}.ToList

            Else
                rows = {row1YposSideA, row2YposSideA}.ToList
            End If

            Dim marked As Boolean = False
            For Each row As Single In rows

                For Each col As Single In cols
                    'adds the page template 8 times
                    'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)
                    cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(col), ConvSngl(row))
                Next
            Next

#Region "Marks"

            If Not marked Then
                cb.SetLineWidth(1.5)
                cb.SetCMYKColorStroke(0, 0, 0, 500)
                For Each row As Single In rows
                    Dim yStartA As Single = row + bleed
                    Dim yStartB As Single = yStartA + blockHeight
                    Dim xStart As Single = 0

                    'marks the top left cut mark per row
                    MakeLine(cb, xStart, yStartA, markWidth, yStartA)

                    'marks the bottom left cut mark per row
                    MakeLine(cb, xStart, yStartB, markWidth, yStartB)

                    'marks the top right cut mark per row
                    MakeLine(cb, xRightStart, yStartA, xRightStop, yStartA)

                    'marks the bottom right cut mark per row
                    MakeLine(cb, xRightStart, yStartB, xRightStop, yStartB)

                Next
                'If MsgBox("dude") Then
                '    Beep()
                'End If

                For Each col As Single In cols
                    'goes through each "column" and adds the left and right cut marks
                    Dim xStartA As Single = col + bleed 'left cut mark
                    Dim xStartB As Single = xStartA + blockWidth 'right cut mark
                    Dim yStart As Single = 0

                    If doubleSided AndAlso GenUtil.IsEven(i) Then
                        ' its a double sided calendar, and it's the back side
                        yStart = 12.125 - markHeight
                    Else
                        yStart = 0
                    End If


                    'left cut mark
                    MakeLine(cb, xStartA, yStart, xStartA, yStart + markHeight)

                    'right cut mark
                    MakeLine(cb, xStartB, yStart, xStartB, yStart + markHeight)

                Next


                cb.Stroke()
                marked = True
            End If
#End Region


        End While

        ' step 5: close the document
        document.Close()
        reader.Close()
        writer.Close()

        If rotatePages(tempDestination, OutputPdfPath, 270) Then
            File.Delete(tempDestination)
        End If

    End Sub

    Private Sub ImposeChristiansDaily(ByVal OriginalPdfPath As String, ByVal OutputPdfPath As String)
        Dim DocTitle As String = Path.GetFileNameWithoutExtension(OriginalPdfPath)
        Dim document As Document = CreateDoc(12, 18.11, DocTitle)
        Dim writer As PdfWriter = createWriter(OutputPdfPath, document)
        ' open the document
        document.Open()

        Dim reader As PdfReader = getPdfReader(OriginalPdfPath)
        Dim info As New PdfInfo(OriginalPdfPath)

        Dim i As Integer = 0
        Dim cb As PdfContentByte = writer.DirectContent


        Dim xOffset As Single = 0.188 'distance between edge of paper and first block on x axis (side to side)
        Dim yOffset As Single = 0.6 'distance from bottom of paper to bottom of bottom pre-trimmed block (top to bottom)
        Dim xSpacer As Single = 0.375 'space between each block (side to side)
        Dim ySpacer As Single = 0.5 'space between each block (top to bottom)

        Dim yStart As Single = 0
        Dim yStop As Single = 0.375




        While i < info.pageCount

            document.NewPage()
            i += 1
            Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)

            Dim marked As Boolean = False

            For row As Integer = 0 To 3
                Dim CurHeight As Single = yOffset + ((info.pageHeight * row) + (ySpacer * row)) 'figures out the y (top to bottom) placement of block
                For column As Integer = 0 To 3
                    Dim curWidth As Single = xOffset + (info.pageWidth * column) + (xSpacer * column) 'figures out the x (side to side) placement of block
                    'key for list below  (page, width, skew, skew, height, side to side placement, up & down placement)
                    cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, ConvSngl(curWidth), ConvSngl(CurHeight))

                    If Not marked Then
                        cb.SetLineWidth(1.5)
                        cb.SetCMYKColorStroke(0, 0, 0, 500)
                        'marks left edge
                        cb.MoveTo(ConvSngl(curWidth), ConvSngl(yStart))
                        cb.LineTo(ConvSngl(curWidth), ConvSngl(yStop))

                        'marks right edge
                        curWidth += info.pageWidth
                        cb.MoveTo(ConvSngl(curWidth), ConvSngl(yStart))
                        cb.LineTo(ConvSngl(curWidth), ConvSngl(yStop))

                    End If

                Next
                cb.Stroke()
                marked = True 'only makes marks 1 x per sheet
            Next

        End While

        ' step 5: close the document
        document.Close()
        reader.Close()
        writer.Close()
    End Sub

    Private Sub MakeLine(ByRef cb As PdfContentByte,
                     ByVal xStart As Single, ByVal yStart As Single,
                     ByVal xStop As Single, ByVal yStop As Single)
        cb.MoveTo(ConvSngl(xStart), ConvSngl(yStart))
        cb.LineTo(ConvSngl(xStop), ConvSngl(yStop))
    End Sub

#End Region



End Class


''' <summary>
''' contains info about the specified pdf. eg. pageHeight, pageWidth, pageCount, pageRotation
''' </summary>
Public Class PdfInfo


#Region "Properties"

    ''' <summary>
    ''' height of pdf's first page in inches
    ''' </summary>
    ''' <returns></returns>
    Public Property pageHeight As Single = 0.0

    ''' <summary>
    ''' width of pdf's first page in inches
    ''' </summary>
    ''' <returns></returns>
    Public Property pageWidth As Single = 0.0

    ''' <summary>
    ''' rotation of pdf's first page
    ''' </summary>
    ''' <returns></returns>
    Public Property pageRotation As Single = 0.0

    Public Property pageCount As Integer = 0


#End Region


#Region "New"

    ''' <summary>
    ''' contains info about the specified pdf. eg. pageHeight, pageWidth, pageCount, pageRotation
    ''' </summary>
    ''' <param name="filePath"></param>
    Public Sub New(ByVal filePath As String)
        If File.Exists(filePath) Then
            Try
                Dim psize As iTextSharp.text.Rectangle
                Dim aReader As PdfReader
                'end of itextsharp pdf imposition variables

                aReader = New PdfReader(filePath)
                pageCount = aReader.NumberOfPages

                'assume whole doc is as 1st page; useless to impose a doc with varying pages
                psize = aReader.GetPageSizeWithRotation(1)
                pageRotation = aReader.GetPageRotation(1)

                aReader.Close()

                'itextsharp finds the size in inches multiplied by 72. (maybe pixels?)
                pageWidth = psize.Width / 72
                pageHeight = psize.Height / 72
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub
#End Region

#Region "Methods"


    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("Height: " & pageHeight)
        sb.AppendLine("Width: " & pageWidth)
        sb.AppendLine("Rotation: " & pageRotation)
        sb.AppendLine("Page Count: " & pageCount)
        Return sb.ToString
        'Return MyBase.ToString()
    End Function

#End Region


End Class

