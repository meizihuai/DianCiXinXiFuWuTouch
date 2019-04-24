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
Imports SpeechLib
Public Class PNTZBQBuhuo
    Public flagUIAready As Boolean = False
    Private DeviceAddress As String
    Private CtlFather As CtlOnlineTZBQ
    Sub New(ByVal _CtlFather As CtlOnlineTZBQ)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        CtlFather = _CtlFather
        DeviceAddress = CtlFather.myDeviceInfo.Address
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
    Private Sub PNTZBQBuhuo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        iniLV()
        iniChart()
        ' iniColors()
        DeviceAddress = CtlFather.myDeviceInfo.Address
        flagUIAready = True
    End Sub
    Private Sub iniLv()
        LV22.Clear()
        LV22.View = View.Details
        LV22.GridLines = False
        LV22.FullRowSelect = True
        LV22.Columns.Add("序号", 50)
        LV22.Columns.Add("时间", 150)
        LV22.Columns.Add("频率(MHz)", 100)
        LV22.Columns.Add("地点", 100)
        LV22.Columns.Add("信号电平", 100)
        LV22.Columns.Add("最小值")
        LV22.Columns.Add("最大值")
    End Sub
    Private Sub iniChart()
        Chart3.Series.Clear()
        Chart3.ChartAreas(0).AxisY.Maximum = 40
        Chart3.ChartAreas(0).AxisY.Minimum = -120
        Chart3.ChartAreas(0).AxisY.Interval = 20
        Chart3.ChartAreas(0).AxisY.IntervalOffset = 20
        Chart3.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        Dim Series As New Series '频谱   0
        Series.Label = "频谱数据"
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.FastLine
        Series.IsVisibleInLegend = False
        'Series.ToolTip = "频率：#VALX\\n场强：#VAL"
        'Series.LabelToolTip = "频率：#VALX\\n场强：#VAL"
        Chart3.Series.Add(Series)
        Dim ser As New Series("illegalsignal") '信号   1
        ser.ChartType = SeriesChartType.Column
        ser.Color = Color.Red
        ser("PointWidth") = 0.25
        ser.IsVisibleInLegend = False
        'ser.ToolTip = "频率：#VALX\\n场强：#VAL"
        'ser.LabelToolTip = "频率：#VALX\\n场强：#VAL"
        Chart3.Series.Add(ser)
        Dim Series2 As New Series '最大值   2
        Series2.Label = "最大值"
        Series2.XValueType = ChartValueType.Auto
        Series2.ChartType = SeriesChartType.FastLine
        Series2.IsVisibleInLegend = False
        Chart3.Series.Add(Series2)
        Dim Series3 As New Series '状态跟踪   3
        Series3.Label = "状态跟踪"
        Series3.XValueType = ChartValueType.Auto
        Series3.ChartType = SeriesChartType.StepLine
        Series3.IsVisibleInLegend = False
        Chart3.Series.Add(Series3)
        Dim series4 As New Series '状态跟踪   3
        series4.Label = "状态跟踪"
        series4.XValueType = ChartValueType.Auto
        series4.ChartType = SeriesChartType.StepLine
        series4.IsVisibleInLegend = False
        Chart3.Series.Add(series4)
    End Sub
    Private Sub iniColors()
        Chart3.BackColor = Color.Transparent
        Chart3.ChartAreas(0).BackColor = Color.Transparent
        Chart3.ChartAreas(0).AxisX.LabelStyle.ForeColor = Color.LightGray
        Chart3.ChartAreas(0).AxisY.LabelStyle.ForeColor = Color.LightGray
        Chart3.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisX.MinorGrid.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisY.MinorGrid.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisX.MinorTickMark.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisY.MinorTickMark.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisX.MajorTickMark.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisY.MajorTickMark.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisX.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisY.LineColor = Color.LightGray
        Chart3.ChartAreas(0).AxisX.TitleForeColor = Color.LightGray
        Chart3.ChartAreas(0).AxisY.TitleForeColor = Color.LightGray
        Chart3.ForeColor = Color.LightGray
        Chart3.BorderlineColor = Color.LightGray
    End Sub
    Dim Thread_PPSJ As Thread
    Structure runLocation
        Dim lng As String
        Dim lat As String
        Dim time As String
    End Structure
    Structure json_PPSJ
        Dim freqStart As Double
        Dim freqStep As Double
        Dim freqEnd As Double
        Dim deviceID As String
        Dim dataCount As Integer
        Dim runLocation As runLocation
        Dim value() As Double
        Dim isDSGFreq As Boolean
        Dim DSGFreqBase64 As String
    End Structure
    Structure JSON_Msg
        Dim func As String
        Dim msg As String
        Sub New(ByVal _func As String, ByVal _Msg As String)
            func = _func
            msg = _Msg
        End Sub
    End Structure
    Dim DrawThread As Thread
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
        'handleTZBQList(TZBQList)
        If IsNothing(DrawThread) = False Then
            Try
                DrawThread.Abort()
            Catch ex As Exception

            End Try
        End If
        DrawThread = New Thread(AddressOf handleTZBQList)
        DrawThread.Start(TZBQList)
    End Sub
    Dim DisPlayLock As Object
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
        If func = "CXNEW" Then
            Dim str As String = "<TZBQ:SBZT,0>"
            sendMsgToDev("<TZBQ:SBZT,0>")
        End If
        If func = "JCMB" Then
            'Label16.Text = "模板建立完成!"
            'ctlfather.Invoke(Sub() MsgBox("模板建立完成！"))
            Dim str As String = BQ.Substring(InStr(BQ, "<"), InStr(BQ, ">") - InStr(BQ, "<") - 1)
            Dim st() As String = str.Split(",")
            Dim numofSinle As Integer = st(2)
            Dim resultMsg As New StringBuilder
            For i = 3 To st.Length - 1 Step 2
                Dim pl As String = st(i)
                Dim cq As String = st(i + 1)
                resultMsg.AppendLine(pl & "MHz," & cq & "dBm")
            Next
            MsgBox(resultMsg.ToString, MsgBoxStyle.OkOnly, "设备模板")
        End If
        If func = "JCPD" Then
            'MsgBox(BQ)
            '<TZBQ:JCPD,3,300,3,-79,-79,-79>
        End If
        If func = "PPSJ" Then
            Dim st() As String = BQ.Split(",")
            Dim startPQ As Double = st(2)
            Dim endPD As Double = st(3)
            Dim bujin As Double = st(4)
            Dim leixing As String = st(5)
            If leixing = "R" Then leixing = "实时"
            If leixing = "M" Then leixing = "模板"
            If leixing = "W" Then leixing = "报警"
            Dim geshu As Integer = st(6).Split(">")(0)
            Dim x(geshu - 1) As Double
            Dim y(geshu - 1) As Double
            For i = 0 To geshu - 1
                x(i) = startPQ + i * bujin
            Next
            Dim BCDstr As String = BQ.Split("]")(1)
            For i = 0 To geshu - 1
                Dim bcd As String = BCDstr.Substring(i * 2, 2)
                y(i) = BCD2Int(bcd)
            Next
            Dim js As json_PPSJ
            js.dataCount = x.Count
            js.deviceID = CtlFather.myDeviceInfo.DeviceID
            js.freqStart = x(0)
            js.freqStep = bujin
            js.value = y
            js.isDSGFreq = False
            ctlfather.Invoke(Sub() HandlePPSJList(js))
        End If
        If func = "SSSJ" Then
            '  ctlfather.Invoke(Sub() HandleSSSJ(BQ))
        End If
    End Sub

    Private Sub SendMsgToDev(ByVal msg As String)
        CtlFather.SendMsgToDev(msg)
    End Sub
   Private Sub HandlePPSJList(ByVal itm As json_PPSJ)
        Try
            ctlfather.Invoke(Sub() handlePinPuFenXi(itm))
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try
    End Sub
    Dim recentFreqStart As Double
    Dim recentFreqEnd As Double
    Private Sub handlePinPuFenXi(ByVal p As json_PPSJ)
        Try
            Dim jstmp As String = JsonConvert.SerializeObject(p)
            Dim jsLen As Long = jstmp.Length
            Dim jslenstr As String = GetLen(jsLen)
            Dim freqStart As Double = p.freqStart
            Dim freqStep As Double = p.freqStep
            Dim yy() As Double = p.value
            Dim isDSGFreq As Boolean = False
            If p.isDSGFreq Then
                isDSGFreq = True
                yy = DSGBase2PPSJValues(p.DSGFreqBase64)
            End If
            If IsNothing(yy) Then
                Exit Sub
            End If
            Dim dataCount As Integer = yy.Count
            Dim deviceID As String = p.deviceID
            Dim maxCount As Integer = 5000
            Dim xx() As Double
            If dataCount <= maxCount Then
                ReDim xx(dataCount - 1)
                For i = 0 To yy.Count - 1
                    xx(i) = freqStart + i * freqStep
                Next
            Else
                Dim realValue() As Double = yy
                Dim xlist As New List(Of Double)
                Dim ylist As New List(Of Double)
                Dim st As Integer = Math.Ceiling(dataCount / maxCount)
                For i = 0 To dataCount - 1 Step st
                    xlist.Add(freqStart + i * freqStep)
                    ylist.Add(realValue(i))
                Next
                If xlist(xlist.Count - 1) <> freqStart + (dataCount - 1) * freqStep Then
                    xlist.Add(freqStart + (dataCount - 1) * freqStep)
                    ylist.Add(yy(yy.Length - 1))
                End If
                xx = xlist.ToArray
                yy = ylist.ToArray
            End If
            Dim jieshu As Double = freqStart + (dataCount - 1) * freqStep
            p.freqEnd = jieshu
            xx(xx.Length - 1) = p.freqEnd
            If freqStart <> recentFreqStart Or jieshu <> recentFreqEnd Then
                recentFreqStart = freqStart
                recentFreqEnd = jieshu
            End If
            Chart3.ChartAreas(0).AxisX.Minimum = freqStart
            Chart3.ChartAreas(0).AxisX.Maximum = jieshu
            Chart3.ChartAreas(0).AxisX.Interval = (jieshu - freqStart) / 5

            Try
                Dim du As Integer = AutoFenXiDu
                HandleMuBan(xx, yy)
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub
    Structure MuBanPinPu
        Dim time As String
        Dim XX() As Double
        Dim yy() As Double
    End Structure
    Dim isShengChengMuBan As Boolean = False
    Dim muban As MuBanPinPu
    Dim ShengChengMuBanEndTime As Date = "1999-01-01 00:00:00"
    Dim signalStartTime As Date = Nothing
    Dim sigNalCount As Integer = 0
    Private Sub HandleMuBan(ByVal xx() As Double, ByVal yy() As Double)
        If isShengChengMuBan = False Then
            If IsNothing(muban) = False Then
                If IsNothing(muban.XX) = False And IsNothing(muban.yy) = False Then
                    Dim Series2 As New Series
                    Series2.Label = "频谱数据"
                    Series2.XValueType = ChartValueType.Auto
                    Series2.ChartType = SeriesChartType.FastLine
                    Series2.IsVisibleInLegend = False
                    Series2.Color = Color.Black
                    Series2.Name = "series2"
                    Dim series3 As New Series
                    series3.Label = "频谱数据"
                    series3.XValueType = ChartValueType.Auto
                    series3.ChartType = SeriesChartType.FastLine
                    series3.IsVisibleInLegend = False
                    series3.Color = Color.YellowGreen
                    series3.Name = "series3"
                    Dim chayy(yy.Count - 1) As Double
                    For i = 0 To xx.Count - 1
                        Dim cx As Double = xx(i)
                        Dim cy As Double = yy(i) - muban.yy(i)
                        If cy >= 0 Then
                            Series2.Points.AddXY(cx, 0)
                            Series2.Points.AddXY(cx, cy)
                            Series2.Points.AddXY(cx, 0)
                            series3.Points.AddXY(cx, 0)
                        Else
                            Series2.Points.AddXY(cx, 0)
                            series3.Points.AddXY(cx, 0)
                            series3.Points.AddXY(cx, cy)
                            series3.Points.AddXY(cx, 0)
                        End If
                        chayy(i) = cy
                    Next
                    Chart3.Series(3) = Series2
                    Chart3.Series(4) = series3
                    Dim fucha As Double = 5
                    Dim daikuan As Double = 3
                    Dim mincha As Double = 3
                    Try
                        fucha = Val(TextBox1.Text)
                        daikuan = Val(TextBox2.Text) * 2 + 1
                        mincha = Val(TextBox3.Text)
                    Catch ex As Exception

                    End Try
                    If fucha < 1 Then fucha = 1
                    If daikuan Mod 2 = 0 Then daikuan = daikuan + 1
                    If daikuan < 3 Then daikuan = 3
                    If mincha < 0 Then mincha = 0
                    Console.WriteLine("幅差：" & fucha)
                    Console.WriteLine("带宽：" & daikuan)
                    Console.WriteLine("小差：" & mincha)
                    Dim result(,) As Double = XinHaoFenLi2(xx, chayy, daikuan, fucha, mincha)
                    Dim ser As New Series("illegalsignal")
                    ser.ChartType = SeriesChartType.Column
                    ser("PointWidth") = 0.1
                    ser.Color = Color.Red
                    ser.IsVisibleInLegend = False
                    Chart3.Series(1).Points.Clear()
                    If IsNothing(result) = False Then
                        For i = 0 To result.Length / 2 - 1
                            Dim rx As Double = result(i, 0)
                            For j = 0 To xx.Count - 1
                                If xx(j) = rx Then
                                    Dim ry As Double = yy(j)
                                    If CheckBox1.Checked Then
                                        If CheckBox2.Checked Then
                                            Try
                                                Console.WriteLine("捕获到新信号" & rx & "MHz")
                                                speek("捕获到新信号" & rx & "MHz")
                                            Catch ex As Exception
                                                'Console.WriteLine(ex.ToString)
                                            End Try
                                        End If
                                        If True Then
                                            Dim msgTxt As String = "捕获到新信号" & rx & "MHz"
                                            'For Each itms In CLB.CheckedItems
                                            '    sendWeChatMsg(itms, msgTxt, False, "", "")
                                            'Next
                                        End If
                                        Dim itm As New ListViewItem(LV22.Items.Count + 1)
                                        itm.SubItems.Add(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                        itm.SubItems.Add(rx)
                                        itm.SubItems.Add(DeviceAddress)
                                        itm.SubItems.Add(ry)
                                        itm.SubItems.Add(ry)
                                        itm.SubItems.Add(ry)
                                        Dim isin As Boolean = False
                                        For Each it As ListViewItem In LV22.Items
                                            If it.SubItems(2).Text = rx Then
                                                it.SubItems(4).Text = ry
                                                If Val(it.SubItems(5).Text) > ry Then
                                                    it.SubItems(5).Text = ry
                                                End If
                                                If Val(it.SubItems(6).Text) < ry Then
                                                    it.SubItems(6).Text = ry
                                                End If
                                                isin = True
                                                Exit For
                                            End If
                                        Next

                                        If isin = False Then
                                            Dim isfind As Boolean = False
                                            For m = 0 To LV22.Items.Count - 1
                                                If Val(LV22.Items(m).SubItems(2).Text) > rx Then
                                                    isfind = True
                                                    LV22.Items.Insert(m, itm)
                                                    Exit For
                                                End If
                                            Next
                                            If isfind = False Then
                                                LV22.Items.Add(itm)
                                            End If
                                        End If
                                    End If
                                    For m = 0 To LV22.Items.Count - 1
                                        LV22.Items(m).Text = m + 1
                                    Next
                                    Dim jieti As Integer = (AutoFenXiDu - 1) / 2
                                    For n = 0 To xx.Count - 1
                                        If xx(n) = rx Then
                                            For m = n - jieti To n + jieti
                                                Chart3.Series(1).Points.AddXY(xx(m), yy(m))
                                            Next
                                            Exit For
                                        End If
                                    Next
                                End If
                            Next
                        Next
                    End If
                Else
                    Chart3.Series(3).Points.Clear()
                    Chart3.Series(1).Points.Clear()
                End If
            Else
                Chart3.Series(3).Points.Clear()
                Chart3.Series(1).Points.Clear()
            End If
        End If
        Dim Series As New Series
        Series.Label = "频谱数据"
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.FastLine
        Series.IsVisibleInLegend = False
        Series.Color = Color.Blue
        For i = 0 To xx.Count - 1
            Series.Points.AddXY(xx(i), yy(i))
        Next
        Chart3.Series(0) = Series
        If True Then  '实时生成模板
            If isShengChengMuBan = False Then
                If Now < ShengChengMuBanEndTime Then
                    isShengChengMuBan = True
                End If
            End If
            ' MsgBox(isShengChengMuBan)
            If isShengChengMuBan = True Then
                If IsNothing(muban) Then
                    muban = New MuBanPinPu
                End If
                muban.time = Now.ToString("yyyy-MM-dd HH:mm:ss")
                If xx.Count = yy.Count Then
                    If IsNothing(muban.XX) Or IsNothing(muban.yy) Then
                        ReDim muban.XX(xx.Count - 1)
                        ReDim muban.yy(yy.Count - 1)
                        For i = 0 To xx.Count - 1
                            muban.XX(i) = xx(i)
                            muban.yy(i) = yy(i)
                        Next
                    Else
                        If xx.Count = muban.XX.Count Then
                            Dim iseque As Boolean = True
                            For i = 0 To xx.Count - 1
                                If xx(i) <> muban.XX(i) Then
                                    iseque = False
                                    Exit For
                                End If
                            Next
                            If iseque Then
                                For i = 0 To xx.Count - 1
                                    If yy(i) > muban.yy(i) Then
                                        muban.yy(i) = yy(i)
                                    End If
                                Next
                            End If
                        End If
                    End If
                    If IsNothing(muban.XX) = False And IsNothing(muban.yy) = False Then
                        Dim ser As New Series
                        ser.Label = "频谱数据"
                        ser.XValueType = ChartValueType.Auto
                        ser.ChartType = SeriesChartType.FastLine
                        ser.Color = Color.FromArgb(236, 170, 0)
                        ser.IsVisibleInLegend = False
                        For i = 0 To xx.Count - 1
                            ser.Points.AddXY(xx(i), muban.yy(i))
                        Next
                        Chart3.Series(2) = ser
                    End If
                End If
                If Now >= ShengChengMuBanEndTime Then
                    Label152.Text = "模板生成完毕"
                    isShengChengMuBan = False
                    'Label12.Text = ShengChengMuBanEndTime.ToString("yyyy-MM-dd HH:mm:ss")
                End If
            End If
        Else

        End If
    End Sub
    Public Sub speek(ByVal str As String)
        Dim th As New Thread(AddressOf th_speek)
        th.Start(str)
    End Sub
    Private Sub th_speek(ByVal str As String)
        Try
            Dim voice As New SpVoice
            voice.Rate = voiceRate
            voice.Volume = 100
            voice.Voice = voice.GetVoices().Item(0)
            voice.Speak(str)
        Catch ex As Exception
            '‘MsgBox(ex.ToString)
        End Try

    End Sub


    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        sigNalCount = 0
        signalStartTime = Nothing
        iniChart()
        iniLv()
        Dim freqbegin As Double = Val(txtFreqStart.Text)
        Dim freqend As Double = Val(txtFreqEnd.Text)
        Dim freqstep As Double = Val(txtFreqStep.Text)
        Dim gcValue As Integer = 8
        Dim isHackOneSnigle As Boolean = False
        Dim id As String = CtlFather.myDeviceInfo.DeviceID
        Dim msg As String = "<TZBQ:SMBH," & id & "," & freqbegin & "," & freqend & "," & freqstep / 1000 & "," & 1 & "," & 0 & "," & 0 & "," & 1 & "," & 1 & ">"
        SendMsgToDev(msg)
    End Sub
    Private Sub Panel17_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel17.Click
        sigNalCount = 0
        signalStartTime = Nothing
        iniChart()
        iniLv()
        Dim freqbegin As Double = Val(txtFreqStart.Text)
        Dim freqend As Double = Val(txtFreqEnd.Text)
        Dim freqstep As Double = Val(txtFreqStep.Text)
        Dim gcValue As Integer = 8
        Dim isHackOneSnigle As Boolean = False
        Dim id As String = CtlFather.myDeviceInfo.DeviceID
        Dim msg As String = "<TZBQ:SMBH," & id & "," & freqbegin & "," & freqend & "," & freqstep / 1000 & "," & 1 & "," & 0 & "," & 0 & "," & 1 & "," & 1 & ">"
        SendMsgToDev(msg)
    End Sub
    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        stopDevice()
    End Sub
    Private Sub stopDevice()
        Dim id As String = CtlFather.myDeviceInfo.DeviceID
        sendMsgToDev("<TZBQ:STOP," & id & ">")
    End Sub
    Private Sub Panel19_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel19.Click
        stopDevice()
    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click
        MakeModule()
    End Sub

    Private Sub Panel23_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel23.Click
        MakeModule()
    End Sub
    Private Sub MakeModule()
        muban = Nothing
        Dim msg As String = InputBox("请输入模板建立时长", "电磁信息服务")
        If msg = "" Then
            MsgBox("请设置正确的时长！")
            Exit Sub
        End If
        Dim value As Integer = Val(msg)
        If value > 0 Then
            ShengChengMuBanEndTime = Now.AddSeconds(value)
            Label152.Text = ShengChengMuBanEndTime.ToString("HH:mm:ss") & "结束"
            Label17.Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
            Label12.Text = value & " 秒"
        Else
            ShengChengMuBanEndTime = "1999-01-01 00:00:00"
            Label152.Text = "请设置正确的时长！"
            MsgBox("请设置正确的时长！")
        End If
    End Sub

    Private Sub Panel17_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel17.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel19_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel19.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel7_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel7.Paint

    End Sub

   
End Class
