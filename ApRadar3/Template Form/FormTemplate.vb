Public Class FormTemplate
    Inherits ResizableForm

    Private Sub FormTemplate_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub blnClose_Click(sender As System.Object, e As System.EventArgs) Handles blnClose.Click
        Me.Dispose()
    End Sub
End Class