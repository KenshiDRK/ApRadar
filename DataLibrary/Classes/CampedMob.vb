Public Class CampedMob
    Public Sub New()

    End Sub

    Public Sub New(ByVal Name As String, ByVal ID As Integer, ByVal ServerID As Long, ByVal Zone As Short, ByVal IsDead As Boolean, ByVal DeathTime As String, ByVal Position As System.Drawing.PointF)
        Me.Name = Name
        Me.ID = ID
        Me.ServerID = ServerID
        Me.Zone = Zone
        Me.IsDead = IsDead
        Me.DeathTime = DeathTime
        Me.Position = Position
    End Sub



    Public Property Name() As String
    
    Public Property ID() As Integer
    
    Public Property ServerID() As Long
    
    Public Property Zone() As Byte
    
    Public Property IsDead() As Boolean
    
    Public Property DeathTime() As String
    
    Public Property Position As System.Drawing.PointF
End Class
