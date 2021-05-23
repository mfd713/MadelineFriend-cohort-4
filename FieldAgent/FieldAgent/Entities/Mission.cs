using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class Mission : IValidatableObject
    {
        public int MissionId { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="Codename cannot exceed 50 characters")]
        public string CodeName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }

        [Range(0,9999999999)]
        public decimal? OperationalCost { get; set; }
        public string Notes { get; set; }
        public Agency Agency { get; set; }


        [Required]
        public int AgencyId { get; set; }
        public List<Agent> Agents { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if(StartDate > ProjectedEndDate)
            {
                errors.Add(new ValidationResult("Start Date must be before projected End Date"));
            }

            
            return errors;
        }
    }
}
