using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDemo
{
    public class EmployeeController
    {
        private IEmployeeRepository _employeeRepository;
        private ICustomerRepository _customerRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, ICustomerRepository customerRepository)
        {
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
        }

        public void Run()
        {
            while (true)
            {
                int choice = ConsoleIO.PromptUserInt("0. Exit\n1. Display Employees\n2. Add Employee\n3. Edit Employee\n4. Remove Employee\n5. View Customers by Last Name" +
                    "\n6. Add Customer\n7. Edit Customer\n8. Delete Customer");
                switch (choice)
                {
                    case 1:
                        foreach (var emp in _employeeRepository.ReadAll())
                        {
                            Console.WriteLine($"{emp.EmployeeId} : {emp.FirstName} ");
                        }
                        break;
                    case 2:
                        Employee employee = ConsoleIO.PromptUserEmployee();
                        _employeeRepository.CreateEmployee(employee);
                        break;
                    case 3:
                        Employee toUpdate = ConsoleIO.PromptUserEmployee();
                        toUpdate.EmployeeId = ConsoleIO.PromptUserInt("Enter ID of employee you want to update: ");
                        _employeeRepository.Update(toUpdate);
                        break;
                    case 4:
                        int ToDelete = ConsoleIO.PromptUserInt("Enter ID of employee you want to delete: ");
                        _employeeRepository.Delete(ToDelete);
                        break;
                    case 5:
                        string search = ConsoleIO.PromptUser("Customer last name starts with: ");
                        foreach (var cust in _customerRepository.ReadByName(search))
                        {
                            Console.WriteLine($"{cust.CustomerID} : {cust.FirstName} {cust.LastName}");
                        }
                        break;
                    case 6:
                        Customer customer = ConsoleIO.PromptUserCustomer();
                        _customerRepository.CreateCustomer(customer);
                        break;
                    case 7:
                        Customer custUpdate = ConsoleIO.PromptUserCustomer();
                        custUpdate.CustomerID = ConsoleIO.PromptUserInt("Enter the ID of the customer you want to edit");
                        _customerRepository.Update(custUpdate);
                        break;
                    case 8:
                        int deleteID = ConsoleIO.PromptUserInt("Enter the ID of the Customer you want to delete");
                        _customerRepository.Delete(deleteID);
                        break;
                    default:
                        return;

                }
            }
        }
    }
}
