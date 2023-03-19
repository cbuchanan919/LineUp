Imports System.Windows.Forms
Imports System
Imports System.IO

Public Class frmError

    Delegate Sub addErrorCallback(ByVal errorMessage As String, ByVal SubName As String, ByVal isError As Boolean)

    ''' <summary>
    ''' General Use Error Log
    ''' </summary>
    ''' <param name="ProgramTitle">What to show as the the Error Log's title</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ProgramTitle As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'sets the program to use rtf.
        Me.Text = ProgramTitle
        ErrorRTF.Rtf = "{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Calibri;}} \viewkind4\uc1\pard\sa200\sl276\slmult1\lang9\f0\fs22 \fs40" & ProgramTitle & "\fs22  \line\tab Date: " & Date.Today.ToShortDateString & "   User: " & Environment.UserName & "\par }"

    End Sub

    Private Sub frmError_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub frmError_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub addError(ByVal errorMessage As String, ByVal SubName As String, ByVal isError As Boolean)
        Try
            'This next if statement is used by other if necessary (by other threads)
            If ErrorRTF.InvokeRequired Then
                Dim addErrorDelegate As New addErrorCallback(AddressOf addError)
                Me.Invoke(addErrorDelegate, errorMessage, SubName, isError)
            Else
                'The method calling it is on the same thread as frmError

                Dim NewRtf As String = ErrorRTF.Rtf.Trim
                Dim TabLine As String = " \line\tab "
                NewRtf = NewRtf.TrimEnd("}")

                If isError Then
                    NewRtf &= " \par \b  Error: \b0 "
                Else
                    NewRtf &= " \par  Info: "
                End If

                NewRtf &= TabLine & "Name: \tab " & SubName
                NewRtf &= TabLine & "Time: \tab " & DateTime.Now.ToShortTimeString
                NewRtf &= TabLine & "Msg:  \tab " & errorMessage.Replace("\", "\\")

                'closes the rtf file.
                NewRtf &= "}"
                ErrorRTF.Rtf = NewRtf
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Saves the file to the specified location
    ''' </summary>
    ''' <param name="FileLocation">Specify a file path with a RTF extension.</param>
    ''' <remarks></remarks>
    Public Function SaveLog(ByVal FileLocation As String) As Boolean

        Dim Success As Boolean = False
        Try
            Dim Save As Boolean = False

            Dim FolderPath As String = Path.GetDirectoryName(FileLocation)
            If File.Exists(FileLocation) Then
                If MsgBox("Are you sure you want to replace this file?" & vbCrLf & FileLocation, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Save = True
                End If
            ElseIf Directory.Exists(FolderPath) Then
                Save = True
            End If

            If Save Then
                ErrorRTF.SaveFile(FileLocation, RichTextBoxStreamType.RichText)
                Success = True
            End If

        Catch ex As Exception
            Success = False
        End Try

        Return Success
    End Function

    Public Function getText() As String
        Return ErrorRTF.Text
    End Function
End Class