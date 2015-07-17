<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpawnAlertDialog
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SpawnAlertDialog))
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.PictureBox()
        Me.cmRightClick = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cboZones = New System.Windows.Forms.ComboBox()
        Me.dgSpawnAlerts = New System.Windows.Forms.DataGridView()
        Me.ColAlert = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MobIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MobNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ServerIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DisplayStringDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZoneMobsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cmdAddAllNM = New System.Windows.Forms.Button()
        Me.pbScan = New System.Windows.Forms.ProgressBar()
        Me.rbCurrentZone = New System.Windows.Forms.RadioButton()
        Me.rbAllZones = New System.Windows.Forms.RadioButton()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmRightClick.SuspendLayout()
        CType(Me.dgSpawnAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ZoneMobsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.HeaderPanel.Size = New System.Drawing.Size(519, 33)
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
        Me.lblHeder.Size = New System.Drawing.Size(107, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "Spawn Alert List"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.btnClose.Location = New System.Drawing.Point(497, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(16, 33)
        Me.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnClose.TabIndex = 0
        Me.btnClose.TabStop = False
        '
        'cmRightClick
        '
        Me.cmRightClick.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsAdd, Me.tsRemove})
        Me.cmRightClick.Name = "cmRightClick"
        Me.cmRightClick.Size = New System.Drawing.Size(244, 48)
        '
        'tsAdd
        '
        Me.tsAdd.Name = "tsAdd"
        Me.tsAdd.Size = New System.Drawing.Size(243, 22)
        Me.tsAdd.Text = "Add to Global Watch List"
        '
        'tsRemove
        '
        Me.tsRemove.Enabled = False
        Me.tsRemove.Name = "tsRemove"
        Me.tsRemove.Size = New System.Drawing.Size(243, 22)
        Me.tsRemove.Text = "Remove From Global Watch List"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Location = New System.Drawing.Point(432, 359)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cboZones
        '
        Me.cboZones.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboZones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboZones.FormattingEnabled = True
        Me.cboZones.Location = New System.Drawing.Point(12, 40)
        Me.cboZones.Name = "cboZones"
        Me.cboZones.Size = New System.Drawing.Size(495, 21)
        Me.cboZones.TabIndex = 8
        '
        'dgSpawnAlerts
        '
        Me.dgSpawnAlerts.AllowUserToAddRows = False
        Me.dgSpawnAlerts.AllowUserToDeleteRows = False
        Me.dgSpawnAlerts.AllowUserToOrderColumns = True
        Me.dgSpawnAlerts.AllowUserToResizeColumns = False
        Me.dgSpawnAlerts.AllowUserToResizeRows = False
        Me.dgSpawnAlerts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgSpawnAlerts.AutoGenerateColumns = False
        Me.dgSpawnAlerts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgSpawnAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSpawnAlerts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColAlert, Me.MobIDDataGridViewTextBoxColumn, Me.MobNameDataGridViewTextBoxColumn, Me.ServerIDDataGridViewTextBoxColumn, Me.DisplayStringDataGridViewTextBoxColumn})
        Me.dgSpawnAlerts.DataSource = Me.ZoneMobsBindingSource
        Me.dgSpawnAlerts.Location = New System.Drawing.Point(12, 67)
        Me.dgSpawnAlerts.MultiSelect = False
        Me.dgSpawnAlerts.Name = "dgSpawnAlerts"
        Me.dgSpawnAlerts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgSpawnAlerts.Size = New System.Drawing.Size(495, 286)
        Me.dgSpawnAlerts.TabIndex = 9
        '
        'ColAlert
        '
        Me.ColAlert.FillWeight = 68.19864!
        Me.ColAlert.HeaderText = "Show Alert"
        Me.ColAlert.Name = "ColAlert"
        Me.ColAlert.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'MobIDDataGridViewTextBoxColumn
        '
        Me.MobIDDataGridViewTextBoxColumn.DataPropertyName = "MobID"
        DataGridViewCellStyle1.Format = "X2"
        Me.MobIDDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.MobIDDataGridViewTextBoxColumn.FillWeight = 70.01083!
        Me.MobIDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.MobIDDataGridViewTextBoxColumn.Name = "MobIDDataGridViewTextBoxColumn"
        Me.MobIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'MobNameDataGridViewTextBoxColumn
        '
        Me.MobNameDataGridViewTextBoxColumn.DataPropertyName = "MobName"
        Me.MobNameDataGridViewTextBoxColumn.FillWeight = 130.4932!
        Me.MobNameDataGridViewTextBoxColumn.HeaderText = "Name"
        Me.MobNameDataGridViewTextBoxColumn.Name = "MobNameDataGridViewTextBoxColumn"
        Me.MobNameDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ServerIDDataGridViewTextBoxColumn
        '
        Me.ServerIDDataGridViewTextBoxColumn.DataPropertyName = "ServerID"
        Me.ServerIDDataGridViewTextBoxColumn.HeaderText = "ServerID"
        Me.ServerIDDataGridViewTextBoxColumn.Name = "ServerIDDataGridViewTextBoxColumn"
        Me.ServerIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.ServerIDDataGridViewTextBoxColumn.Visible = False
        '
        'DisplayStringDataGridViewTextBoxColumn
        '
        Me.DisplayStringDataGridViewTextBoxColumn.DataPropertyName = "DisplayString"
        Me.DisplayStringDataGridViewTextBoxColumn.HeaderText = "DisplayString"
        Me.DisplayStringDataGridViewTextBoxColumn.Name = "DisplayStringDataGridViewTextBoxColumn"
        Me.DisplayStringDataGridViewTextBoxColumn.ReadOnly = True
        Me.DisplayStringDataGridViewTextBoxColumn.Visible = False
        '
        'ZoneMobsBindingSource
        '
        Me.ZoneMobsBindingSource.DataSource = GetType(FFXIMemory.ZoneMobs)
        '
        'cmdAddAllNM
        '
        Me.cmdAddAllNM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAddAllNM.Location = New System.Drawing.Point(12, 359)
        Me.cmdAddAllNM.Name = "cmdAddAllNM"
        Me.cmdAddAllNM.Size = New System.Drawing.Size(168, 23)
        Me.cmdAddAllNM.TabIndex = 10
        Me.cmdAddAllNM.Text = "Add NM's to Global Watch List"
        Me.cmdAddAllNM.UseVisualStyleBackColor = True
        '
        'pbScan
        '
        Me.pbScan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbScan.Location = New System.Drawing.Point(354, 359)
        Me.pbScan.Name = "pbScan"
        Me.pbScan.Size = New System.Drawing.Size(72, 23)
        Me.pbScan.TabIndex = 11
        Me.pbScan.Visible = False
        '
        'rbCurrentZone
        '
        Me.rbCurrentZone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbCurrentZone.AutoSize = True
        Me.rbCurrentZone.Checked = True
        Me.rbCurrentZone.ForeColor = System.Drawing.Color.White
        Me.rbCurrentZone.Location = New System.Drawing.Point(186, 362)
        Me.rbCurrentZone.Name = "rbCurrentZone"
        Me.rbCurrentZone.Size = New System.Drawing.Size(87, 17)
        Me.rbCurrentZone.TabIndex = 12
        Me.rbCurrentZone.TabStop = True
        Me.rbCurrentZone.Text = "Current Zone"
        Me.rbCurrentZone.UseVisualStyleBackColor = True
        '
        'rbAllZones
        '
        Me.rbAllZones.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbAllZones.AutoSize = True
        Me.rbAllZones.ForeColor = System.Drawing.Color.White
        Me.rbAllZones.Location = New System.Drawing.Point(279, 362)
        Me.rbAllZones.Name = "rbAllZones"
        Me.rbAllZones.Size = New System.Drawing.Size(69, 17)
        Me.rbAllZones.TabIndex = 12
        Me.rbAllZones.Text = "All Zones"
        Me.rbAllZones.UseVisualStyleBackColor = True
        '
        'SpawnAlertDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(519, 394)
        Me.Controls.Add(Me.rbAllZones)
        Me.Controls.Add(Me.rbCurrentZone)
        Me.Controls.Add(Me.pbScan)
        Me.Controls.Add(Me.cmdAddAllNM)
        Me.Controls.Add(Me.dgSpawnAlerts)
        Me.Controls.Add(Me.cboZones)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.HeaderPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(197, 175)
        Me.Name = "SpawnAlertDialog"
        Me.Text = "Spawn Alerts"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmRightClick.ResumeLayout(False)
        CType(Me.dgSpawnAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ZoneMobsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmRightClick As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboZones As System.Windows.Forms.ComboBox
    Friend WithEvents dgSpawnAlerts As System.Windows.Forms.DataGridView
    Friend WithEvents ZoneMobsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ColAlert As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents MobIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MobNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ServerIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DisplayStringDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdAddAllNM As System.Windows.Forms.Button
    Friend WithEvents pbScan As System.Windows.Forms.ProgressBar
    Friend WithEvents rbCurrentZone As System.Windows.Forms.RadioButton
    Friend WithEvents rbAllZones As System.Windows.Forms.RadioButton
End Class
