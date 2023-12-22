using HotelReservationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.HotelResevationService.Service
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
            base.Add(room);
        }


        public void RemoveRoom(int roomId)
        {
            Room? room = context.Rooms.Where(r => r.Id == roomId).FirstOrDefault();
            base.Remove(room);
        }
    }
}
