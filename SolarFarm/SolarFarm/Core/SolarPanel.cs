using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core
{
    public class SolarPanel
    {
        public string Section { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public MaterialType Material { get; set; }
        public bool IsTracking { get; set; }
    }
}
