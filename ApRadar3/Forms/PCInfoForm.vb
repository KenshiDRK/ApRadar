Imports DataLibrary
Imports DataLibrary.DataAccess
Imports DataLibrary.ApRadarDataset
Public Class PCInfoForm
#Region " MEMBER VARIABLES "
    Private _animator As FormAnimator
    Private _pcRow As PCRow
    Private _lastID As Integer = -10
    Private _direction As SlideDirection
#End Region

#Region " PROPERTIES "
    Private _pcID As Integer
    Public Property PCID() As Integer
        Get
            Return _pcID
        End Get
        Set(ByVal value As Integer)
            _pcID = value
            If _pcID > 0 AndAlso _pcID <> _lastID Then
                PCBindingSource.RemoveFilter()
                PCBindingSource.Filter = String.Format("ServerID = {0}", value)
                _lastID = value
            End If
        End Set
    End Property

    Public ReadOnly Property PCFound() As Boolean
        Get
            Return _pcID > 0
        End Get
    End Property
#End Region

#Region " CONSTRUCTOR "
    Public Sub New()
        InitializeComponent()
        _animator = New FormAnimator(Me)
        If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
            _direction = SlideDirection.Down
        Else
            _direction = SlideDirection.Up
        End If
    End Sub
#End Region

#Region " FORM ACTIONS "

    Private Sub PCInfoForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PCBindingSource.DataSource = MobData.PC
    End Sub

    Private Sub PCInfoForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.BackColor = ThemeHandler.FormBackgroundColor
        Me.ForeColor = ThemeHandler.FormForeColor
        _animator.SlideOut(150, _direction)
    End Sub
#End Region

#Region " PUBLIC METHODS "
    Public Sub RollOut()
        If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
            _direction = SlideDirection.Down
        Else
            _direction = SlideDirection.Up
        End If
        Me.Show()
    End Sub

    Public Sub RollUp()
        Try
            Me.PCBindingSource.EndEdit()
            If Not DataAccess.MobData.GetChanges Is Nothing Then
                DataAccess.DataManager.UpdateAll(DataAccess.MobData)
            End If
            _animator.RollUp(150, _direction)
        Catch ex As Exception
            If DebugRun Then

            End If
        End Try
    End Sub
#End Region

#Region " CONTROLS "
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Validate()
        
    End Sub
#End Region

    Private Sub txtNotes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNotes.TextChanged

    End Sub
End Class