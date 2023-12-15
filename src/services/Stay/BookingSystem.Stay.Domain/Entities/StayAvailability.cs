using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayAvailability", Schema = "Stay")]
public class StayAvailability : EntityBase
{
    public int StaysId { get; set; }
}
