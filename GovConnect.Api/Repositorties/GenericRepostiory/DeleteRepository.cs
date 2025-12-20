using System.Linq.Expressions;
using DataConfigurations;
using IRepository.IGenericRepositories;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// Repository for deleting data from the database.
/// </summary>
/// <typeparam name="T">The type of the entity to be deleted.</typeparam>
public class DeleteRepository<T> : RepositoryDbContext, IDeleteRepository<T> where T : class
{
    /// <summary>
    /// The DbSet representing the entity.
    /// </summary>
    protected DbSet<T> _entity;

    /// <summary>
    /// Initializes a new instance of the  DeleteRepository<T> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteRepository(GovConnectDbContext context) : base(context)
    {
        _entity = this._db.Set<T>();
    }

    /// <summary>
    /// Asynchronously deletes an entity from the database based on a predicate.
    /// </summary>
    /// <param name="predicate">The condition to find the entity to be deleted.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var entity = _entity.AsNoTracking().FirstOrDefault(predicate);

        if (entity == null)
            return 0;

        _entity.Remove(entity);
        return await this.SaveChangesAsync();
    }
}
