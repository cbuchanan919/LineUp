Imports System.IO


''' <summary>
''' This contains the info for 1 project folder. ie X:\Print &amp; Ebook Projects\Books\A Crown For Elizabeth-Faith Murray-MiniBook-42081
''' </summary>
Public Class JQProjectDirInfo


#Region "Properties"

    Public Property ProjectDirectory As DirectoryInfo = Nothing
    Public Property ProjectType As ProductCategory = ProductCategory.Not_Set



#End Region

#Region "Init"

    Public Sub New(ByVal ProjectDirectory As DirectoryInfo, ByVal ProjectType As ProductCategory)
        Me.ProjectDirectory = ProjectDirectory
        Me.ProjectType = ProjectType
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' returns the files in the directory that have (body/cover, final) and doesn't have (archive, links) in the name / full name
    ''' </summary>
    ''' <param name="BodyorCover"></param>
    ''' <returns></returns>
    Public Function FindFiles(ByVal BodyorCover As BodyVsCover) As List(Of FileInfo)
        Dim foundFiles As New List(Of FileInfo)
        If Not IsNothing(ProjectDirectory) Then
            Dim fileList As List(Of FileInfo) = ProjectDirectory.GetFiles("*.indd", SearchOption.AllDirectories).ToList
            fileList.AddRange(ProjectDirectory.GetFiles("*.psd", SearchOption.AllDirectories))

            For Each myFile As FileInfo In fileList

                Dim name As String = myFile.Name.ToLower
                Dim fullname As String = myFile.FullName.ToLower

                If fullname.Contains(BodyorCover.ToString.ToLower) And name.Contains("final") Then
                    If Not fullname.Contains("archive") And Not myFile.DirectoryName.ToLower.Contains("links") Then
                        foundFiles.Add(myFile)
                    End If
                End If

            Next
        End If
        Return foundFiles
    End Function
#End Region
End Class
