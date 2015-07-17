Imports System.IO
Imports FFXIMemory
Imports DataLibrary
Imports System.Text

Public Class WPFDatItemBrowser
    Inherits ResizableForm

    '    Private _dock As DockingClass
    '    Private _animator As FormAnimator
    '    Private _items As List(Of Item)
    '    Private _datPath As String = ""
    '    Private _lastItem As Item = Nothing
    '    Private _ahThread As Threading.Thread
    '    Private WithEvents _parser As AHParser
    '    Private _iof As ItemOverlayForm
    '    Private ReadOnly Property IOF As ItemOverlayForm
    '        Get
    '            If _iof Is Nothing OrElse _iof.IsDisposed Then
    '                _iof = New ItemOverlayForm
    '            End If
    '            Return _iof
    '        End Get
    '    End Property

    '    Private _ahf As AHFetcher.AHParserClient
    '    Private ReadOnly Property AHFetcher As AHFetcher.AHParserClient
    '        Get
    '            If _ahf Is Nothing Then
    '                _ahf = New AHFetcher.AHParserClient
    '            End If
    '            Return _ahf
    '        End Get
    '    End Property
    '    Public Property AppBarForm As AppBarForm

    '    Private Delegate Sub OnItemInfoCallback(ByVal Result As AHFetcher.SearchResult)

    '    Public Sub New()
    '        InitializeComponent()
    '        Me.dgItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
    '    End Sub

    '    Private Sub DatItemBrowser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '        ThemeHandler.ApplyTheme(Me)
    '        ThemeHandler.ApplyTheme(IOF)
    '        Me.pnlProgress.BackColor = Me.BackColor
    '        _dock = New DockingClass(Me)
    '        Me.cboJob.DataSource = [Enum].GetValues(GetType(Job))
    '        Me.cboRace.DataSource = [Enum].GetValues(GetType(Race))
    '        Me.cboSlot.DataSource = [Enum].GetValues(GetType(EquipmentSlot))
    '        _animator = New FormAnimator(Me)
    '        _animator.FadeIn(500)
    '        Me.dgItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
    '    End Sub

    '    Private Sub HeaderPanel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseDown, lblHeder.MouseDown
    '        If e.Button = Windows.Forms.MouseButtons.Left Then
    '            _dock.StartDockDrag(e.X, e.Y)
    '        End If
    '    End Sub

    '    Private Sub HeaderPanel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseMove, lblHeder.MouseMove
    '        If e.Button = Windows.Forms.MouseButtons.Left Then
    '            _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
    '        End If
    '    End Sub

    '    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
    '        Me.Close()
    '    End Sub

    '    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
    '        Me.cmdAddToDatabase.Enabled = False
    '        LoadItems(Me.cboType.Text.Replace(" ", ""))
    '    End Sub

    '    Private Sub LoadItems(ByVal Type As String)
    '        Dim id As Integer = FFXIMemory.FFXI.ItemDats(Type)
    '        _datPath = FFXIMemory.FFXI.GetFilePath(id)
    '        If _datPath <> String.Empty Then
    '            'Now lets load up some data
    '            _items = New List(Of Item)
    '            Me.pnlProgress.Visible = True
    '            If bwFileScanner.IsBusy Then
    '                bwFileScanner.CancelAsync()
    '            Else
    '                bwFileScanner.RunWorkerAsync()
    '            End If
    '        End If
    '    End Sub

    '    Private Sub bwFileScanner_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwFileScanner.DoWork
    '        Dim br As New BinaryReader(IO.File.OpenRead(_datPath))
    '        If br.BaseStream.Length Mod &HC00 <> 0 OrElse br.BaseStream.Length < &HC00 OrElse br.BaseStream.Position <> 0 Then
    '            'Just exit with nothing in the array because there is an erro with the file
    '            Return
    '        Else
    '            Dim T As Item.iType
    '            Item.DeduceType(br, T)
    '            Dim ItemCount As Long = br.BaseStream.Length / &HC00
    '            Dim currentItem As Integer = 0
    '            Dim i As Item
    '            While br.BaseStream.Position < br.BaseStream.Length
    '                If bwFileScanner.CancellationPending Then
    '                    e.Cancel = True
    '                    _items.Clear()
    '                    Exit While
    '                End If
    '                i = New Item
    '                If Not i.Read(br, T) Then
    '                    _items.Clear()
    '                    Exit While
    '                End If
    '                If i.GetFieldValue("name") <> "." AndAlso i.GetFieldValue("name") <> "" Then
    '                    _items.Add(i)
    '                End If

    '                currentItem += 1
    '                bwFileScanner.ReportProgress(currentItem / ItemCount * 100, String.Format("Scanning entry {0} of {1}", currentItem, ItemCount))
    '                If br.BaseStream.Length = &HC00 AndAlso T = Item.iType.Currency Then
    '                    Exit While
    '                End If
    '            End While
    '        End If
    '    End Sub

    '    Private Sub bwFileScanner_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwFileScanner.ProgressChanged
    '        Me.lblStatusMessage.Text = String.Format("{0} {1}%", e.UserState.ToString, e.ProgressPercentage)
    '        Me.pbLoading.Value = e.ProgressPercentage
    '    End Sub

    '    Private Sub bwFileScanner_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwFileScanner.RunWorkerCompleted
    '        If e.Cancelled Then
    '            bwFileScanner.RunWorkerAsync()
    '        Else
    '            _items.Sort()
    '#If DEBUG Then
    '            Serializer.SerializeToXML(Of List(Of Item))(String.Format("C:\{0}.xml", Me.cboType.Text), _items)
    '#End If
    '            Me.LevelDataGridViewTextBoxColumn.Visible = Me.cboType.SelectedIndex < 2
    '            Dim sl As New SortableBindingList(Of Item)(_items)
    '            Me.dgItems.DataSource = sl
    '            Me.lblItemCount.Text = String.Format("{0} Items found", _items.Count)
    '            'Me.lbItems.DataSource = _items
    '            Me.pnlProgress.Visible = False
    '            Me.cmdAddToDatabase.Enabled = True
    '        End If
    '    End Sub

    '    Private Sub cmdApplyFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApplyFilter.Click
    '        If Not _items Is Nothing Then
    '            Dim newItems As IQueryable(Of Item) = _items.AsQueryable
    '            If Me.txtName.Text.Trim <> String.Empty Then
    '                If Me.txtName.Text.StartsWith("%") Then
    '                    newItems = newItems.Where(Function(c) c.Name.ToLower.EndsWith(Me.txtName.Text.Trim.ToLower.Replace("%", "")) OrElse c.GetFieldText("log-name-singular").ToLower.EndsWith(Me.txtName.Text.Trim.ToLower.Replace("%", "")))
    '                ElseIf Me.txtName.Text.EndsWith("%") Then
    '                    newItems = newItems.Where(Function(c) c.Name.ToLower.StartsWith(Me.txtName.Text.Trim.ToLower.Replace("%", "")) OrElse c.GetFieldText("log-name-singular").ToLower.StartsWith(Me.txtName.Text.Trim.ToLower.Replace("%", "")))
    '                Else
    '                    newItems = newItems.Where(Function(c) c.Name.ToLower = Me.txtName.Text.Trim.ToLower OrElse c.GetFieldText("log-name-singular").ToLower = Me.txtName.Text.Trim.ToLower)
    '                End If
    '            End If
    '            If Me.cboJob.SelectedIndex > -1 Then
    '                newItems = newItems.Where(Function(c) (c.Jobs And Me.cboJob.SelectedValue) = Me.cboJob.SelectedValue OrElse c.Jobs = Job.All)
    '            End If

    '            If Me.cboRace.SelectedIndex > -1 Then
    '                newItems = newItems.Where(Function(c) (c.Races And Me.cboRace.SelectedValue) = Me.cboRace.SelectedValue OrElse c.Races = Race.All)
    '            End If

    '            If Me.cboSlot.SelectedIndex > -1 Then
    '                newItems = newItems.Where(Function(c) (c.Slots And Me.cboSlot.SelectedValue) = Me.cboSlot.SelectedValue OrElse c.Slots = EquipmentSlot.All)
    '            End If
    '            Dim min, max As Integer
    '            If Integer.TryParse(Me.txtLevelMin.Text, min) AndAlso Integer.TryParse(Me.txtLevelMax.Text, max) Then
    '                newItems = newItems.Where(Function(c) c.Level >= min AndAlso c.Level <= max)
    '            End If
    '            Dim sl As New SortableBindingList(Of Item)(newItems.ToList)
    '            Me.lblItemCount.Text = String.Format("{0} Items found", newItems.Count)
    '            Me.dgItems.DataSource = sl
    '        End If
    '    End Sub

    '    Private Sub cmdClearFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearFilter.Click
    '        Me.txtName.Text = String.Empty
    '        Me.cboJob.SelectedIndex = -1
    '        Me.cboRace.SelectedIndex = -1
    '        Me.cboSlot.SelectedIndex = -1
    '        Me.txtLevelMin.Text = 1
    '        Me.txtLevelMax.Text = 99
    '        Dim sl As New SortableBindingList(Of Item)(_items)
    '        Me.lblItemCount.Text = String.Format("{0} Items found", _items.Count)
    '        Me.dgItems.DataSource = sl
    '    End Sub

    '    Private Sub dgItems_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
    '        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 AndAlso e.Button = MouseButtons.Right Then
    '            dgItems.Rows(e.RowIndex).Selected = True
    '            Dim r As Rectangle = dgItems.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
    '            cmsSearch.Show(CType(sender, Control), r.Left + e.X, r.Top + e.Y)
    '        End If
    '    End Sub

    '    Private Sub dgItems_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
    '        IOF.Hide()
    '        'IOF.LoaderImage.Visible = True
    '        'IOF.lblAHInfo.Text = "Loading AH Data..."
    '        _lastItem = Nothing
    '    End Sub

    '    Private Sub dgItems_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '        Dim rowIndex As Integer = Me.dgItems.HitTest(e.X, e.Y).RowIndex
    '        If rowIndex > -1 Then
    '            Dim selectedItem As Item = TryCast(Me.dgItems.Rows(rowIndex).DataBoundItem, Item)
    '            If selectedItem IsNot Nothing AndAlso selectedItem IsNot _lastItem Then
    '                _lastItem = selectedItem
    '                'LoadAHData(selectedItem.ItemID)
    '                IOF.pbIcon.Image = selectedItem.GetIcon
    '                If selectedItem.GetFieldValue("type") = ItemType.Weapon Then
    '                    IOF.lblItemName.Text = String.Format("{0} [{1} dps]", selectedItem.GetFieldText("name"),
    '                                                         selectedItem.GetFieldText("dps"))
    '                Else
    '                    IOF.lblItemName.Text = selectedItem.GetFieldText("name")
    '                End If
    '                If selectedItem.GetFieldValue("type") = ItemType.Armor OrElse selectedItem.GetFieldValue("type") = ItemType.Weapon Then 'Item.iType.Armor OrElse selectedItem.GetFieldValue("type") = Item.iType.Weapon Then

    '                    If selectedItem.HasField("max-charges") AndAlso selectedItem.GetFieldValue("max-charges") > 0 Then
    '                        IOF.lblDescription.Text = String.Format("[{1}] {2}{0}{3}{0}Lv {4} {5}", ControlChars.NewLine,
    '                                                           selectedItem.GetFieldText("slots"),
    '                                                           selectedItem.GetFieldText("races").Replace("All", "All Races"),
    '                                                           selectedItem.GetFieldText("description").Replace("≺Element: ", "").Replace("≻", ""),
    '                                                           selectedItem.GetFieldText("level"),
    '                                                           selectedItem.GetFieldText("jobs").Replace("All", "All Jobs"))
    '                        IOF.lblDescription.Margin = New Padding(3, 0, 3, 15)
    '                        IOF.lblReuse.Visible = True
    '                        IOF.lblReuse.Text = String.Format("<{0}/{0} {1}/[{2}, {1}]>",
    '                                                           selectedItem.GetFieldText("max-charges"),
    '                                                           selectedItem.GetFieldText("use-delay"),
    '                                                           selectedItem.GetFieldText("reuse-delay"))
    '                    Else
    '                        IOF.lblDescription.Text = String.Format("[{1}] {2}{0}{3}{0}Lv {4} {5}", ControlChars.NewLine,
    '                                                           selectedItem.GetFieldText("slots"),
    '                                                           selectedItem.GetFieldText("races").Replace("All", "All Races"),
    '                                                           selectedItem.GetFieldText("description").Replace("≺Element: ", "").Replace("≻", ""),
    '                                                           selectedItem.GetFieldText("level"),
    '                                                           selectedItem.GetFieldText("jobs").Replace("All", "All Jobs"))
    '                        IOF.lblDescription.Margin = New Padding(3, 0, 3, 0)
    '                        IOF.lblReuse.Visible = False
    '                        IOF.lblReuse.Text = ""
    '                    End If
    '                Else
    '                    IOF.lblDescription.Text = selectedItem.GetFieldText("description")
    '                End If

    '            End If
    '            IOF.Location = Me.PointToScreen(New Point(dgItems.Left + e.X + 10, dgItems.Top + e.Y + 10))
    '            If Not IOF.Visible Then
    '                IOF.Show()
    '            End If
    '            Me.dgItems.Focus()
    '        Else
    '            IOF.Hide()
    '        End If
    '    End Sub

    '    Private Sub LoadAHData(ByVal ID As Integer)
    '        If _parser Is Nothing Then
    '            _parser = New AHParser()
    '        End If
    '        _parser.Abort()
    '        If IOF.Visible Then
    '            IOF.lblAHInfo.Text = "Loading AH Data..."
    '            IOF.LoaderImage.Visible = True
    '        End If
    '        _parser.GetItemInfo(ID)
    '    End Sub

    '    Private Sub SearchCompleted(ByVal Results As AHFetcher.SearchResult()) Handles _parser.ItemSearchCompleted
    '        If IOF.Visible AndAlso Results.Count > 0 Then
    '            Dim item As AHFetcher.SearchResult = Results(0)
    '            Dim sb As New StringBuilder
    '            sb.AppendLine(String.Format("{0} - Stock: {1}", item.ItemName, item.Quantity))
    '            sb.AppendLine(String.Format("Speed: {0} sold/day", item.PerDay))
    '            sb.AppendLine(String.Format("Average Cost: {0}", item.AverageCost.ToString("n0")))
    '            sb.AppendLine("History -----------------------------")
    '            For i = 0 To Math.Min(5, item.PastSales.Count - 1)
    '                sb.AppendLine(String.Format("{0}  •  {1} >> {2} ({3})", _
    '                                               item.PastSales(i).SaleDate.ToShortDateString(), _
    '                                               item.PastSales(i).From, _
    '                                               item.PastSales(i).To, _
    '                                               item.PastSales(i).Price.ToString("n0")))
    '            Next
    '            IOF.lblAHInfo.Text = sb.ToString()
    '            IOF.LoaderImage.Visible = False
    '        End If
    '    End Sub


    '    'Private Sub OnGetIAHData(ByVal result As IAsyncResult)
    '    '    Try
    '    '        Dim sResult As AHFetcher.SearchResult = AHFetcher.EndCheckItem(result)
    '    '        PostResult(sResult)
    '    '    Catch
    '    '    End Try
    '    'End Sub

    '    'Private Sub PostResult(ByVal result As AHFetcher.SearchResult)
    '    '    If IOF.InvokeRequired Then
    '    '        IOF.Invoke(New OnItemInfoCallback(AddressOf PostResult), result)
    '    '    Else
    '    '        
    '    '    End If
    '    'End Sub

    '    Private Sub tsFFXIAH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsFFXIAH.Click
    '        If Me.dgItems.SelectedRows.Count > 0 Then
    '            Dim row As Item = TryCast(Me.dgItems.SelectedRows(0).DataBoundItem, Item)
    '            If Not row Is Nothing Then
    '                Dim url As String = String.Format("http://www.ffxiah.com/item/{0}/", row.GetFieldValue("id"))
    '                AppBarForm.BrowserForm.Top = AppBarForm.Bottom
    '                AppBarForm.BrowserForm.Browser.Navigate(url)
    '                AppBarForm.BrowserForm.Show()
    '            End If
    '        End If
    '    End Sub

    '    Private Sub tsWiki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsWiki.Click

    '    End Sub

    '    Private Sub cmdAddToDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddToDatabase.Click
    '        Me.cmdAddToDatabase.Enabled = False
    '        Me.cboType.Enabled = False
    '        Me.pnlProgress.Visible = True
    '        bwItemAdder.RunWorkerAsync()
    '    End Sub

    '    Private Sub bwItemAdder_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwItemAdder.DoWork
    '        Dim addedCount As Integer = 0
    '        Dim i As Item
    '        Dim x As Integer = 1
    '        Dim total As Integer = dgItems.RowCount
    '        For Each dr As DataGridViewRow In dgItems.Rows
    '            i = TryCast(dr.DataBoundItem, Item)
    '            If Not i Is Nothing Then
    '                If Not DataAccess.ItemExists(i.GetFieldValue("id")) Then
    '                    DataAccess.AddItem(i)
    '                    addedCount += 1
    '                End If
    '            End If
    '            bwItemAdder.ReportProgress(CInt(x / total * 100), String.Format("{0} Items added", addedCount))
    '            x += 1
    '        Next
    '    End Sub

    '    Private Sub bwItemAdder_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwItemAdder.ProgressChanged
    '        Me.lblStatusMessage.Text = String.Format("{0} {1}%", e.UserState.ToString, e.ProgressPercentage)
    '        Me.pbLoading.Value = e.ProgressPercentage
    '    End Sub

    '    Private Sub bwItemAdder_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwItemAdder.RunWorkerCompleted
    '        Me.cboType.Enabled = True
    '        Me.cmdAddToDatabase.Enabled = True
    '        Me.pnlProgress.Visible = False
    '    End Sub

    Private Sub WPFDatItemBrowser_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim items = Serializer.DeserializeFromXML(Of List(Of Item))(Application.StartupPath & "ItemsDump.xml")
        Me.EnhancedGridControl1.DataContext = items
    End Sub
End Class