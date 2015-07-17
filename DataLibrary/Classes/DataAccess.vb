Imports DataLibrary.ApRadarDatasetTableAdapters
Imports DataLibrary.ApRadarDataSet
Imports System.Reflection
Imports System.IO
Imports FFXIMemory
Imports System.Data.SqlServerCe


Public Class DataAccess
    Private Shared _isUpdating As Boolean = False

    Shared Sub New()
        'Lets verify the version
        
    End Sub

    Public Shared Sub Initialize()
        Try
            If Not MobData.IsInitialized Then
                MobData.BeginInit()
            End If
        Catch ex As System.Data.SqlServerCe.SqlCeInvalidDatabaseFormatException
            Dim engine As New SqlCeEngine(My.Settings.ApRadarDBConnectionString1)
            engine.Upgrade()
            Initialize()
        End Try
    End Sub

    Private Shared _mobData As ApRadarDataset
    Public Shared ReadOnly Property MobData() As ApRadarDataset
        Get
            If _mobData Is Nothing Then
                _mobData = New ApRadarDataSet
                MobsDataAdapter.Fill(_mobData.Mobs)
                ItemsDataAdapter.Fill(_mobData.Items)
                ItemsToMobsDataAdapter.Fill(_mobData.ItemsToMobs)
                SynthsDataAdapter.Fill(_mobData.Synths)
                PCDataAdapter.Fill(_mobData.PC)
                ToDDataAdapter.Fill(_mobData.ToD)
            End If
            Return _mobData
        End Get
    End Property

    Private Shared _DataManager As TableAdapterManager
    Public Shared ReadOnly Property DataManager() As TableAdapterManager
        Get
            If _DataManager Is Nothing Then
                _DataManager = New TableAdapterManager()
                _DataManager.ItemsTableAdapter = ItemsDataAdapter
                _DataManager.MobsTableAdapter = MobsDataAdapter
                _DataManager.ItemsToMobsTableAdapter = ItemsToMobsDataAdapter
                _DataManager.SynthsTableAdapter = SynthsDataAdapter
                _DataManager.PCTableAdapter = PCDataAdapter
                _DataManager.ToDTableAdapter = ToDDataAdapter
            End If
            Return _DataManager
        End Get
    End Property

    Private Shared _mobsDataAdapter As MobsTableAdapter
    Private Shared ReadOnly Property MobsDataAdapter() As MobsTableAdapter
        Get
            If _mobsDataAdapter Is Nothing Then
                _mobsDataAdapter = New MobsTableAdapter
            End If
            Return _mobsDataAdapter
        End Get
    End Property

    Private Shared _itemsDataAdapter As ItemsTableAdapter
    Private Shared ReadOnly Property ItemsDataAdapter() As ItemsTableAdapter
        Get
            If _itemsDataAdapter Is Nothing Then
                _itemsDataAdapter = New ItemsTableAdapter
            End If
            Return _itemsDataAdapter
        End Get
    End Property

    Private Shared _itemsToMobsDataAdapter As ItemsToMobsTableAdapter
    Private Shared ReadOnly Property ItemsToMobsDataAdapter() As ItemsToMobsTableAdapter
        Get
            If _itemsToMobsDataAdapter Is Nothing Then
                _itemsToMobsDataAdapter = New ItemsToMobsTableAdapter
            End If
            Return _itemsToMobsDataAdapter
        End Get
    End Property

    Private Shared _synthsDataAdapter As SynthsTableAdapter
    Private Shared ReadOnly Property SynthsDataAdapter() As SynthsTableAdapter
        Get
            If _synthsDataAdapter Is Nothing Then
                _synthsDataAdapter = New SynthsTableAdapter
            End If
            Return _synthsDataAdapter
        End Get
    End Property

    Private Shared _pcDataAdapter As PCTableAdapter
    Private Shared ReadOnly Property PCDataAdapter() As PCTableAdapter
        Get
            If _pcDataAdapter Is Nothing Then
                _pcDataAdapter = New PCTableAdapter
            End If
            Return _pcDataAdapter
        End Get
    End Property

    Private Shared _todDataAdapter As ToDTableAdapter
    Private Shared ReadOnly Property ToDDataAdapter() As ToDTableAdapter
        Get
            If _todDataAdapter Is Nothing Then
                _todDataAdapter = New ToDTableAdapter
            End If
            Return _todDataAdapter
        End Get
    End Property

    Public Shared Function AddMob(ByVal MobName As String, ByVal Zone As Integer, ByVal IsNPC As Boolean) As Integer
        Dim index As Integer
        Try
            Dim mobID As Integer = (From c In MobData.Mobs Where c.MobName = MobName And c.Zone = Zone _
                                    Select c.MobPK).FirstOrDefault
            If mobID < 1 Then
                Dim newPK = (From c In MobData.Mobs Select c.MobPK).Max + 1
                Dim m As ApRadarDataset.MobsRow = MobData.Mobs.NewRow
                m.MobPK = newPK
                m.MobName = MobName
                m.Zone = Zone
                m.Aggressive = False
                m.Links = False
                m.NM = False
                m.FishedUp = False
                m.TracksScent = False
                m.TrueSight = False
                m.TrueSound = False
                m.DetectsHealing = False
                m.DetectsLowHP = False
                m.DetectsMagic = False
                m.DetectsSight = False
                m.DetectsSound = False
                m.NPC = IsNPC
                MobData.Mobs.Rows.Add(m)
                DataManager.UpdateAll(MobData)
                index = newPK
            Else
                index = mobID
            End If
        Catch ex As Exception
            index = -1
        End Try
        Return index
    End Function

    Public Shared Function AddPC(ByVal PCName As String, ByVal ServerID As Integer) As Boolean
        Try
            Dim count As Integer = (From c In MobData.PC Where c.ServerID = ServerID).Count
            If count > 0 Then
                Return True
            Else
                Dim pc As ApRadarDataset.PCRow = MobData.PC.NewPCRow
                pc.ServerID = ServerID
                pc.PCName = PCName
                MobData.PC.AddPCRow(pc)
                DataManager.UpdateAll(MobData)
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function AddUpdateToDMob(ByVal MobID As Integer, ByVal Zone As Integer, ByVal IsDead As Boolean, ByVal ToD As DateTime) As ToDRow
        Try
            If Not _isUpdating Then
                _isUpdating = True
                Dim todR As ToDRow = (From c In MobData.ToD Where c.ServerID = MobID).FirstOrDefault
                If todR Is Nothing Then
                    todR = MobData.ToD.NewToDRow
                    todR.ServerID = MobID
                    todR.IsDead = IsDead
                    todR.Zone = Zone
                    If IsDead Then
                        todR.ToD = ToD
                    End If
                    MobData.ToD.AddToDRow(todR)
                    DataManager.UpdateAll(MobData)
                Else

                    If IsDead <> todR.IsDead Then
                        todR.IsDead = IsDead
                        If IsDead AndAlso Not todR.IsDead Then
                            todR.ToD = ToD
                        End If
                        DataManager.UpdateAll(MobData)
                    End If
                End If
                _isUpdating = False
                Return todR
            Else
                Return Nothing
            End If
        Catch ex As Exception
            'Debug.Print(Ex.Message)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetItemID(ByVal ItemName As String) As Integer?
        Return (From c In MobData.Items Where c.ItemName.ToLower = ItemName.ToLower OrElse c.LongName.ToLower = ItemName.ToLower Select c.ItemID).FirstOrDefault
    End Function

    Public Shared Function ItemExists(ByVal ItemID As Integer) As Boolean
        Return (From c In MobData.Items Where c.ItemID = ItemID).Count > 0
    End Function

    Public Shared Sub AddItem(ByVal Item As Item)
        Dim newPK As Integer = (From c In MobData.Items Select c.ItemPK).Max() + 1
        Dim newItem As ItemsRow = MobData.Items.NewItemsRow()
        newItem.ItemPK = newPK
        newItem.ItemID = Item.GetFieldValue("id")
        newItem.ItemLevel = Item.GetFieldValue("level")
        newItem.ItemName = Item.GetFieldValue("name")
        newItem.Jobs = Item.GetFieldText("jobs")
        newItem.LongName = Item.GetFieldText("log-name-singular")
        newItem.MaxCharges = Item.GetFieldValue("max-charges")
        newItem.Races = Item.GetFieldText("races")
        newItem.Rare = (ItemFlags.Rare And Item.GetFieldValue("flags")) = ItemFlags.Rare
        newItem.Ex = (ItemFlags.Ex And Item.GetFieldValue("flags")) = ItemFlags.Ex
        newItem.ReuseDelay = Item.GetFieldText("reuse-delay")
        newItem.Slots = Item.GetFieldText("slots")
        newItem.UseDelay = Item.GetFieldText("use-delay")
        newItem.CastingTime = Item.GetFieldText("casting-time")
        newItem.Description = Item.GetFieldText("description")
        newItem.Icon = Item.GetFieldText("icon")
        MobData.Items.AddItemsRow(newItem)
        DataManager.UpdateAll(MobData)
    End Sub

    'Public Shared Function UpdateTod(ByVal MobID As Integer, ByVal DeathTime As DateTime, ByVal Zone As Integer) As Boolean
    '    Try
    '        Dim tod As ToDRow = (From c In MobData.ToD Where c.ServerID = MobID).FirstOrDefault
    '        If tod Is Nothing Then
    '            tod = MobData.ToD.NewRow
    '            tod.ServerID = MobID
    '            tod.IsDead = True
    '            tod.ToD = DeathTime
    '            MobData.ToD.AddToDRow(tod)
    '        Else
    '            If tod.IsDead = 0 Then
    '                tod.IsDead = 1
    '                tod.ToD = DeathTime
    '            End If
    '        End If
    '        DataManager.UpdateAll(MobData)
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Public Shared Function GetToD(ByVal MobID As Integer, ByRef ts As TimeSpan) As Boolean
        Dim tod As ToDRow = (From c In MobData.ToD Where c.ServerID = MobID).FirstOrDefault
        If tod Is Nothing Then
            Return False
        Else
            If tod.IsDead Then
                If tod.IsToDNull Then
                    Return False
                Else
                    ts = DateTime.Now.Subtract(tod.ToD)
                    Return True
                End If
            Else
                Return False
            End If
        End If
        Return False
    End Function

    Public Shared Function GetResourceDataString(ByVal Resource As String) As String
        Dim Asm As [Assembly] = [Assembly].GetExecutingAssembly()

        ' Resources are named using a fully qualified name.
        Dim strm As Stream = Asm.GetManifestResourceStream(String.Format("{0}.{1}", Asm.GetName().Name, Resource))

        ' Reads the contents of the embedded file.
        Dim reader As StreamReader = New StreamReader(strm)
        Dim contents As String = reader.ReadToEnd()
        reader.Close()
        reader.Dispose()
        Return contents
    End Function

    Public Shared Function GetResourceDataStream(ByVal Resource As String) As Stream
        Dim Asm As [Assembly] = [Assembly].GetExecutingAssembly()

        ' Resources are named using a fully qualified name.
        Dim strm As Stream = Asm.GetManifestResourceStream(String.Format("{0}.{1}", Asm.GetName().Name, Resource))
        Return strm
    End Function
End Class
