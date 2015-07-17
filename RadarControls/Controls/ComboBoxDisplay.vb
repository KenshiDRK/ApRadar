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
Imports System.Windows.Forms

' Implements a ComboBox-like appearance to be used with the ColorPicker control
' be means of the ComboBoxDisplayAdapter.
Public Class ComboBoxDisplay
    Inherits Control

    ' This flag tells us if we have to emulate a dropped-down appearance in the
    ' painting code.
    Private _HasDropDownAppearance As Boolean

    ' This is the currently selected color displayed
    Private _Color As Color

    ' The client area is partitioned into the following rectangles:
    Private _ColorBoxRect As Rectangle  ' the color box rectangle on the left
    Private _TextBoxRect As RectangleF  ' the rectangle for the color name in the middle
    Private _TextFormat As StringFormat ' format of the displayed color name
    Private _ButtonRect As Rectangle     ' the rectanngle for the drop-down button on the right

    ' This event is raised when the user changes the drop-down appearance of the
    ' control by clicking it, or by pressing the Alt+Down Arrow key combination.
    Public Event DropDownAppearanceChanged As EventHandler

    ' The size of the drop-down button image. It is calculated on demand in the DownButtonSize
    ' property procedure.
    Private Shared _DownButtonSize As New Size(-1, -1)

    Private _BorderedColorBox As Boolean = True


    Public Sub New()
        MyBase.New()
        Me.ForeColor = SystemColors.WindowText
        Me.BackColor = SystemColors.Window
        Me._TextFormat = New StringFormat(StringFormatFlags.NoWrap)
        Me._TextFormat.Alignment = StringAlignment.Near
        Me._TextFormat.LineAlignment = StringAlignment.Center
    End Sub


    Public Overridable Property Color() As Color
        Get
            Return Me._Color
        End Get
        Set(ByVal Value As Color)
            Me._Color = Value
            Me.Invalidate()
        End Set
    End Property


    Public Overridable Property HasDropDownAppearance() As Boolean
        Get
            Return Me._HasDropDownAppearance
        End Get
        Set(ByVal Value As Boolean)
            If Me._HasDropDownAppearance = Value Then
                Return
            End If
            Me._HasDropDownAppearance = Value
            Me.Invalidate()
            Me.OnDropDownAppearanceChanged(EventArgs.Empty)
        End Set
    End Property


    Public Property BorderedColorBox() As Boolean
        Get
            Return Me._BorderedColorBox
        End Get
        Set(ByVal Value As Boolean)
            Me._BorderedColorBox = Value
        End Set
    End Property


    Protected Overridable Sub OnDropDownAppearanceChanged(ByVal e As EventArgs)
        RaiseEvent DropDownAppearanceChanged(Me, EventArgs.Empty)
    End Sub


    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Try
            MyBase.OnPaint(e)

            Dim State As ButtonState = ButtonState.Normal
            If Me.HasDropDownAppearance Then
                State = ButtonState.Pushed
            End If

            Dim ColorBrush As New SolidBrush(Me.Color)
            Try
                e.Graphics.FillRectangle(ColorBrush, Me._ColorBoxRect)
            Finally
                ColorBrush.Dispose()
            End Try

            If Me.BorderedColorBox Then
                Dim OutlineRect As Rectangle = Me._ColorBoxRect
                OutlineRect.Width -= 1
                OutlineRect.Height -= 1
                e.Graphics.DrawRectangle(Pens.Black, OutlineRect)
            End If

            If Not Me._TextBoxRect.IsEmpty Then
                e.Graphics.DrawString(Me.Text, Me.Font, System.Drawing.Brushes.Black, Me._TextBoxRect, Me.TextFormat)
            End If

            ControlPaint.DrawComboButton(e.Graphics, Me._ButtonRect, State)
            ControlPaint.DrawBorder3D(e.Graphics, Me.ClientRectangle, Border3DStyle.Flat)

            If Me.ShouldDrawFocusRect Then
                ControlPaint.DrawFocusRectangle(e.Graphics, Me.ClientRectangle)
            End If

        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
        End Try
    End Sub


    Protected Overridable ReadOnly Property ShouldDrawFocusRect() As Boolean
        Get
            Return Me.HasDropDownAppearance OrElse Me.Parent.ContainsFocus
        End Get
    End Property


    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            ' Set focus to itself in the case we don't have focus yet. Originally I thought
            ' that the control regains focus automatically when clicked by the mouse, but it 
            ' didn't work so.
            If Not Me.Focused Then
                Me.Focus()
            End If
            MyBase.OnMouseDown(e)
            Me.HasDropDownAppearance = Not Me.HasDropDownAppearance
        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
        End Try
    End Sub


    ' This method is here for backward compatibility only. Use the DownButtonSize instead.
    Protected Overridable ReadOnly Property DownButtonWidth() As Integer
        Get
            Return Me.DownButtonSize.Width
        End Get
    End Property


    Protected Overridable ReadOnly Property DownButtonSize() As Size
        Get
            ' This doesn't work:
            '		Return SystemInformation.CaptionButtonSize
            ' On Windows 2000, the width is 18 pixels on my system, but on XP, it is 25 pixels,
            '	which is simply too much.

            ' Instead, we'll load the OEM bitmap, which also the native ComboBox control
            ' uses and we'll the size of the bitmap.

            ' Because this property is called frequently and because the bitmap loading seems to be
            ' expensive, we'll load the bitmap only once and cache the size in a shared member.

            If _DownButtonSize.Width > -1 Then          ' width already calculated, return it
                Return _DownButtonSize
            End If

            ' This is from WINUSER.H.
            Const OBM_DNARROW As Integer = 32752

            Dim hBitmap As IntPtr = ComboBoxDisplay.LoadBitmap(IntPtr.Zero, New IntPtr(OBM_DNARROW))
            If hBitmap.Equals(IntPtr.Zero) Then
                ' The bitmap couldn't be loaded, so we'll use a less precious replacement value.
                _DownButtonSize = SystemInformation.SmallIconSize
            Else
                Dim DownBitmap As Bitmap = System.Drawing.Bitmap.FromHbitmap(hBitmap)
                Try
                    _DownButtonSize = DownBitmap.Size
                Finally
                    DownBitmap.Dispose()
                End Try
            End If

            Return _DownButtonSize
        End Get
    End Property


    <System.Runtime.InteropServices.DllImport("user32", SetLastError:=True)> _
    Private Shared Function LoadBitmap( _
     ByVal hInstance As IntPtr, _
     ByVal pBitmapName As IntPtr) As IntPtr

    End Function


    Protected Overridable Property TextFormat() As StringFormat
        Get
            Return Me._TextFormat
        End Get
        Set(ByVal Value As StringFormat)
            If Value Is Nothing Then
                Throw New ArgumentNullException("Value")
            End If
            Me._TextFormat = Value
        End Set
    End Property


    Protected Overridable Sub GetDisplayLayout( _
     ByRef colorBoxRect As Rectangle, _
     ByRef textBoxRect As RectangleF, _
     ByRef buttonRect As Rectangle)

        Const DistanceFromEdge As Integer = 2

        ' The button occupies the right portion.
        buttonRect.X = Me.Width - Me.DownButtonSize.Width - DistanceFromEdge
        buttonRect.Y = DistanceFromEdge
        buttonRect.Size = New Size(Me.DownButtonSize.Width, Me.Height - 2 * DistanceFromEdge)

        If Len(Me.Text) = 0 Then
            ' No text to display, so we'll fill the whole client area  with the current color.
            colorBoxRect = Me.ClientRectangle
            textBoxRect = RectangleF.Empty
        Else
            ' The color box should occupy the left portion of the control. The color box width is 
            ' set to 1.5 * width of the drop-down button. The height is the same as of the button.
            colorBoxRect.X = DistanceFromEdge
            colorBoxRect.Y = DistanceFromEdge
            colorBoxRect.Size = New Size(CInt(1.5 * Me.DownButtonSize.Width), buttonRect.Height)

            ' The text occupies the middle portion.
            textBoxRect = RectangleF.FromLTRB( _
             colorBoxRect.Right + DistanceFromEdge, DistanceFromEdge, _
             buttonRect.Left - DistanceFromEdge, buttonRect.Bottom)
        End If
    End Sub


    Protected Overrides Sub OnLayout(ByVal levent As System.Windows.Forms.LayoutEventArgs)
        ' Recompute the display rectangles here, so they don't have to be recomputed 
        ' every time OnPaint is called by the system.
        Me.GetDisplayLayout(Me._ColorBoxRect, Me._TextBoxRect, Me._ButtonRect)
        MyBase.OnLayout(levent)
        Me.Invalidate()
    End Sub


    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)

        ' If the text changed, we have to recompute the display layout and repaint
        ' the control.
        Me.OnLayout(New LayoutEventArgs(Me, "Text"))
    End Sub


    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyData = (Keys.Down Or Keys.Alt) Then
            ' Alt+Down Arrow should switch the drop-down appearance just like the left mouse click.
            e.Handled = True
            Me.HasDropDownAppearance = Not Me.HasDropDownAppearance
        Else
            MyBase.OnKeyDown(e)
        End If
    End Sub

End Class
