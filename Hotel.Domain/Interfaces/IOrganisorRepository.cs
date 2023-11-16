using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface IOrganisorRepository
    {
        Organisor AddOrganisor(Organisor organisor);
        void DeleteOrganisor(int id);
        Organisor GetOrganisorById(int id);
        IReadOnlyList<Organisor> GetOrganisors(string filter);
        void UpdateOrganisor(Organisor organisor);

    }
}
