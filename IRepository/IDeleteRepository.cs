using System.Linq.Expressions;

namespace IRepository;

public interface IDeleteRepository<T>
{
    public Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);
}