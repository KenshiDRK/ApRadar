Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Reflection

Public Class AudioManager

    <Flags()> _
    Public Enum SoundFlags As UInteger
        ''' <summary>
        ''' The sound is played synchronously, and PlaySound returns after 
        ''' the sound event completes. This is the default behavior.
        ''' </summary>
        SND_SYNC = &H0

        ''' <summary>
        ''' The sound is played asynchronously and PlaySound returns 
        ''' immediately after beginning the sound. To terminate an 
        ''' asynchronously played waveform sound, call PlaySound with 
        ''' pszSound set to NULL.
        ''' </summary>
        SND_ASYNC = &H1

        ''' <summary>
        ''' No default sound event is used. If the sound cannot be found, 
        ''' PlaySound returns silently without playing the default sound.
        ''' </summary>
        SND_NODEFAULT = &H2

        ''' <summary>
        ''' The pszSound parameter points to a sound loaded in memory.
        ''' </summary>
        SND_MEMORY = &H4

        ''' <summary>
        ''' The sound plays repeatedly until PlaySound is called again 
        ''' with the pszSound parameter set to NULL. If this flag is 
        ''' set, you must also set the SND_ASYNC flag.
        ''' </summary>
        SND_LOOP = &H8

        ''' <summary>
        ''' The specified sound event will yield to another sound event 
        ''' that is already playing. If a sound cannot be played because 
        ''' the resource needed to generate that sound is busy playing 
        ''' another sound, the function immediately returns False without 
        ''' playing the requested sound.
        ''' </summary>
        ''' <remarks>If this flag is not specified, PlaySound attempts 
        ''' to stop the currently playing sound so that the device can 
        ''' be used to play the new sound.
        ''' </remarks>
        SND_NOSTOP = &H10

        ''' <summary>
        ''' Stop playing wave
        ''' </summary>
        SND_PURGE = &H40

        ''' <summary>
        ''' If the driver is busy, return immediately without playing 
        ''' the sound.
        ''' </summary>
        SND_NOWAIT = &H2000

        ''' <summary>
        ''' The pszSound parameter is a system-event alias in the 
        ''' registry or the WIN.INI file. Do not use with either 
        ''' SND_FILENAME or SND_RESOURCE.
        ''' </summary>
        SND_ALIAS = &H10000

        ''' <summary>
        ''' The pszSound parameter is a file name. If the file cannot be 
        ''' found, the function plays the default sound unless the 
        ''' SND_NODEFAULT flag is set.
        ''' </summary>
        SND_FILENAME = &H20000

        ''' <summary>
        ''' The pszSound parameter is a resource identifier; hmod must 
        ''' identify the instance that contains the resource.
        ''' </summary>
        SND_RESOURCE = &H40004
    End Enum

    Private Shared _systemSounds As String() = {"Asterik", "Beep", "Exclamation", "Hand"}


    <DllImport("Winmm.dll")>
    Private Shared Function PlaySound(ByVal sound As String, ByVal hMod As IntPtr, ByVal dwFlags As UInt32) As Boolean
    End Function

    <DllImport("Winmm.dll")>
    Private Shared Function PlaySound(ByVal data As Byte(), ByVal hMod As IntPtr, ByVal dwFlags As UInt32) As Boolean
    End Function

    Public Shared Sub PlayAlert(ByVal sound As String)
        If _systemSounds.Contains(sound) Then
            PlaySystemSound(sound)
        Else
            PlaySound(sound)
        End If
    End Sub

    Public Shared Sub PlaySound(ByVal Sound As String)
        PlaySound(Sound, IntPtr.Zero, SoundFlags.SND_FILENAME Or SoundFlags.SND_ASYNC)
    End Sub

    Public Shared Sub PlaySystemSound(ByVal Sound As String)
        Select Case Sound
            Case "Asterik"
                System.Media.SystemSounds.Asterisk.Play()
            Case "Beep"
                System.Media.SystemSounds.Beep.Play()
            Case "Exclamation"
                System.Media.SystemSounds.Exclamation.Play()
            Case "Hand"
                System.Media.SystemSounds.Hand.Play()
            Case Else

        End Select
    End Sub

    Public Shared Sub PlaySoundResource(ByVal Wav As String)
        Dim ns As String = [Assembly].GetExecutingAssembly().GetName().Name.ToString()
        Dim str As Stream = [Assembly].GetExecutingAssembly.GetManifestResourceStream(String.Format("{0}.{1}", ns, Wav))
        If str Is Nothing Then Return

        Dim data(str.Length) As Byte
        str.Read(data, 0, str.Length)
        str.Close()
        PlaySound(data, IntPtr.Zero, SoundFlags.SND_ASYNC Or SoundFlags.SND_MEMORY)
    End Sub
End Class
