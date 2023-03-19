
Public Class frmProdSearch
    Dim XMLSetBindingSource As New BindingSource
    Private prodUVinfo As UvProductInfoIO
    Dim loaded As Boolean = False

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        prodUVinfo = LineUp.MyUvProductInfoIO.clone

    End Sub
    Private Sub frmProdSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LineUp.MybtnFmt.Format_Controls(Me)
        XMLSetBindingSource.DataSource = prodUVinfo.prodInfoDS.Tables(0)
        dgvSearch.DataSource = XMLSetBindingSource
        FormatSearchColumns()
        loaded = True
        UpdateSearchFilter(txtSearch.Text)

    End Sub

    Private Sub FormatSearchColumns()
        If dgvSearch.ColumnCount > 5 Then
            With dgvSearch.Columns(Col_ProductID)
                .HeaderText = "Prod." & vbCrLf & "Num."
                .Width = 50
            End With
            With dgvSearch.Columns(Col_Title)
                .HeaderText = "Title"
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 250
            End With
            With dgvSearch.Columns(Col_TitleInverted)
                .HeaderText = "Inverted Title"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                '.Width = 100
            End With
            With dgvSearch.Columns(Col_Price)
                .HeaderText = "Price"
                .Width = 50
            End With
            With dgvSearch.Columns(Col_Pages)
                .HeaderText = "Pg." & vbCrLf & "Ct."
                .Width = 50
            End With
            With dgvSearch.Columns(Col_Author)
                .HeaderText = "Author"
                .Width = 150
            End With
            With dgvSearch.Columns(6)
                .Width = 50

            End With
            With dgvSearch.Columns(7)
                .Width = 50
            End With
            With dgvSearch.Columns(8)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                '.Width = 150
            End With
            With dgvSearch.Columns(9)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                '.Width = 150
            End With
            With dgvSearch.Columns(Col_Type)
                .HeaderText = "Type"
                .Width = 50
            End With
            With dgvSearch.Columns(Col_Language)
                .HeaderText = "Lang."
                .Width = 50
            End With
            With dgvSearch.Columns(Col_SubType)
                .HeaderText = "Sub" & vbCrLf & "Type"
                .Width = 50
            End With
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
        UpdateSearchFilter(txtSearch.Text)
    End Sub

    Private Sub UpdateSearchFilter(ByVal mySearch As String)
        'updates the filter for the personalizedJobQ Datagridview
        If loaded Then
            'if the xmlFile has been read...
            If prodUVinfo.ProductsWereRead = True Then

                'filters through the design ID's
                Try
                    Dim strStatus As String = ""
                    If mySearch = cSearch Or mySearch = "" Then
                        If rdbInHouse.Checked Then
                            strStatus &= Col_Source & " = '' OR " & Col_Source & " IS NULL"
                        Else
                            strStatus = ""
                        End If
                    ElseIf mySearch.Length > 0 Then
                        'filters the Personalized  based on these columns

                        strStatus = "(" & Col_ProductID & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_Title & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_TitleInverted & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_Price & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_Pages & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_Author & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_Medium & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_Source & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_WebText & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_CatalogText & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_Type & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_Language & " LIKE '%" & mySearch & "%' " &
                                "OR " & Col_SubType & " LIKE '%" & mySearch & "%')"

                        If rdbInHouse.Checked Then
                            strStatus &= " AND (" & Col_Source & " = '' OR " & Col_Source & " IS NULL) "
                        End If
                        '" & _
                        '            "AND DateTime LIKE '%" & strStatus & "'"

                    Else
                        strStatus = ""
                    End If
                    XMLSetBindingSource.Filter = String.Format(strStatus)
                    Label1.Text = CInt(dgvSearch.RowCount - 1) & " Items found"

                Catch ex As Exception
                    LineUp.Log.addError(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod.ToString, True)
                End Try
            End If
        End If

    End Sub

    Private Sub btnAddJob_Click(sender As Object, e As EventArgs) Handles btnAddJob.Click
        Dim msg As String = ""
        If dgvSearch.SelectedRows.Count = 0 Then
            msg = "Please select a job to add to the JobQ"
        ElseIf dgvSearch.SelectedRows.Count > 10 Then
            msg = "Please limit your selection to 10 jobs max"
        End If
        If msg <> "" Then
            MsgBox(msg)
        Else
            msg = "Are you sure you want to add the following rows to the JobQ?" & vbCrLf & vbCrLf
            For Each myRow As DataGridViewRow In dgvSearch.SelectedRows
                Dim CellsToAdd() As Integer = {0, 1, 5}
                For Each aCell In CellsToAdd
                    Try
                        msg &= myRow.Cells(aCell).Value & " - "
                    Catch ex As Exception
                    End Try
                Next
                msg = msg.Substring(0, msg.Length - 3)
                msg &= vbCrLf
            Next
            If MsgBox(msg, MsgBoxStyle.YesNoCancel, "Add Jobs to JobQ?") = MsgBoxResult.Yes Then



                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Hide()
            End If
        End If
    End Sub



    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub rdbAllJobs_CheckedChanged(sender As Object, e As EventArgs) Handles rdbAllJobs.CheckedChanged
        If rdbAllJobs.Checked Then
            UpdateSearchFilter(txtSearch.Text)
        End If
    End Sub

    Private Sub rdbInHouse_CheckedChanged(sender As Object, e As EventArgs) Handles rdbInHouse.CheckedChanged
        If rdbInHouse.Checked Then
            UpdateSearchFilter(txtSearch.Text)
        End If
    End Sub
End Class