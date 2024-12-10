using System.Linq.Expressions;
using DataConfigurations;
using IRepository;
using Microsoft.EntityFrameworkCore;

public class DeleteRepository<T>  :  RepositoryDbContext,  IDeleteRepository<T> where T : class
{
    protected DbSet<T> _entity;
    
    public DeleteRepository(GovConnectDbContext context) : base(context)
    {
        _entity = this._db.Set<T>();
    }

    public async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var entity = _entity.AsNoTracking().FirstOrDefault(predicate);

        if (entity == null)
            return 0;

        _entity.Remove(entity);
        return await this.SaveChangesAsync();
    }
}