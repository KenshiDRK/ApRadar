Imports FFXIMemory
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class MapRadar
    Inherits RadarBase

#Region " MEMBER VARIABLES "
    Private _isMobCamped As Boolean
    Private _lastMapFile As String
    Private _mobPoint As PointF
#End Region

#Region " DELEGATES "
    Public Delegate Sub NewMobListEventHandler(ByVal mobs As Contracts.Shared.MobData())
#End Region

#Region " EVENTS "
    Public Event NewMobList As NewMobListEventHandler
#End Region

#Region " PROPERTIES "
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

    <Browsable(False)> _
    Public Property CurrentMapEntry() As MapData

    <Browsable(False)> _
    Public Property MapImage() As Bitmap

    Private _mapPath As String = My.Settings.MapsLocation
    Public Property MapPath() As String
        Get
            If _mapPath = "/Maps" Then
                My.Settings.MapsLocation = String.Format("{0}/Maps", Application.StartupPath)
                My.Settings.Save()
                _mapPath = My.Settings.MapsLocation
            End If
            Return _mapPath
        End Get
        Set(ByVal value As String)
            If value = "Default" Then
                value = My.Settings.MapsLocation
            End If
            MapController.MapsPath = value
        End Set
    End Property

    Private Property LinkServerRunning As Boolean

    Private _myLinkMobs As List(Of Contracts.Shared.MobData)
    Private ReadOnly Property MyLinkMobs As List(Of Contracts.Shared.MobData)
        Get
            If _myLinkMobs Is Nothing Then
                _myLinkMobs = New List(Of Contracts.Shared.MobData)
            End If
            Return _myLinkMobs
        End Get
    End Property

    Private _linkMobs As Dictionary(Of String, Contracts.Shared.MobData())
    Public ReadOnly Property LinkMobs() As Dictionary(Of String, Contracts.Shared.MobData())
        Get
            If _linkMobs Is Nothing Then
                _linkMobs = New Dictionary(Of String, Contracts.Shared.MobData())
            End If
            Return _linkMobs
        End Get
    End Property
#End Region

