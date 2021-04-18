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
        public DateTime DateInstalled { get; set; }
        public MaterialType Material { get; set; }
        public bool IsTracking { get; set; }

        public string GetKey()
        {
            return $"{Section}-{Row}-{Column}";
        }
        public override bool Equals(object obj)
        {
            return obj is SolarPanel panel &&
                   Section == panel.Section &&
                   Row == panel.Row &&
                   Column == panel.Column &&
                   Material == panel.Material &&
                   IsTracking == panel.IsTracking;
        }
    }
}
