using ModelDTO.ApplicationDTOs.Employee;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.Employee;

public interface IGetApplcationForEmployee : IGetWhenService<Application, ApplicationDTOForEmployee?>
{

}
