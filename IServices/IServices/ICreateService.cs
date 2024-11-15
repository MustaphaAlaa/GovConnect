namespace IServices;

public interface ICreateService<T, TResult>
{
    public Task<TResult> CreateAsync(T entity);
}