Public Class VersionUpdateDialog
    Public Sub New(ByVal CurrentVersion As String, ByVal AvailableVersion As String)
        InitializeComponent()
        Me.lblVersions.Text = String.Format("{0}{1}{2}", CurrentVersion, Environment.NewLine, AvailableVersion)
        Me.lblVersions.Left = Me.cmdCancel.Right - Me.lblVersions.Width
    End Sub

    Private Sub VersionUpdateDialog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
    End Sub
End Class