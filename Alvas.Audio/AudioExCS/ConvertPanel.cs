using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Alvas.Audio;
using System.Diagnostics;

namespace AudioExCS
{
    public class ConvertPanel : UserControl
    {
        public ConvertPanel()
        {
            InitializeComponent();
            ofdFile.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*";
        }

        private OpenFileDialog ofdFile;
        private GroupBox groupBox1;
        private Label lblFileFormat;
        private TextBox tbFile2;
        private Button btnFile2;
        private Button btnMakeMp3;
        private GroupBox gbConvert;
        private Button btnDialog;
        private RadioButton rbDialog;
        private RadioButton rbCombobox;
        private Button btnConvert;
        private ComboBox cbFormatConverted;
		private ProgressBar pbConvert;

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
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFileFormat = new System.Windows.Forms.Label();
            this.tbFile2 = new System.Windows.Forms.TextBox();
            this.btnFile2 = new System.Windows.Forms.Button();
            this.btnMakeMp3 = new System.Windows.Forms.Button();
            this.gbConvert = new System.Windows.Forms.GroupBox();
            this.btnDialog = new System.Windows.Forms.Button();
            this.rbDialog = new System.Windows.Forms.RadioButton();
            this.rbCombobox = new System.Windows.Forms.RadioButton();
            this.btnConvert = new System.Windows.Forms.Button();
            this.cbFormatConverted = new System.Windows.Forms.ComboBox();
			this.pbConvert = new System.Windows.Forms.ProgressBar();
			this.groupBox1.SuspendLayout();
            this.gbConvert.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofdFile
            // 
            this.ofdFile.DefaultExt = "wav";
            this.ofdFile.Filter = "*.wav;*.avi;*.mp3|*.wav;*.avi;*.mp3|*.wav|*.wav|*.avi|*.avi|*.mp3|*.mp3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFileFormat);
            this.groupBox1.Controls.Add(this.tbFile2);
            this.groupBox1.Controls.Add(this.btnFile2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 66);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select source file name";
            // 
            // lblFileFormat
            // 
            this.lblFileFormat.AutoSize = true;
            this.lblFileFormat.Location = new System.Drawing.Point(6, 42);
            this.lblFileFormat.Name = "lblFileFormat";
            this.lblFileFormat.Size = new System.Drawing.Size(0, 13);
            this.lblFileFormat.TabIndex = 29;
            // 
            // tbFile2
            // 
            this.tbFile2.BackColor = System.Drawing.SystemColors.Window;
            this.tbFile2.Location = new System.Drawing.Point(6, 19);
            this.tbFile2.Name = "tbFile2";
            this.tbFile2.ReadOnly = true;
            this.tbFile2.Size = new System.Drawing.Size(183, 20);
            this.tbFile2.TabIndex = 22;
            // 
            // btnFile2
            // 
            this.btnFile2.Location = new System.Drawing.Point(188, 19);
            this.btnFile2.Name = "btnFile2";
            this.btnFile2.Size = new System.Drawing.Size(23, 20);
            this.btnFile2.TabIndex = 23;
            this.btnFile2.Text = "...";
            this.btnFile2.Click += new System.EventHandler(this.btnFile2_Click);
            // 
            // btnMakeMp3
            // 
            this.btnMakeMp3.Enabled = false;
            this.btnMakeMp3.Location = new System.Drawing.Point(10, 188);
            this.btnMakeMp3.Name = "btnMakeMp3";
            this.btnMakeMp3.Size = new System.Drawing.Size(203, 23);
            this.btnMakeMp3.TabIndex = 33;
            this.btnMakeMp3.Text = "Convert *.wav to *.mp3";
            this.btnMakeMp3.Click += new System.EventHandler(this.btnMakeMp3_Click);
            // 
            // gbConvert
            // 
            this.gbConvert.Controls.Add(this.btnDialog);
            this.gbConvert.Controls.Add(this.rbDialog);
            this.gbConvert.Controls.Add(this.rbCombobox);
            this.gbConvert.Controls.Add(this.btnConvert);
            this.gbConvert.Controls.Add(this.cbFormatConverted);
            this.gbConvert.Enabled = false;
            this.gbConvert.Location = new System.Drawing.Point(3, 75);
            this.gbConvert.Name = "gbConvert";
            this.gbConvert.Size = new System.Drawing.Size(215, 107);
            this.gbConvert.TabIndex = 32;
            this.gbConvert.TabStop = false;
            this.gbConvert.Text = "Select destination format";
            // 
            // btnDialog
            // 
            this.btnDialog.Enabled = false;
            this.btnDialog.Location = new System.Drawing.Point(26, 46);
            this.btnDialog.Name = "btnDialog";
            this.btnDialog.Size = new System.Drawing.Size(183, 23);
            this.btnDialog.TabIndex = 29;
            this.btnDialog.Text = "Select destination format";
            this.btnDialog.Click += new System.EventHandler(this.btnDialog_Click);
            // 
            // rbDialog
            // 
            this.rbDialog.Location = new System.Drawing.Point(6, 52);
            this.rbDialog.Name = "rbDialog";
            this.rbDialog.Size = new System.Drawing.Size(14, 13);
            this.rbDialog.TabIndex = 28;
            this.rbDialog.Click += new System.EventHandler(this.rbDialog_CheckedChanged);
            // 
            // rbCombobox
            // 
            this.rbCombobox.Checked = true;
            this.rbCombobox.Location = new System.Drawing.Point(6, 22);
            this.rbCombobox.Name = "rbCombobox";
            this.rbCombobox.Size = new System.Drawing.Size(14, 13);
            this.rbCombobox.TabIndex = 27;
            this.rbCombobox.TabStop = true;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(7, 75);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(202, 23);
            this.btnConvert.TabIndex = 27;
            this.btnConvert.Text = "Convert";
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // cbFormatConverted
            // 
            this.cbFormatConverted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormatConverted.Location = new System.Drawing.Point(26, 19);
            this.cbFormatConverted.Name = "cbFormatConverted";
            this.cbFormatConverted.Size = new System.Drawing.Size(183, 21);
            this.cbFormatConverted.TabIndex = 25;
            this.cbFormatConverted.SelectedIndexChanged += new System.EventHandler(this.cbFormatConverted_SelectedIndexChanged);
			// 
			// pbConvert
			// 
			this.pbConvert.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pbConvert.Location = new System.Drawing.Point(0, 222);
			this.pbConvert.Name = "pbConvert";
			this.pbConvert.Size = new System.Drawing.Size(227, 23);
			this.pbConvert.TabIndex = 35;
			// 
            // ConvertPanel
            // 
			this.Controls.Add(this.pbConvert);
			this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnMakeMp3);
            this.Controls.Add(this.gbConvert);
            this.Name = "ConvertPanel";
            this.Size = new System.Drawing.Size(227, 222);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbConvert.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        //private FileStream fs = null;
        private IntPtr oldFormat = IntPtr.Zero;
        private IntPtr newFormat = IntPtr.Zero;
        //private byte[] data = null;
		IAudioReader ar = null;

