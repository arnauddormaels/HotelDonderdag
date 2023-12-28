using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.mappers;
using Hotel.Presentation.Model;
using Hotel.Presentation.WindowsCustomer;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CustomerUI> customerUIs = new ObservableCollection<CustomerUI>();
        private CustomerManager customerManager;
        private MemberManager membersManager;
        public MainWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            customerUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone, x.GetMembers().Count)).ToList());
            CustomerDataGrid.ItemsSource = customerUIs;
            membersManager = new MemberManager(RepositoryFactory.MembersRepository);
           
            
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            customerUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(SearchTextBox.Text).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone, x.GetMembers().Count)).ToList());
            CustomerDataGrid.ItemsSource = customerUIs;
        
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow w = new CustomerWindow(null);
            CustomerUI customerUI;
            if (w.ShowDialog() == true)
            {
                try
                {
                   w.CustomerUI.Id = customerManager.AddCustomer( w.CustomerUI.Name, w.CustomerUI.Email, w.CustomerUI.Phone, w.CustomerUI.Address).Id;
                    customerUIs.Add(w.CustomerUI);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "add");
                }
                CustomerDataGrid.Items.Refresh();
            }

        }
        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "delete");
            else
            {
                customerManager.DeleteCustomer(((CustomerUI)CustomerDataGrid.SelectedItem).Id.Value);
                customerUIs.Remove((CustomerUI)CustomerDataGrid.SelectedItem);
            }
        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "update");
            else
            {
                CustomerWindow w = new CustomerWindow((CustomerUI)CustomerDataGrid.SelectedItem);
                if (w.ShowDialog() == true)
                {
                    customerManager.UpdateCustomer(w.CustomerUI.Id.Value, w.CustomerUI.Name, w.CustomerUI.Email, w.CustomerUI.Phone, w.CustomerUI.Address);
                    customerUIs[customerUIs.IndexOf((CustomerUI)CustomerDataGrid.SelectedItem)] = w.CustomerUI;
                    CustomerDataGrid.Items.Refresh();
                }
                
            }
        }

        private void MenuItemShowMembers_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "show members");
            else
            {

                CustomerUI customerUI = (CustomerUI)CustomerDataGrid.SelectedItem;
                List<MemberUI> memberUIs;

                if (customerUI.NrOfMembers > 0)
                {
                    //Als member bestaat dan wordt er een lijst meegegeven aan MembersWindow
                    memberUIs = membersManager.GetMembers(customerUI.Id.Value).Select(m => MemberMapper.MapToMemberUI(m)).ToList(); 
                }
                else
                {
                    //Lege lijst meegeven aan MembersWindow
                    memberUIs = new List<MemberUI>();
                }
                MembersWindow w = new MembersWindow(customerUI, memberUIs, customerManager, membersManager);

                w.ShowDialog();
                UpdateView(customerUI, w.memberUIs.ToList()); //Geven we mee om de verandering te krijgen.
            }
        }

        private void UpdateView(CustomerUI customerUI, List<MemberUI> memberUIs)
        {
            //Update van onze DataGrid
            customerUI.NrOfMembers = memberUIs.Count;
            customerUIs[customerUIs.IndexOf((CustomerUI)CustomerDataGrid.SelectedItem)] = customerUI;
            CustomerDataGrid.ItemsSource = customerUIs;
            CustomerDataGrid.Items.Refresh();
        }

        private void MenuItemShowRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "Show Registrtion");
            else
            {

                CustomerUI customerUI = (CustomerUI)CustomerDataGrid.SelectedItem;
                List<MemberUI> memberUIs;

                if (customerUI.NrOfMembers > 0)
                {
                    //Als member bestaat dan wordt er een lijst meegegeven aan MembersWindow
                    memberUIs = membersManager.GetMembers(customerUI.Id.Value).Select(m => new MemberUI(m.Id, m.Name, m.Birthday.ToString())).ToList();
                    RegistrationsWindow w = new RegistrationsWindow(customerUI.Id.Value);

                w.ShowDialog();
                }
                else
                {
                    //Lege lijst meegeven aan MembersWindow
                    MessageBox.Show("You don't have any members, please create a member first.");
                }

            }
        }
    }
}
