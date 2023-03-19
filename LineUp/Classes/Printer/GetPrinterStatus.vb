''' <summary>
''' This class can get the current list of printers, and their status's
''' </summary>
Public Class GetPrinterStatus


#Region "Properties"
    Private Enum PrinterStatus
        Other = 1
        Unknown = 2
        Idle = 3
        Printing = 4
        WarmingUp = 5
        StoppedPrinting = 6
        Offline = 7
        Paused = 8
        pError = 9
        Busy = 10
        NotAvailable = 11
        Waiting = 12
        Processing = 13
        Initialization = 14
        PowerSave = 15
        PendingDeletion = 16
        IOActive = 17
        ManualFeed = 18


        ' For more states see WMI docs.
    End Enum

#End Region


#Region "Init"

    Public Sub New()

    End Sub

#End Region


#Region "Methods"

    Private Function PrinterStatusToString(ByVal ps As PrinterStatus) As String
        Dim s As String
        Select Case ps
            Case PrinterStatus.Other
                s = "Other"
            Case PrinterStatus.Unknown
                s = "Unknown"
            Case PrinterStatus.Idle
                s = "waiting (idle)"
            Case PrinterStatus.Printing
                s = "printing"
            Case PrinterStatus.WarmingUp
                s = "warming up"
            Case PrinterStatus.StoppedPrinting
                s = "Stopped Printing"
            Case PrinterStatus.Offline
                s = "Offline"
            Case PrinterStatus.Paused
                s = "Paused"
            Case PrinterStatus.pError
                s = "Error"
            Case PrinterStatus.Busy
                s = "Busy"
            Case PrinterStatus.NotAvailable
                s = "Not Available"
            Case PrinterStatus.Waiting
                s = "Waiting"
            Case PrinterStatus.Processing
                s = "Processing"
            Case PrinterStatus.Initialization
                s = "Initialization"
            Case PrinterStatus.PowerSave
                s = "Power Save"
            Case PrinterStatus.PendingDeletion
                s = "Pending Deletion"
            Case PrinterStatus.IOActive
                s = "I/O Active"
            Case PrinterStatus.ManualFeed
                s = "Manual Feed"
            Case Else ' there may be more cases
                s = "unknown state"
        End Select
        PrinterStatusToString = s
    End Function


    ''' <summary>
    ''' Returns list of printer status's
    ''' </summary>
    Public Sub popP()
        Dim strPrintServer As String = "localhost"
        Dim WMIObject As String = "winmgmts://" & strPrintServer
        Dim PrinterSet As Object = GetObject(WMIObject).InstancesOf("win32_Printer")
        Dim printDict As New SortedDictionary(Of String, Object)

        Dim sb As New Text.StringBuilder
        For Each p As Object In PrinterSet
            If Not printDict.ContainsKey(p.Name) Then
                printDict.Add(p.Name, p)
            End If
        Next

        For Each pName As String In printDict.Keys
            sb.AppendLine(printDict(pName).Name & ": " & PrinterStatusToString(printDict(pName).PrinterStatus))
        Next
        MsgBox(sb.ToString)
    End Sub

#End Region


End Class
