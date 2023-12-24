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
    public class EventManager
    {
        private readonly IEventRepository _eventRepository;

        public EventManager(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public int AddEvent(int organisorId, Event e)
        {
            try
            {
                return _eventRepository.AddEvent(organisorId, e);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("AddEvent", ex);
            }
        }

        public IReadOnlyList<Event> GetEvents()
        {
            try
            {
                return _eventRepository.GetEvents();
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("GetEvents", ex);
            }

        }

    public List<Event> GetEventsByOrganisorId(int id)
    {
        return _eventRepository.GetEventsByOrganisorId(id);
    }

        public Boolean UpdateStatusEvent(Event @event)
        {
            try
            {
                return _eventRepository.UpdateStatusEvent(@event);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("UpdateStatusEvent", ex);
            }
        }
    }
}
