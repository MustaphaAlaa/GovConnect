using System.Linq.Expressions;

namespace IServices;

public interface IUpdateService<TUpdate,TResult>
{
    Task<TResult> UpdateAsync(TUpdate updateRequest); 
}


