Imports System.Windows
Imports System.Windows.Forms
Imports System.Drawing


''' <summary>
''' Configures the gui buttons and controls to have a similar size and color
''' </summary>
Public Class FormatControls

#Region "Properties"

    Public Property TabBackColor As Color = Color.FromArgb(255, 230, 245, 255)
    Public Property BtnForeColor As Color = Color.White
    Public Property BtnBackColor As Color = Color.DodgerBlue
    Public Property BtnMouseOverBackColor As Color = Color.DarkOrange
    Public Property BtnMouseDownBackColor As Color = Color.Black

    Public Property BtnFont As Font = New Font("Segoe UI Semibold", 9.75)

    Public Property ChangeTabColor As Boolean = False
#End Region

#Region "Init"



    ''' <summary>
    ''' you can send nothing object for Colors/Font if you set "UseDefaults" to true
    ''' </summary>
    ''' <param name="aTabBackColor">Default: Color.FromArgb(255, 230, 245, 255)</param>
    ''' <param name="aBtnForeColor">Default: Color.White</param>
    ''' <param name="aBtnBackColor">Default: Color.DodgerBlue</param>
    ''' <param name="aBtnMouseOverBackColor">Default: Color.DarkOrange</param>  
    ''' <param name="aBtnFont">Default: "Segoe UI Semibold", 9.75</param>
    ''' <param name="UseDefaults">If true, will use the defaults, otherwise it will use the parameters you send</param>
    ''' <param name="willChangeTabColor"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal UseDefaults As Boolean,
                   ByVal willChangeTabColor As Boolean,
                   ByVal ChangeColorsForHolidays As Boolean,
                   ByVal aTabBackColor As Color,
                   ByVal aBtnForeColor As Color,
                   ByVal aBtnBackColor As Color,
                   ByVal aBtnMouseOverBackColor As Color,
                   ByVal abtnMouseDownBackColor As Color,
                   ByVal aBtnFont As Font)

        If UseDefaults = False Then
            TabBackColor = aTabBackColor
            BtnForeColor = aBtnForeColor
            BtnBackColor = aBtnBackColor
            BtnMouseOverBackColor = aBtnMouseOverBackColor
            BtnMouseDownBackColor = abtnMouseDownBackColor
            BtnFont = aBtnFont
        End If
        ChangeTabColor = willChangeTabColor

        If ChangeColorsForHolidays Then
            ConfigureButtonProperties()
        End If

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="useDefaults">Not currently setting anything...</param>
    ''' <param name="willChangeTabColor"></param>
    ''' <param name="ChangeColorsForHolidays"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal useDefaults As Boolean, willChangeTabColor As Boolean, ByVal ChangeColorsForHolidays As Boolean)
        ChangeTabColor = willChangeTabColor

        If ChangeColorsForHolidays Then
            ConfigureButtonProperties()
        End If
    End Sub

#End Region


#Region "Methods"


    ''' <summary>
    ''' Formats the controls and subcontrols so that they all match
    ''' </summary>
    ''' <param name="myControl">The control that contains the subcontrols you wish to format</param>
    ''' <remarks></remarks>
    Public Sub Format_Controls(ByVal myControl As Control)
        Try

            For Each ctrl As Control In myControl.Controls ' CtrlList
                If ctrl.GetType Is GetType(Form) Then
                    ctrl.BackColor = TabBackColor
                End If
                If ctrl.GetType Is GetType(TabPage) Then
                    'separated from the other if/then statements because it's a tab page and contains other controls
                    If ChangeTabColor Then
                        ctrl.BackColor = TabBackColor
                    End If
                End If

                If ctrl.GetType Is GetType(Button) Then
                    Dim ctrlBtn As Button = CType(ctrl, Button)
                    'MsgBox(ctrlBtn.Text)
                    With ctrlBtn 'ctrl
                        .ForeColor = BtnForeColor
                        .BackColor = BtnBackColor
                        .FlatStyle = FlatStyle.Flat
                        .FlatAppearance.BorderColor = Color.White
                        .FlatAppearance.BorderSize = 0
                        .FlatAppearance.MouseOverBackColor = BtnMouseOverBackColor
                        .FlatAppearance.MouseDownBackColor = BtnMouseDownBackColor
                        .Font = BtnFont
                        .Height = 28
                    End With
                ElseIf ctrl.GetType Is GetType(RadioButton) Then
                    Dim ctrlBtn As RadioButton = CType(ctrl, RadioButton)
                    ctrlBtn.FlatStyle = FlatStyle.Flat

                ElseIf ctrl.GetType Is GetType(DataGridView) Then
                    Dim ctrlBtn As DataGridView = CType(ctrl, DataGridView)
                    ctrlBtn.AlternatingRowsDefaultCellStyle.BackColor = TabBackColor

                ElseIf ctrl.Controls.Count > 0 Then

                    Format_Controls(ctrl)

                End If
                'End If

            Next
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub ConfigureButtonProperties()
        Try
            Dim meMonday As Date = GenUtil.GetMonday()

            Dim Christmas As Date = GenUtil.GetMonday(CDate("12/25/" & Date.Now.Year))
            Dim Halloween As Date = GenUtil.GetMonday(CDate("10/31/" & Date.Now.Year))
            Dim NewYear As Date = GenUtil.GetMonday(CDate("1/1/" & Date.Now.Year))

            'forecolor = text
            'backcolor = background

            'meMonday = Christmas
            'meMonday = Halloween
            'meMonday = NewYear

            If meMonday = Christmas Then
                BtnForeColor = Color.White 'Color.FromArgb(255, 206, 0, 0)
                BtnBackColor = Color.FromArgb(255, 180, 84, 91)
                BtnMouseOverBackColor = Color.FromArgb(255, 82, 153, 71)

            ElseIf meMonday = Halloween Then
                BtnForeColor = Color.Black 'White 'Color.FromArgb(15, 142, 160)
                BtnBackColor = Color.FromArgb(128, 115, 21, 174)
                BtnMouseOverBackColor = Color.FromArgb(125, 255, 181, 16)
                TabBackColor = Color.FromArgb(255, 255, 249, 240)

            ElseIf meMonday = NewYear Then
                BtnForeColor = Color.Black ' Color.FromArgb(255, 15, 142, 160) 
                BtnBackColor = Color.FromArgb(255, 226, 20)
                BtnMouseOverBackColor = Color.FromArgb(201, 16, 132)
                TabBackColor = Color.FromArgb(255, 244, 240, 255)

            End If
        Catch ex As Exception

        End Try


    End Sub

#End Region


End Class
