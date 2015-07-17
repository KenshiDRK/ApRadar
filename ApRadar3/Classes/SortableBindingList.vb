Imports System.ComponentModel
Imports System.Reflection

Public Class SortableBindingList(Of T)
    Inherits BindingList(Of T)

    Private _isSorted As Boolean
    Private _sortDirection As ListSortDirection
    Private _sortProperty As PropertyDescriptor

    'This override shows the binded object, that our list supports sorting
    Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    'And that it can sort bi-directional
    Protected Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
        Get
            Return _sortDirection
        End Get
    End Property

    'And that it can sort by T typed object's properties
    Protected Overloads Overrides ReadOnly Property SortPropertyCore() As PropertyDescriptor

        Get
            Return _sortProperty
        End Get
    End Property

    'This is the method, what gets called when the sort event occurs in the bound object
    Protected Overloads Overrides Sub ApplySortCore(ByVal prop As PropertyDescriptor, ByVal direction As ListSortDirection)
        Dim items As List(Of T) = TryCast(Me.Items, List(Of T))

        If items IsNot Nothing Then
            Dim pc As New PropertyComparer(Of T)(prop.Name, direction)
            items.Sort(pc)
            _isSorted = True
            _sortDirection = direction
            _sortProperty = prop
        Else
            _isSorted = False
        End If
        OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub

    'This shows if our list is already sorted or not
    Protected Overloads Overrides ReadOnly Property IsSortedCore() As Boolean
        Get
            Return _isSorted
        End Get
    End Property

    'Removing the sort
    Protected Overrides Sub RemoveSortCore()
        _isSorted = False
    End Sub

    Sub New(ByVal list As ICollection(Of T))
        MyBase.New(list)
    End Sub
End Class

Public Class PropertyComparer(Of T)
    Implements IComparer(Of T)
    Private _property As PropertyInfo
    Private _sortDirection As ListSortDirection

    Public Sub New(ByVal sortProperty As String, ByVal sortDirection As ListSortDirection)
        _property = GetType(T).GetProperty(sortProperty)
        Me._sortDirection = sortDirection
    End Sub

    Public Function Compare(ByVal x As T, ByVal y As T) As Integer Implements IComparer(Of T).Compare
        Dim valueX As Object = _property.GetValue(x, Nothing)
        Dim valueY As Object = _property.GetValue(y, Nothing)

        If _sortDirection = ListSortDirection.Ascending Then Return Comparer.[Default].Compare(valueX, valueY)

        Return Comparer.[Default].Compare(valueY, valueX)
    End Function
End Class