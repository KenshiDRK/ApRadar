Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Xml.Serialization
Public Class MobData
    Implements IDisposable

#Region " MEMBER VARIABLES "
    Private _isPacket As Boolean
    'Private _mobPacket As NPCPacket
    Private _mapID As Byte
    Private _zones As Zones
    Private _dataPreLoaded As Boolean = False
#End Region

#Region " CONSTRUCTORS "

    Protected Sub New()
    End Sub

    Public Sub New(ByVal POL As Process, ByVal MobBase As Integer, ByVal PreloadData As Boolean)
        _mobBase = MobBase
        _pol = POL
        _isPacket = False
        If PreloadData Then
            _dataPreLoaded = True
            MobBlock = MemoryObject.GetByteArray(568)
        End If
    End Sub
#End Region

#Region " ENUMERATIONS "
    Public Enum MobTypes
        PC = 0
        NPC = 1
        NPCUnique = 2 'May just mean can interact. not sure yet
        NPCObject = 3 'so far only doors...
        Boat = 5
    End Enum

    ''' <summary>
    ''' This is the type of model it is.  This is a bitflag so it can me a combination of many.
    ''' </summary>
    ''' <remarks>This may come in quite handy</remarks>
    <Flags()>
    Public Enum SpawnTypes
        None = 0
        PC = 1
        NPC = 2
        GroupMember = 4
        AllianceMember = 8
        Mob = 16
        DoorOrObject = 32 'May just be an interactable object
        Unk1 = 64
        Unk2 = 128
    End Enum

    Public Enum MobOffsets
        LastX = 4
        LastY = 12
        LastZ = 8
        LastDirection = 24
        X = 36
        Y = 44
        Z = 40
        Direction = 56
        ID = 116
        ServerID = 120
        Name = 124
        WarpInfo = 160 '12/12/12 8 bytes added after name
        '4 byte integer added 3/26/2013
        Distance = 216
        ' -8 bytes here
        PetTP = 234 ' erased pet tp on dec 2015 update?
        HP = 236
        MobType = 238 '198
        Race = 239 '199
        AttackTimer = 240
        Face = 252
        Hair = 252 '7/12/11 Start of 4 byte shift (not sure what they added)
        Head = 254
        Body = 256
        Hands = 258
        Legs = 260
        Feet = 262
        MainWeapon = 264
        SubWeapon = 266
        '12/12/12 Shifted -4 after render somewhere between these 2
        PIcon = 292
        GIcon = 296
        Speed = 348
        Status = 364
        Status2 = 368
        ClaimedBy = 392 'Was 340
        SpawnType = 464 'Was 400
        PCTarget = 504
        PetIndex = 506
    End Enum


    '    //Biggest. Struct. Ever.
    '[StructLayout(LayoutKind.Sequential, Pack = 1)] //im sure some of these fields could be removed with the proper packing. but im lazy.
    'public struct SpawnInfo {
    '   public Int32 u1; //possibly a signature. always seems to be this for player records. other bytes noticed here for different data types in the linked list.
    '   public float PredictedX; //These coords jump. a LOT. This leaves me to believe they are predicted values by the client.
    '   public float PredictedZ; //  Prediction is rooted in FPS games to give the user a smoother movement experience.
    '   public float PredictedY; //  Lag in whitegate will GREATLY demonstrate the properties of these values if used.
    '   public float u2;
    '   public Int32 u3;
    '   public float PredictedHeading;
    '   public Int32 u4;
    '   public float u5;
    '   public float X; //These coords are used because it seems like a good mix between actual and predicted.
    '   public float Z; //Also note that the assinine ordering (xzy) is corrected.
    '   public float Y;
    '   public float u6;
    '   public Int32 u7;
    '   public float Heading; //heading is expressed in radians. cos/sin will extract a normalized x/y coord.
    '   public Int32 u8;
    '   public Int32 u9;
    '   public float X2; //These are assumed to be server confirmed (actual) coords.
    '   public float Z2;
    '   public float Y2;
    '   public float u10;
    '   public Int32 u11;
    '   public Int32 u12;
    '   public Int32 u13;
    '   public Int32 u14;
    '   public Int32 u15;
    '   public Int32 u16;
    '   public Int32 u17;
    '   public Int32 u18;
    '   public UInt32 ZoneID;
    '   public UInt32 ServerID;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
    '   public string DisplayName; //player is 16 max, but npc's can be up to 24.
    '   public Int32 pUnknown;
    '   public float RunSpeed;
    '   public float RunSpeed2;
    '   public Int32 pListNode; //a pointer to the node this record belongs to in a resource linked list. note that the data in this list contains many things not just spawn data. further down the chain is unstable.
    '   public Int32 u19;
    '   public Int32 NPCTalking;
    '   public float Distance;
    '   public Int32 u20;
    '   public Int32 u21;
    '   public float Heading2;
    '   public Int32 pPetOwner; //only for permanent pets. charmed mobs do not fill this.
    '   public Int32 u22;
    '   public byte HealthPercent;
    '   public byte u23;
    '   public byte ModelType;
    '   public byte Race;
    '   public Int32 NPCPathingTime;
    '   public Int32 u25;
    '   public Int32 u26;
    '   public Int16 Model;
    '   public Int16 ModelHead;
    '   public Int16 ModelBody;
    '   public Int16 ModelHands;
    '   public Int16 ModelLegs;
    '   public Int16 ModelFeet;
    '   public Int16 ModelMain;
    '   public Int16 ModelSub;
    '   public Int16 ModelRanged;
    '   public Int32 u27;
    '   public Int32 u28;
    '   public Int32 u29;
    '   public Int16 u30;
    '   public Int16 u31;
    '   public byte u33;
    '   public byte u34;
    '   public Int32 u70; //Added Feb 14th 2011 Patch; Fixed by Jetsam
    '   public byte FlagRender;
    '   public byte Flags1; //I am well aware these should be combined,
    '   public byte Flags2; //  but it is easier for future discoveries
    '   public byte Flags3; //  as my documentation and memory dissect
    '   public byte Flags4; //  structs have these separated (to spot
    '   public byte Flags5; //  flag changes after performing an action).
    '   public byte Flags6;
    '   public byte Flags7; //  besides, I am not entirely sure where the
    '   public byte Flags8; //  first flag boundary starts, and what lies
    '   public byte Flags9; //  on a word boundary (and thus is padding).
    '   public Int32 u71; //Added Jul 13th 2011 Patch; Fixed by Zer0Blues
    '   public byte Flags10;
    '   public byte Flags11;
    '   public byte Flags12;
    '   public byte Flags13;
    '   public byte Flags14;
    '   public byte Flags15;
    '   public byte Flags16;
    '   public byte Flags17;
    '   public byte Flags18;
    '   public byte Flags19;
    '   public byte Flags20;
    '   public byte u35;
    '   public Int32 u36;
    '   public Int32 u37;
    '   public Int16 NPCSpeechLoop;
    '   public Int16 NPCSpeechFrame;
    '   public Int32 u38;
    '   public Int32 u39;
    '   public Int16 u40;
    '   public float RunSpeed3;
    '   public Int16 NPCWalkPos1;
    '   public Int16 NPCWalkPos2;
    '   public Int16 NPCWalkMode;
    '   public Int16 u41;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string mou4; //always this. assuming an animation name
    '   public UInt32 FlagsCombat;
    '   public UInt32 FlagsCombatSVR; //im assuming this is updated after the client asks the server. there is a noticable delay between the two
    '   public Int32 u42;
    '   public Int32 u43;
    '   public Int32 u44;
    '   public Int32 u45;
    '   public Int32 u46;
    '   public UInt32 ClaimID; //the SERVER id of the player that has claim on the mob. the claim will bounce around to whomever has the most hate. not exactly the same as /assist but it will have to do.
    '   public Int32 u47;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string Animation1;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string Animation2;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string Animation3;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string Animation4;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string Animation5;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string Animation6;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string Animation7;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string Animation8;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string Animation9;
    '   public Int16 AnimationTime; //guessed, but something to do with the current animation
    '   public Int16 AnimationStep; //guessed, but something to do with the current animation
    '   public Int16 u48;
    '   public Int16 u49;
    '   public UInt32 EmoteID;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string EmoteName;
    '   public byte SpawnType;
    '   public byte u50;
    '   public Int16 u51;
    '   public byte LSColorRed;
    '   public byte LSColorGreen;
    '   public byte LSColorBlue;
    '   public byte u52;
    '   public byte u53;
    '   public byte u54;
    '   public byte CampaignMode; //boolean value. 
    '   public byte u55;
    '   public byte u56;
    '   public byte u57;
    '   public Int16 u58;
    '   public Int16 u59;
    '   public Int16 u60;
    '   public Int16 u61;
    '   public Int16 u62;
    '   public Int16 u63;
    '   public Int16 u64;
    '   [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    '   public string TalkAnimation;
    '   public Int32 u65;
    '   public Int32 u66;
    '   public UInt32 PetID; //the zone id of the spawn considered this spawns pet.
    '   public Int16 u67;
    '   public byte u68;
    '   public byte u69;
    '}
