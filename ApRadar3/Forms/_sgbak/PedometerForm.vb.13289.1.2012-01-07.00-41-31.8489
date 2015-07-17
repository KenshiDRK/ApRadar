Imports FFXIMemory
Imports FFXIMemory.MemoryScanner

Public Class PedometerForm
    Private _dock As DockingClass
    Private WithEvents _watcher As Watcher
    Private _startPoint As Point3D
    Private _current As Point3D
    Private _isRunning As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub PedometerForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MemoryScanner.Scanner.DetachWatcher(_watcher)
    End Sub

    Private Sub NMListEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)
        _watcher = New Watcher(MemoryScanner.WatcherTypes.Position)
        MemoryScanner.Scanner.AttachWatcher(_watcher)
    End Sub

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        _isRunning = False
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

    Private Sub blnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles blnClose.Click
        Me.Close()
    End Sub

#Region " WATCHER EVENTS "
    Public Sub Watcher_PositionUpdated(ByVal Position As Point3D) Handles _watcher.OnPositionUpdated
        Me.lblCurrentPosition.Text = String.Format("X: {0:0.000} Y: {1:0.000}", Position.X, Position.Y)
        _current = Position
        If _isRunning Then
            CalculateDistance(Position)
        End If
    End Sub
#End Region
    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        _startPoint = _current
        Me.lblStartPosition.Text = String.Format("X: {0:0.000} Y: {1:0.000}", _startPoint.X, _startPoint.Y)
        _isRunning = True
    End Sub

    Private Sub CalculateDistance(ByVal Position As Point3D)
        Dim xDist As Single = Position.X - _startPoint.X
        Dim yDist As Single = Position.Y - _startPoint.Y
        Dim angle As Single = -Math.Atan2(yDist, xDist)
        Dim distance As Single = Math.Sqrt(xDist ^ 2 + yDist ^ 2)
        Me.lblDistance.Text = String.Format("{0:0.0} {1:0}° {2}", distance, RadiansToDegrees(angle), GetHeading(angle))
    End Sub
End Class