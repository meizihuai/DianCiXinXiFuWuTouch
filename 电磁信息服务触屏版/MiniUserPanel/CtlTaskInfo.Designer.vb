﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtlTaskInfo
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
        Me.Panel50 = New System.Windows.Forms.Panel()
        Me.LblTime = New System.Windows.Forms.Label()
        Me.lblTaskNickName = New System.Windows.Forms.Label()
        Me.Panel50.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel50
        '
        Me.Panel50.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.Panel50.Controls.Add(Me.LblTime)
        Me.Panel50.Controls.Add(Me.lblTaskNickName)
        Me.Panel50.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel50.Location = New System.Drawing.Point(0, 4)
        Me.Panel50.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel50.Name = "Panel50"
        Me.Panel50.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel50.Size = New System.Drawing.Size(171, 43)
        Me.Panel50.TabIndex = 3
        '
        'LblTime
        '
        Me.LblTime.AutoSize = True
        Me.LblTime.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LblTime.ForeColor = System.Drawing.Color.White
        Me.LblTime.Location = New System.Drawing.Point(3, 23)
        Me.LblTime.Name = "LblTime"
        Me.LblTime.Size = New System.Drawing.Size(46, 17)
        Me.LblTime.TabIndex = 1
        Me.LblTime.Text = "Label2"
        '
        'lblTaskNickName
        '
        Me.lblTaskNickName.AutoSize = True
        Me.lblTaskNickName.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTaskNickName.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTaskNickName.ForeColor = System.Drawing.Color.White
        Me.lblTaskNickName.Location = New System.Drawing.Point(3, 3)
        Me.lblTaskNickName.Name = "lblTaskNickName"
        Me.lblTaskNickName.Size = New System.Drawing.Size(48, 17)
        Me.lblTaskNickName.TabIndex = 0
        Me.lblTaskNickName.Text = "Label1"
        '
        'CtlTaskInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel50)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "CtlTaskInfo"
        Me.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.Size = New System.Drawing.Size(171, 51)
        Me.Panel50.ResumeLayout(False)
        Me.Panel50.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel50 As System.Windows.Forms.Panel
    Friend WithEvents LblTime As System.Windows.Forms.Label
    Friend WithEvents lblTaskNickName As System.Windows.Forms.Label

End Class
