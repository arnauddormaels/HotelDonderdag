﻿using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for MembersWindow.xaml
    /// </summary>
    public partial class MembersWindow : Window
    {
       
        public CustomerUI customerUI { get; set; }
        private ObservableCollection<MemberUI> memberUIs;
        public CustomerManager customerManager;
        public MemberManager memberManager;
        public MembersWindow(CustomerUI customerUI,List<MemberUI> membersUI, CustomerManager customerManager, MemberManager membersManager)
        {
            //Members worden niet afgebeeld op het scherm. De data grid wordt wel opgevuld maar wordt niet afgebeeld.
            InitializeComponent();
            this.customerUI = customerUI;
            this.memberUIs = new ObservableCollection<MemberUI>(membersUI);
            MembersDataGrid.ItemsSource= this.memberUIs;
            this.customerManager = customerManager;
            this.memberManager = membersManager;   
        }
        private void MenuItemAddMember_Click(object sender, RoutedEventArgs e)
        {

            MemberWindow w = new MemberWindow();
            if (w.ShowDialog() == true)
            {
                try
                {
                    if (customerUI.Id != null)
                    {

                        if (DateTime.TryParse(w.MemberUI.BirthDate, out DateTime birthDate))
                        {
                            if (customerManager.CheckMember((int)customerUI.Id,w.MemberUI.Name, birthDate))
                            {
                                //Een controle of de member al bestaat.
                            memberManager.AddMember((int)customerUI.Id, w.MemberUI.Name, birthDate);
                            memberUIs.Add(w.MemberUI);
                            MembersDataGrid.Items.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("Member already exist");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid birthdate format. Please enter a valid date.", "Error");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "add");
                }

            }

        }



        private void MenuItemDeleteMember_Click(object sender, RoutedEventArgs e)
        {



        }



        private void MenuItemUpdateMember_Click(object sender, RoutedEventArgs e)
        {
            if (MembersDataGrid.SelectedItem == null)
            {
                //Als geen member geselecteerd is dan wordt er een melding gegeven.
                MessageBox.Show("Select member you want update");
            }
            else
            {
            MemberWindow w = new MemberWindow((MemberUI)MembersDataGrid.SelectedItem);
            if (w.ShowDialog() == true)
            {
                try
                {
                    if (customerUI.Id != null)
                    {

                        if (DateTime.TryParse(w.MemberUI.BirthDate, out DateTime birthDate))
                        {
                                //Een controle of de member al bestaat.
                                memberManager.UpdateMember((int)customerUI.Id, MemberUI.Id, w.MemberUI.Name, birthDate);
                                memberUIs[memberUIs.IndexOf((MemberUI)MembersDataGrid.SelectedItem)] = w.MemberUI;
                                MembersDataGrid.Items.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Invalid birthdate format. Please enter a valid date.", "Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "add");
                }

            }
            }

        }
    }
}
