using System.Linq.Expressions;

namespace IRepository.IGenericRepositories;

public interface IDeleteRepository<T>
{
    public Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);
}