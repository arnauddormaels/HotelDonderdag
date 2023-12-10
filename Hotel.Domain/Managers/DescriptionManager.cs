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
    public class DescriptionManager
    {
        private IDescriptionRepository _descriptionRepo;
        public DescriptionManager(IDescriptionRepository repo)
        {
            _descriptionRepo = repo;
        }

        public int AddDescription(Description description)
        {
            try
            {
                return _descriptionRepo.AddDescription(description);
            }
            catch (Exception ex)
            {
                throw new DescriptionManagerException("AddDescription", ex);
            }
        }

        public void DeleteDescription(int id)
        {
            try
            {
                _descriptionRepo.DeleteDescription(id);
            }
            catch (Exception ex)
            {
                throw new DescriptionManagerException("DeleteDescription", ex);
            }
        }

        public IReadOnlyList<Description> GetDescriptions()
        {
            return _descriptionRepo.GetDescriptions();
        }
    }
}
