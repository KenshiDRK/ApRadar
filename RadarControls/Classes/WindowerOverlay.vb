Imports FFXIMemory.MobData
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports FFXIMemory
Imports System.Threading
Imports System.Text.RegularExpressions

Public Class WindowerOverlay
    Implements IDisposable

    Private _textHelper As Integer
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private syncObj As New Object
    Private _blockBase As Integer
    Private _blockLength As Integer
    Private _mobAddress As Integer
    Private _filterPassed As Boolean
    Private _isScanning As Boolean = False
    Private _fMem As FFXIMemory.Memory
    Private _myID As Integer
    Private _insertMob As MobData

    Private WithEvents _memTimer As Timers.Timer

    Private Property Mobs As MobList

    Private _objects As TextObjectCollection
    Friend ReadOnly Property Objects() As TextObjectCollection
        Get
            If _objects Is Nothing Then
                _objects = New TextObjectCollection
            End If
            Return _objects
        End Get
    End Property

    Private _settings As RadarSettings
    Public Property Settings() As RadarSettings
        Get
            Return _settings
        End Get
        Set(ByVal value As RadarSettings)
            _settings = value
        End Set
    End Property

    Private _mobList As List(Of MobData)
    Private ReadOnly Property MobList() As List(Of MobData)
        Get
            If _mobList Is Nothing Then
                _mobList = New List(Of MobData)
            End If
            Return _mobList
        End Get
    End Property

    Private _myData As MobData
    Private ReadOnly Property MyData() As MobData
        Get
            If _myData Is Nothing Then
                _myData = New MobData(_ffxi.POL, 0, True)
            End If
            Return _myData
        End Get
    End Property

    Private _pointerBlock As Byte()
    Private Property PointerBlock() As Byte()
        Get
            Return _pointerBlock
        End Get
        Set(ByVal value As Byte())
            _pointerBlock = value
        End Set
    End Property

    Private _ffxi As FFXI
    Public Property FFXI() As FFXI
        Get
            If _ffxi Is Nothing Then
                _ffxi = New FFXI()
            End If
            Return _ffxi
        End Get
        Set(ByVal value As FFXI)
            _ffxi = value
        End Set
    End Property

    Private _corePaintData As RadarPaintData
    Private ReadOnly Property CorePaintData() As RadarPaintData
        Get
            If _corePaintData Is Nothing Then
                _corePaintData = New RadarPaintData
            End If
            Return _corePaintData
        End Get
    End Property

    Private WithEvents _watcher As Watcher
    Private ReadOnly Property MobWatcher As Watcher
        Get
            If _watcher Is Nothing Then
                _watcher = New Watcher(MemoryScanner.WatcherTypes.MobList Or MemoryScanner.WatcherTypes.ZoneChange Or MemoryScanner.WatcherTypes.VNM)
            End If
            Return _watcher
        End Get
    End Property

#Region " CONSTRUCTOR "
    Public Sub New(ByVal pol As Process, ByVal Memlocs As Hashtable)
        _textHelper = Windower.CreateTextHelper("WindowerMMFTextHandler_" & pol.Id)
        MemoryScanner.Scanner.AttachWatcher(MobWatcher)
        LoadSettings()
        '_memTimer = New Timers.Timer() With {.Interval = Settings.RefreshRate}
        'Me.FFXI.POL = pol
        'Me.FFXI.MemLocs = Memlocs
    End Sub
#End Region

#Region " PUBLIC METHODS "
    Public Sub StartRadar()
        _memTimer.Start()
    End Sub

    Public Sub StopRadar()
        _memTimer.Stop()
        'Delete all the text helpers
        For Each item As TextObject In Me.Objects
            item.Destroy()
        Next
        Me.Objects.Clear()
    End Sub
#End Region

