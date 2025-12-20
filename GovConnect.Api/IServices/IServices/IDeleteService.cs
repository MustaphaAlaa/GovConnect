namespace IServices;

public interface IDeleteService<TKey>
{
    Task<bool> DeleteAsync(TKey id);
}