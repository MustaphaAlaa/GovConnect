 
using ModelDTO.ApplicationDTOs.Category;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.Category;

public interface IGetAllServiceCategory :   IAsyncAllRecordsRetrieverService<ServiceCategory, ServiceCategoryDTO >
{
    
}