Imports System.Windows.Forms
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Settings


#Region "Properties"

    Private Property SqlConnStr As String = ""


    ''' <summary>
    ''' The location the settings file was last loaded from / saved to
    ''' </summary>
    ''' <returns></returns>
    Public Property XmlSettingsPath As String = ""
    ''' <summary>
    ''' contains a list of the errors since last cleared / initiated
    ''' </summary>
    ''' <returns></returns>
    Public Property Errors As New List(Of String)

    Public Property SqlTableName As String = ""

    Public Property MySettings As Configuration.SettingsPropertyValueCollection

    Public Property AllSettings As New List(Of Setting)

    Public Property MeXmlOrSql As XmlVsSql = XmlVsSql.NotSet

    Public Enum XmlVsSql
        NotSet
        SQL
        XML
    End Enum

    Private Property RemoveSettings As New List(Of Setting)
#End Region


#Region "Init"

    ''' <summary>
    ''' Settings Class. Auto reads file from specified path.
    ''' </summary>
    ''' <param name="xmlSettingsPath"></param>
    ''' <param name="requiredSettingsNames"></param>
    Public Sub New(ByVal xmlSettingsPath As String, Optional ByVal requiredSettingsNames As List(Of String) = Nothing)

        Me.XmlSettingsPath = xmlSettingsPath
        MeXmlOrSql = XmlVsSql.XML
        If Not File.Exists(Me.XmlSettingsPath) Then
            MsgBox("Settings file not found." & vbCrLf & """" & Me.XmlSettingsPath & """" & vbCrLf & "Please select settings file")
            Dim opnFile As New OpenFileDialog
            opnFile.InitialDirectory = "X:\"
            opnFile.Filter = "XML Config File|*.xml"
            opnFile.Title = "Select Config File"
            If opnFile.ShowDialog = Global.System.Windows.Forms.DialogResult.OK Then
                Me.XmlSettingsPath = opnFile.FileName
            End If
        End If
        ReadSettingsXml()
        If Not IsNothing(requiredSettingsNames) Then
            Dim sb As New Text.StringBuilder
            For Each requiredSetting As String In requiredSettingsNames
                If Not ContainsName(requiredSetting) Then
                    sb.AppendLine(requiredSetting)
                End If
            Next
            If sb.ToString <> "" Then
                Throw New Exception("The following required settings were not loaded:" & vbCrLf & sb.ToString & vbCrLf & vbCrLf & xmlSettingsPath)
            End If
        End If



    End Sub

    ''' <summary>
    ''' Automatically loads sql settings from table
    ''' </summary>
    ''' <param name="SqlConnStr"></param>
    ''' <param name="MySettings"></param>
    ''' <param name="sqlTableName"></param>
    Public Sub New(ByVal SqlConnStr As String, ByVal MySettings As Configuration.SettingsPropertyValueCollection, Optional ByVal SqlTableName As String = "AppSettings", Optional ByVal AutoLoadFromSql As Boolean = True)
        MeXmlOrSql = XmlVsSql.SQL
        Me.SqlConnStr = SqlConnStr
        Me.AllSettings = New List(Of Setting)
        Me.SqlTableName = SqlTableName
        Me.MySettings = MySettings
        'load sql setting here
        If AutoLoadFromSql Then
            LoadSettingsFromSQL()
        End If

    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Shows settings box. Returns true if settings are saved.
    ''' </summary>
    ''' <returns></returns>
    Public Function ShowSettings(Optional AdditionalTabs As List(Of TabPage) = Nothing) As Boolean
        Dim success As Boolean = False
        Try

            Dim italic As New Drawing.Font("Segoe UI", 9.75, Drawing.FontStyle.Italic)

            Dim tabs As New TabControl




            'alphalist is used to make more user friendly labels
            Dim Alpha As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim AlphaList As New List(Of String)
            For Each aChar In Alpha
                AlphaList.Add(aChar)
            Next


            tabs.Dock = DockStyle.Fill
            Dim sorted As SortedDictionary(Of String, List(Of Setting)) = getOrganizedSettings()
            For Each myKey As String In sorted.Keys

                Dim tab As New TabPage
                tab.BackColor = Drawing.Color.WhiteSmoke 'White
                tab.Text = SPACER & myKey & SPACER

                Dim flow As New FlowLayoutPanel
                flow.Dock = DockStyle.Fill
                flow.Parent = tab
                flow.AutoScroll = True
                For Each mySetting As Setting In sorted(myKey)

                    flow.Controls.Add(mySetting.SettingCtrl)

                Next


                tabs.TabPages.Add(tab)
            Next
            If Not IsNothing(AdditionalTabs) Then
                For Each tab As TabPage In AdditionalTabs
                    tabs.TabPages.Add(tab)
                Next
            End If

            Dim selectedPg As Integer = 0
            If tabs.TabCount > 0 Then
                For i As Integer = 0 To tabs.TabCount - 1
                    If tabs.TabPages(i).Text.ToLower.Contains("general") Then
                        selectedPg = i
                    End If
                Next
            End If
            tabs.SelectedIndex = selectedPg

            'creates a new settings form, and displays it. 
            Dim xmlSettings As New frmSettings
            tabs.Parent = xmlSettings.SplitContainer1.Panel1

            If xmlSettings.ShowDialog = DialogResult.OK Then
                For Each mySetting As Setting In AllSettings
                    'mySetting.SettingValue = mySetting.SettingValueTxt.Text.Trim
                    mySetting.UpdateSettingsFromGB()
                Next
                If MeXmlOrSql = XmlVsSql.XML Then
                    WriteSettingsXml()
                Else
                    UpdateMySettingValueFromAllSettings()
                    updateSqlFromAllSettings()
                End If

                success = True
            End If

        Catch ex As Exception
            success = False
            MsgBox(ex.Message)
        End Try
        Return success

    End Function

    Public Function ContainsName(ByVal settingName As String) As Boolean
        For Each mySetting As Setting In AllSettings
            If mySetting.SettingName = settingName Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function GetSetting(ByVal settingName As String) As Setting
        For Each mySetting As Setting In AllSettings
            If mySetting.SettingName = settingName Then
                Return mySetting
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' updates the setting 
    ''' </summary>
    ''' <param name="settingName"></param>
    ''' <param name="newValue"></param>
    Public Function SetSetting(ByVal settingName As String, ByVal newValue As String) As Boolean
        Dim success As Boolean = False
        For Each mySetting As Setting In AllSettings
            If mySetting.SettingName = settingName Then
                mySetting.SettingValue = newValue
                success = True
            End If
        Next
        Return success
    End Function


    ''' <summary>
    ''' returns the current settings list organized by the 'organizedBy' string
    ''' </summary>
    ''' <returns></returns>
    Public Function getOrganizedSettings() As SortedDictionary(Of String, List(Of Setting))
        Dim sorted As New SortedDictionary(Of String, List(Of Setting))

        Dim tempSettings = AllSettings.OrderBy(Function(x) x.SettingName)


        For Each mySetting As Setting In tempSettings
            If mySetting.SettingCategory = "" Then
                mySetting.SettingCategory = "General" 'sets the setting to be general
            End If
            If Not sorted.ContainsKey(mySetting.SettingCategory) Then
                'there's no key found, adding one to the sorted dict.
                sorted.Add(mySetting.SettingCategory, New List(Of Setting))
            End If
            sorted(mySetting.SettingCategory).Add(mySetting) 'adds the setting to the dictionary
        Next
        Return sorted
    End Function

    ''' <summary>
    ''' Updates the allSettings list from MySettings. If no match found, adds one to allSettings
    ''' </summary>
    Public Sub UpdateAllSettingFromMySettings()
        For Each mySetting As Configuration.SettingsPropertyValue In MySettings 'My.Settings.PropertyValues
            Dim matched As Boolean = False

            For Each setting As Utilities.Setting In AllSettings
                If mySetting.Name.ToLower = setting.SettingName.ToLower Then
                    setting.SettingValue = mySetting.PropertyValue 'update local sqlSetting
                    matched = True
                End If
            Next
            If Not matched Then
                Dim newSetting As New Utilities.Setting(mySetting.Name)
                newSetting.SettingValue = mySetting.PropertyValue
                AllSettings.Add(newSetting)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Updates the mySettings list (setting value only) from all settings. If no match found, adds one to allSettings.
    ''' </summary>
    Public Sub UpdateMySettingValueFromAllSettings()
        Dim totalCt As Integer = MySettings.Count
        Dim matchedCt As Integer = 0
        For Each mySetting As Configuration.SettingsPropertyValue In MySettings
            Dim matched As Boolean = False

            For Each setting As Utilities.Setting In AllSettings
                If mySetting.Name.ToLower = setting.SettingName.ToLower Then

                    mySetting.PropertyValue = setting.SettingValue
                    matched = True
                End If
            Next
            If matched Then
                matchedCt += 1
            Else
                Dim newSetting As New Utilities.Setting(mySetting.Name)
                newSetting.SettingValue = mySetting.PropertyValue
                AllSettings.Add(newSetting)
            End If
        Next
        If totalCt <> matchedCt Then
            Throw New Exception($"Only { matchedCt } of { totalCt } settings were matched!")
        ElseIf MySettings.Count <> AllSettings.Count Then
            RemoveSettings.Clear()

            For Each allSetting As Setting In AllSettings
                Dim matched As Boolean = False
                For Each mySetting As Configuration.SettingsPropertyValue In MySettings
                    If mySetting.Name.ToLower = allSetting.SettingName.ToLower Then matched = True
                Next
                If Not matched Then RemoveSettings.Add(allSetting)
            Next
        End If
    End Sub



