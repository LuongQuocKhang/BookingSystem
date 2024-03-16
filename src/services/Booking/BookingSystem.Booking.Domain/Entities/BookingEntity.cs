using BookingSystem.Booking.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Booking.Domain.Entities
{
    [Table("BookingSystem", Schema = "Booking")]
    public class BookingEntity : EntityBase
    {
        public int StayId { get; set; }

        public int UserId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int NumberOfAdults { get; set; }

        public int NumberOfChildren { get; set; }

        public int NumberOfInfants { get; set; }

        public string? MessagerForAuthor { get; set; } = string.Empty;
    }
}
