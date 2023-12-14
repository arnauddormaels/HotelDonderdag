using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.Model
{
    public class RegistrationUI
    {
        List<MemberUI> membersUI = new List<MemberUI>();
        public EventUI eventUI;
        public int Id { get; set; }

        public RegistrationUI(int id, List<MemberUI> members, EventUI @event)
        {
            Id = id;
            this.membersUI = members;
            this.eventUI = @event;
        }

        public RegistrationUI(List<MemberUI> members, EventUI @event)
        {
            this.membersUI = members;
            this.eventUI = @event;
        }
    }
}
