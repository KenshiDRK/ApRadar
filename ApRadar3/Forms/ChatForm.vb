Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.Security.Cryptography
Imports System.Text.Encoding
Imports Contracts.Client
Imports Contracts.Shared
Imports System.Text.RegularExpressions

Public Class ChatForm

    Private _sof As FormAnimator
    Private _dock As DockingClass
    Private _sentList As List(Of String)
    Private _currentIndex As Integer = -1
    Private _isAutoScroll As Boolean = True
    Private _originPoint As Point
    Private _isResizing As Boolean = False
    Private _tells As List(Of String)
    Private _tellIndex As Integer = -1
    Private WithEvents _Proxy As Proxy_Singleton = Proxy_Singleton.GetInstance()
    Private _myPerson As Person
    Private channels As List(Of Channel)
    Private _banList As List(Of Person)
    Private WithEvents _ahParser As AHParser


#Region " FORM EVENTS "

    Private Sub ChatForm_ForeColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ForeColorChanged
        UpdateControlColors(Me)
    End Sub

    Private Sub ChatForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
        _sof = New FormAnimator(Me)
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
        channels = New List(Of Channel)

        _sof.FadeIn(400)
        If GlobalSettings.IsProEnabled AndAlso Not GlobalSettings.EliteMMOUser Then
            Me.txtLogin.Text = GlobalSettings.LicenseUser
            Me.txtLogin.Enabled = False
            Me.txtPassword.Enabled = False
        End If
        _banList = New List(Of Person)
        Me.lbBannedList.DisplayMember = "Name"
        Me.lbBannedList.ValueMember = "ID"
        Me.lbBannedList.DataSource = _banList
        Me.rtbLogBox.Font = My.Settings.ChatFont
        _ahParser = New AHParser

    End Sub

    Private Sub ChatForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        _Proxy.ExitChatSession()
        _Proxy = Nothing
    End Sub

    Private Sub ChatForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        _Proxy.ExitChatSession()
        _Proxy = Nothing
        _sof.FadeOut(500)
    End Sub

    Private Sub ChatForm_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown, BodyPanel.MouseDown, lbBannedList.MouseDown, SplitContainer2.MouseDown, SplitContainer1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim resizeRegion As New Rectangle(sender.Width - 15, sender.Height - 15, 15, 15)
            If resizeRegion.Contains(e.Location) Then
                _isResizing = True
                Me.SuspendLayout()
                _originPoint = e.Location
            End If
        End If
    End Sub

    Private Sub ChatForm_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove, BodyPanel.MouseMove, lbBannedList.MouseMove, SplitContainer2.MouseMove, SplitContainer1.MouseMove
        If _isResizing Then
            Me.Width += e.Location.X - _originPoint.X
            Me.Height += e.Location.Y - _originPoint.Y
            _originPoint = e.Location
        Else
            Dim resizeRegion As New Rectangle(sender.Width - 15, sender.Height - 15, 15, 15)
            If resizeRegion.Contains(e.Location) Then
                Me.Cursor = Cursors.SizeNWSE
            Else
                Me.Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub ChatForm_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp, BodyPanel.MouseUp, lbBannedList.MouseUp, SplitContainer2.MouseUp, SplitContainer1.MouseUp
        If _isResizing Then
            Me.ResumeLayout()
            _isResizing = False
        End If
    End Sub

#End Region

