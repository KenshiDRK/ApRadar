Imports RadarControls

Public NotInheritable Class ARSplash
    Inherits LayeredForm
    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).

    Private _message As String = "Loading..."
    Public WriteOnly Property Message As String
        Set(value As String)
            _message = value
            Me.Invalidate()
        End Set
    End Property

    Private Sub ARSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ARSplash_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim c1 As Color = Color.Black '
        Dim c2 As Color = Color.FromArgb(168, 207, 57) ' Color.FromArgb(163, 45, 130)
        e.Graphics.DrawImage(My.Resources.ApRadar2, 0, 0)

        Using sFont As New Font(Me.Font.Name, 8, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point)
            DrawOutlineString(e.Graphics, sFont, "Version: " & My.Application.Info.Version.ToString(), c2, 5, 112, 5)
            e.Graphics.DrawString("Version " & My.Application.Info.Version.ToString, sFont, New SolidBrush(c1), New Point(5, 112))
        End Using
        DrawOutlineString(e.Graphics, Me.Font, _message, c2, 5, 126, 5)
        e.Graphics.DrawString(_message, Me.Font, New SolidBrush(c1), New Point(5, 126))



        '
        'e.Graphics.DrawString(_message, Me.Font, New SolidBrush(c1), New Point(5, 142))

    End Sub

    Private Sub DrawOutlineString(ByVal g As Graphics, ByVal sFont As Font, ByVal s As String, ByVal c As Color, ByVal x As Single, ByVal y As Single, ByVal size As Integer)
        Dim tan As Single = size / 2
        Using b As New SolidBrush(Color.FromArgb(15, c))
            For bx As Integer = 0 To size
                For by As Integer = 0 To size
                    g.DrawString(s, sFont, b, x - tan + bx, y - tan + by)
                Next
            Next
        End Using
    End Sub
End Class
