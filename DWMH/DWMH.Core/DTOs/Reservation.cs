using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWMH.Core
{
    public class Reservation
    {
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guest Guest { get; set; }
        public int ID { get; set; }
        private decimal _total;

        //Reservation total currently includes the full cost of the last day
        public decimal Total { 
            get { return _total; } 
            set { Total = _total; }
        }
        public Host Host { get; set; }

        public Reservation(DateTime startDate, DateTime endDate, Host host, Guest guest)
        {
            StartDate = startDate;
            EndDate = endDate;
            Host = host;
            Guest = guest;

            for (DateTime d = StartDate; d <= EndDate; d = d.AddDays(1))
            {
                if (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday)
                {
                    _total += host.WeekendRate;
                }
                else
                {
                    _total += host.StandardRate;
                }
            }
        }
        public override bool Equals(object obj)
        {
            return obj is Reservation reservation &&
                   StartDate == reservation.StartDate &&
                   EndDate == reservation.EndDate &&
                   EqualityComparer<Guest>.Default.Equals(Guest, reservation.Guest) &&
                   ID == reservation.ID &&
                   Total == reservation.Total &&
                   EqualityComparer<Host>.Default.Equals(Host, reservation.Host);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartDate, EndDate, Guest, ID, Total, Host);
        }
    }
}
