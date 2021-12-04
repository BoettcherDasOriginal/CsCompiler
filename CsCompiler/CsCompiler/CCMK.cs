using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsCompiler
{
    public class CCMK
    {
        public List<string> ReferencedAssemblies { get; set; }
        public List<string> SrcFiles { get; set; }
        public string OutputAssembly { get; set; }
    }
}
