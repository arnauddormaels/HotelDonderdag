﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.Model
{
    public class OrganisorUI
    {
        public OrganisorUI(int? id, string name, string email, string address, string phone, List<EventUI> events)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            Events = events;
        }
        public OrganisorUI(string name, string email, string address, string phone)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
        }

        public OrganisorUI(int? id, string name, string email, string address, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
        }

        public int? Id { get; set; }
        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        private string _email;
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        public string Address { get; set; }
        private string _phone;

        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(); } }
        public List<EventUI> Events { get; set; }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
