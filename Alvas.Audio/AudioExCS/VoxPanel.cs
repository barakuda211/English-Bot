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
    public class VoxPanel : UserControl
    {
        private OpenFileDialog ofdVox;
        private Button btnConvert;
        private Button btnOpen;
        private ComboBox cbSamplesPerSec;
        private TextBox tbVox;
        private Button btnConvert2;
        private Button btnWav;
        private ComboBox cbSamplesPerSec2;
        private TextBox tbWav;
        private CheckBox cbResample;
        private Button btnVox2Mp3;

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
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.cbSamplesPerSec = new System.Windows.Forms.ComboBox();
            this.tbVox = new System.Windows.Forms.TextBox();
            this.btnConvert2 = new System.Windows.Forms.Button();
            this.btnWav = new System.Windows.Forms.Button();
            this.cbSamplesPerSec2 = new System.Windows.Forms.ComboBox();
            this.tbWav = new System.Windows.Forms.TextBox();
            this.cbResample = new System.Windows.Forms.CheckBox();
            this.btnVox2Mp3 = new System.Windows.Forms.Button();
            this.ofdVox = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(6, 58);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(212, 23);
            this.btnConvert.TabIndex = 5;
            this.btnConvert.Text = "Vox -> Wav";
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(194, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(24, 23);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "...";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbSamplesPerSec
            // 
            this.cbSamplesPerSec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSamplesPerSec.Location = new System.Drawing.Point(6, 31);
            this.cbSamplesPerSec.Name = "cbSamplesPerSec";
            this.cbSamplesPerSec.Size = new System.Drawing.Size(212, 21);
            this.cbSamplesPerSec.TabIndex = 7;
            // 
            // tbVox
            // 
            this.tbVox.Location = new System.Drawing.Point(6, 5);
            this.tbVox.Name = "tbVox";
            this.tbVox.Size = new System.Drawing.Size(187, 20);
            this.tbVox.TabIndex = 6;
            // 
            // btnConvert2
            // 
            this.btnConvert2.Location = new System.Drawing.Point(6, 196);
            this.btnConvert2.Name = "btnConvert2";
            this.btnConvert2.Size = new System.Drawing.Size(212, 23);
            this.btnConvert2.TabIndex = 9;
            this.btnConvert2.Text = "Wav -> Vox";
            this.btnConvert2.Click += new System.EventHandler(this.btnConvert2_Click);
            // 
            // btnWav
            // 
            this.btnWav.Location = new System.Drawing.Point(194, 117);
            this.btnWav.Name = "btnWav";
            this.btnWav.Size = new System.Drawing.Size(24, 23);
            this.btnWav.TabIndex = 8;
            this.btnWav.Text = "...";
            this.btnWav.Click += new System.EventHandler(this.btnWav_Click);
            // 
            // cbSamplesPerSec2
            // 
            this.cbSamplesPerSec2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSamplesPerSec2.Enabled = false;
            this.cbSamplesPerSec2.Location = new System.Drawing.Point(6, 169);
            this.cbSamplesPerSec2.Name = "cbSamplesPerSec2";
            this.cbSamplesPerSec2.Size = new System.Drawing.Size(212, 21);
            this.cbSamplesPerSec2.TabIndex = 11;
            // 
            // tbWav
            // 
            this.tbWav.Location = new System.Drawing.Point(6, 120);
            this.tbWav.Name = "tbWav";
            this.tbWav.Size = new System.Drawing.Size(187, 20);
            this.tbWav.TabIndex = 10;
            // 
            // cbResample
            // 
            this.cbResample.Location = new System.Drawing.Point(6, 146);
            this.cbResample.Name = "cbResample";
            this.cbResample.Size = new System.Drawing.Size(90, 17);
            this.cbResample.TabIndex = 12;
            this.cbResample.Text = "Resample";
            this.cbResample.CheckedChanged += new System.EventHandler(this.cbResample_CheckedChanged);
            // 
            // ofdVox
            // 
            ofdVox.Filter = "*.vox|*.vox";
            // 
            // btnVox2Mp3
            // 
            this.btnVox2Mp3.Location = new System.Drawing.Point(6, 87);
            this.btnVox2Mp3.Name = "btnVox2Mp3";
            this.btnVox2Mp3.Size = new System.Drawing.Size(212, 23);
            this.btnVox2Mp3.TabIndex = 13;
            this.btnVox2Mp3.Text = "Vox -> Mp3";
            this.btnVox2Mp3.Click += new System.EventHandler(this.btnVox2Mp3_Click);
            // 
            // VoxPanel
            // 
            this.Controls.Add(this.btnVox2Mp3);
            this.Controls.Add(this.cbResample);
            this.Controls.Add(this.btnConvert2);
            this.Controls.Add(this.btnWav);
            this.Controls.Add(this.cbSamplesPerSec2);
            this.Controls.Add(this.tbWav);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.cbSamplesPerSec);
            this.Controls.Add(this.tbVox);
            this.Name = "VoxPanel";
            this.Size = new System.Drawing.Size(224, 243);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public VoxPanel()
        {
            InitializeComponent();
            cbSamplesPerSec.DataSource = new int[] {6000, 8000, 11025, 12000, 16000,
22050, 24000, 32000, 44100, 48000};
            cbSamplesPerSec2.DataSource = new int[] {6000, 8000, 11025, 12000, 16000,
22050, 24000, 32000, 44100, 48000};
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            string voxFile = tbVox.Text;
            if (voxFile == string.Empty)
            {
                MessageBox.Show("Please, select file for converting");
                return;
            }
            string wavFile = Path.ChangeExtension(voxFile, ".wav");
            Vox2Wav(voxFile, wavFile, (int)cbSamplesPerSec.SelectedValue);
            MessageBox.Show(string.Format("The result is stored in the file '{0}'. Choose next file for the converting.", wavFile), "The conversion was executed successfully");
            OpenContainingFolder(wavFile);
        }

        private void OpenContainingFolder(string fileName)
        {
            Process.Start(Path.GetDirectoryName(fileName));
        }

        private static void Vox2Wav(string voxFile, string wavFile, int samplesPerSec)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(voxFile));
            IntPtr format = AudioCompressionManager.GetPcmFormat(1, 16, samplesPerSec);
            WaveWriter ww = new WaveWriter(File.Create(wavFile), AudioCompressionManager.FormatBytes(format));
            Vox.Vox2Wav(br, ww);
            br.Close();
            ww.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile(VoxFilter, tbVox);
        }

        private void btnWav_Click(object sender, EventArgs e)
        {
            OpenFile(WavFilter, tbWav);
        }

        private void OpenFile(string filter, TextBox tb)
        {
            ofdVox.Filter = filter;
            if (ofdVox.ShowDialog() == DialogResult.OK)
            {
                tb.Text = ofdVox.FileName;
            }
        }

        private const string VoxFilter = "*.vox|*.vox";
        private const string WavFilter = "*.wav|*.wav";

        private void btnConvert2_Click(object sender, EventArgs e)
        {
            string wavFile = tbWav.Text;
            if (wavFile == string.Empty)
            {
                MessageBox.Show("Please, select file for converting");
                return;
            }
            string voxFile = Path.ChangeExtension(wavFile, ".vox");
            Wav2Vox(wavFile, voxFile, (int)cbSamplesPerSec2.SelectedValue, cbResample.Checked);
            MessageBox.Show(string.Format("The result is stored in the file '{0}'. Choose next file for the converting.", voxFile), "The conversion was executed successfully");
            OpenContainingFolder(voxFile);
        }

        private static void Wav2Vox(string inFile, string outFile, int samplesPerSec, bool isResample)
        {
            WaveReader wr = new WaveReader(File.OpenRead(inFile));
            IntPtr format = wr.ReadFormat();
            byte[] data = wr.ReadData();
            wr.Close();
            WaveFormat wf = AudioCompressionManager.GetWaveFormat(format);
            if (wf.wFormatTag != AudioCompressionManager.PcmFormatTag)//Decode if not PCM data 
            {
                Decode2Pcm(ref format, ref data, ref wf);
            }
            if (isResample && wf.nSamplesPerSec != samplesPerSec)
            {
                Resample(ref format, ref data, ref wf, samplesPerSec);
            }
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(outFile));
            BinaryReader br = new BinaryReader(new MemoryStream(data));
            Vox.Raw2Vox(br, bw, wf.wBitsPerSample);
            br.Close();
            bw.Close();
        }

        private static void Resample(ref IntPtr format, ref byte[] data, ref WaveFormat wf, int samplesPerSec)
        {
            IntPtr newFormat = AudioCompressionManager.GetPcmFormat(wf.nChannels, wf.wBitsPerSample, samplesPerSec);
            byte[] buffer = AudioCompressionManager.Convert(format, newFormat, data, false);
            format = newFormat;
            wf = AudioCompressionManager.GetWaveFormat(newFormat);
            data = buffer;
        }

        private static void Decode2Pcm(ref IntPtr format, ref byte[] data, ref WaveFormat wf)
        {
            IntPtr newFormat = AudioCompressionManager.GetCompatibleFormat(format, AudioCompressionManager.PcmFormatTag);
            byte[] buffer = AudioCompressionManager.Convert(format, newFormat, data, false);
            wf = AudioCompressionManager.GetWaveFormat(newFormat);
            format = newFormat;
            data = buffer;
        }

        private void cbResample_CheckedChanged(object sender, EventArgs e)
        {
            cbSamplesPerSec2.Enabled = cbResample.Checked;
        }

        private void btnVox2Mp3_Click(object sender, EventArgs e)
        {
            string voxFile = tbVox.Text;
            if (voxFile == string.Empty)
            {
                MessageBox.Show("Please, select file for converting");
                return;
            }
            string mp3File = Path.ChangeExtension(voxFile, ".mp3");
            Vox.Vox2Mp3(voxFile, mp3File, (int)cbSamplesPerSec.SelectedValue);
            MessageBox.Show(string.Format("The result is stored in the file '{0}'. Choose next file for the converting.", mp3File), "The conversion was executed successfully");
            OpenContainingFolder(mp3File);
        }

    }
}
