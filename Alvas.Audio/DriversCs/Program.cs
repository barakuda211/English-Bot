using System;
using Alvas.Audio;

namespace DriversCs
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (DriverDetails dd in AudioCompressionManager.GetDriverList())
            {
                Console.WriteLine("# # #");
                Console.WriteLine("Driver: {0}", dd.LongName);
                foreach (FormatTagDetails ftd in AudioCompressionManager.GetFormatTagList(dd.Driver))
                {
                    Console.WriteLine("FormatTag: {0}", ftd.FormatTagName);
                    foreach (FormatDetails fd in AudioCompressionManager.GetFormatList(ftd.FormatTag, dd.Driver))
                    {
                        Console.WriteLine("Format: {0}", fd.FormatName);
                    }
                }
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
