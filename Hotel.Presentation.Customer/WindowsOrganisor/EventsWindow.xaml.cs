using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.mappers;
using Hotel.Presentation.Model;
using Hotel.Presentation.WindowsOrganisor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for EventsWindow.xaml
    /// </summary>
    public partial class EventsWindow : Window
    {
        public OrganisorUI organisorUI { get; set; }
        public ObservableCollection<EventUI> eventUIs;
        public OrganisorManager OrganisorManager;
        public Domain.Managers.EventManager eventManager;

        public EventsWindow(OrganisorUI organisorUI, List<EventUI> eventUIs, OrganisorManager organisorManager, Domain.Managers.EventManager eventManager)
        {
            InitializeComponent();
            this.organisorUI = organisorUI;
            this.eventUIs = new ObservableCollection<EventUI>(eventUIs);
            EventsDataGrid.ItemsSource = this.eventUIs;
            OrganisorManager = organisorManager;
            this.eventManager = eventManager;
        }

        private void MenuItemDisableEvent_Click(object sender, RoutedEventArgs e)
        {
            if(EventsDataGrid.SelectedItem != null)
            {
                EventUI eventUI = (EventUI)EventsDataGrid.SelectedItem;
                eventUI.Status = !eventUI.Status;
                eventUI.Status = eventManager.UpdateStatusEvent(EventMapper.MapToEventModel(eventUI));
                EventsDataGrid.Items.Refresh();
            }

        }


        private void MenuItemAddEvent_Click(object sender, RoutedEventArgs e)
        {
            EventWindow w = new EventWindow(eventManager);
            if (w.ShowDialog() == true)
            {
                try
                {
                    if (organisorUI.Id != null)
                    {

                        //TODO
                        //Hier kan je nog een controle uitvoeren voor niet hetzelfde event toe te voegen.
                        //Een controle of het event al bestaat wordt niet uitgevoerd.

                        //                    memberManager.AddMember((int)customerUI.Id, w.MemberUI.Name, birthDate);
                       w.eventUI.Id = eventManager.AddEvent((int)organisorUI.Id,EventMapper.MapToEventModel(w.eventUI));

                        eventUIs.Add(w.eventUI);
                       EventsDataGrid.Items.Refresh();

                        //                    memberUIs.Add(w.MemberUI);
                        //                    MembersDataGrid.Items.Refresh();
                        //            }
                        //            else
                        //            {
                        //                MessageBox.Show("Invalid birthdate format. Please enter a valid date.", "Error");
                        //            }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "add new event error");
                }

            }
        }
        //Al gedaan
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
