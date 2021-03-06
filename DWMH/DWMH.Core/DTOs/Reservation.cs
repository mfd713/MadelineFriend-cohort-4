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

        public void SetTotal()
        {
            _total = 0;
            for (DateTime d = StartDate; d < EndDate; d = d.AddDays(1))
            {
                if (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday)
                {
                    _total += Host.WeekendRate;
                }
                else
                {
                    _total += Host.StandardRate;
                }
            }
        }
        public void SetTotal(decimal total)
        {
            _total = total;
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
