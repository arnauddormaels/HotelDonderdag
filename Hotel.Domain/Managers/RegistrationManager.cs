using Hotel.Domain.DTO;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
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
        private readonly IMembersRepository _memberRepository;

        //public RegistrationManager(IRegistrationRepository registrationRepository)
        //{
        //    this._registrationRepository = registrationRepository;
        //}

        public RegistrationManager(IRegistrationRepository registrationRepository, IEventRepository eventRepository,  IMembersRepository memberRepository)
        {
            _eventRepository = eventRepository;
            _registrationRepository = registrationRepository;
            _memberRepository = memberRepository;
        }

        //public async Task Register(Guid eventId, Guid userId)
        //{
        //    var @event = await _eventRepository.Get(eventId);
        //    var registration = new Registration(@event, userId);
        //    await _registrationRepository.Add(registration);
        //}

        public List<Registration> GetRegistrations(int customerId)
        {
            return _registrationRepository.GetRegistrations(customerId).ToList();
        }
    }
}
