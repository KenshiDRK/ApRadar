<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TodBrowserForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TodBrowserForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.PictureBox()
        Me.cboZones = New System.Windows.Forms.ComboBox()
        Me.rbDead = New System.Windows.Forms.RadioButton()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.dgTOD = New System.Windows.Forms.DataGridView()
        Me.ServerIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsDeadDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ZoneDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeathTimeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CmBinding = New System.Windows.Forms.BindingSource(Me.components)
        Me.HeaderPanel.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgTOD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmBinding, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(9, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Zone"
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
        Me.HeaderPanel.Size = New System.Drawing.Size(480, 33)
        Me.HeaderPanel.TabIndex = 4
        '
        'lblHeder
        '
        Me.lblHeder.AutoSize = True
        Me.lblHeder.BackColor = System.Drawing.Color.Transparent
        Me.lblHeder.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeder.ForeColor = System.Drawing.Color.White
        Me.lblHeder.Location = New System.Drawing.Point(9, 6)
        Me.lblHeder.Name = "lblHeder"
        Me.lblHeder.Size = New System.Drawing.Size(85, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "ToD Browser"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.btnClose.Location = New System.Drawing.Point(458, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(16, 33)
        Me.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnClose.TabIndex = 0
        Me.btnClose.TabStop = False
        '
        'cboZones
        '
        Me.cboZones.FormattingEnabled = True
        Me.cboZones.Location = New System.Drawing.Point(12, 57)
        Me.cboZones.Name = "cboZones"
        Me.cboZones.Size = New System.Drawing.Size(260, 21)
        Me.cboZones.TabIndex = 5
        '
        'rbDead
        '
        Me.rbDead.AutoSize = True
        Me.rbDead.Checked = True
        Me.rbDead.ForeColor = System.Drawing.Color.White
        Me.rbDead.Location = New System.Drawing.Point(349, 60)
        Me.rbDead.Name = "rbDead"
        Me.rbDead.Size = New System.Drawing.Size(75, 17)
        Me.rbDead.TabIndex = 8
        Me.rbDead.TabStop = True
        Me.rbDead.Text = "Dead Only"
        Me.rbDead.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.ForeColor = System.Drawing.Color.White
        Me.rbAll.Location = New System.Drawing.Point(307, 60)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(36, 17)
        Me.rbAll.TabIndex = 8
        Me.rbAll.Text = "All"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Image = CType(resources.GetObject("cmdRefresh.Image"), System.Drawing.Image)
        Me.cmdRefresh.Location = New System.Drawing.Point(278, 57)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(23, 23)
        Me.cmdRefresh.TabIndex = 9
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'dgTOD
        '
        Me.dgTOD.AllowUserToAddRows = False
        Me.dgTOD.AllowUserToDeleteRows = False
        Me.dgTOD.AutoGenerateColumns = False
        Me.dgTOD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgTOD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTOD.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ServerIDDataGridViewTextBoxColumn, Me.IDDataGridViewTextBoxColumn, Me.NameDataGridViewTextBoxColumn, Me.IsDeadDataGridViewCheckBoxColumn, Me.ZoneDataGridViewTextBoxColumn, Me.DeathTimeDataGridViewTextBoxColumn})
        Me.dgTOD.DataSource = Me.CmBinding
        Me.dgTOD.Location = New System.Drawing.Point(12, 86)
        Me.dgTOD.Name = "dgTOD"
        Me.dgTOD.ReadOnly = True
        Me.dgTOD.RowHeadersVisible = False
        Me.dgTOD.Size = New System.Drawing.Size(456, 341)
        Me.dgTOD.TabIndex = 10
        '
        'ServerIDDataGridViewTextBoxColumn
        '
        Me.ServerIDDataGridViewTextBoxColumn.DataPropertyName = "ServerID"
        Me.ServerIDDataGridViewTextBoxColumn.HeaderText = "ServerID"
        Me.ServerIDDataGridViewTextBoxColumn.Name = "ServerIDDataGridViewTextBoxColumn"
        Me.ServerIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.ServerIDDataGridViewTextBoxColumn.Visible = False
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        DataGridViewCellStyle1.Format = "X2"
        Me.IDDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Name"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        Me.NameDataGridViewTextBoxColumn.ReadOnly = True
        '
        'IsDeadDataGridViewCheckBoxColumn
        '
        Me.IsDeadDataGridViewCheckBoxColumn.DataPropertyName = "IsDead"
        Me.IsDeadDataGridViewCheckBoxColumn.HeaderText = "IsDead"
        Me.IsDeadDataGridViewCheckBoxColumn.Name = "IsDeadDataGridViewCheckBoxColumn"
        Me.IsDeadDataGridViewCheckBoxColumn.ReadOnly = True
        Me.IsDeadDataGridViewCheckBoxColumn.Visible = False
        '
        'ZoneDataGridViewTextBoxColumn
        '
        Me.ZoneDataGridViewTextBoxColumn.DataPropertyName = "Zone"
        Me.ZoneDataGridViewTextBoxColumn.HeaderText = "Zone"
        Me.ZoneDataGridViewTextBoxColumn.Name = "ZoneDataGridViewTextBoxColumn"
        Me.ZoneDataGridViewTextBoxColumn.ReadOnly = True
        Me.ZoneDataGridViewTextBoxColumn.Visible = False
        '
        'DeathTimeDataGridViewTextBoxColumn
        '
        Me.DeathTimeDataGridViewTextBoxColumn.DataPropertyName = "DeathTime"
        Me.DeathTimeDataGridViewTextBoxColumn.HeaderText = "DeathTime"
        Me.DeathTimeDataGridViewTextBoxColumn.Name = "DeathTimeDataGridViewTextBoxColumn"
        Me.DeathTimeDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CmBinding
        '
        Me.CmBinding.DataSource = GetType(DataLibrary.CampedMobsDataset.CampedMobsDataTable)
        '
        'TodBrowserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(480, 439)
        Me.Controls.Add(Me.dgTOD)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.rbAll)
        Me.Controls.Add(Me.rbDead)
        Me.Controls.Add(Me.cboZones)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 36)
        Me.Name = "TodBrowserForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ToD Browser"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgTOD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmBinding, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.PictureBox
    Friend WithEvents cboZones As System.Windows.Forms.ComboBox
    Friend WithEvents rbDead As System.Windows.Forms.RadioButton
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Private WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents dgTOD As System.Windows.Forms.DataGridView
    Friend WithEvents CmBinding As System.Windows.Forms.BindingSource
    Friend WithEvents ServerIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsDeadDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ZoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeathTimeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
