Imports System.IO

Public Class frmCustSearch

    Private title As String = "Customer Search"
    Private aBindingSource As BindingSource
    Private customerData As Personalize_custDataTable = Nothing

    Public Sub New(ByVal custDataInfo As Personalize_custDataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        customerData = custDataInfo
    End Sub
    Private Sub frmCustSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = title
            LineUp.MybtnFmt.Format_Controls(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub frmCustSearch_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        dgvCustSearch.Visible = False
        lblStatus.Parent = SplitContainer1.Panel1
        Do Until customerData.loaded
            Threading.Thread.Sleep(200)
            Application.DoEvents()
            Me.Text = title & " - " & customerData.currentProgress & "% Loaded"
            lblStatus.Text = customerData.currentProgress & "% Loaded"
        Loop
        lblStatus.Visible = False
        dgvCustSearch.Visible = True
        Me.Text = title
        PopulateTable()

        ColNames()
    End Sub

    Private Sub btnCreateDT_Click(sender As Object, e As EventArgs) Handles btnCreateDT.Click
        PopulateTable(True)
    End Sub
    Private Sub PopulateTable(Optional ByRef CreateAllNewDT As Boolean = False)
        Try
            'Dim TablePath As String = Path.Combine(LocalDir, "CustData.xml")
            'DS = Nothing
            'DS = New DataSet

            'If File.Exists(TablePath) Then
            '    If CreateAllNewDT = False Then
            '        Dim TempDS As New DataSet
            '        TempDS.ReadXml(TablePath)

            '        DS.Tables.Add(LineUp.CreateCustomerDataTable(TempDS.Tables(0)))
            '    Else
            '        DS.Tables.Add(LineUp.CreateCustomerDataTable)
            '    End If

            'Else
            '    DS.Tables.Add(LineUp.CreateCustomerDataTable)
            'End If



            'Dim settings As New System.Xml.XmlWriterSettings
            'settings.OmitXmlDeclaration = False
            'settings.WriteEndDocumentOnClose = True
            'settings.Indent = True
            'If File.Exists(TablePath) Then
            '    File.Delete(TablePath)
            'End If
            'DS.Tables(0).WriteXml(TablePath)
            'MsgBox(TablePath)

            aBindingSource = New BindingSource
            aBindingSource.DataSource = customerData.custDT
            dgvCustSearch.DataSource = aBindingSource
        Catch ex As Exception
            MsgBox("PopulateTable Error - " & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub ColNames()
        If dgvCustSearch.ColumnCount > 0 Then
            dgvCustSearch.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
            For Each myCol As DataGridViewColumn In dgvCustSearch.Columns
                Try
                    myCol.DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    Select Case myCol.Name
                        Case "Email"
                            myCol.HeaderText = "Email" & vbCrLf & "Address"
                            myCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                        Case "Phone"
                            myCol.HeaderText = "Phone" & vbCrLf & "Number"
                            myCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                            myCol.Width = 95

                        Case "ShipAddress"
                            myCol.HeaderText = "Shipping" & vbCrLf & "Address"
                            myCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                        Case "BillAddress"
                            myCol.HeaderText = "Billing" & vbCrLf & "Address"
                            myCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                        Case "ShipNote"
                            myCol.HeaderText = "Shipping" & vbCrLf & "Note"
                            myCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                        Case "ReceiptNumber"
                            myCol.HeaderText = "Receipt" & vbCrLf & "Number"
                            myCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                            myCol.Width = 60

                        Case "BillName"
                            myCol.HeaderText = "Billing" & vbCrLf & "Customer"
                            myCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                        Case "MxNumber"
                            myCol.HeaderText = "Invoice" & vbCrLf & "Number"
                            myCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                            myCol.Width = 60

                        Case "DesignIDs"
                            myCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            myCol.HeaderText = "Design ID's"

                    End Select
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next
        End If
    End Sub

    Private Sub txtSearch_GotFocus(sender As Object, e As EventArgs) Handles txtSearch.GotFocus
        If txtSearch.Text = cSearch Then
            txtSearch.Text = ""
        End If
    End Sub

    Private Sub txtSearch_LostFocus(sender As Object, e As EventArgs) Handles txtSearch.LostFocus
        If txtSearch.Text = "" Then
            txtSearch.Text = cSearch
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        SearchFilter(txtSearch, aBindingSource)
    End Sub
    Private Sub SearchFilter(ByVal TextBox As TextBox, ByVal BindingSource As BindingSource)

        Try
            If TextBox.Text = cSearch Then
                BindingSource.Filter = String.Empty
            ElseIf TextBox.Text.Length > 0 Then
                'filters the binding source (either jobq or jobqHistory) based on these columns
                BindingSource.Filter = String.Format("Email LIKE '" & "%" & TextBox.Text & "*' " &
                                                     "OR Phone LIKE '" & "%" & TextBox.Text & "*' " &
                                                     "OR ShipAddress LIKE '" & "%" & TextBox.Text & "*' " &
                                                     "OR BillAddress LIKE '" & "%" & TextBox.Text & "*' " &
                                                     "OR ShipNote LIKE '" & "%" & TextBox.Text & "*' " &
                                                     "OR ReceiptNumber LIKE '" & "%" & TextBox.Text & "*' " &
                                                     "OR BillName LIKE '" & "%" & TextBox.Text & "*' " &
                                                     "OR MxNumber LIKE '" & "%" & TextBox.Text & "*' " &
                                                     "OR DesignIDs LIKE '" & "%" & TextBox.Text) & "%'"

            Else
                BindingSource.Filter = String.Empty
            End If
        Catch ex As Exception
            LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
        End Try
    End Sub

End Class