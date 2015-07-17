Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Xml.Serialization
Imports System.ComponentModel

<AttributeUsage(AttributeTargets.All), TypeConverter(GetType(ExpandableObjectConverter))> _
Public Class RadarSettings

    Public Sub New()
    End Sub

#Region " ENUM "
    Public Enum FilterType
        None
        Regular
        Reverse
        RegEx
    End Enum

    Public Enum RangeType
        Solid
        Ring
    End Enum
#End Region

#Region " EVENTS "
    Public Event OnRefreshChanged(ByVal RefreshRate As Integer)
    Public Event OnTrackedMobChanged(ByVal MobID As Integer)
    Public Event ZoneChanged(ByVal OldZone As Short, ByVal NewZone As Short)
#End Region

#Region " DISPLAY ITEMS "
#Region " MY ITEMS "
    <Category("My Data"), DisplayName("Show My Pointer"), _
    Description("Shows your pointer on the radar")> _
    Public Property ShowMyPointer() As Boolean = True

#End Region

#Region " PC "
    <Category("PC"), DisplayName("Show PC"), _
    Description("Shows nearby PC's on the radar")> _
    Public Property ShowPC() As Boolean = False
    <Category("PC"), DisplayName("Show PC Names"), _
    Description("Shows the PC's Name on the radar")> _
    Public Property ShowPCNames() As Boolean = False

    <XmlIgnore(), Category("PC"), DisplayName("PC Color"), _
    Description("The color used when painting the PC entries on the radar"), _
    DefaultValue(GetType(Color), "Blue")> _
    Public Property PCColor() As Color = Color.Blue

    <Browsable(False)> _
    Public Property PCColorHtml() As String
        Get
            Return ColorTranslator.ToHtml(_PCColor)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                _PCColor = ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property

    <XmlIgnore(), Category("PC"), DisplayName("Party Color"), _
    Description("The color used when painting the Party Member entries on the radar"), _
    DefaultValue(GetType(Color), "Blue")> _
    Public Property PartyColor() As Color = Color.Blue

    <Browsable(False)> _
    Public Property PartyColorHtml() As String
        Get
            Return ColorTranslator.ToHtml(_PartyColor)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                _PartyColor = ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property

    <XmlIgnore(), Category("PC"), DisplayName("Alliance Color"), _
    Description("The color used when painting the Alliance Member entries on the radar"), _
    DefaultValue(GetType(Color), "Blue")> _
    Public Property AllianceColor() As Color = Color.Blue


    <Browsable(False)> _
    Public Property AllianceColorHtml() As String
        Get
            Return ColorTranslator.ToHtml(_AllianceColor)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                _AllianceColor = ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property
    Public Property ShowPartyMembers() As Boolean
    Public Property ShowAllies() As Boolean
    <Category("PC"), DisplayName("PC Filter"), _
    Description("Gets or sets the filter string for PC entries")> _
    Public Property PCFilter() As String = ""
    <Category("PC"), DisplayName("PC Filter Type"), _
    Description("Gets or sets the filter type for PC entries")> _
    Public Property PCFilterType() As FilterType = FilterType.None
#End Region

