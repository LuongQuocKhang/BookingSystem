using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("Host", Schema = "Stay")]
public class HostEntity : EntityBase
{
    public string? Name { get; set; } = string.Empty;
    public int TotalPlace { get; set; }
    public float ResponeRate { get; set; }
    public string? Note { get; set; }
}
