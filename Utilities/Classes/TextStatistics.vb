Imports System.Text.RegularExpressions

'TextStatistics Class ()
'        http://code.google.com/p/php-text-statistics/

'        Released under New BSD license
'        http://www.opensource.org/licenses/bsd-license.php

'        Calculates following readability scores (formulae can be found in wiki):
'          * Flesch Kincaid Reading Ease
'          * Flesch Kincaid Grade Level
'          * Gunning Fog Score
'          * Coleman Liau Index
'          * SMOG Index
'          * Automated Reability Index

'        Will also give:
'          * String length
'          * Letter count
'          * Syllable count
'          * Sentence count
'          * Average words per sentence
'          * Average syllables per word

'        Sample Code
'        ----------------
'        Dim statistics as TextStatistics = new TextStatistics
'        Dim strText as String = "The quick brown fox jumped over the lazy dog."
'        Dim strOutput as String = "Flesch-Kincaid Reading Ease: " & statistics.getFleschKincaidReadingEase(strText)
Public Class TextStatistics
    Protected Friend Shared Function getFleschKincaidReadingEase(ByRef cleanedText As String) As Decimal
        'strText = cleanedText 'cleanText(strText)
        Return Math.Round((206.835 - (1.015 * getAverageWordsPerSentence(cleanedText)) - (84.6 * getAverageSyllablesPerWord(cleanedText))), 1)
    End Function

    Protected Friend Shared Function getFleschKincaidGradeLevel(ByRef cleanedText As String) As Decimal
        'strText = cleanedText 'cleanText(strText)
        Return Math.Round((0.39 - (1.015 * getAverageWordsPerSentence(cleanedText)) + (11.8 * getAverageSyllablesPerWord(cleanedText)) - 15.59), 1)
    End Function

    Protected Friend Shared Function getGunningFogScore(ByRef cleanedText As String) As Decimal
        'strText = cleanedText 'cleanText(strText)
        Return Math.Round(((getAverageWordsPerSentence(cleanedText) + getPercentageWordsWithThreeSyllables(cleanedText, False)) * 0.4), 1)
    End Function

    Protected Friend Shared Function getColemanLiauIndex(ByRef cleanedText As String) As Decimal
        'strText = cleanedText 'cleanText(strText)
        Return Math.Round(((5.89 * (getLetterCount(cleanedText) / getWordCount(cleanedText))) - (0.3 * (getSentenceCount(cleanedText) / getWordCount(cleanedText))) - 15.8), 1)
    End Function

    Protected Friend Shared Function getSMOGIndex(ByRef cleanedText As String) As Decimal
        'strText = cleanedText 'cleanText(strText)
        Return Math.Round(((getAverageWordsPerSentence(cleanedText) + getPercentageWordsWithThreeSyllables(cleanedText, False)) * 0.4), 1)
    End Function

    Protected Friend Shared Function getAutomatedReadability(ByRef cleanedText As String) As Decimal
        'strText = cleanedText 'cleanText(strText)
        Return Math.Round(((4.71 * (getLetterCount(cleanedText) / getWordCount(cleanedText))) + (0.5 * (getWordCount(cleanedText) / getSentenceCount(cleanedText))) - 21.43), 1)
    End Function

    Protected Friend Shared Function getLetterCount(ByRef cleanedText As String) As Integer
        'strText = cleanedText 'cleanText(strText) ' To clear out newlines etc
        Return Regex.Replace(cleanedText, "/[^A-Za-z]+/", "").Length
    End Function

    Protected Friend Shared Function cleanText(ByVal strText As String) As String
        strText = Regex.Replace(strText, "[,:()-]", " ") ' Replace commans, hyphens etc (count them as spaces)
        strText = Regex.Replace("[\.!?]", ".", strText) 'Unify terminators
        strText = Trim(strText)
        If Right(strText, 1) <> "." Then strText &= "." ' Add final terminator, just in case it's missing.
        strText = Regex.Replace(strText, "[ ]*(\n|\r\n|\r)[ ]*", " ") ' Replace new lines with spaces
        strText = Regex.Replace(strText, "([\.])[\. ]+", "$1") ' Check for duplicated terminators
        strText = Trim(Regex.Replace(strText, "[ ]*([\.])", "$1 ")) ' Pad sentence terminators
        strText = Regex.Replace(strText, "[ ]+", " ") ' Remove multiple spaces
        For Each rxpMatch As Match In Regex.Matches(strText, "\. [A-Z]")
            strText = Regex.Replace(strText, rxpMatch.Value, rxpMatch.Value.ToLower) ' Lower case all words following terminators (for gunning fog score)
            'strText = strText.Substring(0, rxpMatch.Index) & rxpMatch.Value.ToLower & strText.Substring(rxpMatch.Index + 3, strText.Length - (rxpMatch.Index + 3))
        Next
        Return strText
    End Function

    Protected Friend Shared Function getSentenceCount(ByVal strText As String) As Decimal
        'strText = cleanText(strText) ' Will be tripped up by "Mr." or "U.K.". Not a major concern at this point.
        Return Math.Max(1, (Regex.Replace("[^\.!?]", "", strText)).Length)
    End Function

    Protected Friend Shared Function getWordCount(ByVal strText As String) As Decimal
        'strText = cleanText(strText)
        ' Will be tripped by by em dashes with spaces either side, among other similar characters
        Return 1 + Regex.Replace(strText, "[^ ]", "").Length ' Space count + 1 is word count
    End Function

    Protected Friend Shared Function getAverageWordsPerSentence(ByVal strText As String) As Decimal
        'strText = cleanText(strText)
        Return (getWordCount(strText) / getSentenceCount(strText))
    End Function

    Protected Friend Shared Function getAverageSyllablesPerWord(ByVal strText As String) As Integer
        'strText = cleanText(strText)
        Dim intSyllableCount As Integer = 0
        Dim intWordCount As Integer = getWordCount(strText)
        Dim arrWords() As String = Split(strText, " ")
        For intLoop As Integer = 0 To intWordCount - 1
            intSyllableCount += getSyllableCount(arrWords(intLoop))
        Next
        Return (intSyllableCount / intWordCount)
    End Function

    Protected Friend Shared Function getWordsWithThreeSyllables(ByVal strText As String, Optional ByVal blnCountProperNouns As Boolean = True) As Integer
        'strText = cleanText(strText)
        Dim intLongWordCount As Integer = 0
        Dim intWordCount As Integer = getWordCount(strText)
        Dim arrWords() As String = Split(strText, " ")
        For intLoop As Integer = 0 To intWordCount - 1
            If (getSyllableCount(arrWords(intLoop)) > 2) Then
                If (blnCountProperNouns) Then
                    intLongWordCount += 1
                Else
                    Dim strFirstLetter As String = arrWords(intLoop).Substring(0, 1)
                    If (strFirstLetter <> strFirstLetter.ToUpper) Then
                        ' First letter is lower case. Count it.
                        intLongWordCount += 1
                    End If
                End If
            End If
        Next

        Return (intLongWordCount)
    End Function

    Protected Friend Shared Function getPercentageWordsWithThreeSyllables(ByVal strText As String, Optional ByVal blnCountProperNouns As Boolean = True) As Decimal
        'strText = cleanText(strText)
        Return ((getWordsWithThreeSyllables(strText, blnCountProperNouns) / getWordCount(strText)) * 100)
    End Function

    Protected Friend Shared Function getSyllableCount(ByVal strWord As String) As Integer

        Dim intSyllableCount As Integer = 0
        strWord = strWord.ToLower

        ' Specific common exceptions that don't follow the rule set below are handled individually
        ' Collection of problem words (with word as key, syllable count as value)
        Dim arrProblemWords As Collections.Specialized.NameValueCollection = New Collections.Specialized.NameValueCollection
        arrProblemWords.Add("simile", "3")
        arrProblemWords.Add("forever", "3")
        arrProblemWords.Add("shoreline", "2")
        arrProblemWords.Add("beyond", "2")
        arrProblemWords.Add("apiece", "2")
        arrProblemWords.Add("bandage", "2")
        arrProblemWords.Add("cabbage", "2")
        arrProblemWords.Add("college", "2")
        arrProblemWords.Add("cottage", "2")
        arrProblemWords.Add("crooked", "2")
        arrProblemWords.Add("crowded", "2")
        arrProblemWords.Add("damage", "2")
        arrProblemWords.Add("doesnt", "2")
        arrProblemWords.Add("flour", "2")
        arrProblemWords.Add("forest", "2")
        arrProblemWords.Add("garbage", "2")
        arrProblemWords.Add("garage", "2")
        arrProblemWords.Add("hundred", "2")
        arrProblemWords.Add("inches", "2")
        arrProblemWords.Add("joyous", "2")
        arrProblemWords.Add("language", "2")
        arrProblemWords.Add("lettuce", "2")
        arrProblemWords.Add("maybe", "2")
        arrProblemWords.Add("mayor", "2")
        arrProblemWords.Add("message", "2")
        arrProblemWords.Add("notice", "2")
        arrProblemWords.Add("office", "2")
        arrProblemWords.Add("package", "2")
        arrProblemWords.Add("palace", "2")
        arrProblemWords.Add("peaches", "2")
        arrProblemWords.Add("peaceful", "2")
        arrProblemWords.Add("police", "2")
        arrProblemWords.Add("postage", "2")
        arrProblemWords.Add("reader", "2")
        arrProblemWords.Add("reading", "2")
        arrProblemWords.Add("really", "2")
        arrProblemWords.Add("rejoice", "2")
        arrProblemWords.Add("sausage", "2")
        arrProblemWords.Add("savage", "2")
        arrProblemWords.Add("service", "2")
        arrProblemWords.Add("surface", "2")
        arrProblemWords.Add("village", "2")
        arrProblemWords.Add("wasnt", "2")
        arrProblemWords.Add("wicked", "2")

        intSyllableCount = arrProblemWords(strWord)
        If (intSyllableCount > 0) Then
            Return intSyllableCount
        End If


        ' These syllables would be counted as two but should be one
        Dim arrSubSyllables() As String = New String() {
         "cial" _
             , "tia" _
             , "cius" _
             , "cious" _
             , "giu" _
             , "ion" _
             , "iou" _
             , "sia" _
             , "[^aeiuoyt]{2,}ed" _
             , ".ely" _
             , "[cg]h?e[rsd]?" _
             , "rved?" _
             , "[aeiouy][dt]es?" _
             , "[aeiouy][^aeiouydt]e[rsd]?" _
             , "^[dr]e[aeiou][^aeiou]+" _
             , "[aeiouy]rse"
         }

        ' These syllables would be counted as one but should be two
        Dim arrAddSyllables() As String = New String() {
        "ia" _
            , "riet" _
            , "dien" _
            , "iu" _
            , "io" _
            , "ii" _
            , "[aeiouym]bl" _
            , "[aeiou]{3}" _
            , "^mc" _
            , "ism" _
            , "([^aeiouy])\1l" _
            , "[^l]lien" _
            , "^coa[dglx]." _
            , "[^gq]ua[^auieo]" _
            , "dnt" _
            , "uity" _
            , "ie(r|st)"
        }

        ' Single syllable prefixes and suffixes
        Dim arrPrefixSuffix() As String = New String() {
        "^un" _
            , "^fore" _
            , "ly" _
            , "less" _
            , "ful" _
            , "ers?" _
            , "ings?"
        }

        ' Remove prefixes and suffixes and count how many were taken
        Dim intPrefixSuffixCount As Integer = 0
        For Each strPrefixSuffix As String In arrPrefixSuffix
            intPrefixSuffixCount += Regex.Matches(strWord, strPrefixSuffix).Count
        Next

        For Each strPrefixSuffix As String In arrPrefixSuffix
            strWord = Regex.Replace(strWord, strPrefixSuffix, "")
        Next

        ' Removed non-word characters from word
        strWord = Regex.Replace(strWord, "/[^a-z]/is", "")
        Dim arrWordParts() As String = Regex.Split(strWord, "[^aeiouy]+")
        Dim intWordPartCount As Integer = 0
        For Each strWordPart As String In arrWordParts
            If (strWordPart <> "") Then
                intWordPartCount += 1
            End If
        Next

        ' Some syllables do not follow normal rules - check for them
        ' Thanks to Joe Kovar for correcting a bug in the following lines
        intSyllableCount = intWordPartCount + intPrefixSuffixCount
        For Each strSyllable As String In arrSubSyllables
            intSyllableCount -= Regex.Matches(strWord, "~" & strSyllable & "~").Count
        Next
        For Each strSyllable As String In arrAddSyllables
            intSyllableCount += Regex.Matches(strWord, "~" & strSyllable & "~").Count
        Next
        intSyllableCount = If(intSyllableCount = 0, 1, intSyllableCount)
        Return intSyllableCount
    End Function
    Public Shared Function Distance(ByVal s1 As String, ByVal s2 As String, ByVal maxOffset As Integer) As Single
        If String.IsNullOrEmpty(s1) Then
            Return If(String.IsNullOrEmpty(s2), 0, s2.Length)
        End If
        If String.IsNullOrEmpty(s2) Then
            Return s1.Length
        End If
        Dim c As Integer = 0
        Dim offset1 As Integer = 0
        Dim offset2 As Integer = 0
        Dim lcs As Integer = 0
        While (c + offset1 < s1.Length) AndAlso (c + offset2 < s2.Length)
            If s1(c + offset1) = s2(c + offset2) Then
                lcs += 1
            Else
                offset1 = 0
                offset2 = 0
                For i As Integer = 0 To maxOffset - 1
                    If (c + i < s1.Length) AndAlso (s1(c + i) = s2(c)) Then
                        offset1 = i
                        Exit For
                    End If
                    If (c + i < s2.Length) AndAlso (s1(c) = s2(c + i)) Then
                        offset2 = i
                        Exit For
                    End If
                Next
            End If
            c += 1
        End While
        Return (s1.Length + s2.Length) \ 2 - lcs
    End Function

    Public Shared Function GetSimilarity(ByVal s1 As String, ByVal s2 As String, Optional ByVal offset As Integer = 5) As Single
        Dim dis As Single = Distance(s1, s2, offset) 'offset is the maximum offset that the code looks for a corresponding letter
        Dim maxLen As Integer = Math.Max(s1.Length, s2.Length)
        If maxLen = 0 Then
            Return 1
        Else
            Return 1 - dis / maxLen
        End If
    End Function
