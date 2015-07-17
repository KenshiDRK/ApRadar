Imports System
Imports System.Collections.Specialized
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms



#Region "Public Enums"

' Enum for possible RTF colors
Public Enum RtfColor
    Black
    Maroon
    Green
    Olive
    Navy
    Purple
    Teal
    Gray
    Silver
    Red
    Lime
    Yellow
    Blue
    Fuchsia
    Aqua
    White
End Enum

#End Region

''' <summary>
''' This class adds the following functionality to RichTextBox:
''' 
''' 1.	Allows plain text to be inserted or appended programmatically to RTF
'''		content.
''' 2.	Allows the font, text color, and highlight color of plain text to be
'''		specified when inserting or appending text as RTF.
'''	3.	Allows images to be inserted programmatically, or with interaction from
'''		the user.
''' </summary>
''' <remarks>
''' Many solutions to the problem of programmatically inserting images
''' into a RichTextBox use the clipboard or hard code the RTF for
''' the image in the program.  This class is an attempt to make the process of
''' inserting images at runtime more flexible without the overhead of maintaining
''' the clipboard or the use of huge, cumbersome strings.
''' 
''' RTF Specification v1.6 was used and is referred to many times in this document.
''' http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnrtfspec/html/rtfspec.asp
''' 
''' For information about the RichEdit (Unmanaged RichTextBox) ...
''' http://msdn.microsoft.com/library/default.asp?url=/library/en-us/shellcc/platform/commctls/richedit/richeditcontrols/aboutricheditcontrols.asp
''' </remarks>
Public Class ExRichTextBox
    Inherits System.Windows.Forms.RichTextBox

#Region "My Enums"

    ' Specifies the flags/options for the unmanaged call to the GDI+ method
    ' Metafile.EmfToWmfBits().
    Private Enum EmfToWmfBitsFlags

        ' Use the default conversion
        EmfToWmfBitsFlagsDefault = &H0

        ' Embedded the source of the EMF metafiel within the resulting WMF
        ' metafile
        EmfToWmfBitsFlagsEmbedEmf = &H1

        ' Place a 22-byte header in the resulting WMF file.  The header is
        ' required for the metafile to be considered placeable.
        EmfToWmfBitsFlagsIncludePlaceable = &H2

        ' Don't simulate clipping by using the XOR operator.
        EmfToWmfBitsFlagsNoXORClip = &H4
    End Enum

#End Region

#Region "My Structs"

    ' Definitions for colors in an RTF document
    Private Structure RtfColorDef
        Dim none As String
        Public Shared Black As String = "\red0\green0\blue0"
        Public Shared Maroon As String = "\red128\green0\blue0"
        Public Shared Green As String = "\red0\green128\blue0"
        Public Shared Olive As String = "\red128\green128\blue0"
        Public Shared Navy As String = "\red0\green0\blue128"
        Public Shared Purple As String = "\red128\green0\blue128"
        Public Shared Teal As String = "\red0\green128\blue128"
        Public Shared Gray As String = "\red128\green128\blue128"
        Public Shared Silver As String = "\red192\green192\blue192"
        Public Shared Red As String = "\red255\green0\blue0"
        Public Shared Lime As String = "\red0\green255\blue0"
        Public Shared Yellow As String = "\red255\green255\blue0"
        Public Shared Blue As String = "\red0\green0\blue255"
        Public Shared Fuchsia As String = "\red255\green0\blue255"
        Public Shared Aqua As String = "\red0\green255\blue255"
        Public Shared White As String = "\red255\green255\blue255"
    End Structure

    ' Control words for RTF font families
    Private Structure RtfFontFamilyDef
        Dim none As String
        Public Shared Unknown As String = "\fnil"
        Public Shared Roman As String = "\froman"
        Public Shared Swiss As String = "\fswiss"
        Public Shared Modern As String = "\fmodern"
        Public Shared Script As String = "\fscript"
        Public Shared Decor As String = "\fdecor"
        Public Shared Technical As String = "\ftech"
        Public Shared BiDirect As String = "\fbidi"
    End Structure

#End Region

#Region "My Constants"

    ' Not used in this application.  Descriptions can be found with documentation
    ' of Windows GDI function SetMapMode
    Private Const MM_TEXT As Integer = 1
    Private Const MM_LOMETRIC As Integer = 2
    Private Const MM_HIMETRIC As Integer = 3
    Private Const MM_LOENGLISH As Integer = 4
    Private Const MM_HIENGLISH As Integer = 5
    Private Const MM_TWIPS As Integer = 6

    ' Ensures that the metafile maintains a 1:1 aspect ratio
    Private Const MM_ISOTROPIC As Integer = 7

    ' Allows the x-coordinates and y-coordinates of the metafile to be adjusted
    ' independently
    Private Const MM_ANISOTROPIC As Integer = 8

    ' Represents an unknown font family
    Private Const FF_UNKNOWN As String = "UNKNOWN"

    ' The number of hundredths of millimeters (0.01 mm) in an inch
    ' For more information, see GetImagePrefix() method.
    Private Const HMM_PER_INCH As Integer = 2540

    ' The number of twips in an inch
    ' For more information, see GetImagePrefix() method.
    Private Const TWIPS_PER_INCH As Integer = 1440

#End Region

#Region "My Privates"

    ' The default text color
    Private m_textColor As RtfColor

    ' The default text background color
    Private highlightColor As RtfColor

    ' Dictionary that maps color enums to RTF color codes
    Private _rtfColor As HybridDictionary

    ' Dictionary that mapas Framework font families to RTF font families
    Private rtfFontFamily As HybridDictionary

    ' The horizontal resolution at which the control is being displayed
    Private xDpi As Single

    ' The vertical resolution at which the control is being displayed
    Private yDpi As Single

#End Region

#Region "Elements required to create an RTF document"

    ' RTF HEADER
    '		 * ----------
    '		 * 
    '		 * \rtf[N]		- For text to be considered to be RTF, it must be enclosed in this tag.
    '		 *				  rtf1 is used because the RichTextBox conforms to RTF Specification
    '		 *				  version 1.
    '		 * \ansi		- The character set.
    '		 * \ansicpg[N]	- Specifies that unicode characters might be embedded. ansicpg1252
    '		 *				  is the default used by Windows.
    '		 * \deff[N]		- The default font. \deff0 means the default font is the first font
    '		 *				  found.
    '		 * \deflang[N]	- The default language. \deflang1033 specifies US English.
    '		 * 

    Private Const RTF_HEADER As String = "{\rtf1\ansi\ansicpg1252\deff0\deflang1033"

    ' RTF DOCUMENT AREA
    '		 * -----------------
    '		 * 
    '		 * \viewkind[N]	- The type of view or zoom level.  \viewkind4 specifies normal view.
    '		 * \uc[N]		- The number of bytes corresponding to a Unicode character.
    '		 * \pard		- Resets to default paragraph properties
    '		 * \cf[N]		- Foreground color.  \cf1 refers to the color at index 1 in
    '		 *				  the color table
    '		 * \f[N]		- Font number. \f0 refers to the font at index 0 in the font
    '		 *				  table.
    '		 * \fs[N]		- Font size in half-points.
    '		 * 

    Private Const RTF_DOCUMENT_PRE As String = "\viewkind4\uc1\pard\cf1\f0\fs20"
    Private Const RTF_DOCUMENT_POST As String = "\cf0\fs17}"
    Private RTF_IMAGE_POST As String = "}"

#End Region

#Region "Accessors"

    ' TODO: This can be ommitted along with RemoveBadCharacters
    ' Overrides the default implementation of RTF.  This is done because the control
    ' was originally developed to run in an instant messenger that uses the
    ' Jabber XML-based protocol.  The framework would throw an exception when the
    ' XML contained the null character, so I filtered out.
    Public Shadows Property Rtf() As String
        Get
            Return RemoveBadChars(MyBase.Rtf)
        End Get
        Set(ByVal value As String)
            MyBase.Rtf = value
        End Set
    End Property

    ' The color of the text
    Public Property TextColor() As RtfColor
        Get
            Return m_textColor
        End Get
        Set(ByVal value As RtfColor)
            m_textColor = value
        End Set
    End Property

    ' The color of the highlight
    Public Property HiglightColor() As RtfColor
        Get
            Return highlightColor
        End Get
        Set(ByVal value As RtfColor)
            highlightColor = value
        End Set
    End Property

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Initializes the text colors, creates dictionaries for RTF colors and
    ''' font families, and stores the horizontal and vertical resolution of
    ''' the RichTextBox's graphics context.
    ''' </summary>
    Public Sub New()
        MyBase.New()

        ' Initialize default text and background colors
        m_textColor = rtfColor.Black
        highlightColor = RtfColor.White

        ' Initialize the dictionary mapping color codes to definitions
        _rtfColor = New HybridDictionary()
        _rtfColor.Add(RtfColor.Aqua, RtfColorDef.Aqua)
        _rtfColor.Add(RtfColor.Black, RtfColorDef.Black)
        _rtfColor.Add(RtfColor.Blue, RtfColorDef.Blue)
        _rtfColor.Add(RtfColor.Fuchsia, RtfColorDef.Fuchsia)
        _rtfColor.Add(RtfColor.Gray, RtfColorDef.Gray)
        _rtfColor.Add(RtfColor.Green, RtfColorDef.Green)
        _rtfColor.Add(RtfColor.Lime, RtfColorDef.Lime)
        _rtfColor.Add(RtfColor.Maroon, RtfColorDef.Maroon)
        _rtfColor.Add(RtfColor.Navy, RtfColorDef.Navy)
        _rtfColor.Add(RtfColor.Olive, RtfColorDef.Olive)
        _rtfColor.Add(RtfColor.Purple, RtfColorDef.Purple)
        _rtfColor.Add(RtfColor.Red, RtfColorDef.Red)
        _rtfColor.Add(RtfColor.Silver, RtfColorDef.Silver)
        _rtfColor.Add(RtfColor.Teal, RtfColorDef.Teal)
        _rtfColor.Add(RtfColor.White, RtfColorDef.White)
        _rtfColor.Add(RtfColor.Yellow, RtfColorDef.Yellow)

        ' Initialize the dictionary mapping default Framework font families to
        ' RTF font families
        rtfFontFamily = New HybridDictionary()
        rtfFontFamily.Add(FontFamily.GenericMonospace.Name, RtfFontFamilyDef.Modern)
        rtfFontFamily.Add(FontFamily.GenericSansSerif, RtfFontFamilyDef.Swiss)
        rtfFontFamily.Add(FontFamily.GenericSerif, RtfFontFamilyDef.Roman)
        rtfFontFamily.Add(FF_UNKNOWN, RtfFontFamilyDef.Unknown)

        ' Get the horizontal and vertical resolutions at which the object is
        ' being displayed
        Using _graphics As Graphics = Me.CreateGraphics()
            xDpi = _graphics.DpiX
            yDpi = _graphics.DpiY
        End Using
    End Sub

    ''' <summary>
    ''' Calls the default constructor then sets the text color.
    ''' </summary>
    ''' <param name="_textColor"></param>
    Public Sub New(ByVal _textColor As RtfColor)
        Me.New()
        m_textColor = _textColor
    End Sub

    ''' <summary>
    ''' Calls the default constructor then sets te text and highlight colors.
    ''' </summary>
    ''' <param name="_textColor"></param>
    ''' <param name="_highlightColor"></param>
    Public Sub New(ByVal _textColor As RtfColor, ByVal _highlightColor As RtfColor)
        Me.New()
        m_textColor = _textColor
        highlightColor = _highlightColor
    End Sub

#End Region

#Region "Append RTF or Text to RichTextBox Contents"

    ''' <summary>
    ''' Assumes the string passed as a paramter is valid RTF text and attempts
    ''' to append it as RTF to the content of the control.
    ''' </summary>
    ''' <param name="_rtf"></param>
    Public Sub AppendRtf(ByVal _rtf As String)

        ' Move caret to the end of the text
        Me.[Select](Me.TextLength, 0)

        ' Since SelectedRtf is null, this will append the string to the
        ' end of the existing RTF
        Me.SelectedRtf = _rtf
    End Sub

    ''' <summary>
    ''' Assumes that the string passed as a parameter is valid RTF text and
    ''' attempts to insert it as RTF into the content of the control.
    ''' </summary>
    ''' <remarks>
    ''' NOTE: The text is inserted wherever the caret is at the time of the call,
    ''' and if any text is selected, that text is replaced.
    ''' </remarks>
    ''' <param name="_rtf"></param>
    Public Sub InsertRtf(ByVal _rtf As String)
        Me.SelectedRtf = _rtf
    End Sub

    ''' <summary>
    ''' Appends the text using the current font, text, and highlight colors.
    ''' </summary>
    ''' <param name="_text"></param>
    Public Sub AppendTextAsRtf(ByVal _text As String)
        AppendTextAsRtf(_text, Me.Font)
    End Sub


    ''' <summary>
    ''' Appends the text using the given font, and current text and highlight
    ''' colors.
    ''' </summary>
    ''' <param name="_text"></param>
    ''' <param name="_font"></param>
    Public Sub AppendTextAsRtf(ByVal _text As String, ByVal _font As Font)
        AppendTextAsRtf(_text, _font, m_textColor)
    End Sub

    ''' <summary>
    ''' Appends the text using the given font and text color, and the current
    ''' highlight color.
    ''' </summary>
    ''' <param name="_text"></param>
    ''' <param name="_font"></param>
    ''' <param name="_textColor"></param>
    Public Sub AppendTextAsRtf(ByVal _text As String, ByVal _font As Font, ByVal _textColor As RtfColor)
        AppendTextAsRtf(_text, _font, _textColor, highlightColor)
    End Sub

    ''' <summary>
    ''' Appends the text using the given font, text, and highlight colors.  Simply
    ''' moves the caret to the end of the RichTextBox's text and makes a call to
    ''' insert.
    ''' </summary>
    ''' <param name="_text"></param>
    ''' <param name="_font"></param>
    ''' <param name="_textColor"></param>
    ''' <param name="_backColor"></param>
    Public Sub AppendTextAsRtf(ByVal _text As String, ByVal _font As Font, ByVal _textColor As RtfColor, ByVal _backColor As RtfColor)
        ' Move carret to the end of the text
        Me.[Select](Me.TextLength, 0)

        InsertTextAsRtf(_text, _font, _textColor, _backColor)
    End Sub

#End Region

#Region "Insert Plain Text"

    ''' <summary>
    ''' Inserts the text using the current font, text, and highlight colors.
    ''' </summary>
    ''' <param name="_text"></param>
    Public Sub InsertTextAsRtf(ByVal _text As String)
        InsertTextAsRtf(_text, Me.Font)
    End Sub


    ''' <summary>
    ''' Inserts the text using the given font, and current text and highlight
    ''' colors.
    ''' </summary>
    ''' <param name="_text"></param>
    ''' <param name="_font"></param>
    Public Sub InsertTextAsRtf(ByVal _text As String, ByVal _font As Font)
        InsertTextAsRtf(_text, _font, m_textColor)
    End Sub

    ''' <summary>
    ''' Inserts the text using the given font and text color, and the current
    ''' highlight color.
    ''' </summary>
    ''' <param name="_text"></param>
    ''' <param name="_font"></param>
    ''' <param name="_textColor"></param>
    Public Sub InsertTextAsRtf(ByVal _text As String, ByVal _font As Font, ByVal _textColor As RtfColor)
        InsertTextAsRtf(_text, _font, _textColor, highlightColor)
    End Sub

    ''' <summary>
    ''' Inserts the text using the given font, text, and highlight colors.  The
    ''' text is wrapped in RTF codes so that the specified formatting is kept.
    ''' You can only assign valid RTF to the RichTextBox.Rtf property, else
    ''' an exception is thrown.  The RTF string should follow this format ...
    ''' 
    ''' {\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{[FONTS]}{\colortbl ;[COLORS]}}
    ''' \viewkind4\uc1\pard\cf1\f0\fs20 [DOCUMENT AREA] }
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' NOTE: The text is inserted wherever the caret is at the time of the call,
    ''' and if any text is selected, that text is replaced.
    ''' </remarks>
    ''' <param name="_text"></param>
    ''' <param name="_font"></param>
    ''' <param name="_textColor"></param>
    ''' <param name="_backColor "></param>
    Public Sub InsertTextAsRtf(ByVal _text As String, ByVal _font As Font, ByVal _textColor As RtfColor, ByVal _backColor As RtfColor)

        Dim _rtf As New StringBuilder()

        ' Append the RTF header
        _rtf.Append(RTF_HEADER)

        ' Create the font table from the font passed in and append it to the
        ' RTF string
        _rtf.Append(GetFontTable(_font))

        ' Create the color table from the colors passed in and append it to the
        ' RTF string
        _rtf.Append(GetColorTable(_textColor, _backColor))

        ' Create the document area from the text to be added as RTF and append
        ' it to the RTF string.
        _rtf.Append(GetDocumentArea(_text, _font))

        Me.SelectedRtf = _rtf.ToString()
    End Sub

    ''' <summary>
    ''' Creates the Document Area of the RTF being inserted. The document area
    ''' (in this case) consists of the text being added as RTF and all the
    ''' formatting specified in the Font object passed in. This should have the
    ''' form ...
    ''' 
    ''' \viewkind4\uc1\pard\cf1\f0\fs20 [DOCUMENT AREA] }
    '''
    ''' </summary>
    ''' <param name="_text"></param>
    ''' <param name="_font"></param>
    ''' <returns>
    ''' The document area as a string.
    ''' </returns>
    Private Function GetDocumentArea(ByVal _text As String, ByVal _font As Font) As String

        Dim _doc As New StringBuilder()

        ' Append the standard RTF document area control string
        _doc.Append(RTF_DOCUMENT_PRE)

        ' Set the highlight color (the color behind the text) to the
        ' third color in the color table.  See GetColorTable for more details.
        _doc.Append("\highlight2")

        ' If the font is bold, attach corresponding tag
        If _font.Bold Then
            _doc.Append("\b")
        End If

        ' If the font is italic, attach corresponding tag
        If _font.Italic Then
            _doc.Append("\i")
        End If

        ' If the font is strikeout, attach corresponding tag
        If _font.Strikeout Then
            _doc.Append("\strike")
        End If

        ' If the font is underlined, attach corresponding tag
        If _font.Underline Then
            _doc.Append("\ul")
        End If

        ' Set the font to the first font in the font table.
        ' See GetFontTable for more details.
        _doc.Append("\f0")

        ' Set the size of the font.  In RTF, font size is measured in
        ' half-points, so the font size is twice the value obtained from
        ' Font.SizeInPoints
        _doc.Append("\fs")
        _doc.Append(CInt(Math.Round((2 * _font.SizeInPoints))))

        ' Apppend a space before starting actual text (for clarity)
        _doc.Append(" ")

        ' Append actual text, however, replace newlines with RTF \par.
        ' Any other special text should be handled here (e.g.) tabs, etc.
        _doc.Append(_text.Replace(vbLf, "\par "))

        ' RTF isn't strict when it comes to closing control words, but what the
        ' heck ...

        ' Remove the highlight
        _doc.Append("\highlight0")

        ' If font is bold, close tag
        If _font.Bold Then
            _doc.Append("\b0")
        End If

        ' If font is italic, close tag
        If _font.Italic Then
            _doc.Append("\i0")
        End If

        ' If font is strikeout, close tag
        If _font.Strikeout Then
            _doc.Append("\strike0")
        End If

        ' If font is underlined, cloes tag
        If _font.Underline Then
            _doc.Append("\ulnone")
        End If

        ' Revert back to default font and size
        _doc.Append("\f0")
        _doc.Append("\fs20")

        ' Close the document area control string
        _doc.Append(RTF_DOCUMENT_POST)

        Return _doc.ToString()
    End Function

