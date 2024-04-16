using BusTicketBookingSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusTicketBookingSystem.Database
{
    public class Data_config : DbContext
    {
        public Data_config(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Agency> agency { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Bus> bus { get; set; }
        public DbSet<Passengers> passengers { get; set; }
        public DbSet<Routes> routes { get; set; }
        public DbSet<Schedules> schedules { get; set; }
        public DbSet<Seats> seats { get; set; }
        public DbSet<SeatSelection> seatselection { get; set; }
        public DbSet<Users> user { get; set; }

        

    }
}
