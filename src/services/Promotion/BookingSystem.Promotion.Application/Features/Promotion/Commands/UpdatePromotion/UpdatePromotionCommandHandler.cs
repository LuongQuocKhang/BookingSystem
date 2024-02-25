using AutoMapper;
using BookingSystem.Promotion.Application.Abstractions;
using BookingSystem.Promotion.Domain.Entities;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Commands.UpdatePromotion;

public class UpdatePromotionCommandHandler(IMapper mapper, IPromotionRepository promotionRepository)
    : IRequestHandler<UpdatePromotionCommand, bool>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository ?? throw new ArgumentNullException(nameof(promotionRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<bool> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
    {
        PromotionEntity promotion = _mapper.Map<PromotionEntity>(request);

        bool isUpdated = await _promotionRepository.UpdatePromotion(promotion, cancellationToken)
            .ConfigureAwait(false);

        return isUpdated;
    }
}
