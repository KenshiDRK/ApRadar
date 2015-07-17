<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FilterForm
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbPCRegEx = New System.Windows.Forms.RadioButton()
        Me.rbPCReverse = New System.Windows.Forms.RadioButton()
        Me.rbPCNone = New System.Windows.Forms.RadioButton()
        Me.rbPCStandard = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPCFilter = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbNPCRegEx = New System.Windows.Forms.RadioButton()
        Me.rbNPCReverse = New System.Windows.Forms.RadioButton()
        Me.rbNPCNone = New System.Windows.Forms.RadioButton()
        Me.rbNPCStandard = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNpcFilter = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.rbPCRegEx)
        Me.Panel1.Controls.Add(Me.rbPCReverse)
        Me.Panel1.Controls.Add(Me.rbPCNone)
        Me.Panel1.Controls.Add(Me.rbPCStandard)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(10, 10)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(10)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(433, 22)
        Me.Panel1.TabIndex = 0
        '
        'rbPCRegEx
        '
        Me.rbPCRegEx.AutoSize = True
        Me.rbPCRegEx.ForeColor = System.Drawing.Color.White
        Me.rbPCRegEx.Location = New System.Drawing.Point(259, 2)
        Me.rbPCRegEx.Name = "rbPCRegEx"
        Me.rbPCRegEx.Size = New System.Drawing.Size(57, 17)
        Me.rbPCRegEx.TabIndex = 3
        Me.rbPCRegEx.TabStop = True
        Me.rbPCRegEx.Tag = "3"
        Me.rbPCRegEx.Text = "RegEx"
        Me.rbPCRegEx.UseVisualStyleBackColor = True
        '
        'rbPCReverse
        '
        Me.rbPCReverse.AutoSize = True
        Me.rbPCReverse.ForeColor = System.Drawing.Color.White
        Me.rbPCReverse.Location = New System.Drawing.Point(188, 2)
        Me.rbPCReverse.Name = "rbPCReverse"
        Me.rbPCReverse.Size = New System.Drawing.Size(65, 17)
        Me.rbPCReverse.TabIndex = 2
        Me.rbPCReverse.TabStop = True
        Me.rbPCReverse.Tag = "2"
        Me.rbPCReverse.Text = "Reverse"
        Me.rbPCReverse.UseVisualStyleBackColor = True
        '
        'rbPCNone
        '
        Me.rbPCNone.AutoSize = True
        Me.rbPCNone.ForeColor = System.Drawing.Color.White
        Me.rbPCNone.Location = New System.Drawing.Point(57, 2)
        Me.rbPCNone.Name = "rbPCNone"
        Me.rbPCNone.Size = New System.Drawing.Size(51, 17)
        Me.rbPCNone.TabIndex = 0
        Me.rbPCNone.TabStop = True
        Me.rbPCNone.Tag = "0"
        Me.rbPCNone.Text = "None"
        Me.rbPCNone.UseVisualStyleBackColor = True
        '
        'rbPCStandard
        '
        Me.rbPCStandard.AutoSize = True
        Me.rbPCStandard.ForeColor = System.Drawing.Color.White
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
        'txtPCFilter
        '
        Me.txtPCFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtPCFilter.ForeColor = System.Drawing.Color.Black
        Me.txtPCFilter.Location = New System.Drawing.Point(10, 32)
        Me.txtPCFilter.Margin = New System.Windows.Forms.Padding(10)
        Me.txtPCFilter.Name = "txtPCFilter"
        Me.txtPCFilter.Size = New System.Drawing.Size(433, 20)
        Me.txtPCFilter.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.rbNPCRegEx)
        Me.Panel2.Controls.Add(Me.rbNPCReverse)
        Me.Panel2.Controls.Add(Me.rbNPCNone)
        Me.Panel2.Controls.Add(Me.rbNPCStandard)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.ForeColor = System.Drawing.Color.White
        Me.Panel2.Location = New System.Drawing.Point(10, 52)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(433, 22)
        Me.Panel2.TabIndex = 2
        '
        'rbNPCRegEx
        '
        Me.rbNPCRegEx.AutoSize = True
        Me.rbNPCRegEx.ForeColor = System.Drawing.Color.White
        Me.rbNPCRegEx.Location = New System.Drawing.Point(259, 2)
        Me.rbNPCRegEx.Name = "rbNPCRegEx"
        Me.rbNPCRegEx.Size = New System.Drawing.Size(57, 17)
        Me.rbNPCRegEx.TabIndex = 3
        Me.rbNPCRegEx.TabStop = True
        Me.rbNPCRegEx.Tag = "3"
        Me.rbNPCRegEx.Text = "RegEx"
        Me.rbNPCRegEx.UseVisualStyleBackColor = True
        '
        'rbNPCReverse
        '
        Me.rbNPCReverse.AutoSize = True
        Me.rbNPCReverse.ForeColor = System.Drawing.Color.White
        Me.rbNPCReverse.Location = New System.Drawing.Point(188, 2)
        Me.rbNPCReverse.Name = "rbNPCReverse"
        Me.rbNPCReverse.Size = New System.Drawing.Size(65, 17)
        Me.rbNPCReverse.TabIndex = 2
        Me.rbNPCReverse.TabStop = True
        Me.rbNPCReverse.Tag = "2"
        Me.rbNPCReverse.Text = "Reverse"
        Me.rbNPCReverse.UseVisualStyleBackColor = True
        '
        'rbNPCNone
        '
        Me.rbNPCNone.AutoSize = True
        Me.rbNPCNone.ForeColor = System.Drawing.Color.White
        Me.rbNPCNone.Location = New System.Drawing.Point(57, 2)
        Me.rbNPCNone.Name = "rbNPCNone"
        Me.rbNPCNone.Size = New System.Drawing.Size(51, 17)
        Me.rbNPCNone.TabIndex = 0
        Me.rbNPCNone.TabStop = True
        Me.rbNPCNone.Tag = "0"
        Me.rbNPCNone.Text = "None"
        Me.rbNPCNone.UseVisualStyleBackColor = True
        '
        'rbNPCStandard
        '
        Me.rbNPCStandard.AutoSize = True
        Me.rbNPCStandard.ForeColor = System.Drawing.Color.White
        Me.rbNPCStandard.Location = New System.Drawing.Point(114, 2)
        Me.rbNPCStandard.Name = "rbNPCStandard"
        Me.rbNPCStandard.Size = New System.Drawing.Size(68, 17)
        Me.rbNPCStandard.TabIndex = 1
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
        'txtNpcFilter
        '
        Me.txtNpcFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtNpcFilter.ForeColor = System.Drawing.Color.Black
        Me.txtNpcFilter.Location = New System.Drawing.Point(10, 74)
        Me.txtNpcFilter.Name = "txtNpcFilter"
        Me.txtNpcFilter.Size = New System.Drawing.Size(433, 20)
        Me.txtNpcFilter.TabIndex = 3
        '
        'FilterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(453, 109)
        Me.Controls.Add(Me.txtNpcFilter)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.txtPCFilter)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "FilterForm"
        Me.Padding = New System.Windows.Forms.Padding(10)
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FilterForm"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbPCRegEx As System.Windows.Forms.RadioButton
    Friend WithEvents rbPCReverse As System.Windows.Forms.RadioButton
    Friend WithEvents rbPCStandard As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPCFilter As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNpcFilter As System.Windows.Forms.TextBox
    Friend WithEvents rbPCNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbNPCRegEx As System.Windows.Forms.RadioButton
    Friend WithEvents rbNPCReverse As System.Windows.Forms.RadioButton
    Friend WithEvents rbNPCNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbNPCStandard As System.Windows.Forms.RadioButton
End Class
