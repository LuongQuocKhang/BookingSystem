using AutoMapper;
using BookingSystem.Promotion.Domain.Entities;
using BookingSystem.Promotion.gRPC.Protos;
using BookingSystem.Promotion.Infrastructure.Abstractions;
using Grpc.Core;

namespace BookingSystem.Promotion.gRPC.Services;
public class PromotionProtoService(IPromotionRepository promotionRepository, IMapper mapper) 
    : PromotionService.PromotionServiceBase
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository ?? throw new ArgumentNullException(nameof(promotionRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public override async Task<GetPromotionResponse> GetPromotions(GetPromotionsQuery request, ServerCallContext context)
    {
        GetPromotionResponse response = new();

        IReadOnlyCollection<PromotionEntity> promotions = await _promotionRepository.GetPromotionsByStay(request.StayId, CancellationToken.None)
            .ConfigureAwait(false);

        if(promotions.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount for stay: {request.StayId} not found."));
        }

        IReadOnlyCollection<PromotionViewModel> promotionViews = _mapper.Map<IReadOnlyCollection<PromotionViewModel>>(promotions);

        response.StayPromotions.AddRange(promotionViews);

        return response;
    }
}