#Region " CONTROL EVENTS "
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles blnClose.Click
        Me.Dispose()
    End Sub

    Private Sub txtMessage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMessage.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            e.Handled = True
        End If
        If e.Control AndAlso e.KeyCode = Keys.R Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMessage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMessage.KeyPress
        Dim keyList As Char() = New Char() {ChrW(Keys.Enter), ChrW(Keys.Escape)}
        If keyList.Contains(e.KeyChar) Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtMessage_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMessage.KeyUp
        Select Case e.KeyCode
            Case Keys.Enter
                Me.cmdSend.PerformClick()
                e.Handled = True
            Case Keys.Up
                If Not _sentList Is Nothing Then
                    If _sentList.Count > 0 Then
                        If _currentIndex > 0 Then
                            _currentIndex -= 1
                        Else
                            _currentIndex = _sentList.Count - 1
                        End If
                        Me.txtMessage.Text = _sentList(_currentIndex)
                    End If
                End If
                e.Handled = True
            Case Keys.Down
                If Not _sentList Is Nothing Then
                    If _sentList.Count > 0 Then
                        If _currentIndex < _sentList.Count - 1 Then
                            _currentIndex += 1
                        Else
                            _currentIndex = 0
                        End If
                        Me.txtMessage.Text = _sentList(_currentIndex)
                    End If
                End If
                e.Handled = True
            Case Keys.Escape
                _currentIndex = -1
                Me.txtMessage.Text = String.Empty
            Case Keys.R
                If e.Control Then
                    If _tells.Count > 0 Then
                        If _tellIndex < _tells.Count - 1 Then
                            _tellIndex += 1
                        Else
                            _tellIndex = 0
                        End If
                        Me.txtMessage.Text = String.Format("/t {0} {1}", _tells(_tellIndex), Me.txtMessage.Text)
                        Me.txtMessage.SelectionStart = Me.txtMessage.TextLength
                    End If
                    e.Handled = True
                End If
        End Select
    End Sub

    Private Sub cmdConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click
        If Me.txtLogin.Text.Trim <> String.Empty Then
            Try
                _myPerson = New Person(0, Me.txtLogin.Text, False, False)
                _Proxy.Connect(_myPerson)
                _sentList = New List(Of String)
                _tells = New List(Of String)
                Me.cmdConnect.Enabled = False
                Me.txtLogin.Enabled = False
                Me.txtPassword.Enabled = False
                Me.cmdDisconnect.Enabled = True
                Me.txtMessage.Focus()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cmdDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisconnect.Click
        Disconnect(False)
    End Sub

    Private Sub rtbLogBox_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles rtbLogBox.LinkClicked
        Process.Start(e.LinkText)
    End Sub

    Private Sub rtbLogBox_VScroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtbLogBox.VScroll
        Dim p As Point = Me.rtbLogBox.GetPositionFromCharIndex(rtbLogBox.TextLength - 1)
        If p.Y <= Me.rtbLogBox.Height - 17 Then
            _isAutoScroll = True
        Else
            _isAutoScroll = False
        End If
    End Sub

    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        If Me.txtMessage.Text.Trim <> String.Empty Then
            Dim message As String = Me.txtMessage.Text
            If message.StartsWith("/t") Then
                message = message.Replace("/t ", String.Empty)
                Dim parts As String() = message.Split(New Char() {" "c}, 2)
                If parts.Length = 2 Then
                    Dim whoto As String = FindPerson(parts(0))
                    If whoto <> String.Empty Then
                        _Proxy.SayAndClear(MessageType.Tell, whoto, parts(1))
                        PostMessage(My.Settings.Tell, String.Format(">> {0} : {1}", whoto, parts(1)))
                    End If
                End If
            ElseIf message.ToLower.StartsWith("/sh") Then
                message = message.Replace("/sh ", String.Empty)
                _Proxy.SayAndClear(MessageType.Shout, Nothing, message)
            ElseIf message.ToLower.StartsWith("/em") Then
                message = message.Replace("/em ", String.Empty)
                _Proxy.SayAndClear(MessageType.Emote, Nothing, message)
            ElseIf message.ToLower.StartsWith("/afk") Then
                _Proxy.ChangeStatus(_myPerson.Name, PersonStatus.AFK)
            ElseIf message.ToLower.StartsWith("/away") Then
                _Proxy.ChangeStatus(_myPerson.Name, PersonStatus.Away)
            ElseIf message.ToLower.StartsWith("/back") Then
                _Proxy.ChangeStatus(_myPerson.Name, PersonStatus.Normal)
            ElseIf message = "/?" Or message.ToLower = "/help" Then
                'TODO: Add Help post
                PostHelpMessage()
            ElseIf message.ToLower.StartsWith("/check") OrElse message.ToLower.StartsWith("/c") Then
                message = message.Replace("/check ", String.Empty).Replace("/c ", String.Empty)
                PostMessage(Color.Orange, String.Format("Checking ffxiah for {0}...", message))
                _ahParser.GetItemInfo(message)
            Else
                _Proxy.SayAndClear(MessageType.Say, Nothing, message)
            End If
            If Not _sentList Is Nothing Then
                _sentList.Insert(0, Me.txtMessage.Text)
            End If
            Me.txtMessage.Text = String.Empty
        End If
    End Sub

    Private Sub tvUserList_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvUserList.DragDrop
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
                Dim pNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode", True), TreeNode)
                Dim tv As TreeView = CType(sender, TreeView)
                Dim pt As Point = tv.PointToClient(New Point(e.X, e.Y))
                Dim targetNode As TreeNode = tv.GetNodeAt(pt)
                If targetNode.Parent Is Nothing AndAlso targetNode.Name <> "bannedNode" Then
                    _Proxy.SwitchChannel(pNode.Tag, targetNode.Name)
                End If
            End If
        End If
    End Sub

    Private Sub tvUserList_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvUserList.DragOver
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            Dim tv As TreeView = CType(sender, TreeView)
            Dim pt As Point = tv.PointToClient(New Point(e.X, e.Y))
            Dim targetNode As TreeNode = tv.GetNodeAt(pt)
            tv.SelectedNode = targetNode
            If targetNode.Parent Is Nothing AndAlso targetNode.Name <> "bannedNode" Then
                e.Effect = DragDropEffects.Move
            Else
                e.Effect = DragDropEffects.None
            End If
        End If
    End Sub

    Private Sub tvUserList_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvUserList.ItemDrag
        If _myPerson.IsAdmin Then
            If TypeOf e.Item Is TreeNode Then
                Dim tn As TreeNode = DirectCast(e.Item, TreeNode)
                If Not tn.Parent Is Nothing AndAlso Not tn.Parent.Name = "bannedNode" Then
                    DoDragDrop(tn, DragDropEffects.All)
                End If
            End If
        End If
    End Sub

    Private Sub tvUserList_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvUserList.NodeMouseDoubleClick
        Dim c As Channel = GetChannel(e.Node.Name)
        If Not c Is Nothing AndAlso (_myPerson.channel Is Nothing OrElse c.Name <> _myPerson.channel.Name) Then
            If c.Password <> String.Empty AndAlso Not _myPerson.IsAdmin Then
                Dim cpd As New ChannelPassDialog
                If cpd.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim ePass As String = ComputeMD5HAsh(cpd.txtPassword.Text)
                    If ePass = c.Password Then
                        _Proxy.SwitchChannel(_myPerson.Name, e.Node.Name)
                    End If
                End If
            Else
                _Proxy.SwitchChannel(_myPerson.Name, e.Node.Name)
            End If
        End If
        e.Node.EnsureVisible()
    End Sub

    Private Sub _ahParser_ItemDataComplete(ByVal Item As AHParser.Item) Handles _ahParser.ItemDataComplete
        If Item.ItemName <> String.Empty Then
            PostItemInfo(Item)
        Else
            PostMessage(Color.Orange, "No information found for that item...")
        End If
    End Sub

    Private Sub _ahParser_FinishedSearch(ByVal ItemData As AHFetcher.SearchResult()) Handles _ahParser.ItemSearchCompleted
        If ItemData.Count > 0 Then
            PostSearchResult(ItemData)
        Else
            PostMessage(Color.Orange, "No information found for that item...")
        End If
    End Sub
