using BusTicketBookingSystem.Database;
using BusTicketBookingSystem.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicketBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassengersController : ControllerBase
    {
        private readonly Data_config _context;

        public PassengersController(Data_config context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passengers>>> GetPassengers()
        {
            return await _context.passengers.ToListAsync();
        }

        [HttpGet("{PassengerID}")]
        public async Task<ActionResult<Passengers>> GetPassenger(int PassengerID)
        {
            var passenger = await _context.passengers.FindAsync(PassengerID);

            if (passenger == null)
            {
                return NotFound();
            }

            return passenger;
        }

        [HttpPost]
        public async Task<ActionResult<Passengers>> PostPassenger(Passengers passenger)
        {
            _context.passengers.Add(passenger);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPassenger), new { id = passenger.PassengerID }, passenger);
        }

        [HttpPut("{PassengerID}")]
        public async Task<IActionResult> PutPassenger(int PassengerID, Passengers passenger)
        {
            if (PassengerID != passenger.PassengerID)
            {
                return BadRequest();
            }

            _context.Entry(passenger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerExists(PassengerID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{PassengerID}")]
        public async Task<IActionResult> DeletePassenger(int PassengerID)
        {
            var passenger = await _context.passengers.FindAsync(PassengerID);
            if (passenger == null)
            {
                return NotFound();
            }

            _context.passengers.Remove(passenger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassengerExists(int PassengerID)
        {
            return _context.passengers.Any(e => e.PassengerID == PassengerID);
        }
    }
}
