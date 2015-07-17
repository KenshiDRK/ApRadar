Imports DataLibrary.CampedMobsDataset
Imports System.Data.Linq

Public Class CampedMobManager
    Private Shared _savePath As String = My.Application.Info.DirectoryPath & "\Data\CampedMobs.dat"
    Private Shared _campedMobs As List(Of CampedMob)
    Private Shared _queueCount = 0

    Public Shared Event DebugEvent(ByVal Location As String, ByVal EventType As String, ByVal Message As String)

    Public Enum EditType
        Update
        Delete
    End Enum

    Public Shared ReadOnly Property CampedMobs() As List(Of CampedMob)
        Get
            If _campedMobs Is Nothing Then
                If IO.File.Exists(_savePath) Then
                    LoadData()
                Else
                    RaiseEvent DebugEvent("Property.CampedMobs", "LoadFileFailed", "Unable to find the camped mob datasource")
                    _campedMobs = New List(Of CampedMob)
                End If
            End If
            Return _campedMobs
        End Get
    End Property

    Private Shared Sub HoldQueue()
        _queueCount += 1
    End Sub

    Private Shared Sub ReleaseQueue()
        If _queueCount > 0 Then
            _queueCount -= 1
        End If
    End Sub

    Private Shared Function QueueHolding() As Boolean
        Return _queueCount > 0
    End Function

    Public Shared Sub AddCampedMob(ByVal cMob As CampedMob)
        Dim t As New Threading.Thread(AddressOf AddCampedMobThread)
        t.Start(cMob)
    End Sub

    Public Shared Sub ModifyCampedMobs(ByVal mob As CampedMob, ByVal Type As EditType)
        Select Case Type
            Case EditType.Update
                Dim t As New Threading.Thread(AddressOf UpdateCampedMobThread)
                t.Start(mob)
            Case EditType.Delete
                'Not Currently implemented
                'Saving all mobs in the file
        End Select
    End Sub

    Private Shared Sub AddCampedMobThread(ByVal mob As CampedMob)
        While QueueHolding()
            System.Threading.Thread.Sleep(100)
        End While
        HoldQueue()
        CampedMobs.Add(mob)
        ReleaseQueue()
    End Sub

    Public Shared Sub UpdateCampedMobThread(ByVal mob As CampedMob)
        While QueueHolding()
            System.Threading.Thread.Sleep(100)
        End While
        HoldQueue()
        Dim row = (From c In CampedMobs Where c.ServerID = mob.ServerID).FirstOrDefault
        If Not row Is Nothing Then
            row.DeathTime = mob.DeathTime
            row.IsDead = mob.IsDead
            row.Name = mob.Name
        End If
        ReleaseQueue()
    End Sub

    Public Shared Sub DeleteCampedMobThread(ByVal ServerID As Long)
        While QueueHolding()
            System.Threading.Thread.Sleep(100)
        End While
        HoldQueue()
        Dim mob = (From c In CampedMobs Where c.ServerID = ServerID).FirstOrDefault
        If Not mob Is Nothing Then
            _campedMobs.Remove(mob)
        End If
        ReleaseQueue()
    End Sub

    Public Shared Function GetCampedMobs() As CampedMob()
        Return CampedMobs.ToArray
    End Function

    Public Shared Sub LoadData()
        If IO.File.Exists(_savePath) Then
            Try
                Dim fs As New IO.StreamReader(_savePath)
                Dim xs As New Xml.Serialization.XmlSerializer(GetType(List(Of CampedMob)))
                _campedMobs = CType(xs.Deserialize(fs), List(Of CampedMob))
                fs.Close()
                fs.Dispose()
            Catch ex As Exception
                RaiseEvent DebugEvent("CampedMobManager.LoadData", "Serialization Error", ex.Message)
                _campedMobs = New List(Of CampedMob)
            End Try
        Else
            _campedMobs = New List(Of CampedMob)
        End If
    End Sub

    Public Shared Sub SaveData()
        Try
            Dim fs As New IO.StreamWriter(_savePath)
            Dim xs As New Xml.Serialization.XmlSerializer(GetType(List(Of CampedMob)))
            xs.Serialize(fs, _campedMobs)
            fs.Flush()
            fs.Close()
            fs.Dispose()
        Catch ex As Exception
            RaiseEvent DebugEvent("CampedMobManager.SaveData()", "Save Error", ex.Message)
        End Try
    End Sub
End Class
