Imports System.Xml
Imports System.IO

''' <summary>
''' Info for 1 Personalized Order
''' </summary>
Public Class PersonalizeMxInfo


#Region "Properties"



    Private _MxNumber As String = ""
    ''' <summary>
    ''' Order Number
    ''' </summary>
    ''' <returns></returns>
    Public Property MxNumber As String
        Get
            Return _MxNumber
        End Get
        Set(value As String)
            value = value.ToLower
            If value.Contains("mx") Then
                value = value.Replace("mx", "Mx")
            Else
                value = "Mx" & value
            End If
            _MxNumber = value
        End Set
    End Property
    ''' <summary>
    ''' Returns the Order number as integer
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MxNumberNoMx As Integer
        Get
            Return _MxNumber.ToLower.Replace("mx", "")
        End Get
    End Property

    ''' <summary>
    ''' Customer Number
    ''' </summary>
    ''' <returns></returns>
    Public Property CustomerNumber As Integer = cNullInt




    ''' <summary>
    ''' Email Address for emailing
    ''' </summary>
    ''' <returns></returns>
    Public Property Email As String = ""


    ''' <summary>
    ''' Phone number for order
    ''' </summary>
    ''' <returns></returns>
    Public Property PhoneNum As String = ""

    ''' <summary>
    ''' A customer's Purchase order number
    ''' </summary>
    ''' <returns></returns>
    Public Property PurchaseOrderNumber As String = ""
    ''' <summary>
    ''' Billing Address for order
    ''' </summary>
    ''' <returns></returns>
    Public Property BillAddress As String = ""


    ''' <summary>
    ''' Shipping Address for Order
    ''' </summary>
    ''' <returns></returns>
    Public Property ShipAddress As String = ""


    ''' <summary>
    ''' Shipping note for order
    ''' </summary>
    ''' <returns></returns>
    Public Property ShipNote As String = ""


    ''' <summary>
    ''' Order's receipt number
    ''' </summary>
    ''' <returns></returns>
    Public Property ReceiptNumber As String = ""


    ''' <summary>
    ''' Person who is billed for order
    ''' </summary>
    ''' <returns></returns>
    Public Property BillName As String = ""


    ''' <summary>
    ''' Time the xml was last written to.
    ''' </summary>
    ''' <returns></returns>
    Public Property FileLastWriteTime As New Date

    Public Property PaidBackorder As Boolean = False


    ''' <summary>
    ''' Returns the invoice's pdf file path
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property invoicePdfFilePath As String
        Get
            Return Path.Combine(My.Settings.dirMxOrders, MxNumber & ".pdf")
        End Get
    End Property

    ''' <summary>
    ''' Returns the invoice's txt file path
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property invoiceTxtFilePath As String
        Get
            Return Path.Combine(My.Settings.dirMxOrders, MxNumber & ".txt")
        End Get
    End Property

    Private Property _xmlFilePath As String = ""

    ''' <summary>
    ''' last time the xml file was written to
    ''' </summary>
    ''' <returns></returns>
    Public Property XmlLastReadTime As Date = cNullDate

    Private Property logErrors As Boolean = False

#End Region


#Region "Init"

    ''' <summary>
    ''' New blank class
    ''' </summary>
    Public Sub New()

    End Sub


    ''' <summary>
    ''' Automatically loads the file
    ''' </summary>
    ''' <param name="orderNumber"></param>
    Public Sub New(ByVal orderNumber As Integer, Optional ByVal logThrownErrors As Boolean = True)
        'Dim mxNum As String = "Mx" & orderNumber.ToString
        MxNumber = orderNumber.ToString
        _xmlFilePath = configXmlFilePath()

        If Not ReadMxInfo(_xmlFilePath) Then
            ResetXMLString()
        End If
        logErrors = logThrownErrors
    End Sub
#End Region


