Public Class TekBusDeviceInfo
    Public lineId As Integer
    Public gpsTime As String
    Public freqUpdateTime As String
    Public TekDeviceId As String
    Public GwDeviceId As String
    Public HDeviceId As String
    Public TekCoordinfo As CoordInfo
    Public GwCoordinfo As CoordInfo
    Public HkCoordinfo As CoordInfo
    Public freqJson As json_PPSJ
    Public oldPointInfo As String
    Public lng As Double
    Public lat As Double
End Class
Public Class CoordInfo
    Public x As Double
    Public y As Double
End Class
