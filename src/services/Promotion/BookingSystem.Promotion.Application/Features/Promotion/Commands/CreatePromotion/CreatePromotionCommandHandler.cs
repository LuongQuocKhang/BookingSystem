using AutoMapper;
using BookingSystem.Promotion.Application.Abstractions;
using BookingSystem.Promotion.Domain.Entities;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Commands.CreatePromotion;

public class CreatePromotionCommandHandler(IMapper mapper, IPromotionRepository promotionRepository)
    : IRequestHandler<CreatePromotionCommand, int>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository ?? throw new ArgumentNullException(nameof(promotionRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<int> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
    {
        PromotionEntity promotion = _mapper.Map<PromotionEntity>(request);

        int promotionId = await _promotionRepository.CreatePromotion(promotion, cancellationToken)
            .ConfigureAwait(false);

        return promotionId;
    }
}
