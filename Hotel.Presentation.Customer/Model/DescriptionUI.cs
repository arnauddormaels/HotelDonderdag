namespace Hotel.Presentation.Customer.Model
{
    public class DescriptionUI
    {
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

    }
}