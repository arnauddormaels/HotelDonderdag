namespace Hotel.Presentation.Customer.Model
{
    public class PriceInfoUI
    {
        public PriceInfoUI(int adultPrice, int childPrice, int discount, int adultAge)
        {
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
            AdultAge = adultAge;
        }

        public int AdultPrice { get; set; }
        public int ChildPrice { get; set; }
        public int Discount { get; set; }
        public int AdultAge { get; set; }
    }
}