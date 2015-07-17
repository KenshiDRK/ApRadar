Public Class DownloadFiles
    Public Sub New(ByVal FileName As String)
        Me.FileName = FileName
    End Sub
    Public Property FileName() As String
    Public Property Progress() As Integer
End Class
