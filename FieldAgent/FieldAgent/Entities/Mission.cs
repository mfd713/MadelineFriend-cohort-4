using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class Mission
    {
        public int MissionId { get; set; }
        public string CodeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? OperationalCost { get; set; }
        public string Notes { get; set; }

        public Agency Agency { get; set; }
        public int AgencyId { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
