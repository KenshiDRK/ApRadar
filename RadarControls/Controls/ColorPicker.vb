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
Imports System.Windows.Forms.Design
Imports System.ComponentModel
Imports System.Windows.Forms


<DefaultProperty("Color"), DefaultEvent("ColorChanged")> _
Public Class ColorPicker
    Inherits Control

    ' The IDropDownDisplayAdapter implementation used to render the required appearance
    ' of the control.
    Private WithEvents _DisplayAdapter As IDropDownDisplayAdapter

    ' Should the control display the color's name?
    Private _TextDisplayed As Boolean = True

    ' The IWindowsFormsEditorService implementation used to display the drop-down window.
    Private _EditorService As WindowsFormsEditorService

    ' The event is raised when the Color property changes.
    Public Event ColorChanged As EventHandler

    Private Const DefaultColorName As String = "Black"
    Private Const DefaultWidth As Integer = 121


    ' Defines the appearance of the control.
    Public Enum ColorPickerAppearance
        Button = 0
        ComboBox = 1
        EditableComboBox = 2
        Custom = 3
    End Enum


    Public Sub New(ByVal c As Color)
        MyBase.New()

        ' Init the display adapter.
        Me.DisplayAdapter = New ComboBoxDisplayAdapter(New EditableComboBoxDisplay)
        Me.SetColor(c)

        _EditorService = New WindowsFormsEditorService(Me)
        MyBase.TabStop = False
    End Sub


    Public Sub New()
        Me.New(System.Drawing.Color.FromName(DefaultColorName))
    End Sub


    <Description("The currently selected color."), Category("Appearance"), _
    DefaultValue(GetType(Color), DefaultColorName)> _
    Public Property Color() As Color
        Get
            Return Me._DisplayAdapter.Color
        End Get
        Set(ByVal Value As Color)
            Me.SetColor(Value)
            RaiseEvent ColorChanged(Me, EventArgs.Empty)
        End Set
    End Property


    <Description("True meanse the control displays the currently selected color's name, False otherwise."), _
    Category("Appearance"), DefaultValue(True)> _
    Public Property TextDisplayed() As Boolean
        Get
            Return _TextDisplayed
        End Get
        Set(ByVal Value As Boolean)
            _TextDisplayed = Value
            Me.SetColor(Me.Color)
        End Set
    End Property



    <Description("Sets or returns the appearance of the control."), _
    Category("Appearance"), DefaultValue(ColorPickerAppearance.Button)> _
    Public Property Appearance() As ColorPickerAppearance
        Get
            ' Return the appropriate enum constant according to the type of the control
            ' used for rendering.
            ' Note: We must check the EditableComboBoxDisplay type BEFORE the ComboBoxDisplay
            ' type, because the EditableComboBoxDisplay control derives from ComboBoxDisplay.
            ' If we'd have checked the ComboBoxDisplay type first, even the EditableComboBoxDisplay
            ' would satisfy the test.
            If TypeOf Me._DisplayAdapter.Adaptee Is CheckBox Then
                Return ColorPickerAppearance.Button
            ElseIf TypeOf Me._DisplayAdapter.Adaptee Is EditableComboBoxDisplay Then
                Return ColorPickerAppearance.EditableComboBox
            ElseIf TypeOf Me._DisplayAdapter.Adaptee Is ComboBoxDisplay Then
                Return ColorPickerAppearance.ComboBox
            Else
                Return ColorPickerAppearance.Custom
            End If
        End Get
        Set(ByVal Value As ColorPickerAppearance)
            ' NOOP if the appearance wouldn't change.
            If Value = Me.Appearance Then
                Return
            End If

            ' Instantiate and set the appropriate IDropDownDisplayAdapter implementation.
            If Value = ColorPickerAppearance.Button Then
                Me.DisplayAdapter = New CheckBoxDisplayAdapter(New CheckBox)
            ElseIf Value = ColorPickerAppearance.ComboBox Then
                Me.DisplayAdapter = New ComboBoxDisplayAdapter(New ComboBoxDisplay)
            ElseIf Value = ColorPickerAppearance.EditableComboBox Then
                Me.DisplayAdapter = New ComboBoxDisplayAdapter(New EditableComboBoxDisplay)
            Else
                Throw New InvalidOperationException( _
                 "To set custom appearance, set the ColorPicker.DisplayAdapter property at runtime.")
            End If

        End Set
    End Property


    ' Associates the IDropDownDisplayAdapter implementation with the control.
    <Browsable(False)> _
    Public Property DisplayAdapter() As IDropDownDisplayAdapter
        Get
            Return Me._DisplayAdapter
        End Get
        Set(ByVal Value As IDropDownDisplayAdapter)
            If Value Is Nothing Then
                Throw New ArgumentNullException("Value")
            End If

            ' To centralize control, this method is called from the constructor as well as from
            ' user code. In the call from user code, we have to preserve the current color selection
            ' so it doesn't change to default color after the adapter is set up.
            Dim SavedColor As Color = Color.Black
            If Not Me._DisplayAdapter Is Nothing Then
                SavedColor = Me._DisplayAdapter.Color
            End If
            Me.Controls.Clear()
            Me._DisplayAdapter = Value
            Me._DisplayAdapter.Adaptee.Dock = DockStyle.Fill
            Me.Controls.Add(Me._DisplayAdapter.Adaptee)
            Me.SetColor(SavedColor)
        End Set
    End Property



    ' Sets the associated CheckBox color and Text according to the TextDisplayed property value.
    Private Sub SetColor(ByVal c As Color)
        Me._DisplayAdapter.Color = c
        If _TextDisplayed Then
            Me._DisplayAdapter.Text = ColorPicker.ColorTypeConverter.ConvertToString(c)
        Else
            Me._DisplayAdapter.Text = String.Empty
        End If
    End Sub


    ' Primitive color inversion.
    Public Shared Function GetInvertedColor(ByVal c As Color) As Color
        If (CInt(c.R) + CInt(c.G) + CInt(c.B)) > ((255I * 3I) \ 2I) Then
            Return System.Drawing.Color.Black
        Else
            Return System.Drawing.Color.White
        End If
    End Function


    ' Shortcut to the system-provided Color type converter.
    Public Shared ReadOnly Property ColorTypeConverter() As TypeConverter
        Get
            Static _Converter As TypeConverter
            If _Converter Is Nothing Then
                _Converter = System.ComponentModel.TypeDescriptor.GetConverter(GetType(Color))
                Debug.Assert(Not _Converter Is Nothing)
            End If
            Return _Converter
        End Get
    End Property


    ' If the associated display is dropped down, we'll display the drop-down UI.
    ' Otherwise we'll close it.
    Private Sub OnDropDownAppearanceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _DisplayAdapter.DropDownAppearanceChanged
        If Me._DisplayAdapter.HasDropDownAppearance Then
            Me.ShowDropDown()
        Else
            Me.CloseDropDown()
        End If
    End Sub


    Private Sub ShowDropDown()
        Try
            ' This is the Color type editor - it displays the drop-down UI calling
            ' our IWindowsFormsEditorService implementation.
            Dim Editor As New System.Drawing.Design.ColorEditor

            ' Display the UI.
            Dim C As Color = Me.Color
            Dim NewValue As Object = Editor.EditValue(_EditorService, C)

            ' If the user didn't cancel the selection, remember the new color.
            If (Not NewValue Is Nothing) AndAlso (Not _EditorService.Canceled) Then
                Me.Color = CType(NewValue, Color)
            End If

            ' Finally, "pop-up" the associated dropdow display.
            Me._DisplayAdapter.HasDropDownAppearance = False

        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
        End Try
    End Sub


    Private Sub CloseDropDown()
        _EditorService.CloseDropDown()
    End Sub


    Protected Overrides Sub OnEnter(ByVal e As EventArgs)
        MyBase.OnEnter(e)
        ' Invalidate the display so it shows the focus rectangle.
        Me._DisplayAdapter.Adaptee.Invalidate()
    End Sub


    Protected Overrides Sub OnLeave(ByVal e As EventArgs)
        MyBase.OnLeave(e)
        ' Invalidate the display so it doesn't show the focus rectangle.
        Me._DisplayAdapter.Adaptee.Invalidate()
    End Sub


    Protected Overrides ReadOnly Property DefaultSize() As System.Drawing.Size
        Get
            Return New Size(ColorPicker.DefaultWidth, SystemInformation.CaptionHeight + 1)
        End Get
    End Property



    ' We've shadowed the TabStop property, because we don't want the control to be tabbed to 
    ' directly. Instead, the adaptee control is used to interact with the user, so we delegate
    ' to it.
    Public Shadows Property TabStop() As Boolean
        Get
            Return Me._DisplayAdapter.Adaptee.TabStop
        End Get
        Set(ByVal Value As Boolean)
            Me._DisplayAdapter.Adaptee.TabStop = Value
        End Set
    End Property

    ' No need to display ForeColor and BackColor and Text in the property browser:

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Overrides Property ForeColor() As System.Drawing.Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            MyBase.ForeColor = Value
        End Set
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Overrides Property BackColor() As System.Drawing.Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            MyBase.BackColor = Value
        End Set
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal Value As String)
            MyBase.Text = Value
        End Set
    End Property


End Class



