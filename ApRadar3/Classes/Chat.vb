Imports System.Threading
Imports System.Text.RegularExpressions

Public Class Chat
#Region " VARIABLES "
    Private chatBase As Integer

    Private WithEvents chattimer As Timers.Timer
    Private LineCount As Integer
    Private _pol As Process
    Private lastline As Integer
    Private tComplete As Boolean = True
    Private logStart As Integer
    Private LogOffsets As Byte()
    Private lineOffsets As New List(Of Int16)
    Private TStart As Threading.ThreadStart
    Private _isVista As Boolean
    Private OffBytes As Byte()
    Private _isRunning As Boolean

    'THREAD ITEMS
    Private LineInfo As ChatLine
    Private line As Byte()
    Private LineString As String
    Private ItemsArr As String()
    Private b As Byte()
    Private c As Char() = {","c}
    Private MsgID As Integer
    Private _sync As SynchronizationContext = SynchronizationContext.Current
#End Region

#Region " PROPERTIES "

    ''' <summary>
    ''' Get the chatlog base offset
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ChatBaseOffset() As Integer
        Get
            Return chatBase
        End Get
    End Property

    ''' <summary>
    ''' Process to monitor
    ''' </summary>
    ''' <value>POL Process</value>
    ''' <remarks></remarks>
    Public WriteOnly Property POL() As Process
        Set(ByVal value As Process)
            If _isRunning Then
                StopLogging()
                _pol = value
                Initialize(_pol)
                StartLogging()
            Else
                _pol = value
                Initialize(_pol)
            End If
        End Set
    End Property
#End Region

#Region " DELEGATES "
    Public Delegate Sub NewLineEventHandler(ByVal LineInfo As ChatLine)
#End Region

#Region " EVENTS "
    ''' <summary>
    ''' Event raised when a new chat line is added
    ''' </summary>
    ''' <param name="LineInfo">The chatline</param>
    ''' <remarks></remarks>
    Public Event NewLine As NewLineEventHandler
#End Region

#Region " DELEGATES "
    ''' <summary>
    ''' Delegate used for invoking the main thread from the chat monitor thread
    ''' </summary>
    ''' <param name="LineInfo">The chatline</param>
    ''' <remarks></remarks>
    Public Delegate Sub LineAdded(ByVal LineInfo As ChatLine)
#End Region

#Region " ENUM "
    ''' <summary>
    ''' Enumeration to hold offset positions
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum offsets
        OffsetArray = 4
        LastIndexArray = 100
        TotalLineCount = 8
        EndPointer = 12
        BlockLineCount = 240
        LogStart = 244
        LastLogStart = 248
        EndLogOffset = 256
    End Enum
#End Region

#Region " STRUCTURES "
    ''' <summary>
    ''' Structure of the chatline
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    <Serializable()> _
    Public Structure ChatLine
        Dim ChatType As String
        Dim UK1 As String
        Dim UK2 As String
        Dim color As String
        Dim MsgID As Integer
        Dim GroupMsgID As Integer
        Dim UK3 As String
        Dim UK4 As String
        Dim UK5 As String
        Dim UK6 As String
        Dim UK7 As String
        Dim LineText As String
    End Structure

    ''' <summary>
    ''' Unused atm
    ''' </summary>
    ''' <remarks></remarks>
    Private Structure ThreadItems
        Dim POL As Process
        Dim LineCount As String
        Dim LastLine As Integer
        Dim LogStart As Integer
        Dim LineOffsets As List(Of Int16)
    End Structure
#End Region

#Region " INITIALIZE "
    ''' <summary>
    ''' Create a new instance of the ChatLog class
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        If Not Initialize() Then
            MsgBox("Unable to initialize... FFXI must be running")
        End If
    End Sub

    Public Sub New(ByVal POL As Process)
        If Not Initialize(POL) Then
            MsgBox("Unable to initialize... FFXI Must be running")
        End If
    End Sub

    ''' <summary>
    ''' Initializes the chat monitor, checking for POL and getting the ChatBase pointer
    ''' </summary>
    ''' <returns>True if all is well, False if errors</returns>
    ''' <remarks></remarks>
    Private Function Initialize() As Boolean
        If _pol Is Nothing Then
            _pol = FFXIMemory.Memory.FindProcess("pol")
            If Not _pol Is Nothing Then
                Return Initialize(_pol)
            Else
                Return False
            End If
        End If

        Return True
    End Function

    Private Function Initialize(ByVal POL As Process) As Boolean
        Try
            _pol = POL
            chattimer = New Timers.Timer
            chattimer.Interval = 100
            'logthread = New LogThread
            Dim fmem As New FFXIMemory.MemLocs(_pol)
            chatBase = fmem.locs.Item("CHATBASE")
            fmem = Nothing
            chatBase = New FFXIMemory.Memory(POL, chatBase + 4).GetInt32
            If My.Computer.Info.OSVersion >= "6.0" Then
                chatBase = New FFXIMemory.Memory(POL, chatBase + 4).GetInt32
                _isVista = True
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region " CONTROL FUNCTIONS (Stop/Start) "
    ''' <summary>
    ''' Start monitoring the chatlog
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartLogging()
        lastline = -1
        If _pol Is Nothing Then
            If Initialize() Then
                chattimer.Start()
            Else
                MsgBox("Unable to initialize... FFXI must be running")
            End If
        Else
            chattimer.Start()
        End If
        _isRunning = True
    End Sub

    ''' <summary>
    ''' Stop monitoring the chatlog
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopLogging()
        chattimer.Stop()
        _isRunning = False
    End Sub
