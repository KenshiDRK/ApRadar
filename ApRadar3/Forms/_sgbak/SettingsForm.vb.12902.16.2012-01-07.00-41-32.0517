Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Public Class SettingsForm
    Private _isLoadignSettings As Boolean = True
    Private _dock As DockingClass
    Private _animator As FormAnimator
    Private _defaultAlerts As String() = {"[Custom]", "Asterik", "Beep", "Exclamation", "Hand"}

    Private _themes As DataTable
    Private ReadOnly Property Themes() As DataTable
        Get
            If _themes Is Nothing Then
                _themes = New DataTable
                _themes.Columns.Add("Value", GetType(String))
                _themes.Columns.Add("Display", GetType(String))
            End If
            Return _themes
        End Get
    End Property

    Public Event SettingsChanged()


    Private Sub SettingsForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        _animator.FadeOut(300)
    End Sub

    Private Sub SettingsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)

        _animator = New FormAnimator(Me)

        If NoBar Then Me.tpApBar.Hide()

        LoadThemes()

        cboAlertSound.Items.AddRange(_defaultAlerts)

        cboTheme.SelectedValue = My.Settings.CurrentTheme

        txtRefreshRate.Text = My.Settings.RefreshRate

        cboServer.DataSource = [Enum].GetValues(GetType(Servers))
        cboTextRendering.DataSource = [Enum].GetValues(GetType(TextRenderingHint))
        cboSmoothingMode.DataSource = [Enum].GetValues(GetType(SmoothingMode))
        cboCompositingQuality.DataSource = [Enum].GetValues(GetType(CompositingQuality))
        ckAppUpdates.Checked = My.Settings.CheckAppUpdates
        ckMapUpdates.Checked = My.Settings.CheckMapUpdates
        ckBeta.Checked = My.Settings.CheckForBetaReleases
        cboServer.SelectedItem = [Enum].Parse(GetType(Servers), My.Settings.Server)
        cboTextRendering.SelectedItem = My.Settings.TextRendering
        cboSmoothingMode.SelectedItem = My.Settings.SmoothingMode
        cboCompositingQuality.SelectedItem = My.Settings.CompositingQuality
        ckAutoSaveSettings.Checked = My.Settings.AutoSaveRadarSettings
        ckAutoHide.Checked = My.Settings.AutoHide
        ckShowClock.Checked = My.Settings.ShowClock
        ckShowPosition.Checked = My.Settings.ShowPosition
        ckShowZone.Checked = My.Settings.ShowZoneTimer
        cboMonitor.SelectedItem = My.Settings.Monitor
        ckHideAltTab.Checked = My.Settings.HideAltTab

        ckNewsUpdates.Checked = My.Settings.AutomaticNewsUpdates
        nuNewsInterval.Value = My.Settings.NewsCheckInterval
        nuMaxNews.Value = My.Settings.MaxNewsItems

        ckSupressToastForm.Checked = My.Settings.SupressToast
        ckShowTracker.Checked = My.Settings.ShowTracker
        ckEnableOutOfRange.Checked = My.Settings.EnableOutOfRange
        ckPlayAlert.Checked = My.Settings.PlayAlertOnSpawn

        If _defaultAlerts.Contains(My.Settings.AlertSound) Then
            cboAlertSound.SelectedItem = My.Settings.AlertSound
        Else
            cboAlertSound.SelectedIndex = 0
            txtCustomSound.Text = My.Settings.AlertSound
        End If

        If Screen.AllScreens.Count < 2 Then
            cboMonitor.Enabled = False
        End If
        If GlobalSettings.IsProEnabled Then
            ckCampingMode.Checked = My.Settings.CampingMode
            ckSpawnAlerts.Checked = My.Settings.StartSpawnAlerts
            ckUseTimeout.Checked = My.Settings.UseAlertTimeout
        End If
        _isLoadignSettings = False
        _animator.FadeIn(300)
    End Sub

    Private Sub Settings_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckAppUpdates.CheckedChanged, ckMapUpdates.CheckedChanged, cboCompositingQuality.SelectedValueChanged, cboSmoothingMode.SelectedValueChanged, cboTextRendering.SelectedValueChanged, ckAutoHide.CheckedChanged, cboServer.SelectedValueChanged, ckShowZone.CheckedChanged, ckShowPosition.CheckedChanged, ckShowClock.CheckedChanged, cboMonitor.SelectedIndexChanged, cboTheme.SelectedValueChanged, ckAutoSaveSettings.CheckedChanged, ckCampingMode.CheckedChanged, ckCampingMode.CheckedChanged, ckHideAltTab.CheckedChanged, ckHideAltTab.CheckedChanged, ckNewsUpdates.CheckedChanged, nuMaxNews.ValueChanged, ckSpawnAlerts.CheckedChanged, ckUseTimeout.CheckedChanged, cboAlertSound.SelectedIndexChanged, ckSupressToastForm.CheckedChanged, ckShowTracker.CheckedChanged, ckEnableOutOfRange.CheckedChanged, ckPlayAlert.CheckedChanged, txtRefreshRate.TextChanged, ckBeta.CheckedChanged
        If Not _isLoadignSettings Then
            cmdApply.Enabled = True
        End If
    End Sub

    Private Sub HeaderPanel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseDown, lblHeder.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.StartDockDrag(e.X, e.Y)
        End If
    End Sub

    Private Sub HeaderPanel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseMove, lblHeder.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
        End If
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        SaveSettings()
        Close()
    End Sub

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        SaveSettings()
        cmdApply.Enabled = False
    End Sub

    Private Sub SaveSettings()
        My.Settings.AutoHide = ckAutoHide.Checked
        My.Settings.CheckAppUpdates = ckAppUpdates.Checked
        My.Settings.CheckForBetaReleases = ckBeta.Checked
        My.Settings.CheckMapUpdates = ckAppUpdates.Checked
        My.Settings.Server = cboServer.SelectedItem.ToString
        If cboCompositingQuality.SelectedValue = CompositingQuality.Invalid Then
            cboCompositingQuality.SelectedItem = My.Settings.CompositingQuality
        Else
            My.Settings.CompositingQuality = cboCompositingQuality.SelectedValue
        End If
        My.Settings.TextRendering = cboTextRendering.SelectedValue
        If cboSmoothingMode.SelectedValue = SmoothingMode.Invalid Then
            cboSmoothingMode.SelectedItem = My.Settings.SmoothingMode
        Else
            My.Settings.SmoothingMode = cboSmoothingMode.SelectedValue
        End If
        My.Settings.ShowClock = ckShowClock.Checked
        My.Settings.ShowPosition = ckShowPosition.Checked
        My.Settings.ShowZoneTimer = ckShowZone.Checked
        My.Settings.Monitor = cboMonitor.Text
        My.Settings.AutoSaveRadarSettings = ckAutoSaveSettings.Checked
        My.Settings.HideAltTab = ckHideAltTab.Checked

        My.Settings.AutomaticNewsUpdates = ckNewsUpdates.Checked
        My.Settings.NewsCheckInterval = nuNewsInterval.Value
        My.Settings.MaxNewsItems = nuMaxNews.Value
        My.Settings.ShowTracker = ckShowTracker.Checked
        My.Settings.SupressToast = ckSupressToastForm.Checked
        My.Settings.EnableOutOfRange = ckEnableOutOfRange.Checked
        My.Settings.PlayAlertOnSpawn = ckPlayAlert.Checked

        If cboAlertSound.SelectedIndex = 0 Then
            My.Settings.AlertSound = txtCustomSound.Text
        Else
            My.Settings.AlertSound = cboAlertSound.Text
        End If

        If Integer.TryParse(Me.txtRefreshRate.Text, New Integer) Then
            My.Settings.RefreshRate = CInt(Me.txtRefreshRate.Text)
        Else
            My.Settings.RefreshRate = 100
        End If

        If GlobalSettings.IsProEnabled Then
            My.Settings.CampingMode = ckCampingMode.Checked
            My.Settings.StartSpawnAlerts = ckSpawnAlerts.Checked
            My.Settings.UseAlertTimeout = ckUseTimeout.Checked
        Else
            My.Settings.CampingMode = False
            My.Settings.StartSpawnAlerts = False
        End If
        ThemeHandler.LoadTheme(cboTheme.SelectedValue)
        My.Settings.Save()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Close()
    End Sub

    Private Sub LoadThemes()
        Dim themeList As New List(Of String)(IO.Directory.GetDirectories(Application.StartupPath & "\Themes"))
        themeList.Sort()
        Themes.Rows.Clear()
        For Each Theme As String In themeList
            Themes.Rows.Add(Theme & "\", Theme.Substring(Theme.LastIndexOf(IO.Path.DirectorySeparatorChar) + 1))
        Next
        cboTheme.DisplayMember = "Display"
        cboTheme.ValueMember = "Value"
        cboTheme.DataSource = Themes

    End Sub

    Private Sub nuNewsInterval_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nuNewsInterval.ValueChanged
        If Not _isLoadignSettings Then
            cmdApply.Enabled = True
        End If
    End Sub

    Private Sub cboAlertSound_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAlertSound.SelectedIndexChanged
        txtCustomSound.Enabled = cboAlertSound.SelectedIndex = 0
        cmdBrowseAudio.Enabled = cboAlertSound.SelectedIndex = 0
    End Sub

    Private Sub cmdBrowseAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseAudio.Click
        Using ofd As New OpenFileDialog() With {.Filter = "WAV Files (*.wav) | *.wav"}
            If ofd.ShowDialog = DialogResult.OK Then
                txtCustomSound.Text = ofd.FileName
            End If
        End Using
    End Sub

    Private Sub cmdPlayAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlayAudio.Click
        If cboAlertSound.SelectedIndex = 0 Then
            If IO.File.Exists(txtCustomSound.Text) Then
                'My.Computer.Audio.Play(Me.txtCustomSound.Text, AudioPlayMode.Background)
                AudioManager.PlaySound(txtCustomSound.Text)
            Else
                MessageBox.Show("The specified file was not found.", "File not found")
            End If
        Else
            AudioManager.PlaySystemSound(cboAlertSound.Text)
        End If
    End Sub


End Class