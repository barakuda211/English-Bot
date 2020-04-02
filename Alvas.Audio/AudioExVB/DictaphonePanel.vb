Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports Alvas.Audio
Imports System.IO

Namespace AudioExCS
	Public Class DictaphonePanel
		Inherits UserControl
		Private Const timeFormat As String = "Time in ms: {0}"

		Public Sub New()
			InitializeComponent()
			dict = New Dictaphone()
			dict.SoundLevelMeter = vum
			AddHandler dict.ChangeState, AddressOf dict_ChangeState
			AddHandler dict.ChangePosition, AddressOf dict_ChangePosition
			Dim leftVolume As Integer = -1
			Dim rightVolume As Integer = -1
				'tbVolumeLeft.Value = leftVolume;
				'tbVolumeRight.Value = rightVolume;
			dict.GetVolume(leftVolume, rightVolume)
		End Sub
		Private ilButtons As ImageList
		Private acm As Alvas.Audio.AudioCompressionManager
		Private gbRecorder As GroupBox
		Private btnStopRec As Button
		Private btnRec As Button
		Private btnPauseRec As Button
		Private sfdFile As SaveFileDialog
		Private gbPlayer As GroupBox
		Private btnStop As Button
		Private btnPlay As Button
		Private btnPause As Button
		Private vum As Alvas.Audio.SoundLevelMeter
		Private lblTime As Label
		Private gbSetup As GroupBox
		Private cbMemory As CheckBox
		Private cbRecorder As ComboBox
		Private tbFile As TextBox
		Private label4 As Label
		Private cbFormat As ComboBox
		Private label3 As Label
		Private cbFormatTag As ComboBox
		Private label5 As Label
		Private btnFile As Button
		Private label6 As Label

		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso Not (components Is Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(DictaphonePanel))
			Me.ilButtons = New System.Windows.Forms.ImageList(Me.components)
			Me.acm = New Alvas.Audio.AudioCompressionManager()
			Me.gbRecorder = New System.Windows.Forms.GroupBox()
			Me.btnStopRec = New System.Windows.Forms.Button()
			Me.btnRec = New System.Windows.Forms.Button()
			Me.btnPauseRec = New System.Windows.Forms.Button()
			Me.sfdFile = New System.Windows.Forms.SaveFileDialog()
			Me.gbPlayer = New System.Windows.Forms.GroupBox()
			Me.btnStop = New System.Windows.Forms.Button()
			Me.btnPlay = New System.Windows.Forms.Button()
			Me.btnPause = New System.Windows.Forms.Button()
			Me.vum = New Alvas.Audio.SoundLevelMeter()
			Me.lblTime = New System.Windows.Forms.Label()
			Me.gbSetup = New System.Windows.Forms.GroupBox()
			Me.cbMemory = New System.Windows.Forms.CheckBox()
			Me.cbRecorder = New System.Windows.Forms.ComboBox()
			Me.tbFile = New System.Windows.Forms.TextBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.cbFormat = New System.Windows.Forms.ComboBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.cbFormatTag = New System.Windows.Forms.ComboBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.btnFile = New System.Windows.Forms.Button()
			Me.label6 = New System.Windows.Forms.Label()
			Me.gbRecorder.SuspendLayout()
			Me.gbPlayer.SuspendLayout()
			Me.gbSetup.SuspendLayout()
			Me.SuspendLayout()
			' 
			' ilButtons
			' 
			Me.ilButtons.ImageStream = DirectCast((resources.GetObject("ilButtons.ImageStream")), System.Windows.Forms.ImageListStreamer)
			Me.ilButtons.TransparentColor = System.Drawing.Color.Silver
			'            this.ilButtons.Images.SetKeyName(0, "");
			'            this.ilButtons.Images.SetKeyName(1, "");
			'            this.ilButtons.Images.SetKeyName(2, "");
			'            this.ilButtons.Images.SetKeyName(3, "");
			'            this.ilButtons.Images.SetKeyName(4, "");
			' 
			' gbRecorder
			' 
			Me.gbRecorder.Controls.Add(Me.btnStopRec)
			Me.gbRecorder.Controls.Add(Me.btnRec)
			Me.gbRecorder.Controls.Add(Me.btnPauseRec)
			Me.gbRecorder.Location = New System.Drawing.Point(3, 237)
			Me.gbRecorder.Name = "gbRecorder"
			Me.gbRecorder.Size = New System.Drawing.Size(217, 51)
			Me.gbRecorder.TabIndex = 42
			Me.gbRecorder.TabStop = False
			Me.gbRecorder.Text = "RecorderEx"
			' 
			' btnStopRec
			' 
			Me.btnStopRec.Enabled = False
			Me.btnStopRec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.btnStopRec.ImageIndex = 3
			Me.btnStopRec.ImageList = Me.ilButtons
			Me.btnStopRec.Location = New System.Drawing.Point(148, 19)
			Me.btnStopRec.Name = "btnStopRec"
			Me.btnStopRec.Size = New System.Drawing.Size(60, 23)
			Me.btnStopRec.TabIndex = 23
			Me.btnStopRec.Text = "Stop"
			Me.btnStopRec.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			AddHandler Me.btnStopRec.Click, AddressOf btnStopRec_Click
			' 
			' btnRec
			' 
			Me.btnRec.BackColor = System.Drawing.SystemColors.Control
			Me.btnRec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.btnRec.ImageIndex = 4
			Me.btnRec.ImageList = Me.ilButtons
			Me.btnRec.Location = New System.Drawing.Point(8, 19)
			Me.btnRec.Name = "btnRec"
			Me.btnRec.Size = New System.Drawing.Size(60, 23)
			Me.btnRec.TabIndex = 22
			Me.btnRec.Text = "Rec"
			Me.btnRec.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			AddHandler Me.btnRec.Click, AddressOf btnRec_Click
			'            this.btnRec.UseVisualStyleBackColor = false;
			' 
			' btnPauseRec
			' 
			Me.btnPauseRec.Enabled = False
			Me.btnPauseRec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.btnPauseRec.ImageIndex = 2
			Me.btnPauseRec.ImageList = Me.ilButtons
			Me.btnPauseRec.Location = New System.Drawing.Point(78, 19)
			Me.btnPauseRec.Name = "btnPauseRec"
			Me.btnPauseRec.Size = New System.Drawing.Size(60, 23)
			Me.btnPauseRec.TabIndex = 24
			Me.btnPauseRec.Text = "Pause"
			Me.btnPauseRec.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			AddHandler Me.btnPauseRec.Click, AddressOf btnPauseRec_Click
			' 
			' sfdFile
			' 
			Me.sfdFile.DefaultExt = "wav"
			Me.sfdFile.Filter = "*.wav|*.wav"
			' 
			' gbPlayer
			' 
			Me.gbPlayer.Controls.Add(Me.btnStop)
			Me.gbPlayer.Controls.Add(Me.btnPlay)
			Me.gbPlayer.Controls.Add(Me.btnPause)
			Me.gbPlayer.Location = New System.Drawing.Point(3, 383)
			Me.gbPlayer.Name = "gbPlayer"
			Me.gbPlayer.Size = New System.Drawing.Size(217, 51)
			Me.gbPlayer.TabIndex = 41
			Me.gbPlayer.TabStop = False
			Me.gbPlayer.Text = "PlayerEx"
			' 
			' btnStop
			' 
			Me.btnStop.Enabled = False
			Me.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.btnStop.ImageIndex = 3
			Me.btnStop.ImageList = Me.ilButtons
			Me.btnStop.Location = New System.Drawing.Point(148, 19)
			Me.btnStop.Name = "btnStop"
			Me.btnStop.Size = New System.Drawing.Size(60, 23)
			Me.btnStop.TabIndex = 33
			Me.btnStop.Text = "Stop"
			Me.btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			AddHandler Me.btnStop.Click, AddressOf btnStop_Click
			' 
			' btnPlay
			' 
			Me.btnPlay.BackColor = System.Drawing.SystemColors.Control
			Me.btnPlay.Enabled = False
			Me.btnPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.btnPlay.ImageIndex = 1
			Me.btnPlay.ImageList = Me.ilButtons
			Me.btnPlay.Location = New System.Drawing.Point(8, 19)
			Me.btnPlay.Name = "btnPlay"
			Me.btnPlay.Size = New System.Drawing.Size(60, 23)
			Me.btnPlay.TabIndex = 30
			Me.btnPlay.Text = "Play"
			Me.btnPlay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			AddHandler Me.btnPlay.Click, AddressOf btnPlay_Click
			'            this.btnPlay.UseVisualStyleBackColor = false;
			' 
			' btnPause
			' 
			Me.btnPause.Enabled = False
			Me.btnPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.btnPause.ImageIndex = 2
			Me.btnPause.ImageList = Me.ilButtons
			Me.btnPause.Location = New System.Drawing.Point(78, 19)
			Me.btnPause.Name = "btnPause"
			Me.btnPause.Size = New System.Drawing.Size(60, 23)
			Me.btnPause.TabIndex = 32
			Me.btnPause.Text = "Pause"
			Me.btnPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			AddHandler Me.btnPause.Click, AddressOf btnPause_Click
			' 
			' vum
			' 
			Me.vum.BackColor = System.Drawing.Color.Black
			Me.vum.ForeColor = System.Drawing.Color.Lime
			Me.vum.Location = New System.Drawing.Point(2, 308)
			Me.vum.Name = "vum"
			Me.vum.OwnerDraw = False
			Me.vum.Size = New System.Drawing.Size(217, 69)
			Me.vum.TabIndex = 40
			' 
			' lblTime
			' 
			Me.lblTime.AutoSize = True
			Me.lblTime.Location = New System.Drawing.Point(-1, 292)
			Me.lblTime.Name = "lblTime"
			Me.lblTime.Size = New System.Drawing.Size(69, 13)
			Me.lblTime.TabIndex = 39
			Me.lblTime.Text = "Time in ms: 0"
			' 
			' gbSetup
			' 
			Me.gbSetup.Controls.Add(Me.cbMemory)
			Me.gbSetup.Controls.Add(Me.cbRecorder)
			Me.gbSetup.Controls.Add(Me.tbFile)
			Me.gbSetup.Controls.Add(Me.label4)
			Me.gbSetup.Controls.Add(Me.cbFormat)
			Me.gbSetup.Controls.Add(Me.label3)
			Me.gbSetup.Controls.Add(Me.cbFormatTag)
			Me.gbSetup.Controls.Add(Me.label5)
			Me.gbSetup.Controls.Add(Me.btnFile)
			Me.gbSetup.Controls.Add(Me.label6)
			Me.gbSetup.Location = New System.Drawing.Point(3, 3)
			Me.gbSetup.Name = "gbSetup"
			Me.gbSetup.Size = New System.Drawing.Size(217, 228)
			Me.gbSetup.TabIndex = 38
			Me.gbSetup.TabStop = False
			Me.gbSetup.Text = "Recorder Options"
			' 
			' cbMemory
			' 
			Me.cbMemory.Checked = True
			Me.cbMemory.CheckState = System.Windows.Forms.CheckState.Checked
			Me.cbMemory.Location = New System.Drawing.Point(6, 202)
			Me.cbMemory.Name = "cbMemory"
			Me.cbMemory.Size = New System.Drawing.Size(194, 17)
			Me.cbMemory.TabIndex = 30
			Me.cbMemory.Text = "Record in memory"
			' 
			' cbRecorder
			' 
			Me.cbRecorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbRecorder.Location = New System.Drawing.Point(6, 32)
			Me.cbRecorder.Name = "cbRecorder"
			Me.cbRecorder.Size = New System.Drawing.Size(205, 21)
			Me.cbRecorder.TabIndex = 15
			AddHandler Me.cbRecorder.SelectedIndexChanged, AddressOf cbRecorder_SelectedIndexChanged
			' 
			' tbFile
			' 
			Me.tbFile.Location = New System.Drawing.Point(6, 176)
			Me.tbFile.Name = "tbFile"
			Me.tbFile.Size = New System.Drawing.Size(183, 20)
			Me.tbFile.TabIndex = 16
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(3, 160)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(85, 13)
			Me.label4.TabIndex = 21
			Me.label4.Text = "Select file name:"
			' 
			' cbFormat
			' 
			Me.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbFormat.Location = New System.Drawing.Point(6, 128)
			Me.cbFormat.Name = "cbFormat"
			Me.cbFormat.Size = New System.Drawing.Size(205, 21)
			Me.cbFormat.TabIndex = 12
			AddHandler Me.cbFormat.SelectedIndexChanged, AddressOf cbFormat_SelectedIndexChanged
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(3, 112)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(101, 13)
			Me.label3.TabIndex = 20
			Me.label3.Text = "Select audio format:"
			' 
			' cbFormatTag
			' 
			Me.cbFormatTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbFormatTag.Location = New System.Drawing.Point(6, 80)
			Me.cbFormatTag.Name = "cbFormatTag"
			Me.cbFormatTag.Size = New System.Drawing.Size(205, 21)
			Me.cbFormatTag.TabIndex = 11
			AddHandler Me.cbFormatTag.SelectedIndexChanged, AddressOf cbFormatTag_SelectedIndexChanged
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Location = New System.Drawing.Point(3, 64)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(119, 13)
			Me.label5.TabIndex = 19
			Me.label5.Text = "Select audio format tag:"
			' 
			' btnFile
			' 
			Me.btnFile.Location = New System.Drawing.Point(189, 176)
			Me.btnFile.Name = "btnFile"
			Me.btnFile.Size = New System.Drawing.Size(22, 20)
			Me.btnFile.TabIndex = 17
			Me.btnFile.Text = "..."
			AddHandler Me.btnFile.Click, AddressOf btnFile_Click
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Location = New System.Drawing.Point(3, 16)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(111, 13)
			Me.label6.TabIndex = 18
			Me.label6.Text = "Select audio recorder:"
			' 
			' DictaphonePanel
			' 
			'            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			'            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Me.Controls.Add(Me.gbRecorder)
			Me.Controls.Add(Me.gbPlayer)
			Me.Controls.Add(Me.vum)
			Me.Controls.Add(Me.lblTime)
			Me.Controls.Add(Me.gbSetup)
			Me.Name = "DictaphonePanel"
			Me.Size = New System.Drawing.Size(226, 443)
			AddHandler Me.Load, AddressOf DictaphonePanel_Load
			Me.gbRecorder.ResumeLayout(False)
			Me.gbPlayer.ResumeLayout(False)
			Me.gbSetup.ResumeLayout(False)
			Me.gbSetup.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private Sub dict_ChangePosition(ByVal sender As Object, ByVal e As PositionEventArgs)
			lblTime.Text = String.Format(timeFormat, e.Position)
		End Sub

		Private dict As Dictaphone

		Private Sub EnumFormatTags()
			'cbFormatTag.DisplayMember = "FormatTagName";
			'cbFormatTag.ValueMember = "FormatTag";
			cbFormatTag.DataSource = AudioCompressionManager.GetFormatTagList()
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

		Private Sub btnFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
			If sfdFile.ShowDialog() = DialogResult.OK Then
				Me.tbFile.Text = sfdFile.FileName
			End If
		End Sub

		Private Sub cbRecorder_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
			dict.RecorderID = cbRecorder.SelectedIndex - 1
		End Sub

		Private Sub EnumFormats()
			'cbFormat.DisplayMember = "FormatName";
			'cbFormat.ValueMember = "FormatHandle";
			Dim formatTag As Integer = DirectCast(cbFormatTag.SelectedValue, FormatTagDetails).FormatTag
			cbFormat.DataSource = AudioCompressionManager.GetFormatList(formatTag)
		End Sub

		Private Sub cbFormatTag_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
			EnumFormats()
		End Sub

		Private Sub cbFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
			Dim pafd As FormatDetails = DirectCast(cbFormat.SelectedItem, FormatDetails)
			dict.Format = pafd.FormatHandle
		End Sub

		Private Sub btnRec_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If dict.Format.Equals(IntPtr.Zero) Then
                MessageBox.Show("Please, select audio format before start recording!")
                Return
            End If
            dict.StartRecord(cbMemory.Checked, tbFile.Text)
		End Sub

		Private Sub btnStopRec_Click(ByVal sender As Object, ByVal e As System.EventArgs)
			dict.StopRecord()
		End Sub

		Private Sub btnPauseRec_Click(ByVal sender As Object, ByVal e As System.EventArgs)
			dict.PauseRecord()
		End Sub

		Private Sub btnPlay_Click(ByVal sender As Object, ByVal e As EventArgs)
			dict.StartPlay()
		End Sub

		Private Sub btnStop_Click(ByVal sender As Object, ByVal e As EventArgs)
			dict.ClosePlayer()
		End Sub

		Private Sub btnPause_Click(ByVal sender As Object, ByVal e As EventArgs)
			dict.PausePlay()
		End Sub

		Private Sub dict_ChangeState(ByVal sender As Object, ByVal e As DictaphoneStateEventArgs)
			Select Case e.State
				Case DictaphoneState.Initial
					lblTime.Text = String.Format(timeFormat, 0)
					btnStop.Enabled = False
					btnPause.Enabled = False
					btnPlay.Enabled = True
					gbSetup.Enabled = True
					gbRecorder.Enabled = True

					btnStopRec.Enabled = False
					btnPauseRec.Enabled = False
					btnRec.Enabled = True
					btnPlay.Enabled = True
					gbSetup.Enabled = True
					gbPlayer.Enabled = True
					Exit Select
				Case DictaphoneState.PausePlay
					btnPause.Enabled = False
					btnPlay.Enabled = True
					Exit Select
				Case DictaphoneState.Play
					btnPlay.Enabled = False
					btnPause.Enabled = True
					btnStop.Enabled = True

					btnRec.Enabled = False
					gbSetup.Enabled = False
					gbRecorder.Enabled = False
					Exit Select
				Case DictaphoneState.PauseRecord
					btnPauseRec.Enabled = False
					btnRec.Enabled = True
					btnStopRec.Enabled = True
					Exit Select
				Case DictaphoneState.Record
					btnStopRec.Enabled = True
					btnPauseRec.Enabled = True
					btnRec.Enabled = False
					gbSetup.Enabled = False
					gbPlayer.Enabled = False
					Exit Select
				Case Else
					Exit Select
			End Select
		End Sub

		'---------

		'private void tbVolumeLeft_Scroll(object sender, EventArgs e)
		'{
		'    SetVolume(tbVolumeLeft.Value, true);
		'}

		'private void SetVolume(int value, bool isLeft)
		'{
		'    int leftVolume = -1;
		'    int rightVolume = -1;
		'    dict.GetVolume(ref leftVolume, ref rightVolume);
		'    if (isLeft)
		'    {
		'        leftVolume = value;
		'    }
		'    else
		'    {
		'        rightVolume = value;
		'    }
		'    dict.SetVolume(leftVolume, rightVolume);
		'}

		'private void tbVolumeRight_Scroll(object sender, EventArgs e)
		'{
		'    SetVolume(tbVolumeRight.Value, false);
		'}

		Private Sub DictaphonePanel_Load(ByVal sender As Object, ByVal e As EventArgs)
			tbFile.Text = Path.ChangeExtension(Application.ExecutablePath, ".wav")
			EnumRecorders()
			EnumFormatTags()
		End Sub

	End Class
End Namespace
