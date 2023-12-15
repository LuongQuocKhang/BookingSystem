using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities
{
    [Table("Stays", Schema = "Stay")]
    public class Stays : EntityBase
    {
        public string Name { get; set; }
        public int NumberOfBeds { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfBaths { get; set; }
        public int NumberOfBeedrooms { get; set; }
        public string? HostedDate { get; set; }
        public string? Address { get; set; }
        public double Rating { get; set; }
        public double PricePerNight { get; set; }
        public string? StayInformation { get; set; }
        public double ServiceCharge { get; set; }
        public int HostId { get; set; }
        public string? CancellationPolicy { get; set; }
        public string? CheckInTime { get; set; }
        public string? CheckOutTime { get; set; }
        public string? SpecialNotes { get; set; }
        public string? AvatarImage { get; set; }

        public virtual ICollection<Amenity> Amenities { get; set; }
        public virtual ICollection<RoomRate> RoomRates { get; set; }
        public virtual ICollection<StayAvailability> StayAvailability { get;set; }
        public virtual ICollection<StayReview> StayReviews { get;set; }
        public virtual ICollection<StayImage> StayImages { get;set; }

        [ForeignKey("HostId")]
        public StayHostEntity HostInformation { get; set; }
    }
}
