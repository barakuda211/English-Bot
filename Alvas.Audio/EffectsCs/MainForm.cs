using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alvas.Audio;
using System.Runtime.InteropServices;
using System.IO;

namespace EffectsCs
{

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        protected override void UpdateDefaultButton()
        {
            Control ctrl = FindFocusedControl(splitContainer1);
            if (ctrl != null && ctrl.Tag != null)
            {
                propertyGrid1.SelectedObject = (ctrl != null) ? ctrl.Tag : null;
            }
        }

        public static Control FindFocusedControl(Control container)
        {
            foreach (Control childControl in container.Controls)
            {
                if (childControl.Focused)
                {
                    return childControl;
                }
            }

            foreach (Control childControl in container.Controls)
            {
                Control maybeFocusedControl = FindFocusedControl(childControl);
                if (maybeFocusedControl != null)
                {
                    return maybeFocusedControl;
                }
            }

            return null; // Couldn't find any, darn!
        }

        private void Init()
        {
            sfdAudio.Filter = "*.wav|*.wav";
            sfdAudio.DefaultExt = "wav";
            ofdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*";
            btnPlay.Enabled = false;
            btnStop.Enabled = false;
            btnSave.Enabled = false;

            ChorusAudioEffect aeChorus = AudioEffect.CreateChorusAudioEffect();
            NewTab(aeChorus, "Chorus");
            CompressorAudioEffect aeCompressor = AudioEffect.CreateCompressorAudioEffect();
            NewTab(aeCompressor, "Compressor");
            DistortionAudioEffect aeDistortion = AudioEffect.CreateDistortionAudioEffect();
            NewTab(aeDistortion, "Distortion");
            EchoAudioEffect aeEcho = AudioEffect.CreateEchoAudioEffect();
            NewTab(aeEcho, "Echo");
            FlangerAudioEffect aeFlanger = AudioEffect.CreateFlangerAudioEffect();
            NewTab(aeFlanger, "Flanger");
            GargleAudioEffect aeGargle = AudioEffect.CreateGargleAudioEffect();
            NewTab(aeGargle, "Gargle");
            I3DL2ReverbAudioEffect aeI3DL2Reverb = AudioEffect.CreateI3DL2ReverbAudioEffect();
            NewTab(aeI3DL2Reverb, "I3DL2Reverb");
            ParamEqAudioEffect aeParamEq = AudioEffect.CreateParamEqAudioEffect();
            NewTab(aeParamEq, "ParamEq");
            WavesReverbAudioEffect aeWavesReverb = AudioEffect.CreateWavesReverbAudioEffect();
            NewTab(aeWavesReverb, "WavesReverb");
        }

        private void NewTab(AudioEffect ae, string name)
        {
            TabPage tab = InitParams(ae, name);
            tcEffects.TabPages.Add(tab);
        }

