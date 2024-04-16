using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTicketBookingSystem.Models.Domain;
using BusTicketBookingSystem.Database;

namespace BusTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly Data_config _context;

        public SchedulesController(Data_config context)
        {
            _context = context;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedules>>> GetSchedules()
        {
            var schedules = await _context.schedules
                                           .Include(s => s.Route)
                                           .Include(s => s.Bus)
                                           .ThenInclude(b => b.Seats)

                                           .ToListAsync();
            return schedules;
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedules>> GetSchedule(int id)
        {
            var schedule = await _context.schedules.FindAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }

        // GET: api/Schedules/StartStation/{startStation}
        [HttpGet("StartStation/{startStation}")]
        public async Task<ActionResult<IEnumerable<Schedules>>> GetScheduleByStartStation(string startStation)
        {
            var schedules = await _context.schedules
                                            .Include(s => s.Route)
                                            .Include(s => s.Bus)
                                            .ThenInclude(b=> b.Seats)
                                         
                                            .Where(s => s.Route.StartStation == startStation)
                                            .ToListAsync();

            if (schedules == null || !schedules.Any())
            {
                return NotFound();
            }

            return schedules;
        }


        // PUT: api/Schedules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedules schedule)
        {
            if (id != schedule.ScheduleID)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
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

        // POST: api/Schedules
        [HttpPost]
        public async Task<ActionResult<Schedules>> PostSchedule(Schedules schedule)
        {
            _context.schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.ScheduleID }, schedule);
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(int id)
        {
            return _context.schedules.Any(e => e.ScheduleID == id);
        }
    }
}
