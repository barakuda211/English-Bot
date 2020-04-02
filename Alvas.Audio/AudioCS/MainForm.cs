using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Alvas.Audio;

namespace AudioCS
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	internal class MainForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Label lblSpeed;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer tInterval;
		private System.Windows.Forms.TrackBar tbLV;
		private System.Windows.Forms.TrackBar tbRV;
		private System.Windows.Forms.TrackBar tbSpeed;
		private System.Windows.Forms.ListBox lbFile;
		private System.Windows.Forms.Button tbnRecord;
		private System.Windows.Forms.Button btnStopRecord;
		private System.Windows.Forms.TextBox tbFileName;
		private System.Windows.Forms.Button btnFileName;
		private System.Windows.Forms.Label lblPosition;
		private System.Windows.Forms.Label lblRemaining;
		private System.Windows.Forms.Label lblDuration;
		private System.Windows.Forms.TrackBar tbPosition;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.OpenFileDialog ofdFile;
		private System.Windows.Forms.SaveFileDialog sfdFile;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.ImageList ilButtons;
		private Alvas.Audio.Player pl;
		private Alvas.Audio.Recorder rec;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton rb44100;
		private System.Windows.Forms.RadioButton rb22050;
		private System.Windows.Forms.RadioButton rb11025;
		private System.Windows.Forms.RadioButton rb8000;
		private System.Windows.Forms.RadioButton rb16;
		private System.Windows.Forms.RadioButton rb8;
		private System.Windows.Forms.GroupBox gbChannels;
		private System.Windows.Forms.RadioButton rbOneChannel;
		private System.Windows.Forms.RadioButton rbTwoChannel;
		private System.Windows.Forms.Label label2;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.btnPlay = new System.Windows.Forms.Button();
			this.ilButtons = new System.Windows.Forms.ImageList(this.components);
			this.btnPause = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.tbPosition = new System.Windows.Forms.TrackBar();
			this.tbLV = new System.Windows.Forms.TrackBar();
			this.tbRV = new System.Windows.Forms.TrackBar();
			this.tbSpeed = new System.Windows.Forms.TrackBar();
			this.lblSpeed = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tInterval = new System.Windows.Forms.Timer(this.components);
			this.lbFile = new System.Windows.Forms.ListBox();
			this.tbnRecord = new System.Windows.Forms.Button();
			this.btnStopRecord = new System.Windows.Forms.Button();
			this.tbFileName = new System.Windows.Forms.TextBox();
			this.btnFileName = new System.Windows.Forms.Button();
			this.lblPosition = new System.Windows.Forms.Label();
			this.lblRemaining = new System.Windows.Forms.Label();
			this.lblDuration = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lblStatus = new System.Windows.Forms.Label();
			this.btnOpen = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.rb44100 = new System.Windows.Forms.RadioButton();
			this.rb22050 = new System.Windows.Forms.RadioButton();
			this.rb11025 = new System.Windows.Forms.RadioButton();
			this.rb8000 = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rb16 = new System.Windows.Forms.RadioButton();
			this.rb8 = new System.Windows.Forms.RadioButton();
			this.gbChannels = new System.Windows.Forms.GroupBox();
			this.rbOneChannel = new System.Windows.Forms.RadioButton();
			this.rbTwoChannel = new System.Windows.Forms.RadioButton();
			this.pl = new Alvas.Audio.Player();
			this.rec = new Alvas.Audio.Recorder();
			this.ofdFile = new System.Windows.Forms.OpenFileDialog();
			this.sfdFile = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.tbPosition)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbLV)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbRV)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.gbChannels.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnPlay
			// 
			this.btnPlay.Enabled = false;
			this.btnPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPlay.ImageIndex = 1;
			this.btnPlay.ImageList = this.ilButtons;
			this.btnPlay.Location = new System.Drawing.Point(64, 64);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(56, 23);
			this.btnPlay.TabIndex = 6;
			this.btnPlay.Text = "Play";
			this.btnPlay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// ilButtons
			// 
			this.ilButtons.ImageSize = new System.Drawing.Size(16, 16);
			this.ilButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilButtons.ImageStream")));
			this.ilButtons.TransparentColor = System.Drawing.Color.Silver;
			// 
			// btnPause
			// 
			this.btnPause.Enabled = false;
			this.btnPause.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.btnPause.ImageIndex = 2;
			this.btnPause.ImageList = this.ilButtons;
			this.btnPause.Location = new System.Drawing.Point(120, 64);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(72, 23);
			this.btnPause.TabIndex = 7;
			this.btnPause.Text = "Pause";
			this.btnPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnStop.ImageIndex = 3;
			this.btnStop.ImageList = this.ilButtons;
			this.btnStop.Location = new System.Drawing.Point(192, 64);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(56, 23);
			this.btnStop.TabIndex = 8;
			this.btnStop.Text = "Stop";
			this.btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// tbPosition
			// 
			this.tbPosition.Location = new System.Drawing.Point(0, 224);
			this.tbPosition.Maximum = 100;
			this.tbPosition.Name = "tbPosition";
			this.tbPosition.Size = new System.Drawing.Size(256, 42);
			this.tbPosition.TabIndex = 9;
			this.tbPosition.TickStyle = System.Windows.Forms.TickStyle.None;
			this.tbPosition.Scroll += new System.EventHandler(this.tbPosition_Scroll);
			// 
			// tbLV
			// 
			this.tbLV.Location = new System.Drawing.Point(8, 96);
			this.tbLV.Minimum = 1;
			this.tbLV.Name = "tbLV";
			this.tbLV.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tbLV.Size = new System.Drawing.Size(42, 104);
			this.tbLV.TabIndex = 10;
			this.tbLV.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.tbLV.Value = 10;
			this.tbLV.Scroll += new System.EventHandler(this.tbLV_Scroll);
			// 
			// tbRV
			// 
			this.tbRV.Location = new System.Drawing.Point(48, 96);
			this.tbRV.Minimum = 1;
			this.tbRV.Name = "tbRV";
			this.tbRV.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tbRV.Size = new System.Drawing.Size(42, 104);
			this.tbRV.TabIndex = 11;
			this.tbRV.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.tbRV.Value = 10;
			this.tbRV.Scroll += new System.EventHandler(this.tbRV_Scroll);
			// 
			// tbSpeed
			// 
			this.tbSpeed.Location = new System.Drawing.Point(208, 104);
			this.tbSpeed.Name = "tbSpeed";
			this.tbSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tbSpeed.Size = new System.Drawing.Size(42, 104);
			this.tbSpeed.TabIndex = 12;
			this.tbSpeed.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.tbSpeed.Value = 5;
			this.tbSpeed.Scroll += new System.EventHandler(this.tbSpeed_Scroll);
			// 
			// lblSpeed
			// 
			this.lblSpeed.Location = new System.Drawing.Point(184, 200);
			this.lblSpeed.Name = "lblSpeed";
			this.lblSpeed.Size = new System.Drawing.Size(64, 16);
			this.lblSpeed.TabIndex = 13;
			this.lblSpeed.Text = "Speed: ";
			this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 200);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 16);
			this.label1.TabIndex = 14;
			this.label1.Text = "LV";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(56, 200);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 15;
			this.label2.Text = "RV";
			// 
			// tInterval
			// 
			this.tInterval.Interval = 500;
			this.tInterval.Tick += new System.EventHandler(this.tInterval_Tick);
			// 
			// lbFile
			// 
			this.lbFile.Location = new System.Drawing.Point(8, 8);
			this.lbFile.Name = "lbFile";
			this.lbFile.Size = new System.Drawing.Size(240, 56);
			this.lbFile.TabIndex = 16;
			this.lbFile.SelectedIndexChanged += new System.EventHandler(this.lbFile_SelectedIndexChanged);
			// 
			// tbnRecord
			// 
			this.tbnRecord.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.tbnRecord.ImageIndex = 4;
			this.tbnRecord.ImageList = this.ilButtons;
			this.tbnRecord.Location = new System.Drawing.Point(24, 224);
			this.tbnRecord.Name = "tbnRecord";
			this.tbnRecord.Size = new System.Drawing.Size(48, 23);
			this.tbnRecord.TabIndex = 17;
			this.tbnRecord.Text = "Rec";
			this.tbnRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.tbnRecord.Click += new System.EventHandler(this.tbnRecord_Click);
			// 
			// btnStopRecord
			// 
			this.btnStopRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnStopRecord.ImageIndex = 3;
			this.btnStopRecord.ImageList = this.ilButtons;
			this.btnStopRecord.Location = new System.Drawing.Point(176, 224);
			this.btnStopRecord.Name = "btnStopRecord";
			this.btnStopRecord.Size = new System.Drawing.Size(56, 23);
			this.btnStopRecord.TabIndex = 18;
			this.btnStopRecord.Text = "Stop";
			this.btnStopRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnStopRecord.Click += new System.EventHandler(this.btnStopRecord_Click);
			// 
			// tbFileName
			// 
			this.tbFileName.Location = new System.Drawing.Point(24, 200);
			this.tbFileName.Name = "tbFileName";
			this.tbFileName.Size = new System.Drawing.Size(184, 20);
			this.tbFileName.TabIndex = 19;
			this.tbFileName.Text = "";
			// 
			// btnFileName
			// 
			this.btnFileName.Location = new System.Drawing.Point(208, 200);
			this.btnFileName.Name = "btnFileName";
			this.btnFileName.Size = new System.Drawing.Size(22, 20);
			this.btnFileName.TabIndex = 20;
			this.btnFileName.Text = "...";
			this.btnFileName.Click += new System.EventHandler(this.btnFileName_Click);
			// 
			// lblPosition
			// 
			this.lblPosition.Location = new System.Drawing.Point(96, 112);
			this.lblPosition.Name = "lblPosition";
			this.lblPosition.Size = new System.Drawing.Size(112, 16);
			this.lblPosition.TabIndex = 21;
			this.lblPosition.Text = "Position: ";
			// 
			// lblRemaining
			// 
			this.lblRemaining.Location = new System.Drawing.Point(96, 136);
			this.lblRemaining.Name = "lblRemaining";
			this.lblRemaining.Size = new System.Drawing.Size(112, 16);
			this.lblRemaining.TabIndex = 22;
			this.lblRemaining.Text = "Remaining:";
			// 
			// lblDuration
			// 
			this.lblDuration.Location = new System.Drawing.Point(96, 160);
			this.lblDuration.Name = "lblDuration";
			this.lblDuration.Size = new System.Drawing.Size(112, 16);
			this.lblDuration.TabIndex = 23;
			this.lblDuration.Text = "Duration:";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(264, 278);
			this.tabControl1.TabIndex = 26;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lblStatus);
			this.tabPage1.Controls.Add(this.btnOpen);
			this.tabPage1.Controls.Add(this.lbFile);
			this.tabPage1.Controls.Add(this.tbPosition);
			this.tabPage1.Controls.Add(this.lblSpeed);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.tbLV);
			this.tabPage1.Controls.Add(this.tbSpeed);
			this.tabPage1.Controls.Add(this.btnPause);
			this.tabPage1.Controls.Add(this.btnPlay);
			this.tabPage1.Controls.Add(this.lblPosition);
			this.tabPage1.Controls.Add(this.lblRemaining);
			this.tabPage1.Controls.Add(this.tbRV);
			this.tabPage1.Controls.Add(this.btnStop);
			this.tabPage1.Controls.Add(this.lblDuration);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(256, 252);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Player";
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(96, 184);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(112, 16);
			this.lblStatus.TabIndex = 25;
			this.lblStatus.Text = "Status:";
			// 
			// btnOpen
			// 
			this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOpen.ImageIndex = 0;
			this.btnOpen.ImageList = this.ilButtons;
			this.btnOpen.Location = new System.Drawing.Point(8, 64);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(56, 23);
			this.btnOpen.TabIndex = 24;
			this.btnOpen.Text = "Open";
			this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Controls.Add(this.groupBox2);
			this.tabPage2.Controls.Add(this.gbChannels);
			this.tabPage2.Controls.Add(this.tbnRecord);
			this.tabPage2.Controls.Add(this.btnStopRecord);
			this.tabPage2.Controls.Add(this.tbFileName);
			this.tabPage2.Controls.Add(this.btnFileName);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(256, 252);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Recorder";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.rb44100);
			this.groupBox3.Controls.Add(this.rb22050);
			this.groupBox3.Controls.Add(this.rb11025);
			this.groupBox3.Controls.Add(this.rb8000);
			this.groupBox3.Location = new System.Drawing.Point(24, 120);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(208, 72);
			this.groupBox3.TabIndex = 26;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Samples Per Sec";
			// 
			// rb44100
			// 
			this.rb44100.Checked = true;
			this.rb44100.Location = new System.Drawing.Point(112, 40);
			this.rb44100.Name = "rb44100";
			this.rb44100.Size = new System.Drawing.Size(88, 24);
			this.rb44100.TabIndex = 3;
			this.rb44100.TabStop = true;
			this.rb44100.Text = "44100";
			// 
			// rb22050
			// 
			this.rb22050.Location = new System.Drawing.Point(8, 40);
			this.rb22050.Name = "rb22050";
			this.rb22050.Size = new System.Drawing.Size(88, 24);
			this.rb22050.TabIndex = 2;
			this.rb22050.Text = "22050";
			// 
			// rb11025
			// 
			this.rb11025.Location = new System.Drawing.Point(112, 16);
			this.rb11025.Name = "rb11025";
			this.rb11025.Size = new System.Drawing.Size(88, 24);
			this.rb11025.TabIndex = 1;
			this.rb11025.Text = "11025";
			// 
			// rb8000
			// 
			this.rb8000.Location = new System.Drawing.Point(8, 16);
			this.rb8000.Name = "rb8000";
			this.rb8000.Size = new System.Drawing.Size(88, 24);
			this.rb8000.TabIndex = 0;
			this.rb8000.Text = "8000";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rb16);
			this.groupBox2.Controls.Add(this.rb8);
			this.groupBox2.Location = new System.Drawing.Point(24, 64);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(208, 48);
			this.groupBox2.TabIndex = 25;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Bits Per Sample";
			// 
			// rb16
			// 
			this.rb16.Checked = true;
			this.rb16.Location = new System.Drawing.Point(112, 16);
			this.rb16.Name = "rb16";
			this.rb16.Size = new System.Drawing.Size(88, 24);
			this.rb16.TabIndex = 24;
			this.rb16.TabStop = true;
			this.rb16.Text = "16";
			// 
			// rb8
			// 
			this.rb8.Location = new System.Drawing.Point(8, 16);
			this.rb8.Name = "rb8";
			this.rb8.Size = new System.Drawing.Size(88, 24);
			this.rb8.TabIndex = 23;
			this.rb8.Text = "8";
			// 
			// gbChannels
			// 
			this.gbChannels.Controls.Add(this.rbOneChannel);
			this.gbChannels.Controls.Add(this.rbTwoChannel);
			this.gbChannels.Location = new System.Drawing.Point(24, 8);
			this.gbChannels.Name = "gbChannels";
			this.gbChannels.Size = new System.Drawing.Size(208, 48);
			this.gbChannels.TabIndex = 24;
			this.gbChannels.TabStop = false;
			this.gbChannels.Text = "Channels";
			// 
			// rbOneChannel
			// 
			this.rbOneChannel.Location = new System.Drawing.Point(8, 16);
			this.rbOneChannel.Name = "rbOneChannel";
			this.rbOneChannel.Size = new System.Drawing.Size(88, 24);
			this.rbOneChannel.TabIndex = 21;
			this.rbOneChannel.Text = "One channel";
			// 
			// rbTwoChannel
			// 
			this.rbTwoChannel.Checked = true;
			this.rbTwoChannel.Location = new System.Drawing.Point(112, 16);
			this.rbTwoChannel.Name = "rbTwoChannel";
			this.rbTwoChannel.Size = new System.Drawing.Size(88, 24);
			this.rbTwoChannel.TabIndex = 22;
			this.rbTwoChannel.TabStop = true;
			this.rbTwoChannel.Text = "Two channel";
			// 
			// pl
			// 
			this.pl.FileName = "";
			// 
			// sfdFile
			// 
			this.sfdFile.DefaultExt = "wav";
			this.sfdFile.Filter = "*.wav|*.wav";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(264, 278);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Alvas.Audio";
			((System.ComponentModel.ISupportInitialize)(this.tbPosition)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbLV)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbRV)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.gbChannels.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void btnPlay_Click(object sender, System.EventArgs e)
		{
			if (pl.FileName != "") 
			{
				pl.Play();
				btnPause.Text = "Pause";
				this.tInterval.Enabled = true;
			}
			else
			{
				MessageBox.Show("Please select audio file from list!");
			}
		}

		private void btnPause_Click(object sender, System.EventArgs e)
		{
			if (pl.Status == "paused")
			{
				pl.Resume();
				btnPause.Text = "Pause";
			}
			else
			{
				pl.Pause();
				btnPause.Text = "Resume";
			}
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			pl.Stop();
			this.tInterval.Enabled = false;
		}

		private void tInterval_Tick(object sender, System.EventArgs e)
		{
			this.Text = string.Format("Speed: {0}, LeftVolume: {1}, RightVolume: {2}", pl.Speed, pl.LeftVolume, pl.RightVolume);
			
			btnPause.Enabled = pl.Status != "stopped";
			btnStop.Enabled = pl.Status != "stopped";
			int pos = (pl.PositionInMS * this.tbPosition.Maximum) / pl.DurationInMS;
			this.tbPosition.Value = pos;
			this.lblPosition.Text = string.Format("Position: {0}", GetTime(pl.PositionInMS));
			this.lblRemaining.Text = string.Format("Remaining: {0}", GetTime(pl.DurationInMS - pl.PositionInMS));
			this.lblDuration.Text = string.Format("Duration: {0}", GetTime(pl.DurationInMS));
			this.lblStatus.Text = string.Format("Status: {0}", pl.Status);
		}

		private void lbFile_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			pl.FileName = lbFile.SelectedItem.ToString();
			btnPlay.Enabled = true;
		}

		private void tbPosition_Scroll(object sender, System.EventArgs e)
		{
			int pos = (this.tbPosition.Value * pl.DurationInMS) / this.tbPosition.Maximum;
			pl.ChangePosition(pos);
		}

		private void tbSpeed_Scroll(object sender, System.EventArgs e)
		{
			if (tbSpeed.Value == 5)
			{
				pl.Speed = 1000;
			}
			else if (tbSpeed.Value > 5)
			{
				pl.Speed = 1000 * (tbSpeed.Value - 5 + 1);
			}
			else if (tbSpeed.Value < 5)
			{
				pl.Speed = 1000 / (5 - tbSpeed.Value);
			}
			this.lblSpeed.Text = string.Format("Speed: {0}", pl.Speed/10);
		}

		private void tbLV_Scroll(object sender, System.EventArgs e)
		{
			pl.LeftVolume = tbLV.Value * 100;
		}

		private void tbRV_Scroll(object sender, System.EventArgs e)
		{
			pl.RightVolume = tbRV.Value * 100;
		}

		private void tbnRecord_Click(object sender, System.EventArgs e)
		{
			rec.Open();
			Recorder.Channel rc = rbOneChannel.Checked?Recorder.Channel.Mono:Recorder.Channel.Stereo;
			Recorder.BitsPerSample rbps = rb8.Checked?Recorder.BitsPerSample.Bps8:Recorder.BitsPerSample.Bps16;
			Recorder.SamplesPerSec rsps;
			if (this.rb8000.Checked)
			{
				rsps = Recorder.SamplesPerSec.Sps8000;
			}
			else if (this.rb11025.Checked)
			{
				rsps = Recorder.SamplesPerSec.Sps11025;
			}
			else if (this.rb22050.Checked)
			{
				rsps = Recorder.SamplesPerSec.Sps22050;
			}
			else
			{
				rsps = Recorder.SamplesPerSec.Sps44100;
			}
			rec.Configure(rc, rbps, rsps);
			rec.Record();
		}

		private void btnStopRecord_Click(object sender, System.EventArgs e)
		{
			rec.Stop();
			rec.Save(this.tbFileName.Text);
			rec.Close();
		}

		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			if (ofdFile.ShowDialog() == DialogResult.OK)
			{
				int ndx = this.lbFile.Items.Add(ofdFile.FileName);
				this.lbFile.SelectedIndex = ndx;
			}
		}

		private void btnFileName_Click(object sender, System.EventArgs e)
		{
			if (sfdFile.ShowDialog() == DialogResult.OK)
			{
				this.tbFileName.Text = sfdFile.FileName;
			}
		}

		private string GetTime(int ms)
		{
			DateTime dt = new DateTime((long)ms * 10000);
			return dt.ToString("T");
		}




	}
}
