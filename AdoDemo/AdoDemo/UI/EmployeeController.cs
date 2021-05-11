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

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Run()
        {
            while (true)
            {
                int choice = ConsoleIO.PromptUserInt("1. Display Employees\n2. Add Employee\n3. Edit Employee\n4. Remove Employee\n5. Exit");
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
                        return;

                }
            }
        }
    }
}
