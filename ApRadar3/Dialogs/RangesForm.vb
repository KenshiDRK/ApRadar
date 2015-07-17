Imports RadarControls

Public Class RangesForm
#Region " MEMBER VARIABLES "
    Private _dock As DockingClass
#End Region

#Region " CONSTRUCTOR "
    Public Sub New(ByVal RadarRanges As Range())
        InitializeComponent()
        If Not RadarRanges Is Nothing Then
            For Each Range In RadarRanges
                DTRanges.Rows.Add(Range.Size, Range.RangeColor)
            Next
        End If
        dgRanges.DataSource = DTRanges
    End Sub

    Private Sub RangesForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
    End Sub
#End Region

#Region " PROPERTIES "
    Private _dtRanges As DataTable
    Private ReadOnly Property DTRanges() As DataTable
        Get
            If _dtRanges Is Nothing Then
                _dtRanges = New DataTable("Ranges")
                _dtRanges.Columns.Add("Size", GetType(Integer))
                _dtRanges.Columns.Add("Color", GetType(Color))
            End If
            Return _dtRanges
        End Get
    End Property

    Private _ranges As List(Of Range)
    Public ReadOnly Property Ranges() As List(Of Range)
        Get
            If _ranges Is Nothing Then
                _ranges = New List(Of Range)
            End If
            Return _ranges
        End Get
    End Property
#End Region

#Region " CONTROLS "
    Private Sub cmdColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdColor.Click
        Me.ColorDialog1.Color = cmdColor.BackColor
        If Me.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.cmdColor.BackColor = Me.ColorDialog1.Color
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        DTRanges.Rows.Add(Me.nuRange.Value, Me.cmdColor.BackColor)
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        For Each row As DataRow In Me.DTRanges.Rows
            Ranges.Add(New Range(row.Item("Size"), row.Item("Color")))
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
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
#End Region
End Class