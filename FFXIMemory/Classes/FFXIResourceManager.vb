Imports System.Globalization
Imports System.IO
Imports System.Text


' TODO: Make a real design decision on how to decide what language a string resource should be returned as.
'       Should it be the UI language, the selected POL region, ...?
' For now, this will always return the English text, except for non-expando autotrans messages, which will be in the language specified by their ID.
Public Class FFXIResourceManager

    Private Shared E As New FFXIEncoding()

    Public Shared Function GetResourceString(ByVal ResourceID As UInteger) As String
        Dim ResourceString As String = FFXIResourceManager.GetResourceStringInternal(ResourceID)
        If ResourceString = "" Then
            Return "Bad Resource ID"
        Else
            Return ResourceString
        End If
    End Function

    Public Shared Function IsValidResourceID(ByVal ResourceID As UInteger) As Boolean
        Return (FFXIResourceManager.GetResourceStringInternal(ResourceID) IsNot Nothing)
    End Function

    Public Shared Function GetAreaName(ByVal ID As UShort) As String
        Return FFXIResourceManager.GetStringTableEntry(55465, ID)
    End Function

    Public Shared Function GetJobName(ByVal ID As UShort) As String
        Return FFXIResourceManager.GetStringTableEntry(55467, ID)
    End Function

    Public Shared Function GetRegionName(ByVal ID As UShort) As String
        Return FFXIResourceManager.GetStringTableEntry(55654, ID)
    End Function

    Public Shared Function GetAbilityName(ByVal ID As UShort) As String
#If False Then
			Dim BR As BinaryReader = FFXIResourceManager.OpenDATFile(85)
			' JP = 10
			If BR IsNot Nothing Then
				If (ID + 1) * &H400 <= BR.BaseStream.Length Then
					BR.BaseStream.Position = ID * &H400
					Dim AbilityData As Byte() = BR.ReadBytes(&H400)
					BR.Close()
					If FFXIEncryption.DecodeDataBlock(AbilityData) Then
						Return FFXIResourceManager.E.GetString(AbilityData, &Ha, 32).TrimEnd(ControlChars.NullChar)
					End If
				End If
				BR.Close()
			End If
			Return Nothing
#Else
        Return [String].Format("Ability #{0}", ID)
#End If
    End Function

    Public Shared Function GetSpellName(ByVal ID As UShort) As String
#If False Then
			Dim BR As BinaryReader = FFXIResourceManager.OpenDATFile(86)
			If BR IsNot Nothing Then
				If (ID + 1) * &H400 <= BR.BaseStream.Length Then
					BR.BaseStream.Position = ID * &H400
					Dim SpellData As Byte() = BR.ReadBytes(&H400)
					BR.Close()
					If FFXIEncryption.DecodeDataBlock(SpellData) Then
						Return FFXIResourceManager.E.GetString(SpellData, &H3d, 20).TrimEnd(ControlChars.NullChar)
					End If
				End If
				BR.Close()
			End If
			Return Nothing
#Else
        Return [String].Format("Spell #{0}", ID)
