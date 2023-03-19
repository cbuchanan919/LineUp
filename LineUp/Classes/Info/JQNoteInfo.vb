
''' <summary>
''' Stores a job q note
''' </summary>
Public Class JQNoteInfo


#Region "Properties"
    Private Property NoteIO As JQNoteIO = Nothing
    Public Property NoteId As Integer = cNullInt
    Public Property ItemNumber As Integer = cNullInt
    Public Property NoteTitle As String = ""
    Public Property NoteText As String = ""
    Public Property NoteBodOrCov As BodyVsCover = BodyVsCover.Not_Set
    Public Property LastEditTime As Date = cNullDate
    Public Property ShowOnTicket As Boolean = False


    '--------------- UI stuff ---------------
    Private Property IsInit As Boolean = False
    Private Property NoteGB As GroupBox = Nothing
    Private Property RtfBox As RichTextBox = Nothing
    Private Property MainTitleLbl As Label = Nothing
    Private Property LastEditedTimeLbl As Label = Nothing
    Private Property ItemNumTxt As TextBox = Nothing
    Private Property TitleTxt As TextBox = Nothing
    'Private Property SaveBtn As Button = Nothing
    Private Property DateModifiedLbl As Label = Nothing

#End Region


#Region "Init"
    Public Sub New(ByRef NoteIO As JQNoteIO)
        If IsNothing(NoteIO) Then Throw New Exception("Job Q Note IO is not defined")
        Me.NoteIO = NoteIO
    End Sub

    Public Sub New(ByVal NoteId As Integer,
                   ByVal ItemNumber As Integer,
                   ByVal NoteTitle As String,
                   ByVal NoteText As String,
                   ByVal NoteBodOrCov As BodyVsCover,
                   ByVal LastEditTime As Date,
                   ByVal ShowOnTicket As Boolean,
                   ByRef NoteIO As JQNoteIO)
        If IsNothing(NoteIO) Then Throw New Exception("Job Q Note IO is not defined")
        Me.NoteId = NoteId
        Me.ItemNumber = ItemNumber
        Me.NoteTitle = NoteTitle
        Me.NoteText = NoteText
        Me.NoteBodOrCov = NoteBodOrCov
        Me.LastEditTime = LastEditTime
        Me.ShowOnTicket = ShowOnTicket
        Me.NoteIO = NoteIO
    End Sub
#End Region


#Region "Methods"
    Public Sub SelfCheck()
        LastEditTime = Now

    End Sub

    Public Function Clone(Optional ByVal newNoteID = cNullInt) As JQNoteInfo
        Dim n As New JQNoteInfo(newNoteID, ItemNumber, NoteTitle, NoteText, NoteBodOrCov, LastEditTime, ShowOnTicket, NoteIO)
        Return n
    End Function

    Public Function GetGroupBox() As GroupBox
        If Not IsNothing(NoteGB) Then
            Return NoteGB
        Else
            'GBox is nothing. Create new one.
            NoteGB = New GroupBox
            NoteGB.Size = New Size(600, 800)

            Dim flow As New FlowLayoutPanel
            flow.AutoScroll = True
            flow.FlowDirection = FlowDirection.LeftToRight
            flow.Dock = DockStyle.Fill

            MainTitleLbl = New Label With {.Text = NoteTitle, .Font = New Drawing.Font("Segoe UI", 14, FontStyle.Bold), .AutoSize = True}

            flow.Controls.Add(MainTitleLbl)

            RtfBox = New RichTextBox()
            RtfBox.Size = New Size(580, 550)
            RtfBox.Rtf = NoteText
            AddHandler RtfBox.TextChanged, AddressOf UpdateFromGroupBox
            flow.Controls.Add(RtfBox)

            DateModifiedLbl = New Label With {.AutoSize = True, .Text = GetDateModifiedLblStr()
            }
            flow.Controls.Add(DateModifiedLbl)


            Dim tb As New TableLayoutPanel
            tb.Width = 580
            tb.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100))
            tb.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

            tb.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
            tb.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))

            Dim ItemNumLbl As New Label With {.Text = "Item #:", .TextAlign = ContentAlignment.MiddleRight}
            ItemNumTxt = New TextBox With {.Text = ItemNumber, .Dock = DockStyle.Fill}
            If ItemNumber <> cNullInt Then ItemNumTxt.Enabled = False
            AddHandler ItemNumTxt.KeyPress, AddressOf ItemNumText_Key
            AddHandler ItemNumTxt.TextChanged, AddressOf UpdateFromGroupBox
            tb.Controls.Add(ItemNumLbl, 0, 0)
            tb.Controls.Add(ItemNumTxt, 1, 0)

            Dim TitleLbl As New Label With {.Text = "Title:", .TextAlign = ContentAlignment.MiddleRight}
            TitleTxt = New TextBox With {.Text = NoteTitle, .Dock = DockStyle.Fill}
            AddHandler TitleTxt.TextChanged, AddressOf UpdateFromGroupBox
            tb.Controls.Add(TitleLbl, 0, 1)
            tb.Controls.Add(TitleTxt, 1, 1)

            flow.Controls.Add(tb)

            Dim SaveBtn As New Button With {.Text = "Save", .MinimumSize = New Size(120, 30)}
            flow.Controls.Add(SaveBtn)
            AddHandler SaveBtn.Click, AddressOf SaveBtn_Click

            Dim EditBtn As New Button With {.Text = "Edit Externally", .MinimumSize = New Size(120, 30)}
            AddHandler EditBtn.Click, AddressOf EditExternally_Click
            flow.Controls.Add(EditBtn)
            NoteGB.Controls.Add(flow)
            IsInit = True
            Return NoteGB

        End If
    End Function
    Private Sub EditExternally_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If NoteId = cNullInt Then
                If MsgBox("Your note has not been saved. Okay to save?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    NoteIO.InsertUpdateNote(Me, InsertOrUpdate.InsertInto)
                End If
            End If
            If NoteId <> cNullInt Then
                Dim fp As String = IO.Path.Combine(My.Settings.dirResources, "NotesToEdit", NoteId.ToString & "-Note.rtf")
                RtfBox.SaveFile(fp)
                If IO.File.Exists(fp) Then
                    Diagnostics.Process.Start(fp)
                    If MsgBox("Click ok when done editing." & vbCrLf & vbCrLf & vbCrLf & "Make SURE to save!") Then
                        Dim success As Boolean = False
                        Do Until success
                            Try
                                RtfBox.LoadFile(fp)
                                NoteIO.InsertUpdateNote(Me, InsertOrUpdate.Update)
                                success = True
                            Catch ex As Exception
                                If MessageBox.Show(ex.Message & vbCrLf & vbCrLf & "(Make sure you closed Word)" & vbCrLf & vbCrLf & "Try Again?", "Error Loading File...", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
                                    success = True
                                End If
                                MsgBox(ex.Message, MsgBoxStyle.YesNo, "Error Loading File...")
                            End Try
                        Loop


                    End If
                End If


            End If
            GetDateModifiedLblStr()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Editing...")
        End Try

    End Sub
    Private Sub ItemNumText_Key(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Dim OKChars As List(Of Char) = "-0123456789".ToList
        OKChars.Add(vbBack)

        If Not OKChars.Contains(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub SaveBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        If NoteId = cNullInt Then
            NoteIO.InsertUpdateNote(Me, InsertOrUpdate.InsertInto)
        Else
            NoteIO.InsertUpdateNote(Me, InsertOrUpdate.Update)
        End If
        GetDateModifiedLblStr()
    End Sub

    Public Sub UpdateFromGroupBox(ByVal sender As Object, ByVal e As EventArgs)
        If IsInit Then
            'Get updates from edits
            Select Case True
                Case sender Is RtfBox
                    NoteText = RtfBox.Rtf



                Case sender Is TitleTxt
                    NoteTitle = TitleTxt.Text
                    MainTitleLbl.Text = NoteTitle

                Case sender Is ItemNumTxt
                    If ItemNumTxt.Text.Trim = "" Then
                        ItemNumber = cNullInt
                    Else
                        ItemNumber = Integer.Parse(ItemNumTxt.Text)
                    End If


                Case sender Is Nothing

            End Select


        End If


    End Sub

    Private Function GetDateModifiedLblStr() As String
        Dim modifiedStr As String = ""
        If LastEditTime = cNullDate Then
            modifiedStr = "(Last Saved: Never)"
        Else
            modifiedStr = "(Last Saved: " & LastEditTime.ToString & ")"
        End If

        If IsInit AndAlso Not IsNothing(DateModifiedLbl) Then
            DateModifiedLbl.Text = modifiedStr
        End If
        Return modifiedStr
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("NoteId: " & NoteId)
        sb.AppendLine("ItemNumber: " & ItemNumber)
        sb.AppendLine("NoteTitle: " & NoteTitle)
        sb.AppendLine("NoteText: " & NoteText)
        sb.AppendLine("NoteBodOrCov: " & NoteBodOrCov)
        sb.AppendLine("LastEditTime: " & LastEditTime)
        sb.AppendLine("ShowOnTicket: " & ShowOnTicket)
        Return sb.ToString()
    End Function


#End Region


End Class





