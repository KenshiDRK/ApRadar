Imports System.IO
Imports System.Text
Imports System.Drawing
Imports System.Windows.Media.Imaging

Public Class Item
    Inherits Thing
    Implements IComparable(Of Item)

    Public Sub New()
        ' Fill Thing helpers
        Me.IconField_ = "icon"
        ' Clear fields
        Me.Clear()
    End Sub

    Public Overrides Function ToString() As String
        Return [String].Format("{0} {1} {2}", Me.ID_, Me.Name_, Me.Level_)
    End Function

#Region "Fields"

    Public Shared ReadOnly Property AllFields() As List(Of String)
        Get
            ' General
            ' English-Specific
            ' Furniture-Specific
            ' UsableItem-Specific
            ' Equipment-Specific
            ' Armor-Specific
            ' Weapon-Specific
            ' Enchantment Info
            ' Puppet Item Info
            ' Special Stuff
            Return New List(Of String)(New String() {"id", "flags", "stack-size", "type", "resource-id", "valid-targets", _
             "name", "description", "log-name-singular", "log-name-plural", "element", "storage-slots", _
             "activation-time", "level", "slots", "races", "jobs", "shield-size", _
             "damage", "delay", "dps", "skill", "jug-size", "max-charges", _
             "casting-time", "use-delay", "reuse-delay", "puppet-slot", "element-charge", "icon", _
             "unknown-1", "unknown-2", "unknown-3"})
        End Get
    End Property

    Public Overrides Function GetAllFields() As List(Of String)
        Return Item.AllFields
    End Function

#Region "Data Fields"

    ' General
    Private ID_ As System.Nullable(Of UInteger)
    Public ReadOnly Property ID As UInteger?
        Get
            Return ID_
        End Get
    End Property

    Private Flags_ As System.Nullable(Of ItemFlags)
    Private StackSize_ As System.Nullable(Of UShort)
    Private Type_ As System.Nullable(Of ItemType)
    Private ResourceID_ As System.Nullable(Of UShort)
    Private ValidTargets_ As System.Nullable(Of ValidTarget)
    Private Name_ As String
    Public ReadOnly Property Name As String
        Get
            Return Name_
        End Get
    End Property

    Private Description_ As String
    Public ReadOnly Property Description As String
        Get
            Return Description_
        End Get
    End Property
    ' English-Specific
    Private LogNameSingular_ As String
    Private LogNamePlural_ As String
    ' Furniture-Specific
    Private Element_ As System.Nullable(Of Element)
    Private StorageSlots_ As System.Nullable(Of Integer)
    ' UsableItem-Specific
    Private ActivationTime_ As System.Nullable(Of UShort)
    ' Equipment-Specific
    Private Level_ As System.Nullable(Of UShort)
    Public ReadOnly Property Level As UShort?
        Get
            Return Level_
        End Get
    End Property
    Private Slots_ As System.Nullable(Of EquipmentSlot)
    Public ReadOnly Property Slots As EquipmentSlot?
        Get
            Return Slots_
        End Get
    End Property
    Private Races_ As System.Nullable(Of Race)
    Public ReadOnly Property Races As Race?
        Get
            Return Races_
        End Get
    End Property
    Private Jobs_ As System.Nullable(Of Job)
    Public ReadOnly Property Jobs As Job?
        Get
            Return Jobs_
        End Get
    End Property
    ' Armor-Specific
    Private ShieldSize_ As System.Nullable(Of UShort)
    ' Weapon-Specific
    Private Damage_ As System.Nullable(Of UShort)
    Private Delay_ As System.Nullable(Of Short)
    Private DPS_ As System.Nullable(Of UShort)
    Private Skill_ As System.Nullable(Of Skill)
    Private JugSize_ As System.Nullable(Of Byte)
    ' Enchantment Info
    Private MaxCharges_ As System.Nullable(Of Byte)
    Private CastingTime_ As System.Nullable(Of Byte)
    Private UseDelay_ As System.Nullable(Of UShort)
    Private ReuseDelay_ As System.Nullable(Of UInteger)
    ' Puppet Item Info
    Private PuppetSlot_ As System.Nullable(Of PuppetSlot)
    Private ElementCharge_ As System.Nullable(Of UInteger)
    ' Special
    Private Icon_ As Graphic
    Private Unknown1_ As System.Nullable(Of UInteger)
    Private Unknown2_ As System.Nullable(Of UShort)
    Private Unknown3_ As System.Nullable(Of UInteger)

