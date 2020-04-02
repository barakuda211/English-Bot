Imports Alvas.Audio
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing.Drawing2D

Class Window1
    Private ofdAudio As New OpenFileDialog()
    Private plex As New PlayerEx()
    Private wfv As WaveformVisualizer
    Private ar As IAudioReader
    Private state As DeviceState

    Protected Overrides Sub OnInitialized(ByVal e As System.EventArgs)
        MyBase.OnInitialized(e)
        Init()
    End Sub

    Private Sub Init()
        InitWaveformVisualizer()
        ofdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*"
        tbbOpen.IsEnabled = True
        tbbPlay.IsEnabled = False
        tbbStop.IsEnabled = False

        AddHandler plex.Done, AddressOf plex_Done
        plex.BufferSizeInMS = 100
        pgMain.SelectedObject = wfv
    End Sub

    Private Sub InitWaveformVisualizer()
        wfv = New WaveformVisualizer()
        'wfv.Dock = DockStyle.Fill;
        wfh.Child = wfv
        AddHandler wfv.PaintBackground, AddressOf wfv_PaintBackground
    End Sub

    Private Sub wfv_PaintBackground(ByVal sender As Object, ByVal e As PaintEventArgs)
        ''Uncomment this code if you want to use a custom background

        'Dim rect As System.Drawing.Rectangle = e.ClipRectangle
        'Dim g As System.Drawing.Graphics = e.Graphics
        'rect.Height /= 2
        'Dim brush1 As System.Drawing.Brush = New System.Drawing.Drawing2D.LinearGradientBrush(rect, _
        '    wfv.BackColor, wfv.TimeLineColor, LinearGradientMode.Vertical)
        'Dim brush2 As System.Drawing.Brush = New System.Drawing.Drawing2D.LinearGradientBrush(rect, _
        '    wfv.TimeLineColor, wfv.BackColor, LinearGradientMode.Vertical)
        'g.FillRectangle(brush1, rect)
        'rect.Offset(0, rect.Height)
        'g.FillRectangle(brush2, rect)
    End Sub

    Private Sub plex_Done(ByVal sender As Object, ByVal e As DoneEventArgs)
        If e.IsEndPlaying Then
            [Stop]()
            wfv.Position = 0
        Else
            Dim position As Integer = CInt(plex.GetPosition(Alvas.Audio.TimeFormat.Milliseconds))
            wfv.Position = position
        End If
    End Sub

    Private Sub [Stop]()
        plex.ClosePlayer()
        state = DeviceState.Stopped
        UpdateToolBar()
    End Sub

    Private Sub UpdateToolBar()
        Select Case state
            Case DeviceState.Closed
                tbbOpen.IsEnabled = True
                tbbPlay.IsEnabled = False
                tbbStop.IsEnabled = False
                Exit Select
            Case DeviceState.InProgress
                tbbOpen.IsEnabled = False
                tbbPlay.IsEnabled = False
                tbbStop.IsEnabled = True
                Exit Select
            Case DeviceState.Stopped
                tbbOpen.IsEnabled = True
                tbbPlay.IsEnabled = True
                tbbStop.IsEnabled = False
                Exit Select
        End Select
    End Sub

    Private Sub Open()
        If ofdAudio.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            If ar IsNot Nothing Then
                ar.Close()
                ar = Nothing
            End If
            Dim fileName As String = ofdAudio.FileName
            ar = Nothing
            Select Case System.IO.Path.GetExtension(fileName.ToLower())
                Case ".avi"
                    ar = New AviReader(File.OpenRead(fileName))
                    If Not DirectCast(ar, AviReader).HasAudio Then
                        System.Windows.MessageBox.Show(String.Format("'{0}' file is not contains audio data", fileName))
                        Return
                    End If
                    Exit Select
                Case ".au"
                Case ".snd"
                    ar = New AuReader(File.OpenRead(fileName))
                    Exit Select
                Case ".wav"
                    ar = New WaveReader(File.OpenRead(fileName))
                    Exit Select
                Case ".mp3"
                    ar = New Mp3Reader(File.OpenRead(fileName))
                    Exit Select
                Case Else
                    ar = New DsReader(fileName)
                    If Not DirectCast(ar, DsReader).HasAudio Then
                        ar = Nothing
                        System.Windows.MessageBox.Show(String.Format("'{0}' file is not contains audio data", fileName))
                    End If
                    Exit Select
            End Select
            Play()
            UpdateToolBar()
        End If
    End Sub

    Private Sub Play()
        Dim format As IntPtr = ar.ReadFormat()
        plex.OpenPlayer(format)
        Dim data As Byte() = ar.ReadData()
        plex.AddData(data)
        wfv.Assign(format, data)
        plex.StartPlay()
        state = DeviceState.InProgress
        UpdateToolBar()
    End Sub

    Private Sub tbbOpen_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Open()
    End Sub

    Private Sub tbbPlay_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Play()
    End Sub

    Private Sub tbbStop_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        [Stop]()
    End Sub

End Class
