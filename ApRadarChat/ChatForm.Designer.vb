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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChatForm))
        Me.HeaderPanel = New System.Windows.Forms.Panel
        Me.lblHeder = New System.Windows.Forms.Label
        Me.blnClose = New System.Windows.Forms.PictureBox
        Me.cmdConnect = New System.Windows.Forms.Button
        Me.cmdDisconnect = New System.Windows.Forms.Button
        Me.txtLogin = New System.Windows.Forms.TextBox
        Me.rtbLogBox = New System.Windows.Forms.RichTextBox
        Me.cmdSend = New System.Windows.Forms.Button
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.lbl = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tvUserList = New System.Windows.Forms.TreeView
        Me.adminMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PromoteAdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveAdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.KickUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BanUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveBanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HeaderPanel.SuspendLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.adminMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackgroundImage = Global.ApRadarChat.My.Resources.Resources.tbg
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
        Me.blnClose.Image = Global.ApRadarChat.My.Resources.Resources.Close
        Me.blnClose.Location = New System.Drawing.Point(601, 0)
        Me.blnClose.Name = "blnClose"
        Me.blnClose.Size = New System.Drawing.Size(16, 33)
        Me.blnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.blnClose.TabIndex = 0
        Me.blnClose.TabStop = False
        '
        'cmdConnect
        '
        Me.cmdConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdConnect.Location = New System.Drawing.Point(295, 50)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(75, 23)
        Me.cmdConnect.TabIndex = 2
        Me.cmdConnect.Tag = "exclude"
        Me.cmdConnect.Text = "Connect"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDisconnect.Enabled = False
        Me.cmdDisconnect.Location = New System.Drawing.Point(376, 50)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.cmdDisconnect.TabIndex = 7
        Me.cmdDisconnect.Tag = "exclude"
        Me.cmdDisconnect.Text = "Disconnect"
        Me.cmdDisconnect.UseVisualStyleBackColor = True
        '
        'txtLogin
        '
        Me.txtLogin.Location = New System.Drawing.Point(12, 52)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(140, 20)
        Me.txtLogin.TabIndex = 0
        Me.txtLogin.Tag = "exclude"
        '
        'rtbLogBox
        '
        Me.rtbLogBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbLogBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.rtbLogBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbLogBox.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbLogBox.ForeColor = System.Drawing.Color.White
        Me.rtbLogBox.Location = New System.Drawing.Point(12, 79)
        Me.rtbLogBox.Name = "rtbLogBox"
        Me.rtbLogBox.ReadOnly = True
        Me.rtbLogBox.Size = New System.Drawing.Size(439, 253)
        Me.rtbLogBox.TabIndex = 5
        Me.rtbLogBox.Tag = "exclude"
        Me.rtbLogBox.Text = ""
        '
        'cmdSend
        '
        Me.cmdSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSend.Location = New System.Drawing.Point(376, 338)
        Me.cmdSend.Name = "cmdSend"
        Me.cmdSend.Size = New System.Drawing.Size(75, 23)
        Me.cmdSend.TabIndex = 4
        Me.cmdSend.Tag = "exclude"
        Me.cmdSend.Text = "Send"
        Me.cmdSend.UseVisualStyleBackColor = True
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.Location = New System.Drawing.Point(12, 340)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(358, 20)
        Me.txtMessage.TabIndex = 3
        Me.txtMessage.Tag = "exclude"
        '
        'lbl
        '
        Me.lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.White
        Me.lbl.Location = New System.Drawing.Point(9, 363)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(602, 42)
        Me.lbl.TabIndex = 7
        Me.lbl.Text = resources.GetString("lbl.Text")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(9, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Forum Username"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(158, 52)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txtPassword.Size = New System.Drawing.Size(131, 20)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.Tag = "exclude"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(155, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Forum Password"
        '
        'tvUserList
        '
        Me.tvUserList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvUserList.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.tvUserList.ForeColor = System.Drawing.SystemColors.Info
        Me.tvUserList.LineColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.tvUserList.Location = New System.Drawing.Point(457, 52)
        Me.tvUserList.Name = "tvUserList"
        Me.tvUserList.Size = New System.Drawing.Size(154, 308)
        Me.tvUserList.TabIndex = 9
        '
        'adminMenu
        '
        Me.adminMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PromoteAdminToolStripMenuItem, Me.RemoveAdminToolStripMenuItem, Me.ToolStripSeparator1, Me.KickUserToolStripMenuItem, Me.BanUserToolStripMenuItem, Me.RemoveBanToolStripMenuItem})
        Me.adminMenu.Name = "adminMenu"
        Me.adminMenu.Size = New System.Drawing.Size(160, 120)
        '
        'PromoteAdminToolStripMenuItem
        '
        Me.PromoteAdminToolStripMenuItem.Name = "PromoteAdminToolStripMenuItem"
        Me.PromoteAdminToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.PromoteAdminToolStripMenuItem.Text = "Promote Admin"
        '
        'RemoveAdminToolStripMenuItem
        '
        Me.RemoveAdminToolStripMenuItem.Name = "RemoveAdminToolStripMenuItem"
        Me.RemoveAdminToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.RemoveAdminToolStripMenuItem.Text = "Remove Admin"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(156, 6)
        '
        'KickUserToolStripMenuItem
        '
        Me.KickUserToolStripMenuItem.Name = "KickUserToolStripMenuItem"
        Me.KickUserToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.KickUserToolStripMenuItem.Text = "Kick User"
        '
        'BanUserToolStripMenuItem
        '
        Me.BanUserToolStripMenuItem.Name = "BanUserToolStripMenuItem"
        Me.BanUserToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.BanUserToolStripMenuItem.Text = "Ban User"
        '
        'RemoveBanToolStripMenuItem
        '
        Me.RemoveBanToolStripMenuItem.Name = "RemoveBanToolStripMenuItem"
        Me.RemoveBanToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.RemoveBanToolStripMenuItem.Text = "Remove Ban"
        '
        'ChatForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(623, 414)
        Me.Controls.Add(Me.tvUserList)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbl)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.rtbLogBox)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtLogin)
        Me.Controls.Add(Me.cmdSend)
        Me.Controls.Add(Me.cmdDisconnect)
        Me.Controls.Add(Me.cmdConnect)
        Me.Controls.Add(Me.HeaderPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChatForm"
        Me.Text = "ChatForm"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.adminMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents lbl As System.Windows.Forms.Label
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
    Friend WithEvents RemoveBanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
