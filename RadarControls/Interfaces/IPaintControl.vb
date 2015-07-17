Imports System.Windows.Forms

Public Interface IPaintControl
    Sub InvokePaint(ByVal e As PaintEventArgs)
    Sub InvokePaintBackground(ByVal e As PaintEventArgs)
End Interface
