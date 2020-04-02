Imports System
Imports Alvas.Audio

Module Program

    Sub Main()
        For Each dd As DriverDetails In AudioCompressionManager.GetDriverList()
            Console.WriteLine("# # #")
            Console.WriteLine("Driver: {0}", dd.LongName)
            For Each ftd As FormatTagDetails In AudioCompressionManager.GetFormatTagList(dd.Driver)
                Console.WriteLine("FormatTag: {0}", ftd.FormatTagName)
                For Each fd As FormatDetails In AudioCompressionManager.GetFormatList(ftd.FormatTag, dd.Driver)
                    Console.WriteLine("Format: {0}", fd.FormatName)
                Next
            Next
        Next
        Console.WriteLine("Press any key to exit")
        Console.ReadKey()
    End Sub

End Module
