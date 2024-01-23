using BookingSystem.Promotion.Application.Dtos;
using BookingSystem.Promotion.Domain.Constant;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Promotion.Application.Features.Promotion.Commands.CreatePromotion;

public class CreatePromotionCommand : IRequest<int>
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string? Description { get; set; } = string.Empty;

    public double PercentageDiscount { get; set; }

    public double PriceDiscount { get; set; }

    [Required]
    public DateTime FromDate { get; set; }

    [Required]
    public DateTime ToDate { get; set; }

    [Required]
    public DiscountType DiscountType { get; set; }

    public ICollection<StayPromotionDto> StayPromotions { get; set; } = new List<StayPromotionDto>();
}
