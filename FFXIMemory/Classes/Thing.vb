Imports System.Drawing
Imports System.Xml
Imports System.Globalization
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Reflection

Public MustInherit Class Thing
    Implements IItem

    ''' <summary>
    ''' Helper field; if set, it will be used by GetIcon to find and return an icon image.
    ''' </summary>
    Protected IconField_ As String

#Region "Implemented IItem Members"


    Public Overridable Function GetIcon() As Image Implements IItem.GetIcon
        If Me.IconField_ Is Nothing Then
            Return Nothing
        End If
        Dim IconValue As Object = Me.GetFieldValue(Me.IconField_)
        If IconValue Is Nothing OrElse TypeOf IconValue Is Image Then
            Return TryCast(IconValue, Image)
        ElseIf TypeOf IconValue Is IItem Then
            Return DirectCast(IconValue, IItem).GetIcon()
        End If
        Return Nothing
    End Function

    Public Overridable Function GetFields() As List(Of String) Implements IItem.GetFields
        Dim Fields As List(Of [String]) = New List(Of String)()
        For Each Field As String In Me.GetAllFields()
            If Me.HasField(Field) Then
                Fields.Add(Field)
            End If
        Next
        Return Fields
    End Function

    Public Overridable Sub Load(ByVal Element As XmlElement) Implements IItem.Load
        Me.Clear()
        If Element Is Nothing Then
            Throw New ArgumentNullException()
        End If
        If Element.Name <> "thing" OrElse Not Element.HasAttribute("type") Then
            Throw New ArgumentException("Invalid Load")
        End If
        If Element.GetAttribute("type") <> Me.[GetType]().Name Then
            Throw New ArgumentException("Wrong item loaded")
        End If
        For Each FieldName As String In Me.GetAllFields()
            Try
                Dim FieldElement As XmlElement = TryCast(Element.SelectSingleNode([String].Format("./child::field[@name = '{0}']", FieldName)), XmlElement)
                If FieldElement IsNot Nothing Then
                    Me.LoadField(FieldName, FieldElement)
                End If
            Catch
            End Try
        Next
    End Sub

    Public Overridable Function Save(ByVal Document As XmlDocument) As XmlElement Implements IItem.Save
        Dim E As XmlElement = Document.CreateElement("thing")
        If True Then
            Dim A As XmlAttribute = Document.CreateAttribute("type")
            A.InnerText = Me.[GetType]().Name
            E.Attributes.Append(A)
        End If
        For Each FieldName As String In Me.GetFields()
            Dim F As XmlElement = Document.CreateElement("field")
            If True Then
                Dim A As XmlAttribute = Document.CreateAttribute("name")
                A.InnerText = FieldName
                F.Attributes.Append(A)
            End If
            Me.SaveField(FieldName, Document, F)
            E.AppendChild(F)
        Next
        Return E
    End Function

#End Region

#Region "Helper Routines"

    Protected Function FormatTime(ByVal time As Double) As String
        Dim seconds As Double = time Mod 60
        Dim minutes As Long = CLng(Math.Truncate(time - seconds)) \ 60
        Dim hours As Long = minutes \ 60
        minutes = minutes Mod 60
        'Dim days As Long = hours \ 24
        'hours = hours Mod 24
        Dim Result As String = [String].Empty
        'If days > 0 Then
        '    Result += [String].Format("{0}d", days)
        'End If
        If hours > 0 Then
            Result += [String].Format("{0}:{1:00}:{2:00}", hours, minutes, seconds)
        End If
        If minutes > 0 Then
            Result += [String].Format("{0}:{1:00}", minutes, seconds)
        End If
        If seconds > 0 OrElse Result = [String].Empty Then
            Result += [String].Format("0:{0:00}", seconds)
        End If
        Return Result
    End Function

#End Region

#Region "Load()/Save() Subroutines"

    Protected MustOverride Sub LoadField(ByVal Field As String, ByVal Element As XmlElement)

    ' Generics don't allow specifying value types as constraints, nor does it allow casting to
    ' an unbounded type parameter.  As a result, a separate function is needed for each array type.

    Protected Function LoadByteArray(ByVal Node As XmlElement) As Byte()
        Dim ArraySize As Integer = 0
        Try
            ArraySize = Integer.Parse(Node.GetAttribute("array-size"), NumberStyles.[Integer])
        Catch
            Return Nothing
        End Try
        If ArraySize < 0 Then
            Return Nothing
        End If
        Dim Result As Byte() = New Byte(ArraySize - 1) {}
        For i As Integer = 0 To ArraySize - 1
            Try
                Dim ElementNode As XmlNode = Node.SelectSingleNode([String].Format("./element[@index = '{0}']", i))
                If ElementNode IsNot Nothing AndAlso TypeOf ElementNode Is System.Xml.XmlElement Then
                    Result(i) = CByte(ULong.Parse(ElementNode.InnerText, NumberStyles.[Integer]))
                End If
            Catch
                Return Nothing
            End Try
        Next
        Return Result
    End Function

    Protected Function LoadTextArray(ByVal Node As XmlElement) As String()
        Dim ArraySize As Integer = 0
        Try
            ArraySize = Integer.Parse(Node.GetAttribute("array-size"), NumberStyles.[Integer])
        Catch
            Return Nothing
        End Try
        If ArraySize < 0 Then
            Return Nothing
        End If
        Dim Result As String() = New String(ArraySize - 1) {}
        For i As Integer = 0 To ArraySize - 1
            Try
                Dim ElementNode As XmlNode = Node.SelectSingleNode([String].Format("./element[@index = '{0}']", i))
                If ElementNode IsNot Nothing AndAlso TypeOf ElementNode Is System.Xml.XmlElement Then
                    Result(i) = ElementNode.InnerText
                End If
            Catch
                Return Nothing
            End Try
        Next
        Return Result
    End Function

    Protected Function LoadHexField(ByVal Node As XmlElement) As System.Nullable(Of ULong)
        Try
            Return ULong.Parse(Node.InnerText, NumberStyles.HexNumber)
        Catch
            Return Nothing
        End Try
    End Function

    Protected Function LoadImageField(ByVal Node As XmlElement) As Image
        If Not Node.HasAttribute("format") OrElse Node.GetAttribute("format") <> "image/png" Then
            Return Nothing
        End If
        If Not Node.HasAttribute("encoding") OrElse Node.GetAttribute("encoding") <> "base64" Then
            Return Nothing
        End If
        Dim ImageData As Byte() = Convert.FromBase64String(Node.InnerText)
        Dim MS As New MemoryStream(ImageData, False)
        Dim Result As Image = New Bitmap(MS)
        MS.Close()
        MS.Dispose()
        Return Result
    End Function

    Protected Function LoadSignedIntegerField(ByVal Node As XmlElement) As System.Nullable(Of Long)
        Try
            Return Long.Parse(Node.InnerText, NumberStyles.[Integer])
        Catch
            Return Nothing
        End Try
    End Function

    Protected Sub LoadThingField(ByVal Node As XmlElement, ByVal T As IItem)
        Dim ThingRoot As XmlElement = TryCast(Node.SelectSingleNode([String].Format("./child::thing[@type = '{0}']", T.[GetType]().Name)), XmlElement)
        If ThingRoot IsNot Nothing Then
            T.Load(ThingRoot)
        Else
            Throw New ArgumentException("Invalid Field Type")
        End If
    End Sub

    Protected Function LoadTextField(ByVal Node As XmlElement) As String
        Return Node.InnerText
    End Function

    Protected Function LoadUnsignedIntegerField(ByVal Node As XmlElement) As System.Nullable(Of ULong)
        Try
            Return ULong.Parse(Node.InnerText, NumberStyles.[Integer])
        Catch
            Return Nothing
        End Try
    End Function

    Protected Overridable Sub SaveField(ByVal Value As Object, ByVal Document As XmlDocument, ByVal Element As XmlElement)
        ' Default Implementation:
        ' - Array           -> recurse
        ' - IItem          -> Save()
        ' - Image           -> save as PNG/base64
        ' - Enum            -> save as hex number
        ' - Everything Else -> Value.ToString()
        If Value IsNot Nothing Then
            If TypeOf Value Is IItem Then
                Element.AppendChild(DirectCast(Value, IItem).Save(Document))
            ElseIf TypeOf Value Is Array Then
                Dim Values As Array = TryCast(Value, Array)
                If True Then
                    Dim A As XmlAttribute = Document.CreateAttribute("array-size")
                    A.InnerText = Values.Length.ToString()
                    Element.Attributes.Append(A)
                End If
                For i As Integer = 0 To Values.Length - 1
                    Dim E As XmlElement = Document.CreateElement("element")
                    If True Then
                        Dim A As XmlAttribute = Document.CreateAttribute("index")
                        A.InnerText = i.ToString()
                        E.Attributes.Append(A)
                    End If
                    Me.SaveField(Values.GetValue(i), Document, E)
                    Element.AppendChild(E)
                Next
            ElseIf TypeOf Value Is Image Then
                If True Then
                    Dim A As XmlAttribute = Document.CreateAttribute("format")
                    A.InnerText = "image/png"
                    Element.Attributes.Append(A)
                End If
                If True Then
                    Dim A As XmlAttribute = Document.CreateAttribute("encoding")
                    A.InnerText = "base64"
                    Element.Attributes.Append(A)
                End If
                Dim MS As New MemoryStream()
                DirectCast(Value, Image).Save(MS, ImageFormat.Png)
                Element.InnerText = Convert.ToBase64String(MS.GetBuffer())
                MS.Close()
                MS.Dispose()
            ElseIf TypeOf Value Is [Enum] Then
                ' Store enums as hex numbers
                Element.InnerText = DirectCast(Value, [Enum]).ToString("X")
            Else
                Element.InnerText = Value.ToString()
            End If
        End If
    End Sub

    Protected Overridable Sub SaveField(ByVal Field As String, ByVal Document As XmlDocument, ByVal Element As XmlElement)
        Me.SaveField(Me.GetFieldValue(Field), Document, Element)
    End Sub

#End Region

#Region "Abstract IItem Members"

    Public MustOverride Sub Clear() Implements IItem.Clear
    Public MustOverride Function HasField(ByVal Field As String) As Boolean Implements IItem.HasField
    Public MustOverride Function GetAllFields() As List(Of String) Implements IItem.GetAllFields
    Public MustOverride Function GetFieldText(ByVal Field As String) As String Implements IItem.GetFieldText
    Public MustOverride Function GetFieldValue(ByVal Field As String) As Object Implements IItem.GetFieldValue

#End Region

#Region "Thing Instantiation"

    Public Shared Function Create(ByVal TypeName As String) As IItem
        Try
            Dim FullName As String = String.Format("{0}.{1}", GetType(Thing).[Namespace], TypeName)
            Return TryCast(Assembly.GetExecutingAssembly().CreateInstance(FullName, False), IItem)
        Catch
        End Try
        Return Nothing
    End Function

    Public Function GetFieldName(ByVal Field As String) As String Implements IItem.GetFieldName
        Return ""
    End Function

    Public ReadOnly Property TypeName As String Implements IItem.TypeName
        Get
            Return ""
        End Get
    End Property
#End Region

End Class
