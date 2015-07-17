<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AlphaOverlayRadarForm
    Inherits RadarControls.LayeredForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AlphaOverlayRadarForm))
        Dim RadarSettings1 As RadarControls.RadarSettings = New RadarControls.RadarSettings()
        Me.OverlayRadar = New RadarControls.AlphaRadarRenderer()
        Me.cmRadarMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RadarItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cHideFloors = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowTargetInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsShowHeaderInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cHideInCombat = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cShowNPC = New System.Windows.Forms.ToolStripMenuItem()
        Me.cHideObjectsAndDoors = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowMobs = New System.Windows.Forms.ToolStripMenuItem()
        Me.xShowCamped = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowNPCNames = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowMobDirection = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cShowPC = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowPCNames = New System.Windows.Forms.ToolStripMenuItem()
        Me.cPartyMembersOnly = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.cShowDistance = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowHP = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowCompass = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowID = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.cBlip = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowTrackerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RangesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cAggro = New System.Windows.Forms.ToolStripMenuItem()
        Me.cJobAbility = New System.Windows.Forms.ToolStripMenuItem()
        Me.cSpell = New System.Windows.Forms.ToolStripMenuItem()
        Me.cVisible = New System.Windows.Forms.ToolStripMenuItem()
        Me.cAddRange = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsFilters = New System.Windows.Forms.ToolStripMenuItem()
        Me.xClickThrough = New System.Windows.Forms.ToolStripMenuItem()
        Me.cAlwaysShowTarget = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cSetFont = New System.Windows.Forms.ToolStripMenuItem()
        Me.cSetColors = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.cSaveSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.cSaveSettingsAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cLoadSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowSettingsDialog = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ApExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.VisibleTimer = New System.Windows.Forms.Timer(Me.components)
        Me.cmRadarMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'OverlayRadar
        '
        Me.OverlayRadar.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed
        Me.OverlayRadar.CurrentMapEntry = Nothing
        Me.OverlayRadar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.OverlayRadar.LinkServerRunning = False
        Me.OverlayRadar.MapData = Nothing
        Me.OverlayRadar.MapMatrix = Nothing
        Me.OverlayRadar.MapPath = "Maps"
        Me.OverlayRadar.NMList = CType(resources.GetObject("OverlayRadar.NMList"), System.Collections.Generic.List(Of String))
        Me.OverlayRadar.ProEnabled = False
        Me.OverlayRadar.RadarType = RadarControls.AlphaRadarRenderer.RadarTypes.Overlay
        RadarSettings1.AllianceColorHtml = "Blue"
        RadarSettings1.BlipSize = 4
        RadarSettings1.CampedColor = System.Drawing.Color.SeaGreen
        RadarSettings1.CampedColorHtml = "SeaGreen"
        RadarSettings1.ClickThrough = False
        RadarSettings1.CurrentMap = CType(-1, Short)
        RadarSettings1.CustomRanges = Nothing
        RadarSettings1.DataFontName = resources.GetString("RadarSettings1.DataFontName")
        RadarSettings1.DisableDocking = False
        RadarSettings1.DisableDragging = False
        RadarSettings1.DrawLineToTarget = False
        RadarSettings1.Font = Nothing
        RadarSettings1.HideInfoInCombat = True
        RadarSettings1.HideOtherFloors = False
        RadarSettings1.LinkColorHtml = "Red"
        RadarSettings1.Location = New System.Drawing.Point(100, 100)
        RadarSettings1.MapLevel = CType(-1, Short)
        RadarSettings1.MapOpacity = 0.8!
        RadarSettings1.MobColorHtml = "Red"
        RadarSettings1.NMColorHtml = "HotPink"
        RadarSettings1.NPCColorHtml = "Red"
        RadarSettings1.NPCFilter = ""
        RadarSettings1.NPCFilterType = RadarControls.RadarSettings.FilterType.None
        RadarSettings1.PartyColorHtml = "Blue"
        RadarSettings1.PCColorHtml = "Blue"
        RadarSettings1.PCFilter = ""
        RadarSettings1.PCFilterType = RadarControls.RadarSettings.FilterType.None
        RadarSettings1.RangeDisplay = RadarControls.RadarSettings.RangeType.Solid
        RadarSettings1.RefreshRate = 100
        RadarSettings1.ShowAggro = False
        RadarSettings1.ShowAll = False
        RadarSettings1.ShowAllies = False
        RadarSettings1.ShowCompass = False
        RadarSettings1.ShowDistance = False
        RadarSettings1.ShowFilterPanel = False
        RadarSettings1.ShowHP = False
        RadarSettings1.ShowId = False
        RadarSettings1.ShowJobAbility = False
        RadarSettings1.ShowMobs = False
        RadarSettings1.ShowMyPointer = True
        RadarSettings1.ShowNPC = False
        RadarSettings1.ShowNPCNames = False
        RadarSettings1.ShowPartyMembers = False
        RadarSettings1.ShowPC = False
        RadarSettings1.ShowPCNames = False
        RadarSettings1.ShowPOS = False
        RadarSettings1.ShowSight = False
        RadarSettings1.ShowSpell = False
        RadarSettings1.ShowTargetInfo = False
        RadarSettings1.ShowTracker = False
        RadarSettings1.ShowVisibleRange = False
        RadarSettings1.Size = New System.Drawing.Size(512, 512)
        RadarSettings1.StayOnTop = False
        RadarSettings1.TargetHighlightColorHtml = "Black"
        RadarSettings1.TrackedMob = 0
        RadarSettings1.Transparency = 1.0!
        RadarSettings1.Zoom = 1.0!
        Me.OverlayRadar.Settings = RadarSettings1
        Me.OverlayRadar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed
        Me.OverlayRadar.TextRendering = System.Drawing.Text.TextRenderingHint.SystemDefault
        '
        'cmRadarMenu
        '
        Me.cmRadarMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RadarItemsToolStripMenuItem, Me.RangesToolStripMenuItem1, Me.tsFilters, Me.xClickThrough, Me.cAlwaysShowTarget, Me.ToolStripSeparator2, Me.cSetFont, Me.cSetColors, Me.ToolStripSeparator12, Me.cSaveSettings, Me.cSaveSettingsAs, Me.cLoadSettings, Me.cShowSettingsDialog, Me.ToolStripSeparator3, Me.ApExit})
        Me.cmRadarMenu.Name = "ContextMenuStrip1"
        Me.cmRadarMenu.Size = New System.Drawing.Size(241, 308)
        '
        'RadarItemsToolStripMenuItem
        '
        Me.RadarItemsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cHideFloors, Me.cShowTargetInfo, Me.tsShowHeaderInfo, Me.cHideInCombat, Me.ToolStripSeparator1, Me.cShowNPC, Me.cHideObjectsAndDoors, Me.cShowMobs, Me.xShowCamped, Me.cShowNPCNames, Me.cShowMobDirection, Me.ToolStripSeparator5, Me.cShowPC, Me.cShowPCNames, Me.cPartyMembersOnly, Me.ToolStripSeparator6, Me.cShowDistance, Me.cShowHP, Me.cShowCompass, Me.cShowID, Me.cShowAll, Me.ToolStripSeparator7, Me.cBlip, Me.ShowTrackerToolStripMenuItem})
        Me.RadarItemsToolStripMenuItem.Name = "RadarItemsToolStripMenuItem"
        Me.RadarItemsToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.RadarItemsToolStripMenuItem.Text = "Radar Items"
        '
        'cHideFloors
        '
        Me.cHideFloors.CheckOnClick = True
        Me.cHideFloors.Name = "cHideFloors"
        Me.cHideFloors.Size = New System.Drawing.Size(251, 22)
        Me.cHideFloors.Text = "Hide Mobs on Other Floors"
        '
        'cShowTargetInfo
        '
        Me.cShowTargetInfo.CheckOnClick = True
        Me.cShowTargetInfo.Name = "cShowTargetInfo"
        Me.cShowTargetInfo.Size = New System.Drawing.Size(251, 22)
        Me.cShowTargetInfo.Text = "Target Info"
        '
        'tsShowHeaderInfo
        '
        Me.tsShowHeaderInfo.CheckOnClick = True
        Me.tsShowHeaderInfo.Name = "tsShowHeaderInfo"
        Me.tsShowHeaderInfo.Size = New System.Drawing.Size(251, 22)
        Me.tsShowHeaderInfo.Text = "Header Bar Info"
        '
        'cHideInCombat
        '
        Me.cHideInCombat.CheckOnClick = True
        Me.cHideInCombat.Name = "cHideInCombat"
        Me.cHideInCombat.Size = New System.Drawing.Size(251, 22)
        Me.cHideInCombat.Text = "Hide Target Info Panel in Combat"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(248, 6)
        '
        'cShowNPC
        '
        Me.cShowNPC.CheckOnClick = True
        Me.cShowNPC.Name = "cShowNPC"
        Me.cShowNPC.Size = New System.Drawing.Size(251, 22)
        Me.cShowNPC.Text = "NPC"
        '
        'cHideObjectsAndDoors
        '
        Me.cHideObjectsAndDoors.CheckOnClick = True
        Me.cHideObjectsAndDoors.Name = "cHideObjectsAndDoors"
        Me.cHideObjectsAndDoors.Size = New System.Drawing.Size(251, 22)
        Me.cHideObjectsAndDoors.Text = "Hide Objects and Doors"
        '
        'cShowMobs
        '
        Me.cShowMobs.CheckOnClick = True
        Me.cShowMobs.Name = "cShowMobs"
        Me.cShowMobs.Size = New System.Drawing.Size(251, 22)
        Me.cShowMobs.Text = "Mobs"
        '
        'xShowCamped
        '
        Me.xShowCamped.CheckOnClick = True
        Me.xShowCamped.Name = "xShowCamped"
        Me.xShowCamped.Size = New System.Drawing.Size(251, 22)
        Me.xShowCamped.Text = "Camped Mobs"
        '
        'cShowNPCNames
        '
        Me.cShowNPCNames.CheckOnClick = True
        Me.cShowNPCNames.Name = "cShowNPCNames"
        Me.cShowNPCNames.Size = New System.Drawing.Size(251, 22)
        Me.cShowNPCNames.Text = "NPC Names"
        '
        'cShowMobDirection
        '
        Me.cShowMobDirection.CheckOnClick = True
        Me.cShowMobDirection.Name = "cShowMobDirection"
        Me.cShowMobDirection.Size = New System.Drawing.Size(251, 22)
        Me.cShowMobDirection.Text = "NPC Direction"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(248, 6)
        '
        'cShowPC
        '
        Me.cShowPC.CheckOnClick = True
        Me.cShowPC.Name = "cShowPC"
        Me.cShowPC.Size = New System.Drawing.Size(251, 22)
        Me.cShowPC.Text = "PC"
        '
        'cShowPCNames
        '
        Me.cShowPCNames.CheckOnClick = True
        Me.cShowPCNames.Name = "cShowPCNames"
        Me.cShowPCNames.Size = New System.Drawing.Size(251, 22)
        Me.cShowPCNames.Text = "PC Names"
        '
        'cPartyMembersOnly
        '
        Me.cPartyMembersOnly.CheckOnClick = True
        Me.cPartyMembersOnly.Name = "cPartyMembersOnly"
        Me.cPartyMembersOnly.Size = New System.Drawing.Size(251, 22)
        Me.cPartyMembersOnly.Text = "Party Members Only"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(248, 6)
        '
        'cShowDistance
        '
        Me.cShowDistance.CheckOnClick = True
        Me.cShowDistance.Name = "cShowDistance"
        Me.cShowDistance.Size = New System.Drawing.Size(251, 22)
        Me.cShowDistance.Text = "Distance"
        '
        'cShowHP
        '
        Me.cShowHP.CheckOnClick = True
        Me.cShowHP.Name = "cShowHP"
        Me.cShowHP.Size = New System.Drawing.Size(251, 22)
        Me.cShowHP.Text = "HP"
        '
        'cShowCompass
        '
        Me.cShowCompass.CheckOnClick = True
        Me.cShowCompass.Name = "cShowCompass"
        Me.cShowCompass.Size = New System.Drawing.Size(251, 22)
        Me.cShowCompass.Text = "Compass"
        '
        'cShowID
        '
        Me.cShowID.CheckOnClick = True
        Me.cShowID.Name = "cShowID"
        Me.cShowID.Size = New System.Drawing.Size(251, 22)
        Me.cShowID.Text = "Mob ID"
        '
        'cShowAll
        '
        Me.cShowAll.CheckOnClick = True
        Me.cShowAll.Name = "cShowAll"
        Me.cShowAll.Size = New System.Drawing.Size(251, 22)
        Me.cShowAll.Text = "All Mobs"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(248, 6)
        '
        'cBlip
        '
        Me.cBlip.Name = "cBlip"
        Me.cBlip.Size = New System.Drawing.Size(251, 22)
        Me.cBlip.Text = "Blip Size"
        '
        'ShowTrackerToolStripMenuItem
        '
        Me.ShowTrackerToolStripMenuItem.Name = "ShowTrackerToolStripMenuItem"
        Me.ShowTrackerToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
        Me.ShowTrackerToolStripMenuItem.Text = "Tracker"
        '
        'RangesToolStripMenuItem1
        '
        Me.RangesToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cAggro, Me.cJobAbility, Me.cSpell, Me.cVisible, Me.cAddRange})
        Me.RangesToolStripMenuItem1.Name = "RangesToolStripMenuItem1"
        Me.RangesToolStripMenuItem1.Size = New System.Drawing.Size(240, 22)
        Me.RangesToolStripMenuItem1.Text = "Ranges"
        '
        'cAggro
        '
        Me.cAggro.CheckOnClick = True
        Me.cAggro.Name = "cAggro"
        Me.cAggro.Size = New System.Drawing.Size(166, 22)
        Me.cAggro.Text = "Aggro Range"
        '
        'cJobAbility
        '
        Me.cJobAbility.CheckOnClick = True
        Me.cJobAbility.Name = "cJobAbility"
        Me.cJobAbility.Size = New System.Drawing.Size(166, 22)
        Me.cJobAbility.Text = "Job Ability Range"
        '
        'cSpell
        '
        Me.cSpell.CheckOnClick = True
        Me.cSpell.Name = "cSpell"
        Me.cSpell.Size = New System.Drawing.Size(166, 22)
        Me.cSpell.Text = "Spell Range"
        '
        'cVisible
        '
        Me.cVisible.Name = "cVisible"
        Me.cVisible.Size = New System.Drawing.Size(166, 22)
        Me.cVisible.Text = "Visible Range"
        '
        'cAddRange
        '
        Me.cAddRange.Name = "cAddRange"
        Me.cAddRange.Size = New System.Drawing.Size(166, 22)
        Me.cAddRange.Text = "Custom Ranges..."
        '
        'tsFilters
        '
        Me.tsFilters.Name = "tsFilters"
        Me.tsFilters.Size = New System.Drawing.Size(240, 22)
        Me.tsFilters.Text = "Filters"
        '
        'xClickThrough
        '
        Me.xClickThrough.CheckOnClick = True
        Me.xClickThrough.Name = "xClickThrough"
        Me.xClickThrough.Size = New System.Drawing.Size(240, 22)
        Me.xClickThrough.Text = "Click Through"
        '
        'cAlwaysShowTarget
        '
        Me.cAlwaysShowTarget.CheckOnClick = True
        Me.cAlwaysShowTarget.Name = "cAlwaysShowTarget"
        Me.cAlwaysShowTarget.Size = New System.Drawing.Size(240, 22)
        Me.cAlwaysShowTarget.Text = "Always Show Target"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(237, 6)
        '
        'cSetFont
        '
        Me.cSetFont.Name = "cSetFont"
        Me.cSetFont.Size = New System.Drawing.Size(240, 22)
        Me.cSetFont.Text = "Font"
        '
        'cSetColors
        '
        Me.cSetColors.Name = "cSetColors"
        Me.cSetColors.Size = New System.Drawing.Size(240, 22)
        Me.cSetColors.Text = "Colors"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(237, 6)
        '
        'cSaveSettings
        '
        Me.cSaveSettings.Name = "cSaveSettings"
        Me.cSaveSettings.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.cSaveSettings.Size = New System.Drawing.Size(240, 22)
        Me.cSaveSettings.Text = "Save Settings..."
        '
        'cSaveSettingsAs
        '
        Me.cSaveSettingsAs.Name = "cSaveSettingsAs"
        Me.cSaveSettingsAs.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.cSaveSettingsAs.Size = New System.Drawing.Size(240, 22)
        Me.cSaveSettingsAs.Text = "Save Settings As..."
        '
        'cLoadSettings
        '
        Me.cLoadSettings.Name = "cLoadSettings"
        Me.cLoadSettings.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cLoadSettings.Size = New System.Drawing.Size(240, 22)
        Me.cLoadSettings.Text = "Load Settings"
        '
        'cShowSettingsDialog
        '
        Me.cShowSettingsDialog.Name = "cShowSettingsDialog"
        Me.cShowSettingsDialog.Size = New System.Drawing.Size(240, 22)
        Me.cShowSettingsDialog.Text = "Show Settings Designer"
        Me.cShowSettingsDialog.Visible = False
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(237, 6)
        '
        'ApExit
        '
        Me.ApExit.Name = "ApExit"
        Me.ApExit.Size = New System.Drawing.Size(240, 22)
        Me.ApExit.Text = "Close"
        '
        'VisibleTimer
        '
        Me.VisibleTimer.Interval = 250
        '
        'AlphaOverlayRadarForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(512, 512)
        Me.ContextMenuStrip = Me.cmRadarMenu
        Me.Draggable = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Layered = True
        Me.Name = "AlphaOverlayRadarForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "AlphaOverlayRadar"
        Me.cmRadarMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OverlayRadar As RadarControls.AlphaRadarRenderer
    Friend WithEvents cmRadarMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RadarItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cHideFloors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowTargetInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsShowHeaderInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cHideInCombat As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cShowNPC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowMobs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowNPCNames As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowMobDirection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents xShowCamped As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cShowPC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowPCNames As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cPartyMembersOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cShowDistance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowHP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowCompass As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowID As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cBlip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowTrackerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RangesToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cAggro As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cJobAbility As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cSpell As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cVisible As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cAddRange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsFilters As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents xClickThrough As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cSetFont As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cSetColors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cSaveSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cSaveSettingsAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cLoadSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowSettingsDialog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ApExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VisibleTimer As System.Windows.Forms.Timer
    Friend WithEvents cHideObjectsAndDoors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cAlwaysShowTarget As System.Windows.Forms.ToolStripMenuItem
End Class
