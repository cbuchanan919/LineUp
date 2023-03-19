Imports System.Data.SqlClient

''' <summary>
''' Manages a list of barcodes
''' </summary>
Public Class JQBarcodeIO


#Region "Properties"
    Private Property SqlConnStr As String = ""
    ''' <summary>
    ''' a dictionary of barcodes. key is the BarcodeValue
    ''' </summary>
    ''' <returns></returns>
    Public Property barcodes As New Dictionary(Of String, JQBarcodeInfo)


    ''' <summary>
    ''' returns a data table format of the barcodes dict
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property barcodesTable As DataTable
        Get
            Dim dt As New DataTable
            dt.TableName = "Barcodes"
            dt.Columns.Add(New DataColumn("BarcodeText"))
            dt.Columns.Add(New DataColumn("BarcodeValue"))
            For Each bCode As JQBarcodeInfo In barcodes.Values
                Dim r As DataRow = dt.NewRow
                r(0) = bCode.BarcodeText
                r(1) = bCode.BarcodeValue
                dt.Rows.Add(r)
            Next
            Return dt
        End Get
    End Property

#End Region


#Region "Init"
    Public Sub New(ByVal SqlConnStr As String)
        Me.SqlConnStr = SqlConnStr
    End Sub

#End Region


#Region "Methods"
    ''' <summary>
    ''' gets the matching barcode from the barcodes dict. if no match, returns nothing
    ''' </summary>
    ''' <param name="BarcodeValue"></param>
    ''' <returns></returns>
    Public Function GetBarcode(ByVal BarcodeValue As String) As JQBarcodeInfo
        If barcodes.ContainsKey(BarcodeValue) Then
            Return barcodes(BarcodeValue)
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>
    ''' loads the barcodes dict from sql.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBarcodesFromSql() As Boolean
        Dim success As Boolean = False
        barcodes.Clear()

        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    cmd.CommandText = "SELECT BarcodeText, BarcodeValue from QPBarcodes"
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader
                        Do While reader.Read
                            'reads through each row of the sql query (think table layout).
                            Dim bcode As New JQBarcodeInfo()
                            If Not IsDBNull(reader(0)) Then bcode.BarcodeText = reader.GetString(0)
                            If Not IsDBNull(reader(1)) Then bcode.BarcodeValue = reader.GetString(1)
                            If bcode.BarcodeValue <> "" Then
                                If Not barcodes.ContainsKey(bcode.BarcodeValue) Then
                                    barcodes.Add(bcode.BarcodeValue, Nothing) 'adds the key
                                End If
                                barcodes(bcode.BarcodeValue) = bcode 'adds the value

                            End If
                        Loop


                    End Using
                End Using
            End Using


            success = True
        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Get Barcodes From SQL")
        End Try
        Return success
    End Function

    ''' <summary>
    ''' deletes the specified barcode from sql. Should run Load
    ''' </summary>
    ''' <param name="barcodeToDelete"></param>
    ''' <returns></returns>
    Public Function DeleteBarcodeFromSql(ByVal barcodeToDelete As JQBarcodeInfo) As Boolean
        Dim success As Boolean = False
        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    cmd.CommandText = "DELETE FROM QPBarcodes WHERE BarcodeValue = @BarcodeValue"
                    cmd.Parameters.Add("@BarcodeValue", SqlDbType.VarChar).Value = barcodeToDelete.BarcodeValue
                    conn.Open()
                    If cmd.ExecuteNonQuery = 1 Then
                        success = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Delete Barcode")
        End Try
        Return success
    End Function


    Public Function UpdateInsertBarcodeToSql(ByVal barcodeToUpdate As JQBarcodeInfo) As Boolean
        Dim success As Boolean = False
        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    cmd.CommandText = "UPDATE QPBarcodes SET BarcodeText = @BarcodeText WHERE BarcodeValue = @BarcodeValue"
                    cmd.Parameters.Add("@BarcodeText", SqlDbType.VarChar).Value = barcodeToUpdate.BarcodeText
                    cmd.Parameters.Add("@BarcodeValue", SqlDbType.VarChar).Value = barcodeToUpdate.BarcodeValue
                    conn.Open()
                    Select Case cmd.ExecuteNonQuery
                        Case 0
                            'nothing was updated. insert now.
                            cmd.CommandText = "INSERT INTO QPBarcodes (BarcodeText, BarcodeValue) VALUES (@BarcodeText, @BarcodeValue)"
                            If cmd.ExecuteNonQuery = 1 Then
                                success = True
                            End If
                        Case 1
                            success = True
                        Case Else
                            ' failed in some way.
                    End Select

                End Using
            End Using
        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Update / Insert Barcode to SQL")
        End Try
        Return success
    End Function

    Public Sub BarcodeGenerator()
        Try
            'creates a range of barcodes
            Dim title As String = "Barcode Image Generator"

            Dim beginningNum As String = InputBox("Please enter the beginning product number", title)
            Dim endingNum As String = ""
            If beginningNum = "" Then
                Beep()
            ElseIf IsNumeric(beginningNum) Then
                endingNum = InputBox("Please enter the end product number", title)
            Else
                MsgBox("Please enter a valid number")
            End If
            If endingNum = "" Then
                Beep()
            ElseIf IsNumeric(endingNum) Then
                Dim tmpBarcodes As New JQBarcodeIO("")
                For i As Integer = beginningNum To endingNum
                    Dim b As New JQBarcodeInfo
                    b.BarcodeText = i
                    b.BarcodeValue = i
                    tmpBarcodes.barcodes.Add(i, b)
                Next
                If MsgBox("Are you sure you want to create " & tmpBarcodes.barcodes.Count & " barcodes?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    For Each b As JQBarcodeInfo In tmpBarcodes.barcodes.Values
                        b.CreateBarcode(,, True)
                    Next

                    MsgBox("All " & tmpBarcodes.barcodes.Count & " images created. They will be found in: " & vbCrLf & My.Settings.dirMxOrders & vbCrLf & vbCrLf & "They will be deleted when the program closes, so please move them to your desired location!")
                End If
            Else
                MsgBox("Please enter a valid number")
            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function CreateBarcodeValue() As String
        Dim ReturnBarcode As String = ""
        Dim AllowAlphabet As String = "abcdefghijklmnopqrstuvwxyz"
        Dim AllowNumbers As String = "1234567890"
        Dim rdm As New Random()
        Dim meContinue As Boolean = False
        Do Until meContinue = True
            'loops until it makes sure that it's a unique code
            ReturnBarcode = ""
            For i As Integer = 0 To 1
                ReturnBarcode &= AllowAlphabet(rdm.Next(0, AllowAlphabet.Count - 1))
                ReturnBarcode &= AllowNumbers(rdm.Next(0, AllowNumbers.Count - 1))
            Next
            meContinue = True

            For Each bCode As JQBarcodeInfo In barcodes.Values
                If bCode.BarcodeValue = ReturnBarcode Then
                    meContinue = False
                End If
            Next
        Loop

        Return ReturnBarcode
    End Function
#End Region


End Class