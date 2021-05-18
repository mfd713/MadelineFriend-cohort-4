using System;
using System.Collections.Generic;

#nullable disable

namespace GravelFamily.DAL
{
    public class ProjectEmployee
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}
