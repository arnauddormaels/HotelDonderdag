using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.mappers
{
    public static class DescriptionMapper
    {
        public static DescriptionUI ToDescriptionUI(Description description)
        {
            return new DescriptionUI(description.Id, description.Name,description.DescriptionText, description.Duration, description.Location);
        }

        public static Description ToDescription(this DescriptionUI descriptionUI)
        {
            return new Description(descriptionUI.Id, descriptionUI.Name,descriptionUI.Location, descriptionUI.Duration,descriptionUI.Description);
        }
    }
}
