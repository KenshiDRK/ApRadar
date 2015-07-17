Imports DataLibrary
Imports DataLibrary.ApRadarDataSet
Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports DataLibrary.CampedMobsDataset
Imports FFXIMemory
Imports System.Drawing
Public Class DrawingHelpers
    Public Shared Sub DrawOutlineString(ByVal g As Graphics, ByVal s As String, ByVal font As Font, ByVal brush As System.Drawing.Brush, ByVal x As Single, ByVal y As Single, ByVal size As Integer)
        Dim tan As Single = size / 2
        For bx = 0 To size
            For by = 0 To size
                g.DrawString(s, font, brush, CSng(x - tan + bx), CSng(y - tan + by))
            Next
        Next
    End Sub
End Class
