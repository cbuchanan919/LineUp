Public Module Constants
    Public Const Darby As String = "Darby"
    Public Const KJV As String = "KJV"


    Public Const English As String = "English"
    Public Const Abbreviation As String = "Abbreviation"

    Public Const cReference As String = "Reference"
    Public Const cText As String = "text"
    Public Const cFilename As String = "Filename"

    Public Const cVerse As String = "verse"
    Public Const cMonth As String = "Month"
    Public Const cLoadXmlFirst As String = "Please load an XML file first!"

    Public Const cName As String = "Name"

    '----------------------------------------- used with sync -----------------------------------------
    Public Const cSourceRootDir As String = "OriginalRootDir"
    Public Const cDestination As String = "Destination"
    Public Const cFilePath As String = "FilePath"
    Public Const cIsFtp As String = "IsFtp"
    Public Const cFtpServer As String = "FtpServer"
    Public Const cFtpPassword As String = "FtpPassword"
    Public Const cFtpUserName As String = "FtpUserName"
    Public Const cSettings As String = "Settings"
    Public Const cSetting As String = "Setting"
    Public Const cThreadCount As String = "ThreadCount"
    Public Const cDirResources As String = "Resources"
    Public Const cIsEnabled As String = "Enabled"
    Public Const cNickname As String = "Nickname"

    Public Const cProcess As String = "Stopping Sync Process" & vbCrLf
    Public Const SPACER As String = "     "
    Public Const cThreads As String = "Threads: "
    Public Const cSuccess As String = "Success - "
    Public Const cFailed As String = "FAILED - "

    Public Const cEmailUser As String = "EmailFromUser"
    Public Const cEmailPass As String = "EmailPass"
    Public Const cEmailPort As String = "EmailPort"
    Public Const cEmailToAddress As String = "EmailTo"
    Public Const cEmailServer As String = "EmailServer"

    Public Const cSqlUser As String = "SqlUserName"
    Public Const cSqlPass As String = "SqlPass"
    Public Const cSqlDatabase As String = "SqlDatabase"
    Public Const cSqlServer As String = "SqlServer"
    Public Const cSqlTable As String = "SqlTable"
    Public Const cSqlTableHistory As String = "SqlTableHistory"

    Public Const cAny As String = "*"
    Public Const cNone As String = "None"

    Public Const cAllowAll As String = "AllowAllFiles"
    Public Const cExcludedCategory As String = "ExcludedCategory"
    Public Const cExcludedSize As String = "ExcludedSize"
    Public Const cExcludedYear As String = "ExcludedYear"
    Public Const cExcludedExtension As String = "ExcludedExt"

    Public Const cRenameFiles As String = "RenameFiles"

    Public Const cImage As String = "Image"
    Public Const cGeneral As String = "General"
    Public Const cAudio As String = "Audio"
    Public Const cEbook As String = "Ebook"

    Public Const cLarge As String = "Lrg"
    Public Const cMedium As String = "Med"
    Public Const cSmall As String = "Sml"
    Public Const cNonStandard As String = "Nsd"

    Public Const cProduct As String = "Product"
    Public Const cMaster As String = "Master"
    Public Const cLibrary As String = "Library"
    Public Const cPageUp As String = "PageUp"
    Public Const cFinal As String = "Final"
    Public Const cProof2 As String = "Proof2"
    Public Const cProof As String = "Proof"
    Public Const cPrint As String = "Print"

    Public Const cSearch As String = "Search..."
    'Public Const cModifiedFolderLog As String = "Modified Folder Log"
    Public Const cModifiedFilesLog As String = "Modified Files Log"


    '--------------------------------------------------------------------------------------------------


    ''------------------- Receipt Emailer ------------------------
    'Public Const cOrder As String = "Order"
    'Public Const cUpdateDateTime As String = "UpdateDateTime"
    'Public Const cOrderNumber As String = "OrderNumber"
    'Public Const cCustomerNumber As String = "CustomerNumber"
    'Public Const cReceiptNumber As String = "ReceiptNumber"
    'Public Const cPurchaseOrderNumber As String = "PurchaseOrderNumber"
    'Public Const cOrderDate As String = "OrderDate"
    'Public Const cOrderTime As String = "OrderTime"
    'Public Const cFillDate As String = "FillDate"
    'Public Const cShipDate As String = "ShipDate"
    'Public Const cInvoiceDate As String = "InvoiceDate"
    'Public Const cOrderNote As String = "OrderNote"
    'Public Const cShipNote As String = "ShipNote"

    'Public Const cCustomerDiscount As String = "CustomerDiscount"
    'Public Const cCancellationDate As String = "CancellationDate"

    'Public Const cBilling As String = "Billing"
    'Public Const cName As String = "Name"
    'Public Const cAttentionName As String = "AttentionName"
    'Public Const cStreet1 As String = "Street1"
    'Public Const cStreet2 As String = "Street2"
    'Public Const cStreet3 As String = "Street3"
    'Public Const cCity As String = "City"
    'Public Const cState As String = "State"
    'Public Const cZip As String = "Zip"
    'Public Const cCountry As String = "Country"
    'Public Const cPhoneNumber As String = "PhoneNumber"
    'Public Const cEmailAddress As String = "EmailAddress"

    'Public Const cShipments As String = "Shipments"
    'Public Const cShipment As String = "Shipment"
    'Public Const cShippingAddress As String = "ShippingAddress"
    'Public Const cItems As String = "Items"
    'Public Const cItem As String = "Item"
    'Public Const cProductID As String = "ProductID"
    'Public Const cYear As String = "Year"
    'Public Const cQuantity As String = "Quantity"
    'Public Const cDesignID As String = "DesignID"
    'Public Const cOrderStatus As String = "OrderStatus"
    'Public Const cUnitPrice As String = "UnitPrice"




    'Public Const td As String = "<td>"
    'Public Const td15WidthAlignRight As String = "<td style=""width:15%; text-align: right;"">"
    'Public Const td15Width As String = "<td style=""width:15%;"">"
    'Public Const tdPad10Left As String = "<td style=""padding-left:10px"">"
    'Public Const tdAlignTop As String = "<td style=""vertical-align:top;"">"
    'Public Const tdAlignTopPadLeft20px As String = "<td style=""vertical-align:top; padding-left:20px;"">"
    'Public Const td2ColSpanAlignRightPadRight As String = "<td colspan=""2"" style=""text-align:right; padding-right:10px"">"
    'Public Const tdEnd As String = "</td>"


    'Public Const trLineBreak As String = "<tr><td style=""height: 20px""  colspan=""7""></td></tr> <!-- line break-->"
    'Public Const trDivider As String = "<tr><td style=""height: 1px; background-color: #cccccc;""  colspan=""7""></td></tr>"
    'Public Const tr As String = "<tr>"
    'Public Const trAltColor As String = "<tr style=""background-color:azure;"">"
    'Public Const trEnd As String = "</tr>"
    'Public Const nbsp As String = "&nbsp;"
    'Public Const strong As String = "<strong>"
    'Public Const strongEnd As String = "</strong>"
    'Public Const br As String = "<br/>"
    ''------------------------------------------------------------

    Public Const cValue As String = "Value"
    Public Const cHelp As String = "Help"
    Public Const cOrganized As String = "Organized"
End Module
