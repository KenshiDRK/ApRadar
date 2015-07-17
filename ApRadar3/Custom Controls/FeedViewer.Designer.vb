<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FeedViewer
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblDate = New System.Windows.Forms.Label
        Me.lnkTitle = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Gray
        Me.lblDate.Location = New System.Drawing.Point(4, 19)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.lblDate.Size = New System.Drawing.Size(55, 14)
        Me.lblDate.TabIndex = 4
        Me.lblDate.Text = "PublishDate"
        '
        'lnkTitle
        '
        Me.lnkTitle.AutoEllipsis = True
        Me.lnkTitle.AutoSize = True
        Me.lnkTitle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkTitle.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lnkTitle.Location = New System.Drawing.Point(3, 4)
        Me.lnkTitle.Name = "lnkTitle"
        Me.lnkTitle.Size = New System.Drawing.Size(31, 15)
        Me.lnkTitle.TabIndex = 3
        Me.lnkTitle.TabStop = True
        Me.lnkTitle.Text = "Title"
        '
        'FeedViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lnkTitle)
        Me.Name = "FeedViewer"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.Size = New System.Drawing.Size(275, 38)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents lnkTitle As System.Windows.Forms.LinkLabel

End Class
