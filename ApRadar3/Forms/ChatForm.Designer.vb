<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChatForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChatForm))
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        Me.blnClose = New System.Windows.Forms.PictureBox()
        Me.cmdConnect = New System.Windows.Forms.Button()
        Me.cmdDisconnect = New System.Windows.Forms.Button()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.rtbLogBox = New System.Windows.Forms.RichTextBox()
        Me.cmdSend = New System.Windows.Forms.Button()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tvUserList = New System.Windows.Forms.TreeView()
        Me.adminMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PromoteAdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveAdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.KickUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BanUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateChannelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteChannelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lbBannedList = New System.Windows.Forms.ListBox()
        Me.banMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveBanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BodyPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdSettings = New System.Windows.Forms.Button()
        Me.tbTransparency = New System.Windows.Forms.TrackBar()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.adminMenu.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.banMenu.SuspendLayout()
        Me.BodyPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.tbTransparency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackgroundImage = Global.ApRadar3.My.Resources.Resources.tbg
        Me.HeaderPanel.Controls.Add(Me.lblHeder)
        Me.HeaderPanel.Controls.Add(Me.blnClose)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Padding = New System.Windows.Forms.Padding(5, 0, 6, 0)
        Me.HeaderPanel.Size = New System.Drawing.Size(623, 33)
        Me.HeaderPanel.TabIndex = 6
        '
        'lblHeder
        '
        Me.lblHeder.AutoSize = True
        Me.lblHeder.BackColor = System.Drawing.Color.Transparent
        Me.lblHeder.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeder.ForeColor = System.Drawing.Color.White
        Me.lblHeder.Location = New System.Drawing.Point(9, 6)
        Me.lblHeder.Name = "lblHeder"
        Me.lblHeder.Size = New System.Drawing.Size(91, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "ApRadar Chat"
        '
        'blnClose
        '
        Me.blnClose.BackColor = System.Drawing.Color.Transparent
        Me.blnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.blnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.blnClose.Location = New System.Drawing.Point(601, 0)
        Me.blnClose.Name = "blnClose"
        Me.blnClose.Size = New System.Drawing.Size(16, 33)
        Me.blnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.blnClose.TabIndex = 0
        Me.blnClose.TabStop = False
        '
        'cmdConnect
        '
        Me.cmdConnect.Location = New System.Drawing.Point(244, 17)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(75, 23)
        Me.cmdConnect.TabIndex = 2
        Me.cmdConnect.Tag = "exclude"
        Me.cmdConnect.Text = "Connect"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.Enabled = False
        Me.cmdDisconnect.Location = New System.Drawing.Point(325, 17)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.cmdDisconnect.TabIndex = 3
        Me.cmdDisconnect.Tag = "exclude"
        Me.cmdDisconnect.Text = "Disconnect"
        Me.cmdDisconnect.UseVisualStyleBackColor = True
        '
        'txtLogin
        '
        Me.txtLogin.Location = New System.Drawing.Point(12, 19)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(110, 20)
        Me.txtLogin.TabIndex = 0
        Me.txtLogin.Tag = "exclude"
        '
        'rtbLogBox
        '
        Me.rtbLogBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.rtbLogBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbLogBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbLogBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbLogBox.ForeColor = System.Drawing.Color.White
        Me.rtbLogBox.Location = New System.Drawing.Point(3, 3)
        Me.rtbLogBox.Name = "rtbLogBox"
        Me.rtbLogBox.ReadOnly = True
        Me.rtbLogBox.Size = New System.Drawing.Size(414, 279)
        Me.rtbLogBox.TabIndex = 5
        Me.rtbLogBox.Tag = "exclude"
        Me.rtbLogBox.Text = ""
        '
        'cmdSend
        '
        Me.cmdSend.BackColor = System.Drawing.Color.Black
        Me.cmdSend.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdSend.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdSend.Location = New System.Drawing.Point(342, 3)
        Me.cmdSend.Name = "cmdSend"
        Me.cmdSend.Size = New System.Drawing.Size(75, 22)
        Me.cmdSend.TabIndex = 1
        Me.cmdSend.Tag = "exclude"
        Me.cmdSend.Text = "Send"
        Me.cmdSend.UseVisualStyleBackColor = False
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.Location = New System.Drawing.Point(3, 3)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(333, 22)
        Me.txtMessage.TabIndex = 0
        Me.txtMessage.Tag = "exclude"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(9, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Forum Username"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(128, 19)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txtPassword.Size = New System.Drawing.Size(110, 20)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.Tag = "exclude"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(125, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Forum Password"
        '
        'tvUserList
        '
        Me.tvUserList.AllowDrop = True
        Me.tvUserList.BackColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.tvUserList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tvUserList.ContextMenuStrip = Me.adminMenu
        Me.tvUserList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvUserList.ForeColor = System.Drawing.SystemColors.Info
        Me.tvUserList.LineColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.tvUserList.Location = New System.Drawing.Point(3, 3)
        Me.tvUserList.Name = "tvUserList"
        Me.tvUserList.Size = New System.Drawing.Size(169, 211)
        Me.tvUserList.TabIndex = 0
        Me.tvUserList.Tag = "exclude"
        '
        'adminMenu
        '
        Me.adminMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PromoteAdminToolStripMenuItem, Me.RemoveAdminToolStripMenuItem, Me.ToolStripSeparator1, Me.KickUserToolStripMenuItem, Me.ToolStripSeparator2, Me.BanUserToolStripMenuItem, Me.ToolStripSeparator3, Me.CreateChannelToolStripMenuItem, Me.DeleteChannelToolStripMenuItem})
        Me.adminMenu.Name = "adminMenu"
        Me.adminMenu.Size = New System.Drawing.Size(160, 154)
        '
        'PromoteAdminToolStripMenuItem
        '
        Me.PromoteAdminToolStripMenuItem.Name = "PromoteAdminToolStripMenuItem"
        Me.PromoteAdminToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.PromoteAdminToolStripMenuItem.Tag = "admin"
        Me.PromoteAdminToolStripMenuItem.Text = "Promote Admin"
        Me.PromoteAdminToolStripMenuItem.Visible = False
        '
        'RemoveAdminToolStripMenuItem
        '
        Me.RemoveAdminToolStripMenuItem.Name = "RemoveAdminToolStripMenuItem"
        Me.RemoveAdminToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.RemoveAdminToolStripMenuItem.Tag = "admin"
        Me.RemoveAdminToolStripMenuItem.Text = "Remove Admin"
        Me.RemoveAdminToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(156, 6)
        Me.ToolStripSeparator1.Tag = "admin"
        Me.ToolStripSeparator1.Visible = False
        '
        'KickUserToolStripMenuItem
        '
        Me.KickUserToolStripMenuItem.Name = "KickUserToolStripMenuItem"
        Me.KickUserToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.KickUserToolStripMenuItem.Tag = "admin"
        Me.KickUserToolStripMenuItem.Text = "Kick User"
        Me.KickUserToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(156, 6)
        Me.ToolStripSeparator2.Tag = "admin"
        Me.ToolStripSeparator2.Visible = False
        '
        'BanUserToolStripMenuItem
        '
        Me.BanUserToolStripMenuItem.Name = "BanUserToolStripMenuItem"
        Me.BanUserToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.BanUserToolStripMenuItem.Tag = "admin"
        Me.BanUserToolStripMenuItem.Text = "Ban User"
        Me.BanUserToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(156, 6)
        Me.ToolStripSeparator3.Tag = "admin"
        Me.ToolStripSeparator3.Visible = False
        '
        'CreateChannelToolStripMenuItem
        '
        Me.CreateChannelToolStripMenuItem.Name = "CreateChannelToolStripMenuItem"
        Me.CreateChannelToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.CreateChannelToolStripMenuItem.Text = "Create Channel"
        '
        'DeleteChannelToolStripMenuItem
        '
        Me.DeleteChannelToolStripMenuItem.Name = "DeleteChannelToolStripMenuItem"
        Me.DeleteChannelToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.DeleteChannelToolStripMenuItem.Text = "Delete Channel"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(599, 317)
        Me.SplitContainer1.SplitterDistance = 420
        Me.SplitContainer1.TabIndex = 10
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.SplitContainer3.Panel1.Controls.Add(Me.rtbLogBox)
        Me.SplitContainer3.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtMessage)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdSend)
        Me.SplitContainer3.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer3.Size = New System.Drawing.Size(420, 317)
        Me.SplitContainer3.SplitterDistance = 285
        Me.SplitContainer3.TabIndex = 6
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.SplitContainer2.Panel1.Controls.Add(Me.tvUserList)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.SplitContainer2.Panel2.Controls.Add(Me.lbBannedList)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(175, 317)
        Me.SplitContainer2.SplitterDistance = 217
        Me.SplitContainer2.TabIndex = 10
        '
        'lbBannedList
        '
        Me.lbBannedList.BackColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.lbBannedList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lbBannedList.ContextMenuStrip = Me.banMenu
        Me.lbBannedList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbBannedList.ForeColor = System.Drawing.SystemColors.Info
        Me.lbBannedList.FormattingEnabled = True
        Me.lbBannedList.IntegralHeight = False
        Me.lbBannedList.Location = New System.Drawing.Point(3, 3)
        Me.lbBannedList.Name = "lbBannedList"
        Me.lbBannedList.Size = New System.Drawing.Size(169, 90)
        Me.lbBannedList.TabIndex = 0
        Me.lbBannedList.Tag = "exclude"
        '
        'banMenu
        '
        Me.banMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveBanToolStripMenuItem})
        Me.banMenu.Name = "banMenu"
        Me.banMenu.Size = New System.Drawing.Size(141, 26)
        '
        'RemoveBanToolStripMenuItem
        '
        Me.RemoveBanToolStripMenuItem.Name = "RemoveBanToolStripMenuItem"
        Me.RemoveBanToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.RemoveBanToolStripMenuItem.Text = "Remove Ban"
        '
        'BodyPanel
        '
        Me.BodyPanel.Controls.Add(Me.SplitContainer1)
        Me.BodyPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BodyPanel.Location = New System.Drawing.Point(0, 79)
        Me.BodyPanel.Name = "BodyPanel"
        Me.BodyPanel.Padding = New System.Windows.Forms.Padding(12, 0, 12, 5)
        Me.BodyPanel.Size = New System.Drawing.Size(623, 322)
        Me.BodyPanel.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdSettings)
        Me.Panel1.Controls.Add(Me.tbTransparency)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmdConnect)
        Me.Panel1.Controls.Add(Me.txtPassword)
        Me.Panel1.Controls.Add(Me.txtLogin)
        Me.Panel1.Controls.Add(Me.cmdDisconnect)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 33)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(623, 46)
        Me.Panel1.TabIndex = 0
        '
        'cmdSettings
        '
        Me.cmdSettings.Location = New System.Drawing.Point(406, 17)
        Me.cmdSettings.Name = "cmdSettings"
        Me.cmdSettings.Size = New System.Drawing.Size(75, 23)
        Me.cmdSettings.TabIndex = 10
        Me.cmdSettings.Tag = "exclude"
        Me.cmdSettings.Text = "Settings"
        Me.cmdSettings.UseVisualStyleBackColor = True
        '
        'tbTransparency
        '
        Me.tbTransparency.Location = New System.Drawing.Point(526, 13)
        Me.tbTransparency.Maximum = 100
        Me.tbTransparency.Minimum = 30
        Me.tbTransparency.Name = "tbTransparency"
        Me.tbTransparency.Size = New System.Drawing.Size(85, 45)
        Me.tbTransparency.TabIndex = 9
        Me.tbTransparency.TickFrequency = 10
        Me.tbTransparency.Value = 100
        '
        'ChatForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(623, 401)
        Me.Controls.Add(Me.BodyPanel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.HeaderPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ChatForm"
        Me.Text = "ApRadar Chat"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.adminMenu.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.banMenu.ResumeLayout(False)
        Me.BodyPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.tbTransparency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents blnClose As System.Windows.Forms.PictureBox
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents cmdDisconnect As System.Windows.Forms.Button
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents rtbLogBox As System.Windows.Forms.RichTextBox
    Friend WithEvents cmdSend As System.Windows.Forms.Button
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tvUserList As System.Windows.Forms.TreeView
    Friend WithEvents adminMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PromoteAdminToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveAdminToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents KickUserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BanUserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CreateChannelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteChannelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lbBannedList As System.Windows.Forms.ListBox
    Friend WithEvents BodyPanel As System.Windows.Forms.Panel
    Friend WithEvents banMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveBanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tbTransparency As System.Windows.Forms.TrackBar
    Friend WithEvents cmdSettings As System.Windows.Forms.Button
End Class
