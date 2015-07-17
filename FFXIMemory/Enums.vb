Public Enum AbilityType As Byte
    General = &H0
    Job = &H1
    Pet = &H2
    Weapon = &H3
    Trait = &H4
    BloodPactRage = &H6
    Corsair = &H8
    CorsairShot = &H9
    BloodPactWard = &HA
    Samba = &HB
    Waltz = &HC
    [Step] = &HD
    Flourish1 = &HE
    Scholar = &HF
    Jig = &H10
    Flourish2 = &H11
    Monster = &H12
    Flourish3 = &H13
End Enum

Public Enum Element As Byte
    Fire = &H0
    Ice = &H1
    Air = &H2
    Earth = &H3
    Thunder = &H4
    Water = &H5
    Light = &H6
    Dark = &H7
    Special = &HF
    ' this is the element set on the Meteor spell
    Undecided = &HFF
    ' this is the element set on inactive furnishing items in the item data
End Enum

Public Enum ElementColor As Byte
    Red = &H0
    Clear = &H1
    Green = &H2
    Yellow = &H3
    Purple = &H4
    Blue = &H5
    White = &H6
    Black = &H7
End Enum

<Flags()> _
Public Enum EquipmentSlot As UShort
    ' Specific Slots
    None = &H0
    Main = &H1
    [Sub] = &H2
    Range = &H4
    Ammo = &H8
    Head = &H10
    Body = &H20
    Hands = &H40
    Legs = &H80
    Feet = &H100
    Neck = &H200
    Waist = &H400
    LEar = &H800
    REar = &H1000
    LRing = &H2000
    RRing = &H4000
    Back = &H8000
    ' Slot Groups
    Ears = &H1800
    Rings = &H6000
    All = &HFFFF
End Enum

<Flags()> _
Public Enum ItemFlags As UShort
    None = &H0
    ' Simple Flags - mostly assumed meanings
    Flag00 = &H1
    Flag01 = &H2
    Flag02 = &H4
    Flag03 = &H8
    Flag04 = &H10
    Inscribable = &H20
    NoAuction = &H40
    Scroll = &H80
    Linkshell = &H100
    CanUse = &H200
    CanTradeNPC = &H400
    CanEquip = &H800
    NoSale = &H1000
    NoDelivery = &H2000
    NoTradePC = &H4000
    Rare = &H8000
    ' Combined Flags
    Ex = &H6040
    ' NoAuction + NoDelivery + NoTrade
End Enum

Public Enum ItemType As UShort
    [Nothing] = &H0
    Item = &H1
    QuestItem = &H2
    Fish = &H3
    Weapon = &H4
    Armor = &H5
    Linkshell = &H6
    UsableItem = &H7
    Crystal = &H8
    Currency = &H9
    Furnishing = &HA
    Plant = &HB
    Flowerpot = &HC
    PuppetItem = &HD
    Mannequin = &HE
    Book = &HF
    RacingForm = &H10
    BettingSlip = &H11
    SoulPlate = &H12
    Reflector = &H13
    ItemType20 = &H14
    LotteryTicket = &H15
    ItemType22 = &H16
    ItemType23 = &H17
    ItemType24 = &H18
    ItemType25 = &H19
End Enum

<Flags()> _
Public Enum Job As UInteger
    None = &H0
    'All = &H1FFFFE
    All = &H7FFFFE
    ' Masks valid jobs
    ' Specific
    WAR = &H2
    MNK = &H4
    WHM = &H8
    BLM = &H10
    RDM = &H20
    THF = &H40
    PLD = &H80
    DRK = &H100
    BST = &H200
    BRD = &H400
    RNG = &H800
    SAM = &H1000
    NIN = &H2000
    DRG = &H4000
    SMN = &H8000
    BLU = &H10000
    COR = &H20000
    PUP = &H40000
    DNC = &H80000
    SCH = &H100000
    GEO = &H200000
    RUN = &H400000
    'JOB23 = &H800000
    'JOB24 = &H1000000
    'JOB25 = &H2000000
    'JOB26 = &H4000000
    'JOB27 = &H8000000
    'JOB28 = &H10000000
    'JOB29 = &H20000000
    'JOB30 = &H40000000
    'JOB31 = &H80000000UI
End Enum

Public Enum MagicType As Byte
    None = &H0
    WhiteMagic = &H1
    BlackMagic = &H2
    SummonerPact = &H3
    Ninjutsu = &H4
    BardSong = &H5
    BlueMagic = &H6
End Enum

Public Enum PuppetSlot As Byte
    None = &H0
    Head = &H1
    Body = &H2
    Attachment = &H3
End Enum

<Flags()> _
Public Enum Race As UShort
    None = &H0
    All = &H1FE
    ' Specific
    HumeMale = &H2
    HumeFemale = &H4
    ElvaanMale = &H8
    ElvaanFemale = &H10
    TarutaruMale = &H20
    TarutaruFemale = &H40
    Mithra = &H80
    Galka = &H100
    ' Race Groups
    Hume = &H6
    Elvaan = &H18
    Tarutaru = &H60
    ' Gender Groups (with Mithra = female, and Galka = male)
    Male = &H12A
    Female = &HD4
End Enum

Public Enum Skill As Byte
    None = &H0
    HandToHand = &H1
    Dagger = &H2
    Sword = &H3
    GreatSword = &H4
    Axe = &H5
    GreatAxe = &H6
    Scythe = &H7
    PoleArm = &H8
    Katana = &H9
    GreatKatana = &HA
    Club = &HB
    Staff = &HC
    Ranged = &H19
    Marksmanship = &H1A
    Thrown = &H1B
    DivineMagic = &H20
    HealingMagic = &H21
    EnhancingMagic = &H22
    EnfeeblingMagic = &H23
    ElementalMagic = &H24
    DarkMagic = &H25
    SummoningMagic = &H26
    Ninjutsu = &H27
    Singing = &H28
    StringInstrument = &H29
    WindInstrument = &H2A
    BlueMagic = &H2B
    Fishing = &H30
    ' These are assumed values, no known data actually uses them
    Woodworking = &H31
    Smithing = &H32
    Goldsmithing = &H33
    Clothcraft = &H34
    Leathercraft = &H35
    Bonecraft = &H36
    Alchemy = &H37
    Cooking = &H38
    ' Set on pet food
    Special = &HFF
End Enum

<Flags()> _
Public Enum ValidTarget As UShort
    None = &H0
    Self = &H1
    Player = &H2
    PartyMember = &H4
    Ally = &H8
    NPC = &H10
    Enemy = &H20
    Unknown = &H40
    CorpseOnly = &H80
    Corpse = &H9D
    ' CorpseOnly + NPC + Ally + Partymember + Self
End Enum

Public Enum Direction
    East = 0
    SouthEast = 1
    South = 2
    SouthWest = 3
    West = 4
    NorthWest = 5
    North = 6
    NorthEast = 7
End Enum
