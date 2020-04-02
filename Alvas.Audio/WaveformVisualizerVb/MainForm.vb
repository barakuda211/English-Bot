Imports System
Imports System.Windows.Forms
Imports Alvas.Audio
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing

Public Class MainForm
    Public Sub New()
        InitializeComponent()
        Init()
    End Sub

    Private Sub Init()
        InitWaveformVisualizer()
        ofdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.vox|*.vox|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*"
        tbbOpen.Enabled = True

        plex = New PlayerEx()
        AddHandler plex.Done, AddressOf plex_Done
        plex.BufferSizeInMS = 100
        pgMain.SelectedObject = wfv
    End Sub

    Private Sub InitWaveformVisualizer()
        wfv = New WaveformVisualizer()
        wfv.Dock = DockStyle.Fill
        AddHandler wfv.PaintBackground, AddressOf wfv_PaintBackground
        Me.pMain.Controls.Add(wfv)
    End Sub

    Private Sub wfv_PaintBackground(ByVal sender As Object, ByVal e As PaintEventArgs)
        ''Uncomment this code if you want to use a custom background

        'Dim rect As Rectangle = e.ClipRectangle
        'Dim g As Graphics = e.Graphics
        'rect.Height /= 2
        'Dim brush1 As Brush = New LinearGradientBrush(rect, wfv.BackColor, wfv.TimeLineColor, LinearGradientMode.Vertical)
        'Dim brush2 As Brush = New LinearGradientBrush(rect, wfv.TimeLineColor, wfv.BackColor, LinearGradientMode.Vertical)
        'g.FillRectangle(brush1, rect)
        'rect.Offset(0, rect.Height)
        'g.FillRectangle(brush2, rect)
    End Sub

    Private plex As PlayerEx
    Private wfv As WaveformVisualizer
    Private state As DeviceState

    Private Sub plex_Done(ByVal sender As Object, ByVal e As DoneEventArgs)
        If e.IsEndPlaying Then
            tbMain_ButtonClick(tbMain, New ToolBarButtonClickEventArgs(tbbStop))
            wfv.Position = 0
        Else
            Dim position As Integer = CInt(plex.GetPosition(Alvas.Audio.TimeFormat.Milliseconds))
            wfv.Position = position
        End If
    End Sub

    Private Sub tbMain_ButtonClick(ByVal sender As Object, ByVal e As ToolBarButtonClickEventArgs) Handles tbMain.ButtonClick
        Select Case tbMain.Buttons.IndexOf(e.Button)
            Case 0
                Open()
                Exit Select
            Case 1
                Play()
                Exit Select
            Case 2
                [Stop]()
                Exit Select
        End Select

        UpdateToolBar()
    End Sub

    Private Sub [Stop]()
        plex.ClosePlayer()
        state = DeviceState.Stopped
    End Sub

    Private Sub UpdateToolBar()
        Select Case state
            Case DeviceState.Closed
                tbbOpen.Enabled = True
                tbbPlay.Enabled = False
                tbbStop.Enabled = False
                Exit Select
            Case DeviceState.InProgress
                tbbOpen.Enabled = False
                tbbPlay.Enabled = False
                tbbStop.Enabled = True
                Exit Select
            Case DeviceState.Stopped
                tbbOpen.Enabled = True
                tbbPlay.Enabled = True
                tbbStop.Enabled = False
                Exit Select
        End Select
    End Sub

    Private ar As IAudioReader

    Private Sub Open()
        If ofdAudio.ShowDialog() = DialogResult.OK Then
            If ar IsNot Nothing Then
                ar.Close()
                ar = Nothing
            End If
            Dim fileName As String = ofdAudio.FileName
            ar = Nothing
            Select Case Path.GetExtension(fileName.ToLower())
                Case ".vox"
                    ar = GetVoxReader(fileName, 8000)
                    Exit Select
                Case ".avi"
                    ar = New AviReader(File.OpenRead(fileName))
                    If Not DirectCast(ar, AviReader).HasAudio Then
                        MessageBox.Show(String.Format("'{0}' file is not contains audio data", fileName))
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
                        MessageBox.Show(String.Format("'{0}' file is not contains audio data", fileName))
                    End If
                    Exit Select
            End Select
            Play()
            UpdateToolBar()
        End If
    End Sub

    Private Shared Function GetVoxReader(ByVal fileName As String, ByVal sampleRate As Integer) As IAudioReader
        Dim br As New BinaryReader(File.OpenRead(fileName))
        Dim ms As New MemoryStream()
        Dim format As IntPtr = AudioCompressionManager.GetPcmFormat(1, 16, sampleRate)
        Dim ww As New WaveWriter(ms, AudioCompressionManager.FormatBytes(format))
        Vox.Vox2Wav(br, ww)
        br.Close()
        Return New WaveReader(ms)
    End Function

    Private Sub Play()
        Dim format As IntPtr = ar.ReadFormat()
        plex.OpenPlayer(format)
        Dim data As Byte() = ar.ReadData()
        plex.AddData(data)
        wfv.Assign(format, data)
        plex.StartPlay()
        state = DeviceState.InProgress
    End Sub


End Class
