<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CNTFreqBscan
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.CBDevices = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTaskBg = New System.Windows.Forms.RichTextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtFreqStep = New System.Windows.Forms.TextBox()
        Me.txtFreqEnd = New System.Windows.Forms.TextBox()
        Me.txtFreqStart = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtEmailName = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtWechatName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtEndTime = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtStartTime = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNicName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.txtTaskBg)
        Me.Panel1.Controls.Add(Me.Label69)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.txtFreqStep)
        Me.Panel1.Controls.Add(Me.txtFreqEnd)
        Me.Panel1.Controls.Add(Me.txtFreqStart)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.txtEmailName)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtWechatName)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtEndTime)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtStartTime)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtNicName)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(694, 536)
        Me.Panel1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.ProgressBar1)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Location = New System.Drawing.Point(35, 475)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(479, 45)
        Me.Panel2.TabIndex = 73
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(306, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(154, 20)
        Me.Label10.TabIndex = 73
        Me.Label10.Text = "成功:0，失败:0，共计:0"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(111, 9)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(189, 20)
        Me.ProgressBar1.TabIndex = 72
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 20)
        Me.Label6.TabIndex = 71
        Me.Label6.Text = "任务下发进度:"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(307, 125)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(58, 34)
        Me.Button5.TabIndex = 70
        Me.Button5.Text = "删除"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(307, 83)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(58, 34)
        Me.Button4.TabIndex = 69
        Me.Button4.Text = "反选"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(307, 41)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(58, 34)
        Me.Button3.TabIndex = 68
        Me.Button3.Text = "全选"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'CBDevices
        '
        Me.CBDevices.CheckOnClick = True
        Me.CBDevices.FormattingEnabled = True
        Me.CBDevices.HorizontalScrollbar = True
        Me.CBDevices.Location = New System.Drawing.Point(100, 8)
        Me.CBDevices.Name = "CBDevices"
        Me.CBDevices.Size = New System.Drawing.Size(201, 151)
        Me.CBDevices.TabIndex = 67
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 20)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "(右边点选)"
        '
        'txtTaskBg
        '
        Me.txtTaskBg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTaskBg.Location = New System.Drawing.Point(111, 283)
        Me.txtTaskBg.Name = "txtTaskBg"
        Me.txtTaskBg.Size = New System.Drawing.Size(179, 117)
        Me.txtTaskBg.TabIndex = 64
        Me.txtTaskBg.Text = ""
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(44, 286)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(68, 20)
        Me.Label69.TabIndex = 63
        Me.Label69.Text = "任务背景:"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(44, 419)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(454, 40)
        Me.Label29.TabIndex = 62
        Me.Label29.Text = "1.任务结束，生成《频谱监测报告》通过微信，邮件以及浏览器发布共享" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2.评估历史数据，通过""数据服务""功能回放浏览"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(320, 157)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(105, 20)
        Me.Label20.TabIndex = 57
        Me.Label20.Text = "频率步进(KHz):"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(320, 113)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(110, 20)
        Me.Label19.TabIndex = 56
        Me.Label19.Text = "终止频率(MHz):"
        '
        'txtFreqStep
        '
        Me.txtFreqStep.Location = New System.Drawing.Point(432, 154)
        Me.txtFreqStep.Name = "txtFreqStep"
        Me.txtFreqStep.Size = New System.Drawing.Size(179, 26)
        Me.txtFreqStep.TabIndex = 55
        '
        'txtFreqEnd
        '
        Me.txtFreqEnd.Location = New System.Drawing.Point(432, 110)
        Me.txtFreqEnd.Name = "txtFreqEnd"
        Me.txtFreqEnd.Size = New System.Drawing.Size(179, 26)
        Me.txtFreqEnd.TabIndex = 53
        '
        'txtFreqStart
        '
        Me.txtFreqStart.Location = New System.Drawing.Point(432, 66)
        Me.txtFreqStart.Name = "txtFreqStart"
        Me.txtFreqStart.Size = New System.Drawing.Size(179, 26)
        Me.txtFreqStart.TabIndex = 51
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(320, 69)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(110, 20)
        Me.Label21.TabIndex = 50
        Me.Label21.Text = "起始频率(MHz):"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(532, 473)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(79, 34)
        Me.Button1.TabIndex = 48
        Me.Button1.Text = "下发任务"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtEmailName
        '
        Me.txtEmailName.Location = New System.Drawing.Point(111, 242)
        Me.txtEmailName.Name = "txtEmailName"
        Me.txtEmailName.Size = New System.Drawing.Size(179, 26)
        Me.txtEmailName.TabIndex = 41
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(44, 245)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 20)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "邮件通知:"
        '
        'txtWechatName
        '
        Me.txtWechatName.Location = New System.Drawing.Point(111, 198)
        Me.txtWechatName.Name = "txtWechatName"
        Me.txtWechatName.Size = New System.Drawing.Size(179, 26)
        Me.txtWechatName.TabIndex = 39
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(44, 201)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 20)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "微信通知:"
        '
        'txtEndTime
        '
        Me.txtEndTime.Location = New System.Drawing.Point(111, 155)
        Me.txtEndTime.Name = "txtEndTime"
        Me.txtEndTime.Size = New System.Drawing.Size(179, 26)
        Me.txtEndTime.TabIndex = 35
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(44, 158)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 20)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "结束时间:"
        '
        'txtStartTime
        '
        Me.txtStartTime.Location = New System.Drawing.Point(111, 111)
        Me.txtStartTime.Name = "txtStartTime"
        Me.txtStartTime.Size = New System.Drawing.Size(179, 26)
        Me.txtStartTime.TabIndex = 33
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(44, 114)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 20)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "开始时间:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 20)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "执行设备:"
        '
        'txtNicName
        '
        Me.txtNicName.Location = New System.Drawing.Point(111, 66)
        Me.txtNicName.Name = "txtNicName"
        Me.txtNicName.Size = New System.Drawing.Size(179, 26)
        Me.txtNicName.TabIndex = 30
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(44, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 20)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "任务备注:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(44, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 20)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "任务类型:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(109, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "频谱监测"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Button5)
        Me.Panel3.Controls.Add(Me.Button4)
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.CBDevices)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Location = New System.Drawing.Point(310, 227)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(371, 173)
        Me.Panel3.TabIndex = 74
        '
        'CNTFreqBscan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "CNTFreqBscan"
        Me.Size = New System.Drawing.Size(694, 536)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents CBDevices As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTaskBg As System.Windows.Forms.RichTextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtFreqStep As System.Windows.Forms.TextBox
    Friend WithEvents txtFreqEnd As System.Windows.Forms.TextBox
    Friend WithEvents txtFreqStart As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtEmailName As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtWechatName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEndTime As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtStartTime As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNicName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel

End Class
