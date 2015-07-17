Imports System.Collections
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Text


Public Class FFXIEncoding
    Inherits Encoding
    ' http://www.microsoft.com/globaldev/reference/dbcs/932.htm
    ' The main table, and the 60 lead-byte tables
    Private Shared ConversionTables As New SortedList(61)

    Public Sub New()
    End Sub

    Public Overrides ReadOnly Property EncodingName() As String
        Get
            Return "Japanese (Shift-JIS, with FFXI extensions)"
        End Get
    End Property
    Public Overrides ReadOnly Property BodyName() As String
        Get
            Return "iso-2022-jp-ffxi"
        End Get
    End Property
    Public Overrides ReadOnly Property HeaderName() As String
        Get
            Return "iso-2022-jp-ffxi"
        End Get
    End Property
    Public Overrides ReadOnly Property WebName() As String
        Get
            Return "iso-2022-jp-ffxi"
        End Get
    End Property

    Public Shared ReadOnly SpecialMarkerStart As Char = "≺"c
    ' ≺
    Public Shared ReadOnly SpecialMarkerEnd As Char = "≻"c
    ' ≻
#Region "Utility Functions"

    Public Overrides Function GetByteCount(ByVal chars As Char(), ByVal index As Integer, ByVal count As Integer) As Integer
        Return Me.GetBytes(chars, index, count).Length
    End Function

    Public Overrides Function GetCharCount(ByVal bytes As Byte(), ByVal index As Integer, ByVal count As Integer) As Integer
        Return Me.GetString(bytes, index, count).Length
    End Function

    Public Overrides Function GetMaxByteCount(ByVal charCount As Integer) As Integer
        Return charCount * 2
    End Function

    Public Overrides Function GetMaxCharCount(ByVal byteCount As Integer) As Integer
        ' Assume all autotrans stuff -> every 6 bytes can become, say, 60 characters -> bytes * 10
        ' Assume all elements -> every 2 bytes can become <Element: ElementName> (say, 30 characters,
        '  given that ElementName (and possibly Element:) should be localizable) -> bytes * 15.
        Return byteCount * 15
    End Function

    Friend Function GetConversionTable(ByVal Table As Byte) As BinaryReader
        If FFXIEncoding.ConversionTables(Table) Is Nothing Then
            Dim ResourceStream As Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream([String].Format("FFXIMemory.{0:X2}xx.dat", Table))
            If ResourceStream IsNot Nothing Then
                FFXIEncoding.ConversionTables(Table) = New BinaryReader(ResourceStream)
            End If
        End If
        Return TryCast(FFXIEncoding.ConversionTables(Table), BinaryReader)
    End Function

    Private Function GetTableEntry(ByVal Table As Byte, ByVal Entry As Byte) As UShort
        Dim BR As BinaryReader = Me.GetConversionTable(Table)
        If BR IsNot Nothing Then
            BR.BaseStream.Seek(2 * Entry, SeekOrigin.Begin)
            Return BR.ReadUInt16()
        End If
        Return &HFFFF
    End Function

#End Region

