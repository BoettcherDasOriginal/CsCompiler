using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using YamlDotNet.Serialization;

namespace CsCompiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CsCompiler";
            Console.WriteLine("CsCompiler version: 1.0.0");
            Console.WriteLine("(c) BöttcherDasOriginal. GPL-3.0 License\n");


            if (args.Length == 1)
            {
                FileInfo fileInfo = new FileInfo(args[0]);
                if (fileInfo.Exists)
                {
                    if(fileInfo.Extension.ToUpper(CultureInfo.InvariantCulture) == ".CCMK")
                    {
                        Console.WriteLine("Reading CCMK File...");
                        CCMK ccmk = ccmkFileHandler.ReadCcmkFile(fileInfo);
                        Console.WriteLine("Initializing compiler...");
                        CsCompiler.CompileExecutable(ccmk);

                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
