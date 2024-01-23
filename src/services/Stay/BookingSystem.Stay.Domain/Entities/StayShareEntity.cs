using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;


[Table("StayShares", Schema = "Stay")]
public class StayShareEntity : EntityBase
{
    public int UserId { get; set; }
    public int StayId { get; set; }

    [ForeignKey("StayId")]
    public StayEntity? Stay { get; set; }
}
