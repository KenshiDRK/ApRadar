<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RangesForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RangesForm))
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.cmdColor = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.nuRange = New System.Windows.Forms.NumericUpDown()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.dgRanges = New System.Windows.Forms.DataGridView()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        CType(Me.nuRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgRanges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HeaderPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdColor
        '
        Me.cmdColor.BackColor = System.Drawing.Color.LimeGreen
        Me.cmdColor.Location = New System.Drawing.Point(93, 38)
        Me.cmdColor.Name = "cmdColor"
        Me.cmdColor.Size = New System.Drawing.Size(75, 23)
        Me.cmdColor.TabIndex = 0
        Me.cmdColor.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(174, 215)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.Location = New System.Drawing.Point(93, 215)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'nuRange
        '
        Me.nuRange.Location = New System.Drawing.Point(12, 39)
        Me.nuRange.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nuRange.Name = "nuRange"
        Me.nuRange.Size = New System.Drawing.Size(75, 20)
        Me.nuRange.TabIndex = 2
        Me.nuRange.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(174, 38)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(75, 23)
        Me.cmdAdd.TabIndex = 3
        Me.cmdAdd.Text = "Add Range"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'dgRanges
        '
        Me.dgRanges.AllowUserToAddRows = False
        Me.dgRanges.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgRanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgRanges.Location = New System.Drawing.Point(12, 67)
        Me.dgRanges.Name = "dgRanges"
        Me.dgRanges.ReadOnly = True
        Me.dgRanges.Size = New System.Drawing.Size(237, 142)
        Me.dgRanges.TabIndex = 4
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackgroundImage = Global.ApRadar3.My.Resources.Resources.tbg
        Me.HeaderPanel.Controls.Add(Me.lblHeder)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Padding = New System.Windows.Forms.Padding(5, 0, 6, 0)
        Me.HeaderPanel.Size = New System.Drawing.Size(261, 33)
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
        Me.lblHeder.Size = New System.Drawing.Size(102, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "Custom Ranges"
        '
        'RangesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(261, 250)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Controls.Add(Me.dgRanges)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.nuRange)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.cmdColor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "RangesForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Custom Ranges"
        CType(Me.nuRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgRanges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents cmdColor As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents nuRange As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents dgRanges As System.Windows.Forms.DataGridView
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
End Class
