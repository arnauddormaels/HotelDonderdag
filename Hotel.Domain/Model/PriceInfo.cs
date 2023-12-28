using Hotel.Domain.Exceptions;
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

        public int AdultPrice
        {
            get => _adultPrice;
            set
            {
                if (value <= 0)
                {
                    throw new PriceInfoException("AdultPrice must be a positive number.");
                }
                _adultPrice = value;
            }
        }

        public int ChildPrice
        {
            get => _childPrice;
            set
            {
                if (value <= 0)
                {
                    throw new PriceInfoException("ChildPrice must be a positive number.");
                }
                _childPrice = value;
            }
        }

        public int Discount
        {
            get => _discount;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new PriceInfoException("Discount must be between 0 and 100 (inclusive).");
                }
                _discount = value;
            }
        }

        public int AdultAge
        {
            get => _adultAge;
            set
            {
                if (value <= 0)
                {
                    throw new PriceInfoException("AdultAge must be a positive number.");
                }
                _adultAge = value;
            }
        }

        public int Id { get => _id; set => _id = value; }

        public int CalculatePrice(int age)
        {
            if (age >= AdultAge)
            {
                return AdultPrice - AdultPrice * Discount / 100;
            }
            else
            {
                return ChildPrice - ChildPrice * Discount / 100;
            }
        }

        public override bool Equals(Object obj)
        {
            if (typeof(PriceInfo) != obj.GetType())
            {
                throw new PriceInfoException("Object is not a PriceInfo");
            }
            PriceInfo priceInfo = (PriceInfo)obj;

            return priceInfo != null &&
                    _id == priceInfo._id &&
                   _adultPrice == priceInfo._adultPrice &&
                   _childPrice == priceInfo._childPrice &&
                   _discount == priceInfo._discount &&
                   _adultAge == priceInfo._adultAge;
        }

    }
}
