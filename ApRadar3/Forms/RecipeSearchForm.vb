Imports DataLibrary
Imports FFXIMemory
Imports DataLibrary.ApRadarDataset

Public Class RecipeSearchForm
#Region " MEMBER VARIABLES "
    Private _dock As DockingClass
    Private _curIndex As Integer = 0
    Private _count As Integer = 0
    Private _animator As FormAnimator
    Private _craftSkills As CraftingSkills
    Private _direction As SlideDirection
#End Region

#Region " ENUM "
    Public Enum SearchType
        Result
        UsedIn
        Desynth
    End Enum
#End Region

#Region " PROPERTIES "
    Private _itemDT As DataTable
    Private ReadOnly Property ItemsDataTable() As DataTable
        Get
            If _itemDT Is Nothing Then
                _itemDT = New DataTable("Items")
                _itemDT.Columns.Add("ID", GetType(Integer))
                _itemDT.Columns.Add("Name", GetType(String))
            End If
            Return _itemDT
        End Get
    End Property

    Private _resultsDT As DataTable
    Private ReadOnly Property ResultsDataTable() As DataTable
        Get
            If _resultsDT Is Nothing Then
                _resultsDT = New DataTable("Results")
                _resultsDT.Columns.Add("ID", GetType(Integer))
                _resultsDT.Columns.Add("Name", GetType(String))
            End If
            Return _resultsDT
        End Get
    End Property

    Private _recipes As IEnumerable(Of SynthsRow)
    Private Property Recipes() As IEnumerable(Of SynthsRow)
        Get
            Return _recipes
        End Get
        Set(ByVal value As IEnumerable(Of SynthsRow))
            _recipes = value
        End Set
    End Property

    Private ReadOnly Property CraftSkills() As CraftingSkills
        Get
            If _craftSkills Is Nothing Then
                _craftSkills = New CraftingSkills(MemoryScanner.Scanner.FFXI)
            End If
            Return _craftSkills
        End Get
    End Property
#End Region

#Region " FORM ACTIONS "
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Private Sub RecipeSearchForm_ForeColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ForeColorChanged
        UpdateControlColors(Me)
    End Sub

    Private Sub RecipeSearchForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Opacity = 1
        _animator.FadeOut(500)
    End Sub

    Private Sub RecipeSearchForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ThemeHandler.ApplyTheme(Me)
        _dock = New DockingClass(Me)
        _dock.UseDocking = True
        _animator = New FormAnimator(Me)
        If ThemeHandler.ActiveTheme.DockPosition = DockMode.Top Then
            _animator.SlideOut(150, SlideDirection.Down)
        Else
            _animator.SlideOut(150, SlideDirection.Up)
        End If
    End Sub
#End Region

