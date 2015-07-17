''' <summary>
''' Mob Data - String binding class
''' </summary>
Public Class ZoneMobs
    Implements IComparable(Of ZoneMobs)

    Public Sub New(ByVal Name As String, ByVal ID As Short, ByVal ServerID As Integer)
        MobName = Name
        MobID = ID
        _serverID = ServerID
    End Sub

    Private _MobName As String
    Public Property MobName() As String
        Get
            Return _MobName
        End Get
        Set(ByVal value As String)
            _MobName = value
        End Set
    End Property
    Private _MobId As Short
    Public Property MobID() As Short
        Get
            Return _MobId
        End Get
        Set(ByVal value As Short)
            _MobId = value
        End Set
    End Property

    Private _serverID As Integer
    Public Property ServerID() As Integer
        Get
            Return _serverID
        End Get
        Set(ByVal value As Integer)
            _serverID = value
        End Set
    End Property

    Public ReadOnly Property DisplayString As String
        Get
            Return String.Format("{0} : {1}", Me.MobID.ToString("X2"), Me.MobName)
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return String.Format("{0} : {1}", Me.MobID.ToString("X2"), Me.MobName)
    End Function

    Public Function CompareTo(ByVal other As ZoneMobs) As Integer Implements System.IComparable(Of ZoneMobs).CompareTo
        'Return MobName.CompareTo(other.MobName) And MobID.CompareTo(other.MobID) '(MobName & " : " & MobID.ToString()).CompareTo(other.MobName & " : " & other.MobID.ToString())
        If MobName <> other.MobName Then
            Return MobName.CompareTo(other.MobName)
        Else
            Return MobID.CompareTo(other.MobID)
        End If
        'Return String.Compare(MobName & MobID.ToString("X2"), other.MobName & other.MobID.ToString("X2"))
    End Function
End Class
