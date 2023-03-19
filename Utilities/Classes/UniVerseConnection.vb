Imports U2.Data.Client
Imports U2.Data.Client.UO
'Imports eContentConversion




Public Class UniVerseConnection


#Region "Properties"







    Private Property UVcon As U2Connection
    Private Property UV1Sess As UniSession
    Private Property UpdateFldSubr As UniSubroutine

    Public Property DS() As DataSet

    Private Property userID() As String

    Private Property pass() As String

    Private Property server() As String

    Private Property database() As String

    Private ReadOnly Property uvConnStr As String
        Get
            Dim ConnStr As New U2ConnectionStringBuilder


            ConnStr.UserID = userID
            ConnStr.Password = pass
            ConnStr.Server = server
            ConnStr.Database = database
            ConnStr.ServerType = "UNIVERSE"
            ConnStr.Pooling = False
            ConnStr.AccessMode = "Native"
            ConnStr.RpcServiceType = "uvcs"
            Return ConnStr.ToString()
        End Get
    End Property

#End Region


#Region "Init"

    Public Sub New(ByVal userID As String, ByVal password As String, Optional ByVal server As String = "BTP-UV", Optional ByVal database As String = "BTP")

        Me.userID = userID
        pass = password
        Me.server = server
        Me.database = database


    End Sub

#End Region


#Region "Methods"




    Public Function UVAccess(ByVal sqlQuery As String) As Boolean





        Dim I As Integer = 0
        Dim success As Boolean = True
        Try



            'connect & open
            UVcon = New U2Connection(uvConnStr)
            UVcon.Open()
            Dim SqlSess As UniSession = UVcon.UniSession

            'defining objects

            Dim UVD As New UVData
            DS = New DataSet
            Dim SqlSubr As UniSubroutine = SqlSess.CreateUniSubroutine("*EXECSQL", 7)

            'execute Subr

            'Dim selectResult As Object = UVD.EXECSQL(SqlSubr, sqlQuery)
            Dim rowCt As Integer = 0
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If UVData.SQLEXEC(SqlSubr, sqlQuery, rowCt, msg, dt) Then
                DS.Tables.Add(dt)
                success = True
                UVcon.Close()

            Else
                UVcon.Close()
                Throw New ArgumentException("Query Failed: " & vbCrLf & msg)
            End If

            UVcon.Close()
            UVcon.Dispose()
            'If TypeOf selectResult Is DataTable Then
            '    DS.Tables.Add(selectResult)
            '    success = True
            'Else

            'End If



            'UVcon.ConnectionString = uvConnStr


            'Dim quote As String = """"



            '  SqlStatement = "Select BOOK, TITLE, TASKS, DOERS, " & TkString.Quoted("START.DATES") & ", " & TkString.Quoted("DONE.DATES") &
            '" FROM BOOKS WHERE BOOK=11996;"
            'SqlStatement = "SELECT ITEM, TITLE, AUTHOR FROM PRODUCTS WHERE TITLE Like God*"
            'SqlStatement = "Select ITEM, TITLE, AUTHOR FROM PRODUCTS WHERE TITLE Like 'We%'"

            'SqlStatement = "SELECT ITEM, TITLE, AUTHOR FROM PRODUCTS WHERE ITEM=1005"

            'SqlStatement = "SELECT " & quoted("CUST.ID3") & ", EMAIL FROM " & quoted("MX.INVOICES") & " WHERE INVOICE=150000" '& quoted("MX.INVOICES.ID") & 

            'SqlStatement = "SELECT ID3 FROM CUSTOMERS WHERE " & quoted("CUST.ID") & "=3932"

            ' SqlStatement = "SELECT NAME FROM CUSTOMERS WHERE " & quoted("CUST.ID") & "=44018"

            'SqlStatement = "SELECT TOTALS FROM " & quoted("MX.INVOICES") & " WHERE INVOICE=150409"



            'Dim da As U2DataAdapter = New U2DataAdapter(sqlStatement, UVcon)
            'da.Fill(DS)
            'da.Dispose()






        Catch ex As Exception

            'MsgBox("Exception: " & ex.Message, MsgBoxStyle.OkOnly, "TestUVAccess")
            Console.WriteLine("Exception: " & ex.Message & "Test_UVAccess")
            success = False
        End Try
        Return success
    End Function

    Public Function quoted(ByVal stringToQuote As String) As String
        Dim quote As String = """"
        Return quote & stringToQuote & quote


    End Function

    Public Function quoted(ByVal stringsToQuote() As String) As String
        Dim quote As String = """"
        Dim sb As New Text.StringBuilder
        For i As Integer = 0 To stringsToQuote.Length - 1
            sb.Append(quote & stringsToQuote(i) & quote)

            If i < stringsToQuote.Length - 1 Then
                sb.Append(", ")
            End If
        Next
        Return sb.ToString
    End Function


    ''' <summary>
    ''' updates the uv product with the pic count
    ''' </summary>
    ''' <param name="ProdID"></param>
    ''' <param name="PicCnt"></param>
    Public Sub updatePicCount(ByVal ProdID As Integer, ByVal PicCnt As Integer)

        UVcon = New U2Connection
        UVcon.ConnectionString = uvConnStr
        UVcon.Open()
        If PicCnt > 99 Then PicCnt = 99
        If PicCnt < 0 Then PicCnt = 0

        UV1Sess = UVcon.UniSession
        UpdateFldSubr = UV1Sess.CreateUniSubroutine("*UPDATE.UV.FIELD", 5)

        UpdateFldSubr.SetArg(0, "")
        UpdateFldSubr.SetArg(1, "PRODUCTS")
        UpdateFldSubr.SetArg(2, $"{ProdID.ToString}")
        UpdateFldSubr.SetArg(3, $"50")                 '50 is the field nbr of the PIC field in the PRODUCTS file
        UpdateFldSubr.SetArg(4, $"{PicCnt.ToString}")  'Cnt of Png files.  If Pic Cnt > 99 then "99"

        UpdateFldSubr.Call() : Debug.WriteLine("UpdateFld Rtn Msg=" & UpdateFldSubr.GetArg(0))               '--- Write the UV Record

        If UpdateFldSubr.GetArg(0).IndexOf("Error") >= 0 Then Throw New ArgumentException("UpdUvFld - " & UpdateFldSubr.GetArg(0))
        UVcon.Close()

    End Sub
#End Region


End Class
