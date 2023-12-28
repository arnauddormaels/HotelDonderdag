using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.mappers;
using Hotel.Presentation.Model;
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

namespace Hotel.Presentation.WindowsCustomer
{
    /// <summary>
    /// Interaction logic for RegistrationsWindow.xaml
    /// </summary>
    public partial class RegistrationsWindow : Window
    {
        private ObservableCollection<RegistrationUI> registrationUIs = new ObservableCollection<RegistrationUI>();
        private RegistrationManager registrationManager;
        private string conn = "Data Source=LAPTOP-UMGHNHQ1\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True";
        private int customerId;
        public RegistrationsWindow(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
            registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository, RepositoryFactory.EventRepository, RepositoryFactory.MembersRepository);
            registrationUIs = new ObservableCollection<RegistrationUI>(registrationManager.GetRegistrations(customerId).Select(x => RegistrationMapper.MapToRegistrationUI(x)).ToList());
            RegistrationsDataGrid.ItemsSource = registrationUIs;
        }

        private void MenuItemAddRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow w = new RegistrationWindow(customerId);

            RegistrationUI registrationUI;
            if (w.ShowDialog() == true)
            {
                try
                {
                    registrationUIs = new ObservableCollection<RegistrationUI>(registrationManager.GetRegistrations(customerId).Select(x => RegistrationMapper.MapToRegistrationUI(x)).ToList());
                    RegistrationsDataGrid.ItemsSource = registrationUIs;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "MenuItemAddRegistration_Click");
                }
            }
            RegistrationsDataGrid.Items.Refresh();

        }

        private void MenuItemDeleteRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationUI selectedRegistration = (RegistrationUI)RegistrationsDataGrid.SelectedItem;
            if (selectedRegistration == null)
            {
                //Als geen member geselecteerd is dan wordt er een melding gegeven.
                MessageBox.Show("Select registration you want delete");
            }
            else
            {
                //MessageBoxResult result = MessageBox.Show("Wil je doorgaan?", "Vraag", MessageBoxButton.YesNo); if (result == MessageBoxResult.Yes)
                // {     //Code voor het geval van "Ja"}else{     // Code voor het geval van "Nee" of als het venster wordt gesloten}
                MessageBoxResult confirmDeleteOrNot = MessageBox.Show("Delete registration", "Are you sure you want to delete" + selectedRegistration.eventName + "for these users" + selectedRegistration.memberNames + "?", MessageBoxButton.YesNo);
                if (confirmDeleteOrNot == MessageBoxResult.Yes)
                {

                    try
                    {


                        //Een controle of de member al bestaat.
                        registrationManager.DeleteRegistration(selectedRegistration.Id);
                        registrationUIs.Remove((RegistrationUI)RegistrationsDataGrid.SelectedItem);
                        RegistrationsDataGrid.Items.Refresh();

                        MessageBox.Show("Registration is deleted");

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Remove registration ");
                    }
                }

            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