#Region " CONTROLS "
    Private Sub HeaderPanel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseDown, lblHeder.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.StartDockDrag(e.X, e.Y)
        End If
    End Sub

    Private Sub HeaderPanel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderPanel.MouseMove, lblHeder.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            _dock.UpdateDockDrag(New UpdateDockDragArgs(e.X, e.Y))
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub lstItems_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstItems.GotFocus
        Me.lstResults.SelectedIndex = -1
    End Sub

    Private Sub lstItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstItems.SelectedIndexChanged
        GetItemInfo()
    End Sub

    Private Sub lstResults_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstResults.GotFocus
        Me.lstItems.SelectedIndex = -1
    End Sub

    Private Sub lstResults_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstResults.SelectedIndexChanged
        If Me.lstResults.SelectedIndex > -1 Then
            Dim itemID As Integer = Me.lstResults.SelectedValue
            Dim item = (From items In DataAccess.MobData.Items Where _
                        items.ItemID = itemID).FirstOrDefault
            If Not item Is Nothing Then
                BuildItemDisplay(item)
            Else
                ClearItemInfo()
            End If
        End If
    End Sub

    Private Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        If _curIndex < _count - 1 Then
            _curIndex += 1
            Dim sr = _recipes(_curIndex)
            LoadGenInfo(sr)
            LoadResults(sr)
            LoadIngredients(sr)
            GetItemInfo()
            If _curIndex = _count - 1 Then
                Me.cmdNext.Enabled = False
            End If
            If _curIndex > 0 Then
                Me.cmdPrevious.Enabled = True
            End If
            Me.lblRecipeCount.Text = String.Format("{0} of {1}", _curIndex + 1, _count)
        End If
    End Sub

    Private Sub cmdPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrevious.Click
        If _curIndex > 0 Then
            _curIndex -= 1
            Dim sr = _recipes(_curIndex)
            LoadGenInfo(sr)
            LoadResults(sr)
            LoadIngredients(sr)
            GetItemInfo()
            If _curIndex = 0 Then
                Me.cmdPrevious.Enabled = False
            End If
            If _curIndex < _count - 1 Then
                Me.cmdNext.Enabled = True
            End If
            Me.lblRecipeCount.Text = String.Format("{0} of {1}", _curIndex + 1, _count)
        End If
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        ReloadSearchResults()
    End Sub

    Private Sub txtItem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtItem_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyUp
        If e.KeyCode = Keys.Enter Then
            Me.cmdSearch.PerformClick()
        End If
    End Sub

    Private Sub txtItem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItem.TextChanged
        Me.txtItem.ForeColor = Color.FromArgb(39, 40, 44)
    End Sub

    Private Sub rbRecipes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbRecipes.CheckedChanged, rbUsedIn.CheckedChanged, rbDesynth.CheckedChanged
        ReloadSearchResults()
    End Sub
#End Region

