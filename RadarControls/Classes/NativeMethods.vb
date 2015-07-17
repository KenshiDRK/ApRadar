Imports System.Runtime.InteropServices
Public Class NativeMethods
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure POINT
        Public x As Integer
        Public y As Integer
    End Structure
End Class
