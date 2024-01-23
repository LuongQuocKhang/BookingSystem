using AutoMapper;
using BookingSystem.Promotion.Application.ViewModel;
using BookingSystem.Promotion.Domain.Entities;
using BookingSystem.Promotion.Infrastructure.Abstractions;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotionsByStay;

public class GetPromotionsByStayQueryHandler(IPromotionRepository promotionRepository, IMapper mapper)
    : IRequestHandler<GetPromotionsByStayQuery, IReadOnlyCollection<PromotionViewModel>>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository ?? throw new ArgumentNullException(nameof(promotionRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<IReadOnlyCollection<PromotionViewModel>> Handle(GetPromotionsByStayQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<PromotionEntity> promotions = await _promotionRepository.GetPromotionsByStay(request.StayId, cancellationToken)
            .ConfigureAwait(false);

        IReadOnlyCollection<PromotionViewModel> promotionViews = _mapper.Map<IReadOnlyCollection<PromotionViewModel>>(promotions);

        return promotionViews;
    }
}