#Region " PUBLIC METHODS "
    Public Sub SearchItem(ByVal Item As String, ByVal Type As SearchType)
        'Reset the current item
        _curIndex = 0
        Me.cmdPrevious.Enabled = False
        'Get the item id
        Dim polItemID As Integer = (From c In DataAccess.MobData.Items Where _
                                 c.ItemName.ToLower = Item.ToLower _
                                 Select c.ItemID).FirstOrDefault
        'If we found the imem id, lets start searching for recipes
        If polItemID > 0 Then

            If Type = SearchType.Result Then
                _recipes = From c In DataAccess.MobData.Synths Where _
                           (c.HQ0 = polItemID Or c.HQ1 = polItemID Or c.HQ2 = polItemID Or c.HQ3 = polItemID) _
                           And c.Crystal <> 4100 Order By c.Crystal
                _count = _recipes.Count
                Dim uiCount As Integer = (From c In DataAccess.MobData.Synths Where _
                                       (Not c.IsIngredient1Null AndAlso c.Ingredient1 = polItemID) Or (Not c.IsIngredient2Null AndAlso c.Ingredient2 = polItemID) Or _
                                       (Not c.IsIngredient3Null AndAlso c.Ingredient3 = polItemID) Or (Not c.IsIngredient4Null AndAlso c.Ingredient4 = polItemID) Or _
                                       (Not c.IsIngredient5Null AndAlso c.Ingredient5 = polItemID) Or (Not c.IsIngredient6Null AndAlso c.Ingredient6 = polItemID) Or _
                                       (Not c.IsIngredient7Null AndAlso c.Ingredient7 = polItemID) Or (Not c.IsIngredient8Null AndAlso c.Ingredient8 = polItemID)).Count
                Dim dCount As Integer = (From c In DataAccess.MobData.Synths Where _
                                        (c.HQ0 = polItemID Or c.HQ1 = polItemID Or c.HQ2 = polItemID Or c.HQ3 = polItemID) _
                                         Where c.Crystal = 4100).Count

                Me.rbRecipes.Text = String.Format("Recipes ({0})", _count)
                Me.rbUsedIn.Text = String.Format("Used in recipes ({0})", uiCount)
                Me.rbDesynth.Text = String.Format("Desynthesis ({0})", dCount)
            ElseIf Type = SearchType.UsedIn Then
                Dim rCount As Integer = (From c In DataAccess.MobData.Synths Where _
                                        (c.HQ0 = polItemID Or c.HQ1 = polItemID Or c.HQ2 = polItemID Or c.HQ3 = polItemID) _
                                         And c.Crystal <> 4100).Count
                Dim dCount As Integer = (From c In DataAccess.MobData.Synths Where _
                                        (c.HQ0 = polItemID Or c.HQ1 = polItemID Or c.HQ2 = polItemID Or c.HQ3 = polItemID) _
                                         And c.Crystal = 4100).Count
                _recipes = From c In DataAccess.MobData.Synths Where _
                           (Not c.IsIngredient1Null AndAlso c.Ingredient1 = polItemID) Or (Not c.IsIngredient2Null AndAlso c.Ingredient2 = polItemID) Or _
                                       (Not c.IsIngredient3Null AndAlso c.Ingredient3 = polItemID) Or (Not c.IsIngredient4Null AndAlso c.Ingredient4 = polItemID) Or _
                                       (Not c.IsIngredient5Null AndAlso c.Ingredient5 = polItemID) Or (Not c.IsIngredient6Null AndAlso c.Ingredient6 = polItemID) Or _
                                       (Not c.IsIngredient7Null AndAlso c.Ingredient7 = polItemID) Or (Not c.IsIngredient8Null AndAlso c.Ingredient8 = polItemID) _
                           Order By c.Crystal
                _count = _recipes.Count
                Me.rbRecipes.Text = String.Format("Recipes ({0})", rCount)
                Me.rbUsedIn.Text = String.Format("Used in recipes ({0})", _count)
                Me.rbDesynth.Text = String.Format("Desynthesis ({0})", dCount)
            Else
                _recipes = From c In DataAccess.MobData.Synths Where _
                           (c.HQ0 = polItemID Or c.HQ1 = polItemID Or c.HQ2 = polItemID Or c.HQ3 = polItemID) _
                           And c.Crystal = 4100
                _count = _recipes.Count

                Dim rCount As Integer = (From c In DataAccess.MobData.Synths Where _
                                        (c.HQ0 = polItemID Or c.HQ1 = polItemID Or c.HQ2 = polItemID Or c.HQ3 = polItemID) _
                                         And c.Crystal <> 4100).Count

                Dim uiCount As Integer = (From c In DataAccess.MobData.Synths Where _
                                       (Not c.IsIngredient1Null AndAlso c.Ingredient1 = polItemID) Or (Not c.IsIngredient2Null AndAlso c.Ingredient2 = polItemID) Or _
                                       (Not c.IsIngredient3Null AndAlso c.Ingredient3 = polItemID) Or (Not c.IsIngredient4Null AndAlso c.Ingredient4 = polItemID) Or _
                                       (Not c.IsIngredient5Null AndAlso c.Ingredient5 = polItemID) Or (Not c.IsIngredient6Null AndAlso c.Ingredient6 = polItemID) Or _
                                       (Not c.IsIngredient7Null AndAlso c.Ingredient7 = polItemID) Or (Not c.IsIngredient8Null AndAlso c.Ingredient8 = polItemID)).Count
                Me.rbRecipes.Text = String.Format("Recipes ({0})", rCount)
                Me.rbUsedIn.Text = String.Format("Used in recipes ({0})", uiCount)
                Me.rbDesynth.Text = String.Format("Desynthesis ({0})", _count)
            End If

            If _count > 0 Then
                If _count > 1 Then
                    Me.cmdNext.Enabled = True
                Else
                    Me.cmdNext.Enabled = False
                End If
                Me.lblRecipeCount.Text = String.Format("1 of {0}", _recipes.Count)
                Me.txtItem.Text = (From c In DataAccess.MobData.Items Where _
                                   c.ItemID = polItemID Select c.ItemName).FirstOrDefault
                Dim sr As SynthsRow = _recipes(0)
                LoadGenInfo(sr)
                LoadResults(sr)
                LoadIngredients(sr)
            Else
                ItemsDataTable.Clear()
                ResultsDataTable.Clear()
                ClearItemInfo()
                Me.lbCrystal.Text = "Item not found..."
                Me.lbCrystal.ForeColor = Color.White
                For Each ctl As Control In pnlSkills.Controls
                    If TypeOf ctl Is Label Then
                        ctl.Text = "0"
                    End If
                Next
                Me.cmdNext.Enabled = False
                Me.cmdPrevious.Enabled = False
                Me.lblRecipeCount.Text = "0 of 0"
                Me.txtItem.Text = Item
            End If
        End If
    End Sub

