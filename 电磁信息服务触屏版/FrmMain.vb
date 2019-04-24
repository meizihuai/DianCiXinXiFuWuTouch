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
Public Class FrmMain
    Dim title As String = "电磁信息云服务触屏版系统触屏版 " & Version
    Dim defaultCtlMain As CtlMain
    Private Sub FrmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub
    Private Sub FrmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Panel5.Dock = DockStyle.Bottom
        Panel5.Visible = False
        Me.WindowState = FormWindowState.Maximized
        Control.CheckForIllegalCrossThreadCalls = False
        PanelMain.BackColor = Color.Transparent       
        ResizeControls()
        lblTitle.BackColor = Color.Transparent
        Me.Text = title
        Dim th As New Thread(AddressOf StatusTimer)
        th.Start()
        ini()
    End Sub
    Private Sub StatusTimer()
        While True
            Dim str As String = Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim week As Integer = Now.DayOfWeek
            Dim ws() As String = New String() {"星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"}
            str = str & " " & ws(week) & " " & usr
            Label4.Text = str
            Sleep(1000)
        End While
    End Sub
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            If Not DesignMode Then
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ExStyle = cp.ExStyle Or &H2000000
                Return cp
            Else
                Return MyBase.CreateParams
            End If
        End Get
    End Property
    Private Sub DefaultShow()
        Panel5.Visible = False
        lblTitle.Visible = True
        If IsNothing(defaultCtlMain) Then
            defaultCtlMain = New CtlMain
            AddHandler defaultCtlMain.OnSelectDeviceChange, AddressOf SelectDeviceChanged
            AddHandler defaultCtlMain.OnGuidChange, AddressOf OnGuidChange
        End If
        PanelIndex.Controls.Clear()
        PanelIndex.Controls.Add(defaultCtlMain)
        'OnGuidChange("监测服务")
    End Sub
    Private Sub SelectDeviceByName(ByVal dName As String)
        For Each dstu In alldevlist
            If dstu.Name = dName Then
                SelectDeviceChanged(dstu)
                Return
            End If
        Next
    End Sub
    Public Sub SelectDeviceChanged(ByVal dstu As DeviceStu)
        If IsNothing(selectedDeviceStu) Then
            selectedDeviceStu = dstu
        Else
            If selectedDeviceStu.DeviceID <> dstu.DeviceID Then
                selectedDeviceStu = dstu
            End If
        End If
        Dim kind As String = "频谱传感器"
        If selectedDeviceStu.Kind.ToLower = "tzbq" Then
            kind = "微型传感器"
        End If
        Label2.Text = "选中 " & kind & " " & selectedDeviceStu.Name
    End Sub
    Private Sub OnGuidChange(ByVal gName As String)
        If gName = "值班管理" Then
            Dim ctl As New CtlDuty
            PanelIndex.Controls.Clear()
            PanelIndex.Controls.Add(ctl)
        End If
        If gName = "任务管理" Then
            Dim ctl As New CtlTaskManager
            PanelIndex.Controls.Clear()
            PanelIndex.Controls.Add(ctl)
        End If
        If gName = "设备管理" Then
            Dim ctl As New CtlDeviceSetting
            PanelIndex.Controls.Clear()
            PanelIndex.Controls.Add(ctl)
        End If
        If gName = "数据服务" Then
            Dim ctl As New CTLDataService
            PanelIndex.Controls.Clear()
            PanelIndex.Controls.Add(ctl)
        End If
        If gName = "公交系统" Then
            Dim ctl As New CTLBusSystem
            PanelIndex.Controls.Clear()
            PanelIndex.Controls.Add(ctl)
        End If
        If gName = "信息服务" Then
            Dim ctl As New CTLInfoService
            PanelIndex.Controls.Clear()
            PanelIndex.Controls.Add(ctl)
        End If
        If gName = "监测服务" Then
            If IsNothing(selectedDeviceStu) Then
                MsgBox("请先选择设备，再进行监测")
                Return
            End If
            If selectedDeviceStu.Kind.ToLower = "tss" Then
                Dim ctl As New CtlOnlineTSS(selectedDeviceStu)
                PanelIndex.Controls.Clear()
                PanelIndex.Controls.Add(ctl)
            End If
            If selectedDeviceStu.Kind.ToLower = "tzbq" Then
                Dim ctl As New CtlOnlineTZBQ(selectedDeviceStu)
                PanelIndex.Controls.Clear()
                PanelIndex.Controls.Add(ctl)
            End If
        End If
        lblTitle2.Text = gName
        lblTitle.Visible = False
        Panel5.Visible = True
    End Sub

    Private Sub Frm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        ResizeControls()
    End Sub
    Private Sub ResizeControls()
        If Me.WindowState = FormWindowState.Minimized Then Return
        Dim bmp As New Bitmap(Me.Width, Me.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        Dim FColor As Color = ColorTranslator.FromHtml("#5C5278")
        Dim TColor As Color = ColorTranslator.FromHtml("#4FA9B5")
        Dim b As Brush = New LinearGradientBrush(Me.ClientRectangle, FColor, TColor, LinearGradientMode.Vertical)
        g.FillRectangle(b, Me.ClientRectangle)
        Me.BackgroundImage = bmp
    End Sub
    Private Sub ini()
        Dim th As New Thread(AddressOf thini)
        th.Start()
    End Sub

    Public Sub thini()
        If ServerIP = "123.207.31.37" Then
            Me.Text = title & "  [ " & "云服务" & " ]"
        ElseIf ServerIP = "61.145.180.149" Then
            Me.Text = title & "  [ " & "东莞网" & " ]"
        Else
            Me.Text = title & "  [ " & ServerIP & " ]"
        End If
        Label2.Text = ""
        Dim bk As String = Label2.Text
        Label2.Text = "正在检测公网连接……"
        Dim p As New Ping
        Dim ps As PingReply = p.Send("123.207.31.37")
        If ps.Status = IPStatus.Success Then
            isLoadGis = True
        Else
            isLoadGis = False
        End If
        Label2.Text = "正在获取在线设备列表……"
        GetOnlineDevice()
        Label2.Text = bk
        Me.Invoke(Sub() DefaultShow())
    End Sub
    Public Sub GetOnlineDevice()
        Dim list As List(Of DeviceStu) = MZHelper.GetOnlineDeviceList
        If IsNothing(list) = False Then
            alldevlist = list
        End If
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        DefaultShow()
        If IsNothing(RunReciveMsgCtlTSS) = False Then
            RunReciveMsgCtlTSS.CloseAll()
        End If
        If IsNothing(RunReciveMsgCtlTZBQ) = False Then
            RunReciveMsgCtlTZBQ.CloseAll()
        End If
    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        'SelectDeviceByName("广州大道怡景1")
        OnGuidChange("监测服务")
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        selectDeviceFrm.Show()
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub
End Class