#End Region

#Region " XML PROPERTIES "
    Public Property ItemID() As Integer
        Get
            Return ID_
        End Get
        Set(ByVal value As Integer)
        End Set
    End Property

    Public Property ItemName As String
        Get
            Return Name_
        End Get
        Set(ByVal value As String)
            Name_ = value
        End Set
    End Property

    Public Property LogName As String
        Get
            Return LogNameSingular_
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private _iconImage As BitmapImage = Nothing
    <Xml.Serialization.XmlIgnore()>
    Public Property Icon As BitmapImage
        Get
            Return _iconImage
        End Get
        Set(value As BitmapImage)
            _iconImage = value
        End Set
    End Property

    Public Property IconString As String
        Get
            Dim ms As New MemoryStream
            Icon_.GetIcon().Save(ms, Drawing.Imaging.ImageFormat.Png)
            Return Convert.ToBase64String(ms.ToArray())
        End Get
        Set(value As String)
            Dim ms As New MemoryStream
            Dim b As Byte() = Convert.FromBase64String(value)
            ms.Write(b, 0, b.Length)
            ms.Position = 0
            Icon = New BitmapImage
            Icon.BeginInit()
            Icon.StreamSource = ms
            Icon.EndInit()
        End Set
    End Property
#End Region

    Public Overrides Sub Clear()
        If Me.Icon_ IsNot Nothing Then
            Me.Icon_.Clear()
        End If
        Me.ID_ = Nothing
        Me.Flags_ = Nothing
        Me.StackSize_ = Nothing
        Me.Type_ = Nothing
        Me.ResourceID_ = Nothing
        Me.ValidTargets_ = Nothing
        Me.Name_ = Nothing
        Me.Description_ = Nothing
        Me.LogNameSingular_ = Nothing
        Me.LogNamePlural_ = Nothing
        Me.Element_ = Nothing
        Me.StorageSlots_ = Nothing
        Me.ActivationTime_ = Nothing
        Me.Level_ = Nothing
        Me.Slots_ = Nothing
        Me.Races_ = Nothing
        Me.Jobs_ = Nothing
        Me.ShieldSize_ = Nothing
        Me.Damage_ = Nothing
        Me.Delay_ = Nothing
        Me.DPS_ = Nothing
        Me.Skill_ = Nothing
        Me.JugSize_ = Nothing
        Me.MaxCharges_ = Nothing
        Me.CastingTime_ = Nothing
        Me.UseDelay_ = Nothing
        Me.ReuseDelay_ = Nothing
        Me.PuppetSlot_ = Nothing
        Me.ElementCharge_ = Nothing
        Me.Icon_ = Nothing
        Me.Unknown1_ = Nothing
        Me.Unknown2_ = Nothing
        Me.Unknown3_ = Nothing
    End Sub

#End Region

