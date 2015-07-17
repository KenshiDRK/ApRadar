Public Class SelectProcessDialog
    Public Sub New(ByVal procs As Process())

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.cboProcesses.DisplayMember = "MainWindowTitle"
        Me.cboProcesses.ValueMember = "Id"
        Me.cboProcesses.DataSource = procs
    End Sub

    Public ReadOnly Property Process() As Process
        Get
            Return Process.GetProcessById(Me.cboProcesses.SelectedValue)
        End Get
    End Property

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class