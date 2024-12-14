using System.Linq.Expressions;

namespace IServices;

public interface IGeWhenService<TResult>
{
    Task<TResult> GetByAsync(Expression<Func<TResult, bool>> predicate);
}