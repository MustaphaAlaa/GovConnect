using DataConfigurations;

namespace Repositorties.SPs;

public abstract class SPBase
{
    protected GovConnectDbContext _context;

    protected SPBase(GovConnectDbContext context)
    {
        _context = context;
    }
}
