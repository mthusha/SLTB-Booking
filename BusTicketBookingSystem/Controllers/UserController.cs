using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BusTicketBookingSystem.Models.Domain;
using BusTicketBookingSystem.Database;

namespace BusTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Data_config _context;

        public UserController(Data_config context)
        {
            _context = context;
        }

        // POST: api/User/Login
        [HttpPost("Login")]
        public IActionResult Login(Users user)
        {
            var existingUser = _context.user.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (existingUser == null)
            {
                return NotFound("User not found or invalid credentials.");
            }

            // Check user role
            if (existingUser.Role == user.Role)
            {
                return Ok("Login successful!");
            }

            return Forbid("Access denied. Role mismatch.");
        }
    }
}
