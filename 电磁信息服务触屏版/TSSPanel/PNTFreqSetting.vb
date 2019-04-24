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
Public Class PNTFreqSetting
    Public flagUIAready As Boolean = False
    Private HttpMsgUrl As String
    Private CtlFather As CtlOnlineTSS
    Sub New(ByVal _CtlFather As CtlOnlineTSS)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        CtlFather = _CtlFather
        ' 在 InitializeComponent() 调用之后添加任何初始化。
    End Sub
    Private Sub PNTFreqSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        GetHTTPMsgUrl()
        TextBox4.Text = CtlFather.myDeviceInfo.Name
        TextBox6.Text = CtlFather.myDeviceInfo.Lng
        TextBox7.Text = CtlFather.myDeviceInfo.Lat
        Dim th As New Thread(AddressOf GetControlerVersion)
        th.Start()
        flagUIAready = True
    End Sub
    Private Sub GetHTTPMsgUrl()
        HttpMsgUrl = GetAutoH("func=GetHttpMsgUrlById&deviceID=" & CtlFather.myDeviceInfo.DeviceID)
        HttpMsgUrl = HttpMsgUrl.Replace("123.207.31.37", ServerIP)
    End Sub
    Private Function data2img(ByVal by() As Byte) As Bitmap
        Try

            Dim ms As New MemoryStream(by)
            Dim bitmap As New Bitmap(ms)
            Return bitmap
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Sub HandleHttpMsg(ByVal HttpMsg As String)
        Console.WriteLine("收到新消息TSS  " & Now.ToString("HH:mm:ss"))
        If Not Me.flagUIAready Then
            Console.WriteLine("flagUIAready=" & flagUIAready & ",不处理本次数据")
            Return
        End If
        Try
            Dim p As JArray = JArray.Parse(HttpMsg)
            For Each itm As JValue In p
                Dim jMsg As String = itm.Value
                Dim JObj As Object = JObject.Parse(jMsg)
                Dim func As String = JObj("func").ToString
                Console.WriteLine("func= " & func)
                If func = "ScreenImage" Then
                    Dim msg As String = JObj("msg").ToString
                    Try
                        Dim buffer() As Byte = Convert.FromBase64String(msg)
                        If IsNothing(buffer) = False Then
                            Dim decombuffer() As Byte = Decompress(buffer)
                            If IsNothing(decombuffer) = False Then
                                Dim bmp As Bitmap = Me.data2img(decombuffer)
                                If IsNothing(bmp) = False Then
                                    Label41.Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
                                    PBXControlImagr.Image = bmp
                                End If
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Next
        Catch ex As Exception
            Exit Sub
        End Try
        
    End Sub
   
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim nickid As String = TextBox4.Text
        If nickid = "" Then
            MsgBox("备注不能为空")
            Return
        End If
        Dim str As String = "?func=SetDeviceNickID&nickid=" & nickid & "&token=" & token
        Dim result As String = GetH(HttpMsgUrl, str)
        If result = "" Then
            result = "设置备注成功！"
        End If
        MsgBox(result)
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim lng As String = TextBox6.Text
        Dim lat As String = TextBox7.Text
        If Val(lng) < 100 Or Val(lat) < 5 Then MsgBox("请填写正确经纬度") : Return
        Dim str As String = String.Format("?func=SetDeviceLngAndLat&lng={0}&lat={1}&token=" & token, lng, lat)
        Dim result As String = GetH(HttpMsgUrl, str)
        Dim r As String = GetNorResult("result", result)
        Dim msg As String = GetNorResult("msg", result)
        If r = "success" Then
            Dim w As New WarnBox("命令下发成功！")
            w.Show()
        Else
            MsgBox(msg)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim result As String = GethWithToken(HttpMsgUrl, "func=netswitchin")
        If GetNorResult("result", result) = "success" Then
            Dim w As New WarnBox("命令下发成功！")
            w.Show()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim result As String = GethWithToken(HttpMsgUrl, "func=netswitchout")
        If GetNorResult("result", result) = "success" Then
            Dim w As New WarnBox("命令下发成功！")
            w.Show()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim dataType As String = "task"
        Dim funcType As String = "getheartbeat"
        Dim paraMsg As String = ""
        Dim str As String = "?func=tssOrderByCode&dataType=" & dataType & "&funcType=" & funcType & "&paraMsg=" & paraMsg & "&token=" & token
        Dim result As String = GetH(HttpMsgUrl, str)
        Dim w As New WarnBox("命令下发成功！")
        w.Show()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim th As New Thread(AddressOf GetControlerVersion)
        th.Start()
    End Sub
    Private Sub GetControlerVersion()
        Me.Invoke(Sub() Label43.Text = "正在查询……")
        Dim result As String = GethWithToken(HttpMsgUrl, "func=GetControlerVersion")
        Me.Invoke(Sub() Label43.Text = result)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim result As String = GethWithToken(HttpMsgUrl, "func=GetScreenImage")
        If GetNorResult("result", result) = "success" Then
            Dim w As New WarnBox("命令下发成功！")
            w.Show()
        Else
            MsgBox("命令下发失败!" & GetNorResult("msg", result))
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            Timer1.Start()
        Else
            Timer1.Stop()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            GethWithToken(HttpMsgUrl, "func=GetScreenImage")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim dataType As String = "task"
        Dim funcType As String = "reboot"
        Dim paraMsg As String = ""
        Dim str As String = "?func=tssOrderByCode&dataType=" & dataType & "&funcType=" & funcType & "&paraMsg=" & paraMsg & "&token=" & token
        Dim result As String = GetH(HttpMsgUrl, str)
        Dim w As New WarnBox("命令下发成功！")
        w.Show()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim r = MsgBox("是否需要重启工控机？", MsgBoxStyle.YesNo, "提示")
        If r = MsgBoxResult.No Then Return
        Dim result As String = GethWithToken(HttpMsgUrl, "func=ReStartWindows")
        If GetNorResult("result", result) = "success" Then
            Dim w As New WarnBox("命令下发成功！")
            w.Show()
        Else
            MsgBox("命令下发失败!" & GetNorResult("msg", result))
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim r = MsgBox("是否需要关闭工控机？关闭可能无法远程启动！", MsgBoxStyle.YesNo, "提示")
        If r = MsgBoxResult.No Then Return
        Dim result As String = GethWithToken(HttpMsgUrl, "func=ShutdownWindows")
        If GetNorResult("result", result) = "success" Then
            Dim w As New WarnBox("命令下发成功！")
            w.Show()
        Else
            MsgBox("命令下发失败!" & GetNorResult("msg", result))
        End If
    End Sub

   
    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        Dim str As String = TextBox6.Text
        If InStr(str, ",") Then
            Dim st() As String = str.Split(",")
            TextBox6.Text = st(0)
            TextBox7.Text = st(1)
        End If
    End Sub
End Class