        private void GetFormatsConverted(IntPtr format)
        {
            newFormat = IntPtr.Zero;
            //cbFormatConverted.DataSource = AudioCompressionManager.GetCompatibleFormatList(format);
            cbFormatConverted.Items.Clear();
            FormatDetails[] fdArr = AudioCompressionManager.GetCompatibleFormatList(format);
            for (int i = 0; i < fdArr.Length; i++)
            {
                FormatDetails fd = fdArr[i];
                fd.ShowFormatTag = true;
                cbFormatConverted.Items.Add(fd);
            }
        }

        private void btnFile2_Click(object sender, EventArgs e)
        {
            if (ofdFile.ShowDialog(this) == DialogResult.OK)
            {
				tbFile2.Text = ofdFile.FileName;
				int lenExt = 4;
				string ext = ofdFile.FileName.Substring(ofdFile.FileName.Length - lenExt, 
					lenExt).ToLower();
				switch (ext)
				{
                    case ".au":
                    case ".snd":
                        ar = new AuReader(File.OpenRead(ofdFile.FileName));
                        break;
                    case ".wav":
						ar = new WaveReader(File.OpenRead(ofdFile.FileName));
						break;
					case ".avi" :
						ar = new AviReader(File.OpenRead(ofdFile.FileName));
						if (!((AviReader)ar).HasAudio)
						{
							MessageBox.Show("Avi stream has not audio track");
							return;
						}
						break;
					case ".mp3" :
						ar = new Mp3Reader(File.OpenRead(ofdFile.FileName));
						break;
					default:
                        ar = new DsReader(ofdFile.FileName);
                        if (!((DsReader)ar).HasAudio)
                        {
                            MessageBox.Show("DirectShow stream has not audio track");
                            return;
                        }
                        break;
				}
				oldFormat = ar.ReadFormat();
				FormatDetails fd = AudioCompressionManager.GetFormatDetails(oldFormat);
				lblFileFormat.Text = string.Format("{0} {1}", AudioCompressionManager.GetFormatTagDetails(fd.FormatTag).FormatTagName, fd.FormatName);
				GetFormatsConverted(oldFormat);
				gbConvert.Enabled = true;
				btnMakeMp3.Enabled = false;
			}
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (newFormat == IntPtr.Zero)
            {
                MessageBox.Show("Please, specify destination format for converting");
                return;
            }
			string fileName = tbFile2.Text + ".wav";
			int size = ar.Milliseconds2Bytes(1000);
			int len = ar.GetLengthInBytes();
			AcmConverter ac = new AcmConverter(oldFormat, newFormat, false);
			FileStream fs = new FileStream(fileName, FileMode.Create);
			WaveWriter ww = new WaveWriter(fs, AudioCompressionManager.FormatBytes(newFormat));
			pbConvert.Maximum = len;
			int y = 0;
            while (y < len)
			{
				pbConvert.Value = y;
				byte[] data = ar.ReadDataInBytes(y, size);
                if (data.Length == 0)
                {
                    break;
                } 
                y += data.Length;
				byte[] newData = ac.Convert(data);
				ww.WriteData(newData);
			}
			ww.Close();
			ar.Close();
			gbConvert.Enabled = false;
            btnMakeMp3.Enabled = tbFile2.Text.ToLower().EndsWith(".wav");
            OpenContainingFolder(fileName);
        }

