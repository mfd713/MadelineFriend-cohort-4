using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class Alias
    {
        public int AliasId { get; set; }

        [Required]
        public string AliasName { get; set; }

        [StringLength(36)]
        public string InterpolId { get; set; }

        public string Persona { get; set; }

        [Required]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
