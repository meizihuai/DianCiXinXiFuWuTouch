﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BusFreqGis
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写释放以清理组件列表。
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
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BusFreqGis))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.WebGis = New System.Windows.Forms.WebBrowser()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lbl_Time = New System.Windows.Forms.Label()
        Me.lbl_Lat = New System.Windows.Forms.Label()
        Me.lbl_Lng = New System.Windows.Forms.Label()
        Me.lbl_DeviceName = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.lbl_LineName = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.选择该条线路ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LVDetail = New System.Windows.Forms.ListView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel6.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(3, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 20)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "公交监测路线"
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.WebGis)
        Me.Panel6.Controls.Add(Me.Panel8)
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(5, 51)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(721, 671)
        Me.Panel6.TabIndex = 11
        '
        'WebGis
        '
        Me.WebGis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebGis.Location = New System.Drawing.Point(0, 0)
        Me.WebGis.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebGis.Name = "WebGis"
        Me.WebGis.Size = New System.Drawing.Size(719, 362)
        Me.WebGis.TabIndex = 12
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(61, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.Panel8.Controls.Add(Me.Label4)
        Me.Panel8.Controls.Add(Me.Label11)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel8.Location = New System.Drawing.Point(0, 362)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(719, 51)
        Me.Panel8.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(83, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 20)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "频谱信息"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(3, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 20)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "实时频谱"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Chart1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(0, 413)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(719, 256)
        Me.Panel7.TabIndex = 0
        '
        'Chart1
        '
        ChartArea1.AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.Position
        ChartArea1.InnerPlotPosition.Auto = False
        ChartArea1.InnerPlotPosition.Height = 86.0!
        ChartArea1.InnerPlotPosition.Width = 96.0!
        ChartArea1.InnerPlotPosition.X = 4.0!
        ChartArea1.InnerPlotPosition.Y = 4.0!
        ChartArea1.Name = "ChartArea1"
        ChartArea1.Position.Auto = False
        ChartArea1.Position.Height = 100.0!
        ChartArea1.Position.Width = 98.0!
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(0, 0)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(719, 256)
        Me.Chart1.TabIndex = 3
        Me.Chart1.Text = "Chart1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(313, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(726, 722)
        Me.Panel2.TabIndex = 3
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(61, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.Panel5.Controls.Add(Me.CheckBox1)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(5, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(721, 51)
        Me.Panel5.TabIndex = 10
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(85, 17)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(75, 21)
        Me.CheckBox1.TabIndex = 14
        Me.CheckBox1.Text = "固定视图"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(3, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 20)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "频谱地图"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkColor = System.Drawing.Color.Lime
        Me.LinkLabel1.Location = New System.Drawing.Point(117, 167)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(128, 17)
        Me.LinkLabel1.TabIndex = 15
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "打开当前位置历史频谱"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(67, 167)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(44, 17)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "操作："
        '
        'lbl_Time
        '
        Me.lbl_Time.AutoSize = True
        Me.lbl_Time.Location = New System.Drawing.Point(117, 138)
        Me.lbl_Time.Name = "lbl_Time"
        Me.lbl_Time.Size = New System.Drawing.Size(44, 17)
        Me.lbl_Time.TabIndex = 12
        Me.lbl_Time.Text = "未选择"
        '
        'lbl_Lat
        '
        Me.lbl_Lat.AutoSize = True
        Me.lbl_Lat.Location = New System.Drawing.Point(117, 108)
        Me.lbl_Lat.Name = "lbl_Lat"
        Me.lbl_Lat.Size = New System.Drawing.Size(44, 17)
        Me.lbl_Lat.TabIndex = 11
        Me.lbl_Lat.Text = "未选择"
        '
        'lbl_Lng
        '
        Me.lbl_Lng.AutoSize = True
        Me.lbl_Lng.Location = New System.Drawing.Point(117, 78)
        Me.lbl_Lng.Name = "lbl_Lng"
        Me.lbl_Lng.Size = New System.Drawing.Size(44, 17)
        Me.lbl_Lng.TabIndex = 10
        Me.lbl_Lng.Text = "未选择"
        '
        'lbl_DeviceName
        '
        Me.lbl_DeviceName.AutoSize = True
        Me.lbl_DeviceName.Location = New System.Drawing.Point(117, 48)
        Me.lbl_DeviceName.Name = "lbl_DeviceName"
        Me.lbl_DeviceName.Size = New System.Drawing.Size(44, 17)
        Me.lbl_DeviceName.TabIndex = 9
        Me.lbl_DeviceName.Text = "未选择"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(67, 138)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 17)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "时间："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(67, 108)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 17)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "纬度："
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(67, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 17)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "经度："
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 17)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "当前线路设备："
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(61, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.PictureBox3)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(308, 51)
        Me.Panel3.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(190, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 20)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "获取中……"
        '
        'PictureBox3
        '
        Me.PictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(270, 13)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 12
        Me.PictureBox3.TabStop = False
        '
        'lbl_LineName
        '
        Me.lbl_LineName.AutoSize = True
        Me.lbl_LineName.Location = New System.Drawing.Point(117, 18)
        Me.lbl_LineName.Name = "lbl_LineName"
        Me.lbl_LineName.Size = New System.Drawing.Size(44, 17)
        Me.lbl_LineName.TabIndex = 4
        Me.lbl_LineName.Text = "未选择"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "当前选择路线："
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.LinkLabel1)
        Me.Panel9.Controls.Add(Me.Label12)
        Me.Panel9.Controls.Add(Me.lbl_Time)
        Me.Panel9.Controls.Add(Me.lbl_Lat)
        Me.Panel9.Controls.Add(Me.lbl_Lng)
        Me.Panel9.Controls.Add(Me.lbl_DeviceName)
        Me.Panel9.Controls.Add(Me.Label10)
        Me.Panel9.Controls.Add(Me.Label9)
        Me.Panel9.Controls.Add(Me.Label8)
        Me.Panel9.Controls.Add(Me.Label7)
        Me.Panel9.Controls.Add(Me.lbl_LineName)
        Me.Panel9.Controls.Add(Me.Label3)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.ForeColor = System.Drawing.Color.White
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(306, 211)
        Me.Panel9.TabIndex = 0
        '
        '选择该条线路ToolStripMenuItem
        '
        Me.选择该条线路ToolStripMenuItem.Name = "选择该条线路ToolStripMenuItem"
        Me.选择该条线路ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.选择该条线路ToolStripMenuItem.Text = "选择该条线路"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.选择该条线路ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(149, 26)
        '
        'LVDetail
        '
        Me.LVDetail.ContextMenuStrip = Me.ContextMenuStrip1
        Me.LVDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LVDetail.Location = New System.Drawing.Point(0, 211)
        Me.LVDetail.Name = "LVDetail"
        Me.LVDetail.Size = New System.Drawing.Size(306, 458)
        Me.LVDetail.TabIndex = 1
        Me.LVDetail.UseCompatibleStateImageBehavior = False
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.LVDetail)
        Me.Panel4.Controls.Add(Me.Panel9)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 51)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(308, 671)
        Me.Panel4.TabIndex = 10
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(308, 722)
        Me.Panel1.TabIndex = 2
        '
        'BusFreqGis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "BusFreqGis"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.Size = New System.Drawing.Size(1044, 732)
        Me.Panel6.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents WebGis As WebBrowser
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Label12 As Label
    Friend WithEvents lbl_Time As Label
    Friend WithEvents lbl_Lat As Label
    Friend WithEvents lbl_Lng As Label
    Friend WithEvents lbl_DeviceName As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents lbl_LineName As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents 选择该条线路ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents LVDetail As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel1 As Panel
End Class
