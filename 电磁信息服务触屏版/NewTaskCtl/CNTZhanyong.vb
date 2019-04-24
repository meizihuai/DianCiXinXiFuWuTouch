Imports System
Imports System.IO
Imports System.Text
Imports System.Math
Imports Newtonsoft
Imports Newtonsoft.Json

Public Class CNTZhanyong
    Structure tssOrder_stu
        Dim deviceID As String
        Dim task As String
        Dim freqStart As Double
        Dim freqEnd As Double
        Dim freqStep As Double
        Dim saveFreqStep As Integer
        Dim Threshol As Double
        Dim Fucha As Double
        Dim Daikuan As Double
        Dim MinDValue As Double
        Dim Legal As List(Of Double)
        Dim WarnNum As Integer
    End Structure
    Private Sub CNTZhanyong_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        txtTaskNickName7.Text = "占用统计" & Now.ToString("yyyyMMddHHmmss")
        txtStartTime7.Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
        txtEndTime7.Text = Now.AddMinutes(3).ToString("yyyy-MM-dd HH:mm:ss")
        txtWechatName7.Text = ""
        txtEmailName7.Text = "619498477@qq.com"
        txtFreqStart7.Text = "88.000"
        txtFreqEnd7.Text = "108.000"
        txtFreqStep7.Text = "25.00"

    End Sub
    Public Sub AddDevice(ByVal dstu As DeviceStu)
        If dstu.Kind.ToLower <> "tss" Then
            MsgBox("频谱监测任务不支持非频谱传感器设备")
            Return
        End If
        Dim deviceName As String = dstu.Name
        Dim isFind As Boolean = False
        For j = CBDevices.Items.Count - 1 To 0 Step -1
            Dim itm As String = CBDevices.Items(j)
            If itm = deviceName Then
                ' CBDevices.Items.RemoveAt(j)
                isFind = True
                Exit For
            End If
        Next
        If isFind = False Then
            CBDevices.Items.Insert(0, deviceName)
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim task As NormalTaskStu
        task.UserName = usr
        task.TaskName = Label108.Text
        task.TaskNickName = txtTaskNickName7.Text
        task.StartTime = txtStartTime7.Text
        task.EndTime = txtEndTime7.Text
        task.TimeStep = 5
        task.PushWeChartToUserName = txtWechatName7.Text
        task.PushEmailToUserName = txtEmailName7.Text
        Dim TaskCode As String = ""
        Dim freqbegin As String = Val(txtFreqStart7.Text)
        Dim freqend As String = Val(txtFreqEnd7.Text)
        Dim freqstep As String = Val(txtFreqStep7.Text)
        Dim Threshol As String = Val(txtThreshol7.Text)
        If Threshol = "" Or freqbegin = "" Or freqend = "" Or freqstep = "" Or task.TaskNickName = "" Or task.StartTime = "" Or task.EndTime = "" Then
            MsgBox("请完整配置参数")
            Exit Sub
        End If
        If CBDevices.Items.Count = 0 Then
            MsgBox("请至少选择一台频谱传感器")
            Exit Sub
        End If
        task.TaskBg = txtTaskBg7.Text
        Dim count As Integer = CBDevices.Items.Count
        Dim successCount As Integer
        Dim failCount As Integer
        For i = 0 To count - 1
            Dim itmDeviceName As String = CBDevices.Items(i)
            ProgressBar1.Value = 100 * (i + 1) / count
            SyncLock alldevlist
                For Each itm In alldevlist
                    If itm.Name = itmDeviceName Then
                        task.DeviceID = itm.DeviceID
                        If count > 1 Then task.TaskNickName = txtTaskNickName7.Text & "_" & itmDeviceName
                        Exit For
                    End If
                Next
            End SyncLock
            Dim p As tssOrder_stu
            p.freqStart = freqbegin
            p.freqEnd = freqend
            p.freqStep = freqstep
            p.task = "bscan"
            p.deviceID = task.DeviceID
            p.Threshol = Threshol
            TaskCode = JsonConvert.SerializeObject(p)
            task.TaskCode = TaskCode
            Dim msg As String = JsonConvert.SerializeObject(task)
            Dim result As String = GetH(ServerUrl, "func=AddTask&token=" & token & "&TaskJson=" & msg)
            Dim res As String = GetResultPara("result", result)
            If res = "success" Then
                successCount = successCount + 1
            Else
                failCount = failCount + 1
                CBDevices.Items(i) = "<失败>" & CBDevices.Items(i) & "," & GetNorResult("msg", result)
            End If
            Dim str As String = "成功:{0}，失败:{1}，共计:{2}"
            str = String.Format(str, successCount, failCount, count)
            Label10.Text = str
        Next

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For i = 0 To CBDevices.Items.Count - 1
            CBDevices.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        For i = 0 To CBDevices.Items.Count - 1
            If CBDevices.GetItemChecked(i) Then
                CBDevices.SetItemChecked(i, False)
            Else
                CBDevices.SetItemChecked(i, True)
            End If
        Next
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        For i = CBDevices.Items.Count - 1 To 0 Step -1
            If CBDevices.GetItemChecked(i) Then CBDevices.Items.RemoveAt(i)
        Next
    End Sub
End Class
