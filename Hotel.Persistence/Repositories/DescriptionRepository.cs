using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class DescriptionRepository : IDescriptionRepository
    {
        private string connectionString;

        public DescriptionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        IReadOnlyList<Description> IDescriptionRepository.GetDescriptions()
        {
            return new List<Description>();
        }
    }
}
