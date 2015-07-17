Public Class ToastForm
    Private _animator As FormAnimator
    Private _isLoading As Boolean = True
    Private _playAlert As Boolean = False
    Private _persistent As Boolean = False

    Public Property Persistent() As Boolean
        Get
            Return _persistent
        End Get
        Set(ByVal value As Boolean)
            _persistent = value
        End Set
    End Property

    Public Sub New(ByVal PlayAlert As Boolean)
        InitializeComponent()
        _playAlert = PlayAlert
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ToastForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        _animator.RollUp(200, SlideDirection.Up)
        RemoveToastForm(Me)
    End Sub

    Private Sub ToastForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
        UpdateControlColors(Me)
        _animator = New FormAnimator(Me)
        _animator.SlideOut(200, SlideDirection.Up)
        If Not Persistent Then
            hideTimer.Start()
        End If
        If _playAlert Then
            AudioManager.PlayAlert(My.Settings.AlertSound)
        End If

        _isLoading = False
    End Sub

    Private Sub hideTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hideTimer.Tick
        hideTimer.Stop()
        Me.Close()
    End Sub

    Private Sub ToastForm_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged
        If Not _isLoading Then _animator.MoveDown(200)
    End Sub

#Region " OVERRIDEN METHODS "
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80
            Return cp
        End Get
    End Property
#End Region

    Private Sub lblMessage_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblMessage.Resize
        lnkSomeLink.Top = lblMessage.Bottom
    End Sub

    Private Sub btnClose_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.MouseHover
        Me.btnClose.Image = My.Resources.Close16x16_Hover
    End Sub

    Private Sub btnClose_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.MouseLeave
        Me.btnClose.Image = My.Resources.Close16x16
    End Sub
End Class