#End If
    End Function

    ' JP: 4, 5, 6, 7, 8
    Private Shared ItemDATs As UShort() = New UShort() {73, 74, 75, 76, 77}

    Public Shared Function GetItemName(ByVal Language As Byte, ByVal ID As UShort) As String
        For Each ItemDAT As UShort In FFXIResourceManager.ItemDATs
            Dim BR As BinaryReader = FFXIResourceManager.OpenDATFile(ItemDAT)
            If BR IsNot Nothing Then
                Dim T As Item.iType
                Item.DeduceType(BR, T)
                Dim Offset As Long = (ID And &HFFF) * &HC00
                If BR.BaseStream.Length >= Offset + &HC00 Then
                    Dim I As New Item()
                    BR.BaseStream.Position = Offset
                    If I.Read(BR, T) AndAlso CUInt(I.GetFieldValue("id")) = ID Then
                        BR.Close()
                        Return I.GetFieldText("name")
                    End If
                End If
                BR.Close()
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function GetKeyItemName(ByVal Language As Byte, ByVal ID As UShort) As String
#If False Then
			Dim BR As BinaryReader = FFXIResourceManager.OpenDATFile(82)
			' JP = 80
			If BR IsNot Nothing Then
				If Encoding.ASCII.GetString(BR.ReadBytes(4)) = "menu" AndAlso BR.ReadUInt32() = &H101 Then
					BR.BaseStream.Position = &H20
					While BR.BaseStream.Position < BR.BaseStream.Length
						Dim Offset As Long = BR.BaseStream.Position
						Dim ShortName As String = Encoding.ASCII.GetString(BR.ReadBytes(4))
						Dim SizeInfo As UInteger = BR.ReadUInt32()
						If BR.ReadUInt64() <> 0 Then
							Exit While
						End If
						If ShortName = "sc_i" Then
							BR.BaseStream.Position += &H14
							Dim EntryCount As UInteger = BR.ReadUInt32()
							For i As UInteger = 0 To EntryCount - 1
								If BR.ReadUInt32() = ID Then
									BR.BaseStream.Position += 4
									BR.BaseStream.Position = Offset + &H10 + BR.ReadUInt32()
									Return FFXIEncryption.ReadEncodedString(BR, FFXIResourceManager.E)
								End If
								BR.BaseStream.Position += 16
							Next
						End If
						' Skip to next one
						BR.BaseStream.Position = Offset + ((SizeInfo And &Hffffff80UI) >> 3)
					End While
				End If
				BR.Close()
			End If
			Return Nothing
#Else
        Return [String].Format("Key Item #{0}", ID)
