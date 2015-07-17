Imports FFXIMemory
Imports RadarControls

Public Class AlphaOverlayRadarForm
    Inherits LayeredForm

#Region " MEMBER VARIABLES "
    Private _lastPos As ApRadar3.AppBarModule.RECT
    Private _isLoadingSettings As Boolean
    Private Property Mobs As MobList

#End Region

#Region " PROPERTIES "
    Private _proEnabled As Boolean
    Public Property ProEnabled() As Boolean
        Get
            Return _proEnabled
        End Get
        Set(ByVal value As Boolean)
            _proEnabled = value
            ProFeatures(value)
        End Set
    End Property

    Private WithEvents _filterPanel As OverlayFilterForm
    Private ReadOnly Property FilterPanel() As OverlayFilterForm
        Get
            If _filterPanel Is Nothing OrElse _filterPanel.IsDisposed Then
                _filterPanel = New OverlayFilterForm
            End If
            Return _filterPanel
        End Get
    End Property

    Private WithEvents _selectColorForm As SelectColorForm
    Private ReadOnly Property SelectColorDialog As SelectColorForm
        Get
            If _selectColorForm Is Nothing OrElse _selectColorForm.IsDisposed Then
                _selectColorForm = New SelectColorForm("Overlay Radar")
            End If
            _selectColorForm.BaseSettings = OverlayRadar.Settings
            Return _selectColorForm
        End Get
    End Property

    Private WithEvents _watcher As Watcher
    Private ReadOnly Property MobWatcher As Watcher
        Get
            If _watcher Is Nothing Then
                _watcher = New Watcher(MemoryScanner.WatcherTypes.MobList Or MemoryScanner.WatcherTypes.ZoneChange)
            End If
            Return _watcher
        End Get
    End Property
#End Region

#Region " CONSTRUCTOR "
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        If Not MemoryScanner.Scanner.FFXI Is Nothing Then
            _lastPos = New ApRadar3.AppBarModule.RECT
            If IsWindowVisible(MemoryScanner.Scanner.FFXI.POL.MainWindowHandle) Then
                Dim r As New ApRadar3.AppBarModule.RECT
                If GetWindowRect(MemoryScanner.Scanner.FFXI.POL.MainWindowHandle, r) Then
                    Left = r.Left
                    Top = r.Top
                    Width = r.Right - r.Left
                    Height = r.Bottom - r.Top
                    _lastPos = r
                End If
            End If
        Else
            Top = 100
            Left = 100
            Width = 512
            Height = 512
        End If

    End Sub
#End Region

#Region " FORM ACTIONS "

    Private Sub OverlayRadarForm_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        If IsProEnabled Then
            OverlayRadar.SaveCampedMobs()
        End If
        OverlayRadar.ResignWatcher()
    End Sub


    Private Sub OverlayRadarForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        cmRadarMenu.Renderer = New Office2007Renderer

        ProFeatures(GlobalSettings.IsProEnabled)
        OverlayRadar.LoadSettings()
        BindSettings()

        MemoryScanner.Scanner.AttachWatcher(MobWatcher)

        OverlayRadar.InitializeRadar(Me)

        TopMost = True
        VisibleTimer.Start()
    End Sub

    Private Sub AlphaOverlayRadarForm_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
        If Mobs IsNot Nothing Then
            OverlayRadar.PaintRadar(e.Graphics, Mobs)
        End If
    End Sub
#End Region

#Region " OVERRIDES "
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
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
    Private Sub VisibleTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles VisibleTimer.Tick
        If MemoryScanner.Scanner.IsGameLoaded Then
            If IsWindowVisible(MemoryScanner.Scanner.FFXI.POL.MainWindowHandle) Then
                Show()
                Dim r As New ApRadar3.AppBarModule.RECT
                If GetWindowRect(MemoryScanner.Scanner.FFXI.POL.MainWindowHandle, r) Then
                    If r.Top <> _lastPos.Top OrElse r.Left <> _lastPos.Left OrElse _
                    r.Right <> _lastPos.Right OrElse r.Bottom <> _lastPos.Bottom Then
                        Left = r.Left
                        Top = r.Top
                        Width = r.Right - r.Left
                        Height = r.Bottom - r.Top
                        _lastPos = r
                    End If
                End If
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
            Else
                Hide()
            End If
        End If
    End Sub

#End Region

