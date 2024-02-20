using BookingSystem.Promotion.gRPC.Protos;

namespace BookingSystem.Stay.Infrastructure.Abstractions;

public interface IPromotionGrpcService
{
    Task<List<PromotionViewModel>> GetPromotions(int stayId);
}