#End Region

#Region " PRIVATE MEMBERS "
    Private Sub LoadGenInfo(ByVal sr As SynthsRow)
        If Not sr Is Nothing Then
            Me.lbCrystal.Text = String.Format("{0} Crystal", [Enum].GetName(GetType(Crystal), sr.Crystal))
            Select Case CType(sr.Crystal, Crystal)
                Case Crystal.Fire
                    Me.lbCrystal.ForeColor = Color.Red
                Case Crystal.Ice
                    Me.lbCrystal.ForeColor = Color.LightBlue
                Case Crystal.Wind
                    Me.lbCrystal.ForeColor = Color.Green
                Case Crystal.Earth
                    Me.lbCrystal.ForeColor = Color.DarkGoldenrod
                Case Crystal.Lightning
                    Me.lbCrystal.ForeColor = Color.Purple
                Case Crystal.Water
                    Me.lbCrystal.ForeColor = Color.Blue
                Case Crystal.Light
                    Me.lbCrystal.ForeColor = Color.White
                Case Crystal.Dark
                    Me.lbCrystal.ForeColor = Color.Brown
            End Select
            Try
                Me.lblAlchemy.Text = String.Format("[{0}] {1} {2}", CraftSkills.Alchemy, sr.Alchemy, sr.Alchemy - CraftSkills.Alchemy)
                Me.lblBone.Text = String.Format("[{0}] {1} {2}", CraftSkills.Bonecraft, sr.Bonecraft, sr.Bonecraft - CraftSkills.Bonecraft)
                Me.lblCloth.Text = String.Format("[{0}] {1} {2}", CraftSkills.Clothcraft, sr.Clothcraft, sr.Clothcraft - CraftSkills.Clothcraft)
                Me.lblCooking.Text = String.Format("[{0}] {1} {2}", CraftSkills.Cooking, sr.Cooking, sr.Cooking - CraftSkills.Cooking)
                Me.lblGold.Text = String.Format("[{0}] {1} {2}", CraftSkills.Goldsmithing, sr.Goldsmithing, sr.Goldsmithing - CraftSkills.Goldsmithing)
                Me.lblLeather.Text = String.Format("[{0}] {1} {2}", CraftSkills.Leathercraft, sr.Leathercraft, sr.Leathercraft - CraftSkills.Leathercraft)
                Me.lblSmithing.Text = String.Format("[{0}] {1} {2}", CraftSkills.Smithing, sr.Smithing, sr.Smithing - CraftSkills.Smithing)
                Me.lblWood.Text = String.Format("[{0}] {1} {2}", CraftSkills.Woodworking, sr.Woodworking, sr.Woodworking - CraftSkills.Woodworking)
            Catch ex As Exception
                'Log the exception to output and continue
                'Debug.Print(Ex.Message)
            End Try
        End If
    End Sub

    Private Sub LoadIngredients(ByVal sr As SynthsRow)
        Dim ingredients As New Hashtable
        ingredients.Add(sr.Ingredient1, 1)
        If Not sr.IsIngredient2Null Then
            If ingredients.Contains(sr.Ingredient2) Then
                ingredients(sr.Ingredient2) += 1
            Else
                ingredients.Add(sr.Ingredient2, 1)
            End If
        End If

        If Not sr.IsIngredient3Null Then
            If ingredients.Contains(sr.Ingredient3) Then
                ingredients(sr.Ingredient3) += 1
            Else
                ingredients.Add(sr.Ingredient3, 1)
            End If
        End If

        If Not sr.IsIngredient4Null Then
            If ingredients.Contains(sr.Ingredient4) Then
                ingredients(sr.Ingredient4) += 1
            Else
                ingredients.Add(sr.Ingredient4, 1)
            End If
        End If

        If Not sr.IsIngredient5Null Then
            If ingredients.Contains(sr.Ingredient5) Then
                ingredients(sr.Ingredient5) += 1
            Else
                ingredients.Add(sr.Ingredient5, 1)
            End If
        End If

        If Not sr.IsIngredient6Null Then
            If ingredients.Contains(sr.Ingredient6) Then
                ingredients(sr.Ingredient6) += 1
            Else
                ingredients.Add(sr.Ingredient6, 1)
            End If
        End If

        If Not sr.IsIngredient7Null Then
            If ingredients.Contains(sr.Ingredient7) Then
                ingredients(sr.Ingredient7) += 1
            Else
                ingredients.Add(sr.Ingredient7, 1)
            End If
        End If

        If Not sr.IsIngredient8Null Then
            If ingredients.Contains(sr.Ingredient8) Then
                ingredients(sr.Ingredient8) += 1
            Else
                ingredients.Add(sr.Ingredient8, 1)
            End If
        End If

        ItemsDataTable.Rows.Clear()
        For Each entry In ingredients
            If entry.Key > 0 Then
                Dim itemID As Integer = entry.Key
                Dim Name As String = (From c In DataAccess.MobData.Items Where _
                                      c.ItemID = itemID Select c.ItemName).FirstOrDefault
                ItemsDataTable.Rows.Add(itemID, String.Format("{0} x{1}", Name, entry.Value))
            End If
        Next
        Me.lstItems.DisplayMember = "Name"
        Me.lstItems.ValueMember = "ID"
        Me.lstItems.DataSource = ItemsDataTable
    End Sub

    Private Sub LoadResults(ByVal sr As SynthsRow)
        ResultsDataTable.Rows.Clear()
        Dim id As Integer
        Dim quantity As Integer
        Dim name As String
        If Not sr.IsHQ0Null Then
            id = sr.HQ0
            name = (From c In DataAccess.MobData.Items Where c.ItemID = id _
                    Select c.ItemName).FirstOrDefault
            If Not sr.IsHQ0QuantityNull Then
                quantity = sr.HQ0Quantity
            Else
                quantity = 1
            End If
            If quantity = 1 Then
                ResultsDataTable.Rows.Add(id, String.Format("NQ: {0}", name))
            Else
                ResultsDataTable.Rows.Add(id, String.Format("NQ: {0} x{1}", name, quantity))
            End If
        End If

        If Not sr.IsHQ1Null Then
            id = sr.HQ1
            name = (From c In DataAccess.MobData.Items Where c.ItemID = id _
                    Select c.ItemName).FirstOrDefault
            If Not sr.IsHQ1QuantityNull Then
                quantity = sr.HQ1Quantity
            Else
                quantity = 1
            End If
            If quantity = 1 Then
                ResultsDataTable.Rows.Add(id, String.Format("HQ1: {0}", name))
            Else
                ResultsDataTable.Rows.Add(id, String.Format("HQ1: {0} x{1}", name, quantity))
            End If
        End If

        If Not sr.IsHQ2Null Then
            id = sr.HQ2
            name = (From c In DataAccess.MobData.Items Where c.ItemID = id _
                    Select c.ItemName).FirstOrDefault
            If Not sr.IsHQ2QuantityNull Then
                quantity = sr.HQ2Quantity
            Else
                quantity = 1
            End If
            If quantity = 1 Then
                ResultsDataTable.Rows.Add(id, String.Format("HQ2: {0}", name))
            Else
                ResultsDataTable.Rows.Add(id, String.Format("HQ2: {0} x{1}", name, quantity))
            End If
        End If

        If Not sr.IsHQ3Null Then
            id = sr.HQ3
            name = (From c In DataAccess.MobData.Items Where c.ItemID = id _
                    Select c.ItemName).FirstOrDefault
            If Not sr.IsHQ3QuantityNull Then
                quantity = sr.HQ3Quantity
            Else
                quantity = 1
            End If
            If quantity = 1 Then
                ResultsDataTable.Rows.Add(id, String.Format("HQ3: {0}", name))
            Else
                ResultsDataTable.Rows.Add(id, String.Format("HQ3: {0} x{1}", name, quantity))
            End If
        End If
        Me.lstResults.DisplayMember = "Name"
        Me.lstResults.ValueMember = "ID"
        Me.lstResults.DataSource = ResultsDataTable
        Me.lstResults.SelectedIndex = -1
    End Sub

    Private Sub GetItemInfo()
        If Me.lstItems.SelectedIndex > -1 Then
            Dim itemID As Integer = Me.lstItems.SelectedValue
            Dim item = (From items In DataAccess.MobData.Items Where _
                        items.ItemID = itemID).FirstOrDefault
            If Not item Is Nothing Then
                BuildItemDisplay(item)
            Else
                ClearItemInfo()
            End If
        End If
    End Sub

    Private Sub BuildItemDisplay(ByVal Item As ItemsRow)
        Me.picIcon.Image = GetImage(Item.Icon)
        Me.lblItemName.Text = Item.ItemName
        Me.picRare.Visible = Item.Rare
        Me.picEx.Visible = Item.Ex
        Dim info As New System.Text.StringBuilder
        If Item.Slots <> String.Empty Then
            info.Append(String.Format("[{0}] {1}", Item.Slots, Item.Races))
            info.Append(Environment.NewLine)
        End If

        info.Append(Item.Description)
        info.Append(Environment.NewLine)
        If Item.ItemLevel > 0 Then
            info.Append(String.Format("Lv.{0} {1}", Item.ItemLevel, Item.Jobs))
        End If
        If Item.MaxCharges > 0 Then
            info.Append(Environment.NewLine)
            info.Append("                           ")
            info.Append(String.Format("<{0}/{0} {1}/[{2}, {1}]>", Item.MaxCharges, GetTime(Item.UseDelay), GetTime(Item.ReuseDelay)))
        End If
        Me.lblInfo.Text = info.ToString
    End Sub

    Private Function GetTime(ByVal Seconds As Integer) As String
        Dim output As String
        Dim time As New TimeSpan(0, 0, Seconds)
        If time.Hours > 0 Then
            output = String.Format("{0}:{1}:{2}", time.Hours, time.Minutes.ToString("00"), time.Seconds.ToString("00"))
        Else
            output = String.Format("{0}:{1}", time.Minutes, time.Seconds.ToString("00"))
        End If
        Return output
    End Function

    Private Function GetImage(ByVal Base64 As String) As Bitmap
        Dim imgBytes As Byte() = Convert.FromBase64String(Base64)
        Dim ms As New IO.MemoryStream(imgBytes, False)
        Return New Bitmap(ms)
    End Function

    Private Sub ClearItemInfo()
        Me.picIcon.Image = Nothing
        Me.picEx.Visible = False
        Me.picRare.Visible = False
        Me.lblItemName.Text = String.Empty
        Me.lblInfo.Text = String.Empty
    End Sub

    Private Sub ReloadSearchResults()
        If Me.rbRecipes.Checked Then
            SearchItem(Me.txtItem.Text, SearchType.Result)
        ElseIf Me.rbUsedIn.Checked Then
            SearchItem(Me.txtItem.Text, SearchType.UsedIn)
        ElseIf Me.rbDesynth.Checked Then
            SearchItem(Me.txtItem.Text, SearchType.Desynth)
        End If
        GetItemInfo()
    End Sub
#End Region

End Class