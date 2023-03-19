Imports System.IO

''' <summary>
''' contains the info for 1 job q barcode
''' </summary>
Public Class JQBarcodeInfo


#Region "Properties"
    ''' <summary>
    ''' What the barcode would represent. ie. "Printing on the 1200a"
    ''' </summary>
    ''' <returns></returns>
    Public Property BarcodeText As String = ""
    ''' <summary>
    ''' the 4 digit code to store in the barcode itself. ie. "1y2z"
    ''' </summary>
    ''' <returns></returns>
    Public Property BarcodeValue As String = ""

    ''' <summary>
    ''' If a barcode image has been created, this FinalFilePath points to it.
    ''' </summary>
    ''' <returns></returns>
    Public Property FinalFilePath As String = ""

    '''' <summary>
    '''' used in CreateBarcode
    '''' </summary>
    '''' <returns></returns>
    'Private Property pfc As New Drawing.Text.PrivateFontCollection

#End Region


#Region "Methods"

    ''' <summary>
    ''' creates a barcode for each new DesignID given
    ''' </summary> 
    ''' <param name="CustomBarcode"></param>
    ''' <remarks></remarks>
    Public Sub CreateBarcode(Optional CustomBarcode As String = "", Optional ByVal folderPath As String = "", Optional ByVal createSubText As Boolean = False)

        Try
            'adds the 3 of 9 font file
            Dim pfc As New Drawing.Text.PrivateFontCollection
            pfc.AddFontFile(My.Settings.dirResources & "fre3of9x.ttf")
            Dim ThreeofNine As System.Drawing.Font = New System.Drawing.Font(pfc.Families(0), 50)

            pfc.AddFontFile("C:\Windows\Fonts\courbd.ttf")
            Dim textFont As New Font(pfc.Families(0), 20, FontStyle.Bold)

            'used to record the different barcode Designs already created 
            'Dim DesignIDs As New List(Of String)
            Dim bCode As String = "*~" & BarcodeValue & "~*"
            Dim FileName As String = BarcodeText
            If CustomBarcode <> "" Then
                'if custombarcode info is passed, then it sets it to be created instead
                bCode = "*~" & CustomBarcode & "~*"
                FileName = CustomBarcode
            End If

            'Dim Size As Integer = 600

            Dim TextSize As Size = TextRenderer.MeasureText(bCode, ThreeofNine)
            Dim WidthSize As Integer = TextSize.Width * 6.5 'the size that was close to the correct size
            Dim heightSize As Integer = 130
            If createSubText Then
                heightSize = 500
            End If
            Using Bmp As New Bitmap(WidthSize, heightSize, Imaging.PixelFormat.Format32bppPArgb)


                Bmp.SetResolution(600, 600) 'Sets the resolution to 600 DPI

                'Create a graphics object from the bitmap
                Using G = Graphics.FromImage(Bmp)

                    'Paint the canvas white
                    G.Clear(Color.White)
                    'Set various modes to higher quality
                    G.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                    G.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

                    'Create a font
                    Using F As New System.Drawing.Font(ThreeofNine, FontStyle.Regular)
                        'Create a brush
                        Using B As New SolidBrush(Color.Black)

                            'Draw some text as barcode
                            G.DrawString(bCode, F, B, 5, 20)
                            If createSubText Then
                                G.DrawString(BarcodeValue, textFont, B, (WidthSize / 2) - 50, 325) 'draws the text underneath
                            End If
                        End Using
                    End Using
                End Using


                'Save the file as a png
                If folderPath = "" Then folderPath = My.Settings.dirMxOrders
                Dim filepath As String = Path.Combine(folderPath, FileName & ".png")
                If File.Exists(filepath) Then
                    File.Delete(filepath)
                End If
                Bmp.Save(filepath, Imaging.ImageFormat.Png)
                FinalFilePath = filepath
            End Using

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Create Barcode")
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try

    End Sub


    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("BarcodeText: " & BarcodeText)
        sb.AppendLine("BarcodeValue: " & BarcodeValue)
        sb.AppendLine("FinalFilePath: " & FinalFilePath)
        Return sb.ToString()
    End Function

#End Region


End Class


