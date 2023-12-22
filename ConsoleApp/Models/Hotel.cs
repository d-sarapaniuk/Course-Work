namespace HotelReservationService.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Room> Rooms { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
