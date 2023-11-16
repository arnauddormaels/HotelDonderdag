using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
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

namespace Hotel.Presentation.Customer
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerUI CustomerUI { get; set; }
        public CustomerWindow(CustomerUI customerUI)
        {
            InitializeComponent();
            this.CustomerUI = customerUI;
            if (CustomerUI != null)
            {
                IdTextBox.Text = CustomerUI.Id.ToString();
                NameTextBox.Text = CustomerUI.Name;
                EmailTextBox.Text = CustomerUI.Email;
                PhoneTextBox.Text = CustomerUI.Phone;
                string[] address = Address.ToAddressArray(CustomerUI.Address);
                CityTextBox.Text = address[0];
                StreetTextBox.Text = address[2];
                ZipTextBox.Text = address[1];
                HouseNumberTextBox.Text = address[3];

            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerUI == null)
            {
                //Nieuw
                //wegschrijven
                //TODO nrofmembers
                Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);
                CustomerUI = new CustomerUI(NameTextBox.Text, EmailTextBox.Text, address.ToString(), PhoneTextBox.Text, 0);
            }
            else
            {
                //Update
                //update DB
                CustomerUI.Email=EmailTextBox.Text;
                CustomerUI.Phone=PhoneTextBox.Text;
                CustomerUI.Name=NameTextBox.Text;

                CustomerUI.Address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text).ToString();
            }
            DialogResult = true;
            
            Close();
        }

    }
}
