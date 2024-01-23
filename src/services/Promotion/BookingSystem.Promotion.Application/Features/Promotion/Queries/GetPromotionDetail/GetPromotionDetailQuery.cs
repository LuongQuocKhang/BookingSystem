using BookingSystem.Promotion.Application.ViewModel;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotionDetail;

public class GetPromotionDetailQuery : IRequest<PromotionViewModel>
{
    public int PromotionId { get; set; }
}
