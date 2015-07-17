<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MobTrackerForm
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
        Me.lbMobs = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lbMobs
        '
        Me.lbMobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbMobs.FormattingEnabled = True
        Me.lbMobs.Location = New System.Drawing.Point(0, 0)
        Me.lbMobs.Name = "lbMobs"
        Me.lbMobs.Size = New System.Drawing.Size(328, 318)
        Me.lbMobs.TabIndex = 0
        '
        'MobTrackerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 318)
        Me.Controls.Add(Me.lbMobs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "MobTrackerForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Mob Tracker"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbMobs As System.Windows.Forms.ListBox
End Class
