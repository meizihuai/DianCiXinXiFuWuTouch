Imports System
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Threading.Thread
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.DataVisualization.Charting
Imports Newtonsoft
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.Math
Imports System.Media
Imports OfficeOpenXml
Public Class PNTDeviceLog
    Public flagUIAready As Boolean = False
    Private CtlTSSFather As CtlOnlineTSS
    Private CtlTZBQFather As CtlOnlineTZBQ
    Private FatherType As Integer = 0
    Sub New(ByVal _CtlFather As CtlOnlineTSS)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        CtlTSSFather = _CtlFather
        FatherType = 0
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
    Sub New(ByVal _CtlTZBQFather As CtlOnlineTZBQ)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        CtlTZBQFather = _CtlTZBQFather
        FatherType = 1
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
    Private Sub PNTDeviceLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        flagUIAready = True
        If FatherType = 0 Then
            Label33.Text = CtlTSSFather.myDeviceInfo.Name
        End If
        If FatherType = 1 Then
            Label33.Text = CtlTZBQFather.myDeviceInfo.Name
        End If
        DTP.Value = Now.AddDays(-1)
        DTP2.Value = Now
        GetDeviceLog()
        iniLV()
    End Sub
    Private Sub iniLV()
        LVDeviceLog.Clear()
        LVDeviceLog.View = View.Details
        LVDeviceLog.GridLines = True
        LVDeviceLog.FullRowSelect = True
        LVDeviceLog.Columns.Add("序号", 45)
        LVDeviceLog.Columns.Add("设备ID", 50)
        LVDeviceLog.Columns.Add("台站名称", 150)
        LVDeviceLog.Columns.Add("时间", 150)
        LVDeviceLog.Columns.Add("地点", 150)
        LVDeviceLog.Columns.Add("发生事件", 200)
        LVDeviceLog.Columns.Add("执行结果", 150)
        LVDeviceLog.Columns.Add("设备状态", 150)
    End Sub
    Private Sub Panel19_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel19.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub
    Private Sub Panel19_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel19.Click
        GetDeviceLog()
    End Sub
    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        GetDeviceLog()
    End Sub
    Private Sub GetDeviceLog()
        Dim th As New Thread(AddressOf th_GetDevLog)
        th.Start()
    End Sub
    Private Sub th_GetDevLog()
        Dim OldText As String = Label8.Text
        Label8.Text = "正在搜索……"
        If Label33.Text = "未选择" Or Label33.Text = "" Then
            Label8.Text = OldText
            Me.Invoke(Sub()                        
                          MsgBox("请选择设备")
                      End Sub)
            Return
        End If
        LVDeviceLog.Visible = False
        LVDeviceLog.Items.Clear()
        Dim nickName As String = Label33.Text
        Dim sdate As String = DTP.Value.ToString("yyyy-MM-dd")
        Dim edate As String = DTP2.Value.ToString("yyyy-MM-dd")
        Dim str As String = "func=GetDeviceLogByNickNameWithTimeRegion&nickname=" & nickName & "&startTime=" & sdate & "&endTime=" & edate
        Dim result As String = GetServerResult(str)
        Try
            Me.Invoke(Sub()
                          Dim dt As DataTable = JsonConvert.DeserializeObject(result, GetType(DataTable))
                          If IsNothing(dt) Then Return
                          If dt.Rows.Count = 0 Then Return
                          LVDeviceLog.Items.Clear()
                          For i = 0 To dt.Rows.Count - 1
                              Dim row As DataRow = dt.Rows(i)
                              Dim itm As New ListViewItem(i + 1)
                              itm.SubItems.Add(row("DeviceID"))
                              itm.SubItems.Add(row("DeviceNickName"))
                              itm.SubItems.Add(row("Time"))
                              itm.SubItems.Add(row("Address"))
                              itm.SubItems.Add(row("Log"))
                              itm.SubItems.Add(row("Result"))
                              itm.SubItems.Add(row("Status"))
                              LVDeviceLog.Items.Add(itm)
                          Next
                      End Sub)

        Catch ex As Exception

        End Try
        LVDeviceLog.Visible = True
        Label8.Text = OldText
    End Sub
End Class
