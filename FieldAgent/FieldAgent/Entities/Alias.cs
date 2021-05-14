using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class Alias
    {
        public int AliasId { get; set; }
        public string AliasName { get; set; }
        public string InterpolId { get; set; }
        public string Persona { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
