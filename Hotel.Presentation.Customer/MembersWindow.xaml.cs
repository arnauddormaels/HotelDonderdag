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
    /// Interaction logic for MembersWindow.xaml
    /// </summary>
    public partial class MembersWindow : Window
    {
       
        public CustomerUI customerUI { get; set; }
        private ObservableCollection<MemberUI> memberUIs;
        public MembersWindow(CustomerUI customerUI,List<MemberUI> membersUI)
        {
            //Members worden niet afgebeeld op het scherm. De data grid wordt wel opgevuld maar wordt niet afgebeeld.
            InitializeComponent();
            this.customerUI = customerUI;
            this.memberUIs = new ObservableCollection<MemberUI>(membersUI);
            MembersDataGrid.ItemsSource= this.memberUIs;
            

        }
        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {



        }



        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {



        }



        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {



        }
    }
}
