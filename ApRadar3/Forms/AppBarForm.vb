﻿Imports FFXIMemory
Imports DataLibrary
Imports DataLibrary.DataAccess
Imports RadarControls
Imports ApRadar3.AppBarManager
Imports FFXIMemory.MobData

Public Class AppBarForm
#Region " MEMBER VARIABLES "
    Private WithEvents _abm As AppBarManager
    Private _bf As BrowserForm
    Private WithEvents _orf As AlphaOverlayRadarForm
    Private WithEvents _mrf As AlphaMappedRadarForm
    Private WithEvents _mdf As MobDataForm
    Private WithEvents _pcf As PCInfoForm
    Private _dbf As DataBrowserForm

    Private _mob As DataLibrary.ApRadarDataSet.MobsRow
    Private _targetInfoData As String
    Private _isTargetInfoDropped As Boolean
    Private _isPCInfoDropped As Boolean

    Private _screen As Screen
    Private _sof As FormAnimator
    Private WithEvents _timer As Timer
    Private _isAutoHide As Boolean
    Private _isHidden As Boolean
    Private WithEvents _targetTimer As Timer

    'Private _ffxi As FFXI
    Private _pData As MobData
    Private _currentZone As Short
    Private _zoneName As String = "Unknown"


    'Private _targetId As Integer
    Private _lastTargetId As Integer = -1
    Private _tData As MobData

    Private _tsOverlayCMS As ContextMenuStrip
    Private _tsMappedCMS As ContextMenuStrip

    Private _isProcessSelected As Boolean
    Private _currentProcessID As Integer

    Private _isLoading As Boolean
    Private _lostFocusCount As Integer

    Private _isMobCamped As Boolean

    Private _pList As IEnumerable(Of Process)
#End Region

#Region " DELEGATE "
    Private Delegate Sub MobSpawnedCallback(ByVal mob As MobData, ByVal OutofRange As Boolean)
#End Region

#Region " PROPERTIES "
    Public ReadOnly Property BrowserForm() As BrowserForm
        Get
            If _bf Is Nothing OrElse _bf.IsDisposed Then
                _bf = New BrowserForm
            End If
            Return _bf
        End Get
    End Property

    Public ReadOnly Property DataForm() As DataBrowserForm
        Get
            If _dbf Is Nothing OrElse _dbf.IsDisposed Then
                _dbf = New DataBrowserForm
            End If
            Return _dbf
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

    Private WithEvents _zoneTimer As Stopwatch
    Private ReadOnly Property ZoneTimer() As Stopwatch
        Get
            If _zoneTimer Is Nothing Then
                _zoneTimer = New Stopwatch
            End If
            Return _zoneTimer
        End Get
    End Property
    Private Property CampingMode() As Boolean

    Private Property Mobs As MobList

    Private Property SpawnWatching As Boolean

    Private _spawnList As List(Of Integer)
    Friend Property SpawnList As List(Of Integer)
        Get
            If _spawnList Is Nothing Then
                _spawnList = New List(Of Integer)
            End If
            Return _spawnList
        End Get
        Set(ByVal value As List(Of Integer))
            _spawnList = value
        End Set
    End Property

    Private _watchList As List(Of Integer)
    Friend Property WatchList As List(Of Integer)
        Get
            If _watchList Is Nothing Then
                _watchList = New List(Of Integer)
            End If
            Return _watchList
        End Get
        Set(ByVal value As List(Of Integer))
            _watchList = value
        End Set
    End Property

    Private _trackForms As List(Of SpawnTrackerForm)
    Private ReadOnly Property TrackForms As List(Of SpawnTrackerForm)
        Get
            If _trackForms Is Nothing Then
                _trackForms = New List(Of SpawnTrackerForm)
            End If
            Return _trackForms
        End Get
    End Property


    Private _debugForm As DebugForm
    Private ReadOnly Property DebugForm As DebugForm
        Get
            If _debugForm Is Nothing OrElse _debugForm.IsDisposed Then
                _debugForm = New DebugForm
            End If
            Return _debugForm
        End Get
    End Property

    Private WithEvents _mobWatcher As Watcher
    Private ReadOnly Property MobWatcher As Watcher
        Get
            If _mobWatcher Is Nothing Then
                _mobWatcher = New Watcher(MemoryScanner.WatcherTypes.MobList Or MemoryScanner.WatcherTypes.ZoneChange)
            End If
            Return _mobWatcher
        End Get
    End Property
#End Region

