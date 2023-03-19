Imports System.Data.SqlClient
Imports System.Windows.Forms

''' <summary>
''' manages the printers and printer queues specified in the sql database.
''' </summary>
Public Class SQLConnectionUtilities


#Region "Variables & Properties"


    Public Property sqlConn As SqlConnection
    Public Property sqlCmd As SqlCommand


    ''' <summary>
    ''' Sql Statement to use. ie. SELECT * FROM BTP_JobQ
    ''' </summary>
    ''' <returns></returns>
    Public Property SqlQuery As String = ""
    ''' <summary>
    ''' Server to connect to
    ''' </summary>
    ''' <returns></returns>
    Private Property serverName() As String

    ''' <summary>
    ''' User ID to use
    ''' </summary>
    ''' <returns></returns>
    Private Property userID() As String

    ''' <summary>
    ''' User's password
    ''' </summary>
    ''' <returns></returns>
    Private Property password() As String

    ''' <summary>
    ''' SQL database name to use
    ''' </summary>
    ''' <returns></returns>
    Private Property sqlDatabaseName() As String



    ''' <summary>
    ''' finished sql connection string
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property sqlConnStr(Optional ByVal useIntegratedSecurity As Boolean = False) As String
        Get
            If useIntegratedSecurity Then
                Dim str =
               "Server=" & serverName & ";" &
               "DataBase=" & sqlDatabaseName & ";" &
               "Integrated Security=True;" &
               "Connect Timeout=120;"
                Return str
            Else
                Dim str =
               "Server=" & serverName & ";" &
               "DataBase=" & sqlDatabaseName & ";" & ' "Integrated Security=True;" &
               "User Id=" & userID & ";" &
               "Password=" & password & ";" &
               "Connect Timeout=120;"
                Return str
            End If

        End Get
    End Property

    ''' <summary>
    ''' copies the sql connection info... 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Copy(Optional ByVal newQuery As String = "", Optional ByVal newDatabase As String = "") As SQLConnectionUtilities
        Get
            Dim tempStatement = SqlQuery
            Dim tempDatabase = sqlDatabaseName

            If newQuery <> "" Then tempStatement = newQuery 'if a statement was specified, it gets entered here
            If newDatabase <> "" Then tempDatabase = newDatabase 'if a database was specified, it gets entered here.

            Return New SQLConnectionUtilities(serverName, userID, password, tempDatabase, newQuery)
        End Get
    End Property




#End Region


#Region "Init"




    ''' <summary>
    ''' Creates a new sql connection
    ''' </summary>
    ''' <param name="ServerName">Server to connect to</param>
    ''' <param name="UserId">the user's id</param>
    ''' <param name="Password">the user's password</param>
    ''' <param name="SqlDatabaseName">database name</param>
    Public Sub New(ByVal ServerName As String,
                   ByVal UserId As String,
                   ByVal Password As String,
                   ByVal SqlDatabaseName As String,
                   Optional ByVal SqlQuery As String = "")

        Me.serverName = ServerName
        Me.userID = UserId
        Me.password = Password
        Me.sqlDatabaseName = SqlDatabaseName
        Me.SqlQuery = SqlQuery

    End Sub


#End Region


#Region "Methods"



    ''' <summary>
    ''' Updates specified value, based on id
    ''' </summary>
    ''' <param name="idColName">the column name containing the unique id's</param>
    ''' <param name="IDColValue">The unique row id</param>
    ''' <param name="colToUpdate">column name to update</param>
    ''' <param name="newValue">new value for row in the columnToUpdate</param>
    ''' <param name="errors">Returns errors</param>
    ''' <returns></returns>
    Public Function UpdateSQLItem(ByVal idColName As String, ByVal IDColValue As String, ByVal colToUpdate As String, ByVal newValue As String, ByVal sqlTableName As String, Optional ByVal errors As String = "") As Boolean
        Dim success As Boolean = False
        Try
            colToUpdate = colToUpdate.Replace("@", "") 'removes any potential at signs from the column
            colToUpdate = colToUpdate.Replace(" ", "")
            colToUpdate = colToUpdate.Replace(";", "")
            'Creates sql command string, 
            Dim updateTableCMD As String =
                "Update " & sqlTableName & " SET " & colToUpdate & " = @colToUpdate" &
                " WHERE " & idColName & " = @idColValue"

            'opens and creates the sql command
            sqlConn = New SqlConnection(sqlConnStr)
            sqlConn.Open()
            Dim cmd As New SqlCommand(updateTableCMD, sqlConn)

            With cmd.Parameters
                .AddWithValue("@colToUpdate", newValue)
                .AddWithValue("@idColValue", IDColValue)
            End With


            cmd.ExecuteNonQuery()
            sqlConn.Close()




            success = True
        Catch ex As Exception
            errors &= "Exception message = " & ex.Message & vbCrLf & vbCrLf
        End Try
        Return success
    End Function




#End Region



End Class