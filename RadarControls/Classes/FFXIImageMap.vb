Imports System.Drawing
Imports System.Drawing.Imaging

Public Class FFXIImageMap
    Private ReadOnly m_mapid As Integer
    Private m_xScale As Single
    Private m_xOffset As Single
    Private m_yScale As Single
    Private m_yOffset As Single
    Private ReadOnly m_mapPath As String
    Private mapImage As Image
    Private loadAttempted As Boolean
    Private m_bounds As RectangleF = RectangleF.Empty
    Private mapMaxX, mapMinX, mapMaxY, mapMinY As Single

    Public Sub New(ByVal mapid As Integer, ByVal mapPath As String, ByVal xScale As Single, ByVal xOffset As Single, ByVal yScale As Single, ByVal yOffset As Single)
        m_mapid = mapid
        m_xScale = xScale
        m_xOffset = xOffset
        m_yScale = yScale
        m_yOffset = yOffset
        m_mapPath = mapPath
    End Sub

    ''' <summary>Gets the X zone scale.</summary>
    Public ReadOnly Property XScale() As Single
        Get
            Return m_xScale
        End Get
    End Property
    ''' <summary>Gets the X offset to place map from zone center.</summary>
    Public ReadOnly Property XOffset() As Single
        Get
            Return m_xOffset
        End Get
    End Property
    ''' <summary>Gets the Y zone scale.</summary>
    Public ReadOnly Property YScale() As Single
        Get
            Return m_yScale
        End Get
    End Property
    ''' <summary>Gets the Y offset to place map from zone center.</summary>
    Public ReadOnly Property YOffset() As Single
        Get
            Return m_yOffset
        End Get
    End Property

    Public ReadOnly Property MapPath As String
        Get
            Return m_mapPath
        End Get
    End Property

    Public Sub SetMapLocation(ByVal X As Single, ByVal Y As Single)
        m_xOffset = X
        m_yOffset = Y
        m_bounds = RectangleF.Empty
        'force recalculation
    End Sub

    Public Sub SetMapLocation(ByVal scale As Single, ByVal X As Single, ByVal Y As Single)
        SetMapLocation(scale, -scale, X, Y)
    End Sub

    Public Sub SetMapLocation(ByVal Xscale As Single, ByVal Yscale As Single, ByVal X As Single, ByVal Y As Single)
        m_xScale = Xscale
        m_yScale = Yscale
        SetMapLocation(X, Y)
    End Sub

    ''' <summary>Gets the map id of this map image.</summary>
    Public ReadOnly Property MapID() As Integer
        Get
            Return m_mapid
        End Get
    End Property

    ''' <summary>Retrieve the image boundaries in MAP coordinates.</summary>
    Public ReadOnly Property Bounds() As RectangleF
        Get
            'The bounds will never change, so only calculate it once (upon demand) and then cache it
            If m_bounds = RectangleF.Empty Then
                'map is scaled by a factor of 2, so reduce the value by half
                m_bounds = New RectangleF((-m_xOffset / m_xScale), (-m_yOffset / m_yScale), (512 / m_xScale) * 0.5F, (512 / m_yScale) * 0.5F)
            End If
            Return m_bounds
        End Get
    End Property

    Public ReadOnly Property CheckedBounds As RectangleF
        Get
            Return CheckBounds(Me.Bounds)
        End Get
    End Property

    ''' <summary>Checks the given box againt the current map boundaries and expands it as necessary.</summary>
    Public Function CheckBounds(ByVal Bounds As RectangleF) As RectangleF
        CheckBatch(Bounds.X, Bounds.Y)
        CheckBatch(Bounds.Right, Bounds.Bottom)
        Return CheckBatchEnd()
    End Function
    ''' <summary>Checks the given point against the current map boundaries and expands it as necessary as part of a batch. Use CheckBatchend when finished.</summary>
    Public Sub CheckBatch(ByVal X As Single, ByVal Y As Single)
        If X > mapMaxX Then
            mapMaxX = X
        End If
        If Y > mapMaxY Then
            mapMaxY = Y
        End If
        If X < mapMinX Then
            mapMinX = X
        End If
        If Y < mapMinY Then
            mapMinY = Y
        End If
    End Sub
    ''' <summary>Recalculates the current map boundaries after checking points in batch.</summary>
    Public Function CheckBatchEnd() As RectangleF
        'prevent bad data from hosing the app...
        If mapMaxX > 32000 Then
            mapMaxX = 32000
        End If
        If mapMaxY > 32000 Then
            mapMaxY = 32000
        End If
        If mapMinX < -32000 Then
            mapMinX = -32000
        End If
        If mapMinY < -32000 Then
            mapMinY = -32000
        End If

        Return RectangleF.FromLTRB(mapMinX, mapMinY, mapMaxX, mapMaxY)
    End Function


    Friend ReadOnly Property Image As Image
        Get
            If mapImage Is Nothing AndAlso Not loadAttempted Then
                Try
                    Dim tempImage As Bitmap = Nothing
                    If IO.File.Exists(Me.MapPath) Then
                        tempImage = New Bitmap(Me.MapPath)
                    End If

                    If tempImage IsNot Nothing Then
                        If tempImage.PixelFormat <> Imaging.PixelFormat.Format32bppArgb Then
                            Dim newImage As New Bitmap(tempImage.Width, tempImage.Height, PixelFormat.Format32bppArgb)
                            newImage.SetResolution(tempImage.HorizontalResolution, tempImage.VerticalResolution)
                            Dim g = Graphics.FromImage(newImage)
                            g.DrawImage(tempImage, 0, 0)
                            g.Dispose()
                            mapImage = newImage
                        Else
                            mapImage = tempImage
                        End If
                    Else
                        mapImage = My.Resources.DefaultMap
                    End If
                Catch ex As Exception

                End Try
                loadAttempted = True
            End If
            Return mapImage

        End Get
    End Property

    ''' <summary>Releases cached image data for the map.</summary>
    Public Sub ClearCache()
        mapImage = Nothing
        loadAttempted = False
    End Sub

    Public Overrides Function ToString() As String
        Return m_mapid.ToString()
    End Function
End Class
