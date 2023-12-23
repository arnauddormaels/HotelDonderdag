using Hotel.Domain.Managers;
using Hotel.Presentation.Customer.mappers;
using Hotel.Presentation.Customer.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Hotel.Presentation.Customer.WindowsCustomer
{
    /// <summary>
    /// Interaction logic for RegistrationsWindow.xaml
    /// </summary>
    public partial class RegistrationsWindow : Window
    {
        private ObservableCollection<RegistrationUI> registrationUIs = new ObservableCollection<RegistrationUI>();
        private RegistrationManager registrationManager;
        private string conn = "Data Source=LAPTOP-UMGHNHQ1\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True";

        public RegistrationsWindow(int customerId)
        {
            InitializeComponent();
            registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository, RepositoryFactory.EventRepository, RepositoryFactory.MembersRepository);
            registrationUIs = new ObservableCollection<RegistrationUI>(registrationManager.GetRegistrations(customerId).Select(x => RegistrationMapper.MapToRegistrationUI(x)).ToList());
            RegistrationsDataGrid.ItemsSource = registrationUIs;
        }

        private void MenuItemAddRegistration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDeleteRegistration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemUpdateRegistration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
