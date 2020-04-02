using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Alvas.Audio;
using System.IO;

namespace AudioExCS
{
    public class DictaphonePanel : UserControl
    {
        private const string timeFormat = "Time in ms: {0}";

        public DictaphonePanel()
        {
            InitializeComponent();
            dict = new Dictaphone();
            dict.SoundLevelMeter = vum;
            dict.ChangeState += new Dictaphone.ChangeStateEventHandler(dict_ChangeState);
            dict.ChangePosition += new Dictaphone.ChangePositionEventHandler(dict_ChangePosition);
            int leftVolume = -1;
            int rightVolume = -1;
            dict.GetVolume(ref leftVolume, ref rightVolume);
            //tbVolumeLeft.Value = leftVolume;
            //tbVolumeRight.Value = rightVolume;
        }
        private ImageList ilButtons;
        private Alvas.Audio.AudioCompressionManager acm;
        private GroupBox gbRecorder;
        private Button btnStopRec;
        private Button btnRec;
        private Button btnPauseRec;
        private SaveFileDialog sfdFile;
        private GroupBox gbPlayer;
        private Button btnStop;
        private Button btnPlay;
        private Button btnPause;
        private Alvas.Audio.SoundLevelMeter vum;
        private Label lblTime;
        private GroupBox gbSetup;
        private CheckBox cbMemory;
        private ComboBox cbRecorder;
        private TextBox tbFile;
        private Label label4;
        private ComboBox cbFormat;
        private Label label3;
        private ComboBox cbFormatTag;
        private Label label5;
        private Button btnFile;
        private Label label6;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictaphonePanel));
            this.ilButtons = new System.Windows.Forms.ImageList(this.components);
            this.acm = new Alvas.Audio.AudioCompressionManager();
            this.gbRecorder = new System.Windows.Forms.GroupBox();
            this.btnStopRec = new System.Windows.Forms.Button();
            this.btnRec = new System.Windows.Forms.Button();
            this.btnPauseRec = new System.Windows.Forms.Button();
            this.sfdFile = new System.Windows.Forms.SaveFileDialog();
            this.gbPlayer = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.vum = new Alvas.Audio.SoundLevelMeter();
            this.lblTime = new System.Windows.Forms.Label();
            this.gbSetup = new System.Windows.Forms.GroupBox();
            this.cbMemory = new System.Windows.Forms.CheckBox();
            this.cbRecorder = new System.Windows.Forms.ComboBox();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFormatTag = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFile = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.gbRecorder.SuspendLayout();
            this.gbPlayer.SuspendLayout();
            this.gbSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilButtons
            // 
            this.ilButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilButtons.ImageStream")));
            this.ilButtons.TransparentColor = System.Drawing.Color.Silver;
