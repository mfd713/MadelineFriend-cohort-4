using System;
using System.Collections.Generic;

#nullable disable

namespace GravelFamily.DAL
{
    public class Login
    {
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public Customer Customer { get; set; }
    }
}
