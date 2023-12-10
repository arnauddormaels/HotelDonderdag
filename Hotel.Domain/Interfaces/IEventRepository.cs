using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface IEventRepository
    {
        int AddEvent(int organisorId, Event e);
        List<Event> GetEventsByOrganisorId(int id);
    }
}
