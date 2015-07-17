Imports FFXIMemory

Public Class OffsetForm

    Private Sub OffsetForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If MemoryScanner.Scanner.IsRunning Then
            For Each item In MemoryScanner.Scanner.FFXI.MemLocs
                lblOffsets.Text &= String.Format("{0} : {1:X2}{2}", item.Key, item.Value, ControlChars.NewLine)
            Next
        End If
    End Sub
End Class