using System.ComponentModel.DataAnnotations;

namespace BusTicketBookingSystem.Models.Domain
{
    public class SeatSelection
    {
        [Key]
        public int SeatSelectionID { get; set; }
        public int BookingID { get; set; }
        public int SeatID { get; set; }
        public Booking Booking { get; set; }
        public Seats Seat { get; set; }
    }
}
