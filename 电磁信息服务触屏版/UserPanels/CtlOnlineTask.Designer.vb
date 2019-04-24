<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtlTask
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtlTask))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.WebGis = New System.Windows.Forms.WebBrowser()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel29 = New System.Windows.Forms.Panel()
        Me.Panel27 = New System.Windows.Forms.Panel()
        Me.Panel51 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel27.SuspendLayout()
        Me.Panel51.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Panel1.Controls.Add(Me.WebGis)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Panel1.Size = New System.Drawing.Size(926, 614)
        Me.Panel1.TabIndex = 0
        '
        'WebGis
        '
        Me.WebGis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebGis.Location = New System.Drawing.Point(0, 4)
        Me.WebGis.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.WebGis.MinimumSize = New System.Drawing.Size(23, 28)
        Me.WebGis.Name = "WebGis"
        Me.WebGis.Size = New System.Drawing.Size(669, 610)
        Me.WebGis.TabIndex = 3
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel29)
        Me.Panel7.Controls.Add(Me.Panel27)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel7.Location = New System.Drawing.Point(669, 4)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        Me.Panel7.Size = New System.Drawing.Size(257, 610)
        Me.Panel7.TabIndex = 2
        '
        'Panel29
        '
        Me.Panel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel29.Location = New System.Drawing.Point(12, 47)
        Me.Panel29.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel29.Name = "Panel29"
        Me.Panel29.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel29.Size = New System.Drawing.Size(245, 563)
        Me.Panel29.TabIndex = 2
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
        Me.Label5.Location = New System.Drawing.Point(41, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(132, 27)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "在线任务列表"
        '
        'PictureBox3
        '
        Me.PictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(179, 11)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 21
        Me.PictureBox3.TabStop = False
        '
        'CtlTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "CtlTask"
        Me.Size = New System.Drawing.Size(926, 614)
        Me.Panel1.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel27.ResumeLayout(False)
        Me.Panel51.ResumeLayout(False)
        Me.Panel51.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Friend WithEvents Panel51 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents WebGis As System.Windows.Forms.WebBrowser
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox

End Class