#End Region

#Region " STRUCTURES "
    ''' <summary>
    ''' Mob structure
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MobInfo
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Public EntityVTablePtr As Byte() '0
        Public LastX As Single '4
        Public LastZ As Single '8
        Public LastY As Single '12
        Public LocUnk As Single '16
        Public LocRoll As Single '20
        Public LastDirection As Single '24
        Public LocPitch As Single '28
        Public Unk01 As Single '32
        Public PosX As Single '36
        Public PosZ As Single '40
        Public PosY As Single '44
        Public LastUnk As Single '48
        Public LastRoll As Single '52
        Public PosDirection As Single '56
        Public LastPitch As Single '60
        Public Unk02 As Integer '64
        Public MoveX As Single '68
        Public MoveZ As Single '72
        Public MoveY As Single '76
        Public MoveUnk As Single '80
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=28)> _
        Public Unk03 As Byte() '84
        Public UnknownVTablePtr As Integer '112
        Public ID As Integer '116
        Public ServerCharId As Integer '120
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=28)> _
        Public MobName As String '124
        Public RunSpeed As Single '152
        Public AnimationSpeed As Single '156
        Public WarpInfo As Integer '160 WarpStruct Pointer
        Public Unk04 As Integer '164
        Public Unk05 As Integer  '168
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=44)> _
        Public Unk06 As Byte() '172
        Public distance As Single '216
        Public Unk07 As Integer '220
        Public Unk08 As Integer '224
        Public Heading As Single '228
        Public PetOwnderID As Integer '232
        Public HP_Percent As Byte '236
        Public Unk09 As Byte '237
        Public MobType As Byte '238
        Public Race As Byte '239
        Public AttackTimer As Byte '240
        Public Unk10 As Short '241
        Public Unk11 As byte '243
        Public Fade As Byte '244
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=7)> _
        Public Unk13 As Byte() '245
        Public Hair As Short '252
        Public Head As Short '254
        Public Body As Short '256
        Public Hands As Short '258
        Public Legs As Short '260
        Public Feet As Short '262
        Public MainWeapon As Short '264
        Public SubWeapon As Short '266
        Public Ranged As Short '268
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=14)> _
        Public Unk14 As Byte() '270
        Public ActionWaitTimer1 As Short '284
        Public ActionWaitTimer2 As Short '286
        Public Flags1 As Integer '288
        Public pIcon As Integer '292
        Public gIcon As Integer '296
        Public Flags4 As Integer '300
        Public Flags5 As Integer '304
        Public Unk15 As Single '308
        Public Unk16 As Integer '312
        Public Unk17 As Short '316
        Public Unk18 As Integer '318
        Public NpcSpeechLoop As Short '322
        Public NPCSpeechFrame As Short '324
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=22)> _
        Public Unk19 As Byte() '326
        Public Speed As Single '348
        Public NPCWalkPos1 As Short '352
        Public NPCWalkPos2 As Short '354
        Public NPCWalkMode As Short '356
        Public CostumeID As Short '358
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public mou4 As String '360
        Public Status As Integer '364
        Public Status2 As Integer '368
        Public StatusNpcChat As Integer '372
        Public Unk20 As Integer '376
        Public Unk21 As Integer '380
        Public Unk22 As Integer '384
        Public Unk23 As Integer '388
        Public ClaimedBy As Integer '392
        Public Unk24 As Integer '396
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation1 As String '400
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation2 As String '404
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation3 As String '408
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation4 As String '412
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation5 As String '416
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation6 As String '420
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation7 As String '424
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation8 As String '428
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation9 As String '432
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation10 As String '436
        Public AnimationTime As Short '440
        Public AnimationStep As Short '442
        Public AnimationPlay As Byte '444
        Public Unk25 As Byte '445
        Public Unk26 As Short '446
        Public Unk27 As Short '448
        Public Unk28 As Short '450
        Public EmoteID As Integer '452
        Public Unk29 As Integer '456
        Public Unk30 As Integer '460
        Public SpawnType As Integer '464
        Public LSColorRed As Byte '468
        Public LSColorGreen As Byte '469
        Public LSColorBlue As Byte '470
        Public LSUnk As Byte '471
        Public NameColor As Short '472
        <MarshalAs(UnmanagedType.I1)> _
        Public CampaignMode As Boolean '474
        Public Unk31 As Byte '475
        Public FishingTimer As Short '476
        Public FishingCastTimer As Short '478
        Public FishingUnknown0001 As Integer '480
        Public FishingUnknown0002 As Integer '484
        Public FishingUnknown0003 As Short '488
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=14)> _
        Public Unk32 As Byte() '490
        Public PCTarget As Short '504
        Public PetIndex As Short '506
        Public Unk33 As Short '508
        Public Unk34 As Byte '510
        Public BallistaScoreFlag As Byte '511
        Public PankrationEnabled As Byte '512
        Public PankrationFlagFlip As Byte '513
        Public Unk35 As Short '514
        Public ModelSize As Single '516
        Public Unk36 As Integer '520
        Public Unk37 As Short '524
        Public Unk38 As Short '526
        Public MonstrosityFlag As Short '528
        Public Unk39 As Short '530
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=36)> _
        Public MonstrosityName As String '532
    End Structure

    'Biggest. Struct. Ever.
    'im sure some of these fields could be removed with the proper packing. but im lazy.
    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure SpawnInfo
        Public EntityVTablePtr As UInt32 '0
        'possibly a signature. always seems to be this for player records. other bytes noticed here for different data types in the linked list.
        Public PredictedX As Single '4
        'These coords jump. a LOT. This leaves me to believe they are predicted values by the client.
        Public PredictedZ As Single '8
        '  Prediction is rooted in FPS games to give the user a smoother movement experience.
        Public PredictedY As Single '12
        '  Lag in whitegate will GREATLY demonstrate the properties of these values if used.
        Public locUnk As Single '16
        Public locRoll As Single '20
        Public PredictedHeading As Single '24
        Public locPitch As Single '28
        Public Unk1 As Single '32
        Public X As Single '36
        'These coords are used because it seems like a good mix between actual and predicted.
        Public Z As Single '40
        'Also note that the assinine ordering (xzy) is corrected.
        Public Y As Single '44
        Public lastUnk As Single '48
        Public lastRoll As Single '52
        Public Heading As Single '56
        'heading is expressed in radians. cos/sin will extract a normalized x/y coord.
        Public lastPitch As Single '60 
        Public Unk2 As UInt32 '64
        Public X2 As Single '68
        'These are assumed to be server confirmed (actual) coords.
        Public Z2 As Single '72
        Public Y2 As Single '76
        Public moveUnk As Single '80
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=28)> _
        Public Unk00 As byte() '84
        Public UnknownVTablePtr As UInt32 '112
        Public ZoneID As UInt32 '116
        Public ServerID As UInt32 '120
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=28)> _
        Public DisplayName As String '124
        Public RunSpeed As Single '152
        Public RunSpeed2 As Single '156
        Public pListNode As UInt32 '160
        'a pointer to the node this record belongs to in a resource linked list. note that the data in this list contains many things not just spawn data. further down the chain is unstable.
        Public Unk01 As UInt32 '164
        Public NPCTalking As UInt32 '168
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=11)> _
        Public Unk03 As UInt32() '172
        Public Distance As Single '216
        Public Unk04 As UInt32 '220
        Public Unk05 As UInt32 '224
        Public Heading2 As Single '228
        Public pPetOwner As UInt32 '232
        'only for permanent pets. charmed mobs do not fill this.
        Public HealthPercent As Byte '236
        Public Unk06 As Byte '237
        Public ModelType As Byte '238
        Public Race As Byte '239
        Public Unk07 As Byte '240
        Public Unk08 As UInt16 '241
        Public Unk09 As Byte '243
        Public ModelFade As Byte '244
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=7)> _
        Public Unk13 As Byte() '245
        Public Model As UInt16 '252
        Public ModelHead As UInt16 '254
        Public ModelBody As UInt16 '256
        Public ModelHands As UInt16 '258
        Public ModelLegs As UInt16 '260
        Public ModelFeet As UInt16 '262
        Public ModelMain As UInt16 '264
        Public ModelSub As UInt16 '266
        Public ModelRanged As UInt16 '268
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=14)> _
        Public Unk14 As Byte() '270
        Public ActionWaitTimer1 As UInt16 '284
        Public ActionWaitTimer2 As UInt16 '286
        'Added Feb 14th 2011 Patch; Fixed by Jetsam
        Public Flags1 As UInt32 '288
        'I am well aware these should be combined,
        Public Flags2 As UInt32 '292
        '  but it is easier for future discoveries
        Public Flags3 As UInt32 '296
        '  as my documentation and memory dissect
        Public Flags4 As UInt32 '300
        '  structs have these separated (to spot
        Public Flags5 As UInt32 '304
        '  flag changes after performing an action).
        Public Unk15 As Single '308
        Public Unk16 As UInt32 '312
        Public Unk17 As UInt16 '316
        Public Unk18 As UInt32 '318
        Public NPCSpeechLoop As UInt16 '322
        Public NPCSpeechFrame As UInt16 '324
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=22)> _
        Public Unk19 As Byte() '326
        Public RunSpeed3 As Single '348
        Public NPCWalkPos1 As UInt16 '352
        Public NPCWalkPos2 As UInt16 '354
        Public NPCWalkMode As UInt16 '356
        Public CostumeID As UInt16 '358
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public mou4 As String '360
        'always this. assuming an animation name
        Public FlagsCombat As UInt32 '364
        Public FlagsCombatSVR As UInt32 '368
        'im assuming this is updated after the client asks the server. there is a noticable delay between the two
        Public StatusNpcChat As UInt32 '372
        Public Unk20 As UInt32 '376
        Public Unk21 As UInt32 '380
        Public Unk22 As UInt32 '384
        Public Unk23 As UInt32 '388
        Public ClaimID As UInt32 '392
        'the SERVER id of the player that has claim on the mob. the claim will bounce around to whomever has the most hate. not exactly the same as /assist but it will have to do.
        Public Unk25 As UInt32 '396
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation1 As String '400
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation2 As String '404
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation3 As String '408
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation4 As String '412
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation5 As String '416
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation6 As String '420
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation7 As String '424
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation8 As String '428
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation9 As String '432
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=4)> _
        Public Animation10 As String '436
        Public AnimationTime As UInt16 '440
        'guessed, but something to do with the current animation
        Public AnimationStep As UInt16 '442
        'guessed, but something to do with the current animation
        Public AnimationPlay As Byte '444
        Public Unk26 As Byte '445
        Public Unk27 As UInt16 '446
        Public Unk28 As UInt16 '448
        Public Unk29 As UInt16 '450
        Public EmoteID As UInt32 '452
        Public Unk30 As Integer '456
        Public Unk31 As Integer '460
        Public SpawnType As UInt32 '464
        Public LSColorRed As Byte '468
        Public LSColorGreen As Byte '469
        Public LSColorBlue As Byte '470
        Public LSUnk As Byte '471
        Public NameColor As UInt16 '472
        <MarshalAs(UnmanagedType.I1)> _
        Public CampaignMode As Boolean '474
        'boolean value. 
        Public Unk32 As Byte '475
        Public FishingTimer As UInt16 '476
        Public FishingCastTimer As UInt16 '478
        Public FishingUnknown0001 As UInt32 '480
        Public FishingUnknown0002 As UInt32 '484
        Public FishingUnknown0003 As UInt16 '488
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=14)> _
        Public Unk33 As Byte() '490
        Public TargetIndex As UInt16 '504
        Public PetID As UInt16 '506
        'the zone id of the spawn considered this spawns pet.
        Public Unk34 As UInt16 '508
        Public Unk35 As Byte '510
        Public BallistaScoreFlag As Byte '511
        Public PankrationEnabled As Byte '512
        Public PankrationFlagFlip As Byte '513
        Public Unk36 As UInt16 '514
        Public ModelSize As Single '516
        Public Unk37 As UInt32 '520
        Public Unk38 As UInt16 '524
        Public Unk39 As UInt16 '526
        Public MonstrosityFlag As UInt16 '528
        Public Unk40 As UInt16 '530
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=36)> _
        Public MonstrosityName As String '532
    End Structure

    Public Class FilterInfo
        Public Property MapFiltered As Boolean
        Public Property OverlayFiltered As Boolean
    End Class