#End Region

#Region " ADMIN MENU EVENTS "
    Private Sub PromoteAdminToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromoteAdminToolStripMenuItem.Click

        If Not Me.tvUserList.SelectedNode Is Nothing AndAlso _
        Not Me.tvUserList.SelectedNode.Parent Is Nothing AndAlso _
        Not Me.tvUserList.SelectedNode.Text.StartsWith("[A]") Then
            _Proxy.SayAndClear(MessageType.Promote, Me.tvUserList.SelectedNode.Tag, "")
        End If
    End Sub

    Private Sub RemoveAdminToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveAdminToolStripMenuItem.Click

        If Not Me.tvUserList.SelectedNode Is Nothing AndAlso _
        Not Me.tvUserList.SelectedNode.Parent Is Nothing AndAlso _
        Me.tvUserList.SelectedNode.Text.StartsWith("[A]") AndAlso _
        Me.tvUserList.SelectedNode.Text <> "Apnea" Then
            _Proxy.SayAndClear(MessageType.Demote, Me.tvUserList.SelectedNode.Tag, "")
        End If
    End Sub

    Private Sub KickUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KickUserToolStripMenuItem.Click
        If _myPerson.Name = "Apnea" Then
            If Not Me.tvUserList.SelectedNode Is Nothing AndAlso _
            Not tvUserList.SelectedNode.Parent Is Nothing AndAlso _
            tvUserList.SelectedNode.Tag <> "Apnea" Then
                _Proxy.SayAndClear(MessageType.Kick, Me.tvUserList.SelectedNode.Tag, "")
            End If
        Else
            If Not Me.tvUserList.SelectedNode Is Nothing AndAlso _
            Not Me.tvUserList.SelectedNode.Parent Is Nothing AndAlso _
            tvUserList.SelectedNode.Tag <> "Apnea" Then
                _Proxy.SayAndClear(MessageType.Kick, Me.tvUserList.SelectedNode.Tag, "")
            End If
        End If
    End Sub

    Private Sub BanUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BanUserToolStripMenuItem.Click
        Dim ipb As String = InputBox("Please give a reason for this ban", "Ban Reason")
        If ipb <> String.Empty Then
            If _myPerson.Name = "Apnea" Then
                If Not Me.tvUserList.SelectedNode Is Nothing AndAlso _
                Not Me.tvUserList.SelectedNode.Parent Is Nothing AndAlso _
                tvUserList.SelectedNode.Tag <> "Apnea" Then
                    _Proxy.SayAndClear(MessageType.Ban, Me.tvUserList.SelectedNode.Tag, ipb)
                End If
            Else
                If Not Me.tvUserList.SelectedNode Is Nothing AndAlso _
                Not Me.tvUserList.SelectedNode.Parent Is Nothing AndAlso _
                Not tvUserList.SelectedNode.Text.StartsWith("[A]") Then
                    _Proxy.SayAndClear(MessageType.Ban, Me.tvUserList.SelectedNode.Tag, ipb)
                End If
            End If
        End If
    End Sub

    Private Sub RemoveBanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub CreateChannelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateChannelToolStripMenuItem.Click
        Dim ccd As New CreateChannelDialog
        If ccd.ShowDialog = Windows.Forms.DialogResult.OK AndAlso ccd.txtChannelName.Text <> String.Empty Then
            Dim c As Channel = GetChannel(ccd.txtChannelName.Text)
            If c Is Nothing Then
                If ccd.txtPassword.Text.Trim <> String.Empty Then
                    _Proxy.CreateChannel(ccd.txtChannelName.Text, ComputeMD5HAsh(ccd.txtPassword.Text))
                Else
                    _Proxy.CreateChannel(ccd.txtChannelName.Text, String.Empty)
                End If
            Else
                MessageBox.Show("That channel already exists, please choose another name for your channel")
            End If
        End If
    End Sub

    Private Sub DeleteChannelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteChannelToolStripMenuItem.Click
        If Me.tvUserList.SelectedNode.Parent Is Nothing AndAlso Me.tvUserList.SelectedNode.Name <> "The Lounge" Then
            _Proxy.DeleteChannel(Me.tvUserList.SelectedNode.Name)
        End If
    End Sub
