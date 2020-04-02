Imports System
Imports Alvas.Audio

Module Program

    Sub Main()
        AddHandler rex.Data, AddressOf rex_Data
        AddHandler rex.Open, AddressOf rex_Open
        AddHandler rex.Close, AddressOf rex_Close
        rex.Format = pcmFormat
        Try
            rex.StartRecord()
        Catch ex As Exception
            Console.WriteLine("Error: {0}", ex.Message)
        End Try
        Console.WriteLine("Please press enter to exit!")
        Console.ReadLine()
        Try
            rex.StopRecord()
        Catch ex As Exception
            Console.WriteLine("Error: {0}", ex.Message)
        End Try
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
End Module
