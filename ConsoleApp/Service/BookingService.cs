using HotelReservationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationService.Service
{
    public class BookingService : Service
    {
        public void CreateBooking(int customerId, int roomId)
        {
            Room? room = context.Rooms.Where(r => r.Id == roomId).FirstOrDefault();
            Customer? customer = context.Customers.Where(c => c.Id == customerId).FirstOrDefault();
            if (room == null || customer == null) { return; }
            Booking booking = new Booking()
            {
                Customer = customer,
                Room = room,
            };
            room.IsAvailable = false;
            context.Add(booking);
            context.SaveChanges();
        }
        public void RemoveBooking(int bookingId)
        {
            Booking? booking = context.Bookings.Where(b => b.Id == bookingId).FirstOrDefault();
            Remove(booking);
        }
        public Dictionary<int, string> GetBookings(int hotelId)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            var bookings = context.Bookings.Where(b => b.Room.HotelId == hotelId).OrderBy(b => b.Id);
            foreach ( Booking booking in bookings )
            {
                result.Add(booking.Id, booking.Customer.FirstName + ' ' + booking.Customer.LastName);
            }
            return result;

        }
    }
}
