namespace HotelReservationService.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        
        public ICollection<Booking>? Bookings { get; set; }
        public override string ToString()
        {
            return FirstName + ' ' + LastName;
        }

    }
}
