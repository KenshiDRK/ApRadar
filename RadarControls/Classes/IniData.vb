Public Class IniData
#Region " PROPERTIES "
    Private _xScale As Double
    Public Property XScale() As Double
        Get
            Return _xScale
        End Get
        Set(ByVal value As Double)
            _xScale = value
        End Set
    End Property

    Private _xModifier As Double
    Public Property XModifier() As Double
        Get
            Return _xModifier
        End Get
        Set(ByVal value As Double)
            _xModifier = value
        End Set
    End Property

    Private _yScale As Double
    Public Property YScale() As Double
        Get
            Return _yScale
        End Get
        Set(ByVal value As Double)
            _yScale = value
        End Set
    End Property

    Private _yModifer As Double
    Public Property YModifier() As Double
        Get
            Return _yModifer
        End Get
        Set(ByVal value As Double)
            _yModifer = value
        End Set
    End Property
#End Region

#Region " CONSTRUCTORS "
    Public Sub New()
    End Sub

    Public Sub New(ByVal xScale As Double, ByVal xModifier As Double, ByVal yScale As Double, ByVal yModifier As Double)
        _xScale = xScale
        _xModifier = xModifier
        _yScale = yScale
        _yModifer = yModifier
    End Sub
#End Region
End Class
