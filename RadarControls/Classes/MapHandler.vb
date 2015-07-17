Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class MapHandler
#Region " PROPERTIES "
    Private _mapList As List(Of MapData)
    Public ReadOnly Property MapList() As List(Of MapData)
        Get
            If _mapList Is Nothing Then
                _mapList = New List(Of MapData)
            End If
            Return _mapList
        End Get
    End Property

    Private _iniFound As Boolean = False
    Public ReadOnly Property IniFound() As Boolean
        Get
            Return _iniFound
        End Get
    End Property

    Private _mapsPath As String
    Public Property MapsPath() As String
        Get
            Return _mapsPath
        End Get
        Set(ByVal value As String)
            _mapsPath = value
        End Set
    End Property
#End Region

#Region " CONSTRUCTORS "
    Public Sub New()
        ReadIni()
    End Sub
#End Region

#Region " PUBLIC METHODS "
    Public Sub ReloadIniData()
        MapList.Clear()
        ReadIni()
    End Sub
#End Region

#Region " PRIVATE METHODS "
    Public Function ReadIni() As Boolean
        Dim ini As String = FindIniFile()
        If IO.File.Exists(ini) Then
            _iniFound = True
            Using fReader As New IO.StreamReader(ini)
                Dim parts As String()
                Dim subParts As String()
                Dim line As String = ""
                Dim subLine As String = ""
                Dim commentIndex As Integer = -1
                Dim lineNumber As Integer = 0
                Dim mData As MapData
                'Dim pattern As String = "^[a-f0-9]"
                While fReader.Peek <> -1
                    line = fReader.ReadLine
                    lineNumber += 1
                    Try
                        'Make sure we are on an actual entry and not a comment or blank line
                        If line.Length = 0 OrElse line.StartsWith(";") OrElse line.StartsWith("[") Then
                            Continue While
                        End If

                        'Split the line into the pairs
                        parts = line.Split("=")
                        If parts.Length <> 2 Then
                            Continue While
                        End If

                        'Grab tha map data parts (Map, Floor)
                        subParts = parts(0).Split("_")
                        If subParts.Length <> 2 Then
                            Continue While
                        End If

                        'Initialize the map data object
                        mData = New MapData() With {.Map = Convert.ToInt16(subParts(0), 16)}
                        If Not Integer.TryParse(subParts(1), NumberStyles.Any, CultureInfo.InvariantCulture, mData.Level) Then
                            Continue While
                        End If

                        'Now lets parse out all the ini data and the boxes
                        subLine = parts(1)
                        commentIndex = subLine.IndexOf(";")
                        If commentIndex > -1 Then
                            subLine = subLine.Substring(0, commentIndex)
                        End If
                        subLine = subLine.Trim

                        'split it up into the parts
                        subParts = subLine.Split(",")
                        If subParts.Length = 0 OrElse ((subParts.Length - 4) Mod 6 <> 0) Then
                            Continue While
                        End If

                        'Set the ini data
                        mData.IniData = New IniData(Double.Parse(subParts(0), CultureInfo.InvariantCulture),
                                                    Double.Parse(subParts(1), CultureInfo.InvariantCulture),
                                                    Double.Parse(subParts(2), CultureInfo.InvariantCulture),
                                                    Double.Parse(subParts(3), CultureInfo.InvariantCulture))

                        Dim i As Integer = 4
                        While i < subParts.Length
                            mData.Boxes.Add(New Box(Double.Parse(subParts(i), CultureInfo.InvariantCulture),
                                                    Double.Parse(subParts(i + 2), CultureInfo.InvariantCulture),
                                                    Double.Parse(subParts(i + 1), CultureInfo.InvariantCulture),
                                                    Double.Parse(subParts(i + 3), CultureInfo.InvariantCulture),
                                                    Double.Parse(subParts(i + 5), CultureInfo.InvariantCulture),
                                                    Double.Parse(subParts(i + 4), CultureInfo.InvariantCulture)))
                            i += 6
                        End While
                        If Not mData Is Nothing Then
                            MapList.Add(mData)
                        End If
                    Catch ex As Exception
                        'Right now we will do nothing, just continue and move on
                    End Try
                End While
            End Using

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' 6/16/2011 
            ' This is the old method, which I may reinstate as the new method is causing errors
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'For Each line As String In iniData
            '    lineNumber += 1
            '    If line.Length > 12 AndAlso Not line.StartsWith(";") Then
            '        mData = New MapData
            '        SplitChars = New Char() {"="c}
            '        Dim Items As String() = line.Split(SplitChars)
            '        Try
            '            If (Items(0).Length = 2) Then
            '                mData.Map = Byte.Parse(Items(0).ToUpper, Globalization.NumberStyles.HexNumber)
            '                mData.Level = 0
            '            Else
            '                mData.Map = Byte.Parse(Items(0).Substring(0, 2), Globalization.NumberStyles.HexNumber)
            '                mData.Level = Byte.Parse(Items(0).Substring(3))
            '            End If
            '            Items(1) = Items(1).Replace("--", "-").Replace(" ", "")
            '            Items = Strings.Split(Items(1), ",", -1, 0)
            '            mData.IniData.XScale = Val(Items(0))
            '            mData.IniData.XModifier = Val(Items(1))
            '            mData.IniData.YScale = Val(Items(2))
            '            mData.IniData.YModifier = Val(Items(3))
            '            Dim BoxCount As Integer = (Items.Length - 4)
            '            Dim MinCount As Integer = 4
            '            Do While (MinCount <= BoxCount)
            '                Dim box1 As New Box(CSng(Items(MinCount)), CSng(Items((MinCount + 2))), CSng(Items((MinCount + 1))), CSng(Items((MinCount + 3))), CSng(Items((MinCount + 5))), CSng(Items((MinCount + 4))))
            '                mData.Boxes.Add(box1)
            '                MinCount = (MinCount + 6)
            '            Loop
            '            MapList.Add(mData)
            '        Catch ex As Exception
            '            'Debug.WriteLine(String.Format("Error reading line {0}: {1} - {2}", lineNumber, line, ex.Message))
            '            ''Debug.Print(Ex.Message)
            '            'Do nothing, just continue
            '        End Try
            '    End If
            'Next
        Else
            _iniFound = False
        End If
    End Function

    Public Sub WriteEntry(ByVal Entry As MapData)
        'Get the inifile location
        Dim ini As String = FindIniFile()
        'Verify that the file exists
        If ini <> String.Empty AndAlso IO.File.Exists(ini) Then
            'Read all the lines into an array
            Dim lines As String() = File.ReadAllLines(ini)
            'Build the entry name
            Dim mapName As String = String.Format("{0}_{1}", Entry.Map.ToString("x2"), Entry.Level)
            'Loop through all the lines
            For i = 0 To lines.Count - 1
                'Check for the entry
                If lines(i).StartsWith(mapName) Then
                    'Split the line on the commas
                    Dim split As String() = lines(i).Split(","c)
                    'Verify that this is a valid line
                    If split.Count > 4 Then
                        'Build the new entry
                        Dim newLine As New System.Text.StringBuilder
                        'Add the map name and x scale
                        newLine.Append(split(0))
                        'Add the new x modifier
                        newLine.Append(String.Format(",{0}", Entry.IniData.XModifier))
                        'Add the y scale
                        newLine.Append(String.Format(",{0}", split(2)))
                        'Add the new y modifier
                        newLine.Append(String.Format(",{0}", Entry.IniData.YModifier))
                        'Add the rest of the entries
                        For x = 4 To split.Count - 1
                            newLine.Append(String.Format(",{0}", split(x)))
                        Next
                        'Overwrite the line in the array
                        lines(i) = newLine.ToString
                    End If
                    'Write the file
                    IO.File.WriteAllLines(ini, lines)
                    'Exit the loop as we are now all done
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Function FindIniFile() As String
        Dim iniPath As String = String.Format("{0}\map.ini", My.Settings.MapsLocation)
        If iniPath = String.Empty OrElse Not File.Exists(iniPath) Then
            Dim iniFiles As String() = Directory.GetFiles(My.Application.Info.DirectoryPath, "map.ini", SearchOption.AllDirectories)
            If iniFiles.Count > 0 Then
                iniPath = iniFiles(0)
            End If
        End If
        If iniPath <> String.Empty Then

            Me.MapsPath = Path.GetDirectoryName(iniPath)
            My.Settings.MapsLocation = Me.MapsPath
            My.Settings.Save()
        End If
        Return iniPath
    End Function
#End Region
End Class
