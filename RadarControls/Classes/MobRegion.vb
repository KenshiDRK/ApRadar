Imports System.Drawing
Public Class MobRegion
    Public Sub New(ByVal Id As Integer, ByVal Name As String, ByVal Region As rectangle)
        _mobId = Id
        _mobName = Name
        _mobRegion = Region
    End Sub

    Private _mobId As Integer
    Public Property MobID() As Integer
        Get
            Return _mobId
        End Get
        Set(ByVal value As Integer)
            _mobId = value
        End Set
    End Property

    Private _mobName As String
    Public Property MobName() As String
        Get
            Return _mobName
        End Get
        Set(ByVal value As String)
            _mobName = value
        End Set
    End Property

    Private _mobRegion As Rectangle
    Public Property MobRegion() As Rectangle
        Get
            Return _mobRegion
        End Get
        Set(ByVal value As Rectangle)
            _mobRegion = value
        End Set
    End Property
End Class
