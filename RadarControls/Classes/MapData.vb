Imports System.Drawing

Public Class MapData
#Region " PROPERTIES "
    Private _map As Short = 0
    Public Property Map() As Short
        Get
            Return _map
        End Get
        Set(ByVal value As Short)
            _map = value
        End Set
    End Property

    Private _level As Byte = 0
    Public Property Level() As Byte
        Get
            Return _level
        End Get
        Set(ByVal value As Byte)
            _level = value
        End Set
    End Property

    Private _boxes As List(Of Box)
    Public ReadOnly Property Boxes() As List(Of Box)
        Get
            If _boxes Is Nothing Then
                _boxes = New List(Of Box)
            End If
            Return _boxes
        End Get
    End Property

    Private _iniData As IniData
    Public Property IniData() As IniData
        Get
            If _iniData Is Nothing Then
                _iniData = New IniData()
            End If
            Return _iniData
        End Get
        Set(ByVal value As IniData)
            _iniData = value
        End Set
    End Property
#End Region

#Region " CONSTRUCTORS "

#End Region

#Region " PUBLIC METHODS "
    Public Function ConvertPosTo2D(ByVal curX As Double, ByVal curY As Double) As PointF
        'Dim x, y As Single
        'x = Me.IniData.XModifier + (Me.IniData.XScale * Math.Round(curX, 3))
        'y = Me.IniData.YModifier + (Me.IniData.YScale * Math.Round(curY, 3))
        'Return New PointF(x, y)

        Return New PointF(
            (Me.IniData.XModifier + (Me.IniData.XScale * curX)),
            (Me.IniData.YModifier + (Me.IniData.YScale * curY)))

    End Function

    Public Function Convert2DToPos(ByVal point As PointF) As PointF
        Return New PointF(
            ((point.X - Me.IniData.XModifier) / Me.IniData.XScale),
            ((point.Y - Me.IniData.YModifier) / Me.IniData.YScale))
    End Function

    Function ConvertPosToRelative(ByVal curX As Single, ByVal curY As Single) As String
        'Y=0.0003906250058207661
        Return Chr(65 + Math.Floor(CInt(IniData.XModifier + (IniData.XScale * curX) - 8) / 16)) & _
               "-" & _
               Math.Floor(CInt(IniData.YModifier + (IniData.YScale * curY) + 7) / 16)
    End Function
#End Region
End Class
