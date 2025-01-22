using IRepository.IGenericRepositories;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;
using System.Linq.Expressions;

namespace Services.ApplicationServices.Fees;

public class DeleteServiceFeesService : IDeleteServiceFees
{
    private readonly IDeleteRepository<ServiceFees> _deleteRepository;

    public DeleteServiceFeesService(IDeleteRepository<ServiceFees> deleteRepository)
    {
        _deleteRepository = deleteRepository;
    }

    public async Task<bool> DeleteAsync(CompositeKeyForServiceFees id)
    {
        Expression<Func<ServiceFees, bool>> expression =
            appFees => appFees.ServiceCategoryId == id.ServiceCategoryId
                        && appFees.ServicePurposeId == id.ApplicationPurposeId;

        var deleted = await _deleteRepository.DeleteAsync(expression);

        return deleted > 0;
    }
}
