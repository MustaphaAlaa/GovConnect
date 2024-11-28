using System.Linq.Expressions;

namespace IServices;

public interface IGeWhenService<TResult>
{
    Task<TResult> GetByAsync(Expression<Func<TResult, bool>> predicate);
}

public interface IGetWhenService<T, TResult>
{
    Task<TResult> GetByAsync(Expression<Func<T, bool>> predicate);
}

public interface IGetByService<T, TResult>
{
    Task<TResult> GetByAsync(T typeDTO);
}

