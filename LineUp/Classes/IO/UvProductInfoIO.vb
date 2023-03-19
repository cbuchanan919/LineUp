
''' <summary>
''' Reads sql table to get product data, if not found, contacts uv server.
''' Stores each product info as uvProductInfo
''' </summary>
''' <remarks>"Find_Product" will find the related btpID and store it in variables</remarks>
Public Class UvProductInfoIO


#Region "Properties and Variables"

    Private Property SqlInfo As SQLConnectionUtilities

    Private Property UvUserName As String = ""
    Private Property UvPassword As String = ""
    Private Property UV As UniVerseConnection = Nothing

    Public Property ProductsWereRead As Boolean = False

    Private Property FailedUvReadTries As Integer = 0
    Private Property FailedSqlReadTries As Integer = 0

    Public Property productDict() As New SortedDictionary(Of Integer, UvProductInfo)
    Private Property uvLoaded As Boolean = False

    Public Enum InfoLoadOptions
        DoNotLoad
        UV
        UvAsThread
    End Enum

    Private Property LoadedFrom As InfoLoadOptions = InfoLoadOptions.UvAsThread

    ''' <summary>
    ''' generates a dataset from the different products found.
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property prodInfoDS As DataSet
        Get
            Dim newDs As New DataSet("ProductInfos")
            Dim tb As New DataTable("Products")

            Dim ColNames() As String = {Col_ProductID,
                                        Col_Title,
                                        Col_TitleInverted,
                                        Col_Price,
                                        Col_Pages,
                                        Col_PageSize,
                                        Col_Author,
                                        Col_Medium,
                                        Col_Source,
                                        Col_WebText,
                                        Col_CatalogText,
                                        Col_Type,
                                        Col_Language,
                                        Col_SubType,
                                        col_SupItem,
                                        col_SupID,
                                        col_Year,
                                        "QuanOrdered"}


            For Each Col In ColNames
                tb.Columns.Add(Col)
            Next
            Dim prodKeys As New List(Of Integer)
            For Each myKey As String In productDict.Keys
                If IsNumeric(myKey) Then
                    prodKeys.Add(CInt(myKey))

                End If
            Next
            'prodKeys.AddRange(productDict.Keys)
            prodKeys.Sort()
            For Each key As String In prodKeys
                Dim prod As UvProductInfo = productDict(key)
                Dim NewDR As DataRow = tb.NewRow
                With prod
                    NewDR(Col_ProductID) = .ItemNum
                    NewDR(Col_Title) = .Title
                    NewDR(Col_TitleInverted) = .InvTitle
                    NewDR(Col_Price) = .PricePer
                    NewDR(Col_Pages) = .PageCt
                    NewDR(Col_PageSize) = .PageSize
                    NewDR(Col_Author) = .Author
                    NewDR(Col_Medium) = .Type
                    NewDR(Col_Source) = .Source
                    NewDR(Col_WebText) = .WebText
                    NewDR(Col_CatalogText) = .CatalogText
                    NewDR(Col_Type) = .TypeUV
                    NewDR(Col_Language) = .Language
                    NewDR(Col_SubType) = .SubType
                    NewDR(col_SupItem) = .SupItem
                    NewDR(col_SupID) = .SupID
                    NewDR(col_Year) = .Year
                    NewDR("QuanOrdered") = .TempOrdered
                End With
                tb.Rows.Add(NewDR)
            Next

            newDs.Tables.Add(tb)
            Return newDs
        End Get
    End Property



#End Region


#Region "Init"
    ''' <summary>
    ''' loads products from UV
    ''' </summary>
    Public Sub New(ByVal UvUserName As String,
                   ByVal UvPassword As String,
                   ByVal SqlInfo As Utilities.SQLConnectionUtilities,
                   Optional ByVal loadFrom As InfoLoadOptions = InfoLoadOptions.UV)

        Me.SqlInfo = SqlInfo
        Me.UvUserName = UvUserName
        Me.UvPassword = UvPassword
        LoadedFrom = loadFrom

        UV = New Utilities.UniVerseConnection(UvUserName, UvPassword)

        ReadProducts()


    End Sub

