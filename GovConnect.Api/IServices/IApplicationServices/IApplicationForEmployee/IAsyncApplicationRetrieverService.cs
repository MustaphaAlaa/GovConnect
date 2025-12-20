using ModelDTO.ApplicationDTOs.Employee;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.Employee;

public interface IAsyncApplicationRetrieverService : IAsyncRetrieveService<Application, ApplicationDTOForEmployee?>
{

}
