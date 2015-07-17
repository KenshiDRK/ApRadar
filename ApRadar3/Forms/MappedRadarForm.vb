Imports FFXIMemory
Imports Contracts.Shared

Public Class MappedRadarForm
#Region " MEMBER VARIABLES "
    Private _dock As DockingClass
    Private _shiftDown As Boolean
    Private _altDown As Boolean
    Private WithEvents _ifd As IniFixDialog
    Private _filterPanelShown As Boolean
    Private _isLoadingSettings As Boolean = True
    Private _bottomRight As Rectangle
    Private _isResizing As Boolean
    Private _isMoving As Boolean
    Private _startLoc As New Point(0, 0)
    Private _initialStyle As Integer
    Private _mapRoot As String

#End Region

#Region " PROPERTIES "
    'Private ReadOnly Property IniFixDialog() As IniFixDialog
    '    Get
    '        If _ifd Is Nothing OrElse _ifd.IsDisposed Then
    '            _ifd = New IniFixDialog
    '        End If
    '        Return _ifd
    '    End Get
    'End Property

    Private WithEvents _filterPanel As FilterForm
    Private ReadOnly Property FilterPanel() As FilterForm
        Get
            If _filterPanel Is Nothing OrElse _filterPanel.IsDisposed Then
                _filterPanel = New FilterForm()
            End If
            Return _filterPanel
        End Get
    End Property

    Private WithEvents _selectColorForm As SelectColorForm
    Private ReadOnly Property SelectColorDialog As SelectColorForm
        Get
            If _selectColorForm Is Nothing OrElse _selectColorForm.IsDisposed Then
                _selectColorForm = New SelectColorForm("Mapped Radar")
            End If
            _selectColorForm.BaseSettings = Me.MapRadar.Settings
            Return _selectColorForm
        End Get
    End Property

    Private _proEnabled As Boolean
    Public Property ProEnabled() As Boolean
        Get
            Return _proEnabled
        End Get
        Set(ByVal value As Boolean)
            _proEnabled = value
            cShowAll.Enabled = value
            cShowID.Enabled = value
            xShowCampedMobs.Enabled = value
            'xLinkedRadars.Enabled = value
            If value = False Then
                xShowCampedMobs.Checked = False
            End If
            MapRadar.ProEnabled = value
        End Set
    End Property

    Private WithEvents _nmListEditor As NMListEditor
    Private ReadOnly Property NMEditor() As NMListEditor
        Get
            If _nmListEditor Is Nothing OrElse _nmListEditor.IsDisposed Then
                _nmListEditor = New NMListEditor
            End If
            Return _nmListEditor
        End Get
    End Property

    Private WithEvents _lrf As LinkedRadarsForm
    Private ReadOnly Property LRF() As LinkedRadarsForm
        Get
            If _lrf Is Nothing OrElse _lrf.IsDisposed Then
                _lrf = New LinkedRadarsForm
            End If
            Return _lrf
        End Get
    End Property
#End Region

#Region " CONSTRUCTORS "
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region " FORM ACTIONS "
    Private Sub MappedRadarForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If _filterPanelShown Then
            FilterPanel.Close()
        End If
        If My.Settings.AutoSaveRadarSettings Then
            MapRadar.SaveSettings()
        End If
        MapRadar.ResignWatcher()
    End Sub

    Private Sub MappedRadarForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Up AndAlso e.Shift Then

        ElseIf e.KeyCode = Keys.Down AndAlso e.Shift Then

        End If
    End Sub

    Private Sub MappedRadarForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmMapRadar.Renderer = New Office2007Renderer
        cShowID.Enabled = GlobalSettings.IsProEnabled
        cShowAll.Enabled = GlobalSettings.IsProEnabled
        xShowCampedMobs.Enabled = GlobalSettings.IsProEnabled
        xLinkedRadars.Enabled = GlobalSettings.IsProEnabled
        
        _dock = New DockingClass(Me)
        _dock.UseDocking = True

        MapRadar.ProEnabled = GlobalSettings.IsProEnabled
        MapRadar.LoadSettings()
        BindSettings()
        Opacity = MapRadar.Settings.Transparency
        If MapRadar.Settings.ShowFilterPanel Then
            cShowFilterPanel.PerformClick()
        End If
        MapRadar.InitializeRadar()



        ActiveTimer.Start()

        _mapRoot = MapRadar.MapPath
        LoadMapPacks()
    End Sub

    Private Sub MappedRadarForm_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        MapRadar.Settings.Location = Location
    End Sub

    Private Sub MappedRadarForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If _filterPanelShown Then
            FilterPanel.Top = Bottom
            FilterPanel.Width = Width
        End If
        MapRadar.Settings.Size = Size
    End Sub
