using AutoMapper;
using BookingSystem.Promotion.Application.Abstractions;
using BookingSystem.Promotion.Application.ViewModel;
using BookingSystem.Promotion.Domain.Entities;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotions;

public class GetPromotionsQueryHandler(IPromotionRepository promotionRepository, IMapper mapper) 
    : IRequestHandler<GetPromotionsQuery, IReadOnlyCollection<PromotionViewModel>>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository ?? throw new ArgumentNullException(nameof(promotionRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<IReadOnlyCollection<PromotionViewModel>> Handle(GetPromotionsQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<PromotionEntity> promotions = await _promotionRepository.GetPromotions(request.PazeIndex, 
            request.PazeSize, 
            request.OrderBy, 
            cancellationToken).ConfigureAwait(false);

        IReadOnlyCollection<PromotionViewModel> promotionViews = _mapper.Map<IReadOnlyCollection<PromotionViewModel>>(promotions);

        return promotionViews;
    }
}
