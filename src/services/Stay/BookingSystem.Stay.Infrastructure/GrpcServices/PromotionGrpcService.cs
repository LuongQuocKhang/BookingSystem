using BookingSystem.Promotion.gRPC.Protos;
using BookingSystem.Stay.Infrastructure.Abstractions;

namespace BookingSystem.Stay.Infrastructure.GrpcServices;

public class PromotionGrpcService(PromotionService.PromotionServiceClient promotionProtoService) : IPromotionGrpcService
{
    private readonly PromotionService.PromotionServiceClient _promotionProtoService = promotionProtoService ?? throw new ArgumentNullException(nameof(promotionProtoService));

    public async Task<List<PromotionViewModel>> GetPromotions(int stayId)
    {
        GetPromotionsQuery getPromotionRequest = new()
        {
            StayId = stayId
        };

        GetPromotionResponse response = await _promotionProtoService.GetPromotionsAsync(getPromotionRequest)
            .ConfigureAwait(false);

        if (response == null) return [];

        return [.. response.StayPromotions];
    }
}
