using System.Linq.Expressions;
using DataConfigurations;
using IRepository;
using Microsoft.EntityFrameworkCore;

public class GetRepository<Entity> : RepositoryDbContext, IGetRepository<Entity> where Entity : class
{
    protected DbSet<Entity> _entities;

    public GetRepository(DVLDDbContext context) : base(context)
    {
        _entities = this._db.Set<Entity>();
    }

    public Task<Entity?> GetAsync(Expression<Func<Entity, bool>> predicate)
    {
        return _entities.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

}