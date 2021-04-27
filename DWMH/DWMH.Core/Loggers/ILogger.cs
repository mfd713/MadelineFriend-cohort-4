using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWMH.Core.Loggers
{
    public interface ILogger
    {
        public void Log(Exception e);
    }
}
