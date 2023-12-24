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
        private DateTime _fixture;
        private int _nrOfPlaces;
        private Boolean _status; 
        private PriceInfo _priceInfo;
        private Description _description;

        public Event(DateTime fixture, int nrOfPlaces, PriceInfo priceInfo, Description description)
        {
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            PriceInfo = priceInfo;
            Description = description;
        }

        public Event(int id, DateTime fixture, int nrOfPlaces, PriceInfo priceInfo, Description description)
        {
            Id = id;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            PriceInfo = priceInfo;
            Description = description;
        }

        public Event(int id, DateTime fixture, int nrOfPlaces, PriceInfo priceInfo, Description description, bool status) : this(id, fixture, nrOfPlaces, priceInfo, description)
        {
            Status = status;
        }

        public int Id { get => _id; set => _id = value; }
        public DateTime Fixture { get => _fixture; set => _fixture = value; }
        public int NrOfPlaces { get => _nrOfPlaces; set => _nrOfPlaces = value; }
        public PriceInfo PriceInfo { get => _priceInfo; set => _priceInfo = value; }
        public Description Description { get => _description; set => _description = value; }
        public bool Status { get => _status; set => _status = value; }
    }
}
