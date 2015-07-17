Imports FFXIMemory
Imports System.Threading
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.IO
Imports System.Xml.Serialization
Imports System.Windows.Forms
Imports DataLibrary
Imports DataLibrary.ApRadarDataSet
Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public MustInherit Class RadarBase
    Inherits Control

#Region " MEMBER VARIABLES "
    Protected syncObj As New Object
    Protected _displayText As String
    Protected _pingSize As Byte
    Protected _thisMobCamped As Boolean
    Protected _filterPassed As Boolean
    Protected _mobCount As Integer
#End Region

#Region " PROPERTIES "
    Protected WithEvents _settings As RadarSettings
    <Category("Radar Settings")> _
    Public Property Settings() As RadarSettings
        Get
            If _settings Is Nothing Then
                _settings = New RadarSettings
            End If
            Return _settings
        End Get
        Set(ByVal value As RadarSettings)
            _settings = value
        End Set
    End Property

    Private _corePaintData As RadarPaintData
    <Browsable(False)> _
    Protected ReadOnly Property CorePaintData() As RadarPaintData
        Get
            If _corePaintData Is Nothing Then
                _corePaintData = New RadarPaintData
            End If
            Return _corePaintData
        End Get
    End Property

    Protected Property MyData As MobData
    Protected Property TargetData As MobData

    Private _mobs As List(Of MobData)
    Protected Property Mobs As List(Of MobData)
        Get
            If _mobs Is Nothing Then
                _mobs = New List(Of MobData)
            End If
            Return _mobs
        End Get
        Set(ByVal value As List(Of MobData))
            _mobs = value
        End Set
    End Property

    Private _zones As FFXIMemory.Zones
    Protected ReadOnly Property Zones() As FFXIMemory.Zones
        Get
            If _zones Is Nothing Then
                _zones = New FFXIMemory.Zones
            End If
            Return _zones
        End Get
    End Property

    Public Property ProEnabled As Boolean

    Private _nmList As List(Of String)
    Public Property NMList() As List(Of String)
        Get
            If _nmList Is Nothing Then
                _nmList = New List(Of String)
            End If
            Return _nmList
        End Get
        Set(ByVal value As List(Of String))
            _nmList = value
        End Set
    End Property

    Private _textRendering As TextRenderingHint = TextRenderingHint.SystemDefault
    Public Property TextRendering() As TextRenderingHint
        Get
            Return _textRendering
        End Get
        Set(ByVal value As TextRenderingHint)
            _textRendering = value
        End Set
    End Property

    Private _smoothingMode As SmoothingMode = Drawing2D.SmoothingMode.HighSpeed
    Public Property SmoothingMode() As SmoothingMode
        Get
            Return _smoothingMode
        End Get
        Set(ByVal value As SmoothingMode)
            _smoothingMode = value
        End Set
    End Property

    Private _compositingQuality As CompositingQuality = Drawing2D.CompositingQuality.HighSpeed
    Public Property CompositingQuality() As CompositingQuality
        Get
            Return _compositingQuality
        End Get
        Set(ByVal value As CompositingQuality)
            _compositingQuality = value
        End Set
    End Property

    Protected Property PartyMembers() As String()

    Private WithEvents _watcher As Watcher
    Protected ReadOnly Property Watcher As Watcher
        Get
            If _watcher Is Nothing Then
                _watcher = New Watcher(MemoryScanner.WatcherTypes.MobList Or
                                       MemoryScanner.WatcherTypes.ZoneChange)
            End If
            Return _watcher
        End Get
    End Property

    Private _mobIDList As List(Of Integer)
    Protected ReadOnly Property MobIDList As List(Of Integer)
        Get
            If _mobIDList Is Nothing Then
                _mobIDList = New List(Of Integer)
            End If
            Return _mobIDList
        End Get
    End Property

    Public Property IsInitialized As Boolean

    Private WithEvents _settingsForm As SettingsForm
    <Browsable(False)> _
    Public ReadOnly Property SettingsForm() As SettingsForm
        Get
            If _settingsForm Is Nothing OrElse _settingsForm.IsDisposed Then
                _settingsForm = New SettingsForm(Settings)
                AddHandler _settingsForm.propGrid.PropertyValueChanged, AddressOf Setting_Changed
            End If
            Return _settingsForm
        End Get
    End Property
#End Region

