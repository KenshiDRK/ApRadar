Public Class Windower
    'Text Helper: (MMF Name = "WindowerMMFTextHandler")

    Public Declare Function CreateTextHelper Lib "WindowerHelper.dll" (ByVal name As String) As Integer
    Public Declare Sub DeleteTextHelper Lib "WindowerHelper.dll" (ByVal helper As Integer)

    Public Declare Sub CTHCreateTextObject Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String)
    Public Declare Sub CTHDeleteTextObject Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String)

    Public Declare Sub CTHSetText Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal text As String)
    Public Declare Sub CTHSetVisibility Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal visible As Boolean)
    Public Declare Sub CTHSetFont Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByRef font As Byte, ByVal height As Short)
    Public Declare Sub CTHSetColor Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal a As Byte, ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)
    Public Declare Sub CTHSetLocation Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal x As Single, ByVal y As Single)
    Public Declare Sub CTHSetBold Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal bold As Boolean)
    Public Declare Sub CTHSetItalic Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal italic As Boolean)
    Public Declare Sub CTHSetBGColor Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal a As Byte, ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)
    Public Declare Sub CTHSetBGBorderSize Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal pixels As Single)
    Public Declare Sub CTHSetBGVisibility Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal visible As Boolean)
    Public Declare Sub CTHSetRightJustified Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal name As String, ByVal justified As Boolean)

    Public Declare Sub CTHFlushCommands Lib "WindowerHelper.dll" (ByVal helper As Integer)

    'Keyboard Helper: (MMF Name = "WindowerMMFKeyboardHandler")

    Public Declare Function CreateKeyboardHelper Lib "WindowerHelper.dll" (ByVal name As String) As Integer
    Public Declare Sub DeleteKeyboardHelper Lib "WindowerHelper.dll" (ByVal helper As Integer)

    Public Declare Sub CKHSetKey Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal key As Byte, ByVal down As Boolean)
    Public Declare Sub CKHBlockInput Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal block As Boolean)
    Public Declare Sub CKHSendString Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal data As String)

    'Console Helper: (MMF Name = "WindowerMMFConsoleHandler")

    Public Declare Function CreateConsoleHelper Lib "WindowerHelper.dll" (ByVal name As String) As Integer
    Public Declare Sub DeleteConsoleHelper Lib "WindowerHelper.dll" (ByVal helper As Integer)

    Public Declare Function CCHIsNewCommand Lib "WindowerHelper.dll" (ByVal helper As Integer) As Boolean
    Public Declare Function CCHGetArgCount Lib "WindowerHelper.dll" (ByVal helper As Integer) As Short
    Public Declare Sub CCHGetArg Lib "WindowerHelper.dll" (ByVal helper As Integer, ByVal index As Short, ByVal buffer As String)

End Class
