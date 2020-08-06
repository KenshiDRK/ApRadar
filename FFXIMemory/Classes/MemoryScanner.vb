﻿Imports System.Threading
Imports System.Text.RegularExpressions

''' <summary>
''' Observer Pattern memory scanning implementation
''' </summary>
''' <remarks></remarks>
Public Class MemoryScanner

#Region " MEMBER VARIABLES "
    Private _memory As Memory
    Private WithEvents _mobs As MobList

    Private ReadOnly _watchers As New List(Of Watcher)()
    Private WithEvents _timer As Timers.Timer
    Private _isScanning As Boolean
    Private Property PointerBlock() As Byte()
    Private _mobAddress As Integer
    Private _mob As MobData
    Private _lastMap As Short = 0
    Private ReadOnly _partyMembers As Dictionary(Of Byte, String)
    Private ReadOnly _syncObj As New Object()
    'Chat Variables
    Private ReadOnly _isVista As Boolean
    Private _logOffsets As Byte()
    Private _chatBase As Integer
    Private _lineCount As Byte
    Private _lastLine As Integer = -1
    Private _logStart As Integer
    Private _logOffsetBytes As Byte()
    Private ReadOnly _lineOffsets As New List(Of Int16)()
    Private _line As Byte()
    Private _lineInfo As ChatLine
    Private _splitChar As Char() = {","c}
    Private _lineText As String
    Private _lineParts As String()
    Private _lastMsgID As Integer
    'VNM VAriables
    Private _lastDirection As Direction = Direction.East
    Private _lastDistance As Integer = -1
    Private _ffxiNeedsReinitialization As Boolean
    Private _lastPosition As Point3D
#End Region

#Region " CHAT STRUCTURES "
    ''' <summary>
    ''' Structure of the chatline
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Structure ChatLine
        Dim ChatType As String
        Dim UK1 As String
        Dim UK2 As String
        Dim color As String
        Dim MsgID As Integer
        Dim GroupMsgID As Integer
        Dim UK3 As String
        Dim UK4 As String
        Dim UK5 As String
        Dim UK6 As String
        Dim UK7 As String
        Dim LineText As String
    End Structure

    ''' <summary>
    ''' Unused atm
    ''' </summary>
    ''' <remarks></remarks>
    Private Structure ThreadItems
        Dim POL As Process
        Dim LineCount As String
        Dim LastLine As Integer
        Dim LogStart As Integer
        Dim LineOffsets As List(Of Int16)
    End Structure
#End Region

#Region " STRUCTURES "
    Public Structure Point3D
        Public X As Single
        Public Y As Single
        Public Z As Single
    End Structure
#End Region

#Region " SHARED METHODS "

    Private Shared _scanner As MemoryScanner
    ''' <summary>
    ''' Singleton property
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Scanner As MemoryScanner
        Get
            If _scanner Is Nothing Then
                _scanner = New MemoryScanner
            End If
            Return _scanner
        End Get
    End Property

    ''' <summary>
    ''' Checks to see if the game object has been initiated and that the game is running
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsGameLoaded As Boolean
        Get
            'Verify that the FFXI objectg has been set and that the POL Process is not null
            If Me.FFXI Is Nothing OrElse Me.FFXI.POL Is Nothing Then
                Return False
            Else
                'If all is loaded and good, check to see if the game has been loaded
                Return Me.FFXI.IsGameLoaded
            End If
        End Get
    End Property
#End Region

