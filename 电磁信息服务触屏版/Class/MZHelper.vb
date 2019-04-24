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
Public Class MZHelper
    Shared Sub New()

    End Sub
    Shared Function GetOnlineDeviceList() As List(Of DeviceStu)
        Dim result As String = GetAutoH("func=getalldevlist")
        If result = "[]" Then
            Return Nothing
        End If
        Try           
            Dim list As List(Of DeviceStu) = JsonConvert.DeserializeObject(result, GetType(List(Of DeviceStu)))
            Return List
        Catch ex As Exception

        End Try
        Return Nothing
    End Function
    Shared Function GetAllDBDeviceList() As DataTable
        Dim result As String = GetAutoH("func=GetAllDBDevlist")
        If result = "[]" Then
            Return Nothing
        End If
        Try
            Dim dt As DataTable = JsonConvert.DeserializeObject(result, GetType(DataTable))
            Return dt
        Catch ex As Exception

        End Try
        Return Nothing
    End Function
    Shared Function GetTaskPanInfo() As normalResponse
        Dim result As String = GetAutoH("func=GetTaskPanInfo")
        Dim np As normalResponse = JsonConvert.DeserializeObject(result, GetType(normalResponse))
        Return np
    End Function

End Class
