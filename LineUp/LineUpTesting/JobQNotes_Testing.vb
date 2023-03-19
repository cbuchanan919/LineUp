Imports System.Text
Imports LineUp
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class JobQNotes_Testing

    <TestMethod()>
    Public Sub AddDeleteQNote()

        Dim io As New JQNoteIO("Server=BTP-SQL;DataBase=CB_Job_Q;User Id=;Password=;Connect Timeout=120;")

        Dim n As New JQNoteInfo(cNullInt, 1234, "A Note Title", "Note Text", BodyVsCover.Not_Set, Now, False, io)

        Dim success As Boolean = io.InsertUpdateNote(n, InsertOrUpdate.InsertInto)
        Console.WriteLine("Saved:" & vbCrLf & vbCrLf & n.ToString & vbCrLf & vbCrLf)
        n.NoteTitle = "A different Note title"
        If Not io.InsertUpdateNote(n, InsertOrUpdate.Update) Then success = False
        Console.WriteLine("Updated: " & vbCrLf & n.ToString & vbCrLf & vbCrLf)

        If Not io.DeleteNote(n) Then success = False
        Assert.IsTrue(success)

    End Sub

    '<TestMethod()> Public Sub MassDuplicateNote()

    '    Dim sw As New Stopwatch()
    '    sw.Start()

    '    Dim io As New JobQNoteIO("Server=BTP-SQL;DataBase=CB_Job_Q;User Id=;Password=;Connect Timeout=120;")
    '    io.LoadAllNotes()
    '    Dim n As JobQNote = io.LoadedNotes.First
    '    Console.WriteLine("Load Note: " & sw.Elapsed.ToString)
    '    Dim notes As New List(Of JobQNote)

    '    For i As Integer = 1 To 1000
    '        Dim iNote As JobQNote = n.Clone
    '        iNote.ItemNumber = i
    '        iNote.NoteTitle = "Note " & i
    '        notes.Add(n.Clone)
    '    Next
    '    io.InsertUpdateNotes(notes, InsertOrUpdate.InsertInto)
    '    Console.WriteLine("Saved All" & sw.Elapsed.ToString)
    '    'Dim n As New JobQNote(cNullInt, 1234, "A Note Title", "Note Text", BodyVsCover.Not_Set, Now, False, io)

    '    sw.Reset()
    '    Console.WriteLine(vbCrLf)
    '    io.LoadAllNotes()
    '    Console.WriteLine("Loaded all " & io.LoadedNotes.Count & " notes. " & sw.Elapsed.ToString)

    '    For Each myNote As JobQNote In io.LoadedNotes
    '        If Not myNote.NoteId = n.NoteId Then
    '            io.DeleteNote(myNote)
    '        End If
    '    Next
    '    Console.WriteLine("Deleted all notes: " & sw.Elapsed.ToString)
    '    Assert.IsTrue(1 = 1)


    '    'Dim success As Boolean = io.InsertUpdateNote(n, InsertOrUpdate.InsertInto)
    '    'Console.WriteLine("Saved:" & vbCrLf & vbCrLf & n.ToString & vbCrLf & vbCrLf)
    '    'n.NoteTitle = "A different Note title"
    '    'If Not io.InsertUpdateNote(n, InsertOrUpdate.Update) Then success = False
    '    'Console.WriteLine("Updated: " & vbCrLf & n.ToString & vbCrLf & vbCrLf)

    '    'If Not io.DeleteNote(n) Then success = False
    '    'Assert.IsTrue(success)

    'End Sub

End Class