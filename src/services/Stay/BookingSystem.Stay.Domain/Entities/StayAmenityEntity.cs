using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayAmenities", Schema = "Stay")]
public class StayAmenityEntity : EntityBase
{
    public int AmenityId { get; set; }

    public int StayId { get; set; }

    [ForeignKey("StayId")]
    public StayEntity? Stay { get; set; }
    
    [ForeignKey("AmenityId")]
    public AmenityEntity? Amenity { get; set; }
}
