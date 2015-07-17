Imports System.Net.Mail
Imports System.Net

Public Class MailHandler
    Public Shared Sub SendMessage(ByVal SendTo As String, ByVal SentFrom As String, ByVal subject As String, ByVal Body As String)
        Dim client As New SmtpClient("") With {.UseDefaultCredentials = False, .Credentials = New NetworkCredential("", ""), .EnableSsl = True}
        Using mm As New MailMessage() With {.Subject = subject}
            mm.To.Add(New MailAddress(SendTo))
            mm.From = New MailAddress(SentFrom)
            mm.Body = Body
            client.Send(mm)
        End Using
    End Sub
End Class
