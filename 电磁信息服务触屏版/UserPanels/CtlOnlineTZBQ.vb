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
Public Class CtlOnlineTZBQ
    Private HttpMsgUrl As String
    Public myDeviceInfo As DeviceStu
    Private selectIndex As Integer
    Private selectColor As Color = Color.FromArgb(0, 86, 131)
    Private unSelectColor As Color = Color.FromArgb(0, 65, 106)
    Private threadReciveNewMsg As Thread
    Private selectCtlLock As Object
    Private CtlFreqBscan As PNTZBQFreqBscan
    Private CtlBuhuo As PNTZBQBuhuo
    Private CtlPointScan As PNTZBQPointScan
    Private CtlJiandu As PNTZBQJiandu
    Private CtlSetting As PNTZBQSetting
    Private CtlDeviceLog As PNTDeviceLog

    Sub New(ByVal _myDeviceInfo As DeviceStu)
        '此调用是设计器所必需的。
        InitializeComponent()
        myDeviceInfo = _myDeviceInfo
        '在 InitializeComponent() 调用之后添加任何初始化。
    End Sub
    Public Sub CloseAll()
        Try
            If IsNothing(threadReciveNewMsg) = False Then
                threadReciveNewMsg.Abort()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CtlOnlineTZBQ_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        CloseAll()
    End Sub
    Private Sub CtlOnlineTZBQ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        selectCtlLock = New Object
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        SelectPanelTabIndex(3)
        If IsNothing(RunReciveMsgCtlTSS) = False Then
            RunReciveMsgCtlTSS.CloseAll()
        End If
        If IsNothing(RunReciveMsgCtlTZBQ) = False Then
            RunReciveMsgCtlTZBQ.CloseAll()
        End If
        RunReciveMsgCtlTZBQ = Me
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
        While True
            Dim result As String = GetHttpMsg()
            If result = "" Then Continue While
            Console.WriteLine("recive new device msg")
            HandleHttpMsg(result)
        End While
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
    Private Sub HandleHttpMsg(ByVal HttpMsg As String)
        Try
            Dim isHandleOver As Boolean = False
            Try
                SyncLock selectCtlLock
                    Dim index As Integer = selectIndex

                    Select Case index
                        Case 0
                            If IsNothing(CtlFreqBscan) = False Then
                                Try
                                    If CtlFreqBscan.flagUIAready Then
                                        CtlFreqBscan.HandleHttpMsg(HttpMsg)
                                    End If
                                Catch ex As Exception

                                End Try
                            End If
                        Case 1
                            If IsNothing(CtlBuhuo) = False Then
                                Try
                                    If CtlBuhuo.flagUIAready Then
                                        CtlBuhuo.HandleHttpMsg(HttpMsg)
                                    End If
                                Catch ex As Exception

                                End Try
                            End If
                        Case 2
                            If IsNothing(CtlPointScan) = False Then
                                Try
                                    If CtlPointScan.flagUIAready Then
                                        CtlPointScan.HandleHttpMsg(HttpMsg)
                                    End If
                                Catch ex As Exception

                                End Try
                            End If
                        Case 3
                            If IsNothing(CtlJiandu) = False Then
                                Try
                                    If CtlJiandu.flagUIAready Then
                                        CtlJiandu.HandleHttpMsg(HttpMsg)
                                    End If
                                Catch ex As Exception

                                End Try
                            End If
                        Case 4
                            If IsNothing(CtlSetting) = False Then
                                Try
                                    If CtlSetting.flagUIAready Then
                                        CtlSetting.HandleHttpMsg(HttpMsg)
                                    End If
                                Catch ex As Exception

                                End Try
                            End If

                    End Select
                End SyncLock
            Catch ex As Exception

                Console.WriteLine(ex.Message)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Sub SendMsgToDev(ByVal msg As String)
        Dim th As New Thread(AddressOf th_SendMsgToDev)
        th.Start(msg)
    End Sub
    Private Sub th_SendMsgToDev(ByVal msg As String)
        Dim str As String = "?func=tzbqOrder&datamsg=" & msg & "&token=" & token
        Dim result As String = GetH(HttpMsgUrl, str)
        Dim r As String = GetNorResult("result", result)
        Dim msgt As String = GetNorResult("msg", result)
        Dim errmsg As String = GetNorResult("errmsg", result)
        Me.Invoke(Sub()
                      If r = "success" Then
                          Dim w As New WarnBox("命令下发成功！")
                          w.Show()
                      Else
                          If msgt = "Please login" Then
                              Login()
                              sendMsgToDev(msg)
                          Else
                              Dim sb As New StringBuilder
                              sb.AppendLine("命令下发失败")
                              sb.AppendLine(msgt)
                              sb.AppendLine(errmsg)
                              MsgBox(sb.ToString)
                          End If

                      End If
                  End Sub)
        Console.WriteLine(result)
    End Sub
    Private Sub CloseOldCtl()
        If selectIndex = 0 Then
            If IsNothing(CtlFreqBscan) = False Then
                Try
                    CtlFreqBscan.Dispose()
                    CtlFreqBscan = Nothing
                Catch ex As Exception

                End Try
            End If
        End If
        If selectIndex = 1 Then
            If IsNothing(CtlBuhuo) = False Then
                Try
                    CtlBuhuo.Dispose()
                    CtlBuhuo = Nothing
                Catch ex As Exception

                End Try
            End If
        End If
        If selectIndex = 2 Then
            If IsNothing(CtlPointScan) = False Then
                Try
                    CtlPointScan.Dispose()
                    CtlPointScan = Nothing
                Catch ex As Exception

                End Try
            End If
        End If

        If selectIndex = 3 Then
            If IsNothing(CtlJiandu) = False Then
                Try
                    CtlJiandu.Dispose()
                    CtlJiandu = Nothing
                Catch ex As Exception

                End Try
            End If
        End If

        If selectIndex = 4 Then
            If IsNothing(CtlSetting) = False Then
                Try
                    CtlSetting.Dispose()
                    CtlSetting = Nothing
                Catch ex As Exception

                End Try
            End If
        End If

        If selectIndex = 5 Then
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
            If index = 0 Then '频谱扫描          
                Panel5.Controls.Clear()
                Dim ctl As New PNTZBQFreqBscan(Me)
                Panel5.Controls.Add(ctl)
                CtlFreqBscan = ctl
            End If
            If index = 1 Then '条件捕获
                Dim ctl As New PNTZBQBuhuo(Me)
                Panel5.Controls.Add(ctl)
                CtlBuhuo = ctl
            End If
            If index = 2 Then '离散扫描
                Dim ctl As New PNTZBQPointScan(Me)
                Panel5.Controls.Add(ctl)
                CtlPointScan = ctl
            End If
            If index = 3 Then '监督评估
                Dim ctl As New PNTZBQJiandu(Me)
                Panel5.Controls.Add(ctl)
                CtlJiandu = ctl
            End If
            If index = 4 Then '设备设置
                Dim ctl As New PNTZBQSetting(Me)
                Panel5.Controls.Add(ctl)
                CtlSetting = ctl
            End If
            If index = 5 Then '设备日志
                Panel5.Controls.Clear()
                Dim ctl As New PNTDeviceLog(Me)
                Panel5.Controls.Add(ctl)
                CtlDeviceLog = ctl
            End If
        End SyncLock
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        SelectPanelTabIndex(0)
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        SelectPanelTabIndex(1)
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        SelectPanelTabIndex(2)
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        SelectPanelTabIndex(3)
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        SelectPanelTabIndex(4)
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        SelectPanelTabIndex(5)
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
  
End Class
