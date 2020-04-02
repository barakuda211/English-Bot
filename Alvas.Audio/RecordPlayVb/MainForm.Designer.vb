Partial Class MainForm
	Inherits System.Windows.Forms.Form
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.tsMain = New System.Windows.Forms.ToolStrip
        Me.tsbNew = New System.Windows.Forms.ToolStripButton
        Me.tsbOpen = New System.Windows.Forms.ToolStripButton
        Me.tsbRecord = New System.Windows.Forms.ToolStripButton
        Me.tsbPlay = New System.Windows.Forms.ToolStripButton
        Me.tsbPause = New System.Windows.Forms.ToolStripButton
        Me.tsbStop = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tsbBackward = New System.Windows.Forms.ToolStripButton
        Me.tstStep = New System.Windows.Forms.ToolStripTextBox
        Me.tsbForward = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.tsbRecordFrom = New System.Windows.Forms.ToolStripButton
        Me.tstTime = New System.Windows.Forms.ToolStripTextBox
        Me.tsbPlayFrom = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.tsbClose = New System.Windows.Forms.ToolStripButton
        Me.ssMain = New System.Windows.Forms.StatusStrip
        Me.tspProgress = New System.Windows.Forms.ToolStripProgressBar
        Me.tsslPosition = New System.Windows.Forms.ToolStripStatusLabel
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.ofdAudio = New System.Windows.Forms.OpenFileDialog
        Me.gbPlayer = New System.Windows.Forms.GroupBox
        Me.cbPlayer = New System.Windows.Forms.ComboBox
        Me.label3 = New System.Windows.Forms.Label
        Me.cbMute = New System.Windows.Forms.CheckBox
        Me.tbPlayer = New System.Windows.Forms.TrackBar
        Me.tbRecorder = New System.Windows.Forms.TrackBar
        Me.cbRecorderLine = New System.Windows.Forms.ComboBox
        Me.gbRecorder = New System.Windows.Forms.GroupBox
        Me.cbRecorder = New System.Windows.Forms.ComboBox
        Me.label2 = New System.Windows.Forms.Label
        Me.gbDictophone = New System.Windows.Forms.GroupBox
        Me.nudVolumeLevelScale = New System.Windows.Forms.NumericUpDown
        Me.label5 = New System.Windows.Forms.Label
        Me.nudSilentLevel = New System.Windows.Forms.NumericUpDown
        Me.label4 = New System.Windows.Forms.Label
        Me.cbSkipSilent = New System.Windows.Forms.CheckBox
        Me.nudBufferSizeInMs = New System.Windows.Forms.NumericUpDown
        Me.label1 = New System.Windows.Forms.Label
        Me.sfdAudio = New System.Windows.Forms.SaveFileDialog
        Me.tbTimeline = New System.Windows.Forms.TrackBar
        Me.tsMain.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.gbPlayer.SuspendLayout()
        CType(Me.tbPlayer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbRecorder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbRecorder.SuspendLayout()
        Me.gbDictophone.SuspendLayout()
        CType(Me.nudVolumeLevelScale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudSilentLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudBufferSizeInMs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTimeline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tsMain
        '
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbOpen, Me.tsbRecord, Me.tsbPlay, Me.tsbPause, Me.tsbStop, Me.toolStripSeparator1, Me.tsbBackward, Me.tstStep, Me.tsbForward, Me.toolStripSeparator2, Me.tsbRecordFrom, Me.tstTime, Me.tsbPlayFrom, Me.toolStripSeparator3, Me.tsbClose})
        Me.tsMain.Location = New System.Drawing.Point(0, 0)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Size = New System.Drawing.Size(347, 25)
        Me.tsMain.TabIndex = 2
        '
        'tsbNew
        '
        Me.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbNew.Image = CType(resources.GetObject("tsbNew.Image"), System.Drawing.Image)
        Me.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbNew.Name = "tsbNew"
        Me.tsbNew.Size = New System.Drawing.Size(23, 22)
        Me.tsbNew.Text = "New"
        '
        'tsbOpen
        '
        Me.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbOpen.Image = CType(resources.GetObject("tsbOpen.Image"), System.Drawing.Image)
        Me.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOpen.Name = "tsbOpen"
        Me.tsbOpen.Size = New System.Drawing.Size(23, 22)
        Me.tsbOpen.Text = "Open"
        '
        'tsbRecord
        '
        Me.tsbRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbRecord.Image = CType(resources.GetObject("tsbRecord.Image"), System.Drawing.Image)
        Me.tsbRecord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRecord.Name = "tsbRecord"
        Me.tsbRecord.Size = New System.Drawing.Size(23, 22)
        Me.tsbRecord.Text = "Record"
        '
        'tsbPlay
        '
        Me.tsbPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPlay.Image = CType(resources.GetObject("tsbPlay.Image"), System.Drawing.Image)
        Me.tsbPlay.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPlay.Name = "tsbPlay"
        Me.tsbPlay.Size = New System.Drawing.Size(23, 22)
        Me.tsbPlay.Text = "Play"
        '
        'tsbPause
        '
        Me.tsbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPause.Image = CType(resources.GetObject("tsbPause.Image"), System.Drawing.Image)
        Me.tsbPause.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPause.Name = "tsbPause"
        Me.tsbPause.Size = New System.Drawing.Size(23, 22)
        Me.tsbPause.Text = "Pause"
        '
        'tsbStop
        '
        Me.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbStop.Image = CType(resources.GetObject("tsbStop.Image"), System.Drawing.Image)
        Me.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbStop.Name = "tsbStop"
        Me.tsbStop.Size = New System.Drawing.Size(23, 22)
        Me.tsbStop.Text = "Stop"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tsbBackward
        '
        Me.tsbBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBackward.Image = CType(resources.GetObject("tsbBackward.Image"), System.Drawing.Image)
        Me.tsbBackward.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBackward.Name = "tsbBackward"
        Me.tsbBackward.Size = New System.Drawing.Size(23, 22)
        Me.tsbBackward.Text = "Backward"
        '
        'tstStep
        '
        Me.tstStep.Name = "tstStep"
        Me.tstStep.Size = New System.Drawing.Size(25, 25)
        Me.tstStep.Text = "10"
        Me.tstStep.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tsbForward
        '
        Me.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbForward.Image = CType(resources.GetObject("tsbForward.Image"), System.Drawing.Image)
        Me.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbForward.Name = "tsbForward"
        Me.tsbForward.Size = New System.Drawing.Size(23, 22)
        Me.tsbForward.Text = "Forward"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsbRecordFrom
        '
        Me.tsbRecordFrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbRecordFrom.Image = CType(resources.GetObject("tsbRecordFrom.Image"), System.Drawing.Image)
        Me.tsbRecordFrom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRecordFrom.Name = "tsbRecordFrom"
        Me.tsbRecordFrom.Size = New System.Drawing.Size(23, 22)
        Me.tsbRecordFrom.Text = "Record"
        '
        'tstTime
        '
        Me.tstTime.Name = "tstTime"
        Me.tstTime.Size = New System.Drawing.Size(30, 25)
        Me.tstTime.Text = "10"
        Me.tstTime.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tsbPlayFrom
        '
        Me.tsbPlayFrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPlayFrom.Image = CType(resources.GetObject("tsbPlayFrom.Image"), System.Drawing.Image)
        Me.tsbPlayFrom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPlayFrom.Name = "tsbPlayFrom"
        Me.tsbPlayFrom.Size = New System.Drawing.Size(23, 22)
        Me.tsbPlayFrom.Text = "Play"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'tsbClose
        '
        Me.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClose.Image = CType(resources.GetObject("tsbClose.Image"), System.Drawing.Image)
        Me.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClose.Name = "tsbClose"
        Me.tsbClose.Size = New System.Drawing.Size(23, 22)
        Me.tsbClose.Text = "Close"
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tspProgress, Me.tsslPosition, Me.tsslStatus})
        Me.ssMain.Location = New System.Drawing.Point(0, 302)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(347, 22)
        Me.ssMain.TabIndex = 3
        Me.ssMain.Text = "statusStrip1"
        '
        'tspProgress
        '
        Me.tspProgress.Name = "tspProgress"
        Me.tspProgress.Size = New System.Drawing.Size(150, 16)
        '
        'tsslPosition
        '
        Me.tsslPosition.Name = "tsslPosition"
        Me.tsslPosition.Size = New System.Drawing.Size(44, 17)
        Me.tsslPosition.Text = "Position"
        '
        'tsslStatus
        '
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(38, 17)
        Me.tsslStatus.Text = "Status"
        '
        'ofdAudio
        '
        Me.ofdAudio.DefaultExt = "wav"
        '
        'gbPlayer
        '
        Me.gbPlayer.Controls.Add(Me.cbPlayer)
        Me.gbPlayer.Controls.Add(Me.label3)
        Me.gbPlayer.Controls.Add(Me.cbMute)
        Me.gbPlayer.Controls.Add(Me.tbPlayer)
        Me.gbPlayer.Location = New System.Drawing.Point(6, 111)
        Me.gbPlayer.Name = "gbPlayer"
        Me.gbPlayer.Size = New System.Drawing.Size(164, 140)
        Me.gbPlayer.TabIndex = 43
        Me.gbPlayer.TabStop = False
        Me.gbPlayer.Text = "Player"
        '
        'cbPlayer
        '
        Me.cbPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPlayer.Location = New System.Drawing.Point(6, 36)
        Me.cbPlayer.Name = "cbPlayer"
        Me.cbPlayer.Size = New System.Drawing.Size(152, 21)
        Me.cbPlayer.TabIndex = 12
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(6, 20)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(71, 13)
        Me.label3.TabIndex = 20
        Me.label3.Text = "Select player:"
        '
        'cbMute
        '
        Me.cbMute.AutoSize = True
        Me.cbMute.Location = New System.Drawing.Point(6, 63)
        Me.cbMute.Name = "cbMute"
        Me.cbMute.Size = New System.Drawing.Size(50, 17)
        Me.cbMute.TabIndex = 49
        Me.cbMute.Text = "Mute"
        Me.cbMute.UseVisualStyleBackColor = True
        '
        'tbPlayer
        '
        Me.tbPlayer.Location = New System.Drawing.Point(6, 90)
        Me.tbPlayer.Maximum = 65535
        Me.tbPlayer.Name = "tbPlayer"
        Me.tbPlayer.Size = New System.Drawing.Size(155, 42)
        Me.tbPlayer.TabIndex = 47
        Me.tbPlayer.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tbRecorder
        '
        Me.tbRecorder.Location = New System.Drawing.Point(6, 90)
        Me.tbRecorder.Maximum = 65535
        Me.tbRecorder.Name = "tbRecorder"
        Me.tbRecorder.Size = New System.Drawing.Size(155, 42)
        Me.tbRecorder.TabIndex = 50
        Me.tbRecorder.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'cbRecorderLine
        '
        Me.cbRecorderLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRecorderLine.Location = New System.Drawing.Point(6, 63)
        Me.cbRecorderLine.Name = "cbRecorderLine"
        Me.cbRecorderLine.Size = New System.Drawing.Size(152, 21)
        Me.cbRecorderLine.TabIndex = 52
        '
        'gbRecorder
        '
        Me.gbRecorder.Controls.Add(Me.cbRecorder)
        Me.gbRecorder.Controls.Add(Me.cbRecorderLine)
        Me.gbRecorder.Controls.Add(Me.label2)
        Me.gbRecorder.Controls.Add(Me.tbRecorder)
        Me.gbRecorder.Location = New System.Drawing.Point(176, 111)
        Me.gbRecorder.Name = "gbRecorder"
        Me.gbRecorder.Size = New System.Drawing.Size(164, 140)
        Me.gbRecorder.TabIndex = 53
        Me.gbRecorder.TabStop = False
        Me.gbRecorder.Text = "Recorder"
        '
        'cbRecorder
        '
        Me.cbRecorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRecorder.Location = New System.Drawing.Point(6, 36)
        Me.cbRecorder.Name = "cbRecorder"
        Me.cbRecorder.Size = New System.Drawing.Size(152, 21)
        Me.cbRecorder.TabIndex = 11
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(3, 20)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(82, 13)
        Me.label2.TabIndex = 19
        Me.label2.Text = "Select recorder:"
        '
        'gbDictophone
        '
        Me.gbDictophone.Controls.Add(Me.nudVolumeLevelScale)
        Me.gbDictophone.Controls.Add(Me.label5)
        Me.gbDictophone.Controls.Add(Me.nudSilentLevel)
        Me.gbDictophone.Controls.Add(Me.label4)
        Me.gbDictophone.Controls.Add(Me.cbSkipSilent)
        Me.gbDictophone.Controls.Add(Me.nudBufferSizeInMs)
        Me.gbDictophone.Controls.Add(Me.label1)
        Me.gbDictophone.Location = New System.Drawing.Point(6, 28)
        Me.gbDictophone.Name = "gbDictophone"
        Me.gbDictophone.Size = New System.Drawing.Size(334, 77)
        Me.gbDictophone.TabIndex = 54
        Me.gbDictophone.TabStop = False
        Me.gbDictophone.Text = "General"
        '
        'nudVolumeLevelScale
        '
        Me.nudVolumeLevelScale.Location = New System.Drawing.Point(273, 49)
        Me.nudVolumeLevelScale.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudVolumeLevelScale.Name = "nudVolumeLevelScale"
        Me.nudVolumeLevelScale.Size = New System.Drawing.Size(52, 20)
        Me.nudVolumeLevelScale.TabIndex = 54
        Me.nudVolumeLevelScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudVolumeLevelScale.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(173, 51)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(92, 13)
        Me.label5.TabIndex = 53
        Me.label5.Text = "Volume scale in %"
        '
        'nudSilentLevel
        '
        Me.nudSilentLevel.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nudSilentLevel.Location = New System.Drawing.Point(96, 23)
        Me.nudSilentLevel.Maximum = New Decimal(New Integer() {32000, 0, 0, 0})
        Me.nudSilentLevel.Name = "nudSilentLevel"
        Me.nudSilentLevel.Size = New System.Drawing.Size(59, 20)
        Me.nudSilentLevel.TabIndex = 52
        Me.nudSilentLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudSilentLevel.Value = New Decimal(New Integer() {10000, 0, 0, 0})
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(3, 25)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(58, 13)
        Me.label4.TabIndex = 51
        Me.label4.Text = "Silent level"
        '
        'cbSkipSilent
        '
        Me.cbSkipSilent.AutoSize = True
        Me.cbSkipSilent.Location = New System.Drawing.Point(3, 46)
        Me.cbSkipSilent.Name = "cbSkipSilent"
        Me.cbSkipSilent.Size = New System.Drawing.Size(74, 17)
        Me.cbSkipSilent.TabIndex = 50
        Me.cbSkipSilent.Text = "Skip silent"
        Me.cbSkipSilent.UseVisualStyleBackColor = True
        '
        'nudBufferSizeInMs
        '
        Me.nudBufferSizeInMs.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudBufferSizeInMs.Location = New System.Drawing.Point(273, 23)
        Me.nudBufferSizeInMs.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudBufferSizeInMs.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nudBufferSizeInMs.Name = "nudBufferSizeInMs"
        Me.nudBufferSizeInMs.Size = New System.Drawing.Size(52, 20)
        Me.nudBufferSizeInMs.TabIndex = 1
        Me.nudBufferSizeInMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudBufferSizeInMs.Value = New Decimal(New Integer() {500, 0, 0, 0})
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(173, 25)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(83, 13)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Buffer size in ms"
        '
        'sfdAudio
        '
        Me.sfdAudio.DefaultExt = "wav"
        Me.sfdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3"
        '
        'tbTimeline
        '
        Me.tbTimeline.Location = New System.Drawing.Point(0, 257)
        Me.tbTimeline.Maximum = 65535
        Me.tbTimeline.Name = "tbTimeline"
        Me.tbTimeline.Size = New System.Drawing.Size(347, 42)
        Me.tbTimeline.TabIndex = 56
        Me.tbTimeline.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(347, 324)
        Me.Controls.Add(Me.tbTimeline)
        Me.Controls.Add(Me.gbDictophone)
        Me.Controls.Add(Me.gbRecorder)
        Me.Controls.Add(Me.gbPlayer)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.tsMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainForm"
        Me.Text = "Dictophone"
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.gbPlayer.ResumeLayout(False)
        Me.gbPlayer.PerformLayout()
        CType(Me.tbPlayer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbRecorder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbRecorder.ResumeLayout(False)
        Me.gbRecorder.PerformLayout()
        Me.gbDictophone.ResumeLayout(False)
        Me.gbDictophone.PerformLayout()
        CType(Me.nudVolumeLevelScale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudSilentLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudBufferSizeInMs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTimeline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

		#End Region

		Private tsMain As System.Windows.Forms.ToolStrip
		Private WithEvents tsbOpen As System.Windows.Forms.ToolStripButton
		Private WithEvents tsbRecord As System.Windows.Forms.ToolStripButton
		Private WithEvents tsbPlay As System.Windows.Forms.ToolStripButton
		Private WithEvents tsbNew As System.Windows.Forms.ToolStripButton
		Private WithEvents tsbPause As System.Windows.Forms.ToolStripButton
		Private ssMain As System.Windows.Forms.StatusStrip
		Private tsslStatus As System.Windows.Forms.ToolStripStatusLabel
		Private tspProgress As System.Windows.Forms.ToolStripProgressBar
		Private WithEvents tsbStop As System.Windows.Forms.ToolStripButton
		Private WithEvents tsbForward As System.Windows.Forms.ToolStripButton
		Private WithEvents tsbBackward As System.Windows.Forms.ToolStripButton
		Private WithEvents tsbPlayFrom As System.Windows.Forms.ToolStripButton
		Private tstStep As System.Windows.Forms.ToolStripTextBox
		Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
		Private tstTime As System.Windows.Forms.ToolStripTextBox
		Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents tsbClose As System.Windows.Forms.ToolStripButton
		Private ofdAudio As System.Windows.Forms.OpenFileDialog
		Private tsslPosition As System.Windows.Forms.ToolStripStatusLabel
		Private gbPlayer As System.Windows.Forms.GroupBox
		Private WithEvents cbPlayer As System.Windows.Forms.ComboBox
		Private label3 As System.Windows.Forms.Label
		Private tbPlayer As System.Windows.Forms.TrackBar
		Private cbMute As System.Windows.Forms.CheckBox
		Private tbRecorder As System.Windows.Forms.TrackBar
		Private cbRecorderLine As System.Windows.Forms.ComboBox
		Private gbRecorder As System.Windows.Forms.GroupBox
		Private WithEvents cbRecorder As System.Windows.Forms.ComboBox
		Private label2 As System.Windows.Forms.Label
		Private gbDictophone As System.Windows.Forms.GroupBox
		Private nudBufferSizeInMs As System.Windows.Forms.NumericUpDown
		Private label1 As System.Windows.Forms.Label
		Private cbSkipSilent As System.Windows.Forms.CheckBox
		Private nudSilentLevel As System.Windows.Forms.NumericUpDown
		Private label4 As System.Windows.Forms.Label
		Private nudVolumeLevelScale As System.Windows.Forms.NumericUpDown
		Private label5 As System.Windows.Forms.Label
		Private WithEvents tsbRecordFrom As System.Windows.Forms.ToolStripButton
		Private sfdAudio As System.Windows.Forms.SaveFileDialog
		Private WithEvents tbTimeline As System.Windows.Forms.TrackBar
	End Class
