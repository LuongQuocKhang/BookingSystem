using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("Amenities", Schema = "Stay")]
public class AmenityEntity : EntityBase
{
    public string? Name { get; set; }

    public string? Icon { get; set; }
}