#Region "SQL IO"




    ''' <summary>
    ''' Updates settings in My.Settings from that sql
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadSettingsFromSQL(Optional showPromptOnError As Boolean = True) As Boolean
        Dim success As Boolean = True
        Try
            AllSettings.Clear()

            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim cols As List(Of String) = {"settingName", "settingValue", "settingDescription", "SettingCategory"}.ToList
                    Dim query As String = $"SELECT { String.Join(", ", cols) } FROM {SqlTableName}"
                    cmd.CommandText = query

                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader
                        While reader.Read
                            Dim setting As New Utilities.Setting()
                            setting.SettingName = reader.GetString(cols.IndexOf("settingName"))
                            setting.SettingValue = reader.GetString(cols.IndexOf("settingValue"))
                            setting.SettingDescription = reader.GetString(cols.IndexOf("settingDescription"))
                            setting.SettingCategory = reader.GetString(cols.IndexOf("SettingCategory"))
                            AllSettings.Add(setting)
                        End While
                    End Using
                End Using
            End Using

            'Dim settingNames As Array = System.Enum.GetNames(GetType(SettingName))

            If AllSettings.Count > 0 Then

                UpdateMySettingValueFromAllSettings()

            End If

        Catch ex As Exception
            ' MsgBox(ex.Message, MsgBoxStyle.Critical, "updateMySettingsFromSQL")
            success = False
            If showPromptOnError Then
                If ex.Message.Contains(" settings were matched!") Then
                    'custom error. don't prompt to update changes.
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Load Settings From SQL")
                ElseIf MsgBox(ex.Message & vbCrLf & vbCrLf & "Loading from SQL Failed. Do you wish to view and edit the Sql Connection?", MsgBoxStyle.YesNo, "Edit Settings?") = MsgBoxResult.Yes Then
                    Dim frm As New frmSqlConnectionUtilities(SqlConnStr)
                    If frm.ShowDialog = DialogResult.OK Then
                        SqlConnStr = frm.SqlInfo.sqlConnStr
                    End If
                    success = LoadSettingsFromSQL(False)
                    If success Then
                        success = ShowSettings()
                    End If
                    'If success Then
                    '    MsgBox("It looks like it loaded ok. Please make sure to edit and save App Settings!")
                    'End If
                End If



            End If



        End Try

        Return success
    End Function



    ''' <summary>
    ''' Updates the sql table for each setting value in my.settings
    ''' </summary>
    ''' <returns></returns>
    Public Function updateSqlFromAllSettings(Optional ByVal UpdateFirstFromMySettings As Boolean = False) As Boolean
        Dim success As Boolean = True

        Try
            If UpdateFirstFromMySettings Then
                UpdateAllSettingFromMySettings()
            End If

            Using conn As New SqlConnection(SqlConnStr)
                Using cmd As SqlCommand = conn.CreateCommand
                    conn.Open()

                    If RemoveSettings.Count > 0 Then
                        For Each mySetting As Utilities.Setting In RemoveSettings
                            AllSettings.Remove(mySetting)

                            Using removecmd As SqlCommand = conn.CreateCommand
                                removecmd.CommandText = $"DELETE FROM { SqlTableName } WHERE SettingName = @SettingName"
                                removecmd.Parameters.Add("@SettingName", SqlDbType.VarChar).Value = mySetting.SettingName
                                removecmd.ExecuteNonQuery()
                            End Using
                        Next
                        RemoveSettings.Clear()
                    End If

                    For Each mySetting As Utilities.Setting In AllSettings 'Configuration.SettingsPropertyValue In My.Settings.PropertyValues
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@SettingName", SqlDbType.VarChar).Value = mySetting.SettingName
                        cmd.Parameters.Add("@SettingValue", SqlDbType.VarChar).Value = mySetting.SettingValue
                        cmd.Parameters.Add("@SettingDescription", SqlDbType.VarChar).Value = mySetting.SettingDescription
                        cmd.Parameters.Add("@SettingCategory", SqlDbType.VarChar).Value = mySetting.SettingCategory

                        Dim selectCmd As SqlCommand = conn.CreateCommand
                        selectCmd.CommandText = $"SELECT COUNT(*) FROM { SqlTableName } WHERE SettingName = @SettingName"
                        selectCmd.Parameters.Add("@SettingName", SqlDbType.VarChar).Value = mySetting.SettingName
                        If selectCmd.ExecuteScalar = 1 Then
                            'row found. Update
                            Dim sb As New Text.StringBuilder
                            sb.Append($"UPDATE { SqlTableName } SET ")
                            sb.Append("SettingValue = @SettingValue, SettingDescription = @SettingDescription, SettingCategory = @SettingCategory ")
                            sb.Append("WHERE ")
                            sb.Append("SettingName = @SettingName")
                            cmd.CommandText = sb.ToString

                            'cmd.Parameters.Add("@SettingName", SqlDbType.VarChar).Value = mySetting.SettingName
                            'cmd.Parameters.Add("@SettingValue", SqlDbType.VarChar).Value = mySetting.SettingValue
                            'cmd.Parameters.Add("@SettingDescription", SqlDbType.VarChar).Value = mySetting.SettingDescription
                            'cmd.Parameters.Add("@SettingCategory", SqlDbType.VarChar).Value = mySetting.SettingCategory

                            cmd.ExecuteNonQuery()
                        Else
                            'not found. Insert
                            Dim sb As New Text.StringBuilder
                            sb.Append($"INSERT INTO { SqlTableName } (SettingName, SettingValue, SettingDescription, SettingCategory)")
                            sb.Append("VALUES (@SettingName, @SettingValue, @SettingDescription, @SettingCategory)")
                            cmd.CommandText = sb.ToString

                            cmd.ExecuteNonQuery()
                        End If
                    Next
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Failed to Save AppSettings")
        End Try


        'For Each value As Configuration.SettingsPropertyValue In My.Settings.PropertyValues
        '    If Not SQLInfo.UpdateSQLItem(settingCols.settingName.ToString, value.Name.ToString, settingCols.settingValue.ToString, value.PropertyValue, SqlTableNames.AppSettings) Then
        '        success = False
        '    End If
        'Next
        Return success
    End Function


