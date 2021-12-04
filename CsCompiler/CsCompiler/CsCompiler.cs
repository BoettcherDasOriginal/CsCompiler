using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.CodeDom.Compiler;
using System.Globalization;
using Microsoft.CSharp;

namespace CsCompiler
{
    public class CsCompiler
    {
        public static bool CompileExecutable(CCMK ccmk)
        {
            CodeDomProvider provider = null;
            bool compileOk = false;

            provider = CodeDomProvider.CreateProvider("CSharp");


            //Check if referenced assemblies exists

            foreach (string dllfile in ccmk.ReferencedAssemblies)
            {
                if(dllfile == "DEBUG.ccmk")
                {

                }
                else
                {
                    FileInfo fileInfo = new FileInfo(dllfile);
                    if (fileInfo.Exists)
                    {
                        if (fileInfo.Extension.ToUpper(CultureInfo.InvariantCulture) == ".DLL") { }
                        else
                        {
                            Console.WriteLine("Reference assembly must have a .dll extension");
                            provider = null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input reference assembly not found:\n{0}", fileInfo.FullName);
                        provider = null;
                    }
                }
            }


            //Check if Src Files exists

            foreach (string srcfile in ccmk.SrcFiles)
            {
                FileInfo fileInfo = new FileInfo(srcfile);
                if (fileInfo.Exists)
                {
                    if(fileInfo.Extension.ToUpper(CultureInfo.InvariantCulture) == ".CS") { }
                    else 
                    {
                        Console.WriteLine("Source file must have a .cs extension");
                        provider = null;
                    }
                }
                else
                {
                    Console.WriteLine("Input source file not found:\n{0}", fileInfo.FullName);
                    provider = null;
                }
            }


            //Exe or Dll Assembly?

            bool isExe = false;
            if (new FileInfo(ccmk.OutputAssembly).Extension.ToUpper(CultureInfo.InvariantCulture) == ".EXE")
            {
                isExe = true;
            }
            else if (new FileInfo(ccmk.OutputAssembly).Extension.ToUpper(CultureInfo.InvariantCulture) == ".DLL")
            {
                isExe = false;
            }
            else
            {
                Console.WriteLine("Output file must have a .exe or .dll extension!");
                provider = null;
            }


            //Start Compiling

            if (provider != null)
            {
                CompilerParameters cp = new CompilerParameters();

                // Generate an executable instead of
                // a class library.
                cp.GenerateExecutable = isExe;

                // Specify the assembly file name to generate.
                cp.OutputAssembly = ccmk.OutputAssembly;

                // Save the assembly as a physical file.
                cp.GenerateInMemory = false;

                // Set whether to treat all warnings as errors.
                cp.TreatWarningsAsErrors = false;


                //Add referenced assemblies
                Console.WriteLine("\nAdding standard assemblies...");

                if (Directory.Exists("libs\\.NET Framework 4.8\\"))
                {
                    Console.WriteLine("Framework version: .NET Framework 4.8");

                    string[] files = Directory.GetFiles("libs\\.NET Framework 4.8\\");
                    foreach (string file in files)
                    {
                        if (file.EndsWith(".dll"))
                        {
                            cp.ReferencedAssemblies.Add(file);
                        }
                    }
                }
                Console.WriteLine("\nAdding custom assemblies...");
                foreach (string dllfile in ccmk.ReferencedAssemblies)
                {
                    if(dllfile == "DEBUG.ccmk") { }
                    else
                    {
                        cp.ReferencedAssemblies.Add(dllfile);
                        Console.WriteLine("{0} - added", dllfile);
                    }
                }

                List<string> srcFiles = new List<string>();
                Console.WriteLine("\nAdding source files...");
                foreach(string srcFile in ccmk.SrcFiles)
                {
                    FileInfo fileInfo = new FileInfo(srcFile);

                    srcFiles.Add(fileInfo.FullName);
                    Console.WriteLine("{0} - added", fileInfo.FullName);
                }

                Console.WriteLine("\nSet output assembly to:\n{0}", ccmk.OutputAssembly);

                Console.WriteLine("\nStarting compiler...");

                // Invoke compilation of the source file.
                CompilerResults cr = provider.CompileAssemblyFromFile(cp, srcFiles.ToArray());

                if (cr.Errors.Count > 0)
                {
                    // Display compilation errors.
                    
                    Console.WriteLine("Errors building {0}", cr.PathToAssembly);
                    foreach (CompilerError ce in cr.Errors)
                    {
                        Console.WriteLine("  {0}", ce.ToString());
                        Console.WriteLine();
                    }
                }
                else
                {
                    // Display a successful compilation message.
                    Console.WriteLine("Source files built into {0} successfully.", cr.PathToAssembly);
                }

                // Return the results of the compilation.
                if (cr.Errors.Count > 0)
                {
                    compileOk = false;
                }
                else
                {
                    compileOk = true;
                }
            }
            return compileOk;
        }
    }
}