#Region " OVERRIDES "
    Protected Overrides Sub OnNewWatcherMobList(ByVal InMobs As FFXIMemory.MobList)
        'Lock it down to prevent dual access to the moblist
        If Not InMobs Is Nothing Then
            MyData = InMobs.Item(MemoryScanner.Scanner.MyID)
            TargetData = InMobs.Item(MemoryScanner.Scanner.TargetID)

            SyncLock (syncObj)
                Me.Mobs = New List(Of MobData)(InMobs.ToClonedArray)

                MyLinkMobs.Clear()

                'Make sure we are either in overlay radar or we are using a good map
                If CheckMap() Then
                    'TODO: HAndle mydata and target data. Need to remove all the scanning in this control. 


                    If Settings.ShowNPC OrElse Settings.ShowPC OrElse Settings.ShowPartyMembers Then
                        'Lets check on showing party members

                        For Each mob As MobData In Me.Mobs
                            'mob.Filters = New MobData.FilterInfo With {.MapFiltered = False, .OverlayFiltered = False}

                            'If (Settings.ShowNPC) OrElse (mob.MobType = MobData.MobTypes.PC AndAlso (Settings.ShowPC OrElse Settings.ShowPartyMemebrs)) Then
                            '    If Settings.HideOtherFloors AndAlso Math.Abs(mob.Z - MyData.Z) >= 6 Then
                            '        'We are hiding mobs on other floors, so lets filter this one out and move on
                            '        mob.Filters = True
                            '    Else
                            '        If mob.MobType = MobData.MobTypes.PC Then
                            '            If Settings.ShowPC AndAlso (Settings.ShowAll OrElse (mob.WarpInfo > 0 AndAlso mob.ID <> MyData.ID)) Then
                            '                If Settings.PCFilterType <> RadarSettings.FilterType.None AndAlso Not Settings.PCFilter Is Nothing Then
                            '                    'Grab each of the filter conditions
                            '                    Dim filters As String() = Settings.PCFilter.Split(",")
                            '                    'Check the filter type
                            '                    If Not CheckFilter(Settings.PCFilterType, mob.Name, mob.ID, filters) Then
                            '                        mob.Filters = True
                            '                    End If
                            '                ElseIf Settings.ShowPartyMemebrs Then
                            '                    Dim isPartyMember As Boolean
                            '                    For Each p As String In PartyMembers
                            '                        If p <> String.Empty AndAlso mob.Name = p Then
                            '                            isPartyMember = True
                            '                            Exit For
                            '                        End If
                            '                    Next
                            '                    If Not isPartyMember Then
                            '                        mob.Filters = True
                            '                    End If

                            '                End If
                            '            Else
                            '                mob.Filters = True
                            '            End If
                            '        Else
                            '            _isMobCamped = False
                            '            'Lets check if the mob is camped
                            '            If Me.Settings.ShowCampedMobs Then
                            '                _isMobCamped = CampedMobExists(mob.ServerID)
                            '            End If

                            '            If Settings.ShowNPC AndAlso (Settings.ShowAll OrElse (mob.WarpInfo > 0 AndAlso mob.Name <> "NPC" AndAlso mob.HP > 0) OrElse _isMobCamped) Then
                            '                If Settings.NPCFilterType <> RadarSettings.FilterType.None And Not Settings.NPCFilter Is Nothing Then
                            '                    Dim filters As String() = Settings.NPCFilter.Split(",")
                            '                    If Not CheckFilter(Settings.NPCFilterType, mob.Name, mob.ID, filters) Then
                            '                        mob.Filters = True
                            '                    End If
                            '                End If
                            '                If LinkServerRunning Then
                            '                    MyLinkMobs.Add(New Contracts.Shared.MobData(Settings.CurrentMap, mob.Name, mob.ID, mob.HP, New Contracts.Shared.Position(mob.X, mob.Y, mob.Z)))
                            '                End If
                            '            Else
                            '                mob.Filters = True
                            '            End If
                            '        End If
                            '    End If
                            'Else
                            '    mob.Filters = True
                            'End If
                        Next

                    End If
                End If
                If LinkServerRunning Then
                    Dim myMob As New Contracts.Shared.MobData(Settings.CurrentMap, MyData.Name, MyData.ID, MyData.HP, New Contracts.Shared.Position(MyData.X, MyData.Y, MyData.Z)) With {.IsPC = True}
                    MyLinkMobs.Add(myMob)
                End If
            End SyncLock

        End If
        MyBase.OnNewWatcherMobList(InMobs)
    End Sub

    Private Sub FilterMob(ByRef Mob As MobData)
    End Sub

    Protected Overrides Sub PaintRadar(ByRef g As System.Drawing.Graphics)
        g.SmoothingMode = SmoothingMode
        g.TextRenderingHint = TextRendering
        g.CompositingQuality = CompositingQuality

        'Set the center point of the control for painting later
        CorePaintData.CenterPoint = New PointF(CSng(Width) / 2.0F, CSng(Height) / 2.0F)

        'Set the current map
        Settings.CurrentMap = MemoryScanner.Scanner.CurrentMap

        Dim size As New SizeF(512.0F, 512.0F)
        If Not MapImage Is Nothing Then
            size = MapImage.Size
        End If
        CorePaintData.XScale = CSng(Width) / size.Width * Settings.Zoom
        CorePaintData.YScale = CSng(Width) / size.Height * Settings.Zoom
        'Get the map scale values
        CorePaintData.MapScaleX = CSng(Width) / size.Width
        CorePaintData.MapScaleY = CSng(Height) / size.Height
        PaintMappedRadar(g)
    End Sub

    Protected Overrides Sub PaintMyPointer(ByVal g As System.Drawing.Graphics)
        Dim path As New GraphicsPath()


        'rotate it 90° clockwise using the center of the object
        '_pointerEnd.Y = (Math.Sin(MyData.Direction) * 10.0F)
        '_pointerEnd.X = (_pointerEnd.Y / Math.Tan(MyData.Direction))
        Using rot As New Matrix()
            Dim degrees As Single = MyData.Direction * 180 / Math.PI
            If Settings.Zoom = 1 Then
                path.AddPolygon(New Point() {New Point(CorePaintData.MyScaledPosition.X, CorePaintData.MyScaledPosition.Y + 2), New Point(CorePaintData.MyScaledPosition.X - 6, CorePaintData.MyScaledPosition.Y + 6), New Point(CorePaintData.MyScaledPosition.X, CorePaintData.MyScaledPosition.Y - 10), New Point(CorePaintData.MyScaledPosition.X + 6, CorePaintData.MyScaledPosition.Y + 6)})
                rot.RotateAt(degrees + 90, CorePaintData.MyScaledPosition)
                'g.FillEllipse(Brushes.Green, CorePaintData.MyScaledPosition.X - 3.0F, CorePaintData.MyScaledPosition.Y - 3.0F, 6, 6)
                'g.DrawLine(New Pen(Color.Green, 2), CorePaintData.MyScaledPosition.X, CorePaintData.MyScaledPosition.Y, CorePaintData.MyScaledPosition.X + _pointerEnd.X, CorePaintData.MyScaledPosition.Y + _pointerEnd.Y)
            Else
                path.AddPolygon(New Point() {New Point(CorePaintData.CenterPoint.X, CorePaintData.CenterPoint.Y + 2), New Point(CorePaintData.CenterPoint.X - 6, CorePaintData.CenterPoint.Y + 6), New Point(CorePaintData.CenterPoint.X, CorePaintData.CenterPoint.Y - 10), New Point(CorePaintData.CenterPoint.X + 6, CorePaintData.CenterPoint.Y + 6)})
                rot.RotateAt(degrees + 90, CorePaintData.CenterPoint)
                'g.FillEllipse(Brushes.Green, CorePaintData.CenterPoint.X - 3.0F, CorePaintData.CenterPoint.Y - 3.0F, 6, 6)
                'g.DrawLine(New Pen(Color.Green, 2), CorePaintData.CenterPoint.X, CorePaintData.CenterPoint.Y, CorePaintData.CenterPoint.X + _pointerEnd.X, CorePaintData.CenterPoint.Y + _pointerEnd.Y)
            End If
            path.Transform(rot)
        End Using
        g.FillPath(Brushes.Green, path)
    End Sub

    Protected Overrides Sub PaintRange(ByVal g As System.Drawing.Graphics, ByVal Range As Single, ByVal pen As System.Drawing.Pen)
        Dim rangePoint As PointF = CurrentMapEntry.ConvertPosTo2D(MyData.X - Range, MyData.Y - Range)

        rangePoint.X = (MyData.MapX - rangePoint.X) * 2.0F
        rangePoint.Y = (MyData.MapY - rangePoint.Y) * 2.0F
        Dim pointX, pointY As Single

        If Settings.Zoom = 1.0F Then
            pointX = MyData.MapX * CorePaintData.MapScaleX * 2
            pointY = MyData.MapY * CorePaintData.MapScaleY * 2
            If Settings.RangeDisplay = RadarSettings.RangeType.Solid Then
                g.FillEllipse(New SolidBrush(Color.FromArgb(96, pen.Color)), pointX - rangePoint.X, _
                              pointY - rangePoint.Y, _
                              rangePoint.X * 2, _
                              rangePoint.Y * 2)
            Else
                g.DrawEllipse(pen, pointX - rangePoint.X, _
                              pointY - rangePoint.Y, _
                              rangePoint.X * 2, _
                              rangePoint.Y * 2)
            End If
        Else
            If Settings.RangeDisplay = RadarSettings.RangeType.Solid Then
                g.FillEllipse(New SolidBrush(Color.FromArgb(96, pen.Color)), CorePaintData.CenterPoint.X - (rangePoint.X * Settings.Zoom) * CorePaintData.MapScaleX, _
                              CorePaintData.CenterPoint.Y - (rangePoint.Y * Settings.Zoom) * CorePaintData.MapScaleY, _
                              rangePoint.X * 2 * Settings.Zoom * CorePaintData.MapScaleX, _
                              rangePoint.Y * 2 * Settings.Zoom * CorePaintData.MapScaleY)
            Else
                g.DrawEllipse(pen, CorePaintData.CenterPoint.X - (rangePoint.X * Settings.Zoom) * CorePaintData.MapScaleX, _
                              CorePaintData.CenterPoint.Y - (rangePoint.Y * Settings.Zoom) * CorePaintData.MapScaleY, _
                              rangePoint.X * 2 * Settings.Zoom * CorePaintData.MapScaleX, _
                              rangePoint.Y * 2 * Settings.Zoom * CorePaintData.MapScaleY)
            End If
        End If
    End Sub