        private void OpenContainingFolder(string fileName)
        {
            MessageBox.Show(string.Format("The result is stored in the file '{0}'. Choose next file for the converting.", fileName), "The conversion was executed successfully");
            Process.Start(Path.GetDirectoryName(fileName));
        }

        private void NewFormatFromComboBox()
        {
            newFormat = (cbFormatConverted.SelectedIndex >= 0) ? ((FormatDetails)cbFormatConverted.SelectedItem).FormatHandle : IntPtr.Zero;
        }

        private void cbFormatConverted_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewFormatFromComboBox();
        }

        private void ConvertDialog()
        {
            FormatChooseResult res = AudioCompressionManager.ChooseCompatibleFormat(this.Handle, oldFormat);
            if (res.Result != 0)
            {
                btnDialog.Text = "Select destination format";
            }
            else
            {
                newFormat = res.Format;
                btnDialog.Text = string.Format("{0} {1}", res.FormatTagName, res.FormatName);
                cbFormatConverted.Enabled = false;
            }
        }

        private void rbDialog_CheckedChanged(object sender, EventArgs e)
        {
            btnDialog.Enabled = rbDialog.Checked;
            cbFormatConverted.Enabled = !rbDialog.Checked;
            if (!rbDialog.Checked)
            {
                NewFormatFromComboBox();
            }
        }

        private void btnMakeMp3_Click(object sender, EventArgs e)
        {
            string fileName = tbFile2.Text + ".mp3";
            AudioCompressionManager.Wave2Mp3(tbFile2.Text, fileName);
            btnMakeMp3.Enabled = false;
            OpenContainingFolder(fileName);
        }

        private void btnDialog_Click(object sender, EventArgs e)
        {
            ConvertDialog();
        }

    }
}
