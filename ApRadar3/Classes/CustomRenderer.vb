Public Class CustomRenderer
    Inherits ToolStripRenderer

    Protected Sub New()
        
    End Sub

    'Protected Overrides Sub OnRenderToolStripPanelBackground(ByVal e As System.Windows.Forms.ToolStripPanelRenderEventArgs)
    '    MyBase.OnRenderToolStripPanelBackground(e)
    'End Sub

    ''' <summary>
    ''' Raises the RenderButtonBackground event. 
    ''' </summary>
    ''' <param name="e">An ToolStripItemRenderEventArgs containing the event data.</param>
    Protected Overloads Overrides Sub OnRenderButtonBackground(ByVal e As ToolStripItemRenderEventArgs)
        ' Cast to correct type
        Dim button As ToolStripButton = DirectCast(e.Item, ToolStripButton)

        If button.Selected OrElse button.Pressed OrElse button.Checked Then
            'RenderToolButtonBackground(e.Graphics, button, e.ToolStrip)
        End If
    End Sub

    'Private Sub RenderToolButtonBackground(ByVal g As Graphics, ByVal button As ToolStripButton, ByVal toolstrip As ToolStrip)
    '    ' We only draw a background if the item is selected or being pressed
    '    If button.Enabled Then
    '        If button.Checked Then
    '            If button.Pressed Then
    '                DrawGradientToolItem(g, button, _itemToolItemPressedColors)
    '            ElseIf button.Selected Then
    '                DrawGradientToolItem(g, button, _itemToolItemCheckPressColors)
    '            Else
    '                DrawGradientToolItem(g, button, _itemToolItemCheckedColors)
    '            End If
    '        Else
    '            If button.Pressed Then
    '                DrawGradientToolItem(g, button, _itemToolItemPressedColors)
    '            ElseIf button.Selected Then
    '                DrawGradientToolItem(g, button, _itemToolItemSelectedColors)
    '            End If
    '        End If
    '    Else
    '        If button.Selected Then
    '            ' Get the mouse position in tool strip coordinates
    '            Dim mousePos As Point = toolstrip.PointToClient(Control.MousePosition)

    '            ' If the mouse is not in the item area, then draw disabled
    '            If Not button.Bounds.Contains(mousePos) Then
    '                DrawGradientToolItem(g, button, _itemDisabledColors)
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub DrawGradientToolItem(ByVal g As Graphics, ByVal item As ToolStripItem, ByVal colors As GradientItemColors)
    '    ' Perform drawing into the entire background of the item
    '    DrawGradientItem(g, New Rectangle(Point.Empty, item.Bounds.Size), colors)
    'End Sub


    'Private Sub DrawGradientContextMenuItem(ByVal g As Graphics, ByVal item As ToolStripItem, ByVal colors As GradientItemColors)
    '    ' Do we need to draw with separator on the opposite edge?
    '    Dim backRect As New Rectangle(2, 0, item.Bounds.Width - 3, item.Bounds.Height)

    '    ' Perform actual drawing into the background
    '    DrawGradientItem(g, backRect, colors)
    'End Sub

    'Private Sub DrawGradientItem(ByVal g As Graphics, ByVal backRect As Rectangle, ByVal colors As GradientItemColors)
    '    ' Cannot paint a zero sized area
    '    If (backRect.Width > 0) AndAlso (backRect.Height > 0) Then
    '        ' Draw the background of the entire item
    '        DrawGradientBack(g, backRect, colors)

    '        ' Draw the border of the entire item
    '        DrawGradientBorder(g, backRect, colors)
    '    End If
    'End Sub

    'Private Sub DrawGradientBack(ByVal g As Graphics, ByVal backRect As Rectangle, ByVal colors As GradientItemColors)
    '    ' Reduce rect draw drawing inside the border
    '    backRect.Inflate(-1, -1)

    '    Dim y2 As Integer = backRect.Height / 2
    '    Dim backRect1 As New Rectangle(backRect.X, backRect.Y, backRect.Width, y2)
    '    Dim backRect2 As New Rectangle(backRect.X, backRect.Y + y2, backRect.Width, backRect.Height - y2)
    '    Dim backRect1I As Rectangle = backRect1
    '    Dim backRect2I As Rectangle = backRect2
    '    backRect1I.Inflate(1, 1)
    '    backRect2I.Inflate(1, 1)

    '    Using insideBrush1 As New LinearGradientBrush(backRect1I, colors.InsideTop1, colors.InsideTop2, 90.0F), insideBrush2 As New LinearGradientBrush(backRect2I, colors.InsideBottom1, colors.InsideBottom2, 90.0F)
    '        g.FillRectangle(insideBrush1, backRect1)
    '        g.FillRectangle(insideBrush2, backRect2)
    '    End Using

    '    y2 = backRect.Height / 2
    '    backRect1 = New Rectangle(backRect.X, backRect.Y, backRect.Width, y2)
    '    backRect2 = New Rectangle(backRect.X, backRect.Y + y2, backRect.Width, backRect.Height - y2)
    '    backRect1I = backRect1
    '    backRect2I = backRect2
    '    backRect1I.Inflate(1, 1)
    '    backRect2I.Inflate(1, 1)

    '    Using fillBrush1 As New LinearGradientBrush(backRect1I, colors.FillTop1, colors.FillTop2, 90.0F), fillBrush2 As New LinearGradientBrush(backRect2I, colors.FillBottom1, colors.FillBottom2, 90.0F)
    '        ' Reduce rect one more time for the innermost drawing
    '        backRect.Inflate(-1, -1)

    '        y2 = backRect.Height / 2
    '        backRect1 = New Rectangle(backRect.X, backRect.Y, backRect.Width, y2)
    '        backRect2 = New Rectangle(backRect.X, backRect.Y + y2, backRect.Width, backRect.Height - y2)

    '        g.FillRectangle(fillBrush1, backRect1)
    '        g.FillRectangle(fillBrush2, backRect2)
    '    End Using
    'End Sub

    'Private Sub DrawGradientBorder(ByVal g As Graphics, ByVal backRect As Rectangle, ByVal colors As GradientItemColors)
    '    ' Drawing with anti aliasing to create smoother appearance
    '    Using uaa As New UseAntiAlias(g)
    '        Dim backRectI As Rectangle = backRect
    '        backRectI.Inflate(1, 1)

    '        ' Finally draw the border around the menu item
    '        Using borderBrush As New LinearGradientBrush(backRectI, colors.Border1, colors.Border2, 90.0F)
    '            ' Sigma curve, so go from color1 to color2 and back to color1 again
    '            borderBrush.SetSigmaBellShape(0.5F)

    '            ' Convert the brush to a pen for DrawPath call
    '            Using borderPen As New Pen(borderBrush)
    '                ' Create border path around the entire item
    '                Using borderPath As GraphicsPath = CreateBorderPath(backRect, _cutMenuItemBack)
    '                    g.DrawPath(borderPen, borderPath)
    '                End Using
    '            End Using
    '        End Using
    '    End Using
    'End Sub
End Class