#End Region

#Region " PAINT METHODS "
    ''' <summary>
    ''' Paint method for the mapped radar
    ''' </summary>
    ''' <param name="g">The graphics object to use for painting</param>
    ''' <remarks></remarks>
    Private Sub PaintMappedRadar(ByVal g As Graphics)
        Try
            'Check to see if the CurrentMapEntry exists
            If MyData IsNot Nothing Then
                If CurrentMapEntry Is Nothing Then
                    PaintNoMap(g)
                Else
                    PaintWithMap(g)
                End If

                If Settings.ShowPOS Then
                    PaintMyPOS(g)
                End If

            End If

        Catch ex As Exception
            'Debug.Print(Ex.Message)
        End Try
    End Sub


    Private Sub PaintNoMap(ByVal g As Graphics)
        'There is not entry found so we are goint to use the center of the map
        CorePaintData.XScale = Width / 50
        CorePaintData.YScale = Height / 50
        CorePaintData.MyScaledPosition = CorePaintData.CenterPoint
        PaintMap(g)
        MobIDList.Clear()
        SyncLock (syncObj)
            For Each Mob As MobData In Mobs.ToList
                If Not Mob.Filters.MapFiltered Then
                    'Get the mobs distance from my position
                    Mob.XDistance = (Mob.X - MyData.X) * CorePaintData.XScale / 2
                    Mob.YDistance = -(Mob.Y - MyData.Y) * CorePaintData.YScale / 2
                    'Get the radius of the mobs distance path
                    Mob.Radius = Math.Sqrt(Mob.XDistance ^ 2 + Mob.YDistance ^ 2)
                    'Calculate the angle from 0 of the mob

                    If Mob.YDistance <= 0 Then
                        Mob.Degrees = -Mob.Degrees
                    Else
                        Mob.Degrees = Math.Acos(Mob.XDistance / Mob.Radius)
                    End If

                    Mob.Degrees *= (180 / Math.PI)
                    'Calculate my angle
                    MyData.Degrees = MyData.Direction * (180 / Math.PI)
                    If MyData.Degrees < 0 Then
                        MyData.Degrees += 360
                    End If

                    'Fix the mobs degrees in relation to mine
                    Mob.Degrees -= MyData.Degrees
                    If Mob.Degrees < 0 Then
                        Mob.Degrees += 360
                    End If
                    'Convert the degrees to radians
                    Mob.Degrees *= (Math.PI / 180)

                    'Calculate the x and y coordinates for the radar
                    Mob.MapX = CorePaintData.CenterPoint.X + (Mob.Radius * Math.Cos(Mob.Degrees + CorePaintData.NinetyDegrees))
                    Mob.MapY = CorePaintData.CenterPoint.Y + (Mob.Radius * Math.Sin(Mob.Degrees + CorePaintData.NinetyDegrees))
                    If Mob.MobType = MobData.MobTypes.PC Then
                        If Settings.ShowPC OrElse Settings.ShowPartyMembers Then
                            PaintMob(g, Mob)
                        End If
                    Else

                        If Settings.ShowNPC Then
                            PaintMob(g, Mob)
                        End If
                    End If
                End If

            Next
        End SyncLock
    End Sub

    Private Sub PaintWithMap(ByVal g As Graphics)
        'We have an entry so lets use it
        'Set myposition on the map
        CorePaintData.MyPosition = CurrentMapEntry.ConvertPosTo2D(MyData.X, MyData.Y)
        MyData.MapX = CorePaintData.MyPosition.X
        MyData.MapY = CorePaintData.MyPosition.Y
        'Save my scaled position for the map scaled at 0
        CorePaintData.MyScaledPosition = New PointF(CorePaintData.MyPosition.X * CorePaintData.MapScaleX * 2.0F, _
                                                    CorePaintData.MyPosition.Y * CorePaintData.MapScaleY * 2.0F)
        CorePaintData.MapW = (CorePaintData.MyScaledPosition.X - CorePaintData.CenterPoint.X) / (CorePaintData.MapScaleX)
        CorePaintData.MapH = (CorePaintData.MyScaledPosition.Y - CorePaintData.CenterPoint.Y) / (CorePaintData.MapScaleY)
        'Paint the map on the background
        PaintMap(g)

        'Paint the ranges
        'Paint the spell casting range
        If Settings.ShowSpell Then
            PaintRange(g, 25, Pens.WhiteSmoke)
        End If
        'Paint the Job Ability range
        If Settings.ShowJobAbility Then
            PaintRange(g, 20, Pens.Yellow)
        End If
        'Paint the aggro range
        If Settings.ShowAggro Then
            PaintRange(g, 15, Pens.Tomato)
        End If
        'Paint the max visible range
        If Settings.ShowVisibleRange Then
            PaintRange(g, 50, Pens.LimeGreen)
        End If

        'Paint the custom ranges
        If Not Settings.CustomRanges Is Nothing Then
            For Each entry As Range In Settings.CustomRanges
                PaintRange(g, entry.Size, New Pen(entry.RangeColor))
            Next
        End If

        'Paint my pointer
        If Settings.ShowMyPointer Then
            PaintMyPointer(g)
        End If

        'Paint all the mobs
        SyncLock (syncObj)
            MobIDList.Clear()
            For Each mob As MobData In Mobs.ToList
                If Not mob.Filters.MapFiltered Then
                    'Get the mobs relative position on the map
                    _mobPoint = CurrentMapEntry.ConvertPosTo2D(mob.X, mob.Y)
                    If Settings.Zoom = 1.0 Then
                        mob.MapX = _mobPoint.X * CorePaintData.MapScaleX * 2.0F
                        mob.MapY = _mobPoint.Y * CorePaintData.MapScaleY * 2.0F
                    Else
                        mob.MapX = CorePaintData.CenterPoint.X + ((_mobPoint.X * 2.0F / CorePaintData.MapScaleX - CorePaintData.MyPosition.X * 2.0F / CorePaintData.MapScaleX) * _
                            CorePaintData.XScale) * (CorePaintData.MapScaleX)
                        mob.MapY = CorePaintData.CenterPoint.Y + ((_mobPoint.Y * 2.0F / CorePaintData.MapScaleY - CorePaintData.MyPosition.Y * 2.0F / CorePaintData.MapScaleY) * _
                            CorePaintData.YScale) * (CorePaintData.MapScaleY)
                    End If

                    If mob.MobType = MobData.MobTypes.PC Then
                        If Settings.ShowPC OrElse Settings.ShowPartyMembers Then
                            PaintMob(g, mob)
                        End If
                    Else

                        If Settings.ShowNPC Then
                            MobIDList.Add(mob.ID)
                            PaintMob(g, mob)
                        End If
                    End If

                End If
            Next

            'Handle any link mobs
            If LinkServerRunning Then
                ProcessLinkMobs(g)
            End If
        End SyncLock
    End Sub

    Private Sub ProcessLinkMobs(ByVal g As Graphics)
        For Each MobList As Contracts.Shared.MobData() In LinkMobs.Values
            For Each mob In MobList
                If Not MobIDList.Contains(mob.ID) Then
                    MobIDList.Add(mob.ID)
                    'If Settings.NPCFilterType <> RadarSettings.FilterType.None Then
                    Dim filters As String() = Settings.NPCFilter.Split(",")
                    If Not mob.IsPC AndAlso CheckFilter(Settings.NPCFilterType, mob.Name, mob.ID, filters) Then
                        _mobPoint = CurrentMapEntry.ConvertPosTo2D(mob.Pos.X, mob.Pos.Y)
                        If Settings.Zoom = 1.0 Then
                            mob.Pos.MapX = _mobPoint.X * CorePaintData.MapScaleX * 2.0F
                            mob.Pos.MapY = _mobPoint.Y * CorePaintData.MapScaleY * 2.0F
                        Else
                            mob.Pos.MapX = CorePaintData.CenterPoint.X + ((_mobPoint.X * 2.0F / CorePaintData.MapScaleX - CorePaintData.MyPosition.X * 2.0F / CorePaintData.MapScaleX) * _
                                CorePaintData.XScale) * (CorePaintData.MapScaleX)
                            mob.Pos.MapY = CorePaintData.CenterPoint.Y + ((_mobPoint.Y * 2.0F / CorePaintData.MapScaleY - CorePaintData.MyPosition.Y * 2.0F / CorePaintData.MapScaleY) * _
                                CorePaintData.YScale) * (CorePaintData.MapScaleY)
                        End If
                        PaintLinkMob(g, mob)
                    End If
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Paints the map on the background of the control
    ''' </summary>
    ''' <param name="g">The graphics object used for painting</param>
    ''' <remarks></remarks>
    Private Sub PaintMap(ByVal g As Graphics)
        If Not MapImage Is Nothing Then
            If Settings.Zoom = 1 Then
                BackgroundImageLayout = ImageLayout.Stretch
                'Me.BackgroundImage = MapImage
                g.DrawImage(MapImage, _
                            New RectangleF(0.0F, 0.0F, Width, Height), _
                            New RectangleF(0.0F, 0.0F, MapImage.Width, MapImage.Height), _
                            GraphicsUnit.Pixel)
            Else
                BackgroundImage = Nothing
                Dim xZoom As Single = MapImage.Width / Settings.Zoom
                Dim yZoom As Single = MapImage.Height / Settings.Zoom
                Dim xShift As Single = (MapImage.Width - xZoom) / 2.0F
                Dim yShift As Single = (MapImage.Height - yZoom) / 2.0F
                ' Dim img As New Bitmap(Me.Width, Me.Height)
                'Dim ig As Graphics = Graphics.FromImage(img)
                g.DrawImage(MapImage, _
                            New RectangleF(0.0F, 0.0F, Width, Height), _
                            New RectangleF(xShift + CorePaintData.MapW, yShift + CorePaintData.MapH, xZoom, yZoom), _
                            GraphicsUnit.Pixel)
                'Me.BackgroundImageLayout = ImageLayout.Stretch
                'Me.BackgroundImage = img
                'This is not currently implemented as I can't seem to get it looking
                'the way that I want it to.
                'PaintMiniMap(g, shift + CorePaintData.MapW, shift + CorePaintData.MapH, zoomSize)
            End If
        End If
    End Sub

    Private Sub PaintMyPOS(ByVal g As Graphics)
        g.DrawString(String.Format("({0})", CurrentMapEntry.ConvertPosToRelative(MyData.X, MyData.Y)),
                     New Font(Font, FontStyle.Bold), Brushes.Red, 10, 10)
    End Sub

    Private Sub PaintLinkMob(ByVal g As Graphics, ByVal mob As Contracts.Shared.MobData)
        'Paint the pc blip
        'Dim brush As SolidBrush = Settings.LinkBrush
        'If mob.IsPC Then
        '    brush = Settings.PCBrush
        'End If
        'g.FillEllipse(brush, _
        '              CSng(mob.Pos.MapX - (Settings.BlipSize / 2)), _
        '              CSng(mob.Pos.MapY - (Settings.BlipSize / 2)), _
        '              Settings.BlipSize, Settings.BlipSize)
        '_displayText = GetLinkDisplayText(mob)
        'If _displayText <> String.Empty Then
        '    g.DrawString(_displayText, _
        '                 Font, _
        '                 brush, _
        '                 mob.Pos.MapX + Settings.BlipSize, _
        '                 mob.Pos.MapY - (Settings.BlipSize / 2) - 1)
        'End If
    End Sub
