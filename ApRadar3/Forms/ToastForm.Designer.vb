<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ToastForm
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
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.PictureBox()
        Me.hideTimer = New System.Windows.Forms.Timer(Me.components)
        Me.lnkSomeLink = New System.Windows.Forms.LinkLabel()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.ForeColor = System.Drawing.Color.Yellow
        Me.lblMessage.Location = New System.Drawing.Point(9, 38)
        Me.lblMessage.MaximumSize = New System.Drawing.Size(219, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(43, 14)
        Me.lblMessage.TabIndex = 3
        Me.lblMessage.Text = "Label2"
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackgroundImage = Global.ApRadar3.My.Resources.Resources.tbg
        Me.HeaderPanel.Controls.Add(Me.lblHeder)
        Me.HeaderPanel.Controls.Add(Me.btnClose)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Padding = New System.Windows.Forms.Padding(5, 0, 6, 0)
        Me.HeaderPanel.Size = New System.Drawing.Size(243, 33)
        Me.HeaderPanel.TabIndex = 5
        '
        'lblHeder
        '
        Me.lblHeder.AutoSize = True
        Me.lblHeder.BackColor = System.Drawing.Color.Transparent
        Me.lblHeder.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeder.ForeColor = System.Drawing.Color.White
        Me.lblHeder.Location = New System.Drawing.Point(9, 6)
        Me.lblHeder.Name = "lblHeder"
        Me.lblHeder.Size = New System.Drawing.Size(99, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "ApRadar Alert!"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.btnClose.Location = New System.Drawing.Point(221, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(16, 33)
        Me.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnClose.TabIndex = 0
        Me.btnClose.TabStop = False
        '
        'hideTimer
        '
        Me.hideTimer.Interval = 30000
        '
        'lnkSomeLink
        '
        Me.lnkSomeLink.AutoSize = True
        Me.lnkSomeLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkSomeLink.LinkColor = System.Drawing.Color.Yellow
        Me.lnkSomeLink.Location = New System.Drawing.Point(9, 52)
        Me.lnkSomeLink.Name = "lnkSomeLink"
        Me.lnkSomeLink.Size = New System.Drawing.Size(65, 14)
        Me.lnkSomeLink.TabIndex = 6
        Me.lnkSomeLink.TabStop = True
        Me.lnkSomeLink.Text = "LinkLabel1"
        Me.lnkSomeLink.Visible = False
        '
        'ToastForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(243, 59)
        Me.Controls.Add(Me.lnkSomeLink)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Controls.Add(Me.lblMessage)
        Me.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ToastForm"
        Me.Padding = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ToastForm"
        Me.TopMost = True
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.PictureBox
    Friend WithEvents hideTimer As System.Windows.Forms.Timer
    Friend WithEvents lnkSomeLink As System.Windows.Forms.LinkLabel
End Class
