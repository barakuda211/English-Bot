' rem run cmd as administrator
' rem install service
' \\Windows\Microsoft.NET\Framework\v2.0.50727\installutil AudioWindowsServiceVb.exe
' rem uninstall service
' \\Windows\Microsoft.NET\Framework\v2.0.50727\installutil AudioWindowsServiceVb.exe /u
Imports Alvas.Audio
Imports System.Threading

Public Class AudioService

    Protected Overrides Sub OnStart(ByVal args() As String)
        Dim t As Thread = New Thread(AddressOf Start)
        t.Start()
    End Sub

    Protected Overrides Sub OnStop()
        rex.StopRecord()
    End Sub

    Sub Start()
        AddHandler rex.Data, AddressOf rex_Data
        AddHandler rex.Open, AddressOf rex_Open
        AddHandler rex.Close, AddressOf rex_Close
        rex.Format = pcmFormat
        rex.StartRecord()
    End Sub

    Private rex As New RecorderEx(True)
    Private play As New PlayerEx(True)
    Private pcmFormat As IntPtr = AudioCompressionManager.GetPcmFormat(1, 16, 44100)

    Private Sub rex_Open(ByVal sender As Object, ByVal e As EventArgs)
        play.OpenPlayer(pcmFormat)
        play.StartPlay()
    End Sub

    Private Sub rex_Close(ByVal sender As Object, ByVal e As EventArgs)
        play.ClosePlayer()
    End Sub

    Private Sub rex_Data(ByVal sender As Object, ByVal e As DataEventArgs)
        Dim data As Byte() = e.Data
        play.AddData(data)
    End Sub
End Class
