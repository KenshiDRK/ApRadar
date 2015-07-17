Module General
    Friend OverlaySettingsPath As String = My.Application.Info.DirectoryPath & "\Settings\OverlaySettings.xml"
    Friend MappedSettingsPath As String = My.Application.Info.DirectoryPath & "\Settings\MappedSettings.xml"
    Friend DataPath As String = My.Application.Info.DirectoryPath & "\Data\"
    Friend IsProEnabled As Boolean = True

    Private Sub OutputDebug(ByVal Message As String)
        Debug.Print(Message)
    End Sub
End Module
