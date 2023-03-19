Imports System.Data.SqlClient

''' <summary>
''' Controls Job Q Note IO
''' </summary>
Public Class JQNoteIO


#Region "Properties"
    Private Property SqlConnStr As String = ""
    Public Property LoadedNotes As New List(Of JQNoteInfo)
#End Region


#Region "Init"
    Public Sub New(ByVal SqlConnStr As String)
        Me.SqlConnStr = SqlConnStr
    End Sub

#End Region


#Region "Methods"


    Public Function LoadAllNotes() As List(Of JQNoteInfo)
        Dim notes As New List(Of JQNoteInfo)
        LoadedNotes.Clear()

        Try
            Using sqlConn As New SqlConnection(SqlConnStr)
                Using sqlCmd As SqlCommand = sqlConn.CreateCommand
                    Dim query As String = "SELECT ID, ItemNum, NoteTitle, NoteText, NoteBodOrCov, LastEditTime, ShowOnTicket from QpNotes"
                    'If noteID <> cNullInt Then
                    '    query &= " WHERE ID=@ID"
                    '    sqlCmd.Parameters.AddWithValue("@ID", noteID)
                    'End If
                    sqlCmd.CommandText = query
                    sqlConn.Open()

                    Using reader As SqlDataReader = sqlCmd.ExecuteReader
                        Do While reader.Read
                            Dim note As New JQNoteInfo(Me)
                            If Not reader.IsDBNull(0) Then note.NoteId = reader.GetInt32(0)
                            If Not reader.IsDBNull(1) Then note.ItemNumber = reader.GetInt32(1)
                            If Not reader.IsDBNull(2) Then note.NoteTitle = reader.GetString(2)
                            If Not reader.IsDBNull(3) Then note.NoteText = reader.GetString(3)
                            If Not reader.IsDBNull(4) Then note.NoteBodOrCov = reader.GetInt32(4)
                            If Not reader.IsDBNull(5) Then note.LastEditTime = reader.GetDateTime(5)
                            If Not reader.IsDBNull(6) Then note.ShowOnTicket = reader.GetBoolean(6)
                            notes.Add(note)
                        Loop
                    End Using
                End Using
            End Using
            LoadedNotes.AddRange(notes)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Loading Notes From SQL Error")

        End Try
        Return notes
    End Function

    Public Function InsertUpdateNote(ByVal note As JQNoteInfo, ByVal AddVsUpdate As InsertOrUpdate) As Boolean
        Dim notes As New List(Of JQNoteInfo)
        notes.Add(note)
        Return InsertUpdateNotes(notes, AddVsUpdate)
    End Function

    ''' <summary>
    ''' inserts /adds note to sql
    ''' </summary>
    ''' <param name="notes"></param>
    ''' <param name="AddVsUpdate"></param>
    ''' <returns></returns>
    Public Function InsertUpdateNotes(ByVal notes As List(Of JQNoteInfo), ByVal AddVsUpdate As InsertOrUpdate) As Boolean
        Dim success As Boolean = True
        Try
            Using sqlConn As New SqlConnection(SqlConnStr)
                Using sqlCmd As SqlCommand = sqlConn.CreateCommand
                    Dim query As String = ""
                    Select Case AddVsUpdate
                        Case InsertOrUpdate.InsertInto
                            query &= "INSERT INTO QpNotes "
                            query &= "(ItemNum, NoteTitle, NoteText, NoteBodOrCov, LastEditTime, ShowOnTicket) VALUES "
                            query &= "(@ItemNum, @NoteTitle, @NoteText, @NoteBodOrCov, @LastEditTime, @ShowOnTicket)"
                        Case InsertOrUpdate.Update
                            query &= "UPDATE QpNotes "
                            query &= "SET ItemNum = @ItemNum, NoteTitle = @NoteTitle, NoteText = @NoteText, NoteBodOrCov = @NoteBodOrCov, LastEditTime = @LastEditTime, ShowOnTicket = @ShowOnTicket "
                            query &= "WHERE ID = @ID"
                    End Select

                    If query <> "" Then
                        sqlConn.Open()
                        sqlCmd.CommandText = query
                        For Each note As JQNoteInfo In notes
                            note.SelfCheck()

                            With sqlCmd.Parameters
                                .Clear()
                                If AddVsUpdate = InsertOrUpdate.Update Then .Add("@ID", SqlDbType.Int).Value = note.NoteId
                                .Add("@ItemNum", SqlDbType.Int).Value = note.ItemNumber
                                .Add("@NoteTitle", SqlDbType.VarChar).Value = note.NoteTitle
                                .Add("@NoteText", SqlDbType.VarChar).Value = note.NoteText
                                .Add("@NoteBodOrCov", SqlDbType.Int).Value = note.NoteBodOrCov
                                .Add("@LastEditTime", SqlDbType.DateTime).Value = note.LastEditTime
                                .Add("@ShowOnTicket", SqlDbType.Bit).Value = note.ShowOnTicket
                            End With
                            sqlCmd.ExecuteNonQuery()

                            If AddVsUpdate = InsertOrUpdate.InsertInto Then
                                Dim q2 As String = "Select @@Identity"
                                Using cmd2 As SqlCommand = sqlConn.CreateCommand
                                    cmd2.CommandText = q2
                                    note.NoteId = cmd2.ExecuteScalar
                                End Using
                                If Not LoadedNotes.Contains(note) Then
                                    LoadedNotes.Add(note)
                                End If
                            End If
                        Next
                    End If

                End Using
            End Using

        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Notes to SQL Error")
        End Try

        Return success


    End Function

    ''' <summary>
    ''' deletes the note from the sql database. Doesn't currently remove it from the loadedNotes list
    ''' </summary>
    ''' <param name="note"></param>
    ''' <returns></returns>
    Public Function DeleteNote(ByVal note As JQNoteInfo) As Boolean
        Dim success As Boolean = False
        Try
            Using sqlConn As New SqlConnection(SqlConnStr)
                Using sqlCmd As SqlCommand = sqlConn.CreateCommand
                    Dim query As String = "DELETE FROM QpNotes Where ID = @ID"
                    sqlCmd.Parameters.AddWithValue("@ID", note.NoteId)
                    sqlCmd.CommandText = query
                    sqlConn.Open()
                    If sqlCmd.ExecuteNonQuery = 1 Then
                        success = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Delete Note From SQL Error")
        End Try
        Return success


    End Function
    ''' <summary>
    ''' Returns a list of notes for the item number
    ''' </summary>
    ''' <param name="ItemNum"></param>
    ''' <returns></returns>
    Public Function getNotesForItemNumber(ByVal ItemNum As Integer, Optional ByVal addBlankNote As Boolean = True) As List(Of JQNoteInfo)
        Dim found As New List(Of JQNoteInfo)
        For Each myNote In LoadedNotes
            If myNote.ItemNumber = ItemNum Then
                found.Add(myNote)
            End If
        Next
        If addBlankNote AndAlso found.Count = 0 AndAlso ItemNum > 0 Then
            Dim NewNote As New JQNoteInfo(Me)
            NewNote.ItemNumber = ItemNum
            NewNote.NoteTitle = "Note - (" & ItemNum & ")"
            found.Add(NewNote)
        End If
        Return found
    End Function

#End Region


End Class