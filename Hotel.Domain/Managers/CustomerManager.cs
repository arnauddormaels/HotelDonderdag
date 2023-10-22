using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddCustomer(string name, string email, string phone, string address)
        {
               
            Customer customer = new Customer(name, new ContactInfo(email, phone, new Address(Address.ToAddressLine(address))));
            try
            {
                _customerRepository.AddCustomer(customer);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("AddCustomer", ex);
            }
        }

        public void DeleteCustomer(int id)
        {
            _customerRepository.DeleteCustomer(id);
        }

        public IReadOnlyList<Customer> GetCustomers(string filter)
        {
            try
            {
                return _customerRepository.GetCustomers(filter);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("GetCustomers", ex);
            }
        }

        public void UpdateCustomer(int id, string name, string email, string phone, string address)
        {
            Customer customer = new Customer(id, name, new ContactInfo(email, phone, new Address(Address.ToAddressLine(address))));

            _customerRepository.UpdateCustomer(customer);
        }
    }
}
