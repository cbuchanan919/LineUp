Imports System.Data.SqlClient
''' <summary>
''' Reads/Writes email history to sql.
''' </summary>
Public Class EmailLogIO


#Region "Properties"

    Private Property SqlConnStr As String = ""

    ''' <summary>
    ''' key = id, value = email log info
    ''' </summary>
    ''' <returns></returns>
    Public Property SentEmails As New Dictionary(Of Integer, EmailLogInfo)


#End Region


#Region "Init"

    Public Sub New(ByVal SqlConnStr As String)
        Me.SqlConnStr = SqlConnStr

    End Sub

#End Region


#Region "Methods"

    'rowID, dateTime, computer name, user name, contents
    ''' <summary>
    ''' gets the ID, TimeSent, ComputerName, UserName and Subject (but not the contents) for all the items found, and updates the SentEmails dictionary.
    ''' </summary>
    ''' <returns></returns>
    Public Function getSentEmails() As Boolean

        Dim success As Boolean = True

        Try
            'clear log
            SentEmails.Clear()
            Using sqlConn As New SqlConnection(SqlConnStr)
                Using sqlCmd As SqlCommand = sqlConn.CreateCommand
                    sqlCmd.CommandText = "SELECT ID, TimeSent, ComputerName, UserName, Subject, EmailType FROM QpEmailLog"
                    sqlConn.Open()

                    Using sqlReader As SqlDataReader = sqlCmd.ExecuteReader
                        If sqlReader.HasRows Then
                            While (sqlReader.Read)

                                'reads each entry into the sentEmails dict
                                'the integers below (ie. .getDatetime(1)) are associated with the columns in the select statement above.
                                Dim eLog As New EmailLogInfo
                                eLog.ID = sqlReader.GetInt32(0)
                                eLog.TimeSent = sqlReader.GetDateTime(1)
                                eLog.ComputerName = sqlReader.GetString(2)
                                eLog.UserName = sqlReader.GetString(3)
                                eLog.Subject = sqlReader.GetString(4)
                                eLog.EmailType = sqlReader.GetInt32(5)
                                If eLog.ID <> cNullInt Then
                                    'updates sent emails dict
                                    If SentEmails.ContainsKey(eLog.ID) Then
                                        SentEmails(eLog.ID) = eLog
                                    Else
                                        SentEmails.Add(eLog.ID, eLog)
                                    End If
                                End If


                            End While
                        End If
                    End Using

                End Using
            End Using

            If SentEmails.Count = 0 Then success = False 'there should be at least 1 in the log...

        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Get Sent Emails")
        End Try

        Return success

    End Function
    ''' <summary>
    ''' Gets /returns all the details for a specific log entry. Also updates that entry in the sentEmails Dict.
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <returns></returns>
    Public Function getAllJobDetails(ByVal ID As Integer) As EmailLogInfo

        Dim updatedLog As EmailLogInfo = Nothing
        Using sqlConn As New SqlConnection(SqlConnStr)
            Using sqlCmd As SqlCommand = sqlConn.CreateCommand
                sqlCmd.CommandText = "SELECT ID, TimeSent, ComputerName, UserName, Subject, Contents, EmailType FROM QpEmailLog WHERE ID=@ID"
                sqlCmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID
                sqlConn.Open()

                Using sqlReader As SqlDataReader = sqlCmd.ExecuteReader
                    If sqlReader.HasRows Then
                        sqlReader.Read()
                        'reads each entry into the sentEmails dict
                        'the integers below (ie. .getDatetime(1)) are associated with the columns in the select statement above.
                        Dim eLog As New EmailLogInfo
                        eLog.ID = sqlReader.GetInt32(0)
                        eLog.TimeSent = sqlReader.GetDateTime(1)
                        eLog.ComputerName = sqlReader.GetString(2)
                        eLog.UserName = sqlReader.GetString(3)
                        eLog.Subject = sqlReader.GetString(4)
                        eLog.Contents = sqlReader.GetString(5)
                        eLog.EmailType = sqlReader.GetInt32(6)

                        If eLog.ID <> cNullInt Then
                            'updates sent emails dict
                            If SentEmails.ContainsKey(eLog.ID) Then
                                SentEmails(eLog.ID) = eLog
                            Else
                                SentEmails.Add(eLog.ID, eLog)
                            End If

                            updatedLog = eLog
                        End If



                    End If
                End Using

            End Using
        End Using

        Return updatedLog

    End Function

    ''' <summary>
    ''' inserts a new log to sql. recommend running 'getSentEmails' after
    ''' </summary>
    ''' <param name="log"></param>
    ''' <returns></returns>
    Public Function AddLogToSql(ByVal log As EmailLogInfo) As Boolean
        Dim success As Boolean = False
        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim query As String = ""
                    query &= "INSERT INTO QpEmailLog (TimeSent, ComputerName, UserName, Subject, Contents, EmailType) "
                    query &= "VALUES (@TimeSent, @ComputerName, @UserName, @Subject, @Contents, @EmailType)"
                    cmd.CommandText = query
                    With cmd.Parameters
                        .Add("@TimeSent", SqlDbType.DateTime).Value = log.TimeSent
                        .Add("@ComputerName", SqlDbType.VarChar).Value = log.ComputerName
                        .Add("@UserName", SqlDbType.VarChar).Value = log.UserName
                        .Add("@Subject", SqlDbType.VarChar).Value = log.Subject
                        .Add("@Contents", SqlDbType.VarChar).Value = log.Contents
                        .Add("@EmailType", SqlDbType.Int).Value = log.EmailType
                    End With
                    conn.Open()
                    If cmd.ExecuteNonQuery = 1 Then
                        success = True
                    End If
                End Using
            End Using

        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "AddLogToSql")
        End Try
        Return success
    End Function

    ''' <summary>
    ''' deletes the email log with the specified id from sql. Recommend running 'getSentEmails' after
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function DeleteLogFromSql(ByVal id As Integer) As Boolean
        Dim success As Boolean = False
        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    cmd.CommandText = "DELETE FROM QpEmailLog WHERE ID=@ID"
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                    conn.Open()
                    If cmd.ExecuteNonQuery = 1 Then
                        success = True
                    End If
                End Using
            End Using

        Catch ex As Exception
            success = False
            MsgBox(ex.Message, MsgBoxStyle.Critical, "DeleteLogFromSql")
        End Try
        Return success
    End Function
#End Region


End Class