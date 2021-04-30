using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWMH.Core.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Critical Error\n"+e.Message);
            Console.ResetColor();
        }
    }
}
