<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MemLocsForm
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
        Me.lblMemlocs = New System.Windows.Forms.Label()
        Me.lblValues = New System.Windows.Forms.Label()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.HeaderPanel.Size = New System.Drawing.Size(346, 33)
        Me.HeaderPanel.TabIndex = 8
        '
        'lblHeder
        '
        Me.lblHeder.AutoSize = True
        Me.lblHeder.BackColor = System.Drawing.Color.Transparent
        Me.lblHeder.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeder.ForeColor = System.Drawing.Color.White
        Me.lblHeder.Location = New System.Drawing.Point(9, 6)
        Me.lblHeder.Name = "lblHeder"
        Me.lblHeder.Size = New System.Drawing.Size(128, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "Memlocs Info Form"
        '
        'blnClose
        '
        Me.blnClose.BackColor = System.Drawing.Color.Transparent
        Me.blnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.blnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.blnClose.Location = New System.Drawing.Point(324, 0)
        Me.blnClose.Name = "blnClose"
        Me.blnClose.Size = New System.Drawing.Size(16, 33)
        Me.blnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.blnClose.TabIndex = 0
        Me.blnClose.TabStop = False
        '
        'lblMemlocs
        '
        Me.lblMemlocs.AutoSize = True
        Me.lblMemlocs.ForeColor = System.Drawing.Color.White
        Me.lblMemlocs.Location = New System.Drawing.Point(12, 40)
        Me.lblMemlocs.Name = "lblMemlocs"
        Me.lblMemlocs.Size = New System.Drawing.Size(49, 13)
        Me.lblMemlocs.TabIndex = 9
        Me.lblMemlocs.Text = "Memlocs"
        '
        'lblValues
        '
        Me.lblValues.AutoSize = True
        Me.lblValues.ForeColor = System.Drawing.Color.White
        Me.lblValues.Location = New System.Drawing.Point(217, 40)
        Me.lblValues.Name = "lblValues"
        Me.lblValues.Size = New System.Drawing.Size(39, 13)
        Me.lblValues.TabIndex = 9
        Me.lblValues.Text = "Values"
        '
        'MemLocsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(346, 295)
        Me.Controls.Add(Me.lblValues)
        Me.Controls.Add(Me.lblMemlocs)
        Me.Controls.Add(Me.HeaderPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "MemLocsForm"
        Me.Text = "MemLocsForm"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents blnClose As System.Windows.Forms.PictureBox
    Friend WithEvents lblMemlocs As System.Windows.Forms.Label
    Friend WithEvents lblValues As System.Windows.Forms.Label
End Class
