using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWMH.Core.Repos
{
    public interface IGuestRepository
    {
        public List<Guest> ReadAll();
        public Guest ReadByEmail(string email);
    }
}