#Region " FORM ACTIONS "
    Private Sub AppBarForm_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Activated
        If Not NoBar Then
            BringToFront()
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        MemoryScanner.Scanner.EndScanning()

        'Opacity = 1
        If Not _sof Is Nothing Then
            _sof.FadeOut(1000)
        End If
        If _abm IsNot Nothing Then
            _abm.UnregisterAppBar()
        End If
        CampedMobManager.SaveData()
    End Sub

    Private Sub AppBarForm_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        If NoBar Then
            Hide()
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            If DebugRun Then
                DebugForm.Show()
            End If

            If DebugRun Then DebugForm.AddDebugMessage("Applying Theme...", False)
            ApplyTheme()
            If DebugRun Then DebugForm.AppendMessage("  Success!", True)
            _isLoading = True

            If DebugRun Then DebugForm.AddDebugMessage("Initializing the database...", False)
            DataAccess.Initialize()
            If DebugRun Then DebugForm.AppendMessage("  Success!", True)
            'DataAccess.MobData.AcceptChanges()

            'Setup the menus
            If DebugRun Then DebugForm.AddDebugMessage("Creating menu renderers...", False)
            Dim renderer As New Office2007Renderer
            CloseMenu.Renderer = renderer
            MainMenu.Renderer = renderer
            NotifyMenu.Renderer = renderer
            If DebugRun Then DebugForm.AppendMessage("  Success!", True)

            For Each tsi As ToolStripItem In tsApRadar.DropDownItems
                If tsi.Tag = "pro" Then
                    tsi.Enabled = GlobalSettings.IsProEnabled
                End If
            Next

            For Each tsi As ToolStripItem In NotifyMenu.Items
                If tsi.Tag = "pro" Then
                    tsi.Enabled = GlobalSettings.IsProEnabled
                End If
            Next

            If DebugRun Then DebugForm.AddDebugMessage("Initializing watcher...", False)
            MemoryScanner.Scanner.RefreshRate = My.Settings.RefreshRate
            MobWatcher.Init()
            If DebugRun Then DebugForm.AppendMessage("  Success!", True)

            If DebugRun Then DebugForm.AddDebugMessage("Attaching watcher...", False)
            MemoryScanner.Scanner.AttachWatcher(MobWatcher)
            If DebugRun Then DebugForm.AppendMessage("  Success!", True)

            If DebugRun Then DebugForm.AddDebugMessage("Loading settings...", False)
            tsPosition.Visible = My.Settings.ShowPosition
            positionSpacer.Visible = My.Settings.ShowPosition

            tsClock.Visible = My.Settings.ShowClock
            clockSpacer.Visible = My.Settings.ShowClock

            tsZone.Visible = My.Settings.ShowZoneTimer
            zoneSpacer.Visible = My.Settings.ShowZoneTimer

            If GlobalSettings.IsProEnabled Then

                tsCampingMode.Checked = My.Settings.CampingMode
                ntsCampingMode.Checked = My.Settings.CampingMode
                tsSpawnAlerts.Checked = My.Settings.StartSpawnAlerts
                'Load the camped mobs data
                CampedMobManager.LoadData()
                'Load the global watch list for spawn alerts
                LoadWatchList()

            End If


            If DebugRun Then DebugForm.AppendMessage("  Success!", True)

            'Set the renderers
            If Not NoBar Then
                If DebugRun Then DebugForm.AddDebugMessage("Begin rendering the ApBar...", True)

                'If DebugRun Then DebugForm.AddDebugMessage("Loading RSS feed...", False)
                'LoadRSS()
                'If My.Settings.AutomaticNewsUpdates Then
                'NewsTimer.Interval = (My.Settings.NewsCheckInterval * 60 * 1000)
                'NewsTimer.Start()
                'End If
                If DebugRun Then DebugForm.AppendMessage("  Success!", True)

                ntsSelectProcess.Visible = False
                ProcessSeperator.Visible = False

                'The mob dataform
                If DebugRun Then DebugForm.AddDebugMessage("Initializing form objects...", False)
                _mdf = New MobDataForm(Me)
                _pcf = New PCInfoForm
                If DebugRun Then DebugForm.AppendMessage("  Success!", True)

                Dim mt As MonitorType
                If DebugRun Then DebugForm.AddDebugMessage("Setting screen...", False)
                If My.Settings.Monitor = "Secondary" AndAlso Screen.AllScreens.Count > 1 Then
                    If Screen.AllScreens(1) Is Screen.PrimaryScreen Then
                        _screen = Screen.AllScreens(0)
                    Else
                        _screen = Screen.AllScreens(1)
                    End If
                    mt = MonitorType.Secondary
                Else
                    _screen = Screen.PrimaryScreen
                    mt = MonitorType.Primary
                End If
                If DebugRun Then DebugForm.AppendMessage("  Success!", True)

                If DebugRun Then DebugForm.AddDebugMessage("Initializing ApBar Manager...", False)
                Try
                    _abm = New AppBarManager(Me, ThemeHandler.ActiveTheme.DockPosition, mt, _screen.Bounds.Width, 36)
                Catch ex As Exception
                    _abm = New AppBarManager(Me, DockMode.Top, mt, 100, 36)
                End Try
                If DebugRun Then DebugForm.AppendMessage("  Success!", True)

                tsAutoHideBar.Checked = My.Settings.AutoHide
                If Not My.Settings.AutoHide Then
                    If DebugRun Then DebugForm.AddDebugMessage("Registering ApBar...", False)
                    _abm.InitializeAppBar()
                    If DebugRun Then DebugForm.AppendMessage("  Success!", True)
                Else
                    If DebugRun Then DebugForm.AddDebugMessage("Starting Auto-Hide...", False)
                    Location = New Point(_screen.Bounds.Left, _screen.Bounds.Top)
                    Width = _screen.Bounds.Width
                    _isAutoHide = True
                    If _timer Is Nothing Then
                        _timer = New Timer
                    End If
                    _timer.Interval = 100
                    _timer.Start()
                    If DebugRun Then DebugForm.AppendMessage("  Success!", True)
                End If
                If DebugRun Then DebugForm.AddDebugMessage("Initalizing form animator...", False)
                _sof = New FormAnimator(Me)
                If DebugRun Then DebugForm.AppendMessage("  Success!", True)

                If DebugRun Then DebugForm.AddDebugMessage("Loading form...", False)
                _sof.FadeIn(1000)
                If DebugRun Then DebugForm.AppendMessage("  Success!", True)


            Else
                Size = New Size(0, 0)
                ntsAutoHide.Visible = False
                _screen = Screen.PrimaryScreen
            End If

            If DebugRun Then DebugForm.AddDebugMessage("Loading available processes...", False)
            LoadAvailableProcesses()
            If DebugRun Then DebugForm.AppendMessage("  Success!", True)

            If My.Settings.MapOpen Then
                If DebugRun Then DebugForm.AddDebugMessage("Opening Map Radar...", False)
                tsMapped.PerformClick()
                If DebugRun Then DebugForm.AppendMessage("  Success!", True)
            End If
            If My.Settings.OverlayOpen Then
                If DebugRun Then DebugForm.AddDebugMessage("Opening Overlay radar...", False)
                tsOverlay.PerformClick()
                If DebugRun Then DebugForm.AppendMessage("  Success!", True)
            End If

            AddHandler CampedMobManager.DebugEvent, AddressOf CampedMobManager_DebugEvent
        Catch ex As Exception
            If DebugRun Then DebugForm.AppendMessage("  Failed!", True)
            MessageBox.Show(ex.Message, "ApBar Form Load")
        End Try
        _isLoading = False
    End Sub
#End Region

#Region " OVERRIDE METHODS "
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80
            cp.ExStyle = cp.ExStyle And Not &H40000
            Return cp
        End Get
    End Property
#End Region

