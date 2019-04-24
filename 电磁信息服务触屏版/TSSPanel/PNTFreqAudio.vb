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
Public Class PNTFreqAudio
    Public flagUIAready As Boolean = False
    Private CtlFather As CtlOnlineTSS
    Private DeviceAddress As String
    Sub New(ByVal _CtlFather As CtlOnlineTSS)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        CtlFather = _CtlFather
        DeviceAddress = _CtlFather.myDeviceInfo.Address
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub

    Private Sub PNTFreqAudio_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If IsNothing(Thread_Audio) = False Then
            Try
                Thread_Audio.Abort()
            Catch ex As Exception

            End Try
        End If
        If IsNothing(Thread_PPSJ) = False Then
            Try
                Thread_PPSJ.Abort()
            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Sub PNTFreqAudio_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        iniLV()
        iniChart()
        'iniColors()
        flagUIAready = True
        iniRadio()
    End Sub
    Private Sub inichart4()
        Chart4.Series.Clear()
        Chart4.ChartAreas(0).AxisY.Maximum = 255
        Chart4.ChartAreas(0).AxisY.Minimum = 0
        Chart4.ChartAreas(0).AxisY.Interval = 51
        Chart4.ChartAreas(0).AxisY.IntervalOffset = 51

        Chart4.ChartAreas(0).AxisX.Maximum = 800
        Chart4.ChartAreas(0).AxisX.Minimum = 0
        Chart4.ChartAreas(0).AxisX.Interval = 100
        Chart4.ChartAreas(0).AxisX.IntervalOffset = 100
        Chart4.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        'chart6.ChartAreas(0).AxisY.IsReversed = True
        'chart6.ChartAreas(0).AxisX.Enabled = AxisEnabled.False
        'chart6.ChartAreas(0).AxisX2.Enabled = AxisEnabled.True
        Chart4.Series.Add("频率")
        Chart4.Series(0).IsVisibleInLegend = False
        Chart4.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.FastLine
        Chart4.Series(0).Points.AddXY(88, 0)
    End Sub
    Private Sub iniChart5()
        Chart5.Series.Clear()
        Chart5.ChartAreas(0).AxisY.Maximum = -20
        Chart5.ChartAreas(0).AxisY.Minimum = -120
        Chart5.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        Chart5.ChartAreas(0).AxisY.Interval = 20
        Dim Series As New Series
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.FastLine
        Series.IsVisibleInLegend = False
        Series.Color = Color.Blue
        Series.Name = ""
        Chart5.Series.Add(Series)
        Series = New Series("illegalsignal")
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.Column
        Series.IsVisibleInLegend = False
        Series.Color = Color.Red
        Series.Name = ""
        Chart5.Series.Add(Series)
    End Sub
    Private Sub iniChart()
        inichart4()
        iniChart5()
    End Sub
    Private Sub iniColors()
        Chart5.BackColor = Color.Transparent
        Chart5.ChartAreas(0).BackColor = Color.Transparent
        Chart5.ChartAreas(0).AxisX.LabelStyle.ForeColor = Color.LightGray
        Chart5.ChartAreas(0).AxisY.LabelStyle.ForeColor = Color.LightGray
        Chart5.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisX.MinorGrid.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisY.MinorGrid.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisX.MinorTickMark.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisY.MinorTickMark.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisX.MajorTickMark.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisY.MajorTickMark.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisX.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisY.LineColor = Color.LightGray
        Chart5.ChartAreas(0).AxisX.TitleForeColor = Color.LightGray
        Chart5.ChartAreas(0).AxisY.TitleForeColor = Color.LightGray
        Chart5.ForeColor = Color.LightGray
        Chart5.BorderlineColor = Color.LightGray

        Chart4.BackColor = Color.Transparent
        Chart4.ChartAreas(0).BackColor = Color.Transparent
        Chart4.ChartAreas(0).AxisX.LabelStyle.ForeColor = Color.LightGray
        Chart4.ChartAreas(0).AxisY.LabelStyle.ForeColor = Color.LightGray
        Chart4.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisX.MinorGrid.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisY.MinorGrid.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisX.MinorTickMark.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisY.MinorTickMark.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisX.MajorTickMark.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisY.MajorTickMark.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisX.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisY.LineColor = Color.LightGray
        Chart4.ChartAreas(0).AxisX.TitleForeColor = Color.LightGray
        Chart4.ChartAreas(0).AxisY.TitleForeColor = Color.LightGray
        Chart4.ForeColor = Color.LightGray
        Chart4.BorderlineColor = Color.LightGray
    End Sub
    Private Sub iniLV()
        LV27.Clear()
        LV27.Items.Clear()
        LV27.Columns.Clear()
        LV27.View = View.Details
        LV27.GridLines = False
        LV27.FullRowSelect = True
        LV27.Columns.Add("序号", 50)
        LV27.Columns.Add("时间", 130)
        LV27.Columns.Add("频率(MHz)", 70)
        LV27.Columns.Add("地点", 80)
        LV27.Columns.Add("信号电平", 100)
        LV27.Columns.Add("最小值")
        LV27.Columns.Add("最大值")
        LV27.Columns.Add("出现次数")
        LV27.Columns.Add("平均值")
        LV27.Columns.Add("统计时长")
        LV27.Columns.Add("占用度")
        LV27.Columns.Add("监测次数")
        LV27.Columns.Add("超标次数")
        LV27.Columns.Add("占用度直方图", 600)
    End Sub
    Private Sub iniRadio()
        Dim w As Integer = Panel34.Width
        Dim h As Integer = Panel34.Height
        Dim bitmap As New Bitmap(w, h)
        Dim g As Graphics = Graphics.FromImage(bitmap)
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.CompositingQuality = CompositingQuality.AssumeLinear
        For i = 0 To w Step 5
            Dim p1 As New Point(i, 30)
            Dim p2 As New Point(i, h - 1)
            If i = w Then
                Dim ti As Integer = w - 1
                p1 = New Point(ti, 30)
                p2 = New Point(ti, h - 1)
                g.DrawLine(New Pen(Brushes.DimGray, 1), p1, p2)
                Exit For
            End If
            g.DrawLine(New Pen(Brushes.DimGray, 1), p1, p2)
        Next
        g.Save()
        Panel34.BackgroundImage = bitmap
    End Sub
    Private Sub Panel34_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel34.MouseDown
        Dim maxNum As Double = 108
        Dim minNum As Double = 88
        Dim width As Integer = Panel34.Width
        Dim x As Integer = e.X
        Dim v As Double = x / width
        Dim pl As Double = minNum + (maxNum - minNum) * v
        Label18.Text = pl.ToString("0.0000") & " MHz"
        LSP.X1 = x
        LSP.X2 = x
        If x > width - 120 Then
            Label18.Left = x - Label18.Width - 10
        Else
            Label18.Left = x + 10
        End If
    End Sub
    Private Sub Panel34_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel34.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim maxNum As Double = 108
            Dim minNum As Double = 88
            Dim width As Integer = Panel34.Width
            Dim x As Integer = e.X
            If x < 0 Then x = 0
            If x > width Then x = width
            Dim v As Double = x / width
            If x = width Then x = width - 1
            Dim pl As Double = minNum + (maxNum - minNum) * v
            Label18.Text = pl.ToString("0.0000") & " MHz"
            LSP.X1 = x
            LSP.X2 = x
            If x > width - 120 Then
                Label18.Left = x - Label18.Width - 10
            Else
                Label18.Left = x + 10
            End If

        End If
    End Sub
    Dim Thread_Audio As Thread
    Structure json_Audio
        Dim freq As Double
        Dim samplingRate As Long
        Dim audioBit As Integer
        Dim channelNum As Integer
        Dim audioBase64 As String
    End Structure
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
    Public Sub HandleHttpMsg(ByVal HttpMsg As String)
        Console.WriteLine("收到新消息TSS  " & Now.ToString("HH:mm:ss"))
        Dim AudioList As New List(Of json_Audio)
        Dim PPSJList As New List(Of json_PPSJ)
        Try
            Dim p As JArray = JArray.Parse(HttpMsg)
            For Each itm As JValue In p
                Dim jMsg As String = itm.Value
                Dim JObj As Object = JObject.Parse(jMsg)
                Dim func As String = JObj("func").ToString
                Console.WriteLine("func= " & func)
                Console.WriteLine("func= " & func)
                If func = "bscan" Then
                    Dim msg As String = JObj("msg").ToString
                    Dim ppsj As json_PPSJ = JsonConvert.DeserializeObject(msg, GetType(json_PPSJ))
                    PPSJList.Add(ppsj)
                End If
                If func = "ifscan_wav" Then
                    Dim msg As String = JObj("msg").ToString
                    Dim audio As json_Audio = JsonConvert.DeserializeObject(msg, GetType(json_Audio))
                    AudioList.Add(audio)
                End If
            Next
        Catch ex As Exception
            Exit Sub
        End Try
        If PPSJList.Count > 0 Then
            If IsNothing(Thread_PPSJ) = False Then
                Try
                    Thread_PPSJ.Abort()
                Catch ex As Exception

                End Try
            End If
            Thread_PPSJ = New Thread(AddressOf HandlePPSJList)
            Thread_PPSJ.Start(PPSJList)
        End If
        If AudioList.Count > 0 Then
            If IsNothing(Thread_Audio) = False Then
                Try
                    Thread_Audio.Abort()
                Catch ex As Exception

                End Try
            End If
            Thread_Audio = New Thread(AddressOf HandleAudioList)
            Thread_Audio.Start(AudioList)
        End If
    End Sub
    Dim DisPlayLock As Object
    Private Sub HandlePPSJList(ByVal Plist As List(Of json_PPSJ))
        If IsNothing(Plist) Then Exit Sub
        If Plist.Count = 0 Then Exit Sub
        If IsNothing(DisPlayLock) Then DisPlayLock = New Object
        Dim count As Integer = Plist.Count
        Dim sleepCount As Double = (GetHttpMsgTimeSpan * 1000 - 100) / (count + 1)
        SyncLock DisPlayLock
            Try
                For i = 0 To Plist.Count - 1
                    ' Try
                    Dim itm As json_PPSJ = Plist(i)
                    Console.WriteLine("dataCount=" & itm.dataCount)
                    CtlFather.Invoke(Sub() handlePinPuFenXi(itm))
                    If i <> count - 1 Then
                        Sleep(sleepCount)
                    End If
                Next
            Catch ex As Exception

            End Try
        End SyncLock
    End Sub
    Dim AudioPlayLock As Object
    Private Sub HandleAudioList(ByVal list As List(Of json_Audio))
        If IsNothing(list) Then Exit Sub
        If list.Count = 0 Then Exit Sub
        If IsNothing(AudioPlayLock) Then AudioPlayLock = New Object
        SyncLock AudioPlayLock
            For Each itm In list
                Dim freq As String = itm.freq
                Label19.Text = freq & " MHz"
                Dim base64 As String = itm.audioBase64
                Try
                    Dim buffer() As Byte = Convert.FromBase64String(base64)
                    If IsNothing(buffer) = False Then
                        If buffer.Length > 44 Then
                            Dim realBy(buffer.Length - 45) As Byte
                            Array.Copy(buffer, 44, realBy, 0, realBy.Length)
                            Dim th As New Thread(AddressOf play)
                            th.Start(buffer)
                        End If
                    End If
                Catch ex As Exception

                End Try
            Next
        End SyncLock

    End Sub
    Private Sub play(ByVal buf() As Byte)
        Try
            Dim ms As MemoryStream = New MemoryStream(buf)
            Dim sp As SoundPlayer = New SoundPlayer(ms)
            sp.Play()
        Catch ex As Exception
        End Try
    End Sub
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
            xx(xx.Length - 1) = p.freqEnd
            Dim jieshu As Double = freqStart + (dataCount - 1) * freqStep
            jieshu = p.freqEnd
            Dim Series As New Series("频谱")
            Series.Label = ""
            Series.XValueType = ChartValueType.Auto
            Series.ChartType = SeriesChartType.FastLine
            Series.IsVisibleInLegend = False
            Series.Color = Color.Blue
            Series.Name = "频谱"
            Series.LabelToolTip = "频率：#VALX 场强：#VAL"
            Series.ToolTip = "频率：#VALX 场强：#VAL"
            For i = 0 To xx.Length - 1
                Series.Points.AddXY(xx(i), yy(i))
            Next
            If freqStart = 88 And jieshu = 108 Then
                Chart5.Series(0) = Series
            End If
            Dim du As Integer = AutoFenXiDu
            Dim result(,) As Double = XinHaoFenLi(xx, yy, du, AutoFenXiFuCha)
            Dim jieti As Integer = (du - 1) / 2
            If TongJiCiShu < 40 Then
                Label24.Text = "正在搜索与统计"
                If freqStart = 88 And jieshu = 108 Then
                    If Chart5.Series.Count >= 2 Then
                        Chart5.Series(1).Points.Clear()
                    End If
                End If
                Dim LVIS() As ListViewItem = XinHao2LV(result)
                If IsNothing(LVIS) = False Then
                    For Each itm As ListViewItem In LVIS
                        Dim pl As String = itm.SubItems(2).Text
                        Dim count As String = itm.SubItems(7).Text
                        Dim cq As String = itm.SubItems(6).Text
                        If IsNumeric(count) Then
                            If freqStart = 88 And jieshu = 108 Then
                                If Chart5.Series.Count >= 2 Then
                                    For j = 0 To xx.Count - 1 - jieti
                                        If xx(j) = pl Then
                                            If j >= jieti Then
                                                For m = j - jieti To j + jieti
                                                    Dim value As Double = yy(m)
                                                    Me.Invoke(Sub() Chart5.Series(1).Points.AddXY(xx(m), value))
                                                Next
                                            End If
                                            Exit For
                                        End If
                                    Next

                                End If
                            End If
                        End If
                    Next
                End If
            Else
                Label24.Text = "搜索完毕，信号数量为 " & sigNalCount
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim TongJiCiShu As Integer
    Dim sigNalCount As Integer
    Dim getSigNalCount As Integer
    Dim signalStartTime As Date
    Dim itmList As List(Of ListViewItem)
    Private Function XinHao2LV(ByVal result(,) As Double) As ListViewItem()
        If IsNothing(result) Then Return Nothing
        If IsNothing(itmList) Then
            itmList = New List(Of ListViewItem)
        End If
        If IsNothing(signalStartTime) Then
            signalStartTime = New Date
            signalStartTime = Now
            sigNalCount = 0
        End If
        If signalStartTime.Year <> Now.Year Then
            signalStartTime = New Date
            signalStartTime = Now
            sigNalCount = 0
        End If
        sigNalCount = sigNalCount + 1
        For i = 0 To result.Length / 2 - 1
            Dim pinlv As Double = result(i, 0)
            Dim changqiang As Double = result(i, 1)
            Dim isinlv4 As Boolean = False
            For k = 0 To itmList.Count - 1
                If itmList(k).SubItems(2).Text = pinlv Then
                    isinlv4 = True
                    Dim min As Double = Val(itmList(k).SubItems(5).Text)
                    Dim max As Double = Val(itmList(k).SubItems(6).Text)
                    If min > changqiang Then min = changqiang
                    If max < changqiang Then max = changqiang
                    itmList(k).SubItems(5).Text = min
                    itmList(k).SubItems(6).Text = max
                    itmList(k).SubItems(7).Text = Val(itmList(k).SubItems(7).Text) + 1
                    itmList(k).SubItems(8).Text = (max + min) * 0.5
                    Dim t As TimeSpan = Now - signalStartTime
                    Dim str As String = t.Hours.ToString("00") & ":" & t.Minutes.ToString("00") & ":" & t.Seconds.ToString("00")
                    itmList(k).SubItems(9).Text = str  '统计时长
                    'itmList(k).SubItems(10).Text = "" '占用度
                    itmList(k).SubItems(11).Text = sigNalCount '监测次数
                    Dim cbcount As Integer = Val(itmList(k).SubItems(12).Text)
                    itmList(k).SubItems(12).Text = cbcount + 1 '超标次数
                    Dim bfb As String = 100 * (cbcount / sigNalCount).ToString("0.00") & "%"
                    itmList(k).SubItems(10).Text = bfb
                    Exit For
                End If
            Next
            If isinlv4 = False Then
                Dim itm As New ListViewItem(itmList.Count + 1) '0
                itm.SubItems.Add(Now.ToString("yyyy-MM-dd HH:mm:ss"))  '1
                itm.SubItems.Add(pinlv) '2
                itm.SubItems.Add(DeviceAddress) '3
                itm.SubItems.Add(changqiang) '4
                itm.SubItems.Add(changqiang) '5
                itm.SubItems.Add(changqiang) '6
                itm.SubItems.Add(1) '6
                itm.SubItems.Add(changqiang) '7
                itm.SubItems.Add("00:00:00") '7
                itm.SubItems.Add(1) '7
                itm.SubItems.Add(sigNalCount) '7
                itm.SubItems.Add(1) '7
                itmList.Add(itm)
            End If
        Next
        For i = 0 To itmList.Count - 1
            itmList(i).Text = i + 1
        Next
        Dim plist As New List(Of ListViewItem)
        For Each itm In itmList
            Dim isadd As Boolean = False
            Dim count As Integer = Val(itm.SubItems(7).Text)
            If count < 10 Then
                Continue For
            End If
            For i = 0 To plist.Count - 1
                Dim itm2 As ListViewItem = plist(i)
                Dim count2 As Integer = Val(itm2.SubItems(7).Text)
                If count > count2 Then
                    plist.Insert(i, itm)
                    isadd = True
                    Exit For
                End If
            Next
            If isadd = False Then
                plist.Add(itm)
            End If
        Next
        Dim cnt As Integer = plist.Count
        getSigNalCount = getSigNalCount + cnt
        For i = 0 To cnt - 1
            Dim itm As ListViewItem = plist(i)
            itm.Text = i + 1
            Dim CBInt As Integer = Val(itm.SubItems(12).Text)
            Dim SumInt As Integer = Val(itm.SubItems(11).Text)
            Dim str As String = GetPerPic(CBInt / SumInt)
            itm.SubItems.Add(str)
        Next
        LV27.Items.Clear()
        For i = 0 To cnt - 1
            Dim itm As ListViewItem = plist(i).Clone
            itm.Text = i + 1
            LV27.Items.Add(itm)
        Next
        Return plist.ToArray
    End Function
    Private Function GetPerPic(ByVal v As Double) As String
        If v >= 1 Then v = 1
        If v <= 0 Then v = 0
        If v = 0 Then Return ""
        Dim fk As String = "■"
        Dim value As Integer = v * 100
        Dim result As String = ""
        For i = 1 To value
            result = result + fk
        Next
        Return result
    End Function

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        getSigNalCount = 0
        iniChart()
        iniLV()
        Dim freqbegin As Double = 88
        Dim freqend As Double = 108
        Dim freqstep As Double = 25
        Dim gcValue As Integer = 8
        Dim isHackOneSnigle As Boolean = False
        CtlFather.SendFreqOrder(freqbegin, freqend, freqstep, gcValue, isHackOneSnigle)
    End Sub

    Private Sub Panel12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel12.Click
        getSigNalCount = 0
        TongJiCiShu = 0
        sigNalCount = 0
        signalStartTime = Nothing
        itmList = Nothing
        iniChart()
        iniLV()
        Dim freqbegin As Double = 88
        Dim freqend As Double = 108
        Dim freqstep As Double = 25
        Dim gcValue As Integer = 8
        Dim isHackOneSnigle As Boolean = False
        CtlFather.SendFreqOrder(freqbegin, freqend, freqstep, gcValue, isHackOneSnigle)
    End Sub


    Private Sub Panel17_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel17.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub


    Private Sub Panel19_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel19.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel12_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel12.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub 快捷输入ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 快捷输入ToolStripMenuItem.Click
        Dim text As String = InputBox("请输入频率值(范围88 -- 108)", "电磁信息云服务触屏版")
        If text = "" Then Exit Sub
        If IsNumeric(text) = False Then Exit Sub
        Dim vt As Double = Val(text)
        If vt < 88 Or vt > 108 Then
            MsgBox("请输入介于88到108之间的值")
            Exit Sub
        End If
        Label18.Text = text & "MHz"
        Dim maxNum As Double = 108
        Dim minNum As Double = 88
        Dim width As Integer = Panel34.Width
        Dim x As Integer = width * ((vt - minNum) / (maxNum - minNum))
        If x >= width Then x = width - 1
        LSP.X1 = x
        LSP.X2 = x
        If x > 400 Then
            Label18.Left = x - Label18.Width - 10
        Else
            Label18.Left = x + 10
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        If LV27.SelectedItems.Count = 0 Then Return
        Dim itm As ListViewItem = LV27.SelectedItems(0)
        Dim pl As String = itm.SubItems(2).Text
        Dim text As String = pl
        If IsNumeric(text) = False Then Exit Sub
        Dim vt As Double = Val(text)
        If vt < 88 Or vt > 108 Then
            MsgBox("请输入介于88到108之间的值")
            Exit Sub
        End If
        Label18.Text = text & "MHz"
        Dim maxNum As Double = 108
        Dim minNum As Double = 88
        Dim width As Integer = Panel34.Width
        Dim x As Integer = width * ((vt - minNum) / (maxNum - minNum))
        If x >= width Then x = width - 1
        LSP.X1 = x
        LSP.X2 = x
        If x > 400 Then
            Label18.Left = x - Label18.Width - 10
        Else
            Label18.Left = x + 10
        End If
        PlayAudio(text)
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        CtlFather.SendStopOrder()
    End Sub

    Private Sub Panel19_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel19.Click
        CtlFather.SendStopOrder()
    End Sub


    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        Dim text As String = txtFreqStart.Text
        text = text.Replace("MHz", "")
        PlayAudio(text)
    End Sub

    Private Sub Panel17_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel17.Click
        Dim text As String = txtFreqStart.Text
        text = text.Replace("MHz", "")
        PlayAudio(text)
    End Sub
    Private Sub PlayAudio(ByVal freq As Double)
        Dim text As String = freq
        Dim msg As String = ""
        Dim freqbegin As String = text
        Dim freqend As String = text
        Dim freqstep As String = text
        Dim p As tssOrder_stu
        p.freqStart = freqbegin
        p.freqEnd = 108
        p.freqStep = 10 * (108 - freqbegin) / 8
        p.task = "ifscan_wav"
        p.deviceID = CtlFather.myDeviceInfo.DeviceID
        Dim orderMsg As String = JsonConvert.SerializeObject(p)
        CtlFather.SendMsgToDev(orderMsg)
    End Sub

End Class
