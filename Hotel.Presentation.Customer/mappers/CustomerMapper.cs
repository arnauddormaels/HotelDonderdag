using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.mappers
{
    public static class CustomerMapper
    {
        public static CustomerUI MapToCustomerUI(Customer customer)
        {
            return new CustomerUI(customer.Id, customer.Name, customer.Contact.Email, customer.Contact.Address.ToString(), customer.Contact.Phone,
                customer.GetMembers().Count());
        }

        public static CustomerWithMemberListUI? MapToCustomerWithMemberListUI(Customer customer)
        {
            List<MemberUI> memberUis = new List<MemberUI>();
            foreach(Member member in customer.GetMembers())
            {
                memberUis.Add(MemberMapper.MapToMemberUI(member));
            }

            return new CustomerWithMemberListUI(customer.Id, customer.Name, customer.Contact.Email, customer.Contact.Address.ToString(), customer.Contact.Phone,
                           memberUis);
        }
    }
}
