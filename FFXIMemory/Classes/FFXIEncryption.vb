Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports System.Text


Public NotInheritable Class FFXIEncryption

    Private Shared Function Rotate(ByVal B As Byte, ByVal ShiftSize As Integer) As Byte
        Return CByte((B >> ShiftSize) Or (B << (8 - ShiftSize)))
    End Function

    Public Shared Function Rotate(ByVal Data As IList(Of Byte), ByVal ShiftSize As Byte) As Boolean
        Return FFXIEncryption.Rotate(Data, 0, Data.Count, ShiftSize)
    End Function

    Public Shared Function Rotate(ByVal Data As IList(Of Byte), ByVal Offset As Integer, ByVal Size As Integer, ByVal ShiftSize As Byte) As Boolean
        If ShiftSize < 1 OrElse ShiftSize > 8 Then
            Return False
        End If
        For i As Integer = 0 To Size - 1
            Data(Offset + i) = FFXIEncryption.Rotate(Data(Offset + i), ShiftSize)
        Next
        Return True
    End Function

    Private Shared Function CountBits(ByVal B As Byte) As Integer
        Dim Count As Integer = 0
        While B <> 0
            If (B And &H1) <> 0 Then
                Count += 1
            End If
            B >>= 1
        End While
        Return Count
    End Function

    Private Shared Function GetTextShiftSize(ByVal Data As IList(Of Byte), ByVal Offset As Integer, ByVal Size As Integer) As Byte
        If Size < 2 Then
            Return 0
        End If
        If Data(Offset + 0) = 0 AndAlso Data(Offset + 1) = 0 Then
            Return 0
        End If
        ' This is the heuristic that ffxitool uses to determine the shift size - it makes absolutely no
        ' sense to me, but it works; I suppose the author of ffxitool reverse engineered what FFXI does.
        Dim BitCount As Integer = FFXIEncryption.CountBits(Data(Offset + 1)) - FFXIEncryption.CountBits(Data(Offset + 0))
        Select Case Math.Abs(BitCount) Mod 5
            Case 0
                Return 1
            Case 1
                Return 7
            Case 2
                Return 2
            Case 3
                Return 6
            Case 4
                Return 3
        End Select
        Return 0
    End Function

    Private Shared Function GetDataShiftSize(ByVal Data As IList(Of Byte), ByVal Offset As Integer, ByVal Size As Integer) As Byte
        If Size < 13 Then
            Return 0
        End If
        ' This is the heuristic that ffxitool uses to determine the shift size - it makes absolutely no
        ' sense to me, but it works; I suppose the author of ffxitool reverse engineered what FFXI does.
        Dim BitCount As Integer = FFXIEncryption.CountBits(Data(Offset + 2)) - FFXIEncryption.CountBits(Data(Offset + 11)) + FFXIEncryption.CountBits(Data(Offset + 12))
        Select Case Math.Abs(BitCount) Mod 5
            Case 0
                Return 7
            Case 1
                Return 1
            Case 2
                Return 6
            Case 3
                Return 2
            Case 4
                Return 5
        End Select
        Return 0
    End Function

    Public Shared Function DecodeTextBlock(ByVal Data As IList(Of Byte)) As Boolean
        Return FFXIEncryption.DecodeTextBlock(Data, 0, Data.Count)
    End Function

    Public Shared Function DecodeTextBlock(ByVal Data As IList(Of Byte), ByVal Offset As Integer, ByVal Size As Integer) As Boolean
        Return FFXIEncryption.Rotate(Data, Offset, Size, FFXIEncryption.GetTextShiftSize(Data, Offset, Size))
    End Function

    Public Shared Function DecodeDataBlock(ByVal Data As IList(Of Byte)) As Boolean
        Return FFXIEncryption.DecodeDataBlock(Data, 0, Data.Count)
    End Function

    Public Shared Function DecodeDataBlock(ByVal Data As IList(Of Byte), ByVal Offset As Integer, ByVal Size As Integer) As Boolean
        Return FFXIEncryption.Rotate(Data, Offset, Size, FFXIEncryption.GetDataShiftSize(Data, Offset, Size))
    End Function

    Public Shared Function ReadEncodedString(ByVal BR As BinaryReader, ByVal E As Encoding) As String
        Dim LineBytes As New List(Of Byte)()
        ' It's NUL-terminated, BUT we need at least two bytes to determine the shift size - for example, a single space becomes "10 00".
        Do
            LineBytes.Add(BR.ReadByte())
        Loop While LineBytes.Count < 2 OrElse CByte(LineBytes(LineBytes.Count - 1)) <> 0
        FFXIEncryption.DecodeTextBlock(LineBytes)
        Return E.GetString(LineBytes.ToArray()).TrimEnd(ControlChars.NullChar)
    End Function

End Class