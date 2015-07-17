Imports FFXIMemory

Public Class PartyEntry
    Private _isPreloaded As Boolean
    
    Private Enum EntryOffsets
        AlliancePointer = 0
        Index = 4
        Name = 6
        ID = 28
        HP = 36
        MP = 38
        TP = 44
        HPP = 46
        MPP = 47
        Zone = 50
        Active = 86
    End Enum

    Private _ffxi As FFXI

    Public Sub New(ByVal FFXI As FFXI, ByVal BaseAddress As Integer, ByVal isPreloaded As Boolean)
        _ffxi = FFXI
        _mobBase = BaseAddress
        _isPreloaded = isPreloaded
    End Sub



#Region " PRIVATE PROPERTIES "
    Private _memoryObject As Memory
    ''' <summary>
    ''' The memory object used to get the data for this mob
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MemoryObject() As Memory
        Get
            If _memoryObject Is Nothing Then
                _memoryObject = New Memory(FFXI.POL, MobBase)
            End If
            Return _memoryObject
        End Get
    End Property

    Private _mobBase As Integer
    ''' <summary>
    ''' The base address of the mobs structure
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MobBase() As Integer
        Get
            Return _mobBase
        End Get
        Set(ByVal value As Integer)
            _mobBase = value
            If _isPreloaded Then
                _mobBlock = MemoryObject.GetByteArray(88)
            End If
        End Set
    End Property

    Public Property FFXI() As FFXI
        Get
            Return _ffxi
        End Get
        Set(ByVal value As FFXI)
            _ffxi = value
            If _isPreloaded Then
                _mobBlock = MemoryObject.GetByteArray(88)
            End If
        End Set
    End Property

    Private _mobBlock As Byte()
    Private ReadOnly Property MobBlock() As Byte()
        Get
            If _mobBlock Is Nothing Then
                _mobBlock = MemoryObject.GetByteArray(88)
            End If
            Return _mobBlock
        End Get
    End Property
#End Region

#Region " PUBLIC PROPERTIES "

    Public ReadOnly Property AlliancePointer() As Integer
        Get
            If _isPreloaded Then
                Return BitConverter.ToInt32(MobBlock, EntryOffsets.AlliancePointer)
            Else
                MemoryObject.Address = MobBase
                Return MemoryObject.GetInt32()
            End If
        End Get
    End Property

    Public ReadOnly Property Index() As Byte
        Get
            If _isPreloaded Then
                Return MobBlock(EntryOffsets.Index)
            Else
                MemoryObject.Address = MobBase + EntryOffsets.Index
                Return MemoryObject.GetByte
            End If
        End Get
    End Property

    Private _name As String
    Public ReadOnly Property Name() As String
        Get
            If _isPreloaded Then
                For i As Integer = EntryOffsets.Name To MobBlock.Length - 1
                    If MobBlock(i) = 0 Then
                        _name = System.Text.Encoding.Default.GetString(MobBlock, EntryOffsets.Name, i - EntryOffsets.Name)
                        Exit For
                    End If
                Next
                Return _name
            Else
                MemoryObject.Address = MobBase + EntryOffsets.Name
                Return MemoryObject.GetName
            End If
        End Get
    End Property

    Public ReadOnly Property ID() As Int16
        Get
            If _isPreloaded Then
                Return BitConverter.ToInt16(MobBlock, EntryOffsets.ID)
            Else
                MemoryObject.Address = MobBase + EntryOffsets.ID
                Return MemoryObject.GetShort
            End If
        End Get
    End Property

    Public ReadOnly Property HP() As Int16
        Get
            If _isPreloaded Then
                Return BitConverter.ToInt16(MobBlock, EntryOffsets.HP)
            Else
                MemoryObject.Address = MobBase + EntryOffsets.HP
                Return MemoryObject.GetShort
            End If
        End Get
    End Property

    Public ReadOnly Property HPP() As Byte
        Get
            If _isPreloaded Then
                Return MobBlock(EntryOffsets.HPP)
            Else
                MemoryObject.Address = MobBase + EntryOffsets.HPP
                Return MemoryObject.GetByte
            End If
        End Get
    End Property

    Public ReadOnly Property MP() As Int16
        Get
            If _isPreloaded Then
                Return BitConverter.ToInt16(MobBlock, EntryOffsets.MP)
            Else
                MemoryObject.Address = MobBase + EntryOffsets.MP
                Return MemoryObject.GetShort
            End If
        End Get
    End Property

    Public ReadOnly Property MPP() As Byte
        Get
            If _isPreloaded Then
                Return MobBlock(EntryOffsets.MPP)
            Else
                MemoryObject.Address = MobBase + EntryOffsets.MPP
                Return MemoryObject.GetByte
            End If
        End Get
    End Property

    Public ReadOnly Property TP() As Int16
        Get
            If _isPreloaded Then
                Return BitConverter.ToInt16(MobBlock, EntryOffsets.TP)
            Else
                MemoryObject.Address = MobBase + EntryOffsets.TP
                Return MemoryObject.GetShort
            End If
        End Get
    End Property

    Public ReadOnly Property Zone() As Byte
        Get
            If _isPreloaded Then
                Return MobBlock(EntryOffsets.Zone)
            Else
                MemoryObject.Address = MobBase + EntryOffsets.Zone
                Return MemoryObject.GetByte
            End If
        End Get
    End Property

    Public ReadOnly Property Active() As Boolean
        Get
            If _isPreloaded Then
                Return BitConverter.ToBoolean(MobBlock, EntryOffsets.Active)
            Else
                MemoryObject.Address = MobBase + EntryOffsets.Active
                Return CBool(MemoryObject.GetByte)
            End If
        End Get
    End Property
#End Region
End Class