#End Region


#Region "Methods"


    Public Function clone() As UvProductInfoIO
        Dim newProds As New UvProductInfoIO(UvUserName, UvPassword, SqlInfo, InfoLoadOptions.DoNotLoad)

        Try
            If Not ProductsWereRead Then
                'nothing to clone
                newProds = New UvProductInfoIO(UvUserName, UvPassword, SqlInfo, InfoLoadOptions.UvAsThread)
            Else
                For Each myKey As String In productDict.Keys
                    If Not newProds.productDict.ContainsKey(myKey) Then
                        newProds.productDict.Add(myKey, Nothing)
                    End If
                    newProds.productDict(myKey) = productDict(myKey).clone
                Next
                newProds.ProductsWereRead = True

            End If
            newProds.LoadedFrom = LoadedFrom
            Return newProds
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "ProdID Clone")
        End Try
        Return newProds
    End Function

    ''' <summary>
    ''' reads the products from the location specified (either xml or sql) at init.
    ''' </summary>
    ''' <returns></returns>
    Public Function ReadProducts() As Boolean
        Dim success As Boolean = False

        Try

            Select Case LoadedFrom
                Case InfoLoadOptions.UvAsThread

                    Dim threadStart As New Threading.ThreadStart(AddressOf startProductRead)
                    Dim thread As New Threading.Thread(threadStart)

                    thread.IsBackground = True
                    thread.Priority = Threading.ThreadPriority.BelowNormal
                    thread.Start()
                    success = True
                Case InfoLoadOptions.UV
                    startProductRead()
                    success = True

                Case Else

            End Select
        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "LoadProducts - uvProdInfoIO")
        End Try
        Return success
    End Function
    Dim b As Integer = 0
    Public Sub startProductRead() 'ByVal existingUVprodInfoIO As uvProdInfoIO

        ReadProductsFromSQL()


        ' - - - - - - update shareword items - - - - - - 
        Dim query As String = "SELECT ITEM, TITLE, SUBTITLE, INVERT, PRICE, PAGES, AUTHOR, LANGUAGE, TYPE, SOURCE, YEAR, " &
            UV.quoted({"PAGE.SIZE", "COVER.2", "SUP.ITEM", "SUB.TYPE", "WEB.TEXT", "CATALOG.TEXT", "SUP.ID"}) & " FROM PRODUCTS " ' WHERE " &
        'UV.quoted("SUP.ID") & "=9358"

        ReadProdsFromUV(query, "", True)
        uvLoaded = True
    End Sub


    Private Function ReadProductsFromSQL() As Boolean
        Dim success As Boolean = False

        Try

            If FailedSqlReadTries <= 3 Then
                productDict.Clear()
                Dim SqlInfoCopy As Utilities.SQLConnectionUtilities = SqlInfo.Copy("", My.Settings.SqlProductsDatabase)
                Using conn As New SqlClient.SqlConnection(SqlInfoCopy.sqlConnStr)
                    Using cmd As SqlClient.SqlCommand = conn.CreateCommand
                        Dim cols As List(Of String) = {"ProductID", "Title", "TitleInverted", "Price", "Pages", "AuthorInverted", "Medium", "Source", "WebText", "CatalogText", "TypeUV", "Language2", "Subtype"}.ToList
                        Dim query As String = ""
                        query &= "SELECT " & String.Join(", ", cols)
                        query &= " FROM Products"
                        cmd.CommandText = query
                        conn.Open()
                        Using reader As SqlClient.SqlDataReader = cmd.ExecuteReader()
                            While reader.Read
                                Dim prod As New UvProductInfo(False)
                                Try

                                    prod.ItemNum = reader.GetInt32(cols.IndexOf("ProductID"))
                                    prod.Title = reader.GetString(cols.IndexOf("Title"))
                                    prod.InvTitle = reader.GetString(cols.IndexOf("TitleInverted"))
                                    prod.PricePer = reader.GetSqlMoney(cols.IndexOf("Price"))
                                    prod.SalePrice = reader.GetSqlMoney(cols.IndexOf("Price"))
                                    prod.PageCt = reader.GetInt32(cols.IndexOf("Pages"))
                                    prod.Author = reader.GetString(cols.IndexOf("AuthorInverted"))
                                    prod.Type = reader.GetString(cols.IndexOf("Medium"))
                                    prod.Source = reader.GetInt32(cols.IndexOf("Source"))
                                    prod.WebText = reader.GetString(cols.IndexOf("WebText"))
                                    prod.CatalogText = reader.GetString(cols.IndexOf("CatalogText"))
                                    prod.TypeUV = reader.GetString(cols.IndexOf("TypeUV"))
                                    prod.Language = reader.GetString(cols.IndexOf("Language2"))
                                    prod.SubType = reader.GetString(cols.IndexOf("Subtype"))
                                    prod.MatchedOK = True
                                    If Not productDict.ContainsKey(prod.ItemNum) Then
                                        productDict.Add(prod.ItemNum, Nothing)
                                    End If

                                    productDict(prod.ItemNum) = prod
                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                End Try
                            End While
                        End Using
                    End Using
                End Using


                success = True
                ProductsWereRead = True

            End If

        Catch ex As Exception
            FailedSqlReadTries += 1
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "ReadProductsFromSQL")
        End Try
        Return success
    End Function


    ''' <summary>
    ''' Returns the items found from UV. If item number supplied, max items it will return is 1.
    ''' </summary>
    ''' <param name="query">sql statement to use</param>
    ''' <param name="itemNumber">returns only the specified item if found</param>
    ''' <returns></returns>
    Private Function ReadProdsFromUV(ByVal query As String, Optional ByVal itemNumber As String = "", Optional updateProdDict As Boolean = False) As List(Of UvProductInfo)
        Dim prods As New Dictionary(Of Integer, UvProductInfo)
        Dim prod As UvProductInfo = Nothing
        Dim err As New Text.StringBuilder
        Try
            If FailedUvReadTries <= 3 Then


                If UV.UVAccess(query) Then
                    For Each myRow As DataRow In UV.DS.Tables(0).Rows
                        Try
                            If Not IsNothing(prod) AndAlso prod.ItemNum.ToString <> myRow("ITEM").ToString Then
                                'new product row. 
                                If prods.ContainsKey(prod.ItemNum) Then
                                    'error...
                                    'MsgBox(prod.ToString)
                                ElseIf prod.ItemNum > cNullInt Then
                                    'adds the product to the dict.
                                    prod.MatchedOK = True
                                    prod.ConfigTitle()
                                    prods.Add(prod.ItemNum, prod)
                                End If
                                prod = New UvProductInfo(False)
                            Else
                                'Beep()
                            End If

                            If IsNothing(prod) Then
                                'after previous if/end if so that it doesn't add a blank item to the dict.
                                prod = New UvProductInfo(True)
                            End If


                            For Each myCol As DataColumn In UV.DS.Tables(0).Columns
                                Dim cellValue As String = myRow(myCol).ToString
                                Dim cellValueInt As Integer = cNullInt
                                Dim cellValueDec As Decimal = cNullInt
                                Integer.TryParse(cellValue, cellValueInt)
                                Decimal.TryParse(cellValue, cellValueDec)

                                Select Case myCol.ColumnName
                                    Case "ITEM"
                                        If cellValue <> "" AndAlso IsNumeric(cellValue) Then
                                            prod.ItemNum = cellValue
                                        End If
                                    Case "TITLE"
                                        prod.Title = cellValue
                                    Case "SUBTITLE"
                                        prod.SubTitle = cellValue
                                    Case "INVERT"
                                        prod.InvTitle = cellValue
                                    Case "PRICE"
                                        If cellValue <> "" AndAlso IsNumeric(cellValue) Then
                                            If cellValueDec.ToString = cellValue Or cellValueDec.ToString("C") = cellValue Then
                                                prod.PricePer = cellValueDec
                                                prod.SalePrice = cellValueDec
                                            End If
                                        End If

                                    Case "PAGES"
                                        prod.PageCt = cellValueInt
                                    Case "AUTHOR"
                                        prod.Author = cellValue
                                    Case "LANGUAGE"
                                        prod.Language = cellValue
                                    Case "SUB.TYPE", "SUB_TYPE"
                                        prod.SubType = cellValue
                                    Case "SOURCE"
                                        prod.Source = cellValue
                                    Case "YEAR"
                                        prod.Year = cellValue
                                    Case "COVER.2", "COVER_2"
                                        prod.Type = cellValue
                                    Case "WEB.TEXT", "WEB_TEXT"
                                        prod.WebText &= cellValue & " "
                                    Case "CATALOG.TEXT", "CATALOG_TEXT"
                                        prod.CatalogText &= cellValue & " "
                                    Case "TYPE"
                                        prod.TypeUV = cellValue
                                    Case "PAGE.SIZE", "PAGE_SIZE"
                                        prod.PageSize = cellValue
                                    Case "SUP.ITEM", "SUP_ITEM"
                                        prod.SupItem = cellValue
                                    Case "SUP.ID", "SUP_ID"
                                        prod.SupID = cellValue


                                End Select

                            Next

                        Catch ex As Exception
                            err.AppendLine(ex.Message)
                            'MsgBox(ex.Message)
                        End Try

                    Next
                    UV.DS.Clear()
                    UV.DS = New DataSet()

                    If Not IsNothing(prod) AndAlso Not prods.ContainsKey(prod.ItemNum) Then
                        'this is at the end... because each id can have multiple rows?
                        prod.MatchedOK = True
                        prod.ConfigTitle()
                        prods.Add(prod.ItemNum, prod)
                    End If
                    ProductsWereRead = True
                End If
            End If



            If updateProdDict Then
                For Each myKey As String In prods.Keys
                    If productDict.ContainsKey(myKey) Then
                        'hopefully it gets most data from sql, if not, adds additional from uv.
                        productDict(myKey).SupID = prods(myKey).SupID
                        productDict(myKey).SupItem = prods(myKey).SupItem
                        productDict(myKey).Source = prods(myKey).Source
                    Else
                        productDict.Add(myKey, prods(myKey))
                    End If
                Next
            End If

        Catch ex As Exception
            FailedUvReadTries += 1
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "getProdsFromUV")
            err.AppendLine(ex.Message)
        End Try
        If err.ToString <> "" Then
            MsgBox("(Number of Failed UV Read Tries: " & FailedUvReadTries & ")" & vbCrLf & vbCrLf & "The following errors were seen when trying to read products from UV:" & vbCrLf & vbCrLf & err.ToString, MsgBoxStyle.Exclamation, "Reading UV Product Data")
        End If






        Dim prodList As New List(Of UvProductInfo)
        For Each myKey As String In prods.Keys
            If itemNumber = "" Then
                prodList.Add(prods(myKey))
            ElseIf myKey = itemNumber Then
                prodList.Add(prods(myKey))
                Return prodList
            End If

        Next
        Return prodList
    End Function


    ''' <summary>
    ''' returns the product info that it finds.
    ''' </summary>
    ''' <param name="ProdNumber"></param>
    ''' <param name="ErrorMessage"></param>
    ''' <returns></returns>
    Public Function findProduct(ByVal ProdNumber As String, Optional ByRef ErrorMessage As String = "") As UvProductInfo
        Dim prod As UvProductInfo = Nothing

        Try
            If ProdNumber <> "" Then
                If productDict.ContainsKey(ProdNumber) Then
                    'record found in xml / product dict.
                    prod = productDict(ProdNumber)

                ElseIf uvLoaded Then
                    'calling uv from 
                    prod = New UvProductInfo(False)
                    prod.ItemNum = ProdNumber


                    If IsNumeric(ProdNumber) Then
                        If LineUp.LineUpLoaded Then
                            'Dim UVItem As New uvProductInfo(Number, Title, TitleInverted, Price, Pages, Author, Medium, Source, WebText, CatalogText, Type, Language, Subtype, Price, True)
                            Dim query As String = "Select @ID, TITLE, SUBTITLE, INVERT, PRICE, PAGES, AUTHOR, LANGUAGE, Type, SOURCE, Year, " &
                                UV.quoted({"COVER.2", "SUP.ITEM", "SUB.TYPE", "WEB.TEXT", "CATALOG.TEXT", "SUP.ID"}) & " FROM PRODUCTS WHERE @ID = '" & ProdNumber & "'" 'WHERE ITEM = 
                            'query re-dimmed to keep orig. with Mark's new uv interface i don't think it needs double quotes?
                            query = "Select @ID, TITLE, SUBTITLE, INVERT, PRICE, PAGES, AUTHOR, LANGUAGE, TYPE, SOURCE, YEAR, " &
                                "COVER.2, SUP.ITEM, SUB.TYPE, WEB.TEXT, CATALOG.TEXT, SUP.ID FROM PRODUCTS WHERE @ID = '" & ProdNumber & "'"
                            Dim foundProds As List(Of UvProductInfo) = ReadProdsFromUV(query, ProdNumber)
                            If foundProds.Count = 1 Then
                                prod = foundProds(0)
                            End If
                        End If


                    End If

                    If Not productDict.ContainsKey(ProdNumber) Then
                        'added in this check to avoid errors that seemed to happen.
                        productDict.Add(ProdNumber, prod)
                    End If
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            ErrorMessage &= ex.Message
        End Try
        Return prod
    End Function



