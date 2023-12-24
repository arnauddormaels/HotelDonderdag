using Hotel.Domain.DTO;
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
using EventManager = Hotel.Domain.Managers.EventManager;

namespace Hotel.Presentation.WindowsCustomer
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public CustomerWithMemberListUI customerUI { get; set; }
        public RegistrationUI registrationUI { get; set; }
        public EventManager eventManager { get; set; }
        public RegistrationManager registrationManager { get; set; }
        public CustomerManager customerManager { get; set; }
        private ObservableCollection<EventUI> eventUIs = new ObservableCollection<EventUI>();
        private ObservableCollection<MemberUI> memberUIs = new ObservableCollection<MemberUI>();

        public RegistrationWindow(int customerId)
        {
            InitializeComponent();
            eventManager = new EventManager(RepositoryFactory.EventRepository);
            registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository, RepositoryFactory.EventRepository, RepositoryFactory.MembersRepository);
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);

            eventUIs = new ObservableCollection<EventUI>(eventManager.GetEvents().Select(@event => EventMapper.MapToEventUI(@event)).ToList());
            customerUI = CustomerMapper.MapToCustomerWithMemberListUI(customerManager.GetCustomerById(customerId));


            memberUIs = new ObservableCollection<MemberUI>(customerUI.Members);

            EventDataGrid.ItemsSource = eventUIs;
            MembersDataGrid.ItemsSource = memberUIs;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventDataGrid.SelectedItem != null && MembersDataGrid.SelectedItems.Count != 0)
            {
                EventUI eventUI = (EventUI)EventDataGrid.SelectedItem;
                List<int> memberIds = new List<int>();
                foreach (MemberUI memberUI in MembersDataGrid.SelectedItems)
                {
                    memberIds.Add(memberUI.Id);
                }
                //TODO : Wat gaat Addregistration returnen? en hoe gaan we de lijst updaten in registraties window? 
                registrationManager.AddRegistration(customerUI.Id.Value, eventUI.Id, memberIds);

                MessageBox.Show("Registration is succesfull");
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select an event and a member");
            }



        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
