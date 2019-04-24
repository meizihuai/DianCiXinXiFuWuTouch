Public Class CtlDevice
    Public myDeviceInfo As DeviceStu
    Public Event OnClick(ByVal dstu As DeviceStu)

    Sub New(ByVal _myDeviceInfo As DeviceStu)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        myDeviceInfo = _myDeviceInfo
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub


    Private Sub CtlDevice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackColor = Color.Transparent
        Me.Dock = DockStyle.Top
        Me.Cursor = Cursors.Hand
        If IsNothing(myDeviceInfo) Then
            LblDeviceName.Text = "未知"
            LblAddress.Text = "未知"
            Return
        End If
        LblDeviceName.Text = myDeviceInfo.Name
        LblAddress.Text = myDeviceInfo.Address
    End Sub

    Private Sub Panel50_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel50.Click
        RaiseEvent OnClick(myDeviceInfo)
    End Sub


   
    Private Sub LblDeviceName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblDeviceName.Click
        RaiseEvent OnClick(myDeviceInfo)
    End Sub

    Private Sub LblAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblAddress.Click
        RaiseEvent OnClick(myDeviceInfo)
    End Sub
End Class
