Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.Security.Cryptography
Imports System.Text.Encoding
Imports Contracts.Shared
Imports Contracts.Client

Public Class ChatForm
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
    Private rootNodes() As String = New String() {"adminNode", "memberNode"}
    Private channels As List(Of Channel)

#Region " FORM EVENTS "
    Private Sub ChatForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
        channels = New List(Of Channel)
    End Sub

    Private Sub ChatForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        _Proxy.ExitChatSession()
        _Proxy = Nothing
    End Sub

    Private Sub ChatForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        _Proxy.ExitChatSession()
        _Proxy = Nothing
    End Sub

    Private Sub ChatForm_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim resizeRegion As New Rectangle(Me.Width - 15, Me.Height - 15, 15, 15)
            If resizeRegion.Contains(e.Location) Then
                _isResizing = True
                Me.SuspendLayout()
                _originPoint = e.Location
            End If
        End If
    End Sub

    Private Sub ChatForm_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If _isResizing Then
            Me.Width += e.Location.X - _originPoint.X
            Me.Height += e.Location.Y - _originPoint.Y
            _originPoint = e.Location
        Else
            Dim resizeRegion As New Rectangle(Me.Width - 15, Me.Height - 15, 15, 15)
            If resizeRegion.Contains(e.Location) Then
                Me.Cursor = Cursors.SizeNWSE
            Else
                Me.Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub ChatForm_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
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
            _dock.UpdateDockDrag(e.X, e.Y)
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
        Dim keyList As Char() = New Char() {Chr(Keys.Enter), Chr(Keys.Escape), Chr(Keys.Up), Chr(Keys.Down)}
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
                If _sentList.Count > 0 Then
                    If _currentIndex > 0 Then
                        _currentIndex -= 1
                    Else
                        _currentIndex = _sentList.Count - 1
                    End If
                    Me.txtMessage.Text = _sentList(_currentIndex)
                End If
                e.Handled = True
            Case Keys.Down
                If _sentList.Count > 0 Then
                    If _currentIndex < _sentList.Count - 1 Then
                        _currentIndex += 1
                    Else
                        _currentIndex = 0
                    End If
                    Me.txtMessage.Text = _sentList(_currentIndex)
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

                Dim cLoginResult As New WebService.ChatLoginResult
                Dim ws As New WebService.ValidationServicePortTypeClient
                If Me.txtPassword.Text <> String.Empty Then
                    cLoginResult = ws.ValidateChatUser(Me.txtLogin.Text, ComputeMD5HAsh(Me.txtPassword.Text))
                End If

                If cLoginResult.CanChat Then
                    _myPerson = New Person(cLoginResult.UserID, Me.txtLogin.Text, False, False)
                    _Proxy.Connect(_myPerson)

                    _sentList = New List(Of String)
                    _tells = New List(Of String)
                    Me.cmdConnect.Enabled = False
                    Me.txtLogin.Enabled = False
                    Me.txtPassword.Enabled = False
                    Me.cmdDisconnect.Enabled = True
                Else
                    PostMessage(Color.Orange, "Incorrect username or password...")
                End If
            Catch ex As Exception

        End Try
        End If
    End Sub



    Private Sub cmdDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisconnect.Click
        _Proxy.ExitChatSession()

        Me.tvUserList.Nodes.Clear()
        Me.cmdConnect.Enabled = True
        Me.cmdDisconnect.Enabled = False
        Me.txtLogin.Enabled = True
        Me.txtPassword.Enabled = True
        If Not _sentList Is Nothing Then
            _sentList.Clear()
        End If
        If Not _myPerson.IsBanned Then
            PostMessage(Color.Orange, "<<< Disconnected from ApRadar Chat >>>")
        End If
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
                        PostMessage(Color.Violet, String.Format(">> {0} : {1}", whoto, parts(1)))
                    End If
                End If
            ElseIf message.ToLower.StartsWith("/sh") Then
                message = message.Replace("/sh ", String.Empty)
                _Proxy.SayAndClear(MessageType.Shout, Nothing, message)
            ElseIf message.ToLower.StartsWith("/em") Then
                message = message.Replace("/em ", String.Empty)
                _Proxy.SayAndClear(MessageType.Emote, Nothing, message)
            Else
                _Proxy.SayAndClear(MessageType.Say, Nothing, message)
            End If
            _sentList.Insert(0, Me.txtMessage.Text)
            Me.txtMessage.Text = String.Empty
        End If
    End Sub
#End Region

