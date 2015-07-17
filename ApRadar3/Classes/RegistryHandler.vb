Imports Microsoft.Win32

Public Class RegistryHandler


    Public Shared Function GetRegValue(ByVal regHive As RegistryHive, ByVal subKey As String, ByVal keyName As String) As Object
        Try
            Dim uiptrKey As New UIntPtr(0)
            If RegOpenKeyEx(regHive, subKey, 0, &H20119, uiptrKey) <> 0 Then
                Throw New Exception("Failed to open registry key.")
            End If

            Dim regType As RegType = regType.REG_NAME
            Dim nSize As Integer = 0

            Dim nResult As Integer = RegQueryValueEx(uiptrKey, keyName, IntPtr.Zero, regType, IntPtr.Zero, nSize)
            If nResult <> 0 Then
                RegCloseKey(uiptrKey)
                Throw New Exception("Failed to query the registry value informaiton.")
            End If

            ' Query Key For Data
            Dim btData As Byte() = New Byte(nSize - 1) {}
            nResult = RegQueryValueEx(uiptrKey, keyName, IntPtr.Zero, regType, btData, nSize)
            If nResult <> 0 Then
                RegCloseKey(uiptrKey)
                Throw New Exception("Failed to query registry value information.")
            End If

            ' Close Key
            RegCloseKey(uiptrKey)

            Return DirectCast(btData, Object)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '' Basis from http://blogs.msdn.com/cumgranosalis/archive/2005/12/09/Win64RegistryPart1.aspx
    'Private Function GetRegistryKeyHandle(ByVal RegisteryKey As Microsoft.Win32.RegistryKey) As System.IntPtr
    '    Dim Type As System.Type = System.Type.GetType("Microsoft.Win32.RegistryKey")
    '    Dim Info As System.Reflection.FieldInfo = Type.GetField("hkey", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)

    '    Dim Handle As System.Runtime.InteropServices.SafeHandle = CType(Info.GetValue(RegisteryKey), System.Runtime.InteropServices.SafeHandle)
    '    Dim RealHandle As System.IntPtr = Handle.DangerousGetHandle

    '    Return Handle.DangerousGetHandle
    'End Function

    '' Basis from: http://www.pinvoke.net/default.aspx/advapi32/RegOpenKeyEx.html
    'Public Enum RegWow64Options As System.Int32
    '    None = 0
    '    KEY_WOW64_64KEY = &H100
    '    KEY_WOW64_32KEY = &H200
    'End Enum

    'Private Enum RegistryRights As System.Int32
    '    ReadKey = 131097
    '    WriteKey = 131078
    'End Enum

    'Private Declare Auto Function RegOpenKeyEx Lib "advapi32.dll" (ByVal hKey As System.IntPtr, ByVal lpSubKey As System.String, ByVal ulOptions As System.Int32, ByVal samDesired As System.Int32, ByRef phkResult As System.Int32) As System.Int32

    'Public Function OpenSubKey(ByVal ParentKey As Microsoft.Win32.RegistryKey, ByVal SubKeyName As String, ByVal Writeable As Boolean, ByVal Options As RegWow64Options) As Microsoft.Win32.RegistryKey
    '    If ParentKey Is Nothing OrElse GetRegistryKeyHandle(ParentKey).Equals(System.IntPtr.Zero) Then
    '        Throw New System.Exception("OpenSubKey: Parent key is not open")
    '    End If

    '    Dim Rights As System.Int32 = RegistryRights.ReadKey
    '    If Writeable Then
    '        Rights = RegistryRights.WriteKey
    '    End If

    '    Dim SubKeyHandle As System.Int32
    '    Dim Result As System.Int32 = RegOpenKeyEx(GetRegistryKeyHandle(ParentKey), SubKeyName, 0, Rights Or Options, SubKeyHandle)
    '    If Result <> 0 Then
    '        Dim W32ex As New System.ComponentModel.Win32Exception()
    '        Throw New System.Exception("OpenSubKey: Exception encountered opening key", W32ex)
    '    End If

    '    Return PointerToRegistryKey(CType(SubKeyHandle, System.IntPtr), Writeable, False)
    'End Function

    'Private Function PointerToRegistryKey(ByVal hKey As System.IntPtr, ByVal writable As Boolean, ByVal ownsHandle As Boolean) As Microsoft.Win32.RegistryKey
    '    ' Create a SafeHandles.SafeRegistryHandle from this pointer - this is a private class
    '    Dim privateConstructors As System.Reflection.BindingFlags = System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic
    '    Dim safeRegistryHandleType As System.Type = GetType(Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid).Assembly.GetType("Microsoft.Win32.SafeHandles.SafeRegistryHandle")
    '    Dim safeRegistryHandleConstructorTypes As System.Type() = {GetType(System.IntPtr), GetType(System.Boolean)}
    '    Dim safeRegistryHandleConstructor As System.Reflection.ConstructorInfo = safeRegistryHandleType.GetConstructor(privateConstructors, Nothing, safeRegistryHandleConstructorTypes, Nothing)
    '    Dim safeHandle As System.Object = safeRegistryHandleConstructor.Invoke(New System.Object() {hKey, ownsHandle})

    '    ' Create a new Registry key using the private constructor using the safeHandle - this should then behave like a .NET natively opened handle and disposed of correctly
    '    Dim registryKeyType As System.Type = GetType(Microsoft.Win32.RegistryKey)
    '    Dim registryKeyConstructorTypes As System.Type() = {safeRegistryHandleType, GetType(System.Boolean)}
    '    Dim registryKeyConstructor As System.Reflection.ConstructorInfo = registryKeyType.GetConstructor(privateConstructors, Nothing, registryKeyConstructorTypes, Nothing)
    '    Dim result As Microsoft.Win32.RegistryKey = DirectCast(registryKeyConstructor.Invoke(New Object() {safeHandle, writable}), Microsoft.Win32.RegistryKey)
    '    Return result
    'End Function
End Class
