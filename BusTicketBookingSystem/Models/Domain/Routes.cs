

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicketBookingSystem.Models.Domain
{
    public class Routes
    {
        [Key]
        
        public int RouteID { get; set; }
        public string StartStation { get; set; }
        public string EndStation { get; set; }

    }
}
