using BookingSystem.Promotion.Application.ViewModel;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotions;

public class GetPromotionsQuery : IRequest<IReadOnlyCollection<PromotionViewModel>>
{
}
