using Hotel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private string connectionString;

        public EventRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
