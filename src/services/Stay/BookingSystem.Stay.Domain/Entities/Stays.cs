using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities
{
    [Table("Stays", Schema = "Stay")]
    public class Stays : EntityBase
    {
        public string? Name { get; set; } = string.Empty;
        public int NumberOfBeds { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string? Address { get; set; }
        public double Rating { get; set; }
        public double NumberOfRating { get; set; }
        public double PricePerNight { get; set; }
        public string? StayInformation { get; set; }
        public double ServiceCharge { get; set; }
        public int HostId { get; set; }

        public string? CancellationPolicy { get; set; }
        public string? CheckInTime { get; set; }
        public string? CheckOutTime { get; set; }

        public string? SpecialNotes { get; set; }
        public string? AvatarImage { get; set; }

        public virtual ICollection<StayAmenity>? Amenities { get; set; }

        public virtual ICollection<RoomRate>? RoomRates { get; set; }

        public virtual ICollection<StayUnAvailability>? StayUnAvailability { get;set; }

        public virtual ICollection<StayReview>? StayReviews { get;set; }

        public virtual ICollection<StayImage>? StayImages { get;set; }

        [ForeignKey("HostId")]
        public StayHostEntity? Host { get; set; }
    }
}