#Region " CONTROLS "
#Region " -- APRADAR MENU "
    Private Sub tsOverlay_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsOverlay.Click, ntsOverlayRadar.Click
        'If Not _ffxi Is Nothing Then
        If _orf Is Nothing OrElse _orf.IsDisposed Then
            _orf = New AlphaOverlayRadarForm()
        End If
        _orf.ProEnabled = GlobalSettings.IsProEnabled
        _orf.OverlayRadar.TextRendering = My.Settings.TextRendering
        _orf.OverlayRadar.SmoothingMode = My.Settings.SmoothingMode
        _orf.OverlayRadar.CompositingQuality = My.Settings.CompositingQuality
        tsOverlayRadar.Visible = True
        _orf.Show()
        'End If
    End Sub

    Private Sub tsMapped_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsMapped.Click, ntsMapRadar.Click
        'If Not _ffxi Is Nothing Then
        If _mrf Is Nothing OrElse _mrf.IsDisposed Then
            _mrf = New AlphaMappedRadarForm()
        End If
        _mrf.ProEnabled = GlobalSettings.IsProEnabled
        _mrf.MapRadar.TextRendering = My.Settings.TextRendering
        _mrf.MapRadar.SmoothingMode = My.Settings.SmoothingMode
        _mrf.MapRadar.CompositingQuality = My.Settings.CompositingQuality
        If Not NoBar Then
            tsMappedRadar.Visible = True
        End If
        _mrf.Show()
        _mrf.BringToFront()
        'End If
    End Sub

    Private Sub tsRecipeSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsRecipeSearch.Click, ntsRecipeSearch.Click
        Dim rsf As New RecipeSearchForm()
        rsf.HeaderPanel.BackgroundImage = ThemeHandler.HeaderImage

        rsf.Left = _screen.Bounds.Left + 110
        If _abm Is Nothing Then
            rsf.Top = 0
        ElseIf _abm.DockMode = DockMode.Top Then
            rsf.Top = Height
        Else
            rsf.Top = (Location.Y - rsf.Height)
        End If
        rsf.Show()
    End Sub

    Private Sub tsDatabase_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsDatabase.Click, ntsDatabase.Click
        DataForm.Left = _screen.Bounds.Left
        If _abm Is Nothing Then
            DataForm.Top = 0
        ElseIf _abm.DockMode = DockMode.Top Then
            DataForm.Top = Height
        Else
            DataForm.Top = Top - DataForm.Height
        End If
        DataForm.CurrentZone = _currentZone
        DataForm.Show()
        DataForm.cboZones.SelectedValue = _currentZone
    End Sub

    Private Sub tsAbout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsAbout.Click, ntsAboutApRadar.Click
        Dim abf As New AboutForm
        abf.Show()
    End Sub

    Private Sub tsCampedMobBrowser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsCampedMobBrowser.Click, ntsViewTod.Click
        If Not _orf Is Nothing AndAlso Not _orf.IsDisposed Then
            _orf.OverlayRadar.SaveCampedMobs()
        End If
        Dim tod As New TodBrowserForm(_currentZone) With {.Left = Left}
        If _abm Is Nothing Then
            tod.Top = 0
        ElseIf _abm.DockMode = DockMode.Top Then
            tod.Top = 36
        Else
            tod.Top = Top - tod.Height
        End If
        tod.Show()
    End Sub

    Private Sub tsSettings_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsSettings.Click, ntsSettings.Click
        Dim sf As New SettingsForm
        AddHandler sf.SettingsChanged, AddressOf SettingsForm_SettingsChanged
        sf.Show()
    End Sub

    Private Sub SettingsForm_SettingsChanged()
        ApplySettings()
    End Sub


    'Private Sub tsMapUpdateCheck_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsMapUpdateCheck.Click, ntsMapUpdates.Click
    'CheckMapVersion(True)
    'End Sub

    Private Sub ntsAutoHide_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ntsAutoHide.CheckedChanged
        tsAutoHideBar.Checked = ntsAutoHide.Checked
    End Sub

    Private Sub tsAutoHideBar_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tsAutoHideBar.CheckedChanged
        Try
            If Not _isLoading AndAlso tsAutoHideBar.Checked <> _isAutoHide Then
                If tsAutoHideBar.Checked Then
                    If Not _abm Is Nothing Then
                        _abm.UnregisterAppBar()
                    End If
                    _isAutoHide = True
                    If _timer Is Nothing Then
                        _timer = New Timer
                    End If
                    _timer.Interval = 100
                    _timer.Start()
                Else
                    _timer.Stop()
                    If Not _abm Is Nothing Then
                        _abm.DockMode = ThemeHandler.ActiveTheme.DockPosition
                        _abm.InitializeAppBar()
                    End If
                    _sof.SlideOut(150, SlideDirection.Down)
                    _isAutoHide = False
                End If
                ntsAutoHide.Checked = tsAutoHideBar.Checked
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click, ntsExit.Click
        tsExit.PerformClick()
    End Sub

    Private Sub tsCampingMode_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tsCampingMode.CheckedChanged
        CampingMode = tsCampingMode.Checked
        ntsCampingMode.Checked = tsCampingMode.Checked
    End Sub

    Private Sub ntsCampingMode_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ntsCampingMode.CheckedChanged
        tsCampingMode.Checked = ntsCampingMode.Checked
    End Sub

    Private Sub tsSpawnAlerts_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tsSpawnAlerts.CheckedChanged
        SpawnWatching = tsSpawnAlerts.Checked
        ntsSpawnAlerts.Checked = tsSpawnAlerts.Checked
        'LEt the watcher know that it should watch for spawns too
        If SpawnWatching Then
            If MobWatcher.Type And MemoryScanner.WatcherTypes.MobStatus <> MemoryScanner.WatcherTypes.MobStatus Then
                MobWatcher.Type = MobWatcher.Type Or MemoryScanner.WatcherTypes.MobStatus
            End If
        Else
            MobWatcher.Type = MobWatcher.Type Xor MemoryScanner.WatcherTypes.MobStatus
        End If

    End Sub

    Private Sub ntsSpawnAlerts_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ntsSpawnAlerts.CheckedChanged
        tsSpawnAlerts.Checked = ntsSpawnAlerts.Checked
    End Sub

    Private Sub tsSpawnFilter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsSpawnFilter.Click, ntsSpawnFilter.Click
        Using sad As New SpawnAlertDialog(_currentZone) With {.SpawnList = SpawnList, .GlobalWatch = WatchList}
            If sad.ShowDialog = DialogResult.OK Then
                SpawnList = sad.SpawnList
                WatchList = sad.GlobalWatch
                SaveWatchList()
            End If
        End Using
    End Sub

    Private Sub tsChat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsChat.Click, ntsApRadarChat.Click
        Dim cf As New ChatForm
        cf.Show()
    End Sub

    Private Sub FFXIRemoteChatServToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsFFXIRemoteChat.Click, ntsFFXIChatServer.Click
        If Not MemoryScanner.Scanner.FFXI Is Nothing Then
            If My.Settings.RemoteChatMessage Then
                Using mf As New RemoteChatMessageDialog()
                    mf.ShowDialog()
                End Using
            End If
            Dim rcs As New RemoteChatServerForm()
            rcs.Show()
        Else
            MessageBox.Show("FFXI Must be running and you must have an instance selected to use the Remote Chat Feature")
        End If
    End Sub

    Private Sub tsDatItemBrowser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsDatItemBrowser.Click, ntsDatItemBrowser.Click
        Dim dib As New DatItemBrowser() With {.AppBarForm = Me}
        dib.Show()
    End Sub
#End Region

#Region " -- MAIN MENU BAR "
    Private Sub txtMob_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSearch.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMob_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtSearch.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(txtSearch.Text.Trim) Then
                tsSearch.PerformClick()
            End If
        End If
    End Sub

    Private Sub tsSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsSearch.Click
        If txtSearch.Text.Trim <> String.Empty Then
            Dim targetHeight As Integer
            Dim searchUrl As String = String.Empty

            'Get the search url
            searchUrl = GetSearchUrl(CType(tsSearchProvider.Tag, SearchProvider), txtSearch.Text)
            If searchUrl = "Recipes" OrElse searchUrl = "Items" Then
                If searchUrl = "Recipes" Then
                    Dim rf As New RecipeSearchForm()
                    rf.SearchItem(txtSearch.Text, RecipeSearchForm.SearchType.Result)
                    rf.Show()
                End If
            Else
                BrowserForm.Browser.Navigate(searchUrl)
                If Not BrowserForm.Visible Then

                    BrowserForm.Width = Math.Min(1024, _screen.Bounds.Width - 106)
                    targetHeight = _screen.Bounds.Height - 236
                    If targetHeight < 800 Then
                        BrowserForm.Height = _screen.Bounds.Height - 236
                    Else
                        BrowserForm.Height = 800
                    End If
                    If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
                        BrowserForm.Location = New Point(_screen.Bounds.Left + 106, Height)
                    Else
                        BrowserForm.Location = New Point(_screen.Bounds.Left + 106, Top - BrowserForm.Height)
                    End If
                    BrowserForm.Opacity = Opacity
                    BrowserForm.Show()
                End If
            End If
        End If
    End Sub

    Private Sub tsAddMob_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsAddMob.Click
        Dim index = DataAccess.AddMob(_tData.Name, _mdf.Zone, True)
        _mdf.MobID = index
        If index > 0 Then
            tsAddMob.Visible = False
        End If
    End Sub

    Private Sub tsPedometer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsPedometer.Click
        Dim p As New PedometerForm
        p.Show()
    End Sub