#End Region



#Region "XML IO"

    ''' <summary>
    ''' reads an xml file to settings
    ''' </summary>
    ''' <returns></returns>
    Public Function ReadSettingsXml() As Boolean
        Dim success As Boolean = True
        Try
            AllSettings = New List(Of Setting)
            If File.Exists(XmlSettingsPath) Then
                Dim xmlDoc As New XmlDocument
                xmlDoc.Load(XmlSettingsPath)
                For Each docNode As XmlNode In xmlDoc.ChildNodes
                    Select Case docNode.Name
                        Case cSettings
                            For Each readSetting As XmlNode In docNode.ChildNodes
                                Select Case readSetting.Name
                                    Case cSetting
                                        Dim setting As New Setting
                                        For Each settingNode As XmlNode In readSetting.ChildNodes
                                            Dim nodeValue As String = settingNode.InnerText
                                            Select Case settingNode.Name
                                                Case cName
                                                    setting.SettingName = nodeValue
                                                Case cValue
                                                    setting.SettingValue = nodeValue
                                                Case cHelp
                                                    setting.SettingDescription = nodeValue
                                                Case cOrganized
                                                    setting.SettingCategory = nodeValue

                                            End Select

                                        Next
                                        AllSettings.Add(setting)
                                End Select

                            Next

                    End Select
                Next


            Else
                Throw New FileNotFoundException("File Not Found - " & XmlSettingsPath)
            End If

        Catch ex As Exception
            success = False
            Errors.Add(ex.Message)
        End Try
        Return success
    End Function


    ''' <summary>
    ''' writes the current settings to xml
    ''' </summary>
    ''' <returns></returns>
    Public Function WriteSettingsXml() As Boolean
        Dim success As Boolean = True
        Dim xmlWriter As XmlWriter = Nothing
        Const dash As String = "*************************"
        Try
            Dim organized As SortedDictionary(Of String, List(Of Setting)) = getOrganizedSettings()
            If File.Exists(XmlSettingsPath) Then
                Try
                    File.Delete(XmlSettingsPath)
                Catch ex As Exception

                End Try
            End If
            Dim xmlSettings As New XmlWriterSettings
            xmlSettings.Encoding = Text.Encoding.UTF8
            xmlSettings.Indent = True
            xmlSettings.OmitXmlDeclaration = False
            xmlSettings.ConformanceLevel = ConformanceLevel.Document
            xmlWriter = XmlWriter.Create(XmlSettingsPath, xmlSettings)

            xmlWriter.WriteStartDocument()
            xmlWriter.WriteStartElement(cSettings)


            For Each myCategory As String In organized.Keys
                xmlWriter.WriteComment(dash & myCategory & dash)
                For Each mySetting As Setting In organized(myCategory)
                    xmlWriter.WriteStartElement(cSetting)
                    xmlWriter.WriteElementString(cName, mySetting.SettingName)
                    xmlWriter.WriteElementString(cValue, mySetting.SettingValue)
                    xmlWriter.WriteElementString(cHelp, mySetting.SettingDescription)
                    xmlWriter.WriteElementString(cOrganized, mySetting.SettingCategory)
                    xmlWriter.WriteEndElement()
                Next
                xmlWriter.WriteComment(dash & myCategory & dash)
            Next

            xmlWriter.WriteEndElement()
            xmlWriter.WriteEndDocument()
            xmlWriter.Flush()
            xmlWriter.Close()

        Catch ex As Exception
            success = False
            Errors.Add(ex.Message)
        Finally
            If Not (xmlWriter Is Nothing) Then
                xmlWriter.Close()
            End If
        End Try
        Return success
    End Function


