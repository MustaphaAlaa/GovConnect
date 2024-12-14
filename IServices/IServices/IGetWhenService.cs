using System.Linq.Expressions;

namespace IServices;

public interface IGetWhenService<T, TResult>
{
    Task<TResult> GetByAsync(Expression<Func<T, bool>> predicate);
}