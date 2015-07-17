' ==============================================================================
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'
' © 2003-2004 LaMarvin. All Rights Reserved.
'
' FMI: http://www.vbinfozine.com/a_default.shtml
' ==============================================================================


' Implements the IDropDownDisplayAdapter by using the ComboBoxDisplay
' as the adaptee control.
Public NotInheritable Class ComboBoxDisplayAdapter
	Implements IDropDownDisplayAdapter

	' The ComboBoxDisplay control - our adaptee.
	Private WithEvents _Display As ComboBoxDisplay


	Public Sub New(ByVal display As ComboBoxDisplay)
		If display Is Nothing Then
			Throw New ArgumentNullException("display")
		End If
		Me._Display = display
	End Sub


	Public ReadOnly Property Adaptee() As System.Windows.Forms.Control Implements IDropDownDisplayAdapter.Adaptee
		Get
			Return Me._Display
		End Get
	End Property


	Public Property Color() As System.Drawing.Color Implements IDropDownDisplayAdapter.Color
		Get
			Return Me._Display.Color
		End Get
		Set(ByVal Value As System.Drawing.Color)
			Me._Display.Color = Value
		End Set
	End Property


	Public Event DropDownAppearanceChanged As EventHandler Implements IDropDownDisplayAdapter.DropDownAppearanceChanged

	Public Property HasDropDownAppearance() As Boolean Implements IDropDownDisplayAdapter.HasDropDownAppearance
		Get
			Return Me._Display.HasDropDownAppearance
		End Get
		Set(ByVal Value As Boolean)
			Me._Display.HasDropDownAppearance = Value
		End Set
	End Property


	Public Property Text() As String Implements IDropDownDisplayAdapter.Text
		Get
			Return Me._Display.Text
		End Get
		Set(ByVal Value As String)
			Me._Display.Text = Value
		End Set
	End Property


	Private Sub _Display_DropDownAppearanceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Display.DropDownAppearanceChanged
		RaiseEvent DropDownAppearanceChanged(Me, e)
	End Sub

End Class
