namespace RecordPlay
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbRecord = new System.Windows.Forms.ToolStripButton();
            this.tsbPlay = new System.Windows.Forms.ToolStripButton();
            this.tsbPause = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBackward = new System.Windows.Forms.ToolStripButton();
            this.tstStep = new System.Windows.Forms.ToolStripTextBox();
            this.tsbForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRecordFrom = new System.Windows.Forms.ToolStripButton();
            this.tstTime = new System.Windows.Forms.ToolStripTextBox();
            this.tsbPlayFrom = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.tspProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ofdAudio = new System.Windows.Forms.OpenFileDialog();
            this.gbPlayer = new System.Windows.Forms.GroupBox();
            this.cbPlayer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMute = new System.Windows.Forms.CheckBox();
            this.tbPlayer = new System.Windows.Forms.TrackBar();
            this.tbRecorder = new System.Windows.Forms.TrackBar();
            this.cbRecorderLine = new System.Windows.Forms.ComboBox();
            this.gbRecorder = new System.Windows.Forms.GroupBox();
            this.cbRecorder = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbDictophone = new System.Windows.Forms.GroupBox();
            this.nudVolumeLevelScale = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudSilentLevel = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSkipSilent = new System.Windows.Forms.CheckBox();
            this.nudBufferSizeInMs = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.sfdAudio = new System.Windows.Forms.SaveFileDialog();
            this.tbTimeline = new System.Windows.Forms.TrackBar();
            this.tsMain.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.gbPlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRecorder)).BeginInit();
            this.gbRecorder.SuspendLayout();
            this.gbDictophone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolumeLevelScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSilentLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeInMs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTimeline)).BeginInit();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbRecord,
            this.tsbPlay,
            this.tsbPause,
            this.tsbStop,
            this.toolStripSeparator1,
            this.tsbBackward,
            this.tstStep,
            this.tsbForward,
            this.toolStripSeparator2,
            this.tsbRecordFrom,
            this.tstTime,
            this.tsbPlayFrom,
            this.toolStripSeparator3,
            this.tsbClose});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(347, 25);
            this.tsMain.TabIndex = 2;
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "New";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Open";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbRecord
            // 
            this.tsbRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRecord.Image = ((System.Drawing.Image)(resources.GetObject("tsbRecord.Image")));
            this.tsbRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRecord.Name = "tsbRecord";
            this.tsbRecord.Size = new System.Drawing.Size(23, 22);
            this.tsbRecord.Text = "Record";
            this.tsbRecord.Click += new System.EventHandler(this.tsbRecord_Click);
            // 
            // tsbPlay
            // 
            this.tsbPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPlay.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlay.Image")));
            this.tsbPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlay.Name = "tsbPlay";
            this.tsbPlay.Size = new System.Drawing.Size(23, 22);
            this.tsbPlay.Text = "Play";
            this.tsbPlay.Click += new System.EventHandler(this.tsbPlay_Click);
            // 
            // tsbPause
            // 
            this.tsbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPause.Image = ((System.Drawing.Image)(resources.GetObject("tsbPause.Image")));
            this.tsbPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPause.Name = "tsbPause";
            this.tsbPause.Size = new System.Drawing.Size(23, 22);
            this.tsbPause.Text = "Pause";
            this.tsbPause.Click += new System.EventHandler(this.tsbPause_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(23, 22);
            this.tsbStop.Text = "Stop";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBackward
            // 
            this.tsbBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBackward.Image = ((System.Drawing.Image)(resources.GetObject("tsbBackward.Image")));
            this.tsbBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBackward.Name = "tsbBackward";
            this.tsbBackward.Size = new System.Drawing.Size(23, 22);
            this.tsbBackward.Text = "Backward";
            this.tsbBackward.Click += new System.EventHandler(this.tsbBackward_Click);
            // 
            // tstStep
            // 
            this.tstStep.Name = "tstStep";
            this.tstStep.Size = new System.Drawing.Size(25, 25);
            this.tstStep.Text = "10";
            this.tstStep.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tsbForward
            // 
            this.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbForward.Image = ((System.Drawing.Image)(resources.GetObject("tsbForward.Image")));
            this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForward.Name = "tsbForward";
            this.tsbForward.Size = new System.Drawing.Size(23, 22);
            this.tsbForward.Text = "Forward";
            this.tsbForward.Click += new System.EventHandler(this.tsbForward_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRecordFrom
            // 
            this.tsbRecordFrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRecordFrom.Image = ((System.Drawing.Image)(resources.GetObject("tsbRecordFrom.Image")));
            this.tsbRecordFrom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRecordFrom.Name = "tsbRecordFrom";
            this.tsbRecordFrom.Size = new System.Drawing.Size(23, 22);
            this.tsbRecordFrom.Text = "Record";
            this.tsbRecordFrom.Click += new System.EventHandler(this.tsbRecordFrom_Click);
            // 
            // tstTime
            // 
            this.tstTime.Name = "tstTime";
            this.tstTime.Size = new System.Drawing.Size(30, 25);
            this.tstTime.Text = "10";
            this.tstTime.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tsbPlayFrom
            // 
            this.tsbPlayFrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPlayFrom.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlayFrom.Image")));
            this.tsbPlayFrom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlayFrom.Name = "tsbPlayFrom";
            this.tsbPlayFrom.Size = new System.Drawing.Size(23, 22);
            this.tsbPlayFrom.Text = "Play";
            this.tsbPlayFrom.Click += new System.EventHandler(this.tsbPlayFrom_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspProgress,
            this.tsslPosition,
            this.tsslStatus});
            this.ssMain.Location = new System.Drawing.Point(0, 302);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(347, 22);
            this.ssMain.TabIndex = 3;
            this.ssMain.Text = "statusStrip1";
            // 
            // tspProgress
            // 
            this.tspProgress.Name = "tspProgress";
            this.tspProgress.Size = new System.Drawing.Size(150, 16);
            // 
            // tsslPosition
            // 
            this.tsslPosition.Name = "tsslPosition";
            this.tsslPosition.Size = new System.Drawing.Size(44, 17);
            this.tsslPosition.Text = "Position";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(38, 17);
            this.tsslStatus.Text = "Status";
            // 
            // ofdAudio
            // 
            this.ofdAudio.DefaultExt = "wav";
            // 
            // gbPlayer
            // 
            this.gbPlayer.Controls.Add(this.cbPlayer);
            this.gbPlayer.Controls.Add(this.label3);
            this.gbPlayer.Controls.Add(this.cbMute);
            this.gbPlayer.Controls.Add(this.tbPlayer);
            this.gbPlayer.Location = new System.Drawing.Point(6, 111);
            this.gbPlayer.Name = "gbPlayer";
            this.gbPlayer.Size = new System.Drawing.Size(164, 140);
            this.gbPlayer.TabIndex = 43;
            this.gbPlayer.TabStop = false;
            this.gbPlayer.Text = "Player";
            // 
            // cbPlayer
            // 
            this.cbPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayer.Location = new System.Drawing.Point(6, 36);
            this.cbPlayer.Name = "cbPlayer";
            this.cbPlayer.Size = new System.Drawing.Size(152, 21);
            this.cbPlayer.TabIndex = 12;
            this.cbPlayer.SelectedIndexChanged += new System.EventHandler(this.cbPlayer_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Select player:";
            // 
            // cbMute
            // 
            this.cbMute.AutoSize = true;
            this.cbMute.Location = new System.Drawing.Point(6, 63);
            this.cbMute.Name = "cbMute";
            this.cbMute.Size = new System.Drawing.Size(50, 17);
            this.cbMute.TabIndex = 49;
            this.cbMute.Text = "Mute";
            this.cbMute.UseVisualStyleBackColor = true;
            // 
            // tbPlayer
            // 
            this.tbPlayer.Location = new System.Drawing.Point(6, 90);
            this.tbPlayer.Maximum = 65535;
            this.tbPlayer.Name = "tbPlayer";
            this.tbPlayer.Size = new System.Drawing.Size(155, 42);
            this.tbPlayer.TabIndex = 47;
            this.tbPlayer.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tbRecorder
            // 
            this.tbRecorder.Location = new System.Drawing.Point(6, 90);
            this.tbRecorder.Maximum = 65535;
            this.tbRecorder.Name = "tbRecorder";
            this.tbRecorder.Size = new System.Drawing.Size(155, 42);
            this.tbRecorder.TabIndex = 50;
            this.tbRecorder.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // cbRecorderLine
            // 
            this.cbRecorderLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRecorderLine.Location = new System.Drawing.Point(6, 63);
            this.cbRecorderLine.Name = "cbRecorderLine";
            this.cbRecorderLine.Size = new System.Drawing.Size(152, 21);
            this.cbRecorderLine.TabIndex = 52;
            // 
            // gbRecorder
            // 
            this.gbRecorder.Controls.Add(this.cbRecorder);
            this.gbRecorder.Controls.Add(this.cbRecorderLine);
            this.gbRecorder.Controls.Add(this.label2);
            this.gbRecorder.Controls.Add(this.tbRecorder);
            this.gbRecorder.Location = new System.Drawing.Point(176, 111);
            this.gbRecorder.Name = "gbRecorder";
            this.gbRecorder.Size = new System.Drawing.Size(164, 140);
            this.gbRecorder.TabIndex = 53;
            this.gbRecorder.TabStop = false;
            this.gbRecorder.Text = "Recorder";
            // 
            // cbRecorder
            // 
            this.cbRecorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRecorder.Location = new System.Drawing.Point(6, 36);
            this.cbRecorder.Name = "cbRecorder";
            this.cbRecorder.Size = new System.Drawing.Size(152, 21);
            this.cbRecorder.TabIndex = 11;
            this.cbRecorder.SelectedIndexChanged += new System.EventHandler(this.cbRecorder_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Select recorder:";
            // 
            // gbDictophone
            // 
            this.gbDictophone.Controls.Add(this.nudVolumeLevelScale);
            this.gbDictophone.Controls.Add(this.label5);
            this.gbDictophone.Controls.Add(this.nudSilentLevel);
            this.gbDictophone.Controls.Add(this.label4);
            this.gbDictophone.Controls.Add(this.cbSkipSilent);
            this.gbDictophone.Controls.Add(this.nudBufferSizeInMs);
            this.gbDictophone.Controls.Add(this.label1);
            this.gbDictophone.Location = new System.Drawing.Point(6, 28);
            this.gbDictophone.Name = "gbDictophone";
            this.gbDictophone.Size = new System.Drawing.Size(334, 77);
            this.gbDictophone.TabIndex = 54;
            this.gbDictophone.TabStop = false;
            this.gbDictophone.Text = "General";
            // 
            // nudVolumeLevelScale
            // 
            this.nudVolumeLevelScale.Location = new System.Drawing.Point(273, 49);
            this.nudVolumeLevelScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVolumeLevelScale.Name = "nudVolumeLevelScale";
            this.nudVolumeLevelScale.Size = new System.Drawing.Size(52, 20);
            this.nudVolumeLevelScale.TabIndex = 54;
            this.nudVolumeLevelScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudVolumeLevelScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(173, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "Volume scale in %";
            // 
            // nudSilentLevel
            // 
            this.nudSilentLevel.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudSilentLevel.Location = new System.Drawing.Point(96, 23);
            this.nudSilentLevel.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            this.nudSilentLevel.Name = "nudSilentLevel";
            this.nudSilentLevel.Size = new System.Drawing.Size(59, 20);
            this.nudSilentLevel.TabIndex = 52;
            this.nudSilentLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSilentLevel.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Silent level";
            // 
            // cbSkipSilent
            // 
            this.cbSkipSilent.AutoSize = true;
            this.cbSkipSilent.Location = new System.Drawing.Point(3, 46);
            this.cbSkipSilent.Name = "cbSkipSilent";
            this.cbSkipSilent.Size = new System.Drawing.Size(74, 17);
            this.cbSkipSilent.TabIndex = 50;
            this.cbSkipSilent.Text = "Skip silent";
            this.cbSkipSilent.UseVisualStyleBackColor = true;
            // 
            // nudBufferSizeInMs
            // 
            this.nudBufferSizeInMs.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudBufferSizeInMs.Location = new System.Drawing.Point(273, 23);
            this.nudBufferSizeInMs.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBufferSizeInMs.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudBufferSizeInMs.Name = "nudBufferSizeInMs";
            this.nudBufferSizeInMs.Size = new System.Drawing.Size(52, 20);
            this.nudBufferSizeInMs.TabIndex = 1;
            this.nudBufferSizeInMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudBufferSizeInMs.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buffer size in ms";
            // 
            // sfdAudio
            // 
            this.sfdAudio.DefaultExt = "wav";
            this.sfdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3";
            // 
            // tbTimeline
            // 
            this.tbTimeline.Location = new System.Drawing.Point(0, 257);
            this.tbTimeline.Maximum = 65535;
            this.tbTimeline.Name = "tbTimeline";
            this.tbTimeline.Size = new System.Drawing.Size(347, 42);
            this.tbTimeline.TabIndex = 55;
            this.tbTimeline.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 324);
            this.Controls.Add(this.tbTimeline);
            this.Controls.Add(this.gbDictophone);
            this.Controls.Add(this.gbRecorder);
            this.Controls.Add(this.gbPlayer);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.tsMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Dictophone";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.gbPlayer.ResumeLayout(false);
            this.gbPlayer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRecorder)).EndInit();
            this.gbRecorder.ResumeLayout(false);
            this.gbRecorder.PerformLayout();
            this.gbDictophone.ResumeLayout(false);
            this.gbDictophone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolumeLevelScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSilentLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeInMs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTimeline)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbRecord;
        private System.Windows.Forms.ToolStripButton tsbPlay;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbPause;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStripProgressBar tspProgress;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.ToolStripButton tsbForward;
        private System.Windows.Forms.ToolStripButton tsbBackward;
        private System.Windows.Forms.ToolStripButton tsbPlayFrom;
        private System.Windows.Forms.ToolStripTextBox tstStep;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox tstTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.OpenFileDialog ofdAudio;
        private System.Windows.Forms.ToolStripStatusLabel tsslPosition;
        private System.Windows.Forms.GroupBox gbPlayer;
        private System.Windows.Forms.ComboBox cbPlayer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbPlayer;
        private System.Windows.Forms.CheckBox cbMute;
        private System.Windows.Forms.TrackBar tbRecorder;
        private System.Windows.Forms.ComboBox cbRecorderLine;
        private System.Windows.Forms.GroupBox gbRecorder;
        private System.Windows.Forms.ComboBox cbRecorder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbDictophone;
        private System.Windows.Forms.NumericUpDown nudBufferSizeInMs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSkipSilent;
        private System.Windows.Forms.NumericUpDown nudSilentLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudVolumeLevelScale;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tsbRecordFrom;
        private System.Windows.Forms.SaveFileDialog sfdAudio;
        private System.Windows.Forms.TrackBar tbTimeline;
    }
}

