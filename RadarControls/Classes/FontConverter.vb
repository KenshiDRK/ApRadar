Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class FontConverter
    Public Shared Function ToBase64String(ByVal font As Drawing.Font) As String
        Try
            Using stream As New MemoryStream()
                Dim formatter As New BinaryFormatter()
                formatter.Serialize(stream, font)
                Return Convert.ToBase64String(stream.ToArray())
            End Using
        Catch
        End Try
        Return Nothing
    End Function
    Public Shared Function FromBase64String(ByVal font As String) As Drawing.Font
        Try
            Using stream As New MemoryStream(Convert.FromBase64String(font))
                Dim formatter As New BinaryFormatter()
                Return formatter.Deserialize(stream)
            End Using
        Catch
        End Try
        Return Nothing
    End Function
End Class
