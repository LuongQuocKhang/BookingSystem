using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Commands.DeletePromotion;

public class DeletePromotionCommand : IRequest<bool>
{
    public int PromotionId { get; set; }
}
