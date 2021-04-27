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
        public decimal StandardRate { get; private set; }
        public decimal WeekendRate { get; private set; }
    }
}
