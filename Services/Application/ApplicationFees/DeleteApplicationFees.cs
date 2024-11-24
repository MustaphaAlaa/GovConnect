using IRepository;
using IServices.Application.Fees;
using ModelDTO.Application.Fees;
using Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Application.Fees;

public class DeleteApplicationFeesService : IDeleteApplicationFees
{
    private readonly IDeleteRepository<ApplicationFees> _deleteRepository;

    public DeleteApplicationFeesService(IDeleteRepository<ApplicationFees> deleteRepository)
    {
        _deleteRepository = deleteRepository;
    }

    public async Task<bool> DeleteAsync(CompositeKeyForApplicationFees id)
    {
        Expression<Func<ApplicationFees, bool>> expression =
            appFees => appFees.ApplicationForId == id.ApplicationForId
                        && appFees.ApplicationTypeId == id.ApplicationTypeId;

        var deleted = await _deleteRepository.DeleteAsync(expression);

        return deleted > 0;
    }
}
