Imports FFXIMemory
Imports System.Data.Odbc
Public Class SpawnAlertDialog
    Inherits ResizableForm

    Private _dock As DockingClass
    Private _zoneID As Integer = -1
    Private _mobs As List(Of ZoneMobs)
    Private _isGlobal As Boolean = False
    Private _isLoading As Boolean

    Private _spawnList As List(Of Integer)
    Public Property SpawnList As List(Of Integer)
        Get
            If _spawnList Is Nothing Then
                _spawnList = New List(Of Integer)
            End If
            Return _spawnList
        End Get
        Set(ByVal value As List(Of Integer))
            _spawnList = value
        End Set
    End Property

    Private _globalWatch As List(Of Integer)
    Public Property GlobalWatch As List(Of Integer)
        Get
            If _globalWatch Is Nothing Then
                _globalWatch = New List(Of Integer)
            End If
            Return _globalWatch
        End Get
        Set(ByVal value As List(Of Integer))
            _globalWatch = value
        End Set
    End Property

    Private _zones As FFXIMemory.Zones
    Private ReadOnly Property Zones As Zones
        Get
            If _zones Is Nothing Then
                _zones = New Zones
            End If
            Return _zones
        End Get
    End Property

    Public Sub New(ByVal ZoneId As Integer)
        _zoneID = ZoneId
        InitializeComponent()
        Me.dgSpawnAlerts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
    End Sub

    Private Sub SpawnAlertDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dgSpawnAlerts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)
        LoadZones()
        If _zoneID = 0 Then
            Me.cboZones.SelectedIndex = 0
        Else
            Me.cboZones.SelectedValue = _zoneID
        End If
    End Sub

    Private Sub clbMobList_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs)
        If Not _isGlobal AndAlso Not _isLoading Then
            If e.NewValue = CheckState.Unchecked Then
                SpawnList.Remove(_mobs(e.Index).ServerID)
            ElseIf e.NewValue = CheckState.Checked Then
                If Not SpawnList.Contains(_mobs(e.Index).ServerID) Then
                    SpawnList.Add(_mobs(e.Index).ServerID)
                End If
            End If
        End If
    End Sub

    Private Sub HeaderPanel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseDown, lblHeder.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.StartDockDrag(e.X, e.Y)
        End If
    End Sub

    Private Sub HeaderPanel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseMove, lblHeder.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgSpawnAlerts_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgSpawnAlerts.CellValueChanged
        If Not _isGlobal AndAlso Not _isLoading AndAlso e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 Then
            'We are in the checkbox column
            Dim md As ZoneMobs = TryCast(dgSpawnAlerts.Rows(e.RowIndex).DataBoundItem, ZoneMobs)
            If dgSpawnAlerts.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Then
                If Not SpawnList.Contains(md.ServerID) Then
                    SpawnList.Add(md.ServerID)
                End If
            Else
                SpawnList.Remove(md.ServerID)
            End If
        End If
    End Sub

    Private Sub dgSpawnAlerts_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgSpawnAlerts.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim rowIndex As Integer = dgSpawnAlerts.HitTest(e.X, e.Y).RowIndex
            If rowIndex > -1 Then
                dgSpawnAlerts.CurrentCell = dgSpawnAlerts.Rows(rowIndex).Cells(1)
                Dim item = dgSpawnAlerts.Rows(rowIndex).DataBoundItem
                If Not item Is Nothing Then
                    If GlobalWatch.Contains(item.ServerID) Then
                        tsRemove.Enabled = True
                        tsAdd.Enabled = False
                    Else
                        tsRemove.Enabled = False
                        tsAdd.Enabled = True
                    End If
                End If
                cmRightClick.Show(dgSpawnAlerts, e.Location)
            End If
        End If
    End Sub

    Private Sub tsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsAdd.Click
        Dim md As ZoneMobs = TryCast(dgSpawnAlerts.CurrentRow.DataBoundItem, ZoneMobs)
        If Not md Is Nothing Then
            If Not GlobalWatch.Contains(md.ServerID) Then
                GlobalWatch.Add(md.ServerID)
                _isGlobal = True
                dgSpawnAlerts.CurrentRow.Cells("ColAlert").Value = True
                _isGlobal = False
            End If
        End If
    End Sub

    Private Sub tsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsRemove.Click
        Dim md As ZoneMobs = TryCast(dgSpawnAlerts.CurrentRow.DataBoundItem, ZoneMobs)
        If Not md Is Nothing Then
            GlobalWatch.Remove(md.ServerID)
            _isGlobal = True
            dgSpawnAlerts.CurrentRow.Cells("ColAlert").Value = False
            _isGlobal = False
        End If
    End Sub

    Private Sub cboZones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZones.SelectedIndexChanged
        _isLoading = True
        _mobs = Zones.GetZoneMobList(Me.cboZones.SelectedValue)
        _mobs.Sort()
        Dim sl As New SortableBindingList(Of ZoneMobs)(_mobs)
        Me.dgSpawnAlerts.DataSource = sl
        For i = 0 To _mobs.Count - 1
            If SpawnList.Contains(_mobs(i).ServerID) Then
                Me.dgSpawnAlerts.Rows(i).Cells("ColAlert").Value = True
            ElseIf GlobalWatch.Contains(_mobs(i).ServerID) Then
                _isGlobal = True
                Me.dgSpawnAlerts.Rows(i).Cells("ColAlert").Value = True
                _isGlobal = False
            End If
        Next
        If cboZones.SelectedIndex > -1 AndAlso _mobs.Count < 1 Then
            MessageBox.Show("No mobs were loaded.  ApRadar checked the following path for your FFXI installation: " & Zones.InstallPath, "No Mobs Found", MessageBoxButtons.OK)
        End If
        _isLoading = False
    End Sub

    Private Sub LoadZones()
        Me.cboZones.BeginUpdate()
        Me.cboZones.DisplayMember = "ZoneName"
        Me.cboZones.ValueMember = "ZoneID"
        Me.cboZones.DataSource = Zones.ZoneList
        Me.cboZones.SelectedIndex = -1
        Me.cboZones.EndUpdate()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub

    Private Sub cmdAddAllNM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddAllNM.Click

        Using NMTable As New DataLibrary.NMList.NotoriousMonstersDataTable()
            NMTable.ReadXml(DataLibrary.DataAccess.GetResourceDataStream("NMList.xml"))
           
            Dim mobs As List(Of ZoneMobs)
            Dim count As Integer = 0
            If rbCurrentZone.Checked Then
                Dim zoneID As Integer = Me.cboZones.SelectedValue
                mobs = Zones.GetZoneMobList(zoneID)
                Dim ZoneNm = (From c In NMTable _
                        Where c.Zone = zoneID _
                        Select c.Name).ToList
                For Each mob In mobs
                    If ZoneNm.Contains(mob.MobName) AndAlso Not GlobalWatch.Contains(mob.ServerID) Then
                        GlobalWatch.Add(mob.ServerID)
                        count += 1
                    End If
                Next

                
            Else
                Me.pbScan.Visible = True

                Dim list As List(Of FFXIMemory.Zones.Zone) = Zones.ZoneList
                Dim i As Integer = 0
                Dim thisZone As Short
                For Each z In list
                    thisZone = z.ZoneID
                    mobs = Zones.GetZoneMobList(z.ZoneID)
                    Dim ZoneNm = (From c In NMTable _
                        Where c.Zone = thisZone _
                        Select c.Name).ToList
                    For Each mob In mobs
                        If ZoneNm.Contains(mob.MobName) AndAlso Not GlobalWatch.Contains(mob.ServerID) Then
                            GlobalWatch.Add(mob.ServerID)
                            count += 1
                        End If
                    Next
                    i += 1
                    Me.pbScan.Value = i / list.Count * 100
                Next

            End If

            'Check off anymobs that are in the selected zone
            _isGlobal = True
            Dim zm As ZoneMobs
            For Each row As DataGridViewRow In dgSpawnAlerts.Rows
                zm = TryCast(row.DataBoundItem, ZoneMobs)
                If Not zm Is Nothing Then
                    If GlobalWatch.Contains(zm.ServerID) Then
                        row.Cells(0).Value = True
                    End If
                End If
            Next
            _isGlobal = False

            Me.pbScan.Visible = False
            MessageBox.Show(String.Format("{0} NM's successfully added to the Global Watch List!", count), "Process Complete!")
        End Using


    End Sub

    Private Sub CreateNMListXML()
        Dim path As String = IO.Path.Combine(Application.StartupPath, "NMFullData.csv")
        If IO.File.Exists(path) Then
            Dim connString As String = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" & IO.Path.GetDirectoryName(path) & "\;Extensions=asc,csv,tab,txt;"
            Using conn As New OdbcConnection(connString)
                Dim cmd As New OdbcCommand(String.Format("SELECT * FROM {0} WHERE Zone IS NOT NULL", IO.Path.GetFileName(path)), conn)
                Dim da As New OdbcDataAdapter(cmd)
                conn.Open()
                Using NMTable As New DataLibrary.NMList.NotoriousMonstersDataTable()
                    da.Fill(NMTable)
                    conn.Close()
                    Dim zoneName As String
                    For Each dr As DataRow In NMTable.Rows
                        zoneName = dr.Item("Zone")
                        dr.Item("Zone") = (From c In Zones.ZoneList _
                        Where c.ZoneName = zoneName _
                        Select c.ZoneID).FirstOrDefault
                    Next
                    'Write the sml to file to be added to the resources
                    NMTable.WriteXml(Application.StartupPath & "\NMList.XML")
                End Using
            End Using
        End If
    End Sub
End Class