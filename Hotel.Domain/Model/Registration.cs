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
        public List<Member> Members { get; set; }
        public List<Event> Events { get; set; }
        public List<PriceInfo> PriceInfos { get; set; }
        public Registration(List<Member> members, List<Event> events, List<PriceInfo> priceInfos)
        {
            Members = members;
            Events = events;
            PriceInfos = priceInfos;
        }
        public Registration(int id, List<Member> members, List<Event> events, List<PriceInfo> priceInfos)
        {
            Id = id;
            Members = members;
            Events = events;
            PriceInfos = priceInfos;
        }
    }
}
