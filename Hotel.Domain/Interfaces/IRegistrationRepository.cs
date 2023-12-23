using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface IRegistrationRepository
    {
        public int AddRegistration(Registration registration);
        public IReadOnlyList<Registration> GetRegistrations(int customerId);
        public void DeleteRegistration(int id);
    }
}
