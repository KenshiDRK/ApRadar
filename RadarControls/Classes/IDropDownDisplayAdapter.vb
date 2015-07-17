Imports System.Windows.Forms

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


' Defines contract used by the ColorPicker control to use any other suitable control
' for rendering its appearance and interacting with the user.
Public Interface IDropDownDisplayAdapter

    ' The current color. The ColorPicker doesn't store the color value itself; it uses
    ' this property exposed by the adapter class (2nd task from teh article).
    Property Color() As System.Drawing.Color

    ' The text that the adapter should display. ColorPicker sets the text to the current color
    ' name or to an empty string, if the ColorPicker.TextDisplayed property is set to False.
    ' (3rd task from the article)
    Property Text() As String

    ' This property and event allows the ColorPicker to interrogate and control 
    ' the appearance of the adapter (i.e. dropped-down, or "normal").
    ' (4th task from the article)
    Property HasDropDownAppearance() As Boolean
    Event DropDownAppearanceChanged As EventHandler

    ' This is the actual adapted control. We've deliberately chosen to "unhide" this aspect
    ' of the adapter pattern (that is, the fact that the adaptee itself must be a Control descendant), 
    ' because it simplified the implementation and seemed to be "natural" in this particular context.
    ' (1st task from the article and additional services not mentioned in the article - search
    ' through the code for ".Adaptee" to learn about the various way this property is used).
    ReadOnly Property Adaptee() As Control
End Interface
