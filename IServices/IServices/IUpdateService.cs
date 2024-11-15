using System.Linq.Expressions;

namespace IServices;

public interface IUpdateService<TResult,TUpdate>
{
    Task<TResult> UpdateAsync(TUpdate updateRequest);
}