#End Region

#Region " -- CLOSE MENU "
    Private Sub tsExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsExit.Click
        If Not BrowserForm Is Nothing AndAlso BrowserForm.Visible Then
            BrowserForm.Close()
        End If
        If Not _mrf Is Nothing AndAlso _mrf.Visible Then
            My.Settings.MapOpen = True
            _mrf.Close()
        Else
            My.Settings.MapOpen = False
        End If
        If Not _orf Is Nothing AndAlso _orf.Visible Then
            My.Settings.OverlayOpen = True
            _orf.Close()
        Else
            My.Settings.OverlayOpen = False
        End If
        Close()
    End Sub

    Private Sub tsOverlayRadar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsOverlayRadar.Click
        If Not _orf Is Nothing AndAlso Not _orf.IsDisposed Then
            _orf.BringToFront()
            _tsOverlayCMS = _orf.ContextMenuStrip
            If _tsOverlayCMS.Visible Then
                _tsOverlayCMS.Hide()
            Else
                _tsOverlayCMS.Show(MainMenu, New Point(tsOverlayRadar.Bounds.Left, 36))
            End If
        End If
    End Sub

    Private Sub tsMappedRadar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsMappedRadar.Click
        If Not _mrf Is Nothing AndAlso Not _mrf.IsDisposed Then
            _mrf.BringToFront()
            _tsMappedCMS = _mrf.ContextMenuStrip
            If _tsMappedCMS.Visible Then
                _tsMappedCMS.Hide()
            Else
                _tsMappedCMS.Show(MainMenu, New Point(tsMappedRadar.Bounds.Left, 36))
            End If
        End If
    End Sub

    Private Sub RSS_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        Dim url As String = String.Empty
        If TypeOf sender Is LinkLabel Then
            url = CType(sender, LinkLabel).Tag
        ElseIf TypeOf sender Is Label Then
            url = CType(CType(sender, Label).Parent, FeedViewer).lnkTitle.Tag
        ElseIf TypeOf sender Is FeedViewer Then
            url = CType(sender, FeedViewer).lnkTitle.Tag
        End If
        BrowserForm.Browser.Navigate(url)
        If Not BrowserForm.Visible Then
            Dim targetHeight As Integer
            BrowserForm.Width = Math.Min(1024, _screen.Bounds.Width - 106)
            targetHeight = _screen.Bounds.Height - 236
            If targetHeight < 800 Then
                BrowserForm.Height = _screen.Bounds.Height - 236
            Else
                BrowserForm.Height = 800
            End If
            If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
                BrowserForm.Location = New Point(_screen.Bounds.Left + 106, Height)
            Else
                BrowserForm.Location = New Point(_screen.Bounds.Left + 106, Top - BrowserForm.Height)
            End If
            BrowserForm.Opacity = Opacity
            BrowserForm.Show()
        End If
    End Sub

    Private Sub tsProcess_DropDownOpening(ByVal sender As Object, ByVal e As EventArgs) Handles tsProcess.DropDownOpening
        LoadAvailableProcesses()
    End Sub
#End Region

#Region " -- FORM CONTROLS "
    Private Sub AppBarForm_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.MouseEnter, MainMenu.MouseEnter, CloseMenu.MouseEnter
        SetActiveWindow(Handle)
    End Sub

    Private Sub tsAlla_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsAlla.Click, tsWiki.Click, tsItems.Click, tsRecipes.Click, tsFFXIAH.Click
        tsSearchProvider.Image = CType(sender, ToolStripItem).Image
        tsSearchProvider.Tag = sender.Tag
        tsSearchProvider.ToolTipText = String.Format("Search using {0}", sender.Text)
    End Sub

    Private Sub tsTargetInfo_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles tsTargetInfo.MouseEnter
        Try
            If _mdf.MobFound Then
                _isTargetInfoDropped = True
                _mdf.Left = _screen.Bounds.Left + 320
                If _abm.DockMode = DockMode.Top Then
                    _mdf.Top = Height
                Else
                    _mdf.Top = (Location.Y - _mdf.Height)
                End If
                _mdf.BringToFront()
                _mdf.SlideOut()
                MouseOverTimer.Start()
            ElseIf _pcf.PCFound Then
                _isPCInfoDropped = True
                _pcf.Left = _screen.Bounds.Left + 320
                If _abm.DockMode = DockMode.Top Then
                    _pcf.Top = Height
                Else
                    _pcf.Top = (Location.Y - _pcf.Height)
                End If
                _pcf.BringToFront()
                _pcf.RollOut()
                MouseOverTimer.Start()
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub tsRSS_DropDownClosed(ByVal sender As Object, ByVal e As EventArgs) Handles tsRSS.DropDownClosed
    'tsRSS.Image = My.Resources.Rss
    'End Sub

    'Private Sub CheckForNewNewsItemsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CheckForNewNewsItemsToolStripMenuItem.Click
    'LoadRSS()
    'End Sub

    Private Sub ScanDatsForNewMobsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsScanDatsForMobs.Click, ntsScanDatsForNewMobs.Click
        Dim dcd As New DatCheckDialog
        dcd.Show()
    End Sub

    Private _windowerRadar As WindowerOverlay

    Private Sub TestRadarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Dim off As New WPFDatItemBrowser
        'off.Show()
        'Dim ml As New MemLocsForm
        'ml.Show()

        'Dim uef As New UnhandledErrorDialog(New FormatException("String was in the incorrect format"))
        'uef.Show()
        'If _windowerRadar Is Nothing Then
        '    _windowerRadar = New WindowerOverlay(MemoryScanner.Scanner.FFXI.POL, Nothing)
        'Else
        '    _windowerRadar.Dispose()
        'End If
        Dim off As New OffsetForm
        off.Show()
    End Sub
#End Region
#End Region

