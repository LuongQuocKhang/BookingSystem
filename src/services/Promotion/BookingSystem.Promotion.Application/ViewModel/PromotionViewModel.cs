using BookingSystem.Promotion.Domain.Constant;

namespace BookingSystem.Promotion.Application.ViewModel;

public class PromotionViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public double PercentageDiscount { get; set; }

    public double PriceDiscount { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public DiscountType DiscountType { get; set; }

    public ICollection<StayPromotionViewModel> StayPromotions { get; set; } = new List<StayPromotionViewModel>();
}
