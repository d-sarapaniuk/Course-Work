using Microsoft.EntityFrameworkCore;
using HotelReservationService.Models;

namespace HotelReservationService.Data
{
    public class Context : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\LocalDB;Initial Catalog=HotelReservationService;Integrated Security=True;");
        }
    }
}
