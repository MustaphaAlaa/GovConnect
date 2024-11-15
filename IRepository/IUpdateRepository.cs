namespace IRepository;

public interface IUpdateRepository<T>
{
    public Task<T>  UpdateAsync (T entity);
}