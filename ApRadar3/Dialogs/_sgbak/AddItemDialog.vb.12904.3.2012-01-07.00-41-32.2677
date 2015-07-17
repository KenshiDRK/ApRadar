Imports DataLibrary
Imports ApRadar3.FormAnimator

Public Class AddItemDialog
#Region " MEMBER VARIABLES "
    Private _dock As DockingClass
    Private _animator As FormAnimator
    Private _mobID As Integer
    Private _isLoading As Boolean = True
#End Region

#Region " PROPERTIES "
    Private _items As New List(Of Integer)
    Public ReadOnly Property Items() As Integer()
        Get
            Return _items.ToArray
        End Get
    End Property
#End Region

#Region " CONSTRUCTORS "
    Public Sub New(ByVal MobID As Integer)
        InitializeComponent()
        _mobID = MobID
    End Sub
#End Region

#Region " FORM ACTIONS "

    Private Sub AddItemDialog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        _animator.FadeOut(300)
    End Sub
    Private Sub AddItemDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
        LoadItems()
        'Dim autoCompleteItems As New AutoCompleteStringCollection
        'autoCompleteItems.AddRange((From itm In DataAccess.MobData.Items _
        '                            Select itm.ItemName).ToArray)
        'Me.txtSearch.AutoCompleteCustomSource = autoCompleteItems
        _animator = New FormAnimator(Me)
        _animator.SlideOut(150, SlideDirection.Right)
        _isLoading = False
    End Sub
#End Region

#Region " CONTROLS "
    Private Sub HeaderPanel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseDown, lblHeader.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.StartDockDrag(e.X, e.Y)
        End If
    End Sub

    Private Sub HeaderPanel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseMove, lblHeader.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
        End If
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
#End Region

#Region " FUNCTIONS "
    Private Sub LoadItems()
        Dim items = (From item In DataAccess.MobData.Items _
                     Order By item.ItemName _
                     Select New With {item.ItemID, _
                                      item.ItemName, _
                                      item.LongName}).Distinct.ToArray
        If Not items Is Nothing Then
            dgItems.DataSource = items
            ID.ReadOnly = True
            ItemName.ReadOnly = True
            LongName.ReadOnly = True
        End If
    End Sub
#End Region

    Private Sub dgItems_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgItems.CellValueChanged
        If Not _isLoading Then
            If e.ColumnIndex = AddThis.Index Then
                If dgItems.Rows(e.RowIndex).Cells("AddThis").Value Then
                    _items.Add(dgItems.Rows(e.RowIndex).Cells("ID").Value)
                Else
                    _items.Remove(dgItems.Rows(e.RowIndex).Cells("ID").Value)
                End If
            End If
        End If
    End Sub

    Private Sub txtSearch_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Leave
        If txtSearch.Text.Trim = String.Empty Then
            txtSearch.Text = "Search..."
            txtSearch.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text <> "Search..." Then
            Dim itm As String = txtSearch.Text
            Dim isFound As Boolean = False
            For Each dr As DataGridViewRow In dgItems.Rows
                If dr.Cells("ItemName").Value.ToString.ToLower.StartsWith(itm.ToLower) Then
                    dgItems.Rows(dr.Index).Cells("ItemName").Selected = True
                    isFound = True
                    Exit For
                End If
            Next
            If Not isFound Then
                txtSearch.ForeColor = Color.Red
            Else
                txtSearch.ForeColor = Color.Black
            End If
        Else
            txtSearch.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub txtSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Enter
        If txtSearch.Text.Trim = "Search..." Then
            txtSearch.Text = String.Empty
            txtSearch.ForeColor = Color.FromKnownColor(KnownColor.WindowText)
        End If
    End Sub
End Class