Imports System.IO
Imports System.Reflection
Imports Microsoft.Win32
Imports System.Text

Public Class Zones

#Region " LOCAL VARIABLES "
    Private Const _mobEntrySize As Byte = 32
#End Region

#Region " CONSTRUCTOR "
    Public Sub New()
        LoadZoneList()
    End Sub
#End Region

#Region " PROPERTIES "
    Private _installPath As String = String.Empty
    Public Property InstallPath() As String
        Get
            If _installPath.Trim() = String.Empty Then
                _installPath = FFXI.GetInstallPath()
            End If
            Return _installPath
        End Get
        Set(ByVal value As String)
            _installPath = value
        End Set
    End Property

    Private _zones As List(Of Zone)
    Public ReadOnly Property ZoneList() As List(Of Zone)
        Get
            If _zones Is Nothing Then
                _zones = New List(Of Zone)
            End If
            Return _zones
        End Get
    End Property

    Private _nmList As List(Of NM)
    Private ReadOnly Property NMList As List(Of NM)
        Get
            If _nmList Is Nothing Then
                _nmList = Serializer.Deserialize(Of List(Of NM))(GetResourceStream("NotoriousMonsters.xml"))
            End If
            Return _nmList
        End Get
    End Property
#End Region

#Region " PUBLIC FUNCTIONS "
    Public Function GetZoneName(ByVal ZoneId As Integer) As String
        For Each zone In ZoneList
            If zone.ZoneID = ZoneId Then
                Return zone.ZoneName
            End If
        Next
        Return "Unknown Zone"
    End Function

    Public Function GetMobName(ByVal ZoneId As Integer, ByVal MobID As Integer) As String
        'For Each z As Zone In ZoneList
        '    If z.ZoneID = ZoneId Then
        '        Dim ml As List(Of MobData) = GetZoneMobList(z.Dats.ToArray)
        '        For Each md As MobData In ml
        '            If md.MobId = MobID Then
        '                Return md.MobName
        '            End If
        '        Next
        '    End If
        'Next
        Return "Unknown Mob"
    End Function

    Public Function GetNMListForZone(ByVal ZoneID As Short) As IEnumerable(Of NM)
        Return NMList.Where(Function(c) c.ZoneID = ZoneID)
    End Function

    Public Function GetAllNM() As List(Of NM)
        Return NMList
    End Function

    Public Function CheckIsNM(ByVal ZoneID As Integer, ByVal NMName As String) As Boolean
        Return NMList.Where(Function(c) c.ZoneID = ZoneID AndAlso c.Name.ToLower = NMName.ToLower).Count > 0
    End Function
#End Region

