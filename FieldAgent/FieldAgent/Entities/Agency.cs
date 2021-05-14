using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class Agency
    {
        public int AgencyId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }

        public List<Location> Locations { get; set; }
        public List<AgencyAgent> Agents { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