#Region " TIMERS "

    Private Sub _timer_tick(ByVal sender As Object, ByVal e As EventArgs) Handles _timer.Tick
        Try
            If Not _isAutoHide Then
                _timer.Stop()
            Else
                If _isHidden AndAlso Not _sof Is Nothing AndAlso Not ThemeHandler.ActiveTheme Is Nothing Then
                    If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
                        If Cursor.Position.X >= _screen.Bounds.Left AndAlso Cursor.Position.X <= _screen.Bounds.Right AndAlso Cursor.Position.Y <= 5 Then
                            _isHidden = False
                            BringToFront()
                            _sof.SlideOut(150, SlideDirection.Down)
                        End If
                    Else
                        If Cursor.Position.X >= _screen.Bounds.Left AndAlso Cursor.Position.X <= _screen.Bounds.Right AndAlso Cursor.Position.Y >= Bounds.Bottom - 5 AndAlso Cursor.Position.Y <= Bounds.Bottom Then
                            _isHidden = False
                            BringToFront()
                            _sof.SlideOut(150, SlideDirection.Up)
                        End If
                    End If
                Else
                    If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
                        If Not Bounds.Contains(Cursor.Position) Then
                            If (BrowserForm Is Nothing OrElse Not BrowserForm.Visible) AndAlso
                            Not tsApRadar.DropDown.Visible AndAlso Not tsSearchProvider.DropDown.Visible _
                            AndAlso Not _isTargetInfoDropped AndAlso Not _isPCInfoDropped AndAlso (_tsOverlayCMS Is Nothing OrElse Not _tsOverlayCMS.Visible) _
                            AndAlso _lostFocusCount >= 10 Then
                                _lostFocusCount = 0
                                _isHidden = True
                                _sof.RollUp(150, SlideDirection.Down)
                            Else
                                _lostFocusCount += 1
                            End If
                        Else
                            _lostFocusCount = 0
                        End If
                    Else
                        If Not Bounds.Contains(Cursor.Position) Then
                            If (BrowserForm Is Nothing OrElse Not BrowserForm.Visible) AndAlso
                            Not tsApRadar.DropDown.Visible AndAlso Not tsSearchProvider.DropDown.Visible _
                            AndAlso Not _isTargetInfoDropped AndAlso (_tsOverlayCMS Is Nothing OrElse Not _tsOverlayCMS.Visible) _
                            AndAlso _lostFocusCount >= 10 Then
                                _lostFocusCount = 0
                                _isHidden = True
                                _sof.RollUp(150, SlideDirection.Up)
                            Else
                                _lostFocusCount += 1
                            End If
                        Else
                            _lostFocusCount = 0
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            If DebugRun Then
                DebugForm.AddDebugMessage("Error", ex.Message, True)
            End If
        End Try
    End Sub


    Private Sub CurrentZoneTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles CurrentZoneTimer.Tick
        If My.Settings.ShowZoneTimer Then
            tsZone.Text = String.Format("Zone: [{0}] {1} - {2}:{3}:{4}", _currentZone.ToString(), _zoneName, ZoneTimer.Elapsed.Hours.ToString("00"), ZoneTimer.Elapsed.Minutes.ToString("00"), ZoneTimer.Elapsed.Seconds.ToString("00"))
        End If
        If My.Settings.ShowClock Then
            tsClock.Text = Date.Now.ToLongTimeString
        End If
    End Sub

    Private Sub MouseOverTimer_tick(ByVal sender As Object, ByVal e As EventArgs) Handles MouseOverTimer.Tick
        Dim cursorLoc As Point = Cursor.Position
        Dim textRect As New Rectangle(_screen.Bounds.Left + tsTargetInfo.Bounds.X, Bounds.Y, _screen.Bounds.Left + tsTargetInfo.Bounds.Width, 36)
        If _isTargetInfoDropped Then
            Dim dropDownRect As Rectangle = _mdf.Bounds
            If Not textRect.Contains(cursorLoc) AndAlso Not dropDownRect.Contains(cursorLoc) Then
                MouseOverTimer.Stop()
                _mdf.RollUp()
                _isTargetInfoDropped = False
            End If
        ElseIf _isPCInfoDropped Then
            Dim dropDownRect As Rectangle = _pcf.Bounds
            If Not textRect.Contains(cursorLoc) AndAlso Not dropDownRect.Contains(cursorLoc) Then
                MouseOverTimer.Stop()
                _pcf.RollUp()
                _isPCInfoDropped = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' This timer will watch for any new spawned pol processes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub processWatchTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles processWatchTimer.Tick
        LoadAvailableProcesses()
    End Sub

    'Private Sub NewsTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles NewsTimer.Tick
    'LoadRSS()
    'End Sub

#End Region

