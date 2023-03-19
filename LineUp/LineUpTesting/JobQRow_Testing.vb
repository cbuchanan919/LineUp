Imports System.Text
Imports LineUp
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class JobQRow_Testing

    '<TestMethod()>
    'Public Sub CheckLayersCalcTest()
    '    ' Arrange
    '    Dim io As New JQNoteIO("Server=BTP-SQL;DataBase=CB_Job_Q;User Id=;Password=;Connect Timeout=120;")

    '    Dim n As New JQNoteInfo(cNullInt, 1234, "A Note Title", "Note Text", BodyVsCover.Not_Set, Now, False, io)

    '    ' Act
    '    Dim success As Boolean = io.InsertUpdateNote(n, InsertOrUpdate.InsertInto)
    '    Console.WriteLine("Saved:" & vbCrLf & vbCrLf & n.ToString & vbCrLf & vbCrLf)
    '    n.NoteTitle = "A different Note title"
    '    If Not io.InsertUpdateNote(n, InsertOrUpdate.Update) Then success = False
    '    Console.WriteLine("Updated: " & vbCrLf & n.ToString & vbCrLf & vbCrLf)

    '    If Not io.DeleteNote(n) Then success = False

    '    ' Assert
    '    Assert.IsTrue(success)

    'End Sub


End Class