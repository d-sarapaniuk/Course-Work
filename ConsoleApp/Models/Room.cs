using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationService.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int Number { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
