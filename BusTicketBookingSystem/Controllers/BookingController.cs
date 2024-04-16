using BusTicketBookingSystem.Database;
using BusTicketBookingSystem.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Data_config _context;

        public BookingController(Data_config context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var bookings = await _context.bookings
                                           .Include(b => b.Passenger)
                                           .Include(b => b.Schedule)
                                               .ThenInclude(s => s.Route)
                                           .Include(b => b.SeatSelections)
                                           .ThenInclude(s=>s.Seat)
                                         .Include(b=>b.Schedule)
                                         .ThenInclude(s=>s.Bus)
                                           .ToListAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedBookings = JsonSerializer.Serialize(bookings, options);

            return Content(serializedBookings, "application/json");
        }

        // GET: api/Seats/Schedule/5
        [HttpGet("Schedule/{id}")]
        public async Task<ActionResult<IEnumerable<Seats>>> GetAvailableSeatsBySchedule(int id)
        {
            // Retrieve the schedule by ID
            var schedule = await _context.schedules
                                         .Include(s => s.Bus)
                                             .ThenInclude(b => b.Seats)
                                         .FirstOrDefaultAsync(s => s.ScheduleID == id);

            if (schedule == null)
            {
                return NotFound();
            }

            // List all seats
            var allSeats = schedule.Bus.Seats;

            // List booked seat IDs for the schedule
            var bookedSeatIds = await _context.seatselection
                                              .Where(ss => ss.Booking.ScheduleID == id)
                                              .Select(ss => ss.SeatID)
                                              .ToListAsync();

            // Filter available seats
            var availableSeats = allSeats.Where(s => !bookedSeatIds.Contains(s.SeatID)).ToList();

            return availableSeats;
        }


        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            // Serialize the booking data with ReferenceHandler.Preserve
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedBooking = JsonSerializer.Serialize(booking, options);

            // Return the serialized booking
            return Content(serializedBooking, "application/json");
        }


        // POST: api/Booking
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            // Retrieve the passenger based on the NIC
            var passenger = await _context.passengers.FirstOrDefaultAsync(p => p.NIC == booking.Passenger.NIC);

            if (passenger == null)
            {
                // If the passenger does not exist, return a BadRequest response
                return BadRequest("Passenger with the provided NIC does not exist.");
            }

            // Check if the total number of seats booked by the passenger exceeds five
            int totalSeatsBooked = await _context.bookings.Where(b => b.PassengerID == passenger.PassengerID)
                                                            .SumAsync(b => b.NumberOfSeats);

            if (totalSeatsBooked + booking.NumberOfSeats > 5)
            {
                // If the total exceeds five, return a BadRequest response
                return BadRequest("Maximum number of seats per passenger (based on NIC) is limited to five.");
            }

            // Check if the requested seats are available
            var availableSeats = await _context.seats
                                                .Where(s => !booking.SeatSelections.Any(ss => ss.SeatID == s.SeatID))
                                                .ToListAsync();

            if (availableSeats.Count < booking.NumberOfSeats)
            {
                // If there are not enough available seats, return a BadRequest response
                return BadRequest("Not enough available seats for the requested booking.");
            }

            // If validation passes, proceed with booking creation
            booking.PassengerID = passenger.PassengerID; // Set the PassengerID for the booking
            _context.bookings.Add(booking);

            // Update seat availability by adding seat selections
            foreach (var seat in availableSeats.Take(booking.NumberOfSeats))
            {
                booking.SeatSelections.Add(new SeatSelection { SeatID = seat.SeatID });
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.BookingID }, booking);
        }



        // PUT: api/Booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingID)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.bookings.Any(e => e.BookingID == id);
        }
    }
}
