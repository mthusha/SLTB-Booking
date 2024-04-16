using System.ComponentModel.DataAnnotations;

namespace BusTicketBookingSystem.Models.Domain
{
    public class Schedules
    {
        [Key]
        public int ScheduleID { get; set; }
        public int RouteID { get; set; }
        public int BusID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public Routes Route { get; set; }
        public Bus Bus { get; set; }
    }
}
