using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Repos;

namespace TestsDAL.TestDoubles
{
    public class HostRepoDouble : IHostRepository
    {
        private List<Host> hosts = new List<Host>();

        public HostRepoDouble()
        {
            Host host = new Host
            {
                LastName = "Testy1",
                ID = "abc-123",
                Email = "test1@gmail.com",
                City = "Chicago",
                State = "IL",
            };
            host.SetRates(50M, 80M);

            hosts.Add(host);
        }
        public List<Host> ReadAll()
        {
            return hosts;
        }

        public Host ReadByEmail(string email)
        {
            return ReadAll().Where(h => h.Email == email).FirstOrDefault();
        }
    }
}
