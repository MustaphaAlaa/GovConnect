namespace IServices;

public interface IRetrieveByTypeService<T, TResult>
{
    Task<TResult> GetByAsync(T typeDTO);
}