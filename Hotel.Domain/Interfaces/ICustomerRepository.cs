using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        void DeleteCustomer(int id);
        Customer GetCustomerById(int id);
        IReadOnlyList<Customer> GetCustomers(string filter);
        void UpdateCustomer(Customer customer);

    }
}