#Region " PRIVATE METHODS "


    'Private Function BuildMobInfo() As String
    '    Dim sb As New System.Text.StringBuilder
    '    Dim name As String = _tData.Name
    '    Dim zone As Integer = MemoryScanner.Scanner.CurrentMap
    '    Dim mob = (From mobs In DataAccess.MobData.Mobs Where _
    '               mobs.MobName = name And mobs.Zone = zone _
    '               Select mobs).FirstOrDefault

    '    If Not mob Is Nothing Then
    '        sb.Append(mob.MobName)
    '        If mob.NM = 1 Then
    '            sb.Append(" [NM]")
    '        End If
    '        sb.Append(Environment.NewLine)
    '        If Not mob.IsFamilyNull Then
    '            sb.Append(String.Format("Family:{0}{0}{1}{2}", ControlChars.Tab, mob.Family & "", Environment.NewLine))
    '        Else
    '            sb.Append(String.Format("Family:{0}", Environment.NewLine))
    '        End If
    '        If Not mob.IsJobNull Then
    '            sb.Append(String.Format("Job:{0}{0}{1}{2}", ControlChars.Tab, mob.Job & "", Environment.NewLine))
    '        Else
    '            sb.Append(String.Format("Job:{0}", Environment.NewLine))
    '        End If
    '        sb.Append(String.Format("Behavior:{0}", ControlChars.Tab))
    '        sb.Append(GetBehavior(mob))
    '        sb.Append(Environment.NewLine)
    '        sb.Append(GetDetection(mob))
    '        sb.Append(Environment.NewLine)
    '        sb.Append(String.Format("Level Range:{0}{1}-{2}", ControlChars.Tab, mob.MinLevel, mob.MaxLevel))

    '        Dim mobId As Integer = mob.MobPK
    '        Dim items = (From i In DataAccess.MobData.Items Join _
    '                     im In DataAccess.MobData.ItemsToMobs On _
    '                     i.ItemID Equals im.ItemID Where _
    '                     im.MobPK = mobId Select New With {i.ItemName})

    '        If Not items Is Nothing Then
    '            Dim isFirst As Boolean = True
    '            sb.Append(Environment.NewLine)
    '            sb.Append(String.Format("Drops:{0}{0}", ControlChars.Tab))
    '            For Each item In items
    '                If isFirst Then
    '                    sb.Append(item.ItemName & Environment.NewLine)
    '                    isFirst = False
    '                Else
    '                    sb.Append(String.Format("{0}{0}{1}{2}", ControlChars.Tab, item.ItemName, Environment.NewLine))
    '                End If
    '            Next
    '        End If
    '    End If
    '    Return sb.ToString
    'End Function

    'Private Shared Function GetBehavior(ByVal Mob As MobsRow) As String
    '    Dim output As String
    '    If Mob.Aggressive Then
    '        output = "Aggressive"
    '    Else
    '        output = String.Empty
    '    End If
    '    If Mob.Links Then
    '        If output = String.Empty Then
    '            output = "Links"
    '        Else
    '            output &= ", Links"
    '        End If
    '    End If
    '    Return output
    'End Function

    'Private Shared Function GetDetection(ByVal Mob As MobsRow) As String
    '    Dim output As String
    '    If Mob.DetectsSight Then
    '        output = "S"
    '    Else
    '        output = String.Empty
    '    End If
    '    If Mob.DetectsSound Then
    '        If output = String.Empty Then
    '            output = "H"
    '        Else
    '            output &= ", H"
    '        End If
    '    End If
    '    If Mob.DetectsMagic Then
    '        If output = String.Empty Then
    '            output = "M"
    '        Else
    '            output &= ", M"
    '        End If
    '    End If
    '    If Mob.DetectsLowHP Then
    '        If output = String.Empty Then
    '            output = "↓HP"
    '        Else
    '            output &= ", ↓HP"
    '        End If
    '    End If
    '    If Mob.DetectsHealing Then
    '        If output = String.Empty Then
    '            output = "Heal"
    '        Else
    '            output &= ", Heal"
    '        End If
    '    End If
    '    If Mob.TracksScent Then
    '        If output = String.Empty Then
    '            output = "Sc"
    '        Else
    '            output &= ", Sc"
    '        End If
    '    End If
    '    If Mob.TrueSight Then
    '        If output = String.Empty Then
    '            output = "T(S)"
    '        Else
    '            output &= ", T(S)"
    '        End If
    '    End If
    '    If Mob.TrueSound Then
    '        If output = String.Empty Then
    '            output = "T(H)"
    '        Else
    '            output &= ", T(H)"
    '        End If
    '    End If
    '    If output <> String.Empty Then
    '        output = String.Format("Detects:{0}{0}{1}", ControlChars.Tab, output)
    '    End If
    '    Return output
    'End Function

    Friend Sub showBrowser(ByVal url As String)
        Dim targetHeight As Integer = _screen.Bounds.Height - 236
        BrowserForm.Browser.Navigate(url)
        BrowserForm.Location = New Point(Left + 106, Height)
        BrowserForm.Width = _screen.Bounds.Width - 106
        If targetHeight < 800 Then
            BrowserForm.Height = _screen.Bounds.Height - 236
        Else
            BrowserForm.Height = 800
        End If
        BrowserForm.Opacity = Opacity
        BrowserForm.Show()
    End Sub

    ''' <summary>
    ''' Method to load any available pol processes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAvailableProcesses()
        'Get the current list of availabel processes
        _pList = From p In Process.GetProcesses Where p.ProcessName = "pol" Or p.ProcessName = "xiloader" Or p.ProcessName = "wingsloader" Or p.ProcessName = "edenxi" Or p.ProcessName = "horizon-loader"

        Dim processMenu As Object

        If NoBar Then
            processMenu = ntsSelectProcess
        Else
            processMenu = tsProcess
        End If

        'Check to see if any processes are found
        If _pList.Count > 0 Then
            'First loop through the drop down items and remove any that
            'are not in the process list
            Dim isFound As Boolean = False
            Dim displayName As String

            For i = processMenu.DropDownItems.Count - 1 To 0 Step -1
                isFound = False
                For Each p As Process In _pList
                    If processMenu.DropDownItems(i).Tag = p.Id Then
                        displayName = String.Format("{0} [{1}]", p.MainWindowTitle, p.Id)
                        processMenu.DropDownItems(i).Text = displayName
                        If p.Id = _currentProcessID Then
                            processMenu.Text = displayName
                        End If
                        isFound = True
                        Exit For
                    End If
                Next
                If Not isFound Then
                    processMenu.DropDownItems.RemoveAt(i)
                End If
            Next

            'Next loop though the processes and add any that 
            'are not already in the list
            Dim tsi As ToolStripItem

            For Each p As Process In _pList
                isFound = False
                For Each item As ToolStripItem In processMenu.DropDownItems
                    If item.Tag = p.Id Then
                        isFound = True
                    End If
                Next
                If Not isFound Then 'AndAlso p.MainWindowTitle.Trim <> String.Empty Then
                    tsi = processMenu.DropDownItems.Add(String.Format("{0} [{1}]", p.MainWindowTitle, p.Id))
                    tsi.Tag = p.Id
                    AddHandler tsi.Click, AddressOf processItem_click
                End If
            Next

            'If there is only one process we go ahead and select it.
            If processMenu.DropDownItems.Count = 1 AndAlso processMenu.DropDownItems(0).Tag <> _currentProcessID Then
                If Not processMenu.DropDownItems(0).Text.StartsWith("PlayOnline") AndAlso
                Not processMenu.DropDownItems(0).Text.StartsWith("[") Then
                    processMenu.DropDownItems(0).PerformClick()
                End If
            End If
        Else
            _currentProcessID = 0
            If processMenu.DropdownItems.Count > 0 Then
                processMenu.DropDownItems.Clear()
            End If
            'We Didn't find any pol processes
            If _isProcessSelected Then _isProcessSelected = False
            If processMenu.text <> "FFXI Not Found" Then
                processMenu.Text = "FFXI Not Found"
            End If
            processMenu = Nothing

            ApRadarIcon.Text = "ApRadar 3 [FFXI Not Found]"

            _pList = Nothing

        End If
    End Sub

    Private Sub ApplySettings()
        MemoryScanner.Scanner.RefreshRate = My.Settings.RefreshRate

        If Not _orf Is Nothing AndAlso Not _orf.IsDisposed Then
            _orf.OverlayRadar.TextRendering = My.Settings.TextRendering
            _orf.OverlayRadar.SmoothingMode = My.Settings.SmoothingMode
            _orf.OverlayRadar.CompositingQuality = My.Settings.CompositingQuality
        End If

        If Not _mrf Is Nothing AndAlso Not _mrf.IsDisposed Then
            _mrf.MapRadar.TextRendering = My.Settings.TextRendering
            _mrf.MapRadar.SmoothingMode = My.Settings.SmoothingMode
            _mrf.MapRadar.CompositingQuality = My.Settings.CompositingQuality
        End If

        tsPosition.Visible = My.Settings.ShowPosition
        positionSpacer.Visible = My.Settings.ShowPosition

        tsZone.Visible = My.Settings.ShowZoneTimer
        zoneSpacer.Visible = My.Settings.ShowZoneTimer

        tsClock.Visible = My.Settings.ShowClock
        clockSpacer.Visible = My.Settings.ShowClock

        'If My.Settings.AutomaticNewsUpdates Then
        'If Not NewsTimer.Enabled Then
        'NewsTimer.Interval = My.Settings.NewsCheckInterval * 60000
        'NewsTimer.Start()
        'End If
        'Else
        'If NewsTimer.Enabled Then
        'NewsTimer.Stop()
        'End If
        'End If

        If My.Settings.Monitor = "Primary" Then

            _screen = Screen.PrimaryScreen
            If Not _abm Is Nothing Then
                _abm.Monitor = MonitorType.Primary
            End If
        Else
            If Not _abm Is Nothing Then
                If Screen.AllScreens.Count > 1 Then
                    If Screen.AllScreens(1) Is Screen.PrimaryScreen Then
                        _screen = Screen.AllScreens(0)
                    Else
                        _screen = Screen.AllScreens(1)
                    End If
                    _abm.Monitor = MonitorType.Secondary
                Else
                    _abm.Monitor = MonitorType.Primary
                End If
            End If
        End If
        'LoadRSS()

        ApplyTheme()
    End Sub

    Private Sub ApplyTheme()
        BackgroundImage = ThemeHandler.BarImage
        Dim fColor As Color = ThemeHandler.BarForeColor
        MainMenu.ForeColor = fColor
        CloseMenu.ForeColor = fColor
        If Not _abm Is Nothing Then
            If tsAutoHideBar.Checked Then
                If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
                    Location = New Point(_screen.Bounds.Left, 0)
                Else
                    Location = New Point(_screen.Bounds.Left, _screen.Bounds.Bottom - 36)
                End If
            Else
                _abm.DockMode = ThemeHandler.ActiveTheme.DockPosition
            End If
        End If
        'Let's handle the open forms and apply the themes
        For Each f As Form In My.Application.OpenForms
            If Not TypeOf f Is AppBarForm Then
                ThemeHandler.ApplyTheme(f)
            End If
        Next
    End Sub

    'Private Sub LoadRSS()
    'Try
    'Dim rss As New RSS
    'rss.LoadFeed("http://apradar.com/external.php?type=RSS2&forumids=13")
    'Dim rti As RSSToolStrip
    'Dim latestDate As Date
    'Dim count As Integer = 0
    ''Clear out any rss entries
    'For i = tsRSS.DropDownItems.Count - 1 To 1 Step -1
    'tsRSS.DropDownItems.RemoveAt(i)
    'Next

    ''Add any new rss items
    'For Each item As RSSItem In rss.FeedItems
    'tsRSS.DropDownItems.Add(New ToolStripSeparator)
    'If item.PubDate > latestDate Then
    'latestDate = item.PubDate
    'End If
    'rti = New RSSToolStrip()
    'rti.FeedViewer.FeedItem = item
    'AddHandler rti.FeedViewer.lnkTitle.LinkClicked, AddressOf RSS_LinkClicked

    'rti.Height = 135
    'tsRSS.DropDownItems.Add(rti)

    'count += 1
    'If count = My.Settings.MaxNewsItems Then
    'Exit For
    'End If
    'Next


    'If latestDate > My.Settings.LastRSSDate Then
    'My.Settings.LastRSSDate = latestDate
    'tsRSS.Image = My.Resources.rssNew
    'End If
    'Catch ex As Exception

    'End Try
    'End Sub

    Private Sub SaveWatchList()
        Serializer.SerializeToXML(Of List(Of Integer))(Application.StartupPath & "\Data\WatchList.dat", WatchList)
    End Sub

    Private Sub LoadWatchList()
        If IO.File.Exists(Application.StartupPath & "\Data\WatchList.dat") Then
            Try
                WatchList = Serializer.DeserializeFromXML(Of List(Of Integer))(Application.StartupPath & "\Data\WatchList.dat")
            Catch ex As Exception

            End Try
        End If
    End Sub

    Friend Sub AddTargetedMobToWatch()
        If Not SpawnList.Contains(_tData.ServerID) Then
            SpawnList.Add(_tData.ServerID)
        End If
    End Sub

    Private Sub ProcessTarget()
        If Not _tData Is Nothing Then

            'Handle the pc entries
            If _tData.MobType = MobTypes.PC Then
                'LightSteelBlue is the standatrd
                tsTargetInfo.ForeColor = ThemeHandler.PCColor
