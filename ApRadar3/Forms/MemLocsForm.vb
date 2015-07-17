Imports FFXIMemory
Public Class MemLocsForm

    Private _dock As DockingClass



    Private Sub HeaderPanel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseDown, lblHeder.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.StartDockDrag(e.X, e.Y)
        End If
    End Sub

    Private Sub HeaderPanel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseMove, lblHeder.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
        End If
    End Sub

    Private Sub blnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles blnClose.Click
        Me.Close()
    End Sub

    Private Sub MemLocsForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        _dock = New DockingClass(Me) With {.UseDocking = True}
        For Each Item In MemoryScanner.Scanner.FFXI.MemLocs
            Me.lblMemlocs.Text &= ControlChars.NewLine & Item.Key
            Me.lblValues.Text &= ControlChars.NewLine & CInt(Item.Value).ToString("X2")
        Next
    End Sub
End Class