#Region " OVERRIDES "
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H20
            Return cp
        End Get
    End Property

    Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
        Settings.Font = FontConverter.ToBase64String(Font)
        MyBase.OnFontChanged(e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If MemoryScanner.Scanner.IsRunning Then
            PaintRadar(e.Graphics)
        End If
        MyBase.OnPaint(e)
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not _watcher Is Nothing Then
            MemoryScanner.Scanner.DetachWatcher(_watcher)
        End If
        MyBase.Dispose(disposing)
    End Sub
#End Region

#Region " CONSTRUCTOR "
    Public Sub New()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or
                    ControlStyles.OptimizedDoubleBuffer Or
                    ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()
    End Sub
#End Region

#Region " RADAR METHODS "
    Protected MustOverride Sub PaintRadar(ByRef g As Graphics)

    ''' <summary>
    ''' Paints a mob on the radar
    ''' </summary>
    ''' <param name="g">The graphics object to use for painting</param>
    ''' <param name="Mob">The mob data to be painted</param>
    ''' <remarks></remarks>
    Protected Sub PaintMob(ByVal g As Graphics, ByVal Mob As MobData)
        ''MobDataOverlay.Visible = False
        'Try

        '    'Get the mobs display text
        '    _displayText = GetDisplayText(Mob)

        '    'Check to see what kind of mob we are painting
        '    If Mob.MobType = MobData.MobTypes.PC Then  'Paint the PC mob
        '        'Paint the pc blip
        '        g.FillEllipse(Settings.PCBrush, _
        '                      CSng(Mob.MapX - (Settings.BlipSize / 2)), _
        '                      CSng(Mob.MapY - (Settings.BlipSize / 2)), _
        '                      Settings.BlipSize, Settings.BlipSize)

        '        'Paint the target ping
        '        If Settings.ShowPing Then
        '            If Mob.ID = TargetData.ID Then
        '                Settings.PingBrush.Color = Color.FromArgb(72, Settings.PCColor)
        '                g.FillEllipse(Settings.PingBrush, _
        '                              CSng(Mob.MapX - ((Settings.BlipSize + _pingSize) / 2)), _
        '                              CSng(Mob.MapY - ((Settings.BlipSize + _pingSize) / 2)), _
        '                              Settings.BlipSize + _pingSize, Settings.BlipSize + _pingSize)
        '                _pingSize += 4
        '                If _pingSize > 45 Then
        '                    _pingSize = 0
        '                End If
        '            End If
        '        End If
        '        If _displayText <> String.Empty Then
        '            g.DrawString(_displayText, _
        '                         Font, _
        '                         Settings.PCBrush, _
        '                         Mob.MapX + Settings.BlipSize, _
        '                         Mob.MapY - (Settings.BlipSize / 2) - 1)
        '        End If
        '    Else
        '        'Get the color to paint the mob depending on the mobs status
        '        If _thisMobCamped Then
        '            Settings.NPCBrush.Color = Settings.CampedColor
        '        Else
        '            If ProEnabled AndAlso (NMList.Contains(Mob.Name) OrElse _
        '            NMList.Contains(Mob.ID.ToString("x2")) OrElse _
        '            NMList.Contains(Mob.ID.ToString("X2")) OrElse _
        '            NMList.Contains(Mob.ID.ToString)) Then
        '                Settings.NPCBrush.Color = Settings.NMColor
        '            ElseIf Mob.ClaimedBy > 0 AndAlso Mob.ClaimedBy <> MyData.ServerID Then
        '                'The mob is claimed by another character
        '                Settings.NPCBrush.Color = Color.Purple
        '            ElseIf Mob.ClaimedBy = MyData.ServerID Then
        '                'The mob is claimed by me
        '                Settings.NPCBrush.Color = Color.Green
        '            Else
        '                'The mob is unclaimed
        '                Settings.NPCBrush.Color = Settings.NPCColor
        '            End If
        '        End If

        '        'Paint the mob blip
        '        If Settings.ShowSight Then
        '            Dim dir As Single = Mob.Direction - (MyData.Direction + 90 * Math.PI / 180)
        '            Dim endPoint As PointF
        '            endPoint.Y = (Math.Sin(dir) * 10.0F)
        '            endPoint.X = (endPoint.Y / Math.Tan(dir))
        '            g.DrawLine(New Pen(Color.YellowGreen, 2), Mob.MapX, Mob.MapY, Mob.MapX + endPoint.X, Mob.MapY + endPoint.Y)
        '        End If

        '        'Paint the blip
        '        g.FillEllipse(Settings.NPCBrush, _
        '                      CSng(Mob.MapX - (Settings.BlipSize / 2)), _
        '                      CSng(Mob.MapY - (Settings.BlipSize / 2)), _
        '                      Settings.BlipSize, Settings.BlipSize)

        '        'Paint the target pinger if this is my current target
        '        If Settings.ShowPing Then
        '            If Mob.ID = TargetData.ID Then
        '                Settings.PingBrush.Color = Color.FromArgb(72, Settings.NPCColor)
        '                g.FillEllipse(Settings.PingBrush, _
        '                              CSng(Mob.MapX - ((Settings.BlipSize + _pingSize) / 2)), _
        '                              CSng(Mob.MapY - ((Settings.BlipSize + _pingSize) / 2)), _
        '                              Settings.BlipSize + _pingSize, Settings.BlipSize + _pingSize)
        '                _pingSize += 4
        '                If _pingSize > 45 Then
        '                    _pingSize = 0
        '                End If
        '            End If
        '        End If


        '        'Paint the current mobs data
        '        If _displayText <> String.Empty Then
        '            g.DrawString(_displayText, _
        '                         Font, _
        '                         Settings.NPCBrush, _
        '                         Mob.MapX + Settings.BlipSize, _
        '                         Mob.MapY - (Settings.BlipSize / 2) - 1)
        '        End If

        '    End If
        'Catch ex As Exception
        '    'Debug.Print(Mob.Name & " : " & ex.Message)
        'End Try
    End Sub

    Protected MustOverride Sub PaintMyPointer(ByVal g As Graphics)

    Protected MustOverride Sub PaintRange(ByVal g As Graphics, ByVal Range As Single, ByVal pen As Pen)

#End Region

#Region " PUBLIC METHODS "
    ''' <summary>
    ''' Initializes the radar starting the scan
    ''' </summary>
    ''' <remarks>This must be called when the radar is started</remarks>
    Public Sub InitializeRadar()
        'Create a new instance of the mob id list
        'This is used to check my id's against any linked radar mobs
        _mobIDList = New List(Of Integer)

        'Create the watcher element and attach it to the scanner
        'This will watch for moblist zonechange and partymembers
        MemoryScanner.Scanner.AttachWatcher(_watcher)

        'Check for the nmlist file
        If ProEnabled AndAlso IO.File.Exists(Application.StartupPath & "\NMList.txt") Then
            NMList.AddRange(IO.File.ReadAllLines(Application.StartupPath & "\NMList.txt"))
        End If
        IsInitialized = True
    End Sub

    ''' <summary>
    ''' Saves the settings to the path specified
    ''' </summary>
    ''' <param name="Path"></param>
    ''' <remarks></remarks>
    Public Sub SaveSettings(ByVal Path As String)
        SerializeSettings(Path)
    End Sub

    ''' <summary>
    ''' Loads the setting from the path specified
    ''' </summary>
    ''' <param name="Path"></param>
    ''' <remarks></remarks>
    Public Sub LoadSettings(ByVal Path As String)
        If IO.File.Exists(Path) Then
            Dim rs As RadarSettings = DeserializeSettings(Path)
            If Not rs Is Nothing Then
                If Not IsProEnabled Then
                    rs.ShowAll = False
                    rs.ShowId = False
                    rs.ShowCampedMobs = False
                End If
                Settings = rs
                Try
                    Font = FontConverter.FromBase64String(rs.Font)
                Catch
                End Try
            Else
                MessageBox.Show("This settings file is invalid or corrupted, Please select a valid settings file.", "Invalid Settings File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Public Sub ShowSettings()
        SettingsForm.Show()
    End Sub

    Public Sub SaveCampedMobs()
        CampedMobManager.SaveData()
    End Sub
#End Region

#Region " PRIVATE METHODS "
    ''' <summary>
    ''' Gets the mob display text
    ''' </summary>
    ''' <param name="mob">The mob object containig the data</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetDisplayText(ByVal mob As MobData) As String
        Dim display As String
        _thisMobCamped = False
        'Set the mobs name
        If mob.ID = &H77 Then
            display = String.Empty
        Else
            display = String.Empty
        End If
        If (mob.MobType <> MobData.MobTypes.PC AndAlso Settings.ShowNPCNames) OrElse (mob.MobType = MobData.MobTypes.PC AndAlso Settings.ShowPCNames) Then
            If mob.Name.Length > 24 Then
                display = String.Empty
            End If
            display &= mob.Name
        End If

        'If pro is enabled then and we are in camping mode then Show the death timer
        If Settings.ShowCampedMobs AndAlso ProEnabled Then
            Dim cmd As CampedMob() = CampedMobManager.GetCampedMobs
            Dim cm = (From c In cmd Where c.ServerID = mob.ServerID).FirstOrDefault  'GetCampedMob(mob.ServerID)
            If Not cm Is Nothing AndAlso cm.IsDead AndAlso Not IsDBNull(cm.DeathTime) Then
                Dim deathDate As DateTime
                If DateTime.TryParse(cm.DeathTime, deathDate) Then

                    Dim ts As TimeSpan = DateTime.Now.Subtract(deathDate)
                    If ts.Days > 0 Then
                        display = String.Format("{0} {1}:{2}:{3}:{4}", display, ts.Days.ToString("00"), ts.Hours.ToString("00"), ts.Minutes.ToString("00"), ts.Seconds.ToString("00"))
                    Else
                        display = String.Format("{0} {1}:{2}:{3}", display, ts.Hours.ToString("00"), ts.Minutes.ToString("00"), ts.Seconds.ToString("00"))
                    End If

                    _thisMobCamped = True
                End If
            End If
        End If

        'Show the distance
        If Settings.ShowDistance Then
            display = String.Format("{0} {1}", display, mob.Distance.ToString("0.0"))
        End If
        'Show the HP
        If Settings.ShowHP Then
            display = String.Format("{0} {1}%", display, mob.HP)
        End If
        'Show the id if specified
        If Settings.ShowId AndAlso ProEnabled Then
            display = String.Format("{0} {1}", mob.ID.ToString("X"), display)
        End If
        'Check to see if pro is enabled and show 
        'the nyzle lamp order if so
        If ProEnabled Then
            If Settings.CurrentMap = &H4D Then
                If mob.ID >= &H1A4 AndAlso mob.ID <= &H1A8 Then
                    display = String.Format("[#{0}] {1}", (mob.ID - &H1A3).ToString(), display)
                End If
            End If
        End If
        Return display
    End Function

    Protected Function GetLinkDisplayText(ByVal Mob As Contracts.Shared.MobData) As String
        Dim display As String
        If Mob.IsPC Then
            display = Mob.Name
        Else
            display = String.Format("(L) {0}", Mob.Name)
        End If

        If Settings.ShowDistance Then
            display = String.Format("{0} {1:0.0}", display, Math.Sqrt(Math.Abs(MyData.X - Mob.Pos.X) ^ 2 + Math.Abs(MyData.Y - Mob.Pos.Y) ^ 2))
        End If

        If Settings.ShowHP Then
            display = String.Format("{0} {1}%", display, Mob.HP)
        End If
        'Show the id if specified
        If Settings.ShowId AndAlso ProEnabled Then
            display = String.Format("{0} {1}", Mob.ID.ToString("X"), display)
        End If
        'Check to see if pro is enabled and show 
        'the nyzle lamp order if so
        If ProEnabled Then
            If Settings.CurrentMap = &H4D Then
                If Mob.ID >= &H1A4 AndAlso Mob.ID <= &H1A8 Then
                    display = String.Format("[#{0}] {1}", (Mob.ID - &H1A3).ToString(), display)
                End If
            End If
        End If
        Return display
    End Function

    ''' <summary>
    ''' Gets your current target's data
    ''' </summary>
    ''' <returns>MobData class contianing all the target's info</returns>
    ''' <remarks></remarks>
    Protected Function GetTargetInfo() As MobData
        Return TargetData
    End Function

    ''' <summary>
    ''' Builds the mobs display info
    ''' </summary>
    ''' <param name="mob">The mob object used to get the display info</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function BuildMobInfo(ByVal Mob As MobsRow) As String
        Return BuildMobInfo(Mob, String.Empty)
    End Function

    ''' <summary>
    ''' Builds the mobs display info
    ''' </summary>
    ''' <param name="mob">The mob object used to get the display info</param>
    ''' <param name="ClaimedBy">The id of the persont that has the mob claimed</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function BuildMobInfo(ByVal mob As MobsRow, ByVal ClaimedBy As String) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(mob.MobName)
        If mob.NM Then
            sb.Append(" [NM]")
        End If
        sb.Append(Environment.NewLine)
        If Not mob.IsFamilyNull Then
            sb.Append(String.Format("Family:{0}{0}{1}{2}", ControlChars.Tab, mob.Family & "", Environment.NewLine))
        Else
            sb.Append(String.Format("Family:{0}", Environment.NewLine))
        End If
        If Not mob.IsJobNull Then
            sb.Append(String.Format("Job:{0}{0}{1}{2}", ControlChars.Tab, mob.Job & "", Environment.NewLine))
        Else
            sb.Append(String.Format("Job:{0}", Environment.NewLine))
        End If
        sb.Append(String.Format("Behavior:{0}{0}", ControlChars.Tab))
        sb.Append(GetBehavior(mob))
        sb.Append(Environment.NewLine)
        sb.Append(GetDetection(mob))
        sb.Append(Environment.NewLine)
        sb.Append(String.Format("Level Range:{0}{1}-{2}", ControlChars.Tab, mob.MinLevel, mob.MaxLevel))

        Dim mobId As Integer = mob.MobPK
        Dim items = (From item In DataAccess.MobData.Items Join _
                     itm In DataAccess.MobData.ItemsToMobs On _
                     item.ItemID Equals itm.ItemID Where _
                     itm.MobPK = mobId Select New With {item.ItemName}).ToArray
        If Not items Is Nothing Then
            Dim isFirst As Boolean = True
            sb.Append(Environment.NewLine)
            sb.Append(String.Format("Drops:{0}{0}", ControlChars.Tab))
            For Each item In items
                If isFirst Then
                    sb.Append(item.ItemName & Environment.NewLine)
                    isFirst = False
                Else
                    sb.Append(String.Format("{0}{0}{1}{2}", ControlChars.Tab, item.ItemName, Environment.NewLine))
                End If
            Next
        End If
        If ClaimedBy <> String.Empty Then
            sb.Append(Environment.NewLine)
            sb.Append(String.Format("Claimed By:{0}{1}", ControlChars.Tab, ClaimedBy))
        End If
        'If Not mob.IsNotesNull Then
        '    sb.Append(Environment.NewLine)
        '    sb.Append("Notes:")
        '    sb.Append(Environment.NewLine)
        '    sb.Append(WordWrap(mob.Notes))
        'End If
        Return sb.ToString
    End Function

    ''' <summary>
    ''' Gets the behavior string for the current target
    ''' </summary>
    ''' <param name="Mob"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetBehavior(ByVal Mob As MobsRow) As String
        Dim output As String = String.Empty
        If Mob.Aggressive Then
            output = "Aggressive"
        End If
        If Mob.Links Then
            If output = String.Empty Then
                output = "Links"
            Else
                output &= ", Links"
            End If
        End If
        Return output
    End Function

    ''' <summary>
    ''' Builds the detection string for the current target
    ''' </summary>
    ''' <param name="Mob"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetDetection(ByVal Mob As MobsRow) As String
        Dim output As String = String.Empty
        If Mob.DetectsSight Then
            output = "S"
        End If
        If Mob.DetectsSound Then
            If output = String.Empty Then
                output = "H"
            Else
                output &= ", H"
            End If
        End If
        If Mob.DetectsMagic Then
            If output = String.Empty Then
                output = "M"
            Else
                output &= ", M"
            End If
        End If
        If Mob.DetectsLowHP Then
            If output = String.Empty Then
                output = "↓HP"
            Else
                output &= ", ↓HP"
            End If
        End If
        If Mob.DetectsHealing Then
            If output = String.Empty Then
                output = "Heal"
            Else
                output &= ", Heal"
            End If
        End If
        If Mob.TracksScent Then
            If output = String.Empty Then
                output = "Sc"
            Else
                output &= ", Sc"
            End If
        End If
        If Mob.TrueSight Then
            If output = String.Empty Then
                output = "T(S)"
            Else
                output &= ", T(S)"
            End If
        End If
        If Mob.TrueSound Then
            If output = String.Empty Then
                output = "T(H)"
            Else
                output &= ", T(H)"
            End If
        End If
        If output <> String.Empty Then
            output = String.Format("Detects:{0}{0}{1}", ControlChars.Tab, output)
        End If
        Return output
    End Function

    ''' <summary>
    ''' Gets the name of the player that has the mob claimed
    ''' </summary>
    ''' <param name="ClaimID">The ServerID of the player with claim</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetClaimedBy(ByVal ClaimID As Integer)
        Dim name = (From c In DataAccess.MobData.PC _
                    Where c.ServerID = ClaimID _
                    Select c.PCName).FirstOrDefault
        If name = String.Empty Then
            name = "Unknown"
        End If
        Return name
    End Function

    ''' <summary>
    ''' Serializes the radar settings to a file
    ''' </summary>
    ''' <param name="Path">The path to the setttings file</param>
    ''' <remarks>If the file does not exist it will be created and
    ''' if it does exist it will be overwritten</remarks>
    Protected Sub SerializeSettings(ByVal Path As String)
        If Not IO.Directory.Exists(IO.Path.GetDirectoryName(Path)) Then
            IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(Path))
        End If
        Dim fs As New FileStream(Path, FileMode.Create)

        Dim s As New XmlSerializer(GetType(RadarSettings))
        s.Serialize(fs, Settings)
        fs.Close()
        fs.Dispose()
    End Sub

    ''' <summary>
    ''' Deserializes the radar settings from a file
    ''' </summary>
    ''' <param name="Path">The path of the settings file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function DeserializeSettings(ByVal Path As String) As RadarSettings
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

    ''' <summary>
    ''' Wraps a long string to fit in the radar info viewer
    ''' </summary>
    ''' <param name="text">The string to wrap</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function WordWrap(ByVal text As String) As String
        Dim noteItems As String() = text.Split(" ")
        Dim lineLength As Integer = 0
        Dim retValue As String = String.Empty
        For Each item In noteItems
            lineLength += item.Length + 1
            If lineLength >= 40 Then
                retValue &= Environment.NewLine
                retValue &= String.Format("{0} ", item)
                lineLength = 0
            Else
                retValue &= String.Format("{0} ", item)
            End If
        Next
        Return retValue
    End Function

    ''' <summary>
    ''' Checks to see if a camped mob exists in the list
    ''' </summary>
    ''' <param name="MobServerID">The Server ID of the mob to check</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CampedMobExists(ByVal MobServerID As Integer) As Boolean
        Try
            Dim cMobs As CampedMob() = CampedMobManager.GetCampedMobs
            Return (From c In cMobs Where c.ServerID = MobServerID AndAlso c.DeathTime <> String.Empty AndAlso c.IsDead).Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks a mobs name against the specified filter and type
    ''' </summary>
    ''' <param name="FilterType">The type of filter to check for</param>
    ''' <param name="MobName">The name of the mob</param>
    ''' <param name="MobID">The mobs id</param>
    ''' <param name="Filters">The filters to check against</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CheckFilter(ByVal FilterType As RadarSettings.FilterType, ByVal MobName As String, ByVal MobID As Integer, ByVal Filters As String()) As Boolean
        Try
            Select Case FilterType
                Case RadarSettings.FilterType.Regular
                    For Each Filter As String In Filters
                        Filter = Filter.ToLower.Trim
                        If MobName.ToLower.Contains(Filter) OrElse MobID.ToString = Filter OrElse MobID.ToString("x2") = Filter Then
                            Return True
                            Exit For
                        End If
                    Next
                Case RadarSettings.FilterType.Reverse
                    _filterPassed = True
                    For Each Filter As String In Filters
                        Filter = Filter.ToLower.Trim
                        If MobName.ToLower.Contains(Filter) OrElse MobID.ToString = Filter OrElse MobID.ToString("x2") = Filter Then
                            _filterPassed = False
                            Exit For
                        End If
                    Next
                    Return _filterPassed
                Case RadarSettings.FilterType.RegEx
                    Try
                        Return Regex.Match(MobName, Settings.NPCFilter).Success
                    Catch
                    End Try
                Case Else
                    Return True
            End Select
        Catch
            Return True
        End Try
    End Function
#End Region

#Region " SETTINGS EVENTS "
    Private Sub Setting_Changed(ByVal sender As Object, ByVal e As PropertyValueChangedEventArgs)
        Settings = SettingsForm.propGrid.SelectedObject
    End Sub
#End Region

#Region " WATCHER EVENTS "
    Protected Overridable Sub OnNewWatcherMobList(ByVal InMobs As MobList) Handles _watcher.NewMobList

        Me.Invalidate()
    End Sub

    Private Sub _Watcher_ZoneChanged(ByVal LastZone As Short, ByVal NewZone As Short) Handles _watcher.ZoneChanged
        Settings.CurrentMap = MemoryScanner.Scanner.CurrentMap
        _mobCount = Zones.GetZoneMobCount(Settings.CurrentMap)
    End Sub

    Private Sub _Watcher_PartyListUpdated(ByVal Members As String()) Handles _watcher.OnPartyListUpdated
        Me.PartyMembers = Members
    End Sub
#End Region
End Class


