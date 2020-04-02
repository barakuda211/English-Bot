Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports Alvas.Audio

    Friend Class MainForm
        Inherits System.Windows.Forms.Form
        Private components As System.ComponentModel.IContainer
        Private btnPlay As System.Windows.Forms.Button
        Private btnPause As System.Windows.Forms.Button
        Private btnStop As System.Windows.Forms.Button
        Private lblSpeed As System.Windows.Forms.Label
        Private label1 As System.Windows.Forms.Label
        Private tInterval As System.Windows.Forms.Timer
        Private tbLV As System.Windows.Forms.TrackBar
        Private tbRV As System.Windows.Forms.TrackBar
        Private tbSpeed As System.Windows.Forms.TrackBar
        Private lbFile As System.Windows.Forms.ListBox
        Private tbnRecord As System.Windows.Forms.Button
        Private btnStopRecord As System.Windows.Forms.Button
        Private tbFileName As System.Windows.Forms.TextBox
        Private btnFileName As System.Windows.Forms.Button
        Private lblPosition As System.Windows.Forms.Label
        Private lblRemaining As System.Windows.Forms.Label
        Private lblDuration As System.Windows.Forms.Label
        Private tbPosition As System.Windows.Forms.TrackBar
        Private tabControl1 As System.Windows.Forms.TabControl
        Private tabPage1 As System.Windows.Forms.TabPage
        Private tabPage2 As System.Windows.Forms.TabPage
        Private ofdFile As System.Windows.Forms.OpenFileDialog
        Private sfdFile As System.Windows.Forms.SaveFileDialog
        Private btnOpen As System.Windows.Forms.Button
        Private ilButtons As System.Windows.Forms.ImageList
        Private pl As Alvas.Audio.Player
        Private rec As Alvas.Audio.Recorder
        Private lblStatus As System.Windows.Forms.Label
        Private groupBox2 As System.Windows.Forms.GroupBox
        Private groupBox3 As System.Windows.Forms.GroupBox
        Private rb44100 As System.Windows.Forms.RadioButton
        Private rb22050 As System.Windows.Forms.RadioButton
        Private rb11025 As System.Windows.Forms.RadioButton
        Private rb8000 As System.Windows.Forms.RadioButton
        Private rb16 As System.Windows.Forms.RadioButton
        Private rb8 As System.Windows.Forms.RadioButton
        Private gbChannels As System.Windows.Forms.GroupBox
        Private rbOneChannel As System.Windows.Forms.RadioButton
        Private rbTwoChannel As System.Windows.Forms.RadioButton
        Private label2 As System.Windows.Forms.Label

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MainForm))
            Me.btnPlay = New System.Windows.Forms.Button
            Me.ilButtons = New System.Windows.Forms.ImageList(Me.components)
            Me.btnPause = New System.Windows.Forms.Button
            Me.btnStop = New System.Windows.Forms.Button
            Me.tbPosition = New System.Windows.Forms.TrackBar
            Me.tbLV = New System.Windows.Forms.TrackBar
            Me.tbRV = New System.Windows.Forms.TrackBar
            Me.tbSpeed = New System.Windows.Forms.TrackBar
            Me.lblSpeed = New System.Windows.Forms.Label
            Me.label1 = New System.Windows.Forms.Label
            Me.label2 = New System.Windows.Forms.Label
            Me.tInterval = New System.Windows.Forms.Timer(Me.components)
            Me.lbFile = New System.Windows.Forms.ListBox
            Me.tbnRecord = New System.Windows.Forms.Button
            Me.btnStopRecord = New System.Windows.Forms.Button
            Me.tbFileName = New System.Windows.Forms.TextBox
            Me.btnFileName = New System.Windows.Forms.Button
            Me.lblPosition = New System.Windows.Forms.Label
            Me.lblRemaining = New System.Windows.Forms.Label
            Me.lblDuration = New System.Windows.Forms.Label
            Me.tabControl1 = New System.Windows.Forms.TabControl
            Me.tabPage1 = New System.Windows.Forms.TabPage
            Me.lblStatus = New System.Windows.Forms.Label
            Me.btnOpen = New System.Windows.Forms.Button
            Me.tabPage2 = New System.Windows.Forms.TabPage
            Me.groupBox3 = New System.Windows.Forms.GroupBox
            Me.rb44100 = New System.Windows.Forms.RadioButton
            Me.rb22050 = New System.Windows.Forms.RadioButton
            Me.rb11025 = New System.Windows.Forms.RadioButton
            Me.rb8000 = New System.Windows.Forms.RadioButton
            Me.groupBox2 = New System.Windows.Forms.GroupBox
            Me.rb16 = New System.Windows.Forms.RadioButton
            Me.rb8 = New System.Windows.Forms.RadioButton
            Me.gbChannels = New System.Windows.Forms.GroupBox
            Me.rbOneChannel = New System.Windows.Forms.RadioButton
            Me.rbTwoChannel = New System.Windows.Forms.RadioButton
            Me.pl = New Alvas.Audio.Player
            Me.rec = New Alvas.Audio.Recorder
            Me.ofdFile = New System.Windows.Forms.OpenFileDialog
            Me.sfdFile = New System.Windows.Forms.SaveFileDialog
            CType((Me.tbPosition), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.tbLV), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.tbRV), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.tbSpeed), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tabControl1.SuspendLayout()
            Me.tabPage1.SuspendLayout()
            Me.tabPage2.SuspendLayout()
            Me.groupBox3.SuspendLayout()
            Me.groupBox2.SuspendLayout()
            Me.gbChannels.SuspendLayout()
            Me.SuspendLayout()
            Me.btnPlay.Enabled = False
            Me.btnPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnPlay.ImageIndex = 1
            Me.btnPlay.ImageList = Me.ilButtons
            Me.btnPlay.Location = New System.Drawing.Point(64, 64)
            Me.btnPlay.Name = "btnPlay"
            Me.btnPlay.Size = New System.Drawing.Size(56, 23)
            Me.btnPlay.TabIndex = 6
            Me.btnPlay.Text = "Play"
            Me.btnPlay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            AddHandler Me.btnPlay.Click, AddressOf Me.btnPlay_Click
            Me.ilButtons.ImageSize = New System.Drawing.Size(16, 16)
            Me.ilButtons.ImageStream = CType((resources.GetObject("ilButtons.ImageStream")), System.Windows.Forms.ImageListStreamer)
            Me.ilButtons.TransparentColor = System.Drawing.Color.Silver
            Me.btnPause.Enabled = False
            Me.btnPause.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
            Me.btnPause.ImageIndex = 2
            Me.btnPause.ImageList = Me.ilButtons
            Me.btnPause.Location = New System.Drawing.Point(120, 64)
            Me.btnPause.Name = "btnPause"
            Me.btnPause.Size = New System.Drawing.Size(72, 23)
            Me.btnPause.TabIndex = 7
            Me.btnPause.Text = "Pause"
            Me.btnPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            AddHandler Me.btnPause.Click, AddressOf Me.btnPause_Click
            Me.btnStop.Enabled = False
            Me.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnStop.ImageIndex = 3
            Me.btnStop.ImageList = Me.ilButtons
            Me.btnStop.Location = New System.Drawing.Point(192, 64)
            Me.btnStop.Name = "btnStop"
            Me.btnStop.Size = New System.Drawing.Size(56, 23)
            Me.btnStop.TabIndex = 8
            Me.btnStop.Text = "Stop"
            Me.btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            AddHandler Me.btnStop.Click, AddressOf Me.btnStop_Click
            Me.tbPosition.Location = New System.Drawing.Point(0, 224)
            Me.tbPosition.Maximum = 100
            Me.tbPosition.Name = "tbPosition"
            Me.tbPosition.Size = New System.Drawing.Size(256, 42)
            Me.tbPosition.TabIndex = 9
            Me.tbPosition.TickStyle = System.Windows.Forms.TickStyle.None
            AddHandler Me.tbPosition.Scroll, AddressOf Me.tbPosition_Scroll
            Me.tbLV.Location = New System.Drawing.Point(8, 96)
            Me.tbLV.Minimum = 1
            Me.tbLV.Name = "tbLV"
            Me.tbLV.Orientation = System.Windows.Forms.Orientation.Vertical
            Me.tbLV.Size = New System.Drawing.Size(42, 104)
            Me.tbLV.TabIndex = 10
            Me.tbLV.TickStyle = System.Windows.Forms.TickStyle.Both
            Me.tbLV.Value = 10
            AddHandler Me.tbLV.Scroll, AddressOf Me.tbLV_Scroll
            Me.tbRV.Location = New System.Drawing.Point(48, 96)
            Me.tbRV.Minimum = 1
            Me.tbRV.Name = "tbRV"
            Me.tbRV.Orientation = System.Windows.Forms.Orientation.Vertical
            Me.tbRV.Size = New System.Drawing.Size(42, 104)
            Me.tbRV.TabIndex = 11
            Me.tbRV.TickStyle = System.Windows.Forms.TickStyle.Both
            Me.tbRV.Value = 10
            AddHandler Me.tbRV.Scroll, AddressOf Me.tbRV_Scroll
            Me.tbSpeed.Location = New System.Drawing.Point(208, 104)
            Me.tbSpeed.Name = "tbSpeed"
            Me.tbSpeed.Orientation = System.Windows.Forms.Orientation.Vertical
            Me.tbSpeed.Size = New System.Drawing.Size(42, 104)
            Me.tbSpeed.TabIndex = 12
            Me.tbSpeed.TickStyle = System.Windows.Forms.TickStyle.Both
            Me.tbSpeed.Value = 5
            AddHandler Me.tbSpeed.Scroll, AddressOf Me.tbSpeed_Scroll
            Me.lblSpeed.Location = New System.Drawing.Point(184, 200)
            Me.lblSpeed.Name = "lblSpeed"
            Me.lblSpeed.Size = New System.Drawing.Size(64, 16)
            Me.lblSpeed.TabIndex = 13
            Me.lblSpeed.Text = "Speed: "
            Me.lblSpeed.TextAlign = System.Drawing.ContentAlignment.TopCenter
            Me.label1.Location = New System.Drawing.Point(16, 200)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(24, 16)
            Me.label1.TabIndex = 14
            Me.label1.Text = "LV"
            Me.label2.Location = New System.Drawing.Point(56, 200)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(24, 16)
            Me.label2.TabIndex = 15
            Me.label2.Text = "RV"
            Me.tInterval.Interval = 500
            AddHandler Me.tInterval.Tick, AddressOf Me.tInterval_Tick
            Me.lbFile.Location = New System.Drawing.Point(8, 8)
            Me.lbFile.Name = "lbFile"
            Me.lbFile.Size = New System.Drawing.Size(240, 56)
            Me.lbFile.TabIndex = 16
            AddHandler Me.lbFile.SelectedIndexChanged, AddressOf Me.lbFile_SelectedIndexChanged
            Me.tbnRecord.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
            Me.tbnRecord.ImageIndex = 4
            Me.tbnRecord.ImageList = Me.ilButtons
            Me.tbnRecord.Location = New System.Drawing.Point(24, 224)
            Me.tbnRecord.Name = "tbnRecord"
            Me.tbnRecord.Size = New System.Drawing.Size(48, 23)
            Me.tbnRecord.TabIndex = 17
            Me.tbnRecord.Text = "Rec"
            Me.tbnRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            AddHandler Me.tbnRecord.Click, AddressOf Me.tbnRecord_Click
            Me.btnStopRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnStopRecord.ImageIndex = 3
            Me.btnStopRecord.ImageList = Me.ilButtons
            Me.btnStopRecord.Location = New System.Drawing.Point(176, 224)
            Me.btnStopRecord.Name = "btnStopRecord"
            Me.btnStopRecord.Size = New System.Drawing.Size(56, 23)
            Me.btnStopRecord.TabIndex = 18
            Me.btnStopRecord.Text = "Stop"
            Me.btnStopRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            AddHandler Me.btnStopRecord.Click, AddressOf Me.btnStopRecord_Click
            Me.tbFileName.Location = New System.Drawing.Point(24, 200)
            Me.tbFileName.Name = "tbFileName"
            Me.tbFileName.Size = New System.Drawing.Size(184, 20)
            Me.tbFileName.TabIndex = 19
            Me.tbFileName.Text = ""
            Me.btnFileName.Location = New System.Drawing.Point(208, 200)
            Me.btnFileName.Name = "btnFileName"
            Me.btnFileName.Size = New System.Drawing.Size(22, 20)
            Me.btnFileName.TabIndex = 20
            Me.btnFileName.Text = "..."
            AddHandler Me.btnFileName.Click, AddressOf Me.btnFileName_Click
            Me.lblPosition.Location = New System.Drawing.Point(96, 112)
            Me.lblPosition.Name = "lblPosition"
            Me.lblPosition.Size = New System.Drawing.Size(112, 16)
            Me.lblPosition.TabIndex = 21
            Me.lblPosition.Text = "Position: "
            Me.lblRemaining.Location = New System.Drawing.Point(96, 136)
            Me.lblRemaining.Name = "lblRemaining"
            Me.lblRemaining.Size = New System.Drawing.Size(112, 16)
            Me.lblRemaining.TabIndex = 22
            Me.lblRemaining.Text = "Remaining:"
            Me.lblDuration.Location = New System.Drawing.Point(96, 160)
            Me.lblDuration.Name = "lblDuration"
            Me.lblDuration.Size = New System.Drawing.Size(112, 16)
            Me.lblDuration.TabIndex = 23
            Me.lblDuration.Text = "Duration:"
            Me.tabControl1.Controls.Add(Me.tabPage1)
            Me.tabControl1.Controls.Add(Me.tabPage2)
            Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabControl1.Location = New System.Drawing.Point(0, 0)
            Me.tabControl1.Name = "tabControl1"
            Me.tabControl1.SelectedIndex = 0
            Me.tabControl1.Size = New System.Drawing.Size(264, 278)
            Me.tabControl1.TabIndex = 26
            Me.tabPage1.Controls.Add(Me.lblStatus)
            Me.tabPage1.Controls.Add(Me.btnOpen)
            Me.tabPage1.Controls.Add(Me.lbFile)
            Me.tabPage1.Controls.Add(Me.tbPosition)
            Me.tabPage1.Controls.Add(Me.lblSpeed)
            Me.tabPage1.Controls.Add(Me.label1)
            Me.tabPage1.Controls.Add(Me.label2)
            Me.tabPage1.Controls.Add(Me.tbLV)
            Me.tabPage1.Controls.Add(Me.tbSpeed)
            Me.tabPage1.Controls.Add(Me.btnPause)
            Me.tabPage1.Controls.Add(Me.btnPlay)
            Me.tabPage1.Controls.Add(Me.lblPosition)
            Me.tabPage1.Controls.Add(Me.lblRemaining)
            Me.tabPage1.Controls.Add(Me.tbRV)
            Me.tabPage1.Controls.Add(Me.btnStop)
            Me.tabPage1.Controls.Add(Me.lblDuration)
            Me.tabPage1.Location = New System.Drawing.Point(4, 22)
            Me.tabPage1.Name = "tabPage1"
            Me.tabPage1.Size = New System.Drawing.Size(256, 252)
            Me.tabPage1.TabIndex = 0
            Me.tabPage1.Text = "Player"
            Me.lblStatus.Location = New System.Drawing.Point(96, 184)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(112, 16)
            Me.lblStatus.TabIndex = 25
            Me.lblStatus.Text = "Status:"
            Me.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnOpen.ImageIndex = 0
            Me.btnOpen.ImageList = Me.ilButtons
            Me.btnOpen.Location = New System.Drawing.Point(8, 64)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(56, 23)
            Me.btnOpen.TabIndex = 24
            Me.btnOpen.Text = "Open"
            Me.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            AddHandler Me.btnOpen.Click, AddressOf Me.btnOpen_Click
            Me.tabPage2.Controls.Add(Me.groupBox3)
            Me.tabPage2.Controls.Add(Me.groupBox2)
            Me.tabPage2.Controls.Add(Me.gbChannels)
            Me.tabPage2.Controls.Add(Me.tbnRecord)
            Me.tabPage2.Controls.Add(Me.btnStopRecord)
            Me.tabPage2.Controls.Add(Me.tbFileName)
            Me.tabPage2.Controls.Add(Me.btnFileName)
            Me.tabPage2.Location = New System.Drawing.Point(4, 22)
            Me.tabPage2.Name = "tabPage2"
            Me.tabPage2.Size = New System.Drawing.Size(256, 252)
            Me.tabPage2.TabIndex = 1
            Me.tabPage2.Text = "Recorder"
            Me.groupBox3.Controls.Add(Me.rb44100)
            Me.groupBox3.Controls.Add(Me.rb22050)
            Me.groupBox3.Controls.Add(Me.rb11025)
            Me.groupBox3.Controls.Add(Me.rb8000)
            Me.groupBox3.Location = New System.Drawing.Point(24, 120)
            Me.groupBox3.Name = "groupBox3"
            Me.groupBox3.Size = New System.Drawing.Size(208, 72)
            Me.groupBox3.TabIndex = 26
            Me.groupBox3.TabStop = False
            Me.groupBox3.Text = "Samples Per Sec"
            Me.rb44100.Checked = True
            Me.rb44100.Location = New System.Drawing.Point(112, 40)
            Me.rb44100.Name = "rb44100"
            Me.rb44100.Size = New System.Drawing.Size(88, 24)
            Me.rb44100.TabIndex = 3
            Me.rb44100.TabStop = True
            Me.rb44100.Text = "44100"
            Me.rb22050.Location = New System.Drawing.Point(8, 40)
            Me.rb22050.Name = "rb22050"
            Me.rb22050.Size = New System.Drawing.Size(88, 24)
            Me.rb22050.TabIndex = 2
            Me.rb22050.Text = "22050"
            Me.rb11025.Location = New System.Drawing.Point(112, 16)
            Me.rb11025.Name = "rb11025"
            Me.rb11025.Size = New System.Drawing.Size(88, 24)
            Me.rb11025.TabIndex = 1
            Me.rb11025.Text = "11025"
            Me.rb8000.Location = New System.Drawing.Point(8, 16)
            Me.rb8000.Name = "rb8000"
            Me.rb8000.Size = New System.Drawing.Size(88, 24)
            Me.rb8000.TabIndex = 0
            Me.rb8000.Text = "8000"
            Me.groupBox2.Controls.Add(Me.rb16)
            Me.groupBox2.Controls.Add(Me.rb8)
            Me.groupBox2.Location = New System.Drawing.Point(24, 64)
            Me.groupBox2.Name = "groupBox2"
            Me.groupBox2.Size = New System.Drawing.Size(208, 48)
            Me.groupBox2.TabIndex = 25
            Me.groupBox2.TabStop = False
            Me.groupBox2.Text = "Bits Per Sample"
            Me.rb16.Checked = True
            Me.rb16.Location = New System.Drawing.Point(112, 16)
            Me.rb16.Name = "rb16"
            Me.rb16.Size = New System.Drawing.Size(88, 24)
            Me.rb16.TabIndex = 24
            Me.rb16.TabStop = True
            Me.rb16.Text = "16"
            Me.rb8.Location = New System.Drawing.Point(8, 16)
            Me.rb8.Name = "rb8"
            Me.rb8.Size = New System.Drawing.Size(88, 24)
            Me.rb8.TabIndex = 23
            Me.rb8.Text = "8"
            Me.gbChannels.Controls.Add(Me.rbOneChannel)
            Me.gbChannels.Controls.Add(Me.rbTwoChannel)
            Me.gbChannels.Location = New System.Drawing.Point(24, 8)
            Me.gbChannels.Name = "gbChannels"
            Me.gbChannels.Size = New System.Drawing.Size(208, 48)
            Me.gbChannels.TabIndex = 24
            Me.gbChannels.TabStop = False
            Me.gbChannels.Text = "Channels"
            Me.rbOneChannel.Location = New System.Drawing.Point(8, 16)
            Me.rbOneChannel.Name = "rbOneChannel"
            Me.rbOneChannel.Size = New System.Drawing.Size(88, 24)
            Me.rbOneChannel.TabIndex = 21
            Me.rbOneChannel.Text = "One channel"
            Me.rbTwoChannel.Checked = True
            Me.rbTwoChannel.Location = New System.Drawing.Point(112, 16)
            Me.rbTwoChannel.Name = "rbTwoChannel"
            Me.rbTwoChannel.Size = New System.Drawing.Size(88, 24)
            Me.rbTwoChannel.TabIndex = 22
            Me.rbTwoChannel.TabStop = True
            Me.rbTwoChannel.Text = "Two channel"
            Me.pl.FileName = ""
            Me.sfdFile.DefaultExt = "wav"
            Me.sfdFile.Filter = "*.wav|*.wav"
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(264, 278)
            Me.Controls.Add(Me.tabControl1)
            Me.Icon = CType((resources.GetObject("$this.Icon")), System.Drawing.Icon)
            Me.Name = "MainForm"
            Me.Text = "Alvas.Audio"
            CType((Me.tbPosition), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.tbLV), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.tbRV), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.tbSpeed), System.ComponentModel.ISupportInitialize).EndInit()
            Me.tabControl1.ResumeLayout(False)
            Me.tabPage1.ResumeLayout(False)
            Me.tabPage2.ResumeLayout(False)
            Me.groupBox3.ResumeLayout(False)
            Me.groupBox2.ResumeLayout(False)
            Me.gbChannels.ResumeLayout(False)
            Me.ResumeLayout(False)
        End Sub

        <STAThread()> _
        Shared Sub Main()
            Application.Run(New MainForm)
        End Sub

        Private Sub btnPlay_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not (pl.FileName = "") Then
                pl.Play()
                btnPause.Text = "Pause"
                Me.tInterval.Enabled = True
            Else
                MessageBox.Show("Please select audio file from list!")
            End If
        End Sub

        Private Sub btnPause_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If pl.Status = "paused" Then
                pl.Resume()
                btnPause.Text = "Pause"
            Else
                pl.Pause()
                btnPause.Text = "Resume"
            End If
        End Sub

        Private Sub btnStop_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            pl.Stop()
            Me.tInterval.Enabled = False
        End Sub

        Private Sub tInterval_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.Text = String.Format("Speed: {0}, LeftVolume: {1}, RightVolume: {2}", pl.Speed, pl.LeftVolume, pl.RightVolume)
            btnPause.Enabled = Not (pl.Status = "stopped")
            btnStop.Enabled = Not (pl.Status = "stopped")
            Dim pos As Integer = (pl.PositionInMS * Me.tbPosition.Maximum) / pl.DurationInMS
            Me.tbPosition.Value = pos
            Me.lblPosition.Text = String.Format("Position: {0}", GetTime(pl.PositionInMS))
            Me.lblRemaining.Text = String.Format("Remaining: {0}", GetTime(pl.DurationInMS - pl.PositionInMS))
            Me.lblDuration.Text = String.Format("Duration: {0}", GetTime(pl.DurationInMS))
            Me.lblStatus.Text = String.Format("Status: {0}", pl.Status)
        End Sub

        Private Sub lbFile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            pl.FileName = lbFile.SelectedItem.ToString
            btnPlay.Enabled = True
        End Sub

        Private Sub tbPosition_Scroll(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim pos As Integer = (Me.tbPosition.Value * pl.DurationInMS) / Me.tbPosition.Maximum
            pl.ChangePosition(pos)
        End Sub

        Private Sub tbSpeed_Scroll(ByVal sender As Object, ByVal e As System.EventArgs)
            If tbSpeed.Value = 5 Then
                pl.Speed = 1000
            Else
                If tbSpeed.Value > 5 Then
                    pl.Speed = 1000 * (tbSpeed.Value - 5 + 1)
                Else
                    If tbSpeed.Value < 5 Then
                        pl.Speed = 1000 / (5 - tbSpeed.Value)
                    End If
                End If
            End If
            Me.lblSpeed.Text = String.Format("Speed: {0}", pl.Speed / 10)
        End Sub

        Private Sub tbLV_Scroll(ByVal sender As Object, ByVal e As System.EventArgs)
            pl.LeftVolume = tbLV.Value * 100
        End Sub

        Private Sub tbRV_Scroll(ByVal sender As Object, ByVal e As System.EventArgs)
            pl.RightVolume = tbRV.Value * 100
        End Sub

        Private Sub tbnRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            rec.Open()
            Dim rc As Recorder.Channel = Microsoft.VisualBasic.IIf(rbOneChannel.Checked, Recorder.Channel.Mono, Recorder.Channel.Stereo)
            Dim rbps As Recorder.BitsPerSample = Microsoft.VisualBasic.IIf(rb8.Checked, Recorder.BitsPerSample.Bps8, Recorder.BitsPerSample.Bps16)
            Dim rsps As Recorder.SamplesPerSec
            If Me.rb8000.Checked Then
                rsps = Recorder.SamplesPerSec.Sps8000
            Else
                If Me.rb11025.Checked Then
                    rsps = Recorder.SamplesPerSec.Sps11025
                Else
                    If Me.rb22050.Checked Then
                        rsps = Recorder.SamplesPerSec.Sps22050
                    Else
                        rsps = Recorder.SamplesPerSec.Sps44100
                    End If
                End If
            End If
            rec.Configure(rc, rbps, rsps)
            rec.Record()
        End Sub

        Private Sub btnStopRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            rec.Stop()
            rec.Save(Me.tbFileName.Text)
            rec.Close()
        End Sub

        Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If ofdFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim ndx As Integer = Me.lbFile.Items.Add(ofdFile.FileName)
            Me.lbFile.SelectedIndex = ndx
        End If
        End Sub

        Private Sub btnFileName_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If sfdFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.tbFileName.Text = sfdFile.FileName
        End If
        End Sub

        Private Function GetTime(ByVal ms As Integer) As String
            Dim dt As DateTime = New DateTime(CType(ms, Long) * 10000)
            Return dt.ToString("T")
        End Function
    End Class
