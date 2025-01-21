using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using DataConfigurations;
using Models.Users;


public abstract class RepositoryDbContext
{
    protected readonly GovConnectDbContext _db;


    public RepositoryDbContext(GovConnectDbContext context)
    {
        _db = context;
    }

    protected async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }
}