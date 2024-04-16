using System.ComponentModel.DataAnnotations;

namespace BusTicketBookingSystem.Models.Domain
{
    public class Agency
    {
        [Key]
        public int PartnerID { get; set; }
        public string Name { get; set; }
        public string APIKey { get; set; }
    }
}
