<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MobDataForm
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
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.lblMobName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lstItems = New System.Windows.Forms.ListBox()
        Me.picEx = New System.Windows.Forms.PictureBox()
        Me.picRare = New System.Windows.Forms.PictureBox()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.picIcon = New System.Windows.Forms.PictureBox()
        Me.lblItemName = New System.Windows.Forms.Label()
        Me.lblWeakness = New System.Windows.Forms.Label()
        Me.lblDetection = New System.Windows.Forms.Label()
        Me.lblBehavior = New System.Windows.Forms.Label()
        Me.lblLevel = New System.Windows.Forms.Label()
        Me.lblJob = New System.Windows.Forms.Label()
        Me.lblFamily = New System.Windows.Forms.Label()
        Me.lblResistances = New System.Windows.Forms.Label()
        Me.lblImmune = New System.Windows.Forms.Label()
        Me.lblStolen = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblComments = New System.Windows.Forms.Label()
        Me.cmdSpawnAlert = New System.Windows.Forms.Button()
        CType(Me.picEx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdEdit
        '
        Me.cmdEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEdit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.cmdEdit.Location = New System.Drawing.Point(282, 283)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(50, 22)
        Me.cmdEdit.TabIndex = 1
        Me.cmdEdit.TabStop = False
        Me.cmdEdit.Text = "Edit"
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'lblMobName
        '
        Me.lblMobName.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblMobName, 4)
        Me.lblMobName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobName.ForeColor = System.Drawing.Color.Tomato
        Me.lblMobName.Location = New System.Drawing.Point(3, 0)
        Me.lblMobName.Name = "lblMobName"
        Me.lblMobName.Size = New System.Drawing.Size(51, 16)
        Me.lblMobName.TabIndex = 1
        Me.lblMobName.Tag = "exclude"
        Me.lblMobName.Text = "Label1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Family: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(204, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Job: "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Level:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(204, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Behavior:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 14)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Detection:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(204, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 14)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Weaknesses:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 14)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Resistances:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(204, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 14)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Immunities:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 14)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Stolen:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 86)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 14)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Comments:"
        '
        'lstItems
        '
        Me.lstItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstItems.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstItems.ForeColor = System.Drawing.Color.LimeGreen
        Me.lstItems.FormattingEnabled = True
        Me.lstItems.IntegralHeight = False
        Me.lstItems.ItemHeight = 14
        Me.lstItems.Location = New System.Drawing.Point(3, 117)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(125, 137)
        Me.lstItems.TabIndex = 0
        Me.lstItems.Tag = "exclude"
        '
        'picEx
        '
        Me.picEx.Image = Global.ApRadar3.My.Resources.Resources.ico_ex
        Me.picEx.Location = New System.Drawing.Point(18, 0)
        Me.picEx.Name = "picEx"
        Me.picEx.Size = New System.Drawing.Size(20, 20)
        Me.picEx.TabIndex = 10
        Me.picEx.TabStop = False
        Me.picEx.Visible = False
        '
        'picRare
        '
        Me.picRare.Image = Global.ApRadar3.My.Resources.Resources.ico_rare
        Me.picRare.Location = New System.Drawing.Point(0, 0)
        Me.picRare.Name = "picRare"
        Me.picRare.Size = New System.Drawing.Size(20, 20)
        Me.picRare.TabIndex = 11
        Me.picRare.TabStop = False
        Me.picRare.Visible = False
        '
        'lblInfo
        '
        Me.lblInfo.AutoEllipsis = True
        Me.lblInfo.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.lblInfo, 2)
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblInfo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.Location = New System.Drawing.Point(39, 26)
        Me.lblInfo.MaximumSize = New System.Drawing.Size(254, 400)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(247, 111)
        Me.lblInfo.TabIndex = 12
        Me.lblInfo.Text = "Info"
        '
        'picIcon
        '
        Me.picIcon.Location = New System.Drawing.Point(3, 3)
        Me.picIcon.Name = "picIcon"
        Me.TableLayoutPanel2.SetRowSpan(Me.picIcon, 2)
        Me.picIcon.Size = New System.Drawing.Size(30, 32)
        Me.picIcon.TabIndex = 9
        Me.picIcon.TabStop = False
        '
        'lblItemName
        '
        Me.lblItemName.AutoSize = True
        Me.lblItemName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemName.Location = New System.Drawing.Point(39, 0)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Size = New System.Drawing.Size(45, 16)
        Me.lblItemName.TabIndex = 8
        Me.lblItemName.Text = "Name"
        '
        'lblWeakness
        '
        Me.lblWeakness.AutoSize = True
        Me.lblWeakness.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeakness.ForeColor = System.Drawing.Color.Yellow
        Me.lblWeakness.Location = New System.Drawing.Point(283, 44)
        Me.lblWeakness.Name = "lblWeakness"
        Me.lblWeakness.Size = New System.Drawing.Size(58, 14)
        Me.lblWeakness.TabIndex = 2
        Me.lblWeakness.Tag = "exclude"
        Me.lblWeakness.Text = "weakness"
        '
        'lblDetection
        '
        Me.lblDetection.AutoSize = True
        Me.lblDetection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetection.ForeColor = System.Drawing.Color.Yellow
        Me.lblDetection.Location = New System.Drawing.Point(134, 44)
        Me.lblDetection.Name = "lblDetection"
        Me.lblDetection.Size = New System.Drawing.Size(51, 14)
        Me.lblDetection.TabIndex = 2
        Me.lblDetection.Tag = "exclude"
        Me.lblDetection.Text = "detection"
        '
        'lblBehavior
        '
        Me.lblBehavior.AutoSize = True
        Me.lblBehavior.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBehavior.ForeColor = System.Drawing.Color.Yellow
        Me.lblBehavior.Location = New System.Drawing.Point(283, 30)
        Me.lblBehavior.Name = "lblBehavior"
        Me.lblBehavior.Size = New System.Drawing.Size(49, 14)
        Me.lblBehavior.TabIndex = 2
        Me.lblBehavior.Tag = "exclude"
        Me.lblBehavior.Text = "behavior"
        '
        'lblLevel
        '
        Me.lblLevel.AutoSize = True
        Me.lblLevel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel.ForeColor = System.Drawing.Color.Yellow
        Me.lblLevel.Location = New System.Drawing.Point(134, 30)
        Me.lblLevel.Name = "lblLevel"
        Me.lblLevel.Size = New System.Drawing.Size(29, 14)
        Me.lblLevel.TabIndex = 2
        Me.lblLevel.Tag = "exclude"
        Me.lblLevel.Text = "level"
        '
        'lblJob
        '
        Me.lblJob.AutoSize = True
        Me.lblJob.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJob.ForeColor = System.Drawing.Color.Yellow
        Me.lblJob.Location = New System.Drawing.Point(283, 16)
        Me.lblJob.Name = "lblJob"
        Me.lblJob.Size = New System.Drawing.Size(21, 14)
        Me.lblJob.TabIndex = 2
        Me.lblJob.Tag = "exclude"
        Me.lblJob.Text = "job"
        '
        'lblFamily
        '
        Me.lblFamily.AutoSize = True
        Me.lblFamily.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFamily.ForeColor = System.Drawing.Color.Yellow
        Me.lblFamily.Location = New System.Drawing.Point(134, 16)
        Me.lblFamily.Name = "lblFamily"
        Me.lblFamily.Size = New System.Drawing.Size(35, 14)
        Me.lblFamily.TabIndex = 2
        Me.lblFamily.Tag = "exclude"
        Me.lblFamily.Text = "family"
        '
        'lblResistances
        '
        Me.lblResistances.AutoSize = True
        Me.lblResistances.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResistances.ForeColor = System.Drawing.Color.Yellow
        Me.lblResistances.Location = New System.Drawing.Point(134, 58)
        Me.lblResistances.Name = "lblResistances"
        Me.lblResistances.Size = New System.Drawing.Size(64, 14)
        Me.lblResistances.TabIndex = 2
        Me.lblResistances.Tag = "exclude"
        Me.lblResistances.Text = "resistances"
        '
        'lblImmune
        '
        Me.lblImmune.AutoSize = True
        Me.lblImmune.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImmune.ForeColor = System.Drawing.Color.Yellow
        Me.lblImmune.Location = New System.Drawing.Point(283, 58)
        Me.lblImmune.Name = "lblImmune"
        Me.lblImmune.Size = New System.Drawing.Size(56, 14)
        Me.lblImmune.TabIndex = 2
        Me.lblImmune.Tag = "exclude"
        Me.lblImmune.Text = "immunities"
        '
        'lblStolen
        '
        Me.lblStolen.AutoSize = True
        Me.lblStolen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStolen.ForeColor = System.Drawing.Color.Yellow
        Me.lblStolen.Location = New System.Drawing.Point(134, 72)
        Me.lblStolen.Name = "lblStolen"
        Me.lblStolen.Size = New System.Drawing.Size(36, 14)
        Me.lblStolen.TabIndex = 2
        Me.lblStolen.Tag = "exclude"
        Me.lblStolen.Text = "stolen"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 279)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(261, 29)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "S = Sight; T(S) = True Sight; H = Sound; T(H) = True Hearing; M = Magic; " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "HP = L" & _
    "ow HP; Sc = Tracks Scent"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.lblMobName, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblFamily, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblLevel, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDetection, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lstItems, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.lblResistances, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lblStolen, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label9, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lblImmune, 3, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lblJob, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblBehavior, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblWeakness, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label12, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.lblComments, 1, 6)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(13, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 9
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(426, 257)
        Me.TableLayoutPanel1.TabIndex = 14
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel2, 3)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.picIcon, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblItemName, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblInfo, 1, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(134, 117)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(289, 137)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.picRare)
        Me.Panel1.Controls.Add(Me.picEx)
        Me.Panel1.Location = New System.Drawing.Point(246, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(40, 20)
        Me.Panel1.TabIndex = 15
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 100)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(39, 14)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Drops:"
        '
        'lblComments
        '
        Me.lblComments.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblComments, 3)
        Me.lblComments.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComments.ForeColor = System.Drawing.Color.Yellow
        Me.lblComments.Location = New System.Drawing.Point(134, 86)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(56, 14)
        Me.lblComments.TabIndex = 2
        Me.lblComments.Tag = "exclude"
        Me.lblComments.Text = "comments"
        '
        'cmdSpawnAlert
        '
        Me.cmdSpawnAlert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.cmdSpawnAlert.Location = New System.Drawing.Point(338, 283)
        Me.cmdSpawnAlert.Name = "cmdSpawnAlert"
        Me.cmdSpawnAlert.Size = New System.Drawing.Size(101, 23)
        Me.cmdSpawnAlert.TabIndex = 15
        Me.cmdSpawnAlert.Text = "Add Spawn Alert"
        Me.cmdSpawnAlert.UseVisualStyleBackColor = True
        '
        'MobDataForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(453, 317)
        Me.Controls.Add(Me.cmdSpawnAlert)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmdEdit)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(320, 36)
        Me.Name = "MobDataForm"
        Me.Opacity = 0.8R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MobDataForm"
        Me.TopMost = True
        CType(Me.picEx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents lblMobName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lstItems As System.Windows.Forms.ListBox
    Friend WithEvents picEx As System.Windows.Forms.PictureBox
    Friend WithEvents picRare As System.Windows.Forms.PictureBox
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents picIcon As System.Windows.Forms.PictureBox
    Friend WithEvents lblItemName As System.Windows.Forms.Label
    Friend WithEvents lblWeakness As System.Windows.Forms.Label
    Friend WithEvents lblDetection As System.Windows.Forms.Label
    Friend WithEvents lblBehavior As System.Windows.Forms.Label
    Friend WithEvents lblLevel As System.Windows.Forms.Label
    Friend WithEvents lblJob As System.Windows.Forms.Label
    Friend WithEvents lblFamily As System.Windows.Forms.Label
    Friend WithEvents lblResistances As System.Windows.Forms.Label
    Friend WithEvents lblImmune As System.Windows.Forms.Label
    Friend WithEvents lblStolen As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblComments As System.Windows.Forms.Label
    Friend WithEvents cmdSpawnAlert As System.Windows.Forms.Button
End Class
