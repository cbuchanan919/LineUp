Imports System.IO
Imports Utilities
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions

''' <summary>
''' Class to interact with Microsoft Word
''' </summary>
Public Class WordConnectionIO


#Region "Methods"


    ''' <summary>
    ''' Closes any running Word App, and then creates new instance of word. Exports pdf from word.
    ''' </summary>
    ''' <param name="WordDocPath">The file path of the document to export</param>
    ''' <param name="PDFPath">The file path of the new pdf</param>
    Public Function ExportWordToPDF(ByVal WordDocPath As String, ByVal PDFPath As String) As Boolean
        Dim success As Boolean = False
        Try
            If Utilities.GenUtil.IsProgramRunning("winword") Then
                GenUtil.KillProgram("winword")
            End If
            Dim wordApp As New Word.Application
            Dim wordDoc As Word.Document = wordApp.Documents.Open(FileName:=WordDocPath, ReadOnly:=True, Visible:=False)
            If File.Exists(PDFPath) Then
                File.Delete(PDFPath)
            End If
            wordDoc.ExportAsFixedFormat(PDFPath, Word.WdExportFormat.wdExportFormatPDF)
            wordDoc.Close(SaveChanges:=False)
            wordApp.Quit()
            success = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Exporting PDF From Word")
            If Utilities.GenUtil.IsProgramRunning("winword") Then
                GenUtil.KillProgram("winword")
            End If
        End Try

        Return success
    End Function


    ''' <summary>
    ''' creates a sub folder in folderPath and adds the files to that.
    ''' </summary>
    ''' <param name="availableProds"></param>
    ''' <param name="folderPath">folder path with word docs to search in.</param>
    Public Sub MatchDocsToProducts(ByVal availableProds As List(Of UvProductInfo), ByVal folderPath As String, ByVal bar As ToolStripProgressBar)

        '-----used to keep track of non matches...---------
        'writes list to file
        Dim noMatchesListFP As String = Path.Combine(My.Settings.dirResources, "unmatchedFiles.txt")
        Dim noMatchesList As New List(Of String)
        If File.Exists(noMatchesListFP) Then
            noMatchesList.AddRange(IO.File.ReadAllLines(noMatchesListFP).ToList)
        End If
        '--------------------------------------------------

        Dim shareWordItems As New List(Of UvProductInfo)
        For Each prod As UvProductInfo In availableProds
            If prod.SupID = cShareWordSupID Then
                'filters by share word supplier id.
                shareWordItems.Add(prod.clone)
            End If
        Next
        Dim rowCount As Integer = shareWordItems.Count 'share word items
        Dim matchCount As Integer = 0

        If shareWordItems.Count < 25 Then
            MsgBox("Please try again in a minute. Still loading...", MsgBoxStyle.Information)
            Exit Sub
        End If
        If Directory.Exists(folderPath) Then
            Try
                Dim dir As New DirectoryInfo(folderPath)
                Dim files As List(Of FileInfo) = dir.GetFiles.ToList
                bar.Maximum = files.Count
                bar.Minimum = 0
                bar.Value = 0
                bar.Step = 1

                If Utilities.GenUtil.IsProgramRunning("winword") Then
                    GenUtil.KillProgram("winword")
                End If
                Dim io As New ShareWordIO(folderPath)
                For Each myFile As FileInfo In files
                    'goes through each file, and tries to find a match in the data table.

                    If noMatchesList.Contains(myFile.FullName.ToLower) Then
                        'ignore file. already scanned
                        Beep()
                    Else

                        Dim matches As List(Of UvProductInfo) = FindShareWordMatch(myFile, shareWordItems)

                        If matches.Count = 0 Then
                            noMatchesList.Add(myFile.FullName.ToLower)
                            File.AppendAllText(noMatchesListFP, myFile.FullName.ToLower & vbCrLf)
                        Else
                            'matches found, move shareword
                            io.moveShareWordFile(myFile, matches)
                        End If




                    End If

                    bar.PerformStep()
                    bar.ToolTipText = "Step " & bar.Value & " out of " & bar.Maximum
                    Application.DoEvents()


                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


        End If
        bar.Value = bar.Maximum
        MsgBox("rowCount" & " = " & rowCount & vbCrLf & "MatchCount = " & matchCount)
    End Sub

    ''' <summary>
    ''' looks in the file for a share word match. returns list of uvProductInfo matches if found. Otherwise returns empty list if no matches found. 
    ''' </summary>
    ''' <param name="fileToSearch">word doc to search inside of</param>
    ''' <param name="shareWordItems">list of loaded share word prods</param>
    ''' <param name="killWordFirst">Forcefully kills any running word apps</param>
    ''' <returns></returns>
    Public Function FindShareWordMatch(ByVal fileToSearch As FileInfo,
                                        ByVal shareWordItems As List(Of UvProductInfo),
                                        Optional ByVal killWordFirst As Boolean = False) As List(Of UvProductInfo)
        Dim shareMatches As New List(Of UvProductInfo)

        If File.Exists(fileToSearch.FullName) Then
            If cWordOkExtensions.Contains(fileToSearch.Extension.ToLower) Then
                'word can open file... maybe...
                If killWordFirst Then
                    If Utilities.GenUtil.IsProgramRunning("winword") Then
                        GenUtil.KillProgram("winword")
                    End If
                End If
                Dim WordApp As New Word.Application
                Dim WordDoc As Word.Document = Nothing
                Try
                    WordDoc = New Word.Document
                    WordDoc = WordApp.Documents.Open(FileName:=fileToSearch.FullName, ReadOnly:=True, Visible:=False)
                    Dim itemFound As Boolean = False

                    For Each shareWord As UvProductInfo In shareWordItems
                        itemFound = shareWord.ItemNum > 0 AndAlso WordDoc.Content.Find.Execute(shareWord.ItemNum, MatchWholeWord:=True)
                        If itemFound Then
                            'word found the shareword item number in the document.
                            shareMatches.Add(shareWord.clone) 'adds the share word match.

                        End If
                    Next
                    Application.DoEvents()

                    WordDoc.Close(SaveChanges:=False)
                    WordDoc = Nothing
                    WordApp.Quit()
                Catch ex As Exception
                    MsgBox(ex.Message & vbCrLf & vbCrLf & "(" & fileToSearch.FullName & ")")
                    Try
                        WordDoc.Close()
                        WordApp.Quit()
                    Catch ex2 As Exception
                        MsgBox(ex2)
                    End Try

                End Try
            End If

        End If

        Return shareMatches

    End Function



#End Region


End Class
