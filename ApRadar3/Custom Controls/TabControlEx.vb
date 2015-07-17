Public Class TabControlEx
    Inherits TabControl

    Private _HeaderWidth As Integer
    Private _HeaderHeight As Integer

    Private _HeaderAlignment As System.Drawing.ContentAlignment
    Private _HeaderPadding As System.Windows.Forms.Padding
    Private _HeaderFont As Font
    Private _HeaderBackColor As Color
    Private _HeaderBackBrush As SolidBrush
    Private _HeaderBorderColor As Color
    Private _HeaderBackPen As Pen
    Private _HeaderForeColor As Color
    Private _HeaderForeBrush As SolidBrush
    Private _HeaderSelectedBackColor As Color
    Private _HeaderSelectedBackBrush As SolidBrush
    Private _HeaderSelectedForeColor As Color
    Private _HeaderSelectedForeBrush As Brush

    Private _BackColor As Color
    Private _BackBrush As System.Drawing.SolidBrush

#Region " Header Properties "
    <System.ComponentModel.DefaultValue(100)> _
    Public Property HeaderWidth() As Integer
        Get
            Return Me._HeaderWidth
        End Get
        Set(ByVal value As Integer)
            Me._HeaderWidth = value
            Me.ItemSize = New Size(Me.ItemSize.Width, value)
        End Set
    End Property

    <System.ComponentModel.DefaultValue(32)> _
    Public Property HeaderHeight() As Integer
        Get
            Return Me._HeaderHeight
        End Get
        Set(ByVal value As Integer)
            Me._HeaderHeight = value
            Me.ItemSize = New Size(value, Me.ItemSize.Height)
        End Set
    End Property

    <System.ComponentModel.DefaultValue(GetType(System.Drawing.ContentAlignment), "ContentAlignment.MiddleLeft")> _
    Public Property HeaderAlignment() As System.Drawing.ContentAlignment
        Get
            Return Me._HeaderAlignment
        End Get
        Set(ByVal value As System.Drawing.ContentAlignment)
            Me._HeaderAlignment = value
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.DefaultValue(GetType(System.Windows.Forms.Padding), "3,3,3,3")> _
    Public Property HeaderPadding() As System.Windows.Forms.Padding
        Get
            Return Me._HeaderPadding
        End Get
        Set(ByVal value As System.Windows.Forms.Padding)
            Me._HeaderPadding = value
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.DefaultValue(GetType(Color), "White")> _
    Public Property HeaderBorderColor() As Color
        Get
            Return Me._HeaderBorderColor
        End Get
        Set(ByVal value As Color)
            If Not value = Me._HeaderBorderColor Then
                Me._HeaderBorderColor = value
                If Me._HeaderBackPen IsNot Nothing Then
                    Me._HeaderBackPen.Dispose()
                    Me._HeaderBackPen = Nothing
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    <System.ComponentModel.DefaultValue(GetType(Color), "LightGray")> _
    Public Property HeaderBackColor() As Color
        Get
            Return Me._HeaderBackColor
        End Get
        Set(ByVal value As Color)
            If Not value = Me._HeaderBackColor Then
                Me._HeaderBackColor = value
                If Me._HeaderBackBrush IsNot Nothing Then
                    Me._HeaderBackBrush.Dispose()
                    Me._HeaderBackBrush = Nothing
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Private ReadOnly Property HeaderBackBrush() As SolidBrush
        Get
            If Me._HeaderBackBrush Is Nothing Then
                Me._HeaderBackBrush = New SolidBrush(Me.HeaderBackColor)
            End If
            Return Me._HeaderBackBrush
        End Get
    End Property

    Private ReadOnly Property HeaderPen() As Pen
        Get
            If Me._HeaderBackPen Is Nothing Then
                Me._HeaderBackPen = New Pen(Me.HeaderBorderColor)
            End If
            Return Me._HeaderBackPen
        End Get
    End Property

    <System.ComponentModel.DefaultValue(GetType(Color), "Black")> _
    Public Property HeaderForeColor() As Color
        Get
            Return Me._HeaderForeColor
        End Get
        Set(ByVal value As Color)
            If Not value = Me._HeaderForeColor Then
                Me._HeaderForeColor = value
                If Me._HeaderForeBrush IsNot Nothing Then
                    Me._HeaderForeBrush.Dispose()
                    Me._HeaderForeBrush = Nothing
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Private ReadOnly Property HeaderForeBrush() As SolidBrush
        Get
            If Me._HeaderForeBrush Is Nothing Then
                Me._HeaderForeBrush = New SolidBrush(Me.HeaderForeColor)
            End If
            Return Me._HeaderForeBrush
        End Get
    End Property

    <System.ComponentModel.DefaultValue(GetType(Color), "DarkGray")> _
    Public Property HeaderSelectedBackColor() As Color
        Get
            Return Me._HeaderSelectedBackColor
        End Get
        Set(ByVal value As Color)
            If Not value = Me._HeaderSelectedBackColor Then
                Me._HeaderSelectedBackColor = value
                If Me._HeaderSelectedBackBrush IsNot Nothing Then
                    Me._HeaderSelectedBackBrush.Dispose()
                    Me._HeaderSelectedBackBrush = Nothing
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Private ReadOnly Property HeaderSelectedBackBrush() As SolidBrush
        Get
            If Me._HeaderSelectedBackBrush Is Nothing Then
                Me._HeaderSelectedBackBrush = New SolidBrush(Me.HeaderSelectedBackColor)
            End If
            Return Me._HeaderSelectedBackBrush
        End Get
    End Property

    <System.ComponentModel.DefaultValue(GetType(Color), "Black")> _
    Public Property HeaderSelectedForeColor() As Color
        Get
            Return Me._HeaderSelectedForeColor
        End Get
        Set(ByVal value As Color)
            If Not value = Me._HeaderSelectedForeColor Then
                Me._HeaderSelectedForeColor = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private ReadOnly Property HeaderSelectedForeBrush() As SolidBrush
        Get
            If Me._HeaderSelectedForeBrush Is Nothing Then
                Me._HeaderSelectedForeBrush = New SolidBrush(Me.HeaderSelectedForeColor)
            End If
            Return Me._HeaderSelectedForeBrush
        End Get
    End Property

    Public Property HeaderFont() As Font
        Get
            Return Me._HeaderFont
        End Get
        Set(ByVal value As Font)
            Me._HeaderFont = value
            Me.Invalidate()
        End Set
    End Property
