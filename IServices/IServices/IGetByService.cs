using System.Linq.Expressions;

namespace IServices;

public interface IGetByService<TResult>
{
    Task<TResult> GetByAsync(Expression<Func<TResult, bool>> predicate);
}

public interface IGetByService<T, TResult>
{
    Task<TResult> GetByAsync(T typeDTO);
}

