Imports System
Imports System.Text
Imports System.Drawing
Imports System.Runtime.InteropServices

Public Class AppBar

#Region " CLASS VARIABLES "
    Private MyDockForm As System.Windows.Forms.Form
    Private fBarRegistered As Boolean
    Friend uCallBack As Integer
    Private PosfixTimer As New System.Timers.Timer
#End Region

#Region " STRUCTURES AND ENUM "

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure RECT
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Structure APPBARDATA
        Public cbSize As Integer
        Public hWnd As IntPtr
        Public uCallbackMessage As Integer
        Public uEdge As Integer
        Public rc As RECT
        Public lParam As Boolean
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure TaskbarState
        Public T_EDGE As ABEdge
        Public T_STATE As ABState
        Public T_SIZE As RECT
    End Structure

    Public Enum ABMsg
        ABM_NEW = 0
        ABM_REMOVE = 1
        ABM_QUERYPOS = 2
        ABM_SETPOS = 3
        ABM_GETSTATE = 4
        ABM_GETTASKBARPOS = 5
        ABM_ACTIVATE = 6
        ABM_GETAUTOHIDEBAR = 7
        ABM_SETAUTOHIDEBAR = 8
        ABM_WINDOWPOSCHANGED = 9
        ABM_SETSTATE = 10
    End Enum

    Public Enum ABState
        ABS_MANUAL = &H0
        ABS_AUTOHIDE = &H1
        ABS_ALWAYSONTOP = &H2
        ABS_AUTOHIDEANDONTOP = &H3
    End Enum

    Public Enum ABNotify
        ABN_STATECHANGE = 0
        ABN_POSCHANGED
        ABN_FULLSCREENAPP
        ABN_WINDOWARRANGE
    End Enum

    Enum ABEdge
        ABE_LEFT = 0
        ABE_TOP = 1
        ABE_RIGHT = 2
        ABE_BOTTOM = 3
    End Enum

    Public Enum AppBarDockStyle As Integer
        None
        ScreenLeft
        ScreenTop
        ScreenRight
        ScreenBottom
    End Enum

    <Serializable()>
    Public Structure ApRadarInfo
        Dim UserName As String
        Dim ExpirationDate As DateTime
        Dim Activations As Integer
    End Structure
#End Region

