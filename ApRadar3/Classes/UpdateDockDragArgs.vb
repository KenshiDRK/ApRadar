Friend Structure UpdateDockDragArgs
    Private _x As Single
    Private _y As Single
    ''' <summary>
    ''' Summary for UpdateDockDragArgs
    ''' </summary>
    Public Sub New(ByVal X As Single, ByVal Y As Single)
        _x = X
        _y = Y
    End Sub
    Public ReadOnly Property X() As Single
        Get
            Return _x
        End Get
    End Property
    Public ReadOnly Property Y() As Single
        Get
            Return _y
        End Get
    End Property
End Structure
