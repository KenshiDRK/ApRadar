Public Class SettingsForm
    Private _rSettings As RadarSettings

    Public Sub New(ByVal Settings As RadarSettings)
        _rSettings = Settings
        InitializeComponent()
    End Sub

    Private Sub SettingsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.propGrid.SelectedObject = _rSettings
    End Sub
End Class