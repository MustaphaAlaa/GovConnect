using ModelDTO.ApplicationDTOs.Category;

namespace IServices.IApplicationServices.Category;

public interface IServiceCategoryService
{
    public Task<bool> IsValidServiceCategory();
}