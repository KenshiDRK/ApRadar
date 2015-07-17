Imports System.Threading
Public Class Watcher
    Implements IWatcher


#Region " Local Structures "
    Private Structure MobStatus
        Public Mob As MobData
        Public Status As MobList.MobStatus
    End Structure

    Private Structure ZoneChangedArgs
        Public LastZone As Short
        Public NewZone As Short
    End Structure

    Private Structure VNMLocation
        Public Direction As Direction
        Public Distance As Integer
    End Structure
#End Region

#Region " Local Variables "
    Private _sync As SynchronizationContext = SynchronizationContext.Current
#End Region

#Region " Delegates and Events "
    Public Delegate Sub OnNewMobListEventHandler(ByVal Mobs As MobList)
    Public Event NewMobList As OnNewMobListEventHandler
    Public Delegate Sub OnMobStatusChangedEventHandler(ByVal Mob As MobData, ByVal Status As MobList.MobStatus)
    Public Event MobStatusChanged As OnMobStatusChangedEventHandler
    Public Delegate Sub OnZoneChangedEventHandler(ByVal LastZone As Short, ByVal NewZone As Short)
    Public Event ZoneChanged As OnZoneChangedEventHandler
    Public Delegate Sub OnPartyListUpdatedEventHandler(ByVal Members As String())
    Public Event OnPartyListUpdated As OnPartyListUpdatedEventHandler
    Public Delegate Sub OnWatcherTypeChangedEventHandler(ByVal NewType As MemoryScanner.WatcherTypes)
    Public Event OnWatcherTypeChanged As OnWatcherTypeChangedEventHandler
    Public Delegate Sub OnNewChatLineEventHandler(ByVal Line As MemoryScanner.ChatLine)
    Public Event OnNewChatLine As OnNewChatLineEventHandler
    Public Delegate Sub OnVNMLocationUpdatedEventHandler(ByVal Direction As Direction, ByVal Distance As Integer)
    Public Event OnVNMLocationUpdated As OnVNMLocationUpdatedEventHandler
    Public Delegate Sub OnPositionUpdatedEventHandler(ByVal Position As MemoryScanner.Point3D)
    Public Event OnPositionUpdated As OnPositionUpdatedEventHandler
#End Region

#Region " Constructors "
    Public Sub New()
    End Sub

    Public Sub New(ByVal Type As MemoryScanner.WatcherTypes)
        Me.Type = Type
    End Sub
#End Region

#Region " Public Methods "
    Public Sub Init()
        'Dummy method used to initialize the class in a singleton property
    End Sub
#End Region

