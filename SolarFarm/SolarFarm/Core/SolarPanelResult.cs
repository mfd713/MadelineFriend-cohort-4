using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core
{
    public class SolarPanelResult : Result
    {
        public SolarPanel Data { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SolarPanelResult result &&
                   Message == result.Message &&
                   Success == result.Success &&
                   EqualityComparer<SolarPanel>.Default.Equals(Data, result.Data);
        }
    }
}
