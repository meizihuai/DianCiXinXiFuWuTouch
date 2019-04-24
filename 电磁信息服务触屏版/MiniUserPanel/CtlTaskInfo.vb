Public Class CtlTaskInfo
    Private myTaskNickName As String
    Private myStartTime As String
    Public Event OnClick(ByVal taskNickName As String)
    Sub New(ByVal _taskNickName As String, ByVal _startTime As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        myTaskNickName = _taskNickName
        myStartTime = _starttime
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
    Private Sub lblTaskNickName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTaskNickName.Click
        RaiseEvent OnClick(myTaskNickName)
    End Sub

    Private Sub CtlTaskInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Top
        Me.Cursor = Cursors.Hand
        lblTaskNickName.Text = myTaskNickName
        LblTime.Text = myStartTime
    End Sub

    Private Sub LblTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblTime.Click
        RaiseEvent OnClick(myTaskNickName)
    End Sub

    Private Sub Panel50_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel50.Click
        RaiseEvent OnClick(myTaskNickName)
    End Sub

End Class