#End Region

#Region " PRIVATE METHODS "
    Private Sub PostMessage(ByVal Color As Color, ByVal Message As String)
        Me.rtbLogBox.SelectionStart = Me.rtbLogBox.TextLength
        Me.rtbLogBox.SelectionFont = My.Settings.ChatFont
        Me.rtbLogBox.SelectionColor = Color
        Me.rtbLogBox.AppendText(String.Format("{0} : {1}{2}", Now.ToLongTimeString, Message, ControlChars.NewLine))
        If _isAutoScroll Then
            Me.rtbLogBox.ScrollToCaret()
        End If
    End Sub

    Private Sub PostMessage(ByVal WhoFrom As Person, ByVal Color As Color, ByVal Message As String)
        Me.rtbLogBox.SelectionStart = Me.rtbLogBox.TextLength
        Me.rtbLogBox.SelectionFont = My.Settings.ChatFont
        If WhoFrom.ID <> _myPerson.ID Then
            rtbLogBox.SelectionColor = Color.Goldenrod
        Else
            rtbLogBox.SelectionColor = Color.PaleGreen
        End If
        rtbLogBox.AppendText(String.Format("{0} : <{1}> ", Now.ToLongTimeString, WhoFrom.Name))
        rtbLogBox.SelectionColor = Color
        rtbLogBox.AppendText(Message & ControlChars.NewLine)
        If _isAutoScroll Then
            rtbLogBox.ScrollToCaret()
        End If
    End Sub

    Private Sub PostItemInfo(ByVal Item As AHParser.Item)
        Me.rtbLogBox.SelectionStart = Me.rtbLogBox.TextLength
        Me.rtbLogBox.SelectionFont = My.Settings.ChatFont
        rtbLogBox.SelectionColor = Color.LimeGreen
        rtbLogBox.AppendText(String.Format("{0} - Stock: {1}{2}", Item.ItemName, Item.Stock, ControlChars.NewLine))
        rtbLogBox.SelectionColor = Color.LightBlue
        rtbLogBox.AppendText(String.Format("Speed: {0} ({1} sold/day){2}", Item.Speed.ToString.Replace("_", " "), Item.PerDay, ControlChars.NewLine))
        rtbLogBox.SelectionColor = Color.LightBlue
        rtbLogBox.AppendText(String.Format("Stack Price: {0}{1}", Item.StackPrice, ControlChars.NewLine))
        rtbLogBox.SelectionColor = Color.LightBlue
        rtbLogBox.AppendText(String.Format("Min: {0} Max: {1} Average: {2}{3}", Item.Min.ToString("n0"), Item.Max.ToString("n0"), Item.Average.ToString("n0"), ControlChars.NewLine))
        rtbLogBox.SelectionColor = Color.Coral
        rtbLogBox.AppendText("History -----------------------------" & ControlChars.NewLine)
        For i = 0 To Math.Min(5, Item.History.Count)
            rtbLogBox.SelectionColor = Color.Coral
            rtbLogBox.SelectionFont = New Font(My.Settings.ChatFont.FontFamily, 8, FontStyle.Regular, GraphicsUnit.Point)
            rtbLogBox.AppendText(String.Format("{0} {1} >> {2} ({3}){4}", _
                                               Item.History(i).SaleDate.ToShortDateString, _
                                               Item.History(i).FromWho, _
                                               Item.History(i).ToWho, _
                                               Item.History(i).Price.ToString("n0"), _
                                               ControlChars.NewLine))
        Next
        If _isAutoScroll Then
            rtbLogBox.ScrollToCaret()
        End If
    End Sub

    Private Sub PostSearchResult(ByVal results As AHFetcher.SearchResult())
        Dim count As Integer = 0
        For Each item In results
            Me.rtbLogBox.AppendText(ControlChars.NewLine)
            Me.rtbLogBox.SelectionStart = Me.rtbLogBox.TextLength
            Me.rtbLogBox.SelectionFont = My.Settings.ChatFont
            rtbLogBox.SelectionColor = Color.LimeGreen
            If count = 1 Then
                rtbLogBox.AppendText(String.Format("[Stack] - Stock: {1}{2}", item.ItemName, item.Quantity, ControlChars.NewLine))
            Else
                rtbLogBox.AppendText(String.Format("{0} - Stock: {1}{2}", item.ItemName, item.Quantity, ControlChars.NewLine))
                rtbLogBox.AppendText(item.ItemStats.Replace("|", ControlChars.NewLine) & ControlChars.NewLine)
            End If
            rtbLogBox.SelectionColor = Color.LightBlue
            rtbLogBox.AppendText(String.Format("Speed: {0} sold/day{1}", item.PerDay, ControlChars.NewLine))
            rtbLogBox.SelectionColor = Color.LightBlue
            rtbLogBox.AppendText(String.Format("Average: {0}{1}", item.AverageCost.ToString("n0"), ControlChars.NewLine))
            rtbLogBox.SelectionColor = Color.Coral
            rtbLogBox.AppendText("History -----------------------------" & ControlChars.NewLine)
            For i = 0 To Math.Min(5, item.PastSales.Count - 1)
                rtbLogBox.SelectionColor = Color.Coral
                rtbLogBox.SelectionFont = New Font(My.Settings.ChatFont.FontFamily, 8, FontStyle.Regular, GraphicsUnit.Point)
                rtbLogBox.AppendText(String.Format("{0}  •  {1} >> {2} ({3}){4}", _
                                                   item.PastSales(i).SaleDate.ToShortDateString(), _
                                                   item.PastSales(i).From, _
                                                   item.PastSales(i).To, _
                                                   item.PastSales(i).Price.ToString("n0"), _
                                                   ControlChars.NewLine))
            Next

            count += 1
        Next
        If _isAutoScroll Then
            rtbLogBox.ScrollToCaret()
        End If
    End Sub

    Private Sub PostHelpMessage()
        Dim nl As String = ControlChars.NewLine
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("   =========== ApRadar Chat Help ============")
        sb.AppendLine("    Commands:")
        sb.AppendLine("     Shout : /sh {Message}")
        sb.AppendLine("     Emote : /em {Emotion")
        sb.AppendLine("     Tell  : /t {Person Name} {Message}")
        sb.AppendLine("     FFXIAH Item Check : /c or /check {Item Name}")
        sb.AppendLine("   ========================================")
        sb.AppendLine("    Status Changes:")
        sb.AppendLine("     /afk  : Sets your status to Away From Keyboard")
        sb.AppendLine("     /away : Sets your status to Away")
        sb.AppendLine("     /back : Sets your status to Normal")
        Me.rtbLogBox.SelectionStart = Me.rtbLogBox.TextLength
        Me.rtbLogBox.SelectionFont = My.Settings.ChatFont
        Me.rtbLogBox.SelectionColor = Color.White
        Me.rtbLogBox.AppendText(sb.ToString())
        If _isAutoScroll Then
            Me.rtbLogBox.ScrollToCaret()
        End If
    End Sub

    Private Sub Disconnect(ByVal IsKickOrBan As Boolean)
        _Proxy.ExitChatSession()
        Me.tvUserList.Nodes.Clear()
        If Not GlobalSettings.IsProEnabled Then
            Me.txtLogin.Enabled = True
            Me.txtPassword.Enabled = True
        End If

        Me.cmdConnect.Enabled = True
        Me.cmdDisconnect.Enabled = False
        If Not _sentList Is Nothing Then
            _sentList.Clear()
        End If
        _banList.Clear()
        lbBannedList.DataSource = _banList.ToArray()
        If Not IsKickOrBan Then
            PostMessage(Color.Orange, "<<< Disconnected from ApRadar Chat >>>")
        End If
    End Sub

    Private Function ComputeMD5HAsh(ByVal Input As String) As String
        Dim bytes As Byte() = UTF8.GetBytes(Input)
        Dim md5 As New MD5CryptoServiceProvider
        Dim output As Byte() = md5.ComputeHash(bytes)
        Dim outHash As String = String.Empty
        For Each b As Byte In output
            outHash &= b.ToString("x2")
        Next
        Return outHash
    End Function

    Private Function FindPerson(ByVal name As String) As String
        For Each tn As TreeNode In Me.tvUserList.Nodes
            For Each subNode As TreeNode In tn.Nodes
                If subNode.Tag.ToLower = name.ToLower Then
                    Return subNode.Tag
                End If
            Next
        Next
        Return String.Empty
    End Function

    Private Sub AddNode(ByVal ParentNode As String, ByVal Key As Object, ByVal Name As String)
        AddNode(ParentNode, Key, Name, String.Empty)
    End Sub

    Private Sub AddNode(ByVal p As Person)
        Dim formatString As String = "{0}"
        If p.IsAdmin Then
            formatString = "[A] " & formatString
        End If
        If p.IsMod Then
            formatString = "[M] " & formatString
        End If
        Select Case p.Status
            Case PersonStatus.AFK
                formatString &= " (AFK)"
            Case PersonStatus.Away
                formatString &= " (Away)"
        End Select

        AddNode(p.channel.Name, p.ID, String.Format(formatString, p.Name), p.Name)

    End Sub

    Private Sub AddNode(ByVal ParentNode As String, ByVal Key As Object, ByVal Name As String, ByVal Tag As String)
        Dim tn As TreeNode
        If ParentNode = String.Empty Then
            tn = Me.tvUserList.Nodes.Add(Key, Name)
        Else
            tn = Me.tvUserList.Nodes(ParentNode).Nodes.Add(Key, Name)
        End If
        tn.Tag = Tag
        UpdateNodeCount()
        tn.EnsureVisible()
    End Sub

    Private Sub RemoveNode(ByVal ParentNode As String, ByVal Key As Object)
        If ParentNode = String.Empty Then
            Me.tvUserList.Nodes.RemoveByKey(Key)
        Else
            Me.tvUserList.Nodes(ParentNode).Nodes.RemoveByKey(Key)
            UpdateNodeCount()
        End If
    End Sub

    Private Sub RemoveNode(ByVal Key As Object)
        Me.tvUserList.Nodes.RemoveByKey(Key)
        For Each tn As TreeNode In Me.tvUserList.Nodes
            tn.Nodes.RemoveByKey(Key)
        Next
        UpdateNodeCount()
    End Sub

    Private Sub ClearNodes()
        For i = Me.tvUserList.Nodes.Count - 1 To 0 Step -1
            If Me.tvUserList.Nodes(i).Name <> "The Lounge" Then
                Me.tvUserList.Nodes.RemoveAt(i)
            End If
        Next
    End Sub

    Private Sub UpdateNodeCount()
        For Each tn As TreeNode In Me.tvUserList.Nodes
            tn.Text = String.Format("{0} [ {1} ]", tn.Name, tn.Nodes.Count)
        Next
    End Sub

    Private Function GetChannel(ByVal Name As String) As Channel
        Return (From c In channels Where c.Name = Name).FirstOrDefault
    End Function

    Private Sub UpdateStatus(ByVal p As Person)
        For Each tn As TreeNode In Me.tvUserList.Nodes
            For Each subNode As TreeNode In tn.Nodes
                If subNode.Name = p.ID Then
                    Dim formatString As String = "{0}"
                    If p.IsAdmin Then
                        formatString = "[A] " & formatString
                    End If
                    If p.IsMod Then
                        formatString = "[M] " & formatString
                    End If
                    Select Case p.Status
                        Case PersonStatus.AFK
                            formatString &= " (AFK)"
                        Case PersonStatus.Away
                            formatString &= " (Away)"
                    End Select
                    subNode.Text = String.Format(formatString, p.Name)
                    Exit Sub
                End If
            Next
        Next
    End Sub
