using BookingSystem.Promotion.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Promotion.Domain.Entities;

[Table("StayPromotions", Schema = "BookingSystem")]
public class StayPromotionEntity : EntityBase
{
    public int PromotionId { get; set; }
    public int StayId { get; set; }

    [ForeignKey("PromotionId")]
    public PromotionEntity Promotion { get; set; }
}
