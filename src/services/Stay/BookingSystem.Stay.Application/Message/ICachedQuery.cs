namespace BookingSystem.Stay.Application.Message;

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICacheQuery
{
}

public interface ICacheQuery
{
    public string Key { get; }

    public TimeSpan? Expiration { get; set; }
}