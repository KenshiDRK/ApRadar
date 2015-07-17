Imports System.Runtime.InteropServices

Public Class FormAnimator
#Region " ENUM "

#End Region

    <DllImport("User32", CharSet:=CharSet.Auto)> _
    Public Shared Function AnimateWindow(ByVal hwnd As IntPtr, ByVal time As Integer, ByVal flags As AnimateWindowFlags) As Boolean
    End Function

    Private _animateForm As Form
    Public Sub New(ByVal AnimateForm As Form)
        _animateForm = AnimateForm
    End Sub

    Public Sub SlideOut(ByVal Interval As Integer, ByVal Direction As SlideDirection)
        Dim flags As AnimateWindowFlags
        Select Case Direction
            Case SlideDirection.Up
                flags = AnimateWindowFlags.AW_VER_NEGATIVE
            Case SlideDirection.Down
                flags = AnimateWindowFlags.AW_VER_POSITIVE
            Case SlideDirection.Right
                flags = AnimateWindowFlags.AW_HOR_POSITIVE
            Case SlideDirection.Left
                flags = AnimateWindowFlags.AW_HOR_NEGATIVE
            Case SlideDirection.DiagonalFromTopLeft
                flags = AnimateWindowFlags.AW_HOR_POSITIVE Or AnimateWindowFlags.AW_VER_POSITIVE
            Case SlideDirection.DiagonalFromTopRight
                flags = AnimateWindowFlags.AW_HOR_NEGATIVE Or AnimateWindowFlags.AW_VER_POSITIVE
            Case SlideDirection.DiagonalFromBottomLeft
                flags = AnimateWindowFlags.AW_HOR_POSITIVE Or AnimateWindowFlags.AW_VER_NEGATIVE
            Case SlideDirection.DiagonalFromBottomRight
                flags = AnimateWindowFlags.AW_HOR_NEGATIVE Or AnimateWindowFlags.AW_VER_NEGATIVE
        End Select
        AnimateWindow(_animateForm.Handle, Interval, flags Or AnimateWindowFlags.AW_SLIDE)
    End Sub

    Public Sub RollUp(ByVal Interval As Integer, ByVal Direction As SlideDirection)
        Dim flags As AnimateWindowFlags
        Select Case Direction
            Case SlideDirection.Up
                flags = AnimateWindowFlags.AW_VER_POSITIVE
            Case SlideDirection.Down
                flags = AnimateWindowFlags.AW_VER_NEGATIVE
            Case SlideDirection.Right
                flags = AnimateWindowFlags.AW_HOR_NEGATIVE
            Case SlideDirection.Left
                flags = AnimateWindowFlags.AW_HOR_POSITIVE
            Case SlideDirection.DiagonalFromTopLeft
                flags = AnimateWindowFlags.AW_HOR_NEGATIVE Or AnimateWindowFlags.AW_VER_NEGATIVE
            Case SlideDirection.DiagonalFromTopRight
                flags = AnimateWindowFlags.AW_HOR_POSITIVE Or AnimateWindowFlags.AW_VER_NEGATIVE
            Case SlideDirection.DiagonalFromBottomLeft
                flags = AnimateWindowFlags.AW_HOR_NEGATIVE Or AnimateWindowFlags.AW_VER_POSITIVE
            Case SlideDirection.DiagonalFromBottomRight
                flags = AnimateWindowFlags.AW_HOR_POSITIVE Or AnimateWindowFlags.AW_VER_POSITIVE
        End Select
        AnimateWindow(_animateForm.Handle, Interval, flags Or AnimateWindowFlags.AW_HIDE Or AnimateWindowFlags.AW_SLIDE)
    End Sub

    Public Sub FadeIn(ByVal Interval As Integer)
        AnimateWindow(_animateForm.Handle, Interval, AnimateWindowFlags.AW_BLEND)
    End Sub

    Public Sub FadeOut(ByVal Interval As Integer)
        AnimateWindow(_animateForm.Handle, Interval, AnimateWindowFlags.AW_HIDE Or AnimateWindowFlags.AW_BLEND)
    End Sub

    Public Sub MoveDown(ByVal Interval As Integer)
        AnimateWindow(_animateForm.Handle, Interval, AnimateWindowFlags.AW_CENTER Or AnimateWindowFlags.AW_VER_POSITIVE Or AnimateWindowFlags.AW_SLIDE)
    End Sub
End Class
