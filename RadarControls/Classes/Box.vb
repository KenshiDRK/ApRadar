Public Class Box
#Region " PROPERTIES "
    Private _x1 As Single
    Public Property X1() As Single
        Get
            Return _x1
        End Get
        Set(ByVal value As Single)
            _x1 = value
        End Set
    End Property

    Private _y1 As Single
    Public Property Y1() As Single
        Get
            Return _y1
        End Get
        Set(ByVal value As Single)
            _y1 = value
        End Set
    End Property

    Private _z1 As Single
    Public Property Z1() As Single
        Get
            Return _z1
        End Get
        Set(ByVal value As Single)
            _z1 = value
        End Set
    End Property

    Private _x2 As Single
    Public Property X2() As Single
        Get
            Return _x2
        End Get
        Set(ByVal value As Single)
            _x2 = value
        End Set
    End Property

    Private _y2 As Single
    Public Property Y2() As Single
        Get
            Return _y2
        End Get
        Set(ByVal value As Single)
            _y2 = value
        End Set
    End Property

    Private _z2 As Single
    Public Property Z2() As Single
        Get
            Return _z2
        End Get
        Set(ByVal value As Single)
            _z2 = value
        End Set
    End Property
#End Region

#Region " CONSTRUCTOR "
    Public Sub New(ByVal x1 As Single, ByVal y1 As Single, ByVal z1 As Single, ByVal x2 As Single, ByVal y2 As Single, ByVal z2 As Single)
        _x1 = x1
        _y1 = y1
        _z1 = z1
        _x2 = x2
        _y2 = y2
        _z2 = z2
    End Sub
#End Region
End Class