#End Region

#Region " EVENT HANDLERS "
    ''' <summary>
    ''' The chatTimer tick event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chattimer_tick(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs) Handles chattimer.Elapsed
        If tComplete Then
            getLoginfo()
        End If
    End Sub
#End Region

#Region " FUNCTIONS "
    ''' <summary>
    ''' Sub called by the timer to get the log info
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getLoginfo()
        tComplete = False
        LogOffsets = New FFXIMemory.Memory(_pol, chatBase).GetByteArray(258)
        If _isVista Then
            LineCount = LogOffsets(200)
        Else
            LineCount = LogOffsets(240)
        End If
        If lastline = -1 Then lastline = LineCount
        getoffsets()
        If _isVista Then
            logStart = BitConverter.ToInt32(LogOffsets, 204)
        Else
            logStart = BitConverter.ToInt32(LogOffsets, 244)
        End If
        If lastline > LineCount Then
            TStart = New Threading.ThreadStart(AddressOf Process)
            Dim t As New Thread(TStart)
            t.Start()
            t.Join()
        Else
            TStart = New Threading.ThreadStart(AddressOf Process)

            Dim t As New Thread(TStart)
            t.Start()
            t.Join()
        End If
    End Sub

    ''' <summary>
    ''' Gets the curent chatline offsets
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getoffsets()
        If _isVista Then
            Array.Resize(OffBytes, 200)
            Array.Copy(LogOffsets, OffBytes, 200) ' 4)).GetByteArray(100)
        Else
            OffBytes = New FFXIMemory.Memory(_pol, BitConverter.ToInt32(LogOffsets, 4)).GetByteArray(100)
        End If
        lineOffsets.Clear()
        lineOffsets.Add(0)
        For i As Integer = 2 To 98 Step 2
            If BitConverter.ToInt16(OffBytes, i) = 0 Then Exit For
            lineOffsets.Add(BitConverter.ToInt16(OffBytes, i))
            'TODO: Source array was not long enough. Check srcIndex and length, and the array's lower bounds.
        Next
    End Sub

    ''' <summary>
    ''' Gets the line count in the current block
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBlockLineCount() As Integer
        Return LogOffsets(240)
    End Function

    ''' <summary>
    ''' Gets the total line count
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTotalChatLineCount() As Integer
        Return LogOffsets(8)
    End Function

    ''' <summary>
    ''' Function called from within the thread to call the newline event.  If the SyncObject is set, it will check if InvokeRequired and do so if necessary
    ''' </summary>
    ''' <param name="iLineInfo"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub OnNewLine(ByVal iLineInfo As ChatLine)
        _sync.Post(New SendOrPostCallback(AddressOf RaiseNewLineEvent), iLineInfo)
    End Sub

    Private Sub RaiseNewLineEvent(ByVal state As Object)
        RaiseEvent NewLine(CType(state, ChatLine))
    End Sub
#End Region

