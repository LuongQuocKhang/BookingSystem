using AutoMapper;
using BookingSystem.Promotion.Application.Abstractions;
using BookingSystem.Promotion.Application.ViewModel;
using BookingSystem.Promotion.Domain.Entities;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotionDetail;

public class GetPromotionDetailQueryHandler(IPromotionRepository promotionRepository, IMapper mapper)
    : IRequestHandler<GetPromotionDetailQuery, PromotionViewModel>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository ?? throw new ArgumentNullException(nameof(promotionRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<PromotionViewModel> Handle(GetPromotionDetailQuery request, CancellationToken cancellationToken)
    {
        PromotionEntity promotion = await _promotionRepository.GetPromotionDetail(request.PromotionId, cancellationToken)
            .ConfigureAwait(false);

        PromotionViewModel promotionViews = _mapper.Map<PromotionViewModel>(promotion);

        return promotionViews;
    }
}
