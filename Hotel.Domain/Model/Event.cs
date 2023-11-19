using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Event
    {
        private int _id;
        private string _name;
        private DateTime _fixture;
        private int _nrOfPlaces;
        private PriceInfo _priceInfo;
        private Description _description;

        public Event(string name, DateTime fixture, int nrOfPlaces, PriceInfo priceInfo, Description description)
        {
            Name = name;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            PriceInfo = priceInfo;
            Description = description;
        }

        public Event(int id, string name, DateTime fixture, int nrOfPlaces, PriceInfo priceInfo, Description description)
        {
            Id = id;
            Name = name;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            PriceInfo = priceInfo;
            Description = description;
        }

        public int Id { get => _id; set => _id = value; }
        public DateTime Fixture { get => _fixture; set => _fixture = value; }
        public int NrOfPlaces { get => _nrOfPlaces; set => _nrOfPlaces = value; }
        public PriceInfo PriceInfo { get => _priceInfo; set => _priceInfo = value; }
        public Description Description { get => _description; set => _description = value; }
        public string Name { get => _name; set => _name = value; }
    }
}
