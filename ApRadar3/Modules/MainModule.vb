Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text.Encoding
Imports Microsoft.Win32
Imports System.IO
Imports System.Xml.Serialization
Imports System.Reflection

Module MainModule
#Region " CONSTANTS "
    Public Const Alla As String = "http://ffxi.allakhazam.com/search.html?q={0}"
    Public Const wiki As String = "http://wiki.ffxiclopedia.org/wiki/Special:Search?search={0}"
    Public Const ffxiah As String = "http://www.ffxiah.com/search/item?sid={1}&search_q={0}"
    Public Const ffxiahItemUrl As String = "http://www.ffxiah.com/item/{0}/"
    Public Const HeadingBlock As Integer = 45

    Public LicensePath As String = String.Format("{0}\ApneaSoft.ApRadar3.license", Application.StartupPath)
#End Region

#Region " PRIVATE MEMBERS "
    Private _toastForms As New List(Of Form)
#End Region

#Region " PUBLIC VARIABLES "
    Public IsProEnabled As Boolean = True
    Public NeedsUpdate As Boolean
    Public UpdatePath As String = String.Empty
    Public NoBar As Boolean
    Public DebugRun As Boolean
#End Region

#Region " STRUCTURES "
    ''' <summary>
    ''' Mob structure
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MobInfo
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Public EntityVTablePtr As Byte() '0
        Public LastX As Single '4
        Public LastZ As Single '8
        Public LastY As Single '12
        Public LocUnk As Single '16
        Public LocRoll As Single '20
        Public LastDirection As Single '24
        Public LocPitch As Single '28
        Public Unk01 As Single '32
        Public PosX As Single '36
        Public PosZ As Single '40
        Public PosY As Single '44
        Public LastUnk As Single '48
        Public LastRoll As Single '52
        Public PosDirection As Single '56
        Public LastPitch As Single '60
        Public Unk02 As Integer '64
        Public MoveX As Single '68
        Public MoveZ As Single '72
        Public MoveY As Single '76
        Public MoveUnk As Single '80
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=28)> _
        Public Unk03 As Byte() '84
        Public UnknownVTablePtr As Integer '112
        Public ID As Integer '116
        Public ServerCharId As Integer '120
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=28)> _
        Public MobName As String '124
        Public RunSpeed As Single '152
        Public AnimationSpeed As Single '156
        Public WarpInfo As Integer '160 WarpStruct Pointer
        Public Unk04 As Integer '164
        Public Unk05 As Integer  '168
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=44)> _
        Public Unk06 As Byte() '172
        Public distance As Single '216
        Public Unk07 As Integer '220
        Public Unk08 As Integer '224
        Public Heading As Single '228
        Public PetOwnderID As Integer '232
        Public HP_Percent As Byte '236
        Public Unk09 As Byte '237
        Public MobType As Byte '238
        Public Race As Byte '239
        Public AttackTimer As Byte '240
        Public Unk10 As Short '241
        Public Unk11 As Byte '243
        Public Fade As Byte '244
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=7)> _
        Public Unk13 As Byte() '245
        Public Hair As Short '252
        Public Head As Short '254
        Public Body As Short '256
        Public Hands As Short '258
        Public Legs As Short '260
        Public Feet As Short '262
        Public MainWeapon As Short '264
        Public SubWeapon As Short '266
        Public Ranged As Short '268
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=14)> _
        Public Unk14 As Byte() '270
        Public ActionWaitTimer1 As Short '284
        Public ActionWaitTimer2 As Short '286
        Public Flags1 As Integer '288
        Public pIcon As Integer '292
        Public gIcon As Integer '296
        Public Flags4 As Integer '300
        Public Flags5 As Integer '304
        Public Unk15 As Single '308
        Public Unk16 As Integer '312
        Public Unk17 As Short '316
        Public Unk18 As Integer '318
        Public NpcSpeechLoop As Short '322
        Public NPCSpeechFrame As Short '324
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=18)> _
        Public Unk19 As Byte() '326
        Public Speed As Single '344
        Public NPCWalkPos1 As Short '348
        Public NPCWalkPos2 As Short '350
        Public NPCWalkMode As Short '352
        Public CostumeID As Short '354
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public mou4 As String '356
        Public Status As Integer '360
        Public Status2 As Integer '364
        Public StatusNpcChat As Integer '368
        Public Unk20 As Integer '372
        Public Unk21 As Integer '376
        Public Unk22 As Integer '380
        Public Unk23 As Integer '384
        Public ClaimedBy As Integer '388
        Public Unk24 As Integer '392
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation1 As String '396
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation2 As String '400
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation3 As String '404
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation4 As String '408
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation5 As String '412
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation6 As String '416
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation7 As String '420
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation8 As String '424
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation9 As String '428
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation10 As String '432
        Public AnimationTime As Short '436
        Public AnimationStep As Short '438
        Public AnimationPlay As Byte '440
        Public Unk25 As Byte '441
        Public Unk26 As Short '442
        Public Unk27 As Short '444
        Public Unk28 As Short '446
        Public EmoteID As Integer '448
        Public Unk29 As Integer '452
        Public Unk30 As Integer '456
        Public SpawnType As Integer '460
        Public LSColorRed As Byte '464
        Public LSColorGreen As Byte '465
        Public LSColorBlue As Byte '466
        Public LSUnk As Byte '467
        Public NameColor As Short '468
        <MarshalAs(UnmanagedType.I1)> _
        Public CampaignMode As Boolean '470
        Public Unk31 As Byte '471
        Public FishingTimer As Short '472
        Public FishingCastTimer As Short '4674
        Public FishingUnknown0001 As Integer '476
        Public FishingUnknown0002 As Integer '480
        Public FishingUnknown0003 As Short '484
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=14)> _
        Public Unk32 As Byte() '486
        Public PCTarget As Short '500
        Public PetIndex As Short '502
        Public Unk33 As Short '504
        Public Unk34 As Byte '506
        Public BallistaScoreFlag As Byte '507
        Public PankrationEnabled As Byte '508
        Public PankrationFlagFlip As Byte '509
        Public Unk35 As Short '510
        Public ModelSize As Single '512
        Public Unk36 As Integer '516
        Public Unk37 As Short '520
        Public Unk38 As Short '522
        Public MonstrosityFlag As Short '524
        Public Unk39 As Short '526
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=36)> _
        Public MonstrosityName As String '528
    End Structure
