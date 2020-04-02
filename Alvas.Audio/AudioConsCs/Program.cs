using System;
using Alvas.Audio;

namespace AudioConsCs
{
    class Program
    {

        static void Main(string[] args)
        {
            rex.Data += new RecorderEx.DataEventHandler(rex_Data);
            rex.Open += new EventHandler(rex_Open);
            rex.Close += new EventHandler(rex_Close);
            rex.Format = pcmFormat;
            try
            {
                rex.StartRecord();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            Console.WriteLine("Please press enter to exit!");
            Console.ReadLine();
            try
            {
                rex.StopRecord();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
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