        private TabPage InitParams(AudioEffect ae, string name)
        {
            TabPage tp = new TabPage(name);
            TableLayoutPanel tl = new TableLayoutPanel();
            tl.Dock = DockStyle.Top;
            tl.ColumnCount = 2 + 1;
            tl.AutoSizeMode = AutoSizeMode.GrowOnly;
            tl.AutoSize = true;
            tp.Controls.Add(tl);

            tl.RowCount = ae.Params.Length;
            for (int i = 0; i < ae.Params.Length; i++)
            {
                DmoParam param = ae.Params[i];
                CheckBox cb = new CheckBox();
                cb.Text = "Default";
                cb.DataBindings.Add("Checked", param, "IsDefaultValue");
                tl.Controls.Add(cb);
                Label l = new Label();
                l.Text = ae.Params[i].Name + ((ae.Params[i].Unit == "") ? "" : ", " + ae.Params[i].Unit);
                tl.Controls.Add(l);
                if (param is DmoBoolParam)
                {
                    CheckBox ctrl = new CheckBox();
                    ctrl.Tag = param;
                    ctrl.DataBindings.Add("Checked", param, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
                    tl.Controls.Add(ctrl);
                }
                else if (param is DmoEnumParam)
                {
                    DmoEnumParam paramEnum = (DmoEnumParam)param;
                    ComboBox ctrl = new ComboBox();
                    ctrl.DropDownStyle = ComboBoxStyle.DropDownList;
                    ctrl.Items.AddRange(paramEnum.Items);
                    ctrl.Tag = param;
                    ctrl.DataBindings.Add("SelectedIndex", param, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
                    tl.Controls.Add(ctrl);
                }
                else if (param is DmoFloatParam)
                {
                    DmoFloatParam paramFloat = (DmoFloatParam)param;
                    NumericUpDown ctrl = new NumericUpDown();
                    ctrl.DecimalPlaces = 3;
                    ctrl.Increment = 0.001M;
                    ctrl.Minimum = (decimal)paramFloat.Minimum;
                    ctrl.Maximum = (decimal)paramFloat.Maximum;
                    ctrl.Tag = param;
                    ctrl.DataBindings.Add("Value", param, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
                    tl.Controls.Add(ctrl);
                }
                else if (param is DmoIntParam)
                {
                    DmoIntParam paramInt = (DmoIntParam)param;
                    TrackBar ctrl = new TrackBar();
                    ctrl.Minimum = paramInt.Minimum;
                    ctrl.Maximum = paramInt.Maximum;
                    ctrl.Tag = param;
                    ctrl.DataBindings.Add("Value", param, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
                    tl.Controls.Add(ctrl);
                }
                else
                {
                    Label ctrl = new Label();
                    ctrl.Tag = param;
                    tl.Controls.Add(ctrl);
                }
            }

            for (int i = 0; i < tl.RowCount; i++)
            {
                tl.RowStyles.Add(new RowStyle(SizeType.Absolute, 26f));
            }
            tl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, .5f));
            tp.Tag = ae;
            return tp;
        }

        PlayerEx plex = new PlayerEx();

        private void btnPlay_Click(object sender, EventArgs e)
        {
            AudioEffect af = tcEffects.SelectedTab.Tag as AudioEffect;
            byte[] data = null;
            IntPtr format = IntPtr.Zero;
            Prepare(arw, af, ref format, ref data);
            Play(format, data);
            tcEffectsEnabled = false;
            btnPlay.Enabled = false;
            btnOpen.Enabled = false;
            btnStop.Enabled = true;
        }

        public void Prepare(IAudioReader wr, AudioEffect af, ref IntPtr format, ref byte[] data)
        {
            format = wr.ReadFormat();
            WaveFormat wf1 = AudioCompressionManager.GetWaveFormat(format);
            Console.WriteLine("{0},{1},{2}-{3}", wf1.nChannels, wf1.wBitsPerSample, wf1.nSamplesPerSec, wf1.wFormatTag);
            data = wr.ReadData();
            if (wf1.wFormatTag != 1)
            {
                IntPtr formatNew = IntPtr.Zero;
                byte[] dataNew = null;
                AudioCompressionManager.ToPcm(format, data, ref formatNew, ref dataNew);
                format = formatNew;
                data = dataNew;
                WaveFormat wf2 = AudioCompressionManager.GetWaveFormat(format);
                Console.WriteLine("{0},{1},{2}-{3}", wf2.nChannels, wf2.wBitsPerSample, wf2.nSamplesPerSec, wf2.wFormatTag);
            }
            else if (wf1.wBitsPerSample != 16)
            {
                WaveFormat wf = AudioCompressionManager.GetWaveFormat(format);
                IntPtr formatNew = AudioCompressionManager.GetPcmFormat(wf.nChannels, 16, wf.nSamplesPerSec);
                byte[] dataNew = AudioCompressionManager.Convert(format, formatNew, data, false);
                format = formatNew;
                data = dataNew;
                WaveFormat wf2 = AudioCompressionManager.GetWaveFormat(format);
                Console.WriteLine("{0},{1},{2}-{3}", wf2.nChannels, wf2.wBitsPerSample, wf2.nSamplesPerSec, wf2.wFormatTag);
            }
            //wr.Close();
            if (af != null)
            {
                bool hasProcessInPlace = af.HasProcessInPlace;
                //af.GetSupportedOutputFormats();
                GCHandle src = GCHandle.Alloc(data, GCHandleType.Pinned);
                IntPtr formatPtr = src.AddrOfPinnedObject();
                bool res = af.ProcessInPlace(format, data);
                src.Free();
                if (!res)
                {
                    MessageBox.Show("Unable to convert the audio data");
                    return;
                }
            }
        }

        private void Play(IntPtr format, byte[] data)
        {
            if (plex.State != DeviceState.Closed)
            {
                plex.ClosePlayer();
            }
            //Console.WriteLine(plex.State);
            plex.OpenPlayer(format);
            plex.AddData(data);
            plex.StartPlay();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (plex.State != DeviceState.Closed)
            {
                plex.ClosePlayer();
            }
            tcEffectsEnabled = true;
            btnPlay.Enabled = true;
            btnOpen.Enabled = true;
            btnStop.Enabled = false;
            Console.WriteLine("{0} STOP", plex.State);
        }

        IAudioReader arw;
        private bool tcEffectsEnabled = true;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (ofdAudio.ShowDialog() == DialogResult.OK)
            {
                if (arw != null)
                {
                    arw.Close();
                    arw = null;
                }
                string fileName = ofdAudio.FileName;
                arw = null;
                switch (Path.GetExtension(fileName.ToLower()))
                {
                    case ".avi":
                        arw = new AviReader(File.Open(fileName, FileMode.Open, FileAccess.ReadWrite));
                        if (!((AviReader)arw).HasAudio)
                        {
                            MessageBox.Show(string.Format("'{0}' file is not contains audio data", fileName));
                            return;
                        }
                        break;
                    case ".au":
                    case ".snd":
                        arw = new AuReader(File.OpenRead(fileName));
                        break;
                    case ".wav":
                        arw = new WaveReadWriter(File.Open(fileName, FileMode.Open, FileAccess.ReadWrite));
                        break;
                    case ".mp3":
                        arw = new Mp3ReadWriter(File.Open(fileName, FileMode.Open, FileAccess.ReadWrite));
                        break;
                    default:
                        arw = new DsReader(fileName);
                        if (!((DsReader)arw).HasAudio)
                        {
                            arw = null;
                            MessageBox.Show(string.Format("'{0}' file is not contains audio data", fileName));
                        }
                        break;
                }
                btnPlay.Enabled = arw != null;
                btnSave.Enabled = arw != null;
            }
        }

        private void tcEffects_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !tcEffectsEnabled;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (sfdAudio.ShowDialog() == DialogResult.OK)
            {
                Save(sfdAudio.FileName);
            }
        }

        private void Save(IntPtr format, byte[] data, string fileName)
        {
            //string fileName = string.Format(@"e:\Down\wav\{0}.wav", name);
            WaveWriter ww = new WaveWriter(File.Create(fileName),
                AudioCompressionManager.FormatBytes(format));
            ww.WriteData(data);
            ww.Close();
        }


        private void Save(string fileName)
        {
            AudioEffect af = tcEffects.SelectedTab.Tag as AudioEffect;
            byte[] data = null;
            IntPtr format = IntPtr.Zero;
            Prepare(arw, af, ref format, ref data);
            Save(format, data, fileName);
        }


    }
}