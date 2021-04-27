using System;

namespace DWMH.Core
{
    public class Host
    {
        public string LastName { get; set; }

        public string ID { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal StandardRate { get;  set; }
        public decimal WeekendRate { get;  set; }

        public override bool Equals(object obj)
        {
            return obj is Host host &&
                   LastName == host.LastName &&
                   ID == host.ID &&
                   Email == host.Email &&
                   City == host.City &&
                   State == host.State &&
                   StandardRate == host.StandardRate &&
                   WeekendRate == host.WeekendRate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LastName, ID, Email, City, State, StandardRate, WeekendRate);
        }

        public void SetRates(decimal standard, decimal weekend)
        {
            StandardRate = standard;
            WeekendRate = weekend;
        }
    }
}