#End Region

#Region " OVERRIDES "
    'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    '    If _filterPanelShown Then
    '        SendMessage(FilterPanel.Handle, m.Msg, m.WParam, m.LParam)
    '    End If
    '    'End If
    '    MyBase.WndProc(m)
    'End Sub

    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            If My.Settings.HideAltTab Then
                cp.ExStyle = cp.ExStyle Or &H80
            End If
            Return cp
        End Get
    End Property
#End Region

#Region " CONTROLS "

    Private Sub cRingOnly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cRingOnly.CheckedChanged
        If Not _isLoadingSettings Then
            If cRingOnly.Checked Then
                MapRadar.Settings.RangeDisplay = RadarControls.RadarSettings.RangeType.Ring
            Else
                MapRadar.Settings.RangeDisplay = RadarControls.RadarSettings.RangeType.Solid
            End If
        End If
    End Sub

    Private Sub cVisible_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cVisible.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowVisibleRange = cVisible.Checked
        End If
    End Sub

    Private Sub xLinkedRadars_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xLinkedRadars.Click
        LRF.Show()
    End Sub

    Private Sub cMapPackSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cMapPackSelect.Click
        Dim tsi As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
        If Not tsi Is Nothing Then
            MapRadar.MapPath = tsi.Tag
            tsi.Checked = True
            For i As Integer = 0 To cSelectPack.DropDownItems.Count - 1 Step 1
                If Not CType(cSelectPack.DropDownItems(i), ToolStripMenuItem) Is tsi Then
                    CType(cSelectPack.DropDownItems(i), ToolStripMenuItem).Checked = False
                End If
            Next
        End If

    End Sub

    Private Sub xShowCampedMobs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles xShowCampedMobs.CheckedChanged
        MapRadar.Settings.ShowCampedMobs = xShowCampedMobs.Checked
    End Sub

    Private Sub MapRadar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MapRadar.KeyDown
        If e.KeyCode = Keys.ShiftKey Then
            _shiftDown = True
        ElseIf e.KeyCode = Keys.Menu Then
            _altDown = True
        End If
        If e.KeyCode = Keys.PageUp Then
            If e.Modifiers = Keys.Shift Then
                MapRadar.Settings.Zoom += 1
            Else
                MapRadar.Settings.Zoom += 0.25
            End If
        ElseIf e.KeyCode = Keys.PageDown Then
            Dim zoom As Single = MapRadar.Settings.Zoom
            If e.Modifiers = Keys.Shift Then
                zoom -= 1
            Else
                zoom -= 0.25
            End If

            If zoom < 1 Then zoom = 1
            MapRadar.Settings.Zoom = zoom
        ElseIf e.KeyCode = Keys.Home Then
            MapRadar.Settings.Zoom = 1
        End If
    End Sub

    Private Sub MapRadar_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MapRadar.KeyUp
        If e.KeyCode = Keys.ShiftKey Then
            _shiftDown = False
        ElseIf e.KeyCode = Keys.Menu Then
            _altDown = False
        End If
    End Sub

    Private Sub MapRadar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MapRadar.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If _altDown Then
                _startLoc = New Point(e.Location)
            Else
                If _bottomRight.Contains(e.Location) Then
                    _isResizing = True
                    SuspendLayout()
                    _startLoc = New Point(e.Location)
                Else
                    If Not cDragging.Checked Then
                        _dock.StartDockDrag(e.X, e.Y)
                        _isMoving = True
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub MapRadar_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MapRadar.MouseMove
        If _isResizing Then
            Dim diffX, diffY As Integer
            diffX = e.X - _startLoc.X
            diffY = e.Y - _startLoc.Y
            Width += diffX
            Height += diffY
            'Width += Math.Max(diffX, diffY)
            'Height += Math.Max(diffX, diffY)
            _startLoc = e.Location
        Else
            If e.Button = Windows.Forms.MouseButtons.Left AndAlso _isMoving Then
                If Not cDragging.Checked Then
                    _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
                End If
                'TODO: This section is not ready
                'ElseIf e.Button = Windows.Forms.MouseButtons.Left AndAlso _altDown Then
                '    MapRadar.ShiftX(_startLoc.X - e.X)
                '    MapRadar.ShiftY(_startLoc.Y - e.Y)
                '    _startLoc = e.Location
            Else
                _bottomRight = New Rectangle(Width - 15, Height - 15, 15, 15)
                If _bottomRight.Contains(e.Location) Then
                    Cursor = Cursors.SizeNWSE
                Else
                    Cursor = Cursors.Default
                End If
            End If
        End If
    End Sub

    Private Sub MapRadar_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MapRadar.MouseUp
        ResumeLayout()
        _isResizing = False
        _isMoving = False
    End Sub

    Private Sub MapRadar_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MapRadar.MouseWheel
        If _shiftDown Then
            Dim zoomLevel As Single = MapRadar.Settings.Zoom
            zoomLevel += e.Delta / 360
            If zoomLevel > 30 Then
                zoomLevel = 30
            ElseIf zoomLevel < 1 Then
                zoomLevel = 1
            End If
            MapRadar.Settings.Zoom = zoomLevel
        End If
    End Sub

    Private Sub ApExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApExit.Click
        Close()
    End Sub

    'Private Sub xAdjustMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Not IniFixDialog.Visible Then
    '        IniFixDialog.Left = Left + 20 '(Width / 2) - (IniFixDialog.Width / 2)
    '        IniFixDialog.Top = Top + 20 'Bottom - 20 - IniFixDialog.Height
    '        IniFixDialog.xModifier.Value = MapRadar.CurrentMapEntry.IniData.XModifier
    '        IniFixDialog.yModifier.Value = MapRadar.CurrentMapEntry.IniData.YModifier
    '        IniFixDialog.Show(Me)
    '    End If
    'End Sub

    Private Sub cShowNPC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowNPC.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowNPC = cShowNPC.Checked
        End If
    End Sub

    Private Sub cShowMobs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowMobs.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowMobs = cShowMobs.Checked
        End If
    End Sub

    Private Sub cShowNPCNames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowNPCNames.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowNPCNames = cShowNPCNames.Checked
        End If
    End Sub

    Private Sub cShowPC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowPC.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowPC = cShowPC.Checked
        End If
    End Sub

    Private Sub cShowPCNames_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowPCNames.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowPCNames = cShowPCNames.Checked
        End If
    End Sub

    Private Sub cSetNPCColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Using cd As New ColorDialog() With {.Color = MapRadar.Settings.NPCColor}
            If cd.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.Settings.NPCColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub xLinkMobColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Using cd As New ColorDialog() With {.Color = MapRadar.Settings.LinkColor}
            If cd.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.Settings.LinkColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub cSetPCColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Using cd As New ColorDialog() With {.Color = MapRadar.Settings.PCColor}
            If cd.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.Settings.PCColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub cDistance_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cDistance.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowDistance = cDistance.Checked
        End If
    End Sub

    Private Sub cShowHP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowHP.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowHP = cShowHP.Checked
        End If
    End Sub

    Private Sub cSetFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cSetFont.Click
        Using fd As New FontDialog() With {.Font = MapRadar.Font}
            Try
                If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
                    MapRadar.Font = fd.Font
                End If
            Catch ex As Exception
                MapRadar.Font = Me.Font
                'Do nothing
            End Try
        End Using
    End Sub

    Private Sub xSaveSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xSaveSettings.Click
        MapRadar.SaveSettings()
    End Sub

    Private Sub xSaveSettingsAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xSaveSettingsAs.Click
        Using sfd As New SaveFileDialog() With {.Filter = "Xml Settings File|*.xml"}
            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.SaveSettings(sfd.FileName)
            End If
        End Using
    End Sub

    Private Sub xLoadSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xLoadSettings.Click
        Using ofd As New OpenFileDialog() With {.Filter = "Xml Settings File|*.xml"}
            If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.LoadSettings(ofd.FileName)
            End If
        End Using
    End Sub

    Private Sub cShowFilterPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowFilterPanel.Click
        If _filterPanelShown Then
            FilterPanel.Close()
            _dock.Childen.Clear()
            _filterPanelShown = False
            MapRadar.Settings.ShowFilterPanel = False
            Focus()
        Else
            MapRadar.Settings.ShowFilterPanel = True
            FilterPanel.Width = Width
            FilterPanel.Location = New Point(Left, Bottom)
            _filterPanelShown = True
            FilterPanel.Opacity = Opacity
            FilterPanel.NPCFilterType = MapRadar.Settings.NPCFilterType
            FilterPanel.NPCFilter = MapRadar.Settings.NPCFilter
            FilterPanel.PCFilterType = MapRadar.Settings.PCFilterType
            FilterPanel.PCFilter = MapRadar.Settings.PCFilter
            FilterPanel.TopMost = TopMost
            FilterPanel.Show()
            _dock.Childen.Add(New DockingClass.ChildForms(_filterPanel, DockMode.Bottom))
            Focus()
        End If
    End Sub

    Private Sub cShowSettingSdesigner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowSettingsDesigner.Click
        MapRadar.ShowSettings()
    End Sub

    Private Sub cRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim response As String = InputBox("Please enter the refresh rate", "Change Refresh Rate", MapRadar.Settings.RefreshRate)
        If Integer.TryParse(response, New Integer) Then
            MapRadar.Settings.RefreshRate = CInt(response)
        End If
    End Sub

    Private Sub cShowPOS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowPOS.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowPOS = cShowPOS.Checked
        End If
    End Sub

    Private Sub transparency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles p100.Click, p80.Click, p60.Click, p40.Click, p20.Click
        Opacity = CInt(CType(sender, ToolStripItem).Tag) / 100
        If _filterPanelShown Then
            _filterPanel.Opacity = Opacity
        End If
        MapRadar.Settings.Transparency = Opacity
    End Sub

    Private Sub cShowAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowAll.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowAll = cShowAll.Checked
        End If
    End Sub

    Private Sub cShowID_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowID.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowId = cShowID.Checked
        End If
    End Sub

    Private Sub cOnTop_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cOnTop.CheckedChanged
        TopMost = cOnTop.Checked
        If _filterPanelShown Then
            FilterPanel.TopMost = TopMost
        End If
        If Not _isLoadingSettings Then
            MapRadar.Settings.StayOnTop = cOnTop.Checked
        End If
    End Sub

    Private Sub ShowHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHide.Click
        If ShowHide.Text = "Hide Mapped Radar" Then
            Hide()
            If _filterPanelShown Then
                FilterPanel.Hide()
            End If
            ShowHide.Text = "Show Mapped Radar"
        Else
            Show()
            If _filterPanelShown Then
                FilterPanel.Show()
            End If
            ShowHide.Text = "Hide Mapped Radar"
        End If
    End Sub

    Private Sub cResetPosition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cResetPosition.Click
        Size = New Size(512, 512)
        Location = New Point(100, 100)
    End Sub

    Private Sub cSpellCasting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cSpellCasting.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowSpell = cSpellCasting.Checked
        End If
    End Sub

    Private Sub cAggro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cAggro.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowAggro = cAggro.Checked
        End If
    End Sub

    Private Sub cJobAbility_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cJobAbility.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowJobAbility = cJobAbility.Checked
        End If
    End Sub


    Private Sub cMapPackSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cMapPackSelect.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowVisibleRange = cVisible.Checked
        End If
    End Sub

    Private Sub cCustomRanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cCustomRanges.Click
        Using rf As New RangesForm(MapRadar.Settings.CustomRanges)
            If rf.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.Settings.CustomRanges = rf.Ranges.ToArray
            End If
        End Using
    End Sub

    Private Sub cShowParty_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowParty.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.ShowPartyMembers = cShowParty.Checked
        End If
    End Sub

    Private Sub cClickThrough_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cClickThrough.CheckedChanged
        SetClickThrough(cClickThrough.Checked)
        If Not _isLoadingSettings Then
            MapRadar.Settings.ClickThrough = cClickThrough.Checked
        End If
    End Sub

    Private Sub cDocking_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cDocking.CheckedChanged
        _dock.UseDocking = Not cDocking.Checked
        If Not _isLoadingSettings Then
            MapRadar.Settings.DisableDocking = cDocking.Checked
        End If
    End Sub

    Private Sub cDragging_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cDragging.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.DisableDragging = cDragging.Checked
        End If
    End Sub

    Private Sub cNMColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Using cd As New ColorDialog() With {.Color = MapRadar.Settings.NMColor}
            If cd.ShowDialog = Windows.Forms.DialogResult.OK Then
                MapRadar.Settings.NMColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub cEditNMList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cEditNMList.Click
        NMEditor.NMList = MapRadar.NMList
        NMEditor.Show()
    End Sub

    Private Sub ActiveTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActiveTimer.Tick
        Try
            If MapRadar.Settings.StayOnTop Then
                If MemoryScanner.Scanner.FFXI.IsGameLoaded Then
                    If GetForegroundWindow() = MemoryScanner.Scanner.FFXI.POL.MainWindowHandle Then
                        If Not TopMost Then
                            TopMost = True
                            BringToFront()
                        End If
                    Else
                        If TopMost Then
                            TopMost = False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region " EVENT HANDLERS "

    Private Sub MapRadar_NewMobList(ByVal Mobs As Contracts.Shared.MobData()) Handles MapRadar.NewMobList
        If Not LRF Is Nothing AndAlso Not LRF.IsDisposed Then
            LRF.SendMessage(New LinkEventArgs() With {.Type = LinkEventType.MobList, .Zone = MapRadar.Settings.CurrentMap, .Mobs = Mobs})
        End If
    End Sub

    Private Sub _ifd_XModifierChanged(ByVal value As Single) Handles _ifd.XModifierChanged
        MapRadar.CurrentMapEntry.IniData.XModifier = value
    End Sub

    Private Sub _ifd_YModifierChanged(ByVal value As Single) Handles _ifd.YModifierChanged
        MapRadar.CurrentMapEntry.IniData.YModifier = value
    End Sub

    Private Sub _ifd_SaveChanges() Handles _ifd.SaveChanges
        MapRadar.SaveIniEntry()
    End Sub

    Private Sub NMListChanged() Handles _nmListEditor.ListChanged
        MapRadar.NMList = NMEditor.NMList
    End Sub

    Private Sub xHideFloors_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles xHideFloors.CheckedChanged
        If Not _isLoadingSettings Then
            MapRadar.Settings.HideOtherFloors = xHideFloors.Checked
        End If
    End Sub

    Private Sub SelectColorForm_DataSaved(ByVal sender As SelectColorForm) Handles _selectColorForm.DataSaved
        Me.MapRadar.Settings.NPCColor = sender.NPCColor
        Me.MapRadar.Settings.MobColor = sender.MobColor
        Me.MapRadar.Settings.NMColor = sender.NMColor
        Me.MapRadar.Settings.CampedColor = sender.CampedColor
        Me.MapRadar.Settings.LinkColor = sender.LinkColor
        Me.MapRadar.Settings.PCColor = sender.PCColor
        Me.MapRadar.Settings.PartyColor = sender.PartyColor
        Me.MapRadar.Settings.AllianceColor = sender.AllianceColor
    End Sub
