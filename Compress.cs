using System;
using System.Diagnostics;

namespace PDFCompress
{
    class Compress
    {
        public static bool compress(string path)
        {
            string output = path.Replace("input", "output");

            string ghostScriptCommand = $"gs -sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/printer -dNOPAUSE -dQUIET -dBATCH -sOutputFile='{ output }' '{ path }'";

            bool result = shellCommand(ghostScriptCommand);

            return result;
        }

        static bool shellCommand(string command)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "/bin/bash";
            proc.StartInfo.Arguments = $"-c \"{ command }\"";
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();

            return !Convert.ToBoolean(proc.ExitCode);
        }
    }
}