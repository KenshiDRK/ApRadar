Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Graphic
    Inherits Thing

    Public Sub New()
        ' Fill Thing helpers
        Me.IconField_ = "image"
        ' Clear fields
        Me.Clear()
    End Sub

    Public Overrides Function ToString() As String
        Return [String].Format("[{0}] {1} ({2}, {3}x{4})", Me.Category_, Me.ID_, Me.Format_, Me.Width_.GetValueOrDefault(0), Me.Height_.GetValueOrDefault(0))
    End Function

    Public Shared ReadOnly Property AllFields() As List(Of String)
        Get
            Return New List(Of String)(New String() {"format", "flag", "category", "id", "width", "height", _
             "planes", "bits", "compression", "size", "horizontal-resolution", "vertical-resolution", _
             "used-colors", "important-colors", "image"})
        End Get
    End Property

    Public Overrides Function GetAllFields() As List(Of String)
        Return Graphic.AllFields
    End Function

    Public Function Read(ByVal BR As BinaryReader) As Boolean
        Try
            Me.Flag_ = BR.ReadByte()
            If Me.Flag_ <> &H91 AndAlso Me.Flag_ <> &HA1 AndAlso Me.Flag_ <> &HB1 Then
                ' Accuracy unknown
                Me.Clear()
                Return False
            End If
            ' Assumes that BR is set to ASCII encoding
            Me.Category_ = New String(BR.ReadChars(8)).TrimEnd(" "c)
            Me.ID_ = New String(BR.ReadChars(8)).TrimEnd(" "c)
            ' Read BITMAPINFO structure length
            If BR.ReadUInt32() <> 40 Then
                Me.Clear()
                Return False
            End If
            ' Read BITMAPINFO structure
            Me.Width_ = BR.ReadInt32()
            Me.Height_ = BR.ReadInt32()
            Me.Planes_ = BR.ReadUInt16()
            Me.BitCount_ = BR.ReadUInt16()
            Me.Compression_ = BR.ReadUInt32()
            Me.ImageSize_ = BR.ReadUInt32()
            Me.HorizontalResolution_ = BR.ReadUInt32()
            Me.VerticalResolution_ = BR.ReadUInt32()
            Me.UsedColors_ = BR.ReadUInt32()
            Me.ImportantColors_ = BR.ReadUInt32()
            ' Sanity check on the values in the structure
            If Me.Width_ < 0 OrElse Me.Width_ > 16 * 1024 OrElse Me.Height_ < 0 OrElse Me.Height_ > 16 * 1024 OrElse Me.Planes_ <> 1 Then
                Me.Clear()
                Return False
            End If
            If Me.Flag_ = &HA1 Then
                ' Assume DirectX texture
                Me.ReadDXT(BR)
            ElseIf Me.Flag_ = &H91 OrElse Me.Flag_ = &HB1 Then
                ' Bitmap
                Me.ReadBitmap(BR)
            End If
        Catch
            Me.Clear()
        End Try
        Return (Me.Image_ IsNot Nothing)
    End Function

#Region "Data Fields"

    Private Format_ As [String]
    Private Flag_ As System.Nullable(Of Byte)
    Private Category_ As [String]
    Private ID_ As [String]
    Private Width_ As System.Nullable(Of Integer)
    Private Height_ As System.Nullable(Of Integer)
    Private Planes_ As System.Nullable(Of UShort)
    Private BitCount_ As System.Nullable(Of UShort)
    Private Compression_ As System.Nullable(Of UInteger)
    Private ImageSize_ As System.Nullable(Of UInteger)
    Private HorizontalResolution_ As System.Nullable(Of UInteger)
    Private VerticalResolution_ As System.Nullable(Of UInteger)
    Private UsedColors_ As System.Nullable(Of UInteger)
    Private ImportantColors_ As System.Nullable(Of UInteger)
    Private Image_ As Image

#End Region

