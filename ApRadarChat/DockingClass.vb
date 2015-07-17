Public Class DockingClass
    Private UseDock As Boolean
    Private mParentForm As Form
    Private mAlwaysOnTop As Boolean
    Private mDualMonitors As Boolean
    Private mDualPosition As ScreenPositioning

    Private mSnapDistance As Integer = 10

    ' Internally set variables
    Private mStartDragX As Single
    Private mStartDragY As Single

    Private mWorkAreaRect As RECT
    Private mSecondScreen As Rectangle
    Private mWindowStyle As Integer

    Public Sub New(ByVal Parent As Form)
        Me.ParentForm = Parent
    End Sub

    Private DockIn As DockTo = DockTo.Primary

    Private ReadOnly Property ScreenCount() As Integer
        Get
            Return SystemInformation.MonitorCount
        End Get
    End Property

    Public Enum DockPosition
        Top
        Right
        Bottom
        Left
    End Enum

    Private Enum ScreenPositioning
        MainLeft
        MainRight
    End Enum

    Private Enum DockTo
        Primary
        Secondary
    End Enum

    Public Sub StartDockDrag(ByVal X As Single, ByVal Y As Single)

        ' Get the WorkArea - the area of the desktop not taken
        ' by the taskbar, using a little known but interesting
        ' API call
        SystemParametersInfo(SPI_GETWORKAREA, 0&, mWorkAreaRect, 0&)
        If ScreenCount > 1 Then
            mDualMonitors = True
            For Each Scr In Screen.AllScreens
                If Not Scr.Primary Then
                    mSecondScreen = Scr.Bounds
                    Exit For
                End If
            Next
            If mSecondScreen.Left < mWorkAreaRect.Left Then
                mDualPosition = ScreenPositioning.MainRight
            Else
                mDualPosition = ScreenPositioning.MainLeft
            End If
        End If
        mStartDragX = X
        mStartDragY = Y

    End Sub

    Public Sub UpdateDockDrag(ByVal X As Single, ByVal Y As Single)
        Dim DiffX As Long, DiffY As Long
        Dim NewX As Long, NewY As Long
        Dim ToLeftDistance As Long
        Dim ToRightDistance As Long
        Dim ToTopDistance As Long
        Dim ToBottomDistance As Long

        DiffX = X - mStartDragX
        DiffY = Y - mStartDragY

        If DiffX = 0 And DiffY = 0 Then Exit Sub

        NewX = mParentForm.Left + DiffX
        NewY = mParentForm.Top + DiffY

        ' Find the distance to the screen edges
        If mDualMonitors Then
            If mDualPosition = ScreenPositioning.MainRight Then
                If mParentForm.Location.X < 0 Then
                    DockIn = DockTo.Secondary
                    ToRightDistance = mWorkAreaRect.Right - (NewX + mParentForm.Width)
                    ToLeftDistance = NewX - mSecondScreen.Left
                    ToBottomDistance = mSecondScreen.Bottom - (NewY + mParentForm.Height)
                    ToTopDistance = NewY - mSecondScreen.Top
                Else
                    DockIn = DockTo.Primary
                    ToRightDistance = mWorkAreaRect.Right - (NewX + mParentForm.Width)
                    ToLeftDistance = NewX - mSecondScreen.Left
                    ToBottomDistance = mWorkAreaRect.Bottom - (NewY + mParentForm.Height)
                    ToTopDistance = NewY - mWorkAreaRect.Top
                End If
            Else
                If mParentForm.Location.X > mWorkAreaRect.Right Then
                    DockIn = DockTo.Secondary
                    ToRightDistance = mSecondScreen.Right - (NewX + mParentForm.Width)
                    ToLeftDistance = NewX - mWorkAreaRect.Left
                    ToBottomDistance = mSecondScreen.Bottom - (NewY + mParentForm.Height)
                    ToTopDistance = NewY - mSecondScreen.Top
                Else
                    DockIn = DockTo.Primary
                    ToRightDistance = mSecondScreen.Right - (NewX + mParentForm.Width)
                    ToLeftDistance = NewX - mWorkAreaRect.Left
                    ToBottomDistance = mWorkAreaRect.Bottom - (NewY + mParentForm.Height)
                    ToTopDistance = NewY - mWorkAreaRect.Top
                End If
            End If
        Else
            DockIn = DockTo.Primary
            ToRightDistance = mWorkAreaRect.Right - (NewX + mParentForm.Width)
            ToLeftDistance = NewX - mWorkAreaRect.Left
            ToBottomDistance = mWorkAreaRect.Bottom - (NewY + mParentForm.Height)
            ToTopDistance = NewY - mWorkAreaRect.Top
        End If

        ' The idea in all the following code is the same:
        ' If wer'e not already attached some specific edge,
        ' find out if we should.
        ' If wer'e already attached, find out whether we should
        ' "break" the attachment, or stay put.
        If UseDock Then
            If Not DockedLocation.Contains(DockPosition.Bottom) Then
                If Math.Abs(ToBottomDistance) <= mSnapDistance Then
                    ' Attach to edge
                    NewY = mParentForm.Top + ToBottomDistance
                    DockedLocation.Add(DockPosition.Bottom)
                End If
            Else
                If Math.Abs(ToBottomDistance) > mSnapDistance Then
                    ' Break the attachement
                    DockedLocation.Remove(DockPosition.Bottom)
                Else
                    ' Stay at current position
                    NewY = mParentForm.Top
                End If
            End If

            If Not DockedLocation.Contains(DockPosition.Top) Then
                If Math.Abs(ToTopDistance) <= mSnapDistance Then
                    If DockIn = DockTo.Primary Then
                        NewY = mWorkAreaRect.Top
                    Else
                        NewY = mSecondScreen.Top
                    End If
                    DockedLocation.Add(DockPosition.Top)
                End If
            Else
                If Math.Abs(ToTopDistance) > mSnapDistance Then
                    DockedLocation.Remove(DockPosition.Top)
                Else
                    NewY = mParentForm.Top
                End If
            End If

            If Not DockedLocation.Contains(DockPosition.Right) Then
                If Math.Abs(ToRightDistance) <= mSnapDistance Then
                    If DockIn = DockTo.Primary Then
                        NewX = mWorkAreaRect.Right - mParentForm.Width
                    Else
                        NewX = mSecondScreen.Right - mParentForm.Width
                    End If

                    DockedLocation.Add(DockPosition.Right)
                End If
            Else
                If Math.Abs(ToRightDistance) > mSnapDistance Then
                    DockedLocation.Remove(DockPosition.Right)
                Else
                    NewX = mParentForm.Left
                End If
            End If

            If Not DockedLocation.Contains(DockPosition.Left) Then
                If Math.Abs(ToLeftDistance) <= mSnapDistance Then
                    If DockIn = DockTo.Primary Then
                        NewX = mWorkAreaRect.Left
                    Else
                        NewX = mSecondScreen.Left
                    End If
                    DockedLocation.Add(DockPosition.Left)
                End If
            Else
                If Math.Abs(ToLeftDistance) > mSnapDistance Then
                    DockedLocation.Remove(DockPosition.Left)
                Else
                    NewX = mParentForm.Left
                End If
            End If
        End If

        mParentForm.Left = NewX
        mParentForm.Top = NewY

        'Set the position of any child forms
        For Each cf As ChildForms In Me.Childen
            Select Case cf.DockMode
                Case DockMode.Bottom
                    SetWindowPos(cf.ChildForm.Handle, mWindowStyle, _
                                 mParentForm.Left, mParentForm.Bottom, _
                                 cf.ChildForm.Width, cf.ChildForm.Height, 0)
                Case DockMode.Left
                    SetWindowPos(cf.ChildForm.Handle, mWindowStyle, _
                                 NewX - cf.ChildForm.Width, NewY, _
                                 cf.ChildForm.Width, cf.ChildForm.Height, 0)
                Case DockMode.Top
                    SetWindowPos(cf.ChildForm.Handle, mWindowStyle, _
                                 NewX, NewY - cf.ChildForm.Height, _
                                 cf.ChildForm.Width, cf.ChildForm.Height, 0)
                Case DockMode.Right
                    SetWindowPos(cf.ChildForm.Handle, mWindowStyle, _
                                 NewX + mParentForm.Width, NewY, _
                                 cf.ChildForm.Width, cf.ChildForm.Height, 0)
            End Select
        Next

        ' Position the window, converting to pixels again
        'SetWindowPos(mParentForm.Handle, mWindowStyle, _
        '    NewX, _
        '   NewY, _
        '    mParentForm.Width, _
        '    mParentForm.Height, 0)
    End Sub

    Public WriteOnly Property ParentForm() As Form
        Set(ByVal value As Form)
            mParentForm = value
        End Set
    End Property

    Private _childFoms As List(Of ChildForms)
    Public ReadOnly Property Childen() As List(Of ChildForms)
        Get
            If _childFoms Is Nothing Then
                _childFoms = New List(Of ChildForms)
            End If
            Return _childFoms
        End Get
    End Property

    Public Property UseDocking() As Boolean
        Get
            Return UseDock
        End Get
        Set(ByVal value As Boolean)
            UseDock = value
        End Set
    End Property

    Private _dockLocation As List(Of DockPosition)
    Public ReadOnly Property DockedLocation() As List(Of DockPosition)
        Get
            If _dockLocation Is Nothing Then
                _dockLocation = New List(Of DockPosition)
            End If
            Return _dockLocation
        End Get
    End Property

    Public Class ChildForms
        Public Sub New(ByVal f As Form, ByVal DockMode As DockMode)
            _form = f
            _dockType = DockMode
        End Sub

        Private _form As Form
        Public Property ChildForm() As Form
            Get
                Return _form
            End Get
            Set(ByVal value As Form)
                _form = value
            End Set
        End Property

        Private _dockType As DockMode
        Public Property DockMode() As DockMode
            Get
                Return _dockType
            End Get
            Set(ByVal value As DockMode)
                _dockType = value
            End Set
        End Property
    End Class
End Class
