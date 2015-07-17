<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OverlayFilterForm
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
        Me.txtNpcFilter = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbNPCRegEx = New System.Windows.Forms.RadioButton()
        Me.rbNPCReverse = New System.Windows.Forms.RadioButton()
        Me.rbNPCNone = New System.Windows.Forms.RadioButton()
        Me.rbNPCStandard = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPCFilter = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbPCRegEx = New System.Windows.Forms.RadioButton()
        Me.rbPCReverse = New System.Windows.Forms.RadioButton()
        Me.rbPCNone = New System.Windows.Forms.RadioButton()
        Me.rbPCStandard = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.HeaderPanel.BackgroundImage = Global.ApRadar3.My.Resources.Resources.tbg
        Me.HeaderPanel.Controls.Add(Me.lblHeder)
        Me.HeaderPanel.Controls.Add(Me.blnClose)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Padding = New System.Windows.Forms.Padding(5, 0, 6, 0)
        Me.HeaderPanel.Size = New System.Drawing.Size(457, 33)
        Me.HeaderPanel.TabIndex = 7
        '
        'lblHeder
        '
        Me.lblHeder.AutoSize = True
        Me.lblHeder.BackColor = System.Drawing.Color.Transparent
        Me.lblHeder.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeder.ForeColor = System.Drawing.Color.White
        Me.lblHeder.Location = New System.Drawing.Point(9, 6)
        Me.lblHeder.Name = "lblHeder"
        Me.lblHeder.Size = New System.Drawing.Size(136, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "Overlay Radar Filters"
        '
        'blnClose
        '
        Me.blnClose.BackColor = System.Drawing.Color.Transparent
        Me.blnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.blnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.blnClose.Location = New System.Drawing.Point(435, 0)
        Me.blnClose.Name = "blnClose"
        Me.blnClose.Size = New System.Drawing.Size(16, 33)
        Me.blnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.blnClose.TabIndex = 0
        Me.blnClose.TabStop = False
        '
        'txtNpcFilter
        '
        Me.txtNpcFilter.Location = New System.Drawing.Point(12, 125)
        Me.txtNpcFilter.Name = "txtNpcFilter"
        Me.txtNpcFilter.Size = New System.Drawing.Size(433, 20)
        Me.txtNpcFilter.TabIndex = 10
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.rbNPCRegEx)
        Me.Panel2.Controls.Add(Me.rbNPCReverse)
        Me.Panel2.Controls.Add(Me.rbNPCNone)
        Me.Panel2.Controls.Add(Me.rbNPCStandard)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.ForeColor = System.Drawing.Color.White
        Me.Panel2.Location = New System.Drawing.Point(12, 97)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(433, 22)
        Me.Panel2.TabIndex = 9
        '
        'rbNPCRegEx
        '
        Me.rbNPCRegEx.AutoSize = True
        Me.rbNPCRegEx.Location = New System.Drawing.Point(259, 2)
        Me.rbNPCRegEx.Name = "rbNPCRegEx"
        Me.rbNPCRegEx.Size = New System.Drawing.Size(57, 17)
        Me.rbNPCRegEx.TabIndex = 4
        Me.rbNPCRegEx.TabStop = True
        Me.rbNPCRegEx.Tag = "3"
        Me.rbNPCRegEx.Text = "RegEx"
        Me.rbNPCRegEx.UseVisualStyleBackColor = True
        '
        'rbNPCReverse
        '
        Me.rbNPCReverse.AutoSize = True
        Me.rbNPCReverse.Location = New System.Drawing.Point(188, 2)
        Me.rbNPCReverse.Name = "rbNPCReverse"
        Me.rbNPCReverse.Size = New System.Drawing.Size(65, 17)
        Me.rbNPCReverse.TabIndex = 5
        Me.rbNPCReverse.TabStop = True
        Me.rbNPCReverse.Tag = "2"
        Me.rbNPCReverse.Text = "Reverse"
        Me.rbNPCReverse.UseVisualStyleBackColor = True
        '
        'rbNPCNone
        '
        Me.rbNPCNone.AutoSize = True
        Me.rbNPCNone.Location = New System.Drawing.Point(57, 2)
        Me.rbNPCNone.Name = "rbNPCNone"
        Me.rbNPCNone.Size = New System.Drawing.Size(51, 17)
        Me.rbNPCNone.TabIndex = 2
        Me.rbNPCNone.TabStop = True
        Me.rbNPCNone.Tag = "0"
        Me.rbNPCNone.Text = "None"
        Me.rbNPCNone.UseVisualStyleBackColor = True
        '
        'rbNPCStandard
        '
        Me.rbNPCStandard.AutoSize = True
        Me.rbNPCStandard.Location = New System.Drawing.Point(114, 2)
        Me.rbNPCStandard.Name = "rbNPCStandard"
        Me.rbNPCStandard.Size = New System.Drawing.Size(68, 17)
        Me.rbNPCStandard.TabIndex = 3
        Me.rbNPCStandard.TabStop = True
        Me.rbNPCStandard.Tag = "1"
        Me.rbNPCStandard.Text = "Standard"
        Me.rbNPCStandard.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(-3, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "NPC Filter"
        '
        'txtPCFilter
        '
        Me.txtPCFilter.Location = New System.Drawing.Point(12, 71)
        Me.txtPCFilter.Margin = New System.Windows.Forms.Padding(10)
        Me.txtPCFilter.Name = "txtPCFilter"
        Me.txtPCFilter.Size = New System.Drawing.Size(433, 20)
        Me.txtPCFilter.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.rbPCRegEx)
        Me.Panel1.Controls.Add(Me.rbPCReverse)
        Me.Panel1.Controls.Add(Me.rbPCNone)
        Me.Panel1.Controls.Add(Me.rbPCStandard)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(12, 42)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(10)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(433, 22)
        Me.Panel1.TabIndex = 8
        '
        'rbPCRegEx
        '
        Me.rbPCRegEx.AutoSize = True
        Me.rbPCRegEx.Location = New System.Drawing.Point(259, 2)
        Me.rbPCRegEx.Name = "rbPCRegEx"
        Me.rbPCRegEx.Size = New System.Drawing.Size(57, 17)
        Me.rbPCRegEx.TabIndex = 1
        Me.rbPCRegEx.TabStop = True
        Me.rbPCRegEx.Tag = "3"
        Me.rbPCRegEx.Text = "RegEx"
        Me.rbPCRegEx.UseVisualStyleBackColor = True
        '
        'rbPCReverse
        '
        Me.rbPCReverse.AutoSize = True
        Me.rbPCReverse.Location = New System.Drawing.Point(188, 2)
        Me.rbPCReverse.Name = "rbPCReverse"
        Me.rbPCReverse.Size = New System.Drawing.Size(65, 17)
        Me.rbPCReverse.TabIndex = 1
        Me.rbPCReverse.TabStop = True
        Me.rbPCReverse.Tag = "2"
        Me.rbPCReverse.Text = "Reverse"
        Me.rbPCReverse.UseVisualStyleBackColor = True
        '
        'rbPCNone
        '
        Me.rbPCNone.AutoSize = True
        Me.rbPCNone.Location = New System.Drawing.Point(57, 2)
        Me.rbPCNone.Name = "rbPCNone"
        Me.rbPCNone.Size = New System.Drawing.Size(51, 17)
        Me.rbPCNone.TabIndex = 1
        Me.rbPCNone.TabStop = True
        Me.rbPCNone.Tag = "0"
        Me.rbPCNone.Text = "None"
        Me.rbPCNone.UseVisualStyleBackColor = True
        '
        'rbPCStandard
        '
        Me.rbPCStandard.AutoSize = True
        Me.rbPCStandard.Location = New System.Drawing.Point(114, 2)
        Me.rbPCStandard.Name = "rbPCStandard"
        Me.rbPCStandard.Size = New System.Drawing.Size(68, 17)
        Me.rbPCStandard.TabIndex = 1
        Me.rbPCStandard.TabStop = True
        Me.rbPCStandard.Tag = "1"
        Me.rbPCStandard.Text = "Standard"
        Me.rbPCStandard.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(-3, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "PC Filter"
        '
        'OverlayFilterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(457, 158)
        Me.Controls.Add(Me.txtNpcFilter)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.txtPCFilter)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.HeaderPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "OverlayFilterForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OverlayFilterForm"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents blnClose As System.Windows.Forms.PictureBox
    Friend WithEvents txtNpcFilter As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbNPCRegEx As System.Windows.Forms.RadioButton
    Friend WithEvents rbNPCReverse As System.Windows.Forms.RadioButton
    Friend WithEvents rbNPCNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbNPCStandard As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPCFilter As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbPCRegEx As System.Windows.Forms.RadioButton
    Friend WithEvents rbPCReverse As System.Windows.Forms.RadioButton
    Friend WithEvents rbPCNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbPCStandard As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
