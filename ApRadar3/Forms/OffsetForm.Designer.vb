<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OffsetForm
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
        Me.lblOffsets = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblOffsets
        '
        Me.lblOffsets.AutoSize = True
        Me.lblOffsets.Location = New System.Drawing.Point(13, 13)
        Me.lblOffsets.Name = "lblOffsets"
        Me.lblOffsets.Size = New System.Drawing.Size(39, 13)
        Me.lblOffsets.TabIndex = 0
        Me.lblOffsets.Text = "Label1"
        '
        'OffsetForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 302)
        Me.Controls.Add(Me.lblOffsets)
        Me.Name = "OffsetForm"
        Me.Text = "OffsetForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblOffsets As System.Windows.Forms.Label
End Class