#Region " THREAD FUNCTIONS "
    ''' <summary>
    ''' The monitoring thread
    ''' </summary>
    ''' <remarks>Called every 100ms by the loop timer</remarks>
    Private Sub Process()
        Try
            If lastline > LineCount Then
                For i As Integer = lastline To 49
                    'Read the chat line into a byte array
                    line = New FFXIMemory.Memory(_pol, logStart + lineOffsets.Item(i)).GetByteArray(200)
                    'Convert the bytes to a string
                    LineString = System.Text.Encoding.Default.GetString(line)
                    'Split the string into parts
                    ItemsArr = LineString.Split(c, 12)
                    'The chat type
                    LineInfo.ChatType = ItemsArr(0)
                    'Unknown value 1
                    LineInfo.UK1 = ItemsArr(1)
                    'Unknown value 2
                    LineInfo.UK2 = ItemsArr(2)
                    'The line color
                    LineInfo.color = ItemsArr(3)
                    'Chat line message ID
                    LineInfo.MsgID = Integer.Parse(ItemsArr(4), Globalization.NumberStyles.AllowHexSpecifier)
                    'Chatline Group Message ID
                    LineInfo.GroupMsgID = Integer.Parse(ItemsArr(5), Globalization.NumberStyles.AllowHexSpecifier)
                    'Unknown value 3
                    LineInfo.UK3 = ItemsArr(6)
                    'Unknown value 4
                    LineInfo.UK4 = ItemsArr(7)
                    'Unknown value 5
                    LineInfo.UK5 = ItemsArr(8)
                    'Unknown value 6
                    LineInfo.UK6 = ItemsArr(9)
                    'Unknown value 7
                    LineInfo.UK7 = ItemsArr(10)
                    'Get the bytes for the actual text
                    b = System.Text.Encoding.Default.GetBytes(ItemsArr(11))
                    'Clean up the text and set the line
                    LineInfo.LineText = CleanUpString(b, LineInfo.ChatType, LineInfo)
                    'Raise the AddLine event
                    OnNewLine(LineInfo)
                    'RaiseEvent NewLine(LineInfo)
                Next
            Else
                For i As Integer = lastline To LineCount - 1
                    'Read the chat line into a byte array
                    line = New FFXIMemory.Memory(_pol, logStart + lineOffsets.Item(i)).GetByteArray(200)
                    'Convert the bytes to a string
                    LineString = System.Text.Encoding.Default.GetString(line)
                    'Split the string into parts
                    ItemsArr = LineString.Split(c, 12)
                    'The chat type
                    LineInfo.ChatType = ItemsArr(0)
                    'Unknown value 1
                    LineInfo.UK1 = ItemsArr(1)
                    'Unknown value 2
                    LineInfo.UK2 = ItemsArr(2)
                    'The line color
                    LineInfo.color = ItemsArr(3)
                    'Chat line message ID
                    LineInfo.MsgID = Integer.Parse(ItemsArr(4), Globalization.NumberStyles.AllowHexSpecifier)
                    'Chatline Group Message ID
                    LineInfo.GroupMsgID = Integer.Parse(ItemsArr(5), Globalization.NumberStyles.AllowHexSpecifier)
                    'Unknown value 3
                    LineInfo.UK3 = ItemsArr(6)
                    'Unknown value 4
                    LineInfo.UK4 = ItemsArr(7)
                    'Unknown value 5
                    LineInfo.UK5 = ItemsArr(8)
                    'Unknown value 6
                    LineInfo.UK6 = ItemsArr(9)
                    'Unknown value 7
                    LineInfo.UK7 = ItemsArr(10)
                    'Get the bytes for the actual text
                    b = System.Text.Encoding.Default.GetBytes(ItemsArr(11))
                    'Clean up the text and set the line
                    LineInfo.LineText = CleanUpString(b, LineInfo.ChatType, LineInfo)
                    'Raise the AddLine event
                    OnNewLine(LineInfo)
                    'RaiseEvent NewLine(LineInfo)
                Next
            End If
        Catch ex As Exception
            Debug.Print("Line Error: " & Date.Now)
            'Catch any mishaps and continue
        Finally
            If lastline > LineCount Then
                lastline = 0
            Else
                lastline = LineCount
            End If
            'Raise the Thread complete event
            'RaiseEvent ThreadComplete()
            tComplete = True
        End Try
    End Sub

    ''' <summary>
    ''' Cleans up the Linetext from the chatlog, removing any strange or unknown characters
    ''' </summary>
    ''' <param name="bytes">The bytes to perform clean up on</param>
    ''' <param name="ChatType">The chat line type</param>
    ''' <param name="li">the chatline structure</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function CleanUpString(ByVal bytes As Byte(), ByVal ChatType As String, ByVal li As ChatLine) As String
        Dim bJP As Boolean = False
        Dim pattern As String
        If ChatType <> "ce" OrElse ChatType = "8d" Then
            If MsgID <> li.MsgID Then
                If ChatType = "7b" Then
                    pattern = "[A-Z0-9]|<|>|}|\(|=|ï"
                Else
                    pattern = "[A-Z0-9]|<|>|{|}|\(|=|ï"
                End If

                Do Until Regex.Match(Chr(bytes(0)).ToString, pattern).Success  'bytes(0) = 40 OrElse bytes(0) = 61 OrElse bytes(0) = 60 OrElse bytes(0) = 62 OrElse (bytes(0) >= 65 AndAlso bytes(0) <= 90)
                    Array.Copy(bytes, 1, bytes, 0, bytes.Length - 2)
                Loop
                MsgID = li.MsgID
            Else
                If ChatType = "7b" Then
                    pattern = "[A-Za-z0-9]|<|>|}|\(|=|ï"
                Else
                    pattern = "[A-Za-z0-9]|<|>|{|}|\(|=|ï"
                End If

                Do Until Regex.Match(Chr(bytes(0)).ToString, pattern).Success 'bytes(0) = 40 OrElse bytes(0) = 61 OrElse Chr(bytes(0)) = "{"c _
                    'OrElse Chr(bytes(0)) = "}"c OrElse (bytes(0) >= 65 AndAlso bytes(0) <= 90) OrElse (bytes(0) >= 97 AndAlso bytes(0) <= 122)
                    Array.Copy(bytes, 1, bytes, 0, bytes.Length - 2)
                Loop
            End If
        Else
            Array.Copy(bytes, 4, bytes, 0, bytes.Length - 4)
        End If
        'Clean out any dirty characters 
        Dim b1 As New List(Of Byte)
        For i As Integer = 0 To bytes.GetUpperBound(0)
            'Need to handle the Items in the chat log
            If bytes(i) <> 30 Then
                If bytes(i) = 2 Then
                    b1.Add(1)
                Else
                    b1.Add(bytes(i))
                End If
            End If
        Next
        bytes = b1.ToArray
        b1.Clear()
        b1 = Nothing
        For x As Integer = 0 To bytes.GetUpperBound(0)
            'check for the end of the line and resize the array
            If bytes(x) = 0 Then
                Array.Resize(bytes, x)
                Exit For
            ElseIf bytes(x) = 239 AndAlso bytes(x + 1) = 39 Then 'Look for auto Translate codes
                'Replace with " {"

                bytes(x) = 32
                bytes(x + 1) = 123
            ElseIf bytes(x) = 239 AndAlso bytes(x + 1) = 40 Then 'Closing AT
                'Replace with "} "
                bytes(x) = 125
                bytes(x + 1) = 32
            ElseIf bytes(x) = 127 Then
                bytes(x) = 32
            End If
            If System.Text.Encoding.Default.GetString(bytes).Contains("Moogle : ") Then
            Else
                If bytes(x) > 127 Then
                    bJP = True
                End If
            End If
        Next
        If bJP Then
            Return ConvertToJPText(System.Text.Encoding.Default.GetString(bytes)).TrimEnd("1"c).Replace("  ", " ")
        Else
            Return System.Text.Encoding.Default.GetString(bytes).Replace("  ", " ").TrimEnd("1"c)
        End If
    End Function

    ''' <summary>
    ''' Converts a string to Shift-JIS format
    ''' </summary>
    ''' <param name="source">The source string to convert</param>
    ''' <returns>A string in Shift-JIS format</returns>
    ''' <remarks>This only works if the user has support for far eastern languages installed</remarks>
    Public Shared Function ConvertToJPText(ByVal source As String) As String
        Dim eEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift-jis")
        Dim SourceEncoding As System.Text.Encoding = System.Text.Encoding.Default
        Dim b As Byte() = SourceEncoding.GetBytes(source)
        Return eEncoding.GetString(b)
    End Function
#End Region

#Region " NON IMPLEMENTED FUNCTIONS "
    ''' <summary>
    ''' Test function for changing the text ina chatline
    ''' </summary>
    ''' <param name="LineNumber">The line number to change</param>
    ''' <param name="NewText">The new text to set</param>
    ''' <remarks>Does not work properly and is not currently used</remarks>
    Public Sub SetLineText(ByVal LineNumber As Integer, ByVal NewText As String)
        Dim loc As Integer = logStart + lineOffsets(LineNumber - 1) + 57
        Dim line As Byte() = New FFXIMemory.Memory(_pol, loc).GetByteArray(143)
        MsgBox(System.Text.Encoding.Default.GetString(line))
        Dim start As Integer = Array.IndexOf(line, Chr(&H20))
        If start < 0 Then
            start = 0
        End If
        Dim fmem As New FFXIMemory.Memory(_pol, loc + start)
        Dim b As Byte() = System.Text.Encoding.Default.GetBytes(NewText & Chr(0))
        fmem.SetByteArray(b)
    End Sub
#End Region
End Class

