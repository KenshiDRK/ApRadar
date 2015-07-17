Imports FFXIMemory
Imports DataLibrary
Imports DataLibrary.ApRadarDataset
Imports System.Text.RegularExpressions

Public Class DataBrowserForm
#Region " MEMBER VARIABLES "
    Private _isLoading As Boolean = True
    Private _isloadingItems As Boolean
    Private _dock As DockingClass
    Private _animator As FormAnimator
    Private _zone As Integer = -1
    Private _mobID As Integer
    Private _currentMob As MobsRow
#End Region

#Region " PROPERTIES "
    Private _zones As Zones
    Public ReadOnly Property Zones() As Zones
        Get
            If _zones Is Nothing Then
                _zones = New Zones
            End If
            Return _zones
        End Get
    End Property

    Private _currentZone As Short = 0
    Public Property CurrentZone() As Short
        Get
            Return _currentZone
        End Get
        Set(ByVal value As Short)
            _currentZone = value
            Me.cboZones.SelectedValue = value
        End Set
    End Property
#End Region

#Region " CONSTRUCTORS "
    Public Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(ByVal ZoneID As Integer)
        InitializeComponent()
        CurrentZone = ZoneID
    End Sub

#End Region

#Region " FORM ACTIONS "

    Private Sub DataBrowserForm_ForeColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ForeColorChanged
        UpdateControlColors(Me)
    End Sub

    Private Sub DataBrowserForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Opacity = 1
        If _dock.DockedLocation.Contains(DockingClass.DockPosition.Top) Then
            _animator.RollUp(150, SlideDirection.Down)
        ElseIf _dock.DockedLocation.Contains(DockingClass.DockPosition.Bottom) Then
            _animator.RollUp(150, SlideDirection.Up)
        ElseIf _dock.DockedLocation.Contains(DockingClass.DockPosition.Left) Then
            _animator.RollUp(150, SlideDirection.Right)
        ElseIf _dock.DockedLocation.Contains(DockingClass.DockPosition.Right) Then
            _animator.RollUp(150, SlideDirection.Left)
        Else
            _animator.FadeOut(500)
        End If
        MobsBindingSource.RemoveFilter()
    End Sub

    Private Sub DataBrowserForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)
        _dock.UseDocking = True

        LoadZones()
        If CurrentZone >= 0 Then
            Me.cboZones.SelectedValue = _zone
        End If
        MobsBindingSource.DataSource = DataAccess.MobData.Mobs
        _animator = New FormAnimator(Me)

        If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
            _dock.DockedLocation.Add(DockingClass.DockPosition.Top)
            _animator.SlideOut(150, SlideDirection.Down)
        Else
            _dock.DockedLocation.Add(DockingClass.DockPosition.Bottom)
            _animator.SlideOut(150, SlideDirection.Up)
        End If

        _isLoading = False
        Me.TopMost = True
        Me.BringToFront()
        Me.TopMost = False
        If _mobID > 0 Then
            Me.cboMobs.SelectedValue = _mobID
        Else
            Me.cboMobs.SelectedIndex = -1
        End If
        LoadItems()
    End Sub
#End Region

