

''' <summary>
''' stores the information for 1 email log
''' </summary>
Public Class EmailLogInfo


#Region "Properties"

    Public Property ID As Integer = cNullInt

    Public Property TimeSent As Date = cNullDate

    Public Property ComputerName As String = ""

    Public Property UserName As String = ""

    Public Property Subject As String = ""

    Public Property Contents As String = ""

    Public Property EmailType As TypeOfEmail = TypeOfEmail.LineupLog

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("ID:".PadRight(15) & vbTab & ID)
        sb.AppendLine("Time Sent:".PadRight(15) & vbTab & TimeSent)
        sb.AppendLine("Computer Name:".PadRight(15) & vbTab & ComputerName)
        sb.AppendLine("User Name:".PadRight(15) & vbTab & UserName)
        sb.AppendLine("Subject:".PadRight(15) & vbTab & Subject)
        sb.AppendLine("Email Type:".PadRight(15) & vbTab & EmailType.ToString())
        sb.AppendLine("Xml Contents: ".PadRight(15))
        sb.AppendLine()
        sb.AppendLine(Contents)
        Return sb.ToString
        'Return MyBase.ToString()
    End Function
    Public Enum TypeOfEmail
        LineupLog
        ProofRequestErrorLog
        JobStatusUpdate
    End Enum
#End Region


#Region "Init"
    ''' <summary>
    ''' New email log
    ''' </summary>
    ''' <param name="popInfo">If true, populates the username and computername</param>
    Public Sub New(Optional ByVal popInfo As Boolean = False, Optional ByVal EmailType As TypeOfEmail = TypeOfEmail.LineupLog)
        If popInfo Then
            ComputerName = Environment.MachineName
            UserName = Environment.UserName
        End If
        Me.EmailType = EmailType
    End Sub
#End Region


End Class