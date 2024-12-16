using IRepository;
using IServices.IApplicationServices.Category;
using Models.ApplicationModels;

namespace Services.ApplicationServices.For;

public class IDeleteServiceCategoryService : IDeleteServiceCategory
{
    private readonly IDeleteRepository<ServiceCategory> _deleteRepository;

    public IDeleteServiceCategoryService(IDeleteRepository<ServiceCategory> deleteRepository)
    {
        _deleteRepository = deleteRepository;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException("Invalid InternationalDrivingLicenseId");

        var deleted = await _deleteRepository.DeleteAsync(t => t.ServiceCategoryId == id);

        return deleted > 0;
    }
}