#Region " PRIVATE FUNCTIONS "
    Private Sub LoadZoneList()
        Dim xDoc As New Xml.XmlDocument
        xDoc.LoadXml(GetResourceData("ZoneList.xml"))
        Dim z As Zone
        For Each node As Xml.XmlNode In xDoc.GetElementsByTagName("zone")
            z = New Zone(node.Attributes("id").Value, node.Attributes("name").Value)
            For Each dNode As Xml.XmlNode In node.ChildNodes
                If dNode.InnerXml <> String.Empty Then
                    z.Dats.Add(dNode.InnerXml)
                End If
            Next
            ZoneList.Add(z)
        Next
        ZoneList.Sort()
    End Sub

    Private Sub LoadNMList()

    End Sub

    Private Function GetResourceData(ByVal Resource As String) As String
        Dim Asm As [Assembly] = [Assembly].GetExecutingAssembly()

        ' Resources are named using a fully qualified name.
        Dim strm As Stream = Asm.GetManifestResourceStream( _
        String.Format("{0}.{1}", Asm.GetName().Name, Resource))

        ' Reads the contents of the embedded file.
        Dim reader As StreamReader = New StreamReader(strm)
        Dim contents As String = reader.ReadToEnd()
        reader.Close()
        reader.Dispose()
        Return contents
    End Function

    Private Function GetResourceStream(ByVal Resource As String) As Stream
        Dim Asm As [Assembly] = [Assembly].GetExecutingAssembly()

        ' Resources are named using a fully qualified name.
        Dim strm As Stream = Asm.GetManifestResourceStream( _
        String.Format("{0}.{1}", Asm.GetName().Name, Resource))

        ' Reads the contents of the embedded file.
        Return strm
    End Function

    

    Public Function GetZoneMobList(ByVal ZoneID As Integer) As List(Of ZoneMobs)
        Dim md As New List(Of ZoneMobs)
        Dim fileData As Byte()
        Dim mobID As Integer
        Dim serverId As Integer
        Dim mobName As String
        Dim baseValue As Byte
        Dim isFirst As Boolean
        Dim datPaths As New List(Of String)

        Try

            datPaths = (From c In ZoneList Where c.ZoneID = ZoneID Select c.Dats).FirstOrDefault

            For Each dPath As String In datPaths
                dPath = String.Format("{0}{1}", InstallPath, dPath)
                fileData = IO.File.ReadAllBytes(dPath)
                isFirst = True
                For i As Integer = _mobEntrySize To fileData.Count - 1 Step _mobEntrySize
                    If i + _mobEntrySize < fileData.Count Then
                        mobName = GetString(fileData.ToArray, i)
                        If isFirst Then
                            baseValue = fileData(i + _mobEntrySize - 3)
                            isFirst = False
                        End If
                        serverId = BitConverter.ToInt32(fileData, i + _mobEntrySize - 4)

                        mobID = BitConverter.ToInt16(fileData, i + _mobEntrySize - 4) - BitConverter.ToInt16(New Byte() {0, baseValue}, 0)
                        If mobName <> String.Empty Then
                            md.Add(New ZoneMobs(mobName, mobID, serverId))
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            'Debug.Print(Ex.Message)
        End Try
        Return md
    End Function

    Public Function GetZoneMobCount(ByVal ZoneID As Integer) As Integer
        Dim datPaths As New List(Of String)
        Dim totalMobs As Integer = 0
        Try

            For Each z As Zone In ZoneList
                If z.ZoneID = ZoneID Then
                    datPaths = z.Dats
                    Exit For
                End If
            Next
            For Each dPath As String In datPaths
                dPath = String.Format("{0}{1}", InstallPath, dPath)
                Dim fInfo As New FileInfo(dPath)
                totalMobs += (fInfo.Length / _mobEntrySize) - 1
            Next
        Catch ex As Exception
            ''Debug.Print(Ex.Message)
        End Try
        Return totalMobs
    End Function

    Private Function GetString(ByVal Arr As Byte(), ByVal StartIndex As Integer) As String
        Dim b As Byte
        Dim nameArr As New List(Of Byte)
        For i = 0 To 23
            b = Arr(StartIndex + i)
            If b <> 0 Then
                nameArr.Add(b)
            Else
                Exit For
            End If
        Next
        Return System.Text.Encoding.GetEncoding("shift-jis").GetString(nameArr.ToArray)
    End Function
#End Region

#Region " SUBCLASSES "
    Public Class Zone
        Implements IComparable(Of Zone)

        Public Sub New(ByVal Id As Integer, ByVal Name As String)
            ZoneID = Id
            ZoneName = Name
        End Sub
        Public Property ZoneID() As Integer
        Public Property ZoneName() As String

        Private _dats As List(Of String)
        Public ReadOnly Property Dats() As List(Of String)
            Get
                If _dats Is Nothing Then
                    _dats = New List(Of String)
                End If
                Return _dats
            End Get
        End Property

        Public Function CompareTo(ByVal other As Zone) As Integer Implements System.IComparable(Of Zone).CompareTo
            Return Me.ZoneName.CompareTo(other.ZoneName)
        End Function
    End Class
#End Region
End Class
