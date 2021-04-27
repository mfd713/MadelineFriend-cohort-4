using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Repos;
using System.IO;
using DWMH.Core.Exceptions;

namespace DWMH.DAL
{
    public class GuestFileRepository : IGuestRepository
    {
        private readonly string filePath;
        
        public GuestFileRepository(string file)
        {
            filePath = file;
        }

        public List<Guest> ReadAll()
        {
            List < Guest > guests = new List<Guest>();

            if (!File.Exists(filePath))
            {
                return guests;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch(IOException e)
            {
                throw new RepositoryException("could not read guests", e);
            }

            for(int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Guest guest = new Guest
                {
                    ID = int.Parse(fields[0]),
                    FirstName = fields[1],
                    LastName = fields[2],
                    Email = fields[3]
                };

                if(guest != null)
                {
                    guests.Add(guest);
                }
            }

            return guests;
        }


        public Guest ReadByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
