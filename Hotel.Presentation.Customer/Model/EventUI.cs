using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.Model
{
    public class EventUI
    {
        public int Id { get; set; }
        public DateTime Fixture { get; set; }
        public int NrOfPlaces { get; set; }
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



    }
}