#End If
    End Function

    Public Shared Function GetAutoTranslatorMessage(ByVal Category As Byte, ByVal Language As Byte, ByVal ID As UShort) As String
        ' FIXME: This is probably a stale file
        Dim BR As BinaryReader = FFXIResourceManager.OpenDATFile(55665)
        ' JP = 55545
        If BR IsNot Nothing Then
            While BR.BaseStream.Position + 76 <= BR.BaseStream.Length
                Dim GroupCat As Byte = BR.ReadByte()
                Dim GroupLang As Byte = BR.ReadByte()
                Dim GroupID As UShort = CUShort(BR.ReadByte() * 256 + BR.ReadByte())
                BR.BaseStream.Position += 64
                Dim Messages As UInteger = BR.ReadUInt32()
                Dim DataBytes As UInteger = BR.ReadUInt32()
                If GroupID = (ID And &HFF00) Then
                    ' We found the right group (ignoring category & language for now)
                    Dim i As UInteger = 0
                    While i < Messages AndAlso BR.BaseStream.Position + 5 < BR.BaseStream.Length
                        Dim MessageCat As Byte = BR.ReadByte()
                        Dim MessageLang As Byte = BR.ReadByte()
                        Dim MessageID As UShort = CUShort(BR.ReadByte() * 256 + BR.ReadByte())
                        Dim TextLength As Byte = BR.ReadByte()
                        If MessageID = ID Then
                            ' We found the right message (ignoring category & language for now)
                            Dim MessageBytes As Byte() = BR.ReadBytes(TextLength)
                            BR.Close()
                            Dim MessageText As String = FFXIResourceManager.E.GetString(MessageBytes).TrimEnd(ControlChars.NullChar)
                            Return FFXIResourceManager.MaybeExpandAutoTranslatorMessage(MessageText)
                        Else
                            BR.BaseStream.Position += TextLength
                            If MessageLang = &H4 Then
                                ' There is an extra string to skip for Japanese entries
                                TextLength = BR.ReadByte()
                                BR.BaseStream.Position += TextLength
                            End If
                        End If
                        i += 1
                    End While
                Else
                    BR.BaseStream.Position += DataBytes
                End If
            End While
            BR.Close()
        End If
        Return Nothing
    End Function

    Private Shared Function GetResourceStringInternal(ByVal ResourceID As UInteger) As String
        Dim Category As Byte = CByte((ResourceID >> 24) And &HFF)
        Dim Language As Byte = CByte((ResourceID >> 16) And &HFF)
        Dim ID As UShort = CUShort(ResourceID And &HFFFF)
        Select Case Category
            Case &H2
                Return FFXIResourceManager.GetAutoTranslatorMessage(Category, Language, ID)
            Case &H4
                Return FFXIResourceManager.GetAutoTranslatorMessage(Category, Language, ID)
            Case &H6
                Return FFXIResourceManager.GetItemName(Language, ID)
            Case &H7
                Return FFXIResourceManager.GetItemName(Language, ID)
            Case &H8
                Return FFXIResourceManager.GetItemName(Language, ID)
            Case &H9
                Return FFXIResourceManager.GetItemName(Language, ID)
            Case &H13
                Return FFXIResourceManager.GetKeyItemName(Language, ID)
        End Select
        Return Nothing
    End Function

    Private Shared Function GetStringTableEntry(ByVal FileNumber As UShort, ByVal ID As UShort) As String
        Dim BR As BinaryReader = FFXIResourceManager.OpenDATFile(FileNumber)
        Try
            If BR IsNot Nothing Then
                BR.BaseStream.Position = &H18
                ' FIXME: Assumes single-string table; code should be made more generic.
                Dim HeaderBytes As UInteger = BR.ReadUInt32()
                Dim EntryBytes As UInteger = BR.ReadUInt32()
                BR.ReadUInt32()
                Dim DataBytes As UInteger = BR.ReadUInt32()
                If HeaderBytes = &H40 AndAlso ID * 8 < EntryBytes AndAlso HeaderBytes + EntryBytes + DataBytes = BR.BaseStream.Length Then
                    BR.BaseStream.Position = &H40 + ID * 8
                    Dim Offset As UInteger = (BR.ReadUInt32() Xor &HFFFFFFFFUI)
                    Dim Length As UInteger = (BR.ReadUInt32() Xor &HFFFFFFFFUI) - 40
                    If Length >= 0 AndAlso 40 + Offset + Length <= DataBytes Then
                        BR.BaseStream.Position = HeaderBytes + EntryBytes + 40 + Offset
                        Dim TextBytes As Byte() = BR.ReadBytes(CInt(Length))
                        For i As UInteger = 0 To TextBytes.Length - 1
                            TextBytes(i) = TextBytes(i) Xor &HFF
                        Next
                        Return E.GetString(TextBytes).TrimEnd(ControlChars.NullChar)
                    End If
                End If
            End If
            ' ignore
        Catch
        Finally
            BR.Close()
        End Try
        Return Nothing
    End Function

    Private Shared Function MaybeExpandAutoTranslatorMessage(ByVal Text As String) As String
        ' Reference to a string table entry? => return referenced string
        If Text IsNot Nothing AndAlso Text.Length > 2 AndAlso Text.Length <= 6 AndAlso Text(0) = "@"c Then
            Dim ReferenceType As Char = Text(1)
            Try
                Dim ID As UShort = UShort.Parse(Text.Substring(2), NumberStyles.AllowHexSpecifier)
                Select Case ReferenceType
                    Case "A"c
                        Return FFXIResourceManager.GetAreaName(ID)
                    Case "C"c
                        Return FFXIResourceManager.GetSpellName(ID)
                    Case "J"c
                        Return FFXIResourceManager.GetJobName(ID)
                    Case "Y"c
                        Return FFXIResourceManager.GetAbilityName(ID)
                End Select
            Catch
            End Try
        End If
        Return Text
    End Function

    Private Shared Function OpenDATFile(ByVal FileNumber As UShort) As BinaryReader
        Try
            Dim FullDATFileName As String = FFXI.GetFilePath(FileNumber)
            If File.Exists(FullDATFileName) Then
                Return New BinaryReader(New FileStream(FullDATFileName, FileMode.Open, FileAccess.Read))
            End If
        Catch
        End Try
        Return Nothing
    End Function

End Class

