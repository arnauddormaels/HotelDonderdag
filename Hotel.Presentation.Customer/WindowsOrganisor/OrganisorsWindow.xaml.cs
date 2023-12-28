using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.mappers;
using Hotel.Presentation.Model;
using Hotel.Presentation.WindowsOrganisor;
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

namespace Hotel.Presentation
{
    /// <summary>
    /// Interaction logic for OrganisorWindow.xaml
    /// </summary>
    public partial class OrganisorsWindow : Window
    {
        private ObservableCollection<OrganisorUI> organisorUis = new ObservableCollection<OrganisorUI>();
        private OrganisorManager organisorManager;
        private Domain.Managers.EventManager eventsManager;
        
        public OrganisorsWindow()
        {
            InitializeComponent();
            organisorManager = new OrganisorManager(RepositoryFactory.OrganisorRepository);

            organisorUis = new ObservableCollection<OrganisorUI>(organisorManager.GetOrganisors(null).Select(x => new OrganisorUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone)) //TODO  
                .ToList());

            OrganisorsDataGrid.ItemsSource = organisorUis;
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
                List<EventUI> events = eventsManager.GetEventsByOrganisorId(organisorUI.Id.Value).Select(@event => EventMapper.MapToEventUI(@event)).ToList();
                /*List<EventUI> events = eventsManager.GetEventsByOrganisorId(organisorUI.Id.Value).Select(@event => {
                    PriceInfoUI priceInfoUI = new PriceInfoUI(@event.PriceInfo.AdultPrice, @event.PriceInfo.ChildPrice, @event.PriceInfo.Discount, @event.PriceInfo.AdultAge);
                    DescriptionUI descriptionUI = new DescriptionUI(@event.Description.Name, @event.Description.DescriptionText, @event.Description.Duration, @event.Description.Location);
                return new EventUI(@event.Id, @event.Fixture, @event.NrOfPlaces, priceInfoUI, descriptionUI); }).ToList();
                */
                EventsWindow w = new EventsWindow((OrganisorUI)OrganisorsDataGrid.SelectedItem, events,organisorManager, eventsManager);
                w.Show();
            }
        }

        private void MenuItemUpdateOrganisor_Click(object sender, RoutedEventArgs e)
        {
            if (OrganisorsDataGrid.SelectedItem == null) MessageBox.Show("not selected", "update");
            else
            {
                OrganisorWindow w = new OrganisorWindow((OrganisorUI)OrganisorsDataGrid.SelectedItem);
                if (w.ShowDialog() == true)
                {
                    organisorManager.UpdateOrganisor(w.OrganisorUI.Id.Value, w.OrganisorUI.Name, w.OrganisorUI.Email, w.OrganisorUI.Phone, w.OrganisorUI.Address);
                    organisorUis[organisorUis.IndexOf((OrganisorUI)OrganisorsDataGrid.SelectedItem)] = w.OrganisorUI;
                    OrganisorsDataGrid.Items.Refresh();
                    MessageBox.Show("Organisor updated");

                }

            }
        }

        private void MenuItemDeleteOrganisor_Click(object sender, RoutedEventArgs e)
        {
            if (OrganisorsDataGrid.SelectedItem == null) MessageBox.Show("not selected", "delete");
            else
            {
                organisorManager.DeleteOrganisor(((OrganisorUI)OrganisorsDataGrid.SelectedItem).Id.Value);
                organisorUis.Remove((OrganisorUI)OrganisorsDataGrid.SelectedItem);
            }
        }

        private void MenuItemAddOrganisor_Click(object sender, RoutedEventArgs e)
        {
            OrganisorWindow w = new OrganisorWindow(null);
            OrganisorUI organisorUI;
            if (w.ShowDialog() == true)
            {
                try
                {
                    w.OrganisorUI.Id = organisorManager.AddOrganisor(w.OrganisorUI.Name, w.OrganisorUI.Email, w.OrganisorUI.Phone, w.OrganisorUI.Address).Id;
                    organisorUis.Add(w.OrganisorUI);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "add");
                }
                MessageBox.Show("New organisor added");
                OrganisorsDataGrid.Items.Refresh();
            }
        }
    }
}



