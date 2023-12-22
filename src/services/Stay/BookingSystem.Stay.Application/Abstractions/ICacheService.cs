namespace BookingSystem.Stay.Application.Abstractions;

public interface ICacheService
{
    Task<T?> GetOrCreateAsync<T>(string key,
        Func<CancellationToken, Task<T>> handle,
        TimeSpan? expiration,
        CancellationToken cancellationToken = default);
}
