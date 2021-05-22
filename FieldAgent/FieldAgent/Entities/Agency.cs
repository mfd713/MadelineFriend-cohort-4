using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class Agency
    {
        public int AgencyId { get; set; }

        [Required(ErrorMessage ="A short name is required")]
        [StringLength(25, ErrorMessage ="Short name cannot exceed 25 characters")]
        public string ShortName { get; set; }

        [StringLength(255, ErrorMessage = "Long name cannot exceed 255 characters")]
        public string LongName { get; set; }

        public List<Location> Locations { get; set; }
        public List<AgencyAgent> Agents { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
