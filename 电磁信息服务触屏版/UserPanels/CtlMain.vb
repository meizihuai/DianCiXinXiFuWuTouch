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
Public Class CtlMain
    Public Event OnSelectDeviceChange(ByVal dstu As DeviceStu)
    Public Event OnGuidChange(ByVal gName As String)
    Private Sub CtlMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        '  PanOnlineDeviceList.AutoScroll = False
        If isLoadGis Then
            WebGis.Navigate(gisurl)
        Else
            ini()
        End If
    End Sub

    Private Sub WebGis_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebGis.DocumentCompleted
        ini()
    End Sub
    Private Sub ini()
        lblTssOnline.Text = 0
        LblTssOfflinePercent.Text = "0%"
        lblTssOffline.Text = 0
        LblTssOfflinePercent.Text = "0%"
        lblTssBroken.Text = 0
        LblTssBrokenPercent.Text = "0%"
        LblTssSum.Text = 0
        GetOnlineDeviceList()
    End Sub
    Private Sub GetOnlineDeviceList()
        Dim list As List(Of DeviceStu) = MZHelper.GetOnlineDeviceList
        If IsNothing(list) Then
            MsgBox("没有获取到在线设备")
            Return
        End If
        PanOnlineDeviceList.Controls.Clear()
        Dim OnLineTss As Integer = 0
        Dim OnlineTZBQ As Integer = 0
        Dim SumTss As Integer = 0
        Dim SumTZBQ As Integer = 0
        CleanGis(Me, WebGis)
        For Each itm In list
            If itm.Kind.ToLower = "tzbq" Then
                If isLoadGis Then AddNewIco(Me, itm.Lng, itm.Lat, itm.Name, TZBQIco, WebGis)
                Dim d As New CtlDevice(itm)
                AddHandler d.OnClick, AddressOf Device_OnClick
                PanOnlineDeviceList.Controls.Add(d)
                PanOnlineDeviceList.Controls.SetChildIndex(d, 0)
                OnlineTZBQ = OnlineTZBQ + 1
            End If
        Next
        Dim flagSelected As Boolean = False
        For Each itm In list
            If itm.Kind.ToLower = "tss" Then
                If isLoadGis Then AddNewIco(Me, itm.Lng, itm.Lat, itm.Name, TssIco, WebGis)
                Dim d As New CtlDevice(itm)
                AddHandler d.OnClick, AddressOf Device_OnClick
                PanOnlineDeviceList.Controls.Add(d)
                PanOnlineDeviceList.Controls.SetChildIndex(d, 0)
                OnLineTss = OnLineTss + 1
                If Not flagSelected Then
                    flagSelected = True
                    Device_OnClick(itm)
                End If
            End If
        Next
        Dim dt As DataTable = MZHelper.GetAllDBDeviceList
        If IsNothing(dt) Then
            SumTss = OnLineTss
            SumTZBQ = OnlineTZBQ
        Else
            For Each row As DataRow In dt.Rows
                Dim kind As String = row("Kind")
                If kind = "TZBQ" Then
                    SumTZBQ = SumTZBQ + 1
                End If
                If kind = "TSS" Then
                    SumTss = SumTss + 1
                End If
            Next
        End If

        lblTssOnline.Text = OnLineTss
        LblTssOnlinePercent.Text = (100 * OnLineTss / SumTss).ToString("0.0") & "%"
        lblTssOffline.Text = SumTss - OnLineTss
        LblTssOfflinePercent.Text = (100 - (100 * OnLineTss / SumTss).ToString("0.0")) & "%"
        lblTssBroken.Text = 0
        LblTssBrokenPercent.Text = "0%"
        LblTssSum.Text = SumTss
    End Sub

    Private Sub Device_OnClick(ByVal dstu As DeviceStu)
        setGisCenter3(Me, dstu.Lng, dstu.Lat, 11, WebGis)
        If IsNothing(selectedDeviceStu) Then
            RaiseEvent OnSelectDeviceChange(dstu)
        Else
            If selectedDeviceStu.DeviceID <> dstu.DeviceID Then
                RaiseEvent OnSelectDeviceChange(dstu)
            End If
        End If
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        GetOnlineDeviceList()
    End Sub

    Private Sub PanOnlineDeviceList_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanOnlineDeviceList.MouseWheel
        If e.Delta > 0 Then '向上滚动

        End If
        If e.Delta < 0 Then '向下滚动

        End If
    End Sub

    Private Sub PanOnlineDeviceList_Paint(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanOnlineDeviceList.Paint

    End Sub

    Private Sub Panel14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel14.Click
        RaiseEvent OnGuidChange("任务管理")
    End Sub

    Private Sub Label38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label38.Click
        RaiseEvent OnGuidChange("任务管理")
    End Sub

    Private Sub Panel24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel24.Click
        RaiseEvent OnGuidChange("监测服务")
    End Sub

    Private Sub Label37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label37.Click
        RaiseEvent OnGuidChange("监测服务")
    End Sub

    Private Sub Panel22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel22.Click
        RaiseEvent OnGuidChange("设备管理")
    End Sub

    Private Sub Label39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label39.Click
        RaiseEvent OnGuidChange("设备管理")
    End Sub

    Private Sub Panel25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel25.Click
        RaiseEvent OnGuidChange("数据服务")
    End Sub

    Private Sub Label40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label40.Click
        RaiseEvent OnGuidChange("数据服务")
    End Sub

    Private Sub Panel23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel23.Click
        RaiseEvent OnGuidChange("公交系统")
    End Sub

    Private Sub Label42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label42.Click
        RaiseEvent OnGuidChange("公交系统")
    End Sub

    Private Sub Panel26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel26.Click
        RaiseEvent OnGuidChange("信息服务")
    End Sub

    Private Sub Label41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label41.Click
        RaiseEvent OnGuidChange("信息服务")
    End Sub

    Private Sub Panel21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel21.Click
        RaiseEvent OnGuidChange("值班管理")
    End Sub

    Private Sub Label34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label34.Click
        RaiseEvent OnGuidChange("值班管理")
    End Sub

    Private Sub Label35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label35.Click
        RaiseEvent OnGuidChange("值班管理")
    End Sub

    Private Sub Label36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label36.Click
        RaiseEvent OnGuidChange("值班管理")
    End Sub

   
    Private Sub Panel24_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel24.Paint

    End Sub
End Class
