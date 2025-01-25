namespace Events.Models
{

    public class Booking
    {
        public int BookingId { get; set; }
        public int Event_ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }
        public DateTime BookingDate { get; set; }

      
    }

}
