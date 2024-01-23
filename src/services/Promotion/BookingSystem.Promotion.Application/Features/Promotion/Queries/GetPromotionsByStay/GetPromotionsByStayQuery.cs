using BookingSystem.Promotion.Application.ViewModel;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotionsByStay;

public class GetPromotionsByStayQuery : IRequest<IReadOnlyCollection<PromotionViewModel>>
{
    public int StayId { get; set; }
}
