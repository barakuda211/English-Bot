using System;
using System.Windows.Forms;
using Alvas.Audio;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace WaveformVisualizerCs
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
            InitWaveformVisualizer();
            ofdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.vox|*.vox|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*";
            tbbOpen.Enabled = true;

            plex = new PlayerEx();
            plex.Done += new PlayerEx.DoneEventHandler(plex_Done);
            plex.BufferSizeInMS = 100;
            pgMain.SelectedObject = wfv;
        }

        private void InitWaveformVisualizer()
        {
            wfv = new WaveformVisualizer();
            wfv.Dock = DockStyle.Fill;
            wfv.PaintBackground += new PaintEventHandler(wfv_PaintBackground);
            this.pMain.Controls.Add(wfv);
        }

        void wfv_PaintBackground(object sender, PaintEventArgs e)
        {
            ////Uncomment this code if you want to use a custom background

            //Rectangle rect = e.ClipRectangle;
            //Graphics g = e.Graphics;
            //rect.Height /= 2;
            //Brush brush1 = new LinearGradientBrush(rect, wfv.BackColor, wfv.TimeLineColor, LinearGradientMode.Vertical);
            //Brush brush2 = new LinearGradientBrush(rect, wfv.TimeLineColor, wfv.BackColor, LinearGradientMode.Vertical);
            //g.FillRectangle(brush1, rect);
            //rect.Offset(0, rect.Height);
            //g.FillRectangle(brush2, rect);
        }

        PlayerEx plex;
        private WaveformVisualizer wfv;
        DeviceState state;

        void plex_Done(object sender, DoneEventArgs e)
        {
            if (e.IsEndPlaying)
            {
                tbMain_ButtonClick(tbMain, new ToolBarButtonClickEventArgs(tbbStop));
                wfv.Position = 0;
            }
            else
            {
                int position = (int)plex.GetPosition(Alvas.Audio.TimeFormat.Milliseconds);
                wfv.Position = position;
            }
        }

        private void tbMain_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (tbMain.Buttons.IndexOf(e.Button))
            {
                case 0: Open();
                    break;

                case 1: Play();
                    break;

                case 2: Stop();
                    break;
            }

            UpdateToolBar();
        }

        private void Stop()
        {
            plex.ClosePlayer();
            state = DeviceState.Stopped;
        }

        private void UpdateToolBar()
        {
            switch (state)
            {
                case DeviceState.Closed: 
                    tbbOpen.Enabled = true;
                    tbbPlay.Enabled = false;
                    tbbStop.Enabled = false;
                    break;
                case DeviceState.InProgress: 
                    tbbOpen.Enabled = false;
                    tbbPlay.Enabled = false;
                    tbbStop.Enabled = true;
                    break;
                case DeviceState.Stopped:
                    tbbOpen.Enabled = true;
                    tbbPlay.Enabled = true;
                    tbbStop.Enabled = false;
                    break;
            }
        }

        IAudioReader ar;

        private void Open()
        {
            if (ofdAudio.ShowDialog() == DialogResult.OK)
            {
                if (ar != null)
                {
                    ar.Close();
                    ar = null;
                }
                string fileName = ofdAudio.FileName;
                ar = null;
                switch (Path.GetExtension(fileName.ToLower()))
                {
                    case ".vox":
                        ar = GetVoxReader(fileName, 8000);
                        break;
                    case ".avi":
                        ar = new AviReader(File.OpenRead(fileName));
                        if (!((AviReader)ar).HasAudio)
                        {
                            MessageBox.Show(string.Format("'{0}' file is not contains audio data", fileName));
                            return;
                        }
                        break;
                    case ".au":
                    case ".snd":
                        ar = new AuReader(File.OpenRead(fileName));
                        break;
                    case ".wav":
                        ar = new WaveReader(File.OpenRead(fileName));
                        break;
                    case ".mp3":
                        ar = new Mp3Reader(File.OpenRead(fileName));
                        break;
                    default:
                        ar = new DsReader(fileName);
                        if (!((DsReader)ar).HasAudio)
                        {
                            ar = null;
                            MessageBox.Show(string.Format("'{0}' file is not contains audio data", fileName));
                        }
                        break;
                }
                Play();
                UpdateToolBar();
            }
        }

        private static IAudioReader GetVoxReader(string fileName, int sampleRate)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(fileName));
            MemoryStream ms = new MemoryStream();
            IntPtr format = AudioCompressionManager.GetPcmFormat(1, 16, sampleRate);
            WaveWriter ww = new WaveWriter(ms, AudioCompressionManager.FormatBytes(format));
            Vox.Vox2Wav(br, ww);
            br.Close();
            return new WaveReader(ms);
        }

        private void Play()
        {
            IntPtr format = ar.ReadFormat();
            plex.OpenPlayer(format);
            byte[] data = ar.ReadData();
            plex.AddData(data);
            wfv.Assign(format, data);
            plex.StartPlay();
            state = DeviceState.InProgress;
        }


    }
}