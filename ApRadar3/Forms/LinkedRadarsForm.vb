Imports Contracts.Shared
Imports Contracts.Client
Public Class LinkedRadarsForm
    Private _sof As FormAnimator
    Private _dock As DockingClass
    Private _lastIn As Integer
    Private _lastOut As Integer

    Private Delegate Sub CreatedServerCallback(ByVal server As LinkServerResult)
    Private Delegate Sub JoinedServerCallback(ByVal ClientID As String)

    Public Delegate Sub NewMessageEventHandler(ByVal e As LinkEventArgs)
    Public Delegate Sub ServerStatusEventHandler(ByVal Running As Boolean)

    Private WithEvents _proxy As LinkProxySingleton
    Public Event NewMessage As NewMessageEventHandler
    Private _clientCount As Integer
    Public Event ServerStatus As ServerStatusEventHandler

    Private Sub AboutForm_ForeColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ForeColorChanged
        UpdateControlColors(Me)
    End Sub

    Private Sub AboutForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If cmdStop.Enabled Then
            cmdStop.PerformClick()
        End If
        'Roll up the form
        _sof.FadeOut(500)

    End Sub

    Private Sub AboutForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ThemeHandler.ApplyTheme(Me)
        _sof = New FormAnimator(Me)
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
        _sof.FadeIn(500)
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
        Close()
    End Sub

    Private Sub rbCreateServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCreateServer.CheckedChanged, rbJoinServer.CheckedChanged
        If rbCreateServer.Checked Then
            cmdStart.Text = "Start Server"
            cmdStop.Text = "Stop Server"
            txtServerID.ReadOnly = True
        Else
            cmdStart.Text = "Connect"
            cmdStop.Text = "Disconnect"
            txtServerID.ReadOnly = False
        End If
    End Sub

#Region " PROXY EVENTS "
    Private Sub Proxy_NewMessage(ByVal e As LinkEventArgs) Handles _proxy.NewMessage
        If rbCreateServer.Checked Then
            If e.Type = LinkEventType.ClientConnected Then
                _clientCount += 1
                lblStatus.Text = String.Format("Status: {0} Clients connected...", _clientCount)
            ElseIf e.Type = LinkEventType.ClientDisconnected Then
                _clientCount -= 1
                RaiseEvent NewMessage(e)
                lblStatus.Text = String.Format("Status: {0} Clients connected...", _clientCount)
            End If
        End If
        If e.Type = LinkEventType.MobList Then
            RaiseEvent NewMessage(e)
            _lastIn = e.Mobs.Count
            lblInOut.Text = String.Format("Outgoing: {0}    Incoming: {1}", _lastOut, _lastIn)
        ElseIf e.Type = LinkEventType.ServerStopped Then
            cmdStop.PerformClick()
        End If
    End Sub

    Private Sub Proxy_CreatedServer(ByVal server As LinkServerResult) Handles _proxy.ServerCreated
        If InvokeRequired Then
            Invoke(New CreatedServerCallback(AddressOf Proxy_CreatedServer), server)
        Else
            txtServerID.Text = server.ServerID
            txtClientID.Text = server.ClientID
            cmdStop.Enabled = True
            cmdStart.Enabled = False
            lblStatus.Text = "Status: 0 Clients Connected..."
            RaiseEvent ServerStatus(True)
            pnlRB.Enabled = False
        End If
    End Sub

    Private Sub Proxy_JoinedServer(ByVal ClientID As String) Handles _proxy.ServerJoined
        If InvokeRequired Then
            Invoke(New JoinedServerCallback(AddressOf Proxy_JoinedServer), ClientID)
        Else
            If ClientID <> String.Empty Then
                txtClientID.Text = ClientID
                cmdStart.Enabled = False
                cmdStop.Enabled = True
                lblStatus.Text = "Status: Connected!"
                RaiseEvent ServerStatus(True)
                pnlRB.Enabled = False
            Else
                MessageBox.Show("There was an error connecting to that server, please verify the server ID and Password and try again")
            End If
        End If
    End Sub
#End Region

#Region " PUBLIC METHODS "
    Public Sub SendMessage(ByVal e As LinkEventArgs)
        e.ClientID = txtClientID.Text.Trim()
        e.ServerID = txtServerID.Text.Trim()
        _proxy.SendMessage(e)
        _lastOut = e.Mobs.Count
        lblInOut.Text = String.Format("Outgoing: {0}    Incoming: {1}", _lastOut, _lastIn)
    End Sub
#End Region

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        If _proxy Is Nothing Then
            _proxy = LinkProxySingleton.GetClientInstance()
        End If
        If rbCreateServer.Checked Then
            If txtPassword.Text <> String.Empty Then
                Dim enc As New Encrypt(Encrypt.EncryptionType.Rijndael)
                _proxy.CreateLinkServer(enc.EncryptData(txtPassword.Text))
            End If
        Else
            If txtServerID.Text <> String.Empty AndAlso txtPassword.Text <> String.Empty Then
                Dim enc As New Encrypt(Encrypt.EncryptionType.Rijndael)
                _proxy.JoinLinkServer(txtServerID.Text, enc.EncryptData(txtPassword.Text))
            End If
        End If
    End Sub

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        If rbCreateServer.Checked Then
            Dim le As New LinkEventArgs() With {.Type = LinkEventType.ServerStopped, .ClientID = txtClientID.Text, .ServerID = txtServerID.Text}
            _proxy.SendMessage(le)
            cmdStart.Enabled = True
            cmdStop.Enabled = False
            txtServerID.Text = String.Empty
            txtClientID.Text = String.Empty
            _proxy = Nothing
        Else
            Dim le As New LinkEventArgs() With {.Type = LinkEventType.ClientDisconnected, .ClientID = txtClientID.Text, .ServerID = txtServerID.Text}
            _proxy.SendMessage(le)
            cmdStop.Enabled = False
            cmdStart.Enabled = True
            txtClientID.Text = String.Empty
            _proxy = Nothing
        End If
        lblStatus.Text = "Status: Not Connected..."
        RaiseEvent ServerStatus(False)
        pnlRB.Enabled = True
    End Sub
End Class
