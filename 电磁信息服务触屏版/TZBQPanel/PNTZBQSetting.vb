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
Public Class PNTZBQSetting
    Private selectDeviceID As String
    Public flagUIAready As Boolean
    Private CtlFather As CtlOnlineTZBQ
    Private HttpMsgUrl As String
    Sub New(ByVal _CtlFather As CtlOnlineTZBQ)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        CtlFather = _CtlFather
        selectDeviceID = CtlFather.myDeviceInfo.DeviceID
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
    Private Sub PNTZBQSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        GetHTTPMsgUrl()
        flagUIAready = True
        TextBox4.Text = CtlFather.myDeviceInfo.Name
        TextBox8.Text = CtlFather.myDeviceInfo.DeviceID
        TextBox6.Text = CtlFather.myDeviceInfo.Lng
        TextBox7.Text = CtlFather.myDeviceInfo.Lat
        TextBox9.Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Sub
    Private Sub GetHTTPMsgUrl()
        HttpMsgUrl = GetAutoH("func=GetHttpMsgUrlById&deviceID=" & CtlFather.myDeviceInfo.DeviceID)
        HttpMsgUrl = HttpMsgUrl.Replace("123.207.31.37", ServerIP)
    End Sub
    Dim DrawThread As Thread
    Dim DisPlayLock As Object
    Public Sub HandleHttpMsg(ByVal HttpMsg As String)

        Dim TZBQList As New List(Of String)
        Try
            Dim p As JArray = JArray.Parse(HttpMsg)
            Console.WriteLine(p.Count)
            For Each itm As JValue In p
                Dim jMsg As String = itm.Value
                If jMsg <> "" Then
                    TZBQList.Add(jMsg)
                End If
            Next
        Catch ex As Exception

            Exit Sub
        End Try
        If TZBQList.Count <= 0 Then Exit Sub
        If IsNothing(DrawThread) = False Then
            Try
                DrawThread.Abort()
            Catch ex As Exception

            End Try
        End If
        DrawThread = New Thread(AddressOf handleTZBQList)
        DrawThread.Start(TZBQList)
    End Sub
    Private Sub handleTZBQList(ByVal bqlist As List(Of String))
        If IsNothing(DisPlayLock) Then DisPlayLock = New Object
        Dim maxCount As Integer = bqlist.Count
        Dim count As Integer = bqlist.Count
        If count > maxCount Then count = maxCount
        Dim sleepCount As Double = (GetHttpMsgTimeSpan * 1000 - 100) / (count + 1)
        SyncLock DisPlayLock
            For i = 0 To count - 1
                Try
                    If i < maxCount Then
                        Dim bq As String = bqlist(i)
                        CtlFather.Invoke(Sub() handleBQ(bq))
                    End If
                Catch ex As Exception

                End Try
                If i < count - 1 Then
                    Sleep(sleepCount)
                End If
            Next
        End SyncLock
    End Sub
    Private Sub handleBQ(ByVal BQ As String)
        If InStr(BQ, "<TZBQ:") Then  Else Exit Sub
        Dim a As Integer = InStr(BQ, "<")
        Dim b As Integer = InStr(BQ, Chr(13))
        BQ = Mid(BQ, a, b - 1 + 1)
        Dim func As String = getFuncByBQ(BQ)
        Dim id As String = getIDbyBQ(BQ)
        'If id <> selectDeviceID Then Exit Sub
        If InStr(func, "ECHO_") Then
            Dim k As String = func.Split("_")(1)
            If k = "TIME" Then
                CtlFather.Invoke(Sub() MsgBox("时间同步成功！"))
            End If
            If k = "JCPD" Then
                Console.WriteLine(BQ)
                'ctlfather.Invoke(Sub() MsgBox(BQ))
                'ctlfather.Invoke(Sub() MsgBox("成功下发频点检测命令！"))
            End If
            If k = "JCMB" Then
                CtlFather.Invoke(Sub() MsgBox("成功下发模板命令"))
            End If
            If k = "SGPS" Then
                CtlFather.Invoke(Sub() MsgBox("GPS设置成功"))
            End If
            If k = "SZID" Then
                CtlFather.Invoke(Sub() MsgBox("ID设置成功"))
            End If
        End If
    End Sub
    Private Sub SendMsgToDev(ByVal msg As String)
        CtlFather.SendMsgToDev(msg)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim id As String = selectDeviceID
        TextBox9.Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim msg As String = "<TZBQ:TIME," & id & "," & TextBox9.Text & ">"
        SendMsgToDev(msg)
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim id As String = selectDeviceID
        Dim msg As String = "<TZBQ:TIME," & id & "," & TextBox9.Text & ">"
        SendMsgToDev(msg)
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        Dim id As String = selectDeviceID
        Dim msg As String = "<TZBQ:SZID," & id & "," & TextBox8.Text & ">"
        SendMsgToDev(msg)
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim lng As String = TextBox6.Text
        Dim lat As String = TextBox7.Text
        Dim msg As String = "<TZBQ:SGPS," & selectDeviceID & "," & lng & "," & lat & ">"
        SendMsgToDev(msg)
        If Val(lng) < 100 Or Val(lat) < 5 Then MsgBox("请填写正确经纬度") : Return
        Dim str As String = String.Format("?func=SetDeviceLngAndLat&lng={0}&lat={1}&token=" & token, lng, lat)
        Dim result As String = GetH(HttpMsgUrl, str)
        Dim r As String = GetNorResult("result", result)
        msg = GetNorResult("msg", result)
        If r = "success" Then
            Dim w As New WarnBox("命令下发成功！")
            w.Show()
        Else
            MsgBox(msg)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim nickid As String = TextBox4.Text
        If nickid = "" Then
            MsgBox("备注不能为空")
            Return
        End If
        Dim str As String = "func=SetDeviceNickID&nickid=" & nickid
        Dim result As String = GethWithToken(HttpMsgUrl, str)
        If result = "" Then
            result = "设置备注成功！"
        End If
        MsgBox(result)
    End Sub
End Class
