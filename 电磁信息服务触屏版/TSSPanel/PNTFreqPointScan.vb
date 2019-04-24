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
Public Class PNTFreqPointScan
    Public flagUIAready As Boolean = False
    Private CtlFather As CtlOnlineTSS
    Sub New(ByVal _CtlFather As CtlOnlineTSS)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        CtlFather = _CtlFather
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub

    Private Sub PNTFreqPointScan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        iniLV()
        iniChart()
        iniColors()
        DeviceAddress = CtlFather.myDeviceInfo.Address
        flagUIAready = True
    End Sub
    Private Sub iniChart()
        Chart2.Series.Clear()
        Chart2.ChartAreas(0).AxisY.Maximum = -20
        Chart2.ChartAreas(0).AxisY.Minimum = -120
        Chart2.ChartAreas(0).AxisX.LabelStyle.IsStaggered = False
        Chart2.ChartAreas(0).AxisY.Interval = 20    
        ' Chart2.ChartAreas(0).AxisY.IsReversed = True
        ' Chart2.ChartAreas(0).AxisY.IsStartedFromZero = True
        Dim Series As New Series
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        Series.ChartType = SeriesChartType.FastLine
        Series.IsVisibleInLegend = False
        Series.Color = Color.Blue
        Series.Name = ""
        Chart2.Series.Add(Series)
        Series = New Series("illegalsignal")
        Series.Label = ""
        Series.XValueType = ChartValueType.Auto
        '  Series.BorderWidth = "0.5"
        Series.ChartType = SeriesChartType.Column
        Series.IsVisibleInLegend = False
        Series.Color = Color.Red
        Series.Name = ""
        Chart2.Series.Add(Series)

    End Sub
    Private Sub iniColors()
        Chart2.BackColor = Color.Transparent
        Chart2.ChartAreas(0).BackColor = Color.Transparent
        Chart2.ChartAreas(0).AxisX.LabelStyle.ForeColor = Color.LightGray
        Chart2.ChartAreas(0).AxisY.LabelStyle.ForeColor = Color.LightGray
        Chart2.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisX.MinorGrid.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisY.MinorGrid.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisX.MinorTickMark.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisY.MinorTickMark.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisX.MajorTickMark.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisY.MajorTickMark.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisX.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisY.LineColor = Color.LightGray
        Chart2.ChartAreas(0).AxisX.TitleForeColor = Color.LightGray
        Chart2.ChartAreas(0).AxisY.TitleForeColor = Color.LightGray
        Chart2.ForeColor = Color.LightGray
        Chart2.BorderlineColor = Color.LightGray
    End Sub
    Private Sub iniLV()
        LV20.Clear()
        LV20.Items.Clear()
        LV20.Columns.Clear()
        LV20.View = View.Details
        LV20.GridLines = False
        LV20.FullRowSelect = True
        LV20.Columns.Add("序号", 50)
        LV20.Columns.Add("时间", 130)
        LV20.Columns.Add("频率(MHz)", 70)
        LV20.Columns.Add("地点", 80)
        LV20.Columns.Add("信号电平", 100)
        LV20.Columns.Add("最小值")
        LV20.Columns.Add("最大值")
        LV20.Columns.Add("出现次数")
        LV20.Columns.Add("平均值")
        LV20.Columns.Add("统计时长")
        LV20.Columns.Add("占用度")
        LV20.Columns.Add("监测次数")
        LV20.Columns.Add("超标次数")
        LV20.Columns.Add("占用度直方图", 600)
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
            xx(xx.Length - 1) = p.freqEnd
            Dim jieshu As Double = freqStart + (dataCount - 1) * freqStep
            jieshu = p.freqEnd
            If freqStart <> recentFreqStart Or jieshu <> recentFreqEnd Then
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
            For i = 0 To xx.Length - 1
                Series.Points.AddXY(xx(i), yy(i))
            Next
            Chart2.ChartAreas(0).AxisX.Minimum = freqStart
            Chart2.ChartAreas(0).AxisX.Maximum = jieshu
            Chart2.ChartAreas(0).AxisX.Interval = (jieshu - freqStart) / 5
            Chart2.Series(0) = Series
            Try
                Dim du As Integer = AutoFenXiDu
                Dim result(,) As Double = XinHaoFenLi(xx, yy, du, AutoFenXiFuCha)
                Dim jieti As Integer = (du - 1) / 2
                If TongJiCiShu < 60 Then
                    Label11.Text = "正在统计中,当前信号数量为 " & LV20.Items.Count
                    TongJiCiShu = TongJiCiShu + 1
                    XinHao2LV(result)
                    If Chart2.Series.Count >= 2 Then
                        Chart2.Series(1).Points.Clear()
                    End If
                    For Each itm As ListViewItem In LV20.Items
                        Dim pl As String = itm.SubItems(2).Text
                        Dim count As String = itm.SubItems(7).Text
                        Dim cq As String = itm.SubItems(6).Text
                        If IsNumeric(count) Then
                            If Val(count) >= 10 Then
                                If Chart2.Series.Count >= 2 Then
                                    For j = 0 To xx.Count - 1 - jieti
                                        If xx(j) = pl Then
                                            If j >= jieti Then
                                                For m = j - jieti To j + jieti
                                                    Dim value As Double = yy(m)
                                                    Chart2.Series(1).Points.AddXY(xx(m), value)
                                                Next
                                            End If
                                            Exit For
                                        End If
                                    Next
                                End If                              
                            End If
                        End If
                    Next
                Else
                    Label11.Text = LV20.Items.Count
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub
    Dim TongJiCiShu As Integer = 0
    Private DeviceAddress As String
    Dim signalStartTime As Date
    Dim sigNalCount As Integer = 0
    Dim itmList As List(Of ListViewItem)
    Private Sub XinHao2LV(ByVal result(,) As Double)
        If IsNothing(result) Then Exit Sub
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
        LV20.Items.Clear()

        Dim cnt As Integer = plist.Count
        For i = 0 To cnt - 1
            Dim itm As ListViewItem = plist(i)
            itm.Text = i + 1
            Dim CBInt As Integer = Val(itm.SubItems(12).Text)
            Dim SumInt As Integer = Val(itm.SubItems(11).Text)
            Dim str As String = GetPerPic(CBInt / SumInt)
            itm.SubItems.Add(str)
            LV20.Items.Add(itm)
        Next

    End Sub
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

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        TongJiCiShu = 0
        sigNalCount = 0
        signalStartTime = Nothing
        itmList = Nothing
        iniChart()
        iniLV()
        Dim freqbegin As Double = Val(txtFreqStart.Text)
        Dim freqend As Double = Val(txtFreqEnd.Text)
        Dim freqstep As Double = Val(txtFreqStep.Text)
        Dim gcValue As Integer = 8
        Dim isHackOneSnigle As Boolean = False
        CtlFather.SendFreqOrder(freqbegin, freqend, freqstep, gcValue, isHackOneSnigle)
    End Sub
    Private Sub Panel17_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel17.Click
        iniChart()
        iniLV()
        Dim freqbegin As Double = Val(txtFreqStart.Text)
        Dim freqend As Double = Val(txtFreqEnd.Text)
        Dim freqstep As Double = Val(txtFreqStep.Text)
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

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click
        Export(1)
    End Sub
    Private Sub Panel23_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel23.Click
        Export(1)
    End Sub
    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click
        Export(2)
    End Sub
    Private Sub Panel25_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel25.Click
        Export(2)
    End Sub
    Private Sub Export(ByVal type As Integer)
        If type = 1 Then
            Dim list As New List(Of ListViewItem)
            For Each itm In LV20.Items
                list.Add(itm)
            Next
            Dim colCount As Integer = LV20.Columns.Count
            Dim SFD As New SaveFileDialog
            SFD.Filter = "*.docx|*.docx"
            Dim result = SFD.ShowDialog
            If result = DialogResult.OK Or result = DialogResult.Yes Then
                Dim path As String = SFD.FileName
                If File.Exists(path) Then File.Delete(path)
                Dim doc As Novacode.DocX = Novacode.DocX.Create(path)
                Dim startTime As String = list(0).SubItems(6).Text
                Dim endTime As String = list(0).SubItems(7).Text
                doc.InsertParagraph.AppendLine(" ● 信号信息统计表[" & startTime & " To " & endTime & "]").Font(New FontFamily("宋体")).FontSize(15)
                Dim tab As Novacode.Table = doc.AddTable(list.Count + 1, colCount - 2)
                tab.Design = Novacode.TableDesign.TableGrid
                tab.Alignment = Novacode.Alignment.center
                For i = 0 To colCount - 3
                    tab.Rows(0).Cells(i).FillColor = Color.FromArgb(226, 226, 226)
                Next
                For i = 0 To 5
                    Dim cName As String = LV20.Columns(i).Text
                    tab.Rows(0).Cells(i).Paragraphs(0).Append(cName).Bold().FontSize(7)
                Next
                For i = 6 To colCount - 3
                    Dim cName As String = LV20.Columns(i + 2).Text
                    tab.Rows(0).Cells(i).Paragraphs(0).Append(cName).Bold().FontSize(7)
                Next
                For i = 0 To list.Count - 1
                    Dim itm As ListViewItem = list(i)
                    For j = 0 To 5
                        Dim cValue As String = itm.SubItems(j).Text
                        tab.Rows(i + 1).Cells(j).Paragraphs(0).Append(cValue).FontSize(7)
                    Next
                    For j = 6 To colCount - 3
                        Dim cValue As String = itm.SubItems(j + 2).Text
                        tab.Rows(i + 1).Cells(j).Paragraphs(0).Append(cValue).FontSize(7)
                    Next
                Next
                doc.InsertParagraph.InsertTableAfterSelf(tab)
                doc.Save()
                MsgBox("已导出为Word文件")
            End If
        End If
        If type = 2 Then
            Dim list As New List(Of ListViewItem)
            For Each itm In LV20.Items
                list.Add(itm)
            Next
            Dim excel As New ExcelPackage
            Dim exSheet As ExcelWorksheet = excel.Workbook.Worksheets.Add("信号表")
            Dim colCount As Integer = LV20.Columns.Count
            For i = 0 To colCount - 1
                exSheet.Cells(1, i + 1).Value = LV20.Columns(i).Text
            Next
            For i = 0 To list.Count - 1
                Dim itm As ListViewItem = list(i)
                For j = 0 To colCount - 1
                    exSheet.Cells(i + 2, j + 1).Value = itm.SubItems(j).Text
                Next
            Next
            Dim SFD As New SaveFileDialog
            SFD.Filter = "*.xlsx|*.xlsx"
            Dim result = SFD.ShowDialog
            If result = DialogResult.OK Or result = DialogResult.Yes Then
                Dim path As String = SFD.FileName
                If File.Exists(path) Then File.Delete(path)
                excel.SaveAs(New FileInfo(path))
                MsgBox("已导出为Excel文件")
            End If
        End If
    End Sub

 
   
   
    Private Sub Panel17_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel17.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel19_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel19.Paint
        ControlPaint.DrawBorder(e.Graphics, sender.ClientRectangle, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid, Color.White, 1, ButtonBorderStyle.Solid)
    End Sub
End Class
