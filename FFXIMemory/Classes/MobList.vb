Imports System.Runtime.Serialization

Public Class MobList
    Implements IDisposable

    Private _listInternal As Dictionary(Of Integer, MobData)

    Public Sub New()
        _listInternal = New Dictionary(Of Integer, MobData)
    End Sub

    Public Sub New(ByVal Mobs As List(Of MobData))
        _listInternal = New Dictionary(Of Integer, MobData)
        For Each m In Mobs
            Add(m)
        Next
    End Sub

    Public Sub New(ByVal Mobs As MobData())
        _listInternal = New Dictionary(Of Integer, MobData)
        For Each m In Mobs
            Add(m)
        Next
    End Sub

    Public Enum MobStatus
        Dead
        Alive
        OutOfRange
    End Enum

    Public Event MobStatusChanged(ByVal Mob As MobData, ByVal Status As MobStatus)

    Public Overloads Sub Add(ByVal Mob As MobData)
        'Check to see if this mob exists already
        If Not _listInternal.ContainsKey(Mob.ServerID) Then
            'If not we add it
            If Mob.MobType = MobData.MobTypes.NPC Then
                RaiseEvent MobStatusChanged(Mob, IIf(CheckIsDead(Mob), MobStatus.Dead, MobStatus.Alive))
            End If

            _listInternal.Add(Mob.ServerID, Mob)
        Else
            Update(Mob)
        End If
    End Sub

    Public Sub AddRange(ByVal Mobs As MobData())
        For Each mob In Mobs
            Me.Add(mob)
        Next
    End Sub

    Public Sub Update(ByVal Mob As MobData)
        Dim isDead, wasDead As Boolean


        Mob.Filters.MapFiltered = False
        Mob.Filters.OverlayFiltered = False
        If _listInternal.ContainsKey(Mob.ServerID) Then
            If Mob.MobType <> MobData.MobTypes.PC Then
                isDead = CheckIsDead(Mob)
                wasDead = CheckIsDead(_listInternal(Mob.ServerID))
                If wasDead <> isDead Then
                    If isDead Then
                        Mob.MobIsDead = True
                        RaiseEvent MobStatusChanged(Mob, MobStatus.Dead)
                    Else
                        Mob.MobIsDead = False
                        RaiseEvent MobStatusChanged(Mob, MobStatus.Alive)
                    End If
                ElseIf isDead AndAlso (_listInternal(Mob.ServerID).PIcon = 0 And Mob.PIcon = 16) Then
                    RaiseEvent MobStatusChanged(Mob, MobStatus.OutOfRange)
                End If
            End If
            _listInternal(Mob.ServerID) = Mob
        Else
            If Mob.MobType = MobData.MobTypes.NPC Then
                RaiseEvent MobStatusChanged(Mob, IIf(CheckIsDead(Mob), MobStatus.Dead, MobStatus.Alive))
            End If
            _listInternal.Add(Mob.ServerID, Mob)
        End If
    End Sub

    Public Overloads Sub Remove(ByVal Mob As MobData)
        _listInternal.Remove(Mob.ServerID)
    End Sub

    Public Overloads Sub Remove(ByVal MobID As Integer)
        _listInternal.Remove(MobID)
    End Sub

    Public Function Item(ByVal MobId As Integer) As MobData
        If _listInternal.ContainsKey(MobId) Then
            Return _listInternal.Item(MobId)
        Else
            Return Nothing
        End If
    End Function

    Public Function ItemByZoneID(ByVal MobZoneID As Short) As MobData
        Return _listInternal.Values.Where(Function(c) c.ID = MobZoneID).FirstOrDefault()
    End Function

    Public Function ToList() As List(Of MobData)
        Return _listInternal.Values.ToList
    End Function

    ''' <summary>
    ''' Creates a shallow copied array of the mobs in the list
    ''' </summary>
    ''' <returns>Array of MobData</returns>
    ''' <remarks></remarks>
    Public Function ToClonedArray() As MobData()
        Return _listInternal.Values.ToArray.Clone
    End Function

    Public Sub Clear()
        _listInternal.Clear()
    End Sub

    Private Shared Function CheckIsDead(ByVal Mob As MobData) As Boolean
        If Not Mob Is Nothing Then
            Return Mob.HP = 0 OrElse Mob.WarpInfo = 0
        End If
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                _listInternal.Clear()
                _listInternal = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
