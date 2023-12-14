using Hotel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class RegistrationManager
    {
        private readonly IEventRepository _eventRepository;
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationManager(IEventRepository eventRepository, IRegistrationRepository registrationRepository)
        {
            _eventRepository = eventRepository;
            _registrationRepository = registrationRepository;
        }

        //public async Task Register(Guid eventId, Guid userId)
        //{
        //    var @event = await _eventRepository.Get(eventId);
        //    var registration = new Registration(@event, userId);
        //    await _registrationRepository.Add(registration);
        //}


    }
}