#End Region

#Region " CHAT EVENTS "
    Private Delegate Sub ProxyEventCallback(ByVal sender As Object, ByVal e As ProxyEventArgs)
    Private Delegate Sub ProxyCallBackCallback(ByVal sender As Object, ByVal e As ProxyCallBackEventArgs)


    Private Sub Proxy_Callback(ByVal sender As Object, ByVal e As ProxyCallBackEventArgs) Handles _Proxy.ProxyCallBackEvent
        If Me.InvokeRequired Then
            Me.Invoke(New ProxyCallBackCallback(AddressOf Proxy_Callback), sender, e)
        Else
            Select Case e.callbackType
                Case MessageType.Enter
                    'Check if this person is already in the list
                    Dim isFound As Boolean = False
                    For Each n As TreeNode In Me.tvUserList.Nodes
                        If n.Nodes.ContainsKey(e.WhoFrom.Name) Then
                            isFound = True
                            Exit For
                        End If
                    Next
                    If Not isFound Then
                        AddNode(e.WhoFrom)
                        PostMessage(Color.Orange, String.Format("*** {0} has joined ***", e.WhoFrom.Name))
                    End If
                Case MessageType.Leave
                    'Remove the user from the tree
                    RemoveNode(e.WhoFrom.ID)
                    PostMessage(Color.Orange, String.Format("*** {0} has left ***", e.WhoFrom.Name))
                Case MessageType.Say
                    PostMessage(e.WhoFrom, My.Settings.Say, e.Message)
                    'PostMessage(Color.FromKnownColor(KnownColor.Info), String.Format("<{0}> {1}", e.WhoFrom.Name, e.Message))
                    If My.Settings.FlashWindow AndAlso e.WhoFrom.ID <> _myPerson.ID Then
                        FlashWindow(Me.Handle)
                    End If
                Case MessageType.Shout
                    PostMessage(e.WhoFrom, My.Settings.Shout, e.Message)
                    'PostMessage(Color.LightCoral, String.Format("<{0}> {1}", e.WhoFrom.Name, e.Message))
                    If e.WhoFrom.ID <> _myPerson.ID Then
                        FlashWindow(Me.Handle)
                    End If
                Case MessageType.Emote
                    PostMessage(My.Settings.Emote, String.Format("* {0} {1}", e.WhoFrom.Name, e.Message))
                    If My.Settings.FlashWindow AndAlso e.WhoFrom.ID <> _myPerson.ID Then
                        FlashWindow(Me.Handle)
                    End If
                Case MessageType.Tell
                    _tells.Insert(0, e.WhoFrom.Name)
                    PostMessage(My.Settings.Tell, String.Format("{0} >> {1}", e.WhoFrom.Name, e.Message))
                    If My.Settings.FlashWindow AndAlso e.WhoFrom.ID <> _myPerson.ID Then
                        FlashWindow(Me.Handle)
                    End If
                Case MessageType.Kick
                    PostMessage(Color.Orange, "*** You have been kicked from the server ***")
                    Me.cmdDisconnect.PerformClick()
                Case MessageType.Ban
                    PostMessage(Color.Red, "*** You have been banned from the server ***")
                    PostMessage(Color.Red, String.Format("Reason: {0}", e.Message))
                    _myPerson.IsBanned = True
                    Me.cmdDisconnect.PerformClick()
                Case MessageType.Promote
                    If e.WhoFrom.Name = _myPerson.Name Then
                        _myPerson.IsAdmin = True
                        Me.tvUserList.ContextMenuStrip = adminMenu
                        RemoveNode(_myPerson.ID)
                        AddNode(e.WhoFrom)
                        PostMessage(Color.Orange, "*** You have been promoted to an admin ***")
                    Else
                        RemoveNode(e.WhoFrom.ID)
                        AddNode(e.WhoFrom)
                        PostMessage(Color.Orange, String.Format("*** {0} has been promoted to an admin ***", e.WhoFrom.Name))
                    End If
                Case MessageType.Demote
                    If e.WhoFrom.Name = _myPerson.Name Then
                        _myPerson.IsAdmin = False
                        Me.tvUserList.ContextMenuStrip = Nothing
                        RemoveNode(_myPerson.ID)
                        AddNode(_myPerson)
                        PostMessage(Color.Orange, "*** Your admin status has been removed ***")
                    Else
                        RemoveNode(e.WhoFrom.ID)
                        AddNode(e.WhoFrom)
                        PostMessage(Color.Orange, String.Format("*** {0}'s admin status has been removed ***", e.WhoFrom.Name))
                    End If
                Case MessageType.Ping
                    Debug.Print(e.Message)
                Case MessageType.UserBanned
                    _banList.Add(e.WhoFrom)
                    Me.lbBannedList.DataSource = _banList.ToArray()
                Case MessageType.BanRemoved
                    For Each p As Person In _banList
                        If p.ID = e.WhoFrom.ID Then
                            _banList.Remove(p)
                            Exit For
                        End If
                    Next
                    Me.lbBannedList.DataSource = _banList.ToArray()
                Case MessageType.StatusChanged
                    UpdateStatus(e.WhoFrom)
            End Select

        End If
    End Sub

    Private Sub Proxy_Event(ByVal sender As Object, ByVal e As ProxyEventArgs) Handles _Proxy.ProxyEvent
        If Me.InvokeRequired Then
            Me.BeginInvoke(New ProxyEventCallback(AddressOf Proxy_Event), sender, e)
        Else
            If Not e.state Is Nothing Then

                _banList = New List(Of Person)(e.state.BannedList)
                Me.lbBannedList.DataSource = _banList.ToArray()
                For Each c As Channel In e.state.Channels
                    AddNode(String.Empty, c.Name, c.Name, c.Password)
                Next
                channels.AddRange(e.state.Channels)
                For Each person In e.state.People
                    If person.Name = Me.txtLogin.Text Then
                        If person.IsAdmin Then
                            For Each item As ToolStripItem In Me.adminMenu.Items
                                If item.Tag = "admin" Then
                                    item.Visible = True
                                End If
                            Next
                            Me.lblHeder.Text = "ApRadar Chat [Admin]"

                        Else
                            Me.lblHeder.Text = "ApRadar Chat"
                            For Each item As ToolStripItem In Me.adminMenu.Items
                                If item.Tag = "admin" Then
                                    item.Visible = False
                                End If
                            Next
                        End If
                        _myPerson = person
                    End If
                    AddNode(person)
                Next
                PostMessage(Color.Orange, "<<< Welcome to ApRadar Chat >>>")
                PostMessage(Color.Orange, "<<< For help, type /? or /help >>>")
            End If
        End If
    End Sub

    Private Sub Proxy_ChannelEvent(ByVal sender As Object, ByVal e As ChannelEventArgs) Handles _Proxy.ChannelEvent
        Select Case e.EventType
            Case MessageType.ChannelCreated
                channels.Add(e.Channel)
                AddNode(String.Empty, e.Channel.Name, e.Channel.Name, e.Channel.Password)
            Case MessageType.ChannelRemoved
                RemoveNode(e.Channel.Name)
                Dim cToRemove As Channel = (From c In channels Where c.Name = e.Channel.Name).FirstOrDefault
                If Not cToRemove Is Nothing Then
                    channels.Remove(cToRemove)
                End If
            Case MessageType.MovedChannel
                If e.WhoFrom.ID = _myPerson.ID Then
                    PostMessage(Color.Orange, String.Format("<<< Welcome to the {0} channel >>>", e.WhoFrom.channel.Name))
                End If
                RemoveNode(e.WhoFrom.ID)
                AddNode(e.WhoFrom)
                If e.WhoFrom.ID = _myPerson.ID Then
                    _myPerson = e.WhoFrom
                End If
            Case MessageType.ChannelList
                For Each c As Channel In e.ChannelList
                    AddNode(String.Empty, e.Channel.Name, e.Channel.Name, e.Channel.Password)
                Next
        End Select
    End Sub

    Private Sub Proxy_StateChanged(ByVal type As Contracts.Client.CloseType) Handles _Proxy.StateChanged
        If type = CloseType.Faulted Then
            MessageBox.Show(String.Format("{0}: The connection has faulted...", Now.ToLongTimeString))
        Else
            'MessageBox.Show(String.Format("{0}: The connection has been closed", Now.ToLongTimeString))
        End If
    End Sub
#End Region


    Private Sub RemoveBanToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveBanToolStripMenuItem.Click
        If Not Me.lbBannedList.SelectedItem Is Nothing Then
            Dim p As Person = CType(Me.lbBannedList.SelectedItem, Person)
            _Proxy.RemoveBan(p.ID, p.Name)
        End If
    End Sub

    Private Sub tbTransparency_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbTransparency.Scroll
        Me.Opacity = tbTransparency.Value / 100
    End Sub

    Private Sub cmdSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSettings.Click
        Dim csd As New ChatSettingsDialog
        csd.ShowDialog()
        Me.rtbLogBox.SelectionStart = 0
        Me.rtbLogBox.SelectionLength = Me.rtbLogBox.TextLength
        Me.rtbLogBox.SelectionFont = My.Settings.ChatFont
        Me.rtbLogBox.SelectionStart = Me.rtbLogBox.TextLength
        Me.txtMessage.Font = My.Settings.ChatFont
    End Sub

End Class