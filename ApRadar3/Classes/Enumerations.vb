
Public Enum DockMode As Integer
    Left = &H0S
    Top = &H1S
    Right = &H2S
    Bottom = &H3S
    None = -1
End Enum

Public Enum AnimateWindowFlags
    AW_HOR_POSITIVE = &H1
    AW_HOR_NEGATIVE = &H2
    AW_VER_POSITIVE = &H4
    AW_VER_NEGATIVE = &H8
    AW_CENTER = &H10
    AW_HIDE = &H10000
    AW_ACTIVATE = &H20000
    AW_SLIDE = &H40000
    AW_BLEND = &H80000
End Enum

Public Enum SlideDirection
    Up
    Down
    Left
    Right
    DiagonalFromTopLeft
    DiagonalFromTopRight
    DiagonalFromBottomLeft
    DiagonalFromBottomRight
End Enum