#Region " MENU ITEMS "

    Private Sub xHideFloors_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cHideFloors.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.HideOtherFloors = cHideFloors.Checked
        End If
    End Sub

    Private Sub cShowTargetInfo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowTargetInfo.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowTargetInfo = cShowTargetInfo.Checked
        End If
    End Sub

    Private Sub cHideInCombat_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cHideInCombat.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.HideInfoInCombat = cHideInCombat.Checked
        End If
    End Sub

    Private Sub cShowNPC_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowNPC.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowNPC = cShowNPC.Checked
        End If
    End Sub

    Private Sub cHideObjectsAndDoors_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cHideObjectsAndDoors.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.HideObjectsOrDoors = cHideObjectsAndDoors.Checked
        End If
    End Sub

    Private Sub cShowMobs_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowMobs.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowMobs = cShowMobs.Checked
        End If
    End Sub

    Private Sub cAlwaysShowTarget_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cAlwaysShowTarget.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.AlwaysShowTarget = cAlwaysShowTarget.Checked
        End If
    End Sub

    Private Sub cShowMobNames_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowNPCNames.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowNPCNames = cShowNPCNames.Checked
        End If
    End Sub

    Private Sub cShowMobDirection_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowMobDirection.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowSight = cShowMobDirection.Checked
        End If
    End Sub

    Private Sub cShowPC_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowPC.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowPC = cShowPC.Checked
        End If
    End Sub

    Private Sub cShowPCNames_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowPCNames.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowPCNames = cShowPCNames.Checked
        End If
    End Sub

    Private Sub cPartyMembersOnly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cPartyMembersOnly.CheckedChanged
        OverlayRadar.Settings.ShowPartyMembers = cPartyMembersOnly.Checked
    End Sub

    Private Sub cSetFont_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cSetFont.Click
        Using fd As New FontDialog() With {.Font = OverlayRadar.Font}
            If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
                OverlayRadar.Font = fd.Font
            End If
        End Using
    End Sub

    Private Sub ApExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ApExit.Click
        Dispose()
    End Sub

    Private Sub cShowCompass_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowCompass.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowCompass = cShowCompass.Checked
        End If
    End Sub

    Private Sub cShowAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowAll.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowAll = cShowAll.Checked
        End If
    End Sub

    Private Sub cShowID_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowID.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowId = cShowID.Checked
        End If
    End Sub

    Private Sub ShowTrackerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShowTrackerToolStripMenuItem.Click
        OverlayRadar.ShowMobTracker()
    End Sub

    Private Sub cShowHP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowHP.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowHP = cShowHP.Checked
        End If
    End Sub
    Private Sub cSaveSettings_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cSaveSettings.Click
        OverlayRadar.SaveSettings()
    End Sub

    Private Sub cSaveSettingsAs_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cSaveSettingsAs.Click
        Using sfd As New SaveFileDialog() With {.Filter = "Xml Settings File|*.xml"}
            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                OverlayRadar.SaveSettings(sfd.FileName)
            End If
        End Using
    End Sub

    Private Sub cLoadSettings_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cLoadSettings.Click
        Using ofd As New OpenFileDialog() With {.Filter = "Xml Settings File|*.xml"}
            If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
                OverlayRadar.LoadSettings(ofd.FileName)
            End If
        End Using
    End Sub

    Private Sub tsFilters_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsFilters.Click
        FilterPanel.NPCFilterType = OverlayRadar.Settings.NPCFilterType
        FilterPanel.NPCFilter = OverlayRadar.Settings.NPCFilter
        FilterPanel.PCFilterType = OverlayRadar.Settings.PCFilterType
        FilterPanel.PCFilter = OverlayRadar.Settings.PCFilter
        FilterPanel.Show()
    End Sub

    Private Sub cSetColors_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cSetColors.Click
        SelectColorDialog.Show()
    End Sub

    Private Sub cShowSettingsDialog_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cShowSettingsDialog.Click
        OverlayRadar.ShowSettings()
    End Sub

    Private Sub cAggro_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cAggro.Click
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowAggro = cAggro.Checked
        End If
    End Sub

    Private Sub cJobAbility_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cJobAbility.Click
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowJobAbility = cJobAbility.Checked
        End If
    End Sub

    Private Sub cSpell_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cSpell.Click
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowSpell = cSpell.Checked
        End If
    End Sub

    Private Sub cVisible_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cVisible.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowVisibleRange = cVisible.Checked
        End If
    End Sub

    Private Sub cAddRange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cAddRange.Click
        Dim rf As New RangesForm(OverlayRadar.Settings.CustomRanges)
        If rf.ShowDialog = Windows.Forms.DialogResult.OK Then
            OverlayRadar.Settings.CustomRanges = rf.Ranges.ToArray
        End If
    End Sub

    'Private Sub OverlayRadar_SettingsChanged()
    '    _isLoadingSettings = True
    '    BindSettings()
    '    _isLoadingSettings = False
    'End Sub

    Private Sub xClickThrough_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles xClickThrough.CheckedChanged
        ClickThrough = xClickThrough.Checked
    End Sub

    Private Sub cShowDistance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cShowDistance.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowDistance = cShowDistance.Checked
        End If
    End Sub

    Private Sub xShowCamped_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles xShowCamped.CheckedChanged
        OverlayRadar.Settings.ShowCampedMobs = xShowCamped.Checked
    End Sub

    Private Sub tsShowHeaderInfo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tsShowHeaderInfo.CheckedChanged
        OverlayRadar.Settings.ShowHeaderText = tsShowHeaderInfo.Checked
    End Sub
