Imports FFXIMemory
Imports FFXIMemory.MemoryScanner

Public Class IniGeneratorForm

    Private _dock As DockingClass
    Private WithEvents _watcher As Watcher
    Private _isLoading As Boolean = True

    Private _mapController As RadarControls.MapHandler
    Private ReadOnly Property MapController As RadarControls.MapHandler
        Get
            If _mapController Is Nothing Then
                _mapController = New RadarControls.MapHandler
            End If
            Return _mapController
        End Get
    End Property

    Private Sub IniGeneratorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)
        _watcher = New Watcher(WatcherTypes.Position)
        MemoryScanner.Scanner.AttachWatcher(_watcher)
        Me.cboType.SelectedIndex = 0
        LoadZones()
        _isLoading = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles blnClose.Click
        Me.Close()
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

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        txtMapX.Text = e.X.ToString()
        txtMapY.Text = e.Y.ToString()
    End Sub

    Private Sub PosChanged(ByVal position As Point3D) Handles _watcher.OnPositionUpdated
        txtX.Text = position.X.ToString("0.000")
        txtY.Text = position.Y.ToString("0.000")
        txtZ.Text = position.Z.ToString("0.000")
    End Sub

    Private Sub LoadZones()
        Dim z As New Zones()
        Me.cboZone.DisplayMember = "ZoneName"
        Me.cboZone.ValueMember = "ZoneID"
        Me.cboZone.DataSource = z.ZoneList.ToArray()
        Me.cboZone.SelectedIndex = -1
    End Sub

    Private Sub cboZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboZone.SelectedIndexChanged
        If Not _isLoading Then
            'Dim floors = MapController.MapList.Where(Function(c) c.Map = cboZone.SelectedValue)
            Dim floors = IO.Directory.GetFiles(MapController.MapsPath, CInt(cboZone.SelectedValue).ToString("x") & "_*")
            Me.cboFloor.BeginUpdate()
            Me.cboFloor.Items.Clear()
            For i = 0 To floors.Count - 1
                Me.cboFloor.Items.Add(i)
            Next
            Me.cboFloor.EndUpdate()
            If (cboFloor.Items.Count > 0) Then
                Me.cboFloor.SelectedIndex = 0
            End If
            LoadMap()
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Try
            Dim modifier As Double
            Select Case cboType.SelectedIndex
                Case 0
                    modifier = 0.1
                Case 1
                    modifier = 0.2
                Case 2
                    modifier = 0.4
            End Select

            txtResult.Text = String.Format("{0:x2}_{1}={2},{3:0.0},-{2},{4:0.0},{5},{6},{7},{8},{9},{10}",
                                           Me.cboZone.SelectedValue,
                                           Me.cboFloor.Text,
                                           modifier,
                                           CDbl(txtMapX.Text) / 2 - (modifier * CDbl(txtX.Text)),
                                           CDbl(txtMapY.Text) / 2 + (modifier * CDbl(txtY.Text)),
                                           txtXMin.Text, txtZMin.Text, txtYMin.Text,
                                           txtXMax.Text, txtZMax.Text, txtYMax.Text)

        Catch ex As Exception
            txtResult.Text = "Error: " + ex.Message
        End Try
    End Sub

    Private Sub cboFloor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFloor.SelectedIndexChanged
        LoadMap()
    End Sub

    Private Sub LoadMap()
        Try
            Dim map As String = String.Format("{0}\{1:x2}_{2}.gif", MapController.MapsPath, cboZone.SelectedValue, IIf(cboFloor.Text <> "", cboFloor.Text, "0"))
            Me.PictureBox1.Load(map)
        Catch ex As Exception
            txtResult.Text = "Error: Unable to load the map. " + ex.Message
        End Try
    End Sub
End Class