#End Region

#Region " MEMORY PROPERTIES "
    Private _memoryObject As Memory
    ''' <summary>
    ''' The memory object used to get the data for this mob
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <XmlIgnore()>
    Public ReadOnly Property MemoryObject() As Memory
        Get
            If _memoryObject Is Nothing Then
                _memoryObject = New Memory(POL, MobBase)
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
            If Not POL Is Nothing Then
                MemoryObject.Address = value
                MobBlock = MemoryObject.GetByteArray(568)
            End If
        End Set
    End Property

    Private _pol As Process
    ''' <summary>
    ''' The POL process to use for getting the mob data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property POL() As Process
        Get
            Return _pol
        End Get
        Set(ByVal value As Process)
            _pol = value
            If MobBase > 0 Then
                _memoryObject = New Memory(value, MobBase)
                MobBlock = MemoryObject.GetByteArray(568)
            End If
        End Set
    End Property

    Private Property MobBlock() As Byte()

    Private _lastX As Single
    ''' <summary>
    ''' Mobs last x coordinate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LastX() As Single
        Get
            'Return _lastX
            If _dataPreLoaded Then
                Return BitConverter.ToSingle(MobBlock, MobOffsets.LastX)
            Else
                MemoryObject.Address = MobBase + MobOffsets.LastX
                Return MemoryObject.GetFloat
            End If
        End Get
        Set(ByVal value As Single)
            MemoryObject.Address = MobBase + MobOffsets.LastX
            MemoryObject.SetFloat(value)
        End Set
    End Property

    Private _lastY As Single
    ''' <summary>
    ''' Mobs last Y coordinate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LastY() As Single
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToSingle(MobBlock, MobOffsets.LastY)
            Else
                MemoryObject.Address = MobBase + MobOffsets.LastY
                Return MemoryObject.GetFloat
            End If
        End Get
        Set(ByVal value As Single)
            MemoryObject.Address = MobBase + MobOffsets.LastY
            MemoryObject.SetFloat(value)
        End Set
    End Property

    Private _lastZ As Single
    ''' <summary>
    ''' mobs last Z coordinate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LastZ() As Single
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToSingle(MobBlock, MobOffsets.LastZ)
            Else
                MemoryObject.Address = MobBase + MobOffsets.LastZ
                Return MemoryObject.GetFloat
            End If
        End Get
        Set(ByVal value As Single)
            MemoryObject.Address = MobBase + MobOffsets.LastZ
            MemoryObject.SetFloat(value)
        End Set
    End Property

    Private _lastDirection As Single
    ''' <summary>
    ''' Mobs last direction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LastDirection() As Single
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToSingle(MobBlock, MobOffsets.LastDirection)
            Else
                MemoryObject.Address = MobBase + MobOffsets.LastDirection
                Return MemoryObject.GetFloat
            End If
        End Get
        Set(ByVal value As Single)
            MemoryObject.Address = MobBase + MobOffsets.LastDirection
            MemoryObject.SetFloat(value)
        End Set
    End Property

    Private _x As Single
    ''' <summary>
    ''' Mobs current x coordinate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property X() As Single
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToSingle(MobBlock, MobOffsets.X)
            Else
                MemoryObject.Address = MobBase + MobOffsets.X
                Return MemoryObject.GetFloat
            End If
        End Get
        Set(ByVal value As Single)
            MemoryObject.Address = MobBase + MobOffsets.X
            MemoryObject.SetFloat(value)
        End Set
    End Property

    Private _y As Single
    ''' <summary>
    ''' Mobs current Y coordinate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Y() As Single
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToSingle(MobBlock, MobOffsets.Y)
            Else
                MemoryObject.Address = MobBase + MobOffsets.Y
                Return MemoryObject.GetFloat
            End If
        End Get
        Set(ByVal value As Single)
            MemoryObject.Address = MobBase + MobOffsets.Y
            MemoryObject.SetFloat(value)
        End Set
    End Property

    Private _z As Single
    ''' <summary>
    ''' Mobs current Z coordinate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Z() As Single
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToSingle(MobBlock, MobOffsets.Z)
            Else
                MemoryObject.Address = MobBase + MobOffsets.Z
                Return MemoryObject.GetFloat
            End If
        End Get
        Set(ByVal value As Single)
            MemoryObject.Address = MobBase + MobOffsets.Z
            MemoryObject.SetFloat(value)
        End Set
    End Property

    Private _direction As Single
    ''' <summary>
    ''' Mobs current Direction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Direction() As Single
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToSingle(MobBlock, MobOffsets.Direction)
            Else
                MemoryObject.Address = MobBase + MobOffsets.Direction
                Return MemoryObject.GetFloat
            End If
        End Get
        Set(ByVal value As Single)
            MemoryObject.Address = MobBase + MobOffsets.Direction
            MemoryObject.SetFloat(value)
        End Set
    End Property

    Private _id As Integer
    ''' <summary>
    ''' Mobs array ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID() As Integer
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToInt32(MobBlock, MobOffsets.ID)
            Else
                MemoryObject.Address = MobBase + MobOffsets.ID
                Return MemoryObject.GetInt32
            End If
        End Get
        Set(ByVal value As Integer)
            MemoryObject.Address = MobBase + MobOffsets.ID
            MemoryObject.SetInt32(value)
        End Set
    End Property

    Private _serverID As Integer
    ''' <summary>
    ''' Mobs Server ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ServerID() As Integer
        Get
            Try
                If _dataPreLoaded Then
                    Return BitConverter.ToInt32(MobBlock, MobOffsets.ServerID)
                Else
                    MemoryObject.Address = MobBase + MobOffsets.ServerID
                    Return MemoryObject.GetInt32
                End If
            Catch
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            MemoryObject.Address = MobBase + MobOffsets.ServerID
            MemoryObject.SetInt32(value)
        End Set
    End Property

    Private _name As String
    ''' <summary>
    ''' Mobs name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name() As String
        Get
            If _dataPreLoaded Then
                If ID = &H77 Then
                    _name = String.Empty
                End If
                For i As Integer = MobOffsets.Name To MobBlock.Length - 1
                    If MobBlock(i) = 0 Then
                        _name = System.Text.Encoding.Default.GetString(MobBlock, MobOffsets.Name, i - MobOffsets.Name)
                        Exit For
                    End If
                Next
                Return _name
            Else
                MemoryObject.Address = MobBase + MobOffsets.Name
                Return MemoryObject.GetName
            End If
        End Get
    End Property

    Private _warpInfo As Integer
    ''' <summary>
    ''' Pointer to mobs warp structure
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WarpInfo() As Integer
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToInt32(MobBlock, MobOffsets.WarpInfo)
            Else
                MemoryObject.Address = MobBase + MobOffsets.WarpInfo
                Return MemoryObject.GetInt32
            End If
        End Get
    End Property

    Private _distance As Single
    ''' <summary>
    ''' Mobs distance from my position
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Distance() As Single
        Get
            If _dataPreLoaded Then
                Return Math.Sqrt(BitConverter.ToSingle(MobBlock, MobOffsets.Distance))
            Else
                MemoryObject.Address = MobBase + MobOffsets.Distance
                Return Math.Sqrt(MemoryObject.GetFloat)
            End If
        End Get
    End Property

    Private _tp As Short
    ''' <summary>
    ''' Mobs current tp percent
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TP() As Short
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToInt16(MobBlock, MobOffsets.PetTP)
            Else
                MemoryObject.Address = MobBase + MobOffsets.PetTP
                Return MemoryObject.GetShort
            End If
        End Get
    End Property

    Private _hp As Byte
    ''' <summary>
    ''' Mobs current HP percent
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HP() As Byte
        Get
            If _dataPreLoaded Then
                Return MobBlock(MobOffsets.HP)
            Else
                MemoryObject.Address = MobBase + MobOffsets.HP
                Return MemoryObject.GetByte
            End If
        End Get
    End Property

    Private _mobType As Byte
    ''' <summary>
    ''' Mobs type 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>0=PC 1=NPC 2=MOB 3=Other</remarks>
    Public ReadOnly Property MobType() As Byte
        Get
            If _dataPreLoaded Then
                Return MobBlock(MobOffsets.MobType)
            Else
                MemoryObject.Address = MobBase + MobOffsets.MobType
                Return MemoryObject.GetByte
            End If
        End Get
    End Property

    Private _race As Byte
    ''' <summary>
    ''' Mobs race
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Race() As Byte
        Get
            'Return _race
            Return MobBlock(MobOffsets.Race)
            'MemoryObject.Address = MobBase + MobOffsets.Race
            'Return MemoryObject.GetByte
        End Get
        Set(ByVal value As Byte)
            MemoryObject.Address = MobBase + MobOffsets.Race
            MemoryObject.SetByte(value)
        End Set
    End Property

    Private _attackTimer As Byte
    ''' <summary>
    ''' Mobs attack timer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This is the countdown until the next swing of the mob</remarks>
    Public ReadOnly Property AttackTimer() As Byte
        Get
            'Return _attackTimer
            Return MobBlock(MobOffsets.AttackTimer)
            'MemoryObject.Address = MobBase + MobOffsets.AttackTimer
            'Return MemoryObject.GetByte
        End Get
    End Property

    Private _face As Byte
    ''' <summary>
    ''' Mobs fade value
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Not entirely sure what this is used for, more research is needed</remarks>
    Public Property Face() As Byte
        Get
            'Return _fade
            Return MobBlock(MobOffsets.Face)
            'MemoryObject.Address = MobBase + MobOffsets.Fade
            'Return MemoryObject.GetByte
        End Get
        Set(ByVal value As Byte)
            MemoryObject.Address = MobBase + MobOffsets.Face
            MemoryObject.SetByte(value)
        End Set
    End Property

    Private _hair As Short
    ''' <summary>
    ''' Mobs hair value
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Hair() As Short
        Get
            'Return _hair
            Return BitConverter.ToInt16(MobBlock, MobOffsets.Hair)
            'MemoryObject.Address = MobBase + MobOffsets.Hair
            'Return MemoryObject.GetShort()
        End Get
        Set(ByVal value As Short)
            MemoryObject.Address = MobBase + MobOffsets.Hair
            MemoryObject.SetShort(value)
        End Set
    End Property

    Private _head As Short
    ''' <summary>
    ''' Mobs head armor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Head() As Short
        Get
            'Return _head
            Return BitConverter.ToInt16(MobBlock, MobOffsets.Head)
            'MemoryObject.Address = MobBase + MobOffsets.Head
            'Return MemoryObject.GetShort
        End Get
        Set(ByVal value As Short)
            MemoryObject.Address = MobBase + MobOffsets.Head
            MemoryObject.SetShort(value)
        End Set
    End Property

    Private _body As Short
    ''' <summary>
    ''' Mobs body armor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Body() As Short
        Get
            'Return _body
            Return BitConverter.ToInt16(MobBlock, MobOffsets.Body)
            'MemoryObject.Address = MobBase + MobOffsets.Body
            'Return MemoryObject.GetShort
        End Get
        Set(ByVal value As Short)
            MemoryObject.Address = MobBase + MobOffsets.Body
            MemoryObject.SetShort(value)
        End Set
    End Property

    Private _hands As Short
    ''' <summary>
    ''' Mobs hand armor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Hands() As Short
        Get
            'Return _hands
            Return BitConverter.ToInt16(MobBlock, MobOffsets.Hands)
            'MemoryObject.Address = MobBase + MobOffsets.Hands
            'Return MemoryObject.GetShort
        End Get
        Set(ByVal value As Short)
            MemoryObject.Address = MobBase + MobOffsets.Hands
            MemoryObject.SetShort(value)
        End Set
    End Property

    Private _legs As Short
    ''' <summary>
    ''' Mobs leg armor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Legs() As Short
        Get
            'Return _legs
            Return BitConverter.ToInt16(MobBlock, MobOffsets.Legs)
            'MemoryObject.Address = MobBase + MobOffsets.Legs
            'Return MemoryObject.GetShort
        End Get
        Set(ByVal value As Short)
            MemoryObject.Address = MobBase + MobOffsets.Legs
            MemoryObject.SetShort(value)
        End Set
    End Property

    Private _feet As Short
    ''' <summary>
    ''' Mobs feet armor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Feet() As Short
        Get
            'Return _feet
            Return BitConverter.ToInt16(MobBlock, MobOffsets.Feet)
            'MemoryObject.Address = MobBase + MobOffsets.Feet
            'Return MemoryObject.GetShort
        End Get
        Set(ByVal value As Short)
            MemoryObject.Address = MobBase + MobOffsets.Feet
            MemoryObject.SetShort(value)
        End Set
    End Property

    Private _mainWeapon As Short
    ''' <summary>
    ''' Mobs main weapon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainWeapon() As Short
        Get
            'Return _mainWeapon
            Return BitConverter.ToInt16(MobBlock, MobOffsets.MainWeapon)
            'MemoryObject.Address = MobBase + MobOffsets.MainWeapon
            'Return MemoryObject.GetShort
        End Get
        Set(ByVal value As Short)
            MemoryObject.Address = MobBase + MobOffsets.MainWeapon
            MemoryObject.SetShort(value)
        End Set
    End Property

    Private _subWeapon As Short
    ''' <summary>
    ''' Mobs sub weapon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SubWeapon() As Short
        Get
            'Return _subWeapon
            Return BitConverter.ToInt16(MobBlock, MobOffsets.SubWeapon)
            'MemoryObject.Address = MobBase + MobOffsets.SubWeapon
            'Return MemoryObject.GetShort
        End Get
        Set(ByVal value As Short)
            MemoryObject.Address = MobBase + MobOffsets.SubWeapon
            MemoryObject.SetShort(value)
        End Set
    End Property

    Private _pIcon As Byte
    ''' <summary>
    ''' Mobs player icon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PIcon() As Integer
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToInt32(MobBlock, MobOffsets.PIcon)
            Else
                MemoryObject.Address = MobBase + MobOffsets.PIcon
                Return MemoryObject.GetInt32
            End If
        End Get
        Set(ByVal value As Integer)
            MemoryObject.Address = MobBase + MobOffsets.PIcon
            MemoryObject.SetInt32(value)
        End Set
    End Property

    Private _gIcon As Short
    ''' <summary>
    ''' Mobs GM Icon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GIcon() As Integer
        Get
            'Return _gIcon
            Return BitConverter.ToInt32(MobBlock, MobOffsets.GIcon)
            'MemoryObject.Address = MobBase + MobOffsets.GIcon
            'Return MemoryObject.GetShort
        End Get
        Set(ByVal value As Integer)
            MemoryObject.Address = MobBase + MobOffsets.GIcon
            MemoryObject.SetInt32(value)
        End Set
    End Property

    Private _speed As Single
    ''' <summary>
    ''' Mobs speed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Speed() As Single
        Get
            'Return _speed
            Return BitConverter.ToSingle(MobBlock, MobOffsets.Speed)
            'MemoryObject.Address = MobBase + MobOffsets.Speed
            'Return MemoryObject.GetFloat
        End Get
        Set(ByVal value As Single)
            MemoryObject.Address = MobBase + MobOffsets.Speed
            MemoryObject.SetFloat(value)
        End Set
    End Property

    Private _status As Integer
    ''' <summary>
    ''' Mobs status
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status() As Integer
        Get
            'Return _status
            Return BitConverter.ToInt32(MobBlock, MobOffsets.Status)
            'MemoryObject.Address = MobBase + MobOffsets.Status
            'Return MemoryObject.GetInt32
        End Get
        Set(ByVal value As Integer)
            MemoryObject.Address = MobBase + MobOffsets.Status
            MemoryObject.SetInt32(value)
        End Set
    End Property

    Private _status2 As Integer
    ''' <summary>
    ''' Mobs status2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status2() As Integer
        Get
            'Return _status2
            'MemoryObject.Address = MobBase + MobOffsets.Status2
            'Return MemoryObject.GetInt32
        End Get
        Set(ByVal value As Integer)
            MemoryObject.Address = MobBase + MobOffsets.Status2
            MemoryObject.SetInt32(value)
        End Set
    End Property

    Private _claimedBy As Integer
    ''' <summary>
    ''' ServerId of the player that has the mob claimed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ClaimedBy() As Integer
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToInt32(MobBlock, MobOffsets.ClaimedBy)
            Else
                MemoryObject.Address = MobBase + MobOffsets.ClaimedBy
                Return MemoryObject.GetInt32
            End If

        End Get
    End Property

    Public ReadOnly Property SpawnType As SpawnTypes
        Get
            If _dataPreLoaded Then
                Return [Enum].Parse(GetType(SpawnTypes), MobBlock(MobOffsets.SpawnType))
            Else
                MemoryObject.Address = MobBase + MobOffsets.SpawnType
                Return [Enum].Parse(GetType(SpawnTypes), MemoryObject.GetInt32())
            End If
        End Get
    End Property

    Private _petIndex As Short
    ''' <summary>
    ''' Mobs pet index
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PetIndex() As Short
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToInt16(MobBlock, MobOffsets.PetIndex)
            Else
                MemoryObject.Address = MobBase + MobOffsets.PetIndex
                Return MemoryObject.GetShort
            End If
        End Get
    End Property

    Public ReadOnly Property PCTarget As Short
        Get
            If _dataPreLoaded Then
                Return BitConverter.ToInt16(MobBlock, MobOffsets.PCTarget)
            Else
                MemoryObject.Address = MobBase + MobOffsets.PCTarget
                Return MemoryObject.GetShort
            End If
        End Get
    End Property


