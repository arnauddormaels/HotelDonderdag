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
        private ObservableCollection<OrganisorUI> organisorUIs;
        public OrganisorWindow()
        {
            InitializeComponent();
            organisorManager = new OrganisorManager(RepositoryFactory.OrganisorRepository);
            organisorUIs = new ObservableCollection<OrganisorUI>(organisorManager.GetOrganisors(null).Select(x => new OrganisorUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)).ToList());
            OrganisorsDataGrid.ItemsSource = organisorUIs;
            eventsManager = new Domain.Managers.EventManager(RepositoryFactory.EventRepository);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemShowActivities_Click(object sender, RoutedEventArgs e)
        {
            //TODO 
            EventsWindow w = new EventsWindow(OrganisorsDataGrid.SelectedItem, organisorManager, eventsManager);
            w.Show();
        }

        private void MenuItemUpdateOrganisor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDeleteOrganisor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemAddOrganisor_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
