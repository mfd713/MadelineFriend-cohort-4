using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Exceptions;
using DWMH.Core.Repos;

namespace DWMH.DAL
{
    public class HostFileRepository : IHostRepository
    {
        private string filePath;

        public HostFileRepository(string file)
        {
            filePath = file;
        }
        public List<Host> ReadAll()
        {
            List<Host> hosts = new List<Host>();

            if (!File.Exists(filePath))
            {
                return hosts;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException e)
            {
                throw new RepositoryException("could not read hosts", e);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Host host = new Host
                {
                    ID = fields[0],
                    LastName = fields[1],
                    Email = fields[2],
                    City = fields[5],
                    State = fields[6],
                    StandardRate = decimal.Parse(fields[7]),
                    WeekendRate = decimal.Parse(fields[8]),
                };

                if (host != null)
                {
                    hosts.Add(host);
                }
            }

            return hosts;
        }

        public Host ReadByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
