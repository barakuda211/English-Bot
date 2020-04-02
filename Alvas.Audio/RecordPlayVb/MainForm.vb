Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Alvas.Audio
Imports System.IO

Public Partial Class MainForm
		Public Sub New()
			InitializeComponent()
			Init()
		End Sub

    Private Sub Init()
        ofdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*"
        sfdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.*|*.*"
        'cbMute.;
        tbPlayer.Maximum = UShort.MaxValue
        ' 65535;
        tbRecorder.Maximum = UShort.MaxValue
        ' 65535;
        tspProgress.Maximum = Short.MaxValue
        AddHandler rp.PropertyChanged, AddressOf rp_PropertyChanged
        '
        InitButtons(rp.State)
        EnumRecorders()
        EnumPlayers()
        cbMute.DataBindings.Add("Checked", rp, RecordPlayer.PlayerVolumeMuteProperty, False, DataSourceUpdateMode.OnPropertyChanged)
        tbPlayer.DataBindings.Add("Value", rp, RecordPlayer.PlayerVolumeProperty, False, DataSourceUpdateMode.OnPropertyChanged)
        '
        cbRecorderLine.DataSource = rp.RecorderLines
        cbRecorderLine.DataBindings.Add("SelectedIndex", rp, RecordPlayer.RecorderLinesIndexProperty, False, DataSourceUpdateMode.OnPropertyChanged)
        tbRecorder.DataBindings.Add("Value", rp, RecordPlayer.RecorderVolumeProperty, False, DataSourceUpdateMode.OnPropertyChanged)
        '
        tbTimeline.SmallChange = 1000
        tbTimeline.LargeChange = 10000
        tbTimeline.DataBindings.Add("Maximum", rp, "Duration", False, DataSourceUpdateMode.OnPropertyChanged)
        tbTimeline.DataBindings.Add("Value", rp, "Position", False, DataSourceUpdateMode.OnPropertyChanged)
        '

        nudBufferSizeInMs.DataBindings.Add("Value", rp, RecordPlayer.BufferSizeInMSProperty, False, DataSourceUpdateMode.OnPropertyChanged)
        cbSkipSilent.DataBindings.Add("Checked", rp, RecordPlayer.SkipSilentProperty, False, DataSourceUpdateMode.OnPropertyChanged)
        nudSilentLevel.DataBindings.Add("Value", rp, RecordPlayer.SilentLevelProperty, False, DataSourceUpdateMode.OnPropertyChanged)
        nudVolumeLevelScale.Value = 100
        nudVolumeLevelScale.Increment = 100
        nudVolumeLevelScale.Minimum = 50
        nudVolumeLevelScale.Maximum = 1000
        nudVolumeLevelScale.DataBindings.Add("Value", rp, RecordPlayer.VolumeScaleProperty, False, DataSourceUpdateMode.OnPropertyChanged)
    End Sub

		Private Sub rp_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
			Select Case e.PropertyName
				Case RecordPlayer.StateProperty
					InitButtons(rp.State)
					Exit Select
				Case RecordPlayer.VolumeLevelProperty
					tspProgress.Value = rp.VolumeLevel
					Exit Select
				Case RecordPlayer.PositionProperty, RecordPlayer.DurationProperty
					tsslPosition.Text = String.Format("{0} : {1}", rp.Position, rp.Duration)
					Exit Select
				Case Else
					Exit Select
			End Select
		End Sub

		Private rp As New RecordPlayer()

		Private Sub InitButtons(ByVal state As DeviceState)
			tsslStatus.Text = state.ToString()
			Select Case state
				Case DeviceState.Opened
					tsbNew.Enabled = False
					tsbOpen.Enabled = False
					tsbRecord.Enabled = rp.CanRecord
					tsbRecordFrom.Enabled = rp.CanRecord
					tsbPlay.Enabled = True
					tsbPause.Enabled = False
					tsbStop.Enabled = False
					tsbForward.Enabled = True
					tsbBackward.Enabled = True
					tsbPlayFrom.Enabled = True
					tsbClose.Enabled = True
					Text = rp.FormatDetails.ToString()
					cbPlayer.Enabled = True
					cbRecorder.Enabled = True
					nudBufferSizeInMs.Enabled = True
					Exit Select
				Case DeviceState.Stopped
					tsbNew.Enabled = False
					tsbOpen.Enabled = False
					tsbRecord.Enabled = rp.CanRecord
					tsbRecordFrom.Enabled = rp.CanRecord
					tsbPlay.Enabled = True
					tsbPause.Enabled = False
					tsbStop.Enabled = False
					tsbForward.Enabled = True
					tsbBackward.Enabled = True
					tsbPlayFrom.Enabled = True
					tsbClose.Enabled = True
					cbPlayer.Enabled = True
					cbRecorder.Enabled = True
					nudBufferSizeInMs.Enabled = True
					Exit Select
				Case DeviceState.Paused
					tsbNew.Enabled = False
					tsbOpen.Enabled = False
					tsbRecord.Enabled = rp.CanRecord
					tsbRecordFrom.Enabled = rp.CanRecord
					tsbPlay.Enabled = True
					tsbPause.Enabled = False
					tsbStop.Enabled = True
					tsbForward.Enabled = True
					tsbBackward.Enabled = True
					tsbPlayFrom.Enabled = True
					tsbClose.Enabled = False
					cbPlayer.Enabled = False
					cbRecorder.Enabled = False
					nudBufferSizeInMs.Enabled = True
					Exit Select
				Case DeviceState.InProgress
					tsbNew.Enabled = False
					tsbOpen.Enabled = False
					tsbRecord.Enabled = False
					tsbRecordFrom.Enabled = False
					tsbPlay.Enabled = False
					tsbPause.Enabled = True
					tsbStop.Enabled = True
					tsbForward.Enabled = True
					tsbBackward.Enabled = True
					tsbPlayFrom.Enabled = True
					tsbClose.Enabled = False
					cbPlayer.Enabled = False
					cbRecorder.Enabled = False
					nudBufferSizeInMs.Enabled = False
					Exit Select
            Case DeviceState.Closed ', Else
                tsbNew.Enabled = True
                tsbOpen.Enabled = True
                tsbRecord.Enabled = False
                tsbRecordFrom.Enabled = False
                tsbPlay.Enabled = False
                tsbPause.Enabled = False
                tsbStop.Enabled = False
                tsbForward.Enabled = False
                tsbBackward.Enabled = False
                tsbPlayFrom.Enabled = False
                tsbClose.Enabled = False
                cbPlayer.Enabled = True
                cbRecorder.Enabled = True
                nudBufferSizeInMs.Enabled = True
                Exit Select
        End Select
		End Sub

    Private Sub tsbClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbClose.Click
        rp.Close()
    End Sub

		Public ReadOnly Property Time() As Integer
			Get
				Try
					Return Integer.Parse(tstTime.Text)
				Catch
					Return 0
				End Try
			End Get
		End Property

    Private Sub tsbPlayFrom_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbPlayFrom.Click
        rp.Play(Position)
    End Sub

		Public ReadOnly Property Position() As Integer
			Get
				Try
					Return Integer.Parse(tstTime.Text) * 1000
				Catch
					Return 0
				End Try
			End Get
		End Property

		Public ReadOnly Property [Step]() As Integer
			Get
				Try
					Return Integer.Parse(tstStep.Text) * 1000
				Catch
					Return 10 * 1000
				End Try
			End Get
		End Property

    Private Sub tsbBackward_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbBackward.Click
        rp.Backward([Step])
    End Sub

    Private Sub tsbForward_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbForward.Click
        rp.Forward([Step])
    End Sub

    Private Sub tsbStop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbStop.Click
        rp.[Stop]()
    End Sub

    Private Sub tsbPause_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbPause.Click
        rp.Pause()
    End Sub

    Private Sub tsbPlay_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbPlay.Click
        rp.Play()
    End Sub

    Private Sub Record()
        Dim pos As Integer = rp.Position
        If pos <= 0 Then
            rp.Record()
        Else
            rp.Record(pos)
        End If
    End Sub

    Private Sub tsbRecord_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbRecord.Click
        Try
            Record()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tsbNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbNew.Click
        If sfdAudio.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim fileName As String = sfdAudio.FileName
            Dim stream As Stream = Nothing
            Dim arw As IAudioReadWriter = Nothing
            Dim fd As FormatDialog = Nothing
            Select Case Path.GetExtension(fileName.ToLower())
                Case ".wav"
                    fd = New FormatDialog(False)
                    If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        stream = File.Create(fileName)
                        arw = New WaveReadWriter(stream, AudioCompressionManager.FormatBytes(fd.Format))
                        rp.Open(arw)
                    End If
                    Exit Select
                Case ".mp3"
                    fd = New FormatDialog(True)
                    If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        stream = File.Create(fileName)
                        arw = New Mp3ReadWriter(stream, fd.Format)
                        rp.Open(arw)
                    End If
                    Exit Select
                Case Else
                    Return
            End Select
        End If
    End Sub

    Private Sub tsbOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpen.Click
        If ofdAudio.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim fileName As String = ofdAudio.FileName
            Dim arw As IAudioReader = Nothing
            Select Case Path.GetExtension(fileName.ToLower())
                Case ".au"
                Case ".snd"
                    arw = New AuReader(File.OpenRead(fileName))
                    Exit Select
                Case ".avi"
                    arw = New AviReader(File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
                    If Not CType(arw, AviReader).HasAudio Then
                        MessageBox.Show(String.Format("'{0}' file is not contains audio data", fileName))
                        Return
                    End If
                    Exit Select
                Case ".wav"
                    arw = New WaveReadWriter(File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
                    Exit Select
                Case ".mp3"
                    arw = New Mp3ReadWriter(File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
                    Exit Select
                Case Else
                    arw = New DsReader(fileName)
                    If Not CType(arw, DsReader).HasAudio Then
                        MessageBox.Show(String.Format("'{0}' file is not contains audio data", fileName))
                        Return
                    End If
                    Exit Select
                    'Dim fd As New FormatDialog(False)
                    'If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    '    arw = New RawReadWriter(stream, fd.Format)
                    '    Exit Select
                    'Else
                    '    Return
                    'End If
            End Select
            rp.Open(arw)
        End If
    End Sub

		Private Sub EnumPlayers()
			Dim count As Integer = PlayerEx.PlayerCount
			If count > 0 Then
				For i As Integer = -1 To count - 1
					cbPlayer.Items.Add(PlayerEx.GetPlayerName(i))
				Next
				cbPlayer.SelectedIndex = 0
			End If
		End Sub

		Private Sub EnumRecorders()
			Dim count As Integer = RecorderEx.RecorderCount
			If count > 0 Then
				For i As Integer = -1 To count - 1
					cbRecorder.Items.Add(RecorderEx.GetRecorderName(i))
				Next
				cbRecorder.SelectedIndex = 0
			End If
		End Sub

    Private Sub cbPlayer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbPlayer.SelectedIndexChanged
        rp.PlayerID = cbPlayer.SelectedIndex - 1
    End Sub

    Private Sub cbRecorder_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbRecorder.SelectedIndexChanged
        rp.RecorderID = cbRecorder.SelectedIndex - 1
    End Sub

    Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler rp.PropertyChanged, AddressOf rp_PropertyChanged
    End Sub

    Private Sub tsbRecordFrom_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsbRecordFrom.Click
        Try
            rp.Record(Position)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

	End Class
