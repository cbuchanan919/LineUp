Public Module Constants

    ''' <summary>
    ''' type of ProductType
    ''' </summary>
    Public Enum BodyVsCover
        Not_Set
        Body
        Cover
    End Enum

    Public Enum InsertOrUpdate
        InsertInto
        Update
    End Enum

    ''' <summary>
    ''' non breaking space. (used to cleanly be able to split a gui element)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Nbsp As String
        Get
            Return Chr(160).ToString
        End Get
    End Property

    'different status to keep text the same
    Public Const cStatusA As String = "Status: "
    Public Const cStatusGood As String = "Status: Good "
    Public Const cSearch As String = "Search..."
    Public Const cSplit As String = "§"
    'paper weights
    Public Const c16lbPaper As String = "16lb Paper"
    Public Const c20lbPaper As String = "20lb Paper"
    Public Const c24lbPaper As String = "24lb Paper"
    Public Const c28lbPaper As String = "28lb Paper"
    Public Const c100lbCover As String = "100lb Cover"
    Public Const c80lbCover As String = "80lb Cover"

    Public Const cUnknown As String = "?"

    'Product Categories
    Public Enum ProductCategory
        Not_Set
        Full_Bleed_Book
        Book
        Book_12x9
        Pamphlet
        Mini_Pamphlet
        Hymn_Book
        Brochure
        Leaflet
        Tract_Card
        Chart
        Share_Word
        Alignment_Sheet
        Mini_Gift_Book
        Mini_Economy_Book
        Cd_Album_Cover
        Job_Ticket
        Periodical
    End Enum

    ''' <summary>
    ''' Returns the product category found. If not found, returns -1
    ''' </summary>
    ''' <param name="ProductCategory"></param>
    ''' <returns></returns>
    Public Function ProductCategoryFromString(ByVal ProductCategory As String) As ProductCategory
        ProductCategory = ProductCategory.ToLower.Replace(" ", "_")
        Dim categories As Array = System.Enum.GetValues(GetType(ProductCategory))
        For Each category As ProductCategory In categories

            If category.ToString.ToLower = ProductCategory Then
                Return category
            End If
        Next

        Return -1
    End Function



    ''' <summary>
    ''' Used to manage printers. The available printer options / settings
    ''' </summary>
    Public Enum PrinterCategory
        Undefined
        Alignment_Sheets
        Basic_Printer
        Book_12x9_Body
        Book_12x9_Cover
        Book_Body
        Book_Body_FullBleed
        Book_Cover
        Brochure
        CD_Album_Cover
        Chart
        Default_Printer
        Gospel_Of_Peace
        Hymnbook_Body
        Hymnbook_Cover
        Job_Ticket
        Label_Printer
        Leaflet
        Mini_Book_12x18_Body
        Mini_JNC_Printer
        Pamphlet_Body
        Pamphlet_Cover
        ShareWord_Printer
        TC_Body
        TC_Envelope
        TC_Overseas_Cover
        Tract_Card


    End Enum

    ''' <summary>
    ''' returns printer category as readable string
    ''' </summary>
    ''' <param name="printer"></param>
    ''' <returns></returns>
    Public Function PrinterCategoryToString(ByVal printer As PrinterCategory) As String
        Return printer.ToString.Replace("_", " ")
    End Function
    Public Function PrinterCategoryFromString(ByVal PrinterString As String) As PrinterCategory
        PrinterString = PrinterString.ToLower.Replace(" ", "_")
        Dim printers As Array = System.Enum.GetValues(GetType(PrinterCategory))
        For Each printer As PrinterCategory In printers

            If printer.ToString.ToLower = PrinterString Then
                Return printer
            End If
        Next

        Return -1
    End Function



    Public Const cShareWord As String = "Share Word"
    Public Const cShareWordSupID As String = "9358"
    Public Const cShipNote As String = "Ship Note: "

    'products data table columns

    Public Const Col_ProductID As String = "ProductID"
    Public Const Col_Number As String = "Number"
    Public Const Col_Title As String = "Title"
    Public Const Col_TitleInverted As String = "TitleInverted"
    Public Const Col_Price As String = "Price"
    Public Const Col_Pages As String = "Pages"
    Public Const Col_Author As String = "Author"
    Public Const Col_Medium As String = "Medium"
    Public Const Col_Source As String = "Source"
    Public Const Col_WebText As String = "WebText"
    Public Const Col_CatalogText As String = "CatalogText"
    Public Const Col_Type As String = "Type"
    Public Const Col_Language As String = "Language"
    Public Const Col_SubType As String = "Subtype"
    Public Const Col_PageSize As String = "PageSize"
    Public Const col_SupItem As String = "SupItem"
    Public Const col_SupID As String = "SupID"
    Public Const col_Year As String = "Year"


    'Products dgv columns
    Public Const col_FinalQuan As String = "FinalQuantity"
    Public Const col_Description As String = "Description"
    Public Const col_Status As String = "Status"
    Public Const col_Priority As String = "Priority"
    Public Const col_OrderPlaced As String = "OrderPlaced"
    Public Const col_DueDate As String = "DueDate"
    Public Const col_SubmittedBy As String = "SubmittedBy"
    Public Const col_Comments As String = "Comments"
    Public Const col_CompletedOn As String = "CompletedOn"
    Public Const col_LastUpdated As String = "LastUpdated"
    Public Const col_IsActive As String = "IsActive"
    Public Const col_JobID As String = "JobID"
    Public Const col_JobTicketCreated As String = "JobTicketCreated"

    ''' <summary>
    ''' The null date is: 01/01/1900
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property cNullDate As Date
        Get
            Return New Date(1900, 1, 1)
        End Get
    End Property
    Public Const cNullInt As Integer = -99

    'used to separate between current & archive status in personalize tab
    Public Const cCurrent As String = "Current"
    Public Const cArchive As String = "Archive"

    'Personalize Table Constants
    Public Const Col_OrderNumber As String = "OrderNumber"
    Public Const Col_Item As String = "Item"
    Public Const Col_DesignID As String = "DesignID"
    Public Const Col_Quantity As String = "Quantity"
    Public Const Col_OrderStatus As String = "OrderStatus"
    Public Const Col_PrintStatus As String = "PrintStatus"
    Public Const Col_OrderCreationDate As String = "OrderCreationDate"
    Public Const Col_OrderCreationTime As String = "OrderCreationTime"
    Public Const Col_DateTime As String = "DateTime"
    Public Const Col_StatusHistory As String = "StatusHistory"
    Public Const Col_Labels As String = "labels"
    Public Const Col_Filler As String = "Filler"

    'used to backup the job q's
    Public Const cBackups As String = "Backups"



    ''' <summary>
    ''' extensions that ms word opens.
    ''' </summary>
    Public ReadOnly Property cWordOkExtensions() As List(Of String)
        Get

            Dim ext As List(Of String) = {
                ".doc",
                ".docm",
                ".docx",
                ".docx",
                ".dot",
                ".dotm",
                ".dotx",
                ".htm",
                ".html",
                ".mht",
                ".mhtml",
                ".odt",
                ".rtf",
                ".txt",
                ".wps",
                ".xml",
                ".xps"}.ToList

            Return ext

        End Get
    End Property

    ''' <summary>
    ''' tries to take a string with a month name and convert it to the correct month number
    ''' </summary>
    ''' <param name="whichMonth"></param>
    ''' <returns></returns>
    Public Function FindMonth(ByVal whichMonth As String) As Integer
        whichMonth = whichMonth.ToLower

        Dim months() As String = {"jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec"}

        Dim MoNum As String = ""
        For i As Integer = 0 To months.Count - 1
            If whichMonth.Contains(months(i)) Then
                MoNum = (i + 1).ToString.PadLeft(2, "0") 'if there is only 1 digit in the month number, it adds a 0 to keep it sortable
            End If
        Next
        Return MoNum
    End Function

    ''' <summary>
    ''' Sql Settings For Lineup. Loads and Saves settings to/from sql
    ''' </summary>
    ''' <returns></returns>
    Public Property Settings As Utilities.Settings = Nothing

End Module