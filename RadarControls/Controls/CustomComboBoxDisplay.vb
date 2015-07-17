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


Public Class CustomComboBoxDisplay
	Inherits ComboBoxDisplay

	Protected Overrides Sub GetDisplayLayout( _
	 ByRef colorBoxRect As System.Drawing.Rectangle, _
	 ByRef textBoxRect As System.Drawing.RectangleF, _
	 ByRef buttonRect As System.Drawing.Rectangle)

		' Let the base class calculate the general layout.
		MyBase.GetDisplayLayout(colorBoxRect, textBoxRect, buttonRect)

		' Make the drop-down button 16x16 pixels.
		buttonRect.Width = 16
		buttonRect.Height = 16
		buttonRect.Y = (Me.Height - buttonRect.Height) \ 2
	End Sub
End Class
