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
        public IReadOnlyList<Description> GetDescriptions()
        {
            return _descriptionRepo.GetDescriptions();
        }
    }
}
