' ==============================================================================
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'
' © 2003-2004 LaMarvin. All Rights Reserved.
'
' FMI: http://www.vbinfozine.com/a_default.shtml
' ==============================================================================

Imports System.Drawing
Imports System.Windows.Forms.Design
Imports System.ComponentModel
Imports System.Windows.Forms


' This class actually hosts the ColorEditor.ColorUI control instance by implementing the 
' IWindowsFormsEditorService interface.
Friend Class WindowsFormsEditorService
    Implements IWindowsFormsEditorService
    Implements IServiceProvider

    ' The associated control.
    Private _Owner As Control

    ' Reference to the drop down form, which hosts the ColorUI control.
    Private _DropDownHolder As DropDownForm

    ' Cached _DropDownHolder.Canceled flag in order to allow it to be inspected
    ' by the owning Control control.
    Private _Canceled As Boolean


    Public Sub New(ByVal owner As Control)
        Debug.Assert(Not owner Is Nothing)
        _Owner = owner
    End Sub


    Public ReadOnly Property Canceled() As Boolean
        Get
            Return _Canceled
        End Get
    End Property


    Public Sub CloseDropDown() Implements IWindowsFormsEditorService.CloseDropDown
        If Not _DropDownHolder Is Nothing Then
            _DropDownHolder.CloseDropDown()
        End If
    End Sub


    Public Sub DropDownControl(ByVal control As Control) Implements IWindowsFormsEditorService.DropDownControl
        _Canceled = False

        ' Initialize the hosting form for the control.
        _DropDownHolder = New DropDownForm
        _DropDownHolder.Bounds = control.Bounds
        _DropDownHolder.SetControl(control)

        ' Lookup a parent form for the owning control and make the dropdown form to be owned
        ' by it. This prevents to show the dropdown form icon when the user presses the At+Tab system 
        ' key while the dropdown form is displayed.
        Dim PickerForm As control = Me.GetParentForm(_Owner)
        If (Not PickerForm Is Nothing) AndAlso (TypeOf PickerForm Is Form) Then
            _DropDownHolder.Owner = DirectCast(PickerForm, Form)
        End If

        ' Ensure the whole drop-down UI is displayed on the screen and show it.
        Me.PositionDropDownHolder()
        _DropDownHolder.Show()       ' ShowDialog would disable clicking outside the dropdown area!

        ' Wait for the user to select something from the dropped-down control (in which case the 
        '	control calls our CloseDropDown method) or cancel the selection (no CloseDropDown called, 
        ' the Cancel flag is set to True).
        Me.DoModalLoop()

        ' Remember the cancel flag and get rid of the drop down form.
        _Canceled = _DropDownHolder.Canceled
        _DropDownHolder.Dispose()
        _DropDownHolder = Nothing
    End Sub


    Public Function ShowDialog(ByVal dialog As Form) As DialogResult Implements IWindowsFormsEditorService.ShowDialog
        Throw New NotSupportedException
    End Function


    Public Function GetService(ByVal serviceType As System.Type) As Object Implements System.IServiceProvider.GetService
        If serviceType.Equals(GetType(IWindowsFormsEditorService)) Then
            Return Me
        End If
        Return Nothing
    End Function


    Private Sub DoModalLoop()
        Debug.Assert(Not _DropDownHolder Is Nothing)
        Do While _DropDownHolder.Visible
            Application.DoEvents()
            ' The following is the undocumented User32 call. For more information
            ' see the accompanying article at http://www.vbinfozine.com/a_Control.shtml
            WindowsFormsEditorService.MsgWaitForMultipleObjects(1, IntPtr.Zero, 1, 5, 255)
        Loop
    End Sub


    ' Don't forget (as I did:-) to declare the DllImport methods as Shared!
    ' Otherwise you'll get an exception *at runtime*!
    <System.Runtime.InteropServices.DllImport("User32", SetLastError:=True)> _
    Private Shared Function MsgWaitForMultipleObjects( _
     ByVal nCount As Int32, _
     ByVal pHandles As IntPtr, _
     ByVal bWaitAll As Int16, _
     ByVal dwMilliseconds As Int32, _
     ByVal dwWakeMask As Int32) As Int32
    End Function


    Private Sub PositionDropDownHolder()
        ' Convert _Owner location to screen coordinates.
        Dim Loc As Point = _Owner.Parent.PointToScreen(_Owner.Location)

        Dim ScreenRect As Rectangle = Screen.PrimaryScreen.WorkingArea

        ' Position the dropdown X coordinate in order to be displayed in its entirety.
        If Loc.X < ScreenRect.X Then
            Loc.X = ScreenRect.X
        ElseIf (Loc.X + _DropDownHolder.Width) > ScreenRect.Right Then
            Loc.X = ScreenRect.Right - _DropDownHolder.Width
        End If

        ' Do the same for the Y coordinate.
        If (Loc.Y + _Owner.Height + _DropDownHolder.Height) > ScreenRect.Bottom Then
            Loc.Offset(0, -_DropDownHolder.Height)           ' dropdown will be above the picker control
        Else
            Loc.Offset(0, _Owner.Height)             ' dropdown will be below the picker
        End If

        _DropDownHolder.Location = Loc
    End Sub


    Private Function GetParentForm(ByVal ctl As Control) As Control
        Do
            If ctl.Parent Is Nothing Then
                Return ctl
            Else
                ctl = ctl.Parent
            End If
        Loop
    End Function


    ' This is a simple Form descendant that hosts any control and provides the drop-down 
    ' appearance for it.
    Private Class DropDownForm
        Inherits Form

        Private _Canceled As Boolean         ' did the user cancel the color selection?
        Private _CloseDropDownCalled As Boolean      ' was the form closed by calling the CloseDropDown method?

        Public Sub New()
            MyBase.New()
            Me.FormBorderStyle = FormBorderStyle.None
            Me.ShowInTaskbar = False
            Me.KeyPreview = True             ' to catch the ESC key
            Me.StartPosition = FormStartPosition.Manual

            ' The control is hosted by a Panel, which provides the simple border frame
            ' not available for Forms.
            Dim P As New Panel
            P.BorderStyle = BorderStyle.FixedSingle
            P.Dock = DockStyle.Fill
            Me.Controls.Add(P)
        End Sub


        Public Sub SetControl(ByVal ctl As Control)
            DirectCast(Me.Controls(0), Panel).Controls.Add(ctl)
        End Sub


        Public ReadOnly Property Canceled() As Boolean
            Get
                Return _Canceled
            End Get
        End Property


        Public Sub CloseDropDown()
            _CloseDropDownCalled = True
            Me.Hide()
        End Sub


        Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
            MyBase.OnKeyDown(e)
            If (e.Modifiers = 0) AndAlso (e.KeyCode = Keys.Escape) Then
                Me.Hide()
            End If
        End Sub


        Protected Overrides Sub OnDeactivate(ByVal e As System.EventArgs)
            ' We set the Owner to Nothing BEFORE calling the base class.
            ' If we wouldn't do it, the parent form would lose focus after 
            ' this dropdown is closed.
            Me.Owner = Nothing

            MyBase.OnDeactivate(e)

            ' If the form was closed wasn't closed as a result of the CloseDropDown method call, it was because
            ' the user clicked outside the form, or pressed the ESC key.
            If Not _CloseDropDownCalled Then
                _Canceled = True
            End If
            Me.Hide()
        End Sub

    End Class


End Class
