Imports System.Text.RegularExpressions
Imports System.Net
Imports System.IO
Imports System.Threading
Imports DataLibrary

Public Class AHParser
    Private WithEvents _wc As New Net.WebClient
    Private _bgThread As Thread
    Public Delegate Sub ItemDataCompleteEventHandler(ByVal ItemData As Item)
    Public Delegate Sub ItemSearchCompletedEventHandler(ByVal Data As AHFetcher.SearchResult())

    Public Event ItemDataComplete As ItemDataCompleteEventHandler
    Public Event ItemSearchCompleted As ItemSearchCompletedEventHandler

    Private _sync As SynchronizationContext = SynchronizationContext.Current
    Public ReadOnly Property Sync() As SynchronizationContext
        Get
            Return _sync
        End Get
    End Property
    Public Enum Speed
        Dead_Slow
        Very_Slow
        Slow
        Average
        Fast
        Very_Fast
    End Enum

    Public Structure History
        Public Sub New(ByVal FromWho As String, ByVal ToWho As String, ByVal Price As Integer, ByVal SaleDate As Date)
            Me.FromWho = FromWho
            Me.ToWho = ToWho
            Me.Price = Price
            Me.SaleDate = SaleDate
        End Sub

        Dim FromWho As String
        Dim ToWho As String
        Dim Price As Integer
        Dim SaleDate As Date
    End Structure

    Public Structure Item
        Dim ItemName As String
        Dim Stock As Integer
        Dim Min As Integer
        Dim Max As Integer
        Dim Average As Integer
        Dim PerDay As Single
        Dim Speed As Speed
        Dim StackPrice As String
        Dim History As List(Of History)
    End Structure

    Public Sub GetItemInfo(ByVal ItemName As String)
        _bgThread = New Thread(AddressOf CheckAHService) With {.IsBackground = True}
        _bgThread.Start(ItemName)
    End Sub

    Public Sub GetItemInfo(ByVal ItemID As Integer)
        _bgThread = New Thread(AddressOf CheckAHServiceByID) With {.IsBackground = True}
        _bgThread.Start(ItemID)
    End Sub

    Public Sub Abort()
        If Not _bgThread Is Nothing AndAlso _bgThread.IsAlive Then
            _bgThread.Abort()
        End If
    End Sub

    Private Sub CheckAHService(ByVal name As String)
        Using client As New AHFetcher.AHParserClient()
            Dim results As New List(Of AHFetcher.SearchResult)()
            Dim result = client.CheckItem([Enum].Parse(GetType(AHFetcher.Servers), My.Settings.Server), name)
            If Not result Is Nothing Then
                results.Add(result)
            End If
            result = client.CheckStack([Enum].Parse(GetType(AHFetcher.Servers), My.Settings.Server), name)
            If Not result Is Nothing Then
                results.Add(result)
            End If
            OnFinishedSearch(results.ToArray())
        End Using
    End Sub

    Private Sub CheckAHServiceByID(ByVal ItemID As Integer)
        Using client As New AHFetcher.AHParserClient()
            Dim results As New List(Of AHFetcher.SearchResult)()
            Dim result = client.CheckItemByID([Enum].Parse(GetType(AHFetcher.Servers), My.Settings.Server), ItemID)
            If Not result Is Nothing Then
                results.Add(result)
            End If
            result = client.CheckStackByID([Enum].Parse(GetType(AHFetcher.Servers), My.Settings.Server), ItemID)
            If Not result Is Nothing Then
                results.Add(result)
            End If
            OnFinishedSearch(results.ToArray())
        End Using
    End Sub

    'Private Sub GetItemInfoThread(ByVal ItemName As String)
    '    Dim id As Integer = (From c In DataAccess.MobData.Items Where c.ItemName.ToLower = ItemName.ToLower OrElse c.LongName.ToLower = ItemName.ToLower _
    '                         Select c.ItemID).FirstOrDefault

    '    Dim request As WebRequest
    '    Dim response As WebResponse
    '    Dim url As String

    '    If id > 0 Then
    '        request = WebRequest.Create(String.Format("http://ffxiah.com/item.php?id={0}&sid={1}", id, CInt([Enum].Parse(GetType(Servers), My.Settings.Server))))
    '    Else
    '        url = MainModule.GetFFXIAHSearchURL(ItemName)
    '        request = WebRequest.Create(url)
    '        ' Get the response.
    '        response = CType(request.GetResponse(), HttpWebResponse)
    '        request = WebRequest.Create(String.Format("{0}&sid={1}", response.ResponseUri.AbsoluteUri, CInt([Enum].Parse(GetType(Servers), My.Settings.Server))))
    '    End If
    '    ' Display the status.

    '    response = CType(request.GetResponse, HttpWebResponse)

    '    Dim dataStream = response.GetResponseStream()
    '    ' Open the stream using a StreamReader for easy access.
    '    Using reader As New StreamReader(dataStream)
    '        ' Read the content.
    '        Dim responseFromServer As String = reader.ReadToEnd()
    '        ' Cleanup the streams and the response.
    '        reader.Close()
    '        dataStream.Close()
    '        response.Close()
    '        Dim thisItem = ParseHtml(responseFromServer)
    '        OnCompleted(thisItem)
    '    End Using
    'End Sub

    Private Sub OnFinishedSearch(ByVal ItemData As AHFetcher.SearchResult())
        Sync.Post(New SendOrPostCallback(AddressOf FinishedSearch), ItemData)
    End Sub

    Private Sub FinishedSearch(ByVal state As Object)
        RaiseEvent ItemSearchCompleted(state)
    End Sub

    Private Sub OnCompleted(ByVal Item As Item)
        Sync.Post(New SendOrPostCallback(AddressOf ItemCompleted), Item)
    End Sub

    Private Sub ItemCompleted(ByVal state As Object)
        RaiseEvent ItemDataComplete(state)
    End Sub

    Private Sub _wc_DownloadStringcomplete(ByVal sender As Object, ByVal e As Net.DownloadStringCompletedEventArgs) Handles _wc.DownloadStringCompleted
        If e.Result <> String.Empty Then
            Dim thisItem = ParseHtml(e.Result)
            RaiseEvent ItemDataComplete(thisItem)
        End If
    End Sub

    Private Shared Function ParseHtml(ByVal HTML As String) As Item
        Dim thisItem As New Item() With {.History = New List(Of History)()}

        Dim startItem As Integer = HTML.IndexOf("<span class=item>")
        If startItem > -1 Then
            Dim endItem As Integer = HTML.IndexOf("</span>", startItem)
            If endItem > startItem Then
                Dim item As String = HTML.Substring(startItem, endItem - startItem)
                item = Regex.Replace(item, "<(.|\n)*?>", "").Replace("&nbsp;", String.Empty)

                thisItem.ItemName = item

                Dim startHistory As Integer = HTML.IndexOf("td['ph'] = ") + 12
                Dim endHistory As Integer = HTML.IndexOf(";", startHistory)
                If endHistory > startHistory Then
                    Dim history As String = HTML.Substring(startHistory, endHistory - startHistory)
                    Dim hEntries As String() = history.Split("],[")
                    For Each entry In hEntries
                        If entry.Trim <> String.Empty Then
                            thisItem.History.Add(ParseHistoryEntry(entry))
                        End If
                    Next



                    Dim startStats As Integer = HTML.IndexOf("<tr><td>Info</td><td>")
                    Dim endStats As Integer = HTML.IndexOf("</table>", startStats)
                    If endStats > startStats Then
                        Dim stats As String = HTML.Substring(startStats, endStats - startStats)
                        ParseStats(stats, thisItem)
                    End If
                End If
            End If
        End If
        Return thisItem
    End Function

    Private Shared Function ParseHistoryEntry(ByVal entry As String) As History
        entry = Regex.Replace(entry, "\[|\]", String.Empty)

        Dim parts As String() = GetParts(entry)
        If parts.Length = 7 Then
            Dim hist As New History() With {.SaleDate = Date.Parse(parts(0)), .FromWho = parts(2), .ToWho = parts(4), .Price = Integer.Parse(parts(5).Replace(",", String.Empty))}
            Return hist
        Else
            Return Nothing
        End If
    End Function

    Private Shared Sub ParseStats(ByVal stats As String, ByRef item As Item)
        stats = Regex.Replace(stats, "<(.|\n)*?>", ";")
        Do Until stats.IndexOf(";;") < 0
            stats = stats.Replace(";;", ";")
        Loop
        Dim parts As String() = stats.Split(";")
        For i = 0 To parts.Length - 1
            Select Case parts(i).Replace("Â", String.Empty)
                Case "Stock"
                    item.Stock = Integer.Parse(parts(i + 1))
                    i += 1
                Case "Rate"
                    item.Speed = [Enum].Parse(GetType(Speed), parts(i + 1).Replace(" ", "_"))
                    i += 1
                Case "Min"
                    item.Min = Integer.Parse(parts(i + 1).Replace(",", String.Empty))
                    i += 1
                Case "Max"
                    item.Max = Integer.Parse(parts(i + 1).Replace(",", String.Empty))
                    i += 1
                Case "Average"
                    item.Average = Integer.Parse(parts(i + 1).Replace(",", String.Empty))
                    i += 1
                Case "Stack Price"
                    item.StackPrice = parts(i + 1)
                    i += 1
                Case Else

            End Select
            If parts(i).Contains("sold/day") Then
                parts(i) = Regex.Replace(parts(i), "\(|\)|sold/day", String.Empty)
                item.PerDay = Single.Parse(parts(i))
            End If
        Next

    End Sub

    Private Shared Function GetParts(ByVal entry As String) As String()
        Dim items As New List(Of String)
        Dim finish As Integer
        Dim start As Integer = -1
        For i = 0 To entry.Length - 1
            If entry(i) = "'" Then
                If start = -1 Then
                    start = i + 1
                Else
                    finish = i
                End If
            End If
            If start > -1 AndAlso finish > 0 Then
                items.Add(entry.Substring(start, finish - start))
                start = -1
                finish = 0
            End If
        Next
        Return items.ToArray
    End Function
End Class
