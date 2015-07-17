Public Class ItemOverlayForm

    Private Sub lblDescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblDescription.Click

    End Sub

    Private Sub lblDescription_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblDescription.Resize, Me.Resize
        Me.lblReuse.Location = New Point(Me.Width - (lblReuse.Width + 10), Me.lblDescription.Bottom + 2)
        Me.lblAHInfo.Location = New Point(lblDescription.Left, Me.lblReuse.Bottom + 6)
        Me.LoaderImage.Location = New Point(lblDescription.Left, Me.lblAHInfo.Bottom + 6)
    End Sub


End Class