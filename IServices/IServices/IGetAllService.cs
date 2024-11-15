using System.Linq.Expressions;

namespace IServices;

public interface IGetAllService<TResult,TModel>
{
    Task<List<TResult>> GetAllAsync();
    Task<IQueryable<TResult>> GetAllAsync (Expression<Func<TModel, bool>> predicate);
}