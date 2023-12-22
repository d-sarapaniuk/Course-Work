using HotelReservationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationService.Service
{
    public class RoomService : Service
    {
        public void AddRoom(int hotelId, int number, decimal price)
        {
            Room room = new Room()
            {
                HotelId = hotelId,
                Number = number,
                Price = price
            };
            Add(room);
        }


        public void RemoveRoom(int roomId)
        {
            Room? room = context.Rooms.Where(r => r.Id == roomId).FirstOrDefault();
            Remove(room);
        }
    }
}
