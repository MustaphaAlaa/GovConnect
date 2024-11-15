using DataConfigurations;
using IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repositorties;

public class CreateRepository<Entity> : RepositoryDbContext,
    ICreateRepository<Entity> where Entity : class
{
    protected DbSet<Entity> _entity;

    public CreateRepository(DVLDDbContext context) : base(context)
    {
        _entity = context.Set<Entity>();
    }

    public async Task<Entity> CreateAsync(Entity entity)
    {
        await _entity.AddAsync(entity);
        await this.SaveChangesAsync();
        return entity;
    }
}