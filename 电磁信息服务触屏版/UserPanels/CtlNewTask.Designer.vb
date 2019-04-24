<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtlNewTask
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtlNewTask))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelTaskContent = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelTaskFather5 = New System.Windows.Forms.Panel()
        Me.PanelTaskSon5 = New System.Windows.Forms.Panel()
        Me.PanelTaskFather4 = New System.Windows.Forms.Panel()
        Me.PanelTaskSon4 = New System.Windows.Forms.Panel()
        Me.PanelTaskFather3 = New System.Windows.Forms.Panel()
        Me.PanelTaskSon3 = New System.Windows.Forms.Panel()
        Me.PanelTaskFather2 = New System.Windows.Forms.Panel()
        Me.PanelTaskSon2 = New System.Windows.Forms.Panel()
        Me.PanelTaskFather1 = New System.Windows.Forms.Panel()
        Me.PanelTaskSon1 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.PanOnlineDeviceList = New System.Windows.Forms.Panel()
        Me.Panel27 = New System.Windows.Forms.Panel()
        Me.Panel51 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.PanelTaskFather5.SuspendLayout()
        Me.PanelTaskSon5.SuspendLayout()
        Me.PanelTaskFather4.SuspendLayout()
        Me.PanelTaskSon4.SuspendLayout()
        Me.PanelTaskFather3.SuspendLayout()
        Me.PanelTaskSon3.SuspendLayout()
        Me.PanelTaskFather2.SuspendLayout()
        Me.PanelTaskSon2.SuspendLayout()
        Me.PanelTaskFather1.SuspendLayout()
        Me.PanelTaskSon1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel27.SuspendLayout()
        Me.Panel51.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Panel1.Controls.Add(Me.PanelTaskContent)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Panel1.Size = New System.Drawing.Size(909, 668)
        Me.Panel1.TabIndex = 1
        '
        'PanelTaskContent
        '
        Me.PanelTaskContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskContent.Location = New System.Drawing.Point(0, 117)
        Me.PanelTaskContent.Name = "PanelTaskContent"
        Me.PanelTaskContent.Size = New System.Drawing.Size(652, 551)
        Me.PanelTaskContent.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(652, 113)
        Me.Panel2.TabIndex = 3
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelTaskFather5, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelTaskFather4, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelTaskFather3, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelTaskFather2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelTaskFather1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(652, 113)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'PanelTaskFather5
        '
        Me.PanelTaskFather5.Controls.Add(Me.PanelTaskSon5)
        Me.PanelTaskFather5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskFather5.Location = New System.Drawing.Point(523, 3)
        Me.PanelTaskFather5.Name = "PanelTaskFather5"
        Me.PanelTaskFather5.Padding = New System.Windows.Forms.Padding(10)
        Me.PanelTaskFather5.Size = New System.Drawing.Size(126, 107)
        Me.PanelTaskFather5.TabIndex = 4
        '
        'PanelTaskSon5
        '
        Me.PanelTaskSon5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(106, Byte), Integer))
        Me.PanelTaskSon5.Controls.Add(Me.Label34)
        Me.PanelTaskSon5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PanelTaskSon5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskSon5.Location = New System.Drawing.Point(10, 10)
        Me.PanelTaskSon5.Name = "PanelTaskSon5"
        Me.PanelTaskSon5.Size = New System.Drawing.Size(106, 87)
        Me.PanelTaskSon5.TabIndex = 1
        '
        'PanelTaskFather4
        '
        Me.PanelTaskFather4.Controls.Add(Me.PanelTaskSon4)
        Me.PanelTaskFather4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskFather4.Location = New System.Drawing.Point(393, 3)
        Me.PanelTaskFather4.Name = "PanelTaskFather4"
        Me.PanelTaskFather4.Padding = New System.Windows.Forms.Padding(10)
        Me.PanelTaskFather4.Size = New System.Drawing.Size(124, 107)
        Me.PanelTaskFather4.TabIndex = 3
        '
        'PanelTaskSon4
        '
        Me.PanelTaskSon4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(106, Byte), Integer))
        Me.PanelTaskSon4.Controls.Add(Me.Label4)
        Me.PanelTaskSon4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PanelTaskSon4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskSon4.Location = New System.Drawing.Point(10, 10)
        Me.PanelTaskSon4.Name = "PanelTaskSon4"
        Me.PanelTaskSon4.Size = New System.Drawing.Size(104, 87)
        Me.PanelTaskSon4.TabIndex = 1
        '
        'PanelTaskFather3
        '
        Me.PanelTaskFather3.Controls.Add(Me.PanelTaskSon3)
        Me.PanelTaskFather3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskFather3.Location = New System.Drawing.Point(263, 3)
        Me.PanelTaskFather3.Name = "PanelTaskFather3"
        Me.PanelTaskFather3.Padding = New System.Windows.Forms.Padding(10)
        Me.PanelTaskFather3.Size = New System.Drawing.Size(124, 107)
        Me.PanelTaskFather3.TabIndex = 2
        '
        'PanelTaskSon3
        '
        Me.PanelTaskSon3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(106, Byte), Integer))
        Me.PanelTaskSon3.Controls.Add(Me.Label3)
        Me.PanelTaskSon3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PanelTaskSon3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskSon3.Location = New System.Drawing.Point(10, 10)
        Me.PanelTaskSon3.Name = "PanelTaskSon3"
        Me.PanelTaskSon3.Size = New System.Drawing.Size(104, 87)
        Me.PanelTaskSon3.TabIndex = 1
        '
        'PanelTaskFather2
        '
        Me.PanelTaskFather2.Controls.Add(Me.PanelTaskSon2)
        Me.PanelTaskFather2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskFather2.Location = New System.Drawing.Point(133, 3)
        Me.PanelTaskFather2.Name = "PanelTaskFather2"
        Me.PanelTaskFather2.Padding = New System.Windows.Forms.Padding(10)
        Me.PanelTaskFather2.Size = New System.Drawing.Size(124, 107)
        Me.PanelTaskFather2.TabIndex = 1
        '
        'PanelTaskSon2
        '
        Me.PanelTaskSon2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(106, Byte), Integer))
        Me.PanelTaskSon2.Controls.Add(Me.Label2)
        Me.PanelTaskSon2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PanelTaskSon2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskSon2.Location = New System.Drawing.Point(10, 10)
        Me.PanelTaskSon2.Name = "PanelTaskSon2"
        Me.PanelTaskSon2.Size = New System.Drawing.Size(104, 87)
        Me.PanelTaskSon2.TabIndex = 1
        '
        'PanelTaskFather1
        '
        Me.PanelTaskFather1.Controls.Add(Me.PanelTaskSon1)
        Me.PanelTaskFather1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskFather1.Location = New System.Drawing.Point(3, 3)
        Me.PanelTaskFather1.Name = "PanelTaskFather1"
        Me.PanelTaskFather1.Padding = New System.Windows.Forms.Padding(10)
        Me.PanelTaskFather1.Size = New System.Drawing.Size(124, 107)
        Me.PanelTaskFather1.TabIndex = 0
        '
        'PanelTaskSon1
        '
        Me.PanelTaskSon1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(106, Byte), Integer))
        Me.PanelTaskSon1.Controls.Add(Me.Label1)
        Me.PanelTaskSon1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PanelTaskSon1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTaskSon1.Location = New System.Drawing.Point(10, 10)
        Me.PanelTaskSon1.Name = "PanelTaskSon1"
        Me.PanelTaskSon1.Size = New System.Drawing.Size(104, 87)
        Me.PanelTaskSon1.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.PanOnlineDeviceList)
        Me.Panel7.Controls.Add(Me.Panel27)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel7.Location = New System.Drawing.Point(652, 4)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        Me.Panel7.Size = New System.Drawing.Size(257, 664)
        Me.Panel7.TabIndex = 2
        '
        'PanOnlineDeviceList
        '
        Me.PanOnlineDeviceList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanOnlineDeviceList.Location = New System.Drawing.Point(12, 47)
        Me.PanOnlineDeviceList.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanOnlineDeviceList.Name = "PanOnlineDeviceList"
        Me.PanOnlineDeviceList.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanOnlineDeviceList.Size = New System.Drawing.Size(245, 617)
        Me.PanOnlineDeviceList.TabIndex = 2
        '
        'Panel27
        '
        Me.Panel27.Controls.Add(Me.Panel51)
        Me.Panel27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel27.Location = New System.Drawing.Point(12, 0)
        Me.Panel27.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel27.Name = "Panel27"
        Me.Panel27.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Panel27.Size = New System.Drawing.Size(245, 47)
        Me.Panel27.TabIndex = 0
        '
        'Panel51
        '
        Me.Panel51.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(106, Byte), Integer))
        Me.Panel51.Controls.Add(Me.PictureBox3)
        Me.Panel51.Controls.Add(Me.Label5)
        Me.Panel51.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel51.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel51.Location = New System.Drawing.Point(3, 0)
        Me.Panel51.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel51.Name = "Panel51"
        Me.Panel51.Size = New System.Drawing.Size(239, 47)
        Me.Panel51.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("微软雅黑 Light", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(63, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 27)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "待选设备"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(6, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 27)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "频谱监测"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(6, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 27)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "可用评估"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(6, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 27)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "占用统计"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(-3, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 27)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "黑广播捕获"
        '
        'Label34
        '
        Me.Label34.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.White
        Me.Label34.Location = New System.Drawing.Point(7, 30)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(92, 27)
        Me.Label34.TabIndex = 5
        Me.Label34.Text = "违章捕获"
        '
        'PictureBox3
        '
        Me.PictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(156, 11)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 21
        Me.PictureBox3.TabStop = False
        '
        'CtlNewTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "CtlNewTask"
        Me.Size = New System.Drawing.Size(909, 668)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.PanelTaskFather5.ResumeLayout(False)
        Me.PanelTaskSon5.ResumeLayout(False)
        Me.PanelTaskSon5.PerformLayout()
        Me.PanelTaskFather4.ResumeLayout(False)
        Me.PanelTaskSon4.ResumeLayout(False)
        Me.PanelTaskSon4.PerformLayout()
        Me.PanelTaskFather3.ResumeLayout(False)
        Me.PanelTaskSon3.ResumeLayout(False)
        Me.PanelTaskSon3.PerformLayout()
        Me.PanelTaskFather2.ResumeLayout(False)
        Me.PanelTaskSon2.ResumeLayout(False)
        Me.PanelTaskSon2.PerformLayout()
        Me.PanelTaskFather1.ResumeLayout(False)
        Me.PanelTaskSon1.ResumeLayout(False)
        Me.PanelTaskSon1.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel27.ResumeLayout(False)
        Me.Panel51.ResumeLayout(False)
        Me.Panel51.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents PanOnlineDeviceList As System.Windows.Forms.Panel
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Friend WithEvents Panel51 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PanelTaskFather5 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskFather4 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskFather3 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskFather2 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskFather1 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskSon1 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskSon5 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskSon4 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskSon3 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskSon2 As System.Windows.Forms.Panel
    Friend WithEvents PanelTaskContent As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox

End Class