#Region " IWatcher Implementation "
    Private _type As MemoryScanner.WatcherTypes
    Public Property Type As MemoryScanner.WatcherTypes Implements IWatcher.Type
        Get
            Return _type
        End Get
        Set(ByVal value As MemoryScanner.WatcherTypes)
            _type = value
            RaiseEvent OnWatcherTypeChanged(value)
        End Set
    End Property

    ''' <summary>
    ''' IWatcher MoblistUpdated Implementation
    ''' </summary>
    ''' <param name="Mobs">The Updated mob list</param>
    ''' <remarks></remarks>
    Public Sub MobListUpdated(ByVal Mobs As MobList) Implements IWatcher.MobListUpdated
        Try
            PostNewMobList(Mobs)
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' IWatcher MobStatusUpdated Implementatin
    ''' </summary>
    ''' <param name="Mob">The mob who's status has changed</param>
    ''' <param name="Status">The mobs new status</param>
    ''' <remarks></remarks>
    Public Sub MobStatusUpdated(ByVal Mob As MobData, ByVal Status As MobList.MobStatus) Implements IWatcher.MobStatusUpdated
        PostMobStatus(Mob, Status)
    End Sub

    Public Sub ZoneUpdated(ByVal LastZone As Short, ByVal NewZone As Short) Implements IWatcher.ZoneUpdated
        PostZoneChanged(LastZone, NewZone)
    End Sub

    Public Sub PartyListUpdated(ByVal Members() As String) Implements IWatcher.PartyListUpdated
        PostPartyList(Members)
    End Sub

    Public Sub NewChatLine(ByVal Line As MemoryScanner.ChatLine) Implements IWatcher.NewChatLine
        PostNewChatLine(Line)
    End Sub

    Public Sub VNMLocationUpdated(ByVal Direction As Direction, ByVal Distance As Integer) Implements IWatcher.VNMLocationUpdated
        PostVNMListUpdated(Direction, Distance)
    End Sub

    Public Sub PositionUpdated(ByVal Position As MemoryScanner.Point3D) Implements IWatcher.PositionUpdated
        PostPositionUpdated(Position)
    End Sub
#End Region

#Region " Synchronization Context Methods "
    Public Sub PostNewMobList(ByVal Mobs As MobList)
        _sync.Send(New SendOrPostCallback(AddressOf OnNewMobList), Mobs)
    End Sub

    Private Sub OnNewMobList(ByVal state As Object)
        Dim mobs As MobList = TryCast(state, MobList)
        Try
            If mobs IsNot Nothing Then
                RaiseEvent NewMobList(mobs)
            End If
        Catch ex As Exception
            ''Debug.Print(Ex.Message)
        End Try
    End Sub

    Private Sub PostMobStatus(ByVal mob As MobData, ByVal status As MobList.MobStatus)
        Dim ms As New MobStatus() With {.Mob = mob, .Status = status}
        _sync.Send(New SendOrPostCallback(AddressOf OnMobStatusChanged), ms)
    End Sub

    Private Sub OnMobStatusChanged(ByVal state As Object)
        If state IsNot Nothing Then
            Dim ms As MobStatus = CType(state, MobStatus)
            RaiseEvent MobStatusChanged(ms.Mob, ms.Status)
        End If
    End Sub

    Private Sub PostZoneChanged(ByVal LastZone As Short, ByVal NewZone As Short)
        Dim zce As New ZoneChangedArgs With {.LastZone = LastZone, .NewZone = NewZone}
        _sync.Post(New SendOrPostCallback(AddressOf OnZoneChanged), zce)
    End Sub

    Private Sub OnZoneChanged(ByVal state As Object)
        If state IsNot Nothing Then
            Dim zce As ZoneChangedArgs = CType(state, ZoneChangedArgs)
            RaiseEvent ZoneChanged(zce.LastZone, zce.NewZone)
        End If
    End Sub

    Private Sub PostPartyList(ByVal Members As String())
        _sync.Post(New SendOrPostCallback(AddressOf OnPartyList), Members)
    End Sub

    Private Sub OnPartyList(ByVal state As Object)
        If state IsNot Nothing Then
            RaiseEvent OnPartyListUpdated(CType(state, String()))
        End If
    End Sub

    Private Sub PostNewChatLine(ByVal Line As MemoryScanner.ChatLine)
        _sync.Post(New SendOrPostCallback(AddressOf OnChatLine), Line)
    End Sub

    Private Sub OnChatLine(ByVal state As Object)
        If state IsNot Nothing Then
            RaiseEvent OnNewChatLine(CType(state, MemoryScanner.ChatLine))
        End If
    End Sub

    Private Sub PostVNMListUpdated(ByVal Direction As Direction, ByVal Distance As Integer)
        Dim loc As New VNMLocation With {.Direction = Direction, .Distance = Distance}
        _sync.Post(New SendOrPostCallback(AddressOf OnVNMLocation), loc)
    End Sub

    Private Sub OnVNMLocation(ByVal state As Object)
        If state IsNot Nothing Then
            Dim loc As VNMLocation = CType(state, VNMLocation)
            RaiseEvent OnVNMLocationUpdated(loc.Direction, loc.Distance)
        End If
    End Sub

    Public Sub PostPositionUpdated(ByVal Postition As MemoryScanner.Point3D)
        _sync.Post(New SendOrPostCallback(AddressOf OnPosition), Postition)
    End Sub

    Public Sub OnPosition(ByVal state As Object)
        If state IsNot Nothing Then
            RaiseEvent OnPositionUpdated(CType(state, MemoryScanner.Point3D))
        End If
    End Sub
#End Region
End Class
