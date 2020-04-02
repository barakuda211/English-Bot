using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alvas.Audio;

namespace SoxConvertCs
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ofdSoxPath.Filter = "*sox.exe|sox.exe";
            ofdInput.Filter = "*.Aifc;*.Aiff;*.Au;*.Hcom;*.Voc;*.Wav;*.Gsm;*.Lpc10;*.Ogg;*.Wv|.Aifc;*.Aiff;*.Au;*.Hcom;*.Voc;*.Wav;*.Gsm;*.Lpc10;*.Ogg;*.Wv|*.*|*.*";
            string[] arr = new string[] {
        "Aifc",
        "Aiff",
        "Au",
        "Hcom",
        "Voc",
        "Wav",
        "Gsm",
        "Lpc10",
        "Ogg",
        "Wv",
            };
            cbType.DataSource = arr;
        }


        int _AudioFileIndex;

        public int AudioFileIndex
        {
            get { return _AudioFileIndex; }
            set { _AudioFileIndex = value; }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            string soxPath = tbSoxPath.Text;
            string inputFile = tbInput.Text;
            string outputFile = tbOutput.Text;
            if (string.IsNullOrEmpty(soxPath))
            {
                MessageBox.Show("Please specify path to Sox executable file");
                return;
            }
            if (string.IsNullOrEmpty(inputFile))
            {
                MessageBox.Show("Please specify path to ");
                return;
            }
            if (string.IsNullOrEmpty(outputFile))
            {
                MessageBox.Show("Please specify path to ");
                return;
            }
            try
            {
                SoxAudioFileType OutputType = (SoxAudioFileType)(AudioFileIndex);
                Sox.Convert(soxPath, inputFile, outputFile, OutputType);
                MessageBox.Show("Complete!");
            }
            catch (SoxException ex)
            {
                MessageBox.Show(String.Format("{0}: {1}", ex.Code, ex.Message));
            }
        }

        private void btnSoxPath_Click(object sender, EventArgs e)
        {
            ofdSoxPath.FileName = tbSoxPath.Text;
            if (ofdSoxPath.ShowDialog() == DialogResult.OK)
            {
                tbSoxPath.Text = ofdSoxPath.FileName;
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            ofdInput.FileName = tbInput.Text;
            if (ofdInput.ShowDialog() == DialogResult.OK)
            {
                tbInput.Text = ofdInput.FileName;
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            sdOutput.FileName = tbOutput.Text;
            if (sdOutput.ShowDialog() == DialogResult.OK)
            {
                tbOutput.Text = sdOutput.FileName;
            }
        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            GenerateOutputFileName();
        }

        private void GenerateOutputFileName()
        {
            tbOutput.Text = tbInput.Text + string.Format(".{0}", cbType.Items[AudioFileIndex]);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AudioFileIndex = cbType.SelectedIndex;
            GenerateOutputFileName();
        }

    }
}