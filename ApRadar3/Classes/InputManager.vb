Imports System.Runtime.InteropServices

Public Class InputManager
#Region " MEMBER VARIABLES "
    Private WM_CHAR As Integer = &H102

#End Region

#Region " CONSTANTS "
    Const INPUT_KEYBOARD As Integer = 1
#End Region

#Region " ENUM "
    ''' <summary>The set of valid MapTypes used in MapVirtualKey
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MapVirtualKeyMapTypes As UInt32
        ''' <summary>uCode is a virtual-key code and is translated into a scan code.
        ''' If it is a virtual-key code that does not distinguish between left- and 
        ''' right-hand keys, the left-hand scan code is returned. 
        ''' If there is no translation, the function returns 0.
        ''' </summary>
        ''' <remarks></remarks>
        MAPVK_VK_TO_VSC = &H0

        ''' <summary>uCode is a scan code and is translated into a virtual-key code that
        ''' does not distinguish between left- and right-hand keys. If there is no 
        ''' translation, the function returns 0.
        ''' </summary>
        ''' <remarks></remarks>
        MAPVK_VSC_TO_VK = &H1

        ''' <summary>uCode is a virtual-key code and is translated into an unshifted
        ''' character value in the low-order word of the return value. Dead keys (diacritics)
        ''' are indicated by setting the top bit of the return value. If there is no
        ''' translation, the function returns 0.
        ''' </summary>
        ''' <remarks></remarks>
        MAPVK_VK_TO_CHAR = &H2

        ''' <summary>Windows NT/2000/XP: uCode is a scan code and is translated into a
        ''' virtual-key code that distinguishes between left- and right-hand keys. If
        ''' there is no translation, the function returns 0.
        ''' </summary>
        ''' <remarks></remarks>
        MAPVK_VSC_TO_VK_EX = &H3

        ''' <summary>Not currently documented
        ''' </summary>
        ''' <remarks></remarks>
        MAPVK_VK_TO_VSC_EX = &H4
    End Enum

    Public Enum KEYEVENTF As UInteger
        KEYDOWN = &H0
        EXTENDEDKEY = &H1
        KEYUP = &H2
        UNICODE = &H4
        SCANCODE = &H8
    End Enum
#End Region

#Region " STRUCTURES "
    Private Structure INPUT
        Dim dwType As Integer
        Dim mkhi As MOUSEKEYBDHARDWAREINPUT
    End Structure

    Private Structure KEYBDINPUT
        Public wVk As Short
        Public wScan As Short
        Public dwFlags As Integer
        Public time As Integer
        Public dwExtraInfo As IntPtr
    End Structure

    Private Structure HARDWAREINPUT
        Public uMsg As Integer
        Public wParamL As Short
        Public wParamH As Short
    End Structure

    <StructLayout(LayoutKind.Explicit)> _
    Private Structure MOUSEKEYBDHARDWAREINPUT
        <FieldOffset(0)> Public mi As MOUSEINPUT
        <FieldOffset(0)> Public ki As KEYBDINPUT
        <FieldOffset(0)> Public hi As HARDWAREINPUT
    End Structure

    Private Structure MOUSEINPUT
        Public dx As Integer
        Public dy As Integer
        Public mouseData As Integer
        Public dwFlags As Integer
        Public time As Integer
        Public dwExtraInfo As IntPtr
    End Structure
#End Region

#Region " API "
    ''' <summary>The MapVirtualKey function translates (maps) a virtual-key code into a scan
    ''' code or character value, or translates a scan code into a virtual-key code
    ''' </summary>
    ''' <param name="uCode">[in] Specifies the virtual-key code or scan code for a key.
    ''' How this value is interpreted depends on the value of the uMapType parameter</param>
    ''' <param name="uMapType">[in] Specifies the translation to perform. The value of this
    ''' parameter depends on the value of the uCode parameter.</param>
    ''' <returns>Either a scan code, a virtual-key code, or a character value, depending on
    ''' the value of uCode and uMapType. If there is no translation, the return value is zero</returns>
    ''' <remarks></remarks>
    <DllImport("User32.dll", SetLastError:=False, CallingConvention:=CallingConvention.StdCall, _
     CharSet:=CharSet.Auto)> _
    Public Shared Function MapVirtualKey(ByVal uCode As UInt32, ByVal uMapType As MapVirtualKeyMapTypes) As UInt32
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As Char, ByVal lParam As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function VkKeyScan(ByVal c As Char) As Int16
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function SendInput(ByVal nInputs As Integer, ByRef pInputs As INPUT, ByVal cbSize As Integer) As Integer
    End Function
#End Region

#Region " PUBLIC FUNCTIONS "
    Public Sub SendString(ByVal POL As Process, ByVal Message As String)
        Dim key As Int16
        Dim vKey As Byte
        Dim code As Integer
        For Each c As Char In Message.ToCharArray
            key = VkKeyScan(c)
            vKey = key And &HFF
            'shift = key And &H100
            If key <> &H101 Then
                'If shift AndAlso Not shiftDown Then
                '    code = MapVirtualKey(VK_SHIFT, 0)
                '    PostMessage(hWnd, WM_KEYDOWN, VK_SHIFT, code)
                '    shiftDown = True
                'End If

                code = MapVirtualKey(key, 0)

                'PostMessage(hWnd, WM_KEYDOWN, vKey, code)
                PostMessage(POL.MainWindowHandle, WM_CHAR, c, code)

                'code = code Or &H80000000
                'code = code Or &H40000000

                'PostMessage(hWnd, WM_KEYUP, vKey, code)
                'If Not shift And shiftDown Then
                '    code = MapVirtualKey(VK_SHIFT, 0)
                '    code = code Or &H80000000
                '    code = code Or &H40000000
                '    PostMessage(hWnd, WM_KEYUP, VK_SHIFT, code)
                '    shiftDown = False
                'End If
            End If
        Next
        'If shiftDown Then
        '    code = MapVirtualKey(VK_SHIFT, 0)
        '    code = code Or &H80000000
        '    code = code Or &H40000000
        '    PostMessage(hWnd, WM_KEYUP, VK_SHIFT, code)
        '    shiftDown = False
        'End If

        PressKey(POL, Keys.Enter)

        'code = MapVirtualKey(VK_RETURN, 0)
        'PostMessage(hWnd, WM_KEYDOWN, VK_RETURN, code)
        'code = code Or &H80000000
        'code = code Or &H40000000
        'PostMessage(hWnd, WM_KEYUP, VK_RETURN, code)
    End Sub

    Public Shared Sub PressKey(ByVal POL As Process, ByVal Key As Keys)

        AppActivate(POL.Id)
        System.Threading.Thread.Sleep(100)
        DoKeyBoard(KEYEVENTF.KEYDOWN, Key)
        DoKeyBoard(KEYEVENTF.KEYUP, Key)
    End Sub


    Private Shared Sub DoKeyBoard(ByVal flags As KEYEVENTF, ByVal key As Keys)
        Dim input As New INPUT
        Dim ki As New KEYBDINPUT
        input.dwType = INPUT_KEYBOARD
        input.mkhi.ki = ki
        input.mkhi.ki.wVk = Convert.ToInt16(key)
        input.mkhi.ki.wScan = 0
        input.mkhi.ki.time = 0
        input.mkhi.ki.dwFlags = flags
        input.mkhi.ki.dwExtraInfo = IntPtr.Zero
        Dim cbSize As Integer = Marshal.SizeOf(input)
        Dim result As Integer = SendInput(1, input, cbSize)
        If result = 0 Then Debug.WriteLine(Marshal.GetLastWin32Error)
    End Sub
#End Region
End Class
