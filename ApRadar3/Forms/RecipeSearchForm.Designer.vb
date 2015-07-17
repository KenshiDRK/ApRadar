<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RecipeSearchForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RecipeSearchForm))
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.PictureBox()
        Me.cmdPrevious = New System.Windows.Forms.Button()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.lblRecipeCount = New System.Windows.Forms.Label()
        Me.lstItems = New System.Windows.Forms.ListBox()
        Me.lblItemName = New System.Windows.Forms.Label()
        Me.picRare = New System.Windows.Forms.PictureBox()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.picIcon = New System.Windows.Forms.PictureBox()
        Me.lbCrystal = New System.Windows.Forms.Label()
        Me.lstResults = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.lblAlchemy = New System.Windows.Forms.Label()
        Me.lblGold = New System.Windows.Forms.Label()
        Me.lblWood = New System.Windows.Forms.Label()
        Me.lblBone = New System.Windows.Forms.Label()
        Me.lblCloth = New System.Windows.Forms.Label()
        Me.lblLeather = New System.Windows.Forms.Label()
        Me.lblSmithing = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.pnlSkills = New System.Windows.Forms.Panel()
        Me.lblCooking = New System.Windows.Forms.Label()
        Me.rbRecipes = New System.Windows.Forms.RadioButton()
        Me.rbUsedIn = New System.Windows.Forms.RadioButton()
        Me.rbDesynth = New System.Windows.Forms.RadioButton()
        Me.picEx = New System.Windows.Forms.PictureBox()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSkills.SuspendLayout()
        CType(Me.picEx, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.HeaderPanel.Size = New System.Drawing.Size(524, 33)
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
        Me.lblHeder.Size = New System.Drawing.Size(94, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "Recipe Search"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.btnClose.Location = New System.Drawing.Point(502, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(16, 33)
        Me.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnClose.TabIndex = 0
        Me.btnClose.TabStop = False
        '
        'cmdPrevious
        '
        Me.cmdPrevious.Enabled = False
        Me.cmdPrevious.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.cmdPrevious.Location = New System.Drawing.Point(13, 41)
        Me.cmdPrevious.Name = "cmdPrevious"
        Me.cmdPrevious.Size = New System.Drawing.Size(23, 23)
        Me.cmdPrevious.TabIndex = 5
        Me.cmdPrevious.Text = "<"
        Me.cmdPrevious.UseVisualStyleBackColor = True
        '
        'cmdNext
        '
        Me.cmdNext.Enabled = False
        Me.cmdNext.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.cmdNext.Location = New System.Drawing.Point(133, 41)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(23, 23)
        Me.cmdNext.TabIndex = 6
        Me.cmdNext.Text = ">"
        Me.cmdNext.UseVisualStyleBackColor = True
        '
        'lblRecipeCount
        '
        Me.lblRecipeCount.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecipeCount.ForeColor = System.Drawing.Color.White
        Me.lblRecipeCount.Location = New System.Drawing.Point(42, 41)
        Me.lblRecipeCount.Name = "lblRecipeCount"
        Me.lblRecipeCount.Size = New System.Drawing.Size(85, 23)
        Me.lblRecipeCount.TabIndex = 7
        Me.lblRecipeCount.Text = "0 of 0"
        Me.lblRecipeCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lstItems
        '
        Me.lstItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstItems.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstItems.ForeColor = System.Drawing.Color.LimeGreen
        Me.lstItems.FormattingEnabled = True
        Me.lstItems.ItemHeight = 14
        Me.lstItems.Location = New System.Drawing.Point(12, 97)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(144, 112)
        Me.lstItems.TabIndex = 8
        Me.lstItems.Tag = "exclude"
        '
        'lblItemName
        '
        Me.lblItemName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemName.Location = New System.Drawing.Point(205, 189)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Size = New System.Drawing.Size(217, 16)
        Me.lblItemName.TabIndex = 9
        '
        'picRare
        '
        Me.picRare.Image = Global.ApRadar3.My.Resources.Resources.ico_rare
        Me.picRare.Location = New System.Drawing.Point(474, 189)
        Me.picRare.Name = "picRare"
        Me.picRare.Size = New System.Drawing.Size(20, 20)
        Me.picRare.TabIndex = 11
        Me.picRare.TabStop = False
        Me.picRare.Visible = False
        '
        'lblInfo
        '
        Me.lblInfo.AutoEllipsis = True
        Me.lblInfo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.Location = New System.Drawing.Point(205, 206)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(307, 122)
        Me.lblInfo.TabIndex = 13
        '
        'picIcon
        '
        Me.picIcon.Location = New System.Drawing.Point(167, 189)
        Me.picIcon.Name = "picIcon"
        Me.picIcon.Size = New System.Drawing.Size(32, 32)
        Me.picIcon.TabIndex = 10
        Me.picIcon.TabStop = False
        '
        'lbCrystal
        '
        Me.lbCrystal.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCrystal.Location = New System.Drawing.Point(10, 70)
        Me.lbCrystal.Name = "lbCrystal"
        Me.lbCrystal.Size = New System.Drawing.Size(150, 17)
        Me.lbCrystal.TabIndex = 14
        Me.lbCrystal.Text = "No recipes found..."
        '
        'lstResults
        '
        Me.lstResults.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.lstResults.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstResults.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstResults.ForeColor = System.Drawing.Color.LimeGreen
        Me.lstResults.FormattingEnabled = True
        Me.lstResults.ItemHeight = 14
        Me.lstResults.Location = New System.Drawing.Point(12, 231)
        Me.lstResults.Name = "lstResults"
        Me.lstResults.Size = New System.Drawing.Size(144, 56)
        Me.lstResults.TabIndex = 15
        Me.lstResults.Tag = "exclude"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 212)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 14)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Results"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 17
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Alchemy")
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(125, 32)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 17
        Me.PictureBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox2, "Goldsmithing")
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(125, 2)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 17
        Me.PictureBox3.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox3, "Bonecraft")
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(245, 2)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox4.TabIndex = 17
        Me.PictureBox4.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox4, "Clothcraft")
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(245, 32)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox5.TabIndex = 17
        Me.PictureBox5.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox5, "Leathercraft")
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(5, 62)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox6.TabIndex = 17
        Me.PictureBox6.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox6, "Smithing")
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(125, 62)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox7.TabIndex = 17
        Me.PictureBox7.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox7, "Woodworking")
        '
        'lblAlchemy
        '
        Me.lblAlchemy.AutoSize = True
        Me.lblAlchemy.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlchemy.Location = New System.Drawing.Point(36, 7)
        Me.lblAlchemy.Name = "lblAlchemy"
        Me.lblAlchemy.Size = New System.Drawing.Size(14, 15)
        Me.lblAlchemy.TabIndex = 18
        Me.lblAlchemy.Text = "0"
        '
        'lblGold
        '
        Me.lblGold.AutoSize = True
        Me.lblGold.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGold.Location = New System.Drawing.Point(155, 36)
        Me.lblGold.Name = "lblGold"
        Me.lblGold.Size = New System.Drawing.Size(14, 15)
        Me.lblGold.TabIndex = 18
        Me.lblGold.Text = "0"
        '
        'lblWood
        '
        Me.lblWood.AutoSize = True
        Me.lblWood.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWood.Location = New System.Drawing.Point(155, 66)
        Me.lblWood.Name = "lblWood"
        Me.lblWood.Size = New System.Drawing.Size(14, 15)
        Me.lblWood.TabIndex = 18
        Me.lblWood.Text = "0"
        '
        'lblBone
        '
        Me.lblBone.AutoSize = True
        Me.lblBone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBone.Location = New System.Drawing.Point(155, 7)
        Me.lblBone.Name = "lblBone"
        Me.lblBone.Size = New System.Drawing.Size(14, 15)
        Me.lblBone.TabIndex = 18
        Me.lblBone.Text = "0"
        '
        'lblCloth
        '
        Me.lblCloth.AutoSize = True
        Me.lblCloth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCloth.Location = New System.Drawing.Point(275, 7)
        Me.lblCloth.Name = "lblCloth"
        Me.lblCloth.Size = New System.Drawing.Size(14, 15)
        Me.lblCloth.TabIndex = 18
        Me.lblCloth.Text = "0"
        '
        'lblLeather
        '
        Me.lblLeather.AutoSize = True
        Me.lblLeather.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeather.Location = New System.Drawing.Point(275, 36)
        Me.lblLeather.Name = "lblLeather"
        Me.lblLeather.Size = New System.Drawing.Size(14, 15)
        Me.lblLeather.TabIndex = 18
        Me.lblLeather.Text = "0"
        '
        'lblSmithing
        '
        Me.lblSmithing.AutoSize = True
        Me.lblSmithing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmithing.Location = New System.Drawing.Point(35, 66)
        Me.lblSmithing.Name = "lblSmithing"
        Me.lblSmithing.Size = New System.Drawing.Size(14, 15)
        Me.lblSmithing.TabIndex = 18
        Me.lblSmithing.Text = "0"
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ToolTipTitle = "Synthesis"
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(5, 32)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox8.TabIndex = 17
        Me.PictureBox8.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox8, "Cooking")
        '
        'txtItem
        '
        Me.txtItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.txtItem.Location = New System.Drawing.Point(162, 42)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(294, 20)
        Me.txtItem.TabIndex = 19
        '
        'cmdSearch
        '
        Me.cmdSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.cmdSearch.Location = New System.Drawing.Point(462, 40)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(50, 23)
        Me.cmdSearch.TabIndex = 20
        Me.cmdSearch.Text = "Search"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'pnlSkills
        '
        Me.pnlSkills.Controls.Add(Me.lblWood)
        Me.pnlSkills.Controls.Add(Me.lblSmithing)
        Me.pnlSkills.Controls.Add(Me.lblLeather)
        Me.pnlSkills.Controls.Add(Me.lblCooking)
        Me.pnlSkills.Controls.Add(Me.lblGold)
        Me.pnlSkills.Controls.Add(Me.lblCloth)
        Me.pnlSkills.Controls.Add(Me.lblBone)
        Me.pnlSkills.Controls.Add(Me.lblAlchemy)
        Me.pnlSkills.Controls.Add(Me.PictureBox4)
        Me.pnlSkills.Controls.Add(Me.PictureBox3)
        Me.pnlSkills.Controls.Add(Me.PictureBox7)
        Me.pnlSkills.Controls.Add(Me.PictureBox6)
        Me.pnlSkills.Controls.Add(Me.PictureBox5)
        Me.pnlSkills.Controls.Add(Me.PictureBox8)
        Me.pnlSkills.Controls.Add(Me.PictureBox2)
        Me.pnlSkills.Controls.Add(Me.PictureBox1)
        Me.pnlSkills.Location = New System.Drawing.Point(162, 90)
        Me.pnlSkills.Name = "pnlSkills"
        Me.pnlSkills.Size = New System.Drawing.Size(350, 93)
        Me.pnlSkills.TabIndex = 21
        '
        'lblCooking
        '
        Me.lblCooking.AutoSize = True
        Me.lblCooking.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCooking.Location = New System.Drawing.Point(35, 36)
        Me.lblCooking.Name = "lblCooking"
        Me.lblCooking.Size = New System.Drawing.Size(14, 15)
        Me.lblCooking.TabIndex = 18
        Me.lblCooking.Text = "0"
        '
        'rbRecipes
        '
        Me.rbRecipes.AutoSize = True
        Me.rbRecipes.Checked = True
        Me.rbRecipes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbRecipes.Location = New System.Drawing.Point(162, 69)
        Me.rbRecipes.Name = "rbRecipes"
        Me.rbRecipes.Size = New System.Drawing.Size(64, 18)
        Me.rbRecipes.TabIndex = 23
        Me.rbRecipes.TabStop = True
        Me.rbRecipes.Text = "Recipes"
        Me.rbRecipes.UseVisualStyleBackColor = True
        '
        'rbUsedIn
        '
        Me.rbUsedIn.AutoSize = True
        Me.rbUsedIn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbUsedIn.Location = New System.Drawing.Point(264, 69)
        Me.rbUsedIn.Name = "rbUsedIn"
        Me.rbUsedIn.Size = New System.Drawing.Size(100, 18)
        Me.rbUsedIn.TabIndex = 23
        Me.rbUsedIn.Text = "Used in recipes"
        Me.rbUsedIn.UseVisualStyleBackColor = True
        '
        'rbDesynth
        '
        Me.rbDesynth.AutoSize = True
        Me.rbDesynth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbDesynth.Location = New System.Drawing.Point(407, 69)
        Me.rbDesynth.Name = "rbDesynth"
        Me.rbDesynth.Size = New System.Drawing.Size(85, 18)
        Me.rbDesynth.TabIndex = 23
        Me.rbDesynth.Text = "Desynthesis"
        Me.rbDesynth.UseVisualStyleBackColor = True
        '
        'picEx
        '
        Me.picEx.Image = Global.ApRadar3.My.Resources.Resources.ico_ex
        Me.picEx.Location = New System.Drawing.Point(492, 189)
        Me.picEx.Name = "picEx"
        Me.picEx.Size = New System.Drawing.Size(20, 20)
        Me.picEx.TabIndex = 24
        Me.picEx.TabStop = False
        Me.picEx.Visible = False
        '
        'RecipeSearchForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(524, 342)
        Me.Controls.Add(Me.picRare)
        Me.Controls.Add(Me.picEx)
        Me.Controls.Add(Me.rbDesynth)
        Me.Controls.Add(Me.rbUsedIn)
        Me.Controls.Add(Me.rbRecipes)
        Me.Controls.Add(Me.pnlSkills)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.txtItem)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstResults)
        Me.Controls.Add(Me.lbCrystal)
        Me.Controls.Add(Me.lblItemName)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.picIcon)
        Me.Controls.Add(Me.lstItems)
        Me.Controls.Add(Me.lblRecipeCount)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdPrevious)
        Me.Controls.Add(Me.HeaderPanel)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(110, 36)
        Me.Name = "RecipeSearchForm"
        Me.Opacity = 0.9R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Recipe Search"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSkills.ResumeLayout(False)
        Me.pnlSkills.PerformLayout()
        CType(Me.picEx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.PictureBox
    Friend WithEvents cmdPrevious As System.Windows.Forms.Button
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    Friend WithEvents lblRecipeCount As System.Windows.Forms.Label
    Friend WithEvents lstItems As System.Windows.Forms.ListBox
    Friend WithEvents lblItemName As System.Windows.Forms.Label
    Friend WithEvents picRare As System.Windows.Forms.PictureBox
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents picIcon As System.Windows.Forms.PictureBox
    Friend WithEvents lbCrystal As System.Windows.Forms.Label
    Friend WithEvents lstResults As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents lblAlchemy As System.Windows.Forms.Label
    Friend WithEvents lblGold As System.Windows.Forms.Label
    Friend WithEvents lblWood As System.Windows.Forms.Label
    Friend WithEvents lblBone As System.Windows.Forms.Label
    Friend WithEvents lblCloth As System.Windows.Forms.Label
    Friend WithEvents lblLeather As System.Windows.Forms.Label
    Friend WithEvents lblSmithing As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents pnlSkills As System.Windows.Forms.Panel
    Friend WithEvents lblCooking As System.Windows.Forms.Label
    Friend WithEvents PictureBox8 As System.Windows.Forms.PictureBox
    Friend WithEvents rbRecipes As System.Windows.Forms.RadioButton
    Friend WithEvents rbUsedIn As System.Windows.Forms.RadioButton
    Friend WithEvents rbDesynth As System.Windows.Forms.RadioButton
    Friend WithEvents picEx As System.Windows.Forms.PictureBox
End Class