#Region " PROPERTIES "
    Private _ffxi As FFXI
    ''' <summary>
    ''' FFXI Property object
    ''' This holds the process info and memory locations 
    ''' and various game info
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FFXI As FFXI
        Get
            Return _ffxi
        End Get
        Set(ByVal value As FFXI)
            _ffxi = value
            _ffxiNeedsReinitialization = True
            If (Not value Is Nothing AndAlso _watchers.Count > 0 AndAlso Not _timer.Enabled) Then
                _timer.Start()
                IsRunning = True
            ElseIf value Is Nothing OrElse _watchers.Count = 0 Then
                _timer.Stop()
                IsRunning = False
            End If
        End Set
    End Property

    ''' <summary>
    ''' My current Zone ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MyID() As Integer

    ''' <summary>
    ''' The Id of my current target
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetID() As Integer

    ''' <summary>
    ''' The id of the current map
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrentMap() As Short

    ''' <summary>
    ''' Boolean used to determine if the scanner is currently 
    ''' running and scanning memory
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsRunning As Boolean

    ''' <summary>
    ''' The different watcher types that are attached
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property AttachedWatcherTypes As WatcherTypes

    Private _refreshRate As Integer = 200
    ''' <summary>
    ''' The scanning rate interval.  The default is 200 ms
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Anything less than 10 causes ridiculous memory consumption 
    ''' and therefore is dropped and 10 is the minimum set vaule.  100ms is 
    ''' an ideal interval for smooth movement and low memory consumption</remarks>
    Public Property RefreshRate As Integer
        Get
            Return _refreshRate
        End Get
        Set(ByVal value As Integer)
            If value < 10 Then
                value = 10
            End If
            _refreshRate = value
            _timer.Interval = value
        End Set
    End Property

#End Region

#Region " ENUM "
    <Flags()>
    Public Enum WatcherTypes
        None
        All
        MobList
        MobStatus
        ZoneChange
        Chat
        VNM
        Position
    End Enum

    ''' <summary>
    ''' Enumeration to hold the chat offset positions
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum ChatOffsets
        OffsetArray = 4
        LastIndexArray = 100
        TotalLineCount = 8
        EndPointer = 12
        BlockLineCount = 240
        LogStart = 244
        LastLogStart = 248
        EndLogOffset = 256
    End Enum
#End Region

#Region " CONSTRUCTOR "
    Public Sub New()
        'Instantiate class level objects
        _timer = New Timers.Timer(Me.RefreshRate)
        _mobs = New MobList()
        _partyMembers = New Dictionary(Of Byte, String)
        If My.Computer.Info.OSVersion >= "6.0" Then
            _isVista = True
        End If
        _lastLine = -1
    End Sub
#End Region

#Region " PUBLIC METHODS "
    ''' <summary>
    ''' Attaches a watcher to the scanners event queue
    ''' </summary>
    ''' <param name="watcher">The watcher to attach to the event queue</param>
    ''' <remarks></remarks>
    Public Sub AttachWatcher(ByVal watcher As Watcher)
        'Attach the watchers TypeChanged event to the internal handler
        AddHandler watcher.OnWatcherTypeChanged, AddressOf Watcher_TypeChanged
        _watchers.Add(watcher)
        'If the scanner thread is not running, start it up
        If Not _timer.Enabled AndAlso FFXI IsNot Nothing Then
            _timer.Start()
            IsRunning = True
        End If
        'Add this type to the attached types
        AttachedWatcherTypes = AttachedWatcherTypes Or watcher.Type
    End Sub

    ''' <summary>
    ''' Detaches a watcher from the event queue
    ''' </summary>
    ''' <param name="watcher">The watcher to detach</param>
    ''' <remarks></remarks>
    Public Sub DetachWatcher(ByVal watcher As Watcher)
        'Remove the watcher
        _watchers.Remove(watcher)
        'Check to see it we have any watchers attached and if not, stop scanning to save resources
        If _watchers.Count = 0 Then
            _timer.Stop()
            IsRunning = False
        End If
        'Since we are not sure if other watchers are using the same type, we clear it out then rebuild it
        AttachedWatcherTypes = WatcherTypes.None
        For Each watcher In _watchers
            AttachedWatcherTypes = AttachedWatcherTypes Or watcher.Type
        Next
    End Sub

    ''' <summary>
    ''' Ends the scanning and detaches all watchers
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EndScanning()
        _watchers.Clear()
        _timer.Stop()
        IsRunning = False
        AttachedWatcherTypes = WatcherTypes.None
    End Sub
#End Region

#Region " PRIVATE METHODS "
    Private Function CheckIfAttachedWatcher(ByVal WatcherType As WatcherTypes) As Boolean
        Return (AttachedWatcherTypes And WatcherType) = WatcherType
    End Function

    ''' <summary>
    ''' Cleans up the Linetext from the chatlog, removing any strange or unknown characters
    ''' </summary>
    ''' <param name="bytes">The bytes to perform clean up on</param>
    ''' <param name="ChatType">The chat line type</param>
    ''' <param name="li">the chatline structure</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function CleanUpString(ByVal bytes As Byte(), ByVal ChatType As String, ByVal li As ChatLine) As String
        Dim bJP As Boolean = False
        Dim pattern As String
        If ChatType <> "ce" OrElse ChatType = "8d" Then
            If _lastMsgID <> li.MsgID Then
                If ChatType = "7b" Then
                    pattern = "[A-Z0-9]|<|>|}|\(|=|ï"
                Else
                    pattern = "[A-Z0-9]|<|>|{|}|\(|=|ï"
                End If

                Do Until Regex.Match(Chr(bytes(0)).ToString, pattern).Success  'bytes(0) = 40 OrElse bytes(0) = 61 OrElse bytes(0) = 60 OrElse bytes(0) = 62 OrElse (bytes(0) >= 65 AndAlso bytes(0) <= 90)
                    Array.Copy(bytes, 1, bytes, 0, bytes.Length - 1)
                Loop
                _lastMsgID = li.MsgID
            Else
                If ChatType = "7b" Then
                    pattern = "[A-Za-z0-9]|<|>|}|\(|=|ï"
                Else
                    pattern = "[A-Za-z0-9]|<|>|{|}|\(|=|ï"
                End If

                Do Until Regex.Match(Chr(bytes(0)).ToString, pattern).Success 'bytes(0) = 40 OrElse bytes(0) = 61 OrElse Chr(bytes(0)) = "{"c _
                    'OrElse Chr(bytes(0)) = "}"c OrElse (bytes(0) >= 65 AndAlso bytes(0) <= 90) OrElse (bytes(0) >= 97 AndAlso bytes(0) <= 122)
                    Array.Copy(bytes, 1, bytes, 0, bytes.Length - 1)
                Loop
            End If
        Else
            Array.Copy(bytes, 4, bytes, 0, bytes.Length - 4)
        End If
        'Clean out any dirty characters 
        Dim b1 As New List(Of Byte)
        For i As Integer = 0 To bytes.GetUpperBound(0)
            'Need to handle the Items in the chat log
            If bytes(i) <> 30 Then
                If bytes(i) = 2 Then
                    b1.Add(1)
                Else
                    b1.Add(bytes(i))
                End If
            End If
        Next
        bytes = b1.ToArray
        b1.Clear()
        b1 = Nothing
        For x As Integer = 0 To bytes.GetUpperBound(0)
            'check for the end of the line and resize the array
            If bytes(x) = 0 Then
                Array.Resize(bytes, x)
                Exit For
            ElseIf bytes(x) = 239 AndAlso bytes(x + 1) = 39 Then 'Look for auto Translate codes
                'Replace with " {"

                bytes(x) = 32
                bytes(x + 1) = 123
            ElseIf bytes(x) = 239 AndAlso bytes(x + 1) = 40 Then 'Closing AT
                'Replace with "} "
                bytes(x) = 125
                bytes(x + 1) = 32
            ElseIf bytes(x) = 127 Then
                bytes(x) = 32
            End If
            If System.Text.Encoding.Default.GetString(bytes).Contains("Moogle : ") Then
            Else
                If bytes(x) > 127 Then
                    bJP = True
                End If
            End If
        Next
        If bJP Then
            Return ConvertToJPText(System.Text.Encoding.Default.GetString(bytes)).TrimEnd("1"c).Replace("  ", " ")
        Else
            Return System.Text.Encoding.Default.GetString(bytes).Replace("  ", " ").TrimEnd("1"c)
        End If
    End Function

    ''' <summary>
    ''' Converts a string to Shift-JIS format
    ''' </summary>
    ''' <param name="source">The source string to convert</param>
    ''' <returns>A string in Shift-JIS format</returns>
    ''' <remarks>This only works if the user has support for far eastern languages installed</remarks>
    Public Shared Function ConvertToJPText(ByVal source As String) As String
        Dim eEncoding = System.Text.Encoding.GetEncoding("shift-jis")
        Dim SourceEncoding = System.Text.Encoding.Default
        Dim b As Byte() = SourceEncoding.GetBytes(source)
        Return eEncoding.GetString(b)
    End Function
#End Region

#Region " WATCHER EVENTS "
    ''' <summary>
    ''' Event handler for attached watchers TypeChanged event
    ''' </summary>
    ''' <param name="NewType">The new type that the watcher is using</param>
    ''' <remarks></remarks>
    Private Sub Watcher_TypeChanged(ByVal NewType As MemoryScanner.WatcherTypes)
        AttachedWatcherTypes = AttachedWatcherTypes Or NewType
    End Sub