#End Region

#Region "Insert Image"

    ''' <summary>
    ''' Inserts an image into the RichTextBox.  The image is wrapped in a Windows
    ''' Format Metafile, because although Microsoft discourages the use of a WMF,
    ''' the RichTextBox (and even MS Word), wraps an image in a WMF before inserting
    ''' the image into a document.  The WMF is attached in HEX format (a string of
    ''' HEX numbers).
    ''' 
    ''' The RTF Specification v1.6 says that you should be able to insert bitmaps,
    ''' .jpegs, .gifs, .pngs, and Enhanced Metafiles (.emf) directly into an RTF
    ''' document without the WMF wrapper. This works fine with MS Word,
    ''' however, when you don't wrap images in a WMF, WordPad and
    ''' RichTextBoxes simply ignore them.  Both use the riched20.dll or msfted.dll.
    ''' </summary>
    ''' <remarks>
    ''' NOTE: The image is inserted wherever the caret is at the time of the call,
    ''' and if any text is selected, that text is replaced.
    ''' </remarks>
    ''' <param name="_image"></param>
    Public Sub InsertImage(ByVal _image As Image)

        Dim _rtf As New StringBuilder()

        ' Append the RTF header
        _rtf.Append(RTF_HEADER)

        ' Create the font table using the RichTextBox's current font and append
        ' it to the RTF string
        _rtf.Append(GetFontTable(Me.Font))

        ' Create the image control string and append it to the RTF string
        _rtf.Append(GetImagePrefix(_image))

        ' Create the Windows Metafile and append its bytes in HEX format
        _rtf.Append(GetRtfImage(_image))

        ' Close the RTF image control string
        _rtf.Append(RTF_IMAGE_POST)

        Me.SelectedRtf = _rtf.ToString()
    End Sub

    ''' <summary>
    ''' Creates the RTF control string that describes the image being inserted.
    ''' This description (in this case) specifies that the image is an
    ''' MM_ANISOTROPIC metafile, meaning that both X and Y axes can be scaled
    ''' independently.  The control string also gives the images current dimensions,
    ''' and its target dimensions, so if you want to control the size of the
    ''' image being inserted, this would be the place to do it. The prefix should
    ''' have the form ...
    ''' 
    ''' {\pict\wmetafile8\picw[A]\pich[B]\picwgoal[C]\pichgoal[D]
    ''' 
    ''' where ...
    ''' 
    ''' A	= current width of the metafile in hundredths of millimeters (0.01mm)
    '''		= Image Width in Inches * Number of (0.01mm) per inch
    '''		= (Image Width in Pixels / Graphics Context's Horizontal Resolution) * 2540
    '''		= (Image Width in Pixels / Graphics.DpiX) * 2540
    ''' 
    ''' B	= current height of the metafile in hundredths of millimeters (0.01mm)
    '''		= Image Height in Inches * Number of (0.01mm) per inch
    '''		= (Image Height in Pixels / Graphics Context's Vertical Resolution) * 2540
    '''		= (Image Height in Pixels / Graphics.DpiX) * 2540
    ''' 
    ''' C	= target width of the metafile in twips
    '''		= Image Width in Inches * Number of twips per inch
    '''		= (Image Width in Pixels / Graphics Context's Horizontal Resolution) * 1440
    '''		= (Image Width in Pixels / Graphics.DpiX) * 1440
    ''' 
    ''' D	= target height of the metafile in twips
    '''		= Image Height in Inches * Number of twips per inch
    '''		= (Image Height in Pixels / Graphics Context's Horizontal Resolution) * 1440
    '''		= (Image Height in Pixels / Graphics.DpiX) * 1440
    '''	
    ''' </summary>
    ''' <remarks>
    ''' The Graphics Context's resolution is simply the current resolution at which
    ''' windows is being displayed.  Normally it's 96 dpi, but instead of assuming
    ''' I just added the code.
    ''' 
    ''' According to Ken Howe at pbdr.com, "Twips are screen-independent units
    ''' used to ensure that the placement and proportion of screen elements in
    ''' your screen application are the same on all display systems."
    ''' 
    ''' Units Used
    ''' ----------
    ''' 1 Twip = 1/20 Point
    ''' 1 Point = 1/72 Inch
    ''' 1 Twip = 1/1440 Inch
    ''' 
    ''' 1 Inch = 2.54 cm
    ''' 1 Inch = 25.4 mm
    ''' 1 Inch = 2540 (0.01)mm
    ''' </remarks>
    ''' <param name="_image"></param>
    ''' <returns></returns>
    Private Function GetImagePrefix(ByVal _image As Image) As String

        Dim _rtf As New StringBuilder()

        ' Calculate the current width of the image in (0.01)mm
        Dim picw As Integer = CInt(Math.Round((_image.Width / xDpi) * HMM_PER_INCH))

        ' Calculate the current height of the image in (0.01)mm
        Dim pich As Integer = CInt(Math.Round((_image.Height / yDpi) * HMM_PER_INCH))

        ' Calculate the target width of the image in twips
        Dim picwgoal As Integer = CInt(Math.Round((_image.Width / xDpi) * TWIPS_PER_INCH))

        ' Calculate the target height of the image in twips
        Dim pichgoal As Integer = CInt(Math.Round((_image.Height / yDpi) * TWIPS_PER_INCH))

        ' Append values to RTF string
        _rtf.Append("{\pict\wmetafile8")
        _rtf.Append("\picw")
        _rtf.Append(picw)
        _rtf.Append("\pich")
        _rtf.Append(pich)
        _rtf.Append("\picwgoal")
        _rtf.Append(picwgoal)
        _rtf.Append("\pichgoal")
        _rtf.Append(pichgoal)
        _rtf.Append(" ")

        Return _rtf.ToString()
    End Function

    ''' <summary>
    ''' Use the EmfToWmfBits function in the GDI+ specification to convert a 
    ''' Enhanced Metafile to a Windows Metafile
    ''' </summary>
    ''' <param name="_hEmf">
    ''' A handle to the Enhanced Metafile to be converted
    ''' </param>
    ''' <param name="_bufferSize">
    ''' The size of the buffer used to store the Windows Metafile bits returned
    ''' </param>
    ''' <param name="_buffer">
    ''' An array of bytes used to hold the Windows Metafile bits returned
    ''' </param>
    ''' <param name="_mappingMode">
    ''' The mapping mode of the image.  This control uses MM_ANISOTROPIC.
    ''' </param>
    ''' <param name="_flags">
    ''' Flags used to specify the format of the Windows Metafile returned
    ''' </param>
    <DllImportAttribute("gdiplus.dll")> _
    Private Shared Function GdipEmfToWmfBits(ByVal _hEmf As IntPtr, ByVal _bufferSize As UInteger, ByVal _buffer As Byte(), ByVal _mappingMode As Integer, ByVal _flags As EmfToWmfBitsFlags) As UInteger
    End Function


    ''' <summary>
    ''' Wraps the image in an Enhanced Metafile by drawing the image onto the
    ''' graphics context, then converts the Enhanced Metafile to a Windows
    ''' Metafile, and finally appends the bits of the Windows Metafile in HEX
    ''' to a string and returns the string.
    ''' </summary>
    ''' <param name="_image"></param>
    ''' <returns>
    ''' A string containing the bits of a Windows Metafile in HEX
    ''' </returns>
    Private Function GetRtfImage(ByVal _image As Image) As String

        Dim _rtf As StringBuilder = Nothing

        ' Used to store the enhanced metafile
        Dim _stream As MemoryStream = Nothing

        ' Used to create the metafile and draw the image
        Dim _graphics As Graphics = Nothing

        ' The enhanced metafile
        Dim _metaFile As Metafile = Nothing

        ' Handle to the device context used to create the metafile
        Dim _hdc As IntPtr

        Try
            _rtf = New StringBuilder()
            _stream = New MemoryStream()

            ' Get a graphics context from the RichTextBox
            _graphics = Me.CreateGraphics()

            ' Get the device context from the graphics context
            _hdc = _graphics.GetHdc()

            ' Create a new Enhanced Metafile from the device context
            _metaFile = New Metafile(_stream, _hdc)

            ' Release the device context
            _graphics.ReleaseHdc(_hdc)


            ' Get a graphics context from the Enhanced Metafile
            Using g1 = Graphics.FromImage(_metaFile)
                ' Draw the image on the Enhanced Metafile
                g1.DrawImage(_image, New Rectangle(0, 0, _image.Width, _image.Height))
            End Using

            
            ' Get the handle of the Enhanced Metafile
            Dim _hEmf As IntPtr = _metaFile.GetHenhmetafile

            ' A call to EmfToWmfBits with a null buffer return the size of the
            ' buffer need to store the WMF bits.  Use this to get the buffer
            ' size.
            Dim _bufferSize As UInteger = GdipEmfToWmfBits(_hEmf, 0, Nothing, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault)

            ' Create an array to hold the bits
            Dim _buffer As Byte() = New Byte(_bufferSize - 1) {}

            ' A call to EmfToWmfBits with a valid buffer copies the bits into the
            ' buffer an returns the number of bits in the WMF.  
            Dim _convertedSize As UInteger = GdipEmfToWmfBits(_hEmf, _bufferSize, _buffer, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsIncludePlaceable)

            ' Append the bits to the RTF string
            For i As Integer = 0 To _buffer.Length - 1
                _rtf.Append([String].Format("{0:X2}", _buffer(i)))
            Next

            Return _rtf.ToString()
        Finally
            If _graphics IsNot Nothing Then
                _graphics.Dispose()
            End If
            If _metaFile IsNot Nothing Then
                _metaFile.Dispose()
            End If
            If _stream IsNot Nothing Then
                _stream.Close()
            End If
        End Try
    End Function

#End Region

#Region "RTF Helpers"

    ''' <summary>
    ''' Creates a font table from a font object.  When an Insert or Append 
    ''' operation is performed a font is either specified or the default font
    ''' is used.  In any case, on any Insert or Append, only one font is used,
    ''' thus the font table will always contain a single font.  The font table
    ''' should have the form ...
    ''' 
    ''' {\fonttbl{\f0\[FAMILY]\fcharset0 [FONT_NAME];}
    ''' </summary>
    ''' <param name="_font"></param>
    ''' <returns></returns>
    Private Function GetFontTable(ByVal _font As Font) As String

        Dim _fontTable As New StringBuilder()

        ' Append table control string
        _fontTable.Append("{\fonttbl{\f0")
        _fontTable.Append("\")

        ' If the font's family corresponds to an RTF family, append the
        ' RTF family name, else, append the RTF for unknown font family.
        If rtfFontFamily.Contains(_font.FontFamily.Name) Then
            _fontTable.Append(rtfFontFamily(_font.FontFamily.Name))
        Else
            _fontTable.Append(rtfFontFamily(FF_UNKNOWN))
        End If

        ' \fcharset specifies the character set of a font in the font table.
        ' 0 is for ANSI.
        _fontTable.Append("\fcharset0 ")

        ' Append the name of the font
        _fontTable.Append(_font.Name)

        ' Close control string
        _fontTable.Append(";}}")

        Return _fontTable.ToString()
    End Function

    ''' <summary>
    ''' Creates a font table from the RtfColor structure.  When an Insert or Append
    ''' operation is performed, _textColor and _backColor are either specified
    ''' or the default is used.  In any case, on any Insert or Append, only three
    ''' colors are used.  The default color of the RichTextBox (signified by a
    ''' semicolon (;) without a definition), is always the first color (index 0) in
    ''' the color table.  The second color is always the text color, and the third
    ''' is always the highlight color (color behind the text).  The color table
    ''' should have the form ...
    ''' 
    ''' {\colortbl ;[TEXT_COLOR];[HIGHLIGHT_COLOR];}
    ''' 
    ''' </summary>
    ''' <param name="_textColor"></param>
    ''' <param name="_backColor"></param>
    ''' <returns></returns>
    Private Function GetColorTable(ByVal _textColor As RtfColor, ByVal _backColor As RtfColor) As String

        Dim _colorTable As New StringBuilder()

        ' Append color table control string and default font (;)
        _colorTable.Append("{\colortbl ;")

        ' Append the text color
        _colorTable.Append(_rtfColor(_textColor))
        _colorTable.Append(";")

        ' Append the highlight color
        _colorTable.Append(_rtfColor(_backColor))
        _colorTable.Append(";}\n")

        Return _colorTable.ToString()
    End Function

    ''' <summary>
    ''' Called by overrided RichTextBox.Rtf accessor.
    ''' Removes the null character from the RTF.  This is residue from developing
    ''' the control for a specific instant messaging protocol and can be ommitted.
    ''' </summary>
    ''' <param name="_originalRtf"></param>
    ''' <returns>RTF without null character</returns>
    Private Function RemoveBadChars(ByVal _originalRtf As String) As String
        Return _originalRtf.Replace(vbNullChar, "")
    End Function

#End Region

End Class


