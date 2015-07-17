Public Class RSSToolStrip
    Inherits ToolStripControlHost

    Public Sub New()
        MyBase.New(New FeedViewer)
        Me.FeedViewer.Dock = DockStyle.Fill
    End Sub

    Public ReadOnly Property FeedViewer() As FeedViewer
        Get
            Return CType(Control, FeedViewer)
        End Get
    End Property

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        Me.ToolTipText = Me.Text
        MyBase.OnTextChanged(e)
    End Sub
End Class
