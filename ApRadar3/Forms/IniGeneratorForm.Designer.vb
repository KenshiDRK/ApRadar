<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IniGeneratorForm
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
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        Me.blnClose = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cboZone = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboFloor = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtX = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtY = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtZ = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtMapX = New System.Windows.Forms.TextBox()
        Me.txtMapY = New System.Windows.Forms.TextBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtXMin = New System.Windows.Forms.TextBox()
        Me.txtXMax = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtYMin = New System.Windows.Forms.TextBox()
        Me.txtYMax = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtZMin = New System.Windows.Forms.TextBox()
        Me.txtZMax = New System.Windows.Forms.TextBox()
        Me.txtResult = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.HeaderPanel.Size = New System.Drawing.Size(856, 33)
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
        Me.lblHeder.Size = New System.Drawing.Size(195, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "Map.ini Line Generator (BETA)"
        '
        'blnClose
        '
        Me.blnClose.BackColor = System.Drawing.Color.Transparent
        Me.blnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.blnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.blnClose.Location = New System.Drawing.Point(834, 0)
        Me.blnClose.Name = "blnClose"
        Me.blnClose.Size = New System.Drawing.Size(16, 33)
        Me.blnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.blnClose.TabIndex = 0
        Me.blnClose.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Cross
        Me.PictureBox1.Location = New System.Drawing.Point(331, 43)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(512, 512)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'cboZone
        '
        Me.cboZone.FormattingEnabled = True
        Me.cboZone.Location = New System.Drawing.Point(50, 43)
        Me.cboZone.Name = "cboZone"
        Me.cboZone.Size = New System.Drawing.Size(275, 21)
        Me.cboZone.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Zone"
        '
        'cboFloor
        '
        Me.cboFloor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFloor.FormattingEnabled = True
        Me.cboFloor.Location = New System.Drawing.Point(50, 70)
        Me.cboFloor.Name = "cboFloor"
        Me.cboFloor.Size = New System.Drawing.Size(111, 21)
        Me.cboFloor.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Floor"
        '
        'txtX
        '
        Me.txtX.Location = New System.Drawing.Point(50, 98)
        Me.txtX.Name = "txtX"
        Me.txtX.ReadOnly = True
        Me.txtX.Size = New System.Drawing.Size(73, 20)
        Me.txtX.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "X"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(129, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Y"
        '
        'txtY
        '
        Me.txtY.Location = New System.Drawing.Point(151, 98)
        Me.txtY.Name = "txtY"
        Me.txtY.ReadOnly = True
        Me.txtY.Size = New System.Drawing.Size(73, 20)
        Me.txtY.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(230, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Z"
        '
        'txtZ
        '
        Me.txtZ.Location = New System.Drawing.Point(252, 98)
        Me.txtZ.Name = "txtZ"
        Me.txtZ.ReadOnly = True
        Me.txtZ.Size = New System.Drawing.Size(73, 20)
        Me.txtZ.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Map Coordinates"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(14, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "X"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(129, 146)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(14, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Y"
        '
        'txtMapX
        '
        Me.txtMapX.Location = New System.Drawing.Point(50, 143)
        Me.txtMapX.Name = "txtMapX"
        Me.txtMapX.ReadOnly = True
        Me.txtMapX.Size = New System.Drawing.Size(73, 20)
        Me.txtMapX.TabIndex = 6
        '
        'txtMapY
        '
        Me.txtMapY.Location = New System.Drawing.Point(151, 143)
        Me.txtMapY.Name = "txtMapY"
        Me.txtMapY.ReadOnly = True
        Me.txtMapY.Size = New System.Drawing.Size(73, 20)
        Me.txtMapY.TabIndex = 7
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.FormattingEnabled = True
        Me.cboType.Items.AddRange(New Object() {"Field", "Dungeon", "Town"})
        Me.cboType.Location = New System.Drawing.Point(214, 70)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(111, 21)
        Me.cboType.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(176, 73)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Type"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 170)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Box Boundries"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 186)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(188, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "X Range (East -10000 to West 10000)"
        '
        'txtXMin
        '
        Me.txtXMin.Location = New System.Drawing.Point(50, 205)
        Me.txtXMin.Name = "txtXMin"
        Me.txtXMin.Size = New System.Drawing.Size(73, 20)
        Me.txtXMin.TabIndex = 8
        Me.txtXMin.Text = "-10000"
        '
        'txtXMax
        '
        Me.txtXMax.Location = New System.Drawing.Point(151, 205)
        Me.txtXMax.Name = "txtXMax"
        Me.txtXMax.Size = New System.Drawing.Size(73, 20)
        Me.txtXMax.TabIndex = 9
        Me.txtXMax.Text = "10000"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 228)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(196, 13)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Y Range (North -10000 to South 10000)"
        '
        'txtYMin
        '
        Me.txtYMin.Location = New System.Drawing.Point(50, 247)
        Me.txtYMin.Name = "txtYMin"
        Me.txtYMin.Size = New System.Drawing.Size(73, 20)
        Me.txtYMin.TabIndex = 10
        Me.txtYMin.Text = "-10000"
        '
        'txtYMax
        '
        Me.txtYMax.Location = New System.Drawing.Point(151, 247)
        Me.txtYMax.Name = "txtYMax"
        Me.txtYMax.Size = New System.Drawing.Size(73, 20)
        Me.txtYMax.TabIndex = 11
        Me.txtYMax.Text = "10000"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(12, 270)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(136, 13)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "Z Range (-10000 to 10000)"
        '
        'txtZMin
        '
        Me.txtZMin.Location = New System.Drawing.Point(50, 289)
        Me.txtZMin.Name = "txtZMin"
        Me.txtZMin.Size = New System.Drawing.Size(73, 20)
        Me.txtZMin.TabIndex = 12
        Me.txtZMin.Text = "-10000"
        '
        'txtZMax
        '
        Me.txtZMax.Location = New System.Drawing.Point(151, 289)
        Me.txtZMax.Name = "txtZMax"
        Me.txtZMax.Size = New System.Drawing.Size(73, 20)
        Me.txtZMax.TabIndex = 13
        Me.txtZMax.Text = "10000"
        '
        'txtResult
        '
        Me.txtResult.Location = New System.Drawing.Point(15, 328)
        Me.txtResult.Multiline = True
        Me.txtResult.Name = "txtResult"
        Me.txtResult.Size = New System.Drawing.Size(310, 198)
        Me.txtResult.TabIndex = 14
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(12, 312)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(37, 13)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "Result"
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(15, 532)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(310, 23)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Save Entry"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'IniGeneratorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(856, 569)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.txtZ)
        Me.Controls.Add(Me.txtZMax)
        Me.Controls.Add(Me.txtYMax)
        Me.Controls.Add(Me.txtXMax)
        Me.Controls.Add(Me.txtMapY)
        Me.Controls.Add(Me.txtY)
        Me.Controls.Add(Me.txtZMin)
        Me.Controls.Add(Me.txtYMin)
        Me.Controls.Add(Me.txtXMin)
        Me.Controls.Add(Me.txtMapX)
        Me.Controls.Add(Me.txtX)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboFloor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboZone)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.HeaderPanel)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "IniGeneratorForm"
        Me.Text = "IniGeneratorForm"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents blnClose As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cboZone As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboFloor As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtX As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtY As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtZ As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMapX As System.Windows.Forms.TextBox
    Friend WithEvents txtMapY As System.Windows.Forms.TextBox
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtXMin As System.Windows.Forms.TextBox
    Friend WithEvents txtXMax As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtYMin As System.Windows.Forms.TextBox
    Friend WithEvents txtYMax As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtZMin As System.Windows.Forms.TextBox
    Friend WithEvents txtZMax As System.Windows.Forms.TextBox
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
