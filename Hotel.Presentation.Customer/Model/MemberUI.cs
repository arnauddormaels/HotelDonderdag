using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.Model
{
    public class MemberUI : INotifyPropertyChanged
    {

        public MemberUI(string name, string birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }
        public MemberUI(int id,string name, string birthDate)
        {
            _id = id;
            Name = name;
            BirthDate = birthDate;
            
        }
        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        private string _birthDate; 
        public string BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged(); } }
        
        private int _id;
        //public int Id { get { return _id; } set { _id = value; OnPropertyChanged(); } }



        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
