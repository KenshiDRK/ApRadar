Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.Drawing.Drawing2D
Imports FFXIMemory
Imports DataLibrary

Public Class GraphicsEngine
    Inherits Component

#Region " LOCAL VARIABLES "
    Private clientCenter As New PointF(256, 256)
    Private mapCenter As New PointF(0, 0)
    Private mapViewOffset As New PointF(0, 0)
    Private _ffxi As FFXI

#End Region

#Region " ENUM "
    Public Enum RadarType
        Mapped
        Overlay
    End Enum
#End Region

#Region " PROPERTIES "
    Private _corePaintData As RadarPaintData
    <Browsable(False)> _
    Private ReadOnly Property CorePaintData() As RadarPaintData
        Get
            If _corePaintData Is Nothing Then
                _corePaintData = New RadarPaintData
            End If
            Return _corePaintData
        End Get
    End Property


    Private _settings As RadarSettings
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
    Private _mobs As MobList
    Private ReadOnly Property Mobs() As MobList
        Get
            Return _mobs
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

    Private _myData As MobData
    Private ReadOnly Property MyData() As MobData
        Get
            If _myData Is Nothing Then
                _myData = New MobData(_ffxi.POL, 0, True)
            End If
            Return _myData
        End Get
    End Property

    Private _zones As FFXIMemory.Zones
    Private ReadOnly Property Zones() As FFXIMemory.Zones
        Get
            If _zones Is Nothing Then
                _zones = New FFXIMemory.Zones
            End If
            Return _zones
        End Get
    End Property

    Private _dataLayer As DataAccess
    <Browsable(False)> _
    Private ReadOnly Property DataLayer() As DataAccess
        Get
            If _dataLayer Is Nothing Then
                _dataLayer = New DataAccess
            End If
            Return _dataLayer
        End Get
    End Property

    Private _mapData As MapHandler
    <Browsable(False)> _
    Public ReadOnly Property MapController() As MapHandler
        Get
            If _mapData Is Nothing Then
                _mapData = New MapHandler()
            End If
            Return _mapData
        End Get
    End Property

    Private _currentMapentry As MapData
    <Browsable(False)> _
    Public Property CurrentMapEntry() As MapData
        Get
            Return _currentMapentry
        End Get
        Set(ByVal value As MapData)
            _currentMapentry = value
        End Set
    End Property

    Private _mapImage As Bitmap
    <Browsable(False)> _
    Public Property MapImage() As Bitmap
        Get
            Return _mapImage
        End Get
        Set(ByVal value As Bitmap)
            _mapImage = value
        End Set
    End Property
#End Region

#Region " PUBLIC METHODS "
    Public Sub Initialize(ByVal SettingsFile As String)
        'Lets initialize the radar and add the settings
        'check to make sure the file exists
        If IO.File.Exists(SettingsFile) Then
            'Load the settings
            LoadSettings(SettingsFile)
        End If
    End Sub

    Public Sub RenderRadar(ByVal Surface As Graphics, ByVal type As RadarType)
        Try

            Surface.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            Surface.TextRenderingHint = Text.TextRenderingHint.AntiAliasGridFit
            Surface.CompositingQuality = Drawing2D.CompositingQuality.HighQuality



            Dim clientSurface As RectangleF = Surface.ClipBounds
            CorePaintData.CenterPoint = New PointF(clientSurface.Width / 2.0F, clientSurface.Height / 2.0F)
            If type = RadarType.Mapped Then

            Else
                CorePaintData.XScale = clientSurface.Width / 50.0F
                CorePaintData.YScale = clientSurface.Height / 50.0F
                PaintOverlayRadar(Surface)
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region " PRIVATE MEMBERS "
    Private Sub LoadSettings(ByVal Path As String)
        If IO.File.Exists(Path) Then
            Dim rs As RadarSettings = DeserializeSettings(Path)
            If Not rs Is Nothing Then
                If Not IsProEnabled Then
                    rs.ShowAll = False
                    rs.ShowId = False
                    rs.ShowCampedMobs = False
                End If
                Me.Settings = rs
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
#End Region

#Region " PAINTING "
#Region " OVERLAY RADAR "
    Private Sub PaintOverlayRadar(ByVal g As Graphics)
        If Settings.ShowMyPointer Then
            PaintMyPointer(g, RadarType.Overlay)
        End If
        'Paint the ranges 
        'Paint the spell casting range
        If Settings.ShowSpell Then
            PaintRange(g, 25, Pens.WhiteSmoke, RadarType.Overlay)
        End If
        'Paint the Job Ability range
        If Settings.ShowJobAbility Then
            PaintRange(g, 20, Pens.Yellow, RadarType.Overlay)
        End If
        'Paint the aggro range
        If Settings.ShowAggro Then
            PaintRange(g, 15, Pens.Tomato, RadarType.Overlay)
        End If

        'Paint the custom ranges
        If Not Settings.CustomRanges Is Nothing Then
            For Each entry As Range In Settings.CustomRanges
                PaintRange(g, entry.Size, New Pen(entry.RangeColor), RadarType.Overlay)
            Next
        End If
    End Sub
#End Region