#End Region

#Region " PRIVATE METHODS "
    ''' <summary>
    ''' Checks for the appropriate map image based on your 
    ''' current coordinates.
    ''' </summary>
    ''' <remarks>This function relies on the map.ini</remarks>
    Private Function CheckMap() As Boolean
        Dim isFound As Boolean = False
        Dim ret As Boolean = True
        Dim thisMapFile As String
        Try
            If Not MyData Is Nothing Then
                If MapController.IniFound Then
                    For Each map As MapData In MapController.MapList
                        If map.Map = Settings.CurrentMap Then
                            For Each entry As Box In map.Boxes
                                With MyData
                                    If (.X >= entry.X1 And .X <= entry.X2) AndAlso _
                                       (.Y >= entry.Y1 And .Y <= entry.Y2) AndAlso _
                                       (.Z >= entry.Z1 And .Z <= entry.Z2) Then

                                        thisMapFile = String.Format("{0}\{1}_{2}.gif", MapPath, map.Map.ToString("x2"), map.Level)
                                        If thisMapFile <> _lastMapFile Then
                                            MapImage = New Bitmap(String.Format("{0}\{1}_{2}.gif", MapPath, map.Map.ToString("x2"), map.Level))
                                            _lastMapFile = thisMapFile
                                        End If
                                        CurrentMapEntry = map
                                        Settings.MapLevel = map.Level
                                        isFound = True
                                        Exit For
                                    End If
                                End With
                            Next
                            If isFound Then Exit For
                        End If
                    Next
                Else
                    ret = False
                    MessageBox.Show("No Map data found. Please ensure that you have the latest maps and map.ini")
                End If
            End If
        Catch ex As Exception
            Debug.Print("CheckMap: " & ex.Message)
        Finally
            If Not isFound Then
                If My.Settings.MapsLocation <> String.Empty Then
                    MapImage = New Bitmap(String.Format("{0}\0a_0.gif", My.Settings.MapsLocation))
                End If
            End If
        End Try
        Return ret
    End Function

    Private Sub NotifyNewLinkMobs(ByVal Mobs As Contracts.Shared.MobData())
        RaiseEvent NewMobList(Mobs)
    End Sub
#End Region
End Class
