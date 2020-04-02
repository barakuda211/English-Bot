/* 
 * rem run cmd as administrator
 * rem install service
 * \\Windows\Microsoft.NET\Framework\v2.0.50727\installutil AudioWindowsServiceCs.exe
 * rem uninstall service
 * \\Windows\Microsoft.NET\Framework\v2.0.50727\installutil AudioWindowsServiceCs.exe /u
 */
using System;
using System.ServiceProcess;
using System.Threading;
using Alvas.Audio;

namespace AudioWindowsServiceCs
{
    public partial class AudioService : ServiceBase
    {
        public AudioService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread t = new Thread(Start);
            t.Start();
        }

        protected override void OnStop()
        {
            rex.StopRecord();
        }

        static void Start()
        {
            rex.Data += new RecorderEx.DataEventHandler(rex_Data);
            rex.Open += new EventHandler(rex_Open);
            rex.Close += new EventHandler(rex_Close);
            rex.Format = pcmFormat;
            rex.StartRecord();
        }

        static RecorderEx rex = new RecorderEx(true);
        static PlayerEx play = new PlayerEx(true);
        static IntPtr pcmFormat = AudioCompressionManager.GetPcmFormat(1, 16, 44100);

        static void rex_Open(object sender, EventArgs e)
        {
            play.OpenPlayer(pcmFormat);
            play.StartPlay();
        }

        static void rex_Close(object sender, EventArgs e)
        {
            play.ClosePlayer();
        }

        static void rex_Data(object sender, DataEventArgs e)
        {
            byte[] data = e.Data;
            play.AddData(data);
        }
    }
}
