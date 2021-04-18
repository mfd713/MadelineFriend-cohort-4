using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core
{
    public class ListOfPanelsResult : Result
    {
        public Dictionary<string,SolarPanel> Data { get; set; }
    }
}
