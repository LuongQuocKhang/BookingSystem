using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities;

[Table("StayReviews", Schema = "Stay")]
public class StayReview : EntityBase
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public int StaysId { get; set; }
    public int UserId { get; set; }
}
