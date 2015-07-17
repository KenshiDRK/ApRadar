Public Class RemoteChatMessageDialog

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        My.Settings.RemoteChatMessage = Not Me.CheckBox1.Checked
        My.Settings.Save()
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub RemoteChatMessageDialog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
    End Sub
End Class