Imports System.Xml
Imports System.Drawing

Public Interface IItem
    ''' <summary>Gets a name for this type of IThing.</summary>
    ReadOnly Property TypeName() As String

    ''' <summary>
    ''' Returns the full list of fields that can be supported by this IThing, with the guarantee that
    ''' no instance of this class will ever contain a field that is not in this list.
    ''' </summary>
    ''' <returns>A list of field names.</returns>
    Function GetAllFields() As List(Of String)

    ''' <summary>
    ''' Returns the list of fields supported by this IThing instance.
    ''' </summary>
    ''' <returns>A list of field names.</returns>
    Function GetFields() As List(Of String)

    ''' <summary>
    ''' Checks whether or not a field with the given name is available.
    ''' </summary>
    ''' <param name="Field">The name of the field to check.</param>
    ''' <returns>true if the field is available, false otherwise.</returns>
    Function HasField(ByVal Field As String) As Boolean

    ''' <summary>
    ''' Returns a descriptive name for the given field (suitable for use as label, for example).
    ''' </summary>
    ''' <param name="Field">The (internal) name of the field.</param>
    ''' <returns>The field's descriptive name.</returns>
    Function GetFieldName(ByVal Field As String) As String

    ''' <summary>
    ''' Returns the value of the given field in text form.
    ''' </summary>
    ''' <param name="Field">The name of the field.</param>
    ''' <returns>The field's value in text form.</returns>
    Function GetFieldText(ByVal Field As String) As String

    ''' <summary>
    ''' Returns the value of the given field.
    ''' </summary>
    ''' <param name="Field">The name of the field.</param>
    ''' <returns>The field's value.</returns>
    Function GetFieldValue(ByVal Field As String) As Object

    ''' <summary>
    ''' Clears all fields of this IThing.
    ''' </summary>
    Sub Clear()

    ''' <summary>
    ''' Returns an image that can serve as an icon for this IThing.
    ''' </summary>
    ''' <returns>The icon image, or null if no such image is available.</returns>
    Function GetIcon() As Image

    ''' <summary>
    ''' Fills this IThing based on the given XML representation.
    ''' </summary>
    ''' <param name="Node">The XML representation to load.</param>
    Sub Load(ByVal Node As XmlElement)

    ''' <summary>
    ''' Saves this IThing in XML form (suitable for later Load()).
    ''' </summary>
    ''' <param name="Document">The XML document to use as context for the created XML representation.</param>
    ''' <returns>The created XML representation of this IThing.</returns>
    Function Save(ByVal Document As XmlDocument) As XmlElement

End Interface
