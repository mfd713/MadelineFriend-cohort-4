using System;
using System.Collections.Generic;

#nullable disable

namespace GravelFamily.DAL
{
    public class Project
    {
        public Project()
        {
            ProjectEmployees = new HashSet<ProjectEmployee>();
            ProjectItems = new HashSet<ProjectItem>();
        }

        public int ProjectId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public ICollection<ProjectItem> ProjectItems { get; set; }
    }
}