#End Region




    Public Sub convertFromMySettings(ByVal SettingsList As System.Configuration.SettingsPropertyValueCollection)
        For Each oldSetting As System.Configuration.SettingsPropertyValue In SettingsList
            Dim newSetting As New Setting
            newSetting.SettingName = oldSetting.Name
            newSetting.SettingValue = oldSetting.PropertyValue
            AllSettings.Add(newSetting)

        Next
    End Sub
#End Region


End Class


Public Class Setting

#Region "Properties"
    ''' <summary>
    ''' Setting's Name
    ''' </summary>
    ''' <returns></returns>
    Public Property SettingName As String = ""

    ''' <summary>
    ''' Setting's Value
    ''' </summary>
    ''' <returns></returns>
    Public Property SettingValue As String = ""

    ''' <summary>
    ''' Setting's Description (Help?) string
    ''' </summary>
    ''' <returns></returns>
    Public Property SettingDescription As String = ""

    ''' <summary>
    ''' The setting's category - Used to organize settings
    ''' </summary>
    ''' <returns></returns>
    Public Property SettingCategory As String = ""




    ' -------- UI Stuff ----------

    Private Property SettingNameLbl As LinkLabel = Nothing

    ''' <summary>
    ''' the setting's textbox
    ''' </summary>
    ''' <returns></returns>
    Private Property SettingValueTxt As TextBox = Nothing

    Private Property SettingDescriptionLbl As LinkLabel = Nothing




    Private gb As Control = Nothing
    ''' <summary>
    ''' The UI Control that manages the setting
    ''' </summary>
    ''' <returns></returns>
    Public Property SettingCtrl As Control
        Get
            If IsNothing(gb) Then
                gb = CreateSettingCtrl()
            End If
            Return gb
        End Get
        Set(value As Control)
            gb = value
        End Set
    End Property

