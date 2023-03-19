Imports System.Windows.Forms
Imports System.IO
Public Class frmHelp

    Public LoadFixList As Boolean = False

    'HelpEdited is used to see if the text has been edited since it was loaded.
    Dim HelpEdited As Boolean = False
    'loadedHelpName keeps track of current file name
    Dim loadedHelpName As String = ""
    Dim loadedHelpFilePath As String = ""


    Private Sub Help_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim selectItem As Boolean = LoadFixList
        LineUp.MybtnFmt.Format_Controls(Me)
        RefreshHelpList()
        Dim int As Integer = 0
        Dim SelInt As Integer = -1
        If selectItem = False Then
            For Each myItem In lstBoxHelp.Items
                Try
                    If myItem.Contains("Line Up") Then
                        ' lstBoxHelp.SelectedItem = myItem
                        SelInt = int
                    End If
                Catch ex As Exception

                End Try
                int += 1
            Next
            If SelInt >= 0 Then
                lstBoxHelp.SelectedIndex = SelInt
            End If


        End If
    End Sub

    Private Sub Help_close(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        'checks to see if the file has been saved. if cancelled, then it won't close the form
        If HelpEditedCheck() = DialogResult.Cancel Then
            e.Cancel = True
        End If
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Me.DialogResult = System.Windows.Forms.DialogResult.OK
        If HelpEditedCheck() <> DialogResult.Cancel Then
            Me.Close()
        End If

    End Sub



    Private Sub btnAddNewHelp_Click(sender As Object, e As EventArgs) Handles btnAddNewHelp.Click
        'tries to add a new help file to the resources directory
        Try
            'clears the previous title from the textbox
            'NewHelp.txtTitle.Text = ""
            NewHelp.txtTitle.SelectAll()
            If NewHelp.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim write As System.IO.StreamWriter
                write = File.CreateText(My.Settings.dirResources & NewHelp.txtTitle.Text & ".rtf")
                write.WriteLine("{\rtf1")
                write.WriteLine(NewHelp.txtTitle.Text & " - Help")
                write.WriteLine("}")
                write.Close()
                RefreshHelpList()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnRemoveHelp_Click(sender As Object, e As EventArgs) Handles btnRemoveHelp.Click
        'removes the file from the directory if confirmed that the user wants to do so.
        If MsgBox("Are you sure you want to remove """ & loadedHelpName & """?", MsgBoxStyle.OkCancel, "Delete this article?") = MsgBoxResult.Ok Then
            Try
                File.Delete(My.Settings.dirResources & loadedHelpName & ".rtf")
                RefreshHelpList()
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
        End If
    End Sub

    Private Function HelpEditedCheck() As DialogResult
        'function to take care of multiple prompts of save changes
        Dim result As Integer = DialogResult.Ignore
        HelpEdited = RtfEditor1.isRtfBoxModified

        If HelpEdited = True Then
            result = MsgBox("It looks like there are unsaved changes to """ & loadedHelpName & """. Do you want to save it?", MsgBoxStyle.YesNoCancel, "Save Help File?")
            If result = DialogResult.Yes Then
                RtfEditor1.SaveRtf(My.Settings.dirResources & loadedHelpName & ".rtf")
                HelpEdited = False
                result = DialogResult.OK
            End If
        Else
            result = DialogResult.OK
        End If


        Return result
    End Function

    Private Sub btnRefreshHelp_Click() Handles btnRefreshHelp.Click 'sender As Object, e As EventArgs
        RefreshHelpList()
    End Sub
    Public Sub RefreshHelpList()

        Dim ContinueBOO As Boolean = True
        'if the text file has been updated then it prompts the user

        If HelpEditedCheck() = DialogResult.Cancel Then
            ContinueBOO = False
        End If


        If ContinueBOO = True Then
            Try
                RtfEditor1.ClearRTFBox()
                lstBoxHelp.Items.Clear()
                Dim Dir As New DirectoryInfo(My.Settings.dirResources)
                For Each File As FileInfo In Dir.GetFiles
                    If File.Name.Contains(".rtf") Then
                        lstBoxHelp.Items.Add(File.Name.Replace(".rtf", ""))
                    End If
                Next
                ' listBoxHelp.SelectedIndex = 0
                ' HelpEdited = False
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
        End If
        HelpEdited = False
        If LoadFixList Then
            Dim Int As Integer = 0
            Dim selInt As Integer = -1
            For Each myItem As String In lstBoxHelp.Items
                Try
                    If myItem.Contains("Fix") Then
                        'lstBoxHelp.SelectedItem = myItem
                        selInt = Int
                    End If
                Catch ex As Exception

                End Try
                Int += 1
            Next
            If selInt >= 0 Then
                lstBoxHelp.SelectedIndex = selInt
            End If

            LoadFixList = False
        End If
    End Sub

    Private Sub listBoxHelp_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles lstBoxHelp.SelectedIndexChanged
        'handles the changing of help files.

        'if the file has been edited, then it checks the user if they want to save it.
        Dim ContinueBOO As Boolean = True
        If HelpEditedCheck() = DialogResult.Cancel Then
            ContinueBOO = False
        End If


        If ContinueBOO = True Then


            Try
                loadedHelpName = lstBoxHelp.SelectedItem.ToString
                loadedHelpFilePath = My.Settings.dirResources & loadedHelpName & ".rtf"
                RtfEditor1.OpenFile(loadedHelpFilePath)
                HelpEdited = False
            Catch ex As Exception
                If ex.Message.Contains("instance of an object") Then
                Else
                    MsgBox(ex.Message.ToString)
                End If

            End Try
        End If

    End Sub

    Private Sub btnUpdateHelp() 'Handles btnSaveHelp.Click 'sender As Object, e As EventArgs
        Try
            'MsgBox(txtHelp.Rtf)
            If MsgBox("Are you sure you want overwrite """ & loadedHelpName & """?", MsgBoxStyle.OkCancel, "Save?") = MsgBoxResult.Ok Then
                RtfEditor1.SaveRtf(My.Settings.dirResources & loadedHelpName & ".rtf")
                HelpEdited = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub




    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click
        'This uses the NewHelp prompt to rename the file
        Dim Title As String = NewHelp.Text
        Dim myLabel As String = NewHelp.Label1.Text
        Dim PromptTitle As String = NewHelp.PromptTitle

        NewHelp.PromptTitle = "Rename Article?"
        NewHelp.Label1.Text = "Please enter a new name for: """ & loadedHelpName & """"
        NewHelp.Text = "Rename Help File"
        NewHelp.txtTitle.Text = loadedHelpName
        NewHelp.txtTitle.SelectAll()
        Dim Result As Integer = NewHelp.ShowDialog
        If Result = vbOK Then
            If loadedHelpName <> NewHelp.txtTitle.Text AndAlso NewHelp.txtTitle.Text <> "" Then
                If File.Exists(My.Settings.dirResources & loadedHelpName & ".rtf") Then
                    Try
                        My.Computer.FileSystem.RenameFile(My.Settings.dirResources & loadedHelpName & ".rtf", NewHelp.txtTitle.Text & ".rtf")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
                'Else
                '    MsgBox("I couldn't rename it, Sorry!", MsgBoxStyle.Exclamation, "Error")
            End If
        End If
        RefreshHelpList()
        For i As Integer = 0 To lstBoxHelp.Items.Count - 1
            If lstBoxHelp.Items(i).ToString.Contains(NewHelp.txtTitle.Text) Then
                lstBoxHelp.SelectedIndex = i
            End If
        Next
        NewHelp.Text = Title
        NewHelp.Label1.Text = myLabel
        NewHelp.PromptTitle = PromptTitle
    End Sub

End Class
