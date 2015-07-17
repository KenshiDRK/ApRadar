Public Class PacketMob
    'Public Sub New(ByVal p As HookHandler.NPCPacket)

    'End Sub

    Private _id As Short
    Public Property ID() As Short
        Get
            Return _id
        End Get
        Set(ByVal value As Short)
            _id = value
        End Set
    End Property

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _x As Single
    Public Property X() As Single
        Get
            Return _x
        End Get
        Set(ByVal value As Single)
            _x = value
        End Set
    End Property

    Private _y As Single
    Public Property Y() As Single
        Get
            Return _y
        End Get
        Set(ByVal value As Single)
            _y = value
        End Set
    End Property

    Private _z As Single
    Public Property Z() As Single
        Get
            Return _z
        End Get
        Set(ByVal value As Single)
            _z = value
        End Set
    End Property

    Private _direction As Single
    Public Property Direction() As Single
        Get
            Return _direction
        End Get
        Set(ByVal value As Single)
            _direction = value
        End Set
    End Property
End Class
