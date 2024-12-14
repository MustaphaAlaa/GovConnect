namespace IServices;

public interface IGetByService<T, TResult>
{
    Task<TResult> GetByAsync(T typeDTO);
}