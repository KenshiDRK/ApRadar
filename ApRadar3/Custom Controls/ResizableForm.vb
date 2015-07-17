Public Class ResizableForm
    Inherits Form
    Private _resizeThreshold = 5
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Const HTTOP As Integer = 12
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14
    Private Const HTBOTTOM As Integer = 15
    Private Const HTBOTTOMLEFT As Integer = 16
    Private Const HTBOTTOMRIGHT As Integer = 17

    Public Sub New()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.UpdateStyles()
    End Sub

    Protected Overloads Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_NCHITTEST Then
            'decode the mouse coordinates
            Dim lp As Integer = CInt(m.LParam)
            Dim x As Integer = lp And &HFFFF
            Dim y As Integer = (lp >> 16) And &HFFFF

            'inform the o/s where the hit test landed
            If x >= Bounds.Left AndAlso x <= Bounds.Left + _resizeThreshold AndAlso y >= Bounds.Bottom - _resizeThreshold AndAlso y <= Bounds.Bottom Then
                m.Result = New IntPtr(HTBOTTOMLEFT)
            ElseIf x >= Bounds.Left AndAlso x <= Bounds.Left + _resizeThreshold AndAlso y >= Bounds.Top AndAlso y <= Bounds.Top + _resizeThreshold Then
                m.Result = New IntPtr(HTTOPRIGHT)
            ElseIf x >= Bounds.Right - _resizeThreshold AndAlso x <= Bounds.Right AndAlso y >= Bounds.Top AndAlso y <= Bounds.Top + _resizeThreshold Then
                m.Result = New IntPtr(HTTOPLEFT)
            ElseIf x >= Bounds.Right - _resizeThreshold AndAlso x <= Bounds.Right AndAlso y >= Bounds.Bottom - _resizeThreshold AndAlso y <= Bounds.Bottom Then
                m.Result = New IntPtr(HTBOTTOMRIGHT)
            ElseIf x >= Bounds.Left AndAlso x <= Bounds.Left + _resizeThreshold Then
                m.Result = New IntPtr(HTLEFT)
            ElseIf x >= Bounds.Right - _resizeThreshold AndAlso x <= Bounds.Right Then
                m.Result = New IntPtr(HTRIGHT)
            ElseIf y >= Bounds.Top AndAlso y <= Bounds.Top + _resizeThreshold Then
                m.Result = New IntPtr(HTTOP)
            ElseIf y >= Bounds.Bottom - _resizeThreshold AndAlso y <= Bounds.Bottom Then
                m.Result = New IntPtr(HTBOTTOM)
            Else
                MyBase.WndProc(m)
            End If
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Private Sub ResizableForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ResizableForm
        '
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Name = "ResizableForm"
        Me.ResumeLayout(False)

    End Sub
End Class
