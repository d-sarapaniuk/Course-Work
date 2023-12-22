using HotelReservationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.HotelResevationService.Service
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
    }
}
