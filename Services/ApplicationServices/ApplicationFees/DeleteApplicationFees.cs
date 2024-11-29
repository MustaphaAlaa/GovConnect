using IRepository;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;
using System.Linq.Expressions;

namespace Services.ApplicationServices.Fees;

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
