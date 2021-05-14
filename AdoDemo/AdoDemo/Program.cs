using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdoDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string sqlConnectionString = @"Server=localhost;Database=GravelFamily;User Id=sa;Password=C0hort-4!";
            EmployeeRepository employeeRepository = new EmployeeRepository(sqlConnectionString);
            CustomerRepository customerRepository = new CustomerRepository(sqlConnectionString);
            EmployeeController controller = new EmployeeController(employeeRepository,customerRepository);
            controller.Run();
        }


    }
}
