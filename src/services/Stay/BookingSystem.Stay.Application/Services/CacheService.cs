using BookingSystem.Stay.Application.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace BookingSystem.Stay.Application.Services;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    private readonly IMemoryCache _memoryCache = memoryCache 
        ?? throw new ArgumentNullException(nameof(memoryCache));

    public async Task<T?> GetOrCreateAsync<T>(string key, 
        Func<CancellationToken, Task<T>> handle, 
        TimeSpan? expiration, 
        CancellationToken cancellationToken = default)
    {
        T? result = await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);

                return handle(cancellationToken);
            });

        return result;
    }
}
