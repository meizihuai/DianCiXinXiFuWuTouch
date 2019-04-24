Imports System
Imports System.IO
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Threading
Imports System.Threading.Thread
Imports System.Net
Imports System.Net.NetworkInformation
Public Class CtlTaskManager
    Private selectColor As Color = Color.FromArgb(0, 86, 131)
    Private unSelectColor As Color = Color.FromArgb(0, 65, 106)
    Private Sub CtlTaskManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Panel5.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        SelectPanelTabIndex(0)
        ini()
        ' SelectPanelTabIndex(2)
    End Sub
    Private Sub Panel21_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelOnlineTask.Click
        SelectPanelTabIndex(0)
    End Sub
    Private Sub Panel15_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelHistoryTask.Click
        SelectPanelTabIndex(1)
    End Sub
    Private Sub Panel16_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelNewTask.Click
        SelectPanelTabIndex(2)
    End Sub
    Private Sub SelectPanelTabIndex(ByVal index As Integer)
        If index = 0 Then
            PanelOnlineTask.BackColor = selectColor
            PanelHistoryTask.BackColor = unSelectColor
            PanelNewTask.BackColor = unSelectColor
            PanelTab.Controls.Clear()
            Dim ctl As New CtlTask
            PanelTab.Controls.Add(ctl)
        End If
        If index = 1 Then
            PanelOnlineTask.BackColor = unSelectColor
            PanelHistoryTask.BackColor = selectColor
            PanelNewTask.BackColor = unSelectColor
            PanelTab.Controls.Clear()
            Dim ctl As New CtlHistoryTask
            PanelTab.Controls.Add(ctl)
        End If
        If index = 2 Then
            PanelOnlineTask.BackColor = unSelectColor
            PanelHistoryTask.BackColor = unSelectColor
            PanelNewTask.BackColor = selectColor
            PanelTab.Controls.Clear()
            Dim ctl As New CtlNewTask
            PanelTab.Controls.Add(ctl)
        End If
    End Sub
    Private Sub ini()
        Dim th As New Thread(AddressOf ThGetTaskPanInfo)
        th.Start()
    End Sub
    Private Sub ThGetTaskPanInfo()
        Dim np As NormalResponse = MZHelper.GetTaskPanInfo
        If np.result = False Then Return
        Label1.Text = "刷新时间:" & Now.ToString("HH:mm:ss")
        Dim obj As JObject = np.data
        Dim unOverCount As Integer = obj("unOverCount")
        Dim overCount As Integer = obj("overCount")
        Dim onlineDeivceCount As Integer = obj("onlineDeivceCount")
        Me.Invoke(Sub()
                      Label34.Text = unOverCount
                      Label12.Text = overCount
                      Label15.Text = onlineDeivceCount
                  End Sub)
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        Dim th As New Thread(AddressOf ThGetTaskPanInfo)
        th.Start()
    End Sub

    Private Sub PanelNewTask_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelNewTask.Paint

    End Sub

    Private Sub PanelOnlineTask_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelOnlineTask.Paint

    End Sub

    Private Sub PanelTab_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelTab.Paint

    End Sub

    Private Sub Label34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label34.Click

    End Sub
End Class
