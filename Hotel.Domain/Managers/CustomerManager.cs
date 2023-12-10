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
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer AddCustomer(string name, string email, string phone, string address)
        {
               
            Customer customer = new Customer(name, new ContactInfo(email, phone, new Address(Address.ToAddressLine(address))));
            try
            {
                return _customerRepository.AddCustomer(customer);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("AddCustomer", ex);
            }
        }

        public void DeleteCustomer(int id)
        {
            try
            {
                _customerRepository.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("DeleteCustomer", ex);
            }
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
        public Customer GetCustomerById(int id)
        {
            try
            {
                return _customerRepository.GetCustomerById(id);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("GetCustomer", ex);
            }
        }
        public void UpdateCustomer(int id, string name, string email, string phone, string address)
        {
            Customer customer = new Customer(id, name, new ContactInfo(email, phone, new Address(Address.ToAddressLine(address))));

            _customerRepository.UpdateCustomer(customer);
        }

        public List<Member> GetMembersByCustomerId(int customerId)
        {
            try
            {
                return _customerRepository.GetCustomerById(customerId).GetMembers().ToList<Member>();
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("GetMembersByCustomerId", ex);
            }
        }

        public bool CheckMember(int customerId, string memberName, DateTime birthDate)
        { //Check uitvoeren in de customer of member al bestaat.
            DateOnly date = DateOnly.FromDateTime(birthDate);
            Member member = new Member(memberName, date);
            try
            {
               return _customerRepository.GetCustomerById(customerId).CheckMember(member);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
