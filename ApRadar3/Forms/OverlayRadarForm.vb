Imports RadarControls
Imports FFXIMemory

Public Class OverlayRadarForm
    Inherits LayeredForm

#Region " MEMBER VARIABLES "
    Private _dock As DockingClass
    Private _lastPos As ApRadar3.AppBarModule.RECT
    Private _isLoadingSettings As Boolean
    Private _initialStyle As Integer
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
            _selectColorForm.BaseSettings = Me.OverlayRadar.Settings
            Return _selectColorForm
        End Get
    End Property
#End Region

#Region " CONSTRUCTORS "
    Public Sub New()
        InitializeComponent()

        If Not MemoryScanner.Scanner.FFXI Is Nothing Then
            _lastPos = New ApRadar3.AppBarModule.RECT
            If IsWindowVisible(MemoryScanner.Scanner.FFXI.POL.MainWindowHandle) Then
                Dim r As New ApRadar3.AppBarModule.RECT
                If GetWindowRect(MemoryScanner.Scanner.FFXI.POL.MainWindowHandle, r) Then
                    Me.Left = r.Left
                    Me.Top = r.Top
                    Me.Width = r.Right - r.Left
                    Me.Height = r.Bottom - r.Top
                    _lastPos = r
                End If
            End If
        Else
            Me.Top = 100
            Me.Left = 100
            Me.Width = 512
            Me.Height = 512
        End If
    End Sub
#End Region

#Region " OVERRIDEN METHODS "
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80
            Return cp
        End Get
    End Property
#End Region

#Region " FORM ACTIONS "

    Private Sub OverlayRadarForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If IsProEnabled Then
            Me.OverlayRadar.SaveCampedMobs()
        End If
        Me.OverlayRadar.ResignWatcher()
    End Sub


    Private Sub OverlayRadarForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.cmRadarMenu.Renderer = New Office2007Renderer
        _dock = New DockingClass(Me)
        _dock.UseDocking = False

        ProFeatures(GlobalSettings.IsProEnabled)
        Me.OverlayRadar.LoadSettings()
        BindSettings()

        Me.OverlayRadar.InitializeRadar()

        Me.TopMost = True
        VisibleTimer.Start()
    End Sub
#End Region

#Region " CONTROLS "
    Private Sub OverlayRadar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles OverlayRadar.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.StartDockDrag(e.X, e.Y)
        End If
    End Sub

    Private Sub OverlayRadar_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles OverlayRadar.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
        End If
    End Sub

    Private Sub VisibleTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VisibleTimer.Tick
        If Not MemoryScanner.Scanner.FFXI Is Nothing Then
            If IsWindowVisible(MemoryScanner.Scanner.FFXI.POL.MainWindowHandle) Then
                Me.Show()
                Dim r As New ApRadar3.AppBarModule.RECT
                If GetWindowRect(MemoryScanner.Scanner.FFXI.POL.MainWindowHandle, r) Then
                    If r.Top <> _lastPos.Top OrElse r.Left <> _lastPos.Left OrElse _
                    r.Right <> _lastPos.Right OrElse r.Bottom <> _lastPos.Bottom Then
                        Me.Left = r.Left
                        Me.Top = r.Top
                        Me.Width = r.Right - r.Left
                        Me.Height = r.Bottom - r.Top
                        _lastPos = r
                    End If
                End If
                If GetForegroundWindow() = MemoryScanner.Scanner.FFXI.POL.MainWindowHandle Then
                    If Not Me.TopMost Then
                        Me.TopMost = True
                        Me.BringToFront()
                    End If
                Else
                    If Me.TopMost Then
                        Me.TopMost = False
                    End If
                End If
            Else
                Me.Hide()
            End If
        End If
    End Sub
#End Region

