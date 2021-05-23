using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldAgentWeb.Models
{
    public class AgentAliasesModel
    {
        public int AliasId { get; set; }
        public string AliasName { get; set; }
        public string InterpolId { get; set; }
        public string Persona { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
    }
}
