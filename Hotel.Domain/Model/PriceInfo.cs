using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class PriceInfo
    {
        private int _id;
        private int _adultPrice;
        private int _childPrice;
        private int _discount; //in %
        private int _adultAge;

        public PriceInfo(int adultPrice, int childPrice, int discount, int adultAge)
        {
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
            AdultAge = adultAge;
        }

        public PriceInfo(int id, int adultPrice, int childPrice, int discount, int adultAge)
        {
            Id = id;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
            AdultAge = adultAge;
        }

        public int AdultPrice { get => _adultPrice; set => _adultPrice = value; }
        public int ChildPrice { get => _childPrice; set => _childPrice = value; }
        public int Discount { get => _discount; set => _discount = value; }
        public int AdultAge { get => _adultAge; set => _adultAge = value; }
        public int Id { get => _id; set => _id = value; }
    }
}
