Imports System.IO


''' <summary>
''' Class with methods for finding the correct shareword folder, as well as other folder maintence thingys.
''' </summary>
Public Class ShareWordIO


#Region "Init"
    Public Sub New(ByVal ShareWordFolder As String)
        ShareWordDir = New DirectoryInfo(ShareWordFolder)

    End Sub

#End Region


#Region "Properties"

    ''' <summary>
    ''' the root share word projects directory. (directory that contains project folders)
    ''' </summary>
    Public Property ShareWordDir() As DirectoryInfo


#End Region


#Region "Methods"


    Public Function GetShareWordFolderByID(ByVal itemNumber As String) As DirectoryInfo
        For Each shareFolder As DirectoryInfo In ShareWordDir.GetDirectories()
            If shareFolder.Name.Contains(itemNumber) Then
                Dim titleParts() As String = shareFolder.Name.Split("-")
                For Each part As String In titleParts
                    part = part.Trim
                    If part = itemNumber Then
                        Return shareFolder
                    End If
                Next
            End If

        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' compares the string to folder title.
    ''' </summary>
    ''' <param name="SearchStr"></param>
    ''' <param name="percentTheSame">valid numbers are from 0.0 - 1.0 ie. .75, or .88, or .5</param>
    ''' <returns></returns>
    Public Function GetSimilarFoldersByTitle(ByVal SearchStr As String, ByVal percentTheSame As Single) As List(Of DirectoryInfo)
        If percentTheSame > 1 Then percentTheSame = percentTheSame / 100
        Dim similar As New List(Of DirectoryInfo)
        Try
            For Each shareFolder As DirectoryInfo In ShareWordDir.GetDirectories()
                Dim maxSimilar As Single = Utilities.TextStatistics.GetSimilarity(shareFolder.Name, SearchStr, 1000) 'gets overall match. (probably includes item number)
                Dim currentMatch As Single = 0
                Dim parts() As String = shareFolder.Name.Split("-")
                'folder name is probable either '42240 - Beautiful Snow' or 'Beautiful Snow - 42240'
                For Each part As String In parts
                    part = part.Trim
                    If Not IsNumeric(part) AndAlso part <> "" Then
                        currentMatch = Utilities.TextStatistics.GetSimilarity(part, SearchStr, 1000)
                        If currentMatch > maxSimilar Then
                            'if the individual part is more similar to the searchString, it makes it equal to the max.
                            maxSimilar = currentMatch
                        End If
                    End If
                Next
                If maxSimilar >= percentTheSame Then
                    similar.Add(shareFolder)
                End If


            Next
        Catch ex As Exception

        End Try
        Return similar
    End Function

    ''' <summary>
    ''' goes through each folder in the shareWordDir and if there are multiple files in that folder, moves the folder to the specified subfolder name.
    ''' </summary>
    ''' <param name="subFolderName"></param>
    Protected Friend Sub moveFoldersWithMultipleFiles(ByVal subFolderName As String)
        Dim subDir As String = Path.Combine(ShareWordDir.FullName, subFolderName)
        If Not Directory.Exists(subDir) Then
            Directory.CreateDirectory(subDir)
        End If
        For Each myDir As DirectoryInfo In ShareWordDir.GetDirectories
            Dim files As List(Of FileInfo) = myDir.GetFiles.ToList
            If files.Count > 1 Then
                Directory.Move(myDir.FullName, Path.Combine(subDir, myDir.Name))
            End If
        Next
    End Sub





    ''' <summary>
    ''' puts file in subdir based on share word info. If no match found, puts in a separate folder.
    ''' </summary>
    ''' <param name="fileToMove"></param>
    ''' <param name="shareWords"></param> 
    ''' <returns></returns>
    Public Function moveShareWordFile(ByVal fileToMove As FileInfo,
                                       ByVal shareWords As List(Of UvProductInfo)) As Boolean
        Dim success As Boolean = False
        Try
            If File.Exists(fileToMove.FullName) Then
                Dim dir As DirectoryInfo = fileToMove.Directory 'parent dir.
                Dim newDir As String = ""

                Select Case shareWords.Count
                    Case 0
                        'move file so won't be re-scanned if word fails.
                        newDir = Path.Combine(dir.FullName, "0 - No Match")
                        If Not Directory.Exists(newDir) Then
                            Directory.CreateDirectory(newDir)
                        End If
                        File.Move(fileToMove.FullName, Path.Combine(newDir, fileToMove.Name))
                        success = True

                    Case 1
                        'the file exists, and the share word is defined.
                        Dim matched As String = Path.Combine(dir.FullName, "Matched")
                        If Not Directory.Exists(matched) Then
                            Directory.CreateDirectory(matched)
                        End If

                        ' Dim io As New ShareWordIO(Path.Combine(dir.FullName, "Matched"))
                        Dim foundDir As DirectoryInfo = GetShareWordFolderByID(shareWords(0).ItemNum)
                        If IsNothing(foundDir) Then
                            'no match found. Create new folder
                            newDir = Path.Combine(dir.FullName, "Matched", shareWords(0).folderTitle)
                        Else
                            'match found. Use existing folder
                            newDir = foundDir.FullName
                        End If

                        If Not Directory.Exists(newDir) Then
                            Directory.CreateDirectory(newDir)
                        End If
                        File.Move(fileToMove.FullName, Path.Combine(newDir, fileToMove.Name))
                        success = True

                    Case Else
                        Dim folderName As New List(Of String)
                        For Each share As UvProductInfo In shareWords
                            folderName.Add(share.ItemNum)
                        Next
                        newDir = Path.Combine(dir.FullName, "0 - Multiple Matches", String.Join(", ", folderName))
                        If Not Directory.Exists(newDir) Then
                            Directory.CreateDirectory(newDir)
                        End If
                        File.Move(fileToMove.FullName, Path.Combine(newDir, fileToMove.Name))
                End Select

            Else
                Throw New Exception("File not found: " & fileToMove.FullName)
            End If




        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return success
    End Function


#End Region


End Class
