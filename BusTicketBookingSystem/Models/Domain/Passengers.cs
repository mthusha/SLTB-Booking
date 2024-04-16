using System.ComponentModel.DataAnnotations;

namespace BusTicketBookingSystem.Models.Domain
{
    public class Passengers
    {
        [Key]
        public int PassengerID { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

     /*   public List<Booking> Bookings { get; set; }*/
    }
}
