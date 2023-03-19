''' <summary>
''' Contains shared methods to manage printer gui
''' </summary>
Public Class PrinterGui

#Region "Properties"
    Public Shared Property flowPanel As FlowLayoutPanel
#End Region

#Region "Methods"


    ''' <summary>
    ''' creates a table layout panel for arranging the details of the printer and printerqueues.
    ''' </summary>
    ''' <param name="labelText"></param>
    ''' <param name="ctrl"></param>
    ''' <param name="LabelBText"></param>
    ''' <param name="ctrlB"></param>
    ''' <returns></returns>
    Public Shared Function CreateTable(Optional ByVal labelText As String = "", Optional ByVal ctrl As Control = Nothing, Optional ByVal LabelBText As String = "", Optional ByVal ctrlB As Control = Nothing, Optional ByVal height As Integer = 28) As TableLayoutPanel

        Dim tb As New TableLayoutPanel
        tb.Size = New Drawing.Size(324, height)
        tb.ColumnCount = 2
        tb.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100))
        tb.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

        If LabelBText <> "" Or Not IsNothing(ctrlB) Then
            'secondary label, 4 columns
            tb.ColumnCount = 4
            tb.ColumnStyles(1).Width = 70
            tb.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100))
            tb.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        End If



        If labelText <> "" Then
            Dim l As New Label
            l.Text = labelText
            l.Anchor = AnchorStyles.Right
            tb.Controls.Add(l, 0, 0)
        End If

        If Not IsNothing(ctrl) Then
            If TypeOf ctrl Is TextBox Then ctrl.Dock = DockStyle.Fill
            tb.Controls.Add(ctrl, 1, 0)
        End If


        'for four cols
        If LabelBText <> "" Then
            'adds second label to row
            Dim l As New Label
            l.Text = LabelBText
            l.Anchor = AnchorStyles.Right
            tb.Controls.Add(l, 2, 0)
        End If

        If Not IsNothing(ctrlB) Then
            'adds second control to row
            If TypeOf ctrlB Is TextBox Then ctrl.Dock = DockStyle.Fill
            tb.Controls.Add(ctrlB, 3, 0)
        End If

        Return tb
    End Function




    ''' <summary>
    ''' Creates a new generic text box
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CreateTxtBox() As TextBox
        Dim tb As New TextBox
        tb.Dock = DockStyle.Fill
        Return tb
    End Function

    ''' <summary>
    ''' creates a new button to 'standard' specs
    ''' </summary>
    ''' <param name="btnText"></param>
    ''' <returns></returns>
    Public Shared Function CreateBtn(ByVal btnText As String) As Button
        Dim bt As New Button
        bt.Width = 100
        bt.Height = 27
        bt.Dock = DockStyle.Fill
        bt.Text = btnText
        Return bt
    End Function
    ''' <summary>
    ''' creates a new blank checkbox
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CreateChkBox() As CheckBox
        Dim chk As New CheckBox
        Return chk
    End Function

    Public Shared Function CreateCboBox(ByVal ItemList As List(Of String)) As ComboBox
        Dim cbo As New ComboBox
        cbo.DropDownStyle = ComboBoxStyle.DropDownList
        For Each item As String In ItemList
            cbo.Items.Add(item.Replace("_", " "))
        Next

        cbo.Dock = DockStyle.Fill
        Return cbo
    End Function


    Public Shared Sub FlowPanelAddControl(ByVal ctrl As Control)
        If Not IsNothing(flowPanel) Then
            flowPanel.Controls.Add(ctrl)
        End If
    End Sub
    Public Shared Sub FlowPanelRemoveControl(ByVal ctrl As Control)
        If Not IsNothing(flowPanel) Then
            flowPanel.Controls.Remove(ctrl)
        End If
    End Sub

#End Region

End Class