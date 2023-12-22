using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayTags", Schema = "Stay")]
public class StayTagEntity : EntityBase
{
    public int StayId { get; set; }
    public string? Label { get; set; } = string.Empty;

    [ForeignKey("StayId")]
    public StayEntity? Stay { get; set; }
}
