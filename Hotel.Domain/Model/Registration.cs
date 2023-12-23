using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Registration
    {

        public int Id { get; set; }
        public Dictionary<int, Member> Members { get; set; } = new Dictionary<int, Member>();
        public Event Event { get; set; }
        public Registration(Dictionary<int, Member> members, Event @event)
        {
            Members = members;
            Event = @event;
        }

        public Registration(int id, Dictionary<int, Member> members, Event @event)
        {
            Id = id;
            Members = members;
            Event = @event;
        }

        public Registration(int id, Event @event)
        {
            Id = id;
            Event = @event;
        }
    }
}
