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
        private readonly ICustomerRepository _customerRepository;

        public RegistrationManager(IEventRepository eventRepository, IRegistrationRepository registrationRepository, ICustomerRepository customerRepository)
        {
            _eventRepository = eventRepository;
            _registrationRepository = registrationRepository;
            _customerRepository = customerRepository;
        }

        //public async Task Register(Guid eventId, Guid userId)
        //{
        //    var @event = await _eventRepository.Get(eventId);
        //    var registration = new Registration(@event, userId);
        //    await _registrationRepository.Add(registration);
        //}

        public List<Registration> GetRegistrations(int customerId)
        {
            Customer customer = _customerRepository.GetCustomerById(customerId);

            RegistrationDTO registrationDTOs = new List<RegistrationDTO>();
            _registrationRepository.GetMembersFromRegistration(customerId);

            List<Registration> registrations = _registrationRepository.GetRegistrations(customerId);

            foreach (var registration in registrations)
            {
                Event @event = _eventRepository.Get(registration.EventId);
                var registrationDTO = new RegistrationDTO(registration.Id, event, customer, registration.Members);
                       registrationDTOs.Add(registrationDTO);
                       }
               return registrationDTOs;
               })
            }
        }
    }
}
