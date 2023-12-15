using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;


[Table("StayShares", Schema = "Stay")]
public class StayShare : EntityBase
{
    public int StaysId { get; set; }
    public int UserId { get; set; }
}
