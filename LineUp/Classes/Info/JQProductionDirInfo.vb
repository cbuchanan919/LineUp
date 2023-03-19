Imports System.IO

''' <summary>
''' "ProductionDirInfo" class stores the different info for that product category. - this is more of an overarching category...
''' </summary>
''' <remarks></remarks>
Public Class JQProductionDirInfo



#Region "Public Properties"

    ''' <summary>
    ''' Type of product - ie. book, pamphlet, hymnbook etc.
    ''' </summary>
    Public Property ProductType As ProductCategory = ProductCategory.Not_Set

    ''' <summary>
    ''' Type of file - body vs cover
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductBodyOrCover As BodyVsCover = BodyVsCover.Not_Set

    ''' <summary>
    ''' Type of printer to use. (book / pamphlet / cover / body  etc
    ''' </summary>
    ''' <returns></returns>
    Public Property MyPrinterCategory As PrinterCategory = PrinterCategory.Undefined

    ''' <summary>
    ''' Production Folder (where any of this type of production file would be found)
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductionDirectory As DirectoryInfo = Nothing


    ''' <summary>
    ''' How many copies are per print layer. (Production folders are organized by xUP)
    ''' </summary>
    ''' <returns></returns>
    Public Property CopiesPerLayer As Integer = 0


#End Region


#Region "Init"

    ''' <summary>
    ''' New Product Type
    ''' </summary>
    ''' <param name="ProductType">Type of product - ie. book, pamphlet, hymnbook etc.</param>
    ''' <param name="ProductionDirectory">Production Directory of the product</param>
    ''' <param name="ProductFileType">Body or cover?</param>
    ''' <param name="CopiesPerLayer"></param>
    Public Sub New(ByVal ProductType As ProductCategory,
                   ByVal ProductionDirectory As DirectoryInfo,
                   ByVal ProductFileType As BodyVsCover,
                   ByVal CopiesPerLayer As Integer,
                   ByVal ProductPrinterCategory As PrinterCategory)
        Me.ProductType = ProductType
        Me.ProductionDirectory = ProductionDirectory
        Me.CopiesPerLayer = CopiesPerLayer
        Me.ProductBodyOrCover = ProductFileType
        Me.MyPrinterCategory = ProductPrinterCategory
    End Sub


#End Region


#Region "Methods"
    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("Production File Info:")
        sb.AppendLine($"Type: { ProductType }")
        sb.AppendLine($"Body / Cover: { ProductBodyOrCover }")
        sb.AppendLine($"Directory: { ProductionDirectory.FullName }")
        sb.AppendLine($"Copies Per Layer: { CopiesPerLayer }")
        sb.AppendLine($"Printer Category: { MyPrinterCategory.ToString.Replace("_", " ") }")
        Return sb.ToString
        'Return MyBase.ToString()
    End Function

#End Region



End Class