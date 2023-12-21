using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.DTO
{
    public class RegistrationDTO
    {
        public RegistrationDTO(int eventId, int customerId, List<int> memberIds)
        {
            EventId = eventId;
            this.customerId = customerId;
            this.memberIds = memberIds;
        }

        public RegistrationDTO(int id, int eventId, int customerId, List<int> memberIds)
        {
            Id = id;
            EventId = eventId;
            this.customerId = customerId;
            this.memberIds = memberIds;
        }

        public int Id { get; set; }
        public int EventId { get; set; }
        public int customerId { get; set; }
        public List<int> memberIds { get; set; }


    }
}
