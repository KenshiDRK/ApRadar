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

Imports System.Drawing
Imports System.Windows.Forms

' Implements the IDropDownDisplayAdapter by using the standard CheckBox
' as the adaptee control.
Friend NotInheritable Class CheckBoxDisplayAdapter
	Implements IDropDownDisplayAdapter

	Public Event DropDownAppearanceChanged As EventHandler Implements IDropDownDisplayAdapter.DropDownAppearanceChanged


	Public Sub New(ByVal checkBox As CheckBox)
		Debug.Assert(Not checkBox Is Nothing)
		Me._CheckBox = checkBox
		checkBox.Appearance = Appearance.Button
		checkBox.TextAlign = ContentAlignment.MiddleCenter
	End Sub


	Public Property Color() As System.Drawing.Color Implements IDropDownDisplayAdapter.Color
		Get
			Return Me._CheckBox.BackColor
		End Get
		Set(ByVal Value As System.Drawing.Color)
			' Store the color in the BackColor property and set the ForeColor
			' to a value that will make the displayed text readable.
			Me._CheckBox.BackColor = Value
			Me._CheckBox.ForeColor = ColorPicker.GetInvertedColor(Value)
		End Set
	End Property


	Public Property HasDropDownAppearance() As Boolean Implements IDropDownDisplayAdapter.HasDropDownAppearance
		Get
			Return Me._CheckBox.Checked
		End Get
		Set(ByVal Value As Boolean)
			Me._CheckBox.Checked = Value
		End Set
	End Property


	Public Property Text() As String Implements IDropDownDisplayAdapter.Text
		Get
			Return Me._CheckBox.Text
		End Get
		Set(ByVal Value As String)
			Me._CheckBox.Text = Value
		End Set
	End Property


	Public ReadOnly Property Adaptee() As System.Windows.Forms.Control Implements IDropDownDisplayAdapter.Adaptee
		Get
			Return Me._CheckBox
		End Get
	End Property


	Private Sub _CheckBox_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _CheckBox.CheckStateChanged
		RaiseEvent DropDownAppearanceChanged(Me, EventArgs.Empty)
	End Sub

	Private WithEvents _CheckBox As CheckBox	' the adaptee

End Class
