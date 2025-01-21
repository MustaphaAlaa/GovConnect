using System.Linq.Expressions;
using DataConfigurations;
using IRepository;
using Microsoft.EntityFrameworkCore;

public class GetAllRepository<Entity> : RepositoryDbContext, IGetAllRepository<Entity> where Entity : class
{
    protected DbSet<Entity> _entities;

    public GetAllRepository(GovConnectDbContext context) : base(context)
    {
        _entities = this._db.Set<Entity>();
    }

    public Task<List<Entity>> GetAllAsync()
    {
        return _entities.AsNoTracking().Select(entity => entity).ToListAsync();
    }

    public Task<IQueryable<Entity>> GetAllAsync(Expression<Func<Entity, bool>> predicate)
    {
        var queryable = _entities.AsNoTracking().Where(predicate);
        return Task.FromResult(queryable);
    }
}