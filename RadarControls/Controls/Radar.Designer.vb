<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Radar
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
        Me.components = New System.ComponentModel.Container
        Me.MemoryScanTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MousePosTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'MemoryScanTimer
        '
        '
        'Radar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Radar"
        Me.Size = New System.Drawing.Size(532, 482)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MemoryScanTimer As System.Windows.Forms.Timer
    Friend WithEvents MousePosTimer As System.Windows.Forms.Timer

End Class
