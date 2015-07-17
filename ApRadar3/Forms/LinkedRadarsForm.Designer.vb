<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LinkedRadarsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LinkedRadarsForm))
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        Me.blnClose = New System.Windows.Forms.PictureBox()
        Me.rbCreateServer = New System.Windows.Forms.RadioButton()
        Me.rbJoinServer = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtServerID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtClientID = New System.Windows.Forms.TextBox()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.pnlRB = New System.Windows.Forms.Panel()
        Me.lblInOut = New System.Windows.Forms.Label()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRB.SuspendLayout()
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
        Me.HeaderPanel.Size = New System.Drawing.Size(258, 33)
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
        Me.lblHeder.Size = New System.Drawing.Size(93, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "Linked Radars"
        '
        'blnClose
        '
        Me.blnClose.BackColor = System.Drawing.Color.Transparent
        Me.blnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.blnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.blnClose.Location = New System.Drawing.Point(236, 0)
        Me.blnClose.Name = "blnClose"
        Me.blnClose.Size = New System.Drawing.Size(16, 33)
        Me.blnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.blnClose.TabIndex = 0
        Me.blnClose.TabStop = False
        '
        'rbCreateServer
        '
        Me.rbCreateServer.AutoSize = True
        Me.rbCreateServer.Checked = True
        Me.rbCreateServer.Location = New System.Drawing.Point(5, 6)
        Me.rbCreateServer.Name = "rbCreateServer"
        Me.rbCreateServer.Size = New System.Drawing.Size(90, 17)
        Me.rbCreateServer.TabIndex = 0
        Me.rbCreateServer.TabStop = True
        Me.rbCreateServer.Text = "Create Server"
        Me.rbCreateServer.UseVisualStyleBackColor = True
        '
        'rbJoinServer
        '
        Me.rbJoinServer.AutoSize = True
        Me.rbJoinServer.Location = New System.Drawing.Point(101, 6)
        Me.rbJoinServer.Name = "rbJoinServer"
        Me.rbJoinServer.Size = New System.Drawing.Size(78, 17)
        Me.rbJoinServer.TabIndex = 1
        Me.rbJoinServer.Text = "Join Server"
        Me.rbJoinServer.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Server Password"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(12, 77)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txtPassword.Size = New System.Drawing.Size(234, 20)
        Me.txtPassword.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Server ID"
        '
        'txtServerID
        '
        Me.txtServerID.Location = New System.Drawing.Point(12, 117)
        Me.txtServerID.Name = "txtServerID"
        Me.txtServerID.ReadOnly = True
        Me.txtServerID.Size = New System.Drawing.Size(234, 20)
        Me.txtServerID.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Client ID"
        '
        'txtClientID
        '
        Me.txtClientID.Location = New System.Drawing.Point(12, 157)
        Me.txtClientID.Name = "txtClientID"
        Me.txtClientID.ReadOnly = True
        Me.txtClientID.Size = New System.Drawing.Size(234, 20)
        Me.txtClientID.TabIndex = 9
        Me.txtClientID.TabStop = False
        '
        'cmdStart
        '
        Me.cmdStart.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.cmdStart.Location = New System.Drawing.Point(12, 183)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(114, 23)
        Me.cmdStart.TabIndex = 3
        Me.cmdStart.Text = "Start Server"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(9, 209)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(124, 13)
        Me.lblStatus.TabIndex = 11
        Me.lblStatus.Text = "Status: Not Connected..."
        '
        'cmdStop
        '
        Me.cmdStop.Enabled = False
        Me.cmdStop.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.cmdStop.Location = New System.Drawing.Point(132, 183)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(114, 23)
        Me.cmdStop.TabIndex = 4
        Me.cmdStop.Text = "Stop Server"
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'pnlRB
        '
        Me.pnlRB.Controls.Add(Me.rbJoinServer)
        Me.pnlRB.Controls.Add(Me.rbCreateServer)
        Me.pnlRB.Location = New System.Drawing.Point(7, 34)
        Me.pnlRB.Name = "pnlRB"
        Me.pnlRB.Size = New System.Drawing.Size(194, 26)
        Me.pnlRB.TabIndex = 0
        '
        'lblInOut
        '
        Me.lblInOut.AutoSize = True
        Me.lblInOut.Location = New System.Drawing.Point(9, 225)
        Me.lblInOut.Name = "lblInOut"
        Me.lblInOut.Size = New System.Drawing.Size(129, 13)
        Me.lblInOut.TabIndex = 11
        Me.lblInOut.Text = "Outgoing: 0    Incoming: 0"
        '
        'LinkedRadarsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(258, 246)
        Me.Controls.Add(Me.pnlRB)
        Me.Controls.Add(Me.lblInOut)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.cmdStop)
        Me.Controls.Add(Me.cmdStart)
        Me.Controls.Add(Me.txtClientID)
        Me.Controls.Add(Me.txtServerID)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.HeaderPanel)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "LinkedRadarsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Linked Radars"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRB.ResumeLayout(False)
        Me.pnlRB.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents blnClose As System.Windows.Forms.PictureBox
    Friend WithEvents rbCreateServer As System.Windows.Forms.RadioButton
    Friend WithEvents rbJoinServer As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtServerID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtClientID As System.Windows.Forms.TextBox
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents pnlRB As System.Windows.Forms.Panel
    Friend WithEvents lblInOut As System.Windows.Forms.Label
End Class
