using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Exceptions;
using DWMH.Core.Repos;
using System.IO;

namespace DWMH.DAL
{
    public class ReservationFileRepository : IReservationRepository
    {
        const string HEADER = "id,start_date,end_date,guest_id,total";
        private string directory;

        public ReservationFileRepository(string directory)
        {
            this.directory = directory;
        }

        public Reservation Create(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Reservation Delete(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> ReadByHost(Host host)
        {
            List<Reservation> reservations = new List<Reservation>();
            string path = GetFilePath(host.ID);

            if (!File.Exists(path))
            {
                return reservations;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (IOException e)
            {
                throw new RepositoryException("could not read reservations", e);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Reservation reservation = Deserealize(fields, host.Email);

                if(reservation != null)
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public Reservation Update(int id)
        {
            throw new NotImplementedException();
        }

        private void Write(List<Reservation> reservations)
        {
            throw new NotImplementedException();
        }

        private string Serialize(Reservation reservation)
        {
            return string.Format("{0},{1},{2},{3},{4}",
                reservation.ID,
                reservation.StartDate,
                reservation.EndDate,
                reservation.Guest.ID,
                reservation.Total
                );
        }

        private Reservation Deserealize(string[] entry, string hostEmail)
        {
            if(entry.Length != 5)
            {
                return null;
            }

            Guest guest = new Guest();
            guest.ID = int.Parse(entry[3]);

            Host host = new Host();
            host.Email = hostEmail;

            Reservation reservation = new Reservation(DateTime.Parse(entry[1]), DateTime.Parse(entry[2]),
                host, guest);
            reservation.ID = int.Parse(entry[0]);

            return reservation;
        }

        private string GetFilePath(string hostId)
        {
            return Path.Combine(directory, $"{hostId}.csv");
        }
    }
}
