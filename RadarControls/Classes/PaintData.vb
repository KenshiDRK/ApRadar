Imports System.Drawing
Public Class RadarPaintData
    Private _centerPoint As PointF
    Public Property CenterPoint() As PointF
        Get
            Return _centerPoint
        End Get
        Set(ByVal value As PointF)
            _centerPoint = value
        End Set
    End Property

    Private _xScale As Single
    Public Property XScale() As Single
        Get
            Return _xScale
        End Get
        Set(ByVal value As Single)
            _xScale = value
        End Set
    End Property

    Private _yScale As Single
    Public Property YScale() As Single
        Get
            Return _yScale
        End Get
        Set(ByVal value As Single)
            _yScale = value
        End Set
    End Property

    Private _mapScaleX As Single
    Public Property MapScaleX() As Single
        Get
            Return _mapScaleX
        End Get
        Set(ByVal value As Single)
            _mapScaleX = value
        End Set
    End Property

    Private _mapScaleY As Single
    Public Property MapScaleY() As Single
        Get
            Return _mapScaleY
        End Get
        Set(ByVal value As Single)
            _mapScaleY = value
        End Set
    End Property

    Private _myPosition As PointF
    Public Property MyPosition() As PointF
        Get
            Return _myPosition
        End Get
        Set(ByVal value As PointF)
            _myPosition = value
        End Set
    End Property

    Private _myScaledPosition As PointF
    Public Property MyScaledPosition() As PointF
        Get
            Return _myScaledPosition
        End Get
        Set(ByVal value As PointF)
            _myScaledPosition = value
        End Set
    End Property

    Private _mapW As Single
    Public Property MapW() As Single
        Get
            Return _mapW
        End Get
        Set(ByVal value As Single)
            _mapW = value
        End Set
    End Property

    Private _mapH As Single
    Public Property MapH() As Single
        Get
            Return _mapH
        End Get
        Set(ByVal value As Single)
            _mapH = value
        End Set
    End Property

    Public ReadOnly Property NinetyDegrees() As Single
        Get
            Return CSng(-90 * Math.PI / 180)
        End Get
    End Property

    Public Property XShift As Single

    Public Property YShift As Single
End Class
