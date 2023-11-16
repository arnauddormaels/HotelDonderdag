using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class OrganisorRepository : IOrganisorRepository
    {
        private string connectionString;

        public OrganisorRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Organisor AddOrganisor(Organisor organisor)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrganisor(int id)
        {
            throw new NotImplementedException();
        }

        public Organisor GetOrganisorById(int id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Organisor> GetOrganisors(string filter)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrganisor(Organisor organisor)
        {
            throw new NotImplementedException();
        }

    }
}
