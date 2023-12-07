using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Description
    {
        private int id;
        private string _name;
        private int _duration;
        private string _location;
        private string _description;

        public Description(string name, string location, int duration, string descriptionText)
        {
            Name = name;
            Duration = duration;
            Location = location;
            DescriptionText = descriptionText;
        }

        public Description(int id, string name, string location, int duration, string descriptionText)
        {
            Id = id;
            Name = name;
            Location = location;
            Duration = duration;
            DescriptionText = descriptionText;
        }

        public int Duration { get => _duration; set => _duration = value; }
        public string Location { get => _location; set => _location = value; }
        public string DescriptionText { get => _description; set => _description = value; }
        public string Name { get => _name; set => _name = value; }
        public int Id { get => id; set => id = value; }
    }
}
