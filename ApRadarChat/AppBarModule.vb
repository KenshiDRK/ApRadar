Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Public Enum DockMode As Integer
    Left = &H0S
    Top = &H1S
    Right = &H2S
    Bottom = &H3S
    None = -1
End Enum

Module AppBarModule
    Public Const WM_MOUSEHOVER = &H2A1
    Public Const SHGFI_ICON = &H100
    Public Const SHGFI_SMALLICON = &H1    ' : Small icon
    Public Const SHGFI_LARGEICON = &H0    ' : Large icon

    Public Const HWND_TOPMOST As Short = -1
    Public Const SWP_NOSIZE As Short = &H1S
    Public Const SWP_NOMOVE As Short = &H2S

    Public Const DFC_BUTTON As Short = 4
    Public Const DFCS_BUTTON3STATE As Short = &H10S

    Public Const HWND_NOTOPMOST = -2
    Public Const SPI_GETWORKAREA = 48
    Public Const WM_NCHITTEST = &H84
    Public Const FLASHW_CAPTION As Int32 = &H1
    Public Const FLASHW_TRAY As Int32 = &H2
    Public Const FLASHW_ALL As Int32 = (FLASHW_CAPTION Or FLASHW_TRAY)
    Public Const WS_EX_TOOLWINDOW = &H80L
    Public Const WS_EX_APPWINDOW = &H40000L


#Region " STRUCTURES "
    Public Structure SHFILEINFO
        Public hIcon As IntPtr            ' : icon
        Public iIcon As Integer           ' : icondex
        Public dwAttributes As Integer    ' : SFGAO_ flags
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Public Structure POINTAPI
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Public Structure WINDOWPLACEMENT
        Dim Length As Integer
        Dim flags As Integer
        Dim ShowCmd As Integer
        Dim ptMinPosition As POINTAPI
        Dim ptMaxPosition As POINTAPI
        Dim rcNormalPosition As RECT
    End Structure

    Public Structure APPBARDATA
        Dim cbSize As Integer
        Dim hWnd As IntPtr
        Dim uCallbackMessage As Integer
        Dim uEdge As Integer
        Dim rc As RECT
        Dim lParam As IntPtr
    End Structure

    Public Structure FLASHWINFO
        Public cbSize As Int32
        Public hwnd As IntPtr
        Public dwFlags As Int32
        Public uCount As Int32
        Public dwTimeout As Int32
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure TaskbarState
        Public T_EDGE As ABEdge
        Public T_STATE As ABState
        Public T_SIZE As RECT
    End Structure

#End Region

#Region " ENUM "
    Public Enum GWL As Integer
        ExStyle = -20
    End Enum

    Public Enum WS_EX As Integer
        Transparent = &H20
        Layered = &H80000
    End Enum

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
#End Region

#Region " API "
    <DllImport("User32")> _
    Public Function GetWindowPlacement(ByVal hWnd As Integer, ByRef lpwndpl As WINDOWPLACEMENT) As Integer
    End Function

    <DllImport("User32")> _
    Public Function DrawFrameControl(ByVal hdc As Integer, ByRef lpRect As RECT, ByVal un1 As Integer, ByVal un2 As Integer) As Integer
    End Function

    <DllImport("User32")> _
    Public Function SetRect(ByRef lpRect As RECT, ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer) As Integer
    End Function

    <DllImport("User32")> _
    Public Function GetCursorPos(ByRef lpPoint As POINTAPI) As Integer
    End Function

    <DllImport("Shell32")> _
    Public Function SHAppBarMessage(ByVal dwMessage As ABMsg, ByRef pData As APPBARDATA) As Integer
    End Function

    <DllImport("User32")> _
    Public Function SetWindowPos(ByVal hWnd As Integer, ByVal hwndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    End Function

    <DllImport("Shell32", CharSet:=CharSet.Auto)> _
    Public Function SHGetFileInfo(ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFO, ByVal cbFileInfo As Integer, ByVal uFlags As Integer) As IntPtr
    End Function

    <DllImport("User32.dll")> _
    Public Sub SetActiveWindow(ByVal hwnd As IntPtr)
    End Sub

    <DllImport("user32")> _
    Public Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As RECT, ByVal fuWinIni As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Function IsWindowVisible(ByVal hwnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32")> _
    Public Function FlashWindowEx(ByRef pfwi As FLASHWINFO) As Int32
    End Function

    <DllImport("user32.dll")> _
    Public Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Function SendMessage(ByVal hWnd As HandleRef, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Function GetActiveWindow() As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Function GetForegroundWindow() As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Function SetWindowText(ByVal hwnd As IntPtr, ByVal lpString As String) As Integer
    End Function 'SetWindowText

    <DllImport("user32.dll", EntryPoint:="GetWindowLong")> _
    Public Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As GWL) As Integer
    End Function

    <DllImport("user32.dll", EntryPoint:="SetWindowLong")> _
    Public Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As GWL, ByVal dwNewLong As WS_EX) As Integer
    End Function

    <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
    Public Function RegisterWindowMessage(ByVal msg As String) As Integer
    End Function

    <DllImport("User32.dll", ExactSpelling:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Function MoveWindow(ByVal hWnd As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, _
                                   ByVal repaint As Boolean) As Boolean
    End Function

    <DllImport("advapi32.dll", CharSet:=CharSet.Unicode, EntryPoint:="RegOpenKeyEx")> _
    Public Function RegOpenKeyEx(ByVal hKey As RegistryHive, ByVal subKey As String, ByVal options As UInteger, ByVal sam As Integer, ByRef phkResult As UIntPtr) As Integer
    End Function

    <DllImport("advapi32.dll", CharSet:=CharSet.Unicode, EntryPoint:="RegQueryValueExW", SetLastError:=True)> _
    Public Function RegQueryValueEx(ByVal hKey As UIntPtr, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As UInteger, ByVal lpData As System.Text.StringBuilder, ByRef lpcbData As UInteger) As Integer
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Function RegCloseKey(ByVal hKey As UIntPtr) As Integer
    End Function
#End Region
End Module

