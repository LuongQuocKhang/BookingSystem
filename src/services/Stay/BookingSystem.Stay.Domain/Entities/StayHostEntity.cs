using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayHost", Schema = "Stay")]
public class StayHostEntity : EntityBase
{
    public string Name { get; set; }
}
