using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldAgentWeb.Models
{
    public class AgencyMissionsModel
    {
        public int MissionId { get; set; }
        public string CodeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? OperationalCost { get; set; }
        public string Notes { get; set; }
        public string AgencyName { get; set; }
    }
}
