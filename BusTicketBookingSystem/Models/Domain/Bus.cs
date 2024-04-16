using System.ComponentModel.DataAnnotations;

namespace BusTicketBookingSystem.Models.Domain
{
    public class Bus
    {
        [Key]
        public int BusID { get; set; }
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
      
        public List<Seats> Seats { get; set; }
    }
}