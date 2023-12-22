using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayUnAvailability", Schema = "Stay")]
public class StayUnAvailabilityEntity : EntityBase
{
    public DateTime UnAvaiableDate { get; set; }
    public int StayId { get; set; }

    [ForeignKey("StayId")]
    public StayEntity? Stay { get; set; }
}
