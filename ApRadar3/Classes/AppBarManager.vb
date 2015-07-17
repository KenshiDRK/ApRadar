Imports System.Windows.Forms
Imports System.Runtime.InteropServices



Public Class AppBarManager
#Region " MEMBER VARIABLES "
    Private _yMin, _xMin, _xMax, _yMax As Integer
    Private _WPL As WINDOWPLACEMENT
    Private _barWidth As Integer = 20
    Private _dockMode As DockMode

    'Private _APD As APPBARDATA
    Private _screen As Rectangle
    Private _targetForm As Form
    Private _isDocked As Boolean
    Private _myX As Integer = 100
    Private _myY As Integer = 36
    Private _monitor As MonitorType
#End Region

#Region " ENUM "
    Public Enum MonitorType
        Primary
        Secondary
    End Enum
#End Region

#Region " DELEGATES "
    Public Delegate Sub BarLoadEventHandler(ByVal x As Object)
#End Region

#Region " EVENTS "
    Public Event BarLoad As BarLoadEventHandler
    Public Event DockComplete()
#End Region

#Region " CONSTRUCTOR "
    Public Sub New(ByVal TargetForm As Form, ByVal Mode As DockMode, ByVal Monitor As MonitorType, ByVal Width As Integer, ByVal Height As Integer)
        _targetForm = TargetForm

        _xMin = Width
        _yMin = Height

        _myX = Width
        _myY = Height

        _xMax = _screen.Width - _barWidth
        _yMax = _screen.Width - _barWidth
        _isDocked = False
        _dockMode = Mode
        _WPL.Length = Len(_WPL)
        GetWindowPlacement(_targetForm.Handle.ToInt32, _WPL)
        If Monitor = MonitorType.Secondary Then
            If Screen.AllScreens.Length >= 2 Then
                If Screen.AllScreens(1) Is Screen.PrimaryScreen Then
                    _screen = Screen.AllScreens(0).Bounds
                Else
                    _screen = Screen.AllScreens(1).Bounds
                End If
            Else
                _screen = Screen.PrimaryScreen.Bounds
            End If
        Else
            _screen = Screen.PrimaryScreen.Bounds
        End If
    End Sub
#End Region

#Region " PROPERTIES "
    Public WriteOnly Property Transparency() As Integer
        Set(ByVal value As Integer)
            _targetForm.Opacity = value / 100
        End Set
    End Property

    Public Property Monitor() As MonitorType
        Get
            Return _monitor
        End Get
        Set(ByVal value As MonitorType)
            If value = MonitorType.Primary Then
                _screen = Screen.PrimaryScreen.Bounds
            Else
                If Screen.AllScreens.Length >= 2 Then
                    If Screen.AllScreens(1) Is Screen.PrimaryScreen Then
                        _screen = Screen.AllScreens(0).Bounds
                    Else
                        _screen = Screen.AllScreens(1).Bounds
                    End If

                End If
            End If
            If value <> _monitor Then
                ABSetPos(_targetForm.Handle, New Size(_screen.Width, 36), DockMode)
            End If
            _monitor = value
        End Set
    End Property

    Public Property DockMode() As DockMode
        Get
            Return _dockMode
        End Get
        Set(ByVal value As DockMode)
            If value <> _dockMode Then
                If value <> _dockMode Then
                    _dockMode = value
                    ABSetPos(_targetForm.Handle, New Size(_screen.Width, 36), DockMode)
                End If
            End If
        End Set
    End Property

#End Region

#Region " FUNCTIONS "
    Public Sub InitializeAppBar(ByVal Screen As Rectangle)
        _screen = Screen
    End Sub

    Public Sub InitializeAppBar()

        Dim abd As APPBARDATA = New APPBARDATA

        abd.cbSize = Marshal.SizeOf(abd)
        abd.hWnd = _targetForm.Handle

        SHAppBarMessage(CType(ABMsg.ABM_NEW, Integer), abd)
        ABSetPos(_targetForm.Handle, New Size(_screen.Width, 36), DockMode)
        RaiseEvent DockComplete()
    End Sub

    Friend Sub ABSetPos(ByVal hWnd As IntPtr, ByVal idealSize As Size, ByVal DockEdge As ABEdge)
        Dim abd As APPBARDATA = New APPBARDATA

        abd.cbSize = Marshal.SizeOf(abd)
        abd.hWnd = hWnd
        abd.uEdge = CType(ABEdge.ABE_TOP, Integer)
        abd.uEdge = CType(DockEdge, Integer)
        If abd.uEdge = CType(ABEdge.ABE_LEFT, Integer) OrElse abd.uEdge = CType(ABEdge.ABE_RIGHT, Integer) Then
            abd.rc.top = 0
            abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height
            If abd.uEdge = CType(ABEdge.ABE_LEFT, Integer) Then
                abd.rc.left = 0
                abd.rc.right = idealSize.Width
            Else
                abd.rc.right = SystemInformation.PrimaryMonitorSize.Width
                abd.rc.left = abd.rc.right - idealSize.Width
            End If
        Else
            abd.rc.Left = _screen.Left
            abd.rc.Right = _screen.Right
            If abd.uEdge = CType(ABEdge.ABE_TOP, Integer) Then
                abd.rc.top = 0
                abd.rc.bottom = idealSize.Height
            Else
                abd.rc.Bottom = _screen.Height
                abd.rc.top = abd.rc.bottom - idealSize.Height
            End If
        End If
        SHAppBarMessage(CType(ABMsg.ABM_QUERYPOS, Integer), abd)
        Select Case abd.uEdge
            Case CType(ABEdge.ABE_LEFT, Integer)
                abd.rc.right = abd.rc.left + idealSize.Width
                ' break
            Case CType(ABEdge.ABE_RIGHT, Integer)
                abd.rc.left = abd.rc.right - idealSize.Width
                ' break
            Case CType(ABEdge.ABE_TOP, Integer)
                abd.rc.bottom = abd.rc.top + idealSize.Height
                ' break
            Case CType(ABEdge.ABE_BOTTOM, Integer)
                abd.rc.top = abd.rc.bottom - idealSize.Height
                ' break
            Case Else
                abd.rc.Bottom = abd.rc.Top + idealSize.Height
        End Select

        SHAppBarMessage(CType(ABMsg.ABM_SETPOS, Integer), abd)
        MoveWindow(abd.hWnd, abd.rc.left, abd.rc.top, abd.rc.right - abd.rc.left, abd.rc.bottom - abd.rc.top, True)
    End Sub

    Public Sub SetAutoHide()
        Dim lRet As Long
        Dim abd As APPBARDATA = New APPBARDATA
        abd.cbSize = Marshal.SizeOf(abd)
        abd.uEdge = DockMode
        abd.hWnd = _targetForm.Handle
        abd.lParam = True

        lRet = SHAppBarMessage(ABMsg.ABM_SETAUTOHIDEBAR, abd)
    End Sub

    Friend Sub UnregisterAppBar()
        Dim abd As APPBARDATA = New APPBARDATA
        abd.cbSize = Marshal.SizeOf(abd)
        abd.hWnd = _targetForm.Handle
        SHAppBarMessage(ABMsg.ABM_REMOVE, abd)
    End Sub
#End Region
End Class
