using System.ComponentModel.DataAnnotations;

namespace BusTicketBookingSystem.Models.Domain
{
    public class Seats
    {
        [Key]
        public int SeatID { get; set; }
        public int BusID { get; set; }
       /* public Bus Bus { get; set; }*/
    }
}