End Class








''From "main" class


'Public Function getFileNameRange(ByVal inFilename As String) As String()
'    'If boolIsSearchActive = False Then
'    'allows funtion to return "Done" which causes worker threads to terminate
'    If Me.InvokeRequired Then
'        Dim d As New getFilenameRangeCallback(AddressOf getFileNameRange)
'        Return Me.Invoke(d, New Object() {})
'    Else
'        Dim intFileIndex As Integer = lstFiles.Items.IndexOf(inFilename)
'        Dim strFiles(lstFiles.Items.Count - intFileIndex - 2) As String
'        Dim intLoop2 As Integer = 0
'        For intLoop As Integer = intFileIndex + 1 To lstFiles.Items.Count - 1
'            strFiles(intLoop2) = lstFiles.Items(intLoop)
'            intLoop2 += 1
'        Next
'        Return (strFiles)
'    End If
'End Function


''from "worker" class

'''' <summary>
'''' Calculates the similarity of the current file vs all the other files in the current list
'''' </summary>
'''' <param name="aRTFBox"></param>
'''' <remarks></remarks>
'Private Sub CalculateSimilarity(ByRef aRTFBox As RichTextBox)
'    If ProcessFile Then
'        If chkCalculateSimilarityChecked Then
'            intCurrentOptionIndex += 1
'            UpdateReplaceBox(aRTFBox)
'            Try
'                aRTFBox.Rtf = aRTFBox.Rtf.Replace("\-", "")
'                'Get Readability scores
'                Dim strHeader As String = "ArticleID,PercentMatch"
'                Dim strScores As String = ""
'                Dim tmpBox As RichTextBox = New RichTextBox
'                Dim sngSimilarity As Single = 0
'                Dim strOtherFileNames As String() = main.Invoke(New frmRtfProcessor.getFilenameRangeCallback(AddressOf main.getFileNameRange), fileName)
'                Dim fileSize As Long = GetFileSize(fileName)
'                Dim otherfileSize As Long
'                Dim boolWithFileSizeRange As Boolean = False
'                intCurrentOptionStepMax = strOtherFileNames.Length - 1
'                For Each strOtherFileName As String In strOtherFileNames
'                    intCurrentOptionStepIndex += 1
'                    otherfileSize = GetFileSize(strOtherFileName)

'                    boolWithFileSizeRange = False
'                    If fileSize >= otherfileSize Then
'                        If sngSimilarityTarget * fileSize < otherfileSize Then boolWithFileSizeRange = True
'                    Else
'                        If sngSimilarityTarget * otherfileSize < fileSize Then boolWithFileSizeRange = True
'                    End If

'                    If boolWithFileSizeRange Then
'                        'If 1 - ((Math.Abs(GetFileSize(strOtherFileName) - fileSize) / fileSize)) >= sngSimilarityTarget
'                        'If (Math.Abs(tmpBox.TextLength - aRTFBox.TextLength) / aRTFBox.TextLength) * 100 >= Int(strSimilarity) Then
'                        'main.Invoke(New frmControl.updateOptionProgressCallback(AddressOf main.updateOptionProgress), intThreadId, intCurrentOptionStepIndex, strOtherFileNames.Length)
'                        tmpBox.LoadFile(strOtherFileName)
'                        sngSimilarity = TextStatistics.GetSimilarity(aRTFBox.Text, tmpBox.Text, 1000)
'                        If sngSimilarity >= sngSimilarityTarget Then
'                            strScores &= strOtherFileName & "," & Int(sngSimilarity * 100) & vbNewLine
'                        End If
'                    End If
'                Next
'                If Len(strScores) > 0 Then IO.File.WriteAllText(Path.Combine(strOutputPath, Path.GetFileNameWithoutExtension(fileName) & "_Duplicates.csv"), strHeader & vbNewLine & strScores)

'                strHeader = Nothing
'                strScores = Nothing
'                tmpBox = Nothing
'                sngSimilarity = Nothing
'                strOtherFileNames = Nothing
'                fileSize = Nothing
'                otherfileSize = Nothing
'                boolWithFileSizeRange = Nothing
'            Catch ex As Exception
'                aRTFBox.Rtf = ReplaceBox.Rtf
'                MsgBox("Unable to 'Calculate Similarity' in " & fileName & vbNewLine & ex.Message, MsgBoxStyle.OkOnly, "Skipping Option")
'            End Try
'            intCurrentOptionStepIndex = 0
'            intCurrentOptionStepMax = 0
'            'main.Invoke(New frmControl.updateOptionProgressCallback(AddressOf main.updateOptionProgress), intThreadId, intCurrentOptionStepIndex, 10)
'        End If
'    End If

'End Sub