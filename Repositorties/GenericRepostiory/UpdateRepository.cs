using DataConfigurations;
using IRepository.IGenericRepositories;
using Microsoft.EntityFrameworkCore;

public class UpdateRepository<Entity> : RepositoryDbContext, IUpdateRepository<Entity> where Entity : class
{
    protected DbSet<Entity> _entity;

    public UpdateRepository(GovConnectDbContext context) : base(context)
    {
        _entity = this._db.Set<Entity>();
    }

    public async Task<Entity> UpdateAsync(Entity entity)
    {
        this._entity.Update(entity);
        await this.SaveChangesAsync();
        return entity;
    }
}