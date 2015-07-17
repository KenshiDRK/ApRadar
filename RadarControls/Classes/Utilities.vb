Imports System.Xml.Serialization
Imports System.IO

Public Class Utilities
    Public Class Serializer
        Public Shared Function SerializeToXml(Of T)(ByVal Path As String, ByVal Data As T) As Boolean
            Try
                Using fs = File.Create(Path)
                    Dim xs As New XmlSerializer(GetType(T))
                    xs.Serialize(fs, Data)
                    fs.Close()
                End Using
                Return True
            Catch ex As Exception
                'Debug.Print(Ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function SerializeToStream(Of T)(ByVal Data As T) As Stream
            Try
                Dim ms As New MemoryStream
                Dim xs As New XmlSerializer(GetType(T))
                xs.Serialize(ms, Data)
                Return ms
            Catch ex As Exception
                'Debug.Print(Ex.Message)
                Return Nothing
            End Try
        End Function

        Public Shared Function DeserializeFromXml(Of T)(ByVal Path As String) As T
            Try
                If IO.File.Exists(Path) Then
                    Dim result As T
                    Using fs = File.OpenRead(Path)
                        Dim xs As New XmlSerializer(GetType(T))
                        result = CType(xs.Deserialize(fs), T)
                        fs.Close()
                    End Using
                    Return result
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Shared Function DeserializeFromStream(Of T)(ByVal InStream As Stream) As T
            Try
                Dim xs As New XmlSerializer(GetType(T))
                Dim result = CType(xs.Deserialize(InStream), T)
                InStream.Close()
                InStream.Dispose()
                Return result
            Catch ex As Exception
                MsgBox(ex.Message)
                Return Nothing
            End Try
        End Function
    End Class

    
End Class