#End Region


#Region "Init"
    Public Sub New()

    End Sub

    Public Sub New(ByVal SettingName As String)
        Me.SettingName = SettingName
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Returns a table layout panel
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateSettingCtrl() As Control
        Dim regular As New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Regular)
        Dim italic As New Drawing.Font("Segoe UI", 9.75, Drawing.FontStyle.Italic)


        Dim tb As New TableLayoutPanel
        tb.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
        'tb.Dock = DockStyle.Fill
        'tb.Parent = box
        tb.Size = New System.Drawing.Size(600, 50)
        tb.BackColor = System.Drawing.Color.White

        tb.RowCount = 2
        tb.ColumnCount = 2
        tb.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 130))
        tb.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))

        SettingNameLbl = New LinkLabel
        SettingNameLbl.AutoSize = True
        SettingNameLbl.Text = GetReadableTitle()
        SettingNameLbl.Anchor = AnchorStyles.Right 'AnchorStyles.Top Or 
        SettingNameLbl.Font = regular
        SettingNameLbl.LinkColor = Drawing.Color.Black
        SettingNameLbl.LinkBehavior = LinkBehavior.HoverUnderline
        AddHandler SettingNameLbl.Click, AddressOf SettingNameLblClicked
        tb.Controls.Add(SettingNameLbl, 0, 0)
        'tb.SetRowSpan(SettingNameLbl, 2)


        SettingValueTxt = New TextBox
        SettingValueTxt.Text = SettingValue
        SettingValueTxt.Dock = DockStyle.Fill
        If SettingName.ToLower.Contains("pass") Then SettingValueTxt.UseSystemPasswordChar = True
        tb.Controls.Add(SettingValueTxt, 1, 0)



        SettingDescriptionLbl = New LinkLabel
        SettingDescriptionLbl.Font = italic
        SettingDescriptionLbl.LinkColor = Drawing.Color.DarkGray
        SettingDescriptionLbl.LinkBehavior = LinkBehavior.HoverUnderline
        SettingDescriptionLbl.Text = SPACER & SettingDescription
        SettingDescriptionLbl.AutoSize = True
        SettingDescriptionLbl.Anchor = (AnchorStyles.Top Or AnchorStyles.Left)
        AddHandler SettingDescriptionLbl.Click, AddressOf DescriptionClicked
        tb.Controls.Add(SettingDescriptionLbl, 1, 1)

        Return tb
        'Return box
    End Function


    ''' <summary>
    ''' Creates a more user friendly name for the setting
    ''' </summary>
    ''' <returns></returns>
    Private Function GetReadableTitle() As String
        'creates a more user friendly name
        Dim title As String = SettingName
        If title.Length > 3 AndAlso title.Substring(0, 3) = "PrE" Then
            'PrE = Project & Ebooks
            title = "Project " & title.Substring(3, title.Length - 3)
        End If
        'alphalist is used to make more user friendly labels
        Dim Alpha As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim AlphaList As New List(Of String)
        For Each aChar In Alpha
            AlphaList.Add(aChar)
        Next


        For i As Integer = 1 To title.Length - 1
            If AlphaList.Contains(title(i)) Then
                title = title.Insert(i, " ")
                i += 1 'adds 1 to the count so that it doesn't count the space
            End If
        Next

        If title.Contains("dir ") Then
            title = title.Replace("dir ", "").Trim
            title &= " Directory"
        End If

        title = title.Replace("_", " ")

        Dim monthReplaces As List(Of String) = {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"}.ToList
        Dim pattern As String = "(?<!\d)\d\d(?!\d)" '"[0-9]{2}"
        Dim regx As New Regex(pattern)

        For Each m As Match In regx.Matches(title)
            If monthReplaces.Contains(m.Value) Then
                title = title.Replace(m.Value, " " & m.Value)
            End If
        Next

        'For Each moReplace As String In monthReplaces
        '    If title.Contains(moReplace) Then
        '        ' for the titles with the month... Puts a space before each month
        '        title = title.Replace(moReplace, " " & moReplace)
        '    End If
        'Next

        'replace extra spaces
        title = title.Trim
        title = Regex.Replace(title, " {2,}", " ")



        Return title & ":"


    End Function

    Private Sub SettingNameLblClicked()
        Dim result As String = InputBox("Please enter new categorization" & vbCrLf & vbCrLf & "(Any changes will be reflected on re-load)", $"Update Category for {SettingName}.", SettingCategory)
        If result.Trim <> "" Then
            SettingCategory = result

        End If
    End Sub

    Private Sub DescriptionClicked()
        Dim result As String = InputBox("Please enter a new description", $"Update description for {SettingName}.", SettingDescription)
        If result.Trim <> "" Then
            SettingDescription = result
            SettingDescriptionLbl.Text = SPACER & SettingDescription
        End If
    End Sub

    ''' <summary>
    ''' updates the setting from the groupbox (currently 3.17.2021, only updates settingvalue
    ''' </summary>
    ''' <returns></returns>
    Public Function UpdateSettingsFromGB() As Boolean
        Dim success As Boolean = False
        If Not IsNothing(gb) Then
            SettingValue = SettingValueTxt.Text.Trim


        End If
        Return success
    End Function

#End Region

End Class


