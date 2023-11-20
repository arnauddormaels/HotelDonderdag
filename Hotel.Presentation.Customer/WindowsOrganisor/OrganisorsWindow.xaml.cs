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
        //int id, DateTime fixture, int nrOfPlaces, PriceInfoUI priceInfo, DescriptionUI description
        public OrganisorWindow()
        {
            InitializeComponent();
            organisorManager = new OrganisorManager(RepositoryFactory.OrganisorRepository);

            organisorUIs = new ObservableCollection<OrganisorUI>(organisorManager.GetOrganisors(null).Select(x => new OrganisorUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)) //TODO  
                .ToList());

            OrganisorsDataGrid.ItemsSource = organisorUIs;
            eventsManager = new Domain.Managers.EventManager(RepositoryFactory.EventRepository);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemShowActivities_Click(object sender, RoutedEventArgs e)
        {
            if (OrganisorsDataGrid.SelectedItem == null) MessageBox.Show("not selected", "show Activities");
            else
            {
                //TODO 
                OrganisorUI organisorUI = (OrganisorUI)OrganisorsDataGrid.SelectedItem;

                List<EventUI> events = eventsManager.GetEventsByOrganisorId(organisorUI.Id.Value).Select(e => {
                    PriceInfoUI priceInfoUI = new PriceInfoUI(e.PriceInfo.AdultPrice, e.PriceInfo.ChildPrice, e.PriceInfo.Discount, e.PriceInfo.AdultAge);
                    DescriptionUI descriptionUI = new DescriptionUI(e.Description.Name, e.Description.DescriptionText, e.Description.Duration, e.Description.Location);
                    return new EventUI(e.Id, e.Fixture, e.NrOfPlaces, priceInfoUI, descriptionUI); }).ToList();
                EventsWindow w = new EventsWindow((OrganisorUI)OrganisorsDataGrid.SelectedItem, events,organisorManager, eventsManager);
                w.Show();
            }
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