#End Region

#Region " ENUM "
    Public Enum SearchProvider
        FFXIClopedia
        Allakhazam
        ItemsDatabase
        Recipes
        FFXIAH
    End Enum

    Public Enum Heading
        N
        NE
        E
        SE
        S
        SW
        W
        NW
    End Enum

    Public Enum Crystal
        Fire = 4096
        Ice = 4097
        Wind = 4098
        Earth = 4099
        Lightning = 4100
        Water = 4101
        Light = 4102
        Dark = 4103
    End Enum

    Public Enum FilterType
        None
        Standard
        Reverse
        RegEx
    End Enum

    Public Enum UpdateType
        MapINI
        Complete
    End Enum

    Public Enum Servers
        Alexander = 10
        Asura = 28
        Bahamut = 1
        Bismarck = 25
        Caitsith = 15
        Carbuncle = 6
        Cerberus = 23
        Diabolos = 14
        Fairy = 30
        Fenrir = 7
        Garuda = 22
        Gilgamesh = 19
        Hades = 32
        Ifrit = 13
        Kujata = 24
        Lakshmi = 27
        Leviathan = 11
        Midgardsormr = 29
        Odin = 12
        Pandemonium = 21
        Phoenix = 5
        Quetzalcoatl = 16
        Ragnarok = 20
        Ramuh = 4
        Remora = 31
        Seraph = 26
        Shiva = 2
        Siren = 17
        Sylph = 8
        Titan = 3
        Unicorn = 18
        Valefor = 9
    End Enum

    Public Enum VerificationResult
        NoConnection
        Valid
        Invalid
    End Enum
#End Region

