using System.Linq.Expressions;

namespace IServices;

public interface IGetAllService<TModel, TResult>
{
    public Task<List<TResult>> GetAllAsync();
    public Task<IQueryable<TResult>> GetAllAsync(Expression<Func<TModel, bool>> predicate);
}