//            this.ilButtons.Images.SetKeyName(0, "");
//            this.ilButtons.Images.SetKeyName(1, "");
//            this.ilButtons.Images.SetKeyName(2, "");
//            this.ilButtons.Images.SetKeyName(3, "");
//            this.ilButtons.Images.SetKeyName(4, "");
            // 
            // gbRecorder
            // 
            this.gbRecorder.Controls.Add(this.btnStopRec);
            this.gbRecorder.Controls.Add(this.btnRec);
            this.gbRecorder.Controls.Add(this.btnPauseRec);
            this.gbRecorder.Location = new System.Drawing.Point(3, 237);
            this.gbRecorder.Name = "gbRecorder";
            this.gbRecorder.Size = new System.Drawing.Size(217, 51);
            this.gbRecorder.TabIndex = 42;
            this.gbRecorder.TabStop = false;
            this.gbRecorder.Text = "RecorderEx";
            // 
            // btnStopRec
            // 
            this.btnStopRec.Enabled = false;
            this.btnStopRec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStopRec.ImageIndex = 3;
            this.btnStopRec.ImageList = this.ilButtons;
            this.btnStopRec.Location = new System.Drawing.Point(148, 19);
            this.btnStopRec.Name = "btnStopRec";
            this.btnStopRec.Size = new System.Drawing.Size(60, 23);
            this.btnStopRec.TabIndex = 23;
            this.btnStopRec.Text = "Stop";
            this.btnStopRec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStopRec.Click += new System.EventHandler(this.btnStopRec_Click);
            // 
            // btnRec
            // 
            this.btnRec.BackColor = System.Drawing.SystemColors.Control;
            this.btnRec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRec.ImageIndex = 4;
            this.btnRec.ImageList = this.ilButtons;
            this.btnRec.Location = new System.Drawing.Point(8, 19);
            this.btnRec.Name = "btnRec";
            this.btnRec.Size = new System.Drawing.Size(60, 23);
            this.btnRec.TabIndex = 22;
            this.btnRec.Text = "Rec";
            this.btnRec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.btnRec.UseVisualStyleBackColor = false;
            this.btnRec.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // btnPauseRec
            // 
            this.btnPauseRec.Enabled = false;
            this.btnPauseRec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPauseRec.ImageIndex = 2;
            this.btnPauseRec.ImageList = this.ilButtons;
            this.btnPauseRec.Location = new System.Drawing.Point(78, 19);
            this.btnPauseRec.Name = "btnPauseRec";
            this.btnPauseRec.Size = new System.Drawing.Size(60, 23);
            this.btnPauseRec.TabIndex = 24;
            this.btnPauseRec.Text = "Pause";
            this.btnPauseRec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPauseRec.Click += new System.EventHandler(this.btnPauseRec_Click);
            // 
            // sfdFile
            // 
            this.sfdFile.DefaultExt = "wav";
            this.sfdFile.Filter = "*.wav|*.wav";
            // 
            // gbPlayer
            // 
            this.gbPlayer.Controls.Add(this.btnStop);
            this.gbPlayer.Controls.Add(this.btnPlay);
            this.gbPlayer.Controls.Add(this.btnPause);
            this.gbPlayer.Location = new System.Drawing.Point(3, 383);
            this.gbPlayer.Name = "gbPlayer";
            this.gbPlayer.Size = new System.Drawing.Size(217, 51);
            this.gbPlayer.TabIndex = 41;
            this.gbPlayer.TabStop = false;
            this.gbPlayer.Text = "PlayerEx";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStop.ImageIndex = 3;
            this.btnStop.ImageList = this.ilButtons;
            this.btnStop.Location = new System.Drawing.Point(148, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(60, 23);
            this.btnStop.TabIndex = 33;
            this.btnStop.Text = "Stop";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.SystemColors.Control;
            this.btnPlay.Enabled = false;
            this.btnPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlay.ImageIndex = 1;
            this.btnPlay.ImageList = this.ilButtons;
            this.btnPlay.Location = new System.Drawing.Point(8, 19);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(60, 23);
            this.btnPlay.TabIndex = 30;
            this.btnPlay.Text = "Play";
            this.btnPlay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPause.ImageIndex = 2;
            this.btnPause.ImageList = this.ilButtons;
            this.btnPause.Location = new System.Drawing.Point(78, 19);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(60, 23);
            this.btnPause.TabIndex = 32;
            this.btnPause.Text = "Pause";
            this.btnPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // vum
            // 
            this.vum.BackColor = System.Drawing.Color.Black;
            this.vum.ForeColor = System.Drawing.Color.Lime;
            this.vum.Location = new System.Drawing.Point(2, 308);
            this.vum.Name = "vum";
            this.vum.OwnerDraw = false;
            this.vum.Size = new System.Drawing.Size(217, 69);
            this.vum.TabIndex = 40;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(-1, 292);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(69, 13);
            this.lblTime.TabIndex = 39;
            this.lblTime.Text = "Time in ms: 0";
            // 
            // gbSetup
            // 
            this.gbSetup.Controls.Add(this.cbMemory);
            this.gbSetup.Controls.Add(this.cbRecorder);
            this.gbSetup.Controls.Add(this.tbFile);
            this.gbSetup.Controls.Add(this.label4);
            this.gbSetup.Controls.Add(this.cbFormat);
            this.gbSetup.Controls.Add(this.label3);
            this.gbSetup.Controls.Add(this.cbFormatTag);
            this.gbSetup.Controls.Add(this.label5);
            this.gbSetup.Controls.Add(this.btnFile);
            this.gbSetup.Controls.Add(this.label6);
            this.gbSetup.Location = new System.Drawing.Point(3, 3);
            this.gbSetup.Name = "gbSetup";
            this.gbSetup.Size = new System.Drawing.Size(217, 228);
            this.gbSetup.TabIndex = 38;
            this.gbSetup.TabStop = false;
            this.gbSetup.Text = "Recorder Options";
            // 
            // cbMemory
            // 
            this.cbMemory.Checked = true;
            this.cbMemory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMemory.Location = new System.Drawing.Point(6, 202);
            this.cbMemory.Name = "cbMemory";
            this.cbMemory.Size = new System.Drawing.Size(194, 17);
            this.cbMemory.TabIndex = 30;
            this.cbMemory.Text = "Record in memory";
            // 
            // cbRecorder
            // 
            this.cbRecorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRecorder.Location = new System.Drawing.Point(6, 32);
            this.cbRecorder.Name = "cbRecorder";
            this.cbRecorder.Size = new System.Drawing.Size(205, 21);
            this.cbRecorder.TabIndex = 15;
            this.cbRecorder.SelectedIndexChanged += new System.EventHandler(this.cbRecorder_SelectedIndexChanged);
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(6, 176);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(183, 20);
            this.tbFile.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Select file name:";
            // 
            // cbFormat
            // 
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.Location = new System.Drawing.Point(6, 128);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(205, 21);
            this.cbFormat.TabIndex = 12;
            this.cbFormat.SelectedIndexChanged += new System.EventHandler(this.cbFormat_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Select audio format:";
            // 
            // cbFormatTag
            // 
            this.cbFormatTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormatTag.Location = new System.Drawing.Point(6, 80);
            this.cbFormatTag.Name = "cbFormatTag";
            this.cbFormatTag.Size = new System.Drawing.Size(205, 21);
            this.cbFormatTag.TabIndex = 11;
            this.cbFormatTag.SelectedIndexChanged += new System.EventHandler(this.cbFormatTag_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Select audio format tag:";
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(189, 176);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(22, 20);
            this.btnFile.TabIndex = 17;
            this.btnFile.Text = "...";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Select audio recorder:";
            // 
            // DictaphonePanel
            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbRecorder);
            this.Controls.Add(this.gbPlayer);
            this.Controls.Add(this.vum);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.gbSetup);
            this.Name = "DictaphonePanel";
            this.Size = new System.Drawing.Size(226, 443);
            this.Load += new System.EventHandler(this.DictaphonePanel_Load);
            this.gbRecorder.ResumeLayout(false);
            this.gbPlayer.ResumeLayout(false);
            this.gbSetup.ResumeLayout(false);
            this.gbSetup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        void dict_ChangePosition(object sender, PositionEventArgs e)
        {
            lblTime.Text = string.Format(timeFormat, e.Position);
        }

        Dictaphone dict;

        private void EnumFormatTags()
        {
            //cbFormatTag.DisplayMember = "FormatTagName";
            //cbFormatTag.ValueMember = "FormatTag";
            cbFormatTag.DataSource = AudioCompressionManager.GetFormatTagList();
        }

        private void EnumRecorders()
        {
            int count = RecorderEx.RecorderCount;
            if (count > 0)
            {
                for (int i = -1; i < count; i++)
                {
                    cbRecorder.Items.Add(RecorderEx.GetRecorderName(i));
                }
                cbRecorder.SelectedIndex = 0;
            }
        }

        private void btnFile_Click(object sender, System.EventArgs e)
        {
            if (sfdFile.ShowDialog() == DialogResult.OK)
            {
                this.tbFile.Text = sfdFile.FileName;
            }
        }

        private void cbRecorder_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            dict.RecorderID = cbRecorder.SelectedIndex - 1;
        }

        private void EnumFormats()
        {
            //cbFormat.DisplayMember = "FormatName";
            //cbFormat.ValueMember = "FormatHandle";
            int formatTag = ((FormatTagDetails)cbFormatTag.SelectedValue).FormatTag;
            cbFormat.DataSource = AudioCompressionManager.GetFormatList(formatTag);
        }

        private void cbFormatTag_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            EnumFormats();
        }

        private void cbFormat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FormatDetails pafd = (FormatDetails)cbFormat.SelectedItem;
            dict.Format = pafd.FormatHandle;
        }

        private void btnRec_Click(object sender, System.EventArgs e)
        {
            if (dict.Format == IntPtr.Zero)
            {
                MessageBox.Show("Please, select audio format before start recording!");
                return;
            }
            dict.StartRecord(cbMemory.Checked, tbFile.Text);
        }

        private void btnStopRec_Click(object sender, System.EventArgs e)
        {
            dict.StopRecord();
        }

        private void btnPauseRec_Click(object sender, System.EventArgs e)
        {
            dict.PauseRecord();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            dict.StartPlay();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            dict.ClosePlayer();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            dict.PausePlay();
        }

        void dict_ChangeState(object sender, DictaphoneStateEventArgs e)
        {
            switch (e.State)
            {
                case DictaphoneState.Initial:
                    lblTime.Text = string.Format(timeFormat, 0);
                    btnStop.Enabled = false;
                    btnPause.Enabled = false;
                    btnPlay.Enabled = true;
                    gbSetup.Enabled = true;
                    gbRecorder.Enabled = true;

                    btnStopRec.Enabled = false;
                    btnPauseRec.Enabled = false;
                    btnRec.Enabled = true;
                    btnPlay.Enabled = true;
                    gbSetup.Enabled = true;
                    gbPlayer.Enabled = true;
                    break;
                case DictaphoneState.PausePlay:
                    btnPause.Enabled = false;
                    btnPlay.Enabled = true;
                    break;
                case DictaphoneState.Play:
                    btnPlay.Enabled = false;
                    btnPause.Enabled = true;
                    btnStop.Enabled = true;

                    btnRec.Enabled = false;
                    gbSetup.Enabled = false;
                    gbRecorder.Enabled = false;
                    break;
                case DictaphoneState.PauseRecord:
                    btnPauseRec.Enabled = false;
                    btnRec.Enabled = true;
                    btnStopRec.Enabled = true;
                    break;
                case DictaphoneState.Record:
                    btnStopRec.Enabled = true;
                    btnPauseRec.Enabled = true;
                    btnRec.Enabled = false;
                    gbSetup.Enabled = false;
                    gbPlayer.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        //---------

        //private void tbVolumeLeft_Scroll(object sender, EventArgs e)
        //{
        //    SetVolume(tbVolumeLeft.Value, true);
        //}

        //private void SetVolume(int value, bool isLeft)
        //{
        //    int leftVolume = -1;
        //    int rightVolume = -1;
        //    dict.GetVolume(ref leftVolume, ref rightVolume);
        //    if (isLeft)
        //    {
        //        leftVolume = value;
        //    }
        //    else
        //    {
        //        rightVolume = value;
        //    }
        //    dict.SetVolume(leftVolume, rightVolume);
        //}

        //private void tbVolumeRight_Scroll(object sender, EventArgs e)
        //{
        //    SetVolume(tbVolumeRight.Value, false);
        //}

        private void DictaphonePanel_Load(object sender, EventArgs e)
        {
            tbFile.Text = Path.ChangeExtension(Application.ExecutablePath, ".wav");
            EnumRecorders();
            EnumFormatTags();
        }

    }
}
