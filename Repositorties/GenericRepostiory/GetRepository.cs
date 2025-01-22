using System.Linq.Expressions;
using DataConfigurations;
using IRepository.IGenericRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetRepository<TEntity> : RepositoryDbContext, IGetRepository<TEntity> where TEntity : class
{
    protected DbSet<TEntity> _entities;

    public GetRepository(GovConnectDbContext context) : base(context)
    {
        _entities = this._db.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.AsNoTracking().FirstOrDefaultAsync(predicate);
        //return _entities.FirstOrDefaultAsync(predicate);
    }

}