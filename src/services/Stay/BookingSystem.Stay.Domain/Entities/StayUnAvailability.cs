using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayUnAvailability", Schema = "Stay")]
public class StayUnAvailability : EntityBase
{
    public int StaysId { get; set; }
    public DateTime UnAvaiableDate { get; set; }
}
