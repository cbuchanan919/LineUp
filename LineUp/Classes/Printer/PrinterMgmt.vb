Imports System.ComponentModel
Imports System.Data.SqlClient

''' <summary>
''' stores the list of printers, and manages them...
''' </summary>
Public Class PrinterMgmt


#Region "Properties"


    ''' <summary>
    ''' used to keep track of your original default printer
    ''' </summary>
    ''' <returns></returns>
    Public Property OriginalPrinter() As String = ""

    ''' <summary>
    ''' keeps track of whether or not the default printer was changed by this instance of lineup.
    ''' </summary>
    ''' <returns></returns>
    Private Property DefaultPrinterChanged() As Boolean = False


    'For setting the default printer
    Private Const ERROR_FILE_NOT_FOUND As Int32 = 2
    Private Const ERROR_INSUFFICIENT_BUFFER As Int32 = 122
    Private Declare Auto Function GetDefaultPrinter Lib "winspool.drv" (ByVal pszBuffer As String, ByRef pcchBuffer As Int32) As Boolean
    Private Declare Auto Function SetDefaultPrinter_API Lib "winspool.drv" Alias "SetDefaultPrinter" (ByVal pszPrinter As String) As Boolean

    Private Property SqlConnStr As String = ""
    Public Property Printers As List(Of PrinterInfo)

    Private Property TempPrinter As PrinterInfo



#End Region


#Region "Init"

    Public Sub New(ByVal SqlConnStr As String)
        Me.SqlConnStr = SqlConnStr
        TempPrinter = New PrinterInfo(SqlConnStr)

    End Sub

#End Region


#Region "Methods"

    Public Sub InstallAllPrinters()

        Dim ct As Integer = 0
        For Each p As PrinterInfo In Printers
            ct += p.Queues.Count
        Next



        If MsgBox("Are you sure you wish to install all " & ct & " printer(s)?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            For Each p As PrinterInfo In Printers
                For Each q As PrinterQueueInfo In p.Queues
                    q.TryToInstallPrinter()
                Next
            Next
        End If

    End Sub

    ''' <summary>
    ''' updates the printer list
    ''' </summary>
    Public Sub updatePrinters()
        Printers = TempPrinter.getPrinters
    End Sub




    ''' <summary>
    ''' Sets the default printer
    ''' </summary>
    ''' <param name="PrinterName">The printer path string</param>
    Public Sub SetDefaultPrinter(ByVal PrinterName As String)
        If SetDefaultPrinter_API(PrinterName) Then
            'printer set ok.
            DefaultPrinterChanged = True
        Else
            Dim err As New Win32Exception
            Try
                'this section tries to install printer
                Process.Start(PrinterName)
                Threading.Thread.Sleep(1000)
            Catch ex2 As Exception

            End Try
            MsgBox(err.Message & vbCrLf & "(" & PrinterName & ")") 'New Win32Exception
        End If

    End Sub


    ''' <summary>
    ''' Looks through the printers by category. If found, sets it to default and returns true
    ''' </summary>
    ''' <param name="category"></param>
    ''' <returns></returns>
    Public Function SetDefaultPrinterByCategory(ByVal category As PrinterCategory) As Boolean
        Dim result As String = GetPrinterByCategory(category)

        If result <> "" Then
            SetDefaultPrinter(result)
            Return True
        Else
            'no match found
            Return False
        End If

    End Function

    ''' <summary>
    ''' Returns the first printer path from queue that matches the category
    ''' </summary>
    ''' <param name="category"></param>
    ''' <returns></returns>
    Public Function GetPrinterByCategory(ByVal category As PrinterCategory) As String

        For Each p As PrinterInfo In Printers
            For Each q As PrinterQueueInfo In p.Queues
                If q.QueueCategory = category Then
                    Return q.QueuePath
                End If
            Next
        Next
        Return ""
    End Function

    ''' <summary>
    ''' Returns the default printer path
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDefaultPrinter() As String
        Dim s As String = Space(128)
        Dim n As Int32 = s.Length
        Dim Success As Boolean = GetDefaultPrinter(s, n)
        If Success Then

            Return Strings.Left(s, n - 1)
        Else
            Dim LastError As Integer = Runtime.InteropServices.Marshal.GetLastWin32Error()
            If LastError = ERROR_FILE_NOT_FOUND Then
                Throw New Win32Exception(LastError, "There is no default printer.")
            ElseIf LastError = ERROR_INSUFFICIENT_BUFFER Then
                s = Space(n)
                Success = GetDefaultPrinter(s, n)
                If Success Then
                    Return Strings.Left(s, n - 1)
                Else
                    Throw New Win32Exception
                End If
            Else
                Throw New Win32Exception
            End If
        End If
    End Function

    ''' <summary>
    ''' if the original printer has been changed, it will reset to the original printer. (specified in sql)
    ''' </summary>
    Public Sub ResetToOriginalPrinter()
        'goes through each printer q to check if there's a master default
        For Each p As PrinterInfo In Printers 'goes through all printers
            For Each q As PrinterQueueInfo In p.Queues 'goes through each queue

                If q.QueueCategory = PrinterCategory.Default_Printer Then 'it's a default printer
                    If q.QueueDescription.ToLower.Contains(My.Computer.Name.ToLower) Then 'it's a computer match
                        If q.QueuePath <> "" Then
                            'there's a q path
                            DefaultPrinterChanged = True
                            OriginalPrinter = q.QueuePath
                        End If

                    End If
                End If
            Next
        Next

        If DefaultPrinterChanged Then
            ' on form close, restores the original default printer
            SetDefaultPrinter(OriginalPrinter)
        End If
    End Sub



#End Region


End Class