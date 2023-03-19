Imports System.IO

Public Class RomanNumeralConverter
    Dim RomanNumPath As String = ""
    Private Roman_Number_Dict As Dictionary(Of String, Integer)
    Private Numeral_to_Roman_Dict As Dictionary(Of Integer, String)

    Const INVALID As String = "Invalid Format!"
    Dim _textStreamReader As StreamReader
    Dim _assembly As Reflection.Assembly


    Public Sub New()

        PopulateDictionary()

    End Sub

    ''' <summary>
    ''' Updates the Dictionary with the roman numeral to number equivalents
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateDictionary()
        Try
            Roman_Number_Dict = Nothing
            Roman_Number_Dict = New Dictionary(Of String, Integer)

            Numeral_to_Roman_Dict = Nothing
            Numeral_to_Roman_Dict = New Dictionary(Of Integer, String)

            Dim line As String = ""
            Dim SR As New StreamReader(Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Utilities.RomNum.txt"))
            line = SR.ReadLine
            Do While line <> ""
                If line.Contains("=") Then
                    Dim Parts() As String = line.Split("=")
                    Roman_Number_Dict.Add(Parts(1), CInt(Parts(0)))
                    Numeral_to_Roman_Dict.Add(CInt(Parts(0)), Parts(1))
                End If
                line = SR.ReadLine
            Loop
            SR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Function ConvertInput(ByVal Input As String) As String
        If Roman_Number_Dict.Count < 100 Or Numeral_to_Roman_Dict.Count < 100 Then
            PopulateDictionary()
        End If
        Dim Output As String = ""
        If Input <> "" Then

            If IsNumeric(Input) Then
                Try
                    Dim myInt As Integer = Input
                    If myInt > 0 AndAlso myInt < 5000 Then
                        Output = Numeral_to_Roman_Dict(myInt)
                    Else
                        Output = INVALID
                    End If
                Catch ex As Exception
                    Output = INVALID
                End Try
            Else

                Try
                    Input = Input.ToUpper
                    Output = Roman_Number_Dict(Input)
                Catch ex As Exception
                End Try

                If Output = "0" Or Output = "" Then
                    Output = INVALID
                End If
            End If

        End If
        Return Output
    End Function
End Class
