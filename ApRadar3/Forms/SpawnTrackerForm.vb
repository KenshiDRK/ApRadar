Imports FFXIMemory
Imports RadarControls
Imports System.Drawing.Drawing2D

Public Class SpawnTrackerForm
    Inherits LayeredForm

    Private _myData As MobData
    Friend Property MyData As MobData
        Get
            Return _myData
        End Get
        Set(ByVal value As MobData)
            _myData = value
            Invalidate()
        End Set
    End Property
    Public Property MobServerID() As Integer

    Private _trackMob As MobData
    Friend Property TrackMob As MobData
        Get
            Return _trackMob
        End Get
        Set(ByVal value As MobData)
            _trackMob = value
            Invalidate()
        End Set
    End Property

    Public Sub New(ByVal MobBase As Integer, ByVal MineBase As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        'SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        'UpdateStyles()
        ' Add any initialization after the InitializeComponent() call.
        TrackMob = New MobData(MemoryScanner.Scanner.FFXI.POL, MobBase, False)
        MobServerID = TrackMob.ServerID
        MyData = New MobData(MemoryScanner.Scanner.FFXI.POL, MineBase, False)
    End Sub

    Private Sub PaintMobTracker(ByVal g As Graphics)
        If Not TrackMob Is Nothing AndAlso Not MyData Is Nothing Then
            Try
                'Set the drawing methods
                g.SmoothingMode = SmoothingMode.HighQuality
                
                'Store location data so as to not have so many readprocessmemory calls
                Dim myLoc As New PointF(MyData.X, MyData.Y)
                Dim mobLoc As New PointF(TrackMob.X, TrackMob.Y)

                'Get the display text
                Dim name As String = String.Format("{0:X} - {1} {2:0.0}", TrackMob.ID, TrackMob.Name, Math.Round(Math.Sqrt((myLoc.X - mobLoc.X) ^ 2 + (myLoc.Y - mobLoc.Y) ^ 2), 1))

                If TrackMob.HP < 1 AndAlso TrackMob.PIcon = 16 Then
                    name &= " (Out Of Range}"
                    'We are dealing with a mob that has spawned out of range
                    'Dim centerPoint As New PointF(Width / 2, 100)
                    'Using f As New Font("Calibri", 50, FontStyle.Bold)
                    '    Dim size As SizeF = g.MeasureString("?", f)
                    '    Using lgb As New LinearGradientBrush(New Point(-20, 0), New Point(20, 0), Color.LimeGreen, Color.Green)
                    '        g.DrawString("?", f, lgb, -(size.Width / 2), -(size.Height / 2))
                    '    End Using
                    'End Using
                Else
                    If TrackMob.HP < 1 Then
                        name &= " (Dead)"
                    End If
                End If
                'The mob is either in range and dead or is alive
                'Calculate my direction
                Dim myDirection As Double = -(MyData.Direction * (180 / Math.PI) - 90)
                'Calculate the mobs direction from me
                Dim direction As Double = Math.Atan2(myLoc.Y - mobLoc.Y, myLoc.X - mobLoc.X) * (180 / Math.PI)
                'Get the center point for rotation
                Dim centerPoint As New PointF(Width / 2, 100)
                'Rotate the painting surface for painting
                g.TranslateTransform(centerPoint.X, centerPoint.Y, MatrixOrder.Prepend)
                g.RotateTransform(myDirection - direction)
                'Paint the arrow

                DrawArrow(g)
                'Reset the rotation so as not to affect the text painting
                g.ResetTransform()
                'End If

                'Measure the string for proper placement
                Dim s As SizeF = g.MeasureString(name, Font)
                'Create the brush for painting the name text
                'Purple = claimed by someone else
                'Green  = claimed by me
                'Red    = unclaimed
                Using nameBrush As New SolidBrush(Color.Purple)
                    If TrackMob.ClaimedBy = 0 Then
                        nameBrush.Color = Color.Red
                    ElseIf TrackMob.ClaimedBy = MyData.ServerID Then
                        nameBrush.Color = Color.LimeGreen
                    End If
                    DrawOutlineString(g, name, Color.Black, Width / 2 - (s.Width / 2), _
                                    Height - s.Height - 2, 5)
                    g.DrawString(name, Font, nameBrush, Width / 2 - (s.Width / 2), Height - s.Height - 2)
                End Using
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub DrawOutlineString(ByVal g As Graphics, ByVal s As String, ByVal c As Color, ByVal x As Single, ByVal y As Single, ByVal size As Integer)
        Dim tan As Single = size / 2
        Using b As New SolidBrush(Color.FromArgb(15, c))
            For bx As Integer = 0 To size
                For by As Integer = 0 To size
                    g.DrawString(s, Me.Font, b, x - tan + bx, y - tan + by)
                Next
            Next
        End Using
    End Sub

    Private Sub DrawArrow(ByRef g As Graphics)
        Dim pts() As Point = { _
            New Point(-20, -10), _
            New Point(0, -10), _
            New Point(0, -20), _
            New Point(20, 0), _
            New Point(0, 20), _
            New Point(0, 10), _
            New Point(-20, 10) _
        }
        Dim c1 As Color = Color.Green
        Dim c2 As Color = Color.LimeGreen
        If TrackMob.HP < 1 Then
            c1 = Color.DarkRed
            c2 = Color.LightPink
        End If
        Using lgb As New LinearGradientBrush(New Point(-20, 0), New Point(20, 0), c1, c2)
            g.FillPolygon(lgb, pts)
        End Using
        g.DrawPolygon(New Pen(c1, 2), pts)
    End Sub

    Private Sub SpawnTrackerForm_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Try
            PaintMobTracker(e.Graphics)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MonitorTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MonitorTimer.Tick
        Invalidate()
    End Sub

    Private Sub SpawnTrackerForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not MyData Is Nothing AndAlso Not TrackMob Is Nothing Then
            MonitorTimer.Start()
        End If
        Me.BringToFront()
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Dispose()
    End Sub

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80
            Return cp
        End Get
    End Property
End Class