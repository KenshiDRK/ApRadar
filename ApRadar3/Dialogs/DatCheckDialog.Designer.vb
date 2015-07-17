<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatCheckDialog
    Inherits ApRadar3.ResizableForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DatCheckDialog))
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.PictureBox()
        Me.cmdScan = New System.Windows.Forms.Button()
        Me.cmdAddMobs = New System.Windows.Forms.Button()
        Me.pbProgress = New System.Windows.Forms.ProgressBar()
        Me.dgMobsToAdd = New System.Windows.Forms.DataGridView()
        Me.lblResult = New System.Windows.Forms.Label()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgMobsToAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.HeaderPanel.Size = New System.Drawing.Size(375, 33)
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
        Me.lblHeder.Size = New System.Drawing.Size(82, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "Dat Checker"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.btnClose.Location = New System.Drawing.Point(353, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(16, 33)
        Me.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnClose.TabIndex = 0
        Me.btnClose.TabStop = False
        '
        'cmdScan
        '
        Me.cmdScan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdScan.Location = New System.Drawing.Point(207, 217)
        Me.cmdScan.Name = "cmdScan"
        Me.cmdScan.Size = New System.Drawing.Size(75, 23)
        Me.cmdScan.TabIndex = 6
        Me.cmdScan.Tag = "exclude"
        Me.cmdScan.Text = "Scan Dats"
        Me.cmdScan.UseVisualStyleBackColor = True
        '
        'cmdAddMobs
        '
        Me.cmdAddMobs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddMobs.Enabled = False
        Me.cmdAddMobs.Location = New System.Drawing.Point(288, 217)
        Me.cmdAddMobs.Name = "cmdAddMobs"
        Me.cmdAddMobs.Size = New System.Drawing.Size(75, 23)
        Me.cmdAddMobs.TabIndex = 6
        Me.cmdAddMobs.Tag = "exclude"
        Me.cmdAddMobs.Text = "Add Mobs"
        Me.cmdAddMobs.UseVisualStyleBackColor = True
        '
        'pbProgress
        '
        Me.pbProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbProgress.Location = New System.Drawing.Point(12, 199)
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(351, 12)
        Me.pbProgress.TabIndex = 7
        Me.pbProgress.Tag = "exclude"
        '
        'dgMobsToAdd
        '
        Me.dgMobsToAdd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgMobsToAdd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgMobsToAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgMobsToAdd.Location = New System.Drawing.Point(12, 39)
        Me.dgMobsToAdd.Name = "dgMobsToAdd"
        Me.dgMobsToAdd.Size = New System.Drawing.Size(351, 154)
        Me.dgMobsToAdd.TabIndex = 8
        Me.dgMobsToAdd.Tag = "exclude"
        '
        'lblResult
        '
        Me.lblResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblResult.ForeColor = System.Drawing.Color.White
        Me.lblResult.Location = New System.Drawing.Point(12, 222)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(189, 18)
        Me.lblResult.TabIndex = 9
        '
        'DatCheckDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(375, 252)
        Me.Controls.Add(Me.lblResult)
        Me.Controls.Add(Me.dgMobsToAdd)
        Me.Controls.Add(Me.pbProgress)
        Me.Controls.Add(Me.cmdAddMobs)
        Me.Controls.Add(Me.cmdScan)
        Me.Controls.Add(Me.HeaderPanel)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DatCheckDialog"
        Me.Text = "Dat Checker"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgMobsToAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.PictureBox
    Friend WithEvents cmdScan As System.Windows.Forms.Button
    Friend WithEvents cmdAddMobs As System.Windows.Forms.Button
    Friend WithEvents pbProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents dgMobsToAdd As System.Windows.Forms.DataGridView
    Friend WithEvents lblResult As System.Windows.Forms.Label
End Class
