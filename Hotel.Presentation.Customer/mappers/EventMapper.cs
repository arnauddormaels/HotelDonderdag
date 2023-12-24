using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.mappers
{
    public static class EventMapper
    {

        public static EventUI MapToEventUI(Event e)
        {
            PriceInfoUI priceInfoUI = new PriceInfoUI(e.PriceInfo.AdultPrice, e.PriceInfo.ChildPrice, e.PriceInfo.Discount, e.PriceInfo.AdultAge);
            DescriptionUI descriptionUI = new DescriptionUI(e.Description.Name, e.Description.DescriptionText, e.Description.Duration, e.Description.Location);
            if (e.Status == null)
            {
                return new EventUI(e.Id, e.Fixture, e.NrOfPlaces,e.Status, priceInfoUI, descriptionUI);
            }
            return new EventUI(e.Id, e.Fixture, e.NrOfPlaces, priceInfoUI, descriptionUI);
        }

        public static Event MapToEventModel(EventUI e)
        {
            PriceInfo priceInfo;
            Description description;
            if (e.PriceInfo.Id == null && e.Description.Id == null)
            {
                priceInfo = new PriceInfo(e.PriceInfo.AdultPrice, e.PriceInfo.ChildPrice, e.PriceInfo.Discount, e.PriceInfo.AdultAge);
                description = new Description(e.Description.Name, e.Description.Description, e.Description.Duration, e.Description.Location);

            }
            else
            {
                 priceInfo = new PriceInfo(e.PriceInfo.Id, e.PriceInfo.AdultPrice, e.PriceInfo.ChildPrice, e.PriceInfo.Discount, e.PriceInfo.AdultAge);
                 description = new Description(e.Description.Id, e.Description.Name, e.Description.Description, e.Description.Duration, e.Description.Location);
            }
            return new Event(e.Fixture, e.NrOfPlaces, priceInfo, description);
        }

    }
}
