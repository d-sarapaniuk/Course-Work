using HotelReservationService.Models;
using HotelReservationService.Data;
namespace BLL.HotelResevationService
{
    public class Service
    {
        protected Context context = new Context();

        public void Add<T>(T obj)
        {
            context.Add(obj);
            context.SaveChanges();
        }

        public void Remove<T>(T obj)
        {
            if (obj != null)
            {
                context.Remove(obj);
                context.SaveChanges();
            }
        }





    }
}
