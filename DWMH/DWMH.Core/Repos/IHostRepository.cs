using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWMH.Core.Repos
{
    public interface IHostRepository
    {
        public List<Host> ReadAll();
        public Host ReadByEmail(string email);

    }
}