#Region "Encoding"

    Private Function EncodeSpecialMarker(ByVal Marker As String) As Byte()
        If Marker.StartsWith("BAD CHAR:") Then
            Dim HexBytes As String = Marker.Substring(9).Trim()
            If HexBytes.Length > 0 AndAlso (HexBytes.Length Mod 2) = 0 Then
                Try
                    Dim EncodedBadChar As Byte() = New Byte(HexBytes.Length \ 2 - 1) {}
                    For i As Integer = 0 To EncodedBadChar.Length - 1
                        EncodedBadChar(i) = Byte.Parse(HexBytes.Substring(2 * i, 2), NumberStyles.HexNumber)
                    Next
                    Return EncodedBadChar
                Catch
                End Try
            End If
        ElseIf Marker.StartsWith("AutoTrans:") Then
            Dim Result As Byte() = New Byte() {&HEF, &H0}
            Select Case Marker.Substring(8).Trim()
                Case "Start"
                    Result(1) = &H27
                    Exit Select
                Case "End"
                    Result(1) = &H28
                    Exit Select
            End Select
            If Result(1) <> &H0 Then
                Return Result
            End If
        ElseIf Marker.StartsWith("Element:") Then
            Dim Result As Byte() = New Byte() {&HEF, &H0}
            Try
                Result(1) = CByte([Enum].Parse(GetType(Element), Marker.Substring(8).Trim()))
                Result(1) += &H1F
            Catch
            End Try
            If Result(1) >= &H1F AndAlso Result(1) <= &H26 Then
                Return Result
            End If
        ElseIf Marker.StartsWith("[") Then
            Dim CloseBracket As Integer = Marker.IndexOf("]"c, 1)
            If CloseBracket > 0 Then
                Dim HexID As String = Marker.Substring(1, CloseBracket - 1)
                Try
                    Dim ResourceID As UInteger = UInteger.Parse(HexID, NumberStyles.HexNumber)
                    Dim EncodedResourceString As Byte() = New Byte(5) {}
                    EncodedResourceString(5) = &HFD
                    EncodedResourceString(4) = CByte(ResourceID And &HFF)
                    ResourceID >>= 8
                    EncodedResourceString(3) = CByte(ResourceID And &HFF)
                    ResourceID >>= 8
                    EncodedResourceString(2) = CByte(ResourceID And &HFF)
                    ResourceID >>= 8
                    EncodedResourceString(1) = CByte(ResourceID And &HFF)
                    ResourceID >>= 8
                    EncodedResourceString(0) = &HFD
                    Return EncodedResourceString
                Catch
                End Try
            End If
        End If
        ' No match with one of our special marker formats => let GetBytes() do regular processing
        Return Nothing
    End Function

    Private Function FindTableEntry(ByVal C As Char) As UShort
        ' Check main table, branching off to other tables if main table indicates a valid lead byte
        Dim MainBR As BinaryReader = Me.GetConversionTable(&H0)
        If MainBR IsNot Nothing Then
            MainBR.BaseStream.Seek(0, SeekOrigin.Begin)
            For i As UShort = 0 To &HFF
                Dim MainEntry As UShort = MainBR.ReadUInt16()
                If MainEntry = CUShort(AscW(C)) Then
                    ' match found
                    Return i
                ElseIf MainEntry = &HFFFE Then
                    ' valid lead byte
                    Dim SubBR As BinaryReader = Me.GetConversionTable(CByte(i))
                    If SubBR IsNot Nothing Then
                        SubBR.BaseStream.Seek(0, SeekOrigin.Begin)
                        For j As UShort = &H0 To &HFF
                            If SubBR.ReadUInt16() = CUShort(AscW(C)) Then
                                ' match found
                                Return CUShort((i << 8) + j)
                            End If
                        Next
                    End If
                End If
            Next
        End If
        Return &HFFFF
        ' no such entry in conversion tables => cannot be encoded
    End Function

    Public Overrides Function GetBytes(ByVal chars As Char(), ByVal index As Integer, ByVal count As Integer) As Byte()
        Dim EncodedBytes As New ArrayList()
        For pos As Integer = index To index + (count - 1)
            If chars(pos) = FFXIEncoding.SpecialMarkerStart Then
                ' Potential special string
                Dim endpos As Integer = pos + 1
                While endpos < index + count AndAlso chars(endpos) <> FFXIEncoding.SpecialMarkerEnd
                    endpos += 1
                End While
                If endpos < index + count Then
                    ' valid end marker found => parse
                    Dim EncodedMarker As Byte() = Me.EncodeSpecialMarker(New String(chars, pos + 1, endpos - pos - 1))
                    If EncodedMarker IsNot Nothing Then
                        EncodedBytes.AddRange(EncodedMarker)
                        pos = endpos
                        Continue For
                    End If
                End If
            End If
            Dim TableEntry As UShort = Me.FindTableEntry(chars(pos))
            If TableEntry <> &HFFFF Then
                If TableEntry > &HFF Then
                    EncodedBytes.Add(CByte((TableEntry And &HFF00) >> 8))
                End If
                EncodedBytes.Add(CByte(TableEntry And &HFF))
            End If
        Next
        Return DirectCast(EncodedBytes.ToArray(GetType(Byte)), Byte())
    End Function

    Public Overrides Function GetBytes(ByVal chars As Char(), ByVal charIndex As Integer, ByVal charCount As Integer, ByVal bytes As Byte(), ByVal byteIndex As Integer) As Integer
        Dim EncodedBytes As Byte() = Me.GetBytes(chars, charIndex, charCount)
        Array.Copy(EncodedBytes, 0, bytes, byteIndex, EncodedBytes.Length)
        Return EncodedBytes.Length
    End Function

#End Region

