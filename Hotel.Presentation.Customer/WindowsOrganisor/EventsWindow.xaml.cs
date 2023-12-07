using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
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
            this.eventUIs = new ObservableCollection <EventUI>(eventUIs);
            EventsDataGrid.ItemsSource = this.eventUIs;
            OrganisorManager = organisorManager;
            this.eventManager = eventManager;
        }

        private void MenuItemUpdateEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDeleteEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemAddEvent_Click(object sender, RoutedEventArgs e)
        {
            //MemberWindow w = new MemberWindow();
            //if (w.ShowDialog() == true)
            //{
            //    try
            //    {
            //        if (organisorUI.Id != null)
            //        {

            //            if (DateTime.TryParse(w.MemberUI.BirthDate, out DateTime birthDate))
            //            {
            //                if (customerManager.CheckMember((int)customerUI.Id, w.MemberUI.Name, birthDate))
            //                {
            //                    //Een controle of de member al bestaat.
            //                    memberManager.AddMember((int)customerUI.Id, w.MemberUI.Name, birthDate);
            //                    memberUIs.Add(w.MemberUI);
            //                    MembersDataGrid.Items.Refresh();

            //                }
            //                else
            //                {
            //                    MessageBox.Show("Member already exist");
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("Invalid birthdate format. Please enter a valid date.", "Error");
            //            }

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "add");
            //    }

            //}
        }
        //Al gedaan
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
