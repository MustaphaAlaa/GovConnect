using Microsoft.EntityFrameworkCore;


namespace DataConfigurations;

public static class DbContextExtensions
{
    public static bool IsMigration(this DbContext  database)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Any(a => a.FullName?.Contains("Microsoft.EntityFrameworkCore.Design") ?? false);
    }
}


