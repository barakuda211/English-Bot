using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alvas.Audio;
using System.IO;

namespace RecordPlay
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
            ofdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.avi|*.avi|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.wma;*.wmv;*.asf;*.mpg;*.aif;*.au;*.snd;*.mid;*.rmi;*.ogg;*.flac;*.cda;*.ac3;*.dts;*.mka;*.mkv;*.mpc;*.m4a;*.aac;*.mpa;*.mp2;*.m1a;*.m2a|*.*|*.*";
            sfdAudio.Filter = "*.wav|*.wav|*.mp3|*.mp3|*.*|*.*";
            //cbMute.;
            tbPlayer.Maximum = ushort.MaxValue;// 65535;
            tbRecorder.Maximum = ushort.MaxValue;// 65535;
            tspProgress.Maximum = short.MaxValue;
            //
            rp.PropertyChanged += new PropertyChangedEventHandler(rp_PropertyChanged);
            InitButtons(rp.State);
            EnumRecorders();
            EnumPlayers();
            cbMute.DataBindings.Add("Checked", rp, RecordPlayer.PlayerVolumeMuteProperty, false, DataSourceUpdateMode.OnPropertyChanged);
            tbPlayer.DataBindings.Add("Value", rp, RecordPlayer.PlayerVolumeProperty, false, DataSourceUpdateMode.OnPropertyChanged);
            //
            cbRecorderLine.DataSource = rp.RecorderLines;
            cbRecorderLine.DataBindings.Add("SelectedIndex", rp, RecordPlayer.RecorderLinesIndexProperty, false, DataSourceUpdateMode.OnPropertyChanged);
            tbRecorder.DataBindings.Add("Value", rp, RecordPlayer.RecorderVolumeProperty, false, DataSourceUpdateMode.OnPropertyChanged);
            //
            tbTimeline.SmallChange = 1000;
            tbTimeline.LargeChange = 10000;
            tbTimeline.DataBindings.Add("Maximum", rp, "Duration", false, DataSourceUpdateMode.OnPropertyChanged);
            tbTimeline.DataBindings.Add("Value", rp, "Position", false, DataSourceUpdateMode.OnPropertyChanged);
            //
            nudBufferSizeInMs.DataBindings.Add("Value", rp, RecordPlayer.BufferSizeInMSProperty, false, DataSourceUpdateMode.OnPropertyChanged);
            cbSkipSilent.DataBindings.Add("Checked", rp, RecordPlayer.SkipSilentProperty, false, DataSourceUpdateMode.OnPropertyChanged);
            nudSilentLevel.DataBindings.Add("Value", rp, RecordPlayer.SilentLevelProperty, false, DataSourceUpdateMode.OnPropertyChanged);
            nudVolumeLevelScale.Value = 100;
            nudVolumeLevelScale.Increment = 100;
            nudVolumeLevelScale.Minimum = 50;
            nudVolumeLevelScale.Maximum = 1000;
            nudVolumeLevelScale.DataBindings.Add("Value", rp, RecordPlayer.VolumeScaleProperty, false, DataSourceUpdateMode.OnPropertyChanged);
        }

        void rp_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case RecordPlayer.StateProperty:
                    InitButtons(rp.State);
                    break;
                case RecordPlayer.VolumeLevelProperty:
                    tspProgress.Value = rp.VolumeLevel;
                    break;
                case RecordPlayer.PositionProperty:
                case RecordPlayer.DurationProperty:
                    tsslPosition.Text = string.Format("{0} : {1}", rp.Position, rp.Duration);
                    break;
                default:
                    break;
            }
        }

        RecordPlayer rp = new RecordPlayer();

        private void InitButtons(DeviceState state)
        {
            tsslStatus.Text = state.ToString();
            //Console.WriteLine(state); 
            switch (state)
            {
                case DeviceState.Opened:
                    tsbNew.Enabled = false;
                    tsbOpen.Enabled = false;
                    tsbRecord.Enabled = rp.CanRecord;
                    tsbRecordFrom.Enabled = rp.CanRecord;
                    tsbPlay.Enabled = true;
                    tsbPause.Enabled = false;
                    tsbStop.Enabled = false;
                    tsbForward.Enabled = true;
                    tsbBackward.Enabled = true;
                    tsbPlayFrom.Enabled = true;
                    tsbClose.Enabled = true;
                    Text = rp.FormatDetails.ToString();
                    cbPlayer.Enabled = true;
                    cbRecorder.Enabled = true;
                    nudBufferSizeInMs.Enabled = true;
                    break;
                case DeviceState.Stopped:
                    tsbNew.Enabled = false;
                    tsbOpen.Enabled = false;
                    tsbRecord.Enabled = rp.CanRecord;
                    tsbRecordFrom.Enabled = rp.CanRecord;
                    tsbPlay.Enabled = true;
                    tsbPause.Enabled = false;
                    tsbStop.Enabled = false;
                    tsbForward.Enabled = true;
                    tsbBackward.Enabled = true;
                    tsbPlayFrom.Enabled = true;
                    tsbClose.Enabled = true;
                    cbPlayer.Enabled = true;
                    cbRecorder.Enabled = true;
                    nudBufferSizeInMs.Enabled = true;
                    break;
                case DeviceState.Paused:
                    tsbNew.Enabled = false;
                    tsbOpen.Enabled = false;
                    tsbRecord.Enabled = rp.CanRecord;
                    tsbRecordFrom.Enabled = rp.CanRecord;
                    tsbPlay.Enabled = true;
                    tsbPause.Enabled = false;
                    tsbStop.Enabled = true;
                    tsbForward.Enabled = true;
                    tsbBackward.Enabled = true;
                    tsbPlayFrom.Enabled = true;
                    tsbClose.Enabled = false;
                    cbPlayer.Enabled = false;
                    cbRecorder.Enabled = false;
                    nudBufferSizeInMs.Enabled = true;
                    break;
                case DeviceState.InProgress:
                    tsbNew.Enabled = false;
                    tsbOpen.Enabled = false;
                    tsbRecord.Enabled = false;
                    tsbRecordFrom.Enabled = false;
                    tsbPlay.Enabled = false;
                    tsbPause.Enabled = true;
                    tsbStop.Enabled = true;
                    tsbForward.Enabled = true;
                    tsbBackward.Enabled = true;
                    tsbPlayFrom.Enabled = true;
                    tsbClose.Enabled = false;
                    cbPlayer.Enabled = false;
                    cbRecorder.Enabled = false;
                    nudBufferSizeInMs.Enabled = false;
                    break;
                case DeviceState.Closed:
                default:
                    tsbNew.Enabled = true;
                    tsbOpen.Enabled = true;
                    tsbRecord.Enabled = false;
                    tsbRecordFrom.Enabled = false;
                    tsbPlay.Enabled = false;
                    tsbPause.Enabled = false;
                    tsbStop.Enabled = false;
                    tsbForward.Enabled = false;
                    tsbBackward.Enabled = false;
                    tsbPlayFrom.Enabled = false;
                    tsbClose.Enabled = false;
                    cbPlayer.Enabled = true;
                    cbRecorder.Enabled = true;
                    nudBufferSizeInMs.Enabled = true;
                    break;
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            rp.Close();
        }

        public int Time
        {
            get 
            {
                try
                {
                    return int.Parse(tstTime.Text);
                }
                catch
                {
                    return 0;
                }
            }
        }

        private void tsbPlayFrom_Click(object sender, EventArgs e)
        {
            rp.Play(Position);
        }

        public int Position
        {
            get
            {
                try
                {
                    return int.Parse(tstTime.Text) * 1000;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int Step
        {
            get 
            {
                try
                {
                    return int.Parse(tstStep.Text) * 1000; 
                }
                catch 
                {
                    return 10 * 1000;
                }
            }
        }

        private void tsbBackward_Click(object sender, EventArgs e)
        {
            rp.Backward(Step);
        }

        private void tsbForward_Click(object sender, EventArgs e)
        {
            rp.Forward(Step);
        }

        private void tsbStop_Click(object sender, EventArgs e)
        {
            rp.Stop();
        }

        private void tsbPause_Click(object sender, EventArgs e)
        {
            rp.Pause();
        }

        private void tsbPlay_Click(object sender, EventArgs e)
        {
            rp.Play();
        }

        private void tsbRecord_Click(object sender, EventArgs e)
        {
            try
            {
                Record();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Record()
        {
            int pos = rp.Position;
            if (pos <= 0)
            {
                rp.Record();
            }
            else
            {
                rp.Record(pos);
            }
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            if (sfdAudio.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfdAudio.FileName;
                Stream stream = null;
                IAudioReadWriter arw = null;
                FormatDialog fd = null;
                switch (Path.GetExtension(fileName.ToLower()))
                {
                    case ".wav":
                        fd = new FormatDialog(false);
                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                            stream = File.Create(fileName);
                            arw = new WaveReadWriter(stream, AudioCompressionManager.FormatBytes(fd.Format));
                            rp.Open(arw);
                        }
                        break;
                    case ".mp3":
                        fd = new FormatDialog(true);
                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                            stream = File.Create(fileName);
                            arw = new Mp3ReadWriter(stream, fd.Format);
                            rp.Open(arw);
                        }
                        break;
                    default:
                        fd = new FormatDialog(false);
                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                            stream = File.Create(fileName);
                            arw = new RawReadWriter(stream, fd.Format);
                            rp.Open(arw);
                        }
                        return;
                }
            }
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            if (ofdAudio.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofdAudio.FileName;
                IAudioReader arw = null;
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
                            MessageBox.Show(string.Format("'{0}' file is not contains audio data", fileName));
                            return;
                        }
                        break;
                    //FormatDialog fd = new FormatDialog(false);
                        //if (fd.ShowDialog() == DialogResult.OK)
                        //{
                        //    arw = new RawReadWriter(stream, fd.Format);
                        //    break;
                        //}
                        //else
                        //{
                        //    return;
                        //}
                }
                rp.Open(arw);
            }
        }

        private void EnumPlayers()
        {
            int count = PlayerEx.PlayerCount;
            if (count > 0)
            {
                for (int i = -1; i < count; i++)
                {
                    cbPlayer.Items.Add(PlayerEx.GetPlayerName(i));
                }
                cbPlayer.SelectedIndex = 0;
            }
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

        private void cbPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            rp.PlayerID = cbPlayer.SelectedIndex - 1;
        }

        private void cbRecorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            rp.RecorderID = cbRecorder.SelectedIndex - 1;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            rp.PropertyChanged -= rp_PropertyChanged;
        }

        private void tsbRecordFrom_Click(object sender, EventArgs e)
        {
            try
            {
                rp.Record(Position);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}