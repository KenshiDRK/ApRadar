Public Class FeedViewer

#Region " PROPERTIES "


    Private _feedItem As RSSItem
    Public Property FeedItem() As RSSItem
        Get
            Return _feedItem
        End Get
        Set(ByVal value As RSSItem)
            _feedItem = value
            lnkTitle.Text = value.Title
            lnkTitle.Tag = value.Link
            Text = value.Description
            lblDate.Text = value.PubDate.ToString
            'Dim content As String = "<body style=font-family:arial;font-size:8pt>" & value.Content.Replace(ControlChars.NewLine, String.Empty) & "</body>"
            'Me.lnkTitle.Tag = content
            ''Me.wbContent.DocumentText = Regex.Replace(content, "\<fieldset class\=\""fieldset\""\>(.+?)\<\/fieldset\>", String.Empty)
        End Set
    End Property
#End Region

End Class
