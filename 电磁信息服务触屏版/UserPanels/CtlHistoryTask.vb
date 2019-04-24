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
Public Class CtlHistoryTask

    Private Sub CtlHistoryTask_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackColor = Color.Transparent
        Panel1.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        Label5.Visible = False
        LVTask.View = View.Details
        LVTask.GridLines = True
        LVTask.FullRowSelect = True
        LVTask.Columns.Add("序号")
        LVTask.Columns.Add("任务类别")
        LVTask.Columns.Add("任务名称", 200)
        LVTask.Columns.Add("设备ID", 150)
        LVTask.Columns.Add("设备名称", 150)
        LVTask.Columns.Add("开始时间", 150)
        LVTask.Columns.Add("结束时间", 150)
        LVTask.Columns.Add("完成状态", 150)
        LVTask.Columns.Add("下载报告", 500)
        Dim th As New Thread(AddressOf GetTaskList)
        th.Start()
    End Sub

    Public Sub GetTaskList()
        Label5.Visible = True
        Label5.Text = "获取中……"
        Dim result As String = GetServerResult("func=GetMyTask")
        LVTask.Visible = False
        LVTask.Items.Clear()
        Label5.Visible = False
        If result = "[]" Then
            Return
        End If
        Dim dt As DataTable = JsonConvert.DeserializeObject(result, GetType(DataTable))
        If IsNothing(dt) = False Then
            Dim dv As DataView = dt.DefaultView
            dv.Sort = "StartTime desc"
            Dim dt2 As DataTable = dv.ToTable
            For Each row As DataRow In dt2.Rows
                Dim itm As New ListViewItem(LVTask.Items.Count + 1)
                itm.SubItems.Add(row("TaskName"))
                itm.SubItems.Add(row("TaskNickName"))
                itm.SubItems.Add(row("DeviceID"))
                itm.SubItems.Add(row("DeviceName").ToString)
                itm.SubItems.Add(row("StartTime"))
                itm.SubItems.Add(row("EndTime"))
                itm.SubItems.Add(row("OverPercent"))
                itm.SubItems.Add(row("ResultReportUrl"))
                LVTask.Items.Add(itm)
            Next
        End If
        Label5.Visible = False
        LVTask.Visible = True
    End Sub
    Private Sub DownSelectIndexReport()
        If LVTask.SelectedItems.Count <= 0 Then Exit Sub
        Dim value As String = LVTask.SelectedItems(0).SubItems(8).Text
        If value = "" Then Exit Sub
        If InStr(value, "http://") Then
            If ServerIP <> "123.207.31.37" Then
                value = value.Replace("123.207.31.37", ServerIP)
            End If
            Process.Start(value)
        Else
            MsgBox("下载路径有误，请检查")
        End If
    End Sub
    Private Sub DeleteSelectIndexTask()
        If LVTask.SelectedItems.Count <= 0 Then Exit Sub
        If MsgBox("是否确认删除该任务记录?", MsgBoxStyle.YesNoCancel, "提示") <> MsgBoxResult.Yes Then
            Exit Sub
        End If
        Dim value As String = LVTask.SelectedItems(0).SubItems(7).Text
        '  If value = "" Then Exit Sub
        Dim itm As ListViewItem = LVTask.SelectedItems(0)
        Dim TaskName As String = itm.SubItems(1).Text
        Dim TaskNickName As String = itm.SubItems(2).Text
        Dim DeviceID As String = itm.SubItems(3).Text
        Dim StartTime As String = itm.SubItems(5).Text
        Dim EndTime As String = itm.SubItems(6).Text
        Dim dik As New Dictionary(Of String, String)
        dik.Add("token", token)
        dik.Add("func", "DeleteMyTask")
        dik.Add("TaskName", TaskName)
        dik.Add("TaskNickName", TaskNickName)
        dik.Add("deviceID", DeviceID)
        dik.Add("StartTime", StartTime)
        dik.Add("EndTime", EndTime)
        Dim msg As String = TransforPara2Query(dik)
        Dim url As String = ServerUrl
        Dim result As String = GetH(url, msg)
        If GetResultPara("result", result) = "success" Then
            MsgBox("删除成功！")
        Else
            MsgBox(result)
        End If
        GetTaskList()
    End Sub
    Private Sub Panel21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel21.Click
        DownSelectIndexReport()
    End Sub

    Private Sub Label35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label35.Click
        DownSelectIndexReport()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        DownSelectIndexReport()
    End Sub

    Private Sub Panel5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel5.Click
        DeleteSelectIndexTask()
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        DeleteSelectIndexTask()
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        DeleteSelectIndexTask()
    End Sub
End Class