#Region " PRIVATE METHODS "
    Private Sub LoadSettings()
        If IO.File.Exists(OverlaySettingsPath) Then
            Dim rs As RadarSettings = DeserializeSettings(OverlaySettingsPath)
            If Not rs Is Nothing Then
                If Not IsProEnabled Then
                    rs.ShowAll = False
                    rs.ShowId = False
                    rs.ShowCampedMobs = False
                End If
                Me.Settings = rs
                Try
                    'Me.Font = FontConverter.FromBase64String(rs.Font)
                Catch
                End Try
            Else
                MessageBox.Show("This settings file is invalid or corrupted, Please select a valid settings file.", "Invalid Settings File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Deserializes the radar settings from a file
    ''' </summary>
    ''' <param name="Path">The path of the settings file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeserializeSettings(ByVal Path As String) As RadarSettings
        Dim rs As RadarSettings = Nothing
        Dim fs As FileStream = Nothing
        Try
            If IO.File.Exists(Path) Then
                fs = IO.File.OpenRead(Path)
                Dim s As New XmlSerializer(GetType(RadarSettings))
                rs = CType(s.Deserialize(fs), RadarSettings)
                fs.Close()
                fs.Dispose()
            Else
                rs = Nothing
            End If
        Catch ex As Exception
            rs = Nothing
        Finally
            If Not fs Is Nothing Then
                fs.Close()
                fs.Dispose()
            End If
        End Try
        Return rs
    End Function

    Private Sub DrawMobs()
        SyncLock (syncObj)
            Dim mobIDList As New List(Of String)
            For Each mob In Mobs.ToList
                'If Not mob.Filters.OverlayFiltered Then
                'Get the mobs distance from my position
                mob.XDistance = (mob.X - MyData.X) * CorePaintData.YScale / 2
                mob.YDistance = -(mob.Y - MyData.Y) * CorePaintData.YScale / 2
                mob.ZDistance = (mob.Z - MyData.Z) * CorePaintData.YScale / 2
                'Get the radius of the mobs distance path
                'Since we ar dealing with a 3 dimensional plane, we need to handle all 3 distances
                mob.Radius = Math.Sqrt(mob.XDistance ^ 2 + mob.YDistance ^ 2 + mob.ZDistance ^ 2) 'Mob.Distance * CorePaintData.YScale / 2 '
                'Calculate the angle from 0 of the mob
                If mob.YDistance <= 0 Then
                    mob.Degrees = -Math.Acos(mob.XDistance / mob.Radius)
                Else
                    mob.Degrees = Math.Acos(mob.XDistance / mob.Radius)
                End If


                mob.Degrees *= (180 / Math.PI)
                'Calculate my angle
                MyData.Degrees = MyData.Direction * (180 / Math.PI)
                If MyData.Degrees < 0 Then
                    MyData.Degrees += 360
                End If
                'Fix the mobs degrees in relation to mine
                mob.Degrees -= MyData.Degrees
                If mob.Degrees < 0 Then
                    mob.Degrees += 360
                End If
                'Convert the degrees to radians
                mob.Degrees *= (Math.PI / 180)
                'Calculate the x and y coordinates for the radar
                mob.MapX = CorePaintData.CenterPoint.X + (mob.Radius * Math.Cos(mob.Degrees + CorePaintData.NinetyDegrees))
                mob.MapY = CorePaintData.CenterPoint.Y + (mob.Radius * Math.Sin(mob.Degrees + CorePaintData.NinetyDegrees))





                If Objects.Contains(mob.ID) Then
                    Objects.ItemByName(mob.ID).Location = New Point(mob.MapX, mob.MapY)
                Else
                    If mob.MobType = MobTypes.PC Then
                        Objects.Add(New TextObject(_textHelper, mob.ID, String.Format("• {0}", mob.Name), Me.Settings.PCColor, New Point(mob.MapX, mob.MapY)))
                    Else
                        Objects.Add(New TextObject(_textHelper, mob.ID, String.Format("• {0}", mob.Name), Me.Settings.NPCColor, New Point(mob.MapX, mob.MapY)))
                    End If

                End If
                mobIDList.Add(mob.ID)
                'End If
            Next
            For i = Objects.Count - 1 To 0 Step -1
                If Not mobIDList.Contains(Objects(i).Name) Then
                    Objects(i).Destroy()
                    Objects.RemoveAt(i)
                End If
            Next
        End SyncLock

    End Sub

    Private Sub _memTimer_elapsed() Handles _memTimer.Elapsed
        'We check to make sure that the process has not exited and that 
        'FFXiMain is still loaded
        If Not _ffxi.POL.HasExited AndAlso _ffxi.IsGameLoaded Then
            'Check to make sure the memory object is created
            If _fMem Is Nothing Then
                _fMem = New Memory(_ffxi.POL, 0)
            End If

            'Set up my data
            'Set the address to ownos
            _fMem.Address = _ffxi.MemLocs("OWNPOSITION")
            'Read my Id
            _myID = _fMem.GetInt32
            'Get the base address of my mob structure
            _fMem.Address = _ffxi.MemLocs("NPCMAP") + (4 * _myID)
            'Set my mob structure base
            MyData.MobBase = _fMem.GetInt32

            Dim t As New Thread(AddressOf MobScanner)
            t.Start()

        End If
    End Sub

    ''' <summary>
    ''' Mob scanning thread
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MobScanner()
        'To prevent reading the memory twice at the same time
        'we will check if it is currently being scanned and just 
        'skip over this method if so.
        If Not _isScanning Then
            _isScanning = True
            SyncLock (syncObj)
                'Empty out the mob list and the mob regions
                MobList.Clear()

                If Settings.ShowNPC OrElse Settings.ShowPC OrElse Settings.ShowPartyMembers Then
                    'Get the block starting point
                    'If we are going to show any mobs, we have to start at
                    'the 0 index as mobs are first in the array
                    If Settings.ShowNPC Then
                        _blockBase = 0
                    Else
                        'If no mobs are to be shown then we start at 768 (0xC00)
                        _blockBase = 768
                    End If

                    'Get the block size
                    'If we are showing pcs we want the end of the block.
                    'we get the size by subracting the start form the total ammount possible
                    If Settings.ShowPC OrElse Settings.ShowPartyMembers Then
                        _blockLength = 2048 - _blockBase
                    Else
                        'if we are not showing pc,s the size is 768 - block base
                        _blockLength = 768 - _blockBase
                    End If

                    'Set the Memory object address to the desired base of the npcmap array
                    _fMem.Address = _ffxi.MemLocs("NPCMAP") + (4 * _blockBase)
                    'Read the pointer block into an array
                    PointerBlock = _fMem.GetByteArray(4 * _blockLength) '1618)
                    'Loop through the pointer block 
                    For i = 0 To UBound(PointerBlock) Step 4
                        _mobAddress = BitConverter.ToInt32(PointerBlock, i)
                        If _mobAddress > 0 Then
                            'Get the mob object to be inserted
                            _insertMob = New MobData(_ffxi.POL, _mobAddress, True)
                            'LEts deal with some PC entries
                            If Not (Settings.HideOtherFloors AndAlso (Math.Abs(_insertMob.Z - MyData.Z) >= 6)) Then
                                If _insertMob.MobType = MobData.MobTypes.PC Then
                                    'Verify taht we should show the mob.  Check to see if show all is checked 
                                    'Or else we check to make sure the mob is alive and should be shown
                                    If Settings.ShowPC AndAlso (Settings.ShowAll OrElse (_insertMob.WarpInfo > 0 AndAlso _insertMob.ID <> MyData.ID)) Then
                                        'If we have a filter lets apply it
                                        If Settings.PCFilterType <> RadarSettings.FilterType.None AndAlso Not Settings.PCFilter Is Nothing Then
                                            'Grab each of the filter conditions
                                            Dim filters As String() = Settings.PCFilter.Split(",")
                                            'Check the filter type
                                            Select Case Settings.PCFilterType
                                                'For standard filter we check to see if it is anywhere in the name
                                                Case RadarSettings.FilterType.Regular
                                                    For Each Filter As String In filters
                                                        If _insertMob.Name.ToLower.Contains(Filter.ToLower.Trim) Then
                                                            AddMob()
                                                            Exit For
                                                        End If
                                                    Next
                                                Case RadarSettings.FilterType.Reverse
                                                    'For a reverse filter we remove any entries found
                                                    _filterPassed = True
                                                    For Each Filter As String In filters
                                                        If _insertMob.Name.ToLower.Contains(Filter.ToLower.Trim) Then
                                                            _filterPassed = False
                                                            Exit For
                                                        End If
                                                    Next
                                                    If _filterPassed Then
                                                        AddMob()
                                                    End If
                                                Case RadarSettings.FilterType.RegEx
                                                    'For a regex filter we match the regex patter specified
                                                    Try
                                                        If Regex.Match(_insertMob.Name, Settings.NPCFilter).Success Then
                                                            AddMob()
                                                        End If
                                                    Catch
                                                    End Try
                                            End Select

                                        Else
                                            AddMob()
                                        End If
                                    End If
                                Else
                                    If Settings.ShowNPC Then

                                        If Settings.ShowAll OrElse (_insertMob.WarpInfo > 0 AndAlso _insertMob.Name <> "NPC" AndAlso _insertMob.HP > 0) Then
                                            If Settings.NPCFilterType <> RadarSettings.FilterType.None And Not Settings.NPCFilter Is Nothing Then
                                                Dim filters As String() = Settings.NPCFilter.Split(",")
                                                Select Case Settings.NPCFilterType
                                                    Case RadarSettings.FilterType.Regular
                                                        For Each Filter As String In filters
                                                            If _insertMob.Name.ToLower.Contains(Filter.ToLower.Trim) OrElse _insertMob.ID.ToString = Filter OrElse _insertMob.ID.ToString("x2") = Filter Then
                                                                AddMob()
                                                                Exit For
                                                            End If
                                                        Next
                                                    Case RadarSettings.FilterType.Reverse
                                                        _filterPassed = True
                                                        For Each Filter As String In filters
                                                            If _insertMob.Name.ToLower.Contains(Filter.ToLower.Trim) OrElse _insertMob.ID.ToString = Filter OrElse _insertMob.ID.ToString("x2") = Filter Then
                                                                _filterPassed = False
                                                                Exit For
                                                            End If
                                                        Next
                                                        If _filterPassed Then
                                                            AddMob()
                                                        End If
                                                    Case RadarSettings.FilterType.RegEx
                                                        Try
                                                            If Regex.Match(_insertMob.Name, Settings.NPCFilter).Success Then
                                                                AddMob()
                                                            End If
                                                        Catch
                                                        End Try
                                                End Select
                                            Else
                                                AddMob()
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
                'Set the display list so that it can show the mobs on the radar
            End SyncLock
            'Compare the mobs for raising the removed event
            _isScanning = False
            DrawMobs()
        End If
    End Sub

    Private Sub AddMob()
        MobList.Add(_insertMob)
    End Sub
#End Region

#Region " Watcher Events "
    Private Sub _watcher_NewMobList(ByVal InMobs As MobList) Handles _watcher.NewMobList
        Me.Mobs = InMobs
        DrawMobs()
        'Me.MapRadar.Settings.CurrentMap = MemoryScanner.Scanner.CurrentMap
        'Me.Invalidate()
    End Sub

    Private Sub _Watcher_ZoneChanged(ByVal LastZone As Short, ByVal NewZone As Short) Handles _watcher.ZoneChanged
        'Me.MapRadar.Settings.CurrentMap = MemoryScanner.Scanner.CurrentMap
    End Sub

    Private Sub _watcher_VNMLocationUpdated(ByVal Direction As Direction, ByVal Distance As Integer) Handles _watcher.OnVNMLocationUpdated
        'Me.MapRadar.VNMDirection = Direction
        'Me.MapRadar.VNMDistance = Distance
    End Sub
#End Region

#Region " TEXT OBJECTS CLASS "
    Public Class TextObject
        Private _textHelper As Integer
        Public Sub New(ByVal Helper As Integer, ByVal Name As String, ByVal Text As String, ByVal Color As Color, ByVal Location As Point)
            _textHelper = Helper
            _name = Name
            Me._text = Text
            Me._color = Color
            Me._location = Location
            Windower.CTHCreateTextObject(Helper, Name)
            Windower.CTHSetColor(Helper, Name, Color.A, Color.R, Color.G, Color.B)
            Windower.CTHSetLocation(Helper, Name, Location.X, Location.Y)
            Windower.CTHSetText(Helper, Name, Text)
            Windower.CTHFlushCommands(Helper)
        End Sub

        Private _name As String
        Public ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        Private _text As String
        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
                Windower.CTHSetText(_textHelper, _name, _name)
                Windower.CTHFlushCommands(_textHelper)
            End Set
        End Property

        Private _color As Color
        Public Property Color() As Color
            Get
                Return _color
            End Get
            Set(ByVal value As Color)
                _color = value
                Windower.CTHSetColor(_textHelper, _name, value.A, value.R, value.G, value.B)
                Windower.CTHFlushCommands(_textHelper)
            End Set
        End Property

        Private _location As Point
        Public Property Location() As Point
            Get
                Return _location
            End Get
            Set(ByVal value As Point)
                _location = value
                Windower.CTHSetLocation(_textHelper, _name, value.X, value.Y)
                Windower.CTHFlushCommands(_textHelper)
            End Set
        End Property

        Public Sub Destroy()
            Windower.CTHDeleteTextObject(_textHelper, _name)
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Class

    Public Class TextObjectCollection
        Inherits System.Collections.CollectionBase

        Default Public Property Item(ByVal index As Integer) As TextObject
            Get
                Return DirectCast(Me.List(index), TextObject)
            End Get
            Set(ByVal value As TextObject)
                Me.List(index) = value
            End Set
        End Property

        Public Function ItemByName(ByVal name As String) As TextObject
            For i As Integer = 0 To Me.List.Count - 1
                If DirectCast(Me.List(i), TextObject).Name = name Then
                    Return Me.List.Item(i)
                End If
            Next
            Return Nothing
        End Function

        Public Function IndexOf(ByVal item As TextObject) As Integer
            Return Me.List.IndexOf(item)
        End Function

        Public Function Add(ByVal item As TextObject) As Integer
            Return Me.List.Add(item)
        End Function

        Public Sub Remove(ByVal item As TextObject)
            Me.List.Remove(item)
        End Sub

        Public Sub RemoveByName(ByVal name As String)
            Me.Remove(ItemByName(name))
        End Sub

        Public Sub AddRange(ByVal collection As TextObjectCollection)
            For i As Integer = 0 To collection.Count - 1
                Me.List.Add(collection(i))
            Next
        End Sub

        Public Sub AddRange(ByVal collection As TextObject())
            Me.AddRange(collection)
        End Sub

        Public Function Contains(ByVal item As TextObject) As Boolean
            Return Me.List.Contains(item)
        End Function

        Public Function Contains(ByVal Name As String) As Boolean
            Return Me.List.Contains(Me.ItemByName(Name))
        End Function
    End Class
#End Region

#Region " IDISPOSABLE SUPPORT "
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                'Delete all the text helpers
                For Each item As TextObject In Me.Objects
                    item.Destroy()
                Next
                'Delete the helper
                Windower.DeleteTextHelper(_textHelper)
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