#Region "Decoding"

    Public Overrides Function GetString(ByVal bytes As Byte()) As String
        Return Me.GetString(bytes, bytes.GetLowerBound(0), 1 + (bytes.GetUpperBound(0) - bytes.GetLowerBound(0)))
    End Function

    Public Overrides Function GetString(ByVal bytes As Byte(), ByVal index As Integer, ByVal count As Integer) As String
        Dim DecodedString As String = [String].Empty
        For pos As Integer = index To index + (count - 1)
            ' FFXI Extension: Elemental symbols
            If bytes(pos) = &HEF AndAlso (pos + 1) < (index + count) AndAlso bytes(pos + 1) >= &H1F AndAlso bytes(pos + 1) <= &H26 Then
                DecodedString += [String].Format("{0}Element: {1}{2}", FFXIEncoding.SpecialMarkerStart, [Enum].Parse(GetType(Element), bytes(System.Threading.Interlocked.Increment(pos)) - &H1F), FFXIEncoding.SpecialMarkerEnd)
                Continue For
            End If
            ' FFXI Extension: Open/Close AutoTranslator Text
            If bytes(pos) = &HEF AndAlso (pos + 1) < (index + count) AndAlso bytes(pos + 1) >= &H27 AndAlso bytes(pos + 1) <= &H28 Then
                DecodedString += FFXIEncoding.SpecialMarkerStart
                DecodedString += "AutoTrans: "
                Select Case bytes(System.Threading.Interlocked.Increment(pos))
                    Case &H27
                        DecodedString += "Start"
                        Exit Select
                    Case &H28
                        DecodedString += "End"
                        Exit Select
                End Select
                DecodedString += FFXIEncoding.SpecialMarkerEnd
                Continue For
            End If
            ' FFXI Extension: Resource Text (Auto-Translator/Item/Key Item)
            If bytes(pos) = &HFD AndAlso pos + 5 < index + count AndAlso bytes(pos + 5) = &HFD Then
                Dim ResourceID As UInteger = 0
                ResourceID <<= 8
                ResourceID += bytes(pos + 1)
                ResourceID <<= 8
                ResourceID += bytes(pos + 2)
                ResourceID <<= 8
                ResourceID += bytes(pos + 3)
                ResourceID <<= 8
                ResourceID += bytes(pos + 4)
                DecodedString += [String].Format("{0}[{1:X8}] {2}{3}", FFXIEncoding.SpecialMarkerStart, ResourceID, FFXIResourceManager.GetResourceString(ResourceID), FFXIEncoding.SpecialMarkerEnd)
                pos += 5
                Continue For
            End If
            ' Default behaviour - use table
            Dim DecodedChar As UShort = Me.GetTableEntry(0, bytes(pos))
            If DecodedChar = &HFFFE Then
                ' Possible Lead Byte
                If pos + 1 < index + count Then
                    Dim Table As Byte = bytes(System.Math.Max(System.Threading.Interlocked.Increment(pos), pos - 1))
                    DecodedChar = Me.GetTableEntry(Table, bytes(pos))
                    If DecodedChar = &HFFFF Then
                        DecodedString += [String].Format("{0}BAD CHAR: {1:X2}{2:X2}{3}", FFXIEncoding.SpecialMarkerStart, Table, bytes(pos), FFXIEncoding.SpecialMarkerEnd)
                    Else
                        DecodedString += ChrW(DecodedChar)
                    End If
                Else
                    DecodedString += [String].Format("{0}BAD CHAR: {1:X2}{2}", FFXIEncoding.SpecialMarkerStart, bytes(pos), FFXIEncoding.SpecialMarkerEnd)
                End If
            ElseIf DecodedChar = &HFFFF Then
                DecodedString += [String].Format("{0}BAD CHAR: {1:X2}{2}", FFXIEncoding.SpecialMarkerStart, bytes(pos), FFXIEncoding.SpecialMarkerEnd)
            Else
                DecodedString += ChrW(DecodedChar)
            End If
        Next
        Return DecodedString
    End Function

    Public Overrides Function GetChars(ByVal bytes As Byte(), ByVal index As Integer, ByVal count As Integer) As Char()
        Return Me.GetString(bytes, index, count).ToCharArray()
    End Function

    Public Overrides Function GetChars(ByVal bytes As Byte(), ByVal byteIndex As Integer, ByVal byteCount As Integer, ByVal chars As Char(), ByVal charIndex As Integer) As Integer
        Dim DecodedChars As Char() = Me.GetChars(bytes, byteIndex, byteCount)
        Array.Copy(DecodedChars, 0, chars, charIndex, DecodedChars.Length)
        Return DecodedChars.Length
    End Function

#End Region

End Class
