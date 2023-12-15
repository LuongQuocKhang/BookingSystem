using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayHost", Schema = "Stay")]
public class StayHostEntity : EntityBase
{
    public string? Name { get; set; } = string.Empty;
    public int TotalPlace { get; set; }
    public float ResponeRate { get; set; }
    public string? Note { get; set; }
}
