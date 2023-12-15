using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayImages", Schema = "Stay")]
public class StayImage : EntityBase
{
    [Base64String]
    public string? Image { get; set; }
    public int StaysId { get; set; }
}
