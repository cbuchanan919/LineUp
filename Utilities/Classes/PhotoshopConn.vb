Imports System.IO
Public Class PhotoshopConn

#Region "Declarations"

    Private psApp As Photoshop.Application = Nothing
    Private scriptArgs As New List(Of String)
    Private scriptPath As String = ""
    Private scriptArgArray As Object() = Nothing
    Private isInit As Boolean = False

    Private Const cError As String = "Error"

    Private _errors As New List(Of String)
    ''' <summary>
    ''' contains any errors that havent been cleared out.
    ''' </summary>
    ''' <returns></returns>
    Public Property errors() As List(Of String)
        Get
            Return _errors
        End Get
        Set(ByVal value As List(Of String))
            _errors = value
        End Set
    End Property


    Private _returnData As String
    ''' <summary>
    ''' contains the data that photoshop returned
    ''' </summary>
    ''' <returns></returns>
    Private Property returnData() As String
        Get
            Return _returnData
        End Get
        Set(ByVal value As String)
            _returnData = value
        End Set
    End Property

    Public ReadOnly Property psAppIsInit As Boolean
        Get
            If IsNothing(psApp) Then
                Return False 'not initalized
            Else
                Return True ' it's not nothing
            End If
        End Get
    End Property

#End Region

#Region "Init"


    Public Sub New(ByVal javaScriptFilePath As String)
        Try
            'MsgBox(javaScriptFilePath)
            psApp = CType(Activator.CreateInstance(Type.GetTypeFromProgID("Photoshop.Application")), Photoshop.Application)
            scriptPath = javaScriptFilePath
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Runs the photoshop script to load a file (closes any open photoshop docs)
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    Public Function loadPsFile(ByVal filePath As String) As Boolean
        Dim success As Boolean = True
        Try
            If File.Exists(filePath) Then
                scriptArgs.Clear()
                scriptArgs.Add("init")
                scriptArgs.Add(convertPathForJavascript(filePath))
                scriptArgArray = scriptArgs.Cast(Of Object)().ToArray()
                returnData = psApp.DoJavaScriptFile(scriptPath, scriptArgArray)
            Else
                success = False
            End If
            If returnData.Contains(cError) Then
                success = False
                errors.Add(returnData)
            End If
        Catch ex As Exception
            success = False
            errors.Add("loadPsFile" & " - " & ex.Message)
        End Try
        Return success
    End Function

    ''' <summary>
    ''' resizes the ps image. 
    ''' </summary>
    ''' <param name="size">width in pixels. Large = 1000, Medium = 320, small = 120</param>
    ''' <param name="useBicubicSharper">true if reducing size. otherwise false</param>
    ''' <returns></returns>
    Public Function resizePsFile(ByVal size As Integer, ByVal useBicubicSharper As Boolean) As Boolean
        Dim success As Boolean = True

        Try
            scriptArgs.Clear()
            scriptArgs.Add("resize")
            scriptArgs.Add(size.ToString)
            scriptArgs.Add(useBicubicSharper.ToString.ToLower)
            scriptArgArray = scriptArgs.Cast(Of Object)().ToArray()
            returnData = psApp.DoJavaScriptFile(scriptPath, scriptArgArray)
            If returnData.Contains(cError) Then
                success = False
                errors.Add(returnData)
            End If
        Catch ex As Exception
            errors.Add("loadPsFile" & " - " & ex.Message)
            success = False
        End Try

        Return success
    End Function

    ''' <summary>
    ''' saves the image as a png to the specified location.
    ''' </summary>
    ''' <param name="savePath"></param>
    ''' <returns></returns>
    Public Function saveForWeb(ByVal savePath As String) As Boolean
        Dim success As Boolean = True
        Try
            scriptArgs.Clear()
            scriptArgs.Add("saveForWeb")
            scriptArgs.Add(convertPathForJavascript(savePath))
            scriptArgArray = scriptArgs.Cast(Of Object)().ToArray()
            returnData = psApp.DoJavaScriptFile(scriptPath, scriptArgArray)
            If returnData.Contains(cError) Then
                success = False
                errors.Add(returnData)
            End If
        Catch ex As Exception
            success = False
            errors.Add("Save For Web" & " - " & ex.Message)
        End Try

        Return success
    End Function

    ''' <summary>
    ''' saves the image as a jpg to the specified location.
    ''' </summary>
    ''' <param name="savePath"></param>
    ''' <returns></returns>
    Public Function saveJpgForWeb(ByVal savePath As String) As Boolean
        Dim success As Boolean = True
        Try
            scriptArgs.Clear()
            scriptArgs.Add("saveJpgForWeb")
            scriptArgs.Add(convertPathForJavascript(savePath))
            scriptArgArray = scriptArgs.Cast(Of Object)().ToArray()
            returnData = psApp.DoJavaScriptFile(scriptPath, scriptArgArray)
            If returnData.Contains(cError) Then
                success = False
                errors.Add(returnData)
            End If
        Catch ex As Exception
            success = False
            errors.Add("Save For Web" & " - " & ex.Message)
        End Try

        Return success
    End Function


    Public Function closePsFiles() As Boolean
        Dim success As Boolean = True
        Try

            scriptArgs.Clear()
            scriptArgs.Add("close")
            scriptArgArray = scriptArgs.Cast(Of Object)().ToArray()
            returnData = psApp.DoJavaScriptFile(scriptPath, scriptArgArray)

            If returnData.Contains(cError) Then
                success = False
                errors.Add(returnData)
            End If

        Catch ex As Exception
            success = False
            errors.Add("closePs" & " - " & ex.Message)
        End Try
        Return success
    End Function

    Public Sub alertOfError()
        Try
            MsgBox(errors(errors.Count - 1), MsgBoxStyle.Exclamation)
        Catch ex As Exception
        End Try
    End Sub



    Public Function convertPathForJavascript(ByVal strToConvert As String)
        Dim sb As New Text.StringBuilder
        For Each letter As String In strToConvert
            If letter = "\" Then
                sb.Append("\")
            End If
            sb.Append(letter)
        Next
        Return sb.ToString

    End Function

#End Region


End Class
