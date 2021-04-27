using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWMH.Core.Loggers
{
    public class NullLogger : ILogger
    {
        public void Log(Exception e)
        {
            return;
        }
    }
}
