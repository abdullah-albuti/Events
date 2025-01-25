namespace Events.Models
{










    public class Event
    {
        public int EventId { get; set; }
        public string image { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }

        public int PackagId { get; set; }
    }

}