#Region "Thing Members"

    Public Overrides Function HasField(ByVal Field As String) As Boolean
        Select Case Field
            ' Objects
            Case "category"
                Return (Me.Category_ IsNot Nothing)
            Case "format"
                Return (Me.Format_ IsNot Nothing)
            Case "id"
                Return (Me.ID_ IsNot Nothing)
            Case "image"
                Return (Me.Image_ IsNot Nothing)
                ' Nullables
            Case "bits"
                Return Me.BitCount_.HasValue
            Case "compression"
                Return Me.Compression_.HasValue
            Case "flag"
                Return Me.Flag_.HasValue
            Case "height"
                Return Me.Height_.HasValue
            Case "horizontal-resolution"
                Return Me.HorizontalResolution_.HasValue
            Case "important-colors"
                Return Me.ImportantColors_.HasValue
            Case "planes"
                Return Me.Planes_.HasValue
            Case "size"
                Return Me.ImageSize_.HasValue
            Case "used-colors"
                Return Me.UsedColors_.HasValue
            Case "vertical-resolution"
                Return Me.VerticalResolution_.HasValue
            Case "width"
                Return Me.Width_.HasValue
            Case Else
                Return False
        End Select
    End Function

    Public Overrides Function GetFieldText(ByVal Field As String) As String
        Select Case Field
            ' Objects
            Case "category"
                Return Me.Category_
            Case "format"
                Return Me.Format_
            Case "id"
                Return Me.ID_
            Case "image"
                Return ""
                ' Nullables
            Case "bits"
                Return (If(Not Me.BitCount_.HasValue, [String].Empty, Me.BitCount_.Value.ToString()))
            Case "compression"
                Return (If(Not Me.Compression_.HasValue, [String].Empty, Me.Compression_.Value.ToString()))
            Case "flag"
                Return (If(Not Me.Flag_.HasValue, [String].Empty, [String].Format("{0:X2}", Me.Flag_.Value)))
            Case "height"
                Return (If(Not Me.Height_.HasValue, [String].Empty, Me.Height_.Value.ToString()))
            Case "horizontal-resolution"
                Return (If(Not Me.HorizontalResolution_.HasValue, [String].Empty, Me.HorizontalResolution_.Value.ToString()))
            Case "important-colors"
                Return (If(Not Me.ImportantColors_.HasValue, [String].Empty, Me.ImportantColors_.Value.ToString()))
            Case "planes"
                Return (If(Not Me.Planes_.HasValue, [String].Empty, Me.Planes_.Value.ToString()))
            Case "size"
                Return (If(Not Me.ImageSize_.HasValue, [String].Empty, Me.ImageSize_.Value.ToString()))
            Case "used-colors"
                Return (If(Not Me.UsedColors_.HasValue, [String].Empty, Me.UsedColors_.Value.ToString()))
            Case "vertical-resolution"
                Return (If(Not Me.VerticalResolution_.HasValue, [String].Empty, Me.VerticalResolution_.Value.ToString()))
            Case "width"
                Return (If(Not Me.Width_.HasValue, [String].Empty, Me.Width_.Value.ToString()))
            Case Else
                Return Nothing
        End Select
    End Function

    Public Overrides Function GetFieldValue(ByVal Field As String) As Object
        Select Case Field
            ' Objects
            Case "category"
                Return Me.Category_
            Case "format"
                Return Me.Format_
            Case "id"
                Return Me.ID_
            Case "image"
                Return Me.Image_
                ' Nullables
            Case "bits"
                Return (If(Not Me.BitCount_.HasValue, Nothing, DirectCast(Me.BitCount_.Value, Object)))
            Case "compression"
                Return (If(Not Me.Compression_.HasValue, Nothing, DirectCast(Me.Compression_.Value, Object)))
            Case "flag"
                Return (If(Not Me.Flag_.HasValue, Nothing, DirectCast(Me.Flag_.Value, Object)))
            Case "height"
                Return (If(Not Me.Height_.HasValue, Nothing, DirectCast(Me.Height_.Value, Object)))
            Case "horizontal-resolution"
                Return (If(Not Me.HorizontalResolution_.HasValue, Nothing, DirectCast(Me.HorizontalResolution_.Value, Object)))
            Case "important-colors"
                Return (If(Not Me.ImportantColors_.HasValue, Nothing, DirectCast(Me.ImportantColors_.Value, Object)))
            Case "planes"
                Return (If(Not Me.Planes_.HasValue, Nothing, DirectCast(Me.Planes_.Value, Object)))
            Case "size"
                Return (If(Not Me.ImageSize_.HasValue, Nothing, DirectCast(Me.ImageSize_.Value, Object)))
            Case "used-colors"
                Return (If(Not Me.UsedColors_.HasValue, Nothing, DirectCast(Me.UsedColors_.Value, Object)))
            Case "vertical-resolution"
                Return (If(Not Me.VerticalResolution_.HasValue, Nothing, DirectCast(Me.VerticalResolution_.Value, Object)))
            Case "width"
                Return (If(Not Me.Width_.HasValue, Nothing, DirectCast(Me.Width_.Value, Object)))
            Case Else
                Return Nothing
        End Select
    End Function

    Protected Overrides Sub LoadField(ByVal Field As String, ByVal Node As System.Xml.XmlElement)
        Select Case Field
            Case "bits"
                Me.BitCount_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "category"
                Me.Category_ = Me.LoadTextField(Node)
                Exit Select
            Case "compression"
                Me.Compression_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "flag"
                Me.Flag_ = CByte(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "format"
                Me.Format_ = Me.LoadTextField(Node)
                Exit Select
            Case "height"
                Me.Height_ = CInt(Me.LoadSignedIntegerField(Node))
                Exit Select
            Case "horizontal-resolution"
                Me.HorizontalResolution_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "id"
                Me.ID_ = Me.LoadTextField(Node)
                Exit Select
            Case "important-colors"
                Me.ImportantColors_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "planes"
                Me.Planes_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "size"
                Me.ImageSize_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "used-colors"
                Me.UsedColors_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "vertical-resolution"
                Me.VerticalResolution_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "width"
                Me.Width_ = CInt(Me.LoadSignedIntegerField(Node))
                Exit Select
            Case "image"
                Me.Image_ = Me.LoadImageField(Node)
                If Me.Image_ IsNot Nothing Then
                    ' Fill in a few other fields if needed
                    If Not Me.Width_.HasValue Then
                        Me.Width_ = Me.Image_.Width
                    End If
                    If Not Me.Height_.HasValue Then
                        Me.Height_ = Me.Image_.Height
                    End If
                    If Not Me.VerticalResolution_.HasValue Then
                        Me.VerticalResolution_ = CUInt(Me.Image_.VerticalResolution)
                    End If
                    If Not Me.HorizontalResolution_.HasValue Then
                        Me.HorizontalResolution_ = CUInt(Me.Image_.HorizontalResolution)
                    End If
                End If
                Exit Select
        End Select
    End Sub

    Public Overrides Sub Clear()
        ' Dispose of the fields if needed
        If Me.Image_ IsNot Nothing Then
            Me.Image_.Dispose()
        End If
        ' Null them
        Me.BitCount_ = Nothing
        Me.Category_ = Nothing
        Me.Compression_ = Nothing
        Me.Flag_ = Nothing
        Me.Format_ = Nothing
        Me.Height_ = Nothing
        Me.HorizontalResolution_ = Nothing
        Me.ID_ = Nothing
        Me.ImageSize_ = Nothing
        Me.Image_ = Nothing
        Me.ImportantColors_ = Nothing
        Me.Planes_ = Nothing
        Me.UsedColors_ = Nothing
        Me.VerticalResolution_ = Nothing
        Me.Width_ = Nothing
    End Sub
#End Region

#Region "Image Reading Subroutines"

    Private Sub ReadDXT(ByVal BR As BinaryReader)
        Dim FourCCArray As Char() = BR.ReadChars(4)
        Array.Reverse(FourCCArray)
        Dim FourCC As String = New String(FourCCArray).TrimEnd(ControlChars.NullChar)
        ' Currently, only the DirectX texture format is (partially) supported
        If Not FourCC.StartsWith("DXT") OrElse Me.Height_ Mod 4 <> 0 OrElse Me.Width_ Mod 4 <> 0 Then
            Me.Clear()
            Return
        End If
        Me.Format_ = "Direct X Texture"
        Dim TexelBlockCount As Integer = (Me.Height_.Value \ 4) * (Me.Width_.Value \ 4)
        ' 4x4 blocks
        BR.ReadUInt64()
        ' Unknown
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim PF As PixelFormat
        If FourCC = "DXT2" OrElse FourCC = "DXT4" Then
            ' These have premultiplied RGB values
            PF = PixelFormat.Format32bppPArgb
        Else
            PF = PixelFormat.Format32bppArgb
        End If
        Dim BM As New Bitmap(Me.Width_.Value, Me.Height_.Value, PF)
        For i As Integer = 0 To TexelBlockCount - 1
            Dim TexelBlock As Color() = Me.ReadTexelBlock(BR, FourCC)
            For j As Integer = 0 To 15
                BM.SetPixel(x + (j Mod 4), y + (j - (j Mod 4)) \ 4, TexelBlock(j))
            Next
            x += 4
            If x >= Me.Width_ Then
                y += 4
                x = 0
            End If
        Next
        Me.Image_ = BM
    End Sub

    Private Sub ReadBitmap(ByVal BR As BinaryReader)
        If Me.Flag_ = &HB1 Then
            BR.ReadInt32()
        End If
        ' Unknown - always seems to be 10 (0x0000000A)
        Dim BM As New Bitmap(Me.Width_.Value, Me.Height_.Value, PixelFormat.Format32bppArgb)
        Me.Format_ = "BitMap"
        Dim PixelCount As Integer = Me.Height_.Value * Me.Width_.Value
        If Me.BitCount_ = 8 Then
            ' 8-bit, with palette
            Dim Palette As Color() = New Color(255) {}
            For i As Integer = 0 To 255
                Palette(i) = Graphic.ReadColor(BR, 32)
            Next
            Dim BitFields As Byte() = BR.ReadBytes(PixelCount)
            For i As Integer = 0 To PixelCount - 1
                Dim x As Integer = (i Mod Me.Width_.Value)
                Dim y As Integer = Me.Height_.Value - 1 - ((i - x) \ Me.Width_.Value)
                BM.SetPixel(x, y, Palette(BitFields(i)))
            Next
        Else
            For i As Integer = 0 To PixelCount - 1
                Dim x As Integer = (i Mod Me.Width_.Value)
                Dim y As Integer = Me.Height_.Value - 1 - ((i - x) \ Me.Width_.Value)
                BM.SetPixel(x, y, Graphic.ReadColor(BR, Me.BitCount_.Value))
            Next
        End If
        Me.Image_ = BM
    End Sub

    Private Function ReadTexelBlock(ByVal BR As BinaryReader, ByVal FourCC As String) As Color()
        Dim AlphaBlock As ULong = 0
        If FourCC = "DXT2" OrElse FourCC = "DXT3" OrElse FourCC = "DXT4" OrElse FourCC = "DXT5" Then
            AlphaBlock = BR.ReadUInt64()
        ElseIf FourCC <> "DXT1" Then
            Return Nothing
        End If
        Dim C0 As UShort = BR.ReadUInt16()
        Dim C1 As UShort = BR.ReadUInt16()
        Dim Colors As Color() = New Color(3) {}
        Colors(0) = Graphic.DecodeRGB565(C0)
        Colors(1) = Graphic.DecodeRGB565(C1)
        If C0 > C1 OrElse FourCC <> "DXT1" Then
            ' opaque, 4-color
            Colors(2) = Color.FromArgb((2 * Colors(0).R + Colors(1).R + 1) \ 3, (2 * Colors(0).G + Colors(1).G + 1) \ 3, (2 * Colors(0).B + Colors(1).B + 1) \ 3)
            Colors(3) = Color.FromArgb((2 * Colors(1).R + Colors(0).R + 1) \ 3, (2 * Colors(1).G + Colors(0).G + 1) \ 3, (2 * Colors(1).B + Colors(0).B + 1) \ 3)
        Else
            ' 1-bit alpha, 3-color
            Colors(2) = Color.FromArgb((Colors(0).R + Colors(1).R) / 2, (Colors(0).G + Colors(1).G) / 2, (Colors(0).B + Colors(1).B) / 2)
            Colors(3) = Color.Transparent
        End If
        Dim CompressedColor As UInteger = BR.ReadUInt32()
        Dim DecodedColors As Color() = New Color(15) {}
        For i As Integer = 0 To 15
            If FourCC = "DXT2" OrElse FourCC = "DXT3" OrElse FourCC = "DXT4" OrElse FourCC = "DXT5" Then
                Dim A As Integer = 255
#If EnableTransparency Then
				If FourCC = "DXT2" OrElse FourCC = "DXT3" Then
					' Seems to be 8 maximum; so treat 8 as 255 and all other values as 3-bit alpha
					A = CInt((AlphaBlock >> (4 * i)) And &Hf)
					If A >= 8 Then
						A = 255
					Else
						A <<= 5
					End If
				Else
					' Interpolated alpha
					Dim Alphas As Integer() = New Integer(7) {}
					Alphas(0) = CByte((AlphaBlock >> 0) And &Hff)
					Alphas(1) = CByte((AlphaBlock >> 8) And &Hff)
					If Alphas(0) > Alphas(1) Then
						Alphas(2) = (Alphas(0) * 6 + Alphas(1) * 1 + 3) \ 7
						Alphas(3) = (Alphas(0) * 5 + Alphas(1) * 2 + 3) \ 7
						Alphas(4) = (Alphas(0) * 4 + Alphas(1) * 3 + 3) \ 7
						Alphas(5) = (Alphas(0) * 3 + Alphas(1) * 4 + 3) \ 7
						Alphas(6) = (Alphas(0) * 2 + Alphas(1) * 5 + 3) \ 7
						Alphas(7) = (Alphas(0) * 1 + Alphas(1) * 6 + 3) \ 7
					Else
						Alphas(2) = (Alphas(0) * 4 + Alphas(1) * 1 + 2) \ 5
						Alphas(3) = (Alphas(0) * 3 + Alphas(1) * 2 + 2) \ 5
						Alphas(4) = (Alphas(0) * 2 + Alphas(1) * 3 + 2) \ 5
						Alphas(5) = (Alphas(0) * 1 + Alphas(1) * 4 + 2) \ 5
						Alphas(6) = 0
						Alphas(7) = 255
					End If
					Dim AlphaMatrix As ULong = (AlphaBlock >> 16) And &HffffffffffffL
					A = Alphas((AlphaMatrix >> (3 * i)) And &H7)
				End If
#End If
                DecodedColors(i) = Color.FromArgb(A, Colors(CompressedColor And &H3))
            Else
                DecodedColors(i) = Colors(CompressedColor And &H3)
            End If
            CompressedColor >>= 2
        Next
        Return DecodedColors
    End Function

#End Region

#Region "Reading/Writing Color Values"

    Public Shared Function ReadColor(ByVal BR As BinaryReader, ByVal BitDepth As Integer) As Color
        Select Case BitDepth
            Case 16
                Return Graphic.DecodeRGB565(BR.ReadUInt16())
            Case 32, 24
                If True Then
                    Dim B As Integer = BR.ReadByte()
                    Dim G As Integer = BR.ReadByte()
                    Dim R As Integer = BR.ReadByte()
                    Dim A As Integer = 255
                    If BitDepth = 32 Then
                        Dim SemiAlpha As Byte = BR.ReadByte()
                        If SemiAlpha < &H80 Then
                            A = 2 * SemiAlpha
                        End If
                    End If
                    Return Color.FromArgb(A, R, G, B)
                End If
            Case 8
                If True Then
                    Dim GrayScale As Integer = BR.ReadByte()
                    Return Color.FromArgb(GrayScale, GrayScale, GrayScale)
                End If
            Case Else
                Return Color.HotPink
        End Select
    End Function

    Private Shared Function DecodeRGB565(ByVal C As UShort) As Color
        Dim R As Short = CShort((C And &HF800) >> 8)
        Dim G As Short = CShort((C And &H7E0) >> 3)
        Dim B As Short = CShort((C And &H1F) << 3)
        Return Color.FromArgb(R, G, B)
    End Function

    Public Shared Sub WriteColor(ByVal BW As BinaryWriter, ByVal C As Color, ByVal BitDepth As Integer)
        Select Case BitDepth
            Case 32, 24
                BW.Write(C.B)
                BW.Write(C.G)
                BW.Write(C.R)
                If BitDepth = 32 Then
                    BW.Write(CByte((CInt(C.A) + 1) \ 2))
                End If
                Exit Select
            Case 16
                BW.Write(Graphic.EncodeRGB565(C))
                Exit Select
            Case 8
                BW.Write(CByte((C.R + C.G + C.B) / 3))
                Exit Select
        End Select
    End Sub

    Private Shared Function EncodeRGB565(ByVal C As Color) As UShort
        Dim ColorValue As UShort = 0
        ColorValue = ColorValue Or CUShort((C.R >> 3) And &H1F)
        ColorValue <<= 6
        ColorValue = ColorValue Or CUShort((C.G >> 2) And &H3F)
        ColorValue <<= 5
        ColorValue = ColorValue Or CUShort((C.B >> 3) And &H1F)
        Return ColorValue
    End Function

#End Region

End Class
