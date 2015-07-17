Imports System.Net
Imports System.Xml

Public Class RSS
    Private _feedItems As List(Of RSSItem)
    Public ReadOnly Property FeedItems() As List(Of RSSItem)
        Get
            If _feedItems Is Nothing Then
                _feedItems = New List(Of RSSItem)
            End If
            Return _feedItems
        End Get
    End Property

    Public Sub LoadFeed(ByVal URL As String)
        Try
            Using wc As New WebClient()
                Dim rssRaw As String = wc.DownloadString(URL)
                If rssRaw <> String.Empty Then
                    Dim rssDoc As New XmlDocument()
                    rssDoc.LoadXml(rssRaw)
                    Dim rssItems As XmlNodeList = rssDoc.SelectNodes("rss/channel/item")
                    Dim partNode As XmlNode
                    Dim title As String
                    Dim link As String
                    Dim description As String
                    Dim content As String
                    Dim pubDate As DateTime
                    For Each item As XmlNode In rssItems
                        'Get the title
                        partNode = item.SelectSingleNode("title")
                        title = partNode.InnerText
                        'Get the publish date
                        partNode = item.SelectSingleNode("pubDate")
                        pubDate = DateTime.Parse(partNode.InnerText)
                        'Get the link
                        partNode = item.SelectSingleNode("link")
                        link = partNode.InnerText
                        'Get the description
                        partNode = item.SelectSingleNode("description")
                        description = partNode.InnerText
                        'Get the content
                        partNode = partNode.NextSibling
                        content = partNode.InnerText
                        FeedItems.Add(New RSSItem(title, link, pubDate, description, content))
                    Next
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class

Public Class RSSItem
    Public Sub New(ByVal Title As String, ByVal Link As String, ByVal PubDate As DateTime, ByVal Description As String, ByVal content As String)
        Me.Title = Title
        Me.Link = Link
        Me.PubDate = PubDate
        Me.Description = Description
        Me.Content = content
    End Sub
    Public Property Title() As String
    Public Property Link() As String
    Public Property PubDate() As DateTime
    Public Property Description() As String
    Public Property Content() As String
End Class
