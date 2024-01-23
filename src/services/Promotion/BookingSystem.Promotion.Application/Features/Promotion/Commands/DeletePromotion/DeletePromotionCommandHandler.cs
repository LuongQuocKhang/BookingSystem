using AutoMapper;
using BookingSystem.Promotion.Infrastructure.Abstractions;
using MediatR;

namespace BookingSystem.Promotion.Application.Features.Promotion.Commands.DeletePromotion;

public class DeletePromotionCommandHandler(IMapper mapper, IPromotionRepository promotionRepository)
    : IRequestHandler<DeletePromotionCommand, bool>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository ?? throw new ArgumentNullException(nameof(promotionRepository));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<bool> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await _promotionRepository.DeletePromotion(request.PromotionId, cancellationToken)
            .ConfigureAwait(false);
        return isDeleted;
    }
}
