Imports System.Runtime.InteropServices
Public Class WindowsAPI

    Public Const PROCESS_ALL_ACCESS = &H1F0FFF

    <DllImport("kernel32.dll")> _
    Public Shared Function ReadProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, <Out()> ByVal lpBuffer As Byte(), ByVal nSize As UIntPtr, ByVal lpNumberOfBytesRead As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function ReadProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As IntPtr, ByVal iSize As Integer, ByRef lpNumberOfBytesRead As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll")> _
    Public Shared Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As Byte(), ByVal nSize As UIntPtr, <Out()> ByRef lpNumberOfBytesWritten As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function OpenProcess(ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Boolean, ByVal dwProcessId As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function CloseHandle(ByVal handle As IntPtr) As Boolean
    End Function

    Public Shared Function Peek(ByVal proc As Process, ByVal target As Integer, ByVal data As Byte()) As Boolean
        Return WindowsAPI.ReadProcessMemory(proc.Handle, New IntPtr(target), data, New UIntPtr(CType(data.Length, UInt32)), New IntPtr(0))
    End Function

    Public Shared Function Poke(ByVal proc As Process, ByVal target As Integer, ByVal data As Byte()) As Boolean
        Return WindowsAPI.WriteProcessMemory(proc.Handle, New IntPtr(target), data, New UIntPtr(CType(data.Length, UInt32)), New IntPtr(0))
    End Function
End Class
