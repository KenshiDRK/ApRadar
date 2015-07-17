<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BrowserForm
    Inherits ApRadar3.ResizableForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BrowserForm))
        Me.Browser = New System.Windows.Forms.WebBrowser()
        Me.NavPanel = New System.Windows.Forms.Panel()
        Me.cmdForward = New System.Windows.Forms.Button()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.picLoading = New System.Windows.Forms.PictureBox()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.lblHeder = New System.Windows.Forms.Label()
        Me.blnClose = New System.Windows.Forms.PictureBox()
        Me.NavPanel.SuspendLayout()
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Browser
        '
        Me.Browser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Browser.Location = New System.Drawing.Point(12, 69)
        Me.Browser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.Browser.Name = "Browser"
        Me.Browser.ScriptErrorsSuppressed = True
        Me.Browser.Size = New System.Drawing.Size(760, 485)
        Me.Browser.TabIndex = 0
        Me.Browser.Visible = False
        '
        'NavPanel
        '
        Me.NavPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.NavPanel.Controls.Add(Me.cmdForward)
        Me.NavPanel.Controls.Add(Me.cmdBack)
        Me.NavPanel.Controls.Add(Me.picLoading)
        Me.NavPanel.Controls.Add(Me.txtUrl)
        Me.NavPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavPanel.Location = New System.Drawing.Point(0, 33)
        Me.NavPanel.Name = "NavPanel"
        Me.NavPanel.Size = New System.Drawing.Size(784, 38)
        Me.NavPanel.TabIndex = 1
        '
        'cmdForward
        '
        Me.cmdForward.Enabled = False
        Me.cmdForward.Image = CType(resources.GetObject("cmdForward.Image"), System.Drawing.Image)
        Me.cmdForward.Location = New System.Drawing.Point(42, 6)
        Me.cmdForward.Name = "cmdForward"
        Me.cmdForward.Size = New System.Drawing.Size(24, 24)
        Me.cmdForward.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmdForward, "Forward")
        Me.cmdForward.UseVisualStyleBackColor = True
        '
        'cmdBack
        '
        Me.cmdBack.Enabled = False
        Me.cmdBack.Image = CType(resources.GetObject("cmdBack.Image"), System.Drawing.Image)
        Me.cmdBack.Location = New System.Drawing.Point(12, 6)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.Size = New System.Drawing.Size(24, 24)
        Me.cmdBack.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmdBack, "Back")
        Me.cmdBack.UseVisualStyleBackColor = True
        '
        'picLoading
        '
        Me.picLoading.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picLoading.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picLoading.Image = CType(resources.GetObject("picLoading.Image"), System.Drawing.Image)
        Me.picLoading.Location = New System.Drawing.Point(745, 6)
        Me.picLoading.Name = "picLoading"
        Me.picLoading.Size = New System.Drawing.Size(27, 24)
        Me.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picLoading.TabIndex = 5
        Me.picLoading.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picLoading, "Forward")
        Me.picLoading.Visible = False
        '
        'txtUrl
        '
        Me.txtUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUrl.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUrl.Location = New System.Drawing.Point(72, 8)
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(667, 20)
        Me.txtUrl.TabIndex = 4
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackgroundImage = Global.ApRadar3.My.Resources.Resources.tbg
        Me.HeaderPanel.Controls.Add(Me.lblHeder)
        Me.HeaderPanel.Controls.Add(Me.blnClose)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Padding = New System.Windows.Forms.Padding(5, 0, 6, 0)
        Me.HeaderPanel.Size = New System.Drawing.Size(784, 33)
        Me.HeaderPanel.TabIndex = 5
        '
        'lblHeder
        '
        Me.lblHeder.AutoSize = True
        Me.lblHeder.BackColor = System.Drawing.Color.Transparent
        Me.lblHeder.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeder.ForeColor = System.Drawing.Color.White
        Me.lblHeder.Location = New System.Drawing.Point(9, 6)
        Me.lblHeder.Name = "lblHeder"
        Me.lblHeder.Size = New System.Drawing.Size(114, 18)
        Me.lblHeder.TabIndex = 1
        Me.lblHeder.Text = "ApRadar Browser"
        '
        'blnClose
        '
        Me.blnClose.BackColor = System.Drawing.Color.Transparent
        Me.blnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.blnClose.Image = Global.ApRadar3.My.Resources.Resources.Close
        Me.blnClose.Location = New System.Drawing.Point(762, 0)
        Me.blnClose.Name = "blnClose"
        Me.blnClose.Size = New System.Drawing.Size(16, 33)
        Me.blnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.blnClose.TabIndex = 0
        Me.blnClose.TabStop = False
        '
        'BrowserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(784, 566)
        Me.Controls.Add(Me.Browser)
        Me.Controls.Add(Me.NavPanel)
        Me.Controls.Add(Me.HeaderPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BrowserForm"
        Me.Opacity = 0.8R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ApRadar Browser"
        Me.NavPanel.ResumeLayout(False)
        Me.NavPanel.PerformLayout()
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.blnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Browser As System.Windows.Forms.WebBrowser
    Friend WithEvents NavPanel As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblHeder As System.Windows.Forms.Label
    Friend WithEvents blnClose As System.Windows.Forms.PictureBox
    Friend WithEvents cmdForward As System.Windows.Forms.Button
    Friend WithEvents cmdBack As System.Windows.Forms.Button
    Friend WithEvents picLoading As System.Windows.Forms.PictureBox
    Friend WithEvents txtUrl As System.Windows.Forms.TextBox
End Class
