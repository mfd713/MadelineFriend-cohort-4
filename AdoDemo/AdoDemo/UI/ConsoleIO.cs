using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDemo
{
    public class ConsoleIO
    {
        public static Employee PromptUserEmployee()
        {
            Employee employee = new Employee();
            employee.FirstName = PromptUser("Enter a first name");
            employee.LastName = PromptUser("Enter a last name");
            employee.StartDate = PromptUserDate("Enter start Date");
            employee.EndDate = PromptUserDate("Enter end Date");
            return employee;
        }

        public static DateTime PromptUserDate(string message)
        {
            DateTime result;
            while (!DateTime.TryParse(PromptUser(message), out result))
            {
                Console.WriteLine("Invalid date");
            }
            return result;

        }

        public static string PromptUser(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        public static int PromptUserInt(string message)
        {
            int result;
            while (!int.TryParse(PromptUser(message), out result))
            {
                Console.WriteLine("Invalid date");
            }
            return result;
        }
    }
}