#Region " SHARED PAINTING "
    ''' <summary>
    ''' Paints the triangle for player position in the center of the radar
    ''' </summary>
    ''' <param name="g">The graphics object used for painting</param>
    ''' <remarks></remarks>
    Private Sub PaintMyPointer(ByVal g As Graphics, ByVal Type As RadarType)
        If Type = RadarType.Mapped Then
            Dim path As New GraphicsPath()
            Dim rot As New Matrix()

            Dim degrees As Single = MyData.Direction * 180 / Math.PI

            If Settings.Zoom = 1 Then
                path.AddPolygon(New Point() {New Point(CorePaintData.MyScaledPosition.X, CorePaintData.MyScaledPosition.Y + 2), _
                                         New Point(CorePaintData.MyScaledPosition.X - 6, CorePaintData.MyScaledPosition.Y + 6), _
                                         New Point(CorePaintData.MyScaledPosition.X, CorePaintData.MyScaledPosition.Y - 10), _
                                         New Point(CorePaintData.MyScaledPosition.X + 6, CorePaintData.MyScaledPosition.Y + 6)} _
                            )
                rot.RotateAt(degrees + 90, CorePaintData.MyScaledPosition)
            Else
                path.AddPolygon(New Point() {New Point(CorePaintData.CenterPoint.X, CorePaintData.CenterPoint.Y + 2), _
                                         New Point(CorePaintData.CenterPoint.X - 6, CorePaintData.CenterPoint.Y + 6), _
                                         New Point(CorePaintData.CenterPoint.X, CorePaintData.CenterPoint.Y - 10), _
                                         New Point(CorePaintData.CenterPoint.X + 6, CorePaintData.CenterPoint.Y + 6)} _
                            )
                rot.RotateAt(degrees + 90, CorePaintData.CenterPoint)
                
            End If
            path.Transform(rot)
            g.FillPath(Brushes.Green, path)

        Else
            'Fill the pointer polygon
            g.FillPolygon(Brushes.Green, _
                          New PointF() {New PointF(CorePaintData.CenterPoint.X - 4, _
                                                   CorePaintData.CenterPoint.Y + 6), _
                                        New PointF(CorePaintData.CenterPoint.X, _
                                                   CorePaintData.CenterPoint.Y - 6), _
                                        New PointF(CorePaintData.CenterPoint.X + 4, _
                                                   CorePaintData.CenterPoint.Y + 6)})
        End If
    End Sub

    ''' <summary>
    ''' Paints a range on the radar
    ''' </summary>
    ''' <param name="g">The graphics object used for painting</param>
    ''' <param name="Range">The radius of the range</param>
    ''' <param name="pen">The pen used for drawing the line</param>
    ''' <remarks></remarks>
    Private Sub PaintRange(ByVal g As Graphics, ByVal Range As Single, ByVal pen As Pen, ByVal Type As RadarType)
        If Type = RadarType.Mapped Then
            Dim rangePoint As PointF = CurrentMapEntry.ConvertPosTo2D(MyData.X - Range, MyData.Y - Range)

            rangePoint.X = (MyData.MapX - rangePoint.X) * 2.0F
            rangePoint.Y = (MyData.MapY - rangePoint.Y) * 2.0F
            Dim pointX, pointY As Single

            If Settings.Zoom = 1.0F Then
                pointX = MyData.MapX * CorePaintData.MapScaleX * 2
                pointY = MyData.MapY * CorePaintData.MapScaleY * 2
                g.FillEllipse(New SolidBrush(Color.FromArgb(96, pen.Color)), pointX - rangePoint.X, _
                              pointY - rangePoint.Y, _
                              rangePoint.X * 2, _
                              rangePoint.Y * 2)
            Else
                g.FillEllipse(New SolidBrush(Color.FromArgb(96, pen.Color)), CorePaintData.CenterPoint.X - (rangePoint.X * Settings.Zoom) * CorePaintData.MapScaleX, _
                              CorePaintData.CenterPoint.Y - (rangePoint.Y * Settings.Zoom) * CorePaintData.MapScaleY, _
                              rangePoint.X * 2 * Settings.Zoom * CorePaintData.MapScaleX, _
                              rangePoint.Y * 2 * Settings.Zoom * CorePaintData.MapScaleY)
            End If
        Else
            g.DrawEllipse(pen, CorePaintData.CenterPoint.X - (Range * CorePaintData.YScale / 2), _
                          CorePaintData.CenterPoint.Y - (Range * CorePaintData.YScale / 2), _
                          Range * CorePaintData.YScale, Range * CorePaintData.YScale)
        End If
    End Sub
#End Region

#Region " STRINGS "
    Private Sub DrawOutlineString(ByVal g As Graphics, ByVal s As String, ByVal font As Font, ByVal StringBrush As Brush, ByVal Outlinebrush As Brush, ByVal x As Single, ByVal y As Single, ByVal size As Integer)
        Dim tan As Single = size / 2
        For bx = 0 To size
            For by = 0 To size
                g.DrawString(s, font, Outlinebrush, x - tan + bx, y - tan + by)
            Next
        Next
        g.DrawString(s, font, StringBrush, x, y)
    End Sub
#End Region
#End Region
End Class
