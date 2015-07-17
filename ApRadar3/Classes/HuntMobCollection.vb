Public Class HuntMobCollection
    Inherits Collections.Generic.List(Of HuntMob)

    Public Sub New()
        
    End Sub
    Public Sub New(ByVal capacity As Integer)
        MyBase.New(capacity)
        
    End Sub
    Public Sub New(ByVal collection As IEnumerable(Of HuntMob))
        MyBase.New(collection)
        
    End Sub

    Public Function GetHunt(ByVal MobID As Int16) As HuntMob
        Return (From c In Me Where c.MobID = MobID).FirstOrDefault()
    End Function

    Public Class HuntMob
        Public Property MobID() As Int16
        Public Property MobName() As String

        Private _isDead As Boolean
        Public Property IsDead() As Boolean
            Get
                Return _isDead
            End Get
            Set(ByVal value As Boolean)
                If _isDead And Not value Then
                    SpawnTime = DateTime.Now
                End If
                _isDead = value
            End Set
        End Property
        Public Property SpawnTime() As DateTime
    End Class
End Class
