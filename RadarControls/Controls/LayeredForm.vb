Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.ComponentModel
Imports System.Windows.Forms

Public Class LayeredForm
    Inherits Form

    Private m_layered As Boolean
    Private m_clickthru As Boolean
    Private m_DualMonitors As Boolean
    Private m_DualPosition As ScreenPositioning
    Private m_WorkAreaRect As RECT
    Private m_SecondScreen As Rectangle

    Private DockIn As DockTo = DockTo.Primary
    Private hWindowDC As IntPtr
    Private hBufferDC As IntPtr
    Private gBuffer As Graphics
    Private rBounds As Rectangle
    Private bUpdateNeeded As Boolean = True
    Private m_layerOpacity As Double = 0

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure POINT
        Public x As Int32
        Public y As Int32
        Public Sub New(ByVal x As Int32, ByVal y As Int32)
            Me.x = x
            Me.y = y
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Private Shadows Structure SIZE
        Public cx As Int32
        Public cy As Int32
        Public Sub New(ByVal cx As Int32, ByVal cy As Int32)
            Me.cx = cx
            Me.cy = cy
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Private Structure ARGB
        Public Blue As Byte
        Public Green As Byte
        Public Red As Byte
        Public Alpha As Byte
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Private Structure BLENDFUNCTION
        Public BlendOp As Byte
        Public BlendFlags As Byte
        Public SourceConstantAlpha As Byte
        Public AlphaFormat As Byte
    End Structure

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Private Const WM_NCHITTEST As Integer = &H84
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const WM_MOUSEWHEEL As Integer = &H20A
    Private Const WM_LBUTTONDOWN As Integer = &H201
    Private Const WM_MOUSEFIRST = &H200
    Private Const WM_MOUSEMOVE = &H200
    Private Const WM_LBUTTONUP = &H202
    Private Const WM_LBUTTONDBLCLK = &H203
    Private Const WM_RBUTTONDOWN = &H204
    Private Const WM_RBUTTONUP = &H205
    Private Const WM_RBUTTONDBLCLK = &H206
    Private Const WM_MBUTTONDOWN = &H207
    Private Const WM_MBUTTONUP = &H208
    Private Const WM_MBUTTONDBLCLK = &H209
    Private Const WM_MOUSEHWHEEL = &H20E

    Private Const HTERROR As Integer = (-2)
    Private Const HTTRANSPARENT As Integer = (-1)
    Private Const HTNOWHERE As Integer = 0
    Private Const HTCLIENT As Integer = 1
    Private Const HTCAPTION As Integer = 2
    Private Const HTSYSMENU As Integer = 3
    Private Const HTGROWBOX As Integer = 4
    Private Const HTSIZE As Integer = HTGROWBOX
    Private Const HTMENU As Integer = 5
    Private Const HTHSCROLL As Integer = 6
    Private Const HTVSCROLL As Integer = 7
    Private Const HTMINBUTTON As Integer = 8
    Private Const HTMAXBUTTON As Integer = 9
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Const HTTOP As Integer = 12
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14
    Private Const HTBOTTOM As Integer = 15
    Private Const HTBOTTOMLEFT As Integer = 16
    Private Const HTBOTTOMRIGHT As Integer = 17
    Private Const HTBORDER As Integer = 18
    Private Const HTREDUCE As Integer = HTMINBUTTON
    Private Const HTZOOM As Integer = HTMAXBUTTON
    Private Const HTSIZEFIRST As Integer = HTLEFT
    Private Const HTSIZELAST As Integer = HTBOTTOMRIGHT
    Private Const HTOBJECT As Integer = 19
    Private Const HTCLOSE As Integer = 20
    Private Const HTHELP As Integer = 21

    Private Const WS_EX_TRANSPARENT As Integer = &H20
    Private Const WS_EX_LAYERED As Integer = &H80000
    Private Const WS_EX_COMPOSITED As Integer = &H2000000

    Private Const ULW_COLORKEY As Int32 = &H1
    Private Const ULW_ALPHA As Int32 = &H2
    Private Const ULW_OPAQUE As Int32 = &H4
    Private Const LWA_ALPHA As Integer = &H2
    Private Const LWA_COLORKEY As Integer = &H1
    Private Const GWL_EXSTYLE As Integer = -20
    Private Const AC_SRC_OVER As Byte = &H0
    Private Const AC_SRC_ALPHA As Byte = &H1

    Public Const SPI_GETWORKAREA = 48

    Private Declare Auto Function UpdateLayeredWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal hdcDst As IntPtr, ByRef pptDst As POINT, ByRef psize As SIZE, ByVal hdcSrc As IntPtr, ByRef pptSrc As POINT, _
     ByVal crKey As UInteger, <[In]()> ByRef pblend As BLENDFUNCTION, ByVal dwFlags As UInteger) As Boolean

    <DllImport("user32.dll")> _
    Private Shared Function SetLayeredWindowAttributes(ByVal hWnd As IntPtr, ByVal crKey As UInteger, ByVal bAlpha As Byte, ByVal dwFlags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function GetDC(ByVal hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
    End Function

    <DllImport("gdi32.dll", SetLastError:=True)> _
    Private Shared Function CreateCompatibleDC(ByVal hDC As IntPtr) As IntPtr
    End Function

    <DllImport("gdi32.dll")> _
    Private Shared Function DeleteDC(ByVal hdc As IntPtr) As Boolean
    End Function

    Private Declare Auto Function SelectObject Lib "gdi32.dll" (ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr

    <DllImport("gdi32.dll")> _
    Private Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    End Function

    <DllImportAttribute("user32.dll")> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    <DllImportAttribute("user32.dll")> _
    Private Shared Function ReleaseCapture() As Boolean
    End Function

    'foreground forcing
    <DllImport("user32.dll")> _
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function AttachThreadInput(ByVal idAttach As UInteger, ByVal idAttachTo As UInteger, ByVal fAttach As Boolean) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function IsIconic(ByVal hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function IsZoomed(ByVal hWnd As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll")> _
    Private Shared Function GetCurrentThread() As IntPtr
    End Function

    <DllImport("kernel32.dll")> _
    Private Shared Function GetCurrentThreadId() As UInteger
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function SetFocus(ByVal hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("User32.Dll")> _
    Private Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByRef lpdwProcessId As UInteger) As UInteger
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function BringWindowToTop(ByVal hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function ShowWindow(ByVal hWnd As IntPtr, ByVal nCmdShow As ShowWindowFlags) As Boolean
    End Function

    <DllImport("user32")> _
    Private Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As RECT, ByVal fuWinIni As Integer) As Integer
    End Function

    Private Enum DockLocation
        Top
        Bottom
        Right
        Left
    End Enum

    Private Enum ScreenPositioning
        MainLeft
        MainRight
    End Enum

    Private Enum DockTo
        Primary
        Secondary
    End Enum

    Private Enum ShowWindowFlags As UInteger
        SW_HIDE = 0
        SW_SHOWNORMAL = 1
        SW_NORMAL = 1
        SW_SHOWMINIMIZED = 2
        SW_SHOWMAXIMIZED = 3
        SW_MAXIMIZE = 3
        SW_SHOWNOACTIVATE = 4
        SW_SHOW = 5
        SW_MINIMIZE = 6
        SW_SHOWMINNOACTIVE = 7
        SW_SHOWNA = 8
        SW_RESTORE = 9
        SW_SHOWDEFAULT = 10
        SW_FORCEMINIMIZE = 11
        SW_MAX = 11
    End Enum

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        rBounds = New Rectangle(0, 0, Width - 1, Height - 1)
        hWindowDC = GetDC(Handle)
        hBufferDC = CreateCompatibleDC(hWindowDC)
        MyBase.OnHandleCreated(e)
        If Layered AndAlso IsHandleCreated AndAlso Not DesignMode Then
            Me.OnPaint(Nothing)
        End If
        'call directly because invalidate doesnt work during initialization
    End Sub

    Protected Overrides Sub OnHandleDestroyed(ByVal e As EventArgs)
        If gBuffer IsNot Nothing Then
            gBuffer.Dispose()
        End If
        DeleteDC(hBufferDC)
        ReleaseDC(IntPtr.Zero, hWindowDC)
        MyBase.OnHandleDestroyed(e)
    End Sub

    ''' <summary>Creates the System.Drawing.Graphics for the control.</summary>
    Public Shadows Function CreateGraphics() As Graphics
        If Layered AndAlso Not DesignMode Then
            'The buffer is a memory bitmap with an alpha channel. If the form size has changed since this buffer was made then it will need to be recreated.
            If bUpdateNeeded Then
                'create a memory buffer with an alpha channel
                Using surfaceNew As New Bitmap(Width, Height, PixelFormat.Format32bppArgb)
                    Dim surfaceOld = SelectObject(hBufferDC, surfaceNew.GetHbitmap())
                    If surfaceOld <> IntPtr.Zero Then
                        DeleteObject(surfaceOld)
                    End If
                End Using

                'create the GDI+ object using the device context
                Dim buffer = Graphics.FromHdc(hBufferDC)
                buffer.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
                buffer.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias

                'dispose the old buffer if required
                If gBuffer IsNot Nothing Then
                    gBuffer.Dispose()
                End If
                gBuffer = buffer
                bUpdateNeeded = False
            End If

            'Prepare drawing by clearing all color information
            gBuffer.Clear(Color.FromArgb(CInt(Math.Truncate(255 * m_layerOpacity)), BackColor))
            Return gBuffer
        Else
            Return MyBase.CreateGraphics()
        End If
    End Function

    ''' <summary>Causes the control to redraw the invalidated regions within its client area.</summary>
    Public Shadows Sub Update()
        If Layered AndAlso Not DesignMode Then
            'Send the buffer to the o/s
            Dim size As New SIZE(Width, Height)
            Dim pointSource As New POINT(0, 0)
            Dim topPos As New POINT(Left, Top)
            Dim blend As New BLENDFUNCTION() With {
                .BlendOp = AC_SRC_OVER,
                .BlendFlags = 0,
                .SourceConstantAlpha = 255,
                .AlphaFormat = AC_SRC_ALPHA
            }

            UpdateLayeredWindow(Handle, IntPtr.Zero, topPos, size, hBufferDC, pointSource, _
             0, blend, ULW_ALPHA)
        Else
            MyBase.Update()
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)

        'Update the draw region and recreated the buffer bitmap at the new size
        rBounds.Width = Width - 1
        rBounds.Height = Height - 1
        bUpdateNeeded = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Layered AndAlso Not DesignMode Then
            'Generate our own graphic buffer if in layered mode
            MyBase.OnPaint(New PaintEventArgs(Me.CreateGraphics(), rBounds))
            Update()
        Else
            'othewise lets use the buffer provided by the control
            MyBase.OnPaint(New PaintEventArgs(e.Graphics, Me.ClientRectangle))
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        'Do not paint the backgroudn because this is a layered form
    End Sub

    <DefaultValue(False)> _
    <Category("Layered")> _
    <Description("Gets or sets whether the form uses a window layer to render its content.")> _
    Public Property Layered() As Boolean
        Get
            Return m_layered
        End Get
        Set(ByVal value As Boolean)
            'only do something if the mode is being changed
            If m_layered <> value Then
                m_layered = value

                If Not DesignMode Then
                    'Do not toggle the layered mode if the window has not been created yet
                    If Not IsHandleCreated Then
                        Return
                    End If

                    'If we are turning layered mode ON and the layered style is already set (usually due to form opacity),
                    '  then remove it immediatly to kill the paint redirection.
                    Dim style As Integer = GetWindowLong(Handle, GWL_EXSTYLE)
                    If m_layered AndAlso ((style And WS_EX_LAYERED) > 0) Then
                        SetWindowLong(Handle, GWL_EXSTYLE, style - WS_EX_LAYERED)
                    End If

                    'Add the layered style if turning layered mode on, or remove the style if turning off
                    style = style Or WS_EX_LAYERED
                    If Not value AndAlso Opacity = 0 Then
                        'keep layered style on if were going to use the regular form opacity mode
                        style -= WS_EX_LAYERED
                    End If
                    SetWindowLong(Handle, GWL_EXSTYLE, style)
                    If Not m_layered AndAlso Opacity > 0 Then
                        'layered mode is being turned off. if the normal form opacity is set then turn on the simple LWA mode
                        SetLayeredWindowAttributes(Handle, 0, CByte(Math.Truncate(255 * Opacity)), LWA_ALPHA)
                    End If

                    'Force repaint
                    bUpdateNeeded = True
                    Invalidate()
                End If

                If Not m_layered Then
                    Me.SetStyle(ControlStyles.UserPaint Or
                                ControlStyles.ResizeRedraw Or
                                ControlStyles.OptimizedDoubleBuffer, True)
                    Me.UpdateStyles()
                Else
                    Me.SetStyle(ControlStyles.UserPaint Or
                                ControlStyles.ResizeRedraw Or
                                ControlStyles.OptimizedDoubleBuffer, False)
                    Me.UpdateStyles()
                End If
            End If
        End Set
    End Property

    <DefaultValue(0)> _
    <Category("Layered")> _
    <Description("Gets or sets the opacity percentage of the background when in layered mode or the entire form when not in layered mode.")> _
    <TypeConverterAttribute(GetType(OpacityConverter))> _
    <DisplayName("Opacity Layer")> _
    Public Property LayerOpacity() As Double
        Get
            Return m_layerOpacity
        End Get
        Set(ByVal value As Double)
            m_layerOpacity = value

            'opacity is a decimal notation percentage.
            If m_layerOpacity < 0 Then
                m_layerOpacity = 0
            End If
            If m_layerOpacity > 1 Then
                m_layerOpacity = 1
            End If
            Invalidate()
        End Set
    End Property

    <DefaultValue(False)> _
    <Category("Layered")> _
    <Description("Gets or sets whether the form ignores mouse input, passing it to the window underneath.")> _
    Public Property ClickThrough() As Boolean
        Get
            Return m_clickthru
        End Get
        Set(ByVal value As Boolean)
            'only do something if the mode is being changed
            If m_clickthru <> value Then
                m_clickthru = value

                If Not DesignMode Then
                    Dim style As Integer = GetWindowLong(Handle, GWL_EXSTYLE)
                    style = style Or WS_EX_TRANSPARENT
                    If Not value Then
                        style -= WS_EX_TRANSPARENT
                    End If
                    SetWindowLong(Handle, GWL_EXSTYLE, style)
                End If
            End If
        End Set
    End Property

    <DefaultValue(False)> _
    <Category("Layered")> _
    <Description("Gets or sets whether the form can be moved by dragging the client area with the mouse.")> _
    Public Property Draggable() As Boolean = False

    <DefaultValue(False)> _
    <Category("Layered")> _
    <Description("Gets or sets whether the form can be resized without a border when in layered mode.")> _
    Public Property Resizable() As Boolean = True

    <DefaultValue(3)> _
    <Category("Layered")> _
    <Description("Gets or sets how thick the resize ""grip"" is.")> _
    Public Property ResizeThreshold() As Integer = 5


    <DefaultValue(False),
    Category("Layered"),
    Description("Gets or sets if theo form is dockable to the edges of the screen")>
    Public Property Dockable() As Boolean = False

    <DefaultValue(10)> _
    <Category("Layered")> _
    <Description("Gets or sets the threshold for auto snapping to the edge")> _
    Public Property DockThreshold() As Integer = 10

    Private m_dockLocations As List(Of DockLocation)
    Private ReadOnly Property DockedLocation As List(Of DockLocation)
        Get
            If m_dockLocations Is Nothing Then
                m_dockLocations = New List(Of DockLocation)
            End If
            Return m_dockLocations
        End Get
    End Property

    Private Shared ReadOnly Property ScreenCount() As Integer
        Get
            Return SystemInformation.MonitorCount
        End Get
    End Property

    Public Property ChildForm As Form

    '''' <summary>Force the window to the active state regardless if another process owns the foreground.</summary>
    'Public Sub ForceToFront()
    '    'switchmon: go go reusable code.
    '    Dim a As UInteger
    '    Dim hWndForeground As IntPtr = GetForegroundWindow()
    '    If hWndForeground <> Me.Handle Then
    '        Dim thread1 As UInteger = GetWindowThreadProcessId(hWndForeground, a)
    '        Dim thread2 As UInteger = GetCurrentThreadId()

    '        If thread1 <> thread2 Then
    '            AttachThreadInput(thread1, thread2, True)
    '            BringWindowToTop(Me.Handle)
    '            If IsIconic(Me.Handle) Then
    '                ShowWindow(Me.Handle, ShowWindowFlags.SW_SHOWNORMAL)
    '            Else
    '                ShowWindow(Me.Handle, ShowWindowFlags.SW_SHOW)
    '            End If
    '            AttachThreadInput(thread1, thread2, False)
    '        Else
    '            SetForegroundWindow(Me.Handle)
    '        End If
    '        If IsIconic(Me.Handle) Then
    '            ShowWindow(Me.Handle, ShowWindowFlags.SW_SHOWNORMAL)
    '        Else
    '            ShowWindow(Me.Handle, ShowWindowFlags.SW_SHOW)
    '        End If
    '    End If
    'End Sub

    'Support setting the layered flag during initialization if the designer wishes the layered capabilities during load
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp = MyBase.CreateParams
            If m_layered AndAlso Not DesignMode Then
                cp.ExStyle = cp.ExStyle Or WS_EX_LAYERED
            End If
            Return cp
        End Get
    End Property

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_NCHITTEST Then
            'decode the mouse coordinates
            Dim lp As Integer = CInt(m.LParam)
            Dim x As Integer = lp And &HFFFF
            Dim y As Integer = (lp >> 16) And &HFFFF

            'inform the o/s where the hit test landed
            If m_layered AndAlso x >= Bounds.Left AndAlso x <= Bounds.Left + ResizeThreshold AndAlso y >= Bounds.Top AndAlso y <= Bounds.Top + ResizeThreshold Then
                m.Result = New IntPtr(HTTOPLEFT)
            ElseIf m_layered AndAlso x >= Bounds.Right - ResizeThreshold AndAlso x <= Bounds.Right AndAlso y >= Bounds.Top AndAlso y <= Bounds.Top + ResizeThreshold Then
                m.Result = New IntPtr(HTTOPRIGHT)
            ElseIf m_layered AndAlso x >= Bounds.Left AndAlso x <= Bounds.Left + ResizeThreshold AndAlso y >= Bounds.Bottom - ResizeThreshold AndAlso y <= Bounds.Bottom Then
                m.Result = New IntPtr(HTBOTTOMLEFT)
            ElseIf m_layered AndAlso x >= Bounds.Right - ResizeThreshold AndAlso x <= Bounds.Right AndAlso y >= Bounds.Bottom - ResizeThreshold AndAlso y <= Bounds.Bottom Then
                m.Result = New IntPtr(HTBOTTOMRIGHT)
            ElseIf m_layered AndAlso Resizable AndAlso x >= Bounds.Left AndAlso x <= Bounds.Left + ResizeThreshold Then
                m.Result = CType(HTLEFT, IntPtr)
            ElseIf m_layered AndAlso Resizable AndAlso x >= Bounds.Right - ResizeThreshold AndAlso x <= Bounds.Right Then
                m.Result = CType(HTRIGHT, IntPtr)
            ElseIf m_layered AndAlso Resizable AndAlso y >= Bounds.Top AndAlso y <= Bounds.Top + ResizeThreshold Then
                m.Result = CType(HTTOP, IntPtr)
            ElseIf m_layered AndAlso Resizable AndAlso y >= Bounds.Bottom - ResizeThreshold AndAlso y <= Bounds.Bottom Then
                m.Result = CType(HTBOTTOM, IntPtr)
            Else
                MyBase.WndProc(m)
                'If ChildForm IsNot Nothing Then
                '    SendMessage(ChildForm.Handle, m.Msg, m.WParam, 0)
                'End If
            End If
        ElseIf Draggable AndAlso m.Msg = WM_LBUTTONDOWN Then
            'Simulate title bar dragging for drag mode and only if the left mouse is down (to allow context menu to still fire)
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'LayeredForm
        '
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Name = "LayeredForm"
        Me.ResumeLayout(False)

    End Sub

    Private Sub LayeredForm_Move(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Move
        If Me.Dockable Then
            SystemParametersInfo(SPI_GETWORKAREA, 0&, m_WorkAreaRect, 0&)
            If ScreenCount > 1 Then
                m_DualMonitors = True
                For Each Scr In Screen.AllScreens
                    If Not Scr.Primary Then
                        m_SecondScreen = Scr.Bounds
                        Exit For
                    End If
                Next
                If m_SecondScreen.Left < m_WorkAreaRect.Left Then
                    m_DualPosition = ScreenPositioning.MainRight
                Else
                    m_DualPosition = ScreenPositioning.MainLeft
                End If
            Else
                m_DualMonitors = False
            End If

            Dim ToLeftDistance As Long
            Dim ToRightDistance As Long
            Dim ToTopDistance As Long
            Dim ToBottomDistance As Long

            If m_DualMonitors Then
                If m_DualPosition = ScreenPositioning.MainRight Then
                    If Me.Location.X < 0 Then
                        DockIn = DockTo.Secondary
                        ToRightDistance = m_WorkAreaRect.Right - (Me.Location.X + Me.Width)
                        ToLeftDistance = Me.Location.X - m_SecondScreen.Left
                        ToBottomDistance = m_SecondScreen.Bottom - (Me.Location.Y + Me.Height)
                        ToTopDistance = Me.Location.Y - m_SecondScreen.Top
                    Else
                        DockIn = DockTo.Primary
                        ToRightDistance = m_WorkAreaRect.Right - (Me.Location.X + Me.Width)
                        ToLeftDistance = Me.Location.X - m_SecondScreen.Left
                        ToBottomDistance = m_WorkAreaRect.Bottom - (Me.Location.Y + Me.Height)
                        ToTopDistance = Me.Location.Y - m_WorkAreaRect.Top
                    End If
                Else
                    If Me.Location.X > m_WorkAreaRect.Right Then
                        DockIn = DockTo.Secondary
                        ToRightDistance = m_SecondScreen.Right - (Me.Location.X + Me.Width)
                        ToLeftDistance = Me.Location.X - m_WorkAreaRect.Left
                        ToBottomDistance = m_SecondScreen.Bottom - (Me.Location.Y + Me.Height)
                        ToTopDistance = Me.Location.Y - m_SecondScreen.Top
                    Else
                        DockIn = DockTo.Primary
                        ToRightDistance = m_SecondScreen.Right - (Me.Location.X + Me.Width)
                        ToLeftDistance = Me.Location.X - m_WorkAreaRect.Left
                        ToBottomDistance = m_WorkAreaRect.Bottom - (Me.Location.Y + Me.Height)
                        ToTopDistance = Me.Location.Y - m_WorkAreaRect.Top
                    End If
                End If
            Else
                DockIn = DockTo.Primary
                ToRightDistance = m_WorkAreaRect.Right - (Me.Location.X + Me.Width)
                ToLeftDistance = Me.Location.X - m_WorkAreaRect.Left
                ToBottomDistance = m_WorkAreaRect.Bottom - (Me.Location.Y + Me.Height)
                ToTopDistance = Me.Location.Y - m_WorkAreaRect.Top
            End If

            HandleDocking(Me.Location.X, Me.Location.Y, ToLeftDistance, ToRightDistance, ToTopDistance, ToBottomDistance)


        End If
    End Sub

    Private Sub HandleDocking(ByRef NewX As Long, ByRef NewY As Long, ByVal ToLeftDistance As Long, ByVal ToRightDistance As Long, ByVal ToTopDistance As Long, ByVal ToBottomDistance As Long)
        If Not DockedLocation.Contains(DockLocation.Bottom) Then
            If Math.Abs(ToBottomDistance) <= DockThreshold Then
                ' Attach to edge
                Me.Top += ToBottomDistance
                DockedLocation.Add(DockLocation.Bottom)
            End If
        Else
            If Math.Abs(ToBottomDistance) > DockThreshold Then
                ' Break the attachement
                DockedLocation.Remove(DockLocation.Bottom)
            Else
                ' Stay at current position
                NewY = Me.Top
            End If
        End If

        If Not DockedLocation.Contains(DockLocation.Top) Then
            If Math.Abs(ToTopDistance) <= DockThreshold Then
                If DockIn = DockTo.Primary Then
                    NewY = m_WorkAreaRect.Top
                Else
                    NewY = m_SecondScreen.Top
                End If
                DockedLocation.Add(DockLocation.Top)
            End If
        Else
            If Math.Abs(ToTopDistance) > DockThreshold Then
                DockedLocation.Remove(DockLocation.Top)
            Else
                NewY = Me.Top
            End If
        End If

        If Not DockedLocation.Contains(DockLocation.Right) Then
            If Math.Abs(ToRightDistance) <= DockThreshold Then
                If DockIn = DockTo.Primary Then
                    NewX = m_WorkAreaRect.Right - Me.Width
                Else
                    NewX = m_SecondScreen.Right - Me.Width
                End If

                DockedLocation.Add(DockLocation.Right)
            End If
        Else
            If Math.Abs(ToRightDistance) > DockThreshold Then
                DockedLocation.Remove(DockLocation.Right)
            Else
                NewX = Me.Left
            End If
        End If

        If Not DockedLocation.Contains(DockLocation.Left) Then
            If Math.Abs(ToLeftDistance) <= DockThreshold Then
                If DockIn = DockTo.Primary Then
                    Me.Left = m_WorkAreaRect.Left
                Else
                    Me.Left = m_SecondScreen.Left
                End If
                DockedLocation.Add(DockLocation.Left)
            End If
        Else
            If Math.Abs(ToLeftDistance) > DockThreshold Then
                DockedLocation.Remove(DockLocation.Left)
            Else
                NewX = Me.Left
            End If
        End If
    End Sub
End Class
