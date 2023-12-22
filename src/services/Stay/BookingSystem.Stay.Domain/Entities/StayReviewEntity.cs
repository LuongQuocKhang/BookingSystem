using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayReviews", Schema = "Stay")]
public class StayReviewEntity : EntityBase
{
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public int UserId { get; set; }
    public int StayId { get; set; }

    [ForeignKey("StayId")]
    public StayEntity? Stay { get; set; }
}
