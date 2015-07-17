<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PCInfoForm
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
        Me.lblPCName = New System.Windows.Forms.Label()
        Me.PCBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.lbServerID = New System.Windows.Forms.Label()
        CType(Me.PCBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPCName
        '
        Me.lblPCName.AutoSize = True
        Me.lblPCName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PCBindingSource, "PCName", True))
        Me.lblPCName.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPCName.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.lblPCName.Location = New System.Drawing.Point(9, 9)
        Me.lblPCName.Name = "lblPCName"
        Me.lblPCName.Size = New System.Drawing.Size(48, 18)
        Me.lblPCName.TabIndex = 1
        Me.lblPCName.Text = "Label1"
        '
        'PCBindingSource
        '
        Me.PCBindingSource.DataMember = "PC"
        Me.PCBindingSource.DataSource = GetType(DataLibrary.ApRadarDataSet)
        '
        'txtNotes
        '
        Me.txtNotes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNotes.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PCBindingSource, "Notes", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtNotes.ForeColor = System.Drawing.Color.White
        Me.txtNotes.Location = New System.Drawing.Point(12, 30)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(288, 205)
        Me.txtNotes.TabIndex = 0
        '
        'lbServerID
        '
        Me.lbServerID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbServerID.AutoSize = True
        Me.lbServerID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PCBindingSource, "ServerID", True))
        Me.lbServerID.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbServerID.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.lbServerID.Location = New System.Drawing.Point(252, 9)
        Me.lbServerID.Name = "lbServerID"
        Me.lbServerID.Size = New System.Drawing.Size(48, 18)
        Me.lbServerID.TabIndex = 1
        Me.lbServerID.Text = "Label1"
        Me.lbServerID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PCInfoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(312, 247)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.lbServerID)
        Me.Controls.Add(Me.lblPCName)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(320, 36)
        Me.Name = "PCInfoForm"
        Me.Opacity = 0.8R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "PCInfoForm"
        Me.TopMost = True
        CType(Me.PCBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPCName As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents PCBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents lbServerID As System.Windows.Forms.Label
End Class
