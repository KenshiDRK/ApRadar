Public Class IniFixDialog
#Region " MEMBER VARIABLES "
    Private _dock As DockingClass
    Private _animator As FormAnimator
#End Region

#Region " EVENTS "
    Public Event XModifierChanged(ByVal Value As Single)
    Public Event YModifierChanged(ByVal value As Single)
    Public Event SaveChanges()
#End Region

#Region " FORM ACTIONS "
    Private Sub IniFixDialog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Opacity = 1.0F
        _animator.FadeOut(500)
    End Sub

    Private Sub IniFixDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
        _animator = New FormAnimator(Me)
        '_animator.FadeIn(500)
        Me.Opacity = 0.8F
    End Sub

    Private Sub IniFixDialog_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.StartDockDrag(e.X, e.Y)
        End If
    End Sub

    Private Sub IniFixDialog_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
        End If
    End Sub
#End Region

#Region " CONTROLS "
    Private Sub xModifier_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xModifier.ValueChanged
        RaiseEvent XModifierChanged(xModifier.Value)
    End Sub

    Private Sub yModifier_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles yModifier.ValueChanged
        RaiseEvent YModifierChanged(yModifier.Value)
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        RaiseEvent SaveChanges()
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
#End Region
End Class