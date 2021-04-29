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
            List<Reservation> reservations = ReadByHost(reservation.Host);

            //set reservation ID
            int maxID = reservations.Max(r => r.ID) + 1;
            reservation.ID = maxID;

            //add it to the list and write
            reservations.Add(reservation);
            Write(reservations, reservation.Host.ID);

            //return added reservation with ID
            return reservation;
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

        public Reservation Update(int id, Reservation reservation)
        {
            //get list of reservations for the given host
            List<Reservation> _reservations = ReadByHost(reservation.Host);

            Reservation result = null;

            //find the reservation ID, update, re-write it
            foreach (var existingReservation in _reservations)
            {
                if (existingReservation.ID == id)
                {
                    existingReservation.StartDate = reservation.StartDate;
                    existingReservation.EndDate = reservation.EndDate;
                    existingReservation.Guest = reservation.Guest;
                    existingReservation.Host = reservation.Host;
                    existingReservation.SetTotal();

                    Write(_reservations, existingReservation.Host.ID);
                    result = existingReservation;
                    return result;
                }
            }

            //return null if not found
            return result;
        }

        private void Write(List<Reservation> reservations, string hostID)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(GetFilePath(hostID));
                writer.WriteLine(HEADER);

                if (reservations == null)
                {
                    return;
                }

                foreach (var reservation in reservations)
                {
                    writer.WriteLine(Serialize(reservation));
                }
            }
            catch (IOException e)
            {
                throw new RepositoryException("could not write reservations", e);
            }
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
            if (entry.Length != 5)
            {
                return null;
            }

            Guest guest = new Guest();
            guest.ID = int.Parse(entry[3]);

            Host host = new Host();
            host.Email = hostEmail;

            Reservation reservation = new Reservation{
                StartDate = DateTime.Parse(entry[1]), 
                EndDate = DateTime.Parse(entry[2]),
                Host = host, 
                Guest = guest };
            reservation.ID = int.Parse(entry[0]);
            reservation.SetTotal(decimal.Parse(entry[4]));

            return reservation;
        }

        private string GetFilePath(string hostId)
        {
            return Path.Combine(directory, $"{hostId}.csv");
        }
    }
}
