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

Public Class CtlDuty
    Private selectCtlLock As Object
    Private selectIndex As Integer
    Private selectColor As Color = Color.FromArgb(0, 86, 131)
    Private unSelectColor As Color = Color.FromArgb(0, 65, 106)
    Private Sub CtlDuty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Fill
        selectCtlLock = New Object
        SelectPanelTabIndex(0)
    End Sub
    Private Sub SelectPanelTabIndex(ByVal index As Integer)
        Dim pList As New List(Of Panel)
        pList.Add(Ptn0)
        pList.Add(Ptn1)
        pList.Add(Ptn2)
        pList.Add(Ptn3)
        For i = 0 To pList.Count - 1
            If index = i Then
                pList(i).BackColor = selectColor
            Else
                pList(i).BackColor = unSelectColor
            End If
        Next
        selectIndex = index
        SyncLock selectCtlLock
            If index = 0 Then '全网全站          
                Panel5.Controls.Clear()
                Dim ctl As New OtherSystemDevice
                Panel5.Controls.Add(ctl)
            End If
            If index = 1 Then '值班日志
                Panel5.Controls.Clear()
                Dim ctl As New LogPanel
                Panel5.Controls.Add(ctl)
            End If
            If index = 2 Then '在线事件
                Panel5.Controls.Clear()
                Dim ctl As New Allwarns
                Panel5.Controls.Add(ctl)
            End If
            If index = 3 Then '系统账号
                Panel5.Controls.Clear()
                Dim ctl As New usrManager
                Panel5.Controls.Add(ctl)
            End If
        End SyncLock
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

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        SelectPanelTabIndex(0)
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        SelectPanelTabIndex(1)
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        SelectPanelTabIndex(2)
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        SelectPanelTabIndex(3)
    End Sub
End Class
