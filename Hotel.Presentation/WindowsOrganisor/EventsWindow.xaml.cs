﻿using Hotel.Domain.Managers;
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

        private void MenuItemUpdateStatusEvent_Click(object sender, RoutedEventArgs e)
        {
            if(EventsDataGrid.SelectedItem != null)
            {
                EventUI eventUI = (EventUI)EventsDataGrid.SelectedItem;
                bool status = eventUI.Status?false:true;
                eventUI.Status = status;
                eventUI.Status = eventManager.UpdateStatusEvent(EventMapper.MapToEventModel(eventUI));
                EventsDataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select event to update the status");
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
                       w.eventUI.Id = eventManager.AddEvent((int)organisorUI.Id,EventMapper.MapToEventModel(w.eventUI));

                        eventUIs.Add(w.eventUI);
                       EventsDataGrid.Items.Refresh();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "add new event error");
                }

            }
        }
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
