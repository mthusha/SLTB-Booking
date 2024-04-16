using BusTicketBookingSystem.Database;
using BusTicketBookingSystem.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly Data_config _context;

        public AgencyController(Data_config context)
        {
            _context = context;
        }


        // GET: api/Agency/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agency>> GetAgency(int id)
        {
            var agency = await _context.agency.FindAsync(id);

            if (agency == null)
            {
                return NotFound();
            }

            return agency;
        }

        // POST: api/Agency
        [HttpPost]
        public async Task<ActionResult<Agency>> PostAgency(Agency agency)
        {
            _context.agency.Add(agency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgency", new { id = agency.PartnerID }, agency);
        }

        // PUT: api/Agency/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgency(int id, Agency agency)
        {
            if (id != agency.PartnerID)
            {
                return BadRequest();
            }

            _context.Entry(agency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyExists(id))
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

        // DELETE: api/Agency/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgency(int id)
        {
            var agency = await _context.agency.FindAsync(id);
            if (agency == null)
            {
                return NotFound();
            }

            _context.agency.Remove(agency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgencyExists(int id)
        {
            return _context.agency.Any(e => e.PartnerID == id);
        }
    }
}
