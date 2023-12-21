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
        public List<Member> Member { get; set; }
        public Event Event { get; set; }
        public Registration(List<Member> member, Event @event)
        {
            Member = member;
            Event = @event;
        }

        public Registration(int id, List<Member> member, Event @event)
        {
            Id = id;
            Member = member;
            Event = @event;
        }
        
    }
}
