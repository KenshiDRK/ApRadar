Imports FFXIMemory
'Imports RadarControls
Imports DataLibrary
Imports DataLibrary.CampedMobsDataset

Public Class TodBrowserForm
#Region " MEMBER VAEIABLES "
    Private _path As String = "./Data/CampedMobs.xml"
    Private _dock As DockingClass
    Private _animator As FormAnimator
    Private _zone As Integer = 0
#End Region

#Region " CONSTRUCTOR "
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal Zone As Integer)
        InitializeComponent()
        Me.dgTOD.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        _zone = Zone
    End Sub
#End Region

#Region " PROPERTIES "
    Private _zones As Zones
    Public ReadOnly Property Zones() As Zones
        Get
            If _zones Is Nothing Then
                _zones = New Zones
            End If
            Return _zones
        End Get
    End Property

    'Private _campedMobs As List(Of CampedMobs)
    'Private ReadOnly Property CampedMobList() As List(Of CampedMobs)
    '    Get
    '        _campedMobs = Utilities.Serializer.DeserializeFromXml(Of List(Of CampedMobs))(_path)
    '        If _campedMobs Is Nothing Then
    '            _campedMobs = New List(Of CampedMobs)
    '        End If
    '        Return _campedMobs
    '    End Get
    'End Property
#End Region

#Region " FORM ACTIONS "

    Private Sub TodBrowserForm_ForeColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ForeColorChanged
        UpdateControlColors(Me)
    End Sub

    Private Sub TodBrowserForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        _animator.FadeOut(150)
    End Sub

    Private Sub TodBrowserForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
        _dock.DockedLocation.Add(DockingClass.DockPosition.Top)
        LoadZones()
        _animator = New FormAnimator(Me)
        If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
            _animator.SlideOut(150, SlideDirection.Down)
        Else
            _animator.SlideOut(150, SlideDirection.Up)
        End If
        If _zone > 0 Then
            Me.cboZones.SelectedValue = _zone
        End If
        Me.dgTOD.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
    End Sub
#End Region

#Region " CONTROLS "
    Private Sub cboZones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZones.SelectedIndexChanged
        LoadMobs()
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rbDead_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDead.CheckedChanged, rbAll.CheckedChanged
        LoadMobs()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        LoadMobs()
    End Sub

    Private Sub DataGridView1_CellParsing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellParsingEventArgs) Handles dgTOD.CellParsing
        If e.ColumnIndex = 5 Then

        End If
    End Sub
#End Region

#Region " PRIVATE MEMBERS "
    Private Sub LoadZones()
        Me.cboZones.BeginUpdate()
        Me.cboZones.DisplayMember = "ZoneName"
        Me.cboZones.ValueMember = "ZoneID"
        Me.cboZones.DataSource = Zones.ZoneList
        Me.cboZones.SelectedIndex = -1
        Me.cboZones.EndUpdate()

    End Sub

    Private Sub LoadMobs()
        If Me.cboZones.SelectedIndex > -1 Then

            If rbAll.Checked Then
                Me.CmBinding.DataSource = (From c In CampedMobManager.GetCampedMobs Where c.Zone = Me.cboZones.SelectedValue).ToArray
            Else
                Me.CmBinding.DataSource = (From c In CampedMobManager.GetCampedMobs Where c.Zone = Me.cboZones.SelectedValue AndAlso _
                                              c.IsDead = True AndAlso c.DeathTime <> String.Empty).ToArray
            End If
        End If
    End Sub
#End Region


End Class