#End Region

#Region " MOB EVENTS "
    ''' <summary>
    ''' EventHandler for the MobList Mob StatusChanged Event.
    ''' </summary>
    ''' <param name="Mob">The Mob Object</param>
    ''' <param name="Status">The new status</param>
    ''' <remarks></remarks>
    Private Sub _mobs_StatusChanged(ByVal Mob As MobData, ByVal Status As MobList.MobStatus) Handles _mobs.MobStatusChanged
        'Notify the watchers
        NotifyMobStatusChanged(Mob, Status)
    End Sub
#End Region

#Region " NOTIFY WATCHER METHODS "
    ''' <summary>
    ''' Method to notify all attached watchers that want it of an update to the Mob list
    ''' </summary>
    ''' <param name="Mobs">The updated mob list</param>
    ''' <remarks></remarks>
    Private Sub NotifyMobListUpdated(ByVal Mobs As MobList)
        'Verify that the list is not null
        If Mobs IsNot Nothing Then
            'Loop through each watcher and see if it desires notification of this event
            For Each w In _watchers.ToList()
                If w.Type And WatcherTypes.MobList = WatcherTypes.MobList OrElse w.Type And WatcherTypes.All = WatcherTypes.All Then
                    'Notify the watcher
                    w.MobListUpdated(Mobs)
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Method to notify all attached watchers that want it of an update to a mobs status
    ''' </summary>
    ''' <param name="Mob">The mob object</param>
    ''' <param name="Status">The new status (Alive, Dead, OutOfRange)</param>
    ''' <remarks></remarks>
    Private Sub NotifyMobStatusChanged(ByVal Mob As MobData, ByVal Status As MobList.MobStatus)
        'Loop through each watcher and see if it desires notification of this event
        For Each w In _watchers.ToList()
            If w.Type And WatcherTypes.MobStatus = WatcherTypes.MobStatus OrElse w.Type And WatcherTypes.All = WatcherTypes.All Then
                'Notify the watcher
                w.MobStatusUpdated(Mob, Status)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Method to notify all attached watchers that the current zone has changed
    ''' </summary>
    ''' <param name="LastZone">The previous zone</param>
    ''' <param name="NewZone">The new zone</param>
    ''' <remarks></remarks>
    Private Sub NotifyZoneChanged(ByVal LastZone As Short, ByVal NewZone As Short)
        'Loop thorugh each watcher and see if ie desires notification of this event
        For Each w In _watchers.ToList()
            If w.Type And WatcherTypes.ZoneChange = WatcherTypes.ZoneChange OrElse w.Type And WatcherTypes.All = WatcherTypes.All Then
                'Notify the watcher
                w.ZoneUpdated(LastZone, NewZone)
            End If
        Next
    End Sub

    Private Sub NotifyNewChatLine(ByVal Line As ChatLine)
        'Loop thorugh each watcher and see if ie desires notification of this event
        For Each w In _watchers.ToList()
            If w.Type And WatcherTypes.Chat = WatcherTypes.Chat OrElse w.Type And WatcherTypes.All = WatcherTypes.All Then
                'Notify the watcher
                w.NewChatLine(Line)
            End If
        Next
    End Sub

    Private Sub NotifyVNMLocation(ByVal Direction As Direction, ByVal Distance As Integer)
        'Loop thorugh each watcher and see if ie desires notification of this event
        For Each w In _watchers.ToList()
            If w.Type And WatcherTypes.VNM = WatcherTypes.VNM OrElse w.Type And WatcherTypes.All = WatcherTypes.All Then
                'Notify the watcher
                w.VNMLocationUpdated(Direction, Distance)
            End If
        Next
    End Sub

    Private Sub NotifyPositionChanged(ByVal Position As Point3D)
        For Each w In _watchers.ToList()
            If w.Type And WatcherTypes.Position = WatcherTypes.Position OrElse w.Type And WatcherTypes.All = WatcherTypes.All Then
                w.PositionUpdated(Position)
            End If
        Next
    End Sub
#End Region

#Region " TIMER EVENTS "
    ''' <summary>
    ''' Handler for the timer elapsed event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub _timer_elapsed(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs) Handles _timer.Elapsed
        If Not _isScanning Then
            Dim t As New Thread(AddressOf ScanningThread) With {.IsBackground = True}
            t.Start()
        End If
    End Sub
#End Region

#Region " BACKGROUND THREAD "
    ''' <summary>
    ''' Method for scanning memory in the background
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ScanningThread()
        _isScanning = True
        Dim m As MobData = Nothing
        Try
            'Make sure we have an ffxi object
            If Not FFXI Is Nothing AndAlso (Not FFXI.POL.HasExited AndAlso FFXI.IsGameLoaded) Then

                '********************** MAP Handling ******************************
                'Lets grab the current map
                If _memory Is Nothing OrElse _ffxiNeedsReinitialization Then
                    _memory = New Memory(FFXI.POL, FFXI.MemLocs("ALLIANCEPOINTER") + 50)
                    _lastMap = 0
                    _ffxiNeedsReinitialization = False
                Else
                    _memory.Address = FFXI.MemLocs("MAPBASE")
                End If
                CurrentMap = _memory.GetShort()
                If CurrentMap > 255 Then
                    If CurrentMap = 290 Then 'bastok mog house floor 1
                        CurrentMap = 229
                    ElseIf CurrentMap = 616 Then 'bastok mog house floor 2
                        CurrentMap = 616
                    ElseIf CurrentMap = 257 Then 'sandoria mog house floor 1
                        CurrentMap = 614
                    ElseIf CurrentMap = 615 Then 'sandoria mog house floor 2
                        CurrentMap = 615
                    ElseIf CurrentMap = 288 Then 'windurst mog house floor 1
                        CurrentMap = 613
                    ElseIf CurrentMap = 617 Then 'windurst mog house floor 2
                        CurrentMap = 617
                    ElseIf CurrentMap = 618 Then 'Mog Patio
                        CurrentMap = 618
                    ElseIf CurrentMap = 256 Then 'jeuno mog house
                        CurrentMap = 612
                    ElseIf CurrentMap = 292 Then 'adoulin mog house
                        CurrentMap = 619
                    Else
                        CurrentMap = CurrentMap - 444
                    End If
                End If
                If CurrentMap <> _lastMap Then
                    NotifyZoneChanged(_lastMap, CurrentMap)
                    _lastMap = CurrentMap
                    _mobs.Clear()
                End If
                'If we are on a new map. lets clear the mob list 
                If CurrentMap <> 0 Then
                    SyncLock (_syncObj)
                        '********************** ID Handling *******************************
                        'Firts thing we do is get our server id
                        'Grab the memory location from the scanned memlocs
                        _memory.Address = FFXI.MemLocs("OWNPOSITION")
                        'Read the current zone id from memory
                        MyID = _memory.GetInt32()
                        'Use that id to get my spot in the character array
                        'and grab the pointer to my data structure
                        _memory.Address = FFXI.MemLocs("NPCMAP") + 4 * MyID
                        'Set the address to that location + 120 where my 4 byte 
                        'unique server id is located
                        _memory.Address = _memory.GetInt32() + 120
                        'Grab my id
                        MyID = _memory.GetInt32()

                        'Next lets grab our target's server id
                        'This process is the same as my process, but we use the TARGETINFO
                        'memloc instead of OWNPOSITION as the starting point.
                        'This may become preblematic, so lets add an additional try catch herer
                        Try
                            _memory.Address = FFXI.MemLocs("TARGETINFO")
                            _memory.Address = _memory.GetInt32()
                            _memory.Address = _memory.GetInt32() + 72
                            'TargetID =
                            '_memory.Address = _memory.GetInt32() 'FFXI.MemLocs("NPCMAP") + 4 * TargetID
                            _memory.Address = _memory.GetInt32() + 120
                            TargetID = _memory.GetInt32()
                        Catch ex As Exception
#If DEBUG Then
                            Debug.Print("{1}{0}{2}", ControlChars.NewLine, ex.Message, ex.StackTrace)
#End If
                        End Try
                        '********************** MOB Handling ******************************
                        ProcessMobs(m)

                        If _mobs IsNot Nothing Then
                            NotifyMobListUpdated(_mobs)
                        End If
                    End SyncLock

                    ''Notify all the watchers of the new moblist
                    'If _mobs IsNot Nothing Then
                    '    NotifyMobListUpdated(_mobs)
                    'End If

                    If CheckIfAttachedWatcher(WatcherTypes.Position) OrElse CheckIfAttachedWatcher(WatcherTypes.All) Then
                        CheckPosition(m)
                    End If

                    If CheckIfAttachedWatcher(WatcherTypes.VNM) OrElse CheckIfAttachedWatcher(WatcherTypes.All) Then
                        ProcessVNM()
                    End If

                    'Check to see if we should be watching chat and do so if needed
                    If CheckIfAttachedWatcher(WatcherTypes.Chat) OrElse CheckIfAttachedWatcher(WatcherTypes.All) Then
                        ProcessChat()
                    End If
                End If
            End If
        Catch ex As Exception
            'If we are in debug mode, we print out the info to the immediate window.
            'if in release we just bypass the error and continue scanning.
#If DEBUG Then
            Debug.Print("{1}{0}{2}", ControlChars.NewLine, ex.Message, ex.StackTrace)
#End If
        End Try
        _isScanning = False
    End Sub

    Private Sub ProcessMobs(ByRef m As MobData)
        'Set the address to the base of NPCMAP
        _memory.Address = FFXI.MemLocs("NPCMAP")
        'Grab the block of pointers
        PointerBlock = _memory.GetByteArray(4 * 2048)
        'Run through the pointer block and get the mobs
        For i = 0 To UBound(PointerBlock) Step 4

            'Grab the mobs address pointer
            _mobAddress = BitConverter.ToInt32(PointerBlock, i)
            'Make sure the address is > 0
            If _mobAddress > 0 Then
                'Create the mob object and add it to the list
                _mob = New MobData(FFXI.POL, _mobAddress, True)
                If _mob.ServerID = MyID Then
                    m = _mob
                End If
                If Not _mob Is Nothing AndAlso _mob.ServerID <> 0 AndAlso _mob.ID > 0 AndAlso Not New String() {"", "NPC", "Furniture"}.Contains(_mob.Name.Trim) Then
                    _mobs.Update(_mob)
                End If
            End If
        Next
        'Clean up a little bit
        Erase PointerBlock
    End Sub

    Private Sub CheckPosition(ByVal m As MobData)
        If m IsNot Nothing Then
            If m.X <> _lastPosition.X OrElse m.Y <> _lastPosition.Y OrElse m.Z <> _lastPosition.Z Then
                _lastPosition.X = m.X
                _lastPosition.Y = m.Y
                _lastPosition.Z = m.Z
                NotifyPositionChanged(_lastPosition)
            End If
        End If
    End Sub

    Private Sub ProcessVNM()
        _memory.Address = FFXI.MemLocs("VNMINFO")

        _memory.Address = _memory.GetInt32() + 16
        Dim dir = _memory.GetByte()
        _memory.Address += 4
        Dim distance As Integer = _memory.GetInt32

        'If dir <> _lastDirection OrElse distance <> _lastDistance Then
        NotifyVNMLocation(dir, distance)
        _lastDirection = dir
        _lastDistance = distance
    End Sub

    Private Sub ProcessChat()
        'GRab the chatlog pointer base
        _memory.Address = FFXI.MemLocs("CHATBASE") + 4
        'Read the pointer
        _chatBase = _memory.GetInt32()
        'IF we are in vista or later, there is a double pointer, so lets grab the next one
        If _isVista Then
            _memory.Address = _chatBase + 4
            _chatBase = _memory.GetInt32()
        End If
        'Grab the log offsets array
        _memory.Address = _chatBase
        _logOffsets = _memory.GetByteArray(258)
        'Get the current line count
        If _isVista Then
            _lineCount = _logOffsets(200)
        Else
            _lineCount = _logOffsets(244)
        End If

        If _lastLine = -1 Then
            _lastLine = _lineCount
        End If

        'Lets grab the line offsets
        If _isVista Then
            Array.Resize(_logOffsetBytes, 200)
            Array.Copy(_logOffsets, _logOffsetBytes, 200)
        Else
            _memory.Address = BitConverter.ToInt32(_logOffsets, 4)
            _logOffsetBytes = _memory.GetByteArray(100)
        End If

        _lineOffsets.Clear()
        _lineOffsets.Add(0)
        For i As Integer = 2 To 98 Step 2
            If BitConverter.ToInt16(_logOffsetBytes, i) = 0 Then
                Exit For
            End If
            _lineOffsets.Add(BitConverter.ToInt16(_logOffsetBytes, i))
        Next


        If _isVista Then
            _logStart = BitConverter.ToInt32(_logOffsets, 204)
        Else
            _logStart = BitConverter.ToInt32(_logOffsets, 244)
        End If

        'Now lets process the chat log
        Try
            Dim endPoint As Integer = _lineCount - 1
            If _lastLine > _lineCount Then
                endPoint = 49
            End If

            For i = _lastLine To endPoint
                _memory.Address = _logStart + _lineOffsets(i)
                _line = _memory.GetByteArray(200)
                _lineText = System.Text.Encoding.Default.GetString(_line)
                _lineParts = _lineText.Split(_splitChar, 12)
                _lineInfo.ChatType = _lineParts(0)
                _lineInfo.UK1 = _lineParts(1)
                _lineInfo.UK2 = _lineParts(2)
                _lineInfo.color = _lineParts(3)
                _lineInfo.MsgID = Integer.Parse(_lineParts(4), Globalization.NumberStyles.AllowHexSpecifier)
                _lineInfo.GroupMsgID = Integer.Parse(_lineParts(5), Globalization.NumberStyles.AllowHexSpecifier)
                _lineInfo.UK3 = _lineParts(6)
                _lineInfo.UK4 = _lineParts(7)
                _lineInfo.UK5 = _lineParts(8)
                _lineInfo.UK6 = _lineParts(9)
                _lineInfo.UK7 = _lineParts(10)
                _lineInfo.LineText = CleanUpString(System.Text.Encoding.Default.GetBytes(_lineParts(11)), _lineInfo.ChatType, _lineInfo)
                NotifyNewChatLine(_lineInfo)
            Next

        Catch ex As Exception

        Finally
            If _lastLine > _lineCount Then
                _lastLine = 0
            Else
                _lastLine = _lineCount
            End If
        End Try
    End Sub
#End Region

End Class
