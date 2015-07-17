Imports Microsoft.Win32
Imports System.IO


Public Class FFXI
    Public Sub New(ByVal pol As Process)
        _pol = pol
    End Sub

    Public Sub New()
        _pol = Memory.FindProcess("pol")
    End Sub

    Private _pol As Process
    Public Property POL() As Process
        Get
            Return _pol
        End Get
        Set(ByVal value As Process)
            _pol = value
            If Not value Is Nothing Then
                _locs = New MemLocs(_pol).locs
            End If
        End Set
    End Property

    Private _locs As Hashtable
    Public Property MemLocs() As Hashtable
        Get
            If Not _pol Is Nothing Then
                If _locs Is Nothing AndAlso IsGameLoaded() Then
                    Dim ml As New MemLocs(_pol)
                    _locs = ml.locs
                End If
            End If
            Return _locs
        End Get
        Set(ByVal value As Hashtable)
            _locs = value
        End Set
    End Property

    Public Function IsGameLoaded() As Boolean
        If Not POL Is Nothing Then
            Try
                For Each m As ProcessModule In POL.Modules
                    If m.ModuleName = "FFXiMain.dll" Then
                        Return True
                    End If
                Next
            Catch ex As Exception

            End Try
            Return False
        Else
            Return False
        End If
    End Function

    Private Shared _itemDats As Hashtable
    Public Shared ReadOnly Property ItemDats As Hashtable
        Get
            If _itemDats Is Nothing Then
                _itemDats = New Hashtable()
                _itemDats.Add("Armor", 76)
                _itemDats.Add("Currency", 91)
                _itemDats.Add("GeneralItems", 73)
                _itemDats.Add("PuppetItems", 77)
                _itemDats.Add("UsableItems", 74)
                _itemDats.Add("Weapons", 75)
            End If
            Return _itemDats
        End Get
    End Property

    ''' <summary>
    ''' Gets Playonline Installation Folder Path
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetInstallPath() As String
        Dim text1 As String = String.Empty
        Dim key1 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\PlayOnlineUS\InstallFolder")
        If (key1 Is Nothing OrElse key1.GetValue("0001") Is Nothing) Then
            key1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\PlayOnlineEU\InstallFolder")
        End If
        If (key1 Is Nothing OrElse key1.GetValue("0001") Is Nothing) Then
            key1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\PlayOnline\InstallFolder")
        End If
        If (key1 Is Nothing OrElse key1.GetValue("0001") Is Nothing) Then
            key1 = Registry.LocalMachine.OpenSubKey("Software\Wow6432Node\PlayOnlineUS\InstallFolder")
        End If
        If (key1 Is Nothing OrElse key1.GetValue("0001") Is Nothing) Then
            key1 = Registry.LocalMachine.OpenSubKey("Software\Wow6432Node\PlayOnlineEU\InstallFolder")
        End If
        If (key1 Is Nothing OrElse key1.GetValue("0001") Is Nothing) Then
            key1 = Registry.LocalMachine.OpenSubKey("Software\Wow6432Node\PlayOnline\InstallFolder")
        End If
        If (((key1 IsNot Nothing)) AndAlso ((key1.GetValue("0001") IsNot Nothing))) Then
            text1 = DirectCast(key1.GetValue("0001"), String)
            Return text1.TrimEnd(New Char() {"\"c})
        End If
        Return String.Empty
    End Function

    Public Shared Function GetFilePath(ByVal FileNumber As Integer, ByRef App As Byte, ByRef Dir As Byte, ByRef File As Byte) As Boolean
        Dim DataRoot As String = GetInstallPath()
        For i As Byte = 1 To 9
            Dim Suffix As String = ""
            Dim DataDir As String = DataRoot
            If i > 1 Then
                Suffix = i.ToString()
                DataDir = Path.Combine(DataRoot, "Rom" & Suffix)
            End If
            Dim VTableFile As String = Path.Combine(DataDir, [String].Format("VTABLE{0}.DAT", Suffix))
            Dim FTableFile As String = Path.Combine(DataDir, [String].Format("FTABLE{0}.DAT", Suffix))
            If i = 1 Then
                ' add the Rom now (not needed for the *TABLE.DAT, but needed for the other DAT paths)
                DataDir = Path.Combine(DataRoot, "Rom")
            End If
            If System.IO.File.Exists(VTableFile) AndAlso System.IO.File.Exists(FTableFile) Then
                Try
                    Using VBR As New BinaryReader(New FileStream(VTableFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        If FileNumber < VBR.BaseStream.Length Then
                            VBR.BaseStream.Seek(FileNumber, SeekOrigin.Begin)
                            If VBR.ReadByte() = i Then
                                Dim FBR As New BinaryReader(New FileStream(FTableFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                                FBR.BaseStream.Seek(2 * FileNumber, SeekOrigin.Begin)
                                Dim FileDir As UShort = FBR.ReadUInt16()
                                App = CByte(i - 1)
                                Dir = CByte(FileDir \ &H80)
                                File = CByte(FileDir Mod &H80)
                                FBR.Close()
                                Return True
                            End If
                        End If
                        VBR.Close()
                    End Using
                Catch
                End Try
            End If
        Next
        App = 0
        Dir = 0
        File = 0
        Return False
    End Function

    Public Shared Function GetFilePath(ByVal App As Byte, ByVal Dir As Byte, ByVal File As Byte) As String
        Dim ROMDir As String = "Rom"
        If App > 0 Then
            App += 1
            ROMDir += App.ToString()
        End If
        Return Path.Combine(GetInstallPath, Path.Combine(ROMDir, Path.Combine(Dir.ToString(), Path.ChangeExtension(File.ToString(), ".dat"))))
    End Function

    Public Shared Function GetFilePath(ByVal FileNumber As Integer) As String
        Dim App As Byte = 0
        Dim Dir As Byte = 0
        Dim File As Byte = 0
        If Not FFXI.GetFilePath(FileNumber, App, Dir, File) Then
            Return Nothing
        End If
        Return FFXI.GetFilePath(App, Dir, File)
    End Function
End Class
