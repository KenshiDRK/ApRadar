Imports VaultClientIntegrationLib
Public Class UnhandledErrorDialog
    Inherits ResizableForm
    Private _ex As Exception

    Public Sub New(ByVal ex As Exception)
        InitializeComponent()
        _ex = ex
        Me.lblMessage.Text = String.Format(Me.lblMessage.Text, ex.Message)
    End Sub

    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        Try
            Dim message As String = "An unhandled system error has occured in ApRadar v" & My.Application.Info.Version.ToString & ControlChars.NewLine & _
                                   _ex.StackTrace & ControlChars.NewLine & ControlChars.NewLine & _
                                   _ex.Message & ControlChars.NewLine
            For i As Integer = 0 To _ex.Data.Count - 1
                message &= (_ex.Data.Item(i) & ControlChars.NewLine)
            Next
            message &= (ControlChars.NewLine & "System Information:" & ControlChars.NewLine)
            message &= ("OS: " & My.Computer.Info.OSFullName & ControlChars.NewLine)
            message &= ("OS Platform: " & My.Computer.Info.OSPlatform & ControlChars.NewLine)
            message &= ("OS Version: " & My.Computer.Info.OSVersion & ControlChars.NewLine)
            message &= ("Physical Memory: " & My.Computer.Info.TotalPhysicalMemory & " Available: " & My.Computer.Info.AvailablePhysicalMemory & ControlChars.NewLine)
            message &= ("Virtual Memory: " & My.Computer.Info.TotalVirtualMemory & " Available: " & My.Computer.Info.AvailableVirtualMemory & ControlChars.NewLine)
            MailHandler.SendMessage("SENDTOEMAIL", "ApRadar Errors <noreply@apradar.com>", "ApRadar 3 Error", message)
            If ckCreateBugReport.Checked Then
                Dim item As New FortressItemExpanded
                item.Assignee = ""
                item.Description = txtDescription.Text
                item.Details = message
                item.ItemType = "Bug"
                item.Category = cboCategory.Text
                item.ProjectName = "ApRadar 3"
                item.Platform = "Windows"
                item.Status = "Open"
                If GlobalSettings.IsProEnabled Then
                    item.Custom1 = "Reported By: " & GlobalSettings.LicenseUser
                Else
                    item.Custom1 = "Reported By: ApRadar User"
                End If
                item.VersionStr = My.Application.Info.Version.ToString
                item.Validate()
                ItemTrackingOperations.ProcessCommandAddFortressItem(item)
            End If
        Catch ex As Exception

        End Try
        Me.Dispose()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Dispose()
    End Sub

    Private Sub UnhandledErrorDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ThemeHandler.ApplyTheme(Me)
        ServerOperations.client.LoginOptions.URL = ""
        ServerOperations.client.LoginOptions.User = ""
        ServerOperations.client.LoginOptions.Password = ""
        ServerOperations.Login()
        Dim cats As MantisLib.MantisCategory() = ItemTrackingOperations.ProcessCommandListFortressCategories("")
        Me.cboCategory.DataSource = cats
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ckCreateBugReport.CheckedChanged
        Me.txtDescription.Enabled = ckCreateBugReport.Checked
        Me.cboCategory.Enabled = ckCreateBugReport.Checked
    End Sub
End Class