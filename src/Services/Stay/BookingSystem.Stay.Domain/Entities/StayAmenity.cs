using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayAmenities", Schema = "Stay")]
public class StayAmenity : EntityBase
{
    public int StaysId { get; set; }
}
