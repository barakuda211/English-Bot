using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Alvas.Audio;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace WpfCs
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Init();
        }

        OpenFileDialog ofdAudio = new OpenFileDialog();
        PlayerEx plex = new PlayerEx();
        private WaveformVisualizer wfv;
        IAudioReader ar;
        DeviceState state;

        private void Init()
        {
            InitWaveformVisualizer();
            ofdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*";
            tbbOpen.IsEnabled = true;
            tbbPlay.IsEnabled = false;
            tbbStop.IsEnabled = false;

            plex.Done += new PlayerEx.DoneEventHandler(plex_Done);
            plex.BufferSizeInMS = 100;
            pgMain.SelectedObject = wfv;
        }

        private void InitWaveformVisualizer()
        {
            wfv = new WaveformVisualizer();
            //wfv.Dock = DockStyle.Fill;
            wfh.Child = wfv;
            wfv.PaintBackground += new PaintEventHandler(wfv_PaintBackground);
        }

        void wfv_PaintBackground(object sender, PaintEventArgs e)
        {
            ////Uncomment this code if you want to use a custom background

            //System.Drawing.Rectangle rect = e.ClipRectangle;
            //System.Drawing.Graphics g = e.Graphics;
            //rect.Height /= 2;
            //System.Drawing.Brush brush1 = new System.Drawing.Drawing2D.LinearGradientBrush(rect,
            //    wfv.BackColor, wfv.TimeLineColor, LinearGradientMode.Vertical);
            //System.Drawing.Brush brush2 = new System.Drawing.Drawing2D.LinearGradientBrush(rect,
            //    wfv.TimeLineColor, wfv.BackColor, LinearGradientMode.Vertical);
            //g.FillRectangle(brush1, rect);
            //rect.Offset(0, rect.Height);
            //g.FillRectangle(brush2, rect);
        }

        void plex_Done(object sender, DoneEventArgs e)
        {
            if (e.IsEndPlaying)
            {
                Stop();
                wfv.Position = 0;
            }
            else
            {
                int position = (int)plex.GetPosition(Alvas.Audio.TimeFormat.Milliseconds);
                wfv.Position = position;
            }
        }

        private void Stop()
        {
            plex.ClosePlayer();
            state = DeviceState.Stopped;
            UpdateToolBar();
        }

        private void UpdateToolBar()
        {
            switch (state)
            {
                case DeviceState.Closed:
                    tbbOpen.IsEnabled = true;
                    tbbPlay.IsEnabled = false;
                    tbbStop.IsEnabled = false;
                    break;
                case DeviceState.InProgress:
                    tbbOpen.IsEnabled = false;
                    tbbPlay.IsEnabled = false;
                    tbbStop.IsEnabled = true;
                    break;
                case DeviceState.Stopped:
                    tbbOpen.IsEnabled = true;
                    tbbPlay.IsEnabled = true;
                    tbbStop.IsEnabled = false;
                    break;
            }
        }

        private void Open()
        {
            if (ofdAudio.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ar != null)
                {
                    ar.Close();
                    ar = null;
                }
                string fileName = ofdAudio.FileName;
                ar = null;
                switch (System.IO.Path.GetExtension(fileName.ToLower()))
                {
                    case ".avi":
                        ar = new AviReader(File.OpenRead(fileName));
                        if (!((AviReader)ar).HasAudio)
                        {
                            System.Windows.MessageBox.Show(string.Format("'{0}' file is not contains audio data", fileName));
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
                            System.Windows.MessageBox.Show(string.Format("'{0}' file is not contains audio data", fileName));
                        }
                        break;
                }
                Play();
                UpdateToolBar();
            }
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
            UpdateToolBar();
        }

        private void tbbOpen_Click(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void tbbPlay_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }

        private void tbbStop_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }



    }
}
