Imports System.Windows.Forms
Imports System.IO
Imports System.Xml
Public Class frmBarcodes
#Region "Properties"
    Private Property bCodesIO As JQBarcodeIO
    Private Property BarcodeBindingsource As New BindingSource
    Private Property BarcodeStatusPath As String = ""
    Private Property currentBarcode As JQBarcodeInfo


#End Region

#Region "Init"
    Public Sub New(ByRef bCodes As JQBarcodeIO)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.bCodesIO = bCodes

    End Sub

#End Region

#Region "Methods"

#End Region


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmStatuses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LineUp.MybtnFmt.Format_Controls(Me)
        PopulateBarcodeStatuses()
    End Sub
    ''' <summary>
    ''' populates the barcodes table
    ''' </summary>
    ''' <returns></returns>
    Public Function PopulateBarcodeStatuses() As Boolean
        Dim success As Boolean = True
        Try

            bCodesIO.GetBarcodesFromSql()
            BarcodeBindingsource.DataSource = bCodesIO.barcodesTable
            dgvBarcode.DataSource = BarcodeBindingsource

            BarcodeColumns("BarcodeText", "Barcode Text", 200, True)
            BarcodeColumns("BarcodeValue", "Barcode Value", 200, True)


        Catch ex As Exception
            success = False
        End Try

        Return success
    End Function

    ''' <summary>
    ''' configures column width for the dgv table
    ''' </summary>
    ''' <param name="ColumnName"></param>
    ''' <param name="headerText"></param>
    ''' <param name="ColWidth"></param>
    ''' <param name="ColVisible"></param>
    Private Sub BarcodeColumns(ByVal ColumnName As String, ByVal headerText As String, ByVal ColWidth As Integer, ByVal ColVisible As Boolean)
        'sub to set the settings on the columns
        'keep with XML_Reader_Orders

        With dgvBarcode.Columns(ColumnName)
            .HeaderText = headerText
            .Width = ColWidth
            .Visible = ColVisible
            .SortMode = DataGridViewColumnSortMode.Programmatic
        End With



    End Sub



    Private Sub dgvBarcode_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBarcode.CellEndEdit
        Dim doUpdate As Boolean = False
        If currentBarcode.BarcodeValue = "" Then
            currentBarcode.BarcodeValue = bCodesIO.CreateBarcodeValue()
            doUpdate = True
        End If
        If currentBarcode.BarcodeText <> dgvBarcode.CurrentCell.Value.ToString Then
            currentBarcode.BarcodeText = dgvBarcode.CurrentCell.Value.ToString
            doUpdate = True
        End If
        If doUpdate Then
            bCodesIO.UpdateInsertBarcodeToSql(currentBarcode)
            PopulateBarcodeStatuses()
        End If

    End Sub

    Private Sub dgvBarcode_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvBarcode.CellBeginEdit
        If e.ColumnIndex = 1 Then
            e.Cancel = True
        ElseIf Not IsNothing(dgvBarcode.Rows(e.RowIndex).Cells(1).Value) Then
            currentBarcode = bCodesIO.GetBarcode(dgvBarcode.Rows(e.RowIndex).Cells(1).Value.ToString)
        End If

        If IsNothing(currentBarcode) Then
            currentBarcode = New JQBarcodeInfo
        End If
    End Sub

    'Private Sub dgvBarcode_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles dgvBarcode.UserAddedRow
    '    WriteBarcodeFile(BarcodeStatusPath)
    'End Sub
    Private Sub dgvBarcode_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles dgvBarcode.UserDeletingRow
        If Not IsNothing(dgvBarcode.CurrentRow) Then
            ' a legit row was found
            currentBarcode = bCodesIO.GetBarcode(dgvBarcode.Rows(e.Row.Index).Cells(1).Value.ToString)
            If Not IsNothing(currentBarcode) Then
                'a barcode was found. deleting it now
                bCodesIO.DeleteBarcodeFromSql(currentBarcode)
            End If

        End If
    End Sub

    Private Sub dgvBarcode_UserDeletedRow(sender As Object, e As DataGridViewRowEventArgs) Handles dgvBarcode.UserDeletedRow
        PopulateBarcodeStatuses()
    End Sub

    Private Sub btnToSQL_Click(sender As Object, e As EventArgs) Handles btnToSQL.Click
        For Each myRow As DataGridViewRow In dgvBarcode.Rows
            If myRow.Index < dgvBarcode.Rows.Count Then
                Dim bc As New JQBarcodeInfo()
                If Not IsNothing(myRow.Cells(0).Value) AndAlso Not IsNothing(myRow.Cells(1).Value) Then
                    bc.BarcodeText = myRow.Cells(0).Value.ToString
                    bc.BarcodeValue = myRow.Cells(1).Value.ToString
                    bCodesIO.UpdateInsertBarcodeToSql(bc)

                End If

            End If
        Next
        PopulateBarcodeStatuses()


    End Sub

End Class