#Region " MENU ITEMS "
    Private Sub cRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim result As Integer
        Dim sResult As String = InputBox("Please specify the refresh rate", "Set Refresh Rate", OverlayRadar.Settings.RefreshRate)
        If Integer.TryParse(sResult, result) Then
            OverlayRadar.Settings.RefreshRate = result
        End If
    End Sub

    Private Sub xHideFloors_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cHideFloors.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.HideOtherFloors = cHideFloors.Checked
        End If
    End Sub

    Private Sub cShowTargetInfo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowTargetInfo.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowTargetInfo = cShowTargetInfo.Checked
        End If
    End Sub

    Private Sub cHideInCombat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cHideInCombat.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.HideInfoInCombat = cHideInCombat.Checked
        End If
    End Sub

    Private Sub cShowNPC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowNPC.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowNPC = cShowNPC.Checked
        End If
    End Sub

    Private Sub cShowMobs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowMobs.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowMobs = cShowMobs.Checked
        End If
    End Sub

    Private Sub cShowMobNames_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowNPCNames.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowNPCNames = cShowNPCNames.Checked
        End If
    End Sub

    Private Sub cShowMobDirection_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowMobDirection.CheckedChanged
        If Not _isLoadingSettings Then
            Me.OverlayRadar.Settings.ShowSight = cShowMobDirection.Checked
        End If
    End Sub

    Private Sub cShowPC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowPC.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowPC = cShowPC.Checked
        End If
    End Sub

    Private Sub cShowPCNames_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowPCNames.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowPCNames = cShowPCNames.Checked
        End If
    End Sub

    Private Sub cPartyMembersOnly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cPartyMembersOnly.CheckedChanged
        Me.OverlayRadar.Settings.ShowPartyMembers = cPartyMembersOnly.Checked
    End Sub

    Private Sub cSetFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cSetFont.Click
        Using fd As New FontDialog() With {.Font = OverlayRadar.Font}
            If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
                OverlayRadar.Font = fd.Font
            End If
        End Using
    End Sub

    Private Sub ApExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApExit.Click
        Me.Dispose()
    End Sub

    Private Sub cShowCompass_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowCompass.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowCompass = cShowCompass.Checked
        End If
    End Sub

    Private Sub cShowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowAll.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowAll = cShowAll.Checked
        End If
    End Sub

    Private Sub cShowID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowID.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowId = cShowID.Checked
        End If
    End Sub

    Private Sub ShowTrackerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowTrackerToolStripMenuItem.Click
        OverlayRadar.ShowMobTracker()
    End Sub

    Private Sub cShowHP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowHP.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowHP = cShowHP.Checked
        End If
    End Sub
    Private Sub cSaveSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cSaveSettings.Click
        OverlayRadar.SaveSettings()
    End Sub

    Private Sub cSaveSettingsAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cSaveSettingsAs.Click
        Dim sfd As New SaveFileDialog
        sfd.Filter = "Xml Settings File|*.xml"
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            OverlayRadar.SaveSettings(sfd.FileName)
        End If
    End Sub

    Private Sub cLoadSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cLoadSettings.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "Xml Settings File|*.xml"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            OverlayRadar.LoadSettings(ofd.FileName)
        End If
    End Sub

    Private Sub tsFilters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsFilters.Click
        FilterPanel.NPCFilterType = Me.OverlayRadar.Settings.NPCFilterType
        FilterPanel.NPCFilter = Me.OverlayRadar.Settings.NPCFilter
        FilterPanel.PCFilterType = Me.OverlayRadar.Settings.PCFilterType
        FilterPanel.PCFilter = Me.OverlayRadar.Settings.PCFilter
        FilterPanel.Show()
    End Sub

    Private Sub cSetColors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cSetColors.Click
        Me.SelectColorDialog.Show()
    End Sub

    Private Sub cShowSettingsDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cShowSettingsDialog.Click
        OverlayRadar.ShowSettings()
    End Sub

    Private Sub cAggro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cAggro.Click
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowAggro = Me.cAggro.Checked
        End If
    End Sub

    Private Sub cJobAbility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cJobAbility.Click
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowJobAbility = Me.cJobAbility.Checked
        End If
    End Sub

    Private Sub cSpell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cSpell.Click
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowSpell = Me.cSpell.Checked
        End If
    End Sub

    Private Sub cVisible_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cVisible.CheckedChanged
        If Not _isLoadingSettings Then
            Me.OverlayRadar.Settings.ShowVisibleRange = cVisible.Checked
        End If
    End Sub

    Private Sub cAddRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cAddRange.Click
        Dim rf As New RangesForm(OverlayRadar.Settings.CustomRanges)
        If rf.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.OverlayRadar.Settings.CustomRanges = rf.Ranges.ToArray
        End If
    End Sub

    Private Sub OverlayRadar_SettingsChanged()
        _isLoadingSettings = True
        BindSettings()
        _isLoadingSettings = False
    End Sub

    Private Sub xClickThrough_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles xClickThrough.CheckedChanged
        SetClickThrough(Me.xClickThrough.Checked)
    End Sub

    Private Sub cShowDistance_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cShowDistance.CheckedChanged
        If Not _isLoadingSettings Then
            OverlayRadar.Settings.ShowDistance = cShowDistance.Checked
        End If
    End Sub

    Private Sub xShowCamped_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles xShowCamped.CheckedChanged
        OverlayRadar.Settings.ShowCampedMobs = xShowCamped.Checked
    End Sub

    Private Sub tsShowHeaderInfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsShowHeaderInfo.CheckedChanged
        OverlayRadar.Settings.ShowHeaderText = tsShowHeaderInfo.Checked
    End Sub
