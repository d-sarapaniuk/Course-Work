namespace HotelReservationService.Models
{
    public class Booking
    {
        public int Id { get; set; }
        //public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        //public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

    }
}
