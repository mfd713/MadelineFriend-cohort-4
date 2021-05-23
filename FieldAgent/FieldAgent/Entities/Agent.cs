using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class Agent
    {
        public int AgentId { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="First name can't exceed 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last name can't exceed 50 characters")]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public decimal Height { get; set; }

        public List<Alias> Aliases { get; set; }
        public List<AgencyAgent> Agencies { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
