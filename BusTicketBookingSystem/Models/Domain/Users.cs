using System.ComponentModel.DataAnnotations;

namespace BusTicketBookingSystem.Models.Domain
{
    public class Users
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
