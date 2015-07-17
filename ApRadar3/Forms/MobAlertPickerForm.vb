Imports FFXIMemory
Imports System

Public Class MobAlertPickerForm
    Private _dock As DockingClass
    Public Event HuntListUpdated()
    Private _zone As Short

    Private _huntList As List(Of Short)
    Public ReadOnly Property HuntList() As List(Of Short)
        Get
            If _huntList Is Nothing Then
                _huntList = New List(Of Short)
            End If
            Return _huntList
        End Get
    End Property

    Public Sub New(ByVal ZoneID As Short)
        InitializeComponent()
        _zone = ZoneID
    End Sub

    Private Sub MobAlertPickerForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
        LoadHunts()
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
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

    Private Sub blnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles blnClose.Click
        Me.Close()
    End Sub

    Private Sub LoadHunts()
        Dim z As New Zones
        Dim mobs As List(Of FFXIMemory.ZoneMobs) = z.GetZoneMobList(_zone)

        mobs.Sort()
        Me.clbHuntList.Items.AddRange(mobs.ToArray())
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        HuntList.Clear()
        For Each item In Me.clbHuntList.CheckedItems
            Try
                HuntList.Add(CType(item, FFXIMemory.ZoneMobs).MobID)
            Catch ex As Exception

            End Try
        Next
        RaiseEvent HuntListUpdated()
    End Sub
End Class