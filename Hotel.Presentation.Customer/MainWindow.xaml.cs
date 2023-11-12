using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.Customer.Model;
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

namespace Hotel.Presentation.Customer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CustomerUI> customerUIs = new ObservableCollection<CustomerUI>();
        private CustomerManager customerManager;
        private MemberManager membersManager;
        private string conn = "Data Source=LAPTOP-UMGHNHQ1\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True";
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
                    //Customer customer = new Customer(customerManager.AddCustomer(w.CustomerUI.Name, w.CustomerUI.Email, w.CustomerUI.Phone, w.CustomerUI.Address));
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
                //w.ShowDialog();
                
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
                    memberUIs = membersManager.GetMembers(customerUI.Id.Value).Select(m => new MemberUI(m.Id, m.Name, m.Birthday.ToString())).ToList(); //customerManager.GetMembersByCustomerId(customerUI.Id.Value).Select(m => new MemberUI(m.Name, m.Birthday.ToString())).ToList(); 
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


        //public void EditNumberOfMembers(CustomerUI customerUI, int value)
        //{
        //    int indexOfCustomerUI = customerUIs.IndexOf(customerUI);
        //    customerUIs[indexOfCustomerUI].NrOfMembers = customerUI.NrOfMembers + value;
        //    CustomerDataGrid.ItemsSource = customerUIs;
        //}
    }
}





//public partial class MembersWindow : Window
//{
//    public delegate void UpdateNrOfMembersDelegate(int customerId, int newNrOfMembers);
//    public UpdateNrOfMembersDelegate UpdateNrOfMembersHandler;

//    // ... Andere code ...

//    private void Window_Closed(object sender, EventArgs e)
//    {
//        // Roep de delegate aan om NrOfMembers bij te werken wanneer het venster wordt gesloten.
//        if (UpdateNrOfMembersHandler != null)
//        {
//            UpdateNrOfMembersHandler(customerUI.Id.Value, memberUIs.Count);
//        }





//        public partial class MainWindow : Window
//    {
//        private ObservableCollection<CustomerUI> customerUIs = new ObservableCollection<CustomerUI>();

//        // ... Andere code ...

//        private void MenuItemShowMembers_Click(object sender, RoutedEventArgs e)
//        {
//            if (CustomerDataGrid.SelectedItem == null)
//            {
//                MessageBox.Show("not selected", "show members");
//            }
//            else
//            {
//                CustomerUI customerUI = (CustomerUI)CustomerDataGrid.SelectedItem;
//                List<MemberUI> memberUIs;

//                if (customerUI.NrOfMembers > 0)
//                {
//                    memberUIs = customerManager.GetMembersByCustomerId(customerUI.Id.Value).Select(m => new MemberUI(m.Name, m.Birthday.ToString())).ToList();
//                }
//                else
//                {
//                    memberUIs = new List<MemberUI>();
//                }

//                MembersWindow w = new MembersWindow(customerUI, memberUIs, customerManager, membersManager);

//                // Voeg een event handler toe voor het bijwerken van NrOfMembers.
//                w.UpdateNrOfMembersHandler += UpdateNrOfMembers;
//                w.ShowDialog();
//            }
//        }

//        // Implementeer de methode om NrOfMembers bij te werken.
//        private void UpdateNrOfMembers(int customerId, int newNrOfMembers)
//        {
//            var customerUI = customerUIs.FirstOrDefault(c => c.Id == customerId);
//            if (customerUI != null)
//            {
//                customerUI.NrOfMembers = newNrOfMembers;
//            }
//        }
//    }



   