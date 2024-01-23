using BookingSystem.Promotion.Domain.Common;
using BookingSystem.Promotion.Domain.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Promotion.Domain.Entities;

[Table("Promotions", Schema = "BookingSystem")]
public class PromotionEntity : EntityBase
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public double PercentageDiscount { get; set; }

    public double PriceDiscount { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public DiscountType DiscountType { get; set; }

    public ICollection<StayPromotionEntity> StayPromotions { get; set; } = new List<StayPromotionEntity>();
}
