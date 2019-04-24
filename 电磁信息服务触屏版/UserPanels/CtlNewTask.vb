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
Public Class CtlNewTask
    Private selectCNT As Control
    Private selectDeviceKind As String = "TSS"
    Private selectIndex As Integer = 0
    Private selectColor As Color = Color.FromArgb(238, 238, 238)
    Private unSelectColor As Color = Color.Transparent
    Private selectSonColor As Color = Color.FromArgb(139, 209, 223)
    Private unSelectSonColor As Color = Color.FromArgb(0, 65, 106)
    Private Sub CtlNewTask_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Transparent
        Panel1.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        Dim p As New Padding
        p.All = 0
        TableLayoutPanel1.Margin = p
        SelectPanelFather(0)
        ini()
    End Sub
    Private Sub ini()
        PanOnlineDeviceList.AutoScroll = True
        Dim th As New Thread(AddressOf GetOnlineDeviceList)
        th.Start()

    End Sub
    Private Sub ShowDeviceListByKind(ByVal kind As String)
        For Each ctl As CtlDevice In PanOnlineDeviceList.Controls
            Dim itmKind As String = ctl.myDeviceInfo.Kind
            If itmKind.ToLower = kind.ToLower Then
                ctl.Visible = True
            Else
                ctl.Visible = False
            End If
        Next
    End Sub
    Private Sub GetOnlineDeviceList()
        Dim oldText As String = Label5.Text
        Label5.Text = "获取中……"
        Dim list As List(Of DeviceStu) = MZHelper.GetOnlineDeviceList
        If IsNothing(list) Then
            MsgBox("没有获取到在线设备")
            Label5.Text = oldText
            Return
        End If
        Label5.Text = oldText
        alldevlist = list
        PanOnlineDeviceList.Controls.Clear()
        For Each itm In list
            If itm.Kind.ToLower = "tzbq" Then
                Me.Invoke(Sub()
                              Dim d As New CtlDevice(itm)
                              AddHandler d.OnClick, AddressOf Device_OnClick
                              PanOnlineDeviceList.Controls.Add(d)
                              PanOnlineDeviceList.Controls.SetChildIndex(d, 0)
                          End Sub)
            End If
        Next
        For Each itm In list
            If itm.Kind.ToLower = "tss" Then
                Me.Invoke(Sub()
                              Dim d As New CtlDevice(itm)
                              AddHandler d.OnClick, AddressOf Device_OnClick
                              PanOnlineDeviceList.Controls.Add(d)
                              PanOnlineDeviceList.Controls.SetChildIndex(d, 0)
                          End Sub)
            End If
        Next
        Me.Invoke(Sub()
                      If selectIndex = 1 Or selectIndex = 2 Then
                          selectDeviceKind = "TZBQ"
                          ShowDeviceListByKind(selectDeviceKind)
                      Else
                          selectDeviceKind = "TSS"
                          ShowDeviceListByKind(selectDeviceKind)
                      End If
                  End Sub)
    End Sub
    Private Sub Device_OnClick(ByVal dstu As DeviceStu)
        If IsNothing(selectCNT) Then Return
        Dim index As Integer = selectIndex
        If index = 0 Then '频谱监测
            Dim clt As CNTFreqBscan = selectCNT
            clt.AddDevice(dstu)
        End If
        If index = 1 Then '可用评估s
            Dim clt As CNTKeyong = selectCNT
            clt.AddDevice(dstu)
        End If
        If index = 2 Then '占用统计
            Dim clt As CNTZhanyong = selectCNT
            clt.AddDevice(dstu)
        End If
        If index = 3 Then '黑广播捕获
            Dim clt As CNTHeiGuangBo = selectCNT
            clt.AddDevice(dstu)
        End If
        If index = 4 Then '违章捕获
            Dim clt As CNTWeiZhang = selectCNT
            clt.AddDevice(dstu)
        End If
    End Sub
    Private Sub SelectPanelFather(ByVal index As Integer)
        selectIndex = index
        Dim fList As New List(Of Panel)
        fList.Add(PanelTaskFather1)
        fList.Add(PanelTaskFather2)
        fList.Add(PanelTaskFather3)
        fList.Add(PanelTaskFather4)
        fList.Add(PanelTaskFather5)
        Dim sList As New List(Of Panel)
        sList.Add(PanelTaskSon1)
        sList.Add(PanelTaskSon2)
        sList.Add(PanelTaskSon3)
        sList.Add(PanelTaskSon4)
        sList.Add(PanelTaskSon5)
        For i = 0 To fList.Count - 1
            If i = index Then
                fList(i).BackColor = selectColor
                sList(i).BackColor = selectSonColor
            Else
                fList(i).BackColor = unSelectColor
                sList(i).BackColor = unSelectSonColor
            End If
        Next
        Dim p As New Padding
        p.All = 0
        For i = 0 To fList.Count - 1
            fList(i).Margin = p
            sList(i).Margin = p
        Next
        If index = 1 Then
            selectDeviceKind = "TZBQ"
            ShowDeviceListByKind(selectDeviceKind)
        Else
            selectDeviceKind = "TSS"
            ShowDeviceListByKind(selectDeviceKind)
        End If
        If index = 0 Then '频谱监测
            PanelTaskContent.Controls.Clear()
            selectCNT = New CNTFreqBscan
            PanelTaskContent.Controls.Add(selectCNT)
        End If
        If index = 1 Then '可用评估
            PanelTaskContent.Controls.Clear()
            selectCNT = New CNTKeyong
            PanelTaskContent.Controls.Add(selectCNT)
        End If
        If index = 2 Then '占用统计
            PanelTaskContent.Controls.Clear()
            selectCNT = New CNTZhanyong
            PanelTaskContent.Controls.Add(selectCNT)
        End If
        If index = 3 Then '黑广播捕获
            PanelTaskContent.Controls.Clear()
            selectCNT = New CNTHeiGuangBo
            PanelTaskContent.Controls.Add(selectCNT)
        End If
        If index = 4 Then '违章捕获
            PanelTaskContent.Controls.Clear()
            selectCNT = New CNTWeiZhang
            PanelTaskContent.Controls.Add(selectCNT)
        End If
    End Sub

    Private Sub PanelTaskSon1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelTaskSon1.Click
        SelectPanelFather(0)
    End Sub
    Private Sub PanelTaskSon2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelTaskSon2.Click
        SelectPanelFather(1)
    End Sub
    Private Sub PanelTaskSon3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelTaskSon3.Click
        SelectPanelFather(2)
    End Sub
    Private Sub PanelTaskSon4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelTaskSon4.Click
        SelectPanelFather(3)
    End Sub
    Private Sub PanelTaskSon5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PanelTaskSon5.Click
        SelectPanelFather(4)
    End Sub

    Private Sub PanelTaskContent_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelTaskContent.Paint
        Dim g As Graphics = e.Graphics
        'Dim FColor As Color = ColorTranslator.FromHtml("#5C5278")
        'Dim TColor As Color = ColorTranslator.FromHtml("#4FA9B5")
        Dim FColor As Color = Color.FromArgb(238, 238, 238)
        Dim TColor As Color = Color.FromArgb(141, 210, 223)
        Dim b As Brush = New LinearGradientBrush(Me.ClientRectangle, FColor, TColor, LinearGradientMode.Vertical)
        g.FillRectangle(b, Me.ClientRectangle)
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        Dim th As New Thread(AddressOf GetOnlineDeviceList)
        th.Start()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        SelectPanelFather(0)
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        SelectPanelFather(1)
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        SelectPanelFather(2)
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        SelectPanelFather(3)
    End Sub

    Private Sub Label34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label34.Click
        SelectPanelFather(4)
    End Sub
End Class
