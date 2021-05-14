using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class Agent
    {
        public int AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Height { get; set; }

        public List<Alias> Aliases { get; set; }
        public List<AgencyAgent> Agencies { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