#Region " NPC "
    <Category("NPC"), DisplayName("Show NPC"), _
    Description("Shows nearby NPC's on the radar")> _
    Public Property ShowNPC() As Boolean

    <Category("NPC"), DisplayName("Show Mobs"), _
    Description("Shows nearby mobs")> _
    Public Property ShowMobs As Boolean

    <Category("NPC"), DisplayName("Hide Objects and Doors"),
    Description("Hides NPC's that are flagged as Objects or Doors"),
    DefaultValue(False)>
    Public Property HideObjectsOrDoors As Boolean



    <Category("NPC"), DisplayName("Show NPC Names"), _
    Description("Shows the NPC's name on the radar")> _
    Public Property ShowNPCNames() As Boolean

    <XmlIgnore(), Category("NPC"), DisplayName("NPC Color"), _
    Description("The color used when painting the NPC entries on the radar"), _
    DefaultValue(GetType(Color), "Red")> _
    Public Property NPCColor() As Color = Color.Red

    <Browsable(False)> _
    Public Property NPCColorHtml() As String
        Get
            Return ColorTranslator.ToHtml(_NPCColor)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                _NPCColor = ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property

    <XmlIgnore(), Category("NPC"), DisplayName("Target Highlight Color"), _
    Description("The color used when highlighting your target on the radar"), _
    DefaultValue(GetType(Color), "Black")> _
    Public Property TargetHighlightColor() As Color = Color.Black

    <Browsable(False)> _
    Public Property TargetHighlightColorHtml() As String
        Get
            Return ColorTranslator.ToHtml(TargetHighlightColor)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                TargetHighlightColor = ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property

    <XmlIgnore(), Category("NPC"), DisplayName("Mob Color"), _
    Description("The color used when painting the Mob entries on the radar"), _
    DefaultValue(GetType(Color), "Red")> _
    Public Property MobColor() As Color = Color.Red


    <Browsable(False)> _
    Public Property MobColorHtml() As String
        Get
            Return ColorTranslator.ToHtml(_MobColor)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                _MobColor = ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property

    <XmlIgnore(), Category("NPC"), DisplayName("Link Color"), _
    Description("The color used when painting the Linked mob entries on the radar"), _
    DefaultValue(GetType(Color), "Red")> _
    Public Property LinkColor() As Color = Color.Red


    <Browsable(False)> _
    Public Property LinkColorHtml() As String
        Get
            Return ColorTranslator.ToHtml(_LinkColor)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                _LinkColor = ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property
    <XmlIgnore(), Category("NPC"), DisplayName("Camped Mob Color"), _
    Description("The color used when painting the Camped Mobs on the radar"), _
    DefaultValue(GetType(Color), "Red")> _
    Public Property CampedColor() As Color = Color.SeaGreen

    <Browsable(False)> _
    Public Property CampedColorHtml() As String
        Get
            Return ColorTranslator.ToHtml(CampedColor)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                CampedColor = ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property
    <XmlIgnore(), Category("NPC"), DisplayName("NM Color"), _
    Description("The color used when painting the Notorious Monster entries on the radar"), _
    DefaultValue(GetType(Color), "HotPink")> _
    Public Property NMColor() As Color = Color.HotPink

    <Browsable(False)> _
    Public Property NMColorHtml() As String
        Get
            Return ColorTranslator.ToHtml(NMColor)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                NMColor = ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property
    <Category("NPC"), DisplayName("NPC Filter"), _
    Description("Gets or sets the filter string for NPC entries")> _
    Public Property NPCFilter() As String = ""
    <Category("NPC"), DisplayName("NPC Filter Type"), _
    Description("Gets or sets the filter type for NPC entries")> _
    Public Property NPCFilterType() As FilterType = FilterType.None
    <Category("NPC"), DisplayName("Show Camped Mobs"), DefaultValue(GetType(Boolean), "False"), _
    Description("Show the camped mobs on the radar")> _
    Public Property ShowCampedMobs() As Boolean = False
#End Region

