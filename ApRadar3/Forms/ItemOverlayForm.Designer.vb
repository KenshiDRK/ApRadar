<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemOverlayForm
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
        Me.pbIcon = New System.Windows.Forms.PictureBox()
        Me.lblItemName = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.lblReuse = New System.Windows.Forms.Label()
        Me.lblAHInfo = New System.Windows.Forms.Label()
        Me.LoaderImage = New System.Windows.Forms.PictureBox()
        CType(Me.pbIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoaderImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbIcon
        '
        Me.pbIcon.Location = New System.Drawing.Point(9, 9)
        Me.pbIcon.Name = "pbIcon"
        Me.pbIcon.Size = New System.Drawing.Size(32, 32)
        Me.pbIcon.TabIndex = 0
        Me.pbIcon.TabStop = False
        '
        'lblItemName
        '
        Me.lblItemName.AutoEllipsis = True
        Me.lblItemName.AutoSize = True
        Me.lblItemName.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemName.ForeColor = System.Drawing.Color.White
        Me.lblItemName.Location = New System.Drawing.Point(47, 9)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.lblItemName.Size = New System.Drawing.Size(72, 20)
        Me.lblItemName.TabIndex = 1
        Me.lblItemName.Text = "Name Label"
        '
        'lblDescription
        '
        Me.lblDescription.AutoEllipsis = True
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.ForeColor = System.Drawing.Color.White
        Me.lblDescription.Location = New System.Drawing.Point(47, 26)
        Me.lblDescription.MaximumSize = New System.Drawing.Size(250, 0)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(66, 14)
        Me.lblDescription.TabIndex = 2
        Me.lblDescription.Text = "Desc Label"
        '
        'lblReuse
        '
        Me.lblReuse.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblReuse.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReuse.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.lblReuse.Location = New System.Drawing.Point(47, 43)
        Me.lblReuse.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.lblReuse.Name = "lblReuse"
        Me.lblReuse.Size = New System.Drawing.Size(147, 13)
        Me.lblReuse.TabIndex = 3
        Me.lblReuse.Tag = "exclude"
        Me.lblReuse.Text = "Reuse Delay"
        Me.lblReuse.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'lblAHInfo
        '
        Me.lblAHInfo.AutoEllipsis = True
        Me.lblAHInfo.AutoSize = True
        Me.lblAHInfo.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAHInfo.ForeColor = System.Drawing.Color.White
        Me.lblAHInfo.Location = New System.Drawing.Point(47, 56)
        Me.lblAHInfo.MaximumSize = New System.Drawing.Size(250, 0)
        Me.lblAHInfo.Name = "lblAHInfo"
        Me.lblAHInfo.Size = New System.Drawing.Size(106, 14)
        Me.lblAHInfo.TabIndex = 4
        Me.lblAHInfo.Text = "Loading AH Data..."
        Me.lblAHInfo.Visible = False
        '
        'LoaderImage
        '
        Me.LoaderImage.Image = Global.ApRadar3.My.Resources.Resources.ajax_loader__2_
        Me.LoaderImage.Location = New System.Drawing.Point(50, 73)
        Me.LoaderImage.Name = "LoaderImage"
        Me.LoaderImage.Size = New System.Drawing.Size(38, 14)
        Me.LoaderImage.TabIndex = 5
        Me.LoaderImage.TabStop = False
        Me.LoaderImage.Visible = False
        '
        'ItemOverlayForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(206, 64)
        Me.Controls.Add(Me.lblAHInfo)
        Me.Controls.Add(Me.LoaderImage)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.lblItemName)
        Me.Controls.Add(Me.pbIcon)
        Me.Controls.Add(Me.lblReuse)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ItemOverlayForm"
        Me.Opacity = 0.9R
        Me.Padding = New System.Windows.Forms.Padding(6)
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ItemOverlayForm"
        Me.TopMost = True
        CType(Me.pbIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoaderImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbIcon As System.Windows.Forms.PictureBox
    Friend WithEvents lblItemName As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblReuse As System.Windows.Forms.Label
    Friend WithEvents lblAHInfo As System.Windows.Forms.Label
    Friend WithEvents LoaderImage As System.Windows.Forms.PictureBox
End Class
