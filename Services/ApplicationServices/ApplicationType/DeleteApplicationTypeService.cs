using IRepository;
using IServices.IApplicationServices.Type;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Type;

public class DeleteApplicationTypeService : IDeleteApplicationType
{
    private readonly IDeleteRepository<ApplicationType> _deleteRepository;

    public DeleteApplicationTypeService(IDeleteRepository<ApplicationType> deleteRepository)
    {
        _deleteRepository = deleteRepository;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException("Invalid InternationalDrivingLicenseId");

        var deleted = await _deleteRepository.DeleteAsync(t => t.Id == id);

        return deleted > 0;
    }
}