using IRepository.IGenericRepositories;
using IServices.IApplicationServices.IPurpose;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Purpose;

public class DeleteApplicationPurposeService : IDeleteServicePurpose
{
    private readonly IDeleteRepository<ServicePurpose> _deleteRepository;

    public DeleteApplicationPurposeService(IDeleteRepository<ServicePurpose> deleteRepository)
    {
        _deleteRepository = deleteRepository;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException( );

        var deleted = await _deleteRepository.DeleteAsync(t => t.ServicePurposeId == id);

        return deleted > 0;
    }
}