#End Region

#Region " PRIVATE MEMBERS "
    Private Sub BindSettings()
        _isLoadingSettings = True
        Dim rs As RadarSettings = OverlayRadar.Settings
        Me.cHideFloors.Checked = rs.HideOtherFloors
        Me.cShowTargetInfo.Checked = rs.ShowTargetInfo
        Me.cHideInCombat.Checked = rs.HideInfoInCombat
        Me.cShowNPC.Checked = rs.ShowNPC
        Me.cShowMobs.Checked = rs.ShowMobs
        Me.cShowNPCNames.Checked = rs.ShowNPCNames
        Me.cShowMobDirection.Checked = rs.ShowSight
        Me.cShowPC.Checked = rs.ShowPC
        Me.cShowPCNames.Checked = rs.ShowPCNames
        Me.cShowDistance.Checked = rs.ShowDistance
        Me.cShowHP.Checked = rs.ShowHP
        Me.cShowCompass.Checked = rs.ShowCompass
        Me.cAggro.Checked = rs.ShowAggro
        Me.cSpell.Checked = rs.ShowSpell
        Me.cJobAbility.Checked = rs.ShowJobAbility
        Me.tsShowHeaderInfo.Checked = rs.ShowHeaderText
        If GlobalSettings.IsProEnabled Then
            Me.cShowID.Checked = rs.ShowId
            Me.cShowAll.Checked = rs.ShowAll
            Me.xShowCamped.Checked = rs.ShowCampedMobs
        End If
        _isLoadingSettings = False
    End Sub

    Private Sub ProFeatures(ByVal Enabled As Boolean)
        Me.xShowCamped.Enabled = Enabled
        If Enabled = False Then
            Me.xShowCamped.Checked = False
        End If
        Me.cShowID.Enabled = Enabled
        Me.cShowAll.Enabled = Enabled
        Me.OverlayRadar.ProEnabled = Enabled

        If Enabled Then
            'Me.OverlayRadar.LoadCampedMobs()
        End If
        Me.OverlayRadar.ProEnabled = Enabled
    End Sub

    Private Sub SetClickThrough(ByVal isClickThrough As Boolean)
        If isClickThrough Then
            _initialStyle = GetWindowLong(Me.Handle, GWL.ExStyle)
            SetWindowLong(Me.Handle, GWL.ExStyle, _initialStyle Or WS_EX.Layered Or WS_EX.Transparent)
        Else
            SetWindowLong(Me.Handle, GWL.ExStyle, _initialStyle)
        End If
    End Sub
#End Region

#Region " EVENT HANDLERS "
    Private Sub SelectColorForm_DataSaved(ByVal sender As SelectColorForm) Handles _selectColorForm.DataSaved
        Me.OverlayRadar.Settings.NPCColor = sender.NPCColor
        Me.OverlayRadar.Settings.MobColor = sender.MobColor
        Me.OverlayRadar.Settings.NMColor = sender.NMColor
        Me.OverlayRadar.Settings.CampedColor = sender.CampedColor
        Me.OverlayRadar.Settings.LinkColor = sender.LinkColor
        Me.OverlayRadar.Settings.PCColor = sender.PCColor
        Me.OverlayRadar.Settings.PartyColor = sender.PartyColor
        Me.OverlayRadar.Settings.AllianceColor = sender.AllianceColor
    End Sub
#End Region

#Region " FILTER PANEL EVENTS "
    Private Sub PCFilterTypeChanged(ByVal filter As FilterType) Handles _filterPanel.PCFilterTypeChanged
        Me.OverlayRadar.Settings.PCFilterType = filter
    End Sub

    Private Sub PCFilterChanged(ByVal filter As String) Handles _filterPanel.PCFilterChanged
        Me.OverlayRadar.Settings.PCFilter = filter
    End Sub

    Private Sub NPCFilterTypeChanged(ByVal filter As FilterType) Handles _filterPanel.NPCFilterTypeChanged
        Me.OverlayRadar.Settings.NPCFilterType = filter
    End Sub

    Private Sub NPCFilterChanged(ByVal Filter As String) Handles _filterPanel.NPCFilterChanged
        Me.OverlayRadar.Settings.NPCFilter = Filter
    End Sub
#End Region

End Class