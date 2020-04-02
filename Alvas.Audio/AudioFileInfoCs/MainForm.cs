/*
Opening G:\Documents and Settings\alexander\My Documents\track02.wav
Pcm 44100Hz 2 channels 16 bits per sample
Extra Size: 0 Block Align: 4 Average Bytes Per Second: 176400
WaveFormat: 16 bit PCM: 44kHz 2 channels
Length: 10584000 bytes: 00:01:00 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alvas.Audio;
using System.IO;

namespace AudioFileInfoCs
{
    public partial class MainForm : Form
    {
        private const string filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*";
        private const string OpenOrDragFile = "Open a media file or drag the mouse from Windows Explorer";
        private OpenFileDialog ofdAudio = new OpenFileDialog();

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ofdAudio.Filter = filter;
            tbResult.Text = OpenOrDragFile;
            tbResult.AllowDrop = true;
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            if (ofdAudio.ShowDialog() == DialogResult.OK)
            {
                tbResult.Lines = IntspectFile(ofdAudio.FileName);
            }
        }

        private string[] IntspectFile(string fileName)
        {
            ID3v1 m_id3;
            Dictionary<WaveInfo, string> m_waveTags = null;
            IAudioReader ar = null;
            Stream stream = null;
            string ext = Path.GetExtension(fileName.ToLower());
            m_id3 = null;
            m_waveTags = null;
            switch (ext)
            {
                case ".avi":
                    stream = File.OpenRead(fileName);
                    ar = new AviReader(stream);
                    if (!((AviReader)ar).HasAudio)
                    {
                        return new string[] { string.Format("'{0}' file is not contains audio data", fileName) };
                    }
                    break;
                case ".au":
                case ".snd":
                    ar = new AuReader(File.OpenRead(fileName));
                    break;
                case ".wav":
                    stream = File.OpenRead(fileName);
                    ar = new WaveReader(stream);
                    m_waveTags = (ar as WaveReader).ReadInfoTag();
                    break;
                case ".mp3":
                    stream = File.OpenRead(fileName);
                    ar = new Mp3Reader(stream);

                    Mp3Reader mrID3 = new Mp3Reader(File.OpenRead(fileName));
                    m_id3 = mrID3.ReadID3v1Tag();
                    break;
                default:
                    ar = new DsReader(fileName);
                    if (!((DsReader)ar).HasAudio)
                    {
                        return new string[] { string.Format("'{0}' file is not contains audio data", fileName) };
                    }
                    break;
            }
            IntPtr format = ar.ReadFormat();
            WaveFormat wf = AudioCompressionManager.GetWaveFormat(format);
            List<string> list = new List<string>();
            list.Add(string.Format("Opening {0}", fileName));
            list.Add(string.Format("{0}, {1} Hz, {2} channels, {3} bits per sample", GetFormatTagName(wf.wFormatTag), wf.nSamplesPerSec, wf.nChannels, wf.wBitsPerSample));
            list.Add(string.Format("Block Align: {0}, Average Bytes Per Second: {1}", wf.nBlockAlign, wf.nAvgBytesPerSec ));
            TimeSpan duration = TimeSpan.FromMilliseconds(ar.GetDurationInMS());
            list.Add(string.Format("Duration: {0}", duration));
            if (m_id3 != null)
            {
                list.Add("--------- ID3 -----------");
                list.Add(string.Format("Title: {0}", m_id3.Title));
                list.Add(string.Format("Artist: {0}", m_id3.Artist));
                list.Add(string.Format("Album: {0}", m_id3.Album));
                list.Add(string.Format("Year: {0}", m_id3.Year));
                list.Add(string.Format("Genre: {0}", m_id3.Genre.ToString()));
                list.Add(string.Format("Comment: {0}", m_id3.Comment));
                    
            }
            if (m_waveTags != null)
            {
                list.Add("--------- Wave tags -----------");
                foreach(WaveInfo key in m_waveTags.Keys)
                    list.Add(string.Format("{0}: {1}", key.ToString(), m_waveTags[key]));

            }
            ar.Close();
            return list.ToArray();
        }

        private string GetFormatTagName(int tag)
        {
            FormatTagDetails ftd = AudioCompressionManager.GetFormatTagDetails(tag);
            return ftd.FormatTagName;
        }

        private void tbResult_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tbResult_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array a = (Array)e.Data.GetData(DataFormats.FileDrop);

                if (a != null)
                {
                    string s = a.GetValue(0).ToString();
                    tbResult.Lines = IntspectFile(s);
                    this.Activate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DragDrop function: " + ex.Message);
            }
        }

    }
}