#If DEBUG Then
                tsTargetInfo.Text = String.Format("PC [{0}] {1} - HP: {2}% ", _tData.ID.ToString(), _
                                                     _tData.Name, _tData.HP, _tData.MobBase)
#Else
                tsTargetInfo.Text = String.Format("PC [{0}] {1} - HP: {2}% ", _tData.ID.ToString(),
                                                     _tData.Name, _tData.HP, _tData.MobBase)
#End If
                If _tData.ID <> _lastTargetId Then
                    _lastTargetId = _tData.ID
                End If

                If DataAccess.AddPC(_tData.Name, _tData.ServerID) Then
                    _pcf.PCID = _tData.ServerID
                Else
                    _pcf.PCID = -1
                End If
                _mdf.MobID = -1
                tsAddMob.Visible = False
            Else 'Handle any npc entries
                'Tomato is the standard
                tsTargetInfo.ForeColor = ThemeHandler.NPCColor

#If DEBUG Then
                tsTargetInfo.Text = String.Format("NPC [{0}] {1} HP: {2}% ", _tData.ID.ToString(), _
                                                     _tData.Name, _tData.HP, _tData.MobBase)

#Else
                tsTargetInfo.Text = String.Format("NPC [{0}] {1} HP: {2}% ", _tData.ID.ToString(),
                                                     _tData.Name, _tData.HP, _tData.MobBase)
#End If

                If _tData.ID <> _lastTargetId Then
                    '_targetInfoData = BuildMobInfo()
                    _mdf.Zone = _currentZone
                    Dim name As String = _tData.Name
                    _mob = (From mob In DataAccess.MobData.Mobs
                            Where mob.MobName = name And mob.Zone = _mdf.Zone).FirstOrDefault
                    If _mob Is Nothing Then
                        tsAddMob.Visible = True
                        _mdf.MobID = -1
                    Else
                        tsAddMob.Visible = False
                        _mdf.MobID = _mob.MobPK
                    End If
                    _lastTargetId = _tData.ID
                    _pcf.PCID = -1

                End If


            End If
        Else
            If Not NoBar Then
                _mdf.MobID = -1
                _pcf.PCID = -1
                tsAddMob.Visible = False
                tsTargetInfo.ForeColor = MainMenu.ForeColor
                tsTargetInfo.Text = "No Target"
                _targetInfoData = String.Empty
            End If

        End If
    End Sub
#End Region

#Region " EVENT HANDLERS "
    Private Sub _orf_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles _orf.Disposed
        tsOverlayRadar.Visible = False
    End Sub

    Private Sub _mrf_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles _mrf.Disposed
        tsMappedRadar.Visible = False
    End Sub

    ''' <summary>
    ''' This method handles the click event for the process items.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub processItem_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not _targetTimer Is Nothing Then
                _targetTimer.Stop()
            End If

            Dim tsi As ToolStripItem = DirectCast(sender, ToolStripItem)
            tsi.OwnerItem.Text = tsi.Text

            If Not tsi.Text.StartsWith("PlayOnline") AndAlso tsi.Text.Length < 52 Then
                ApRadarIcon.BalloonTipText = String.Format("ApRadar 3 [{0}]", tsi.Text)
            End If

            MemoryScanner.Scanner.FFXI = New FFXI(Process.GetProcessById(tsi.Tag))

            _isProcessSelected = True
            _currentProcessID = tsi.Tag

            CurrentZoneTimer.Start()
        Catch ex As Exception
            If DebugRun Then
                DebugForm.AddDebugMessage("Error", ex.Message, True)
            End If
        End Try
    End Sub

    Private Sub CampedMobManager_DebugEvent(ByVal Location As String, ByVal EventType As String, ByVal Message As String)
        DebugForm.AddDebugMessage(EventType, String.Format("{0} - {1}", Location, Message))
    End Sub

    Private Sub MobSpawned(ByVal Mob As MobData, ByVal OutOfRange As Boolean)
        If GlobalSettings.IsProEnabled Then
            If OutOfRange Then
                If My.Settings.SupressToast AndAlso My.Settings.PlayAlertOnSpawn Then
                    AudioManager.PlayAlert(My.Settings.AlertSound)
                Else
                    Dim message As String = String.Format("{0} spawned!{1}{2}{1}Out of range spawn detected!",
                                                          Mob.Name, ControlChars.NewLine, Now.ToLongTimeString)
                    Dim timeout As Integer = 0
                    If My.Settings.UseAlertTimeout Then
                        timeout = 15
                    End If
                    ShowToastForm(message, String.Format("{0} spawned!", Mob.Name), timeout, My.Settings.PlayAlertOnSpawn)
                End If
            Else
                'If we aren't showing the toast form, lets just play the alert
                If My.Settings.SupressToast AndAlso My.Settings.PlayAlertOnSpawn Then
                    AudioManager.PlayAlert(My.Settings.AlertSound)
                Else
                    Dim message As String = String.Format("{0} spawned!{3}{6}{3}X: {1}, Y: {2}{3}Distance: {4}{3}HP: {5}",
                                                            Mob.Name, Mob.X, Mob.Y, ControlChars.NewLine, Mob.Distance, Mob.HP, Now.ToLongTimeString)
                    Dim timeout As Integer = 0
                    If My.Settings.UseAlertTimeout Then
                        timeout = 15
                    End If
                    ShowToastForm(message, String.Format("{0} spawned!", Mob.Name), timeout, True)
                End If
            End If

            'Lets show the tracker form
            If My.Settings.ShowTracker Then
                Dim isTrackerUP As Boolean = False
                For Each t As SpawnTrackerForm In TrackForms
                    If t.MobServerID = Mob.ServerID Then
                        isTrackerUP = True
                        Exit For
                    End If
                Next
                If Not isTrackerUP Then
                    Dim stf As New SpawnTrackerForm(Mob.MobBase, _pData.MobBase)
                    Dim r As New RECT
                    'Modify the height to stagger the windows
                    Dim hMod As Integer = 0
                    If TrackForms.Count > 0 AndAlso TrackForms.Count Mod 2 <> 0 Then
                        hMod = 20
                    End If
                    If GetWindowRect(MemoryScanner.Scanner.FFXI.POL.MainWindowHandle, r) Then
                        stf.Location = New Point(r.Left + ((r.Right - r.Left) / 2) - (stf.Width / 2) + 60 * TrackForms.Count, r.Top + 75 + hMod)
                    Else
                        stf.Location = New Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - stf.Width / 2 + 60 * TrackForms.Count, 75 + hMod)
                    End If
                    stf.Show()
                    AddHandler stf.Disposed, AddressOf TrackForm_Disposed
                    TrackForms.Add(stf)
                End If
            End If

        End If
    End Sub

    Private Sub TrackForm_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        TrackForms.Remove(sender)
    End Sub
