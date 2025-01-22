namespace IRepository.IGenericRepositories;

public interface IUpdateRepository<T>
{
    public Task<T> UpdateAsync(T entity);
}