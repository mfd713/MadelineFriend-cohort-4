using System;
using System.Collections.Generic;

#nullable disable

namespace GravelFamily.DAL
{
    public class Customer
    {
        public Customer()
        {
            Projects = new HashSet<Project>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public DateTime CustomerSince { get; set; }

        public Login Login { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
