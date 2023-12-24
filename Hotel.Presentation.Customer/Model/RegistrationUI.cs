using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace Hotel.Presentation.Model
{
    public class RegistrationUI
    {
        public int Id { get; set; }
        public EventUI eventUI;
        public Dictionary<int, MemberUI> memberUIs = new Dictionary<int, MemberUI>();
        public string memberNames { get; set; } = "Registrated Members :"; 
        public string eventName { get => eventUI.Description.Name; }
        public string eventDescription { get => eventUI.Description.Description; }
        public int eventDuration { get => eventUI.Description.Duration; }
        public DateTime eventDate { get => eventUI.Fixture; }
        public string eventLocation { get => eventUI.Description.Location; }
        public int TotalPrice { get; set; } // TODO Prijs berekenen 

        public RegistrationUI(int id, Dictionary<int, MemberUI> members, EventUI @event, int totalPrice)
        {
            Id = id;
            this.memberUIs = members;
            this.eventUI = @event;
            MapRegistrationUI(members);
            this.TotalPrice = totalPrice;

        }

        public RegistrationUI(Dictionary<int, MemberUI> members, EventUI @event)
        {
            this.memberUIs = members;
            this.eventUI = @event;
            //MapRegistrationUI(members);

        }

        public void MapRegistrationUI(Dictionary<int, MemberUI> dictionary)
        {
            foreach (var member in dictionary)
            {
                memberNames += $"{(memberNames.Length == 0 ? "" : "\n")}registrationId : {member.Key} | memberName : {member.Value.Name}";

            }
        }
    }
}
