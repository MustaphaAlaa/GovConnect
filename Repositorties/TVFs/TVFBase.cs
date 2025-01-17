using DataConfigurations;

namespace Repositorties.TVFs;

public abstract class TVFBase
{
    protected GovConnectDbContext _context;

    protected TVFBase(GovConnectDbContext context)
    {
        _context = context;
    }
}
