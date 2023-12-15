using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayImages", Schema = "Stay")]
public class StayImage : EntityBase
{
    public string Image { get; set; }
    public int StaysId { get; set; }
}
