using Hotel.Domain.Managers;
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

namespace Hotel.Presentation.Customer
{
    /// <summary>
    /// Interaction logic for OrganisorWindow.xaml
    /// </summary>
    public partial class OrganisorWindow : Window
    {
        private ObservableCollection<OrganisorUI> organisorUis = new ObservableCollection<OrganisorUI>();
        private OrganisorManager organisorManager;
        private Domain.Managers.EventManager eventsManager;
        public OrganisorWindow()
        {
            InitializeComponent();
            organisorManager = new OrganisorManager(RepositoryFactory.OrganisorRepository);
            organisorUIs = new ObservableCollection<CustomerUI>(organisorManager.GetCustomers(null).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone, x.GetMembers().Count)).ToList());
            OrganisorDataGrid.ItemsSource = customerUIs;
            activitiesManager = new MemberManager(RepositoryFactory.MembersRepository);
        }

    }
}
