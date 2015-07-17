Public Interface IWatcher
    Property Type As MemoryScanner.WatcherTypes
    Sub MobListUpdated(ByVal Mobs As MobList)
    Sub MobStatusUpdated(ByVal Mob As MobData, ByVal Status As MobList.MobStatus)
    Sub ZoneUpdated(ByVal LastZone As Short, ByVal NewZone As Short)
    Sub PartyListUpdated(ByVal Members As String())
    Sub NewChatLine(ByVal Line As MemoryScanner.ChatLine)
    Sub VNMLocationUpdated(ByVal Direction As Direction, ByVal Distance As Integer)
    Sub PositionUpdated(ByVal Position As MemoryScanner.Point3D)
End Interface
