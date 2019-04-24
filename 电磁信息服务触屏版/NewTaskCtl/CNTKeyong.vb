Imports System
Imports System.IO
Imports System.Text
Imports System.Math
Imports Newtonsoft
Imports Newtonsoft.Json
Public Class CNTKeyong

    Private Sub CNTKeyong_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        txtNicName2.Text = Label24.Text & Now.ToString("yyyyMMddHHmmss")
        txtStartTime2.Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
        txtEndTIme2.Text = Now.AddMinutes(3).ToString("yyyy-MM-dd HH:mm:ss")
        txtWechatName2.Text = ""
        txtEmailName2.Text = "619498477@qq.com"
        Dim sb As New StringBuilder
        sb.AppendLine("400.000")
        sb.AppendLine("435.000")
        sb.AppendLine("446.000")
        sb.AppendLine("463.000")
        sb.AppendLine("495.000")
        RTB.Text = sb.ToString
    End Sub

    Private Sub RTB_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RTB.TextChanged
        Dim pdList As New List(Of Double)
        For Each itm In RTB.Text.Split(Chr(10))
            If itm <> "" Then
                If IsNumeric(itm) Then
                    pdList.Add(Val(itm))
                End If
            End If
        Next
        Label6.Text = pdList.Count
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For i = 0 To CBDevices.Items.Count - 1
            CBDevices.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
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
    Public Sub AddDevice(ByVal dstu As DeviceStu)
        If dstu.Kind.ToLower <> "tzbq" Then
            MsgBox("可用评估任务不支持非微型传感器设备")
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
  

    
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim task As NormalTaskStu
        task.UserName = usr
        task.TaskName = Label24.Text
        task.TaskNickName = txtNicName2.Text
        task.StartTime = txtStartTime2.Text
        task.EndTime = txtEndTIme2.Text
        task.TimeStep = 5
        task.PushWeChartToUserName = txtWechatName2.Text
        task.PushEmailToUserName = txtEmailName2.Text
        Dim TaskCode As String = ""
        Dim pdList As New List(Of Double)
        For Each itm In RTB.Text.Split(Chr(10))
            If itm <> "" Then
                If IsNumeric(itm) Then
                    pdList.Add(Val(itm))
                End If
            End If
        Next
        TaskCode = JsonConvert.SerializeObject(pdList)
        If TaskCode = "" Or task.TaskNickName = "" Or task.StartTime = "" Or task.EndTime = "" Then
            MsgBox("请完整配置参数")
            Exit Sub
        End If
        If CBDevices.Items.Count = 0 Then
            MsgBox("请至少选择一台微型传感器")
            Exit Sub
        End If
        task.TaskCode = TaskCode
        task.TaskBg = txtTaskBg2.Text
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
                        If count > 1 Then task.TaskNickName = txtNicName2.Text & "_" & itmDeviceName
                        Exit For
                    End If
                Next
            End SyncLock
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
            Label3.Text = str
        Next
    End Sub
End Class
