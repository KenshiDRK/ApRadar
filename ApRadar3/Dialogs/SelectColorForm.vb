Public Class SelectColorForm

    Private _dock As DockingClass

    Friend Property NPCColor As Color
    Friend Property MobColor As Color
    Friend Property NMColor As Color
    Friend Property CampedColor As Color
    Friend Property LinkColor As Color
    Friend Property PCColor As Color
    Friend Property PartyColor As Color
    Friend Property AllianceColor As Color
    Friend Property TargetHighlight As Color

    Public Event DataSaved(ByVal Sender As SelectColorForm)

    Public Sub New(ByVal ParentName As String)
        InitializeComponent()
        Me.lblHeder.Text &= " " & ParentName
        Me.Text = Me.lblHeder.Text
    End Sub

    Public WriteOnly Property BaseSettings As RadarControls.RadarSettings
        Set(ByVal value As RadarControls.RadarSettings)
            Me.NPCColor = value.NPCColor
            Me.btnNPC.BackColor = Me.NPCColor

            Me.MobColor = value.MobColor
            Me.btnMobs.BackColor = Me.MobColor

            Me.NMColor = value.NMColor
            Me.btnNM.BackColor = Me.NMColor

            Me.CampedColor = value.CampedColor
            Me.btnCamped.BackColor = Me.CampedColor

            Me.LinkColor = value.LinkColor
            Me.btnLink.BackColor = Me.LinkColor

            Me.PCColor = value.PCColor
            Me.btnPC.BackColor = Me.PCColor

            Me.PartyColor = value.PartyColor
            Me.btnParty.BackColor = Me.PartyColor

            Me.AllianceColor = value.AllianceColor
            Me.btnAlliance.BackColor = Me.AllianceColor

            Me.TargetHighlight = value.TargetHighlightColor
            Me.btnTargetHighlight.BackColor = Me.TargetHighlight
        End Set
    End Property

    Private Sub SelectColorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)

        Me.BackColor = ThemeHandler.FormBackgroundColor
        Me.ForeColor = ThemeHandler.FormForeColor
        Me.HeaderPanel.BackgroundImage = ThemeHandler.HeaderImage

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

    Private Sub btnNPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNPC.Click
        Using cd As New ColorDialog With {.Color = Me.NPCColor}
            If cd.ShowDialog = DialogResult.OK Then
                Me.NPCColor = cd.Color
                sender.BackColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub btnMobs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMobs.Click
        Using cd As New ColorDialog With {.Color = Me.MobColor}
            If cd.ShowDialog = DialogResult.OK Then
                Me.MobColor = cd.Color
                sender.BackColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub btnNM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNM.Click
        Using cd As New ColorDialog With {.Color = Me.NMColor}
            If cd.ShowDialog = DialogResult.OK Then
                Me.NMColor = cd.Color
                sender.BackColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub btnCamped_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCamped.Click
        Using cd As New ColorDialog With {.Color = Me.CampedColor}
            If cd.ShowDialog = DialogResult.OK Then
                Me.CampedColor = cd.Color
                sender.BackColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub btnLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLink.Click
        Using cd As New ColorDialog With {.Color = Me.LinkColor}
            If cd.ShowDialog = DialogResult.OK Then
                Me.LinkColor = cd.Color
                sender.BackColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub btnPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPC.Click
        Using cd As New ColorDialog With {.Color = Me.PCColor}
            If cd.ShowDialog = DialogResult.OK Then
                Me.PCColor = cd.Color
                sender.BackColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub btnParty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParty.Click
        Using cd As New ColorDialog With {.Color = Me.PartyColor}
            If cd.ShowDialog = DialogResult.OK Then
                Me.PartyColor = cd.Color
                sender.BackColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub btnAlliance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlliance.Click
        Using cd As New ColorDialog With {.Color = Me.AllianceColor}
            If cd.ShowDialog = DialogResult.OK Then
                Me.AllianceColor = cd.Color
                sender.BackColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub btnTargetHighlight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTargetHighlight.Click
        Using cd As New ColorDialog With {.Color = Me.TargetHighlight}
            If cd.ShowDialog = DialogResult.OK Then
                Me.TargetHighlight = cd.Color
                sender.BackColor = cd.Color
            End If
        End Using
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        RaiseEvent DataSaved(Me)
        Me.Dispose()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Dispose()
    End Sub

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        RaiseEvent DataSaved(Me)
    End Sub


End Class