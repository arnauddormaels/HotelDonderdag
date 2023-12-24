using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.mappers
{
    public static class PriceInfoMapper
    {
        public static PriceInfoUI ToPriceInfoUI(PriceInfo priceInfo)
        {
            return new PriceInfoUI(priceInfo.Id, priceInfo.AdultPrice, priceInfo.ChildPrice, priceInfo.Discount, priceInfo.AdultAge);
        }

        public static PriceInfo ToPriceInfo(PriceInfoUI priceInfoUI)
        {
            return new PriceInfo(priceInfoUI.Id, priceInfoUI.AdultPrice, priceInfoUI.ChildPrice, priceInfoUI.Discount, priceInfoUI.AdultAge);
        }
    }
}