#End Region

#Region " PRIVATE METHODS "
    Private Sub BindSettings()
        _isLoadingSettings = True
        Dim rs As RadarControls.RadarSettings = MapRadar.Settings
        If GlobalSettings.IsProEnabled Then
            cShowAll.Checked = rs.ShowAll
            cShowID.Checked = rs.ShowId
            xShowCampedMobs.Checked = rs.ShowCampedMobs
        End If
        cShowHP.Checked = rs.ShowHP
        cShowNPC.Checked = rs.ShowNPC
        cShowMobs.Checked = rs.ShowMobs
        cShowNPCNames.Checked = rs.ShowNPCNames
        cShowPC.Checked = rs.ShowPC
        cShowPCNames.Checked = rs.ShowPCNames
        cShowParty.Checked = rs.ShowPartyMembers
        cShowPOS.Checked = rs.ShowPOS
        xHideFloors.Checked = rs.HideOtherFloors
        Location = MapRadar.Settings.Location
        cAggro.Checked = MapRadar.Settings.ShowAggro
        cJobAbility.Checked = MapRadar.Settings.ShowJobAbility
        cSpellCasting.Checked = MapRadar.Settings.ShowSpell
        cVisible.Checked = MapRadar.Settings.ShowVisibleRange
        Size = MapRadar.Settings.Size
        cOnTop.Checked = MapRadar.Settings.StayOnTop
        cDragging.Checked = MapRadar.Settings.DisableDragging
        cDocking.Checked = MapRadar.Settings.DisableDocking
        cClickThrough.Checked = MapRadar.Settings.ClickThrough
        cRingOnly.Checked = (MapRadar.Settings.RangeDisplay = RadarControls.RadarSettings.RangeType.Ring)
        _isLoadingSettings = False
    End Sub

    Private Sub SetClickThrough(ByVal isClickThrough As Boolean)
        If isClickThrough Then
            _initialStyle = GetWindowLong(Handle, GWL.ExStyle)
            SetWindowLong(Handle, GWL.ExStyle, _initialStyle Or WS_EX.Layered Or WS_EX.Transparent)
        Else
            SetWindowLong(Handle, GWL.ExStyle, _initialStyle)
        End If
    End Sub

    Private Sub LoadMapPacks()
        Dim tsi As ToolStripItem
        If IO.Directory.Exists(_mapRoot) Then
            For Each Dir As String In IO.Directory.GetDirectories(_mapRoot)
                tsi = cSelectPack.DropDownItems.Add(IO.Path.GetFileName(Dir))
                tsi.Tag = Dir
                AddHandler tsi.Click, AddressOf cMapPackSelect_Click
            Next
        End If
    End Sub
