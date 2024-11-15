namespace IRepository;

public interface ICreateRepository<T>
{
    public Task<T> CreateAsync(T entity);
}