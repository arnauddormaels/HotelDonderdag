using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class OrganisorManager
    {
        private readonly IOrganisorRepository _organisorRepository;

        public OrganisorManager(IOrganisorRepository organisorRepository)
        {
            _organisorRepository = organisorRepository;
        }

        public Organisor AddOrganisor(string name, string email, string phone, string address)
        {

            Organisor organisor = new Organisor(name, new ContactInfo(email, phone, new Address(Address.ToAddressLine(address))));
            try
            {
                return _organisorRepository.AddOrganisor(organisor);
            }
            catch (Exception ex)
            {
                throw new OrganisorManagerException("AddOrganisor", ex);
            }
        }

        public void DeleteOrganisor(int id)
        {
            _organisorRepository.DeleteOrganisor(id);
        }

        public IReadOnlyList<Organisor> GetOrganisors(string filter)
        {
            try
            {
                return _organisorRepository.GetOrganisors(filter);
            }
            catch (Exception ex)
            {
                throw new OrganisorManagerException("GetOrganisors", ex);
            }
        }
        public Organisor GetOrganisorById(int id)
        {
            try
            {
                return _organisorRepository.GetOrganisorById(id);
            }
            catch (Exception ex)
            {
                throw new OrganisorManagerException("GetOrganisor", ex);
            }
        }
        public void UpdateOrganisor(int id, string name, string email, string phone, string address)
        {
            Organisor organisor = new Organisor(id, name, new ContactInfo(email, phone, new Address(Address.ToAddressLine(address))));

            _organisorRepository.UpdateOrganisor(organisor);
        }

        public List<Event> GetEventsByOrganisorId(int organisorId)
        {
            try
            {
                return _organisorRepository.GetOrganisorById(organisorId).GetEvents().ToList<Event>();
            }
            catch (Exception ex)
            {
                throw new OrganisorManagerException("GetEventsByOrganisorId", ex);
            }
        }
    }
}
