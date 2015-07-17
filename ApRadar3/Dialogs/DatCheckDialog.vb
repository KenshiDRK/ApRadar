Public Class DatCheckDialog
    Inherits ResizableForm

    Private _dock As DockingClass
    Private _mobs As List(Of DatChecker.Mobs)
    Private _isScan As Boolean

    Public Sub New()
        InitializeComponent()
        Me.dgMobsToAdd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        
    End Sub

    Private WithEvents _dc As DatChecker
    Private Sub cmdScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdScan.Click
        _dc = New DatChecker
        _dc.CheckDats()
        cmdScan.Enabled = False
        cmdAddMobs.Enabled = False
        _isScan = True
    End Sub

    Private Sub _dc_ProgressChanged(ByVal Progress As Integer) Handles _dc.ProgressChanged
        pbProgress.Value = Progress
        Dim lblFormat As String
        If _isScan Then
            lblFormat = "Scanning Dats {0}% complete..."
        Else
            lblFormat = "Adding Mobs {0}% complete..."
        End If
        lblResult.Text = String.Format(lblFormat, Progress)
    End Sub

    Private Sub _dc_CheckCompleted(ByVal Mobs As List(Of DatChecker.Mobs)) Handles _dc.CheckComplete
        _mobs = Mobs

        If Not Mobs Is Nothing Then
            dgMobsToAdd.DataSource = _mobs.ToArray()
            If Mobs.Count > 0 Then
                If _isScan Then
                    cmdAddMobs.Enabled = True
                End If
                cmdScan.Enabled = True
                lblResult.Text = String.Format("{0} new mobs found.", Mobs.Count)
            Else
                MessageBox.Show("No new Mobs found")
            End If
        Else
            dgMobsToAdd.DataSource = Nothing
        End If
    End Sub

    Private Sub _dc_ErrorOccured(ByVal ex As Exception) Handles _dc.ErrorOccured
        MessageBox.Show(ex.Message)
    End Sub

    Private Sub cmdAddMobs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddMobs.Click
        _dc.AddNewMobs(_mobs)

        cmdScan.Enabled = False
        cmdAddMobs.Enabled = False
        _isScan = False
    End Sub

    Private Sub DatCheckDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dgMobsToAdd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        _dock = New DockingClass(Me)
        ThemeHandler.ApplyTheme(Me)
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
        Close()
    End Sub
End Class