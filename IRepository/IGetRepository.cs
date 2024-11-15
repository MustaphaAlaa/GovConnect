using System.Linq.Expressions;

namespace IRepository;

public interface IGetRepository<TResult>
{
    public Task<TResult?> GetAsync (Expression<Func<TResult, bool>> predicate);
}