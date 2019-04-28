Public Class json_PPSJ
    Public freqStart As Double
    Public freqStep As Double
    Public freqEnd As Double
    Public deviceID As String
    Public dataCount As Integer
    Public runLocation As runLocation
    Public value() As Double
    Public isDSGFreq As Boolean
    Public DSGFreqBase64 As String
    Public grid As GridInfo
End Class

Public Class runLocation
    Public lng As String
    Public lat As String
    Public time As String
End Class
Public Class GridInfo
    Public id As Long
    Public cm As String
    Public cu As String
    Public ct As String
End Class