#Region " DLLIMPORTS "

    <DllImport("SHELL32", CallingConvention:=CallingConvention.StdCall)> _
    Shared Function SHAppBarMessage(ByVal dwMessage As Integer, ByRef pData As APPBARDATA) As Integer
    End Function

    '<DllImport("user32.dll")> _
    'Shared Function GetSystemMetrics(ByVal Index As Integer) As Integer
    'End Function

    '<DllImport("user32.dll", ExactSpelling:=True)> _
    'Public Shared Function LockWorkStation() As Boolean
    'End Function

    <DllImport("User32.dll", ExactSpelling:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function MoveWindow(ByVal hWnd As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, _
                                       ByVal repaint As Boolean) As Boolean
    End Function

    <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function RegisterWindowMessage(ByVal msg As String) As Integer
    End Function

    Private Declare Auto Function FindWindow Lib "user32" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

#End Region

#Region " PROPERTIES "

    Public ReadOnly Property IsBarRegistered() As Boolean
        Get
            Return fBarRegistered
        End Get
    End Property

    Public Property AppBarDockSize() As Size

    Private Property AppBarEdge() As ABEdge

#End Region

    'Constructor
    Friend Sub New(ByRef DockForm As System.Windows.Forms.Form, ByVal dockStyle As AppBarDockStyle, ByVal AppBarSize As Size)

        'Form Übergeben
        MyDockForm = DockForm

        'Dockstyles setzen
        UpdateDockedAppBarPosition(dockStyle)
        AppBarDockSize = AppBarSize

        'Eventhandler Initialisieren
        AddHandler MyDockForm.Closing, AddressOf OnClosing

    End Sub

    'Pos Form
    Public Sub UpdateDockedAppBarPosition(ByVal dockStyle As AppBarDockStyle)
        Select Case dockStyle
            Case AppBarDockStyle.None : Return
            Case AppBarDockStyle.ScreenLeft : AppBarEdge = ABEdge.ABE_LEFT
            Case AppBarDockStyle.ScreenTop : AppBarEdge = ABEdge.ABE_TOP
            Case AppBarDockStyle.ScreenRight : AppBarEdge = ABEdge.ABE_RIGHT
            Case AppBarDockStyle.ScreenBottom : AppBarEdge = ABEdge.ABE_BOTTOM
            Case Else : AppBarEdge = ABEdge.ABE_TOP
        End Select
    End Sub

    'Register (ABSetPos)
    Friend Sub RegisterBar(ByVal hWnd As IntPtr, ByRef uCallbackMessage As Int32, ByVal idealSize As Size, ByVal DockEdge As ABEdge)
        Dim abd As APPBARDATA = New APPBARDATA

        abd.cbSize = Marshal.SizeOf(abd)
        abd.hWnd = hWnd
        AppBarEdge = DockEdge

        uCallbackMessage = RegisterWindowMessage("AppBarMessage")
        abd.uCallbackMessage = uCallbackMessage
        Dim ret As Long = SHAppBarMessage(CType(ABMsg.ABM_NEW, Integer), abd)
        fBarRegistered = True
        ABSetPos(hWnd, idealSize, DockEdge)
    End Sub

    'UnregAppBar
    Friend Shared Sub UnregisterAppBar(ByVal hWnd As IntPtr)
        Dim abd As APPBARDATA = New APPBARDATA
        abd.cbSize = Marshal.SizeOf(abd)
        abd.hWnd = hWnd
        SHAppBarMessage(ABMsg.ABM_REMOVE, abd)
    End Sub

    'Reg Form as Appbar
    Friend Shared Sub ABSetPos(ByVal hWnd As IntPtr, ByVal idealSize As Size, ByVal DockEdge As ABEdge)
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
            abd.rc.left = 0
            abd.rc.right = SystemInformation.PrimaryMonitorSize.Width
            If abd.uEdge = CType(ABEdge.ABE_TOP, Integer) Then
                abd.rc.top = 0
                abd.rc.bottom = idealSize.Height
            Else
                abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height
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
                abd.rc.bottom = abd.rc.top + idealSize.Height
        End Select

        SHAppBarMessage(CType(ABMsg.ABM_SETPOS, Integer), abd)
        MoveWindow(abd.hWnd, abd.rc.left, abd.rc.top, abd.rc.right - abd.rc.left, abd.rc.bottom - abd.rc.top, True)
    End Sub

    Public Sub SetAutoHide()
        Dim lRet As Long
        Dim abd As APPBARDATA = New APPBARDATA
        abd.cbSize = Marshal.SizeOf(abd)
        abd.uEdge = ABEdge.ABE_TOP
        'abd.hWnd = FindWindow("Shell_TrayWnd", "") 'Tasbar Handle
        abd.hWnd = MyDockForm.Handle
        abd.lParam = True

        lRet = SHAppBarMessage(ABMsg.ABM_SETAUTOHIDEBAR, abd)
    End Sub

    Friend Function GetWinTaskbarState(ByRef WinTaskBar As TaskbarState) As Boolean
        Dim hWndAppBar As Long
        Dim ABD As New APPBARDATA
        Dim state As Long, tsize As Long
        Dim msg As New StringBuilder
        Dim position As Integer
        Dim retState As TaskbarState

        Try
            ABD.cbSize = Marshal.SizeOf(ABD)
            ABD.uEdge = 0
            ABD.hWnd = FindWindow("Shell_TrayWnd", "")
            ABD.lParam = True

            'get the appbar state  
            state = SHAppBarMessage(CType(ABMsg.ABM_GETSTATE, Integer), ABD)

            Select Case state
                Case ABState.ABS_MANUAL
                    retState.T_STATE = ABState.ABS_MANUAL
                Case ABState.ABS_ALWAYSONTOP
                    retState.T_STATE = ABState.ABS_ALWAYSONTOP
                Case ABState.ABS_AUTOHIDE
                    retState.T_STATE = ABState.ABS_AUTOHIDE
                Case ABState.ABS_AUTOHIDEANDONTOP
                    retState.T_STATE = ABState.ABS_AUTOHIDEANDONTOP
                Case Else
                    retState.T_STATE = ABState.ABS_ALWAYSONTOP
            End Select

            'see which edge has a taskbar
            For position = ABEdge.ABE_LEFT To ABEdge.ABE_BOTTOM
                ABD.uEdge = position
                hWndAppBar = SHAppBarMessage(CType(ABMsg.ABM_GETAUTOHIDEBAR, Integer), ABD)

                If hWndAppBar > 0 Then
                    Select Case position
                        Case ABEdge.ABE_LEFT : retState.T_EDGE = ABEdge.ABE_LEFT
                        Case ABEdge.ABE_TOP : retState.T_EDGE = ABEdge.ABE_TOP
                        Case ABEdge.ABE_RIGHT : retState.T_EDGE = ABEdge.ABE_RIGHT
                        Case ABEdge.ABE_BOTTOM : retState.T_EDGE = ABEdge.ABE_BOTTOM
                        Case Else : retState.T_EDGE = ABEdge.ABE_TOP
                    End Select
                End If
            Next

            tsize = SHAppBarMessage(CType(ABMsg.ABM_GETTASKBARPOS, Integer), ABD)
            retState.T_SIZE = ABD.rc

            'Rückgabe ob sich der TaskbarStatus verändert hat
            WinTaskBar = retState
            Return True

        Catch ex As Exception
            Trace.Assert(False)
            Return False
        End Try
    End Function

    'Eventhandler Form Close
    Private Overloads Sub OnClosing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        UnregisterAppBar(MyDockForm.Handle)
    End Sub
End Class