#Region " CONTROLS "
    Private Sub cboZones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZones.SelectedIndexChanged
        If Not _isLoading AndAlso Me.cboZones.SelectedIndex > -1 Then
            Dim zone As Integer = Me.cboZones.SelectedValue
            MobsBindingSource.Filter = String.Format("Zone={0}", zone)
            LoadMobs()
            LoadItems()
        Else
            MobsBindingSource.RemoveFilter()
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

    Private Sub cboMobs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMobs.SelectedIndexChanged
        If Not _isLoading AndAlso Me.cboMobs.SelectedIndex > -1 Then
            LoadItems()
        End If
    End Sub

    Private Sub cmdAddItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddItems.Click
        If Me.cboMobs.SelectedIndex > -1 Then
            Dim MobID As Integer = Me.cboMobs.SelectedValue
            Dim aid As New AddItemDialog(Me.cboMobs.SelectedValue)
            aid.Location = New Point(Me.Right, Me.Bottom - aid.Height)
            If aid.ShowDialog = Windows.Forms.DialogResult.OK Then
                For Each item As Integer In aid.Items
                    DataAccess.MobData.ItemsToMobs.Rows.Add(MobID, item)
                Next
                DataAccess.DataManager.UpdateAll(DataAccess.MobData)
                LoadMobItems(MobID)
            End If
        Else
            Me.cboMobs.DataSource = Nothing
        End If
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        If Me.lstItems.SelectedItems.Count > 0 Then

            Dim itemID As Integer
            Dim mobID As Integer = Me.cboMobs.SelectedValue
            itemID = Me.lstItems.SelectedValue
            Dim link As ItemsToMobsRow = (From c In DataAccess.MobData.ItemsToMobs Where c.MobPK = mobID And c.ItemID = itemID).FirstOrDefault

            'itm = DataAccess.MobData.ItemsToMobs.Select(String.Format("MobPK = {0} AND ItemPK = {1}", mobID, itemID)).FirstOrDefault
            If Not link Is Nothing Then
                link.Delete()
            End If

            DataAccess.DataManager.UpdateAll(DataAccess.MobData)
            DataAccess.MobData.AcceptChanges()
            LoadMobItems(mobID)
        End If
    End Sub

    Private Sub cmdDeleteMob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteMob.Click
        If Me.cboMobs.SelectedIndex > -1 Then

            Dim mob As MobsRow = (From mobs In DataAccess.MobData.Mobs Where _
                                  mobs.MobPK = CInt(Me.cboMobs.SelectedValue)).FirstOrDefault
            Dim mPK As Integer = mob.MobPK
            Dim itms = From c In DataAccess.MobData.ItemsToMobs Where c.MobPK = mPK
            For Each item In itms
                item.Delete()
            Next
            mob.Delete()
            DataAccess.DataManager.UpdateAll(DataAccess.MobData)
        End If
    End Sub

    Private Sub lstItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstItems.SelectedIndexChanged
        If Not _isloadingItems AndAlso Me.lstItems.SelectedIndex > -1 Then
            Dim itemID As Integer = Me.lstItems.SelectedValue
            Dim item = (From items In DataAccess.MobData.Items Where _
                        items.ItemID = itemID).FirstOrDefault
            If Not item Is Nothing Then
                BuildItemDisplay(item)
            Else
                ClearItemInfo()
            End If
        End If
    End Sub
#End Region

