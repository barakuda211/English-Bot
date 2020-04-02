Imports Alvas.Audio
Imports System.IO
Imports System.Runtime.InteropServices

Public Class MainForm

    Public Sub New()
        InitializeComponent()
        Init()
    End Sub

    Protected Overrides Sub UpdateDefaultButton()
        Dim ctrl As Control = FindFocusedControl(splitContainer1)
        If ctrl IsNot Nothing AndAlso ctrl.Tag IsNot Nothing Then
            propertyGrid1.SelectedObject = IIf((ctrl IsNot Nothing), ctrl.Tag, Nothing)
        End If
    End Sub

    Public Shared Function FindFocusedControl(ByVal container As Control) As Control
        For Each childControl As Control In container.Controls
            If childControl.Focused Then
                Return childControl
            End If
        Next

        For Each childControl As Control In container.Controls
            Dim maybeFocusedControl As Control = FindFocusedControl(childControl)
            If maybeFocusedControl IsNot Nothing Then
                Return maybeFocusedControl
            End If
        Next

        Return Nothing
        ' Couldn't find any, darn!
    End Function

    Private Sub Init()
        sfdAudio.Filter = "*.wav|*.wav"
        sfdAudio.DefaultExt = "wav"
        ofdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*"
        btnPlay.Enabled = False
        btnStop.Enabled = False
        btnSave.Enabled = False

        Dim aeChorus As ChorusAudioEffect = AudioEffect.CreateChorusAudioEffect()
        NewTab(aeChorus, "Chorus")
        Dim aeCompressor As CompressorAudioEffect = AudioEffect.CreateCompressorAudioEffect()
        NewTab(aeCompressor, "Compressor")
        Dim aeDistortion As DistortionAudioEffect = AudioEffect.CreateDistortionAudioEffect()
        NewTab(aeDistortion, "Distortion")
        Dim aeEcho As EchoAudioEffect = AudioEffect.CreateEchoAudioEffect()
        NewTab(aeEcho, "Echo")
        Dim aeFlanger As FlangerAudioEffect = AudioEffect.CreateFlangerAudioEffect()
        NewTab(aeFlanger, "Flanger")
        Dim aeGargle As GargleAudioEffect = AudioEffect.CreateGargleAudioEffect()
        NewTab(aeGargle, "Gargle")
        Dim aeI3DL2Reverb As I3DL2ReverbAudioEffect = AudioEffect.CreateI3DL2ReverbAudioEffect()
        NewTab(aeI3DL2Reverb, "I3DL2Reverb")
        Dim aeParamEq As ParamEqAudioEffect = AudioEffect.CreateParamEqAudioEffect()
        NewTab(aeParamEq, "ParamEq")
        Dim aeWavesReverb As WavesReverbAudioEffect = AudioEffect.CreateWavesReverbAudioEffect()
        NewTab(aeWavesReverb, "WavesReverb")
    End Sub

    Private Sub NewTab(ByVal ae As AudioEffect, ByVal name As String)
        Dim tab As TabPage = InitParams(ae, name)
        tcEffects.TabPages.Add(tab)
    End Sub

    Private Function InitParams(ByVal ae As AudioEffect, ByVal name As String) As TabPage
        Dim tp As New TabPage(name)
        Dim tl As New TableLayoutPanel()
        tl.Dock = DockStyle.Top
        tl.ColumnCount = 2 + 1
        tl.AutoSizeMode = AutoSizeMode.GrowOnly
        tl.AutoSize = True
        tp.Controls.Add(tl)

        tl.RowCount = ae.Params.Length
        For i As Integer = 0 To ae.Params.Length - 1
            Dim param As DmoParam = ae.Params(i)
            Dim cb As New CheckBox()
            cb.Text = "Default"
            cb.DataBindings.Add("Checked", param, "IsDefaultValue")
            tl.Controls.Add(cb)
            Dim l As New Label()
            l.Text = ae.Params(i).Name + (IIf((ae.Params(i).Unit = ""), "", ", " + ae.Params(i).Unit))
            tl.Controls.Add(l)
            If TypeOf param Is DmoBoolParam Then
                Dim ctrl As New CheckBox()
                ctrl.Tag = param
                ctrl.DataBindings.Add("Checked", param, "Value", False, DataSourceUpdateMode.OnPropertyChanged)
                tl.Controls.Add(ctrl)
            ElseIf TypeOf param Is DmoEnumParam Then
                Dim paramEnum As DmoEnumParam = DirectCast(param, DmoEnumParam)
                Dim ctrl As New ComboBox()
                ctrl.DropDownStyle = ComboBoxStyle.DropDownList
                ctrl.Items.AddRange(paramEnum.Items)
                ctrl.Tag = param
                ctrl.DataBindings.Add("SelectedIndex", param, "Value", False, DataSourceUpdateMode.OnPropertyChanged)
                tl.Controls.Add(ctrl)
            ElseIf TypeOf param Is DmoFloatParam Then
                Dim paramFloat As DmoFloatParam = DirectCast(param, DmoFloatParam)
                Dim ctrl As New NumericUpDown()
                ctrl.DecimalPlaces = 3
                ctrl.Increment = 0.001D
                ctrl.Minimum = CDec(paramFloat.Minimum)
                ctrl.Maximum = CDec(paramFloat.Maximum)
                ctrl.Tag = param
                ctrl.DataBindings.Add("Value", param, "Value", False, DataSourceUpdateMode.OnPropertyChanged)
                tl.Controls.Add(ctrl)
            ElseIf TypeOf param Is DmoIntParam Then
                Dim paramInt As DmoIntParam = DirectCast(param, DmoIntParam)
                Dim ctrl As New TrackBar()
                ctrl.Minimum = paramInt.Minimum
                ctrl.Maximum = paramInt.Maximum
                ctrl.Tag = param
                ctrl.DataBindings.Add("Value", param, "Value", False, DataSourceUpdateMode.OnPropertyChanged)
                tl.Controls.Add(ctrl)
            Else
                Dim ctrl As New Label()
                ctrl.Tag = param
                tl.Controls.Add(ctrl)
            End If
        Next

        For i As Integer = 0 To tl.RowCount - 1
            tl.RowStyles.Add(New RowStyle(SizeType.Absolute, 26.0F))
        Next
        tl.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        tl.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        tl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 0.5F))
        tp.Tag = ae
        Return tp
    End Function

    Private plex As New PlayerEx()

    Private Sub btnPlay_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPlay.Click
        Dim af As AudioEffect = TryCast(tcEffects.SelectedTab.Tag, AudioEffect)
        Dim data As Byte() = Nothing
        Dim format As IntPtr = IntPtr.Zero
        Prepare(arw, af, format, data)
        Play(format, data)
        tcEffectsEnabled = False
        btnPlay.Enabled = False
        btnOpen.Enabled = False
        btnStop.Enabled = True
    End Sub

    Public Sub Prepare(ByVal wr As IAudioReader, ByVal af As AudioEffect, ByRef format As IntPtr, ByRef data As Byte())
        format = wr.ReadFormat()
        Dim wf1 As WaveFormat = AudioCompressionManager.GetWaveFormat(format)
        Console.WriteLine("{0},{1},{2}-{3}", wf1.nChannels, wf1.wBitsPerSample, wf1.nSamplesPerSec, wf1.wFormatTag)
        data = wr.ReadData()
        If wf1.wFormatTag <> 1 Then
            Dim formatNew As IntPtr = IntPtr.Zero
            Dim dataNew As Byte() = Nothing
            AudioCompressionManager.ToPcm(format, data, formatNew, dataNew)
            format = formatNew
            data = dataNew
            Dim wf2 As WaveFormat = AudioCompressionManager.GetWaveFormat(format)
            Console.WriteLine("{0},{1},{2}-{3}", wf2.nChannels, wf2.wBitsPerSample, wf2.nSamplesPerSec, wf2.wFormatTag)
        ElseIf wf1.wBitsPerSample <> 16 Then
            Dim wf As WaveFormat = AudioCompressionManager.GetWaveFormat(format)
            Dim formatNew As IntPtr = AudioCompressionManager.GetPcmFormat(wf.nChannels, 16, wf.nSamplesPerSec)
            Dim dataNew As Byte() = AudioCompressionManager.Convert(format, formatNew, data, False)
            format = formatNew
            data = dataNew
            Dim wf2 As WaveFormat = AudioCompressionManager.GetWaveFormat(format)
            Console.WriteLine("{0},{1},{2}-{3}", wf2.nChannels, wf2.wBitsPerSample, wf2.nSamplesPerSec, wf2.wFormatTag)
        End If
        'wr.Close();
        If af IsNot Nothing Then
            Dim hasProcessInPlace As Boolean = af.HasProcessInPlace
            'af.GetSupportedOutputFormats();
            Dim src As GCHandle = GCHandle.Alloc(data, GCHandleType.Pinned)
            Dim formatPtr As IntPtr = src.AddrOfPinnedObject()
            Dim res As Boolean = af.ProcessInPlace(format, data)
            src.Free()
            If Not res Then
                MessageBox.Show("Unable to convert the audio data")
                Return
            End If
        End If
    End Sub

    Private Sub Play(ByVal format As IntPtr, ByVal data As Byte())
        If plex.State <> DeviceState.Closed Then
            plex.ClosePlayer()
        End If
        'Console.WriteLine(plex.State);
        plex.OpenPlayer(format)
        plex.AddData(data)
        plex.StartPlay()
    End Sub

    Private Sub btnStop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStop.Click
        If plex.State <> DeviceState.Closed Then
            plex.ClosePlayer()
        End If
        tcEffectsEnabled = True
        btnPlay.Enabled = True
        btnOpen.Enabled = True
        btnStop.Enabled = False
        Console.WriteLine("{0} STOP", plex.State)
    End Sub

    Private arw As IAudioReader
    Private tcEffectsEnabled As Boolean = True

    Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
        If ofdAudio.ShowDialog() = DialogResult.OK Then
            If arw IsNot Nothing Then
                arw.Close()
                arw = Nothing
            End If
            Dim fileName As String = ofdAudio.FileName
            arw = Nothing
            Select Case Path.GetExtension(fileName.ToLower())
                Case ".avi"
                    arw = New AviReader(File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
                    If Not DirectCast(arw, AviReader).HasAudio Then
                        MessageBox.Show(String.Format("'{0}' file is not contains audio data", fileName))
                        Return
                    End If
                    Exit Select
                Case ".au", ".snd"
                    arw = New AuReader(File.OpenRead(fileName))
                    Exit Select
                Case ".wav"
                    arw = New WaveReadWriter(File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
                    Exit Select
                Case ".mp3"
                    arw = New Mp3ReadWriter(File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
                    Exit Select
                Case Else
                    arw = New DsReader(fileName)
                    If Not DirectCast(arw, DsReader).HasAudio Then
                        arw = Nothing
                        MessageBox.Show(String.Format("'{0}' file is not contains audio data", fileName))
                    End If
                    Exit Select
            End Select
            btnPlay.Enabled = arw IsNot Nothing
            btnSave.Enabled = arw IsNot Nothing
        End If
    End Sub

    Private Sub tcEffects_Selecting(ByVal sender As Object, ByVal e As TabControlCancelEventArgs) Handles tcEffects.Selecting
        e.Cancel = Not tcEffectsEnabled
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        If sfdAudio.ShowDialog() = DialogResult.OK Then
            Save(sfdAudio.FileName)
        End If
    End Sub

    Private Sub Save(ByVal format As IntPtr, ByVal data As Byte(), ByVal fileName As String)
        'string fileName = string.Format(@"e:\Down\wav\{0}.wav", name);
        Dim ww As New WaveWriter(File.Create(fileName), AudioCompressionManager.FormatBytes(format))
        ww.WriteData(data)
        ww.Close()
    End Sub


    Private Sub Save(ByVal fileName As String)
        Dim af As AudioEffect = TryCast(tcEffects.SelectedTab.Tag, AudioEffect)
        Dim data As Byte() = Nothing
        Dim format As IntPtr = IntPtr.Zero
        Prepare(arw, af, format, data)
        Save(format, data, fileName)
    End Sub

End Class
