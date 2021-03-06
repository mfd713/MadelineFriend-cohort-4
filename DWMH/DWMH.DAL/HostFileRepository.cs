using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Exceptions;
using DWMH.Core.Repos;
using DWMH.Core.Loggers;

namespace DWMH.DAL
{
    public class HostFileRepository : IHostRepository
    {
        private string filePath;
        ILogger logger;

        public HostFileRepository(string file, ILogger logger)
        {
            filePath = file;
            this.logger = logger;
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
                logger.Log(e);
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
                    StandardRate = decimal.Parse(fields[8]),
                    WeekendRate = decimal.Parse(fields[9]),
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
            return ReadAll().Where(h => h.Email == email).FirstOrDefault();
        }
    }
}
