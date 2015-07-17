Imports System.Xml.Serialization
Imports System.IO

Public Class Serializer
    Public Shared Function Serialize(Of T)(ByVal Data As T) As Byte()
        Dim ms As New MemoryStream()
        Dim xs As New XmlSerializer(GetType(T))
        xs.Serialize(ms, Data)
        Dim b As Byte() = ms.ToArray
        ms.Close()
        ms.Dispose()
        Return b
    End Function

    Public Shared Sub SerializeToXML(Of T)(ByVal Path As String, ByVal Data As T)
        Dim fs As New FileStream(Path, FileMode.Create)
        Dim xs As New XmlSerializer(GetType(T))
        xs.Serialize(fs, Data)
        fs.Flush()
        fs.Close()
        fs.Dispose()
    End Sub

    Public Shared Function Deserialize(Of T)(ByVal Data As Byte()) As T
        Dim ret As T
        Using ms As New MemoryStream(Data)
            Dim xs As New XmlSerializer(GetType(T))
            ret = CType(xs.Deserialize(ms), T)
            ms.Close()
        End Using
        Return ret
    End Function

    Public Shared Function Deserialize(Of T)(ByVal Data As Stream) As T
        Dim xs As New XmlSerializer(GetType(T))
        Dim ret As T = CType(xs.Deserialize(Data), T)
        Data.Close()
        Data.Dispose()
        Return ret
    End Function

    Public Shared Function DeserializeFromXML(Of T)(ByVal Path As String) As T
        Dim fs As New FileStream(Path, FileMode.Open)
        Dim xs As New XmlSerializer(GetType(T))
        Dim ret As T = CType(xs.Deserialize(fs), T)
        fs.Close()
        fs.Dispose()
        Return ret
    End Function
End Class
