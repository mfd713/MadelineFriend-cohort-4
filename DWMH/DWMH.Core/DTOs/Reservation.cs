using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWMH.Core
{
    public class Reservation
    {
        public DateTime StatrtDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guest Guest { get; set; }
        public int ID { get; set; }
        public decimal Total { get; set; }
        public Host Host { get; set; }

    }
}
