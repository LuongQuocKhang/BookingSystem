using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("RoomRates", Schema = "Stay")]
public class RoomRateEntity : EntityBase
{
    public string? Name { get; set; } // Monday - Thursday, Rent by month
    public string? Value { get; set; } // $199, -8.34 %
    public int StayId { get; set; }

    [ForeignKey("StayId")]
    public StayEntity? Stay { get; set; }
}
