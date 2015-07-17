Imports System.Xml.Serialization
Imports System.ComponentModel
Public Class Range
    Public Sub New()
    End Sub

    Public Sub New(ByVal Size As Integer, ByVal RangeColor As System.Drawing.Color)
        _size = Size
        _color = RangeColor
    End Sub

    Private _size As Single
    <Browsable(True)> _
    Public Property Size() As Single
        Get
            Return _size
        End Get
        Set(ByVal value As Single)
            _size = value
        End Set
    End Property

    <Browsable(False)> _
    Public Property RangeColorHtml() As String
        Get
            Return System.Drawing.ColorTranslator.ToHtml(_color)
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                _color = System.Drawing.ColorTranslator.FromHtml(value)
            End If
        End Set
    End Property

    Private _color As System.Drawing.Color
    <XmlIgnore(), Browsable(True)> _
    Public Property RangeColor() As System.Drawing.Color
        Get
            Return _color
        End Get
        Set(ByVal value As System.Drawing.Color)
            _color = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return String.Format("{0} [{1}]", Me.Size, Me.RangeColor.ToString)
    End Function
End Class