#Region "Methods"

    ''' <summary>
    ''' Finds the xml file path of the mx number
    ''' </summary>
    ''' <returns></returns>
    Private Function configXmlFilePath() As String
        Dim fp As String = ""
        Try
        fp = My.Settings.dirMxXML
            If Directory.Exists(fp) Then

                Dim dirInfo As New DirectoryInfo(fp)
                For Each myfile As FileInfo In dirInfo.GetFiles("*" & MxNumberNoMx.ToString & "*") 'universe puts out .txt files sometimes, when the extension should be .xml
                    fp = myfile.FullName
                    Exit For
                Next
            End If
        Catch ex As Exception

        End Try


        Return fp
    End Function


    ''' <summary>
    ''' gets personalize order info from the order's xml file
    ''' </summary>
    ''' <param name="xmlFilePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadMxInfo(ByVal xmlFilePath As String) As Boolean 'Public Shared Function ReadProdData


        Dim StatusOK As Boolean = True
        Dim TagName As String = ""
        Dim ValueStr As String = ""

        'resets the xml strings to ""
        ResetXMLString()


        Try

            If File.Exists(xmlFilePath) Then
                XmlLastReadTime = File.GetLastWriteTime(xmlFilePath)
                Dim loaded As Boolean = False
                Dim ConfigXmlDoc As New XmlDocument

                Try
                    ConfigXmlDoc.Load(xmlFilePath)
                    loaded = True
                Catch ex As Exception

                End Try
                If Not loaded Then
                    Try
                        Dim stream As New StreamReader(xmlFilePath, System.Text.Encoding.Default)
                        ConfigXmlDoc.Load(stream)
                        loaded = True
                    Catch ex As Exception

                    End Try
                End If

                If loaded Then

                    Dim OrderNodeList As XmlNodeList = ConfigXmlDoc.DocumentElement.ChildNodes

                    For Each OrderNode As XmlNode In OrderNodeList

                        Dim OrderNodeChild As XmlNodeList = OrderNode.ChildNodes
                        TagName = OrderNode.Name
                        ValueStr = OrderNode.InnerText

                        Select Case TagName
                            Case "OrderNumber"
                                MxNumber = ValueStr
                            Case "ShipNote"
                                ShipNote = ValueStr

                            Case "CustomerNumber"
                                Try
                                    CustomerNumber = ValueStr
                                Catch ex As Exception
                                    MsgBox(ex.Message, MsgBoxStyle.OkOnly, "CustomerNumberError")
                                End Try


                            Case "ReceiptNumber"
                                ReceiptNumber = ValueStr

                            Case "PurchaseOrderNumber"
                                PurchaseOrderNumber = ValueStr

                            Case "Billing"
                                Dim sb As New Text.StringBuilder

                                For Each BillNode As XmlNode In OrderNodeChild
                                    TagName = BillNode.Name
                                    ValueStr = BillNode.InnerText

                                    Select Case TagName
                                        Case "Name", "AttentionName"
                                            sb.AppendLine(ValueStr)
                                            'BillAddress &= ValueStr & vbCrLf
                                            BillName = ValueStr
                                        Case "Street1", "Street2", "Street3"
                                            sb.AppendLine(ValueStr)

                                        Case "City"
                                            sb.Append(ValueStr & ", ")
                                            'BillAddress &= ValueStr & ", "

                                        Case "State"
                                            sb.Append(ValueStr & " ")
                                            'BillAddress &= ValueStr & " "

                                        Case "Zip"
                                            sb.Append(ValueStr)
                                            'BillAddress &= ValueStr

                                        Case "Country"
                                            sb.AppendLine(ValueStr)

                                        Case "PhoneNumber"
                                            PhoneNum = ValueStr

                                        Case "EmailAddress"
                                            Email = ValueStr

                                        Case Else
                                            sb.AppendLine(ValueStr)
                                            'BillAddress &= ValueStr & vbCrLf

                                    End Select

                                Next BillNode
                                If sb.ToString.Trim <> "" Then
                                    BillAddress = sb.ToString.Trim
                                End If

                            Case "Shipments"
                                For Each Shipment As XmlNode In OrderNodeChild
                                    TagName = Shipment.Name
                                    ValueStr = Shipment.InnerText

                                    Select Case TagName

                                        Case "Shipment"

                                            For Each ShippingAddress As XmlNode In Shipment
                                                TagName = ShippingAddress.Name
                                                ValueStr = ShippingAddress.InnerText
                                                Dim sb As New Text.StringBuilder
                                                Select Case TagName

                                                    Case "ShippingAddress"

                                                        For Each ShippingAddressNode As XmlNode In ShippingAddress
                                                            TagName = ShippingAddressNode.Name
                                                            ValueStr = ShippingAddressNode.InnerText
                                                            Select Case TagName
                                                                Case "Name", "AttentionName"
                                                                    sb.AppendLine(ValueStr)

                                                                Case "Street1", "Street2"
                                                                    sb.AppendLine(ValueStr)

                                                                Case "City"
                                                                    sb.Append(ValueStr & ", ")

                                                                Case "State"
                                                                    sb.Append(ValueStr & " ")

                                                                Case "Zip"
                                                                    sb.Append(ValueStr)

                                                                Case Else
                                                                    sb.AppendLine(ValueStr)
                                                            End Select

                                                        Next ShippingAddressNode
                                                    Case Else
                                                        'Console.WriteLine(ValueStr)
                                                End Select
                                                If sb.ToString.Trim <> "" Then
                                                    ShipAddress &= sb.ToString.Trim(vbCrLf) & vbCrLf
                                                End If


                                            Next ShippingAddress

                                    End Select

                                Next Shipment
                            Case "UpdateDateTime"
                            Case "OrderDate"
                            Case "OrderTime"
                            Case "Totals"
                            Case "Verse"
                            Case "ShipDate"
                            Case "InvoiceDate"
                            Case "CustomerDiscount"
                            Case "OrderNote"
                            Case "FillDate"
                            Case "PaidBackorder"
                                If ValueStr.ToLower = "y" Then PaidBackorder = True

                            Case Else
                                Console.WriteLine(TagName)
                                Console.WriteLine(ValueStr)
                                Beep()
                        End Select
                    Next OrderNode


                    'Dim ShipNodeList As XmlNode = ConfigXmlDoc.SelectSingleNode("Order/Shipments/Shipment/ShippingAddress")
                    If ShipAddress.Trim = "" Then
                        ShipAddress = BillAddress
                    End If
                    FileLastWriteTime = File.GetLastWriteTime(xmlFilePath)

                Else
                    StatusOK = False

                End If


            Else
                StatusOK = False

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            If logErrors Then
                LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
            End If
            'Process.Start(xmlFilePath)
            StatusOK = False
        End Try

        Return StatusOK

    End Function


    ''' <summary>
    ''' Resets the customer xml strings to ""
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetXMLString()
        Email = ""
        PhoneNum = ""
        ShipAddress = ""
        BillAddress = ""
        ShipNote = ""
        ReceiptNumber = ""
        BillName = ""
        MxNumber = ""
    End Sub



#End Region


End Class