#End Region

    <System.ComponentModel.DefaultValue(GetType(Color), "White")> _
    <System.ComponentModel.Browsable(True)> _
    Public Overrides Property BackColor() As Color
        Get
            Return Me._BackColor
        End Get
        Set(ByVal value As Color)
            If Not Me._BackColor = value Then
                Me._BackColor = value
                If Me._BackBrush IsNot Nothing Then
                    Me._BackBrush.Dispose()
                    Me._BackBrush = Nothing
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Private ReadOnly Property BackBrush() As SolidBrush
        Get
            If Me._BackBrush Is Nothing Then
                Me._BackBrush = New SolidBrush(Me.BackColor)
            End If
            Return Me._BackBrush
        End Get
    End Property

    Public Sub New()
        Me._HeaderWidth = 100
        Me._HeaderHeight = 32
        Me._HeaderAlignment = ContentAlignment.MiddleLeft
        Me._HeaderPadding = New Padding(3)
        Me._BackColor = Color.White
        Me._HeaderBorderColor = Color.White
        Me._HeaderFont = Me.Font
        Me._HeaderForeColor = Color.Black
        Me._HeaderBackColor = Color.DarkGray
        Me._HeaderSelectedBackColor = Color.LightGray
        Me._HeaderSelectedForeColor = Color.Black

        Me.DrawMode = TabDrawMode.OwnerDrawFixed
        Me.SizeMode = TabSizeMode.Fixed
        Me.Alignment = TabAlignment.Left

        Me.ItemSize = New Size(Me.HeaderHeight, Me.HeaderWidth)

        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim g As Graphics

        g = e.Graphics

        g.FillRectangle(Me.BackBrush, e.ClipRectangle)      ' background

        For i As Integer = 0 To Me.TabPages.Count - 1
            Call Me.DrawTabButton(g, i)
            Call Me.DrawTabText(g, i)
        Next
    End Sub

    Private Sub DrawTabButton(ByVal g As Graphics, ByVal TabPageIndex As Integer)
        Dim r As Rectangle
        ' get the tab rectangle
        r = Me.GetTabRect(TabPageIndex)
        ' increase its width we dont want the background in between
        r.Width = r.Width + 2
        ' if first tab page
        If TabPageIndex = 0 Then
            ' reduce its height and move it a little bit lower
            ' since in tab control first tab button is displayed a little
            ' bit heigher
            r.Height = r.Height - 2
            r.Y = r.Y + 2
        End If
        ' if given tab button is selected
        If Me.SelectedIndex = TabPageIndex Then
            ' use selected properties
            'g.FillRectangle(Me.HeaderSelectedBackBrush, r)
            g.DrawLine(New Pen(Me.HeaderSelectedBackColor, 8), New Point(r.Left, r.Top), New Point(r.Left, r.Bottom))
            'g.DrawLine(New Pen(Me.HeaderSelectedBackColor, 1), New Point(r.Left, r.Bottom - 1), New Point(r.Right, r.Bottom - 1))
            ' if currently focused then draw focus rectangle
            'g.FillPolygon(Brushes.Green, New Point() {New Point(r.Right - 20, r.Top), New Point(r.Right - 10, r.Height / 2 + r.Height * TabPageIndex), New Point(r.Right - 20, r.Bottom)})
            If Me.Focused Then
                'System.Windows.Forms.ControlPaint.DrawFocusRectangle(g, New Rectangle(r.Left + 2, r.Top + 2, r.Width - 4, r.Height - 5))
            End If

        Else ' else (not the selected tab page)
            g.FillRectangle(Me.HeaderBackBrush, r)
        End If

        ' if first tab button
        If TabPageIndex = 0 Then
            ' draw a line on top
            g.DrawLine(Me.HeaderPen, r.Left, r.Top, r.Right, r.Top)
        End If
        ' line at left
        g.DrawLine(Me.HeaderPen, r.Left, r.Top, r.Left, r.Bottom - 1)
        ' line at bottom
        g.DrawLine(Me.HeaderPen, r.Left, r.Bottom - 1, r.Right, r.Bottom - 1)
        ' no line at right since we want to give an effect of
        ' pages
    End Sub

    Private Sub DrawTabText(ByVal g As Graphics, ByVal TabPageIndex As Integer)
        Dim iX As Integer
        Dim iY As Integer
        Dim sText As String
        Dim sizeText As SizeF
        Dim rectTab As Rectangle

        ' get tab button rectangle
        rectTab = Me.GetTabRect(TabPageIndex)
        ' get text
        sText = Me.TabPages(TabPageIndex).Text
        ' measure the size of text
        sizeText = g.MeasureString(sText, Me.HeaderFont)

        ' check text alignment
        Select Case Me.HeaderAlignment
            Case ContentAlignment.MiddleLeft, ContentAlignment.BottomLeft, ContentAlignment.TopLeft
                iX = rectTab.Left + Me.HeaderPadding.Left
            Case ContentAlignment.MiddleRight, ContentAlignment.BottomRight, ContentAlignment.TopRight
                iX = rectTab.Right - sizeText.Width - Me.HeaderPadding.Right
            Case ContentAlignment.MiddleCenter, ContentAlignment.BottomCenter, ContentAlignment.TopCenter
                iX = rectTab.Left + (rectTab.Width - Me.HeaderPadding.Left - Me.HeaderPadding.Right - sizeText.Width) / 2
        End Select

        Select Case Me.HeaderAlignment
            Case ContentAlignment.TopLeft, ContentAlignment.TopCenter, ContentAlignment.TopRight
                iY = rectTab.Top + Me.HeaderPadding.Top
            Case ContentAlignment.BottomLeft, ContentAlignment.BottomCenter, ContentAlignment.BottomRight
                iY = rectTab.Bottom - sizeText.Height - Me.HeaderPadding.Bottom
            Case ContentAlignment.MiddleCenter, ContentAlignment.MiddleLeft, ContentAlignment.MiddleRight
                iY = rectTab.Top + (rectTab.Height - Me.HeaderPadding.Top - sizeText.Height) / 2
        End Select

        ' if selected tab button
        If Me.SelectedIndex = TabPageIndex Then
            g.DrawString(sText, Me.HeaderFont, Me.HeaderForeBrush, iX + 8, iY)
        Else
            g.DrawString(sText, Me.HeaderFont, Me.HeaderForeBrush, iX, iY)
        End If
    End Sub

