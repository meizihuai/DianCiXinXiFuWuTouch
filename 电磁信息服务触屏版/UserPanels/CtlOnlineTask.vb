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
Public Class CtlTask
    Dim TaskDt As DataTable
    Private Sub CtlOnlineTask_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Panel29.AutoScroll = True
        Me.BackColor = Color.Transparent
        Panel1.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        If isLoadGis Then
            WebGis.Navigate(gisurl)
        Else
            ini()
        End If
    End Sub
    Private Sub WebGis_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebGis.DocumentCompleted
        ini()
    End Sub
    Private Sub ini()
        Dim th As New Thread(AddressOf GetOnlineTask)
        th.Start()
    End Sub
    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        Dim th As New Thread(AddressOf GetOnlineTask)
        th.Start()
    End Sub
    Private Sub GetOnlineTask()
        Dim result As String = GetServerResult("func=GetMyTask")
        Panel29.Controls.Clear()
        Dim oldLabelText As String = Label5.Text
        Label5.Text = "正在获取……"
        If result = "[]" Then
            Label5.Text = oldLabelText
            Return
        End If
        Dim dt As DataTable = JsonConvert.DeserializeObject(result, GetType(DataTable))
        If IsNothing(dt) = False Then
            Dim dv As DataView = dt.DefaultView
            dv.Sort = "StartTime desc"
            Dim dt2 As DataTable = dv.ToTable
            TaskDt = New DataTable
            TaskDt = dt2
            Dim flagSelected As Boolean = False
            For Each row As DataRow In dt2.Rows
                If row("OverPercent") = "100%" Then Continue For
                Dim TaskNickName As String = row("TaskNickName")
                Dim StartTime As String = row("StartTime")
                If Not flagSelected Then
                    flagSelected = True
                    OnSelectOnlineTask(TaskNickName)
                End If
                Me.Invoke(Sub()
                              Dim u As New CtlTaskInfo(TaskNickName, StartTime)
                              AddHandler u.OnClick, AddressOf OnSelectOnlineTask
                              Panel29.Controls.Add(u)
                              Panel29.Controls.SetChildIndex(u, 0)
                          End Sub)
                'Dim itm As New ListViewItem(LVTask.Items.Count + 1)
                'itm.SubItems.Add(row("TaskName"))
                'itm.SubItems.Add(row("TaskNickName"))
                'itm.SubItems.Add(row("DeviceID"))
                'itm.SubItems.Add(row("DeviceName").ToString)
                'itm.SubItems.Add(row("StartTime"))
                'itm.SubItems.Add(row("EndTime"))
                'itm.SubItems.Add(row("OverPercent"))
                'itm.SubItems.Add(row("ResultReportUrl"))
                'LVTask.Items.Add(itm)
            Next
        End If
        Label5.Text = oldLabelText
    End Sub
    Private Sub OnSelectOnlineTask(ByVal TaskNickName As String)
        Dim row As DataRow
        For Each itm As DataRow In TaskDt.Rows
            If itm("TaskNickName") = TaskNickName Then
                row = itm
            End If
        Next
        Dim taskName As String = row("TaskName")
        Dim DeviceID As String = row("DeviceID")
        Dim DeviceName As String = row("DeviceName").ToString
        Dim StartTime As String = row("StartTime")
        Dim EndTime As String = row("EndTime")
        Dim StartDate As Date = Date.Parse(StartTime)
        Dim EndDate As Date = Date.Parse(EndTime)
        Dim nowDate As Date = Now
        Dim t1 As TimeSpan = EndDate - StartDate
        Dim t2 As TimeSpan = Now - StartDate
        Dim d As Double = t2.TotalSeconds / t1.TotalSeconds
        If d > 1 Then d = 1
        If StartTime >= Now Then
            d = 0
        End If
        Dim percent As String = (d * 100).ToString(0.0) & " %"
        Dim lng As String = ""
        Dim lat As String = ""
        For Each dev In alldevlist
            If dev.DeviceID = DeviceID Then
                lng = dev.Lng
                lat = dev.Lat
                Exit For
            End If
        Next
        Dim sb As New StringBuilder
        sb.Append("任务类型:" & taskName & "<br>")
        sb.Append("任务备注:" & taskNickName & "<br>")
        sb.Append("执行设备:" & DeviceName & "<br>")
        sb.Append("起始时间:" & StartTime & "<br>")
        sb.Append("结束时间:" & EndTime & "<br>")
        sb.Append("任务进度:" & percent)
        script(Me, "showWindowMsg", New String() {lng, lat, sb.ToString, True}, WebGis)
        script(Me, "setcenter", New String() {lng, lat}, WebGis)
    End Sub
End Class
