Public Class SubClassHandler
    Inherits System.Windows.Forms.NativeWindow
    Public Delegate Sub CallBackProcEventHandler(ByRef m As Message)
    Public Event CallBackProc As CallBackProcEventHandler

    Public Sub New(ByVal Handle As IntPtr)
        MyBase.AssignHandle(Handle)
    End Sub

    Public Property SubClassed() As Boolean

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If SubClassed Then
            RaiseEvent CallBackProc(m)
        End If
        MyBase.WndProc(m)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
