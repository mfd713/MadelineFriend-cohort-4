using System;
using System.Collections.Generic;

#nullable disable

namespace GravelFamily.DAL
{
    public class Employee
    {
        public Employee()
        {
            ProjectEmployees = new HashSet<ProjectEmployee>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