#Region " Hide some properties which can disturb our customization "
    <System.ComponentModel.Browsable(False)> _
    Public Overloads Property DrawMode() As System.Windows.Forms.TabDrawMode
        Get
            Return MyBase.DrawMode
        End Get
        Set(ByVal value As System.Windows.Forms.TabDrawMode)
            MyBase.DrawMode = TabDrawMode.OwnerDrawFixed
        End Set
    End Property

    <System.ComponentModel.Browsable(False)> _
    Public Overloads Property Alignment() As System.Windows.Forms.TabAlignment
        Get
            Return MyBase.Alignment
        End Get
        Set(ByVal value As System.Windows.Forms.TabAlignment)
            MyBase.Alignment = TabAlignment.Left
        End Set
    End Property

    <System.ComponentModel.Browsable(False)> _
    Public Overloads Property Appearance() As System.Windows.Forms.TabAppearance
        Get
            Return MyBase.Appearance
        End Get
        Set(ByVal value As System.Windows.Forms.TabAppearance)
            MyBase.Appearance = TabAppearance.Normal
        End Set
    End Property

    <System.ComponentModel.Browsable(False)> _
    Public Overloads Property ItemSize() As Size
        Get
            Return MyBase.ItemSize
        End Get
        Set(ByVal value As Size)
            MyBase.ItemSize = value
        End Set
    End Property

    <System.ComponentModel.Browsable(False)> _
    Public Overloads Property SizeMode() As System.Windows.Forms.TabSizeMode
        Get
            Return MyBase.SizeMode
        End Get
        Set(ByVal value As System.Windows.Forms.TabSizeMode)
            MyBase.SizeMode = TabSizeMode.Fixed
        End Set
    End Property

    <System.ComponentModel.Browsable(False)> _
    Public Overloads Property Multiline() As Boolean
        Get
            Return MyBase.Multiline
        End Get
        Set(ByVal value As Boolean)
            MyBase.Multiline = True
        End Set
    End Property
#End Region

End Class

