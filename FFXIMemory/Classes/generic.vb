Public Class generic

    Public Shared FFXiMain As ProcessModule
    Public Shared pol As Process

    Shared Sub New()
        generic.pol = Memory.findProcess("pol")
        generic.FFXiMain = Memory.GetModule("FFXiMain.dll", generic.pol)
    End Sub

End Class

