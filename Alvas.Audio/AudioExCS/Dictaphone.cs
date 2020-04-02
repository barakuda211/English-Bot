using System;
using System.Text;
using Alvas.Audio;
using System.IO;

namespace AudioExCS
{
    public class Dictaphone
    {
        private Alvas.Audio.RecorderEx recEx;
        private PlayerEx playEx;

        public Dictaphone()
        {
            this.recEx = new Alvas.Audio.RecorderEx();
            this.playEx = new Alvas.Audio.PlayerEx();
            // 
            // recEx
            // 
            this.recEx.Close += new System.EventHandler(this.CloseRecorder);
            this.recEx.Data += new Alvas.Audio.RecorderEx.DataEventHandler(this.DataRecorder);
            this.recEx.Open += new System.EventHandler(this.OpenRecorder);
            // 
            // playEx
            // 
            this.playEx.Close += new System.EventHandler(this.playerEx_Close);
            this.playEx.Open += new System.EventHandler(this.playerEx_Open);
            this.playEx.Done += new Alvas.Audio.PlayerEx.DoneEventHandler(this.playerEx_Done);
        }

        public int RecorderID
        {
            get { return recEx.RecorderID; }
            set { recEx.RecorderID = value; }
        }

        public IntPtr Format
        {
            get { return recEx.Format; }
            set { recEx.Format = value; }
        }

        private WaveReader wr = null;
        private WaveWriter ww = null;
        private Stream _stream = null;

        public void OpenRecorder(object sender, EventArgs e)
        {
            if (ww != null)
            {
                ww.Close();
            }
            ww = new WaveWriter(_stream, recEx.FormatBytes());
            OnChangeState(DictaphoneState.Record);
        }

        public void CloseRecorder(object sender, EventArgs e)
        {
            vum.Data = null;
            OnChangeState(DictaphoneState.Initial);
        }

        public void DataRecorder(object sender, DataEventArgs e)
        {
            byte[] data = e.Data;
            ww.WriteData(data);
            long pos = recEx.GetPosition(TimeFormat.Milliseconds);
            OnChangePosition(pos);
            short[] buffer = AudioCompressionManager.RecalculateData(recEx.Format, data, vum.ClientRectangle.Width);
            vum.Data = buffer;
        }

        private void playerEx_Done(object sender, DoneEventArgs e)
        {
            byte[] data = e.Data;
            long pos = playEx.GetPosition(TimeFormat.Milliseconds);
            OnChangePosition(pos);
            short[] buffer = AudioCompressionManager.RecalculateData(playEx.Format, e.Data, vum.ClientRectangle.Width);
            vum.Data = buffer;
            //------------
            if (e.IsEndPlaying && playEx.State != DeviceState.Closed)
            {
                playEx.ClosePlayer();
            }
            //------------
        }

        public delegate void ChangePositionEventHandler(object sender, PositionEventArgs e);

        public event ChangePositionEventHandler ChangePosition;

        protected virtual void OnChangePosition(long pos)
        {
            if (ChangePosition != null)
            {
                ChangePosition(this, new PositionEventArgs(pos));
            }
        }

        private void playerEx_Close(object sender, EventArgs e)
        {
            vum.Data = null;
            OnChangeState(DictaphoneState.Initial);
        }

        private void playerEx_Open(object sender, EventArgs e)
        {
            OnChangeState(DictaphoneState.Play);
        }

        public void StartRecord(bool inMemory, string fileName)
        {
            if (recEx.State == DeviceState.Closed)
            {
                Stream stream;
                if (inMemory)
                {
                    stream = new MemoryStream();
                }
                else
                {
                    stream = new FileStream(fileName, FileMode.Create);
                }
                _stream = stream;
            }
            try
            {
                recEx.StartRecord();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void StopRecord()
        {
            recEx.StopRecord();
        }

        public void PauseRecord()
        {
            recEx.PauseRecord();
            OnChangeState(DictaphoneState.PauseRecord);
        }

        public void ClosePlayer()
        {
            playEx.ClosePlayer();
        }

        public void PausePlay()
        {
            playEx.PausePlay();
            OnChangeState(DictaphoneState.PausePlay);
        }

        private SoundLevelMeter vum;

        public SoundLevelMeter SoundLevelMeter
        {
            get { return vum; }
            set { vum = value; }
        }


        public void StartPlay()
        {
            if (playEx.State == DeviceState.Paused)
            {
                playEx.ResumePlay();
            }
            else
            {
                wr = new WaveReader(_stream);
                IntPtr format = wr.ReadFormat();
                playEx.OpenPlayer(format);
                byte[] data = wr.ReadData();
                playEx.AddData(data);
                playEx.StartPlay();
            }
        }

        public delegate void ChangeStateEventHandler(object sender, DictaphoneStateEventArgs e);

        public event ChangeStateEventHandler ChangeState;

        protected virtual void OnChangeState(DictaphoneState state)
        {
            if (ChangeState != null)
            {
                ChangeState(this, new DictaphoneStateEventArgs(state));
            }
        }

        public void SetVolume(int leftVolume, int rightVolume)
        {
            playEx.SetVolume(leftVolume, rightVolume);
        }

        public void GetVolume(ref int leftVolume, ref int rightVolume)
        {
            playEx.GetVolume(ref leftVolume, ref rightVolume);
        }

    }
}
