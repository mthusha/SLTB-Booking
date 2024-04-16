using System.ComponentModel.DataAnnotations;

namespace BusTicketBookingSystem.Models.Domain
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        public int PassengerID { get; set; }
        public int ScheduleID { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfSeats { get; set; }
        public Passengers Passenger { get; set; }
        public Schedules Schedule { get; set; }

        public List<SeatSelection> SeatSelections { get; set; }
    }
}