#Region " PUBLIC METHODS "
    Private Function UrlEncode(ByVal s As String) As String
        Dim parts As Char() = s.ToCharArray
        Dim output As String = String.Empty

        For Each c As Char In parts
            If Char.IsLetterOrDigit(c) Then
                output &= c
            Else
                output &= String.Format("%{0}", Asc(c).ToString("X2"))
            End If
        Next
        Return output
    End Function

    Public Function GetSearchUrl(ByVal Provider As SearchProvider, ByVal SearchTerms As String) As String
        Select Case Provider
            Case SearchProvider.FFXIClopedia
                Return String.Format(wiki, SearchTerms)
            Case SearchProvider.Allakhazam
                Return String.Format(Alla, UrlEncode(SearchTerms))
            Case SearchProvider.ItemsDatabase
                Return "Items"
            Case SearchProvider.Recipes
                Return "Recipes"
            Case SearchProvider.FFXIAH
                Return GetFFXIAHSearchURL(SearchTerms)
            Case Else
                Return String.Empty
        End Select
    End Function

    Public Function GetFFXIAHSearchURL(ByVal SearchTerms As String) As String
        Dim itemID As Integer? = DataLibrary.DataAccess.GetItemID(SearchTerms)
        If itemID.HasValue Then
            Return String.Format(ffxiahItemUrl, itemID)
        Else
            Return String.Format(ffxiah, SearchTerms, CInt([Enum].Parse(GetType(Servers), My.Settings.Server)))
        End If
    End Function

    Public Function RadiansToDegrees(ByVal Radians As Single) As Single
        Dim degrees As Single = CSng(Radians * (180 / Math.PI)) + 90
        If degrees < 0 Then
            degrees = 360 + degrees
        End If
        Return degrees
    End Function

    Public Function GetHeading(ByVal Direction As Single) As String
        Dim degrees As Single = RadiansToDegrees(Direction)

        If degrees >= 338 Or degrees < 23 Then
            Return "N"
        ElseIf degrees >= 23 AndAlso degrees <= 68 Then
            Return "NE"
        ElseIf degrees > 68 AndAlso degrees <= 113 Then
            Return "E"
        ElseIf degrees > 113 AndAlso degrees <= 158 Then
            Return "SE"
        ElseIf degrees > 158 AndAlso degrees <= 203 Then
            Return "S"
        ElseIf degrees > 203 AndAlso degrees <= 248 Then
            Return "SW"
        ElseIf degrees > 248 AndAlso degrees <= 293 Then
            Return "W"
        ElseIf degrees > 293 AndAlso degrees <= 338 Then
            Return "NW"
        Else
            Return "Error"
        End If
    End Function

    'Public Sub CheckMapVersion()
        'CheckMapVersion(False)
    'End Sub

    'Public Sub CheckMapVersion(ByVal IsUserCheck As Boolean)
        'NOTE: NEeds to be updated to use a service to get maps etc
        'Try
        '    Using ws As New ValidationServicePortTypeClient()
        '        'GEt the list of files from the server
        '        Dim serverFiles As New List(Of String)(ws.GetFileList())
        '        'Dim serverFilesEx As New List(Of WebService.FileEx)(ws.GetfileListEx())
        '        'Search for the directory containing the map.ini file
        '        'This is considered the maps directory
        '        Dim mapPath As String = String.Empty
        '        For Each file As String In IO.Directory.GetFiles(Application.StartupPath, "*.ini", IO.SearchOption.AllDirectories)
        '            If IO.Path.GetFileName(file) = "map.ini" Then
        '                mapPath = IO.Path.GetDirectoryName(file)
        '                Exit For
        '            End If
        '        Next
        '        'Check to see if we have found the maps directory
        '        If mapPath = String.Empty Then
        '            'The maps directory was not found, so we create the folder
        '            IO.Directory.CreateDirectory(Application.StartupPath & "\Maps")
        '            'Check with the user to see if they want to download the latest maps
        '            If MessageBox.Show("ApRadar was unable to find any maps, would you like to download the latest maps now?", "Download Maps?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '                'If they choose yes, open the file downloader and download the
        '                'latest map pack
        '                'We use the constructor for downloading the latest map pack
        '                Using fdd As New FileDownloadDialog(True)
        '                    If fdd.ShowDialog() = DialogResult.OK Then
        '                        'If all goes well with the downlaoder we will get the version info
        '                        'Grab the version info form the server
        '                        Dim vInfo As VersionInfo = ws.CheckMapVersion
        '                        'Split the result
        '                        My.Settings.MapPackVersion = vInfo.MapPackVersion
        '                        My.Settings.MapIniVersion = vInfo.MapIniVersion
        '                        My.Settings.Save()
        '                    End If
        '                End Using
        '            End If
        '        Else
        '            'Download the version info
        '            Dim vInfo As VersionInfo = ws.CheckMapVersion
        '            'Variable to tell if we have downloaded the entire version
        '            'This is used later to skip checking for any missing files
        '            Dim isFullPackdownloaded As Boolean = False
        '            'Check the map pack version
        '            If vInfo.MapPackVersion > My.Settings.MapPackVersion Then
        '                'If there is an updated map pack, we are going to download it
        '                'including the map ini
        '                If MessageBox.Show("There is an updated map pack available, would you like to download the latest map pack now?", "Download Maps?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '                    'If the user selects to download the map pack, we will 
        '                    'open the downlaod dialog and pass it the entire file list
        '                    Using fdd As New FileDownloadDialog(True)
        '                        isFullPackdownloaded = True
        '                        If fdd.ShowDialog() = DialogResult.OK Then
        '                            'If all went well with the file list, we update the version 
        '                            'info saved on the machine
        '                            My.Settings.MapPackVersion = vInfo.MapPackVersion
        '                            My.Settings.MapIniVersion = vInfo.MapIniVersion
        '                            My.Settings.Save()
        '                            'Flag that we have downloaded the full map pack
        '                            'so we can skip the file check below
        '                            isFullPackdownloaded = True
        '                        End If
        '                    End Using
        '                End If
        '            ElseIf vInfo.MapIniVersion > My.Settings.MapIniVersion Then
        '                'If a newer map pack was not found, we check the map.ini version
        '                'If we find a new version, prompt the user to download the new version
        '                If MessageBox.Show("There is an updated map.ini available, would you like to download it now?", "Download New map.ini?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '                    'Open up the downloader and download the latest map.ini
        '                    Using fdd As New FileDownloadDialog(New String() {"map.ini"}, mapPath, True)
        '                        If fdd.ShowDialog() = DialogResult.OK Then
        '                            'If all goes well with the downloader, we update the versions
        '                            'in our local settings
        '                            My.Settings.MapIniVersion = vInfo.MapIniVersion
        '                            My.Settings.MapPackVersion = vInfo.MapPackVersion
        '                            My.Settings.Save()
        '                        End If
        '                    End Using
        '                End If
        '            End If
        '            'Check to see if we have already downloaded the full map pack
        '            If Not isFullPackdownloaded Then
        '                'If not, we will build an update list for any files not on the local machine
        '                Dim updateList As New List(Of String)()
        '                'Loop through all the files int he server list and
        '                'check to see if they exist ont he local machine
        '                For Each file As String In serverFiles
        '                    If Not IO.File.Exists(String.Format("{0}\{1}", mapPath, file)) Then
        '                        'If the file doe snot exist we add it to the update list
        '                        updateList.Add(file)
        '                    End If
        '                Next
        '                'Check to see if there are any files found that need to be updated
        '                If updateList.Count > 0 Then
        '                    'If we have some, we prompt the user to see if they want to download them
        '                    If MessageBox.Show("There are more maps available for download, would you like to download them now?", "Download Maps?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '                        'Download the update files
        '                        Using fdd As New FileDownloadDialog(updateList.ToArray, mapPath, True)
        '                            fdd.ShowDialog()
        '                        End Using
        '                    End If
        '                ElseIf IsUserCheck Then
        '                    MessageBox.Show("No new updates found", "Update Check complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                End If
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    'End Sub

    Public Sub UpdateControlColors(ByVal f As Form)
        Dim ctl As Control = f.GetNextControl(f, True)
        Do Until ctl Is Nothing
            If Not TypeOf ctl Is Button AndAlso Not TypeOf ctl Is ComboBox AndAlso _
            Not TypeOf ctl Is TextBox AndAlso Not TypeOf ctl Is PictureBox AndAlso Not TypeOf ctl Is DataGridView _
            AndAlso Not TypeOf ctl Is Button AndAlso Not TypeOf ctl Is TreeView AndAlso Not TypeOf ctl Is RichTextBox _
            AndAlso Not TypeOf ctl Is NumericUpDown Then
                Try
                    If TypeOf ctl Is TabPage Then
                        ctl.BackColor = ThemeHandler.FormBackgroundColor
                    ElseIf TypeOf ctl Is TabControlEx Then
                        CType(ctl, TabControlEx).BackColor = ThemeHandler.FormBackgroundColor
                        CType(ctl, TabControlEx).HeaderBackColor = ThemeHandler.FormBackgroundColor
                        CType(ctl, TabControlEx).HeaderForeColor = ThemeHandler.FormForeColor
                    Else
                        'TODO: Check if control supports transparent bg
                        Dim fnc As MethodInfo = ctl.GetType().GetMethod("GetStyle", BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic)
                        If fnc.Invoke(ctl, New Object() {ControlStyles.SupportsTransparentBackColor}) Then
                            ctl.BackColor = Color.Transparent
                        Else
                            ctl.BackColor = ThemeHandler.FormBackgroundColor
                        End If
                    End If

                Catch ex As Exception
                    ctl.BackColor = ThemeHandler.FormBackgroundColor
                End Try
                If ctl.Tag <> "exclude" Then
                    ctl.ForeColor = ThemeHandler.FormForeColor
                End If
            End If
            If ctl.Tag = "h1" Then
                ctl.ForeColor = ThemeHandler.FormH1Color
            End If
            If TypeOf ctl Is DataGridView OrElse TypeOf ctl Is Button Then
                ctl.ForeColor = Color.Black
            End If
            ctl = f.GetNextControl(ctl, True)
        Loop
        f.Refresh()
    End Sub

    Public Function DeserializeXML(Of T)(ByVal xml As String) As T
        Using ms As New MemoryStream()
            Dim b As Byte() = System.Text.Encoding.Default.GetBytes(xml)
            ms.Write(b, 0, b.Length)
            ms.Position = 0
            Dim xs As New XmlSerializer(GetType(T))
            Return CType(xs.Deserialize(ms), T)
        End Using
    End Function

    Public Sub FlashWindow(ByVal hWnd As IntPtr)
        Dim fi As New FLASHWINFO() With {.cbSize = Marshal.SizeOf(GetType(FLASHWINFO)), .dwFlags = FLASHW_TRAY, .hwnd = hWnd, .uCount = 3, .dwTimeout = 1000}
        FlashWindowEx(fi)
    End Sub

    Public Sub ShowToastForm(ByVal Message As String)
        ShowToastForm(Message, False, Nothing, Nothing, String.Empty, 15, False)
    End Sub

    Public Sub ShowToastForm(ByVal Message As String, ByVal PlayAlert As Boolean)
        ShowToastForm(Message, False, Nothing, Nothing, String.Empty, 15, PlayAlert)
    End Sub

    Public Sub ShowToastForm(ByVal Message As String, ByVal HideTime As Integer, ByVal PlayAlert As Boolean)
        ShowToastForm(Message, False, Nothing, Nothing, String.Empty, HideTime, PlayAlert)
    End Sub

    Public Sub ShowToastForm(ByVal Message As String, ByVal Header As String, ByVal HideTime As Integer, ByVal PlayAlert As Boolean)
        ShowToastForm(Message, False, Nothing, Nothing, Header, HideTime, PlayAlert)
    End Sub

    Public Sub ShowToastForm(ByVal Message As String, ByVal ShowLink As Boolean, ByVal LinkText As String, ByVal LinkClickHandler As LinkLabelLinkClickedEventHandler, ByVal PlayAlert As Boolean)
        ShowToastForm(Message, ShowLink, LinkText, LinkClickHandler, 15, PlayAlert)
    End Sub

    Public Sub ShowToastForm(ByVal Message As String, ByVal ShowLink As Boolean, ByVal LinkText As String, ByVal LinkClickHandler As LinkLabelLinkClickedEventHandler, ByVal HideTime As Integer, ByVal PlayAlert As Boolean)
        ShowToastForm(Message, ShowLink, LinkText, LinkClickHandler, String.Empty, HideTime, PlayAlert)
    End Sub

    Public Sub ShowToastForm(ByVal Message As String, ByVal ShowLink As Boolean, ByVal LinkText As String, ByVal LinkClickHandler As LinkLabelLinkClickedEventHandler, ByVal Header As String, ByVal HideTime As Integer, ByVal PlayAlert As Boolean)
        Dim tf As New ToastForm(PlayAlert)
        tf.lblMessage.Text = Message
        If HideTime = 0 Then
            tf.Persistent = True
        Else
            tf.hideTimer.Interval = HideTime * 1000
        End If
        If Header <> String.Empty Then
            tf.lblHeder.Text = Header
        End If
        If ShowLink Then
            tf.lnkSomeLink.Visible = True
            tf.lnkSomeLink.Text = LinkText
            If LinkClickHandler IsNot Nothing Then
                AddHandler tf.lnkSomeLink.LinkClicked, LinkClickHandler
            End If
        End If
        tf.Location = New Point(Screen.PrimaryScreen.WorkingArea.Right - tf.Width, Screen.PrimaryScreen.WorkingArea.Bottom - tf.Height)
        tf.Show()
        For Each ctf As ToastForm In _toastForms
            ctf.Top -= tf.Height
        Next
        _toastForms.Add(tf)
    End Sub

    Public Sub RemoveToastForm(ByVal tf As ToastForm)
        _toastForms.Remove(tf)
        For Each ctf As ToastForm In _toastForms
            If ctf.Top < tf.Top Then
                ctf.Top += tf.Height
            End If
        Next
    End Sub

    Public Function Click_Button(ByVal ButtonObject As Object)
        ButtonObject.PerformClick()
        Return Nothing
    End Function

    Public Function GetSound(ByVal name As String) As System.Media.SystemSound
        Select Case name
            Case "Asterik"
                Return Media.SystemSounds.Asterisk
            Case "Beep"
                Return Media.SystemSounds.Beep
            Case "Exclamation"
                Return Media.SystemSounds.Exclamation
            Case "Hand"
                Return Media.SystemSounds.Hand
            Case "Question"
                Return Media.SystemSounds.Question
            Case Else
                Return Media.SystemSounds.Exclamation
        End Select
    End Function

    Private Function GetResourceData(ByVal Resource As String) As String
        Dim Asm As [Assembly] = [Assembly].GetExecutingAssembly()

        ' Resources are named using a fully qualified name.
        Dim strm As Stream = Asm.GetManifestResourceStream( _
        Asm.GetName().Name + "." + Resource)

        ' Reads the contents of the embedded file.
        Dim reader As StreamReader = New StreamReader(strm)
        Dim contents As String = reader.ReadToEnd()
        reader.Close()
        reader.Dispose()
        Return contents
    End Function

    Public Function FormatSize(ByVal size As Long) As String
        If size < 1024 Then
            Return String.Format("{0} B", size)
        ElseIf size < 1048576 Then
            Return String.Format("{0:0.0} kB", size / 1024)
        ElseIf size < 1073741824 Then
            Return String.Format("{0:0.0} MB", size / 1048576)
        Else
            Return String.Format("{0:0.0} GB", size / 1073741824)
        End If
    End Function
#End Region

#Region " PRIVATE METHODS "
    

    ''' <summary>
    ''' Validates an elitemmo user agianst the elitemmo web service
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateUser() As Boolean

        Return True
    End Function
#End Region
End Module
