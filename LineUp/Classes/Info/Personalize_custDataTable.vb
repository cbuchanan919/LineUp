Imports System.IO


''' <summary>
''' Creates the customer data table. Specific to personalized orders Combines info from different places to be searchable.
''' </summary>
Public Class Personalize_custDataTable


#Region "Properties & Variables"


    Public Const custData As String = "CustData.xml"

    Public Property currentProgress() As Integer = 0

    ''' <summary>
    ''' boolean for whether or not the table has finished loading.
    ''' </summary>
    ''' <returns></returns>
    Public Property loaded() As String = False

    ''' <summary>
    ''' stores the last modified datatable, as read from xml file
    ''' </summary>
    ''' <returns></returns>
    Public Property originalDT() As DataTable = Nothing


    '''' <summary>
    '''' copy of the personalize designs set. Original version is connected with the personalize Job Q.
    '''' </summary>
    '''' <returns></returns>
    'Private Property designsDS() As New DataSet
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Property PersonalizeRows As List(Of PersonalizeRowInfo) = Nothing


    ''' <summary>
    ''' a list of errors that have happened...
    ''' </summary>
    ''' <returns></returns>
    Public Property errors() As String = ""


    Private _custDT As DataTable = Nothing
    ''' <summary>
    ''' current table of customer info
    ''' </summary>
    ''' <returns></returns>
    Public Property custDT() As DataTable
        Get
            If IsNothing(_custDT) Then
                CreateCustomerDataTable()
            End If
            Return _custDT
        End Get
        Set(value As DataTable)
            _custDT = value
        End Set
    End Property

    ''' <summary>
    ''' resource directory
    ''' </summary>
    ''' <returns></returns>
    Public Property resourceDir() As String = ""


    Private Property frmLineUp As LineUp = Nothing



#End Region


#Region "Init"


    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub New(ByVal PersonalizeRows As List(Of PersonalizeRowInfo), ByVal resourceDirectory As String, ByVal mainForm As LineUp, Optional ByVal createTableFromScratch As Boolean = False)
        Me.PersonalizeRows = PersonalizeRows
        resourceDir = resourceDirectory
        frmLineUp = mainForm
        '  designsDS = designDataSet.Copy

        If Not createTableFromScratch Then
            originalDT = readXML()
        End If

    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Shows the current status in a msgbox. 
    ''' </summary>
    Public Sub msgBoxCurrentPercent()
        MsgBox("Sorry, please try again in 1-2 minutes or sooner!" & vbCrLf & vbCrLf &
                   "  -  " & currentProgress & "% DONE  -  " & vbCrLf & vbCrLf & vbCrLf &
                   "(click OK to continue processing)", MsgBoxStyle.OkOnly)
    End Sub

    ''' <summary>
    ''' handles all the steps in the process if being run as a background thread.
    ''' </summary>
    Public Sub runAsThread()
        Try
            If Not IsNothing(PersonalizeRows) Then
                If IsNothing(originalDT) Then
                    originalDT = readXML()
                End If
                CreateCustomerDataTable()
                writeXML()
            End If
            frmLineUp.receiveCustDataTable(Me)
        Catch ex As Exception

        End Try


    End Sub



    ''' <summary>
    ''' Creates a table that contains all the customer info for the personalized tab dgv
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub CreateCustomerDataTable()

        Try
            _custDT = New DataTable
            Dim MxCheckList As New List(Of Integer) 'if OriginalDT is not nothing and contains a matching mx number, it adds it to MxCheckList.

            With _custDT.Columns
                .Add("MxNumber")
                .Add("DesignIDs")
                .Add("Email")
                .Add("Phone")
                .Add("BillName")
                .Add("BillAddress")
                .Add("ReceiptNumber")
                .Add("ShipAddress")
                .Add("ShipNote")


            End With

            If IsNothing(PersonalizeRows) Then
                'don't run sub! (DesignsDS is the original dataset that stores the personalized info (dgv))
            ElseIf PersonalizeRows.Count = 0 Then
                'designs table ran into an error... 
            Else
                'Dim rowCt As Integer = PersonalizeRows.Count
                Dim curRow As Integer = 0

                'DesignsTable is the table that contains all the custom id's
                For Each row As PersonalizeRowInfo In PersonalizeRows

                    'goes through each row, and if another row has the same mx number, it adds it to the current entry.

                    Dim ProcessRow As Boolean = True
                    Dim IDs As New List(Of String) 'stores all the personalize id's for each mx number

                    'Dim ID As String = ""
                    'Dim MxNum As String = "" 'stores the mxOrderNumber
                    'Dim MxNumWithMx As String = "" 'stores the mxOrderNumber (including 'Mx')
                    'Try

                    '    MxNum = row.Item(0).ToString
                    '    MxNumWithMx = "Mx" & MxNum
                    'Catch ex As Exception
                    '    errors &= ex.Message & vbCrLf
                    'End Try

                    'Try
                    '    ID = row.Item(2).ToString
                    'Catch ex As Exception
                    '    errors &= ex.Message & vbCrLf
                    'End Try

                    If Not IsNothing(originalDT) Then
                        'the OriginalDT is a data table that is optionally passed in to reduce file reading time.

                        If MxCheckList.Contains(row.MxNumberNoMX) = False Then
                            For Each OriginalRow As DataRow In originalDT.Rows
                                Dim originalMx As String = OriginalRow.Item(0).ToString.ToLower
                                If row.MxNumberNoMX.ToString = originalMx Or row.MxNumber.ToLower = originalMx Then
                                    'the mxNumber exists in the data table, it will be added the way it is.
                                    MxCheckList.Add(row.MxNumberNoMX)
                                    ProcessRow = False
                                    Dim DTRow As DataRow = _custDT.NewRow
                                    DTRow.ItemArray = OriginalRow.ItemArray
                                    _custDT.Rows.Add(DTRow)
                                End If
                            Next
                        End If


                    End If


                    If ProcessRow Then
                        'goes through and adds all of the design id's to the current mx number
                        IDs.Add($"{row.ItemNumber} - {row.DesignId} - {row.PrintStatus}")
                        For Each aRow As PersonalizeRowInfo In PersonalizeRows
                            If row.MxNumberNoMX = aRow.MxNumberNoMX Then
                                If row.DesignId <> aRow.DesignId Then
                                    IDs.Add($"{row.ItemNumber} - {row.DesignId} - {row.PrintStatus}")
                                End If
                            End If
                        Next


                        'If ID <> "" Then
                        '    Try
                        '        ID = row.Item(1) & " - " & ID & " - " & row.Item(5) 'adds the product ID to the id
                        '    Catch ex As Exception
                        '        errors &= ex.Message & vbCrLf
                        '    End Try
                        '    IDs.Add(ID)
                        '    For Each aRow As DataRow In designsDS.Tables(0).Rows

                        '        If Not IsDBNull(aRow.Item(0)) Then
                        '            If MxNum = aRow.Item(0) Then
                        '                If row.Item(2).ToString <> aRow.Item(2).ToString Then
                        '                    IDs.Add(aRow.Item(1).ToString & " - " & aRow.Item(2).ToString & " - " & aRow.Item(5).ToString)
                        '                End If
                        '            End If
                        '        End If

                        '    Next
                        'End If

                        'ID = ""
                        'For Each DesignID As String In IDs
                        '    ID &= DesignID & vbCrLf
                        'Next
                        'ID = ID.Trim

                        ''ID = ID.Substring(0, ID.Length - 1)

                        If MxCheckList.Contains(row.MxNumberNoMX) = False Then
                            MxCheckList.Add(row.MxNumberNoMX)
                            Dim mxInfo As New PersonalizeMxInfo(row.MxNumberNoMX, False)
                            If mxInfo.MxNumber.Replace("Mx", "") <> "" Then
                                'it loaded successfully
                                Dim DTRow As DataRow = _custDT.NewRow
                                With DTRow
                                    .Item("Email") = mxInfo.Email
                                    .Item("Phone") = mxInfo.PhoneNum
                                    .Item("ShipAddress") = mxInfo.ShipAddress
                                    .Item("BillAddress") = mxInfo.BillAddress
                                    .Item("ShipNote") = mxInfo.ShipNote
                                    .Item("ReceiptNumber") = mxInfo.ReceiptNumber
                                    .Item("BillName") = mxInfo.BillName
                                    .Item("MxNumber") = mxInfo.MxNumberNoMx
                                    .Item("DesignIDs") = String.Join(vbCrLf, IDs)

                                End With
                                _custDT.Rows.Add(DTRow)
                            End If


                        End If
                    End If
                    curRow += 1
                    currentProgress = (Math.Round(curRow / PersonalizeRows.Count, 2) * 100)
                Next
                loaded = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try




    End Sub


    Private Sub writeXML()
        Try
            Dim tablePath As String = Path.Combine(resourceDir, custData)

            Dim settings As New System.Xml.XmlWriterSettings
            settings.OmitXmlDeclaration = False
            settings.WriteEndDocumentOnClose = True
            settings.Indent = True
            If File.Exists(tablePath) Then
                File.Delete(tablePath)
            End If
            Dim tempDS As New DataSet
            tempDS.Tables.Add(custDT)

            tempDS.WriteXml(tablePath)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "WriteXML")
        End Try

    End Sub

    Private Function readXML() As DataTable
        Dim ds As New DataSet
        Dim dt As DataTable = Nothing
        Try
            Dim tablePath As String = Path.Combine(resourceDir, custData)
            If File.Exists(tablePath) Then
                ds.ReadXml(tablePath)
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            End If


        Catch ex As Exception
            ds = Nothing
            MsgBox(ex.Message, MsgBoxStyle.Critical, "ReadXML")
        End Try

        Return dt
    End Function

#End Region


End Class
