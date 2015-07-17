Public Class MobTrackerForm
    Public Event TrackedMobChanged(ByVal Id As Integer)
    Private _zones As FFXIMemory.Zones
    Private _mobs As New List(Of FFXIMemory.ZoneMobs)

    Public Sub New(ByVal zones As FFXIMemory.Zones)
        InitializeComponent()
        _zones = zones
    End Sub

    Public WriteOnly Property ZoneID() As Integer
        Set(ByVal value As Integer)
            _mobs = _zones.GetZoneMobList(value)
            _mobs.Sort()
            Me.lbMobs.DisplayMember = "DisplayString"
            Me.lbMobs.ValueMember = "ServerId"
            Me.lbMobs.DataSource = _mobs
        End Set
    End Property

    Private Sub lbMobs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbMobs.SelectedIndexChanged
        If Me.lbMobs.SelectedIndex > -1 Then
            RaiseEvent TrackedMobChanged(Me.lbMobs.SelectedValue)
        End If
    End Sub
End Class