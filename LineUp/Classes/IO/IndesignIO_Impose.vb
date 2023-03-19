Imports System.IO
Public Class IndesignIO_Impose


#Region "Init"

    Public Sub New()

    End Sub

#End Region


#Region "Methods"

    Public Function ImposeHomePrintTracts(ByVal curTractInfo As TractInfo) As Boolean
        Dim success As Boolean = False
        Try
            If IO.File.Exists(curTractInfo.TractFilePath) Then
                Dim fileInfo As New IO.FileInfo(curTractInfo.TractFilePath)
                fileInfo.IsReadOnly = False
                Dim idApp As InDesign.Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)
                Dim ReturnData As String = ""
                Dim lstReturnData As New List(Of String)
                ReturnData = idApp.DoScript(IO.Path.Combine(My.Settings.dirInddScripts, "Export Printable Tracts.jsx"), InDesign.idScriptLanguage.idJavascript, curTractInfo.ArgsList)
                If ReturnData <> "" Then
                    MsgBox(ReturnData)
                End If
            Else
                Throw (New Exception("File doesn't exist: " & vbCrLf & curTractInfo.TractFilePath))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return success

    End Function

    ''' <summary>
    ''' Processes a book cover. Selected from a dialog box.
    ''' </summary>
    ''' <returns></returns>
    Public Function ImposeABookCover(Optional ByVal FolderPath As String = "", Optional ByVal rowInfo As JQRowInfo = Nothing) As Boolean
        If FolderPath = "" Then FolderPath = My.Settings.PrEBookDir
        Dim openFi As New OpenFileDialog()
        openFi.Filter = "Indesign Files|*.indd;*.indt;*.idml|All Files|*.*"
        openFi.InitialDirectory = FolderPath
        If openFi.ShowDialog = DialogResult.OK Then
            Dim fi As New FileInfo(openFi.FileName)
            Dim defaultResponse As String = fi.Directory.Name
            If Not IsNothing(rowInfo) Then defaultResponse = rowInfo.ItemNumber 'sets the number automagically

            Dim result As String = "Blank"
            Do Until result = "" Or IsNumeric(result)
                result = InputBox("What's the item number for " & openFi.FileName + "?", "Item Number Entry", defaultResponse)
            Loop

            If result <> "" Then
                Try
                    Dim prod As UvProductInfo = LineUp.MyUvProductInfoIO.findProduct(result).clone
                    If MsgBox("Do you want to create a book cover for " & prod.ItemNum & "?" & vbCrLf & vbCrLf & prod.Title & vbCrLf & vbCrLf & "(" & openFi.FileName & ")", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        prod.FoundCoverFiles.Add(New FileInfo(openFi.FileName))
                        Return ImposeProductCovers({prod}.ToList)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If


        End If
        Return False
    End Function

    ''' <summary>
    ''' Creates a cover for the specified CoverFile.
    ''' </summary>
    ''' <param name="CoverFile"></param>
    ''' <param name="ItemNumber"></param>
    ''' <param name="ProdCategory">Currently accepts Book, Pamphlet, Mini-Pamphlet</param>
    ''' <returns></returns>
    Public Function ImposeProductCover(ByVal CoverFile As FileInfo, ByVal ItemNumber As Integer, ByVal ProdCategory As ProductCategory, ByVal Title As String) As Boolean 'ByVal jobs As List(Of JQRowInfo)) As Boolean
        Dim success As Boolean = False
        Try

            If CoverFile.Exists Then
                Dim args As New List(Of String)
                Dim fp As String = CoverFile.FullName.ToLower
                fp = fp.Replace("\\btp-fs\shared\", "x:\")
                If IO.File.Exists(fp) Then
                    fp = Utilities.GenUtil.ConvertForJavaScript(fp)
                    Dim CoverType As String = ProdCategory.ToString
                    'If ProdCategory = ProductCategory.Mini_Pamphlet Then CoverType = "Mini_" & CoverType
                    CoverType &= "_Cover"
                    args.Add(fp & "?" & ItemNumber & "?" & CoverType & "?" & Title.Replace("?", "")) '"bookcover")



                End If
                Dim idApp As InDesign.Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)
                Dim ReturnData As String = ""
                Dim lstReturnData As New List(Of String)
                ReturnData = idApp.DoScript(IO.Path.Combine(My.Settings.dirInddScripts, "Export Product Covers.jsx"), InDesign.idScriptLanguage.idJavascript, args.ToArray)
                If ReturnData <> "" Then
                    'MsgBox(ReturnData)
                End If
                success = True
            Else
                Throw New Exception("File doesn't exist!" & vbCrLf & CoverFile.FullName)
            End If



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Impose Product Covers")
            success = False
        End Try
        Return success
    End Function



    ''' <summary>
    ''' Processes the list of products. - DEPRECIATED!
    ''' </summary>
    ''' <param name="prods">Each product needs to have a cover file referenced, and an item number</param>
    ''' <returns></returns>
    Public Function ImposeProductCovers(ByVal prods As List(Of UvProductInfo)) As Boolean
        Dim success As Boolean = False
        Try
            Dim args As New List(Of String)
            For Each prod As UvProductInfo In prods
                If prod.FoundCoverFiles.Count = 1 Then
                    Dim fp As String = prod.FoundCoverFiles(0).FullName.ToLower
                    fp = fp.Replace("\\btp-fs\shared\", "x:\")
                    If IO.File.Exists(fp) Then
                        fp = Utilities.GenUtil.ConvertForJavaScript(fp)
                        args.Add(fp & "?" & prod.ItemNum & "?" & PrinterCategory.Book_Cover.ToString)
                    End If
                End If
            Next
            Dim idApp As InDesign.Application = CType(Activator.CreateInstance(Type.GetTypeFromProgID("InDesign.Application"), True), InDesign.Application)
            Dim ReturnData As String = ""
            Dim lstReturnData As New List(Of String)
            ReturnData = idApp.DoScript(IO.Path.Combine(My.Settings.dirInddScripts, "Export Product Covers.jsx"), InDesign.idScriptLanguage.idJavascript, args.ToArray)
            If ReturnData <> "" Then
                'MsgBox(ReturnData)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Impose Product Covers")
            success = False
        End Try
        Return success
    End Function

#End Region


End Class