#Region " GENERAL PROPERTIES "
    ''' <summary>
    ''' Show the mobs HP percentage next to their name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Mob Data"), DisplayName("Show HP"), _
    Description("Displays the mobs current HP percentage on the radar")> _
    Public Property ShowHP() As Boolean = False
    ''' <summary>
    ''' Show all mobs, including dead or invisible mobs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("General"), DisplayName("Show All"), _
    Description("Shows all mobs found in memory on the radar. This will show mobs with 0%HP and mobs that have been flagged as dead")> _
    Public Property ShowAll() As Boolean = False
    ''' <summary>
    ''' Show the mobs id next to their name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Mob Data"), DisplayName("Show Mob ID"), _
    Description("Displays the mobs ID on the radar")> _
    Public Property ShowId() As Boolean = False
    ''' <summary>
    ''' Show the compass on the radar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("General"), DisplayName("Show Compass"), _
    Description("Displays a compass on the radar. This setting is not used on the Mapped radar")> _
    Public Property ShowCompass() As Boolean = False
    ''' <summary>
    ''' Show the distance to the mob from your position on the radar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Mob Data"), DisplayName("Show Distance"), _
    Description("Displays the distance to each mob on the radar")> _
    Public Property ShowDistance() As Boolean = False
    ''' <summary>
    ''' Hides mobs that are on other floors
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("General"), DisplayName("Hide Models on Othere Floors"), _
    Description("Hides any PC or NPC that is on a different floor than you")> _
    Public Property HideOtherFloors() As Boolean = False
    ''' <summary>
    ''' Gets or sets the size of the blips on the radar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("General"), DisplayName("Blip Size"), _
    Description("Sets the size of the blip for each entry on the radar")> _
    Public Property BlipSize() As Integer = 4

    Private _refreshRate As Integer = 100
    ''' <summary>
    ''' Sets the data refresh rate for the radar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("General"), DisplayName("Refresh Rate"), _
    Description("Sets the memory scanning rate for the radar in milliseconds. On slower systems it is recommended to use a slower rate of scanning")> _
    Public Property RefreshRate() As Integer
        Get
            Return _refreshRate
        End Get
        Set(ByVal value As Integer)
            _refreshRate = value
            RaiseEvent OnRefreshChanged(value)
        End Set
    End Property
    ''' <summary>
    ''' Draw a line on the radar to the current target
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("General"), DisplayName("Draw Line To Target"), _
    Description("Draws a line from your position to your current target")> _
    Public Property DrawLineToTarget() As Boolean = False

    <Category("General"), DisplayName("Always Show Target"),
    Description("Gets or sets whether or not the current target is shown regardless of filters"),
    DefaultValue(True)>
    Public Property AlwaysShowTarget As Boolean = True
    ''' <summary>
    ''' Shows or hides the sight cone for each mob
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Mob Data"), DisplayName("Show Mobs Sight"), _
    Description("Displays the mobs sight direction on the radar")> _
    Public Property ShowSight() As Boolean = False
    <Category("General"), DisplayName("Show Tracker"), _
    Description("Shows an arrow pointing in the direction to yor selected tracked mob. This is not currently implemented.")> _
    Public Property ShowTracker() As Boolean = False
    <Category("Ranges"), DisplayName("Show Aggro Range"), _
    Description("Shows a ring on the radar to signify your aggro range")> _
    Public Property ShowAggro() As Boolean = False
    <Category("Ranges"), DisplayName("Show Spell Range"), _
    Description("Shows a ring on the radar to signify your spell casting range")> _
    Public Property ShowSpell() As Boolean = False
    <Category("Ranges"), DisplayName("Show Job Ability Range"), _
    Description("Shows a ring on the radar to signify your job ability range")> _
    Public Property ShowJobAbility() As Boolean = False
    <Category("Ranges"), DisplayName("Show Visible Range"), _
    Description("Shows a ring on the radar to signify your highest visible range")> _
    Public Property ShowVisibleRange() As Boolean = False
    <Category("General"), DisplayName("Show Target Info"), _
    Description("Shows info about your current target on the radar. This method is not used on the Mapped radar")> _
    Public Property ShowTargetInfo() As Boolean = False
    <Category("Ranges"), DisplayName("Custom Ranges"), _
    Description("A collection of custom ranges to be dispalyed on the radar")> _
    Public Property CustomRanges() As Range()
    <Category("Ranges"), DisplayName("Range display"), _
    Description("Diaply type for the ranges"), DefaultValue(RangeType.Solid)> _
    Public Property RangeDisplay() As RangeType = RangeType.Solid
    <Category("General"), DisplayName("Hide Target Info In Combat"), _
    Description("Gets or sets the value that determines if the info panel should be hidden in combat")> _
    Public Property HideInfoInCombat() As Boolean = True

    Private _trackedMob As Integer
    <XmlIgnore(), Browsable(False)> _
    Public Property TrackedMob() As Integer
        Get
            Return _trackedMob
        End Get
        Set(ByVal value As Integer)
            _trackedMob = value
            RaiseEvent OnTrackedMobChanged(value)
        End Set
    End Property

    Private _shadowBrush As SolidBrush
    <XmlIgnore(), Browsable(False)> _
    Public ReadOnly Property ShadowBrush() As SolidBrush
        Get
            If _shadowBrush Is Nothing Then
                _shadowBrush = New SolidBrush(Color.FromArgb(30, 64, 64, 64))
            End If
            Return _shadowBrush
        End Get
    End Property

    Private _standardMatrix As Matrix
    <XmlIgnore(), Browsable(False)> _
    Public ReadOnly Property StandardMatrix() As Matrix
        Get
            If _standardMatrix Is Nothing Then
                _standardMatrix = New Matrix(1.0, 0, 0, 1.0, 0, 0)
            End If
            Return _standardMatrix
        End Get
    End Property

    Private _shadowMatrix As Matrix
    <XmlIgnore(), Browsable(False)> _
    Public ReadOnly Property ShadowMatrix() As Matrix
        Get
            If _shadowMatrix Is Nothing Then
                _shadowMatrix = New Matrix(0.25F, 0, 0, 0.25F, 3, 3)
            End If
            Return _shadowMatrix
        End Get
    End Property

    Private _dataFont As Font
    <XmlIgnore(), _
     Category("General"), DisplayName("Data Font"), _
     Description("Gets or sets the radar data font. This font is used for the header info and the Mob Info panel.  This setting is not used on the Mapped Radar"), _
     DefaultValue(GetType(Font), "Calibri, 8pt")> _
    Public Property DataFont() As Font
        Get
            If _dataFont Is Nothing Then
                _dataFont = New Font("Calibri", 8, FontStyle.Regular)
            End If
            Return _dataFont
        End Get
        Set(ByVal value As Font)
            _dataFont = value
        End Set
    End Property

    <Browsable(False)> _
    Public Property DataFontName() As String
        Get
            Return FontConverter.ToBase64String(_dataFont)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                _dataFont = FontConverter.FromBase64String(value)
            End If
        End Set
    End Property
    Public Property Font() As String

    Private _pingBrush As SolidBrush
    <XmlIgnore(), Browsable(False)> _
    Public ReadOnly Property PingBrush() As SolidBrush
        Get
            If _pingBrush Is Nothing Then
                _pingBrush = New SolidBrush(Color.Black)
            End If
            Return _pingBrush
        End Get
    End Property
    <Category("General"), DisplayName("Show Map <POS>"), _
    Description("Shows your current <pos> on the top of the radar")> _
    Public Property ShowPOS() As Boolean
    <Category("General"), DisplayName("Zoom Level"), _
     Description("This is the zoom level of the radar.  This property is only valid if the Radar Type is Mapped")> _
    Public Property Zoom() As Single = 1.0F
    <Category("General"), DisplayName("Show Header Text"), _
     Description("This will turn on or off the header text in the overlay radar."), _
     DefaultValue(GetType(Boolean), "True")> _
    Public Property ShowHeaderText() As Boolean = True
    <Browsable(False)> _
    Public Property Location() As Point = New Point(100, 100)
    <Browsable(False)> _
    Public Property Size() As Size = New Size(512, 512)
    Public Property StayOnTop() As Boolean
    Public Property ClickThrough() As Boolean
    Public Property DisableDocking() As Boolean
    Public Property DisableDragging() As Boolean
    Public Property ShowFilterPanel() As Boolean
    Public Property Transparency() As Single = 1.0F

    Private _mapOpacity As Single = 0.8
    Public Property MapOpacity As Single
        Get
            Return _mapOpacity
        End Get
        Set(ByVal value As Single)
            If value < 0 Then
                value = 0
            ElseIf value > 1 Then
                value = 1
            End If
            _mapOpacity = value
            MapMatrix.Matrix33 = value
            MapAttributes.SetColorMatrix(MapMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)
        End Set
    End Property

    Private _mapAttributes As ImageAttributes
    <XmlIgnore()>
    Public ReadOnly Property MapAttributes As ImageAttributes
        Get
            If _mapAttributes Is Nothing Then
                _mapAttributes = New ImageAttributes()
                _mapAttributes.SetColorMatrix(Me.MapMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)
            End If
            Return _mapAttributes
        End Get
    End Property

    Private _mapMatrix As ColorMatrix
    <XmlIgnore()>
    Public ReadOnly Property MapMatrix As ColorMatrix
        Get
            If _mapMatrix Is Nothing Then
                _mapMatrix = New ColorMatrix() With {.Matrix33 = Me.MapOpacity}
            End If
            Return _mapMatrix
        End Get
    End Property

    <Category("General"), DisplayName("Track VNM"), _
     Description("This will turn on or off the VNM tracker."), _
     DefaultValue(GetType(Boolean), "False")> _
    Public Property TrackVNM As Boolean

    <Category("General"), DisplayName("Show Pedometer"), _
     Description("This will show a pedometer ont he radar when tracking VNM.  IT will be the distance you have traveled since the point of resting."), _
     DefaultValue(GetType(Boolean), "False")> _
    Public Property ShowPedometer As Boolean
#End Region
#End Region

#Region " CURRENT DATA "
    Private _currentMap As Short = -1
    <XmlIgnore(), Browsable(False)> _
    Public Property CurrentMap() As Short
        Get
            Return _currentMap
        End Get
        Set(ByVal value As Short)
            If value <> _currentMap Then
                RaiseEvent ZoneChanged(_currentMap, value)
            End If
            _currentMap = value
        End Set
    End Property
    <XmlIgnore(), Browsable(False)> _
    Public Property MapLevel() As Short = -1
#End Region

#Region " OVERRIDES "
    Public Overrides Function ToString() As String
        Return String.Empty
    End Function
#End Region
End Class
