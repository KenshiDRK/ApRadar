
Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Public Delegate Sub DisposeDelegate()

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            If NeedsUpdate Then
                Process.Start("http://services.apradar.com/files/ApRadar3Update.exe")
            End If
        End Sub

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            If My.Settings.UpgradeRequired Then
                My.Settings.Upgrade()
                My.Settings.UpgradeRequired = False
                My.Settings.Save()
            End If

            'Load the current theme
            CType(SplashScreen, ARSplash).Message = "Loading theme..."
            ThemeHandler.LoadTheme(My.Settings.CurrentTheme)
            GlobalSettings.IsProEnabled = True

            CType(SplashScreen, ARSplash).Message = "Launching ApRadar..."
            'Last thing, lets check if we should start in nobar mode
            If e.CommandLine.Count > 0 Then
                For Each arg As String In e.CommandLine
                    If arg = "/nobar" Then
                        NoBar = True
                        Application.IsSingleInstance = False
                    ElseIf arg = "/d" OrElse arg = "/debug" Then
                        DebugRun = True
                    End If
                Next
            End If
        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Using ued As New UnhandledErrorDialog(e.Exception)
                ued.ShowDialog()
            End Using
        End Sub
    End Class
End Namespace

