
''' <summary>
''' Simple class to keep track of load times. 
''' </summary>
Public Class TimerCB
    Private tmrTitle As String = ""
    Private timeStart As Date
    Private timeEnd As Date
    Private timeDiff As TimeSpan
    Private times As New Text.StringBuilder
    Private curStep As Integer = 1

    ''' <summary>
    ''' Initalizes / starts the timer.
    ''' </summary>
    ''' <param name="timerName"></param>
    Public Sub New(ByVal timerName As String)
        tmrTitle = timerName
        resetStart()
        addCurrentTime()
    End Sub

    ''' <summary>
    ''' Adds current time span to string. Call ToString to get list of times.
    ''' </summary>
    Public Sub addCurrentTime(Optional ByVal stepName As String = "")
        timeEnd = Now
        timeDiff = timeEnd - timeStart
        If stepName <> "" Then
            stepName = ": " & stepName
        End If
        times.AppendLine(timeDiff.ToString & " - Step " & curStep & stepName)

        curStep += 1
    End Sub

    ''' <summary>
    ''' resets the start timer.
    ''' </summary>
    Public Sub resetStart()
        timeStart = Now
        times.Clear()
        curStep = 1
    End Sub

    ''' <summary>
    ''' Returns list of times 'addCurrentTime' was called. (also includes any time ToString method was called)
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function ToString() As String
        addCurrentTime()
        Return tmrTitle & vbCrLf & vbCrLf & times.ToString
    End Function
    Public ReadOnly Property GetCurrentDuration As TimeSpan
        Get
            Return Now - timeStart
        End Get
    End Property



End Class