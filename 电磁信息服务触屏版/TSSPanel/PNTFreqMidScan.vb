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
Public Class PNTFreqMidScan
    Public flagUIAready As Boolean = False
    Private CtlFather As CtlOnlineTSS
    Sub New(ByVal _CtlFather As CtlOnlineTSS)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        CtlFather = _CtlFather
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
    Private Sub PNTFreqBscan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        Panel20.Dock = DockStyle.Fill
        Panel43.Dock = DockStyle.Fill
        ShowPuBuTu()
        iniPBX()
        iniChart()
        iniColors()
        flagUIAready = True
    End Sub
    Private Sub iniColors()
        Chart1.BackColor = Color.Transparent
        Chart1.ChartAreas(0).BackColor = Color.Transparent
        Chart1.ChartAreas(0).AxisX.LabelStyle.ForeColor = Color.LightGray
        Chart1.ChartAreas(0).AxisY.LabelStyle.ForeColor = Color.LightGray
        Chart1.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisX.MinorGrid.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisY.MinorGrid.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisX.MinorTickMark.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisY.MinorTickMark.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisX.MajorTickMark.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisY.MajorTickMark.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisX.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisY.LineColor = Color.LightGray
        Chart1.ChartAreas(0).AxisX.TitleForeColor = Color.LightGray
        Chart1.ChartAreas(0).AxisY.TitleForeColor = Color.LightGray
        Chart1.ForeColor = Color.LightGray
        Chart1.BorderlineColor = Color.LightGray
        Panel43.BorderStyle = Windows.Forms.BorderStyle.None
        PBX.BackColor = Color.Transparent
        PBXLeft.BackColor = Color.Transparent
        PBXRight.BackColor = Color.Transparent
    End Sub
    Private Sub iniChart()

        iniChart1()
        iniChart6()

    End Sub
    Private Sub iniChart1()
        Chart1.Series.Clear()
        'Chart1.ChartAreas(0).CursorX.IsUserEnabled = True
        'Chart1.ChartAreas(0).CursorY.IsUserEnabled = True
        Chart1.ChartAreas(0).AxisY.Maximum = -20
        Chart1.ChartAreas(0).AxisY.Minimum = -120
        Chart1.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        Chart1.ChartAreas(0).AxisY.Interval = 20
        'Chart1.ChartAreas(0).AxisX.Maximum = 0
        'Chart1.ChartAreas(0).AxisX.Minimum = 800
        'Chart1.ChartAreas(0).AxisX.Interval = 100
        Dim Series As New Series("频谱")
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.FastLine
        Series.IsVisibleInLegend = False
        Series.Color = Color.Blue
        Series.Name = "频谱"
        Series.LabelToolTip = "频率：#VALX 场强：#VAL"
        Series.ToolTip = "频率：#VALX 场强：#VAL"
        Chart1.Series.Add(Series) '0

        Series = New Series("markLine")
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.Line
        Series.IsVisibleInLegend = False
        Series.Color = Color.Red
        Series.Name = "markLine"
        Series.LabelToolTip = "频率：#VALX 场强：#VAL"
        Series.ToolTip = "频率：#VALX 场强：#VAL"
        'Series.Label = "频率：#VALX 场强：#VAL"
        'Series.IsValueShownAsLabel = True


        'Series.Label = "#VALX" & "," & "#VAL"
        'Series.Points.AddXY(90, -20)
        'Series.Points.AddXY(90, -120)
        Chart1.Series.Add(Series) '1
        Series = New Series("markPoint")
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.Point
        Series.IsVisibleInLegend = False
        Series.Color = Color.Red
        Series.Name = "markPoint"
        Series.LabelToolTip = "频率：#VALX 场强：#VAL"
        Series.ToolTip = "频率：#VALX 场强：#VAL"
        Series.Label = "频率：#VALX MHz 场强：#VAL dBm"
        Series.IsValueShownAsLabel = True
        Chart1.Series.Add(Series) '2

        Series = New Series("MoudleFreq")
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.FastLine
        Series.IsVisibleInLegend = False
        Series.Color = Color.Gray
        Series.Name = "MoudleFreq"
        Chart1.Series.Add(Series) '3
    End Sub
    Private Sub iniChart6()
        Chart6.Series.Clear()
        Chart6.ChartAreas(0).AxisY.Maximum = -20
        Chart6.ChartAreas(0).AxisY.Minimum = -120
        Chart6.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        Chart6.ChartAreas(0).AxisY.Interval = 20
        Dim Series2 As New Series("")
        Series2.Color = Color.Blue
        Series2.IsValueShownAsLabel = True
        Series2.IsVisibleInLegend = False
        Series2.ToolTip = "#VAL"
        Series2.Color = Color.Blue
        Series2.ChartType = SeriesChartType.FastLine

        Chart6.Series.Add(Series2)
        Dim Series As New Series("1")
        Series.Color = Color.Blue
        Series.IsValueShownAsLabel = True
        Series.IsVisibleInLegend = False
        Series.ToolTip = "#VAL"
        Series.Color = Color.Blue
        Series.ChartType = SeriesChartType.FastLine
        For i = 0 To 1999
            Series.Points.Add(-120)
        Next
        Chart6.Series.Add(Series)
    End Sub
    Private Sub ShowPuBuTu()
        Panel20.Visible = False
        Panel43.Visible = True
    End Sub
    Private Sub ShowShiXu()
        Panel20.Visible = True
        Panel43.Visible = False
    End Sub
    Private Sub PNTFreqBscan_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed

    End Sub



    Private Sub Panel17_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel17.Paint
        ControlPaint.DrawBorder(e.Graphics, Panel17.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel19_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel19.Paint
        ControlPaint.DrawBorder(e.Graphics, Panel19.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel23_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel23.Click
        ShowPuBuTu()
    End Sub
    Private Sub Panel25_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel25.Click
        ShowShiXu()
    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click
        ShowPuBuTu()
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click
        ShowShiXu()
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
    Public Sub HandleHttpMsg(ByVal HttpMsg As String)
        Console.WriteLine("收到新消息TSS  " & Now.ToString("HH:mm:ss"))
        If Not Me.flagUIAready Then
            Console.WriteLine("flagUIAready=" & flagUIAready & ",不处理本次数据")
            Return
        End If
        Dim PPSJList As New List(Of json_PPSJ)
        Try
            Dim p As JArray = JArray.Parse(HttpMsg)
            For Each itm As JValue In p
                Dim jMsg As String = itm.Value
                Dim JObj As Object = JObject.Parse(jMsg)
                Dim func As String = JObj("func").ToString
                Console.WriteLine("func= " & func)
                If func = "bscan" Then
                    Dim msg As String = JObj("msg").ToString
                    Dim ppsj As json_PPSJ = JsonConvert.DeserializeObject(msg, GetType(json_PPSJ))
                    PPSJList.Add(ppsj)
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
    Private recentFreqStart As Double
    Private recentFreqEnd As Double
    Dim freqStartTimeStr As String
    Dim TimeFreqPoint As Double = -88
    Private Sub iniPBX()
        PBX.Image = Nothing
        PBXLeft.Image = Nothing
        PBXRight.Image = Nothing
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
            If freqStart <> recentFreqStart Or jieshu <> recentFreqEnd Then
                iniPBX()
                recentFreqStart = freqStart
                recentFreqEnd = jieshu
            End If
            Dim Series As New Series("频谱")
            Series.Label = ""
            Series.XValueType = ChartValueType.Auto
            Series.ChartType = SeriesChartType.FastLine
            Series.IsVisibleInLegend = False
            Series.Color = Color.Blue
            Series.Name = "频谱"
            Series.LabelToolTip = "频率：#VALX 场强：#VAL"
            Series.ToolTip = "频率：#VALX 场强：#VAL"
            Chart1.ChartAreas(0).AxisX.Minimum = freqStart
            Chart1.ChartAreas(0).AxisX.Maximum = jieshu
            Chart1.ChartAreas(0).AxisX.Interval = (jieshu - freqStart) / 5
            For i = 0 To xx.Length - 1
                Series.Points.AddXY(xx(i), yy(i))
            Next
            If Chart1.Series.Count = 0 Then
                Chart1.Series.Add(Series)
            Else
                Chart1.Series(0) = Series
            End If
            If Chart1.Series.Count >= 3 Then
                If Chart1.Series(1).Points.Count >= 1 Then
                    Dim xValue As Double = Chart1.Series(1).Points(0).XValue
                    For Each ppt In Series.Points
                        If ppt.XValue = xValue Then
                            Chart1.Series(2).Points.Clear()
                            Chart1.Series(2).Points.AddXY(xValue, ppt.YValues(0))
                            ' Chart1.Series(2).Points.AddXY(xValue, -25)
                            Exit For
                        End If
                    Next
                End If
            End If
            GXPuBuTu(xx, yy)
            If freqStartTimeStr = "" Then
                freqStartTimeStr = Now.ToString("HH:mm:ss")
            End If
            If TimeFreqPoint < freqStart Or TimeFreqPoint > jieshu Then
                TimeFreqPoint = freqStart
            End If
            Dim TimeMarkPointValue As Double = 0
            For i = 0 To xx.Count - 1
                If xx(i) = TimeFreqPoint Then
                    TimeMarkPointValue = yy(i)
                    If Chart6.Series(0).Points.Count > 2000 Then
                        Chart6.Series(0).Points.RemoveAt(0)
                    End If
                    Chart6.Series(0).Points.AddXY(Now.ToString("HH:mm:ss"), yy(i))
                    Exit For
                End If
            Next
            Dim str As String = "时间: " & freqStartTimeStr & " To " & Now.ToString("HH:mm:ss") & "  频率范围:[" & freqStart & "," & jieshu & "]"
            str = "Mark " & TimeFreqPoint & "MHz," & TimeMarkPointValue & "dBm" & "   " & str
            If isDSGFreq Then
                str = str & "  <DSG频谱压缩 " & jslenstr & ">"
            Else
                str = str & "  <" & jslenstr & ">"
            End If
            Label13.Text = str
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            ' MsgBox(ex.ToString)
        End Try
    End Sub
    Private recentY As Integer = 0
    Private Sub GXPuBuTu(ByVal xx() As Double, ByVal yy() As Double)
        Try
            If IsNothing(yy) Then Exit Sub
            Dim max As Integer = 255
            Dim min As Integer = 0
            Dim width As Integer = yy.Count
            Dim heigth As Integer = PBX.Height
            If IsNothing(PBX.Image) Then
                Dim bmp As New Bitmap(width, heigth)
                recentY = 0
                For m = 0 To width - 1
                    Dim value As Double = yy(m)
                    Dim vR As Integer = Abs(value + 90)
                    Dim vG As Integer = Abs(value + 10)
                    Dim vB As Integer = Abs(value + 30)
                    If value > -70 Then
                        vR = 255
                        vG = 255
                        vB = 0
                    End If
                    If vR > 255 Then vR = 255 : If vR < 0 Then vR = 0
                    If vB > 255 Then vB = 255 : If vB < 0 Then vB = 0
                    If vG > 255 Then vG = 255 : If vG < 0 Then vG = 0
                    bmp.SetPixel(m, recentY, Color.FromArgb(vR, vG, vB))
                Next
                recentY = recentY + 1
                PBX.Image = bmp
            Else
                Dim bmp As Bitmap = PBX.Image
                If recentY <= heigth - 1 Then
                    For m = 0 To width - 1
                        Dim value As Double = yy(m)
                        Dim vR As Integer = Abs(value + 90)
                        Dim vG As Integer = Abs(value + 10)
                        Dim vB As Integer = Abs(value + 30)
                        If value > -70 Then
                            vR = 255
                            vG = 255
                            vB = 0
                        End If
                        If vR > 255 Then vR = 255 : If vR < 0 Then vR = 0
                        If vB > 255 Then vB = 255 : If vB < 0 Then vB = 0
                        If vG > 255 Then vG = 255 : If vG < 0 Then vG = 0
                        bmp.SetPixel(m, recentY, Color.FromArgb(vR, vG, vB))
                    Next
                    recentY = recentY + 1
                Else
                    Dim nb As Bitmap = bmp.Clone(New Rectangle(0, 1, bmp.Width, bmp.Height - 1), System.Drawing.Imaging.PixelFormat.DontCare)
                    bmp = New Bitmap(bmp.Width, bmp.Height)
                    Dim gk As Graphics = Graphics.FromImage(bmp)
                    gk.DrawImage(nb, 0, 0)
                    gk.Save()
                    For m = 0 To width - 1
                        Dim value As Double = yy(m)
                        Dim vR As Integer = Abs(value + 90)
                        Dim vG As Integer = Abs(value + 10)
                        Dim vB As Integer = Abs(value + 30)
                        If value > -70 Then
                            vR = 255
                            vG = 255
                            vB = 0
                        End If
                        If vR > 255 Then vR = 255 : If vR < 0 Then vR = 0
                        If vB > 255 Then vB = 255 : If vB < 0 Then vB = 0
                        If vG > 255 Then vG = 255 : If vG < 0 Then vG = 0
                        If recentY >= bmp.Height Then
                            bmp.SetPixel(m, bmp.Height - 1, Color.FromArgb(vR, vG, vB))
                        Else
                            bmp.SetPixel(m, recentY, Color.FromArgb(vR, vG, vB))
                        End If

                    Next
                End If
                PBX.Image = bmp
                Dim leftColor As Color = Color.FromArgb(0, 80, 60)
                Dim leftBmp As New Bitmap(Panel44.Width, bmp.Height)
                Dim gleft As Graphics = Graphics.FromImage(leftBmp)
                Dim leftBrush As Brush = New SolidBrush(leftColor)
                gleft.FillRectangle(leftBrush, 0, 0, leftBmp.Width, recentY)
                gleft.Save()
                PBXLeft.Image = leftBmp
                Dim rightBmp As New Bitmap(Panel45.Width, bmp.Height)
                Dim gright As Graphics = Graphics.FromImage(rightBmp)
                Dim rightBrush As Brush = New SolidBrush(leftColor)
                gright.FillRectangle(rightBrush, 0, 0, leftBmp.Width, recentY)
                gright.Save()
                PBXRight.Image = rightBmp
            End If
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            ' MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub PinPuQuYang(ByVal flag As Boolean)
        If flag Then
            If Chart1.Series.Count < 4 Then Return
            Dim Series As New Series("MoudleFreq")
            Series.Label = ""
            Series.XValueType = ChartValueType.Auto
            Series.ChartType = SeriesChartType.FastLine
            Series.IsVisibleInLegend = False
            Series.Color = Color.Gray
            Series.Name = "MoudleFreq"
            Dim se As Series = Chart1.Series(0)
            For Each p In se.Points
                Series.Points.AddXY(p.XValue, p.YValues(0))
            Next
            Chart1.Series(3) = Series
        Else
            If Chart1.Series.Count >= 4 Then Chart1.Series(3).Points.Clear()
        End If

    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label11.Click
        PinPuQuYang(True)
    End Sub

    Private Sub Panel27_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel27.Click
        PinPuQuYang(True)
    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click
        PinPuQuYang(False)
    End Sub
    Private Sub Panel29_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel29.Click
        PinPuQuYang(False)
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        freqStartTimeStr = ""
        iniPBX()
        iniChart1()
        iniChart6()
        Dim freqCenter As Double = Val(txtFreqCenter.Text)
        Dim freqRegin As Double = Val(txtFreqRegin.Text)
        Dim freqstep As Double = Val(txtFreqStep.Text)
        Dim freqbegin As Double = freqCenter - freqRegin / 2
        Dim freqend As Double = freqCenter + freqRegin / 2
        If freqbegin < 0 Or freqend < 0 Then
            MsgBox("起始频率和终止频率不可小于0")
            Return
        End If
        Dim gcValue As Integer = 8
        Dim isHackOneSnigle As Boolean = False
        CtlFather.SendFreqOrder(freqbegin, freqend, freqstep, gcValue, isHackOneSnigle)
    End Sub
    Private Sub Panel17_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel17.Click
        freqStartTimeStr = ""
        iniPBX()
        iniChart1()
        iniChart6()
        Dim freqCenter As Double = Val(txtFreqCenter.Text)
        Dim freqRegin As Double = Val(txtFreqRegin.Text)
        Dim freqstep As Double = Val(txtFreqStep.Text)
        Dim freqbegin As Double = freqCenter - freqRegin / 2
        Dim freqend As Double = freqCenter + freqRegin / 2
        If freqbegin < 0 Or freqend < 0 Then
            MsgBox("起始频率和终止频率不可小于0")
            Return
        End If
        Dim gcValue As Integer = 8
        Dim isHackOneSnigle As Boolean = False
        CtlFather.SendFreqOrder(freqbegin, freqend, freqstep, gcValue, isHackOneSnigle)
    End Sub
    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        CtlFather.SendStopOrder()
    End Sub
    Private Sub Panel19_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel19.Click
        CtlFather.SendStopOrder()
    End Sub
End Class