#Region " PRIVATE METHODS "
    Private Sub PostMessage(ByVal Color As Color, ByVal Message As String)
        Me.rtbLogBox.SelectionStart = Me.rtbLogBox.TextLength
        Me.rtbLogBox.SelectionColor = Color
        Me.rtbLogBox.AppendText(String.Format("{0} : {1}{2}", Now.ToLongTimeString, Message, ControlChars.NewLine))
        If _isAutoScroll Then
            Me.rtbLogBox.ScrollToCaret()
        End If

    End Sub

    Private Sub Disconnect(ByVal IsKickOrBan As Boolean)
        _Proxy.ExitChatSession()
        Me.tvUserList.Nodes.Clear()
        Me.txtLogin.Enabled = True
        Me.txtPassword.Enabled = True
        Me.cmdConnect.Enabled = True
        Me.cmdDisconnect.Enabled = False
        If Not _sentList Is Nothing Then
            _sentList.Clear()
        End If
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
                If subNode.Text.Replace("[A] ", String.Empty).ToLower = name.ToLower Then
                    Return subNode.Text.Replace("[A] ", String.Empty)
                End If
            Next
        Next
        Return String.Empty
    End Function

    Private Sub AddNode(ByVal ParentNode As String, ByVal Key As Object, ByVal Name As String)
        AddNode(ParentNode, Key, Name, String.Empty)
    End Sub

    Private Sub AddNode(ByVal p As Person)
        If p.channel Is Nothing Then
            If p.IsAdmin Then
                AddNode("The Lounge", p.ID, String.Format("[A] {0}", p.Name))
            Else
                AddNode("The Lounge", p.ID, p.Name)
            End If
        Else
            If p.IsAdmin Then
                AddNode(p.channel.Name, p.ID, String.Format("[A] {0}", p.Name))
            Else
                AddNode(p.channel.Name, p.ID, p.Name)
            End If
        End If
    End Sub

    Private Sub AddNode(ByVal ParentNode As String, ByVal Key As Object, ByVal Name As String, ByVal Password As String)
        Dim tn As TreeNode
        If ParentNode = String.Empty Then
            tn = Me.tvUserList.Nodes.Add(Key, Name)
            tn.Tag = Password
        Else
            tn = Me.tvUserList.Nodes(ParentNode).Nodes.Add(Key, Name)
            UpdateNodeCount()
        End If
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

#End Region

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
                    PostMessage(Color.FromKnownColor(KnownColor.Info), String.Format("<{0}> {1}", e.WhoFrom.Name, e.Message))
                    If e.WhoFrom.ID <> _myPerson.ID Then
                        'FlashWindow(Me.Handle)
                    End If
                Case MessageType.Shout
                    PostMessage(Color.LightCoral, String.Format("<{0}> {1}", e.WhoFrom.Name, e.Message))
                    If e.WhoFrom.ID <> _myPerson.ID Then
                        'FlashWindow(Me.Handle)
                    End If
                Case MessageType.Emote
                    PostMessage(Color.Yellow, String.Format("* {0} {1}", e.WhoFrom.Name, e.Message))
                    If e.WhoFrom.ID <> _myPerson.ID Then
                        'FlashWindow(Me.Handle)
                    End If
                Case MessageType.Tell
                    _tells.Insert(0, e.WhoFrom.Name)
                    PostMessage(Color.Violet, String.Format("{0} >> {1}", e.WhoFrom.Name, e.Message))
                    If e.WhoFrom.ID <> _myPerson.ID Then
                        'FlashWindow(Me.Handle)
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
                    If Not Me.tvUserList.Nodes.ContainsKey("bannedNode") Then
                        AddNode("", "bannedNode", "Banned Users")
                    End If
                    If Not Me.tvUserList.Nodes("bannedNode").Nodes.ContainsKey(e.WhoFrom.ID) Then
                        AddNode("bannedNode", e.WhoFrom.ID, e.WhoFrom.Name)
                    End If
                Case MessageType.BanRemoved
                    If Me.tvUserList.Nodes.ContainsKey("bannedNode") Then
                        RemoveNode("bannedNode", e.WhoFrom.ID)
                    End If
            End Select

        End If
    End Sub

    Private Sub Proxy_Event(ByVal sender As Object, ByVal e As ProxyEventArgs) Handles _Proxy.ProxyEvent
        If Me.InvokeRequired Then
            Me.Invoke(New ProxyEventCallback(AddressOf Proxy_Event), sender, e)
        Else
            If Not e.state Is Nothing Then
                
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
                            If Not Me.tvUserList.Nodes.ContainsKey("bannedNode") Then
                                AddNode("", "bannedNode", "Banned Users")
                            End If
                            _myPerson = person
                        Else
                            Me.lblHeder.Text = "ApRadar Chat"
                            For Each item As ToolStripItem In Me.adminMenu.Items
                                If item.Tag = "admin" Then
                                    item.Visible = False
                                End If
                            Next
                        End If
                    End If
                    AddNode(person)
                Next
                PostMessage(Color.Orange, "<<< Welcome to ApRadar Chat >>>")
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

    Private Sub tvUserList_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvUserList.NodeMouseDoubleClick
        Dim c As Channel = GetChannel(e.Node.Name)
        If Not c Is Nothing Then
            If c.Password <> String.Empty Then
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

    End Sub

End Class