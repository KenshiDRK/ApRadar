Public Class FilterForm
    Private _animator As FormAnimator

    Friend Event PCFilterChanged(ByVal Filter As String)
    Friend Event NPCFilterChanged(ByVal Filter As String)

    Friend Event PCFilterTypeChanged(ByVal Type As FilterType)
    Friend Event NPCFilterTypeChanged(ByVal Type As FilterType)

    Private Sub FilterForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not _animator Is Nothing Then
            _animator.RollUp(400, SlideDirection.Down)
        End If
    End Sub

    Private Sub FilterForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ThemeHandler.ApplyTheme(Me)
        _animator = New FormAnimator(Me)
        _animator.SlideOut(250, SlideDirection.Down)
    End Sub

    Friend WriteOnly Property NPCFilterType() As FilterType
        Set(ByVal value As FilterType)
            Select Case value
                Case FilterType.None
                    Me.rbNPCNone.Checked = True
                Case FilterType.RegEx
                    Me.rbNPCRegEx.Checked = True
                Case FilterType.Reverse
                    Me.rbNPCReverse.Checked = True
                Case FilterType.Standard
                    Me.rbNPCStandard.Checked = True
            End Select
        End Set
    End Property

    Friend WriteOnly Property NPCFilter() As String
        Set(ByVal value As String)
            Me.txtNpcFilter.Text = value
        End Set
    End Property

    Friend WriteOnly Property PCFilterType() As FilterType
        Set(ByVal value As FilterType)
            Select Case value
                Case FilterType.None
                    Me.rbPCNone.Checked = True
                Case FilterType.RegEx
                    Me.rbPCRegEx.Checked = True
                Case FilterType.Reverse
                    Me.rbNPCReverse.Checked = True
                Case FilterType.Standard
                    Me.rbPCStandard.Checked = True
            End Select
        End Set
    End Property

    Friend WriteOnly Property PCFilter() As String
        Set(ByVal value As String)
            Me.txtPCFilter.Text = value
        End Set
    End Property

    Private Sub npcFilterType_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbNPCNone.CheckedChanged, rbNPCStandard.CheckedChanged, rbNPCReverse.CheckedChanged, rbNPCRegEx.CheckedChanged
        RaiseEvent NPCFilterTypeChanged(CType(sender, RadioButton).Tag)
    End Sub

    Private Sub txtNpcFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNpcFilter.TextChanged
        RaiseEvent NPCFilterChanged(Me.txtNpcFilter.Text)
    End Sub

    Private Sub pcFilterType_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPCNone.CheckedChanged, rbPCStandard.CheckedChanged, rbPCReverse.CheckedChanged, rbPCRegEx.CheckedChanged
        RaiseEvent PCFilterTypeChanged(CType(sender, RadioButton).Tag)
    End Sub

    Private Sub txtPCFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPCFilter.TextChanged
        RaiseEvent PCFilterChanged(Me.txtPCFilter.Text)
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            If My.Settings.HideAltTab Then
                cp.ExStyle = cp.ExStyle Or &H80
            End If
            Return cp
        End Get
    End Property
End Class