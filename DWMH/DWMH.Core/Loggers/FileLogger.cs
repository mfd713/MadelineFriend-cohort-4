using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DWMH.Core.Loggers
{
    public class FileLogger : ILogger
    {
        private string filePath;

        public FileLogger(string _filePath)
        {
            filePath = _filePath;
        }
        public void Log(Exception e)
        {
            if (File.Exists(filePath))
            {
                using (StreamWriter sw = new StreamWriter(filePath, true)) //true here means lines should append instead of overwrite
                {
                    sw.WriteLine(); //advance to the next line
                    sw.Write($"{DateTime.Now:G} | {e.Message}");
                }
            }

        }
    }
}
