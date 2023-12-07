using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace Hotel.Presentation.Customer.WindowsOrganisor
{
    /// <summary>
    /// Interaction logic for DescriptionWindow.xaml
    /// </summary>
    public partial class DescriptionWindow : Window
    {
        public DescriptionUI DescriptionUI { get; set; }
        private int duration;

        public DescriptionWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {


            if (IsFormatValid())
            {

                if (DescriptionUI == null)
                {
                    //Nieuw
                    //wegschrijven
                    DescriptionUI = new DescriptionUI(NameTextBox.Text, DescriptionTextBox.Text, duration, LocationTextBox.Text);
                    
                }

                else
                {
                    //Update
                    //update DB
                    
                    DescriptionUI.Name = NameTextBox.Text;
                    DescriptionUI.Description = DescriptionTextBox.Text;
                    DescriptionUI.Duration = duration;
                    DescriptionUI.Location = LocationTextBox.Text;

                }
                DialogResult = true;

                Close();
            }

        }
        private bool IsFormatValid()
        {
            if (!int.TryParse(DurationTextBox.Text, out duration ) )
            {
                MessageBox.Show("Duration is not a number");
                return false;
            }
            else if(duration < 0)
            {
                MessageBox.Show("Duration is not a positive number");
                return false;
            }
            else if (NameTextBox.Text == "")
            {
                MessageBox.Show("Name is empty");
                return false;
            }
            else if (DescriptionTextBox.Text == "")
            {
                MessageBox.Show("Description is empty");
                return false;
            }
            else if (LocationTextBox.Text == "")
            {
                MessageBox.Show("Location is empty");
                return false;
            }
            else
            {
                return true;
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