#End Region

#Region " FILTER PANEL EVENTS "
    Private Sub PCFilterTypeChanged(ByVal filter As FilterType) Handles _filterPanel.PCFilterTypeChanged
        MapRadar.Settings.PCFilterType = filter
    End Sub

    Private Sub PCFilterChanged(ByVal filter As String) Handles _filterPanel.PCFilterChanged
        MapRadar.Settings.PCFilter = filter
    End Sub

    Private Sub NPCFilterTypeChanged(ByVal filter As FilterType) Handles _filterPanel.NPCFilterTypeChanged
        MapRadar.Settings.NPCFilterType = filter
    End Sub

    Private Sub NPCFilterChanged(ByVal Filter As String) Handles _filterPanel.NPCFilterChanged
        MapRadar.Settings.NPCFilter = Filter
    End Sub
#End Region

#Region " LINKED RADAR EVENTS "
    Private Sub lrf_NewMessage(ByVal e As LinkEventArgs) Handles _lrf.NewMessage
        If e.Type = LinkEventType.MobList Then
            If MapRadar.LinkServerRunning AndAlso e.Zone = MapRadar.Settings.CurrentMap Then
                If MapRadar.LinkMobs.ContainsKey(e.ClientID) Then
                    MapRadar.LinkMobs(e.ClientID) = e.Mobs
                Else
                    MapRadar.LinkMobs.Add(e.ClientID, e.Mobs)
                End If
            End If
        ElseIf e.Type = LinkEventType.ClientDisconnected Then
            MapRadar.LinkMobs.Remove(e.ClientID)
        End If
    End Sub

    Private Sub lrf_ServerStatus(ByVal IsRunning As Boolean) Handles _lrf.ServerStatus
        MapRadar.LinkServerRunning = IsRunning
    End Sub
#End Region

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Me.SelectColorDialog.Show()
    End Sub


End Class