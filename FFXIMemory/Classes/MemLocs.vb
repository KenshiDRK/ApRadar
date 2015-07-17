Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class MemLocs
    Inherits Hashtable

    Public locs As Hashtable
    Private pol As Process


    'Public Sub New()
    '    pol = Nothing
    '    locs = New Hashtable
    '    Me.Add("INITCODE", FindByteString("C6-41-07-04-8B-51"))
    '    locs.Add("TARGETINFO", FindMemloc("8B-0D", "8B-41-50"))
    '    locs.Add("MAPBASE", FindMemloc("8B-15", "8D-44-3E-20"))
    '    locs.Add("NPCMAP", FindMemloc("04-85", "85-C0-0F-84"))
    '    locs.Add("PCMAP", FindMemloc("04-85", "85-C0-0F-84") + &HC00)
    '    Dim ownPos As Integer = FindByteString("66-C7-44-24-10-79-00-50") + 37
    '    ownPos = New Memory(pol, ownPos).GetInt32() + 4
    '    locs.Add("OWNPOSITION", ownPos)
    '    locs.Add("PLAYERINFO", FindMemloc("8B-0D", "8B-50-20-51"))

    '    locs.Add("MYCHARID", FindMemloc("89-0D", "8B-50-04-89"))
    '    locs.Add("CAMPAIGNINFO", FindByteString("56-8B-74-24-08-57-B9-2C-00-00-00-BF"))
    '    Dim invPointer As Integer = FindMemloc("8B-0D", "6A-01-8B-91")
    '    Dim equipPointer As Integer = New Memory(pol, invPointer - 4).GetInt32 + &HAEC8
    '    invPointer = New Memory(pol, invPointer).GetInt32 + &H6200
    '    locs.Add("EQUIPMENTINFO", equipPointer)
    '    locs.Add("INVENTORY", invPointer)
    '    locs.Add("ALLIANCEPOINTER", FindMemloc("8D-04-C5", "8D-78-04-C7"))
    '    locs.Add("CHATBASE", FindMemloc("8B-0D", "85-C9-74-0F"))
    '    locs.Add("MENUTEXT", FindMemloc("8B-0D", "85-C9-74-15"))
    'End Sub

    Public Sub New(ByVal POL As Process)
        Me.pol = POL
        locs = New Hashtable
        locs.Add("INITCODE", FindByteString("C6-41-07-04-8B-51"))
        locs.Add("TARGETINFO", FindByteString("53-56-8B-F1-8B-48-04-33-DB-3B-CB-75-06-5E-33-C0-5B-59-C3-8B-0D") + 45)
        'FindMemloc("8B-0D", "8B-41-50"))
        'locs.Add("MAPBASE", FindMemloc("8B-15", "8D-44-3E-20"))
        locs.Add("MAPBASE", FindMemloc("89-1D", "8B-CE-89-9E"))
        locs.Add("NPCMAP", FindMemloc("04-85", "85-C0-0F-84"))
        locs.Add("PCMAP", FindMemloc("04-85", "85-C0-0F-84") + &HC00)
        locs.Add("PLAYERINFO", FindMemloc("8B-0D", "8B-50-20-51"))
        locs.Add("VNMINFO", FindMemloc("89-0D", "8B-86-00-07") + &H100)
        'locs.Add("OWNPOSITION", FindByteString("66-C7-44-24-10-79-00-50") + 29)
        Dim ownPos As Integer = FindByteString("66-C7-44-24-10-79-00-50") + 37
        ownPos = New Memory(POL, ownPos).GetInt32() + 4
        locs.Add("OWNPOSITION", ownPos)
        locs.Add("CHARACTERID", FindMemloc("C3-B9", "E8-72-D2-ED"))
        locs.Add("CAMPAIGNINFO", FindByteString("56-8B-74-24-08-57-B9-2C-00-00-00-BF"))
        Dim invPointer As Integer = FindMemloc("8B-0D", "6A-01-8B-91")
        Dim equipPointer As Integer = New Memory(POL, invPointer - 4).GetInt32 + &HAEC8
        invPointer = New Memory(POL, invPointer).GetInt32 + &H6200
        locs.Add("EQUIPMENTINFO", equipPointer)
        locs.Add("INVENTORY", invPointer)
        Dim allyPointer As Integer = FindByteString("0F-BE-C3-8D-0C-52-56-57-8B-F5-8D-04-48") + &H16
        locs.Add("ALLIANCEPOINTER", New Memory(POL, allyPointer).GetInt32()) 'FindMemloc("89-3D", "EB-06-89-2D"))
        locs.Add("CHATBASE", FindMemloc("8B-0D", "85-C9-74-0F"))
        locs.Add("MENUTEXT", FindMemloc("8B-0D", "85-C9-74-15"))
    End Sub

    Private Function ScanSignature(ByVal Sig As String, ByVal Offset As Integer) As Integer
        Dim tSig As String = ""
        For i = 0 To Sig.Length - 1 Step 2
            tSig &= Sig.Substring(i, 2).ToUpper() & "-"
        Next

        tSig = tSig.TrimEnd("-")

        Dim sigAddress As Integer = FindByteString(tSig) + (Sig.Length / 2) + Offset
        Return New Memory(pol, sigAddress).GetInt32()
    End Function


    Private Function FindByteString(ByVal search As String) As Integer
        Dim Address As Integer
        Try
            Dim match1 As Match
            Dim regex1 As New Regex(search)
            Dim StartAddress As Integer = 229376

            Dim Base As Integer
            If Not pol Is Nothing Then
                Base = Memory.GetModule("FFXiMain.dll", pol).BaseAddress

                Dim memory1 As New Memory(pol, Base)
                Dim num2 As Integer = Memory.GetModuleEndAddress("FFXiMain.dll", pol)
                Do
                    Dim Pattern As String = memory1.GetOpcode(StartAddress)
                    match1 = regex1.Match(Pattern)
                    If match1.Success Then
                        Exit Do
                    End If
                    memory1.Address = (memory1.Address + StartAddress)

                Loop While (Not match1.Success And (memory1.Address < num2))
                Address = ((match1.Index / 3) + memory1.Address)
            Else
                Address = 0
            End If
        Catch ex As Exception
            Address = 0
        End Try
        Return Address
    End Function

    Public Function FindMemloc(ByVal strPrefix As String, ByVal strSuffix As String) As Integer
        Dim m As Match
        Dim Address As Integer
        Dim regex1 As New Regex((strPrefix & "-([0-9|A-F][0-9|A-F])-([0-9|A-F][0-9|A-F])-([0-9|A-F][0-9|A-F])-([0-9|A-F][0-9|A-F])-" & strSuffix))
        Dim blockSize As Integer = 229376

        Dim Base As Integer
        If pol Is Nothing Then
            Return 0
        Else
            Base = Memory.GetModule("FFXiMain.dll", pol).BaseAddress
        End If
        Dim endAddress As Integer = Memory.GetModuleEndAddress("FFXiMain.dll", pol)
        Dim mem As New Memory(pol, Base)
        Do
            Dim text1 As String = mem.GetOpcode(blockSize)
            m = regex1.Match(text1)
            mem.Address = (mem.Address + blockSize)
        Loop While (Not m.Success And (mem.Address < endAddress))
        Try
            Address = Int32.Parse((m.Groups.Item(4).ToString & m.Groups.Item(3).ToString & m.Groups.Item(2).ToString & m.Groups.Item(1).ToString), NumberStyles.HexNumber)
        Catch ex As Exception
            Address = 0
        End Try
        Return Address
    End Function
End Class
