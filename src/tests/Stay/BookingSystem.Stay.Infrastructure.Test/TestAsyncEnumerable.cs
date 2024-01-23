using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace BookingSystem.Stay.Infrastructure.Test;

public class TestAsyncEnumerable<T> : IAsyncEnumerable<T>, IQueryable<T>
{
    private readonly IQueryable<T> _queryable;

    public TestAsyncEnumerable(IQueryable<T> queryable)
    {
        _queryable = queryable;
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new TestAsyncEnumerator<T>(_queryable.GetEnumerator());
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _queryable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Type ElementType => _queryable.ElementType;
    public Expression Expression => _queryable.Expression;
    public IQueryProvider Provider => new TestAsyncQueryProvider<T>(_queryable.Provider);
}

public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _enumerator;

    public TestAsyncEnumerator(IEnumerator<T> enumerator)
    {
        _enumerator = enumerator;
    }

    public T Current => _enumerator.Current;

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_enumerator.MoveNext());
    }

    public ValueTask DisposeAsync()
    {
        _enumerator.Dispose();
        return new ValueTask();
    }
}

public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    internal TestAsyncQueryProvider(IQueryProvider inner)
    {
        _inner = inner;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        return new TestAsyncEnumerable<TEntity>(_inner.CreateQuery<TEntity>(expression));
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new TestAsyncEnumerable<TElement>(_inner.CreateQuery<TElement>(expression));
    }

    public object Execute(Expression expression)
    {
        return _inner.Execute(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return _inner.Execute<TResult>(expression);
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        var resultType = typeof(TResult).GetGenericArguments()[0];
        var executeAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
            .GetMethod(nameof(ExecuteAsync))
            .MakeGenericMethod(resultType);

        return (TResult)executeAsyncMethod.Invoke(null, new object[] { _inner, expression, cancellationToken });
    }

    public TResult LastOrDefaultAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        var resultType = typeof(TResult).GetGenericArguments()[0];
        var executeAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
            .GetMethod(nameof(EntityFrameworkQueryableExtensions.LastOrDefaultAsync))
            .MakeGenericMethod(resultType);

        return (TResult)executeAsyncMethod.Invoke(null, new object[] { _inner, expression, cancellationToken });
    }
}