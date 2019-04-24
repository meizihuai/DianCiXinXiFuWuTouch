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
Public Class PNTZBQPointScan
    Public flagUIAready As Boolean = False
    Private CtlFather As CtlOnlineTZBQ
    Sub New(ByVal _CtlFather As CtlOnlineTZBQ)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        CtlFather = _CtlFather
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub

    Private Sub PNTZBQPointScan_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If IsNothing(DrawThread) = False Then
            Try
                DrawThread.Abort()
            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Sub PNTZBQPointScan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        flagUIAready = True
        Dim sb As New StringBuilder
        sb.AppendLine(433.65)
        sb.AppendLine(455.63)
        sb.AppendLine(449.35)
        sb.AppendLine(523.61)
        RTB1.Text = sb.ToString
        iniLV()
        inichart5()
    End Sub
    Private Sub inichart5()
        Chart5.Series.Clear()
        Chart5.ChartAreas(0).AxisY.Maximum = 100
        Chart5.ChartAreas(0).AxisY.Minimum = 0
        Chart5.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        Chart5.ChartAreas(0).AxisY.Interval = 20
        Chart5.ChartAreas(0).AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount
        Chart5.ChartAreas(0).AxisX.Minimum = Double.NaN
        Chart5.ChartAreas(0).AxisX.Maximum = Double.NaN
        'Chart5.ChartAreas(0).AxisX.IsStartedFromZero = True
        Chart5.ChartAreas(0).AxisX.Interval = 1
        Dim Series As New Series
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.FastLine
        Series.IsVisibleInLegend = False
        Series.Color = Color.Blue
        Series.Name = ""
        Chart5.Series.Add(Series) '0  频谱层
        Series = New Series("illegalsignal")
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.Column
        Series.IsVisibleInLegend = False
        Series.Color = Color.Red
        Series.Name = ""
        Chart5.Series.Add(Series) '1  信号层
        Series = New Series("zft")
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.Column
        Series.IsVisibleInLegend = False
        Series.LabelToolTip = "#VAL" & " %"
        Series.ToolTip = "#VAL" & " %"
        Series.Label = "#VAL" & " %"
        Series.Color = Color.Blue
        Series.Name = ""
        Chart5.Series.Add(Series) '2  直方图层

    End Sub
    Private Sub iniLV()
        LV20.Clear()
        LV20.View = View.Details
        LV20.GridLines = False
        LV20.FullRowSelect = True
        LV20.Columns.Add("信号频率(MHz)", 100)
        LV20.Columns.Add("实时电平(dBm)", 100)
        LV20.Columns.Add("属性识别", 70)
        LV20.Columns.Add("状态评估", 70)
        LV20.Columns.Add("可用评估", 70)
        LV20.Columns.Add("占用度", 60)
        LV20.Columns.Add("起始时间", 100)
        LV20.Columns.Add("更新时间", 100)
        LV20.Columns.Add("监测时长", 100)
        LV20.Columns.Add("最大电平(dBm)", 80)
        LV20.Columns.Add("平均电平(dBm)", 80)
        LV20.Columns.Add("最小电平(dBm)", 80)
        LV20.Columns.Add("监测次数", 80)
        LV20.Columns.Add("超标次数", 80)
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
                        ctlfather.Invoke(Sub() handleBQ(bq))
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
                ctlfather.Invoke(Sub() MsgBox("时间同步成功！"))
            End If
            If k = "JCPD" Then
                Console.WriteLine(BQ)
                'ctlfather.Invoke(Sub() MsgBox(BQ))
                'ctlfather.Invoke(Sub() MsgBox("成功下发频点检测命令！"))
            End If
            If k = "JCMB" Then
                ctlfather.Invoke(Sub() MsgBox("成功下发模板命令"))
            End If
            If k = "SGPS" Then
                ctlfather.Invoke(Sub() MsgBox("GPS设置成功"))
            End If
            If k = "SZID" Then
                ctlfather.Invoke(Sub() MsgBox("ID设置成功"))
            End If
        End If
        If func = "SSSJ" Then
            CtlFather.Invoke(Sub() HandleSSSJ(BQ))
        End If
    End Sub
    Dim sssjStartTime As String
    Dim LV20Lock As New Object
    Dim LV26Lock As New Object
    Private Sub HandleSSSJ(ByVal bq As String)
        If sssjStartTime = "" Then
            sssjStartTime = Now.ToString("yyyy-MM-dd HH:mm:ss")
        End If
        Dim strtmp As String = Now.ToString("HH:mm:ss")
        Dim str As String = bq.Substring(InStr(bq, "<"), InStr(bq, ">") - InStr(bq, "<") - 1)
        Dim st() As String = str.Split(",")
        Dim numOfValue As Integer = st(2)
        Dim SigNalList As New List(Of Double)
        Dim CqList As New List(Of Double)
        If st.Length < 2 + numOfValue + 2 Then Return
        If st.Length > numOfValue * 2 Then
            For i = 3 To 2 + numOfValue
                Dim pd As Double = st(i + numOfValue)
                Dim cq As Double = st(i)
                SigNalList.Add(pd)
                CqList.Add(cq)
            Next
        End If       
        Dim count As Integer = SigNalList.Count
        CtlFather.Invoke(Sub()
                             Try
                                 For i = 0 To count - 1
                                     Dim pd As Double = SigNalList(i)
                                     Dim cq As Double = CqList(i)
                                     SyncLock LV26Lock
                                         handleLv26(pd, cq, SigNalList)
                                     End SyncLock
                                     SyncLock LV20Lock
                                         handleLV20(pd, cq, SigNalList)
                                     End SyncLock
                                 Next
                                 Dim Series As New Series("zft")
                                 Series.Label = ""
                                 Series.XValueType = ChartValueType.String
                                 Series.ChartType = SeriesChartType.Column
                                 Series.IsVisibleInLegend = False
                                 Series.LabelToolTip = "#VAL" & " %"
                                 Series.ToolTip = "#VAL" & " %"
                                 Series.Label = "#VAL" & " %"
                                 Series.Color = Color.Blue
                                 Series("PointWidth") = 0.2
                                 Series.Name = ""
                                 Dim list As New List(Of zydStu)
                                 If IsNothing(LV26) = False Then
                                     For Each itm As ListViewItem In LV26
                                         Dim pl As Double = Val(itm.SubItems(0).Text)
                                         Dim plString As String = itm.SubItems(0).Text.ToString
                                         Dim zyd As Double = Val(itm.SubItems(5).Text.Replace("%", ""))
                                         list.Add(New zydStu(pl, zyd))
                                     Next
                                     For i = 0 To list.Count - 1
                                         For j = i + 1 To list.Count - 1
                                             Dim aItm As zydStu = list(i)
                                             Dim bItm As zydStu = list(j)
                                             If aItm.pl > bItm.pl Then
                                                 list(j) = aItm
                                                 list(i) = bItm
                                             End If
                                         Next
                                     Next
                                     For Each itm In list
                                         Series.Points.AddXY(itm.pl.ToString("0.00"), itm.zyd)
                                     Next
                                     Chart5.Series(2) = Series
                                 End If
                             Catch ex As Exception
                                 Console.WriteLine(ex.Message)
                                 Console.WriteLine("111" & ex.Message)
                             End Try
                         End Sub)
    End Sub
    Structure zydStu
        Dim pl As Double
        Dim zyd As Double
        Sub New(ByVal _pl As Double, ByVal _zyd As Double)
            pl = _pl
            zyd = _zyd
        End Sub
    End Structure
    Dim LV26 As List(Of ListViewItem)
    Private Sub handleLv26(ByVal pd As Double, ByVal cq As Double, ByVal SigNalList As List(Of Double))
        Try
            Dim columnCount As Integer = 14
            If IsNothing(LV26) Then LV26 = New List(Of ListViewItem)
            If IsNothing(SigNalList) Then Exit Sub
            If LV26.Count <> SigNalList.Count Then
                LV26.Clear()
                For Each it In SigNalList
                    Dim itm As New ListViewItem(it)
                    For i = 1 To columnCount - 1
                        itm.SubItems.Add("--")
                    Next
                    itm.SubItems(itm.SubItems.Count - 1).Text = 0
                    itm.SubItems(itm.SubItems.Count - 2).Text = 0
                    LV26.Add(itm)
                Next
            End If
            For Each itm As ListViewItem In LV26
                If Val(itm.Text) = pd Then
                    itm.SubItems(1).Text = cq.ToString(".0")
                    If itm.SubItems(9).Text = "--" Then itm.SubItems(9).Text = 0
                    Dim k As Double = Val(itm.SubItems(9).Text)
                    If k = 0 Then k = -110
                    If k < cq Then
                        itm.SubItems(9).Text = cq.ToString(".0")
                    End If
                    If itm.SubItems(11).Text = "--" Then itm.SubItems(11).Text = 0
                    Dim min As Double = Val(itm.SubItems(11).Text)
                    If min = 0 Then min = -1
                    If min > cq Then
                        itm.SubItems(11).Text = cq.ToString(".0")
                    End If

                    If itm.SubItems(10).Text = "--" Then itm.SubItems(10).Text = 0
                    Dim avg As Double = 0
                    If itm.SubItems(10).Text = 0 Then
                        avg = cq.ToString(".0")
                    Else
                        If avg = 0 Then avg = cq
                        avg = ((avg + cq) / 2)
                    End If
                    itm.SubItems(10).Text = avg.ToString(".0")
                    Dim statu As String = "正常"
                    itm.ForeColor = Color.Black
                    If cq >= -60 Then
                        statu = "超标"
                        itm.ForeColor = Color.Red
                    End If
                    itm.SubItems(2).Text = "不明信号"
                    itm.SubItems(3).Text = statu
                    itm.SubItems(4).Text = "可用"
                    itm.SubItems(5).Text = "10%"
                    itm.SubItems(6).Text = sssjStartTime
                    itm.SubItems(7).Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
                    itm.SubItems(12).Text = Val(itm.SubItems(12).Text) + 1
                    If cq >= -86 Then
                        itm.SubItems(13).Text = Val(itm.SubItems(13).Text) + 1
                    End If
                    itm.SubItems(5).Text = ((Val(itm.SubItems(13).Text) / Val(itm.SubItems(12).Text)) * 100).ToString("0.00") & "%"
                    Dim timespan As TimeSpan = Now.Subtract(sssjStartTime)
                    itm.SubItems(8).Text = timespan.Hours.ToString("00") & ":" & timespan.Minutes.ToString("00") & ":" & timespan.Seconds.ToString("00")
                End If
            Next
        Catch ex As Exception
            '  MsgBox(ex.ToString)
        End Try

    End Sub
    Private Sub handleLV20(ByVal pd As Double, ByVal cq As Double, ByVal SigNalList As List(Of Double))
        Try
            If IsNothing(SigNalList) Then Exit Sub
            If LV20.Items.Count <> SigNalList.Count Then
                LV20.Items.Clear()
                For Each it In SigNalList
                    Dim itm As New ListViewItem(it)
                    For i = 1 To LV20.Columns.Count - 1
                        itm.SubItems.Add("--")
                    Next
                    itm.SubItems(itm.SubItems.Count - 1).Text = 0
                    itm.SubItems(itm.SubItems.Count - 2).Text = 0
                    LV20.Items.Add(itm)
                Next
            End If
            For Each itm As ListViewItem In LV20.Items
                If Val(itm.Text) = pd Then
                    itm.SubItems(1).Text = cq.ToString(".0")
                    If itm.SubItems(9).Text = "--" Then itm.SubItems(9).Text = 0
                    Dim k As Double = Val(itm.SubItems(9).Text)
                    If k = 0 Then k = -110
                    If k < cq Then
                        itm.SubItems(9).Text = cq.ToString(".0")
                    End If
                    If itm.SubItems(11).Text = "--" Then itm.SubItems(11).Text = 0
                    Dim min As Double = Val(itm.SubItems(11).Text)
                    If min = 0 Then min = -1
                    If min > cq Then
                        itm.SubItems(11).Text = cq.ToString(".0")
                    End If

                    If itm.SubItems(10).Text = "--" Then itm.SubItems(10).Text = 0
                    Dim avg As Double = 0
                    If itm.SubItems(10).Text = 0 Then
                        avg = cq.ToString(".0")
                    Else
                        If avg = 0 Then avg = cq
                        avg = ((avg + cq) / 2)
                    End If
                    itm.SubItems(10).Text = avg.ToString(".0")
                    Dim statu As String = "正常"
                    itm.ForeColor = Color.Black
                    If cq >= -60 Then
                        statu = "超标"
                        itm.ForeColor = Color.Red
                    End If
                    itm.SubItems(2).Text = "不明信号"
                    itm.SubItems(3).Text = statu
                    itm.SubItems(4).Text = "可用"
                    itm.SubItems(5).Text = "10%"
                    itm.SubItems(6).Text = sssjStartTime
                    itm.SubItems(7).Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
                    itm.SubItems(12).Text = Val(itm.SubItems(12).Text) + 1
                    If cq >= -86 Then
                        itm.SubItems(13).Text = Val(itm.SubItems(13).Text) + 1
                    End If
                    itm.SubItems(5).Text = ((Val(itm.SubItems(13).Text) / Val(itm.SubItems(12).Text)) * 100).ToString("0.00") & "%"
                    Dim timespan As TimeSpan = Now.Subtract(sssjStartTime)
                    itm.SubItems(8).Text = timespan.Hours.ToString("00") & ":" & timespan.Minutes.ToString("00") & ":" & timespan.Seconds.ToString("00")
                End If
            Next
        Catch ex As Exception
            '  MsgBox(ex.ToString)
        End Try

    End Sub
    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        StartPointWork(0)
    End Sub

    Private Sub StartPointWork(ByVal clickCount As Integer)
        Dim id As String = CtlFather.myDeviceInfo.DeviceID
        Dim code As String = RTB1.Text
        If code = "" Then
            MsgBox("请输入频点值，每行代表一个频点")
            Return
        End If
        Dim pds() As String = code.Split(Chr(10))
        Dim pdList As New List(Of Double)
        For Each itm In pds
            If itm = "" Then Continue For
            itm = itm.Replace(Chr(10), "").Replace(Chr(13), "")
            If IsNumeric(itm) Then
                pdList.Add(Val(itm))
            End If
        Next
        If pdList.Count = 0 Then
            MsgBox("请输入有效的频点")
            Return
        End If
        Dim jcpd As String = "<TZBQ:JCPD," & id & "," & 10 & "," & "Y" & "," & 15 & "," & 15 & "," & 10 & "," & 1 & "," & 1 & "," & pdList.Count
        For i = 0 To pdList.Count - 1
            jcpd = jcpd & "," & pdList(i)
        Next
        jcpd = jcpd & ">"
        Dim str As String = "?func=tzbqOrder&datamsg=" & jcpd & "&token=" & token
        Dim HttpMsgUrl As String = CtlFather.myDeviceInfo.HTTPMsgUrl.Replace("123.207.31.37", ServerIP)
        HttpMsgUrl = HttpMsgUrl.Replace("+", ServerIP)
        Dim result As String = GetH(HttpMsgUrl, str)
        Dim r As String = GetNorResult("result", result)
        Dim msgt As String = GetNorResult("msg", result)
        Dim errmsg As String = GetNorResult("errmsg", result)
        If r = "success" Then
            Sleep(800)
            SendMsgToDev("<TZBQ:SSSJ," & id & ",1>")
        Else
            If msgt = "Please login" Then
                Login()
                If clickCount = 0 Then
                    StartPointWork(1)
                Else
                    MsgBox("登录信息失效，请关闭软件重新登录")
                    Return
                End If
            Else
                Dim sb As New StringBuilder
                sb.AppendLine("命令下发失败")
                sb.AppendLine(result)
                MsgBox(sb.ToString)
            End If

        End If
    End Sub
    Private Sub Panel17_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel17.Click
        StartPointWork(0)
    End Sub
    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        stopDevice()
    End Sub
    Private Sub Panel19_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel19.Click
        stopDevice()
    End Sub
    Private Sub SendMsgToDev(ByVal msg As String)
        CtlFather.SendMsgToDev(msg)
    End Sub
    Private Sub stopDevice()
        Dim id As String = CtlFather.myDeviceInfo.DeviceID
        sendMsgToDev("<TZBQ:STOP," & id & ">")
    End Sub

    Private Sub Panel17_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel17.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel19_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel19.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel23_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel23.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel25_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel25.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub
End Class
