using Hotel.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.Presentation.WindowsOrganisor
{
    /// <summary>
    /// Interaction logic for PriceInfoWindow.xaml
    /// </summary>
    public partial class PriceInfoWindow : Window
    {
        public PriceInfoUI PriceInfoUI { get; set; }
        private int adultPrice;
        private int childPrice;
        private int discount;
        private int adultAge;
        public PriceInfoWindow()
        {
            InitializeComponent();
        }

        public PriceInfoWindow(PriceInfoUI priceInfoUI)
        {
            PriceInfoUI = priceInfoUI;
            InitializeComponent();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
           
            
            if (IsFormatValid())
            {

                if (PriceInfoUI == null)
                {
                    //Nieuw
                    //wegschrijven
                    PriceInfoUI = new PriceInfoUI(adultPrice, childPrice, discount, adultAge);
                }

                else
                {
                    //Update
                    //update DB
                    PriceInfoUI.AdultPrice = adultPrice;
                    PriceInfoUI.ChildPrice = childPrice;
                    PriceInfoUI.Discount = discount;
                    PriceInfoUI.AdultAge = adultAge;

                }
                DialogResult = true;

                Close();
            }
            
        }
        public bool IsFormatValid()
        {
           
            if (!int.TryParse(AdultPriceTextBox.Text, out adultPrice))
            {
                MessageBox.Show("Adult Price is not a number");
                return false;
            }
            else if (!int.TryParse(ChildPriceTextBox.Text, out childPrice))
            {
                MessageBox.Show("Child Price is not a number");
                return false;
            }
            else if (!int.TryParse(DiscountTextBox.Text, out discount))
            {
                MessageBox.Show("Discount is not a number");
                return false;
            }
            else if (discount > 100)
            {
                MessageBox.Show("Discount must be smaller than 100");
                return false;
            }
            else if (!int.TryParse(AdultAgeTextBox.Text, out adultAge))
            {
                MessageBox.Show("Adult age is not a number");
                return false;
            }
            else if (adultPrice < 0 && childPrice < 0 && discount < 0 && adultAge < 0)
            {
                MessageBox.Show("Value can not be negative");
                return false;
            }
            else
            {
                return true;
            }
            
       

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