#End Region

#Region " PRIVATE MEMBERS "
    Private Sub BindSettings()
        _isLoadingSettings = True
        Dim rs As RadarSettings = OverlayRadar.Settings
        cHideFloors.Checked = rs.HideOtherFloors
        cShowTargetInfo.Checked = rs.ShowTargetInfo
        cHideInCombat.Checked = rs.HideInfoInCombat
        cShowNPC.Checked = rs.ShowNPC
        cHideObjectsAndDoors.Checked = rs.HideObjectsOrDoors
        cShowMobs.Checked = rs.ShowMobs
        cShowNPCNames.Checked = rs.ShowNPCNames
        cShowMobDirection.Checked = rs.ShowSight
        cShowPC.Checked = rs.ShowPC
        cShowPCNames.Checked = rs.ShowPCNames
        cShowDistance.Checked = rs.ShowDistance
        cShowHP.Checked = rs.ShowHP
        cShowCompass.Checked = rs.ShowCompass
        cAggro.Checked = rs.ShowAggro
        cSpell.Checked = rs.ShowSpell
        cJobAbility.Checked = rs.ShowJobAbility
        tsShowHeaderInfo.Checked = rs.ShowHeaderText
        cAlwaysShowTarget.Checked = rs.AlwaysShowTarget
        If GlobalSettings.IsProEnabled Then
            cShowID.Checked = rs.ShowId
            cShowAll.Checked = rs.ShowAll
            xShowCamped.Checked = rs.ShowCampedMobs
        End If
        _isLoadingSettings = False
    End Sub

    Private Sub ProFeatures(ByVal Enabled As Boolean)
        xShowCamped.Enabled = Enabled
        If Enabled = False Then
            xShowCamped.Checked = False
        End If
        cShowID.Enabled = Enabled
        cShowAll.Enabled = Enabled
        OverlayRadar.ProEnabled = Enabled

        If Enabled Then
            'OverlayRadar.LoadCampedMobs()
        End If
        OverlayRadar.ProEnabled = Enabled
    End Sub
#End Region

#Region " EVENT HANDLERS "
    Private Sub SelectColorForm_DataSaved(ByVal sender As SelectColorForm) Handles _selectColorForm.DataSaved
        OverlayRadar.Settings.NPCColor = sender.NPCColor
        OverlayRadar.Settings.MobColor = sender.MobColor
        OverlayRadar.Settings.NMColor = sender.NMColor
        OverlayRadar.Settings.CampedColor = sender.CampedColor
        OverlayRadar.Settings.LinkColor = sender.LinkColor
        OverlayRadar.Settings.PCColor = sender.PCColor
        OverlayRadar.Settings.PartyColor = sender.PartyColor
        OverlayRadar.Settings.AllianceColor = sender.AllianceColor
        OverlayRadar.Settings.TargetHighlightColor = sender.TargetHighlight
    End Sub
#End Region

#Region " FILTER PANEL EVENTS "
    Private Sub PCFilterTypeChanged(ByVal filter As FilterType) Handles _filterPanel.PCFilterTypeChanged
        OverlayRadar.Settings.PCFilterType = filter
    End Sub

    Private Sub PCFilterChanged(ByVal filter As String) Handles _filterPanel.PCFilterChanged
        OverlayRadar.Settings.PCFilter = filter
    End Sub

    Private Sub NPCFilterTypeChanged(ByVal filter As FilterType) Handles _filterPanel.NPCFilterTypeChanged
        OverlayRadar.Settings.NPCFilterType = filter
    End Sub

    Private Sub NPCFilterChanged(ByVal Filter As String) Handles _filterPanel.NPCFilterChanged
        OverlayRadar.Settings.NPCFilter = Filter
    End Sub
#End Region

#Region " WATCHER EVENTS "
    Private Sub _watcher_NewMobList(ByVal InMobs As MobList) Handles _watcher.NewMobList
        'If Mobs Is Nothing Then
        '    Mobs = New MobList(InMobs.ToClonedArray)
        'Else
        '    Mobs.Clear()
        '    Mobs.AddRange(InMobs.ToClonedArray())
        'End If
        Mobs = InMobs
        'OverlayRadar.PaintRadar(CreateGraphics, InMobs)
        MyBase.Invalidate()
    End Sub

    Private Sub _Watcher_ZoneChanged(ByVal LastZone As Short, ByVal NewZone As Short) Handles _watcher.ZoneChanged
        OverlayRadar.Settings.CurrentMap = MemoryScanner.Scanner.CurrentMap
    End Sub
#End Region

End Class