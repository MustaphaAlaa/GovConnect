using ModelDTO.Application.Type;
using Models.Applications;

namespace IServices.Application.Type;

public interface IGetAllApplicationTypes :   IGetAllService< ApplicationTypeDTO, ApplicationType >
{
    
}