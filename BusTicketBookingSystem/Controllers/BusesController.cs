using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusTicketBookingSystem.Models.Domain;
using BusTicketBookingSystem.Database;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BusTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly Data_config _context;

        public BusesController(Data_config context)
        {
            _context = context;
        }

        // GET: api/Buses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bus>>> GetBuses()
        {
            var buses = await _context.bus.Include(b => b.Seats).ToListAsync();

            // Serialize the data with ReferenceHandler.Preserve
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedBuses = JsonSerializer.Serialize(buses, options);

            // Return the serialized data
            return Content(serializedBuses, "application/json");
        }

        // PUT: api/Buses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBus(int id, Bus bus)
        {
            if (id != bus.BusID)
            {
                return BadRequest();
            }

            _context.Entry(bus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(id))
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

        // POST: api/Buses
        [HttpPost]
        public async Task<ActionResult<Bus>> PostBus(Bus bus)
        {
            _context.bus.Add(bus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBus", new { id = bus.BusID }, bus);
        }

        // DELETE: api/Buses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            var bus = await _context.bus.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            _context.bus.Remove(bus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusExists(int id)
        {
            return _context.bus.Any(e => e.BusID == id);
        }
    }
}
