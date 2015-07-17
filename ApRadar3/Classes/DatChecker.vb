Imports FFXIMemory
Imports System.Threading
Imports System.Text.RegularExpressions
Imports DataLibrary.DataAccess
Imports DataLibrary

Public Class DatChecker
#Region " MEMBER VARIABLES "
    Private _sync As SynchronizationContext = SynchronizationContext.Current
    Private _regexPattern As String = "^[A-Z]+[a-z].*"
#End Region

#Region " DELEGATES "
    Public Delegate Sub ProgressChangedEventHandler(ByVal Progress As Integer)
    Public Delegate Sub CheckCompleteEventHandler(ByVal NewMobs As List(Of Mobs))
    Public Delegate Sub ErrorOccuredHandler(ByVal e As System.Exception)
#End Region

#Region " EVENTS "
    Public Event ProgressChanged As ProgressChangedEventHandler
    Public Event CheckComplete As CheckCompleteEventHandler
    Public Event ErrorOccured As ErrorOccuredHandler

#End Region

#Region " PUBLIC METHODS "
    Public Sub CheckDats()
        Dim t As New Thread(AddressOf CheckDatsThread)
        t.Start()
    End Sub

    Public Sub AddNewMobs(ByVal Mobs As List(Of Mobs))
        Dim t As New Thread(AddressOf AddMobsThread)
        t.Start(Mobs)
    End Sub
#End Region

#Region " PRIVATE METHODS "
    Private Sub CheckDatsThread()
        Try
            Dim addMobs As New List(Of Mobs)
            Dim zones As New Zones
            Dim count As Integer = zones.ZoneList.Count
            Dim index As Integer = 0

            Dim mData As List(Of ZoneMobs)
            Dim mobName As String = String.Empty
            Dim zoneID As Integer
            For Each zone In zones.ZoneList
                mData = zones.GetZoneMobList(zone.ZoneID)
                zoneID = zone.ZoneID
                mobName = String.Empty
                For Each mob In mData
                    'Checkt o make sure that this is a new mob and is a real mob name
                    'We ignore ??? and items that start with _ or all in caps etc
                    If mob.MobName <> mobName AndAlso Regex.Match(mob.MobName, _regexPattern).Success Then
                        mobName = mob.MobName
                        Dim q = (From c In DataLibrary.DataAccess.MobData.Mobs _
                                              Where c.MobName = mobName And c.Zone = zoneID _
                                              Select c).FirstOrDefault
                        If q Is Nothing Then
                            addMobs.Add(New Mobs(zoneID, mobName))
                        End If
                    End If
                Next
                index += 1
                OnProgressChanged(CInt(index / count * 100))
            Next
            OnCheckComplete(addMobs)
        Catch ex As Exception
            OnErrorOccurred(ex)
        End Try
    End Sub

    Private Sub AddMobsThread(ByVal Mobs As List(Of Mobs))
        Dim index As Integer = 0
        Dim count As Integer = Mobs.Count
        For Each mob In Mobs
            Dim newPK = (From c In DataLibrary.DataAccess.MobData.Mobs Select c.MobPK).Max + 1
            Dim m As ApRadarDataSet.MobsRow = DataLibrary.DataAccess.MobData.Mobs.NewRow
            m.MobPK = newPK
            m.MobName = mob.MobName
            m.Zone = mob.ZoneID
            m.Aggressive = False
            m.Links = False
            m.NM = False
            m.FishedUp = False
            m.TracksScent = False
            m.TrueSight = False
            m.TrueSound = False
            m.DetectsHealing = False
            m.DetectsLowHP = False
            m.DetectsMagic = False
            m.DetectsSight = False
            m.DetectsSound = False
            m.NPC = False
            DataLibrary.DataAccess.MobData.Mobs.Rows.Add(m)
            index += 1
            OnProgressChanged(CInt(index / count * 100))
        Next
        DataManager.MobsTableAdapter.Update(DataLibrary.DataAccess.MobData)
        OnCheckComplete(Nothing)
    End Sub

    Private Sub OnProgressChanged(ByVal Progress As Integer)
        _sync.Post(New SendOrPostCallback(AddressOf RaiseProgressChanged), Progress)
    End Sub

    Private Sub RaiseProgressChanged(ByVal state As Object)
        RaiseEvent ProgressChanged(CInt(state))
    End Sub

    Private Sub OnCheckComplete(ByVal NewMobs As List(Of Mobs))
        _sync.Post(New SendOrPostCallback(AddressOf RaiseCheckComplete), NewMobs)
    End Sub

    Private Sub RaiseCheckComplete(ByVal state As Object)
        RaiseEvent CheckComplete(CType(state, List(Of Mobs)))
    End Sub

    Private Sub OnErrorOccurred(ByVal ex As Exception)
        _sync.Post(New SendOrPostCallback(AddressOf RaiseErrorOccured), ex)
    End Sub

    Private Sub RaiseErrorOccured(ByVal state As Object)
        RaiseEvent ErrorOccured(CType(state, Exception))
    End Sub
#End Region

#Region " SUBCLASS "
    Public Class Mobs
        Public Sub New(ByVal ZoneID As Short, ByVal MobName As String)
            Me.ZoneID = ZoneID
            Me.MobName = MobName
        End Sub
        Public Property ZoneID() As Short
        Public Property MobName() As String

    End Class
#End Region
End Class
