using BookingSystem.Promotion.Application.Constant;
using BookingSystem.Promotion.Application.ViewModel;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotions;

public class GetPromotionsQuery : IRequest<IReadOnlyCollection<PromotionViewModel>>
{
    public int PazeIndex { get; set; }

    public int PazeSize { get; set; }

    public OrderBy OrderBy { get; set; }
}
