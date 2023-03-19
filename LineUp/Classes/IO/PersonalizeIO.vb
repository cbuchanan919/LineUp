Imports System
Imports System.IO
Imports System.Xml
Imports System.Threading.Tasks


''' <summary>
''' Loads the dgv PersonalizeXml page
''' </summary>
Public Class PersonalizeIO


#Region "Properties"

    Private Property XmlFilePath As String = ""


    Public Property AllPersonalizeRows As New List(Of PersonalizeRowInfo)
    Public Property DisplayedPersonalizeRows As New ComponentModel.BindingList(Of PersonalizeRowInfo)


    Private Property ActiveCounts() As New Dictionary(Of String, Integer)

    Private Property ArchiveCounts() As New Dictionary(Of String, Integer)

    Public Property PersonalizeTabText() As String = ""

    ''' <summary>
    ''' this records the last updated time of the personalized Job Q
    ''' </summary>
    ''' <returns></returns>
    Public Property PersonalizXmlUpdatedLast As Date = cNullDate

    Private Property frmLineUp As LineUp




    Public Enum SearchCategory
        All
        Archive
        Current
    End Enum




#End Region


#Region "Init"

    Public Sub New(ByVal newXmlFilePath As String, ByVal mainForm As LineUp)
        XmlFilePath = newXmlFilePath
        frmLineUp = mainForm
        AllPersonalizeRows = New List(Of PersonalizeRowInfo)
        DisplayedPersonalizeRows = New ComponentModel.BindingList(Of PersonalizeRowInfo)
    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Loads the personalize XML (or SQL if I get to it)
    ''' </summary>
    Public Async Sub LoadPersonalizedInfo()

        Await ReadPersonalizedXml()
        UpdatePQSaveTime()
        UpdateCounts()
        frmLineUp.ReceiveSuccess()

    End Sub


    'this is just an example of an individual order
    '<Order>
    '  <OrderNumber>147148</OrderNumber>
    '  <Item>8101</Item>
    '  <DesignID>10-14-35250</DesignID>
    '  <Quantity>130</Quantity>
    '  <OrderStatus>Paid</OrderStatus>
    '  <PrintStatus>Shipped</PrintStatus>
    '  <OrderCreationDate>10/08/2013</OrderCreationDate>
    '  <OrderCreationTime>01:46</OrderCreationTime>
    '  <DateTime>2013 10 08 16:50 Archive</DateTime>
    '  <StatusHistory>5/12/2015 - Shipped;10/13/2015 - Shipped;</StatusHistory>
    '</Order>



    Private Function ReadPersonalizedXml() As Task(Of Boolean)


        Dim tResult As Task(Of Boolean) =
            Task.Run(Function() As Boolean

                         Dim xr As XmlReader = Nothing
                         Dim success As Boolean = True

                         Try
                             DisplayedPersonalizeRows = New ComponentModel.BindingList(Of PersonalizeRowInfo)
                             AllPersonalizeRows = New List(Of PersonalizeRowInfo)

                             Dim CurProgress As Integer = 0
                             PersonalizeTabText = ""
                             Dim TagName As String = ""
                             Dim ValueStr As String = ""


                             Dim ProductsXmlDoc As New XmlDocument
                             Dim fs As New FileStream(XmlFilePath, FileMode.Open, FileAccess.Read)
                             xr = XmlReader.Create(fs) 'XmlFilePath
                             ProductsXmlDoc.Load(xr)
                             fs.Close()
                             Dim ProductsXmlNodeList As XmlNodeList = ProductsXmlDoc.DocumentElement.ChildNodes


                             'Dim i As Integer = 0
                             Dim MaxValue As Integer = ProductsXmlNodeList.Count
                             frmLineUp.ReceiveProgress(CurProgress, MaxValue)





                             For Each ProductNode As XmlNode In ProductsXmlNodeList
                                 CurProgress += 1
                                 frmLineUp.ReceiveProgress(CurProgress, MaxValue)

                                 TagName = ProductNode.Name
                                 ValueStr = ProductNode.InnerText

                                 Select Case TagName
                                     Case "Order"


                                         'Dim NewDR As DataRow = data.DesignsDS.Tables(0).NewRow
                                         Dim row As New PersonalizeRowInfo

                                         Dim odate As String = ""  'date
                                         Dim tod As String = "" 'time of day

                                         Dim ProductInfoNodeList As XmlNodeList = ProductNode.ChildNodes
                                         For Each ProductInfoNode As XmlNode In ProductInfoNodeList

                                             TagName = ProductInfoNode.Name
                                             ValueStr = ProductInfoNode.InnerText
                                             'NewDR(TagName) = ValueStr

                                             Select Case TagName
                                                 Case Col_OrderNumber
                                                     row.MxNumber = ValueStr
                                                 Case Col_Item
                                                     row.ItemNumber = ValueStr
                                                 Case Col_DesignID
                                                     row.DesignId = ValueStr
                                                 Case Col_Quantity
                                                     row.DesignQuantity = ValueStr
                                                 Case Col_OrderStatus
                                                     row.UvStatus = ValueStr
                                                 Case Col_PrintStatus
                                                     row.PrintStatus = ValueStr
                                                 Case Col_OrderCreationDate
                                                     odate = ValueStr
                                                 Case Col_OrderCreationTime
                                                     tod = ValueStr
                                                 Case Col_DateTime
                                                     Dim lower As String = ValueStr.ToLower
                                                     If lower.Contains(cArchive.ToLower) Then
                                                         row.IsCurrent = False
                                                     ElseIf lower.Contains(cCurrent.ToLower) Then
                                                         row.IsCurrent = True
                                                     Else
                                                         row.IsCurrent = True
                                                     End If
                                                 Case Col_StatusHistory
                                                     For Each history As String In ValueStr.Split(";")
                                                         If history.Trim <> "" Then
                                                             row.StatusHistory.Add(history.Trim)
                                                         End If
                                                     Next
                                                 Case Col_Labels
                                                     For Each history As String In ValueStr.Split(";")
                                                         If history.Trim <> "" Then
                                                             row.LabelHistory.Add(history.Trim)
                                                         End If
                                                     Next
                                                 Case Else
                                                     MsgBox(TagName)
                                             End Select

                                         Next

                                         'Parse date
                                         Dim result As Date = cNullDate
                                         If Date.TryParse(odate & " " & tod, result) Then
                                             row.OrderCreated = result
                                         Else
                                             row.OrderCreated = Now
                                         End If

                                         'row.ConfigureFilePaths(LineUp.CBConfig)
                                         row.LastToString = row.ToString(False)

                                         'DisplayedPersonalizeRows.Add(row)
                                         AllPersonalizeRows.Add(row)
                                     Case Else
                                         MsgBox(ValueStr, , "XMLReadCustomOrders")
                                 End Select



                             Next

                             xr.Close()
                             xr.Dispose()






                             'ET = DateTime.Now
                             'MsgBox((ET - ST).Milliseconds)

                         Catch ex As Exception
                             success = False
                             MsgBox("Reading XML Error: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "ReadPersonalizedXml")
                         Finally
                             If Not IsNothing(xr) Then
                                 xr.Close()
                                 xr.Dispose()
                             End If
                         End Try
                         Return success
                     End Function)




        Return tResult


    End Function

    ''' <summary>
    ''' Writes the personalied xml to my.settings.mxcustompath
    ''' </summary>
    ''' <returns></returns>
    Public Function WritePersonalizedXml() As Boolean
        Dim success As Boolean = False
        Try
            Dim sb As New Text.StringBuilder


            Dim startT As DateTime = Now
            Dim WriteNecessary As Boolean = False
            For Each allRow As PersonalizeRowInfo In AllPersonalizeRows
                Dim newToString As String = allRow.ToString(False)
                If allRow.LastToString <> newToString Then
                    WriteNecessary = True
                    allRow.LastToString = newToString
                End If
            Next
            If WriteNecessary Then

                Dim fp As String = IO.Path.Combine(My.Settings.MxCustomPath) '.Replace(".xml", "-v2.xml"))
                Dim xmlSettings As New XmlWriterSettings
                xmlSettings.OmitXmlDeclaration = False
                xmlSettings.Indent = True
                Dim writer As XmlWriter = XmlWriter.Create(fp, xmlSettings)
                writer.WriteStartDocument()
                writer.WriteStartElement("Orders")
                For Each allRow As PersonalizeRowInfo In AllPersonalizeRows
                    writer.WriteStartElement("Order")

                    WriteXmlSafeString(Col_OrderNumber, allRow.MxNumberNoMX, writer)
                    WriteXmlSafeString(Col_Item, allRow.ItemNumber, writer)
                    WriteXmlSafeString(Col_DesignID, allRow.DesignId, writer)
                    WriteXmlSafeString(Col_Quantity, allRow.DesignQuantity, writer)
                    WriteXmlSafeString(Col_OrderStatus, allRow.UvStatus, writer)
                    WriteXmlSafeString(Col_PrintStatus, allRow.PrintStatus, writer)
                    WriteXmlSafeString(Col_OrderCreationDate, allRow.OrderCreated.ToShortDateString, writer)
                    WriteXmlSafeString(Col_OrderCreationTime, allRow.OrderCreated.ToString("hh:mmtt").ToLower, writer)
                    If allRow.IsCurrent Then
                        WriteXmlSafeString(Col_DateTime, allRow.OrderCreated.ToString("yyyy MM dd HH:mm") & " Current", writer)
                    Else
                        WriteXmlSafeString(Col_DateTime, allRow.OrderCreated.ToString("yyyy MM dd HH:mm") & " Archive", writer)
                    End If
                    WriteXmlSafeString(Col_StatusHistory, String.Join(";", allRow.StatusHistory), writer)
                    WriteXmlSafeString(Col_Labels, String.Join(";", allRow.LabelHistory), writer)

                    writer.WriteEndElement()
                Next
                writer.WriteEndElement()
                writer.WriteEndDocument()
                writer.Flush()
                writer.Close()

                UpdatePQSaveTime()

                Console.WriteLine("Writing to XML" & vbCrLf & "Duration: " & (Now - startT).ToString)
            End If


            success = True
        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Write Personalized Xml Error")
        End Try
        Return success
    End Function

    Private Sub WriteXmlSafeString(ByVal columnName As String, ByVal value As String, ByRef writer As XmlWriter)
        value = value.Trim
        Dim sb As New Text.StringBuilder
        Dim ch As Char
        For i As Integer = 0 To value.Length - 1
            ch = value(i)
            If XmlConvert.IsXmlChar(ch) Then
                sb.Append(ch)
            End If
        Next
        writer.WriteStartElement(columnName)
        writer.WriteString(sb.ToString)
        writer.WriteEndElement()
        ' writer.Flush()


    End Sub

    ''' <summary>
    ''' Updates the time the file was last updated. It should get updated for each local save.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdatePQSaveTime()
        PersonalizXmlUpdatedLast = File.GetLastWriteTime(My.Settings.MxCustomPath)
    End Sub



    Private Function UpdateCounts() As Boolean
        Dim success As Boolean = False
        Try
            ArchiveCounts = New Dictionary(Of String, Integer)
            ActiveCounts = New Dictionary(Of String, Integer)
            Dim intNewIds As Integer = 0

            For Each row As PersonalizeRowInfo In AllPersonalizeRows
                If IsNothing(row) Then
                    'do nothing
                    Beep()

                Else
                    'sum different statuses to display
                    If row.IsCurrent Then
                        If Not ActiveCounts.ContainsKey(row.PrintStatus) Then
                            ActiveCounts.Add(row.PrintStatus, 0)
                        End If
                        ActiveCounts(row.PrintStatus) += 1
                    Else
                        If Not ArchiveCounts.ContainsKey(row.PrintStatus) Then
                            ArchiveCounts.Add(row.PrintStatus, 0)
                        End If
                        ArchiveCounts(row.PrintStatus) += 1
                    End If

                    'sum not started items
                    If row.PrintStatus.ToLower = "order" Or row.PrintStatus.ToLower = "paged up" Then
                        intNewIds += 1
                    End If
                End If

            Next

            'updates the tab display
            Select Case intNewIds
                Case 0
                    PersonalizeTabText = "Personalize"
                Case 1
                    PersonalizeTabText = "Personalize              (" & intNewIds.ToString & " NEW ORDER)              "
                Case Else
                    PersonalizeTabText = "Personalize              (" & intNewIds.ToString & " NEW ORDERS)              "
            End Select

            success = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "UpdateCounts Error")
        End Try
        Return success
    End Function


    Public Function FilterRows(ByVal searchIn As SearchCategory, ByVal searchTerm As String) As Boolean
        Dim success As Boolean = False
        Try

            DisplayedPersonalizeRows = New ComponentModel.BindingList(Of PersonalizeRowInfo)
            For Each row As PersonalizeRowInfo In AllPersonalizeRows
                Dim ProcessRow As Boolean = False
                If searchIn = SearchCategory.All Then
                    ProcessRow = True
                ElseIf searchIn = SearchCategory.Archive Then
                    If Not row.IsCurrent Then
                        ProcessRow = True
                    End If
                ElseIf searchIn = SearchCategory.Current Then
                    If row.IsCurrent Then
                        ProcessRow = True
                    End If
                End If

                If ProcessRow Then
                    If searchTerm = "" Or searchTerm = "*" Or searchTerm = cSearch Then
                        DisplayedPersonalizeRows.Add(row)
                    ElseIf row.ToString(False).Contains(searchTerm) Then
                        DisplayedPersonalizeRows.Add(row)
                    End If
                End If
            Next
            success = True
        Catch ex As Exception
            success = False
        End Try
        Return success
    End Function

#End Region


End Class

