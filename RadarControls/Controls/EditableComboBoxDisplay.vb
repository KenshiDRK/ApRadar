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


' Extends the ComboBoxDisplay control allowing the color name to be edited directly
' by the user.
Public Class EditableComboBoxDisplay
	Inherits ComboBoxDisplay

	' Our private TextBox descendant that displays the color name and 
	'	processes some required keyboard events.
	Private WithEvents _TextBox As New DialogKeysProcessingTextBox

	' This flag avoids to handle user entered strings twice in the case the user presses
	' the ENTER key.
	Private _EnterKeyInTextBoxPressed As Boolean

	Public Sub New()
		MyBase.New()
		Me._TextBox.Multiline = False
		Me._TextBox.AutoSize = True
		Me._TextBox.WordWrap = False
		Me._TextBox.TabStop = False
		Me._TextBox.BorderStyle = BorderStyle.None
		Me._TextBox.TextAlign = HorizontalAlignment.Left
		Me.Controls.Add(Me._TextBox)
	End Sub


	' Shows/hides and positions the embedded TextBox control according to the current
	' display settings.
	Protected Overrides Sub GetDisplayLayout( _
	 ByRef colorBoxRect As System.Drawing.Rectangle, _
	 ByRef textBoxRect As System.Drawing.RectangleF, _
	 ByRef buttonRect As System.Drawing.Rectangle)

		MyBase.GetDisplayLayout(colorBoxRect, textBoxRect, buttonRect)

		If textBoxRect.IsEmpty Then
			Me._TextBox.Visible = False
		Else
			' The TextBox should cover the same area as the textBox rectangle in the ancestor.
			Me._TextBox.SetBounds( _
			 CInt(textBoxRect.X), CInt(textBoxRect.Y), CInt(textBoxRect.Width), CInt(textBoxRect.Height))
			' Because the TextBox is autosized, we center it vertically AFTER setting its intended bounds.
			Me._TextBox.Location = New Point(Me._TextBox.Location.X, (Me.Height - Me._TextBox.Height) \ 2)
			Me._TextBox.Visible = True
			textBoxRect = RectangleF.Empty
		End If
	End Sub


	Public Overrides Property Text() As String
		Get
			Return MyBase.Text
		End Get
		Set(ByVal Value As String)
			MyBase.Text = Value
			Me._TextBox.Text = Value
		End Set
	End Property


	Public ReadOnly Property TextBox() As TextBox
		Get
			Return Me._TextBox
		End Get
	End Property


	Private Sub _TextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _TextBox.Enter
		Me.Invalidate()		' show the focus rectangle if the user clicked to the embedded textbox directly
		Me._EnterKeyInTextBoxPressed = False
	End Sub


	Private Sub _TextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles _TextBox.Leave
		Me.Invalidate()		' show/hide the focus rectangle, if the user tabbed out or clicked away
	End Sub


	Private Sub _TextBox_AltDownPressed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _TextBox.AltDownPressed
		Me.HasDropDownAppearance = Not Me.HasDropDownAppearance
	End Sub


	Private Sub _TextBox_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles _TextBox.Validated
		If Not Me._EnterKeyInTextBoxPressed Then
			Me.HandleUserEnteredColorName(Me._TextBox.Text, True)
		End If
	End Sub


	Private Sub _TextBox_EnterPressed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _TextBox.EnterPressed
		Me._EnterKeyInTextBoxPressed = True
		Me.HandleUserEnteredColorName(Me._TextBox.Text, False)
	End Sub


	Private Sub _TextBox_EscapePressed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _TextBox.EscapePressed
		Me.HandleUserEnteredColorName(MyBase.Text, False)
	End Sub


	Private Sub HandleUserEnteredColorName(ByVal name As String, ByVal validated As Boolean)
		Try
			' We don't use Color.FromName, because it accepts any name. If the name is not a "KnownColor", it
			' simply creates a color with ARGB values set to 0 - see docs for Color.FromName.

			' The following is case sensitive (i.e. if the user enters "green", it is treated as invalid color.
			' Dim NewColor As KnownColor = CType(System.Enum.Parse(GetType(KnownColor), name), KnownColor)

			' Try to parse what the user entered text by using the color type converter services.
			Dim NewColor As Color = CType(ColorPicker.ColorTypeConverter.ConvertFrom(name), Color)

			' Note that we explicitly disallow entering a color string, which would end up with an empty color name (like "0"
			' for example), because it would counteract the logic for displaying / hidding the TextBox if the
			' text is an empty string.
			If (Len(ColorPicker.ColorTypeConverter.ConvertToString(NewColor)) = 0) Then
				Beep()
				Me._TextBox.Text = MyBase.Text				' retain the original color string
				Return
			End If

			' We've parsed the color name successfully - if we're, in fact, hosted by the ColorPicker control,
			' we set the new color by calling the ColorPicker (this is a public class so one can easily 
			' instantiate and use the control without ColorPicker).
			If TypeOf Me.Parent Is ColorPicker Then
				DirectCast(Me.Parent, ColorPicker).Color = NewColor
			End If

		Catch ex As Exception
			Beep()
			Trace.WriteLine(ex.ToString())
			Me._TextBox.Text = MyBase.Text			' retain the original color string
		End Try

		If Not validated Then
			Me.Focus()
		End If
	End Sub


	' If the user presses a character key while the focus is on the control, we shift
	' the focus to the embedded textbox.
	Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If Me._TextBox.Visible AndAlso Me.IsValidColorStringChar(e.KeyChar) Then
			Me._TextBox.Text = New String(e.KeyChar, 1)			' pass the character to the TextBox
			Me._TextBox.Focus()
			Me._TextBox.SelectionStart = 1			' move the insertion point after the character just pressed
			e.Handled = True
		Else
			MyBase.OnKeyPress(e)
		End If
	End Sub


	Private Function IsValidColorStringChar(ByVal c As Char) As Boolean
		Return Char.IsLetterOrDigit(c) OrElse (c = "#"c)
	End Function


	Protected Overrides Function ProcessDialogKey(ByVal keyData As System.Windows.Forms.Keys) As Boolean
		If (keyData = Keys.F2) AndAlso Me._TextBox.Visible Then
			' The user pressed F2 - start editing the text and position the caret at the end
			' of the TextBox text to emulate Excel-style F2 handling.
			Me._TextBox.Focus()
			Me._TextBox.SelectionStart = Me._TextBox.Text.Length
		Else
			Return MyBase.ProcessDialogKey(keyData)
		End If
	End Function



	Private Class DialogKeysProcessingTextBox
		Inherits TextBox

		Public Event EnterPressed As EventHandler
		Public Event EscapePressed As EventHandler
		Public Event AltDownPressed As EventHandler

		Protected Overrides Function ProcessDialogKey(ByVal keyData As System.Windows.Forms.Keys) As Boolean
			Try
				' Note: keyData is a bitmask, so the following IF statements are evaluated to true
				' only if just the particular tested key has been pressed without any modifiers (Shift, Alt, Ctrl...).
				If keyData = Keys.Return Then
					RaiseEvent EnterPressed(Me, EventArgs.Empty)
					Return True					' True means we've processed the key
				ElseIf keyData = Keys.Escape Then
					RaiseEvent EscapePressed(Me, EventArgs.Empty)
					Return True
				ElseIf keyData = (Keys.Alt Or Keys.Down) Then
					RaiseEvent AltDownPressed(Me, EventArgs.Empty)
					Return True
				Else
					Return MyBase.ProcessDialogKey(keyData)
				End If
			Catch ex As Exception
				Trace.WriteLine(ex.ToString())
			End Try
		End Function

	End Class


End Class