#End Region

#Region " WATCHER EVENTS "
    Private Sub MobWatcher_NewMobList(ByVal InMobs As MobList) Handles _mobWatcher.NewMobList
        _tData = InMobs.Item(MemoryScanner.Scanner.TargetID)
        _pData = InMobs.Item(MemoryScanner.Scanner.MyID)

        Mobs = InMobs

        If Not NoBar AndAlso My.Settings.ShowPosition AndAlso _pData IsNot Nothing Then
#If DEBUG Then
            tsPosition.Text = String.Format("X: {0} Y: {1} Z: {2} [{3}° {4}] {5:X2}", _pData.X.ToString("0.000"), _
                                               _pData.Y.ToString("0.000"), _pData.Z.ToString("0.000"), _
                                               Math.Round(RadiansToDegrees(_pData.Direction), 2), GetHeading(_pData.Direction), _pData.MobBase)
#Else
            tsPosition.Text = String.Format("X: {0} Y: {1} Z: {2} [{3}° {4}]", _pData.X.ToString("0.000"),
                                               _pData.Y.ToString("0.000"), _pData.Z.ToString("0.000"),
                                               Math.Round(RadiansToDegrees(_pData.Direction), 2), GetHeading(_pData.Direction), _pData.MobBase)
#End If

        End If


        ProcessTarget()

        If CampingMode AndAlso GlobalSettings.IsProEnabled Then
            For Each mob As MobData In Mobs.ToList
                If mob.SpawnType = SpawnTypes.Mob AndAlso ValidateCampledMob(mob.Name) Then
                    _isMobCamped = SetCampedMobStatus(mob.ServerID, mob.ID, mob.Name, (mob.HP <= 0 AndAlso mob.WarpInfo = 0), DateTime.Now, New PointF(mob.X, mob.Y))
                End If
            Next
        End If
    End Sub

    Private Sub MobWatcher_MobStatusChanged(ByVal mob As MobData, ByVal status As MobList.MobStatus) Handles _mobWatcher.MobStatusChanged
        If SpawnWatching AndAlso GlobalSettings.IsProEnabled Then
            If status = MobList.MobStatus.Alive Then
                'We have a mob that has just spawned so lets show the alert
                If SpawnList.Contains(mob.ServerID) OrElse WatchList.Contains(mob.ServerID) Then
                    'We either aren't filtering spawns or the mob was found in our spanw list
                    'so let's popup the toast form
                    MobSpawned(mob, False)
                End If
            ElseIf My.Settings.EnableOutOfRange AndAlso mob.Distance < 50.0 AndAlso status = MobList.MobStatus.OutOfRange Then
                'The mob has either moved out of range or has spawned out of range
                If SpawnList.Contains(mob.ServerID) OrElse WatchList.Contains(mob.ServerID) Then
                    MobSpawned(mob, True)
                End If
            End If
        End If
    End Sub

    Private Sub MobWatcher_ZoneChanged(ByVal LastZone As Short, ByVal NewZone As Short) Handles _mobWatcher.ZoneChanged
        ZoneTimer.Reset()
        ZoneTimer.Start()
        _currentZone = NewZone
        _zoneName = Zones.GetZoneName(NewZone)
    End Sub
#End Region

#Region " CAMPING MODE METHODS "
    Private Shared Function ValidateCampledMob(ByVal Name As String) As Boolean
        Return Name.Trim <> String.Empty AndAlso Name <> "NPC" AndAlso Name <> "Moogle" AndAlso
        Not Name.StartsWith("Door:") AndAlso Name <> "Home Point"
    End Function

    Private Function SetCampedMobStatus(ByVal MobServerID As Integer, ByVal Id As Integer, ByVal Name As String, ByVal IsDead As Boolean, ByVal ToD As DateTime?, ByVal Position As PointF) As Boolean
        Dim todString As String
        If Not IsDead Then
            todString = String.Empty
        Else
            todString = CDate(ToD).ToString("G")
        End If

        If CampedMobExists(MobServerID) Then
            Dim cMobs As CampedMob() = CampedMobManager.GetCampedMobs
            Dim cm = (From c In cMobs Where c.ServerID = MobServerID).FirstOrDefault
            If IsDead <> cm.IsDead OrElse cm.Position <> Position Then
                cm.IsDead = IsDead
                cm.DeathTime = todString
                cm.Position = Position
                CampedMobManager.ModifyCampedMobs(cm, CampedMobManager.EditType.Update)
            End If
        Else
            Dim cm As New CampedMob(Name, Id, MobServerID, _currentZone, IsDead, todString, Position)
            CampedMobManager.AddCampedMob(cm)
        End If
        Return IsDead
    End Function

    Private Shared Function CampedMobExists(ByVal MobServerID As Integer)
        Dim cMobs As CampedMob() = CampedMobManager.GetCampedMobs()
        Return (From c In cMobs Where c.ServerID = MobServerID).FirstOrDefault IsNot Nothing
    End Function
#End Region


    Private Sub tsIniGenerator_Click(sender As Object, e As EventArgs) Handles tsIniGenerator.Click
        Dim igf As New IniGeneratorForm
        igf.Show()
    End Sub

    Private Sub tsZone_Click(sender As Object, e As EventArgs) Handles tsZone.Click

    End Sub
End Class
