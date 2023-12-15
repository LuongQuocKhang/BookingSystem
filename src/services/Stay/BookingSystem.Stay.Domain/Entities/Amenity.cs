using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("Amenities", Schema = "Stay")]
public class Amenity : EntityBase
{
    public int AmenityId { get; set; }

    public string? Name { get; set; }

    public string? icon { get; set; }
}