#Region "Archive"




    '''' <summary>
    '''' Loads / reloads the Product Data Table
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function ReadProductsFromXml(Optional ByRef ErrorMessage As String = "") As Boolean
    '    Dim Success As Boolean = True

    '    Try
    '        productDict.Clear()

    '        If File.Exists(ProductXmlFilePath) Then

    '            Dim TagName As String = ""
    '            Dim ValueStr As String = ""


    '            Dim ProductsXmlDoc As New XmlDocument
    '            ProductsXmlDoc.Load(ProductXmlFilePath)

    '            Dim ProductsXmlNodeList As XmlNodeList = ProductsXmlDoc.DocumentElement.ChildNodes

    '            For Each ProductNode As XmlNode In ProductsXmlNodeList
    '                TagName = ProductNode.Name
    '                ValueStr = ProductNode.InnerText

    '                Select Case TagName
    '                    Case "Product"
    '                        Dim prod As New uvProductInfo(False)


    '                        Dim ProductInfoNodeList As XmlNodeList = ProductNode.ChildNodes
    '                        For Each ProductInfoNode As XmlNode In ProductInfoNodeList
    '                            TagName = ProductInfoNode.Name
    '                            ValueStr = ProductInfoNode.InnerText
    '                            Select Case TagName

    '                                Case Col_Number, Col_ProductID
    '                                    prod.ItemNum = ValueStr
    '                                Case Col_Title
    '                                    prod.Title = ValueStr
    '                                Case Col_TitleInverted
    '                                    prod.InvTitle = ValueStr
    '                                Case Col_Price
    '                                    prod.PricePer = ValueStr
    '                                    prod.SalePrice = ValueStr
    '                                Case Col_Pages
    '                                    prod.PageCt = ValueStr
    '                                Case Col_Author
    '                                    prod.Author = ValueStr
    '                                Case Col_Medium
    '                                    prod.Type = ValueStr
    '                                Case Col_Source
    '                                    prod.Source = ValueStr
    '                                Case Col_WebText
    '                                    prod.WebText = ValueStr
    '                                Case Col_CatalogText
    '                                    prod.CatalogText = ValueStr
    '                                Case Col_Type
    '                                    prod.UVType = ValueStr
    '                                Case Col_Language
    '                                    prod.Language = ValueStr
    '                                Case Col_SubType
    '                                    prod.SubType = ValueStr
    '                            End Select

    '                        Next
    '                        prod.MatchedOK = True
    '                        'Dim UVItem As New uvProductInfo(Number, Title, TitleInverted, Price, Pages, Author, Medium, Source, WebText, CatalogText, Type, Language, Subtype, Price, True)
    '                        If productDict.ContainsKey(prod.ItemNum) Then
    '                            MsgBox(prod.ToString, MsgBoxStyle.Information, "Item found again...")
    '                        Else
    '                            productDict.Add(prod.ItemNum, prod)
    '                        End If

    '                        'Dim NewDR As DataRow = XmlSet.Tables(0).NewRow
    '                        'NewDR.ItemArray = {prod.itemNum,
    '                        '                    prod.title,
    '                        '                    prod.invTitle,
    '                        '                    prod.pricePer,
    '                        '                    prod.pageCt,
    '                        '                    prod.author,
    '                        '                    prod.type,
    '                        '                    prod.source,
    '                        '                    prod.webText,
    '                        '                    prod.catalogText,
    '                        '                    prod.UVType,
    '                        '                    prod.language,
    '                        '                    prod.subType}

    '                        'XmlSet.Tables(0).Rows.Add(NewDR)
    '                End Select



    '            Next
    '            'Dim ds As New DataSet
    '            'ds = XmlSet.Copy
    '            ProductsWereRead = True
    '        Else
    '            Success = False
    '            ErrorMessage = "File not found - " & vbCrLf & """" & ProductXmlFilePath & """"
    '        End If

    '    Catch ex As Exception
    '        ErrorMessage = ex.Message
    '        Success = False
    '    End Try

    '    Return Success
    'End Function

    'Public Function findProduct(ByVal ProdNumber As String, Optional ByRef ErrorMessage As String = "") As Boolean


    '    Dim FoundIt As Boolean = False
    '    Try
    '        If IsNumeric(ProdNumber) Then

    '            'tries to make sure not to get a isNull error
    '            If IsNothing(XmlSet) Then
    '                loadProducts()
    '            ElseIf IsNothing(XmlSet.Tables(0)) Then
    '                loadProducts()
    '            ElseIf XmlSet.Tables(0).Columns.Count < 5 Then
    '                loadProducts()
    '            End If

    '            For Each myRow As DataRow In XmlSet.Tables(0).Rows
    '                If FoundIt = False Then
    '                    If Not IsDBNull(myRow.Item(0)) Then
    '                        If myRow.Item(0).ToString = ProdNumber Then
    '                            strItemNum = myRow.Item("Number").ToString
    '                            strTitle = myRow.Item("Title").ToString
    '                            strInvTitle = myRow.Item("TitleInverted").ToString
    '                            strSalePrice = myRow.Item("Price").ToString
    '                            StrPricePer = strSalePrice
    '                            strPageCt = myRow.Item("Pages").ToString
    '                            strAuthor = myRow.Item("Author").ToString
    '                            strType = myRow.Item("Medium").ToString
    '                            strSource = myRow.Item("Source").ToString
    '                            strWebText = myRow.Item("WebText").ToString
    '                            strCatalogText = myRow.Item("CatalogText").ToString
    '                            struvType = myRow.Item("Type").ToString
    '                            strLanguage = myRow.Item("Language").ToString
    '                            strSubType = myRow.Item("Subtype").ToString
    '                            FoundIt = True
    '                            Exit For
    '                        End If
    '                    End If
    '                End If
    '            Next

    '            If FoundIt Then
    '                If strSource = "" Then
    '                    strSource = "In House"
    '                End If

    '            End If


    '        Else
    '            FoundIt = False
    '        End If
    '    Catch ex As Exception
    '        FoundIt = False
    '        ErrorMessage = ex.Message
    '    End Try

    '    Return FoundIt
    'End Function

#End Region


#End Region


End Class