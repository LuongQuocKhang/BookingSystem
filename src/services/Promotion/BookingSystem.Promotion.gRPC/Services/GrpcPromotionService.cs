using BookingSystem.Promotion.gRPC.Protos;
using BookingSystem.Promotion.Infrastructure.Abstractions;

namespace BookingSystem.Promotion.gRPC.Services
{
    public class GrpcPromotionService : PromotionService.PromotionServiceBase
    {
        private readonly IPromotionRepository _promotionRepository;

        public Task<PromotionViewModel> GetPromotionsByStay()
        {
            return null;
        }
    }
}
