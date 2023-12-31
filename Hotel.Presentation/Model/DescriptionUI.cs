﻿namespace Hotel.Presentation.Model
{
    public class DescriptionUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Location { get; set; }

        public DescriptionUI(string name, string description, int duration, string location)
        {
            Name = name;
            Description = description;
            Duration = duration;
            Location = location;
        }
        public DescriptionUI(int id, string name, string description, int duration, string location)
        {
            Id = id;
            Name = name;
            Description = description;
            Duration = duration;
            Location = location;
        }

        public override string ToString()
        {
            return "Name : " + Name + "\nDescription : " + Description + "\nDuration : " + Duration + "\nLocation : " + Location;
        }
    }
}