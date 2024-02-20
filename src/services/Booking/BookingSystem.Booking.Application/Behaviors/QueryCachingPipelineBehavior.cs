using MediatR;

namespace BookingSystem.Booking.Application.Behaviors;

//internal sealed class QueryCachingPipelineBehavior<TRequest, TResponse>(ICacheService cacheService)
//    : IPipelineBehavior<TRequest, TResponse?> where TRequest : ICacheQuery
//{
//    private readonly ICacheService _cacheService = cacheService;

//    public async Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse?> next, CancellationToken cancellationToken)
//    {
//        return await _cacheService.GetOrCreateAsync(request.Key,
//            _ => next(),
//            request.Expiration,
//            cancellationToken);
//    }
//}
