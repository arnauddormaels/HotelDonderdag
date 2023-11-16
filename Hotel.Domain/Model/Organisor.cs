using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Model
{
    public class Organisor
    {
        public Organisor(int id, string name, ContactInfo contact, List<Event> events) : this(id, name, contact)
        {
            _events = events;
        }
        public Organisor(int id, string name, ContactInfo contact)
        {
            Id = id;
            Name = name;
            Contact = contact;
        }

        public Organisor(string name, ContactInfo contact)
        {
            Name = name;
            Contact = contact;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public ContactInfo Contact { get; set; }
        private List<Event> _events = new List<Event>(); // No duplicates


        public IReadOnlyList<Event> GetEvents()
        {
            return _events.AsReadOnly();
        }

        public void AddEvent(Event newEvent)
        {
            if (!_events.Contains(newEvent))
            {
                _events.Add(newEvent);
            }
            else
            {
                throw new OrganisorException("AddEvent");
            }
        }

        public bool CheckEvent(Event newEvent)
        {
            try
            {
                return !_events.Contains(newEvent); 
            }
            catch (Exception)
            {
                throw new OrganisorException("CheckEvent");
            }
        }

        public void RemoveEvent(Event e)
        {
            if (_events.Contains(e)) 
            {
                _events.Remove(e);
            }
            else
            {
                throw new OrganisorException("RemoveEvent");
            }
        }
    }
}
