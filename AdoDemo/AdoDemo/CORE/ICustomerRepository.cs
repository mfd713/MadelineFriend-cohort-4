using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDemo
{
    public interface ICustomerRepository
    {
        Customer CreateCustomer(Customer customer);
        List<Customer> ReadAll();
        List<Customer> ReadByName(string prefix);

        void Update(Customer customer);

        void Delete(int CustomerId);
    }
}