#Region " FUNCTIONS "
    Private Sub LoadZones()
        Me.cboZones.BeginUpdate()
        Me.cboZones.DisplayMember = "ZoneName"
        Me.cboZones.ValueMember = "ZoneID"
        Me.cboZones.DataSource = Zones.ZoneList
        Me.cboZones.SelectedIndex = -1
        Me.cboZones.EndUpdate()
    End Sub

    Public Sub SetMob(ByVal ZoneID As Integer, ByVal MobID As Integer)
        Me.cboZones.SelectedValue = ZoneID
        Me.cboMobs.SelectedValue = MobID
    End Sub

    Private Sub LoadItems()
        _isloadingItems = True
        LoadMobItems(Me.cboMobs.SelectedValue)
        _isloadingItems = False
        If Me.lstItems.Items.Count > 0 Then
            Me.lstItems.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadMobs()
        Me.cboMobs.DisplayMember = "MobName"
        Me.cboMobs.ValueMember = "MobPK"
        Me.cboMobs.DataSource = MobsBindingSource
    End Sub

    Private Sub LoadMobItems(ByVal MobId As Integer)
        ClearItemInfo()
        Dim items = (From Item In DataAccess.MobData.Items Join _
                     im In DataAccess.MobData.ItemsToMobs On _
                     Item.ItemID Equals im.ItemID Where _
                     im.MobPK = MobId _
                     Order By Item.ItemName _
                     Select New With {Item.ItemID, Item.ItemName}).ToArray
        If Not items Is Nothing Then
            Me.lstItems.BeginUpdate()
            Me.lstItems.ValueMember = "ItemID"
            Me.lstItems.DisplayMember = "ItemName"
            Me.lstItems.DataSource = items
            Me.lstItems.SelectedIndex = -1
            Me.lstItems.EndUpdate()
        End If
    End Sub

    Private Sub BuildItemDisplay(ByVal Item As ItemsRow)
        Me.picIcon.Image = GetImage(Item.Icon)
        Me.lblItemName.Text = Item.ItemName
        Me.picRare.Visible = Item.Rare
        Me.picEx.Visible = Item.Ex
        Dim info As New System.Text.StringBuilder
        If Item.Slots <> String.Empty Then
            info.Append(String.Format("[{0}] {1}", Item.Slots, Item.Races))
            info.Append(Environment.NewLine)
        End If

        info.Append(Item.Description)
        info.Append(Environment.NewLine)
        If Item.ItemLevel > 0 Then
            info.Append(String.Format("Lv.{0} {1}", Item.ItemLevel, Item.Jobs))
        End If
        Me.lblInfo.Text = info.ToString
        If Item.MaxCharges > 0 Then
            Me.lblReuse.Text = String.Format("<{0}/{0} {1}/[{2}, {1}]>", Item.MaxCharges, GetTime(Item.UseDelay), GetTime(Item.ReuseDelay))
        Else
            Me.lblReuse.Text = String.Empty
        End If

    End Sub

    Private Sub BuildDescription(ByVal Description As String)
        Dim pattern As String = "\b(Fire|Earth|Water|Air|Ice|Lightning|Light|Dark)\b([+|-])"
        Dim pattern2 As String = "\b(Fire|Earth|Water|Air|Ice|Lightning|Light|Dark)\b"
        Dim matches As MatchCollection = Regex.Matches(Description, pattern)
        Dim blocks As String()
        If matches.Count > 0 Then
            Description = Regex.Replace(Description, pattern, ";$1;$2")
            blocks = Description.Split(";")
            For Each block As String In blocks
                If Regex.Match(block, pattern2).Success Then
                    Select Case block.ToLower
                        Case "fire"
                            'rtbInfo.InsertImage(My.Resources.Trans_Fire)
                        Case "earth"
                            'rtbInfo.InsertImage(My.Resources.Trans_Earth)
                        Case "water"
                            'rtbInfo.InsertImage(My.Resources.Trans_Water)
                        Case "air"
                            'rtbInfo.InsertImage(My.Resources.Trans_Wind)
                        Case "ice"
                            'rtbInfo.InsertImage(My.Resources.Trans_Ice)
                        Case "lightning"
                            'rtbInfo.InsertImage(My.Resources.Trans_Lightning)
                        Case "light"
                            'rtbInfo.InsertImage(My.Resources.Trans_Light)
                        Case "dark"
                            'rtbInfo.InsertImage(My.Resources.Trans_Dark)
                    End Select
                Else
                    'rtbInfo.AppendTextAsRtf(block)
                End If
            Next
        Else
            'rtbInfo.AppendTextAsRtf(Description)
        End If
    End Sub

    Private Function GetTime(ByVal Seconds As Integer) As String
        Dim output As String
        Dim time As New TimeSpan(0, 0, Seconds)
        If time.Hours > 0 Then
            output = String.Format("{0}:{1}:{2}", time.Hours, time.Minutes.ToString("00"), time.Seconds.ToString("00"))
        Else
            output = String.Format("{0}:{1}", time.Minutes, time.Seconds.ToString("00"))
        End If
        Return output
    End Function

    Private Function GetImage(ByVal Base64 As String) As Bitmap
        Dim imgBytes As Byte() = Convert.FromBase64String(Base64)
        Dim ms As New IO.MemoryStream(imgBytes, False)
        Return New Bitmap(ms)
    End Function

    Private Sub ClearItemInfo()
        Me.picIcon.Image = Nothing
        Me.picEx.Visible = False
        Me.picRare.Visible = False
        Me.lblItemName.Text = String.Empty
        Me.lblInfo.Text = String.Empty
        Me.lblReuse.Text = String.Empty
    End Sub
#End Region

    Private Sub GetPriceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetPriceToolStripMenuItem.Click
        If Me.lstItems.SelectedIndex > -1 Then
            Dim url As String = String.Format(ffxiah, Me.lstItems.SelectedValue)
            Dim apf As AppBarForm = My.Application.ApplicationContext.MainForm
            apf.showBrowser(url)
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Me.Validate()
        Me.MobsBindingSource.EndEdit()
        If Not DataAccess.MobData.GetChanges Is Nothing Then
            DataAccess.DataManager.UpdateAll(DataAccess.MobData)
        End If
        'DataAccess.MobData.Mobs.AcceptChanges()
    End Sub
End Class