using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusTicketBookingSystem.Models.Domain;
using BusTicketBookingSystem.Database;

namespace BusTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly Data_config _context;

        public RoutesController(Data_config context)
        {
            _context = context;
        }

        // GET: api/Routes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Routes>>> GetRoutes()
        {
            return await _context.routes.ToListAsync();
        }



        // POST: api/Routes
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<Routes>> PostRoute(Routes route)
        {
            _context.routes.Add(route);
            await _context.SaveChangesAsync();
           /* if ( ) { 
            }*/
            return CreatedAtAction("GetRoute", new { id = route.RouteID }, route);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(int id, Routes route)
        {
            if (id != route.RouteID)
            {
                return BadRequest("Route ID mismatch.");
            }

            _context.Entry(route).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(id))
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

        private bool RouteExists(int id)
        {
            return _context.routes.Any(e => e.RouteID == id);
        }


        // DELETE: api/Routes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await _context.routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            _context.routes.Remove(route);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
