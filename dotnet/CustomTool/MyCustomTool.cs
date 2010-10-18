using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace CustomTool
{
    [ComVisible(true)]
    [Guid("6208087F-4F27-499C-A6C4-B0829BCD3C25")]
    public class MyCustomTool : BaseCodeGeneratorWithSite
    {
        public override string GetDefaultExtension()
        {
            return ".cs";
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            return Encoding.UTF8.GetBytes(
                string.Format("// Hi! This file was generated at {0}.", DateTime.Now));
        }
    }
}