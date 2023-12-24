using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Model
{
    public class EventUI
    {
        public int Id { get; set; }
        public DateTime Fixture { get; set; }
        public int NrOfPlaces { get; set; }
        public bool Status { get; set; }
        public PriceInfoUI PriceInfo { get; set; }
        public DescriptionUI Description { get; set; }
        

        public EventUI(int id, DateTime fixture, int nrOfPlaces, PriceInfoUI priceInfo, DescriptionUI description)
        {
            Id = id;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            PriceInfo = priceInfo;
            Description = description;
        }
        public EventUI(DateTime fixture, int nrOfPlaces, PriceInfoUI priceInfo, DescriptionUI description)
        {
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            PriceInfo = priceInfo;
            Description = description;
        }

        public EventUI(int id, DateTime fixture, int nrOfPlaces, bool status, PriceInfoUI priceInfo, DescriptionUI description)
        {
            Id = id;
            Fixture = fixture;
            NrOfPlaces = nrOfPlaces;
            Status = status;
            PriceInfo = priceInfo;
            Description = description;
        }
    }
}
