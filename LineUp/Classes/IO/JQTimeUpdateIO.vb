Imports System.Data.SqlClient

''' <summary>
''' Responsible for keeping track of when the lineup table was last updated, as well as the sql io
''' </summary>
Public Class JQTimeUpdateIO


#Region "Properties"

    Private Property SqlConnStr As String = ""

    ''' <summary>
    ''' the time that the job q was last updated
    ''' </summary>
    ''' <returns></returns>
    Private Property LastUpdateTime As Date

    ''' <summary>
    ''' the time as string that the job q was last updated. to update time, update lastUpdateTime
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LastUpdateTimeStr As String
        Get
            Return LastUpdateTime.ToString("MM/dd/yyyy hh:mm:ss.fff tt")
        End Get
    End Property


#End Region


#Region "Init"

    Public Sub New(ByVal SqlConnStr As String)
        Me.SqlConnStr = SqlConnStr
    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Records the new update time to sql, (the timer checks against this to make sure that you have the latest database version)
    ''' </summary>
    ''' <param name="errors"></param>
    ''' <returns></returns>
    Public Function UpdateSqlWithNewTime(Optional ByRef errors As String = "") As Boolean
        Dim success As Boolean = False
        Try
            LastUpdateTime = Now
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    cmd.CommandText = "UPDATE UpdatedTime SET UpdateTime = @now WHERE id = 0"
                    cmd.Parameters.Add("@now", SqlDbType.VarChar).Value = LastUpdateTimeStr
                    conn.Open()
                    If cmd.ExecuteNonQuery > 0 Then
                        success = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            success = False
            errors &= ex.Message & vbCrLf
        End Try
        Return success
    End Function


    ''' <summary>
    ''' if the local dateTime matches the sql dateTime, returns true
    ''' </summary>
    ''' <returns></returns>
    Public Function IsUpToDate() As Boolean

        If getSqlUpdatedTime() = LastUpdateTimeStr Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' updates the local time from sql
    ''' </summary>
    ''' <returns></returns>
    Public Function UpdateLocalUpdateTimeFromSql() As Boolean
        Dim success As Boolean = False
        Try
            Dim newDate As Date = cNullDate
            Dim result As String = getSqlUpdatedTime()

            If Date.TryParse(result, newDate) Then
                LastUpdateTime = newDate
                success = True
            End If

        Catch ex As Exception

        End Try
        Return success
    End Function

    ''' <summary>
    ''' gets the latest updated time from sql
    ''' </summary>
    ''' <returns></returns>
    Private Function getSqlUpdatedTime() As String
        Dim result As String = ""
        Try
            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand()
                    cmd.CommandText = "SELECT UpdateTime from UpdatedTime WHERE id = 0"
                    conn.Open()
                    result = cmd.ExecuteScalar
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "getSqlUpdatedTime")
        End Try
        Return result
    End Function


#End Region


End Class
