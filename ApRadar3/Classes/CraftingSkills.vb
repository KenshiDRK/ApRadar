Imports FFXIMemory

Public Class CraftingSkills
    Private _ffxi As FFXI
    Private _mem As Memory
    Public Sub New(ByVal FFXI As FFXI)
        _ffxi = FFXI
    End Sub

    Private ReadOnly Property MemoryObject() As Memory
        Get
            If _mem Is Nothing Then
                If Not _ffxi Is Nothing AndAlso Not _ffxi.POL Is Nothing _
                AndAlso Not _ffxi.POL.HasExited AndAlso _ffxi.IsGameLoaded Then
                    _mem = New Memory(_ffxi.POL, 0)
                End If
            End If
            Return _mem
        End Get
    End Property

    Private Enum Offsets
        Fishing = 170
        Woodworking = 172
        Smithing = 174
        Goldmsithing = 176
        Clothcraft = 178
        Leathercraft = 180
        Bonecraft = 182
        Alchemy = 184
        Cooking = 186
    End Enum

    Public ReadOnly Property Fishing() As Short
        Get
            MemoryObject.Address = _ffxi.MemLocs("PLAYERINFO") + Offsets.Fishing
            Return CalculateLevel(MemoryObject.GetShort)
        End Get
    End Property

    Public ReadOnly Property Woodworking() As Short
        Get
            MemoryObject.Address = _ffxi.MemLocs("PLAYERINFO") + Offsets.Woodworking
            Return CalculateLevel(MemoryObject.GetShort)
        End Get
    End Property

    Public ReadOnly Property Smithing() As Short
        Get
            MemoryObject.Address = _ffxi.MemLocs("PLAYERINFO") + Offsets.Smithing
            Return CalculateLevel(MemoryObject.GetShort)
        End Get
    End Property

    Public ReadOnly Property Goldsmithing() As Short
        Get
            MemoryObject.Address = _ffxi.MemLocs("PLAYERINFO") + Offsets.Goldmsithing
            Return CalculateLevel(MemoryObject.GetShort)
        End Get
    End Property

    Public ReadOnly Property Clothcraft() As Short
        Get
            MemoryObject.Address = _ffxi.MemLocs("PLAYERINFO") + Offsets.Clothcraft
            Return CalculateLevel(MemoryObject.GetShort)
        End Get
    End Property

    Public ReadOnly Property Leathercraft() As Short
        Get
            MemoryObject.Address = _ffxi.MemLocs("PLAYERINFO") + Offsets.Leathercraft
            Return CalculateLevel(MemoryObject.GetShort)
        End Get
    End Property

    Public ReadOnly Property Bonecraft() As Short
        Get
            MemoryObject.Address = _ffxi.MemLocs("PLAYERINFO") + Offsets.Bonecraft
            Return CalculateLevel(MemoryObject.GetShort)
        End Get
    End Property

    Public ReadOnly Property Alchemy() As Short
        Get
            MemoryObject.Address = _ffxi.MemLocs("PLAYERINFO") + Offsets.Alchemy
            Return CalculateLevel(MemoryObject.GetShort)
        End Get
    End Property

    Public ReadOnly Property Cooking() As Short
        Get
            MemoryObject.Address = _ffxi.MemLocs("PLAYERINFO") + Offsets.Cooking
            Return CalculateLevel(MemoryObject.GetShort)
        End Get
    End Property

    Private Shared Function CalculateLevel(ByVal memValue As Short) As Short
        If memValue < 0 Then
            memValue += 32767
        End If
        Return Math.Floor(memValue / 32)
    End Function
End Class
