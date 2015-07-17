Imports RadarControls
Imports FFXIMemory

Public Class IniViewerDialog

    Private Sub IniViewerDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadIniValues()
    End Sub

    Private Sub LoadIniValues()
        Dim z As New Zones
        Dim mh As New MapHandler
        Dim zoneID As Byte = 0
        Dim rootnode As TreeNode
        Dim levelNode As TreeNode
        For Each zone As Zones.Zone In z.ZoneList
            rootnode = Me.TreeView1.Nodes.Add(zone.ZoneID, zone.ZoneName)
            zoneID = zone.ZoneID
            Dim q = From c In mh.MapList Where c.Map = zoneID
            For Each x In q
                levelNode = rootnode.Nodes.Add(String.Format("{0}_{1}", zoneID, x.Level), x.Level)
                For Each b In x.Boxes
                    levelNode.Nodes.Add(String.Format("{0},{1},{2} x {3},{4},{5}", b.X1, b.Y1, b.Z1, b.X2, b.Y2, b.Z2))
                Next
            Next
        Next
    End Sub
End Class