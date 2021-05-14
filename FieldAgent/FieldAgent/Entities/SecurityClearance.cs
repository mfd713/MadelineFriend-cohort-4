using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class SecurityClearance
    {
        public int SecurityClearanceId { get; set; }
        public string SecurityClearanceName { get; set; }

        public List<AgencyAgent> AgencyAgents { get; set; }
    }
}