#Region "Field Access"

    Public Overrides Function HasField(ByVal Field As String) As Boolean
        Select Case Field
            ' Objects
            Case "description"
                Return (Me.Description_ IsNot Nothing)
            Case "icon"
                Return (Me.Icon_ IsNot Nothing)
            Case "log-name-plural"
                Return (Me.LogNamePlural_ IsNot Nothing)
            Case "log-name-singular"
                Return (Me.LogNameSingular_ IsNot Nothing)
            Case "name"
                Return (Me.Name_ IsNot Nothing)
                ' Nullables
            Case "activation-time"
                Return Me.ActivationTime_.HasValue
            Case "casting-time"
                Return Me.CastingTime_.HasValue
            Case "damage"
                Return Me.Damage_.HasValue
            Case "delay"
                Return Me.Delay_.HasValue
            Case "dps"
                Return Me.DPS_.HasValue
            Case "element"
                Return Me.Element_.HasValue
            Case "element-charge"
                Return Me.ElementCharge_.HasValue
            Case "flags"
                Return Me.Flags_.HasValue
            Case "id"
                Return Me.ID_.HasValue
            Case "jobs"
                Return Me.Jobs_.HasValue
            Case "jug-size"
                Return Me.JugSize_.HasValue
            Case "level"
                Return Me.Level_.HasValue
            Case "max-charges"
                Return Me.MaxCharges_.HasValue
            Case "puppet-slot"
                Return Me.PuppetSlot_.HasValue
            Case "races"
                Return Me.Races_.HasValue
            Case "resource-id"
                Return Me.ResourceID_.HasValue
            Case "reuse-delay"
                Return Me.ReuseDelay_.HasValue
            Case "shield-size"
                Return Me.ShieldSize_.HasValue
            Case "skill"
                Return Me.Skill_.HasValue
            Case "slots"
                Return Me.Slots_.HasValue
            Case "stack-size"
                Return Me.StackSize_.HasValue
            Case "storage-slots"
                Return Me.StorageSlots_.HasValue
            Case "type"
                Return Me.Type_.HasValue
            Case "unknown-1"
                Return Me.Unknown1_.HasValue
            Case "unknown-2"
                Return Me.Unknown2_.HasValue
            Case "unknown-3"
                Return Me.Unknown3_.HasValue
            Case "use-delay"
                Return Me.UseDelay_.HasValue
            Case "valid-targets"
                Return Me.ValidTargets_.HasValue
            Case Else
                Return False
        End Select
    End Function

    Public Overrides Function GetFieldText(ByVal Field As String) As String
        Select Case Field
            ' Objects
            Case "description"
                Return Me.Description_
            Case "icon"
                Return Me.Icon_.ToString()
            Case "log-name-plural"
                Return Me.LogNamePlural_
            Case "log-name-singular"
                Return Me.LogNameSingular_
            Case "name"
                Return Me.Name_
                ' Objects - Special Formatting
            Case "element-charge"
                Dim Text As String = [String].Empty
                If Me.ElementCharge_.HasValue Then
                    For i As Short = 0 To 7
                        Dim Charge As Byte = CByte((Me.ElementCharge_ >> (4 * i)) And &HF)
                        If Charge = 0 Then
                            Continue For
                        End If
                        If Text <> [String].Empty Then
                            Text += " "c
                        End If
                        Text += [String].Format("{0}<{1}>", [Enum].Parse(GetType(Element), 1), Charge)
                    Next
                End If
                Return Text
                ' Nullables - Simple Values
            Case "damage"
                Return (If(Not Me.Damage_.HasValue, [String].Empty, [String].Format("{0}", Me.Damage_.Value)))
            Case "dps"
                Return (If(Not Me.DPS_.HasValue, [String].Empty, [String].Format("{0}", Me.DPS_.Value / 100.0)))
            Case "element"
                Return (If(Not Me.Element_.HasValue, [String].Empty, [String].Format("{0}", Me.Element_.Value)))
            Case "flags"
                Return (If(Not Me.Flags_.HasValue, [String].Empty, [String].Format("{0}", Me.Flags_.Value)))
            Case "jobs"
                Return (If(Not Me.Jobs_.HasValue, [String].Empty, [String].Format("{0}", Me.Jobs_.Value)))
            Case "jug-size"
                Return (If(Not Me.JugSize_.HasValue, [String].Empty, [String].Format("{0}", Me.JugSize_.Value)))
            Case "level"
                Return (If(Not Me.Level_.HasValue, [String].Empty, [String].Format("{0}", Me.Level_.Value)))
            Case "max-charges"
                Return (If(Not Me.MaxCharges_.HasValue, [String].Empty, [String].Format("{0}", Me.MaxCharges_.Value)))
            Case "puppet-slot"
                Return (If(Not Me.PuppetSlot_.HasValue, [String].Empty, [String].Format("{0}", Me.PuppetSlot_.Value)))
            Case "races"
                Return (If(Not Me.Races_.HasValue, [String].Empty, [String].Format("{0}", Me.Races_.Value)))
            Case "shield-size"
                Return (If(Not Me.ShieldSize_.HasValue, [String].Empty, [String].Format("{0}", Me.ShieldSize_.Value)))
            Case "skill"
                Return (If(Not Me.Skill_.HasValue, [String].Empty, [String].Format("{0}", Me.Skill_.Value)))
            Case "slots"
                Return (If(Not Me.Slots_.HasValue, [String].Empty, [String].Format("{0}", Me.Slots_.Value)))
            Case "stack-size"
                Return (If(Not Me.StackSize_.HasValue, [String].Empty, [String].Format("{0}", Me.StackSize_.Value)))
            Case "storage-slots"
                Return (If(Not Me.StorageSlots_.HasValue, [String].Empty, [String].Format("{0}", Me.StorageSlots_.Value)))
            Case "type"
                Return (If(Not Me.Type_.HasValue, [String].Empty, [String].Format("{0}", Me.Type_.Value)))
            Case "valid-targets"
                Return (If(Not Me.ValidTargets_.HasValue, [String].Empty, [String].Format("{0}", Me.ValidTargets_.Value)))
                ' Nullables - Hex Values
            Case "id"
                Return (If(Not Me.ID_.HasValue, [String].Empty, [String].Format("{0:X8} ({0})", Me.ID_.Value)))
            Case "resource-id"
                Return (If(Not Me.ResourceID_.HasValue, [String].Empty, [String].Format("{0:X4} ({0})", Me.ResourceID_.Value)))
            Case "unknown-1"
                Return (If(Not Me.Unknown1_.HasValue, [String].Empty, [String].Format("{0:X8} ({0})", Me.Unknown1_.Value)))
            Case "unknown-2"
                Return (If(Not Me.Unknown2_.HasValue, [String].Empty, [String].Format("{0:X4} ({0})", Me.Unknown2_.Value)))
            Case "unknown-3"
                Return (If(Not Me.Unknown3_.HasValue, [String].Empty, [String].Format("{0:X8} ({0})", Me.Unknown3_.Value)))
                ' Nullables - Time Values
            Case "activation-time"
                Return (If(Not Me.ActivationTime_.HasValue, [String].Empty, Me.FormatTime(Me.ActivationTime_.Value / 4.0)))
            Case "casting-time"
                Return (If(Not Me.CastingTime_.HasValue, [String].Empty, Me.FormatTime(Me.CastingTime_.Value / 4.0)))
            Case "reuse-delay"
                Return (If(Not Me.ReuseDelay_.HasValue, [String].Empty, Me.FormatTime(Me.ReuseDelay_.Value)))
            Case "use-delay"
                Return (If(Not Me.UseDelay_.HasValue, [String].Empty, Me.FormatTime(Me.UseDelay_.Value)))
                ' Nullables - Special/Complex Values
            Case "delay"
                Return (If(Not Me.Delay_.HasValue, [String].Empty, [String].Format("{0} ({1:+###0;-###0})", Me.Delay_.Value, Me.Delay_.Value - 240)))
            Case Else
                Return Nothing
        End Select
    End Function

    Public Overrides Function GetFieldValue(ByVal Field As String) As Object
        Select Case Field
            ' Objects
            Case "description"
                Return Me.Description_
            Case "icon"
                Return Me.Icon_
            Case "log-name-plural"
                Return Me.LogNamePlural_
            Case "log-name-singular"
                Return Me.LogNameSingular_
            Case "name"
                Return Me.Name_
                ' Nullables
            Case "activation-time"
                Return (If(Not Me.ActivationTime_.HasValue, Nothing, DirectCast(Me.ActivationTime_.Value, Object)))
            Case "casting-time"
                Return (If(Not Me.CastingTime_.HasValue, Nothing, DirectCast(Me.CastingTime_.Value, Object)))
            Case "damage"
                Return (If(Not Me.Damage_.HasValue, Nothing, DirectCast(Me.Damage_.Value, Object)))
            Case "delay"
                Return (If(Not Me.Delay_.HasValue, Nothing, DirectCast(Me.Delay_.Value, Object)))
            Case "dps"
                Return (If(Not Me.DPS_.HasValue, Nothing, DirectCast(Me.DPS_.Value, Object)))
            Case "element"
                Return (If(Not Me.Element_.HasValue, Nothing, DirectCast(Me.Element_.Value, Object)))
            Case "element-charge"
                Return (If(Not Me.ElementCharge_.HasValue, Nothing, DirectCast(Me.ElementCharge_.Value, Object)))
            Case "flags"
                Return (If(Not Me.Flags_.HasValue, Nothing, DirectCast(Me.Flags_.Value, Object)))
            Case "id"
                Return (If(Not Me.ID_.HasValue, Nothing, DirectCast(Me.ID_.Value, Object)))
            Case "jobs"
                Return (If(Not Me.Jobs_.HasValue, Nothing, DirectCast(Me.Jobs_.Value, Object)))
            Case "jug-size"
                Return (If(Not Me.JugSize_.HasValue, Nothing, DirectCast(Me.JugSize_.Value, Object)))
            Case "level"
                Return (If(Not Me.Level_.HasValue, Nothing, DirectCast(Me.Level_.Value, Object)))
            Case "max-charges"
                Return (If(Not Me.MaxCharges_.HasValue, Nothing, DirectCast(Me.MaxCharges_.Value, Object)))
            Case "puppet-slot"
                Return (If(Not Me.PuppetSlot_.HasValue, Nothing, DirectCast(Me.PuppetSlot_.Value, Object)))
            Case "races"
                Return (If(Not Me.Races_.HasValue, Nothing, DirectCast(Me.Races_.Value, Object)))
            Case "resource-id"
                Return (If(Not Me.ResourceID_.HasValue, Nothing, DirectCast(Me.ResourceID_.Value, Object)))
            Case "reuse-delay"
                Return (If(Not Me.ReuseDelay_.HasValue, Nothing, DirectCast(Me.ReuseDelay_.Value, Object)))
            Case "shield-size"
                Return (If(Not Me.ShieldSize_.HasValue, Nothing, DirectCast(Me.ShieldSize_.Value, Object)))
            Case "skill"
                Return (If(Not Me.Skill_.HasValue, Nothing, DirectCast(Me.Skill_.Value, Object)))
            Case "slots"
                Return (If(Not Me.Slots_.HasValue, Nothing, DirectCast(Me.Slots_.Value, Object)))
            Case "stack-size"
                Return (If(Not Me.StackSize_.HasValue, Nothing, DirectCast(Me.StackSize_.Value, Object)))
            Case "storage-slots"
                Return (If(Not Me.StorageSlots_.HasValue, Nothing, DirectCast(Me.StorageSlots_.Value, Object)))
            Case "type"
                Return (If(Not Me.Type_.HasValue, Nothing, DirectCast(Me.Type_.Value, Object)))
            Case "unknown-1"
                Return (If(Not Me.Unknown1_.HasValue, Nothing, DirectCast(Me.Unknown1_.Value, Object)))
            Case "unknown-2"
                Return (If(Not Me.Unknown2_.HasValue, Nothing, DirectCast(Me.Unknown2_.Value, Object)))
            Case "unknown-3"
                Return (If(Not Me.Unknown3_.HasValue, Nothing, DirectCast(Me.Unknown3_.Value, Object)))
            Case "use-delay"
                Return (If(Not Me.UseDelay_.HasValue, Nothing, DirectCast(Me.UseDelay_.Value, Object)))
            Case "valid-targets"
                Return (If(Not Me.ValidTargets_.HasValue, Nothing, DirectCast(Me.ValidTargets_.Value, Object)))
            Case Else
                Return Nothing
        End Select
    End Function

    Protected Overrides Sub LoadField(ByVal Field As String, ByVal Node As System.Xml.XmlElement)
        Select Case Field
            ' "Simple" Fields
            Case "activation-time"
                Me.ActivationTime_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "casting-time"
                Me.CastingTime_ = CByte(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "damage"
                Me.Damage_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "delay"
                Me.Delay_ = CShort(Me.LoadSignedIntegerField(Node))
                Exit Select
            Case "description"
                Me.Description_ = Me.LoadTextField(Node)
                Exit Select
            Case "dps"
                Me.DPS_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "element"
                Me.Element_ = [Enum].Parse(GetType(Element), Me.LoadHexField(Node))
                Exit Select
            Case "element-charge"
                Me.ElementCharge_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "flags"
                Me.Flags_ = [Enum].Parse(GetType(ItemFlags), LoadHexField(Node))
                Exit Select
            Case "id"
                Me.ID_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "jobs"
                Me.Jobs_ = [Enum].Parse(GetType(Job), LoadHexField(Node))
                Exit Select
            Case "jug-size"
                Me.JugSize_ = CByte(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "level"
                Me.Level_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "log-name-plural"
                Me.LogNamePlural_ = Me.LoadTextField(Node)
                Exit Select
            Case "log-name-singular"
                Me.LogNameSingular_ = Me.LoadTextField(Node)
                Exit Select
            Case "max-charges"
                Me.MaxCharges_ = CByte(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "name"
                Me.Name_ = Me.LoadTextField(Node)
                Exit Select
            Case "puppet-slot"
                Me.PuppetSlot_ = [Enum].Parse(GetType(PuppetSlot), LoadHexField(Node))
                Exit Select
            Case "races"
                Me.Races_ = [Enum].Parse(GetType(Race), LoadHexField(Node))
                Exit Select
            Case "resource-id"
                Me.ResourceID_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "reuse-delay"
                Me.ReuseDelay_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "shield-size"
                Me.ShieldSize_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "skill"
                Me.Skill_ = [Enum].Parse(GetType(Skill), LoadHexField(Node))
                Exit Select
            Case "slots"
                Me.Slots_ = [Enum].Parse(GetType(EquipmentSlot), LoadHexField(Node))
                Exit Select
            Case "stack-size"
                Me.StackSize_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "storage-slots"
                Me.StorageSlots_ = CInt(Me.LoadSignedIntegerField(Node))
                Exit Select
            Case "type"
                Me.Type_ = [Enum].Parse(GetType(ItemType), LoadHexField(Node))
                Exit Select
            Case "unknown-1"
                Me.Unknown1_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "unknown-2"
                Me.Unknown2_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "unknown-3"
                Me.Unknown3_ = CUInt(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "use-delay"
                Me.UseDelay_ = CUShort(Me.LoadUnsignedIntegerField(Node))
                Exit Select
            Case "valid-targets"
                Me.ValidTargets_ = [Enum].Parse(GetType(ValidTarget), LoadHexField(Node))
                Exit Select
                ' Sub-Things
            Case "icon"
                If Me.Icon_ Is Nothing Then
                    Me.Icon_ = New Graphic()
                End If
                Me.LoadThingField(Node, Me.Icon_)
                Exit Select
        End Select
    End Sub

#End Region

#Region "ROM File Reading"

    Public Enum iType
        Unknown
        Armor
        Currency
        Item
        PuppetItem
        UsableItem
        Weapon
    End Enum

    Public Shared Sub DeduceType(ByVal BR As BinaryReader, ByRef T As iType)
        T = iType.Unknown
        Dim FirstItem As Byte() = Nothing
        Dim Position As Long = BR.BaseStream.Position
        BR.BaseStream.Position = 0
        Try
            While BR.BaseStream.Position <> BR.BaseStream.Length
                FirstItem = BR.ReadBytes(&H4)
                BR.BaseStream.Position += (&HC00 - &H4)
                FFXIEncryption.Rotate(FirstItem, 5)
                If True Then
                    ' Type -> Based on ID
                    Dim ID As UInteger = 0
                    For i As Integer = 0 To 3
                        ID <<= 8
                        ID += FirstItem(3 - i)
                    Next
                    If ID = &HFFFF Then
                        T = iType.Currency
                    ElseIf ID < &H1000 Then
                        T = iType.Item
                    ElseIf ID < &H2000 Then
                        T = iType.UsableItem
                    ElseIf ID < &H2800 Then
                        T = iType.PuppetItem
                    ElseIf ID < &H4000 Then
                        T = iType.Armor
                    ElseIf ID < &H7000 Then
                        T = iType.Weapon
                    End If
                End If
                If T <> iType.Unknown Then
                    Exit While
                End If
            End While
        Catch
        End Try
        BR.BaseStream.Position = Position
    End Sub

    Public Function Read(ByVal BR As BinaryReader, ByVal T As iType) As Boolean
        Me.Clear()
        Try
            Dim ItemBytes As Byte() = BR.ReadBytes(&HC00)
            FFXIEncryption.Rotate(ItemBytes, 5)
            BR = New BinaryReader(New MemoryStream(ItemBytes, False))
            BR.BaseStream.Seek(&H280, SeekOrigin.Begin)
            Dim G As New Graphic()
            Dim GraphicSize As Integer = BR.ReadInt32()
            If GraphicSize < 0 OrElse Not G.Read(BR) OrElse BR.BaseStream.Position <> &H280 + 4 + GraphicSize Then
                BR.Close()
                Return False
            End If
            Me.Icon_ = G
            BR.BaseStream.Seek(0, SeekOrigin.Begin)
        Catch
            Return False
        End Try
        ' Common Fields (14 bytes)
        Me.ID_ = BR.ReadUInt32()
        Me.Flags_ = DirectCast(BR.ReadUInt16(), ItemFlags)
        Me.StackSize_ = BR.ReadUInt16()
        ' 0xe0ff for Currency, which kinda suggests this is really 2 separate bytes
        Me.Type_ = DirectCast(BR.ReadUInt16(), ItemType)
        Me.ResourceID_ = BR.ReadUInt16()
        Me.ValidTargets_ = DirectCast(BR.ReadUInt16(), ValidTarget)
        ' Extra Fields (22/30/10/6/2 bytes for Armor/Weapon/Puppet/Item/UsableItem)
        If T = iType.Armor OrElse T = iType.Weapon Then
            Me.Level_ = BR.ReadUInt16()
            Me.Slots_ = DirectCast(BR.ReadUInt16(), EquipmentSlot)
            Me.Races_ = DirectCast(BR.ReadUInt16(), Race)
            Me.Jobs_ = DirectCast(BR.ReadUInt32(), Job)
            If T = iType.Armor Then
                Me.ShieldSize_ = BR.ReadUInt16()
            Else
                ' Weapon
                Me.Damage_ = BR.ReadUInt16()
                Me.Delay_ = BR.ReadInt16()
                Me.DPS_ = BR.ReadUInt16()
                Me.Skill_ = DirectCast(BR.ReadByte(), Skill)
                Me.JugSize_ = BR.ReadByte()
                Me.Unknown1_ = BR.ReadUInt32()
            End If
            Me.MaxCharges_ = BR.ReadByte()
            Me.CastingTime_ = BR.ReadByte()
            Me.UseDelay_ = BR.ReadUInt16()
            If T = iType.Armor Then
                Me.Unknown2_ = BR.ReadUInt16()
            End If
            Me.ReuseDelay_ = BR.ReadUInt32()
            Me.Unknown3_ = BR.ReadUInt32()
        ElseIf T = iType.PuppetItem Then
            Me.PuppetSlot_ = [Enum].Parse(GetType(PuppetSlot), BR.ReadUInt16)
            Me.ElementCharge_ = BR.ReadUInt32()
            Me.Unknown3_ = BR.ReadUInt32()
        ElseIf T = iType.Item Then
            Select Case Me.Type_
                Case ItemType.Flowerpot, ItemType.Furnishing, ItemType.Mannequin
                    Me.Element_ = [Enum].Parse(GetType(Element), BR.ReadByte)
                    BR.ReadByte()
                    Me.StorageSlots_ = BR.ReadInt32()
                    Exit Select
                Case Else
                    Me.Unknown2_ = BR.ReadUInt16()
                    Me.Unknown3_ = BR.ReadUInt32()
                    Exit Select
            End Select
        ElseIf T = iType.UsableItem Then
            Me.ActivationTime_ = BR.ReadUInt16()
            Me.Unknown1_ = BR.ReadUInt32()
        ElseIf T = iType.Currency Then
            Me.Unknown2_ = BR.ReadUInt16()
        End If
        ' Next Up: Strings (variable size)
        Dim StringBase As Long = BR.BaseStream.Position
        Dim StringCount As UInteger = BR.ReadUInt32()
        If StringCount > 9 Then
            ' Sanity check, for safety - 0 strings is fine for now
            Me.Clear()
            Return False
        End If
        Dim E As New FFXIEncoding()
        Dim Strings As String() = New String(StringCount - 1) {}
        For i As Byte = 0 To StringCount - 1
            Dim Offset As Long = StringBase + BR.ReadUInt32()
            Dim Flag As UInteger = BR.ReadUInt32()
            If Offset < 0 OrElse Offset + &H20 > &H280 OrElse (Flag <> 0 AndAlso Flag <> 1) Then
                Me.Clear()
                Return False
            End If
            ' Flag seems to be 1 if the offset is not actually an offset. Could just be padding to make StringCount unique per language, or it could be an indication
            ' of the pronoun to use (a/an/the/...). The latter makes sense because of the increased number of such flags for french and german.
            If Flag = 0 Then
                BR.BaseStream.Position = Offset
                Strings(i) = Me.ReadString(BR, E)
                If Strings(i) Is Nothing Then
                    Me.Clear()
                    Return False
                End If
                BR.BaseStream.Position = StringBase + 4 + 8 * (i + 1)
            End If
        Next
        ' Assign the strings to the proper fields
        Select Case StringCount
            Case 2
                ' Japanese
                Me.Name_ = Strings(0)
                Me.Description_ = Strings(1)
                Exit Select
            Case 5
                ' English
                Me.Name_ = Strings(0)
                ' unused:              Strings[1]
                Me.LogNameSingular_ = Strings(2)
                Me.LogNamePlural_ = Strings(3)
                Me.Description_ = Strings(4)
                Exit Select
            Case 6
                ' French
                Me.Name_ = Strings(0)
                ' unused:              Strings[1]
                ' unused:              Strings[2]
                Me.LogNameSingular_ = Strings(3)
                Me.LogNamePlural_ = Strings(4)
                Me.Description_ = Strings(5)
                Exit Select
            Case 9
                ' German
                Me.Name_ = Strings(0)
                ' unused:              Strings[1]
                ' unused:              Strings[2]
                ' unused:              Strings[3]
                Me.LogNameSingular_ = Strings(4)
                ' unused:              Strings[5]
                ' unused:              Strings[6]
                Me.LogNamePlural_ = Strings(7)
                Me.Description_ = Strings(8)
                Exit Select
        End Select
        BR.Close()
        Return True
    End Function

    Private Function ReadString(ByVal BR As BinaryReader, ByVal E As Encoding) As String
        ' Read past "padding"
        If BR.ReadUInt32() <> 1 Then
            Return Nothing
        End If
        For i As Byte = 0 To 5
            If BR.ReadUInt32() <> 0 Then
                Return Nothing
            End If
        Next
        Dim TextBytes As New List(Of Byte)()
        While BR.BaseStream.Position < &H280
            Dim Next4 As Byte() = BR.ReadBytes(4)
            Dim UsableBytes As Byte = CByte(Next4.Length)
            Dim zeroIndex As Integer = Array.IndexOf(Next4, CByte(0))
            If zeroIndex > -1 Then
                For i = 0 To zeroIndex - 1
                    TextBytes.Add(Next4(i))
                Next
                Return E.GetString(TextBytes.ToArray()).Replace(vbLf, Environment.NewLine)
            Else
                TextBytes.AddRange(Next4)
            End If
            'While UsableBytes > 0 AndAlso Next4(UsableBytes - 1) = 0
            '    UsableBytes -= 1
            'End While
            'If UsableBytes <> 4 Then
            '    Dim i As Byte = 0
            '    While UsableBytes - 1 > 0
            '        UsableBytes -= 1
            '        TextBytes.Add(Next4(i))
            '    End While

            'Else
            '    TextBytes.AddRange(Next4)
            'End If
        End While
        Return Nothing
    End Function

#End Region

    Public Function CompareTo(ByVal other As Item) As Integer Implements System.IComparable(Of Item).CompareTo
        Return Me.GetFieldText("name").CompareTo(other.GetFieldText("name"))
    End Function
End Class

