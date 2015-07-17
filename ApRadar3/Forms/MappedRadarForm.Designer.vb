<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MappedRadarForm
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MappedRadarForm))
        Dim RadarSettings1 As RadarControls.RadarSettings = New RadarControls.RadarSettings()
        Me.cmMapRadar = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowHide = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.xLinkedRadars = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.RadarItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cSelectPack = New System.Windows.Forms.ToolStripMenuItem()
        Me.cMapPackSelect = New System.Windows.Forms.ToolStripMenuItem()
        Me.xHideFloors = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cShowNPC = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowMobs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowNPCNames = New System.Windows.Forms.ToolStripMenuItem()
        Me.xShowCampedMobs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cEditNMList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cShowPC = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowPCNames = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowParty = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.cShowPOS = New System.Windows.Forms.ToolStripMenuItem()
        Me.cDistance = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowHP = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.cShowAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowID = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cOnTop = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowFilterPanel = New System.Windows.Forms.ToolStripMenuItem()
        Me.cDocking = New System.Windows.Forms.ToolStripMenuItem()
        Me.cDragging = New System.Windows.Forms.ToolStripMenuItem()
        Me.cClickThrough = New System.Windows.Forms.ToolStripMenuItem()
        Me.cRanges = New System.Windows.Forms.ToolStripMenuItem()
        Me.cRingOnly = New System.Windows.Forms.ToolStripMenuItem()
        Me.cAggro = New System.Windows.Forms.ToolStripMenuItem()
        Me.cJobAbility = New System.Windows.Forms.ToolStripMenuItem()
        Me.cSpellCasting = New System.Windows.Forms.ToolStripMenuItem()
        Me.cVisible = New System.Windows.Forms.ToolStripMenuItem()
        Me.cCustomRanges = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.cSetFont = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.TransparencyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.p100 = New System.Windows.Forms.ToolStripMenuItem()
        Me.p80 = New System.Windows.Forms.ToolStripMenuItem()
        Me.p60 = New System.Windows.Forms.ToolStripMenuItem()
        Me.p40 = New System.Windows.Forms.ToolStripMenuItem()
        Me.p20 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.xSaveSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.xSaveSettingsAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.xLoadSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.cShowSettingsDesigner = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.cResetPosition = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActiveTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MapRadar = New RadarControls.Radar()
        Me.cmMapRadar.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmMapRadar
        '
        Me.cmMapRadar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowHide, Me.ToolStripSeparator10, Me.xLinkedRadars, Me.ToolStripSeparator2, Me.RadarItemsToolStripMenuItem, Me.OptionsToolStripMenuItem1, Me.cRanges, Me.ToolStripSeparator9, Me.cSetFont, Me.ToolStripMenuItem2, Me.ToolStripSeparator12, Me.TransparencyToolStripMenuItem, Me.ToolStripSeparator13, Me.xSaveSettings, Me.xSaveSettingsAs, Me.xLoadSettings, Me.cShowSettingsDesigner, Me.ToolStripSeparator4, Me.cResetPosition, Me.ApExit})
        Me.cmMapRadar.Name = "ContextMenuStrip1"
        Me.cmMapRadar.Size = New System.Drawing.Size(198, 348)
        '
        'ShowHide
        '
        Me.ShowHide.Name = "ShowHide"
        Me.ShowHide.Size = New System.Drawing.Size(197, 22)
        Me.ShowHide.Text = "Hide Mapped Radar"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(194, 6)
        '
        'xLinkedRadars
        '
        Me.xLinkedRadars.Name = "xLinkedRadars"
        Me.xLinkedRadars.Size = New System.Drawing.Size(197, 22)
        Me.xLinkedRadars.Text = "Linked Radars (Beta)"
        Me.xLinkedRadars.ToolTipText = "Start a Linked Radar server or connect to an existing Linked Radar Server."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(194, 6)
        '
        'RadarItemsToolStripMenuItem
        '
        Me.RadarItemsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cSelectPack, Me.xHideFloors, Me.ToolStripSeparator1, Me.cShowNPC, Me.cShowMobs, Me.cShowNPCNames, Me.xShowCampedMobs, Me.cEditNMList, Me.ToolStripSeparator5, Me.cShowPC, Me.cShowPCNames, Me.cShowParty, Me.ToolStripSeparator6, Me.cShowPOS, Me.cDistance, Me.cShowHP, Me.ToolStripSeparator7, Me.cShowAll, Me.cShowID})
        Me.RadarItemsToolStripMenuItem.Name = "RadarItemsToolStripMenuItem"
        Me.RadarItemsToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.RadarItemsToolStripMenuItem.Text = "Radar Items"
        '
        'cSelectPack
        '
        Me.cSelectPack.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cMapPackSelect})
        Me.cSelectPack.Name = "cSelectPack"
        Me.cSelectPack.Size = New System.Drawing.Size(217, 22)
        Me.cSelectPack.Text = "Select Map Pack"
        '
        'cMapPackSelect
        '
        Me.cMapPackSelect.Checked = True
        Me.cMapPackSelect.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cMapPackSelect.Name = "cMapPackSelect"
        Me.cMapPackSelect.Size = New System.Drawing.Size(112, 22)
        Me.cMapPackSelect.Tag = "Default"
        Me.cMapPackSelect.Text = "Default"
        '
        'xHideFloors
        '
        Me.xHideFloors.CheckOnClick = True
        Me.xHideFloors.Name = "xHideFloors"
        Me.xHideFloors.Size = New System.Drawing.Size(217, 22)
        Me.xHideFloors.Text = "Hide Mobs on Other Floors"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(214, 6)
        '
        'cShowNPC
        '
        Me.cShowNPC.CheckOnClick = True
        Me.cShowNPC.Name = "cShowNPC"
        Me.cShowNPC.Size = New System.Drawing.Size(217, 22)
        Me.cShowNPC.Text = "NPC"
        '
        'cShowMobs
        '
        Me.cShowMobs.CheckOnClick = True
        Me.cShowMobs.Name = "cShowMobs"
        Me.cShowMobs.Size = New System.Drawing.Size(217, 22)
        Me.cShowMobs.Text = "Mobs"
        '
        'cShowNPCNames
        '
        Me.cShowNPCNames.CheckOnClick = True
        Me.cShowNPCNames.Name = "cShowNPCNames"
        Me.cShowNPCNames.Size = New System.Drawing.Size(217, 22)
        Me.cShowNPCNames.Text = "NPC Names"
        '
        'xShowCampedMobs
        '
        Me.xShowCampedMobs.CheckOnClick = True
        Me.xShowCampedMobs.Name = "xShowCampedMobs"
        Me.xShowCampedMobs.Size = New System.Drawing.Size(217, 22)
        Me.xShowCampedMobs.Text = "Camped Mobs"
        '
        'cEditNMList
        '
        Me.cEditNMList.Name = "cEditNMList"
        Me.cEditNMList.Size = New System.Drawing.Size(217, 22)
        Me.cEditNMList.Text = "Edit NM List"
        Me.cEditNMList.Visible = False
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(214, 6)
        '
        'cShowPC
        '
        Me.cShowPC.CheckOnClick = True
        Me.cShowPC.Name = "cShowPC"
        Me.cShowPC.Size = New System.Drawing.Size(217, 22)
        Me.cShowPC.Text = "PC"
        '
        'cShowPCNames
        '
        Me.cShowPCNames.CheckOnClick = True
        Me.cShowPCNames.Name = "cShowPCNames"
        Me.cShowPCNames.Size = New System.Drawing.Size(217, 22)
        Me.cShowPCNames.Text = "PC Names"
        '
        'cShowParty
        '
        Me.cShowParty.CheckOnClick = True
        Me.cShowParty.Name = "cShowParty"
        Me.cShowParty.Size = New System.Drawing.Size(217, 22)
        Me.cShowParty.Text = "Party Members Only"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(214, 6)
        '
        'cShowPOS
        '
        Me.cShowPOS.CheckOnClick = True
        Me.cShowPOS.Name = "cShowPOS"
        Me.cShowPOS.Size = New System.Drawing.Size(217, 22)
        Me.cShowPOS.Text = "<POS>"
        '
        'cDistance
        '
        Me.cDistance.CheckOnClick = True
        Me.cDistance.Name = "cDistance"
        Me.cDistance.Size = New System.Drawing.Size(217, 22)
        Me.cDistance.Text = "Distance"
        '
        'cShowHP
        '
        Me.cShowHP.CheckOnClick = True
        Me.cShowHP.Name = "cShowHP"
        Me.cShowHP.Size = New System.Drawing.Size(217, 22)
        Me.cShowHP.Text = "HP"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(214, 6)
        '
        'cShowAll
        '
        Me.cShowAll.CheckOnClick = True
        Me.cShowAll.Name = "cShowAll"
        Me.cShowAll.Size = New System.Drawing.Size(217, 22)
        Me.cShowAll.Text = "All Mobs"
        '
        'cShowID
        '
        Me.cShowID.CheckOnClick = True
        Me.cShowID.Name = "cShowID"
        Me.cShowID.Size = New System.Drawing.Size(217, 22)
        Me.cShowID.Text = "Mob ID"
        '
        'OptionsToolStripMenuItem1
        '
        Me.OptionsToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cOnTop, Me.cShowFilterPanel, Me.cDocking, Me.cDragging, Me.cClickThrough})
        Me.OptionsToolStripMenuItem1.Name = "OptionsToolStripMenuItem1"
        Me.OptionsToolStripMenuItem1.Size = New System.Drawing.Size(197, 22)
        Me.OptionsToolStripMenuItem1.Text = "Options"
        '
        'cOnTop
        '
        Me.cOnTop.CheckOnClick = True
        Me.cOnTop.Name = "cOnTop"
        Me.cOnTop.Size = New System.Drawing.Size(204, 22)
        Me.cOnTop.Text = "Stay On Top"
        '
        'cShowFilterPanel
        '
        Me.cShowFilterPanel.CheckOnClick = True
        Me.cShowFilterPanel.Name = "cShowFilterPanel"
        Me.cShowFilterPanel.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.cShowFilterPanel.Size = New System.Drawing.Size(204, 22)
        Me.cShowFilterPanel.Text = "Filter Panel"
        '
        'cDocking
        '
        Me.cDocking.CheckOnClick = True
        Me.cDocking.Name = "cDocking"
        Me.cDocking.Size = New System.Drawing.Size(204, 22)
        Me.cDocking.Text = "Disable Docking"
        '
        'cDragging
        '
        Me.cDragging.CheckOnClick = True
        Me.cDragging.Name = "cDragging"
        Me.cDragging.Size = New System.Drawing.Size(204, 22)
        Me.cDragging.Text = "Disable Dragging"
        '
        'cClickThrough
        '
        Me.cClickThrough.CheckOnClick = True
        Me.cClickThrough.Name = "cClickThrough"
        Me.cClickThrough.Size = New System.Drawing.Size(204, 22)
        Me.cClickThrough.Text = "Click Through"
        '
        'cRanges
        '
        Me.cRanges.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cRingOnly, Me.cAggro, Me.cJobAbility, Me.cSpellCasting, Me.cVisible, Me.cCustomRanges})
        Me.cRanges.Name = "cRanges"
        Me.cRanges.Size = New System.Drawing.Size(197, 22)
        Me.cRanges.Text = "Ranges"
        '
        'cRingOnly
        '
        Me.cRingOnly.CheckOnClick = True
        Me.cRingOnly.Name = "cRingOnly"
        Me.cRingOnly.Size = New System.Drawing.Size(194, 22)
        Me.cRingOnly.Text = "Show Range Ring Only"
        '
        'cAggro
        '
        Me.cAggro.CheckOnClick = True
        Me.cAggro.Name = "cAggro"
        Me.cAggro.Size = New System.Drawing.Size(194, 22)
        Me.cAggro.Text = "Aggro Range"
        '
        'cJobAbility
        '
        Me.cJobAbility.CheckOnClick = True
        Me.cJobAbility.Name = "cJobAbility"
        Me.cJobAbility.Size = New System.Drawing.Size(194, 22)
        Me.cJobAbility.Text = "Job Ability"
        '
        'cSpellCasting
        '
        Me.cSpellCasting.CheckOnClick = True
        Me.cSpellCasting.Name = "cSpellCasting"
        Me.cSpellCasting.Size = New System.Drawing.Size(194, 22)
        Me.cSpellCasting.Text = "Spell Casting"
        '
        'cVisible
        '
        Me.cVisible.CheckOnClick = True
        Me.cVisible.Name = "cVisible"
        Me.cVisible.Size = New System.Drawing.Size(194, 22)
        Me.cVisible.Text = "Visible Range"
        '
        'cCustomRanges
        '
        Me.cCustomRanges.Name = "cCustomRanges"
        Me.cCustomRanges.Size = New System.Drawing.Size(194, 22)
        Me.cCustomRanges.Text = "Custom Ranges..."
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(194, 6)
        '
        'cSetFont
        '
        Me.cSetFont.Name = "cSetFont"
        Me.cSetFont.Size = New System.Drawing.Size(197, 22)
        Me.cSetFont.Text = "Font"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(197, 22)
        Me.ToolStripMenuItem2.Text = "Colors"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(194, 6)
        '
        'TransparencyToolStripMenuItem
        '
        Me.TransparencyToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.p100, Me.p80, Me.p60, Me.p40, Me.p20})
        Me.TransparencyToolStripMenuItem.Name = "TransparencyToolStripMenuItem"
        Me.TransparencyToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.TransparencyToolStripMenuItem.Text = "Transparency"
        '
        'p100
        '
        Me.p100.Name = "p100"
        Me.p100.Size = New System.Drawing.Size(102, 22)
        Me.p100.Tag = "100"
        Me.p100.Text = "100%"
        '
        'p80
        '
        Me.p80.Name = "p80"
        Me.p80.Size = New System.Drawing.Size(102, 22)
        Me.p80.Tag = "80"
        Me.p80.Text = "80%"
        '
        'p60
        '
        Me.p60.Name = "p60"
        Me.p60.Size = New System.Drawing.Size(102, 22)
        Me.p60.Tag = "60"
        Me.p60.Text = "60%"
        '
        'p40
        '
        Me.p40.Name = "p40"
        Me.p40.Size = New System.Drawing.Size(102, 22)
        Me.p40.Tag = "40"
        Me.p40.Text = "40%"
        '
        'p20
        '
        Me.p20.Name = "p20"
        Me.p20.Size = New System.Drawing.Size(102, 22)
        Me.p20.Tag = "20"
        Me.p20.Text = "20%"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(194, 6)
        '
        'xSaveSettings
        '
        Me.xSaveSettings.Name = "xSaveSettings"
        Me.xSaveSettings.Size = New System.Drawing.Size(197, 22)
        Me.xSaveSettings.Text = "Save Settings..."
        '
        'xSaveSettingsAs
        '
        Me.xSaveSettingsAs.Name = "xSaveSettingsAs"
        Me.xSaveSettingsAs.Size = New System.Drawing.Size(197, 22)
        Me.xSaveSettingsAs.Text = "Save Settings As..."
        '
        'xLoadSettings
        '
        Me.xLoadSettings.Name = "xLoadSettings"
        Me.xLoadSettings.Size = New System.Drawing.Size(197, 22)
        Me.xLoadSettings.Text = "Load Settings..."
        '
        'cShowSettingsDesigner
        '
        Me.cShowSettingsDesigner.Name = "cShowSettingsDesigner"
        Me.cShowSettingsDesigner.Size = New System.Drawing.Size(197, 22)
        Me.cShowSettingsDesigner.Text = "Show Settings Designer"
        Me.cShowSettingsDesigner.Visible = False
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(194, 6)
        '
        'cResetPosition
        '
        Me.cResetPosition.Name = "cResetPosition"
        Me.cResetPosition.Size = New System.Drawing.Size(197, 22)
        Me.cResetPosition.Text = "Reset Position"
        '
        'ApExit
        '
        Me.ApExit.Name = "ApExit"
        Me.ApExit.Size = New System.Drawing.Size(197, 22)
        Me.ApExit.Text = "Close"
        '
        'ActiveTimer
        '
        Me.ActiveTimer.Interval = 500
        '
        'MapRadar
        '
        Me.MapRadar.BackColor = System.Drawing.Color.Black
        Me.MapRadar.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed
        Me.MapRadar.ContextMenuStrip = Me.cmMapRadar
        Me.MapRadar.CurrentMapEntry = Nothing
        Me.MapRadar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MapRadar.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MapRadar.LinkServerRunning = False
        Me.MapRadar.Location = New System.Drawing.Point(0, 0)
        Me.MapRadar.MapLocation = Nothing
        Me.MapRadar.MapPath = "Maps"
        Me.MapRadar.Margin = New System.Windows.Forms.Padding(4)
        Me.MapRadar.Name = "MapRadar"
        Me.MapRadar.NMList = CType(resources.GetObject("MapRadar.NMList"), System.Collections.Generic.List(Of String))
        Me.MapRadar.ProEnabled = False
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
        RadarSettings1.Font = resources.GetString("RadarSettings1.Font")
        RadarSettings1.HideInfoInCombat = True
        RadarSettings1.HideOtherFloors = False
        RadarSettings1.LinkColorHtml = "Red"
        RadarSettings1.Location = New System.Drawing.Point(100, 100)
        RadarSettings1.MapLevel = CType(-1, Short)
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
        RadarSettings1.ShowNPC = True
        RadarSettings1.ShowNPCNames = True
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
        RadarSettings1.TrackedMob = 0
        RadarSettings1.Transparency = 1.0!
        RadarSettings1.Zoom = 1.0!
        Me.MapRadar.Settings = RadarSettings1
        Me.MapRadar.Size = New System.Drawing.Size(512, 512)
        Me.MapRadar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed
        Me.MapRadar.TabIndex = 1
        Me.MapRadar.TextRendering = System.Drawing.Text.TextRenderingHint.SystemDefault
        '
        'MappedRadarForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(512, 512)
        Me.Controls.Add(Me.MapRadar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(100, 100)
        Me.Name = "MappedRadarForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MappedRadarForm"
        Me.cmMapRadar.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmMapRadar As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowHide As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RadarItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cSelectPack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cMapPackSelect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents xHideFloors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cShowNPC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowNPCNames As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cShowPC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowPCNames As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cShowPOS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cDistance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowHP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OptionsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cOnTop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowFilterPanel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cDocking As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cDragging As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cClickThrough As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cRanges As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TransparencyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents p100 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents p80 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents p60 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents p40 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents p20 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cResetPosition As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ApExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowSettingsDesigner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowID As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cEditNMList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents xSaveSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents xSaveSettingsAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents xLoadSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cAggro As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cJobAbility As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cSpellCasting As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowParty As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cCustomRanges As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ActiveTimer As System.Windows.Forms.Timer
    Friend WithEvents xShowCampedMobs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents xLinkedRadars As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cVisible As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cRingOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MapRadar As RadarControls.Radar
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cSetFont As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cShowMobs As System.Windows.Forms.ToolStripMenuItem
End Class
