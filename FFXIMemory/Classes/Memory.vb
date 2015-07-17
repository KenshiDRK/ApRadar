Imports System.Text
Imports System.Runtime.InteropServices
Imports System.IO

Public Class Memory

#Region " MEMBER VARIABLES "
    Private _Address As Integer
    Private _Process As Process
#End Region

#Region " CONSTRUCTORS "
    Public Sub New(ByVal [process] As Process, ByVal address As Integer)
        Me._Process = [process]
        Me._Address = address
    End Sub

    Public Sub New(ByVal ProcessName As String)
        Me._Process = findProcess(ProcessName)
    End Sub

    Public Sub New()
    End Sub
#End Region

#Region " FUNCTIONS "
    ''' <summary>
    ''' Finds the first instance of a process by it's name
    ''' </summary>
    ''' <param name="pName">The name of the process</param>
    ''' <returns>Process object</returns>
    ''' <remarks></remarks>
    Public Shared Function FindProcess(ByVal pName As String) As Process
        Dim p As Process() = Process.GetProcessesByName(pName)
        If p.Length > 0 Then
            Return p(0)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Gets a specified Process Module from a process by its name
    ''' </summary>
    ''' <param name="ModuleName">The module name</param>
    ''' <param name="ProcessInstance">The process to search</param>
    ''' <returns>Process Module</returns>
    ''' <remarks></remarks>
    Public Shared Function GetModule(ByVal ModuleName As String, ByVal ProcessInstance As Process) As ProcessModule
        Try
            Dim result = (From m In ProcessInstance.Modules.OfType(Of ProcessModule)() _
                          Where m.ModuleName = ModuleName Select m).FirstOrDefault
            Return result
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets the end address of the specifed process module
    ''' </summary>
    ''' <param name="ModuleName">The module name</param>
    ''' <param name="ProcessInstance">The process to search</param>
    ''' <returns>The end point of the specified module</returns>
    ''' <remarks></remarks>
    Public Shared Function GetModuleEndAddress(ByVal ModuleName As String, ByVal ProcessInstance As Process) As Integer
        Try
            Dim EndAddress = (From m In ProcessInstance.Modules.OfType(Of ProcessModule)() _
                              Where m.ModuleName.ToLower = ModuleName.ToLower Select _
                              m.BaseAddress.ToInt32 + m.ModuleMemorySize).FirstOrDefault
            Return EndAddress
        Catch ex As Exception
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Gets the base address of the specified process module
    ''' </summary>
    ''' <param name="ProcessInstance">The proces instance to search</param>
    ''' <param name="ModuleName">The name of the module</param>
    ''' <remarks></remarks>
    Private Sub getBaseAddress(ByVal ProcessInstance As Process, ByVal ModuleName As String)
        Dim address = (From m In ProcessInstance.Modules.OfType(Of ProcessModule)() Where _
                       m.FileName = ModuleName Select _
                       m.BaseAddress).FirstOrDefault
        Me._Address = address.ToInt32
    End Sub

    Public Function GetOpcode(ByVal Size As Integer) As String
        Dim buffer1 As Byte() = Me.GetByteArray(Size)
        Return BitConverter.ToString(buffer1)
    End Function

    Public Function GetByte() As Byte
        Dim data As Byte() = New Byte(1 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, data)
        Return data(0)
    End Function

    Public Function GetByteArray(ByVal Size As Integer) As Byte()
        Dim data As Byte() = New Byte(Size - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, data)
        Return data
    End Function

    Public Function GetStream(ByVal Size As Integer) As MemoryStream
        Dim ms As New MemoryStream
        Dim data As Byte() = New Byte(Size - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, data)
        ms.Write(data, 0, data.Length)
        ms.Position = 0
        Return ms
    End Function

    Public Function GetDouble() As Double
        Dim value As Byte() = New Byte(8 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, value)
        Return BitConverter.ToDouble(value, 0)
    End Function

    Public Function GetFloat() As Single
        Dim value As Byte() = New Byte(4 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, value)
        Return BitConverter.ToSingle(value, 0)
    End Function

    Public Function GetShort() As Short
        Dim value As Byte() = New Byte(2 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, value)
        Return BitConverter.ToInt16(value, 0)
    End Function

    Public Function GetInt32() As Integer
        Dim value As Byte() = New Byte(4 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, value)
        Return BitConverter.ToInt32(value, 0)
    End Function

    Public Function GetInt64() As Long
        Dim value As Byte() = New Byte(8 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, value)
        Return BitConverter.ToInt64(value, 0)
    End Function

    Public Function GetName() As String
        Dim bytes As Byte() = New Byte(24 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, bytes)
        For i As Integer = 0 To 19
            If bytes(i) = 0 Then
                Array.Resize(bytes, i)
                Exit For
            End If
        Next
        Return Encoding.ASCII.GetString(bytes)
    End Function

    Public Function GetProgram() As Integer
        Dim value As Byte() = New Byte(30 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, value)
        Return BitConverter.ToInt32(value, 0)
    End Function

    Public Shared Function GetStringFromBytes(ByVal bytes As Byte()) As String
        For i As Integer = 0 To bytes.Length - 1
            If bytes(i) = 0 Then
                Array.Resize(bytes, i)
                Exit For
            End If
        Next
        Return Encoding.UTF8.GetString(bytes)
    End Function

    Public Function GetString(ByVal MaxLength As Integer) As String
        Dim bytes As Byte() = New Byte(MaxLength - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, bytes)
        For i As Integer = 0 To MaxLength - 1
            If bytes(i) = 0 Then
                Array.Resize(bytes, i)
                Exit For
            End If
        Next
        Return Encoding.ASCII.GetString(bytes)
    End Function

    Public Function GetUInt32() As UInt32
        Dim value As Byte() = New Byte(4 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, value)
        Return BitConverter.ToUInt32(value, 0)
    End Function

    Public Function GetUInt16() As UInt16
        Dim value As Byte() = New Byte(4 - 1) {}
        WindowsAPI.Peek(Me._Process, Me._Address, value)
        Return BitConverter.ToUInt16(value, 0)
    End Function

    Public Function GetStructure(Of T)() As T
        Dim lpBytesWritten As Integer = 0
        Dim buffer As IntPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(GetType(T)))
        Dim pol As IntPtr = WindowsAPI.OpenProcess(WindowsAPI.PROCESS_ALL_ACCESS, False, Me._Process.Id)
        WindowsAPI.ReadProcessMemory(pol, New IntPtr(Me._Address), buffer, Marshal.SizeOf(GetType(T)), lpBytesWritten)
        Dim retValue = CType(Marshal.PtrToStructure(buffer, GetType(T)), T)
        Marshal.FreeCoTaskMem(buffer)
        Return retValue
    End Function

    Public Sub Reset(ByVal value As Integer)
        Dim data As Byte() = BitConverter.GetBytes(value)
        WindowsAPI.Poke(Me._Process, Me._Address, data)
    End Sub

    Public Sub SetByte(ByVal value As Byte)
        Dim data As Byte() = BitConverter.GetBytes(value)
        WindowsAPI.Poke(Me._Process, Me._Address, data)
    End Sub

    Public Sub SetByteArray(ByVal value As Byte())
        WindowsAPI.Poke(Me._Process, Me._Address, value)
    End Sub

    Public Sub SetDouble(ByVal value As Double)
        Dim data As Byte() = BitConverter.GetBytes(value)
        WindowsAPI.Poke(Me._Process, Me._Address, data)
    End Sub

    Public Sub SetFloat(ByVal value As Single)
        Dim data As Byte() = BitConverter.GetBytes(value)
        WindowsAPI.Poke(Me._Process, Me._Address, data)
    End Sub

    Public Sub SetShort(ByVal value As Short)
        Dim data As Byte() = BitConverter.GetBytes(value)
        WindowsAPI.Poke(Me._Process, Me._Address, data)
    End Sub

    Public Sub SetInt32(ByVal value As Integer)
        Dim data As Byte() = BitConverter.GetBytes(value)
        WindowsAPI.Poke(Me._Process, Me._Address, data)
    End Sub

    Public Sub SetInt64(ByVal value As Long)
        Dim data As Byte() = BitConverter.GetBytes(value)
        WindowsAPI.Poke(Me._Process, Me._Address, data)
    End Sub

    Public Sub SetUInt16(ByVal value As UInt16)
        Dim data As Byte() = BitConverter.GetBytes(value)
        WindowsAPI.Poke(Me._Process, Me._Address, data)
    End Sub

    Public Sub SetUInt32(ByVal value As UInt32)
        Dim data As Byte() = BitConverter.GetBytes(value)
        WindowsAPI.Poke(Me._Process, Me.Address, data)
    End Sub
#End Region

#Region " PROPERTIES "
    Public Property Address() As Integer
        Get
            Return Me._Address
        End Get
        Set(ByVal value As Integer)
            Me._Address = value
        End Set
    End Property
#End Region

End Class

