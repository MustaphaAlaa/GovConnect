using System.Linq.Expressions;

namespace IServices;

public interface IGetByService<TResult> 
{
    Task<TResult> GetByAsync (Expression<Func<TResult, bool>> predicate);
}