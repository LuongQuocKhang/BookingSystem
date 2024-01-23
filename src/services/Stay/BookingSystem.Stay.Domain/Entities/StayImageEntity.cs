using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayImages", Schema = "Stay")]
public class StayImageEntity : EntityBase
{
    public string? Image { get; set; }
    public int StayId { get; set; }

    [ForeignKey("StayId")]
    public StayEntity? Stay { get; set; }
}
