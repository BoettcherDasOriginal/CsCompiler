using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.Serialization;

namespace CsCompiler
{
    public static class ccmkFileHandler
    {
        public static CCMK ReadCcmkFile(FileInfo ccmkFileInfo)
        {
            var yamlDeserializer = new DeserializerBuilder().Build();

            string ccmkSrc = File.ReadAllText(ccmkFileInfo.FullName);

            var result = yamlDeserializer.Deserialize<CCMK>(ccmkSrc);

            return result;
        }
    }
}
