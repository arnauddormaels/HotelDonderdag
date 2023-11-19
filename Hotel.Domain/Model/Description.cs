using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Description
    {
        private string _name;
        private int _duration;
        private string _location;
        private string _description;

        public Description(string name, int duration, string location, string descriptionText)
        {
            Name = name;
            Duration = duration;
            Location = location;
            DescriptionText = descriptionText;
        }

        public int Duration { get => _duration; set => _duration = value; }
        public string Location { get => _location; set => _location = value; }
        public string DescriptionText { get => _description; set => _description = value; }
        public string Name { get => _name; set => _name = value; }
    }
}
