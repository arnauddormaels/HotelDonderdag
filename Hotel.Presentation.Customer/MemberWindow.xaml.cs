using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        public MemberUI MemberUI { get; set; }
        public MemberWindow()
        {
            InitializeComponent();
        }

        public MemberWindow(MemberUI memberUI)
        {
            MemberUI = memberUI;
            InitializeComponent();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFormatValid())
            {

            if (MemberUI == null)
            {
                //Nieuw
                //wegschrijven
                //TODO nrofmembers

                string name = NameTextBox.Text;
                string birthDate = BirthDayTextBox.Text;
                MemberUI = new MemberUI(name,birthDate);


            }

            else
            {
                //Update
                //update DB
                MemberUI.Name = NameTextBox.Text;
                MemberUI.BirthDate = BirthDayTextBox.Text;
                

            }
            MessageBox.Show("Member has been succesfully added!");
            DialogResult = true;

            Close();
            }
        }

        private bool IsFormatValid()
        {
           return IsNameFormatCorrect() && IsBirthDayFormatCorrect();
            
        }

        private bool IsNameFormatCorrect()
        {

            if (string.IsNullOrEmpty(NameTextBox.Text) && string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
              MessageBox.Show("Please enter your name");
              return false;
            }
            return true;
        }

        private bool IsBirthDayFormatCorrect()
        {
            // Definieer een reguliere expressie voor het formaat "dd/mm/yyyy"
            string pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$";

            // Controleer of de invoer overeenkomt met het patroon
            if (Regex.IsMatch(BirthDayTextBox.Text, pattern))
            {
                if (DateTime.TryParseExact(BirthDayTextBox.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    // De invoer heeft het juiste formaat en is een geldige datum
                    return true;
                }

                // Hier kun je extra logica toevoegen als de invoer geldig is
            }
            
            MessageBox.Show("Please enter your birthday in correct format(dd/mm/yyyy)");
            return false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
