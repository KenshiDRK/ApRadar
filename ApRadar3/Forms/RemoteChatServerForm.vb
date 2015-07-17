Imports FFXIMemory
Imports Contracts.Client

Public Class RemoteChatServerForm
#Region " MEMBER VARIABLES "
    Private _dock As DockingClass
    Private _sof As FormAnimator

    Private lKeyboardHelper As Integer
    Private WithEvents _proxy As TunnelServerSingleton = TunnelServerSingleton.GetServerInstance()
    Private Delegate Sub EndConnectCallback(ByVal id As String)
    Private _clients As List(Of String)
    Private _lineCount As Integer
    Private WithEvents _watcher As Watcher
#End Region

#Region " FORM EVENTS "

    Private Sub RemoteChatServerForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Check to see if the proxy object needs to be destroyed
        If Not _proxy Is Nothing Then
            If Me.txtServerID.Text <> String.Empty Then
                _proxy.DisconnectServer(Me.txtServerID.Text)
            End If
            _proxy = Nothing
        End If
        'Check to see if the chat object needs to be shut down and destroyed
        If Not _watcher Is Nothing Then
            MemoryScanner.Scanner.DetachWatcher(_watcher)
        End If
        _sof.FadeOut(500)
    End Sub

    Private Sub RemoteChatServerForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
        _sof = New FormAnimator(Me)
        _sof.FadeIn(400)
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

    Private Sub blnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles blnClose.Click
        Me.Close()
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        If MemoryScanner.Scanner.IsRunning AndAlso Me.txtPassword.Text <> String.Empty Then
            If _watcher Is Nothing Then
                _watcher = New Watcher(MemoryScanner.WatcherTypes.Chat)
            End If
            MemoryScanner.Scanner.AttachWatcher(_watcher)
            lKeyboardHelper = Windower.CreateKeyboardHelper(String.Format("WindowerMMFKeyboardHandler_{0}", MemoryScanner.Scanner.FFXI.POL.Id))
        End If
        Dim enc As New Encrypt(Encrypt.EncryptionType.Rijndael)
        Dim pWord As String = enc.EncryptData(Me.txtPassword.Text)
        _proxy.BeginConnectServer(pWord)
    End Sub

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        If Not _watcher Is Nothing Then
            MemoryScanner.Scanner.DetachWatcher(_watcher)
            If lKeyboardHelper > 0 Then
                Windower.DeleteKeyboardHelper(lKeyboardHelper)
            End If
        End If
        Me.cmdStart.Enabled = True
        Me.cmdStop.Enabled = False
        _proxy.DisconnectServer(Me.txtServerID.Text)
        Me.txtServerID.Text = String.Empty
        Me.lblClients.Text = "0 Clients Connected..."

    End Sub
#End Region

#Region " WATCHER EVENTS "
    Private Sub OnNewChatLine(ByVal Line As MemoryScanner.ChatLine) Handles _watcher.OnNewChatLine
        Try
            Dim cLine As New Contracts.Shared.ChatLine() With {.ChatType = Line.ChatType, .color = Line.color, .Message = Line.LineText}
            If Me.txtServerID.Text <> String.Empty Then
                _proxy.SendToClient(Me.txtServerID.Text, cLine)
            End If
            Me.rtbLog.SelectionStart = Me.rtbLog.TextLength
            If Line.LineText.Contains(Chr(1)) Then
                Dim parts As String() = Line.LineText.Split(New Char() {Chr(1)}, 3)
                Me.rtbLog.SelectionColor = ColorTranslator.FromHtml("#" & Line.color)
                If parts.Count = 3 Then
                    Me.rtbLog.AppendText(String.Format("{0} : [{1}] {2}", Now.ToLongTimeString, Line.ChatType, parts(0)))
                    Me.rtbLog.SelectionColor = Color.LimeGreen
                    Me.rtbLog.AppendText(parts(1))
                    Me.rtbLog.SelectionColor = ColorTranslator.FromHtml("#" & Line.color)
                    Me.rtbLog.AppendText(String.Format("{0}{1}", parts(2), ControlChars.NewLine))
                Else
                    Me.rtbLog.AppendText(String.Format("{0} : [{1}] {2}{3}", Now.ToLongTimeString, Line.ChatType, Line.LineText, ControlChars.NewLine))
                End If


            Else
                Me.rtbLog.SelectionColor = ColorTranslator.FromHtml("#" & Line.color.Substring(2))
                Me.rtbLog.AppendText(String.Format("{0} : [{1}] {2}{3}", Now.ToLongTimeString, Line.ChatType, Line.LineText, ControlChars.NewLine))
            End If
            Me.rtbLog.ScrollToCaret()
            _lineCount += 1
            Me.lblLinesLogged.Text = String.Format("{0} Lines logged...", _lineCount)
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region " PROXY EVENTS "
    Private Sub Proxy_NewMessage(ByVal sender As Object, ByVal Message As Contracts.Shared.ClientMessage) Handles _proxy.NewMessage
        Select Case Message.Type
            Case Contracts.Shared.ClientMessageType.Connect
                _clients.Add(Message.ID)
                Me.lblClients.Text = String.Format("{0} Clients Connected...", _clients.Count())
                Me.rtbLog.SelectionStart = Me.rtbLog.TextLength
                Me.rtbLog.SelectionColor = Color.Orange
                Me.rtbLog.AppendText(String.Format("<<< Client Connected {0} >>>{1}", Message.ID, ControlChars.NewLine))

            Case Contracts.Shared.ClientMessageType.Disconnect
                _clients.Remove(Message.ID)
                Me.lblClients.Text = String.Format("{0} Clients Connected...", _clients.Count())
                Me.rtbLog.SelectionStart = Me.rtbLog.TextLength
                Me.rtbLog.SelectionColor = Color.Orange
                Me.rtbLog.AppendText(String.Format("<<< Client Disonnected {0} >>>{1}", Message.ID, ControlChars.NewLine))
            Case Contracts.Shared.ClientMessageType.Message
                Me.rtbLog.SelectionStart = Me.rtbLog.TextLength
                Me.rtbLog.SelectionColor = Color.White
                Me.rtbLog.AppendText(String.Format("Message from client: {0} {1}{2}", Message.ID, Message.Message, ControlChars.NewLine))
                Me.rtbLog.ScrollToCaret()
                If Not MemoryScanner.Scanner.FFXI Is Nothing AndAlso lKeyboardHelper > 0 Then
                    Windower.CKHSendString(lKeyboardHelper, Message.Message)
                End If
        End Select
    End Sub

    Private Sub Proxy_FinishConnect(ByVal sender As Object, ByVal ID As String) Handles _proxy.ConnectionFinished
        HandleEndConnect(ID)
        _lineCount = 0
    End Sub

    Private Sub HandleEndConnect(ByVal Id As String)
        If Me.InvokeRequired Then
            Me.Invoke(New EndConnectCallback(AddressOf HandleEndConnect), Id)
        Else
            Me.txtServerID.Text = Id
            Me.cmdStop.Enabled = True
            Me.cmdStart.Enabled = False
            _clients = New List(Of String)
        End If
    End Sub
#End Region

End Class