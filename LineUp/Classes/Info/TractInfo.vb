
Public Class TractInfo

#Region "Properties"

    Public Property ItemNumber As String
    Public Property TractFilePath As String
    Public Property ExportLocation As String
    Public Property ExportPics As Boolean
    Public Property ExportA4Pdf As Boolean
    Public Property ExportLetterPdf As Boolean



    ''' <summary>
    ''' returns the arguments in the correct order and converted for javascript.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ArgsList() As String()
        Get
            Dim args As New List(Of String)
            With args
                .Add(ItemNumber)
                .Add(Utilities.GenUtil.ConvertForJavaScript(TractFilePath))
                .Add(Utilities.GenUtil.ConvertForJavaScript(ExportLocation))
                .Add(ExportPics.ToString)
                .Add(ExportA4Pdf.ToString)
                .Add(ExportLetterPdf.ToString)
            End With


            Return args.ToArray
        End Get
    End Property

#End Region

#Region "Init"
    Public Sub New(ByVal ItemNumber As String, ByVal TractFilePath As String, ByVal ExportLocation As String, ByVal ExportPics As Boolean, ByVal ExportA4Pdf As Boolean, ByVal ExportLetterPdf As Boolean)
        Me.ItemNumber = ItemNumber
        Me.TractFilePath = TractFilePath
        Me.ExportLocation = ExportLocation
        Me.ExportPics = ExportPics
        Me.ExportA4Pdf = ExportA4Pdf
        Me.ExportLetterPdf = ExportLetterPdf
    End Sub
    Public Sub New()
        'blank
    End Sub

#End Region


End Class