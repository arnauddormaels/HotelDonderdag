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

namespace Hotel.Presentation.Customer.WindowsOrganisor
{
    /// <summary>
    /// Interaction logic for OrganisorWindow.xaml
    /// </summary>
    public partial class OrganisorWindow : Window
    {
        public OrganisorUI OrganisorUI { get; set; }
        public OrganisorWindow(OrganisorUI organisorUI)
        {
            InitializeComponent();
            this.OrganisorUI = organisorUI ;
            if (OrganisorUI != null)
            {
                IdTextBox.Text = OrganisorUI.Id.ToString();
                NameTextBox.Text = OrganisorUI.Name;
                EmailTextBox.Text = OrganisorUI.Email;
                PhoneTextBox.Text = OrganisorUI.Phone;
                string[] address = Address.ToAddressArray(OrganisorUI.Address);
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
            if (OrganisorUI == null)
            {
                //Nieuw
                //wegschrijven
                //TODO nrofmembers
                Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);
                OrganisorUI = new OrganisorUI(NameTextBox.Text, EmailTextBox.Text, address.ToString(), PhoneTextBox.Text);
            }
            else
            {
                //Update
                //update DB
                OrganisorUI.Email = EmailTextBox.Text;
                OrganisorUI.Phone = PhoneTextBox.Text;
                OrganisorUI.Name = NameTextBox.Text;

                OrganisorUI.Address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text).ToString();
            }
            DialogResult = true;

            Close();
        }
        }
}
