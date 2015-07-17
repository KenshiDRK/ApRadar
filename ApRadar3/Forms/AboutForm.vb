Public Class AboutForm
    Private _sof As FormAnimator
    Private _dock As DockingClass

    Private Sub AboutForm_ForeColorChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.ForeColorChanged
        UpdateControlColors(Me)
    End Sub

    Private Sub AboutForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        'Roll up the form
        _sof.FadeOut(500)
    End Sub

    Private Sub AboutForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ThemeHandler.ApplyTheme(Me)
        LoadStats()
        _sof = New FormAnimator(Me)
        _dock = New DockingClass(Me) With {.UseDocking = True}
        _sof.FadeIn(500)
    End Sub

    Private Sub HeaderPanel_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles HeaderPanel.MouseDown, lblHeder.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.StartDockDrag(e.X, e.Y)
        End If
    End Sub

    Private Sub HeaderPanel_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles HeaderPanel.MouseMove, lblHeder.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
        End If
    End Sub

    Private Sub blnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles blnClose.Click
        Close()
    End Sub

    Private Sub LoadStats()
        Dim Mobs As Integer = DataLibrary.DataAccess.MobData.Mobs.Count
        Dim items As Integer = DataLibrary.DataAccess.MobData.Items.Count
        Dim pcs As Integer = DataLibrary.DataAccess.MobData.PC.Count
        Dim synths As Integer = DataLibrary.DataAccess.MobData.Synths.Count
        lblStats.Text = String.Format("{1}{0}{2}{0}{3}{0}{4}", Environment.NewLine, Mobs, pcs, items, synths)
        lblMapPack.Text = String.Format("{0}{1}{2}", My.Settings.MapPackVersion.ToShortDateString, Environment.NewLine, My.Settings.MapIniVersion.ToShortDateString)
       
        lblVersions.Text = My.Application.Info.Version.ToString
        'If My.Settings.BetaRelease Then
        '    Me.lblVersions.Text &= " BETA"
        'End If

        lblMapPack.Left = GroupBox1.Right - lblMapPack.Width
        lblStats.Left = GroupBox1.Right - lblStats.Width
        lblVersions.Left = GroupBox1.Right - lblVersions.Width
    End Sub
End Class
