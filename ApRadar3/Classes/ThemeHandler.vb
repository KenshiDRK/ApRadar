Public Class ThemeHandler
    Public Shared Property ActiveTheme() As Theme

    Public Shared Sub LoadTheme(ByVal Path As String)
        If Path = String.Empty Then
            Path = Application.StartupPath & "\Themes\Black Pearl\"
        End If
        If Not Path.EndsWith("\") Then
            Path &= "\"
        End If
        If Not IO.Directory.Exists(Path) Then
            Path = Application.StartupPath & "\Themes\Black Pearl\"
        End If
        Try
            ThemeHandler.ActiveTheme = Serializer.DeserializeFromXML(Of Theme)(Path & "theme.xml")
            My.Settings.CurrentTheme = Path
            My.Settings.Save()
        Catch ex As Exception
            Dim t As New Theme() With {.BarForeColor = "White", .FormForeColor = "White", .FormBackgroundColor = "#27282C", .NPCColor = "Tomato", .PCColor = "LightSteelBlue"}
            ThemeHandler.ActiveTheme = t
        End Try
    End Sub

    Public Shared Sub ApplyTheme(ByVal f As Form)
        Try
            If TypeOf f Is AppBarForm Then
                f.BackgroundImage = ThemeHandler.BarImage
                f.ForeColor = ThemeHandler.BarForeColor
            Else
                f.ForeColor = ThemeHandler.FormForeColor
                f.BackColor = ThemeHandler.FormBackgroundColor
                f.BackgroundImage = ThemeHandler.FormBGImage
                f.BackgroundImageLayout = ThemeHandler.FormBGImageMode

            End If
            Dim ctl As Control = f.GetNextControl(f, True)
            Do Until ctl Is Nothing
                If TypeOf ctl Is Panel AndAlso ctl.Name = "HeaderPanel" Then
                    ctl.BackgroundImage = ThemeHandler.HeaderImage
                    Exit Do
                End If
                ctl = f.GetNextControl(ctl, True)
            Loop
            UpdateControlColors(f)
        Catch ex As Exception
            'The theme was not able to be applied
        End Try
    End Sub

    Public Shared Function BarImage() As Image
        Try
            If Not ActiveTheme Is Nothing AndAlso ActiveTheme.BarBackground <> String.Empty Then
                Return Image.FromFile(My.Settings.CurrentTheme & ActiveTheme.BarBackground)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function HeaderImage() As Image
        Try
            If Not ActiveTheme Is Nothing AndAlso ActiveTheme.HeaderBar <> String.Empty Then
                Return Image.FromFile(My.Settings.CurrentTheme & ActiveTheme.HeaderBar)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function GetImage(ByVal Path As String) As Image
        Try
            If IO.File.Exists(Path) Then
                Return Image.FromFile(Path)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Shared Function GetColorFromHTML(ByVal HTMLColor As String) As Color
        Return ColorTranslator.FromHtml(HTMLColor)
    End Function

    Public Shared ReadOnly Property NPCColor() As Color
        Get
            Return GetColorFromHTML(ActiveTheme.NPCColor)
        End Get
    End Property

    Public Shared ReadOnly Property PCColor() As Color
        Get
            Return GetColorFromHTML(ActiveTheme.PCColor)
        End Get
    End Property

    Public Shared ReadOnly Property FormBackgroundColor() As Color
        Get
            Return GetColorFromHTML(ActiveTheme.FormBackgroundColor)
        End Get
    End Property

    Public Shared ReadOnly Property FormForeColor() As Color
        Get
            Return GetColorFromHTML(ActiveTheme.FormForeColor)
        End Get
    End Property

    Public Shared ReadOnly Property FormBGImage() As Image
        Get
            If ActiveTheme.FormBGImage <> String.Empty AndAlso IO.File.Exists(My.Settings.CurrentTheme & ActiveTheme.FormBGImage) Then
                Return Image.FromFile(My.Settings.CurrentTheme & ActiveTheme.FormBGImage)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Shared ReadOnly Property FormBGImageMode() As ImageLayout
        Get
            Try
                Return ActiveTheme.FormBGImageMode
            Catch ex As Exception
                Return ImageLayout.Stretch
            End Try
        End Get
    End Property

    Public Shared ReadOnly Property FormH1Color() As Color
        Get
            If ActiveTheme.FormH1Color <> String.Empty Then
                Return GetColorFromHTML(ActiveTheme.FormH1Color)
            Else
                Return Color.Yellow
            End If
        End Get
    End Property

    Public Shared ReadOnly Property BarForeColor() As Color
        Get
            Return GetColorFromHTML(ActiveTheme.BarForeColor)
        End Get
    End Property


End Class
