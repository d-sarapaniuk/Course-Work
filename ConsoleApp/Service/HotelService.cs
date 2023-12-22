using HotelReservationService.Data;
using HotelReservationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationService.Service
{
    public class HotelService : Service
    {

        public void AddHotel(string name)
        {
            Hotel hotel = new Hotel() { Name = name };
            Add<Hotel>(hotel);
        }
        public void RemoveHotel(int hotelId)
        {
            Hotel? hotel = context.Hotels.Where(h => h.Id == hotelId).FirstOrDefault();
            Remove<Hotel>(hotel);
        }
        public Hotel getById(int hotelId)
        {
            Hotel? hotel = context.Hotels.Where(h => h.Id == hotelId).FirstOrDefault();
            return hotel;
        }
        public Dictionary<int, string> getHotels(bool byId = true)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            IOrderedQueryable<Hotel> hotels;
            if (byId)
            {
                hotels = context.Hotels.OrderBy(h => h.Id);
            }
            else
            {
                hotels = context.Hotels.OrderBy(h => h.Name);
            }

            foreach (Hotel h in hotels)
            {
                dict.Add(h.Id, h.Name);
            }
            return dict;
        }
        public Dictionary<int, int> getRooms(int hotelId, bool available = true)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            HotelService hotelService = new HotelService();
            Hotel hotel = hotelService.getById(hotelId);
            var query = context.Rooms.Where(r => r.HotelId == hotelId && r.IsAvailable);
            if (available)
            {
                
                foreach (Room room in query)
                {
                    dict.Add(room.Id, room.Number);
                }
            }
            else
            {
                foreach (Room room in query)
                {
                    dict.Add(room.Id, room.Number);
                }
            }
            return dict;
        }
    }
}