#End Region

#Region " MAP PROPERTIES "
    ''' <summary>
    ''' The X Distance of the mob in relation to my position on the radar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property XDistance() As Single
    ''' <summary>
    ''' The Y Distance of the mob in relation to my position on the radar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property YDistance() As Single
    ''' <summary>
    ''' The Z Distance of the mob in relation to my position on the radar
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ZDistance() As Single
    ''' <summary>
    ''' The mobs degree rotation on the radar from 0 and in relation to my direction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Degrees() As Single
    ''' <summary>
    ''' The radius of the mobs rotation circle on the radar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Radius() As Single
    ''' <summary>
    ''' The mobs X position on the radar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MapX() As Single
    ''' <summary>
    ''' the mobs Y position on the radar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MapY() As Single
#End Region

#Region " DISPLAY PROPERTIES "
    Public Property MobIsDead As Boolean
    Private _filters As FilterInfo
    Public ReadOnly Property Filters As FilterInfo
        Get
            If _filters Is Nothing Then
                _filters = New FilterInfo With {.MapFiltered = False, .OverlayFiltered = False}
            End If
            Return _filters
        End Get
    End Property

    Public Property OverlayFiltered As Boolean
    Public Property MapFiltered As Boolean

#End Region

#Region " PRIVATE METHODS "
    Private Sub LoadData()
        'If _isPacket Then
        '    _x = _mobPacket.X
        '    _y = _mobPacket.Y
        '    _z = _mobPacket.Z
        '    _direction = _mobPacket.Direction
        '    _hp = _mobPacket.HP
        '    _id = _mobPacket.ID
        '    _serverID = _mobPacket.ServerID
        '    _mobType = MobTypes.NPC
        '    _name = _zones.GetMobName(_mapID, _mobPacket.ServerID)
        '    _warpInfo = 100
        'Else
        _lastX = BitConverter.ToSingle(MobBlock, MobOffsets.LastX)
        _lastY = BitConverter.ToSingle(MobBlock, MobOffsets.LastY)
        _lastZ = BitConverter.ToSingle(MobBlock, MobOffsets.LastZ)
        _lastDirection = BitConverter.ToSingle(MobBlock, MobOffsets.LastDirection)
        _x = BitConverter.ToSingle(MobBlock, MobOffsets.X)
        _y = BitConverter.ToSingle(MobBlock, MobOffsets.Y)
        _z = BitConverter.ToSingle(MobBlock, MobOffsets.Z)
        _direction = BitConverter.ToSingle(MobBlock, MobOffsets.Direction)
        _id = BitConverter.ToInt32(MobBlock, MobOffsets.ID)
        _serverID = BitConverter.ToInt32(MobBlock, MobOffsets.ServerID)
        For i As Integer = MobOffsets.Name To MobBlock.Length - 1
            If MobBlock(i) = 0 Then
                _name = System.Text.Encoding.Default.GetString(MobBlock, MobOffsets.Name, i - MobOffsets.Name)
                Exit For
            End If
        Next
        _warpInfo = BitConverter.ToInt32(MobBlock, MobOffsets.WarpInfo)
        _distance = Math.Sqrt(BitConverter.ToSingle(MobBlock, MobOffsets.Distance))
        _tp = BitConverter.ToInt32(MobBlock, MobOffsets.PetTP)
        _hp = MobBlock(MobOffsets.HP)
        If MobBlock(MobOffsets.MobType) > 0 Then
            _mobType = MobTypes.NPC
        Else
            _mobType = MobTypes.PC
        End If
        _race = MobBlock(MobOffsets.Race)
        _attackTimer = MobBlock(MobOffsets.AttackTimer)
        _face = MobBlock(MobOffsets.Face)
        _hair = BitConverter.ToInt16(MobBlock, MobOffsets.Hair)
        _head = BitConverter.ToInt16(MobBlock, MobOffsets.Head)
        _body = BitConverter.ToInt16(MobBlock, MobOffsets.Body)
        _hands = BitConverter.ToInt16(MobBlock, MobOffsets.Hands)
        _legs = BitConverter.ToInt16(MobBlock, MobOffsets.Legs)
        _feet = BitConverter.ToInt16(MobBlock, MobOffsets.Feet)
        _mainWeapon = BitConverter.ToInt16(MobBlock, MobOffsets.MainWeapon)
        _subWeapon = BitConverter.ToInt16(MobBlock, MobOffsets.SubWeapon)
        _pIcon = BitConverter.ToInt32(MobBlock, MobOffsets.PIcon)
        _gIcon = BitConverter.ToInt32(MobBlock, MobOffsets.GIcon)
        _speed = BitConverter.ToSingle(MobBlock, MobOffsets.Speed)
        _status = BitConverter.ToInt32(MobBlock, MobOffsets.Status)
        _status2 = BitConverter.ToInt32(MobBlock, MobOffsets.Status2)
        _claimedBy = BitConverter.ToInt32(MobBlock, MobOffsets.ClaimedBy)
        _petIndex = BitConverter.ToInt16(MobBlock, MobOffsets.PetIndex)
        'End If
    End Sub

    Private Function GetDistance(ByVal ReferencePoint As Point, ByVal MobLocation As Point) As Single
        Return Math.Sqrt((MobLocation.X - ReferencePoint.X) ^ 2 + (MobLocation.Y - ReferencePoint.Y) ^ 2)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Not Me.MobBlock Is Nothing Then
                    Erase MobBlock
                End If
            End If
            MobBlock = Nothing

        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
