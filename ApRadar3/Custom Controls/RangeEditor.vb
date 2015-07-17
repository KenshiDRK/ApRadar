Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Reflection
Public Class RangeEditor
    Inherits CollectionEditor

    Public Sub New(ByVal NewType As Type)
        MyBase.New(NewType)
    End Sub

    Protected Overrides Function CanSelectMultipleInstances() As Boolean
        Return False
    End Function

    Protected Overrides Function CreateCollectionItemType() As System.Type
        Return GetType(RadarControls.Range)
    End Function
End Class
