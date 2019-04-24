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
Imports System.Reflection

Public Class CtlOnlineTSS
    Private HttpMsgUrl As String
    Public myDeviceInfo As DeviceStu
    Private selectIndex As Integer
    Private selectColor As Color = Color.FromArgb(0, 86, 131)
    Private unSelectColor As Color = Color.FromArgb(0, 65, 106)
    Private threadReciveNewMsg As Thread
    Private selectCtlLock As Object
    Private ControlerVersion As String
    Private CtlFreqBscan As PNTFreqBscan
    Private CtlFreqMidScan As PNTFreqMidScan
    Private CtlFreqPointScan As PNTFreqPointScan
    Private CtlFreqBuHuo As PNTFreqBuhuo
    Private CtlFreqAudio As PNTFreqAudio
    Private CtlFreqSetting As PNTFreqSetting
    Private CtlDeviceLog As PNTDeviceLog
    Sub New(ByVal selectDeviceStu As DeviceStu)
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        myDeviceInfo = selectDeviceStu
    End Sub
    Public Sub CloseAll()
        Try
            If IsNothing(threadReciveNewMsg) = False Then
                threadReciveNewMsg.Abort()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CtlOnlineTSS_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        CloseAll()
    End Sub
    Private Sub PTLFreqBscan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        selectCtlLock = New Object
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        SelectPanelTabIndex(1)
        If IsNothing(RunReciveMsgCtlTSS) = False Then
            RunReciveMsgCtlTSS.CloseAll()
        End If
        If IsNothing(RunReciveMsgCtlTZBQ) = False Then
            RunReciveMsgCtlTZBQ.CloseAll()
        End If
        RunReciveMsgCtlTSS = Me
        threadReciveNewMsg = New Thread(AddressOf ReciveDeviceNewMsg)
        threadReciveNewMsg.Start()
    End Sub
    Private Sub ReciveDeviceNewMsg()
        HttpMsgUrl = GetH(ServerUrl, "func=GetHttpMsgUrlById&deviceID=" & myDeviceInfo.DeviceID & "&token=" & token)
        If GetResultPara("result", HttpMsgUrl) = "fail" Then
            If GetResultPara("msg", HttpMsgUrl) = "Please login" Then
                Login()
                HttpMsgUrl = GetH(ServerUrl, "func=GetHttpMsgUrlById&deviceID=" & myDeviceInfo.DeviceID & "&token=" & token)
            End If
        End If
        HttpMsgUrl = HttpMsgUrl.Replace("123.207.31.37", ServerIP)
        Console.WriteLine("HttpMsgUrl=" & HttpMsgUrl)
        Dim th As New Thread(AddressOf GetControlerVersion)
        th.Start()
        While True
            Dim result As String = GetHttpMsg()
            '  Console.WriteLine("recive new device msg result.length=" & result.Length)
            If result = "" Then Continue While
            Console.WriteLine("recive new device msg")
            HandleHttpMsg(result)
        End While
    End Sub
    Private Sub HandleHttpMsg(ByVal HttpMsg As String)
        Try
            Dim isHandleOver As Boolean = False
            Try
                SyncLock selectCtlLock
                    Dim index As Integer = selectIndex
                    Select Case index
                        Case 0
                            If IsNothing(CtlFreqAudio) = False Then
                                If CtlFreqAudio.flagUIAready Then CtlFreqAudio.HandleHttpMsg(HttpMsg)
                            End If
                        Case 1
                            If IsNothing(CtlFreqBscan) = False Then
                                If CtlFreqBscan.flagUIAready Then CtlFreqBscan.HandleHttpMsg(HttpMsg)
                            End If
                        Case 2
                            If IsNothing(CtlFreqMidScan) = False Then
                                If CtlFreqMidScan.flagUIAready Then CtlFreqMidScan.HandleHttpMsg(HttpMsg)
                            End If
                        Case 3
                            If IsNothing(CtlFreqPointScan) = False Then
                                If CtlFreqPointScan.flagUIAready Then CtlFreqPointScan.HandleHttpMsg(HttpMsg)
                            End If
                        Case 4
                            If IsNothing(CtlFreqBuHuo) = False Then
                                If CtlFreqBuHuo.flagUIAready Then CtlFreqBuHuo.HandleHttpMsg(HttpMsg)
                            End If
                        Case 5
                            If IsNothing(CtlFreqSetting) = False Then
                                If CtlFreqSetting.flagUIAready Then CtlFreqSetting.HandleHttpMsg(HttpMsg)
                            End If

                            'Dim t As Type = selectCtl.GetType()
                            'Dim obj As Object = Activator.CreateInstance(t)
                            'Dim mf As MethodInfo = t.GetMethod("HandleHttpMsg")
                            'mf.Invoke(obj, New Object() {HttpMsg})  


                    End Select
                End SyncLock
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                'MsgBox(ex.ToString)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetControlerVersion()
        Dim result As String = GethWithToken(HttpMsgUrl, "func=GetControlerVersion")
        ControlerVersion = result
    End Sub
    Private Function GetHttpMsg() As String
        Try
            Dim url As String = HttpMsgUrl & "?func=GetDevMsg"
            Dim req As HttpWebRequest = WebRequest.Create(url)
            ' Me.Invoke(Sub() MsgBox(HttpMsgUrl & "?func=GetDevMsg"))
            req.Accept = "*/*"
            req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN; rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13"
            req.KeepAlive = True
            req.Timeout = 5000
            req.ReadWriteTimeout = 5000
            req.ContentType = "application/x-www-form-urlencoded"
            req.Method = "GET"
            Dim rp As HttpWebResponse = req.GetResponse
            Dim str As String = New StreamReader(rp.GetResponseStream(), Encoding.UTF8).ReadToEnd
            Dim b() As Byte = Encoding.Default.GetBytes(str)
            dowload = dowload + b.Length
            Return str
        Catch ex As Exception
        End Try
    End Function
    Private Sub CloseOldCtl()
        If selectIndex = 0 Then
            If IsNothing(CtlFreqAudio) = False Then
                Try
                    CtlFreqAudio.Dispose()
                    CtlFreqAudio = Nothing
                Catch ex As Exception

                End Try
            End If
        End If
        If selectIndex = 1 Then
            If IsNothing(CtlFreqBscan) = False Then
                Try
                    CtlFreqBscan.Dispose()
                    CtlFreqBscan = Nothing
                Catch ex As Exception

                End Try
            End If
        End If
        If selectIndex = 2 Then
            If IsNothing(CtlFreqMidScan) = False Then
                Try
                    CtlFreqMidScan.Dispose()
                    CtlFreqMidScan = Nothing
                Catch ex As Exception

                End Try
            End If
        End If
        If selectIndex = 3 Then
            If IsNothing(CtlFreqPointScan) = False Then
                Try
                    CtlFreqPointScan.Dispose()
                    CtlFreqPointScan = Nothing
                Catch ex As Exception

                End Try
            End If
        End If
        If selectIndex = 4 Then
            If IsNothing(CtlFreqBuHuo) = False Then
                Try
                    CtlFreqBuHuo.Dispose()
                    CtlFreqBuHuo = Nothing
                Catch ex As Exception

                End Try
            End If
        End If
        If selectIndex = 5 Then
            If IsNothing(CtlFreqSetting) = False Then
                Try
                    CtlFreqSetting.Dispose()
                    CtlFreqSetting = Nothing
                Catch ex As Exception

                End Try
            End If
        End If
        If selectIndex = 6 Then
            If IsNothing(CtlDeviceLog) = False Then
                Try
                    CtlDeviceLog.Dispose()
                    CtlDeviceLog = Nothing
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub
    Private Sub SelectPanelTabIndex(ByVal index As Integer)
        Dim pList As New List(Of Panel)
        pList.Add(Ptn0)
        pList.Add(Ptn1)
        pList.Add(Ptn2)
        pList.Add(Ptn3)
        pList.Add(Ptn4)
        pList.Add(Ptn5)
        pList.Add(Ptn6)
        For i = 0 To pList.Count - 1
            If index = i Then
                pList(i).BackColor = selectColor
            Else
                pList(i).BackColor = unSelectColor
            End If
        Next
        CloseOldCtl()
        selectIndex = index
        SyncLock selectCtlLock
            If index = 0 Then '远端侦听          
                Panel5.Controls.Clear()
                Dim ctl As New PNTFreqAudio(Me)
                Panel5.Controls.Add(ctl)
                CtlFreqAudio = ctl
            End If
            If index = 1 Then '频谱扫描
                Panel5.Controls.Clear()
                Dim ctl As New PNTFreqBscan(Me)
                Panel5.Controls.Add(ctl)
                CtlFreqBscan = ctl
            End If
            If index = 2 Then '中频分析
                Panel5.Controls.Clear()
                Dim ctl As New PNTFreqMidScan(Me)
                Panel5.Controls.Add(ctl)
                CtlFreqMidScan = ctl
            End If
            If index = 3 Then '离散扫描
                Panel5.Controls.Clear()
                Dim ctl As New PNTFreqPointScan(Me)
                Panel5.Controls.Add(ctl)
                CtlFreqPointScan = ctl
            End If
            If index = 4 Then '条件捕获
                Panel5.Controls.Clear()
                Dim ctl As New PNTFreqBuhuo(Me)
                Panel5.Controls.Add(ctl)
                CtlFreqBuHuo = ctl
            End If
            If index = 5 Then '设备设置
                Panel5.Controls.Clear()
                Dim ctl As New PNTFreqSetting(Me)
                Panel5.Controls.Add(ctl)
                CtlFreqSetting = ctl
            End If
            If index = 6 Then '设备日志
                Panel5.Controls.Clear()
                Dim ctl As New PNTDeviceLog(Me)
                Panel5.Controls.Add(ctl)              
                CtlDeviceLog = ctl
            End If
        End SyncLock
    End Sub

    Private Sub Ptn0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ptn0.Click
        SelectPanelTabIndex(0)
    End Sub
    Private Sub Ptn1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ptn1.Click
        SelectPanelTabIndex(1)
    End Sub
    Private Sub Ptn2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ptn2.Click
        SelectPanelTabIndex(2)
    End Sub
    Private Sub Ptn3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ptn3.Click
        SelectPanelTabIndex(3)
    End Sub
    Private Sub Ptn4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ptn4.Click
        SelectPanelTabIndex(4)
    End Sub
    Private Sub Ptn5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ptn5.Click
        SelectPanelTabIndex(5)
    End Sub
    Private Sub Ptn6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ptn6.Click
        SelectPanelTabIndex(6)
    End Sub
    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        SelectPanelTabIndex(0)
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        SelectPanelTabIndex(1)
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        SelectPanelTabIndex(2)
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        SelectPanelTabIndex(3)
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        SelectPanelTabIndex(4)
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        SelectPanelTabIndex(5)
    End Sub

    Private Sub Panel5_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel5.Paint

    End Sub

    Public Sub SendFreqOrder(ByVal freqbegin As Double, ByVal freqend As Double, ByVal freqstep As Double, ByVal gcValue As Integer, ByVal isHackOneSingle As Boolean)
        Dim msg As String = ""
        Dim p As tssOrder_stu
        p.freqStart = freqbegin
        p.freqEnd = freqend
        p.freqStep = freqstep
        p.gcValue = gcValue
        p.task = "bscan"
        Dim DHDeviceStr As String = "2to1"
        If isHackOneSingle Then
            DHDeviceStr = "one"
        End If
        p.DHDevice = DHDeviceStr
        p.deviceID = myDeviceInfo.DeviceID
        Dim orderMsg As String = JsonConvert.SerializeObject(p)
        SendMsgToDev(orderMsg)
    End Sub
    Public Sub SendStopOrder()
        Dim p As tssOrder_stu
        p.freqStart = 88
        p.freqEnd = 108
        p.freqStep = 25
        p.task = "stop"
        p.deviceID = ""
        Dim orderMsg As String = JsonConvert.SerializeObject(p)
        SendMsgToDev(orderMsg)
    End Sub
    Public Function SendMsgToDev(ByVal orderMsg As String)
        Dim th As New Thread(AddressOf Th_SendMsgToDev)
        th.Start(orderMsg)
    End Function
    Private Sub Th_SendMsgToDev(ByVal orderMsg As String)
        Dim str As String = "?func=tssOrder&datamsg=" & orderMsg & "&token=" & token
        Dim result As String = GetH(HttpMsgUrl, str)
        Dim r As String = GetNorResult("result", result)
        Dim msg As String = GetNorResult("msg", result)
        Dim errmsg As String = GetNorResult("errmsg", result)
        Me.Invoke(Sub()
                      If r = "success" Then
                          Dim w As New WarnBox("命令下发成功！")
                          w.Show()
                      Else
                          If msg = "Please login" Then
                              Login()
                              SendMsgToDev(orderMsg)
                          Else
                              Dim sb As New StringBuilder
                              sb.AppendLine("命令下发失败")
                              sb.AppendLine(result)
                              MsgBox(sb.ToString)
                          End If
                      End If
                  End Sub)

    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        SelectPanelTabIndex(6)
    End Sub
End Class
